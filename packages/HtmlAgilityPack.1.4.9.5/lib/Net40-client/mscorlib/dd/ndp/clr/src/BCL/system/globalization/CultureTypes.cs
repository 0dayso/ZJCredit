// Decompiled with JetBrains decompiler
// Type: System.Globalization.CultureTypes
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Globalization
{
  /// <summary>定义可以使用 <see cref="M:System.Globalization.CultureInfo.GetCultures(System.Globalization.CultureTypes)" /> 方法检索的区域性列表的类型。</summary>
  [Flags]
  [ComVisible(true)]
  [Serializable]
  public enum CultureTypes
  {
    NeutralCultures = 1,
    SpecificCultures = 2,
    InstalledWin32Cultures = 4,
    AllCultures = InstalledWin32Cultures | SpecificCultures | NeutralCultures,
    UserCustomCulture = 8,
    ReplacementCultures = 16,
    [Obsolete("This value has been deprecated.  Please use other values in CultureTypes.")] WindowsOnlyCultures = 32,
    [Obsolete("This value has been deprecated.  Please use other values in CultureTypes.")] FrameworkCultures = 64,
  }
}
