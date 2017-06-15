// Decompiled with JetBrains decompiler
// Type: System.IO.IsolatedStorage.IsolatedStorageScope
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.IO.IsolatedStorage
{
  /// <summary>枚举受 <see cref="T:System.IO.IsolatedStorage.IsolatedStorage" /> 支持的独立存储范围的级别。</summary>
  [Flags]
  [ComVisible(true)]
  [Serializable]
  public enum IsolatedStorageScope
  {
    None = 0,
    User = 1,
    Domain = 2,
    Assembly = 4,
    Roaming = 8,
    Machine = 16,
    Application = 32,
  }
}
