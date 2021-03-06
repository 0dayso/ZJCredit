﻿// Decompiled with JetBrains decompiler
// Type: System.Security.PermissionSetEnumerator
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;

namespace System.Security
{
  internal class PermissionSetEnumerator : IEnumerator
  {
    private PermissionSetEnumeratorInternal enm;

    public object Current
    {
      get
      {
        return this.enm.Current;
      }
    }

    internal PermissionSetEnumerator(PermissionSet permSet)
    {
      this.enm = new PermissionSetEnumeratorInternal(permSet);
    }

    public bool MoveNext()
    {
      return this.enm.MoveNext();
    }

    public void Reset()
    {
      this.enm.Reset();
    }
  }
}
