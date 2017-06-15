// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ICustomFactory
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>使用户能够为扩展 <see cref="T:System.MarshalByRefObject" /> 的托管对象编写激活代码。</summary>
  [ComVisible(true)]
  public interface ICustomFactory
  {
    /// <summary>创建指定类型的新实例。</summary>
    /// <returns>与指定类型关联的 <see cref="T:System.MarshalByRefObject" />。</returns>
    /// <param name="serverType">要激活的类型。</param>
    MarshalByRefObject CreateInstance(Type serverType);
  }
}
