// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.GuidAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>当不需要自动 GUID 时提供显式的 <see cref="T:System.Guid" />。</summary>
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.Delegate, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class GuidAttribute : Attribute
  {
    internal string _val;

    /// <summary>获取类的 <see cref="T:System.Guid" />。</summary>
    /// <returns>类的 <see cref="T:System.Guid" />。</returns>
    [__DynamicallyInvokable]
    public string Value
    {
      [__DynamicallyInvokable] get
      {
        return this._val;
      }
    }

    /// <summary>用指定的 GUID 初始化 <see cref="T:System.Runtime.InteropServices.GuidAttribute" /> 类的新实例。</summary>
    /// <param name="guid">要分配的 <see cref="T:System.Guid" />。</param>
    [__DynamicallyInvokable]
    public GuidAttribute(string guid)
    {
      this._val = guid;
    }
  }
}
