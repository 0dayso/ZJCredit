// Decompiled with JetBrains decompiler
// Type: System.Threading.HostExecutionContextSwitcher
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.ConstrainedExecution;
using System.Security;

namespace System.Threading
{
  internal class HostExecutionContextSwitcher
  {
    internal ExecutionContext executionContext;
    internal HostExecutionContext previousHostContext;
    internal HostExecutionContext currentHostContext;

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    public static void Undo(object switcherObject)
    {
      if (switcherObject == null)
        return;
      HostExecutionContextManager executionContextManager = HostExecutionContextManager.GetCurrentHostExecutionContextManager();
      if (executionContextManager == null)
        return;
      executionContextManager.Revert(switcherObject);
    }
  }
}
