// Decompiled with JetBrains decompiler
// Type: System.Reflection.BindingFlags
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>指定控制绑定和由反射执行的成员和类型搜索方法的标志。</summary>
  [Flags]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public enum BindingFlags
  {
    Default = 0,
    [__DynamicallyInvokable] IgnoreCase = 1,
    [__DynamicallyInvokable] DeclaredOnly = 2,
    [__DynamicallyInvokable] Instance = 4,
    [__DynamicallyInvokable] Static = 8,
    [__DynamicallyInvokable] Public = 16,
    [__DynamicallyInvokable] NonPublic = 32,
    [__DynamicallyInvokable] FlattenHierarchy = 64,
    InvokeMethod = 256,
    CreateInstance = 512,
    GetField = 1024,
    SetField = 2048,
    GetProperty = 4096,
    SetProperty = 8192,
    PutDispProperty = 16384,
    PutRefDispProperty = 32768,
    [__DynamicallyInvokable] ExactBinding = 65536,
    SuppressChangeType = 131072,
    [__DynamicallyInvokable] OptionalParamBinding = 262144,
    IgnoreReturn = 16777216,
  }
}
