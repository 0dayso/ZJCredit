// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComRegisterFunctionAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>指定在注册程序集以便从 COM 中使用时要调用的方法；这样可以在注册过程中执行用户编写的代码。</summary>
  [AttributeUsage(AttributeTargets.Method, Inherited = false)]
  [ComVisible(true)]
  public sealed class ComRegisterFunctionAttribute : Attribute
  {
  }
}
