// Decompiled with JetBrains decompiler
// Type: System.Empty
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
  [Serializable]
  internal sealed class Empty : ISerializable
  {
    public static readonly Empty Value = new Empty();

    private Empty()
    {
    }

    public override string ToString()
    {
      return string.Empty;
    }

    [SecurityCritical]
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException("info");
      UnitySerializationHolder.GetUnitySerializationInfo(info, 1, (string) null, (RuntimeAssembly) null);
    }
  }
}
