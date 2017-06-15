// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.QualifiedAce
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Security.Principal;

namespace System.Security.AccessControl
{
  /// <summary>表示包含限定符的访问控制项 (ACE)。由 <see cref="T:System.Security.AccessControl.AceQualifier" /> 对象表示的限定符指定 ACE 是允许访问、拒绝访问、导致系统审核或是导致系统警告。<see cref="T:System.Security.AccessControl.QualifiedAce" /> 类为 <see cref="T:System.Security.AccessControl.CommonAce" /> 类和 <see cref="T:System.Security.AccessControl.ObjectAce" /> 类的抽象基类。</summary>
  public abstract class QualifiedAce : KnownAce
  {
    private readonly bool _isCallback;
    private readonly AceQualifier _qualifier;
    private byte[] _opaque;

    /// <summary>获取一个指定 ACE 是允许访问、拒绝访问、导致系统审核或是导致系统警告的值。</summary>
    /// <returns>一个指定 ACE 是允许访问、拒绝访问、导致系统审核或是导致系统警告的值。</returns>
    public AceQualifier AceQualifier
    {
      get
      {
        return this._qualifier;
      }
    }

    /// <summary>指定此 <see cref="T:System.Security.AccessControl.QualifiedAce" /> 对象是否包含回调数据。</summary>
    /// <returns>如果此 <see cref="T:System.Security.AccessControl.QualifiedAce" /> 对象包含回调数据，则为 true；否则为 false。</returns>
    public bool IsCallback
    {
      get
      {
        return this._isCallback;
      }
    }

    internal abstract int MaxOpaqueLengthInternal { get; }

    /// <summary>获取与此 <see cref="T:System.Security.AccessControl.QualifiedAce" /> 对象关联的不透明回调数据的长度。此属性仅对回调访问控制项 (ACE) 有效。</summary>
    /// <returns>不透明回调数据的长度。</returns>
    public int OpaqueLength
    {
      get
      {
        if (this._opaque != null)
          return this._opaque.Length;
        return 0;
      }
    }

    internal QualifiedAce(AceType type, AceFlags flags, int accessMask, SecurityIdentifier sid, byte[] opaque)
      : base(type, flags, accessMask, sid)
    {
      this._qualifier = this.QualifierFromType(type, out this._isCallback);
      this.SetOpaque(opaque);
    }

    private AceQualifier QualifierFromType(AceType type, out bool isCallback)
    {
      switch (type)
      {
        case AceType.AccessAllowed:
          isCallback = false;
          return AceQualifier.AccessAllowed;
        case AceType.AccessDenied:
          isCallback = false;
          return AceQualifier.AccessDenied;
        case AceType.SystemAudit:
          isCallback = false;
          return AceQualifier.SystemAudit;
        case AceType.SystemAlarm:
          isCallback = false;
          return AceQualifier.SystemAlarm;
        case AceType.AccessAllowedObject:
          isCallback = false;
          return AceQualifier.AccessAllowed;
        case AceType.AccessDeniedObject:
          isCallback = false;
          return AceQualifier.AccessDenied;
        case AceType.SystemAuditObject:
          isCallback = false;
          return AceQualifier.SystemAudit;
        case AceType.SystemAlarmObject:
          isCallback = false;
          return AceQualifier.SystemAlarm;
        case AceType.AccessAllowedCallback:
          isCallback = true;
          return AceQualifier.AccessAllowed;
        case AceType.AccessDeniedCallback:
          isCallback = true;
          return AceQualifier.AccessDenied;
        case AceType.AccessAllowedCallbackObject:
          isCallback = true;
          return AceQualifier.AccessAllowed;
        case AceType.AccessDeniedCallbackObject:
          isCallback = true;
          return AceQualifier.AccessDenied;
        case AceType.SystemAuditCallback:
          isCallback = true;
          return AceQualifier.SystemAudit;
        case AceType.SystemAlarmCallback:
          isCallback = true;
          return AceQualifier.SystemAlarm;
        case AceType.SystemAuditCallbackObject:
          isCallback = true;
          return AceQualifier.SystemAudit;
        case AceType.SystemAlarmCallbackObject:
          isCallback = true;
          return AceQualifier.SystemAlarm;
        default:
          throw new SystemException();
      }
    }

    /// <summary>返回与此 <see cref="T:System.Security.AccessControl.QualifiedAce" /> 对象关联的不透明回调数据。</summary>
    /// <returns>一个字节值数组，表示与此 <see cref="T:System.Security.AccessControl.QualifiedAce" /> 对象关联的不透明回调数据。</returns>
    public byte[] GetOpaque()
    {
      return this._opaque;
    }

    /// <summary>设置与此 <see cref="T:System.Security.AccessControl.QualifiedAce" /> 对象关联的不透明回调数据。</summary>
    /// <param name="opaque">一个字节值数组，表示此 <see cref="T:System.Security.AccessControl.QualifiedAce" /> 对象的不透明回调数据。</param>
    public void SetOpaque(byte[] opaque)
    {
      if (opaque != null)
      {
        if (opaque.Length > this.MaxOpaqueLengthInternal)
          throw new ArgumentOutOfRangeException("opaque", string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_ArrayLength"), (object) 0, (object) this.MaxOpaqueLengthInternal));
        if (opaque.Length % 4 != 0)
          throw new ArgumentOutOfRangeException("opaque", string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_ArrayLengthMultiple"), (object) 4));
      }
      this._opaque = opaque;
    }
  }
}
