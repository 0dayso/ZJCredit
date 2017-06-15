// Decompiled with JetBrains decompiler
// Type: System.Security.Principal.WindowsIdentity
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.AccessControl;
using System.Security.Claims;
using System.Security.Permissions;
using System.Text;
using System.Threading;

namespace System.Security.Principal
{
  /// <summary>表示 Windows 用户。</summary>
  [ComVisible(true)]
  [Serializable]
  public class WindowsIdentity : ClaimsIdentity, ISerializable, IDeserializationCallback, IDisposable
  {
    [SecurityCritical]
    private SafeAccessTokenHandle m_safeTokenHandle = SafeAccessTokenHandle.InvalidHandle;
    private int m_isAuthenticated = -1;
    [NonSerialized]
    private string m_issuerName = "AD AUTHORITY";
    [NonSerialized]
    private object m_claimsIntiailizedLock = new object();
    [SecurityCritical]
    private static SafeAccessTokenHandle s_invalidTokenHandle = SafeAccessTokenHandle.InvalidHandle;
    private static RuntimeConstructorInfo s_specialSerializationCtor = typeof (WindowsIdentity).GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, (Binder) null, new Type[1]{ typeof (SerializationInfo) }, (ParameterModifier[]) null) as RuntimeConstructorInfo;
    private string m_name;
    private SecurityIdentifier m_owner;
    private SecurityIdentifier m_user;
    private object m_groups;
    private string m_authType;
    private volatile TokenImpersonationLevel m_impersonationLevel;
    private volatile bool m_impersonationLevelInitialized;
    /// <summary>标识默认 <see cref="T:System.Security.Claims.ClaimsIdentity" /> 颁发者的名称。</summary>
    [NonSerialized]
    public new const string DefaultIssuer = "AD AUTHORITY";
    [NonSerialized]
    private volatile bool m_claimsInitialized;
    [NonSerialized]
    private List<Claim> m_deviceClaims;
    [NonSerialized]
    private List<Claim> m_userClaims;

    /// <summary>获取用于标识用户的身份验证的类型。</summary>
    /// <returns>用于标识用户的身份验证的类型。</returns>
    /// <exception cref="T:System.UnauthorizedAccessException">Windows returned the Windows NT status code STATUS_ACCESS_DENIED.</exception>
    /// <exception cref="T:System.OutOfMemoryException">There is insufficient memory available.</exception>
    /// <exception cref="T:System.Security.SecurityException">The caller does not have the correct permissions. -or-The computer is not attached to a Windows 2003 or later domain.-or-The computer is not running Windows 2003 or later.-or-The user is not a member of the domain the computer is attached to.</exception>
    public override sealed string AuthenticationType
    {
      [SecuritySafeCritical] get
      {
        if (this.m_safeTokenHandle.IsInvalid)
          return string.Empty;
        if (this.m_authType != null)
          return this.m_authType;
        Win32Native.LUID logonAuthId = WindowsIdentity.GetLogonAuthId(this.m_safeTokenHandle);
        if ((int) logonAuthId.LowPart == 998)
          return string.Empty;
        SafeLsaReturnBufferHandle ppLogonSessionData = SafeLsaReturnBufferHandle.InvalidHandle;
        try
        {
          int logonSessionData = Win32Native.LsaGetLogonSessionData(ref logonAuthId, out ppLogonSessionData);
          if (logonSessionData < 0)
            throw WindowsIdentity.GetExceptionFromNtStatus(logonSessionData);
          ppLogonSessionData.Initialize((ulong) (uint) Marshal.SizeOf(typeof (Win32Native.SECURITY_LOGON_SESSION_DATA)));
          return Marshal.PtrToStringUni(ppLogonSessionData.Read<Win32Native.SECURITY_LOGON_SESSION_DATA>(0UL).AuthenticationPackage.Buffer);
        }
        finally
        {
          if (!ppLogonSessionData.IsInvalid)
            ppLogonSessionData.Dispose();
        }
      }
    }

    /// <summary>获取用户的模拟级别。</summary>
    /// <returns>用于指定模拟级别的枚举值之一。</returns>
    [ComVisible(false)]
    public TokenImpersonationLevel ImpersonationLevel
    {
      [SecuritySafeCritical] get
      {
        if (!this.m_impersonationLevelInitialized)
        {
          this.m_impersonationLevel = !this.m_safeTokenHandle.IsInvalid ? (this.GetTokenInformation<int>(TokenInformationClass.TokenType) != 1 ? (TokenImpersonationLevel) (this.GetTokenInformation<int>(TokenInformationClass.TokenImpersonationLevel) + 1) : TokenImpersonationLevel.None) : TokenImpersonationLevel.Anonymous;
          this.m_impersonationLevelInitialized = true;
        }
        return this.m_impersonationLevel;
      }
    }

    /// <summary>获取一个值，该值指示 Windows 是否对用户进行了身份验证。</summary>
    /// <returns>如果用户已经过验证，则为 true；否则为 false。</returns>
    public override bool IsAuthenticated
    {
      get
      {
        if (this.m_isAuthenticated == -1)
          this.m_isAuthenticated = this.CheckNtTokenForSid(new SecurityIdentifier(IdentifierAuthority.NTAuthority, new int[1]{ 11 })) ? 1 : 0;
        return this.m_isAuthenticated == 1;
      }
    }

    /// <summary>获取一个值，该值指示系统是否将用户帐户标识为 <see cref="F:System.Security.Principal.WindowsAccountType.Guest" /> 帐户。</summary>
    /// <returns>如果用户帐户是 <see cref="F:System.Security.Principal.WindowsAccountType.Guest" /> 帐户，则为 true；否则为 false。</returns>
    public virtual bool IsGuest
    {
      [SecuritySafeCritical] get
      {
        if (this.m_safeTokenHandle.IsInvalid)
          return false;
        return this.CheckNtTokenForSid(new SecurityIdentifier(IdentifierAuthority.NTAuthority, new int[2]{ 32, 546 }));
      }
    }

    /// <summary>获取一个值，该值指示系统是否将用户帐户标识为 <see cref="F:System.Security.Principal.WindowsAccountType.System" /> 帐户。</summary>
    /// <returns>如果用户帐户是 <see cref="F:System.Security.Principal.WindowsAccountType.System" /> 帐户，则为 true；否则为 false。</returns>
    public virtual bool IsSystem
    {
      [SecuritySafeCritical] get
      {
        if (this.m_safeTokenHandle.IsInvalid)
          return false;
        return this.User == new SecurityIdentifier(IdentifierAuthority.NTAuthority, new int[1]{ 18 });
      }
    }

    /// <summary>获取一个值，该值指示系统是否将用户帐户标识为匿名帐户。</summary>
    /// <returns>如果用户帐户是匿名帐户，则为 true；否则为 false。</returns>
    public virtual bool IsAnonymous
    {
      [SecuritySafeCritical] get
      {
        if (this.m_safeTokenHandle.IsInvalid)
          return true;
        return this.User == new SecurityIdentifier(IdentifierAuthority.NTAuthority, new int[1]{ 7 });
      }
    }

    /// <summary>获取用户的 Windows 登录名。</summary>
    /// <returns>用户的 Windows 登录名，当前即以该用户的名义运行代码。</returns>
    public override string Name
    {
      [SecuritySafeCritical] get
      {
        return this.GetName();
      }
    }

    /// <summary>获取标记所有者的安全标识符 (SID)。</summary>
    /// <returns>标记所有者的对象。</returns>
    [ComVisible(false)]
    public SecurityIdentifier Owner
    {
      [SecuritySafeCritical] get
      {
        if (this.m_safeTokenHandle.IsInvalid)
          return (SecurityIdentifier) null;
        if (this.m_owner == (SecurityIdentifier) null)
        {
          using (SafeLocalAllocHandle tokenInformation = WindowsIdentity.GetTokenInformation(this.m_safeTokenHandle, TokenInformationClass.TokenOwner))
            this.m_owner = new SecurityIdentifier(tokenInformation.Read<IntPtr>(0UL), true);
        }
        return this.m_owner;
      }
    }

    /// <summary>获取用户的安全标识符 (SID)。</summary>
    /// <returns>用户对象。</returns>
    [ComVisible(false)]
    public SecurityIdentifier User
    {
      [SecuritySafeCritical] get
      {
        if (this.m_safeTokenHandle.IsInvalid)
          return (SecurityIdentifier) null;
        if (this.m_user == (SecurityIdentifier) null)
        {
          using (SafeLocalAllocHandle tokenInformation = WindowsIdentity.GetTokenInformation(this.m_safeTokenHandle, TokenInformationClass.TokenUser))
            this.m_user = new SecurityIdentifier(tokenInformation.Read<IntPtr>(0UL), true);
        }
        return this.m_user;
      }
    }

    /// <summary>获取当前 Windows 用户所属的组。</summary>
    /// <returns>一个对象，它表示当前 Windows 用户所属的组。</returns>
    public IdentityReferenceCollection Groups
    {
      [SecuritySafeCritical] get
      {
        if (this.m_safeTokenHandle.IsInvalid)
          return (IdentityReferenceCollection) null;
        if (this.m_groups == null)
        {
          IdentityReferenceCollection referenceCollection = new IdentityReferenceCollection();
          using (SafeLocalAllocHandle tokenInformation = WindowsIdentity.GetTokenInformation(this.m_safeTokenHandle, TokenInformationClass.TokenGroups))
          {
            if ((int) tokenInformation.Read<uint>(0UL) != 0)
            {
              Win32Native.SID_AND_ATTRIBUTES[] array = new Win32Native.SID_AND_ATTRIBUTES[(int) tokenInformation.Read<Win32Native.TOKEN_GROUPS>(0UL).GroupCount];
              tokenInformation.ReadArray<Win32Native.SID_AND_ATTRIBUTES>((ulong) (uint) Marshal.OffsetOf(typeof (Win32Native.TOKEN_GROUPS), "Groups").ToInt32(), array, 0, array.Length);
              foreach (Win32Native.SID_AND_ATTRIBUTES sidAndAttributes in array)
              {
                uint num = 3221225492;
                if (((int) sidAndAttributes.Attributes & (int) num) == 4)
                  referenceCollection.Add((IdentityReference) new SecurityIdentifier(sidAndAttributes.Sid, true));
              }
            }
          }
          Interlocked.CompareExchange(ref this.m_groups, (object) referenceCollection, (object) null);
        }
        return this.m_groups as IdentityReferenceCollection;
      }
    }

    /// <summary>获取用户的 Windows 帐户标记。</summary>
    /// <returns>与当前执行线程关联的访问令牌的句柄。</returns>
    public virtual IntPtr Token
    {
      [SecuritySafeCritical, SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        return this.m_safeTokenHandle.DangerousGetHandle();
      }
    }

    /// <summary>获取此 <see cref="T:System.Security.Principal.WindowsIdentity" /> 实例的此 <see cref="T:Microsoft.Win32.SafeHandles.SafeAccessTokenHandle" />。</summary>
    /// <returns>返回 <see cref="T:Microsoft.Win32.SafeHandles.SafeAccessTokenHandle" />。</returns>
    public SafeAccessTokenHandle AccessToken
    {
      [SecurityCritical] get
      {
        return this.m_safeTokenHandle;
      }
    }

    /// <summary>获取有 <see cref="F:System.Security.Claims.ClaimTypes.WindowsUserClaim" /> 属性密钥的声明。</summary>
    /// <returns>具有 <see cref="F:System.Security.Claims.ClaimTypes.WindowsUserClaim" /> 属性键的声明的集合。</returns>
    public virtual IEnumerable<Claim> UserClaims
    {
      get
      {
        this.InitializeClaims();
        return (IEnumerable<Claim>) this.m_userClaims.AsReadOnly();
      }
    }

    /// <summary>获取有 <see cref="F:System.Security.Claims.ClaimTypes.WindowsDeviceClaim" /> 属性密钥的声明。</summary>
    /// <returns>具有 <see cref="F:System.Security.Claims.ClaimTypes.WindowsDeviceClaim" /> 属性键的声明的集合。</returns>
    public virtual IEnumerable<Claim> DeviceClaims
    {
      get
      {
        this.InitializeClaims();
        return (IEnumerable<Claim>) this.m_deviceClaims.AsReadOnly();
      }
    }

    /// <summary>为用户获取此 Windows 标识表示的所有声明。</summary>
    /// <returns>表示该 <see cref="T:System.Security.Principal.WindowsIdentity" /> 对象的请求的集合。</returns>
    public override IEnumerable<Claim> Claims
    {
      get
      {
        if (!this.m_claimsInitialized)
          this.InitializeClaims();
        foreach (Claim claim in base.Claims)
          yield return claim;
        foreach (Claim mUserClaim in this.m_userClaims)
          yield return mUserClaim;
        List<Claim>.Enumerator enumerator = new List<Claim>.Enumerator();
        foreach (Claim mDeviceClaim in this.m_deviceClaims)
          yield return mDeviceClaim;
        enumerator = new List<Claim>.Enumerator();
      }
    }

    [SecuritySafeCritical]
    static WindowsIdentity()
    {
    }

    [SecurityCritical]
    private WindowsIdentity()
      : base((IIdentity) null, (IEnumerable<Claim>) null, (string) null, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", "http://schemas.microsoft.com/ws/2008/06/identity/claims/groupsid")
    {
    }

    [SecurityCritical]
    internal WindowsIdentity(SafeAccessTokenHandle safeTokenHandle)
      : this(safeTokenHandle.DangerousGetHandle(), (string) null, -1)
    {
      GC.KeepAlive((object) safeTokenHandle);
    }

    /// <summary>为指定的 Windows 帐户标记表示的用户初始化 <see cref="T:System.Security.Principal.WindowsIdentity" /> 类的新实例。</summary>
    /// <param name="userToken">用户的帐户标记，代码当前即以该用户的名义运行。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="userToken" /> is 0.-or-<paramref name="userToken" /> is duplicated and invalid for impersonation.</exception>
    /// <exception cref="T:System.Security.SecurityException">The caller does not have the correct permissions. -or-A Win32 error occurred.</exception>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPrincipal)]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public WindowsIdentity(IntPtr userToken)
      : this(userToken, (string) null, -1)
    {
    }

    /// <summary>为指定的 Windows 帐户标记和指定的身份验证类型表示的用户初始化 <see cref="T:System.Security.Principal.WindowsIdentity" /> 类的新实例。</summary>
    /// <param name="userToken">用户的帐户标记，代码当前即以该用户的名义运行。</param>
    /// <param name="type">（仅供参考之用。） 用于标识用户的身份验证的类型。有关更多信息，请参见“备注”。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="userToken" /> is 0.-or-<paramref name="userToken" /> is duplicated and invalid for impersonation.</exception>
    /// <exception cref="T:System.Security.SecurityException">The caller does not have the correct permissions. -or-A Win32 error occurred.</exception>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPrincipal)]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public WindowsIdentity(IntPtr userToken, string type)
      : this(userToken, type, -1)
    {
    }

    /// <summary>为指定的 Windows 帐户标记、指定的身份验证类型和指定的 Windows 帐户类型表示的用户初始化 <see cref="T:System.Security.Principal.WindowsIdentity" /> 类的新实例。</summary>
    /// <param name="userToken">用户的帐户标记，代码当前即以该用户的名义运行。</param>
    /// <param name="type">（仅供参考之用。） 用于标识用户的身份验证的类型。有关更多信息，请参见“备注”。</param>
    /// <param name="acctType">枚举值之一。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="userToken" /> is 0.-or-<paramref name="userToken" /> is duplicated and invalid for impersonation.</exception>
    /// <exception cref="T:System.Security.SecurityException">The caller does not have the correct permissions. -or-A Win32 error occurred.</exception>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPrincipal)]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public WindowsIdentity(IntPtr userToken, string type, WindowsAccountType acctType)
      : this(userToken, type, -1)
    {
    }

    /// <summary>为指定的 Windows 帐户标记、指定的身份验证类型、指定的 Windows 帐户类型和指定的身份验证状态表示的用户初始化 <see cref="T:System.Security.Principal.WindowsIdentity" /> 类的新实例。</summary>
    /// <param name="userToken">用户的帐户标记，代码当前即以该用户的名义运行。</param>
    /// <param name="type">（仅供参考之用。） 用于标识用户的身份验证的类型。有关更多信息，请参见“备注”。</param>
    /// <param name="acctType">枚举值之一。</param>
    /// <param name="isAuthenticated">true 指示用户已经过身份验证；否则为 false。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="userToken" /> is 0.-or-<paramref name="userToken" /> is duplicated and invalid for impersonation.</exception>
    /// <exception cref="T:System.Security.SecurityException">The caller does not have the correct permissions. -or-A Win32 error occurred.</exception>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPrincipal)]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public WindowsIdentity(IntPtr userToken, string type, WindowsAccountType acctType, bool isAuthenticated)
      : this(userToken, type, isAuthenticated ? 1 : 0)
    {
    }

    [SecurityCritical]
    private WindowsIdentity(IntPtr userToken, string authType, int isAuthenticated)
      : base((IIdentity) null, (IEnumerable<Claim>) null, (string) null, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", "http://schemas.microsoft.com/ws/2008/06/identity/claims/groupsid")
    {
      this.CreateFromToken(userToken);
      this.m_authType = authType;
      this.m_isAuthenticated = isAuthenticated;
    }

    /// <summary>初始化以指定用户主名称 (UPN) 表示的用户的 <see cref="T:System.Security.Principal.WindowsIdentity" /> 类的新实例。</summary>
    /// <param name="sUserPrincipalName">代码运行时所代表用户的 UPN。</param>
    /// <exception cref="T:System.UnauthorizedAccessException">Windows returned the Windows NT status code STATUS_ACCESS_DENIED.</exception>
    /// <exception cref="T:System.OutOfMemoryException">There is insufficient memory available.</exception>
    /// <exception cref="T:System.Security.SecurityException">The caller does not have the correct permissions. -or-The computer is not attached to a Windows 2003 or later domain.-or-The computer is not running Windows 2003 or later.-or-The user is not a member of the domain the computer is attached to.</exception>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPrincipal)]
    public WindowsIdentity(string sUserPrincipalName)
      : this(sUserPrincipalName, (string) null)
    {
    }

    /// <summary>初始化以指定用户主名称 (UPN) 和指定身份验证类型共同表示的用户的 <see cref="T:System.Security.Principal.WindowsIdentity" /> 类的新实例。</summary>
    /// <param name="sUserPrincipalName">代码运行时所代表用户的 UPN。</param>
    /// <param name="type">（仅供参考之用。） 用于标识用户的身份验证的类型。有关更多信息，请参见“备注”。</param>
    /// <exception cref="T:System.UnauthorizedAccessException">Windows returned the Windows NT status code STATUS_ACCESS_DENIED.</exception>
    /// <exception cref="T:System.OutOfMemoryException">There is insufficient memory available.</exception>
    /// <exception cref="T:System.Security.SecurityException">The caller does not have the correct permissions. -or-The computer is not attached to a Windows 2003 or later domain.-or-The computer is not running Windows 2003 or later.-or-The user is not a member of the domain the computer is attached to.</exception>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPrincipal)]
    public WindowsIdentity(string sUserPrincipalName, string type)
      : base((IIdentity) null, (IEnumerable<Claim>) null, (string) null, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", "http://schemas.microsoft.com/ws/2008/06/identity/claims/groupsid")
    {
      WindowsIdentity.KerbS4ULogon(sUserPrincipalName, ref this.m_safeTokenHandle);
    }

    /// <summary>初始化 <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 流中的信息所表示的用户的 <see cref="T:System.Security.Principal.WindowsIdentity" /> 类的新实例。</summary>
    /// <param name="info">包含用户帐户信息的对象。</param>
    /// <param name="context">指示流特征的对象。</param>
    /// <exception cref="T:System.NotSupportedException">A <see cref="T:System.Security.Principal.WindowsIdentity" /> cannot be serialized across processes. </exception>
    /// <exception cref="T:System.Security.SecurityException">The caller does not have the correct permissions. -or-A Win32 error occurred.</exception>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPrincipal)]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.SerializationFormatter)]
    public WindowsIdentity(SerializationInfo info, StreamingContext context)
      : this(info)
    {
    }

    [SecurityCritical]
    private WindowsIdentity(SerializationInfo info)
      : base(info)
    {
      this.m_claimsInitialized = false;
      IntPtr userToken = (IntPtr) info.GetValue("m_userToken", typeof (IntPtr));
      if (!(userToken != IntPtr.Zero))
        return;
      this.CreateFromToken(userToken);
    }

    /// <summary>使用指定的 <see cref="T:System.Security.Principal.WindowsIdentity" /> 对象初始化 <see cref="T:System.Security.Principal.WindowsIdentity" /> 类的新实例。</summary>
    /// <param name="identity">根据其构造 <see cref="T:System.Security.Principal.WindowsIdentity" /> 新实例的对象。</param>
    [SecuritySafeCritical]
    protected WindowsIdentity(WindowsIdentity identity)
      : base((IIdentity) identity, (IEnumerable<Claim>) null, identity.m_authType, (string) null, (string) null, false)
    {
      if (identity == null)
        throw new ArgumentNullException("identity");
      bool success = false;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        if (identity.m_safeTokenHandle.IsInvalid || identity.m_safeTokenHandle == SafeAccessTokenHandle.InvalidHandle || !(identity.m_safeTokenHandle.DangerousGetHandle() != IntPtr.Zero))
          return;
        identity.m_safeTokenHandle.DangerousAddRef(ref success);
        if (!identity.m_safeTokenHandle.IsInvalid && identity.m_safeTokenHandle.DangerousGetHandle() != IntPtr.Zero)
          this.CreateFromToken(identity.m_safeTokenHandle.DangerousGetHandle());
        this.m_authType = identity.m_authType;
        this.m_isAuthenticated = identity.m_isAuthenticated;
      }
      finally
      {
        if (success)
          identity.m_safeTokenHandle.DangerousRelease();
      }
    }

    [SecurityCritical]
    internal WindowsIdentity(ClaimsIdentity claimsIdentity, IntPtr userToken)
      : base(claimsIdentity)
    {
      if (!(userToken != IntPtr.Zero) || userToken.ToInt64() <= 0L)
        return;
      this.CreateFromToken(userToken);
    }

    [SecurityCritical]
    private void CreateFromToken(IntPtr userToken)
    {
      if (userToken == IntPtr.Zero)
        throw new ArgumentException(Environment.GetResourceString("Argument_TokenZero"));
      uint ReturnLength = (uint) Marshal.SizeOf(typeof (uint));
      Win32Native.GetTokenInformation(userToken, 8U, SafeLocalAllocHandle.InvalidHandle, 0U, out ReturnLength);
      if (Marshal.GetLastWin32Error() == 6)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidImpersonationToken"));
      if (!Win32Native.DuplicateHandle(Win32Native.GetCurrentProcess(), userToken, Win32Native.GetCurrentProcess(), out this.m_safeTokenHandle, 0U, true, 2U))
        throw new SecurityException(Win32Native.GetMessage(Marshal.GetLastWin32Error()));
    }

    [SecurityCritical]
    void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
    {
      this.GetObjectData(info, context);
      info.AddValue("m_userToken", (object) this.m_safeTokenHandle.DangerousGetHandle());
    }

    void IDeserializationCallback.OnDeserialization(object sender)
    {
    }

    /// <summary>返回表示当前 Windows 用户的 <see cref="T:System.Security.Principal.WindowsIdentity" /> 对象。</summary>
    /// <returns>表示当前用户的对象。</returns>
    /// <exception cref="T:System.Security.SecurityException">The caller does not have the correct permissions. </exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlPrincipal" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPrincipal)]
    public static WindowsIdentity GetCurrent()
    {
      return WindowsIdentity.GetCurrentInternal(TokenAccessLevels.MaximumAllowed, false);
    }

    /// <summary>返回一个 <see cref="T:System.Security.Principal.WindowsIdentity" /> 对象，该对象表示线程或进程（具体取决于 <paramref name="ifImpersonating" /> 参数的值）的 Windows 标识。</summary>
    /// <returns>表示 Windows 用户的对象。</returns>
    /// <param name="ifImpersonating">如果为 true，则仅在线程当前正在模拟时才返回 <see cref="T:System.Security.Principal.WindowsIdentity" />；如果为 false，则在线程正在模拟时返回线程的 <see cref="T:System.Security.Principal.WindowsIdentity" />，在线程当前没有模拟时返回进程的 <see cref="T:System.Security.Principal.WindowsIdentity" />。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlPrincipal" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPrincipal)]
    public static WindowsIdentity GetCurrent(bool ifImpersonating)
    {
      return WindowsIdentity.GetCurrentInternal(TokenAccessLevels.MaximumAllowed, ifImpersonating);
    }

    /// <summary>返回一个 <see cref="T:System.Security.Principal.WindowsIdentity" /> 对象，该对象使用指定的所需标记访问级别来表示当前 Windows 用户。</summary>
    /// <returns>表示当前用户的对象。</returns>
    /// <param name="desiredAccess">枚举值的按位组合。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlPrincipal" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPrincipal)]
    public static WindowsIdentity GetCurrent(TokenAccessLevels desiredAccess)
    {
      return WindowsIdentity.GetCurrentInternal(desiredAccess, false);
    }

    /// <summary>返回 <see cref="T:System.Security.Principal.WindowsIdentity" /> 对象，可在代码中将其用作 sentinel 值来表示匿名用户。属性值不表示 Windows 操作系统使用的内置匿名标识。</summary>
    /// <returns>表示匿名用户的对象。</returns>
    [SecuritySafeCritical]
    public static WindowsIdentity GetAnonymous()
    {
      return new WindowsIdentity();
    }

    [SecuritySafeCritical]
    [ComVisible(false)]
    private bool CheckNtTokenForSid(SecurityIdentifier sid)
    {
      if (this.m_safeTokenHandle.IsInvalid)
        return false;
      SafeAccessTokenHandle phNewToken = SafeAccessTokenHandle.InvalidHandle;
      TokenImpersonationLevel impersonationLevel = this.ImpersonationLevel;
      bool IsMember = false;
      try
      {
        if (impersonationLevel == TokenImpersonationLevel.None && !Win32Native.DuplicateTokenEx(this.m_safeTokenHandle, 8U, IntPtr.Zero, 2U, 2U, out phNewToken))
          throw new SecurityException(Win32Native.GetMessage(Marshal.GetLastWin32Error()));
        if (!Win32Native.CheckTokenMembership(impersonationLevel != TokenImpersonationLevel.None ? this.m_safeTokenHandle : phNewToken, sid.BinaryForm, out IsMember))
          throw new SecurityException(Win32Native.GetMessage(Marshal.GetLastWin32Error()));
      }
      finally
      {
        if (phNewToken != SafeAccessTokenHandle.InvalidHandle)
          phNewToken.Dispose();
      }
      return IsMember;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    internal string GetName()
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      if (this.m_safeTokenHandle.IsInvalid)
        return string.Empty;
      if (this.m_name == null)
      {
        using (WindowsIdentity.SafeRevertToSelf(ref stackMark))
          this.m_name = (this.User.Translate(typeof (NTAccount)) as NTAccount).ToString();
      }
      return this.m_name;
    }

    /// <summary>作为模拟 Windows 标识运行指定操作。可以使用 <see cref="M:System.Security.Principal.WindowsIdentity.RunImpersonated(Microsoft.Win32.SafeHandles.SafeAccessTokenHandle,System.Action)" /> 并直接作为参数提供函数，而不是使用模拟方法调用并在 <see cref="T:System.Security.Principal.WindowsImpersonationContext" /> 中运行函数。</summary>
    /// <param name="safeAccessTokenHandle">模拟 Windows 标识 SafeAccessTokenHandle。</param>
    /// <param name="action">要运行的 System.Action。</param>
    [SecuritySafeCritical]
    public static void RunImpersonated(SafeAccessTokenHandle safeAccessTokenHandle, Action action)
    {
      if (action == null)
        throw new ArgumentNullException("action");
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      WindowsIdentity wi = (WindowsIdentity) null;
      if (!safeAccessTokenHandle.IsInvalid)
        wi = new WindowsIdentity(safeAccessTokenHandle);
      using (WindowsIdentity.SafeImpersonate(safeAccessTokenHandle, wi, ref stackMark))
        action();
    }

    /// <summary>作为模拟 Windows 标识运行指定函数。可以使用 <see cref="M:System.Security.Principal.WindowsIdentity.RunImpersonated(Microsoft.Win32.SafeHandles.SafeAccessTokenHandle,System.Action)" /> 并直接作为参数提供函数，而不是使用模拟方法调用并在 <see cref="T:System.Security.Principal.WindowsImpersonationContext" /> 中运行函数。</summary>
    /// <returns>返回函数的结果。</returns>
    /// <param name="safeAccessTokenHandle">模拟 Windows 标识 SafeAccessTokenHandle。</param>
    /// <param name="func">要运行的 System.Func。</param>
    /// <typeparam name="T">函数使用并返回的对象的类型。</typeparam>
    [SecuritySafeCritical]
    public static T RunImpersonated<T>(SafeAccessTokenHandle safeAccessTokenHandle, Func<T> func)
    {
      if (func == null)
        throw new ArgumentNullException("func");
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      WindowsIdentity wi = (WindowsIdentity) null;
      if (!safeAccessTokenHandle.IsInvalid)
        wi = new WindowsIdentity(safeAccessTokenHandle);
      T obj = default (T);
      using (WindowsIdentity.SafeImpersonate(safeAccessTokenHandle, wi, ref stackMark))
        return func();
    }

    /// <summary>模拟 <see cref="T:System.Security.Principal.WindowsIdentity" /> 对象表示的用户。</summary>
    /// <returns>表示模拟之前 Windows 用户的对象，这可以用于恢复为原始用户的上下文。</returns>
    /// <exception cref="T:System.InvalidOperationException">An anonymous identity attempted to perform an impersonation.</exception>
    /// <exception cref="T:System.Security.SecurityException">A Win32 error occurred.</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlPrincipal" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public virtual WindowsImpersonationContext Impersonate()
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.Impersonate(ref stackMark);
    }

    /// <summary>模拟指定用户标记表示的用户。</summary>
    /// <returns>表示模拟之前 Windows 用户的对象，该对象可以用于恢复为原始用户的上下文。</returns>
    /// <param name="userToken">Windows 帐户标记的句柄。通常，通过调用非托管代码（如调用 Win32 API LogonUser 函数）来检索此标记。</param>
    /// <exception cref="T:System.UnauthorizedAccessException">Windows returned the Windows NT status code STATUS_ACCESS_DENIED.</exception>
    /// <exception cref="T:System.OutOfMemoryException">There is insufficient memory available.</exception>
    /// <exception cref="T:System.Security.SecurityException">The caller does not have the correct permissions. </exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlPrincipal" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode | SecurityPermissionFlag.ControlPrincipal)]
    public static WindowsImpersonationContext Impersonate(IntPtr userToken)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      if (userToken == IntPtr.Zero)
        return WindowsIdentity.SafeRevertToSelf(ref stackMark);
      return new WindowsIdentity(userToken, (string) null, -1).Impersonate(ref stackMark);
    }

    [SecurityCritical]
    internal WindowsImpersonationContext Impersonate(ref StackCrawlMark stackMark)
    {
      if (this.m_safeTokenHandle.IsInvalid)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_AnonymousCannotImpersonate"));
      return WindowsIdentity.SafeImpersonate(this.m_safeTokenHandle, this, ref stackMark);
    }

    /// <summary>释放由 <see cref="T:System.Security.Principal.WindowsIdentity" /> 占用的非托管资源，还可以另外再释放托管资源。</summary>
    /// <param name="disposing">若要释放托管资源和非托管资源，则为 true；若仅释放非托管资源，则为 false。</param>
    [SecuritySafeCritical]
    [ComVisible(false)]
    protected virtual void Dispose(bool disposing)
    {
      if (disposing && this.m_safeTokenHandle != null && !this.m_safeTokenHandle.IsClosed)
        this.m_safeTokenHandle.Dispose();
      this.m_name = (string) null;
      this.m_owner = (SecurityIdentifier) null;
      this.m_user = (SecurityIdentifier) null;
    }

    /// <summary>释放由 <see cref="T:System.Security.Principal.WindowsIdentity" /> 使用的所有资源。</summary>
    [ComVisible(false)]
    public void Dispose()
    {
      this.Dispose(true);
    }

    [SecurityCritical]
    internal static WindowsImpersonationContext SafeRevertToSelf(ref StackCrawlMark stackMark)
    {
      return WindowsIdentity.SafeImpersonate(WindowsIdentity.s_invalidTokenHandle, (WindowsIdentity) null, ref stackMark);
    }

    [SecurityCritical]
    internal static WindowsImpersonationContext SafeImpersonate(SafeAccessTokenHandle userToken, WindowsIdentity wi, ref StackCrawlMark stackMark)
    {
      int hr = 0;
      bool isImpersonating;
      SafeAccessTokenHandle currentToken = WindowsIdentity.GetCurrentToken(TokenAccessLevels.MaximumAllowed, false, out isImpersonating, out hr);
      if (currentToken == null || currentToken.IsInvalid)
        throw new SecurityException(Win32Native.GetMessage(hr));
      FrameSecurityDescriptor securityObjectForFrame = SecurityRuntime.GetSecurityObjectForFrame(ref stackMark, true);
      if (securityObjectForFrame == null)
        throw new SecurityException(Environment.GetResourceString("ExecutionEngine_MissingSecurityDescriptor"));
      WindowsImpersonationContext impersonationContext = new WindowsImpersonationContext(currentToken, WindowsIdentity.GetCurrentThreadWI(), isImpersonating, securityObjectForFrame);
      if (userToken.IsInvalid)
      {
        int self = System.Security.Principal.Win32.RevertToSelf();
        if (self < 0)
          Environment.FailFast(Win32Native.GetMessage(self));
        WindowsIdentity.UpdateThreadWI(wi);
        securityObjectForFrame.SetTokenHandles(currentToken, wi == null ? (SafeAccessTokenHandle) null : wi.AccessToken);
      }
      else
      {
        int self = System.Security.Principal.Win32.RevertToSelf();
        if (self < 0)
          Environment.FailFast(Win32Native.GetMessage(self));
        if (System.Security.Principal.Win32.ImpersonateLoggedOnUser(userToken) < 0)
        {
          impersonationContext.Undo();
          throw new SecurityException(Environment.GetResourceString("Argument_ImpersonateUser"));
        }
        WindowsIdentity.UpdateThreadWI(wi);
        securityObjectForFrame.SetTokenHandles(currentToken, wi == null ? (SafeAccessTokenHandle) null : wi.AccessToken);
      }
      return impersonationContext;
    }

    [SecurityCritical]
    internal static WindowsIdentity GetCurrentThreadWI()
    {
      return SecurityContext.GetCurrentWI(Thread.CurrentThread.GetExecutionContextReader());
    }

    [SecurityCritical]
    internal static void UpdateThreadWI(WindowsIdentity wi)
    {
      Thread currentThread = Thread.CurrentThread;
      if (currentThread.GetExecutionContextReader().SecurityContext.WindowsIdentity == wi)
        return;
      ExecutionContext executionContext = currentThread.GetMutableExecutionContext();
      SecurityContext securityContext = executionContext.SecurityContext;
      if (wi != null && securityContext == null)
      {
        securityContext = new SecurityContext();
        executionContext.SecurityContext = securityContext;
      }
      if (securityContext == null)
        return;
      securityContext.WindowsIdentity = wi;
    }

    [SecurityCritical]
    internal static WindowsIdentity GetCurrentInternal(TokenAccessLevels desiredAccess, bool threadOnly)
    {
      int hr = 0;
      bool isImpersonating;
      SafeAccessTokenHandle currentToken = WindowsIdentity.GetCurrentToken(desiredAccess, threadOnly, out isImpersonating, out hr);
      if (currentToken == null || currentToken.IsInvalid)
      {
        if (threadOnly && !isImpersonating)
          return (WindowsIdentity) null;
        throw new SecurityException(Win32Native.GetMessage(hr));
      }
      WindowsIdentity windowsIdentity = new WindowsIdentity();
      windowsIdentity.m_safeTokenHandle.Dispose();
      SafeAccessTokenHandle accessTokenHandle = currentToken;
      windowsIdentity.m_safeTokenHandle = accessTokenHandle;
      return windowsIdentity;
    }

    internal static RuntimeConstructorInfo GetSpecialSerializationCtor()
    {
      return WindowsIdentity.s_specialSerializationCtor;
    }

    private static int GetHRForWin32Error(int dwLastError)
    {
      if (((long) dwLastError & 2147483648L) == 2147483648L)
        return dwLastError;
      return dwLastError & (int) ushort.MaxValue | -2147024896;
    }

    [SecurityCritical]
    private static Exception GetExceptionFromNtStatus(int status)
    {
      if (status == -1073741790)
        return (Exception) new UnauthorizedAccessException();
      if (status == -1073741670 || status == -1073741801)
        return (Exception) new OutOfMemoryException();
      return (Exception) new SecurityException(Win32Native.GetMessage(Win32Native.LsaNtStatusToWinError(status)));
    }

    [SecurityCritical]
    private static SafeAccessTokenHandle GetCurrentToken(TokenAccessLevels desiredAccess, bool threadOnly, out bool isImpersonating, out int hr)
    {
      isImpersonating = true;
      SafeAccessTokenHandle accessTokenHandle = WindowsIdentity.GetCurrentThreadToken(desiredAccess, out hr);
      if (accessTokenHandle == null && hr == WindowsIdentity.GetHRForWin32Error(1008))
      {
        isImpersonating = false;
        if (!threadOnly)
          accessTokenHandle = WindowsIdentity.GetCurrentProcessToken(desiredAccess, out hr);
      }
      return accessTokenHandle;
    }

    [SecurityCritical]
    private static SafeAccessTokenHandle GetCurrentProcessToken(TokenAccessLevels desiredAccess, out int hr)
    {
      hr = 0;
      SafeAccessTokenHandle TokenHandle;
      if (!Win32Native.OpenProcessToken(Win32Native.GetCurrentProcess(), desiredAccess, out TokenHandle))
        hr = WindowsIdentity.GetHRForWin32Error(Marshal.GetLastWin32Error());
      return TokenHandle;
    }

    [SecurityCritical]
    internal static SafeAccessTokenHandle GetCurrentThreadToken(TokenAccessLevels desiredAccess, out int hr)
    {
      SafeAccessTokenHandle phThreadToken;
      hr = System.Security.Principal.Win32.OpenThreadToken(desiredAccess, WinSecurityContext.Both, out phThreadToken);
      return phThreadToken;
    }

    [SecurityCritical]
    private T GetTokenInformation<T>(TokenInformationClass tokenInformationClass) where T : struct
    {
      using (SafeLocalAllocHandle tokenInformation = WindowsIdentity.GetTokenInformation(this.m_safeTokenHandle, tokenInformationClass))
        return tokenInformation.Read<T>(0UL);
    }

    [SecurityCritical]
    internal static ImpersonationQueryResult QueryImpersonation()
    {
      SafeAccessTokenHandle phThreadToken = (SafeAccessTokenHandle) null;
      int num = System.Security.Principal.Win32.OpenThreadToken(TokenAccessLevels.Query, WinSecurityContext.Thread, out phThreadToken);
      if (phThreadToken != null)
      {
        phThreadToken.Close();
        return ImpersonationQueryResult.Impersonated;
      }
      if (num == WindowsIdentity.GetHRForWin32Error(5))
        return ImpersonationQueryResult.Impersonated;
      return num == WindowsIdentity.GetHRForWin32Error(1008) ? ImpersonationQueryResult.NotImpersonated : ImpersonationQueryResult.Failed;
    }

    [SecurityCritical]
    private static Win32Native.LUID GetLogonAuthId(SafeAccessTokenHandle safeTokenHandle)
    {
      using (SafeLocalAllocHandle tokenInformation = WindowsIdentity.GetTokenInformation(safeTokenHandle, TokenInformationClass.TokenStatistics))
        return tokenInformation.Read<Win32Native.TOKEN_STATISTICS>(0UL).AuthenticationId;
    }

    [SecurityCritical]
    private static SafeLocalAllocHandle GetTokenInformation(SafeAccessTokenHandle tokenHandle, TokenInformationClass tokenInformationClass)
    {
      SafeLocalAllocHandle invalidHandle = SafeLocalAllocHandle.InvalidHandle;
      uint ReturnLength = (uint) Marshal.SizeOf(typeof (uint));
      Win32Native.GetTokenInformation(tokenHandle, (uint) tokenInformationClass, invalidHandle, 0U, out ReturnLength);
      int lastWin32Error = Marshal.GetLastWin32Error();
      switch (lastWin32Error)
      {
        case 6:
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidImpersonationToken"));
        case 24:
        case 122:
          UIntPtr sizetdwBytes = new UIntPtr(ReturnLength);
          invalidHandle.Dispose();
          SafeLocalAllocHandle TokenInformation = Win32Native.LocalAlloc(0, sizetdwBytes);
          if (TokenInformation == null || TokenInformation.IsInvalid)
            throw new OutOfMemoryException();
          TokenInformation.Initialize((ulong) ReturnLength);
          if (!Win32Native.GetTokenInformation(tokenHandle, (uint) tokenInformationClass, TokenInformation, ReturnLength, out ReturnLength))
            throw new SecurityException(Win32Native.GetMessage(Marshal.GetLastWin32Error()));
          return TokenInformation;
        default:
          throw new SecurityException(Win32Native.GetMessage(lastWin32Error));
      }
    }

    [SecurityCritical]
    [HandleProcessCorruptedStateExceptions]
    private static unsafe SafeAccessTokenHandle KerbS4ULogon(string upn, ref SafeAccessTokenHandle safeTokenHandle)
    {
      byte[] array = new byte[3]{ (byte) 67, (byte) 76, (byte) 82 };
      using (SafeLocalAllocHandle buffer1 = Win32Native.LocalAlloc(64, new UIntPtr((uint) (array.Length + 1))))
      {
        if (buffer1 == null || buffer1.IsInvalid)
          throw new OutOfMemoryException();
        buffer1.Initialize((ulong) array.Length + 1UL);
        buffer1.WriteArray<byte>(0UL, array, 0, array.Length);
        Win32Native.UNICODE_INTPTR_STRING unicodeIntptrString = new Win32Native.UNICODE_INTPTR_STRING(array.Length, buffer1);
        SafeLsaLogonProcessHandle LsaHandle = SafeLsaLogonProcessHandle.InvalidHandle;
        SafeLsaReturnBufferHandle ProfileBuffer = SafeLsaReturnBufferHandle.InvalidHandle;
        try
        {
          Privilege privilege = (Privilege) null;
          RuntimeHelpers.PrepareConstrainedRegions();
          int status1;
          try
          {
            try
            {
              privilege = new Privilege("SeTcbPrivilege");
              privilege.Enable();
            }
            catch (PrivilegeNotHeldException ex)
            {
            }
            IntPtr SecurityMode = IntPtr.Zero;
            status1 = Win32Native.LsaRegisterLogonProcess(ref unicodeIntptrString, out LsaHandle, out SecurityMode);
            if (5 == Win32Native.LsaNtStatusToWinError(status1))
              status1 = Win32Native.LsaConnectUntrusted(out LsaHandle);
          }
          catch
          {
            if (privilege != null)
              privilege.Revert();
            throw;
          }
          finally
          {
            if (privilege != null)
              privilege.Revert();
          }
          if (status1 < 0)
            throw WindowsIdentity.GetExceptionFromNtStatus(status1);
          byte[] numArray = new byte["Kerberos".Length + 1];
          Encoding.ASCII.GetBytes("Kerberos", 0, "Kerberos".Length, numArray, 0);
          using (SafeLocalAllocHandle buffer2 = Win32Native.LocalAlloc(0, new UIntPtr((uint) numArray.Length)))
          {
            if (buffer2 == null || buffer2.IsInvalid)
              throw new OutOfMemoryException();
            buffer2.Initialize((ulong) (uint) numArray.Length);
            buffer2.WriteArray<byte>(0UL, numArray, 0, numArray.Length);
            Win32Native.UNICODE_INTPTR_STRING PackageName = new Win32Native.UNICODE_INTPTR_STRING("Kerberos".Length, buffer2);
            uint AuthenticationPackage = 0;
            int status2 = Win32Native.LsaLookupAuthenticationPackage(LsaHandle, ref PackageName, out AuthenticationPackage);
            if (status2 < 0)
              throw WindowsIdentity.GetExceptionFromNtStatus(status2);
            Win32Native.TOKEN_SOURCE SourceContext = new Win32Native.TOKEN_SOURCE();
            if (!Win32Native.AllocateLocallyUniqueId(out SourceContext.SourceIdentifier))
              throw new SecurityException(Win32Native.GetMessage(Marshal.GetLastWin32Error()));
            SourceContext.Name = new char[8];
            SourceContext.Name[0] = 'C';
            SourceContext.Name[1] = 'L';
            SourceContext.Name[2] = 'R';
            uint ProfileBufferLength = 0;
            Win32Native.LUID LogonId = new Win32Native.LUID();
            Win32Native.QUOTA_LIMITS Quotas = new Win32Native.QUOTA_LIMITS();
            int SubStatus = 0;
            byte[] bytes = Encoding.Unicode.GetBytes(upn);
            uint num = (uint) (Marshal.SizeOf(typeof (Win32Native.KERB_S4U_LOGON)) + bytes.Length);
            using (SafeLocalAllocHandle localAllocHandle = Win32Native.LocalAlloc(64, new UIntPtr(num)))
            {
              if (localAllocHandle == null || localAllocHandle.IsInvalid)
                throw new OutOfMemoryException();
              localAllocHandle.Initialize((ulong) num);
              ulong byteOffset = (ulong) Marshal.SizeOf(typeof (Win32Native.KERB_S4U_LOGON));
              localAllocHandle.WriteArray<byte>(byteOffset, bytes, 0, bytes.Length);
              byte* pointer = (byte*) null;
              RuntimeHelpers.PrepareConstrainedRegions();
              try
              {
                localAllocHandle.AcquirePointer(ref pointer);
                localAllocHandle.Write<Win32Native.KERB_S4U_LOGON>(0UL, new Win32Native.KERB_S4U_LOGON()
                {
                  MessageType = 12U,
                  Flags = 0U,
                  ClientUpn = new Win32Native.UNICODE_INTPTR_STRING(bytes.Length, new IntPtr((void*) (pointer + byteOffset)))
                });
                int status3 = Win32Native.LsaLogonUser(LsaHandle, ref unicodeIntptrString, 3U, AuthenticationPackage, new IntPtr((void*) pointer), (uint) localAllocHandle.ByteLength, IntPtr.Zero, ref SourceContext, out ProfileBuffer, out ProfileBufferLength, out LogonId, out safeTokenHandle, out Quotas, out SubStatus);
                if (status3 == -1073741714 && SubStatus < 0)
                  status3 = SubStatus;
                if (status3 < 0)
                  throw WindowsIdentity.GetExceptionFromNtStatus(status3);
                if (SubStatus < 0)
                  throw WindowsIdentity.GetExceptionFromNtStatus(SubStatus);
              }
              finally
              {
                if ((IntPtr) pointer != IntPtr.Zero)
                  localAllocHandle.ReleasePointer();
              }
            }
            return safeTokenHandle;
          }
        }
        finally
        {
          if (!LsaHandle.IsInvalid)
            LsaHandle.Dispose();
          if (!ProfileBuffer.IsInvalid)
            ProfileBuffer.Dispose();
        }
      }
    }

    [SecurityCritical]
    internal IntPtr GetTokenInternal()
    {
      return this.m_safeTokenHandle.DangerousGetHandle();
    }

    internal ClaimsIdentity CloneAsBase()
    {
      return base.Clone();
    }

    /// <summary>创建作为当前实例副本的新对象。</summary>
    /// <returns>当前实例的副本。</returns>
    public override ClaimsIdentity Clone()
    {
      return (ClaimsIdentity) new WindowsIdentity(this);
    }

    [SecuritySafeCritical]
    private void InitializeClaims()
    {
      if (this.m_claimsInitialized)
        return;
      lock (this.m_claimsIntiailizedLock)
      {
        if (this.m_claimsInitialized)
          return;
        this.m_userClaims = new List<Claim>();
        this.m_deviceClaims = new List<Claim>();
        if (!string.IsNullOrEmpty(this.Name))
          this.m_userClaims.Add(new Claim(this.NameClaimType, this.Name, "http://www.w3.org/2001/XMLSchema#string", this.m_issuerName, this.m_issuerName, (ClaimsIdentity) this));
        this.AddPrimarySidClaim(this.m_userClaims);
        this.AddGroupSidClaims(this.m_userClaims);
        if (Environment.IsWindows8OrAbove)
        {
          this.AddDeviceGroupSidClaims(this.m_deviceClaims, TokenInformationClass.TokenDeviceGroups);
          this.AddTokenClaims(this.m_userClaims, TokenInformationClass.TokenUserClaimAttributes, "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsuserclaim");
          this.AddTokenClaims(this.m_deviceClaims, TokenInformationClass.TokenDeviceClaimAttributes, "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsdeviceclaim");
        }
        this.m_claimsInitialized = true;
      }
    }

    [SecurityCritical]
    private void AddDeviceGroupSidClaims(List<Claim> instanceClaims, TokenInformationClass tokenInformationClass)
    {
      if (this.m_safeTokenHandle.IsInvalid)
        return;
      SafeLocalAllocHandle localAllocHandle = SafeLocalAllocHandle.InvalidHandle;
      try
      {
        localAllocHandle = WindowsIdentity.GetTokenInformation(this.m_safeTokenHandle, tokenInformationClass);
        int num1 = Marshal.ReadInt32(localAllocHandle.DangerousGetHandle());
        IntPtr ptr = new IntPtr((long) localAllocHandle.DangerousGetHandle() + (long) Marshal.OffsetOf(typeof (Win32Native.TOKEN_GROUPS), "Groups"));
        for (int index = 0; index < num1; ++index)
        {
          Win32Native.SID_AND_ATTRIBUTES sidAndAttributes = (Win32Native.SID_AND_ATTRIBUTES) Marshal.PtrToStructure(ptr, typeof (Win32Native.SID_AND_ATTRIBUTES));
          uint num2 = 3221225492;
          SecurityIdentifier securityIdentifier = new SecurityIdentifier(sidAndAttributes.Sid, true);
          if (((int) sidAndAttributes.Attributes & (int) num2) == 4)
          {
            string type = "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsdevicegroup";
            instanceClaims.Add(new Claim(type, securityIdentifier.Value, "http://www.w3.org/2001/XMLSchema#string", this.m_issuerName, this.m_issuerName, (ClaimsIdentity) this, "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowssubauthority", Convert.ToString((object) securityIdentifier.IdentifierAuthority, (IFormatProvider) CultureInfo.InvariantCulture))
            {
              Properties = {
                {
                  type,
                  ""
                }
              }
            });
          }
          else if (((int) sidAndAttributes.Attributes & (int) num2) == 16)
          {
            string type = "http://schemas.microsoft.com/ws/2008/06/identity/claims/denyonlywindowsdevicegroup";
            instanceClaims.Add(new Claim(type, securityIdentifier.Value, "http://www.w3.org/2001/XMLSchema#string", this.m_issuerName, this.m_issuerName, (ClaimsIdentity) this, "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowssubauthority", Convert.ToString((object) securityIdentifier.IdentifierAuthority, (IFormatProvider) CultureInfo.InvariantCulture))
            {
              Properties = {
                {
                  type,
                  ""
                }
              }
            });
          }
          ptr = new IntPtr((long) ptr + Win32Native.SID_AND_ATTRIBUTES.SizeOf);
        }
      }
      finally
      {
        localAllocHandle.Close();
      }
    }

    [SecurityCritical]
    private void AddGroupSidClaims(List<Claim> instanceClaims)
    {
      if (this.m_safeTokenHandle.IsInvalid)
        return;
      SafeLocalAllocHandle localAllocHandle1 = SafeLocalAllocHandle.InvalidHandle;
      SafeLocalAllocHandle localAllocHandle2 = SafeLocalAllocHandle.InvalidHandle;
      try
      {
        localAllocHandle2 = WindowsIdentity.GetTokenInformation(this.m_safeTokenHandle, TokenInformationClass.TokenPrimaryGroup);
        SecurityIdentifier securityIdentifier1 = new SecurityIdentifier(((Win32Native.TOKEN_PRIMARY_GROUP) Marshal.PtrToStructure(localAllocHandle2.DangerousGetHandle(), typeof (Win32Native.TOKEN_PRIMARY_GROUP))).PrimaryGroup, true);
        bool flag = false;
        localAllocHandle1 = WindowsIdentity.GetTokenInformation(this.m_safeTokenHandle, TokenInformationClass.TokenGroups);
        int num1 = Marshal.ReadInt32(localAllocHandle1.DangerousGetHandle());
        IntPtr ptr = new IntPtr((long) localAllocHandle1.DangerousGetHandle() + (long) Marshal.OffsetOf(typeof (Win32Native.TOKEN_GROUPS), "Groups"));
        for (int index = 0; index < num1; ++index)
        {
          Win32Native.SID_AND_ATTRIBUTES sidAndAttributes = (Win32Native.SID_AND_ATTRIBUTES) Marshal.PtrToStructure(ptr, typeof (Win32Native.SID_AND_ATTRIBUTES));
          uint num2 = 3221225492;
          SecurityIdentifier securityIdentifier2 = new SecurityIdentifier(sidAndAttributes.Sid, true);
          if (((int) sidAndAttributes.Attributes & (int) num2) == 4)
          {
            if (!flag && StringComparer.Ordinal.Equals(securityIdentifier2.Value, securityIdentifier1.Value))
            {
              instanceClaims.Add(new Claim("http://schemas.microsoft.com/ws/2008/06/identity/claims/primarygroupsid", securityIdentifier2.Value, "http://www.w3.org/2001/XMLSchema#string", this.m_issuerName, this.m_issuerName, (ClaimsIdentity) this, "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowssubauthority", Convert.ToString((object) securityIdentifier2.IdentifierAuthority, (IFormatProvider) CultureInfo.InvariantCulture)));
              flag = true;
            }
            instanceClaims.Add(new Claim("http://schemas.microsoft.com/ws/2008/06/identity/claims/groupsid", securityIdentifier2.Value, "http://www.w3.org/2001/XMLSchema#string", this.m_issuerName, this.m_issuerName, (ClaimsIdentity) this, "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowssubauthority", Convert.ToString((object) securityIdentifier2.IdentifierAuthority, (IFormatProvider) CultureInfo.InvariantCulture)));
          }
          else if (((int) sidAndAttributes.Attributes & (int) num2) == 16)
          {
            if (!flag && StringComparer.Ordinal.Equals(securityIdentifier2.Value, securityIdentifier1.Value))
            {
              instanceClaims.Add(new Claim("http://schemas.microsoft.com/ws/2008/06/identity/claims/denyonlyprimarygroupsid", securityIdentifier2.Value, "http://www.w3.org/2001/XMLSchema#string", this.m_issuerName, this.m_issuerName, (ClaimsIdentity) this, "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowssubauthority", Convert.ToString((object) securityIdentifier2.IdentifierAuthority, (IFormatProvider) CultureInfo.InvariantCulture)));
              flag = true;
            }
            instanceClaims.Add(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/denyonlysid", securityIdentifier2.Value, "http://www.w3.org/2001/XMLSchema#string", this.m_issuerName, this.m_issuerName, (ClaimsIdentity) this, "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowssubauthority", Convert.ToString((object) securityIdentifier2.IdentifierAuthority, (IFormatProvider) CultureInfo.InvariantCulture)));
          }
          ptr = new IntPtr((long) ptr + Win32Native.SID_AND_ATTRIBUTES.SizeOf);
        }
      }
      finally
      {
        localAllocHandle1.Close();
        localAllocHandle2.Close();
      }
    }

    [SecurityCritical]
    private void AddPrimarySidClaim(List<Claim> instanceClaims)
    {
      if (this.m_safeTokenHandle.IsInvalid)
        return;
      SafeLocalAllocHandle localAllocHandle = SafeLocalAllocHandle.InvalidHandle;
      try
      {
        localAllocHandle = WindowsIdentity.GetTokenInformation(this.m_safeTokenHandle, TokenInformationClass.TokenUser);
        Win32Native.SID_AND_ATTRIBUTES sidAndAttributes = (Win32Native.SID_AND_ATTRIBUTES) Marshal.PtrToStructure(localAllocHandle.DangerousGetHandle(), typeof (Win32Native.SID_AND_ATTRIBUTES));
        uint num = 16;
        SecurityIdentifier securityIdentifier = new SecurityIdentifier(sidAndAttributes.Sid, true);
        if ((int) sidAndAttributes.Attributes == 0)
        {
          instanceClaims.Add(new Claim("http://schemas.microsoft.com/ws/2008/06/identity/claims/primarysid", securityIdentifier.Value, "http://www.w3.org/2001/XMLSchema#string", this.m_issuerName, this.m_issuerName, (ClaimsIdentity) this, "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowssubauthority", Convert.ToString((object) securityIdentifier.IdentifierAuthority, (IFormatProvider) CultureInfo.InvariantCulture)));
        }
        else
        {
          if (((int) sidAndAttributes.Attributes & (int) num) != 16)
            return;
          instanceClaims.Add(new Claim("http://schemas.microsoft.com/ws/2008/06/identity/claims/denyonlyprimarysid", securityIdentifier.Value, "http://www.w3.org/2001/XMLSchema#string", this.m_issuerName, this.m_issuerName, (ClaimsIdentity) this, "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowssubauthority", Convert.ToString((object) securityIdentifier.IdentifierAuthority, (IFormatProvider) CultureInfo.InvariantCulture)));
        }
      }
      finally
      {
        localAllocHandle.Close();
      }
    }

    [SecurityCritical]
    private void AddTokenClaims(List<Claim> instanceClaims, TokenInformationClass tokenInformationClass, string propertyValue)
    {
      if (this.m_safeTokenHandle.IsInvalid)
        return;
      SafeLocalAllocHandle localAllocHandle = SafeLocalAllocHandle.InvalidHandle;
      try
      {
        SafeLocalAllocHandle invalidHandle = SafeLocalAllocHandle.InvalidHandle;
        localAllocHandle = WindowsIdentity.GetTokenInformation(this.m_safeTokenHandle, tokenInformationClass);
        Win32Native.CLAIM_SECURITY_ATTRIBUTES_INFORMATION attributesInformation = (Win32Native.CLAIM_SECURITY_ATTRIBUTES_INFORMATION) Marshal.PtrToStructure(localAllocHandle.DangerousGetHandle(), typeof (Win32Native.CLAIM_SECURITY_ATTRIBUTES_INFORMATION));
        long num = 0;
        for (int index1 = 0; (long) index1 < (long) attributesInformation.AttributeCount; ++index1)
        {
          Win32Native.CLAIM_SECURITY_ATTRIBUTE_V1 structure = (Win32Native.CLAIM_SECURITY_ATTRIBUTE_V1) Marshal.PtrToStructure(new IntPtr(attributesInformation.Attribute.pAttributeV1.ToInt64() + num), typeof (Win32Native.CLAIM_SECURITY_ATTRIBUTE_V1));
          switch (structure.ValueType)
          {
            case 1:
              long[] destination1 = new long[(int) structure.ValueCount];
              Marshal.Copy(structure.Values.pInt64, destination1, 0, (int) structure.ValueCount);
              for (int index2 = 0; (long) index2 < (long) structure.ValueCount; ++index2)
                instanceClaims.Add(new Claim(structure.Name, Convert.ToString(destination1[index2], (IFormatProvider) CultureInfo.InvariantCulture), "http://www.w3.org/2001/XMLSchema#integer64", this.m_issuerName, this.m_issuerName, (ClaimsIdentity) this, propertyValue, string.Empty));
              break;
            case 2:
              long[] destination2 = new long[(int) structure.ValueCount];
              Marshal.Copy(structure.Values.pUint64, destination2, 0, (int) structure.ValueCount);
              for (int index2 = 0; (long) index2 < (long) structure.ValueCount; ++index2)
                instanceClaims.Add(new Claim(structure.Name, Convert.ToString((ulong) destination2[index2], (IFormatProvider) CultureInfo.InvariantCulture), "http://www.w3.org/2001/XMLSchema#uinteger64", this.m_issuerName, this.m_issuerName, (ClaimsIdentity) this, propertyValue, string.Empty));
              break;
            case 3:
              IntPtr[] destination3 = new IntPtr[(int) structure.ValueCount];
              Marshal.Copy(structure.Values.ppString, destination3, 0, (int) structure.ValueCount);
              for (int index2 = 0; (long) index2 < (long) structure.ValueCount; ++index2)
                instanceClaims.Add(new Claim(structure.Name, Marshal.PtrToStringAuto(destination3[index2]), "http://www.w3.org/2001/XMLSchema#string", this.m_issuerName, this.m_issuerName, (ClaimsIdentity) this, propertyValue, string.Empty));
              break;
            case 6:
              long[] destination4 = new long[(int) structure.ValueCount];
              Marshal.Copy(structure.Values.pUint64, destination4, 0, (int) structure.ValueCount);
              for (int index2 = 0; (long) index2 < (long) structure.ValueCount; ++index2)
                instanceClaims.Add(new Claim(structure.Name, destination4[index2] == 0L ? Convert.ToString(false, (IFormatProvider) CultureInfo.InvariantCulture) : Convert.ToString(true, (IFormatProvider) CultureInfo.InvariantCulture), "http://www.w3.org/2001/XMLSchema#boolean", this.m_issuerName, this.m_issuerName, (ClaimsIdentity) this, propertyValue, string.Empty));
              break;
          }
          num += (long) Marshal.SizeOf<Win32Native.CLAIM_SECURITY_ATTRIBUTE_V1>(structure);
        }
      }
      finally
      {
        localAllocHandle.Close();
      }
    }
  }
}
