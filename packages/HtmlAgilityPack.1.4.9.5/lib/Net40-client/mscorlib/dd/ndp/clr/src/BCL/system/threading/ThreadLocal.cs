// Decompiled with JetBrains decompiler
// Type: System.Threading.ThreadLocal`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Permissions;

namespace System.Threading
{
  /// <summary>提供数据的线程本地存储。</summary>
  /// <typeparam name="T">指定每线程的已存储数据的类型。</typeparam>
  [DebuggerTypeProxy(typeof (SystemThreading_ThreadLocalDebugView<>))]
  [DebuggerDisplay("IsValueCreated={IsValueCreated}, Value={ValueForDebugDisplay}, Count={ValuesCountForDebugDisplay}")]
  [__DynamicallyInvokable]
  [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
  public class ThreadLocal<T> : IDisposable
  {
    private static ThreadLocal<T>.IdManager s_idManager = new ThreadLocal<T>.IdManager();
    private ThreadLocal<T>.LinkedSlot m_linkedSlot = new ThreadLocal<T>.LinkedSlot((ThreadLocal<T>.LinkedSlotVolatile[]) null);
    private Func<T> m_valueFactory;
    [ThreadStatic]
    private static ThreadLocal<T>.LinkedSlotVolatile[] ts_slotArray;
    [ThreadStatic]
    private static ThreadLocal<T>.FinalizationHelper ts_finalizationHelper;
    private int m_idComplement;
    private volatile bool m_initialized;
    private bool m_trackAllValues;

    /// <summary>获取或设置当前线程的此实例的值。</summary>
    /// <returns>返回此 ThreadLocal 负责初始化的对象的实例。</returns>
    /// <exception cref="T:System.ObjectDisposedException">已释放 <see cref="T:System.Threading.ThreadLocal`1" /> 实例。</exception>
    /// <exception cref="T:System.InvalidOperationException">初始化函数尝试以递归方式引用 <see cref="P:System.Threading.ThreadLocal`1.Value" />。</exception>
    /// <exception cref="T:System.MissingMemberException">没有提供默认构造函数，且没有提供值工厂。</exception>
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    [__DynamicallyInvokable]
    public T Value
    {
      [__DynamicallyInvokable] get
      {
        ThreadLocal<T>.LinkedSlotVolatile[] linkedSlotVolatileArray = ThreadLocal<T>.ts_slotArray;
        int index = ~this.m_idComplement;
        ThreadLocal<T>.LinkedSlot linkedSlot;
        if (linkedSlotVolatileArray != null && index >= 0 && (index < linkedSlotVolatileArray.Length && (linkedSlot = linkedSlotVolatileArray[index].Value) != null) && this.m_initialized)
          return linkedSlot.Value;
        return this.GetValueSlow();
      }
      [__DynamicallyInvokable] set
      {
        ThreadLocal<T>.LinkedSlotVolatile[] slotArray = ThreadLocal<T>.ts_slotArray;
        int index = ~this.m_idComplement;
        ThreadLocal<T>.LinkedSlot linkedSlot;
        if (slotArray != null && index >= 0 && (index < slotArray.Length && (linkedSlot = slotArray[index].Value) != null) && this.m_initialized)
          linkedSlot.Value = value;
        else
          this.SetValueSlow(value, slotArray);
      }
    }

    /// <summary>获取当前由已经访问此实例的所有线程存储的所有值的列表。</summary>
    /// <returns>访问此实例由所有线程存储的当前的所有值的列表。</returns>
    /// <exception cref="T:System.ObjectDisposedException">已释放 <see cref="T:System.Threading.ThreadLocal`1" /> 实例。</exception>
    [__DynamicallyInvokable]
    public IList<T> Values
    {
      [__DynamicallyInvokable] get
      {
        if (!this.m_trackAllValues)
          throw new InvalidOperationException(Environment.GetResourceString("ThreadLocal_ValuesNotAvailable"));
        List<T> valuesAsList = this.GetValuesAsList();
        if (valuesAsList != null)
          return (IList<T>) valuesAsList;
        throw new ObjectDisposedException(Environment.GetResourceString("ThreadLocal_Disposed"));
      }
    }

    private int ValuesCountForDebugDisplay
    {
      get
      {
        int num = 0;
        for (ThreadLocal<T>.LinkedSlot linkedSlot = this.m_linkedSlot.Next; linkedSlot != null; linkedSlot = linkedSlot.Next)
          ++num;
        return num;
      }
    }

    /// <summary>获取是否在当前线程上初始化 <see cref="P:System.Threading.ThreadLocal`1.Value" />。</summary>
    /// <returns>如果在当前线程上初始化 <see cref="P:System.Threading.ThreadLocal`1.Value" />，则为 true；否则为 false。</returns>
    /// <exception cref="T:System.ObjectDisposedException">已释放 <see cref="T:System.Threading.ThreadLocal`1" /> 实例。</exception>
    [__DynamicallyInvokable]
    public bool IsValueCreated
    {
      [__DynamicallyInvokable] get
      {
        int index = ~this.m_idComplement;
        if (index < 0)
          throw new ObjectDisposedException(Environment.GetResourceString("ThreadLocal_Disposed"));
        ThreadLocal<T>.LinkedSlotVolatile[] linkedSlotVolatileArray = ThreadLocal<T>.ts_slotArray;
        if (linkedSlotVolatileArray != null && index < linkedSlotVolatileArray.Length)
          return linkedSlotVolatileArray[index].Value != null;
        return false;
      }
    }

    internal T ValueForDebugDisplay
    {
      get
      {
        ThreadLocal<T>.LinkedSlotVolatile[] linkedSlotVolatileArray = ThreadLocal<T>.ts_slotArray;
        int index = ~this.m_idComplement;
        ThreadLocal<T>.LinkedSlot linkedSlot;
        if (linkedSlotVolatileArray == null || index >= linkedSlotVolatileArray.Length || ((linkedSlot = linkedSlotVolatileArray[index].Value) == null || !this.m_initialized))
          return default (T);
        return linkedSlot.Value;
      }
    }

    internal List<T> ValuesForDebugDisplay
    {
      get
      {
        return this.GetValuesAsList();
      }
    }

    /// <summary>初始化 <see cref="T:System.Threading.ThreadLocal`1" /> 实例。</summary>
    [__DynamicallyInvokable]
    public ThreadLocal()
    {
      this.Initialize((Func<T>) null, false);
    }

    /// <summary>初始化 <see cref="T:System.Threading.ThreadLocal`1" /> 实例。</summary>
    /// <param name="trackAllValues">是否要跟踪实例上的所有值集并通过 <see cref="P:System.Threading.ThreadLocal`1.Values" /> 属性将其公开。</param>
    [__DynamicallyInvokable]
    public ThreadLocal(bool trackAllValues)
    {
      this.Initialize((Func<T>) null, trackAllValues);
    }

    /// <summary>使用指定的 <paramref name="valueFactory" /> 函数初始化 <see cref="T:System.Threading.ThreadLocal`1" /> 实例。</summary>
    /// <param name="valueFactory">如果在 <see cref="P:System.Threading.ThreadLocal`1.Value" /> 之前尚未初始化的情况下尝试对其进行检索，则会调用 <see cref="T:System.Func`1" /> 生成延迟初始化的值。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="valueFactory" /> 是 null 引用（在 Visual Basic 中为 Nothing）。</exception>
    [__DynamicallyInvokable]
    public ThreadLocal(Func<T> valueFactory)
    {
      if (valueFactory == null)
        throw new ArgumentNullException("valueFactory");
      this.Initialize(valueFactory, false);
    }

    /// <summary>使用指定的 <paramref name="valueFactory" /> 函数初始化 <see cref="T:System.Threading.ThreadLocal`1" /> 实例。</summary>
    /// <param name="valueFactory">如果在 <see cref="P:System.Threading.ThreadLocal`1.Value" /> 之前尚未初始化的情况下尝试对其进行检索，则会调用 <see cref="T:System.Func`1" /> 生成延迟初始化的值。</param>
    /// <param name="trackAllValues">是否要跟踪实例上的所有值集并通过 <see cref="P:System.Threading.ThreadLocal`1.Values" /> 属性将其公开。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="valueFactory" /> 为 null 引用（在 Visual Basic 中为 Nothing）。</exception>
    [__DynamicallyInvokable]
    public ThreadLocal(Func<T> valueFactory, bool trackAllValues)
    {
      if (valueFactory == null)
        throw new ArgumentNullException("valueFactory");
      this.Initialize(valueFactory, trackAllValues);
    }

    /// <summary>释放此 <see cref="T:System.Threading.ThreadLocal`1" /> 实例使用的资源。</summary>
    [__DynamicallyInvokable]
    ~ThreadLocal()
    {
      this.Dispose(false);
    }

    private void Initialize(Func<T> valueFactory, bool trackAllValues)
    {
      this.m_valueFactory = valueFactory;
      this.m_trackAllValues = trackAllValues;
      try
      {
      }
      finally
      {
        this.m_idComplement = ~ThreadLocal<T>.s_idManager.GetId();
        this.m_initialized = true;
      }
    }

    /// <summary>释放由 <see cref="T:System.Threading.ThreadLocal`1" /> 类的当前实例占用的所有资源。</summary>
    [__DynamicallyInvokable]
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    /// <summary>释放此 <see cref="T:System.Threading.ThreadLocal`1" /> 实例使用的资源。</summary>
    /// <param name="disposing">一个布尔值，该值指示是否由于调用 <see cref="M:System.Threading.ThreadLocal`1.Dispose" /> 的原因而调用此方法。</param>
    [__DynamicallyInvokable]
    protected virtual void Dispose(bool disposing)
    {
      int id;
      lock (ThreadLocal<T>.s_idManager)
      {
        id = ~this.m_idComplement;
        this.m_idComplement = 0;
        if (id < 0 || !this.m_initialized)
          return;
        this.m_initialized = false;
        for (ThreadLocal<T>.LinkedSlot local_3 = this.m_linkedSlot.Next; local_3 != null; local_3 = local_3.Next)
        {
          ThreadLocal<T>.LinkedSlotVolatile[] local_4 = local_3.SlotArray;
          if (local_4 != null)
          {
            local_3.SlotArray = (ThreadLocal<T>.LinkedSlotVolatile[]) null;
            local_4[id].Value.Value = default (T);
            local_4[id].Value = (ThreadLocal<T>.LinkedSlot) null;
          }
        }
      }
      this.m_linkedSlot = (ThreadLocal<T>.LinkedSlot) null;
      ThreadLocal<T>.s_idManager.ReturnId(id);
    }

    /// <summary>创建并返回当前线程的此实例的字符串表示形式。</summary>
    /// <returns>对 <see cref="P:System.Threading.ThreadLocal`1.Value" /> 调用 <see cref="M:System.Object.ToString" /> 的结果。</returns>
    /// <exception cref="T:System.ObjectDisposedException">已释放 <see cref="T:System.Threading.ThreadLocal`1" /> 实例。</exception>
    /// <exception cref="T:System.NullReferenceException">当前线程的 <see cref="P:System.Threading.ThreadLocal`1.Value" /> 为 null 引用（Visual Basic 中为 Nothing）。</exception>
    /// <exception cref="T:System.InvalidOperationException">初始化函数尝试以递归方式引用 <see cref="P:System.Threading.ThreadLocal`1.Value" />。</exception>
    /// <exception cref="T:System.MissingMemberException">没有提供默认构造函数，且没有提供值工厂。</exception>
    [__DynamicallyInvokable]
    public override string ToString()
    {
      return this.Value.ToString();
    }

    private T GetValueSlow()
    {
      if (~this.m_idComplement < 0)
        throw new ObjectDisposedException(Environment.GetResourceString("ThreadLocal_Disposed"));
      Debugger.NotifyOfCrossThreadDependency();
      T obj;
      if (this.m_valueFactory == null)
      {
        obj = default (T);
      }
      else
      {
        obj = this.m_valueFactory();
        if (this.IsValueCreated)
          throw new InvalidOperationException(Environment.GetResourceString("ThreadLocal_Value_RecursiveCallsToValue"));
      }
      this.Value = obj;
      return obj;
    }

    private void SetValueSlow(T value, ThreadLocal<T>.LinkedSlotVolatile[] slotArray)
    {
      int id = ~this.m_idComplement;
      if (id < 0)
        throw new ObjectDisposedException(Environment.GetResourceString("ThreadLocal_Disposed"));
      if (slotArray == null)
      {
        slotArray = new ThreadLocal<T>.LinkedSlotVolatile[ThreadLocal<T>.GetNewTableSize(id + 1)];
        ThreadLocal<T>.ts_finalizationHelper = new ThreadLocal<T>.FinalizationHelper(slotArray, this.m_trackAllValues);
        ThreadLocal<T>.ts_slotArray = slotArray;
      }
      if (id >= slotArray.Length)
      {
        this.GrowTable(ref slotArray, id + 1);
        ThreadLocal<T>.ts_finalizationHelper.SlotArray = slotArray;
        ThreadLocal<T>.ts_slotArray = slotArray;
      }
      if (slotArray[id].Value == null)
      {
        this.CreateLinkedSlot(slotArray, id, value);
      }
      else
      {
        ThreadLocal<T>.LinkedSlot linkedSlot = slotArray[id].Value;
        if (!this.m_initialized)
          throw new ObjectDisposedException(Environment.GetResourceString("ThreadLocal_Disposed"));
        T obj = value;
        linkedSlot.Value = obj;
      }
    }

    private void CreateLinkedSlot(ThreadLocal<T>.LinkedSlotVolatile[] slotArray, int id, T value)
    {
      ThreadLocal<T>.LinkedSlot linkedSlot = new ThreadLocal<T>.LinkedSlot(slotArray);
      lock (ThreadLocal<T>.s_idManager)
      {
        if (!this.m_initialized)
          throw new ObjectDisposedException(Environment.GetResourceString("ThreadLocal_Disposed"));
        ThreadLocal<T>.LinkedSlot local_3 = this.m_linkedSlot.Next;
        linkedSlot.Next = local_3;
        linkedSlot.Previous = this.m_linkedSlot;
        linkedSlot.Value = value;
        if (local_3 != null)
          local_3.Previous = linkedSlot;
        this.m_linkedSlot.Next = linkedSlot;
        slotArray[id].Value = linkedSlot;
      }
    }

    private List<T> GetValuesAsList()
    {
      List<T> objList = new List<T>();
      if (~this.m_idComplement == -1)
        return (List<T>) null;
      for (ThreadLocal<T>.LinkedSlot linkedSlot = this.m_linkedSlot.Next; linkedSlot != null; linkedSlot = linkedSlot.Next)
        objList.Add(linkedSlot.Value);
      return objList;
    }

    private void GrowTable(ref ThreadLocal<T>.LinkedSlotVolatile[] table, int minLength)
    {
      ThreadLocal<T>.LinkedSlotVolatile[] linkedSlotVolatileArray = new ThreadLocal<T>.LinkedSlotVolatile[ThreadLocal<T>.GetNewTableSize(minLength)];
      lock (ThreadLocal<T>.s_idManager)
      {
        for (int local_3 = 0; local_3 < table.Length; ++local_3)
        {
          ThreadLocal<T>.LinkedSlot local_4 = table[local_3].Value;
          if (local_4 != null && local_4.SlotArray != null)
          {
            local_4.SlotArray = linkedSlotVolatileArray;
            linkedSlotVolatileArray[local_3] = table[local_3];
          }
        }
      }
      table = linkedSlotVolatileArray;
    }

    private static int GetNewTableSize(int minSize)
    {
      if ((uint) minSize > 2146435071U)
        return int.MaxValue;
      int num1 = minSize - 1;
      int num2 = 1;
      int num3 = num1 >> num2;
      int num4 = num1 | num3;
      int num5 = 2;
      int num6 = num4 >> num5;
      int num7 = num4 | num6;
      int num8 = 4;
      int num9 = num7 >> num8;
      int num10 = num7 | num9;
      int num11 = 8;
      int num12 = num10 >> num11;
      int num13 = num10 | num12;
      int num14 = 16;
      int num15 = num13 >> num14;
      int num16 = (num13 | num15) + 1;
      if ((uint) num16 > 2146435071U)
        num16 = 2146435071;
      return num16;
    }

    private struct LinkedSlotVolatile
    {
      internal volatile ThreadLocal<T>.LinkedSlot Value;
    }

    private sealed class LinkedSlot
    {
      internal volatile ThreadLocal<T>.LinkedSlot Next;
      internal volatile ThreadLocal<T>.LinkedSlot Previous;
      internal volatile ThreadLocal<T>.LinkedSlotVolatile[] SlotArray;
      internal T Value;

      internal LinkedSlot(ThreadLocal<T>.LinkedSlotVolatile[] slotArray)
      {
        this.SlotArray = slotArray;
      }
    }

    private class IdManager
    {
      private List<bool> m_freeIds = new List<bool>();
      private int m_nextIdToTry;

      internal int GetId()
      {
        lock (this.m_freeIds)
        {
          int local_2 = this.m_nextIdToTry;
          while (local_2 < this.m_freeIds.Count && !this.m_freeIds[local_2])
            ++local_2;
          if (local_2 == this.m_freeIds.Count)
            this.m_freeIds.Add(false);
          else
            this.m_freeIds[local_2] = false;
          this.m_nextIdToTry = local_2 + 1;
          return local_2;
        }
      }

      internal void ReturnId(int id)
      {
        lock (this.m_freeIds)
        {
          this.m_freeIds[id] = true;
          if (id >= this.m_nextIdToTry)
            return;
          this.m_nextIdToTry = id;
        }
      }
    }

    private class FinalizationHelper
    {
      internal ThreadLocal<T>.LinkedSlotVolatile[] SlotArray;
      private bool m_trackAllValues;

      internal FinalizationHelper(ThreadLocal<T>.LinkedSlotVolatile[] slotArray, bool trackAllValues)
      {
        this.SlotArray = slotArray;
        this.m_trackAllValues = trackAllValues;
      }

      ~FinalizationHelper()
      {
        foreach (ThreadLocal<T>.LinkedSlotVolatile slot in this.SlotArray)
        {
          ThreadLocal<T>.LinkedSlot linkedSlot = slot.Value;
          if (linkedSlot != null)
          {
            if (this.m_trackAllValues)
            {
              linkedSlot.SlotArray = (ThreadLocal<T>.LinkedSlotVolatile[]) null;
            }
            else
            {
              lock (ThreadLocal<T>.s_idManager)
              {
                if (linkedSlot.Next != null)
                  linkedSlot.Next.Previous = linkedSlot.Previous;
                linkedSlot.Previous.Next = linkedSlot.Next;
              }
            }
          }
        }
      }
    }
  }
}
