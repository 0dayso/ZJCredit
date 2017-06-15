// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.CspProviderFlags
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>指定修改加密服务提供程序 (CSP) 行为的标志。</summary>
  [ComVisible(true)]
  [Flags]
  [Serializable]
  public enum CspProviderFlags
  {
    NoFlags = 0,
    UseMachineKeyStore = 1,
    UseDefaultKeyContainer = 2,
    UseNonExportableKey = 4,
    UseExistingKey = 8,
    UseArchivableKey = 16,
    UseUserProtectedKey = 32,
    NoPrompt = 64,
    CreateEphemeralKey = 128,
  }
}
