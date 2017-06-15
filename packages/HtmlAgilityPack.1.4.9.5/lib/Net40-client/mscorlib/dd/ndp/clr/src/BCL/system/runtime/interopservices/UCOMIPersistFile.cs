// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.UCOMIPersistFile
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices
{
  /// <summary>请改用 <see cref="T:System.Runtime.InteropServices.ComTypes.IPersistFile" />。</summary>
  [Obsolete("Use System.Runtime.InteropServices.ComTypes.IPersistFile instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
  [Guid("0000010b-0000-0000-C000-000000000046")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  public interface UCOMIPersistFile
  {
    /// <summary>检索对象的类标识符 (CLSID)。</summary>
    /// <param name="pClassID">成功返回时对 CLSID 的引用。</param>
    void GetClassID(out Guid pClassID);

    /// <summary>检查对象自上次保存到其当前文件以来是否更改。</summary>
    /// <returns>如果文件自上次保存以来已经更改，则为 S_OK；如果文件自上次保存以来尚未更改，则为 S_FALSE。</returns>
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int IsDirty();

    /// <summary>打开指定文件并从文件内容初始化对象。</summary>
    /// <param name="pszFileName">以零结尾的字符串，包含要打开的文件的绝对路径。</param>
    /// <param name="dwMode">STGM 枚举中值的组合，指示用来打开 <paramref name="pszFileName" /> 的访问模式。</param>
    void Load([MarshalAs(UnmanagedType.LPWStr)] string pszFileName, int dwMode);

    /// <summary>将该对象的副本保存到指定文件。</summary>
    /// <param name="pszFileName">以零结尾的字符串，包含将该对象保存到的文件的绝对路径。</param>
    /// <param name="fRemember">指示是否将 <paramref name="pszFileName" /> 用作当前工作文件。</param>
    void Save([MarshalAs(UnmanagedType.LPWStr)] string pszFileName, [MarshalAs(UnmanagedType.Bool)] bool fRemember);

    /// <summary>通知该对象它可以写入它的文件。</summary>
    /// <param name="pszFileName">以前保存该对象的文件的绝对路径。</param>
    void SaveCompleted([MarshalAs(UnmanagedType.LPWStr)] string pszFileName);

    /// <summary>检索该对象的当前工作文件的绝对路径，或者如果没有当前工作文件，则检索该对象的默认文件名提示。</summary>
    /// <param name="ppszFileName">指向以零结尾的字符串（它包含当前文件的路径）的指针的地址，或者默认的文件名提示（如 *.txt）。</param>
    void GetCurFile([MarshalAs(UnmanagedType.LPWStr)] out string ppszFileName);
  }
}
