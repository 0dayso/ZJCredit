// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.PrimaryInteropAssemblyAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>指示该特性化的程序集是主 Interop 程序集。</summary>
  [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
  [ComVisible(true)]
  public sealed class PrimaryInteropAssemblyAttribute : Attribute
  {
    internal int _major;
    internal int _minor;

    /// <summary>获取程序集是其主 Interop 程序集的类型库的主要版本号。</summary>
    /// <returns>程序集是其主 Interop 程序集的类型库的主要版本号。</returns>
    public int MajorVersion
    {
      get
      {
        return this._major;
      }
    }

    /// <summary>获取程序集是其主 Interop 程序集的类型库的次要版本号。</summary>
    /// <returns>程序集是其主 Interop 程序集的类型库的次要版本号。</returns>
    public int MinorVersion
    {
      get
      {
        return this._minor;
      }
    }

    /// <summary>用类型库（此程序集是该类型库的主 interop 程序集）的主版本号及次版本号初始化 <see cref="T:System.Runtime.InteropServices.PrimaryInteropAssemblyAttribute" /> 类的新实例。</summary>
    /// <param name="major">程序集是其主 Interop 程序集的类型库的主要版本号。</param>
    /// <param name="minor">程序集是其主 Interop 程序集的类型库的次要版本号。</param>
    public PrimaryInteropAssemblyAttribute(int major, int minor)
    {
      this._major = major;
      this._minor = minor;
    }
  }
}
