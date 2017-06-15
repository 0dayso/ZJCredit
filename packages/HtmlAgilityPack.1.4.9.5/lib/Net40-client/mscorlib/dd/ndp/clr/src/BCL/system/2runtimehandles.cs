// Decompiled with JetBrains decompiler
// Type: System.RuntimeMethodInfoStub
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;

namespace System
{
  internal class RuntimeMethodInfoStub : IRuntimeMethodInfo
  {
    private object m_keepalive;
    private object m_a;
    private object m_b;
    private object m_c;
    private object m_d;
    private object m_e;
    private object m_f;
    private object m_g;
    private object m_h;
    public RuntimeMethodHandleInternal m_value;

    RuntimeMethodHandleInternal IRuntimeMethodInfo.Value
    {
      get
      {
        return this.m_value;
      }
    }

    public RuntimeMethodInfoStub(RuntimeMethodHandleInternal methodHandleValue, object keepalive)
    {
      this.m_keepalive = keepalive;
      this.m_value = methodHandleValue;
    }

    [SecurityCritical]
    public RuntimeMethodInfoStub(IntPtr methodHandleValue, object keepalive)
    {
      this.m_keepalive = keepalive;
      this.m_value = new RuntimeMethodHandleInternal(methodHandleValue);
    }
  }
}
