// Decompiled with JetBrains decompiler
// Type: System.Deployment.Internal.Isolation.StoreCategoryEnumeration
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
  internal class StoreCategoryEnumeration : IEnumerator
  {
    private IEnumSTORE_CATEGORY _enum;
    private bool _fValid;
    private STORE_CATEGORY _current;

    object IEnumerator.Current
    {
      get
      {
        return (object) this.GetCurrent();
      }
    }

    public STORE_CATEGORY Current
    {
      get
      {
        return this.GetCurrent();
      }
    }

    public StoreCategoryEnumeration(IEnumSTORE_CATEGORY pI)
    {
      this._enum = pI;
    }

    public IEnumerator GetEnumerator()
    {
      return (IEnumerator) this;
    }

    private STORE_CATEGORY GetCurrent()
    {
      if (!this._fValid)
        throw new InvalidOperationException();
      return this._current;
    }

    [SecuritySafeCritical]
    public bool MoveNext()
    {
      STORE_CATEGORY[] rgElements = new STORE_CATEGORY[1];
      uint num = this._enum.Next(1U, rgElements);
      if ((int) num == 1)
        this._current = rgElements[0];
      return this._fValid = (int) num == 1;
    }

    [SecuritySafeCritical]
    public void Reset()
    {
      this._fValid = false;
      this._enum.Reset();
    }
  }
}
