// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.TypeLibImporterFlags
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>指示应该如何生成程序集。</summary>
  [Flags]
  [ComVisible(true)]
  [Serializable]
  public enum TypeLibImporterFlags
  {
    None = 0,
    PrimaryInteropAssembly = 1,
    UnsafeInterfaces = 2,
    SafeArrayAsSystemArray = 4,
    TransformDispRetVals = 8,
    PreventClassMembers = 16,
    SerializableValueClasses = 32,
    ImportAsX86 = 256,
    ImportAsX64 = 512,
    ImportAsItanium = 1024,
    ImportAsAgnostic = 2048,
    ReflectionOnlyLoading = 4096,
    NoDefineVersionResource = 8192,
    ImportAsArm = 16384,
  }
}
