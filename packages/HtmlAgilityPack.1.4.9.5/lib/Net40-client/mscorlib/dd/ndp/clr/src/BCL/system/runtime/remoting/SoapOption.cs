// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Metadata.SoapOption
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata
{
  /// <summary>指定与 <see cref="T:System.Runtime.Remoting.Metadata.SoapTypeAttribute" /> 类一起使用的 SOAP 配置选项。</summary>
  [Flags]
  [ComVisible(true)]
  [Serializable]
  public enum SoapOption
  {
    None = 0,
    AlwaysIncludeTypes = 1,
    XsdString = 2,
    EmbedAll = 4,
    Option1 = 8,
    Option2 = 16,
  }
}
