// Decompiled with JetBrains decompiler
// Type: Microsoft.Win32.RegistryKeyPermissionCheck
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace Microsoft.Win32
{
  /// <summary>指定在打开注册表项并访问它们的名称/值对时是否执行安全检查。</summary>
  public enum RegistryKeyPermissionCheck
  {
    Default,
    ReadSubTree,
    ReadWriteSubTree,
  }
}
