// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices._CustomAttributeBuilder
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection.Emit;

namespace System.Runtime.InteropServices
{
  /// <summary>向非托管代码公开 <see cref="T:System.Reflection.Emit.CustomAttributeBuilder" /> 类。</summary>
  [Guid("BE9ACCE8-AAFF-3B91-81AE-8211663F5CAD")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [CLSCompliant(false)]
  [TypeLibImportClass(typeof (CustomAttributeBuilder))]
  [ComVisible(true)]
  public interface _CustomAttributeBuilder
  {
    /// <summary>检索对象提供的类型信息接口的数量（0 或 1）。</summary>
    /// <param name="pcTInfo">此方法返回时包含一个用于接收对象提供的类型信息接口数量的位置指针。该参数未经初始化即被传递。</param>
    void GetTypeInfoCount(out uint pcTInfo);

    /// <summary>检索对象的类型信息，然后可以使用该信息获取接口的类型信息。</summary>
    /// <param name="iTInfo">要返回的类型信息。</param>
    /// <param name="lcid">类型信息的区域设置标识符。</param>
    /// <param name="ppTInfo">指向请求的类型信息对象的指针。</param>
    void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

    /// <summary>将一组名称映射为对应的一组调度标识符。</summary>
    /// <param name="riid">保留供将来使用。必须为 IID_NULL。</param>
    /// <param name="rgszNames">要映射的名称的数组。</param>
    /// <param name="cNames">要映射的名称的计数。</param>
    /// <param name="lcid">要在其中解释名称的区域设置上下文。</param>
    /// <param name="rgDispId">调用方分配的数组，接收对应于这些名称的标识符。</param>
    void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

    /// <summary>提供对某一对象公开的属性和方法的访问。</summary>
    /// <param name="dispIdMember">成员的标识符。</param>
    /// <param name="riid">保留供将来使用。必须为 IID_NULL。</param>
    /// <param name="lcid">要在其中解释参数的区域设置上下文。</param>
    /// <param name="wFlags">描述调用的上下文的标志。</param>
    /// <param name="pDispParams">指向一个结构的指针，该结构包含一个参数数组、一个命名参数的 DISPID 参数数组和数组元素的计数。</param>
    /// <param name="pVarResult">指向一个将存储结果的位置的指针。</param>
    /// <param name="pExcepInfo">指向一个包含异常信息的结构的指针。</param>
    /// <param name="puArgErr">第一个出错参数的索引。</param>
    void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);
  }
}
