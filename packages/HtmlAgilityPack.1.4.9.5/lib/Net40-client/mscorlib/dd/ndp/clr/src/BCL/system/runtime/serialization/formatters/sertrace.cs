// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.Formatters.InternalRM
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Serialization.Formatters
{
  /// <summary>在编译 .NET Framework 序列化基础结构时记录跟踪消息。</summary>
  [SecurityCritical]
  [ComVisible(true)]
  public sealed class InternalRM
  {
    /// <summary>打印 SOAP 跟踪消息。</summary>
    /// <param name="messages">要打印的跟踪消息数组。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.StrongNameIdentityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PublicKeyBlob="00000000000000000400000000000000" Name="System.Runtime.Remoting" />
    /// </PermissionSet>
    [Conditional("_LOGGING")]
    public static void InfoSoap(params object[] messages)
    {
    }

    /// <summary>检查是否启用了 SOAP 跟踪。</summary>
    /// <returns>如果已启用跟踪，则为 true；否则为 false。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.StrongNameIdentityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PublicKeyBlob="00000000000000000400000000000000" Name="System.Runtime.Remoting" />
    /// </PermissionSet>
    public static bool SoapCheckEnabled()
    {
      return BCLDebug.CheckEnabled("SOAP");
    }
  }
}
