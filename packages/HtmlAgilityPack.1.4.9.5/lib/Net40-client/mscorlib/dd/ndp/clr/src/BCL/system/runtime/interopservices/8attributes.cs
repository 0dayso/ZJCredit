// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.TypeLibImportClassAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>指定哪个 <see cref="T:System.Type" /> 独占使用接口。此类不能被继承。</summary>
  [AttributeUsage(AttributeTargets.Interface, Inherited = false)]
  [ComVisible(true)]
  public sealed class TypeLibImportClassAttribute : Attribute
  {
    internal string _importClassName;

    /// <summary>获取独占使用接口的 <see cref="T:System.Type" /> 对象的名称。</summary>
    /// <returns>独占使用接口的 <see cref="T:System.Type" /> 对象的名称。</returns>
    public string Value
    {
      get
      {
        return this._importClassName;
      }
    }

    /// <summary>初始化 <see cref="T:System.Runtime.InteropServices.TypeLibImportClassAttribute" /> 类的新实例，指定独占使用接口的 <see cref="T:System.Type" />。</summary>
    /// <param name="importClass">独占使用接口的 <see cref="T:System.Type" /> 对象。</param>
    public TypeLibImportClassAttribute(Type importClass)
    {
      this._importClassName = importClass.ToString();
    }
  }
}
