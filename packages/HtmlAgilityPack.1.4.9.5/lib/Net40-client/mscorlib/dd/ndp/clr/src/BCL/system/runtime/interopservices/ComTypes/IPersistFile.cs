// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComTypes.IPersistFile
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices.ComTypes
{
  /// <summary>提供具有 IPersist 中的功能的 IPersistFile 接口的托管定义。</summary>
  [Guid("0000010b-0000-0000-C000-000000000046")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [__DynamicallyInvokable]
  [ComImport]
  public interface IPersistFile
  {
    /// <summary>检索对象的类标识符 (CLSID)。</summary>
    /// <param name="pClassID">此方法返回时，包含对 CLSID 的引用。该参数未经初始化即被传递。</param>
    [__DynamicallyInvokable]
    void GetClassID(out Guid pClassID);

    /// <summary>检查对象自上次保存到其当前文件以来是否更改。</summary>
    /// <returns>如果文件自上次保存以来已经更改，则为 S_OK；如果文件自上次保存以来尚未更改，则为 S_FALSE。</returns>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int IsDirty();

    /// <summary>打开指定文件并从文件内容初始化对象。</summary>
    /// <param name="pszFileName">以零结尾的字符串，包含要打开的文件的绝对路径。</param>
    /// <param name="dwMode">STGM 枚举中值的组合，指示用来打开 <paramref name="pszFileName" /> 的访问模式。</param>
    [__DynamicallyInvokable]
    void Load([MarshalAs(UnmanagedType.LPWStr)] string pszFileName, int dwMode);

    /// <summary>将该对象的副本保存到指定文件。</summary>
    /// <param name="pszFileName">以零结尾的字符串，包含将该对象保存到的文件的绝对路径。</param>
    /// <param name="fRemember">将 <paramref name="pszFileName" /> 参数用作当前工作文件时为 true；否则为 false。</param>
    [__DynamicallyInvokable]
    void Save([MarshalAs(UnmanagedType.LPWStr)] string pszFileName, [MarshalAs(UnmanagedType.Bool)] bool fRemember);

    /// <summary>通知该对象它可以写入它的文件。</summary>
    /// <param name="pszFileName">以前保存该对象的文件的绝对路径。</param>
    [__DynamicallyInvokable]
    void SaveCompleted([MarshalAs(UnmanagedType.LPWStr)] string pszFileName);

    /// <summary>检索该对象的当前工作文件的绝对路径，或者，如果没有当前工作文件，则检索该对象的默认文件名提示。</summary>
    /// <param name="ppszFileName">此方法返回时，包含指向一个以零终止的字符串的指针的地址，该字符串中包含当前文件的路径或者默认的文件名提示（如 *.txt）。该参数未经初始化即被传递。</param>
    [__DynamicallyInvokable]
    void GetCurFile([MarshalAs(UnmanagedType.LPWStr)] out string ppszFileName);
  }
}
