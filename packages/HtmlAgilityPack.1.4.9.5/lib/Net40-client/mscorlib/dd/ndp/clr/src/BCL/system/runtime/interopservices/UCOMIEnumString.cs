// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.UCOMIEnumString
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices
{
  /// <summary>请改用 <see cref="T:System.Runtime.InteropServices.ComTypes.IEnumString" />。</summary>
  [Obsolete("Use System.Runtime.InteropServices.ComTypes.IEnumString instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
  [Guid("00000101-0000-0000-C000-000000000046")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  public interface UCOMIEnumString
  {
    /// <summary>检索枚举序列中指定数目的项。</summary>
    /// <returns>如果 <paramref name="pceltFetched" /> 参数与 <paramref name="celt" /> 参数相等，则为 S_OK；否则为 S_FALSE。</returns>
    /// <param name="celt">要在 <paramref name="rgelt" /> 中返回的字符串的数目。</param>
    /// <param name="rgelt">成功返回时，是对枚举的字符串的引用。</param>
    /// <param name="pceltFetched">成功返回时，是对在 <paramref name="rgelt" /> 中枚举的字符串的实际数目的引用。</param>
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Next(int celt, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr), Out] string[] rgelt, out int pceltFetched);

    /// <summary>跳过枚举序列中指定数目的项。</summary>
    /// <returns>如果跳过的元素数目与 <paramref name="celt" /> 参数相等，则为 S_OK；否则为 S_FALSE。</returns>
    /// <param name="celt">枚举中要跳过的元素数目。</param>
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Skip(int celt);

    /// <summary>将枚举序列重置到开始处。</summary>
    /// <returns>具有值 S_OK 的 HRESULT。</returns>
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Reset();

    /// <summary>创建另一个枚举数，它包含与当前枚举数相同的枚举状态。</summary>
    /// <param name="ppenum">成功返回时，是对新创建的枚举数的引用。</param>
    void Clone(out UCOMIEnumString ppenum);
  }
}
