﻿// Decompiled with JetBrains decompiler
// Type: System.Threading.PinnableBufferCache
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Security;
using System.Security.Permissions;

namespace System.Threading
{
  internal sealed class PinnableBufferCache
  {
    private ConcurrentStack<object> m_FreeList = new ConcurrentStack<object>();
    private const int DefaultNumberOfBuffers = 16;
    private string m_CacheName;
    private Func<object> m_factory;
    private List<object> m_NotGen2;
    private int m_gen1CountAtLastRestock;
    private int m_msecNoUseBeyondFreeListSinceThisTime;
    private bool m_moreThanFreeListNeeded;
    private int m_buffersUnderManagement;
    private int m_restockSize;
    private bool m_trimmingExperimentInProgress;
    private int m_minBufferCount;
    private int m_numAllocCalls;

    public PinnableBufferCache(string cacheName, int numberOfElements)
      : this(cacheName, (Func<object>) (() => (object) new byte[numberOfElements]))
    {
    }

    [SecuritySafeCritical]
    [EnvironmentPermission(SecurityAction.Assert, Unrestricted = true)]
    internal PinnableBufferCache(string cacheName, Func<object> factory)
    {
      this.m_NotGen2 = new List<object>(16);
      this.m_factory = factory;
      string variable1 = "PinnableBufferCache_" + cacheName + "_Disabled";
      try
      {
        string environmentVariable = Environment.GetEnvironmentVariable(variable1);
        if (environmentVariable != null)
        {
          PinnableBufferCacheEventSource.Log.DebugMessage("Creating " + cacheName + " PinnableBufferCacheDisabled=" + environmentVariable);
          if (0 <= environmentVariable.IndexOf(cacheName, StringComparison.OrdinalIgnoreCase))
          {
            PinnableBufferCacheEventSource.Log.DebugMessage("Disabling " + cacheName);
            return;
          }
        }
      }
      catch
      {
      }
      string variable2 = "PinnableBufferCache_" + cacheName + "_MinCount";
      try
      {
        string environmentVariable = Environment.GetEnvironmentVariable(variable2);
        if (environmentVariable != null)
        {
          if (int.TryParse(environmentVariable, out this.m_minBufferCount))
            this.CreateNewBuffers();
        }
      }
      catch
      {
      }
      PinnableBufferCacheEventSource.Log.Create(cacheName);
      this.m_CacheName = cacheName;
    }

    public byte[] AllocateBuffer()
    {
      return (byte[]) this.Allocate();
    }

    public void FreeBuffer(byte[] buffer)
    {
      this.Free((object) buffer);
    }

    [SecuritySafeCritical]
    internal object Allocate()
    {
      if (this.m_CacheName == null)
        return this.m_factory();
      object obj;
      if (!this.m_FreeList.TryPop(out obj))
        this.Restock(out obj);
      if (PinnableBufferCacheEventSource.Log.IsEnabled())
      {
        if (Interlocked.Increment(ref this.m_numAllocCalls) >= 1024)
        {
          lock (this)
          {
            if (Interlocked.Exchange(ref this.m_numAllocCalls, 0) >= 1024)
            {
              int local_3 = 0;
              foreach (object item_0 in this.m_FreeList)
              {
                if (GC.GetGeneration(item_0) < GC.MaxGeneration)
                  ++local_3;
              }
              PinnableBufferCacheEventSource.Log.WalkFreeListResult(this.m_CacheName, this.m_FreeList.Count, local_3);
            }
          }
        }
        PinnableBufferCacheEventSource.Log.AllocateBuffer(this.m_CacheName, PinnableBufferCacheEventSource.AddressOf(obj), obj.GetHashCode(), GC.GetGeneration(obj), this.m_FreeList.Count);
      }
      return obj;
    }

    [SecuritySafeCritical]
    internal void Free(object buffer)
    {
      if (this.m_CacheName == null)
        return;
      if (PinnableBufferCacheEventSource.Log.IsEnabled())
        PinnableBufferCacheEventSource.Log.FreeBuffer(this.m_CacheName, PinnableBufferCacheEventSource.AddressOf(buffer), buffer.GetHashCode(), this.m_FreeList.Count);
      if (this.m_gen1CountAtLastRestock + 3 > GC.CollectionCount(GC.MaxGeneration - 1))
      {
        lock (this)
        {
          if (GC.GetGeneration(buffer) < GC.MaxGeneration)
          {
            this.m_moreThanFreeListNeeded = true;
            PinnableBufferCacheEventSource.Log.FreeBufferStillTooYoung(this.m_CacheName, this.m_NotGen2.Count);
            this.m_NotGen2.Add(buffer);
            this.m_gen1CountAtLastRestock = GC.CollectionCount(GC.MaxGeneration - 1);
            return;
          }
        }
      }
      this.m_FreeList.Push(buffer);
    }

    [SecuritySafeCritical]
    private void Restock(out object returnBuffer)
    {
      lock (this)
      {
        if (this.m_FreeList.TryPop(out returnBuffer))
          return;
        if (this.m_restockSize == 0)
          Gen2GcCallback.Register(new Func<object, bool>(PinnableBufferCache.Gen2GcCallbackFunc), (object) this);
        this.m_moreThanFreeListNeeded = true;
        PinnableBufferCacheEventSource.Log.AllocateBufferFreeListEmpty(this.m_CacheName, this.m_NotGen2.Count);
        if (this.m_NotGen2.Count == 0)
          this.CreateNewBuffers();
        int local_2 = this.m_NotGen2.Count - 1;
        if (GC.GetGeneration(this.m_NotGen2[local_2]) < GC.MaxGeneration && GC.GetGeneration(this.m_NotGen2[0]) == GC.MaxGeneration)
          local_2 = 0;
        returnBuffer = this.m_NotGen2[local_2];
        this.m_NotGen2.RemoveAt(local_2);
        if (PinnableBufferCacheEventSource.Log.IsEnabled() && GC.GetGeneration(returnBuffer) < GC.MaxGeneration)
          PinnableBufferCacheEventSource.Log.AllocateBufferFromNotGen2(this.m_CacheName, this.m_NotGen2.Count);
        if (this.AgePendingBuffers() || this.m_NotGen2.Count != this.m_restockSize / 2)
          return;
        PinnableBufferCacheEventSource.Log.DebugMessage("Proactively adding more buffers to aging pool");
        this.CreateNewBuffers();
      }
    }

    [SecuritySafeCritical]
    private bool AgePendingBuffers()
    {
      if (this.m_gen1CountAtLastRestock >= GC.CollectionCount(GC.MaxGeneration - 1))
        return false;
      int promotedToFreeListCount = 0;
      List<object> objectList = new List<object>();
      PinnableBufferCacheEventSource.Log.AllocateBufferAged(this.m_CacheName, this.m_NotGen2.Count);
      for (int index = 0; index < this.m_NotGen2.Count; ++index)
      {
        object obj = this.m_NotGen2[index];
        if (GC.GetGeneration(obj) >= GC.MaxGeneration)
        {
          this.m_FreeList.Push(obj);
          ++promotedToFreeListCount;
        }
        else
          objectList.Add(obj);
      }
      PinnableBufferCacheEventSource.Log.AgePendingBuffersResults(this.m_CacheName, promotedToFreeListCount, objectList.Count);
      this.m_NotGen2 = objectList;
      return true;
    }

    private void CreateNewBuffers()
    {
      this.m_restockSize = this.m_restockSize != 0 ? (this.m_restockSize >= 16 ? (this.m_restockSize >= 256 ? (this.m_restockSize >= 4096 ? 4096 : this.m_restockSize * 3 / 2) : this.m_restockSize * 2) : 16) : 4;
      if (this.m_minBufferCount > this.m_buffersUnderManagement)
        this.m_restockSize = Math.Max(this.m_restockSize, this.m_minBufferCount - this.m_buffersUnderManagement);
      PinnableBufferCacheEventSource.Log.AllocateBufferCreatingNewBuffers(this.m_CacheName, this.m_buffersUnderManagement, this.m_restockSize);
      for (int index = 0; index < this.m_restockSize; ++index)
      {
        object obj1 = this.m_factory();
        object obj2 = new object();
        this.m_NotGen2.Add(obj1);
      }
      this.m_buffersUnderManagement = this.m_buffersUnderManagement + this.m_restockSize;
      this.m_gen1CountAtLastRestock = GC.CollectionCount(GC.MaxGeneration - 1);
    }

    [SecuritySafeCritical]
    private static bool Gen2GcCallbackFunc(object targetObj)
    {
      return ((PinnableBufferCache) targetObj).TrimFreeListIfNeeded();
    }

    [SecuritySafeCritical]
    private bool TrimFreeListIfNeeded()
    {
      int tickCount = Environment.TickCount;
      int deltaMSec = tickCount - this.m_msecNoUseBeyondFreeListSinceThisTime;
      PinnableBufferCacheEventSource.Log.TrimCheck(this.m_CacheName, this.m_buffersUnderManagement, this.m_moreThanFreeListNeeded, deltaMSec);
      if (this.m_moreThanFreeListNeeded)
      {
        this.m_moreThanFreeListNeeded = false;
        this.m_trimmingExperimentInProgress = false;
        this.m_msecNoUseBeyondFreeListSinceThisTime = tickCount;
        return true;
      }
      if (0 <= deltaMSec && deltaMSec < 10000)
        return true;
      lock (this)
      {
        if (this.m_moreThanFreeListNeeded)
        {
          this.m_moreThanFreeListNeeded = false;
          this.m_trimmingExperimentInProgress = false;
          this.m_msecNoUseBeyondFreeListSinceThisTime = tickCount;
          return true;
        }
        int local_4 = this.m_FreeList.Count;
        if (this.m_NotGen2.Count > 0)
        {
          if (!this.m_trimmingExperimentInProgress)
          {
            PinnableBufferCacheEventSource.Log.TrimFlush(this.m_CacheName, this.m_buffersUnderManagement, local_4, this.m_NotGen2.Count);
            this.AgePendingBuffers();
            this.m_trimmingExperimentInProgress = true;
            return true;
          }
          PinnableBufferCacheEventSource.Log.TrimFree(this.m_CacheName, this.m_buffersUnderManagement, local_4, this.m_NotGen2.Count);
          this.m_buffersUnderManagement = this.m_buffersUnderManagement - this.m_NotGen2.Count;
          int local_8 = this.m_buffersUnderManagement / 4;
          if (local_8 < this.m_restockSize)
            this.m_restockSize = Math.Max(local_8, 16);
          this.m_NotGen2.Clear();
          this.m_trimmingExperimentInProgress = false;
          return true;
        }
        int local_5 = local_4 / 4 + 1;
        if (local_4 * 15 <= this.m_buffersUnderManagement || this.m_buffersUnderManagement - local_5 <= this.m_minBufferCount)
        {
          PinnableBufferCacheEventSource.Log.TrimFreeSizeOK(this.m_CacheName, this.m_buffersUnderManagement, local_4);
          return true;
        }
        PinnableBufferCacheEventSource.Log.TrimExperiment(this.m_CacheName, this.m_buffersUnderManagement, local_4, local_5);
        for (int local_9 = 0; local_9 < local_5; ++local_9)
        {
          object local_6;
          if (this.m_FreeList.TryPop(out local_6))
            this.m_NotGen2.Add(local_6);
        }
        this.m_msecNoUseBeyondFreeListSinceThisTime = tickCount;
        this.m_trimmingExperimentInProgress = true;
      }
      return true;
    }
  }
}
