// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.IObjectHandle
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Runtime.Remoting
{
  /// <summary>定义用于从间接寻址打开按值封送对象的接口。</summary>
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("C460E2B4-E199-412a-8456-84DC3E4838C3")]
  [ComVisible(true)]
  public interface IObjectHandle
  {
    /// <summary>打开该对象。</summary>
    /// <returns>已打开的对象。</returns>
    object Unwrap();
  }
}
