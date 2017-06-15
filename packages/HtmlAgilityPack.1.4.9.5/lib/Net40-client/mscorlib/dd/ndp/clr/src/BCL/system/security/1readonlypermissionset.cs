// Decompiled with JetBrains decompiler
// Type: System.Security.ReadOnlyPermissionSetEnumerator
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;

namespace System.Security
{
  internal sealed class ReadOnlyPermissionSetEnumerator : IEnumerator
  {
    private IEnumerator m_permissionSetEnumerator;

    public object Current
    {
      get
      {
        IPermission permission = this.m_permissionSetEnumerator.Current as IPermission;
        if (permission == null)
          return (object) null;
        return (object) permission.Copy();
      }
    }

    internal ReadOnlyPermissionSetEnumerator(IEnumerator permissionSetEnumerator)
    {
      this.m_permissionSetEnumerator = permissionSetEnumerator;
    }

    public bool MoveNext()
    {
      return this.m_permissionSetEnumerator.MoveNext();
    }

    public void Reset()
    {
      this.m_permissionSetEnumerator.Reset();
    }
  }
}
