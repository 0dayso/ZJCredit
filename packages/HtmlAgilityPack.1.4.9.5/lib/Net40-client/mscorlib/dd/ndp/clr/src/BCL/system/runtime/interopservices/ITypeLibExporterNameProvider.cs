// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ITypeLibExporterNameProvider
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>当导出到类型库时提供名称大小写控制。</summary>
  [Guid("FA1F3615-ACB9-486d-9EAC-1BEF87E36B09")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComVisible(true)]
  public interface ITypeLibExporterNameProvider
  {
    /// <summary>返回要控制其大小写的名称的列表。</summary>
    /// <returns>字符串数组，其中每个元素都包含要控制其大小写的类型的名称。</returns>
    [return: MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_BSTR)]
    string[] GetNames();
  }
}
