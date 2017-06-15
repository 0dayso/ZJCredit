// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Proxies.__TransparentProxy
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;

namespace System.Runtime.Remoting.Proxies
{
  internal sealed class __TransparentProxy
  {
    [SecurityCritical]
    private RealProxy _rp;
    private object _stubData;
    private IntPtr _pMT;
    private IntPtr _pInterfaceMT;
    private IntPtr _stub;

    private __TransparentProxy()
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_Constructor"));
    }
  }
}
