// Decompiled with JetBrains decompiler
// Type: System.Resources.UltimateResourceFallbackLocation
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Resources
{
  /// <summary>指定 <see cref="T:System.Resources.ResourceManager" /> 对象是否在主程序集或在附属程序集中查找 app 的默认区域性的资源。</summary>
  [ComVisible(true)]
  [Serializable]
  public enum UltimateResourceFallbackLocation
  {
    MainAssembly,
    Satellite,
  }
}
