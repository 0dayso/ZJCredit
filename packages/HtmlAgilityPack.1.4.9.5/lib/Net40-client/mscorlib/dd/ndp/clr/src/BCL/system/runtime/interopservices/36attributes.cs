// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.TypeLibVersionAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>指定导出类型库的版本号。</summary>
  [AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
  [ComVisible(true)]
  public sealed class TypeLibVersionAttribute : Attribute
  {
    internal int _major;
    internal int _minor;

    /// <summary>获取类型库的主版本号。</summary>
    /// <returns>类型库的主版本号。</returns>
    public int MajorVersion
    {
      get
      {
        return this._major;
      }
    }

    /// <summary>获取类型库的次版本号。</summary>
    /// <returns>类型库的次版本号。</returns>
    public int MinorVersion
    {
      get
      {
        return this._minor;
      }
    }

    /// <summary>使用类型库的主版本和次版本号初始化 <see cref="T:System.Runtime.InteropServices.TypeLibVersionAttribute" /> 类的新实例。</summary>
    /// <param name="major">类型库的主版本号。</param>
    /// <param name="minor">类型库的次版本号。</param>
    public TypeLibVersionAttribute(int major, int minor)
    {
      this._major = major;
      this._minor = minor;
    }
  }
}
