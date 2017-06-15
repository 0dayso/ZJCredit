// Decompiled with JetBrains decompiler
// Type: System.Security.Claims.ClaimsPrincipal
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;
using System.Security.Principal;
using System.Threading;

namespace System.Security.Claims
{
  /// <summary>支持多个基于声明的标识的 <see cref="T:System.Security.Principal.IPrincipal" /> 实现。</summary>
  [ComVisible(true)]
  [Serializable]
  public class ClaimsPrincipal : IPrincipal
  {
    [NonSerialized]
    private static Func<IEnumerable<ClaimsIdentity>, ClaimsIdentity> s_identitySelector = new Func<IEnumerable<ClaimsIdentity>, ClaimsIdentity>(ClaimsPrincipal.SelectPrimaryIdentity);
    [NonSerialized]
    private static Func<ClaimsPrincipal> s_principalSelector = ClaimsPrincipal.ClaimsPrincipalSelector;
    [OptionalField(VersionAdded = 2)]
    private string m_version = "1.0";
    [NonSerialized]
    private List<ClaimsIdentity> m_identities = new List<ClaimsIdentity>();
    [NonSerialized]
    private byte[] m_userSerializationData;
    [NonSerialized]
    private const string PreFix = "System.Security.ClaimsPrincipal.";
    [NonSerialized]
    private const string IdentitiesKey = "System.Security.ClaimsPrincipal.Identities";
    [NonSerialized]
    private const string VersionKey = "System.Security.ClaimsPrincipal.Version";
    [OptionalField(VersionAdded = 2)]
    private string m_serializedClaimsIdentities;

    /// <summary>获取并设置用于选择由 <see cref="P:System.Security.Claims.ClaimsPrincipal.Identity" /> 属性返回的声明标识符的委托。</summary>
    /// <returns>委托。默认值为 null。</returns>
    public static Func<IEnumerable<ClaimsIdentity>, ClaimsIdentity> PrimaryIdentitySelector
    {
      get
      {
        return ClaimsPrincipal.s_identitySelector;
      }
      [SecurityCritical] set
      {
        ClaimsPrincipal.s_identitySelector = value;
      }
    }

    /// <summary>获取并设置用于选择由 <see cref="P:System.Security.Claims.ClaimsPrincipal.Current" /> 属性返回的声明主体的委托。</summary>
    /// <returns>委托。默认值为 null。</returns>
    public static Func<ClaimsPrincipal> ClaimsPrincipalSelector
    {
      get
      {
        return ClaimsPrincipal.s_principalSelector;
      }
      [SecurityCritical] set
      {
        ClaimsPrincipal.s_principalSelector = value;
      }
    }

    protected virtual byte[] CustomSerializationData
    {
      get
      {
        return this.m_userSerializationData;
      }
    }

    /// <summary>获取包含所有声明的集合，这些声明都来自于此声明主体关联的声明标识符。</summary>
    /// <returns>与此主体关联的声明。</returns>
    public virtual IEnumerable<Claim> Claims
    {
      get
      {
        foreach (ClaimsIdentity identity in this.Identities)
        {
          foreach (Claim claim in identity.Claims)
            yield return claim;
        }
      }
    }

    /// <summary>获取当前声明主体。</summary>
    /// <returns>当前声明主体。</returns>
    public static ClaimsPrincipal Current
    {
      get
      {
        if (ClaimsPrincipal.s_principalSelector != null)
          return ClaimsPrincipal.s_principalSelector();
        return ClaimsPrincipal.SelectClaimsPrincipal();
      }
    }

    /// <summary>获取包含与此声明主体关联的所有声明标识符的集合。</summary>
    /// <returns>声明标识的集合。</returns>
    public virtual IEnumerable<ClaimsIdentity> Identities
    {
      get
      {
        return (IEnumerable<ClaimsIdentity>) this.m_identities.AsReadOnly();
      }
    }

    /// <summary>获取与此声明主体相关联的主要声明标识。</summary>
    /// <returns>与此声明主体相关联的主要声明标识。</returns>
    public virtual IIdentity Identity
    {
      get
      {
        if (ClaimsPrincipal.s_identitySelector != null)
          return (IIdentity) ClaimsPrincipal.s_identitySelector((IEnumerable<ClaimsIdentity>) this.m_identities);
        return (IIdentity) ClaimsPrincipal.SelectPrimaryIdentity((IEnumerable<ClaimsIdentity>) this.m_identities);
      }
    }

    /// <summary>初始化 <see cref="T:System.Security.Claims.ClaimsPrincipal" /> 类的新实例。</summary>
    public ClaimsPrincipal()
    {
    }

    /// <summary>使用指定的声明标识初始化 <see cref="T:System.Security.Claims.ClaimsPrincipal" /> 类的新实例。</summary>
    /// <param name="identities">初始化新的声明原则的标示符。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="identities" /> 为 null。</exception>
    public ClaimsPrincipal(IEnumerable<ClaimsIdentity> identities)
    {
      if (identities == null)
        throw new ArgumentNullException("identities");
      this.m_identities.AddRange(identities);
    }

    /// <summary>从指定的标识初始化 <see cref="T:System.Security.Claims.ClaimsPrincipal" /> 类的新实例。</summary>
    /// <param name="identity">初始化新的声明原则的标识。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="identity" /> 为 null。</exception>
    public ClaimsPrincipal(IIdentity identity)
    {
      if (identity == null)
        throw new ArgumentNullException("identity");
      ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
      if (claimsIdentity != null)
        this.m_identities.Add(claimsIdentity);
      else
        this.m_identities.Add(new ClaimsIdentity(identity));
    }

    /// <summary>从指定的主体初始化 <see cref="T:System.Security.Claims.ClaimsPrincipal" /> 类的新实例。</summary>
    /// <param name="principal">从其初始化新的声明主体的主体。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="principal" /> 为 null。</exception>
    public ClaimsPrincipal(IPrincipal principal)
    {
      if (principal == null)
        throw new ArgumentNullException("principal");
      ClaimsPrincipal claimsPrincipal = principal as ClaimsPrincipal;
      if (claimsPrincipal == null)
      {
        this.m_identities.Add(new ClaimsIdentity(principal.Identity));
      }
      else
      {
        if (claimsPrincipal.Identities == null)
          return;
        this.m_identities.AddRange(claimsPrincipal.Identities);
      }
    }

    public ClaimsPrincipal(BinaryReader reader)
    {
      if (reader == null)
        throw new ArgumentNullException("reader");
      this.Initialize(reader);
    }

    /// <summary>初始化从使用 <see cref="T:System.Runtime.Serialization.ISerializable" /> 创建的序列化流创建中的 <see cref="T:System.Security.Claims.ClaimsPrincipal" /> 类的新实例</summary>
    /// <param name="info">序列化的数据。</param>
    /// <param name="context">序列化的上下文。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="info" /> 为 null。</exception>
    [SecurityCritical]
    protected ClaimsPrincipal(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException("info");
      this.Deserialize(info, context);
    }

    private static ClaimsIdentity SelectPrimaryIdentity(IEnumerable<ClaimsIdentity> identities)
    {
      if (identities == null)
        throw new ArgumentNullException("identities");
      ClaimsIdentity claimsIdentity = (ClaimsIdentity) null;
      foreach (ClaimsIdentity identity in identities)
      {
        if (identity is WindowsIdentity)
        {
          claimsIdentity = identity;
          break;
        }
        if (claimsIdentity == null)
          claimsIdentity = identity;
      }
      return claimsIdentity;
    }

    private static ClaimsPrincipal SelectClaimsPrincipal()
    {
      return Thread.CurrentPrincipal as ClaimsPrincipal ?? new ClaimsPrincipal(Thread.CurrentPrincipal);
    }

    public virtual ClaimsPrincipal Clone()
    {
      return new ClaimsPrincipal((IPrincipal) this);
    }

    protected virtual ClaimsIdentity CreateClaimsIdentity(BinaryReader reader)
    {
      if (reader == null)
        throw new ArgumentNullException("reader");
      return new ClaimsIdentity(reader);
    }

    [OnSerializing]
    [SecurityCritical]
    private void OnSerializingMethod(StreamingContext context)
    {
      if (this is ISerializable)
        return;
      this.m_serializedClaimsIdentities = this.SerializeIdentities();
    }

    [OnDeserialized]
    [SecurityCritical]
    private void OnDeserializedMethod(StreamingContext context)
    {
      if (this is ISerializable)
        return;
      this.DeserializeIdentities(this.m_serializedClaimsIdentities);
      this.m_serializedClaimsIdentities = (string) null;
    }

    /// <summary>用序列化当前 <see cref="T:System.Security.Claims.ClaimsPrincipal" /> 对象所需的数据填充 <see cref="T:System.Runtime.Serialization.SerializationInfo" />。</summary>
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
      info.AddValue("System.Security.ClaimsPrincipal.Identities", (object) this.SerializeIdentities());
      info.AddValue("System.Security.ClaimsPrincipal.Version", (object) this.m_version);
    }

    [SecurityCritical]
    [SecurityPermission(SecurityAction.Assert, SerializationFormatter = true)]
    private void Deserialize(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException("info");
      SerializationInfoEnumerator enumerator = info.GetEnumerator();
      while (enumerator.MoveNext())
      {
        string name = enumerator.Name;
        if (!(name == "System.Security.ClaimsPrincipal.Identities"))
        {
          if (name == "System.Security.ClaimsPrincipal.Version")
            this.m_version = info.GetString("System.Security.ClaimsPrincipal.Version");
        }
        else
          this.DeserializeIdentities(info.GetString("System.Security.ClaimsPrincipal.Identities"));
      }
    }

    [SecurityCritical]
    private void DeserializeIdentities(string identities)
    {
      this.m_identities = new List<ClaimsIdentity>();
      if (string.IsNullOrEmpty(identities))
        return;
      BinaryFormatter binaryFormatter = new BinaryFormatter();
      using (MemoryStream memoryStream1 = new MemoryStream(Convert.FromBase64String(identities)))
      {
        List<string> stringList = (List<string>) binaryFormatter.Deserialize((Stream) memoryStream1, (HeaderHandler) null, false);
        int index = 0;
        while (index < stringList.Count)
        {
          ClaimsIdentity claimsIdentity = (ClaimsIdentity) null;
          using (MemoryStream memoryStream2 = new MemoryStream(Convert.FromBase64String(stringList[index + 1])))
            claimsIdentity = (ClaimsIdentity) binaryFormatter.Deserialize((Stream) memoryStream2, (HeaderHandler) null, false);
          if (!string.IsNullOrEmpty(stringList[index]))
          {
            long result;
            if (!long.TryParse(stringList[index], NumberStyles.Integer, (IFormatProvider) NumberFormatInfo.InvariantInfo, out result))
              throw new SerializationException(Environment.GetResourceString("Serialization_CorruptedStream"));
            claimsIdentity = (ClaimsIdentity) new WindowsIdentity(claimsIdentity, new IntPtr(result));
          }
          this.m_identities.Add(claimsIdentity);
          index += 2;
        }
      }
    }

    [SecurityCritical]
    private string SerializeIdentities()
    {
      List<string> stringList = new List<string>();
      BinaryFormatter binaryFormatter = new BinaryFormatter();
      foreach (ClaimsIdentity mIdentity in this.m_identities)
      {
        if (mIdentity.GetType() == typeof (WindowsIdentity))
        {
          WindowsIdentity windowsIdentity = mIdentity as WindowsIdentity;
          stringList.Add(windowsIdentity.GetTokenInternal().ToInt64().ToString((IFormatProvider) NumberFormatInfo.InvariantInfo));
          using (MemoryStream memoryStream = new MemoryStream())
          {
            binaryFormatter.Serialize((Stream) memoryStream, (object) windowsIdentity.CloneAsBase(), (Header[]) null, false);
            stringList.Add(Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int) memoryStream.Length));
          }
        }
        else
        {
          using (MemoryStream memoryStream = new MemoryStream())
          {
            stringList.Add("");
            binaryFormatter.Serialize((Stream) memoryStream, (object) mIdentity, (Header[]) null, false);
            stringList.Add(Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int) memoryStream.Length));
          }
        }
      }
      using (MemoryStream memoryStream = new MemoryStream())
      {
        binaryFormatter.Serialize((Stream) memoryStream, (object) stringList, (Header[]) null, false);
        return Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int) memoryStream.Length);
      }
    }

    /// <summary>添加指定的声明标识到此声明主体。</summary>
    /// <param name="identity">要添加的声明标识。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="identity" /> 为 null。</exception>
    [SecurityCritical]
    public virtual void AddIdentity(ClaimsIdentity identity)
    {
      if (identity == null)
        throw new ArgumentNullException("identity");
      this.m_identities.Add(identity);
    }

    /// <summary>添加指定的声明标识到此声明主体。</summary>
    /// <param name="identities">要添加的声明标识。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="identities" /> 为 null。</exception>
    [SecurityCritical]
    public virtual void AddIdentities(IEnumerable<ClaimsIdentity> identities)
    {
      if (identities == null)
        throw new ArgumentNullException("identities");
      this.m_identities.AddRange(identities);
    }

    /// <summary>检索所有与指定谓词相匹配的声明。</summary>
    /// <returns>匹配声明。</returns>
    /// <param name="match">执行匹配逻辑的函数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="match" /> 为 null。</exception>
    public virtual IEnumerable<Claim> FindAll(Predicate<Claim> match)
    {
      if (match == null)
        throw new ArgumentNullException("match");
      List<Claim> claimList = new List<Claim>();
      foreach (ClaimsIdentity identity in this.Identities)
      {
        if (identity != null)
        {
          foreach (Claim claim in identity.FindAll(match))
            claimList.Add(claim);
        }
      }
      return (IEnumerable<Claim>) claimList.AsReadOnly();
    }

    /// <summary>检索所有声明或有指定声明类型的声明。</summary>
    /// <returns>匹配声明。</returns>
    /// <param name="type">要对其匹配声明的声明类型。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="type" /> 为 null。</exception>
    public virtual IEnumerable<Claim> FindAll(string type)
    {
      if (type == null)
        throw new ArgumentNullException("type");
      List<Claim> claimList = new List<Claim>();
      foreach (ClaimsIdentity identity in this.Identities)
      {
        if (identity != null)
        {
          foreach (Claim claim in identity.FindAll(type))
            claimList.Add(claim);
        }
      }
      return (IEnumerable<Claim>) claimList.AsReadOnly();
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
      Claim claim = (Claim) null;
      foreach (ClaimsIdentity identity in this.Identities)
      {
        if (identity != null)
        {
          claim = identity.FindFirst(match);
          if (claim != null)
            return claim;
        }
      }
      return claim;
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
      Claim claim = (Claim) null;
      for (int index = 0; index < this.m_identities.Count; ++index)
      {
        if (this.m_identities[index] != null)
        {
          claim = this.m_identities[index].FindFirst(type);
          if (claim != null)
            return claim;
        }
      }
      return claim;
    }

    /// <summary>确定与此声明主体关联的声明标识是否包含与指定谓语匹配的声明。</summary>
    /// <returns>如果存在匹配的声明，则为 true；否则，为 false。</returns>
    /// <param name="match">执行匹配逻辑的函数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="match" /> 为 null。</exception>
    public virtual bool HasClaim(Predicate<Claim> match)
    {
      if (match == null)
        throw new ArgumentNullException("match");
      for (int index = 0; index < this.m_identities.Count; ++index)
      {
        if (this.m_identities[index] != null && this.m_identities[index].HasClaim(match))
          return true;
      }
      return false;
    }

    /// <summary>确定与此声明主体关联的声明标识包含与指定谓语匹配的声明。</summary>
    /// <returns>如果存在匹配的声明，则为 true；否则，为 false。</returns>
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
      for (int index = 0; index < this.m_identities.Count; ++index)
      {
        if (this.m_identities[index] != null && this.m_identities[index].HasClaim(type, value))
          return true;
      }
      return false;
    }

    /// <summary>返回指定的由声明主体表示的实体(用户)是否在指定的角色的值。</summary>
    /// <returns>如果声明主体属于指定角色，则为 true；否则为 false。</returns>
    /// <param name="role">要检查的角色。</param>
    public virtual bool IsInRole(string role)
    {
      for (int index = 0; index < this.m_identities.Count; ++index)
      {
        if (this.m_identities[index] != null && this.m_identities[index].HasClaim(this.m_identities[index].RoleClaimType, role))
          return true;
      }
      return false;
    }

    private void Initialize(BinaryReader reader)
    {
      if (reader == null)
        throw new ArgumentNullException("reader");
      ClaimsPrincipal.SerializationMask serializationMask = (ClaimsPrincipal.SerializationMask) reader.ReadInt32();
      int num1 = reader.ReadInt32();
      int num2 = 0;
      if ((serializationMask & ClaimsPrincipal.SerializationMask.HasIdentities) == ClaimsPrincipal.SerializationMask.HasIdentities)
      {
        ++num2;
        int num3 = reader.ReadInt32();
        for (int index = 0; index < num3; ++index)
          this.m_identities.Add(this.CreateClaimsIdentity(reader));
      }
      if ((serializationMask & ClaimsPrincipal.SerializationMask.UserData) == ClaimsPrincipal.SerializationMask.UserData)
      {
        int count = reader.ReadInt32();
        this.m_userSerializationData = reader.ReadBytes(count);
        ++num2;
      }
      for (int index = num2; index < num1; ++index)
        reader.ReadString();
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
      ClaimsPrincipal.SerializationMask serializationMask = ClaimsPrincipal.SerializationMask.None;
      if (this.m_identities.Count > 0)
      {
        serializationMask |= ClaimsPrincipal.SerializationMask.HasIdentities;
        ++num;
      }
      if (userData != null && userData.Length != 0)
      {
        ++num;
        serializationMask |= ClaimsPrincipal.SerializationMask.UserData;
      }
      writer.Write((int) serializationMask);
      writer.Write(num);
      if ((serializationMask & ClaimsPrincipal.SerializationMask.HasIdentities) == ClaimsPrincipal.SerializationMask.HasIdentities)
      {
        writer.Write(this.m_identities.Count);
        foreach (ClaimsIdentity mIdentity in this.m_identities)
          mIdentity.WriteTo(writer);
      }
      if ((serializationMask & ClaimsPrincipal.SerializationMask.UserData) == ClaimsPrincipal.SerializationMask.UserData)
      {
        writer.Write(userData.Length);
        writer.Write(userData);
      }
      writer.Flush();
    }

    private enum SerializationMask
    {
      None,
      HasIdentities,
      UserData,
    }
  }
}
