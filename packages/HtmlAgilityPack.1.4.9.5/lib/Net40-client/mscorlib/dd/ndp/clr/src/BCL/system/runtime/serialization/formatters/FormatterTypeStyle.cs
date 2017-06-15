// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.Formatters.FormatterTypeStyle
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Runtime.Serialization.Formatters
{
  /// <summary>指示类型说明在序列化流中的布局格式。</summary>
  [ComVisible(true)]
  [Serializable]
  public enum FormatterTypeStyle
  {
    TypesWhenNeeded,
    TypesAlways,
    XsdString,
  }
}
