// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComTypes.IEnumString
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices.ComTypes
{
  /// <summary>管理 IEnumString 接口的定义。</summary>
  [Guid("00000101-0000-0000-C000-000000000046")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [__DynamicallyInvokable]
  [ComImport]
  public interface IEnumString
  {
    /// <summary>检索枚举序列中指定数目的项。</summary>
    /// <returns>如果 <paramref name="pceltFetched" /> 参数与 <paramref name="celt" /> 参数相等，则为 S_OK；否则为 S_FALSE。</returns>
    /// <param name="celt">要在 <paramref name="rgelt" /> 中返回的字符串的数目。</param>
    /// <param name="rgelt">此方法返回时，包含对枚举字符串的引用。该参数未经初始化即被传递。</param>
    /// <param name="pceltFetched">此方法返回时，包含对在 <paramref name="rgelt" /> 中枚举的字符串的实际数目的引用。</param>
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Next(int celt, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr), Out] string[] rgelt, IntPtr pceltFetched);

    /// <summary>跳过枚举序列中指定数目的项。</summary>
    /// <returns>如果跳过的元素数目与 <paramref name="celt" /> 参数相等，则为 S_OK；否则为 S_FALSE。</returns>
    /// <param name="celt">枚举中要跳过的元素数目。</param>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Skip(int celt);

    /// <summary>将枚举序列重置到开始处。</summary>
    [__DynamicallyInvokable]
    void Reset();

    /// <summary>创建与当前枚举数包含相同枚举状态的一个新枚举数。</summary>
    /// <param name="ppenum">此方法返回时，包含对该新创建的枚举数的引用。该参数未经初始化即被传递。</param>
    [__DynamicallyInvokable]
    void Clone(out IEnumString ppenum);
  }
}
