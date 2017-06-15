// Decompiled with JetBrains decompiler
// Type: System.StringSplitOptions
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System
{
  /// <summary>指定适用的 <see cref="Overload:System.String.Split" /> 方法重载包含还是省略返回值中的空子字符串。</summary>
  /// <filterpriority>1</filterpriority>
  [ComVisible(false)]
  [Flags]
  [__DynamicallyInvokable]
  public enum StringSplitOptions
  {
    [__DynamicallyInvokable] None = 0,
    [__DynamicallyInvokable] RemoveEmptyEntries = 1,
  }
}
