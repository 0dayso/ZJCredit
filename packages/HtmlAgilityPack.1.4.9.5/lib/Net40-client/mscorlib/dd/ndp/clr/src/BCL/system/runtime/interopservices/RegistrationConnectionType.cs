// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.RegistrationConnectionType
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>定义到类对象的连接的类型。</summary>
  [Flags]
  public enum RegistrationConnectionType
  {
    SingleUse = 0,
    MultipleUse = 1,
    MultiSeparate = 2,
    Suspended = 4,
    Surrogate = 8,
  }
}
