// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Activation.ActivatorLevel
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Activation
{
  /// <summary>定义 <see cref="T:System.Activator" /> 在激活器链中的适当位置。</summary>
  [ComVisible(true)]
  [Serializable]
  public enum ActivatorLevel
  {
    Construction = 4,
    Context = 8,
    AppDomain = 12,
    Process = 16,
    Machine = 20,
  }
}
