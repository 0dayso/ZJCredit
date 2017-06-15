// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.DllImportAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;
using System.Security;

namespace System.Runtime.InteropServices
{
  /// <summary>指示该属性化方法由非托管动态链接库 (DLL) 作为静态入口点公开。</summary>
  [AttributeUsage(AttributeTargets.Method, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class DllImportAttribute : Attribute
  {
    internal string _val;
    /// <summary>指示要调用的 DLL 入口点的名称或序号。</summary>
    [__DynamicallyInvokable]
    public string EntryPoint;
    /// <summary>指示如何向方法封送字符串参数，并控制名称重整。</summary>
    [__DynamicallyInvokable]
    public CharSet CharSet;
    /// <summary>指示被调用方在从属性化方法返回之前是否调用 SetLastError Win32 API 函数。</summary>
    [__DynamicallyInvokable]
    public bool SetLastError;
    /// <summary>控制 <see cref="F:System.Runtime.InteropServices.DllImportAttribute.CharSet" /> 字段是否使公共语言运行时在非托管 DLL 中搜索入口点名称，而不使用指定的入口点名称。</summary>
    [__DynamicallyInvokable]
    public bool ExactSpelling;
    /// <summary>指示是否直接转换具有 HRESULT 或 retval 返回值的非托管方法，或是否自动将 HRESULT 或 retval 返回值转换为异常。</summary>
    [__DynamicallyInvokable]
    public bool PreserveSig;
    /// <summary>指示入口点的调用约定。</summary>
    [__DynamicallyInvokable]
    public CallingConvention CallingConvention;
    /// <summary>将 Unicode 字符转换为 ANSI 字符时，启用或禁用最佳映射行为。</summary>
    [__DynamicallyInvokable]
    public bool BestFitMapping;
    /// <summary>启用或禁用在遇到已被转换为 ANSI“?”字符的无法映射的 Unicode 字符时引发异常。</summary>
    [__DynamicallyInvokable]
    public bool ThrowOnUnmappableChar;

    /// <summary>获取包含入口点的 DLL 文件的名称。</summary>
    /// <returns>包含入口点的 DLL 文件的名称。</returns>
    [__DynamicallyInvokable]
    public string Value
    {
      [__DynamicallyInvokable] get
      {
        return this._val;
      }
    }

    internal DllImportAttribute(string dllName, string entryPoint, CharSet charSet, bool exactSpelling, bool setLastError, bool preserveSig, CallingConvention callingConvention, bool bestFitMapping, bool throwOnUnmappableChar)
    {
      this._val = dllName;
      this.EntryPoint = entryPoint;
      this.CharSet = charSet;
      this.ExactSpelling = exactSpelling;
      this.SetLastError = setLastError;
      this.PreserveSig = preserveSig;
      this.CallingConvention = callingConvention;
      this.BestFitMapping = bestFitMapping;
      this.ThrowOnUnmappableChar = throwOnUnmappableChar;
    }

    /// <summary>使用包含要导入的方法的 DLL 的名称初始化 <see cref="T:System.Runtime.InteropServices.DllImportAttribute" /> 类的新实例。</summary>
    /// <param name="dllName">包含非托管方法的 DLL 的名称。如果 DLL 包含在某个程序集中，则可以包含程序集显示名称。</param>
    [__DynamicallyInvokable]
    public DllImportAttribute(string dllName)
    {
      this._val = dllName;
    }

    [SecurityCritical]
    internal static Attribute GetCustomAttribute(RuntimeMethodInfo method)
    {
      if ((method.Attributes & MethodAttributes.PinvokeImpl) == MethodAttributes.PrivateScope)
        return (Attribute) null;
      MetadataImport metadataImport = ModuleHandle.GetMetadataImport(method.Module.ModuleHandle.GetRuntimeModule());
      string importDll = (string) null;
      int metadataToken = method.MetadataToken;
      PInvokeAttributes attributes = PInvokeAttributes.CharSetNotSpec;
      string importName;
      metadataImport.GetPInvokeMap(metadataToken, out attributes, out importName, out importDll);
      CharSet charSet = CharSet.None;
      switch (attributes & PInvokeAttributes.CharSetMask)
      {
        case PInvokeAttributes.CharSetNotSpec:
          charSet = CharSet.None;
          break;
        case PInvokeAttributes.CharSetAnsi:
          charSet = CharSet.Ansi;
          break;
        case PInvokeAttributes.CharSetUnicode:
          charSet = CharSet.Unicode;
          break;
        case PInvokeAttributes.CharSetMask:
          charSet = CharSet.Auto;
          break;
      }
      CallingConvention callingConvention = CallingConvention.Cdecl;
      switch (attributes & PInvokeAttributes.CallConvMask)
      {
        case PInvokeAttributes.CallConvStdcall:
          callingConvention = CallingConvention.StdCall;
          break;
        case PInvokeAttributes.CallConvThiscall:
          callingConvention = CallingConvention.ThisCall;
          break;
        case PInvokeAttributes.CallConvFastcall:
          callingConvention = CallingConvention.FastCall;
          break;
        case PInvokeAttributes.CallConvWinapi:
          callingConvention = CallingConvention.Winapi;
          break;
        case PInvokeAttributes.CallConvCdecl:
          callingConvention = CallingConvention.Cdecl;
          break;
      }
      bool exactSpelling = (uint) (attributes & PInvokeAttributes.NoMangle) > 0U;
      bool setLastError = (uint) (attributes & PInvokeAttributes.SupportsLastError) > 0U;
      bool bestFitMapping = (attributes & PInvokeAttributes.BestFitMask) == PInvokeAttributes.BestFitEnabled;
      bool throwOnUnmappableChar = (attributes & PInvokeAttributes.ThrowOnUnmappableCharMask) == PInvokeAttributes.ThrowOnUnmappableCharEnabled;
      bool preserveSig = (uint) (method.GetMethodImplementationFlags() & MethodImplAttributes.PreserveSig) > 0U;
      return (Attribute) new DllImportAttribute(importDll, importName, charSet, exactSpelling, setLastError, preserveSig, callingConvention, bestFitMapping, throwOnUnmappableChar);
    }

    internal static bool IsDefined(RuntimeMethodInfo method)
    {
      return (uint) (method.Attributes & MethodAttributes.PinvokeImpl) > 0U;
    }
  }
}
