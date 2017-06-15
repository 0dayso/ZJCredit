// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComAliasNameAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>指示参数或字段类型的 COM 别名。</summary>
  [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, Inherited = false)]
  [ComVisible(true)]
  public sealed class ComAliasNameAttribute : Attribute
  {
    internal string _val;

    /// <summary>获取导入字段或参数时在类型库中找到的别名。</summary>
    /// <returns>导入字段或参数时在类型库中找到的别名。</returns>
    public string Value
    {
      get
      {
        return this._val;
      }
    }

    /// <summary>使用特性化字段或参数的别名初始化 <see cref="T:System.Runtime.InteropServices.ComAliasNameAttribute" /> 类的新实例。</summary>
    /// <param name="alias">导入字段或参数时在类型库中找到的别名。</param>
    public ComAliasNameAttribute(string alias)
    {
      this._val = alias;
    }
  }
}
