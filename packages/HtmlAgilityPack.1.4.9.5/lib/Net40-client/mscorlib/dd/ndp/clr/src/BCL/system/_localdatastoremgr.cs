// Decompiled with JetBrains decompiler
// Type: System.LocalDataStoreSlot
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System
{
  /// <summary>封装内存槽以存储本地数据。此类不能被继承。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  public sealed class LocalDataStoreSlot
  {
    private LocalDataStoreMgr m_mgr;
    private int m_slot;
    private long m_cookie;

    internal LocalDataStoreMgr Manager
    {
      get
      {
        return this.m_mgr;
      }
    }

    internal int Slot
    {
      get
      {
        return this.m_slot;
      }
    }

    internal long Cookie
    {
      get
      {
        return this.m_cookie;
      }
    }

    internal LocalDataStoreSlot(LocalDataStoreMgr mgr, int slot, long cookie)
    {
      this.m_mgr = mgr;
      this.m_slot = slot;
      this.m_cookie = cookie;
    }

    /// <summary>确保释放资源并在垃圾回收器回收时执行其他清理操作<see cref="T:System.LocalDataStoreSlot" />对象。</summary>
    ~LocalDataStoreSlot()
    {
      LocalDataStoreMgr localDataStoreMgr = this.m_mgr;
      if (localDataStoreMgr == null)
        return;
      int slot = this.m_slot;
      this.m_slot = -1;
      localDataStoreMgr.FreeDataSlot(slot, this.m_cookie);
    }
  }
}
