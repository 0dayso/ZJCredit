// Decompiled with JetBrains decompiler
// Type: System.ApplicationId
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security.Util;
using System.Text;

namespace System
{
  /// <summary>所含信息用于唯一标识基于清单的应用程序。此类不能被继承。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [Serializable]
  public sealed class ApplicationId
  {
    private string m_name;
    private Version m_version;
    private string m_processorArchitecture;
    private string m_culture;
    internal byte[] m_publicKeyToken;

    /// <summary>获取应用程序的公钥标记。</summary>
    /// <returns>一个包含应用程序的公钥标记的字节数组。</returns>
    /// <filterpriority>2</filterpriority>
    public byte[] PublicKeyToken
    {
      get
      {
        byte[] numArray = new byte[this.m_publicKeyToken.Length];
        Array.Copy((Array) this.m_publicKeyToken, 0, (Array) numArray, 0, this.m_publicKeyToken.Length);
        return numArray;
      }
    }

    /// <summary>获取应用程序的名称。</summary>
    /// <returns>应用程序的名称。</returns>
    /// <filterpriority>2</filterpriority>
    public string Name
    {
      get
      {
        return this.m_name;
      }
    }

    /// <summary>获取应用程序的版本。</summary>
    /// <returns>
    /// <see cref="T:System.Version" />，用于指定应用程序的版本。</returns>
    /// <filterpriority>2</filterpriority>
    public Version Version
    {
      get
      {
        return this.m_version;
      }
    }

    /// <summary>获取应用程序的目标处理器体系结构。</summary>
    /// <returns>应用程序的处理器体系结构。</returns>
    /// <filterpriority>2</filterpriority>
    public string ProcessorArchitecture
    {
      get
      {
        return this.m_processorArchitecture;
      }
    }

    /// <summary>获取表示此应用程序的区域性信息的字符串。</summary>
    /// <returns>应用程序的区域性信息。</returns>
    /// <filterpriority>2</filterpriority>
    public string Culture
    {
      get
      {
        return this.m_culture;
      }
    }

    internal ApplicationId()
    {
    }

    /// <summary>初始化 <see cref="T:System.ApplicationId" /> 类的新实例。</summary>
    /// <param name="publicKeyToken">表示原始公钥数据的字节数组。</param>
    /// <param name="name">应用程序的名称。</param>
    /// <param name="version">
    /// <see cref="T:System.Version" /> 对象，用于指定应用程序的版本。</param>
    /// <param name="processorArchitecture">应用程序的处理器体系结构。</param>
    /// <param name="culture">应用程序的区域性。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name " />为 null。- 或 -<paramref name="version " />为 null。- 或 -<paramref name="publicKeyToken " />为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="name " /> 是空字符串。</exception>
    public ApplicationId(byte[] publicKeyToken, string name, Version version, string processorArchitecture, string culture)
    {
      if (name == null)
        throw new ArgumentNullException("name");
      if (name.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyApplicationName"));
      if (version == (Version) null)
        throw new ArgumentNullException("version");
      if (publicKeyToken == null)
        throw new ArgumentNullException("publicKeyToken");
      this.m_publicKeyToken = new byte[publicKeyToken.Length];
      Array.Copy((Array) publicKeyToken, 0, (Array) this.m_publicKeyToken, 0, publicKeyToken.Length);
      this.m_name = name;
      this.m_version = version;
      this.m_processorArchitecture = processorArchitecture;
      this.m_culture = culture;
    }

    /// <summary>创建并返回当前应用程序标识的相同副本。</summary>
    /// <returns>
    /// <see cref="T:System.ApplicationId" /> 对象，表示与原件完全相同的副本。</returns>
    /// <filterpriority>2</filterpriority>
    public ApplicationId Copy()
    {
      return new ApplicationId(this.m_publicKeyToken, this.m_name, this.m_version, this.m_processorArchitecture, this.m_culture);
    }

    /// <summary>创建并返回应用程序标识的字符串表示。</summary>
    /// <returns>应用程序标识的字符串表示。</returns>
    /// <filterpriority>2</filterpriority>
    public override string ToString()
    {
      StringBuilder sb = StringBuilderCache.Acquire(16);
      sb.Append(this.m_name);
      if (this.m_culture != null)
      {
        sb.Append(", culture=\"");
        sb.Append(this.m_culture);
        sb.Append("\"");
      }
      sb.Append(", version=\"");
      sb.Append(this.m_version.ToString());
      sb.Append("\"");
      if (this.m_publicKeyToken != null)
      {
        sb.Append(", publicKeyToken=\"");
        sb.Append(Hex.EncodeHexString(this.m_publicKeyToken));
        sb.Append("\"");
      }
      if (this.m_processorArchitecture != null)
      {
        sb.Append(", processorArchitecture =\"");
        sb.Append(this.m_processorArchitecture);
        sb.Append("\"");
      }
      return StringBuilderCache.GetStringAndRelease(sb);
    }

    /// <summary>确定指定的 <see cref="T:System.ApplicationId" /> 对象是否等效于当前 <see cref="T:System.ApplicationId" />。</summary>
    /// <returns>如果指定的 <see cref="T:System.ApplicationId" /> 对象等效于当前 <see cref="T:System.ApplicationId" />，则为 true；否则为 false。</returns>
    /// <param name="o">要与当前 <see cref="T:System.ApplicationId" /> 进行比较的 <see cref="T:System.ApplicationId" /> 对象。</param>
    /// <filterpriority>2</filterpriority>
    public override bool Equals(object o)
    {
      ApplicationId applicationId = o as ApplicationId;
      if (applicationId == null || !object.Equals((object) this.m_name, (object) applicationId.m_name) || (!object.Equals((object) this.m_version, (object) applicationId.m_version) || !object.Equals((object) this.m_processorArchitecture, (object) applicationId.m_processorArchitecture)) || (!object.Equals((object) this.m_culture, (object) applicationId.m_culture) || this.m_publicKeyToken.Length != applicationId.m_publicKeyToken.Length))
        return false;
      for (int index = 0; index < this.m_publicKeyToken.Length; ++index)
      {
        if ((int) this.m_publicKeyToken[index] != (int) applicationId.m_publicKeyToken[index])
          return false;
      }
      return true;
    }

    /// <summary>获取当前应用程序标识的哈希代码。</summary>
    /// <returns>当前应用程序标识的哈希代码。</returns>
    /// <filterpriority>2</filterpriority>
    public override int GetHashCode()
    {
      return this.m_name.GetHashCode() ^ this.m_version.GetHashCode();
    }
  }
}
