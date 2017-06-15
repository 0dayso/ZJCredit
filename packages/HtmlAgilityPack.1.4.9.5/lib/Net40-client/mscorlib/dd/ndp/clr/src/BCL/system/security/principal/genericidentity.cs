// Decompiled with JetBrains decompiler
// Type: System.Security.Principal.GenericIdentity
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Claims;

namespace System.Security.Principal
{
  /// <summary>表示一般用户。</summary>
  [ComVisible(true)]
  [Serializable]
  public class GenericIdentity : ClaimsIdentity
  {
    private string m_name;
    private string m_type;

    /// <summary>为用户获取此最常用标识表示的所有声明。</summary>
    /// <returns>表示该 <see cref="T:System.Security.Principal.GenericIdentity" /> 对象的请求的集合。</returns>
    public override IEnumerable<Claim> Claims
    {
      get
      {
        return base.Claims;
      }
    }

    /// <summary>获取用户的名称。</summary>
    /// <returns>用户名，代码当前即以该用户的名义运行。</returns>
    public override string Name
    {
      get
      {
        return this.m_name;
      }
    }

    /// <summary>获取用于标识用户的身份验证的类型。</summary>
    /// <returns>用于标识用户的身份验证的类型。</returns>
    public override string AuthenticationType
    {
      get
      {
        return this.m_type;
      }
    }

    /// <summary>获取一个值，该值指示是否验证了用户。</summary>
    /// <returns>如果已验证了用户，则为 true；否则为 false。</returns>
    public override bool IsAuthenticated
    {
      get
      {
        return !this.m_name.Equals("");
      }
    }

    /// <summary>初始化 <see cref="T:System.Security.Principal.GenericIdentity" /> 类的新实例，该类表示具有指定名称的用户。</summary>
    /// <param name="name">用户名，代码当前即以该用户的名义运行。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 参数为 null。</exception>
    [SecuritySafeCritical]
    public GenericIdentity(string name)
    {
      if (name == null)
        throw new ArgumentNullException("name");
      this.m_name = name;
      this.m_type = "";
      this.AddNameClaim();
    }

    /// <summary>初始化 <see cref="T:System.Security.Principal.GenericIdentity" /> 类的新实例，该类表示具有指定名称和身份验证类型的用户。</summary>
    /// <param name="name">用户名，代码当前即以该用户的名义运行。</param>
    /// <param name="type">用于标识用户的身份验证的类型。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 参数为 null。- 或 -<paramref name="type" /> 参数为 null。</exception>
    [SecuritySafeCritical]
    public GenericIdentity(string name, string type)
    {
      if (name == null)
        throw new ArgumentNullException("name");
      if (type == null)
        throw new ArgumentNullException("type");
      this.m_name = name;
      this.m_type = type;
      this.AddNameClaim();
    }

    private GenericIdentity()
    {
    }

    /// <summary>使用指定的 <see cref="T:System.Security.Principal.GenericIdentity" /> 对象初始化 <see cref="T:System.Security.Principal.GenericIdentity" /> 类的新实例。</summary>
    /// <param name="identity">根据其构造 <see cref="T:System.Security.Principal.GenericIdentity" /> 新实例的对象。</param>
    protected GenericIdentity(GenericIdentity identity)
      : base((ClaimsIdentity) identity)
    {
      this.m_name = identity.m_name;
      this.m_type = identity.m_type;
    }

    /// <summary>创建作为当前实例副本的新对象。</summary>
    /// <returns>当前实例的副本。</returns>
    public override ClaimsIdentity Clone()
    {
      return (ClaimsIdentity) new GenericIdentity(this);
    }

    [OnDeserialized]
    private void OnDeserializedMethod(StreamingContext context)
    {
      bool flag = false;
      using (IEnumerator<Claim> enumerator = base.Claims.GetEnumerator())
      {
        if (enumerator.MoveNext())
        {
          Claim current = enumerator.Current;
          flag = true;
        }
      }
      if (flag)
        return;
      this.AddNameClaim();
    }

    [SecuritySafeCritical]
    private void AddNameClaim()
    {
      if (this.m_name == null)
        return;
      this.AddClaim(new Claim(this.NameClaimType, this.m_name, "http://www.w3.org/2001/XMLSchema#string", "LOCAL AUTHORITY", "LOCAL AUTHORITY", (ClaimsIdentity) this));
    }
  }
}
