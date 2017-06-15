// Decompiled with JetBrains decompiler
// Type: System.Security.Claims.ClaimsIdentity
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;
using System.Security.Principal;

namespace System.Security.Claims
{
  /// <summary>委托基于声明的标识。</summary>
  [ComVisible(true)]
  [Serializable]
  public class ClaimsIdentity : IIdentity
  {
    [NonSerialized]
    private List<Claim> m_instanceClaims = new List<Claim>();
    [NonSerialized]
    private Collection<IEnumerable<Claim>> m_externalClaims = new Collection<IEnumerable<Claim>>();
    [NonSerialized]
    private string m_nameType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
    [NonSerialized]
    private string m_roleType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
    [OptionalField(VersionAdded = 2)]
    private string m_version = "1.0";
    [NonSerialized]
    private byte[] m_userSerializationData;
    [NonSerialized]
    private const string PreFix = "System.Security.ClaimsIdentity.";
    [NonSerialized]
    private const string ActorKey = "System.Security.ClaimsIdentity.actor";
    [NonSerialized]
    private const string AuthenticationTypeKey = "System.Security.ClaimsIdentity.authenticationType";
    [NonSerialized]
    private const string BootstrapContextKey = "System.Security.ClaimsIdentity.bootstrapContext";
    [NonSerialized]
    private const string ClaimsKey = "System.Security.ClaimsIdentity.claims";
    [NonSerialized]
    private const string LabelKey = "System.Security.ClaimsIdentity.label";
    [NonSerialized]
    private const string NameClaimTypeKey = "System.Security.ClaimsIdentity.nameClaimType";
    [NonSerialized]
    private const string RoleClaimTypeKey = "System.Security.ClaimsIdentity.roleClaimType";
    [NonSerialized]
    private const string VersionKey = "System.Security.ClaimsIdentity.version";
    /// <summary>默认颁发者；“地方当局”。</summary>
    [NonSerialized]
    public const string DefaultIssuer = "LOCAL AUTHORITY";
    /// <summary>默认名称声明类型；<see cref="F:System.Security.Claims.ClaimTypes.Name" />。</summary>
    [NonSerialized]
    public const string DefaultNameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
    /// <summary>默认角色声明类型；<see cref="F:System.Security.Claims.ClaimTypes.Role" />。</summary>
    [NonSerialized]
    public const string DefaultRoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
    [OptionalField(VersionAdded = 2)]
    private ClaimsIdentity m_actor;
    [OptionalField(VersionAdded = 2)]
    private string m_authenticationType;
    [OptionalField(VersionAdded = 2)]
    private object m_bootstrapContext;
    [OptionalField(VersionAdded = 2)]
    private string m_label;
    [OptionalField(VersionAdded = 2)]
    private string m_serializedNameType;
    [OptionalField(VersionAdded = 2)]
    private string m_serializedRoleType;
    [OptionalField(VersionAdded = 2)]
    private string m_serializedClaims;

    /// <summary>获取身份验证类型。</summary>
    /// <returns>身份验证类型。</returns>
    public virtual string AuthenticationType
    {
      get
      {
        return this.m_authenticationType;
      }
    }

    /// <summary>获取一个值，该值指示是否验证了标识。</summary>
    /// <returns>如果标识已经验证，则为 true；否则，为 false。</returns>
    public virtual bool IsAuthenticated
    {
      get
      {
        return !string.IsNullOrEmpty(this.m_authenticationType);
      }
    }

    /// <summary>获取或设置被授予委派权利的调用方的标识。</summary>
    /// <returns>授予委托权利的调用方。</returns>
    /// <exception cref="T:System.InvalidOperationException">尝试设置当前实例的属性。</exception>
    public ClaimsIdentity Actor
    {
      get
      {
        return this.m_actor;
      }
      set
      {
        if (value != null && this.IsCircular(value))
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperationException_ActorGraphCircular"));
        this.m_actor = value;
      }
    }

    /// <summary>获取或设置用于创建此声明标识的令牌。</summary>
    /// <returns>启动上下文。</returns>
    public object BootstrapContext
    {
      get
      {
        return this.m_bootstrapContext;
      }
      [SecurityCritical] set
      {
        this.m_bootstrapContext = value;
      }
    }

    /// <summary>获取与此声明标识关联的声明。</summary>
    /// <returns>与此声明标识相关联的声明的集合。</returns>
    public virtual IEnumerable<Claim> Claims
    {
      get
      {
        for (int i = 0; i < this.m_instanceClaims.Count; ++i)
          yield return this.m_instanceClaims[i];
        if (this.m_externalClaims != null)
        {
          for (int j = 0; j < this.m_externalClaims.Count; ++j)
          {
            if (this.m_externalClaims[j] != null)
            {
              foreach (Claim claim in this.m_externalClaims[j])
                yield return claim;
            }
          }
        }
      }
    }

    protected virtual byte[] CustomSerializationData
    {
      get
      {
        return this.m_userSerializationData;
      }
    }

    internal Collection<IEnumerable<Claim>> ExternalClaims
    {
      [FriendAccessAllowed] get
      {
        return this.m_externalClaims;
      }
    }

    /// <summary>获取或设置此声明标识的标签。</summary>
    /// <returns>标签。</returns>
    public string Label
    {
      get
      {
        return this.m_label;
      }
      set
      {
        this.m_label = value;
      }
    }

    /// <summary>获取此声明标识的名称。</summary>
    /// <returns>姓名或 null。</returns>
    public virtual string Name
    {
      get
      {
        Claim first = this.FindFirst(this.m_nameType);
        if (first != null)
          return first.Value;
        return (string) null;
      }
    }

    /// <summary>获取用于确定为此声明标识的 <see cref="P:System.Security.Claims.ClaimsIdentity.Name" /> 属性提供值的声明的声明类型。</summary>
    /// <returns>名称声明类型。</returns>
    public string NameClaimType
    {
      get
      {
        return this.m_nameType;
      }
    }

    /// <summary>获取将解释为此声明标识中声明的 .NET Framework 角色的声明类型。</summary>
    /// <returns>角色声明类型。</returns>
    public string RoleClaimType
    {
      get
      {
        return this.m_roleType;
      }
    }

    /// <summary>用空声称集合初始化 <see cref="T:System.Security.Claims.ClaimsIdentity" /> 类的新实例。</summary>
    public ClaimsIdentity()
      : this((IEnumerable<Claim>) null)
    {
    }

    /// <summary>初始化 <see cref="T:System.Security.Claims.ClaimsIdentity" /> 类的新实例，该类表示具有指定的 <see cref="T:System.Security.Principal.IIdentity" /> 用户。</summary>
    /// <param name="identity">新声明标识所基于的标识。</param>
    public ClaimsIdentity(IIdentity identity)
      : this(identity, (IEnumerable<Claim>) null)
    {
    }

    /// <summary>使用枚举集合 <see cref="T:System.Security.Claims.Claim" /> 对象的初始化 <see cref="T:System.Security.Claims.ClaimsIdentity" /> 类的新实例。</summary>
    /// <param name="claims">传播声明标识的声明。</param>
    public ClaimsIdentity(IEnumerable<Claim> claims)
      : this((IIdentity) null, claims, (string) null, (string) null, (string) null)
    {
    }

    /// <summary>用空的声明集合和指定的身份验证类型初始化 <see cref="T:System.Security.Claims.ClaimsIdentity" /> 类的新实例。</summary>
    /// <param name="authenticationType">所使用的身份验证的类型。</param>
    public ClaimsIdentity(string authenticationType)
      : this((IIdentity) null, (IEnumerable<Claim>) null, authenticationType, (string) null, (string) null)
    {
    }

    /// <summary>用指定的声称和身份验证类型初始化 <see cref="T:System.Security.Claims.ClaimsIdentity" /> 类的新实例。</summary>
    /// <param name="claims">传播声明标识的声明。</param>
    /// <param name="authenticationType">所使用的身份验证的类型。</param>
    public ClaimsIdentity(IEnumerable<Claim> claims, string authenticationType)
      : this((IIdentity) null, claims, authenticationType, (string) null, (string) null)
    {
    }

    /// <summary>使用指定的 <see cref="T:System.Security.Principal.IIdentity" /> 和类型提供程序初始化 <see cref="T:System.Security.Claims.ClaimsIdentity" /> 类的新实例。</summary>
    /// <param name="identity">新声明标识所基于的标识。</param>
    /// <param name="claims">传播声明标识的声明。</param>
    public ClaimsIdentity(IIdentity identity, IEnumerable<Claim> claims)
      : this(identity, claims, (string) null, (string) null, (string) null)
    {
    }

    /// <summary>用指定的声明、身份验证的类型、名称声明类型、角色声明类型来初始化 <see cref="T:System.Security.Claims.ClaimsIdentity" /> 类的新实例。</summary>
    /// <param name="authenticationType">所使用的身份验证的类型。</param>
    /// <param name="nameType">要用于名称声明的声明类型。</param>
    /// <param name="roleType">要用于角色声明的声明类型。</param>
    public ClaimsIdentity(string authenticationType, string nameType, string roleType)
      : this((IIdentity) null, (IEnumerable<Claim>) null, authenticationType, nameType, roleType)
    {
    }

    /// <summary>用指定的声明、身份验证的类型、名称声明类型、角色声明类型来初始化 <see cref="T:System.Security.Claims.ClaimsIdentity" /> 类的新实例。</summary>
    /// <param name="claims">传播声明标识的声明。</param>
    /// <param name="authenticationType">所使用的身份验证的类型。</param>
    /// <param name="nameType">要用于名称声明的声明类型。</param>
    /// <param name="roleType">要用于角色声明的声明类型。</param>
    public ClaimsIdentity(IEnumerable<Claim> claims, string authenticationType, string nameType, string roleType)
      : this((IIdentity) null, claims, authenticationType, nameType, roleType)
    {
    }

    /// <summary>从指定的 <see cref="T:System.Security.Principal.IIdentity" /> 用指定的声明、身份验证的类型、名称声明类型、角色声明类型来初始化 <see cref="T:System.Security.Claims.ClaimsIdentity" /> 类的新实例。</summary>
    /// <param name="identity">新声明标识所基于的标识。</param>
    /// <param name="claims">传播新声明标识的声明。</param>
    /// <param name="authenticationType">所使用的身份验证的类型。</param>
    /// <param name="nameType">要用于名称声明的声明类型。</param>
    /// <param name="roleType">要用于角色声明的声明类型。</param>
    public ClaimsIdentity(IIdentity identity, IEnumerable<Claim> claims, string authenticationType, string nameType, string roleType)
      : this(identity, claims, authenticationType, nameType, roleType, true)
    {
    }

    internal ClaimsIdentity(IIdentity identity, IEnumerable<Claim> claims, string authenticationType, string nameType, string roleType, bool checkAuthType)
    {
      bool flag1 = false;
      bool flag2 = false;
      if (checkAuthType && identity != null && string.IsNullOrEmpty(authenticationType))
      {
        if (identity is WindowsIdentity)
        {
          try
          {
            this.m_authenticationType = identity.AuthenticationType;
          }
          catch (UnauthorizedAccessException ex)
          {
            this.m_authenticationType = (string) null;
          }
        }
        else
          this.m_authenticationType = identity.AuthenticationType;
      }
      else
        this.m_authenticationType = authenticationType;
      if (!string.IsNullOrEmpty(nameType))
      {
        this.m_nameType = nameType;
        flag1 = true;
      }
      if (!string.IsNullOrEmpty(roleType))
      {
        this.m_roleType = roleType;
        flag2 = true;
      }
      ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
      if (claimsIdentity != null)
      {
        this.m_label = claimsIdentity.m_label;
        if (!flag1)
          this.m_nameType = claimsIdentity.m_nameType;
        if (!flag2)
          this.m_roleType = claimsIdentity.m_roleType;
        this.m_bootstrapContext = claimsIdentity.m_bootstrapContext;
        if (claimsIdentity.Actor != null)
        {
          if (this.IsCircular(claimsIdentity.Actor))
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperationException_ActorGraphCircular"));
          this.m_actor = claimsIdentity.Actor;
        }
        if (claimsIdentity is WindowsIdentity && !(this is WindowsIdentity))
          this.SafeAddClaims(claimsIdentity.Claims);
        else
          this.SafeAddClaims((IEnumerable<Claim>) claimsIdentity.m_instanceClaims);
      }
      else if (identity != null && !string.IsNullOrEmpty(identity.Name))
        this.SafeAddClaim(new Claim(this.m_nameType, identity.Name, "http://www.w3.org/2001/XMLSchema#string", "LOCAL AUTHORITY", "LOCAL AUTHORITY", this));
      if (claims == null)
        return;
      this.SafeAddClaims(claims);
    }

    public ClaimsIdentity(BinaryReader reader)
    {
      if (reader == null)
        throw new ArgumentNullException("reader");
      this.Initialize(reader);
    }

    protected ClaimsIdentity(ClaimsIdentity other)
    {
      if (other == null)
        throw new ArgumentNullException("other");
      if (other.m_actor != null)
        this.m_actor = other.m_actor.Clone();
      this.m_authenticationType = other.m_authenticationType;
      this.m_bootstrapContext = other.m_bootstrapContext;
      this.m_label = other.m_label;
      this.m_nameType = other.m_nameType;
      this.m_roleType = other.m_roleType;
      if (other.m_userSerializationData != null)
        this.m_userSerializationData = other.m_userSerializationData.Clone() as byte[];
      this.SafeAddClaims((IEnumerable<Claim>) other.m_instanceClaims);
    }

    /// <summary>初始化从使用 <see cref="T:System.Runtime.Serialization.ISerializable" /> 创建的序列化流创建中的 <see cref="T:System.Security.Claims.ClaimsIdentity" /> 类的新实例。</summary>
    /// <param name="info">序列化的数据。</param>
    /// <param name="context">序列化的上下文。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="info" /> 为 null。</exception>
    [SecurityCritical]
    protected ClaimsIdentity(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException("info");
      this.Deserialize(info, context, true);
    }

    /// <summary>初始化从使用 <see cref="T:System.Runtime.Serialization.ISerializable" /> 创建的序列化流创建中的 <see cref="T:System.Security.Claims.ClaimsIdentity" /> 类的新实例。</summary>
    /// <param name="info">序列化的数据。</param>
    [SecurityCritical]
    protected ClaimsIdentity(SerializationInfo info)
    {
      if (info == null)
        throw new ArgumentNullException("info");
      StreamingContext context = new StreamingContext();
      this.Deserialize(info, context, false);
    }

    /// <summary>返回从此声明标识中复制的新 <see cref="T:System.Security.Claims.ClaimsIdentity" />。</summary>
    /// <returns>当前实例的副本。</returns>
    public virtual ClaimsIdentity Clone()
    {
      ClaimsIdentity claimsIdentity = new ClaimsIdentity((IEnumerable<Claim>) this.m_instanceClaims);
      claimsIdentity.m_authenticationType = this.m_authenticationType;
      claimsIdentity.m_bootstrapContext = this.m_bootstrapContext;
      claimsIdentity.m_label = this.m_label;
      claimsIdentity.m_nameType = this.m_nameType;
      claimsIdentity.m_roleType = this.m_roleType;
      if (this.Actor != null)
      {
        if (this.IsCircular(this.Actor))
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperationException_ActorGraphCircular"));
        claimsIdentity.Actor = this.Actor;
      }
      return claimsIdentity;
    }

    /// <summary>添加单个声明到此声明标识。</summary>
    /// <param name="claim">要添加的声明。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="claim" /> 为 null。</exception>
    [SecurityCritical]
    public virtual void AddClaim(Claim claim)
    {
      if (claim == null)
        throw new ArgumentNullException("claim");
      if (claim.Subject == this)
        this.m_instanceClaims.Add(claim);
      else
        this.m_instanceClaims.Add(claim.Clone(this));
    }

    /// <summary>添加声明列表到此声明标识。</summary>
    /// <param name="claims">要添加的声明。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="claims" /> 为 null。</exception>
    [SecurityCritical]
    public virtual void AddClaims(IEnumerable<Claim> claims)
    {
      if (claims == null)
        throw new ArgumentNullException("claims");
      foreach (Claim claim in claims)
      {
        if (claim != null)
          this.AddClaim(claim);
      }
    }

    /// <summary>尝试从声明标识中移除一个声明。</summary>
    /// <returns>如果已成功移除了声明，则为 true；否则为 false。</returns>
    /// <param name="claim">要移除的声明。</param>
    [SecurityCritical]
    public virtual bool TryRemoveClaim(Claim claim)
    {
      bool flag = false;
      for (int index = 0; index < this.m_instanceClaims.Count; ++index)
      {
        if (this.m_instanceClaims[index] == claim)
        {
          this.m_instanceClaims.RemoveAt(index);
          flag = true;
          break;
        }
      }
      return flag;
    }

    /// <summary>尝试从声明标识中移除一个声明。</summary>
    /// <param name="claim">要移除的声明。</param>
    /// <exception cref="T:System.InvalidOperationException">无法移除声明。</exception>
    [SecurityCritical]
    public virtual void RemoveClaim(Claim claim)
    {
      if (!this.TryRemoveClaim(claim))
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ClaimCannotBeRemoved", (object) claim));
    }

    [SecuritySafeCritical]
    private void SafeAddClaims(IEnumerable<Claim> claims)
    {
      foreach (Claim claim in claims)
      {
        if (claim.Subject == this)
          this.m_instanceClaims.Add(claim);
        else
          this.m_instanceClaims.Add(claim.Clone(this));
      }
    }

    [SecuritySafeCritical]
    private void SafeAddClaim(Claim claim)
    {
      if (claim.Subject == this)
        this.m_instanceClaims.Add(claim);
      else
        this.m_instanceClaims.Add(claim.Clone(this));
    }

    /// <summary>检索所有与指定谓词相匹配的声明。</summary>
    /// <returns>匹配声明。列表为只读。</returns>
    /// <param name="match">执行匹配逻辑的函数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="match" /> 为 null。</exception>
    public virtual IEnumerable<Claim> FindAll(Predicate<Claim> match)
    {
      if (match == null)
        throw new ArgumentNullException("match");
      List<Claim> claimList = new List<Claim>();
      foreach (Claim claim in this.Claims)
      {
        if (match(claim))
          claimList.Add(claim);
      }
      return (IEnumerable<Claim>) claimList.AsReadOnly();
    }

    /// <summary>检索所有有指定声明类型的声明。</summary>
    /// <returns>匹配声明。列表为只读。</returns>
    /// <param name="type">要对其匹配声明的声明类型。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="type" /> 为 null。</exception>
    public virtual IEnumerable<Claim> FindAll(string type)
    {
      if (type == null)
        throw new ArgumentNullException("type");
      List<Claim> claimList = new List<Claim>();
      foreach (Claim claim in this.Claims)
      {
        if (claim != null && string.Equals(claim.Type, type, StringComparison.OrdinalIgnoreCase))
          claimList.Add(claim);
      }
      return (IEnumerable<Claim>) claimList.AsReadOnly();
    }

    /// <summary>确定该声明标识是否拥有与指定条件相匹配的声明。</summary>
    /// <returns>如果存在匹配的声明，则为 true；否则，为 false。</returns>
    /// <param name="match">执行匹配逻辑的函数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="match" /> 为 null。</exception>
    public virtual bool HasClaim(Predicate<Claim> match)
    {
      if (match == null)
        throw new ArgumentNullException("match");
      foreach (Claim claim in this.Claims)
      {
        if (match(claim))
          return true;
      }
      return false;
    }

    /// <summary>确定该声明标识是否具备指定声明类型和值。</summary>
    /// <returns>如果找到匹配项，则为 true；否则为 false。</returns>
    /// <param name="type">要匹配的声明类型。</param>
    /// <param name="value">要匹配的声明的值。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="type" /> 为 null。- 或 -<paramref name="value" /> 为 null。</exception>
    public virtual bool HasClaim(string type, string value)
    {
      if (type == null)
        throw new ArgumentNullException("type");
      if (value == null)
        throw new ArgumentNullException("value");
      foreach (Claim claim in this.Claims)
      {
        if (claim != null && claim != null && (string.Equals(claim.Type, type, StringComparison.OrdinalIgnoreCase) && string.Equals(claim.Value, value, StringComparison.Ordinal)))
          return true;
      }
      return false;
    }

    /// <summary>检所由指定谓词匹配的第一个声明。</summary>
    /// <returns>如果未找到匹配，则为第一个匹配声明或 null。</returns>
    /// <param name="match">执行匹配逻辑的函数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="match" /> 为 null。</exception>
    public virtual Claim FindFirst(Predicate<Claim> match)
    {
      if (match == null)
        throw new ArgumentNullException("match");
      foreach (Claim claim in this.Claims)
      {
        if (match(claim))
          return claim;
      }
      return (Claim) null;
    }

    /// <summary>检索有指定声明类型的第一个声明。</summary>
    /// <returns>如果未找到匹配，则为第一个匹配声明或 null。</returns>
    /// <param name="type">要匹配的声明类型。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="type" /> 为 null。</exception>
    public virtual Claim FindFirst(string type)
    {
      if (type == null)
        throw new ArgumentNullException("type");
      foreach (Claim claim in this.Claims)
      {
        if (claim != null && string.Equals(claim.Type, type, StringComparison.OrdinalIgnoreCase))
          return claim;
      }
      return (Claim) null;
    }

    [OnSerializing]
    [SecurityCritical]
    private void OnSerializingMethod(StreamingContext context)
    {
      if (this is ISerializable)
        return;
      this.m_serializedClaims = this.SerializeClaims();
      this.m_serializedNameType = this.m_nameType;
      this.m_serializedRoleType = this.m_roleType;
    }

    [OnDeserialized]
    [SecurityCritical]
    private void OnDeserializedMethod(StreamingContext context)
    {
      if (this is ISerializable)
        return;
      if (!string.IsNullOrEmpty(this.m_serializedClaims))
      {
        this.DeserializeClaims(this.m_serializedClaims);
        this.m_serializedClaims = (string) null;
      }
      this.m_nameType = string.IsNullOrEmpty(this.m_serializedNameType) ? "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name" : this.m_serializedNameType;
      this.m_roleType = string.IsNullOrEmpty(this.m_serializedRoleType) ? "http://schemas.microsoft.com/ws/2008/06/identity/claims/role" : this.m_serializedRoleType;
    }

    [OnDeserializing]
    private void OnDeserializingMethod(StreamingContext context)
    {
      if (this is ISerializable)
        return;
      this.m_instanceClaims = new List<Claim>();
      this.m_externalClaims = new Collection<IEnumerable<Claim>>();
    }

    /// <summary>用序列化当前 <see cref="T:System.Security.Claims.ClaimsIdentity" /> 对象所需的数据填充 <see cref="T:System.Runtime.Serialization.SerializationInfo" />。</summary>
    /// <param name="info">要填充数据的对象。</param>
    /// <param name="context">此序列化的目标。可以为 null。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="info" /> 为 null。</exception>
    [SecurityCritical]
    [SecurityPermission(SecurityAction.Assert, SerializationFormatter = true)]
    protected virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException("info");
      BinaryFormatter binaryFormatter = new BinaryFormatter();
      info.AddValue("System.Security.ClaimsIdentity.version", (object) this.m_version);
      if (!string.IsNullOrEmpty(this.m_authenticationType))
        info.AddValue("System.Security.ClaimsIdentity.authenticationType", (object) this.m_authenticationType);
      info.AddValue("System.Security.ClaimsIdentity.nameClaimType", (object) this.m_nameType);
      info.AddValue("System.Security.ClaimsIdentity.roleClaimType", (object) this.m_roleType);
      if (!string.IsNullOrEmpty(this.m_label))
        info.AddValue("System.Security.ClaimsIdentity.label", (object) this.m_label);
      if (this.m_actor != null)
      {
        using (MemoryStream memoryStream = new MemoryStream())
        {
          binaryFormatter.Serialize((Stream) memoryStream, (object) this.m_actor, (Header[]) null, false);
          info.AddValue("System.Security.ClaimsIdentity.actor", (object) Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int) memoryStream.Length));
        }
      }
      info.AddValue("System.Security.ClaimsIdentity.claims", (object) this.SerializeClaims());
      if (this.m_bootstrapContext == null)
        return;
      using (MemoryStream memoryStream = new MemoryStream())
      {
        binaryFormatter.Serialize((Stream) memoryStream, this.m_bootstrapContext, (Header[]) null, false);
        info.AddValue("System.Security.ClaimsIdentity.bootstrapContext", (object) Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int) memoryStream.Length));
      }
    }

    [SecurityCritical]
    private void DeserializeClaims(string serializedClaims)
    {
      if (!string.IsNullOrEmpty(serializedClaims))
      {
        using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(serializedClaims)))
        {
          this.m_instanceClaims = (List<Claim>) new BinaryFormatter().Deserialize((Stream) memoryStream, (HeaderHandler) null, false);
          for (int index = 0; index < this.m_instanceClaims.Count; ++index)
            this.m_instanceClaims[index].Subject = this;
        }
      }
      if (this.m_instanceClaims != null)
        return;
      this.m_instanceClaims = new List<Claim>();
    }

    [SecurityCritical]
    private string SerializeClaims()
    {
      using (MemoryStream memoryStream = new MemoryStream())
      {
        new BinaryFormatter().Serialize((Stream) memoryStream, (object) this.m_instanceClaims, (Header[]) null, false);
        return Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int) memoryStream.Length);
      }
    }

    private bool IsCircular(ClaimsIdentity subject)
    {
      if (this == subject)
        return true;
      for (ClaimsIdentity claimsIdentity = subject; claimsIdentity.Actor != null; claimsIdentity = claimsIdentity.Actor)
      {
        if (this == claimsIdentity.Actor)
          return true;
      }
      return false;
    }

    private void Initialize(BinaryReader reader)
    {
      if (reader == null)
        throw new ArgumentNullException("reader");
      int num1 = reader.ReadInt32();
      int num2 = 1;
      if ((num1 & num2) == 1)
        this.m_authenticationType = reader.ReadString();
      int num3 = 2;
      if ((num1 & num3) == 2)
        this.m_bootstrapContext = (object) reader.ReadString();
      int num4 = 4;
      this.m_nameType = (num1 & num4) != 4 ? "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name" : reader.ReadString();
      int num5 = 8;
      this.m_roleType = (num1 & num5) != 8 ? "http://schemas.microsoft.com/ws/2008/06/identity/claims/role" : reader.ReadString();
      int num6 = 16;
      if ((num1 & num6) != 16)
        return;
      int num7 = reader.ReadInt32();
      for (int index = 0; index < num7; ++index)
        this.m_instanceClaims.Add(new Claim(reader, this));
    }

    protected virtual Claim CreateClaim(BinaryReader reader)
    {
      if (reader == null)
        throw new ArgumentNullException("reader");
      return new Claim(reader, this);
    }

    public virtual void WriteTo(BinaryWriter writer)
    {
      this.WriteTo(writer, (byte[]) null);
    }

    protected virtual void WriteTo(BinaryWriter writer, byte[] userData)
    {
      if (writer == null)
        throw new ArgumentNullException("writer");
      int num = 0;
      ClaimsIdentity.SerializationMask serializationMask = ClaimsIdentity.SerializationMask.None;
      if (this.m_authenticationType != null)
      {
        serializationMask |= ClaimsIdentity.SerializationMask.AuthenticationType;
        ++num;
      }
      if (this.m_bootstrapContext != null && this.m_bootstrapContext is string)
      {
        serializationMask |= ClaimsIdentity.SerializationMask.BootstrapConext;
        ++num;
      }
      if (!string.Equals(this.m_nameType, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", StringComparison.Ordinal))
      {
        serializationMask |= ClaimsIdentity.SerializationMask.NameClaimType;
        ++num;
      }
      if (!string.Equals(this.m_roleType, "http://schemas.microsoft.com/ws/2008/06/identity/claims/role", StringComparison.Ordinal))
      {
        serializationMask |= ClaimsIdentity.SerializationMask.RoleClaimType;
        ++num;
      }
      if (!string.IsNullOrWhiteSpace(this.m_label))
      {
        serializationMask |= ClaimsIdentity.SerializationMask.HasLabel;
        ++num;
      }
      if (this.m_instanceClaims.Count > 0)
      {
        serializationMask |= ClaimsIdentity.SerializationMask.HasClaims;
        ++num;
      }
      if (this.m_actor != null)
      {
        serializationMask |= ClaimsIdentity.SerializationMask.Actor;
        ++num;
      }
      if (userData != null && userData.Length != 0)
      {
        ++num;
        serializationMask |= ClaimsIdentity.SerializationMask.UserData;
      }
      writer.Write((int) serializationMask);
      writer.Write(num);
      if ((serializationMask & ClaimsIdentity.SerializationMask.AuthenticationType) == ClaimsIdentity.SerializationMask.AuthenticationType)
        writer.Write(this.m_authenticationType);
      if ((serializationMask & ClaimsIdentity.SerializationMask.BootstrapConext) == ClaimsIdentity.SerializationMask.BootstrapConext)
        writer.Write(this.m_bootstrapContext as string);
      if ((serializationMask & ClaimsIdentity.SerializationMask.NameClaimType) == ClaimsIdentity.SerializationMask.NameClaimType)
        writer.Write(this.m_nameType);
      if ((serializationMask & ClaimsIdentity.SerializationMask.RoleClaimType) == ClaimsIdentity.SerializationMask.RoleClaimType)
        writer.Write(this.m_roleType);
      if ((serializationMask & ClaimsIdentity.SerializationMask.HasLabel) == ClaimsIdentity.SerializationMask.HasLabel)
        writer.Write(this.m_label);
      if ((serializationMask & ClaimsIdentity.SerializationMask.HasClaims) == ClaimsIdentity.SerializationMask.HasClaims)
      {
        writer.Write(this.m_instanceClaims.Count);
        foreach (Claim mInstanceClaim in this.m_instanceClaims)
          mInstanceClaim.WriteTo(writer);
      }
      if ((serializationMask & ClaimsIdentity.SerializationMask.Actor) == ClaimsIdentity.SerializationMask.Actor)
        this.m_actor.WriteTo(writer);
      if ((serializationMask & ClaimsIdentity.SerializationMask.UserData) == ClaimsIdentity.SerializationMask.UserData)
      {
        writer.Write(userData.Length);
        writer.Write(userData);
      }
      writer.Flush();
    }

    [SecurityCritical]
    [SecurityPermission(SecurityAction.Assert, SerializationFormatter = true)]
    private void Deserialize(SerializationInfo info, StreamingContext context, bool useContext)
    {
      if (info == null)
        throw new ArgumentNullException("info");
      BinaryFormatter binaryFormatter = !useContext ? new BinaryFormatter() : new BinaryFormatter((ISurrogateSelector) null, context);
      SerializationInfoEnumerator enumerator = info.GetEnumerator();
      while (enumerator.MoveNext())
      {
        string name = enumerator.Name;
        // ISSUE: reference to a compiler-generated method
        uint stringHash = \u003CPrivateImplementationDetails\u003E.ComputeStringHash(name);
        if (stringHash <= 959168042U)
        {
          if (stringHash <= 623923795U)
          {
            if ((int) stringHash != 373632733)
            {
              if ((int) stringHash == 623923795 && name == "System.Security.ClaimsIdentity.roleClaimType")
                this.m_roleType = info.GetString("System.Security.ClaimsIdentity.roleClaimType");
            }
            else if (name == "System.Security.ClaimsIdentity.label")
              this.m_label = info.GetString("System.Security.ClaimsIdentity.label");
          }
          else if ((int) stringHash != 656336169)
          {
            if ((int) stringHash == 959168042 && name == "System.Security.ClaimsIdentity.nameClaimType")
              this.m_nameType = info.GetString("System.Security.ClaimsIdentity.nameClaimType");
          }
          else if (name == "System.Security.ClaimsIdentity.authenticationType")
            this.m_authenticationType = info.GetString("System.Security.ClaimsIdentity.authenticationType");
        }
        else if (stringHash <= 1476368026U)
        {
          if ((int) stringHash != 1453716852)
          {
            if ((int) stringHash == 1476368026 && name == "System.Security.ClaimsIdentity.actor")
            {
              using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(info.GetString("System.Security.ClaimsIdentity.actor"))))
                this.m_actor = (ClaimsIdentity) binaryFormatter.Deserialize((Stream) memoryStream, (HeaderHandler) null, false);
            }
          }
          else if (name == "System.Security.ClaimsIdentity.claims")
            this.DeserializeClaims(info.GetString("System.Security.ClaimsIdentity.claims"));
        }
        else if ((int) stringHash != -1814682505)
        {
          if ((int) stringHash == -635945184 && name == "System.Security.ClaimsIdentity.bootstrapContext")
          {
            using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(info.GetString("System.Security.ClaimsIdentity.bootstrapContext"))))
              this.m_bootstrapContext = binaryFormatter.Deserialize((Stream) memoryStream, (HeaderHandler) null, false);
          }
        }
        else if (name == "System.Security.ClaimsIdentity.version")
          info.GetString("System.Security.ClaimsIdentity.version");
      }
    }

    private enum SerializationMask
    {
      None = 0,
      AuthenticationType = 1,
      BootstrapConext = 2,
      NameClaimType = 4,
      RoleClaimType = 8,
      HasClaims = 16,
      HasLabel = 32,
      Actor = 64,
      UserData = 128,
    }
  }
}
