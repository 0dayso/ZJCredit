// Decompiled with JetBrains decompiler
// Type: System.Reflection.AssemblyFileVersionAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>指示编译器使用 Win32 文件版本资源的特定版本号。不要求 Win32 文件版本与程序集的版本号相同。</summary>
  [AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class AssemblyFileVersionAttribute : Attribute
  {
    private string _version;

    /// <summary>获取 Win32 文件版本资源名称。</summary>
    /// <returns>包含文件版本资源名称的字符串。</returns>
    [__DynamicallyInvokable]
    public string Version
    {
      [__DynamicallyInvokable] get
      {
        return this._version;
      }
    }

    /// <summary>初始化 <see cref="T:System.Reflection.AssemblyFileVersionAttribute" /> 类的新实例，指定文件版本。</summary>
    /// <param name="version">文件版本。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="version" /> 为 null。</exception>
    [__DynamicallyInvokable]
    public AssemblyFileVersionAttribute(string version)
    {
      if (version == null)
        throw new ArgumentNullException("version");
      this._version = version;
    }
  }
}
