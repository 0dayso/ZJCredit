// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.HashAlgorithmName
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Security.Cryptography
{
  /// <summary>指定加密哈希算法的名称。</summary>
  public struct HashAlgorithmName : IEquatable<HashAlgorithmName>
  {
    private readonly string _name;

    /// <summary>获取表示“MD5”的哈希算法名称。</summary>
    /// <returns>表示“MD5”的哈希算法名称。</returns>
    public static HashAlgorithmName MD5
    {
      get
      {
        return new HashAlgorithmName("MD5");
      }
    }

    /// <summary>获取表示“SHA1”的哈希算法名称。</summary>
    /// <returns>表示“SHA1”的哈希算法名称。</returns>
    public static HashAlgorithmName SHA1
    {
      get
      {
        return new HashAlgorithmName("SHA1");
      }
    }

    /// <summary>获取表示“SHA256”的哈希算法名称。</summary>
    /// <returns>表示“SHA256”的哈希算法名称。</returns>
    public static HashAlgorithmName SHA256
    {
      get
      {
        return new HashAlgorithmName("SHA256");
      }
    }

    /// <summary>获取表示“SHA384”的哈希算法名称。</summary>
    /// <returns>表示“SHA384”的哈希算法名称。</returns>
    public static HashAlgorithmName SHA384
    {
      get
      {
        return new HashAlgorithmName("SHA384");
      }
    }

    /// <summary>获取表示“SHA512”的哈希算法名称。</summary>
    /// <returns>表示“SHA512”的哈希算法名称。</returns>
    public static HashAlgorithmName SHA512
    {
      get
      {
        return new HashAlgorithmName("SHA512");
      }
    }

    /// <summary>获取算法名称的基础字符串表示形式。</summary>
    /// <returns>为算法名称的字符串表示形式；如果无可用的哈希算法，则为 null 或 <see cref="F:System.String.Empty" />。</returns>
    public string Name
    {
      get
      {
        return this._name;
      }
    }

    /// <summary>初始化具有自定义名称的 <see cref="T:System.Security.Cryptography.HashAlgorithmName" /> 结构的新实例。</summary>
    /// <param name="name">自定义的哈希算法名称。</param>
    public HashAlgorithmName(string name)
    {
      this._name = name;
    }

    /// <summary>确定两个指定的 <see cref="T:System.Security.Cryptography.HashAlgorithmName" /> 对象是否相等。</summary>
    /// <returns>如果 <paramref name="left" /> 和 <paramref name="right" /> 具有相同的 <see cref="P:System.Security.Cryptography.HashAlgorithmName.Name" /> 值，则为 true；否则为 false。 </returns>
    /// <param name="left">要比较的第一个对象。</param>
    /// <param name="right">要比较的第二个对象。</param>
    public static bool operator ==(HashAlgorithmName left, HashAlgorithmName right)
    {
      return left.Equals(right);
    }

    /// <summary>确定两个指定的 <see cref="T:System.Security.Cryptography.HashAlgorithmName" /> 对象是否不相等。</summary>
    /// <returns>如果 <paramref name="left" /> 和 <paramref name="right" /> 均不具有同一 <see cref="P:System.Security.Cryptography.HashAlgorithmName.Name" /> 值，则为 true；否则为 false。 </returns>
    /// <param name="left">要比较的第一个对象。</param>
    /// <param name="right">要比较的第二个对象。</param>
    public static bool operator !=(HashAlgorithmName left, HashAlgorithmName right)
    {
      return !(left == right);
    }

    /// <summary>返回当前 <see cref="T:System.Security.Cryptography.HashAlgorithmName" /> 实例的字符串表示形式。</summary>
    /// <returns>当前 <see cref="T:System.Security.Cryptography.HashAlgorithmName" /> 实例的字符串表示形式。</returns>
    public override string ToString()
    {
      return this._name ?? string.Empty;
    }

    /// <summary>返回一个指示当前实例是否与指定对象相等的值。</summary>
    /// <returns>如果 <paramref name="obj" /> 是 <see cref="T:System.Security.Cryptography.HashAlgorithmName" /> 对象且它的 <see cref="P:System.Security.Cryptography.HashAlgorithmName.Name" /> 属性等于当前实例的属性，则为 true。比较是有序的且区分大小写。</returns>
    /// <param name="obj">要与当前实例进行比较的对象。</param>
    public override bool Equals(object obj)
    {
      if (obj is HashAlgorithmName)
        return this.Equals((HashAlgorithmName) obj);
      return false;
    }

    /// <summary>返回一个值，该值指示两个 <see cref="T:System.Security.Cryptography.HashAlgorithmName" /> 实例是否相等。</summary>
    /// <returns>如果 <paramref name="other" /> 的 <see cref="P:System.Security.Cryptography.HashAlgorithmName.Name" /> 属性等于当前实例的属性，则为 true。比较是有序的且区分大小写。</returns>
    /// <param name="other">要与当前实例进行比较的对象。</param>
    public bool Equals(HashAlgorithmName other)
    {
      return this._name == other._name;
    }

    /// <summary>返回当前实例的哈希代码。</summary>
    /// <returns>为当前实例的哈希代码；如果未向 <see cref="T:System.Security.Cryptography.HashAlgorithmName" /> 构造函数硅酮任何 <paramref name="name" /> 值，则为 0。</returns>
    public override int GetHashCode()
    {
      if (this._name != null)
        return this._name.GetHashCode();
      return 0;
    }
  }
}
