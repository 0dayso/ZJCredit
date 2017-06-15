// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.CoClassAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>指定从类型库中导入的 coclass 的类标识符。</summary>
  [AttributeUsage(AttributeTargets.Interface, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class CoClassAttribute : Attribute
  {
    internal Type _CoClass;

    /// <summary>获取原始 coclass 的类标识符。</summary>
    /// <returns>一个 <see cref="T:System.Type" />，它包含原始 coclass 的类标识符。</returns>
    [__DynamicallyInvokable]
    public Type CoClass
    {
      [__DynamicallyInvokable] get
      {
        return this._CoClass;
      }
    }

    /// <summary>用原始 coclass 的类标识符初始化 <see cref="T:System.Runtime.InteropServices.CoClassAttribute" /> 的新实例。</summary>
    /// <param name="coClass">一个 <see cref="T:System.Type" />，它包含原始 coclass 的类标识符。</param>
    [__DynamicallyInvokable]
    public CoClassAttribute(Type coClass)
    {
      this._CoClass = coClass;
    }
  }
}
