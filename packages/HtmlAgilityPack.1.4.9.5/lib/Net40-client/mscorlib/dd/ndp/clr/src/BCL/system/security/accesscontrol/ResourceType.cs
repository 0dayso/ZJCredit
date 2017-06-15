// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.ResourceType
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Security.AccessControl
{
  /// <summary>指定已定义的本机对象类型。</summary>
  public enum ResourceType
  {
    Unknown,
    FileObject,
    Service,
    Printer,
    RegistryKey,
    LMShare,
    KernelObject,
    WindowObject,
    DSObject,
    DSObjectAll,
    ProviderDefined,
    WmiGuidObject,
    RegistryWow6432Key,
  }
}
