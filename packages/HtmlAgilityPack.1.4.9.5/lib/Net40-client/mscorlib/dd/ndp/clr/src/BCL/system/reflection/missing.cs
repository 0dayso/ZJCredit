// Decompiled with JetBrains decompiler
// Type: System.Reflection.Missing
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Reflection
{
  /// <summary>表示缺少的 <see cref="T:System.Object" />。此类不能被继承。</summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class Missing : ISerializable
  {
    /// <summary>表示 <see cref="T:System.Reflection.Missing" /> 类的唯一实例。</summary>
    [__DynamicallyInvokable]
    public static readonly Missing Value = new Missing();

    private Missing()
    {
    }

    [SecurityCritical]
    void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException("info");
      UnitySerializationHolder.GetUnitySerializationInfo(info, this);
    }
  }
}
