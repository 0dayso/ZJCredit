// Decompiled with JetBrains decompiler
// Type: System.OperatingSystem
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
  /// <summary>表示有关操作系统的信息，如版本和平台标识符。此类不能被继承。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [Serializable]
  public sealed class OperatingSystem : ICloneable, ISerializable
  {
    private Version _version;
    private PlatformID _platform;
    private string _servicePack;
    private string _versionString;

    /// <summary>获取标识操作系统平台的 <see cref="T:System.PlatformID" /> 枚举值。</summary>
    /// <returns>
    /// <see cref="T:System.PlatformID" /> 值之一。</returns>
    /// <filterpriority>2</filterpriority>
    public PlatformID Platform
    {
      get
      {
        return this._platform;
      }
    }

    /// <summary>获取此 <see cref="T:System.OperatingSystem" /> 对象表示的 Service Pack 版本。</summary>
    /// <returns>如果支持 Service Pack 并至少安装了一个 Service Pack，则为该 Service Pack 版本；否则为空字符串 ("")。</returns>
    /// <filterpriority>2</filterpriority>
    public string ServicePack
    {
      get
      {
        if (this._servicePack == null)
          return string.Empty;
        return this._servicePack;
      }
    }

    /// <summary>获取标识操作系统的 <see cref="T:System.Version" /> 对象。</summary>
    /// <returns>
    /// <see cref="T:System.Version" /> 对象，描述操作系统的主版本号、次版本号、内部版本号和修订版本号。</returns>
    /// <filterpriority>2</filterpriority>
    public Version Version
    {
      get
      {
        return this._version;
      }
    }

    /// <summary>获取平台标识符、版本和当前安装在操作系统上的 Service Pack 的连接字符串表示形式。</summary>
    /// <returns>
    /// <see cref="P:System.OperatingSystem.Platform" />、<see cref="P:System.OperatingSystem.Version" /> 和 <see cref="P:System.OperatingSystem.ServicePack" /> 属性的返回值的字符串表示形式。</returns>
    /// <filterpriority>2</filterpriority>
    public string VersionString
    {
      get
      {
        if (this._versionString != null)
          return this._versionString;
        string str;
        switch (this._platform)
        {
          case PlatformID.Win32S:
            str = "Microsoft Win32S ";
            break;
          case PlatformID.Win32Windows:
            str = this._version.Major > 4 || this._version.Major == 4 && this._version.Minor > 0 ? "Microsoft Windows 98 " : "Microsoft Windows 95 ";
            break;
          case PlatformID.Win32NT:
            str = "Microsoft Windows NT ";
            break;
          case PlatformID.WinCE:
            str = "Microsoft Windows CE ";
            break;
          case PlatformID.MacOSX:
            str = "Mac OS X ";
            break;
          default:
            str = "<unknown> ";
            break;
        }
        this._versionString = !string.IsNullOrEmpty(this._servicePack) ? str + this._version.ToString(3) + " " + this._servicePack : str + this._version.ToString();
        return this._versionString;
      }
    }

    private OperatingSystem()
    {
    }

    /// <summary>使用指定的平台标识符值和版本对象来初始化 <see cref="T:System.OperatingSystem" /> 类的新实例。</summary>
    /// <param name="platform">
    /// <see cref="T:System.PlatformID" /> 值之一，指示操作系统平台。</param>
    /// <param name="version">
    /// <see cref="T:System.Version" /> 对象，指示操作系统的版本。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="version" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="platform" /> 不是一个 <see cref="T:System.PlatformID" /> 枚举值。</exception>
    public OperatingSystem(PlatformID platform, Version version)
      : this(platform, version, (string) null)
    {
    }

    internal OperatingSystem(PlatformID platform, Version version, string servicePack)
    {
      if (platform < PlatformID.Win32S || platform > PlatformID.MacOSX)
        throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", (object) platform), "platform");
      if (version == null)
        throw new ArgumentNullException("version");
      this._platform = platform;
      this._version = (Version) version.Clone();
      this._servicePack = servicePack;
    }

    private OperatingSystem(SerializationInfo info, StreamingContext context)
    {
      SerializationInfoEnumerator enumerator = info.GetEnumerator();
      while (enumerator.MoveNext())
      {
        string name = enumerator.Name;
        if (!(name == "_version"))
        {
          if (!(name == "_platform"))
          {
            if (name == "_servicePack")
              this._servicePack = info.GetString("_servicePack");
          }
          else
            this._platform = (PlatformID) info.GetValue("_platform", typeof (PlatformID));
        }
        else
          this._version = (Version) info.GetValue("_version", typeof (Version));
      }
      if (this._version == (Version) null)
        throw new SerializationException(Environment.GetResourceString("Serialization_MissField", (object) "_version"));
    }

    /// <summary>使用反序列化此实例所需的数据填充 <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 对象。</summary>
    /// <param name="info">要用序列化信息填充的对象。</param>
    /// <param name="context">存储和检索序列化数据的位置。留待将来使用。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="info" /> 为 null。</exception>
    /// <filterpriority>2</filterpriority>
    [SecurityCritical]
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException("info");
      info.AddValue("_version", (object) this._version);
      info.AddValue("_platform", (object) this._platform);
      info.AddValue("_servicePack", (object) this._servicePack);
    }

    /// <summary>创建与此实例相同的 <see cref="T:System.OperatingSystem" /> 对象。</summary>
    /// <returns>
    /// <see cref="T:System.OperatingSystem" /> 对象，是此实例的副本。</returns>
    /// <filterpriority>2</filterpriority>
    public object Clone()
    {
      return (object) new OperatingSystem(this._platform, this._version, this._servicePack);
    }

    /// <summary>将此 <see cref="T:System.OperatingSystem" /> 对象的值转换为其等效的字符串表示形式。</summary>
    /// <returns>
    /// <see cref="P:System.OperatingSystem.Platform" />、<see cref="P:System.OperatingSystem.Version" /> 和 <see cref="P:System.OperatingSystem.ServicePack" /> 属性的返回值的字符串表示形式。</returns>
    /// <filterpriority>2</filterpriority>
    public override string ToString()
    {
      return this.VersionString;
    }
  }
}
