// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComUnregisterFunctionAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>指定当注销用于 COM 的程序集时调用的方法；这可以用于注销过程中用户编写代码的执行。</summary>
  [AttributeUsage(AttributeTargets.Method, Inherited = false)]
  [ComVisible(true)]
  public sealed class ComUnregisterFunctionAttribute : Attribute
  {
  }
}
