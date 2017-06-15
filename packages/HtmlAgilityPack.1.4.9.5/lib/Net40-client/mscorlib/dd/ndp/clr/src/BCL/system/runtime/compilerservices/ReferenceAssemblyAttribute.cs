// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.ReferenceAssemblyAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.CompilerServices
{
  /// <summary>将程序集标识为包含元数据但不包含可执行代码的引用程序集。</summary>
  [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class ReferenceAssemblyAttribute : Attribute
  {
    private string _description;

    /// <summary>获取引用程序集的说明。</summary>
    /// <returns>引用程序集的说明。</returns>
    [__DynamicallyInvokable]
    public string Description
    {
      [__DynamicallyInvokable] get
      {
        return this._description;
      }
    }

    /// <summary>初始化 <see cref="T:System.Runtime.CompilerServices.ReferenceAssemblyAttribute" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public ReferenceAssemblyAttribute()
    {
    }

    /// <summary>使用指定的说明初始化 <see cref="T:System.Runtime.CompilerServices.ReferenceAssemblyAttribute" /> 类的新实例。</summary>
    /// <param name="description">引用程序集的说明。</param>
    [__DynamicallyInvokable]
    public ReferenceAssemblyAttribute(string description)
    {
      this._description = description;
    }
  }
}
