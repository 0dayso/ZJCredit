// Decompiled with JetBrains decompiler
// Type: System.Globalization.GregorianCalendarTypes
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Globalization
{
  /// <summary>定义公历的不同语言版本。</summary>
  [ComVisible(true)]
  [Serializable]
  public enum GregorianCalendarTypes
  {
    Localized = 1,
    USEnglish = 2,
    MiddleEastFrench = 9,
    Arabic = 10,
    TransliteratedEnglish = 11,
    TransliteratedFrench = 12,
  }
}
