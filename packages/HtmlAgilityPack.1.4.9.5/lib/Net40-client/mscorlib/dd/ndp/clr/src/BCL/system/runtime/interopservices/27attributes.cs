// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.DefaultDllImportSearchPathsAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>指定用于搜索提供平台调用功能的 DLL 的路径。</summary>
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Method, AllowMultiple = false)]
  [ComVisible(false)]
  [__DynamicallyInvokable]
  public sealed class DefaultDllImportSearchPathsAttribute : Attribute
  {
    internal DllImportSearchPath _paths;

    /// <summary>获取指定路径的枚举值的按位组合，路径在平台调用期间由 LoadLibraryEx 函数搜索。</summary>
    /// <returns>指定平台调用搜索路径的枚举值的按位组合。</returns>
    [__DynamicallyInvokable]
    public DllImportSearchPath Paths
    {
      [__DynamicallyInvokable] get
      {
        return this._paths;
      }
    }

    /// <summary>初始化 <see cref="T:System.Runtime.InteropServices.DefaultDllImportSearchPathsAttribute" /> 类的新实例，该实例指定用于当搜索目标平台调用时的路径。</summary>
    /// <param name="paths">指定平台调用期间 LoadLibraryEx 函数搜索路径的枚举值的按位组合。</param>
    [__DynamicallyInvokable]
    public DefaultDllImportSearchPathsAttribute(DllImportSearchPath paths)
    {
      this._paths = paths;
    }
  }
}
