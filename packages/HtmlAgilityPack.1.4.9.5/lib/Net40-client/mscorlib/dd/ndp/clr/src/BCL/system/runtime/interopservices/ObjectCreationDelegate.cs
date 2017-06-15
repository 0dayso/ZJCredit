// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ObjectCreationDelegate
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>创建 COM 对象。</summary>
  /// <returns>表示 COM 对象的 IUnknown 接口的 <see cref="T:System.IntPtr" /> 对象。</returns>
  /// <param name="aggregator">指向托管对象的 IUnknown 接口的指针。</param>
  [ComVisible(true)]
  public delegate IntPtr ObjectCreationDelegate(IntPtr aggregator);
}
