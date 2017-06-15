// Decompiled with JetBrains decompiler
// Type: System.IAppDomainSetup
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System
{
  /// <summary>表示可以添加到 <see cref="T:System.AppDomain" /> 的实例的程序集绑定信息。</summary>
  /// <filterpriority>2</filterpriority>
  [Guid("27FFF232-A7A8-40dd-8D4A-734AD59FCD41")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComVisible(true)]
  public interface IAppDomainSetup
  {
    /// <summary>获取或设置包含该应用程序的目录的名称。</summary>
    /// <returns>包含应用程序基目录名称的 <see cref="T:System.String" />。</returns>
    /// <filterpriority>2</filterpriority>
    string ApplicationBase { get; set; }

    /// <summary>获取或设置应用程序的名称。</summary>
    /// <returns>作为应用程序名称的 <see cref="T:System.String" />。</returns>
    /// <filterpriority>2</filterpriority>
    string ApplicationName { get; set; }

    /// <summary>获取和设置特定于应用程序的区域名称，在该区域中影像复制文件。</summary>
    /// <returns>一个 <see cref="T:System.String" />，它是在其中进行文件影像复制的完全限定的目录路径名称和文件名。</returns>
    /// <filterpriority>2</filterpriority>
    string CachePath { get; set; }

    /// <summary>获取和设置应用程序域的配置文件的名称。</summary>
    /// <returns>指定配置文件名称的 <see cref="T:System.String" />。</returns>
    /// <filterpriority>2</filterpriority>
    string ConfigurationFile { get; set; }

    /// <summary>获取或设置将在其中存储和访问动态生成文件的目录。</summary>
    /// <returns>指定包含动态程序集的目录的 <see cref="T:System.String" />。</returns>
    /// <filterpriority>2</filterpriority>
    string DynamicBase { get; set; }

    /// <summary>获取或设置与此域关联的许可证文件的位置。</summary>
    /// <returns>指定许可证文件名称的 <see cref="T:System.String" />。</returns>
    /// <filterpriority>2</filterpriority>
    string LicenseFile { get; set; }

    /// <summary>获取或设置目录列表，它与 <see cref="P:System.AppDomainSetup.ApplicationBase" /> 目录结合来探测专用程序集。</summary>
    /// <returns>包含目录名称列表的 <see cref="T:System.String" />，其中每个名称都用分号隔开。</returns>
    /// <filterpriority>2</filterpriority>
    string PrivateBinPath { get; set; }

    /// <summary>获取或设置用于定位应用程序的专用二进制目录路径。</summary>
    /// <returns>包含目录名称列表的 <see cref="T:System.String" />，其中每个名称都用分号隔开。</returns>
    /// <filterpriority>2</filterpriority>
    string PrivateBinPathProbe { get; set; }

    /// <summary>获取或设置目录的名称，这些目录包含要影像复制的程序集。</summary>
    /// <returns>包含目录名称列表的 <see cref="T:System.String" />，其中每个名称都用分号隔开。</returns>
    /// <filterpriority>2</filterpriority>
    string ShadowCopyDirectories { get; set; }

    /// <summary>获取或设置指示影像复制是打开还是关闭的字符串。</summary>
    /// <returns>如果 <see cref="T:System.String" /> 包含值“true”，则指示影像复制已打开；如果包含值“false”，则指示影像复制已关闭。</returns>
    /// <filterpriority>2</filterpriority>
    string ShadowCopyFiles { get; set; }
  }
}
