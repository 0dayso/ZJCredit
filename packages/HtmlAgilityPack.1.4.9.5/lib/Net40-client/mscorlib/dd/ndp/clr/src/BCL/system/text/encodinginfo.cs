// Decompiled with JetBrains decompiler
// Type: System.Text.EncodingInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Text
{
  /// <summary>提供有关编码的基本信息。</summary>
  /// <filterpriority>2</filterpriority>
  [Serializable]
  public sealed class EncodingInfo
  {
    private int iCodePage;
    private string strEncodingName;
    private string strDisplayName;

    /// <summary>获取编码的代码页标识符。</summary>
    /// <returns>编码的代码页标识符。</returns>
    /// <filterpriority>2</filterpriority>
    public int CodePage
    {
      get
      {
        return this.iCodePage;
      }
    }

    /// <summary>获取在 Internet 编号分配管理机构 (IANA) 注册的编码的名称。</summary>
    /// <returns>编码的 IANA 名称。有关 IANA 的更多信息，请参见 www.iana.org。</returns>
    /// <filterpriority>2</filterpriority>
    public string Name
    {
      get
      {
        return this.strEncodingName;
      }
    }

    /// <summary>获取编码的可读说明。</summary>
    /// <returns>编码的可读说明。</returns>
    /// <filterpriority>2</filterpriority>
    public string DisplayName
    {
      get
      {
        return this.strDisplayName;
      }
    }

    internal EncodingInfo(int codePage, string name, string displayName)
    {
      this.iCodePage = codePage;
      this.strEncodingName = name;
      this.strDisplayName = displayName;
    }

    /// <summary>返回与当前 <see cref="T:System.Text.EncodingInfo" /> 对象相对应的 <see cref="T:System.Text.Encoding" /> 对象。</summary>
    /// <returns>与当前 <see cref="T:System.Text.EncodingInfo" /> 对象相对应的 <see cref="T:System.Text.Encoding" /> 对象。</returns>
    /// <filterpriority>1</filterpriority>
    public Encoding GetEncoding()
    {
      return Encoding.GetEncoding(this.iCodePage);
    }

    /// <summary>获取一个值，该值指示指定的对象是否等于当前的 <see cref="T:System.Text.EncodingInfo" /> 对象。</summary>
    /// <returns>如果 <paramref name="value" /> 是一个 <see cref="T:System.Text.EncodingInfo" /> 对象且等于当前的 <see cref="T:System.Text.EncodingInfo" /> 对象，则为 true；否则为 false。</returns>
    /// <param name="value">与当前的 <see cref="T:System.Text.EncodingInfo" /> 对象进行比较的对象。</param>
    /// <filterpriority>1</filterpriority>
    public override bool Equals(object value)
    {
      EncodingInfo encodingInfo = value as EncodingInfo;
      if (encodingInfo != null)
        return this.CodePage == encodingInfo.CodePage;
      return false;
    }

    /// <summary>返回当前 <see cref="T:System.Text.EncodingInfo" /> 对象的哈希代码。</summary>
    /// <returns>32 位有符号整数哈希代码。</returns>
    /// <filterpriority>1</filterpriority>
    public override int GetHashCode()
    {
      return this.CodePage;
    }
  }
}
