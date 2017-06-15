// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ITypeLibImporterNotifySink
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;

namespace System.Runtime.InteropServices
{
  /// <summary>提供回调机制，以供类型库转换器向调用方通知转换的状态，并在转换过程本身之中涉及调用方。</summary>
  [Guid("F1C3BF76-C3E4-11d3-88E7-00902754C43A")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComVisible(true)]
  public interface ITypeLibImporterNotifySink
  {
    /// <summary>通知调用方在类型库转换过程中发生了一个事件。</summary>
    /// <param name="eventKind">指示事件类型的 <see cref="T:System.Runtime.InteropServices.ImporterEventKind" /> 值。</param>
    /// <param name="eventCode">指示有关事件的其他信息。</param>
    /// <param name="eventMsg">事件生成的消息。</param>
    void ReportEvent(ImporterEventKind eventKind, int eventCode, string eventMsg);

    /// <summary>请求用户解析对另一个类型库的引用。</summary>
    /// <returns>与 <paramref name="typeLib" /> 对应的程序集。</returns>
    /// <param name="typeLib">实现 ITypeLib 接口的对象，需要对该接口进行解析。</param>
    Assembly ResolveRef([MarshalAs(UnmanagedType.Interface)] object typeLib);
  }
}
