// Decompiled with JetBrains decompiler
// Type: System.Reflection.AssemblyNameProxy
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>提供可远程使用的 AssemblyName 版本。</summary>
  [ComVisible(true)]
  public class AssemblyNameProxy : MarshalByRefObject
  {
    /// <summary>获取给定文件的 AssemblyName。</summary>
    /// <returns>表示给定文件的 AssemblyName 对象。</returns>
    /// <param name="assemblyFile">要为其获取 AssemblyName 的程序集文件。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="assemblyFile" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="assemblyFile" /> 为空。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">未找到 <paramref name="assemblyFile" />。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="assemblyFile" /> 不是有效程序集。</exception>
    public AssemblyName GetAssemblyName(string assemblyFile)
    {
      return AssemblyName.GetAssemblyName(assemblyFile);
    }
  }
}
