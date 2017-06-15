// Decompiled with JetBrains decompiler
// Type: System.LoaderOptimization
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System
{
  /// <summary>一个枚举，它与 <see cref="T:System.LoaderOptimizationAttribute" /> 类一起使用为可执行文件指定加载程序优化。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [Serializable]
  public enum LoaderOptimization
  {
    NotSpecified = 0,
    SingleDomain = 1,
    MultiDomain = 2,
    [Obsolete("This method has been deprecated. Please use Assembly.Load() instead. http://go.microsoft.com/fwlink/?linkid=14202")] DomainMask = 3,
    MultiDomainHost = 3,
    [Obsolete("This method has been deprecated. Please use Assembly.Load() instead. http://go.microsoft.com/fwlink/?linkid=14202")] DisallowBindings = 4,
  }
}
