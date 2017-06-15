// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ICustomMarshaler
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>提供用于处理方法调用的自定义包装。</summary>
  [ComVisible(true)]
  public interface ICustomMarshaler
  {
    /// <summary>将非托管数据转换为托管数据。</summary>
    /// <returns>一个对象，它表示该 COM 数据的托管视图。</returns>
    /// <param name="pNativeData">指向要包装的非托管数据的指针。</param>
    object MarshalNativeToManaged(IntPtr pNativeData);

    /// <summary>将托管数据转换为非托管数据。</summary>
    /// <returns>一个指向托管对象的 COM 视图的指针。</returns>
    /// <param name="ManagedObj">要转换的托管对象。</param>
    IntPtr MarshalManagedToNative(object ManagedObj);

    /// <summary>对不再需要的非托管数据进行必要的清理。</summary>
    /// <param name="pNativeData">指向要销毁的非托管数据的指针。</param>
    void CleanUpNativeData(IntPtr pNativeData);

    /// <summary>对不再需要的托管数据进行必要的清理。</summary>
    /// <param name="ManagedObj">要销毁的托管对象。</param>
    void CleanUpManagedData(object ManagedObj);

    /// <summary>返回要封送的本机数据的大小。</summary>
    /// <returns>本机数据的大小（以字节为单位）。</returns>
    int GetNativeDataSize();
  }
}
