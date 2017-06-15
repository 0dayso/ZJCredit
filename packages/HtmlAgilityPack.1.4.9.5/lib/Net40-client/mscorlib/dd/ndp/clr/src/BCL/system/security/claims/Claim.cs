// Decompiled with JetBrains decompiler
// Type: System.Security.Claims.Claim
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;

namespace System.Security.Claims
{
  /// <summary>表示声明。</summary>
  [Serializable]
  public class Claim
  {
    [NonSerialized]
    private object m_propertyLock = new object();
    private string m_issuer;
    private string m_originalIssuer;
    private string m_type;
    private string m_value;
    private string m_valueType;
    [NonSerialized]
    private byte[] m_userSerializationData;
    private Dictionary<string, string> m_properties;
    [NonSerialized]
    private ClaimsIdentity m_subject;

    protected virtual byte[] CustomSerializationData
    {
      get
      {
        return this.m_userSerializationData;
      }
    }

    /// <summary>获取声明的颁发者。</summary>
    /// <returns>引用声明颁发者的名称。</returns>
    public string Issuer
    {
      get
      {
        return this.m_issuer;
      }
    }

    /// <summary>获取声明的最初颁发者。</summary>
    /// <returns>引用声明原始颁发者的名称。</returns>
    public string OriginalIssuer
    {
      get
      {
        return this.m_originalIssuer;
      }
    }

    /// <summary>获取包含与此声明关联的附加属性的字典。</summary>
    /// <returns>包含与声明关联的附加属性的字典。作为名称-值对表示的属性。</returns>
    public IDictionary<string, string> Properties
    {
      get
      {
        if (this.m_properties == null)
        {
          lock (this.m_propertyLock)
          {
            if (this.m_properties == null)
              this.m_properties = new Dictionary<string, string>();
          }
        }
        return (IDictionary<string, string>) this.m_properties;
      }
    }

    /// <summary>获取声明的主题。</summary>
    /// <returns>声明的主题。</returns>
    public ClaimsIdentity Subject
    {
      get
      {
        return this.m_subject;
      }
      internal set
      {
        this.m_subject = value;
      }
    }

    /// <summary>获取声称的声称类型。</summary>
    /// <returns>声明类型。</returns>
    public string Type
    {
      get
      {
        return this.m_type;
      }
    }

    /// <summary>获取声明的值。</summary>
    /// <returns>声明值。</returns>
    public string Value
    {
      get
      {
        return this.m_value;
      }
    }

    /// <summary>获取声明的值类型。</summary>
    /// <returns>声明值类型。</returns>
    public string ValueType
    {
      get
      {
        return this.m_valueType;
      }
    }

    public Claim(BinaryReader reader)
      : this(reader, (ClaimsIdentity) null)
    {
    }

    public Claim(BinaryReader reader, ClaimsIdentity subject)
    {
      if (reader == null)
        throw new ArgumentNullException("reader");
      this.Initialize(reader, subject);
    }

    /// <summary>初始化指定声称类型和值的 <see cref="T:System.Security.Claims.Claim" /> 类的新实例。</summary>
    /// <param name="type">声明类型。</param>
    /// <param name="value">声明值。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="type" /> 或 <paramref name="value" /> 为 null。</exception>
    public Claim(string type, string value)
      : this(type, value, "http://www.w3.org/2001/XMLSchema#string", "LOCAL AUTHORITY", "LOCAL AUTHORITY", (ClaimsIdentity) null)
    {
    }

    /// <summary>初始化指定声称类型、值类型和值的 <see cref="T:System.Security.Claims.Claim" /> 类的新实例。</summary>
    /// <param name="type">声明类型。</param>
    /// <param name="value">声明值。</param>
    /// <param name="valueType">声明值类型。当 null 为<see cref="F:System.Security.Claims.ClaimValueTypes.String" /> 时，才使用此参数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="type" /> 或 <paramref name="value" /> 为 null。</exception>
    public Claim(string type, string value, string valueType)
      : this(type, value, valueType, "LOCAL AUTHORITY", "LOCAL AUTHORITY", (ClaimsIdentity) null)
    {
    }

    /// <summary>初始化指定声称类型，值，值类型和颁发者的 <see cref="T:System.Security.Claims.Claim" /> 类的新实例。</summary>
    /// <param name="type">声明类型。</param>
    /// <param name="value">声明值。</param>
    /// <param name="valueType">声明值类型。当 null 为<see cref="F:System.Security.Claims.ClaimValueTypes.String" /> 时，才使用此参数。</param>
    /// <param name="issuer">声明颁发者。如果该参数为空或为 null， 则 <see cref="F:System.Security.Claims.ClaimsIdentity.DefaultIssuer" /> 将被使用。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="type" /> 或 <paramref name="value" /> 为 null。</exception>
    public Claim(string type, string value, string valueType, string issuer)
    {
      string type1 = type;
      string str1 = value;
      string valueType1 = valueType;
      string str2 = issuer;
      // ISSUE: variable of the null type
      __Null local = null;
      // ISSUE: explicit constructor call
      this.\u002Ector(type1, str1, valueType1, str2, str2, (ClaimsIdentity) local);
    }

    /// <summary>使用指定声明类型、值、值类型、颁发者和原始颁发者初始化 <see cref="T:System.Security.Claims.Claim" /> 类的新实例。</summary>
    /// <param name="type">声明类型。</param>
    /// <param name="value">声明值。</param>
    /// <param name="valueType">声明值类型。当 null 为<see cref="F:System.Security.Claims.ClaimValueTypes.String" /> 时，才使用此参数。</param>
    /// <param name="issuer">声明颁发者。如果该参数为空或为 null， 则 <see cref="F:System.Security.Claims.ClaimsIdentity.DefaultIssuer" /> 将被使用。</param>
    /// <param name="originalIssuer">声明的原始颁发者。如果该参数为空或为 null ，则将 <see cref="P:System.Security.Claims.Claim.OriginalIssuer" /> 设置为 <see cref="P:System.Security.Claims.Claim.Issuer" /> 的值。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="type" /> 或 <paramref name="value" /> 为 null。</exception>
    public Claim(string type, string value, string valueType, string issuer, string originalIssuer)
      : this(type, value, valueType, issuer, originalIssuer, (ClaimsIdentity) null)
    {
    }

    /// <summary>使用指定的声明类型、值、值类型、颁发者、原始颁发者和主题初始化 <see cref="T:System.Security.Claims.Claim" /> 类的新实例。</summary>
    /// <param name="type">声明类型。</param>
    /// <param name="value">声明值。</param>
    /// <param name="valueType">声明值类型。当 null 为<see cref="F:System.Security.Claims.ClaimValueTypes.String" /> 时，才使用此参数。</param>
    /// <param name="issuer">声明颁发者。如果该参数为空或为 null， 则 <see cref="F:System.Security.Claims.ClaimsIdentity.DefaultIssuer" /> 将被使用。</param>
    /// <param name="originalIssuer">声明的原始颁发者。如果该参数为空或为 null ，则将 <see cref="P:System.Security.Claims.Claim.OriginalIssuer" /> 设置为 <see cref="P:System.Security.Claims.Claim.Issuer" /> 的值。</param>
    /// <param name="subject">该声明说明的主题。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="type" /> 或 <paramref name="value" /> 为 null。</exception>
    public Claim(string type, string value, string valueType, string issuer, string originalIssuer, ClaimsIdentity subject)
      : this(type, value, valueType, issuer, originalIssuer, subject, (string) null, (string) null)
    {
    }

    internal Claim(string type, string value, string valueType, string issuer, string originalIssuer, ClaimsIdentity subject, string propertyKey, string propertyValue)
    {
      if (type == null)
        throw new ArgumentNullException("type");
      if (value == null)
        throw new ArgumentNullException("value");
      this.m_type = type;
      this.m_value = value;
      this.m_valueType = !string.IsNullOrEmpty(valueType) ? valueType : "http://www.w3.org/2001/XMLSchema#string";
      this.m_issuer = !string.IsNullOrEmpty(issuer) ? issuer : "LOCAL AUTHORITY";
      this.m_originalIssuer = !string.IsNullOrEmpty(originalIssuer) ? originalIssuer : this.m_issuer;
      this.m_subject = subject;
      if (propertyKey == null)
        return;
      this.Properties.Add(propertyKey, propertyValue);
    }

    protected Claim(Claim other)
    {
      Claim other1 = other;
      ClaimsIdentity subject = other1 == null ? (ClaimsIdentity) null : other.m_subject;
      // ISSUE: explicit constructor call
      this.\u002Ector(other1, subject);
    }

    protected Claim(Claim other, ClaimsIdentity subject)
    {
      if (other == null)
        throw new ArgumentNullException("other");
      this.m_issuer = other.m_issuer;
      this.m_originalIssuer = other.m_originalIssuer;
      this.m_subject = subject;
      this.m_type = other.m_type;
      this.m_value = other.m_value;
      this.m_valueType = other.m_valueType;
      if (other.m_properties != null)
      {
        this.m_properties = new Dictionary<string, string>();
        foreach (string key in other.m_properties.Keys)
          this.m_properties.Add(key, other.m_properties[key]);
      }
      if (other.m_userSerializationData == null)
        return;
      this.m_userSerializationData = other.m_userSerializationData.Clone() as byte[];
    }

    [OnDeserialized]
    private void OnDeserializedMethod(StreamingContext context)
    {
      this.m_propertyLock = new object();
    }

    /// <summary>返回从此对象中复制的新 <see cref="T:System.Security.Claims.Claim" /> 对象。新的声明不具有主题。</summary>
    /// <returns>新声明对象。</returns>
    public virtual Claim Clone()
    {
      return this.Clone((ClaimsIdentity) null);
    }

    /// <summary>返回从此对象中复制的新 <see cref="T:System.Security.Claims.Claim" /> 对象。新声明的主题设置为指定的 ClaimsIdentity。</summary>
    /// <returns>新声明对象。</returns>
    /// <param name="identity">新声明的期望主题。</param>
    public virtual Claim Clone(ClaimsIdentity identity)
    {
      return new Claim(this, identity);
    }

    private void Initialize(BinaryReader reader, ClaimsIdentity subject)
    {
      if (reader == null)
        throw new ArgumentNullException("reader");
      this.m_subject = subject;
      Claim.SerializationMask serializationMask = (Claim.SerializationMask) reader.ReadInt32();
      int num1 = 1;
      int num2 = reader.ReadInt32();
      this.m_value = reader.ReadString();
      if ((serializationMask & Claim.SerializationMask.NameClaimType) == Claim.SerializationMask.NameClaimType)
        this.m_type = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
      else if ((serializationMask & Claim.SerializationMask.RoleClaimType) == Claim.SerializationMask.RoleClaimType)
      {
        this.m_type = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
      }
      else
      {
        this.m_type = reader.ReadString();
        ++num1;
      }
      if ((serializationMask & Claim.SerializationMask.StringType) == Claim.SerializationMask.StringType)
      {
        this.m_valueType = reader.ReadString();
        ++num1;
      }
      else
        this.m_valueType = "http://www.w3.org/2001/XMLSchema#string";
      if ((serializationMask & Claim.SerializationMask.Issuer) == Claim.SerializationMask.Issuer)
      {
        this.m_issuer = reader.ReadString();
        ++num1;
      }
      else
        this.m_issuer = "LOCAL AUTHORITY";
      if ((serializationMask & Claim.SerializationMask.OriginalIssuerEqualsIssuer) == Claim.SerializationMask.OriginalIssuerEqualsIssuer)
        this.m_originalIssuer = this.m_issuer;
      else if ((serializationMask & Claim.SerializationMask.OriginalIssuer) == Claim.SerializationMask.OriginalIssuer)
      {
        this.m_originalIssuer = reader.ReadString();
        ++num1;
      }
      else
        this.m_originalIssuer = "LOCAL AUTHORITY";
      if ((serializationMask & Claim.SerializationMask.HasProperties) == Claim.SerializationMask.HasProperties)
      {
        int num3 = reader.ReadInt32();
        for (int index = 0; index < num3; ++index)
          this.Properties.Add(reader.ReadString(), reader.ReadString());
      }
      if ((serializationMask & Claim.SerializationMask.UserData) == Claim.SerializationMask.UserData)
      {
        int count = reader.ReadInt32();
        this.m_userSerializationData = reader.ReadBytes(count);
        ++num1;
      }
      for (int index = num1; index < num2; ++index)
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
      int num = 1;
      Claim.SerializationMask serializationMask = Claim.SerializationMask.None;
      if (string.Equals(this.m_type, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"))
        serializationMask |= Claim.SerializationMask.NameClaimType;
      else if (string.Equals(this.m_type, "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"))
        serializationMask |= Claim.SerializationMask.RoleClaimType;
      else
        ++num;
      if (!string.Equals(this.m_valueType, "http://www.w3.org/2001/XMLSchema#string", StringComparison.Ordinal))
      {
        ++num;
        serializationMask |= Claim.SerializationMask.StringType;
      }
      if (!string.Equals(this.m_issuer, "LOCAL AUTHORITY", StringComparison.Ordinal))
      {
        ++num;
        serializationMask |= Claim.SerializationMask.Issuer;
      }
      if (string.Equals(this.m_originalIssuer, this.m_issuer, StringComparison.Ordinal))
        serializationMask |= Claim.SerializationMask.OriginalIssuerEqualsIssuer;
      else if (!string.Equals(this.m_originalIssuer, "LOCAL AUTHORITY", StringComparison.Ordinal))
      {
        ++num;
        serializationMask |= Claim.SerializationMask.OriginalIssuer;
      }
      if (this.Properties.Count > 0)
      {
        ++num;
        serializationMask |= Claim.SerializationMask.HasProperties;
      }
      if (userData != null && userData.Length != 0)
      {
        ++num;
        serializationMask |= Claim.SerializationMask.UserData;
      }
      writer.Write((int) serializationMask);
      writer.Write(num);
      writer.Write(this.m_value);
      if ((serializationMask & Claim.SerializationMask.NameClaimType) != Claim.SerializationMask.NameClaimType && (serializationMask & Claim.SerializationMask.RoleClaimType) != Claim.SerializationMask.RoleClaimType)
        writer.Write(this.m_type);
      if ((serializationMask & Claim.SerializationMask.StringType) == Claim.SerializationMask.StringType)
        writer.Write(this.m_valueType);
      if ((serializationMask & Claim.SerializationMask.Issuer) == Claim.SerializationMask.Issuer)
        writer.Write(this.m_issuer);
      if ((serializationMask & Claim.SerializationMask.OriginalIssuer) == Claim.SerializationMask.OriginalIssuer)
        writer.Write(this.m_originalIssuer);
      if ((serializationMask & Claim.SerializationMask.HasProperties) == Claim.SerializationMask.HasProperties)
      {
        writer.Write(this.Properties.Count);
        foreach (string key in (IEnumerable<string>) this.Properties.Keys)
        {
          writer.Write(key);
          writer.Write(this.Properties[key]);
        }
      }
      if ((serializationMask & Claim.SerializationMask.UserData) == Claim.SerializationMask.UserData)
      {
        writer.Write(userData.Length);
        writer.Write(userData);
      }
      writer.Flush();
    }

    /// <summary>返回此 <see cref="T:System.Security.Claims.Claim" /> 对象的字符串表示形式。</summary>
    /// <returns>此 <see cref="T:System.Security.Claims.Claim" /> 对象的字符串表示形式。</returns>
    public override string ToString()
    {
      return string.Format((IFormatProvider) CultureInfo.InvariantCulture, "{0}: {1}", (object) this.m_type, (object) this.m_value);
    }

    private enum SerializationMask
    {
      None = 0,
      NameClaimType = 1,
      RoleClaimType = 2,
      StringType = 4,
      Issuer = 8,
      OriginalIssuerEqualsIssuer = 16,
      OriginalIssuer = 32,
      HasProperties = 64,
      UserData = 128,
    }
  }
}
