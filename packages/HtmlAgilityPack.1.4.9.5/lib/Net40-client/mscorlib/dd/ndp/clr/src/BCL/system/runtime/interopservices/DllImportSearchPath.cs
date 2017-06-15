// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.DllImportSearchPath
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>指定用于搜索提供平台调用功能的 DLL 的路径。</summary>
  [Flags]
  [__DynamicallyInvokable]
  public enum DllImportSearchPath
  {
    [__DynamicallyInvokable] UseDllDirectoryForDependencies = 256,
    [__DynamicallyInvokable] ApplicationDirectory = 512,
    [__DynamicallyInvokable] UserDirectories = 1024,
    [__DynamicallyInvokable] System32 = 2048,
    [__DynamicallyInvokable] SafeDirectories = 4096,
    [__DynamicallyInvokable] AssemblyDirectory = 2,
    [__DynamicallyInvokable] LegacyBehavior = 0,
  }
}
