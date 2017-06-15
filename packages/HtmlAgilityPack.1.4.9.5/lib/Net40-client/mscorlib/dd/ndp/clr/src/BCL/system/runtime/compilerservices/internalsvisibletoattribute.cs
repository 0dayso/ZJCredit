// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.InternalsVisibleToAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.CompilerServices
{
  /// <summary>指定通常仅在当前程序集中可见的类型对指定程序集可见。</summary>
  [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
  [__DynamicallyInvokable]
  public sealed class InternalsVisibleToAttribute : Attribute
  {
    private bool _allInternalsVisible = true;
    private string _assemblyName;

    /// <summary>获取友元程序集的名称，采用 internal 关键字标记的所有类型和类型成员对该程序集均为可见。</summary>
    /// <returns>一个表示友元程序集名称的字符串。</returns>
    [__DynamicallyInvokable]
    public string AssemblyName
    {
      [__DynamicallyInvokable] get
      {
        return this._assemblyName;
      }
    }

    /// <summary>不实现此属性。</summary>
    /// <returns>此属性不返回值。</returns>
    public bool AllInternalsVisible
    {
      get
      {
        return this._allInternalsVisible;
      }
      set
      {
        this._allInternalsVisible = value;
      }
    }

    /// <summary>用指定的友元程序集的名称初始化 <see cref="T:System.Runtime.CompilerServices.InternalsVisibleToAttribute" /> 类的新实例。</summary>
    /// <param name="assemblyName">友元程序集的名称。</param>
    [__DynamicallyInvokable]
    public InternalsVisibleToAttribute(string assemblyName)
    {
      this._assemblyName = assemblyName;
    }
  }
}
