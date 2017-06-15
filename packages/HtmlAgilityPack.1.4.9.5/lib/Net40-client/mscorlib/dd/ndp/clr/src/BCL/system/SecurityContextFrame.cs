// Decompiled with JetBrains decompiler
// Type: System.Reflection.SecurityContextFrame
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security;

namespace System.Reflection
{
  internal struct SecurityContextFrame
  {
    private IntPtr m_GSCookie;
    private IntPtr __VFN_table;
    private IntPtr m_Next;
    private IntPtr m_Assembly;

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public void Push(RuntimeAssembly assembly);

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public void Pop();
  }
}
