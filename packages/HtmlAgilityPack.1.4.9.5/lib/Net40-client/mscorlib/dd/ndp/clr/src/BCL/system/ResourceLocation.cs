// Decompiled with JetBrains decompiler
// Type: System.Reflection.ResourceLocation
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>指定资源位置。</summary>
  [Flags]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public enum ResourceLocation
  {
    [__DynamicallyInvokable] Embedded = 1,
    [__DynamicallyInvokable] ContainedInAnotherAssembly = 2,
    [__DynamicallyInvokable] ContainedInManifestFile = 4,
  }
}
