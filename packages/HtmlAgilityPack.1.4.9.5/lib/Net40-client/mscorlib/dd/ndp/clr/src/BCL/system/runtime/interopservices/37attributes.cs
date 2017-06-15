// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComCompatibleVersionAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>向 COM 客户端指示程序集当前版本中的所有类与该程序集早期版本中的类兼容。</summary>
  [AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
  [ComVisible(true)]
  public sealed class ComCompatibleVersionAttribute : Attribute
  {
    internal int _major;
    internal int _minor;
    internal int _build;
    internal int _revision;

    /// <summary>获取程序集的主版本号。</summary>
    /// <returns>程序集的主版本号。</returns>
    public int MajorVersion
    {
      get
      {
        return this._major;
      }
    }

    /// <summary>获取程序集的次版本号。</summary>
    /// <returns>程序集的次版本号。</returns>
    public int MinorVersion
    {
      get
      {
        return this._minor;
      }
    }

    /// <summary>获取程序集的生成号。</summary>
    /// <returns>程序集的生成号。</returns>
    public int BuildNumber
    {
      get
      {
        return this._build;
      }
    }

    /// <summary>获取程序集的修订号。</summary>
    /// <returns>程序集的修订号。</returns>
    public int RevisionNumber
    {
      get
      {
        return this._revision;
      }
    }

    /// <summary>使用程序集的主版本、次版本、内部版本号和修订号初始化 <see cref="T:System.Runtime.InteropServices.ComCompatibleVersionAttribute" /> 类的新实例。</summary>
    /// <param name="major">程序集的主版本号。</param>
    /// <param name="minor">程序集的次版本号。</param>
    /// <param name="build">程序集的生成号。</param>
    /// <param name="revision">程序集的修订号。</param>
    public ComCompatibleVersionAttribute(int major, int minor, int build, int revision)
    {
      this._major = major;
      this._minor = minor;
      this._build = build;
      this._revision = revision;
    }
  }
}
