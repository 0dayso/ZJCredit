// Decompiled with JetBrains decompiler
// Type: System.Security.Principal.IdentityReferenceEnumerator
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System.Security.Principal
{
  [ComVisible(false)]
  internal class IdentityReferenceEnumerator : IEnumerator<IdentityReference>, IDisposable, IEnumerator
  {
    private int _Current;
    private readonly IdentityReferenceCollection _Collection;

    object IEnumerator.Current
    {
      get
      {
        return (object) this._Collection.Identities[this._Current];
      }
    }

    public IdentityReference Current
    {
      get
      {
        return ((IEnumerator) this).Current as IdentityReference;
      }
    }

    internal IdentityReferenceEnumerator(IdentityReferenceCollection collection)
    {
      if (collection == null)
        throw new ArgumentNullException("collection");
      this._Collection = collection;
      this._Current = -1;
    }

    public bool MoveNext()
    {
      this._Current = this._Current + 1;
      return this._Current < this._Collection.Count;
    }

    public void Reset()
    {
      this._Current = -1;
    }

    public void Dispose()
    {
    }
  }
}
