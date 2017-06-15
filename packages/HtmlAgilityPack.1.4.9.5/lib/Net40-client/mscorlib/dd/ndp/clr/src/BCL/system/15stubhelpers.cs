// Decompiled with JetBrains decompiler
// Type: System.StubHelpers.KeyValuePairMarshaler
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security;

namespace System.StubHelpers
{
  [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
  internal static class KeyValuePairMarshaler
  {
    [SecurityCritical]
    internal static IntPtr ConvertToNative<K, V>([In] ref KeyValuePair<K, V> pair)
    {
      return Marshal.GetComInterfaceForObject((object) new CLRIKeyValuePairImpl<K, V>(ref pair), typeof (IKeyValuePair<K, V>));
    }

    [SecurityCritical]
    internal static KeyValuePair<K, V> ConvertToManaged<K, V>(IntPtr pInsp)
    {
      IKeyValuePair<K, V> keyValuePair = (IKeyValuePair<K, V>) InterfaceMarshaler.ConvertToManagedWithoutUnboxing(pInsp);
      return new KeyValuePair<K, V>(keyValuePair.Key, keyValuePair.Value);
    }

    [SecurityCritical]
    internal static object ConvertToManagedBox<K, V>(IntPtr pInsp)
    {
      return (object) KeyValuePairMarshaler.ConvertToManaged<K, V>(pInsp);
    }
  }
}
