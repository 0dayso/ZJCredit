// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.Formatters.FormatterAssemblyStyle
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Runtime.Serialization.Formatters
{
  /// <summary>指示将在反序列化期间用于查找和加载程序集的方法。</summary>
  [ComVisible(true)]
  [Serializable]
  public enum FormatterAssemblyStyle
  {
    Simple,
    Full,
  }
}
