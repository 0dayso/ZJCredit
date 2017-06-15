// Decompiled with JetBrains decompiler
// Type: System.Security.PermissionSetEnumeratorInternal
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security.Util;

namespace System.Security
{
  internal struct PermissionSetEnumeratorInternal
  {
    private PermissionSet m_permSet;
    private TokenBasedSetEnumerator enm;

    public object Current
    {
      get
      {
        return this.enm.Current;
      }
    }

    internal PermissionSetEnumeratorInternal(PermissionSet permSet)
    {
      this.m_permSet = permSet;
      this.enm = new TokenBasedSetEnumerator(permSet.m_permSet);
    }

    public int GetCurrentIndex()
    {
      return this.enm.Index;
    }

    public void Reset()
    {
      this.enm.Reset();
    }

    public bool MoveNext()
    {
      while (this.enm.MoveNext())
      {
        object obj = this.enm.Current;
        IPermission permission1 = obj as IPermission;
        if (permission1 != null)
        {
          this.enm.Current = (object) permission1;
          return true;
        }
        SecurityElement securityElement = obj as SecurityElement;
        if (securityElement != null)
        {
          IPermission permission2 = this.m_permSet.CreatePermission((object) securityElement, this.enm.Index);
          if (permission2 != null)
          {
            this.enm.Current = (object) permission2;
            return true;
          }
        }
      }
      return false;
    }
  }
}
