// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.ApplicationTrustEnumerator
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
  /// <summary>表示 <see cref="T:System.Security.Policy.ApplicationTrustCollection" /> 集合中的 <see cref="T:System.Security.Policy.ApplicationTrust" /> 对象的枚举数。</summary>
  [ComVisible(true)]
  public sealed class ApplicationTrustEnumerator : IEnumerator
  {
    [SecurityCritical]
    private ApplicationTrustCollection m_trusts;
    private int m_current;

    /// <summary>获取 <see cref="T:System.Security.Policy.ApplicationTrustCollection" /> 集合中的当前 <see cref="T:System.Security.Policy.ApplicationTrust" /> 对象。</summary>
    /// <returns>
    /// <see cref="T:System.Security.Policy.ApplicationTrustCollection" /> 中的当前 <see cref="T:System.Security.Policy.ApplicationTrust" />。</returns>
    public ApplicationTrust Current
    {
      [SecuritySafeCritical] get
      {
        return this.m_trusts[this.m_current];
      }
    }

    object IEnumerator.Current
    {
      [SecuritySafeCritical] get
      {
        return (object) this.m_trusts[this.m_current];
      }
    }

    private ApplicationTrustEnumerator()
    {
    }

    [SecurityCritical]
    internal ApplicationTrustEnumerator(ApplicationTrustCollection trusts)
    {
      this.m_trusts = trusts;
      this.m_current = -1;
    }

    /// <summary>移动到 <see cref="T:System.Security.Policy.ApplicationTrustCollection" /> 集合中的下一个元素。</summary>
    /// <returns>如果枚举数成功地推进到下一个元素，则为 true；如果枚举数越过集合的结尾，则为 false。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public bool MoveNext()
    {
      if (this.m_current == this.m_trusts.Count - 1)
        return false;
      this.m_current = this.m_current + 1;
      return true;
    }

    /// <summary>将枚举数重置到 <see cref="T:System.Security.Policy.ApplicationTrustCollection" /> 集合的开头。</summary>
    public void Reset()
    {
      this.m_current = -1;
    }
  }
}
