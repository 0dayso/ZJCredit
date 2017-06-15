// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ExtensibleClassFactory
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices
{
  /// <summary>在创建过程中启用从非托管对象扩展的托管对象的自定义。</summary>
  [ComVisible(true)]
  public sealed class ExtensibleClassFactory
  {
    private ExtensibleClassFactory()
    {
    }

    /// <summary>注册一个 delegate，每次从非托管类型扩展的托管类型的实例需要分配聚合的非托管对象时，都要调用该委托。</summary>
    /// <param name="callback">代替 CoCreateInstance 调用的 delegate。</param>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern void RegisterObjectCreationCallback(ObjectCreationDelegate callback);
  }
}
