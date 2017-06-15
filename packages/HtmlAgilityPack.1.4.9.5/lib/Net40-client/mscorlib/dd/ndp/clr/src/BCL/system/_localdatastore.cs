// Decompiled with JetBrains decompiler
// Type: System.LocalDataStoreHolder
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System
{
  internal sealed class LocalDataStoreHolder
  {
    private LocalDataStore m_Store;

    public LocalDataStore Store
    {
      get
      {
        return this.m_Store;
      }
    }

    public LocalDataStoreHolder(LocalDataStore store)
    {
      this.m_Store = store;
    }

    ~LocalDataStoreHolder()
    {
      LocalDataStore localDataStore = this.m_Store;
      if (localDataStore == null)
        return;
      localDataStore.Dispose();
    }
  }
}
