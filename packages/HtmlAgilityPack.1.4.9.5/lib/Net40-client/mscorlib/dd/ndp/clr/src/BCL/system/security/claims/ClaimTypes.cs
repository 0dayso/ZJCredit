// Decompiled with JetBrains decompiler
// Type: System.Security.Claims.ClaimTypes
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Claims
{
  /// <summary>定义可分配到主题上的显着的声明类型的常量。此类不能被继承。</summary>
  [ComVisible(false)]
  public static class ClaimTypes
  {
    internal const string ClaimTypeNamespace = "http://schemas.microsoft.com/ws/2008/06/identity/claims";
    /// <summary>指定授权的实例实体；http://schemas.microsoft.com/ws/2008/06/identity/claims/authenticationinstant 的 URI 声明。</summary>
    public const string AuthenticationInstant = "http://schemas.microsoft.com/ws/2008/06/identity/claims/authenticationinstant";
    /// <summary>指定授权的实体方法；http://schemas.microsoft.com/ws/2008/06/identity/claims/authenticationmethod 的 URI 声明。</summary>
    public const string AuthenticationMethod = "http://schemas.microsoft.com/ws/2008/06/identity/claims/authenticationmethod";
    /// <summary>指定 cookie 路径；http://schemas.microsoft.com/ws/2008/06/identity/claims/cookiepath 的 URI 声明。</summary>
    public const string CookiePath = "http://schemas.microsoft.com/ws/2008/06/identity/claims/cookiepath";
    /// <summary>指定 deny-only 主要 SID 的实体；http://schemas.microsoft.com/ws/2008/06/identity/claims/denyonlyprimarysid 的 URI 声明。deny-only SID 禁止指定的实体访问可保护对象。</summary>
    public const string DenyOnlyPrimarySid = "http://schemas.microsoft.com/ws/2008/06/identity/claims/denyonlyprimarysid";
    /// <summary>指定 deny-only 主要团队 SID 的实体；http://schemas.microsoft.com/ws/2008/06/identity/claims/denyonlyprimarygroupsid 的 URI 声明。deny-only SID 禁止指定的实体访问可保护对象。</summary>
    public const string DenyOnlyPrimaryGroupSid = "http://schemas.microsoft.com/ws/2008/06/identity/claims/denyonlyprimarygroupsid";
    /// <summary>http://schemas.microsoft.com/ws/2008/06/identity/claims/denyonlywindowsdevicegroup.</summary>
    public const string DenyOnlyWindowsDeviceGroup = "http://schemas.microsoft.com/ws/2008/06/identity/claims/denyonlywindowsdevicegroup";
    /// <summary>http://schemas.microsoft.com/ws/2008/06/identity/claims/dsa。</summary>
    public const string Dsa = "http://schemas.microsoft.com/ws/2008/06/identity/claims/dsa";
    /// <summary>http://schemas.microsoft.com/ws/2008/06/identity/claims/expiration。</summary>
    public const string Expiration = "http://schemas.microsoft.com/ws/2008/06/identity/claims/expiration";
    /// <summary>http://schemas.microsoft.com/ws/2008/06/identity/claims/expired。</summary>
    public const string Expired = "http://schemas.microsoft.com/ws/2008/06/identity/claims/expired";
    /// <summary>为团队指定 SID 的实体，http://schemas.microsoft.com/ws/2008/06/identity/claims/groupsid 的 URI 声明。</summary>
    public const string GroupSid = "http://schemas.microsoft.com/ws/2008/06/identity/claims/groupsid";
    /// <summary>http://schemas.microsoft.com/ws/2008/06/identity/claims/ispersistent。</summary>
    public const string IsPersistent = "http://schemas.microsoft.com/ws/2008/06/identity/claims/ispersistent";
    /// <summary>指定实体主要团队 SID，http://schemas.microsoft.com/ws/2008/06/identity/claims/primarygroupsid 的 URI 声明。</summary>
    public const string PrimaryGroupSid = "http://schemas.microsoft.com/ws/2008/06/identity/claims/primarygroupsid";
    /// <summary>指定实体的主要 SID，http://schemas.microsoft.com/ws/2008/06/identity/claims/primarysid 的 URI 声明。</summary>
    public const string PrimarySid = "http://schemas.microsoft.com/ws/2008/06/identity/claims/primarysid";
    /// <summary>指定实体的角色，http://schemas.microsoft.com/ws/2008/06/identity/claims/role 的 URI 声明。</summary>
    public const string Role = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
    /// <summary>指定序列号，http://schemas.microsoft.com/ws/2008/06/identity/claims/serialnumber 的 URI 声明。</summary>
    public const string SerialNumber = "http://schemas.microsoft.com/ws/2008/06/identity/claims/serialnumber";
    /// <summary>http://schemas.microsoft.com/ws/2008/06/identity/claims/userdata。</summary>
    public const string UserData = "http://schemas.microsoft.com/ws/2008/06/identity/claims/userdata";
    /// <summary>http://schemas.microsoft.com/ws/2008/06/identity/claims/version。</summary>
    public const string Version = "http://schemas.microsoft.com/ws/2008/06/identity/claims/version";
    /// <summary>指定 Windows 域帐户名，http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsaccountname 的 URI 声明。</summary>
    public const string WindowsAccountName = "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsaccountname";
    /// <summary>http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsdeviceclaim。</summary>
    public const string WindowsDeviceClaim = "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsdeviceclaim";
    /// <summary>http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsdevicegroup.</summary>
    public const string WindowsDeviceGroup = "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsdevicegroup";
    /// <summary>http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsuserclaim。</summary>
    public const string WindowsUserClaim = "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsuserclaim";
    /// <summary>http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsfqbnversion。</summary>
    public const string WindowsFqbnVersion = "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsfqbnversion";
    /// <summary>http://schemas.microsoft.com/ws/2008/06/identity/claims/windowssubauthority。</summary>
    public const string WindowsSubAuthority = "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowssubauthority";
    internal const string ClaimType2005Namespace = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims";
    /// <summary>指定用户主体名称 (UPN)；http://schemas.xmlsoap.org/ws/2005/05/identity/claims/upn 的 URI 声明。</summary>
    public const string Anonymous = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/anonymous";
    /// <summary>指定特定有关标识是否已授权的细节，http://schemas.xmlsoap.org/ws/2005/05/identity/claims/authenticated 的 URI 声明。</summary>
    public const string Authentication = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/authentication";
    /// <summary>指定对于实体的授权决定，http://schemas.xmlsoap.org/ws/2005/05/identity/claims/authorizationdecision 的 URI 声明。</summary>
    public const string AuthorizationDecision = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/authorizationdecision";
    /// <summary>指定对于国家/地区实体驻留，http://schemas.xmlsoap.org/ws/2005/05/identity/claims/authorizationdecision 的 URI 声明。</summary>
    public const string Country = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/country";
    /// <summary>指定实体的备用电话号码，http://schemas.xmlsoap.org/ws/2005/05/identity/claims/otherphone URI 声明。</summary>
    public const string DateOfBirth = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/dateofbirth";
    /// <summary>获取声明的 URI，该 URI 指定与计算机名称关联的 DNS 名称或者与 X.509 证书的使用者或颁发者的备用名称关联的 DNS 名称，http://schemas.xmlsoap.org/ws/2005/05/identity/claims/dns 。</summary>
    public const string Dns = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/dns";
    /// <summary>为实体指定拒绝安全标识符 (SID) 要求，http://schemas.xmlsoap.org/ws/2005/05/identity/claims/denyonlysid 的 URI 声明。deny-only SID 禁止指定的实体访问可保护对象。</summary>
    public const string DenyOnlySid = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/denyonlysid";
    /// <summary>指定实体的电子邮件地址，http://schemas.xmlsoap.org/ws/2005/05/identity/claims/email 的 URI 声明。</summary>
    public const string Email = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";
    /// <summary>指定实体的性别，http://schemas.xmlsoap.org/ws/2005/05/identity/claims/gender 的 URI 声明。</summary>
    public const string Gender = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/gender";
    /// <summary>指定实体的给定名称，http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname 的 URI 声明。</summary>
    public const string GivenName = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname";
    /// <summary>指定哈希值，http://schemas.xmlsoap.org/ws/2005/05/identity/claims/system/hash 的 URI 声明。</summary>
    public const string Hash = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/hash";
    /// <summary>指定实体的住宅电话号码，http://schemas.xmlsoap.org/ws/2005/05/identity/claims/homephone 的 URI 声明。</summary>
    public const string HomePhone = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/homephone";
    /// <summary>指定区域实体驻留，http://schemas.xmlsoap.org/ws/2005/05/identity/claims/locality 的 URI 声明。</summary>
    public const string Locality = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/locality";
    /// <summary>指定实体的移动电话号码，http://schemas.xmlsoap.org/ws/2005/05/identity/claims/mobilephone 的 URI 声明。</summary>
    public const string MobilePhone = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/mobilephone";
    /// <summary>指定实体的名称，http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name 的 URI 声明。</summary>
    public const string Name = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
    /// <summary>指定实体的名称，http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier 的 URI 声明。</summary>
    public const string NameIdentifier = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
    /// <summary>指定实体的备用电话号码，http://schemas.xmlsoap.org/ws/2005/05/identity/claims/otherphone 的 URI 声明。</summary>
    public const string OtherPhone = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/otherphone";
    /// <summary>指定实体的邮政编码，http://schemas.xmlsoap.org/ws/2005/05/identity/claims/postalcode 的 URI 声明。</summary>
    public const string PostalCode = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/postalcode";
    /// <summary>指定 RSA 密钥，http://schemas.xmlsoap.org/ws/2005/05/identity/claims/rsa 的 URI 声明。</summary>
    public const string Rsa = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/rsa";
    /// <summary>指定安全标识符 （SID），http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid 的URI 声明。</summary>
    public const string Sid = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid";
    /// <summary>指定服务主体名称 (SPN) 声明，http://schemas.xmlsoap.org/ws/2005/05/identity/claims/spn 的 URI 声明。</summary>
    public const string Spn = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/spn";
    /// <summary>指定省/直辖市/自治区实体驻留，http://schemas.xmlsoap.org/ws/2005/05/identity/claims/stateorprovince 的 URI 声明。</summary>
    public const string StateOrProvince = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/stateorprovince";
    /// <summary>指定实体的街道地址，http://schemas.xmlsoap.org/ws/2005/05/identity/claims/streetaddress 的 URI 声明。</summary>
    public const string StreetAddress = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/streetaddress";
    /// <summary>指定实体的姓氏，http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname 的 URI 声明。</summary>
    public const string Surname = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname";
    /// <summary>确认系统实体，http://schemas.xmlsoap.org/ws/2005/05/identity/claims/system 的声明 URI。</summary>
    public const string System = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/system";
    /// <summary>指定指纹，http://schemas.xmlsoap.org/ws/2005/05/identity/claims/thumbprint 的 URI 声明。指纹是 X.509 证书的全局唯一 SHA-1 哈希。</summary>
    public const string Thumbprint = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/thumbprint";
    /// <summary>指定用户主体名称 (UPN)，http://schemas.xmlsoap.org/ws/2005/05/identity/claims/upn 的 URI 声明。</summary>
    public const string Upn = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/upn";
    /// <summary>指定 URI，http://schemas.xmlsoap.org/ws/2005/05/identity/claims/uri 的 URI 声明。</summary>
    public const string Uri = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/uri";
    /// <summary>指定实体的网页，http://schemas.xmlsoap.org/ws/2005/05/identity/claims/webpage 的 URI 声明。</summary>
    public const string Webpage = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/webpage";
    /// <summary>X.509 证书的识别名名称，http://schemas.xmlsoap.org/ws/2005/05/identity/claims/x500distinguishedname 的 URI 声明。X.500 标准规定了用于定义 X.509 证书所使用的可分辨名称的方法。</summary>
    public const string X500DistinguishedName = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/x500distinguishedname";
    internal const string ClaimType2009Namespace = "http://schemas.xmlsoap.org/ws/2009/09/identity/claims";
    /// <summary>http://schemas.xmlsoap.org/ws/2009/09/identity/claims/actor。</summary>
    public const string Actor = "http://schemas.xmlsoap.org/ws/2009/09/identity/claims/actor";
  }
}
