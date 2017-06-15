// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComTypes.EXCEPINFO
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices.ComTypes
{
  /// <summary>描述在 IDispatch::Invoke 过程中发生的异常。</summary>
  [__DynamicallyInvokable]
  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
  public struct EXCEPINFO
  {
    /// <summary>表示用于标识错误的错误代码。</summary>
    [__DynamicallyInvokable]
    public short wCode;
    /// <summary>此字段为保留字段；必须将其设置为 0。</summary>
    [__DynamicallyInvokable]
    public short wReserved;
    /// <summary>指示异常源的名称。该名称通常是一个应用程序名称。</summary>
    [__DynamicallyInvokable]
    [MarshalAs(UnmanagedType.BStr)]
    public string bstrSource;
    /// <summary>描述用户可能遇到的错误。</summary>
    [__DynamicallyInvokable]
    [MarshalAs(UnmanagedType.BStr)]
    public string bstrDescription;
    /// <summary>包含帮助文件的完全限定驱动器、路径和文件名，该帮助文件包含有关错误的更多信息。</summary>
    [__DynamicallyInvokable]
    [MarshalAs(UnmanagedType.BStr)]
    public string bstrHelpFile;
    /// <summary>指示该主题在帮助文件中的帮助上下文 ID。</summary>
    [__DynamicallyInvokable]
    public int dwHelpContext;
    /// <summary>此字段为保留字段；必须将其设置为 null。</summary>
    public IntPtr pvReserved;
    /// <summary>表示指向一个函数的指针，该函数采用 <see cref="T:System.Runtime.InteropServices.EXCEPINFO" /> 结构作为参数并返回 HRESULT 值。如果不想推迟填充，则将此字段设置为 null。</summary>
    public IntPtr pfnDeferredFillIn;
    /// <summary>描述错误的返回值。</summary>
    [__DynamicallyInvokable]
    public int scode;
  }
}
