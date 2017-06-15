// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.KnownAce
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security.Principal;

namespace System.Security.AccessControl
{
  /// <summary>封装 Microsoft Corporation 当前定义的所有访问控制项 (ACE) 类型。所有 <see cref="T:System.Security.AccessControl.KnownAce" /> 对象都包含一个 32 位的访问掩码和一个 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 对象。</summary>
  public abstract class KnownAce : GenericAce
  {
    private int _accessMask;
    private SecurityIdentifier _sid;
    internal const int AccessMaskLength = 4;

    /// <summary>获取或设置此 <see cref="T:System.Security.AccessControl.KnownAce" /> 对象的访问掩码。</summary>
    /// <returns>此 <see cref="T:System.Security.AccessControl.KnownAce" /> 对象的访问掩码。</returns>
    public int AccessMask
    {
      get
      {
        return this._accessMask;
      }
      set
      {
        this._accessMask = value;
      }
    }

    /// <summary>获取或设置与此 <see cref="T:System.Security.AccessControl.KnownAce" /> 对象关联的 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 对象。</summary>
    /// <returns>与此 <see cref="T:System.Security.AccessControl.KnownAce" /> 对象关联的 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 对象。</returns>
    public SecurityIdentifier SecurityIdentifier
    {
      get
      {
        return this._sid;
      }
      set
      {
        if (value == (SecurityIdentifier) null)
          throw new ArgumentNullException("value");
        this._sid = value;
      }
    }

    internal KnownAce(AceType type, AceFlags flags, int accessMask, SecurityIdentifier securityIdentifier)
      : base(type, flags)
    {
      if (securityIdentifier == (SecurityIdentifier) null)
        throw new ArgumentNullException("securityIdentifier");
      this.AccessMask = accessMask;
      this.SecurityIdentifier = securityIdentifier;
    }
  }
}
