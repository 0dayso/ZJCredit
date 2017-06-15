// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.DefaultCharSetAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>指定 <see cref="T:System.Runtime.InteropServices.CharSet" /> 枚举的值。此类不能被继承。</summary>
  [AttributeUsage(AttributeTargets.Module, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class DefaultCharSetAttribute : Attribute
  {
    internal CharSet _CharSet;

    /// <summary>获取对 <see cref="T:System.Runtime.InteropServices.DllImportAttribute" /> 的任何调用的 <see cref="T:System.Runtime.InteropServices.CharSet" /> 的默认值。</summary>
    /// <returns>对 <see cref="T:System.Runtime.InteropServices.DllImportAttribute" /> 的任何调用的 <see cref="T:System.Runtime.InteropServices.CharSet" /> 的默认值。</returns>
    [__DynamicallyInvokable]
    public CharSet CharSet
    {
      [__DynamicallyInvokable] get
      {
        return this._CharSet;
      }
    }

    /// <summary>使用指定的 <see cref="T:System.Runtime.InteropServices.CharSet" /> 值初始化 <see cref="T:System.Runtime.InteropServices.DefaultCharSetAttribute" /> 类的新实例。</summary>
    /// <param name="charSet">
    /// <see cref="T:System.Runtime.InteropServices.CharSet" /> 值之一。</param>
    [__DynamicallyInvokable]
    public DefaultCharSetAttribute(CharSet charSet)
    {
      this._CharSet = charSet;
    }
  }
}
