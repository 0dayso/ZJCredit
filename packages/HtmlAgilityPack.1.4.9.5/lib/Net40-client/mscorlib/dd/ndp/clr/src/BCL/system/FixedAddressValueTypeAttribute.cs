// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.FixedAddressValueTypeAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.CompilerServices
{
  /// <summary>在静态值类型字段的整个生存期内固定其地址。此类不能被继承。</summary>
  [AttributeUsage(AttributeTargets.Field)]
  [Serializable]
  public sealed class FixedAddressValueTypeAttribute : Attribute
  {
  }
}
