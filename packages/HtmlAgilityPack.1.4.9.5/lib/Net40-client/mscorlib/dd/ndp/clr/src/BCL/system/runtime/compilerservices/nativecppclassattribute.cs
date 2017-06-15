// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.NativeCppClassAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
  /// <summary>将指示类型为非托管类型的元数据应用到程序集。此类不能被继承。</summary>
  [AttributeUsage(AttributeTargets.Struct, Inherited = true)]
  [ComVisible(true)]
  [Serializable]
  public sealed class NativeCppClassAttribute : Attribute
  {
  }
}
