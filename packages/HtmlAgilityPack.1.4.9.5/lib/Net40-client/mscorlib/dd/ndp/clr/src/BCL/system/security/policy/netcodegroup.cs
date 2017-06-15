// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.CodeConnectAccess
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
  /// <summary>指定授予代码的网络资源访问权限。</summary>
  [ComVisible(true)]
  [Serializable]
  public class CodeConnectAccess
  {
    /// <summary>包含用于表示默认端口的值。</summary>
    public static readonly int DefaultPort = -3;
    /// <summary>包含用于表示代码原始 URI 中的端口值的值。</summary>
    public static readonly int OriginPort = -4;
    /// <summary>包含用于表示代码原始 URL 中的方案的值。</summary>
    public static readonly string OriginScheme = "$origin";
    /// <summary>包含表示方案通配符的字符串值。</summary>
    public static readonly string AnyScheme = "*";
    private string _LowerCaseScheme;
    private string _LowerCasePort;
    private int _IntPort;
    private const string DefaultStr = "$default";
    private const string OriginStr = "$origin";
    internal const int NoPort = -1;
    internal const int AnyPort = -2;

    /// <summary>获取当前实例表示的 URI 方案。</summary>
    /// <returns>标识 URI 方案的 <see cref="T:System.String" />，已转换为小写。</returns>
    public string Scheme
    {
      get
      {
        return this._LowerCaseScheme;
      }
    }

    /// <summary>获取当前实例表示的端口。</summary>
    /// <returns>一个 <see cref="T:System.Int32" /> 值，标识与 <see cref="P:System.Security.Policy.CodeConnectAccess.Scheme" /> 属性一起使用的计算机端口。</returns>
    public int Port
    {
      get
      {
        return this._IntPort;
      }
    }

    internal bool IsOriginScheme
    {
      get
      {
        return this._LowerCaseScheme == CodeConnectAccess.OriginScheme;
      }
    }

    internal bool IsAnyScheme
    {
      get
      {
        return this._LowerCaseScheme == CodeConnectAccess.AnyScheme;
      }
    }

    internal bool IsDefaultPort
    {
      get
      {
        return this.Port == CodeConnectAccess.DefaultPort;
      }
    }

    internal bool IsOriginPort
    {
      get
      {
        return this.Port == CodeConnectAccess.OriginPort;
      }
    }

    internal string StrPort
    {
      get
      {
        return this._LowerCasePort;
      }
    }

    /// <summary>初始化 <see cref="T:System.Security.Policy.CodeConnectAccess" /> 类的新实例。</summary>
    /// <param name="allowScheme">当前实例表示的 URI 方案。</param>
    /// <param name="allowPort">当前实例表示的端口。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="allowScheme" /> 为 null。- 或 -<paramref name="allowScheme" /> 为空字符串 ("")。- 或 -<paramref name="allowScheme" /> 包含方案中不允许的字符。- 或 -<paramref name="allowPort" /> 小于 0。- 或 -<paramref name="allowPort" /> 大于 65,535。</exception>
    public CodeConnectAccess(string allowScheme, int allowPort)
    {
      if (!CodeConnectAccess.IsValidScheme(allowScheme))
        throw new ArgumentOutOfRangeException("allowScheme");
      this.SetCodeConnectAccess(allowScheme.ToLower(CultureInfo.InvariantCulture), allowPort);
    }

    private CodeConnectAccess()
    {
    }

    internal CodeConnectAccess(string allowScheme, string allowPort)
    {
      if (allowScheme == null || allowScheme.Length == 0)
        throw new ArgumentNullException("allowScheme");
      if (allowPort == null || allowPort.Length == 0)
        throw new ArgumentNullException("allowPort");
      this._LowerCaseScheme = allowScheme.ToLower(CultureInfo.InvariantCulture);
      if (this._LowerCaseScheme == CodeConnectAccess.OriginScheme)
        this._LowerCaseScheme = CodeConnectAccess.OriginScheme;
      else if (this._LowerCaseScheme == CodeConnectAccess.AnyScheme)
        this._LowerCaseScheme = CodeConnectAccess.AnyScheme;
      else if (!CodeConnectAccess.IsValidScheme(this._LowerCaseScheme))
        throw new ArgumentOutOfRangeException("allowScheme");
      this._LowerCasePort = allowPort.ToLower(CultureInfo.InvariantCulture);
      if (this._LowerCasePort == "$default")
        this._IntPort = CodeConnectAccess.DefaultPort;
      else if (this._LowerCasePort == "$origin")
      {
        this._IntPort = CodeConnectAccess.OriginPort;
      }
      else
      {
        this._IntPort = int.Parse(allowPort, (IFormatProvider) CultureInfo.InvariantCulture);
        if (this._IntPort < 0 || this._IntPort > (int) ushort.MaxValue)
          throw new ArgumentOutOfRangeException("allowPort");
        this._LowerCasePort = this._IntPort.ToString((IFormatProvider) CultureInfo.InvariantCulture);
      }
    }

    /// <summary>返回一个 <see cref="T:System.Security.Policy.CodeConnectAccess" /> 实例，该实例表示使用代码的原始方案对指定端口的访问权限。</summary>
    /// <returns>指定端口的 <see cref="T:System.Security.Policy.CodeConnectAccess" /> 实例。</returns>
    /// <param name="allowPort">返回的实例表示的端口。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="allowPort" /> 小于 0。- 或 -<paramref name="allowPort" /> 大于 65,535。</exception>
    public static CodeConnectAccess CreateOriginSchemeAccess(int allowPort)
    {
      CodeConnectAccess codeConnectAccess = new CodeConnectAccess();
      string lowerCaseScheme = CodeConnectAccess.OriginScheme;
      int allowPort1 = allowPort;
      codeConnectAccess.SetCodeConnectAccess(lowerCaseScheme, allowPort1);
      return codeConnectAccess;
    }

    /// <summary>返回一个 <see cref="T:System.Security.Policy.CodeConnectAccess" /> 实例，该实例表示使用任何方案对指定端口的访问权限。</summary>
    /// <returns>指定端口的 <see cref="T:System.Security.Policy.CodeConnectAccess" /> 实例。</returns>
    /// <param name="allowPort">返回的实例表示的端口。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="allowPort" /> 小于 0。- 或 -<paramref name="allowPort" /> 大于 65,535。</exception>
    public static CodeConnectAccess CreateAnySchemeAccess(int allowPort)
    {
      CodeConnectAccess codeConnectAccess = new CodeConnectAccess();
      string lowerCaseScheme = CodeConnectAccess.AnyScheme;
      int allowPort1 = allowPort;
      codeConnectAccess.SetCodeConnectAccess(lowerCaseScheme, allowPort1);
      return codeConnectAccess;
    }

    private void SetCodeConnectAccess(string lowerCaseScheme, int allowPort)
    {
      this._LowerCaseScheme = lowerCaseScheme;
      if (allowPort == CodeConnectAccess.DefaultPort)
        this._LowerCasePort = "$default";
      else if (allowPort == CodeConnectAccess.OriginPort)
      {
        this._LowerCasePort = "$origin";
      }
      else
      {
        if (allowPort < 0 || allowPort > (int) ushort.MaxValue)
          throw new ArgumentOutOfRangeException("allowPort");
        this._LowerCasePort = allowPort.ToString((IFormatProvider) CultureInfo.InvariantCulture);
      }
      this._IntPort = allowPort;
    }

    /// <summary>返回一个值，指示两个 <see cref="T:System.Security.Policy.CodeConnectAccess" /> 对象是否表示相同的方案和端口。</summary>
    /// <returns>如果两个对象表示相同的方案和端口，则为 true；否则为 false。</returns>
    /// <param name="o">要与当前 <see cref="T:System.Security.Policy.CodeConnectAccess" /> 对象进行比较的对象。</param>
    public override bool Equals(object o)
    {
      if (this == o)
        return true;
      CodeConnectAccess codeConnectAccess = o as CodeConnectAccess;
      if (codeConnectAccess == null || !(this.Scheme == codeConnectAccess.Scheme))
        return false;
      return this.Port == codeConnectAccess.Port;
    }

    public override int GetHashCode()
    {
      return this.Scheme.GetHashCode() + this.Port.GetHashCode();
    }

    internal static bool IsValidScheme(string scheme)
    {
      if (scheme == null || scheme.Length == 0 || !CodeConnectAccess.IsAsciiLetter(scheme[0]))
        return false;
      for (int index = scheme.Length - 1; index > 0; --index)
      {
        if (!CodeConnectAccess.IsAsciiLetterOrDigit(scheme[index]) && (int) scheme[index] != 43 && ((int) scheme[index] != 45 && (int) scheme[index] != 46))
          return false;
      }
      return true;
    }

    private static bool IsAsciiLetterOrDigit(char character)
    {
      if (CodeConnectAccess.IsAsciiLetter(character))
        return true;
      if ((int) character >= 48)
        return (int) character <= 57;
      return false;
    }

    private static bool IsAsciiLetter(char character)
    {
      if ((int) character >= 97 && (int) character <= 122)
        return true;
      if ((int) character >= 65)
        return (int) character <= 90;
      return false;
    }
  }
}
