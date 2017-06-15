// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ICustomAdapter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>为客户端访问实际对象（而不是自定义封送拆收器分发的适配器对象）提供了一种方式。</summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public interface ICustomAdapter
  {
    /// <summary>提供对自定义封送拆收器包装的基础对象的访问权限。</summary>
    /// <returns>适配器对象包含的对象。</returns>
    [__DynamicallyInvokable]
    [return: MarshalAs(UnmanagedType.IUnknown)]
    object GetUnderlyingObject();
  }
}
