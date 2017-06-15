// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.RegistrationClassContext
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>指定执行上下文集，类对象将在这些上下文中对请求构造实例的请求可用。</summary>
  [Flags]
  public enum RegistrationClassContext
  {
    InProcessServer = 1,
    InProcessHandler = 2,
    LocalServer = 4,
    InProcessServer16 = 8,
    RemoteServer = 16,
    InProcessHandler16 = 32,
    Reserved1 = 64,
    Reserved2 = 128,
    Reserved3 = 256,
    Reserved4 = 512,
    NoCodeDownload = 1024,
    Reserved5 = 2048,
    NoCustomMarshal = 4096,
    EnableCodeDownload = 8192,
    NoFailureLog = 16384,
    DisableActivateAsActivator = 32768,
    EnableActivateAsActivator = 65536,
    FromDefaultContext = 131072,
  }
}
