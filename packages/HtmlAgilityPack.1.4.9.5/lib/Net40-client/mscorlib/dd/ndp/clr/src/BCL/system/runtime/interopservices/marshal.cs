// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.Marshal
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices.ComTypes;
using System.Security;
using System.Threading;

namespace System.Runtime.InteropServices
{
  /// <summary>提供了一个方法集合，这些方法用于分配非托管内存、复制非托管内存块、将托管类型转换为非托管类型，此外还提供了在与非托管代码交互时使用的其他杂项方法。</summary>
  [__DynamicallyInvokable]
  public static class Marshal
  {
    private static Guid IID_IUnknown = new Guid("00000000-0000-0000-C000-000000000046");
    /// <summary>表示系统上的默认字符大小；Unicode 系统上默认值为 2，ANSI 系统上默认值为 1。此字段为只读。</summary>
    public static readonly int SystemDefaultCharSize = 2;
    /// <summary>表示用于当前操作系统的双字节字符集 (DBCS) 的最大大小（以字节为单位）。此字段为只读。</summary>
    public static readonly int SystemMaxDBCSCharSize = Marshal.GetSystemMaxDBCSCharSize();
    internal static readonly Guid ManagedNameGuid = new Guid("{0F21F359-AB84-41E8-9A78-36D110E6D2F9}");
    private const int LMEM_FIXED = 0;
    private const int LMEM_MOVEABLE = 2;
    private const long HIWORDMASK = -65536;
    private const string s_strConvertedTypeInfoAssemblyName = "InteropDynamicTypes";
    private const string s_strConvertedTypeInfoAssemblyTitle = "Interop Dynamic Types";
    private const string s_strConvertedTypeInfoAssemblyDesc = "Type dynamically generated from ITypeInfo's";
    private const string s_strConvertedTypeInfoNameSpace = "InteropDynamicTypes";

    private static bool IsWin32Atom(IntPtr ptr)
    {
      return ((long) ptr & -65536L) == 0L;
    }

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    private static bool IsNotWin32Atom(IntPtr ptr)
    {
      return ((ulong) (long) ptr & 18446744073709486080UL) > 0UL;
    }

    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern int GetSystemMaxDBCSCharSize();

    /// <summary>将非托管 ANSI 字符串中第一个 null 字符之前的所有字符复制到托管 <see cref="T:System.String" />，并将每个 ANSI 字符扩展为 Unicode 字符。</summary>
    /// <returns>包含非托管 ANSI 字符串的副本的托管字符串。如果 <paramref name="ptr" /> 为 null，则该方法返回空字符串。</returns>
    /// <param name="ptr">非托管字符串的第一个字符的地址。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static unsafe string PtrToStringAnsi(IntPtr ptr)
    {
      if (IntPtr.Zero == ptr)
        return (string) null;
      if (Marshal.IsWin32Atom(ptr))
        return (string) null;
      if (Win32Native.lstrlenA(ptr) == 0)
        return string.Empty;
      return new string((sbyte*) (void*) ptr);
    }

    /// <summary>分配托管 <see cref="T:System.String" />，然后从非托管 ANSI 字符串向其复制指定数目的字符，并将每个 ANSI 字符扩展为 Unicode 字符。</summary>
    /// <returns>如果 <paramref name="ptr" /> 参数的值不是 null，则为包含本机 ANSI 字符串副本的托管字符串；否则，此方法返回 null。</returns>
    /// <param name="ptr">非托管字符串的第一个字符的地址。</param>
    /// <param name="len">要复制的输入字符串的字节数。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="len" /> 小于零。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static unsafe string PtrToStringAnsi(IntPtr ptr, int len)
    {
      if (ptr == IntPtr.Zero)
        throw new ArgumentNullException("ptr");
      if (len < 0)
        throw new ArgumentException("len");
      return new string((sbyte*) (void*) ptr, 0, len);
    }

    /// <summary>分配托管 <see cref="T:System.String" />，并从非托管 Unicode 字符串向其复制指定数目的字符。</summary>
    /// <returns>如果 <paramref name="ptr" /> 参数的值不是 null，则为包含非托管字符串副本的托管字符串；否则，此方法返回 null。</returns>
    /// <param name="ptr">非托管字符串的第一个字符的地址。</param>
    /// <param name="len">要复制的 Unicode 字符数。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static unsafe string PtrToStringUni(IntPtr ptr, int len)
    {
      if (ptr == IntPtr.Zero)
        throw new ArgumentNullException("ptr");
      if (len < 0)
        throw new ArgumentException("len");
      return new string((char*) (void*) ptr, 0, len);
    }

    /// <summary>分配托管 <see cref="T:System.String" />，并从存储在非托管内存中的字符串向其复制指定数目的字符。</summary>
    /// <returns>如果 <paramref name="ptr" /> 参数的值不是 null，则为包含本机字符串副本的托管字符串；否则，此方法返回 null。</returns>
    /// <param name="ptr">对于 Unicode 平台，表示第一个 Unicode 字符的地址。- 或 -对于 ANSI 平台，表示第一个 ANSI 字符的地址。</param>
    /// <param name="len">要复制的字符数。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="len" /> 小于零。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static string PtrToStringAuto(IntPtr ptr, int len)
    {
      return Marshal.PtrToStringUni(ptr, len);
    }

    /// <summary>分配托管 <see cref="T:System.String" />，并从非托管 Unicode 字符串向其复制第一个空字符之前的所有字符。</summary>
    /// <returns>如果 <paramref name="ptr" /> 参数的值不是 null，则为包含非托管字符串副本的托管字符串；否则，此方法返回 null。</returns>
    /// <param name="ptr">非托管字符串的第一个字符的地址。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static unsafe string PtrToStringUni(IntPtr ptr)
    {
      if (IntPtr.Zero == ptr)
        return (string) null;
      if (Marshal.IsWin32Atom(ptr))
        return (string) null;
      return new string((char*) (void*) ptr);
    }

    /// <summary>分配托管 <see cref="T:System.String" />，并从非托管内存中存储的字符串向其复制第一个空字符之前的所有字符。</summary>
    /// <returns>如果 <paramref name="ptr" /> 参数的值不是 null，则为包含非托管字符串副本的托管字符串；否则，此方法返回 null。</returns>
    /// <param name="ptr">对于 Unicode 平台，表示第一个 Unicode 字符的地址。- 或 -对于 ANSI 平台，表示第一个 ANSI 字符的地址。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static string PtrToStringAuto(IntPtr ptr)
    {
      return Marshal.PtrToStringUni(ptr);
    }

    /// <summary>返回对象的非托管大小（以字节为单位）。</summary>
    /// <returns>非托管代码中指定对象的大小。</returns>
    /// <param name="structure">要返回其大小的对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="structure" /> 参数为 null。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [ComVisible(true)]
    public static int SizeOf(object structure)
    {
      if (structure == null)
        throw new ArgumentNullException("structure");
      return Marshal.SizeOfHelper(structure.GetType(), true);
    }

    /// <summary>[在 .NET Framework 4.5.1 和更高版本中受支持]返回指定类型的对象的非托管大小（以字节为单位）。</summary>
    /// <returns>非托管代码中指定对象的大小（以字节为单位）。</returns>
    /// <param name="structure">要返回其大小的对象。</param>
    /// <typeparam name="T">
    /// <paramref name="structure" /> 参数的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="structure" /> 参数为 null。</exception>
    public static int SizeOf<T>(T structure)
    {
      return Marshal.SizeOf((object) structure);
    }

    /// <summary>返回非托管类型的大小（以字节为单位）。</summary>
    /// <returns>非托管代码中指定类型的大小。</returns>
    /// <param name="t">要返回其大小的类型。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="t" /> 参数是泛型类型。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="t" /> 参数为 null。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public static int SizeOf(Type t)
    {
      if (t == (Type) null)
        throw new ArgumentNullException("t");
      if (!(t is RuntimeType))
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"), "t");
      if (t.IsGenericType)
        throw new ArgumentException(Environment.GetResourceString("Argument_NeedNonGenericType"), "t");
      return Marshal.SizeOfHelper(t, true);
    }

    /// <summary>[在 .NET Framework 4.5.1 和更高版本中受支持]返回非托管类型的大小（以字节为单位）。</summary>
    /// <returns>
    /// <paramref name="T" /> 泛型类型参数指定的类型的大小（以字节为单位）。</returns>
    /// <typeparam name="T">要返回其大小的类型。</typeparam>
    public static int SizeOf<T>()
    {
      return Marshal.SizeOf(typeof (T));
    }

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    internal static uint AlignedSizeOf<T>() where T : struct
    {
      uint num = Marshal.SizeOfType(typeof (T));
      switch (num)
      {
        case 1:
        case 2:
          return num;
        default:
          if (IntPtr.Size == 8 && (int) num == 4)
            return num;
          return Marshal.AlignedSizeOfType(typeof (T));
      }
    }

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern uint SizeOfType(Type type);

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern uint AlignedSizeOfType(Type type);

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern int SizeOfHelper(Type t, bool throwIfNotMarshalable);

    /// <summary>返回托管类的非托管形式的字段偏移量。</summary>
    /// <returns>平台调用声明的指定类中 <paramref name="fieldName" /> 参数的偏移量（以字节为单位）。</returns>
    /// <param name="t">指定托管类的值类型或格式化引用类型。必须将 <see cref="T:System.Runtime.InteropServices.StructLayoutAttribute" /> 应用于该类。</param>
    /// <param name="fieldName">
    /// <paramref name="t" /> 参数中的字段。</param>
    /// <exception cref="T:System.ArgumentException">该类无法作为结构导出，或者字段为非公共字段。从 .NET Framework 2.0 版开始，该字段可以是私有的。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="t" /> 参数为 null。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public static IntPtr OffsetOf(Type t, string fieldName)
    {
      if (t == (Type) null)
        throw new ArgumentNullException("t");
      FieldInfo field = t.GetField(fieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
      if (field == (FieldInfo) null)
        throw new ArgumentException(Environment.GetResourceString("Argument_OffsetOfFieldNotFound", (object) t.FullName), "fieldName");
      RtFieldInfo rtFieldInfo = field as RtFieldInfo;
      // ISSUE: variable of the null type
      __Null local = null;
      if ((FieldInfo) rtFieldInfo == (FieldInfo) local)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeFieldInfo"), "fieldName");
      return Marshal.OffsetOfHelper((IRuntimeFieldInfo) rtFieldInfo);
    }

    /// <summary>[在 .NET Framework 4.5.1 和更高版本中受支持]返回指定托管类的非托管形式的字段偏移量。</summary>
    /// <returns>平台调用声明的指定类中 <paramref name="fieldName" /> 参数的偏移量（以字节为单位）。 </returns>
    /// <param name="fieldName">
    /// <paramref name="T" /> 类型中字段的名称。</param>
    /// <typeparam name="T">托管值类型或格式化引用类型。必须将 <see cref="T:System.Runtime.InteropServices.StructLayoutAttribute" /> 特性应用于该类。</typeparam>
    public static IntPtr OffsetOf<T>(string fieldName)
    {
      return Marshal.OffsetOf(typeof (T), fieldName);
    }

    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern IntPtr OffsetOfHelper(IRuntimeFieldInfo f);

    /// <summary>获取指定数组中指定索引处的元素的地址。</summary>
    /// <returns>
    /// <paramref name="arr" /> 内的 <paramref name="index" /> 的地址。</returns>
    /// <param name="arr">包含所需元素的数组。</param>
    /// <param name="index">所需元素的 <paramref name="arr" /> 参数中的索引。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern IntPtr UnsafeAddrOfPinnedArrayElement(Array arr, int index);

    /// <summary>[在 .NET Framework 4.5.1 和更高版本中受支持]获取指定类型的数组中指定索引处的元素的地址。</summary>
    /// <returns>
    /// <paramref name="arr" /> 中的 <paramref name="index" /> 的地址。</returns>
    /// <param name="arr">包含所需元素的数组。</param>
    /// <param name="index">
    /// <paramref name="arr" /> 数组中所需元素的索引。</param>
    /// <typeparam name="T">数组类型。</typeparam>
    [SecurityCritical]
    public static IntPtr UnsafeAddrOfPinnedArrayElement<T>(T[] arr, int index)
    {
      return Marshal.UnsafeAddrOfPinnedArrayElement((Array) arr, index);
    }

    /// <summary>将数据从一维托管 32 位带符号整数数组复制到非托管内存指针。</summary>
    /// <param name="source">从中进行复制的一维数组。</param>
    /// <param name="startIndex">源数组中从零开始的索引，在此处开始复制。</param>
    /// <param name="destination">要复制到的内存指针。</param>
    /// <param name="length">要复制的数组元素的数目。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="startIndex" /> 和 <paramref name="length" /> 无效。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="startIndex" /> 或 <paramref name="length" /> 为 null。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static void Copy(int[] source, int startIndex, IntPtr destination, int length)
    {
      Marshal.CopyToNative((object) source, startIndex, destination, length);
    }

    /// <summary>将数据从一维托管字符数组复制到非托管内存指针。</summary>
    /// <param name="source">从中进行复制的一维数组。</param>
    /// <param name="startIndex">源数组中从零开始的索引，在此处开始复制。</param>
    /// <param name="destination">要复制到的内存指针。</param>
    /// <param name="length">要复制的数组元素的数目。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="startIndex" /> 和 <paramref name="length" /> 无效。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="startIndex" />、<paramref name="destination" /> 或 <paramref name="length" /> 为 null。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static void Copy(char[] source, int startIndex, IntPtr destination, int length)
    {
      Marshal.CopyToNative((object) source, startIndex, destination, length);
    }

    /// <summary>将数据从一维托管 16 位带符号整数数组复制到非托管内存指针。</summary>
    /// <param name="source">从中进行复制的一维数组。</param>
    /// <param name="startIndex">源数组中从零开始的索引，在此处开始复制。</param>
    /// <param name="destination">要复制到的内存指针。</param>
    /// <param name="length">要复制的数组元素的数目。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="startIndex" /> 和 <paramref name="length" /> 无效。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="source" />、<paramref name="startIndex" />、<paramref name="destination" /> 或 <paramref name="length" /> 为 null。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static void Copy(short[] source, int startIndex, IntPtr destination, int length)
    {
      Marshal.CopyToNative((object) source, startIndex, destination, length);
    }

    /// <summary>将数据从一维托管 64 位带符号整数数组复制到非托管内存指针。</summary>
    /// <param name="source">从中进行复制的一维数组。</param>
    /// <param name="startIndex">源数组中从零开始的索引，在此处开始复制。</param>
    /// <param name="destination">要复制到的内存指针。</param>
    /// <param name="length">要复制的数组元素的数目。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="startIndex" /> 和 <paramref name="length" /> 无效。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="source" />、<paramref name="startIndex" />、<paramref name="destination" /> 或 <paramref name="length" /> 为 null。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static void Copy(long[] source, int startIndex, IntPtr destination, int length)
    {
      Marshal.CopyToNative((object) source, startIndex, destination, length);
    }

    /// <summary>将数据从一维托管单精度浮点数数组复制到非托管内存指针。</summary>
    /// <param name="source">从中进行复制的一维数组。</param>
    /// <param name="startIndex">源数组中从零开始的索引，在此处开始复制。</param>
    /// <param name="destination">要复制到的内存指针。</param>
    /// <param name="length">要复制的数组元素的数目。 </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="startIndex" /> 和 <paramref name="length" /> 无效。 </exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="source" />、<paramref name="startIndex" />、<paramref name="destination" /> 或 <paramref name="length" /> 为 null。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static void Copy(float[] source, int startIndex, IntPtr destination, int length)
    {
      Marshal.CopyToNative((object) source, startIndex, destination, length);
    }

    /// <summary>将数据从一维托管双精度浮点数数组复制到非托管内存指针。</summary>
    /// <param name="source">从中进行复制的一维数组。</param>
    /// <param name="startIndex">源数组中从零开始的索引，在此处开始复制。</param>
    /// <param name="destination">要复制到的内存指针。</param>
    /// <param name="length">要复制的数组元素的数目。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="startIndex" /> 和 <paramref name="length" /> 无效。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="source" />、<paramref name="startIndex" />、<paramref name="destination" /> 或 <paramref name="length" /> 为 null。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static void Copy(double[] source, int startIndex, IntPtr destination, int length)
    {
      Marshal.CopyToNative((object) source, startIndex, destination, length);
    }

    /// <summary>将数据从一维托管 8 位无符号整数数组复制到非托管内存指针。</summary>
    /// <param name="source">从中进行复制的一维数组。</param>
    /// <param name="startIndex">源数组中从零开始的索引，在此处开始复制。</param>
    /// <param name="destination">要复制到的内存指针。</param>
    /// <param name="length">要复制的数组元素的数目。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="startIndex" /> 和 <paramref name="length" /> 无效。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="source" />、<paramref name="startIndex" />、<paramref name="destination" /> 或 <paramref name="length" /> 为 null。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static void Copy(byte[] source, int startIndex, IntPtr destination, int length)
    {
      Marshal.CopyToNative((object) source, startIndex, destination, length);
    }

    /// <summary>将数据从一维托管 <see cref="T:System.IntPtr" /> 数组复制到非托管内存指针。</summary>
    /// <param name="source">从中进行复制的一维数组。</param>
    /// <param name="startIndex">源数组中从零开始的索引，在此处开始复制。</param>
    /// <param name="destination">要复制到的内存指针。</param>
    /// <param name="length">要复制的数组元素的数目。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="source" />、<paramref name="destination" />、<paramref name="startIndex" /> 或 <paramref name="length" /> 为 null。</exception>
    [SecurityCritical]
    public static void Copy(IntPtr[] source, int startIndex, IntPtr destination, int length)
    {
      Marshal.CopyToNative((object) source, startIndex, destination, length);
    }

    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void CopyToNative(object source, int startIndex, IntPtr destination, int length);

    /// <summary>将数据从非托管内存指针复制到托管 32 位带符号整数数组。</summary>
    /// <param name="source">从中进行复制的内存指针。</param>
    /// <param name="destination">要复制到的数组。</param>
    /// <param name="startIndex">目标数组中从零开始的索引，在此处开始复制。</param>
    /// <param name="length">要复制的数组元素的数目。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="source" />、<paramref name="destination" />、<paramref name="startIndex" /> 或 <paramref name="length" /> 为 null。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static void Copy(IntPtr source, int[] destination, int startIndex, int length)
    {
      Marshal.CopyToManaged(source, (object) destination, startIndex, length);
    }

    /// <summary>将数据从非托管内存指针复制到托管字符数组。</summary>
    /// <param name="source">从中进行复制的内存指针。</param>
    /// <param name="destination">要复制到的数组。</param>
    /// <param name="startIndex">目标数组中从零开始的索引，在此处开始复制。</param>
    /// <param name="length">要复制的数组元素的数目。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="source" />、<paramref name="destination" />、<paramref name="startIndex" /> 或 <paramref name="length" /> 为 null。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static void Copy(IntPtr source, char[] destination, int startIndex, int length)
    {
      Marshal.CopyToManaged(source, (object) destination, startIndex, length);
    }

    /// <summary>将数据从非托管内存指针复制到托管 16 位带符号整数数组。</summary>
    /// <param name="source">从中进行复制的内存指针。</param>
    /// <param name="destination">要复制到的数组。</param>
    /// <param name="startIndex">目标数组中从零开始的索引，在此处开始复制。</param>
    /// <param name="length">要复制的数组元素的数目。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="source" />、<paramref name="destination" />、<paramref name="startIndex" /> 或 <paramref name="length" /> 为 null。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static void Copy(IntPtr source, short[] destination, int startIndex, int length)
    {
      Marshal.CopyToManaged(source, (object) destination, startIndex, length);
    }

    /// <summary>将数据从非托管内存指针复制到托管 64 位带符号整数数组。</summary>
    /// <param name="source">从中进行复制的内存指针。</param>
    /// <param name="destination">要复制到的数组。</param>
    /// <param name="startIndex">目标数组中从零开始的索引，在此处开始复制。</param>
    /// <param name="length">要复制的数组元素的数目。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="source" />、<paramref name="destination" />、<paramref name="startIndex" /> 或 <paramref name="length" /> 为 null。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static void Copy(IntPtr source, long[] destination, int startIndex, int length)
    {
      Marshal.CopyToManaged(source, (object) destination, startIndex, length);
    }

    /// <summary>将数据从非托管内存指针复制到托管单精度浮点数数组。</summary>
    /// <param name="source">从中进行复制的内存指针。</param>
    /// <param name="destination">要复制到的数组。</param>
    /// <param name="startIndex">目标数组中从零开始的索引，在此处开始复制。</param>
    /// <param name="length">要复制的数组元素的数目。 </param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="source" />、<paramref name="destination" />、<paramref name="startIndex" /> 或 <paramref name="length" /> 为 null。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static void Copy(IntPtr source, float[] destination, int startIndex, int length)
    {
      Marshal.CopyToManaged(source, (object) destination, startIndex, length);
    }

    /// <summary>将数据从非托管内存指针复制到托管双精度浮点数数组。</summary>
    /// <param name="source">从中进行复制的内存指针。</param>
    /// <param name="destination">要复制到的数组。</param>
    /// <param name="startIndex">目标数组中从零开始的索引，在此处开始复制。</param>
    /// <param name="length">要复制的数组元素的数目。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="source" />、<paramref name="destination" />、<paramref name="startIndex" /> 或 <paramref name="length" /> 为 null。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static void Copy(IntPtr source, double[] destination, int startIndex, int length)
    {
      Marshal.CopyToManaged(source, (object) destination, startIndex, length);
    }

    /// <summary>将数据从非托管内存指针复制到托管 8 位无符号整数数组。</summary>
    /// <param name="source">从中进行复制的内存指针。</param>
    /// <param name="destination">要复制到的数组。</param>
    /// <param name="startIndex">目标数组中从零开始的索引，在此处开始复制。</param>
    /// <param name="length">要复制的数组元素的数目。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="source" />、<paramref name="destination" />、<paramref name="startIndex" /> 或 <paramref name="length" /> 为 null。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static void Copy(IntPtr source, byte[] destination, int startIndex, int length)
    {
      Marshal.CopyToManaged(source, (object) destination, startIndex, length);
    }

    /// <summary>将数据从非托管内存指针复制到托管 <see cref="T:System.IntPtr" /> 数组。</summary>
    /// <param name="source">从中进行复制的内存指针。</param>
    /// <param name="destination">要复制到的数组。</param>
    /// <param name="startIndex">目标数组中从零开始的索引，在此处开始复制。</param>
    /// <param name="length">要复制的数组元素的数目。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="source" />、<paramref name="destination" />、<paramref name="startIndex" /> 或 <paramref name="length" /> 为 null。</exception>
    [SecurityCritical]
    public static void Copy(IntPtr source, IntPtr[] destination, int startIndex, int length)
    {
      Marshal.CopyToManaged(source, (object) destination, startIndex, length);
    }

    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void CopyToManaged(IntPtr source, object destination, int startIndex, int length);

    /// <summary>从非托管内存按给定的偏移量（或索引）读取单个字节。</summary>
    /// <returns>从非托管内存按给定的偏移量读取的字节。</returns>
    /// <param name="ptr">非托管内存中源对象的基址。</param>
    /// <param name="ofs">额外的字节偏移量，在读取前添加到 <paramref name="ptr" /> 参数中。</param>
    /// <exception cref="T:System.AccessViolationException">基址 (<paramref name="ptr" />) 加上偏移字节(<paramref name="ofs" />) 可产生空或无效地址。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="ptr" /> 是 <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> 对象。此方法不接受 <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> 参数。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SuppressUnmanagedCodeSecurity]
    [SecurityCritical]
    [DllImport("mscoree.dll", EntryPoint = "ND_RU1")]
    public static extern byte ReadByte([MarshalAs(UnmanagedType.AsAny), In] object ptr, int ofs);

    /// <summary>从非托管内存按给定的偏移量（或索引）读取单个字节。</summary>
    /// <returns>从非托管内存按给定的偏移量读取的字节。</returns>
    /// <param name="ptr">非托管内存中开始读取的基址。</param>
    /// <param name="ofs">额外的字节偏移量，在读取前添加到 <paramref name="ptr" /> 参数中。</param>
    /// <exception cref="T:System.AccessViolationException">基址 (<paramref name="ptr" />) 加上偏移字节(<paramref name="ofs" />) 可产生空或无效地址。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static unsafe byte ReadByte(IntPtr ptr, int ofs)
    {
      try
      {
        return *(byte*) ((IntPtr) (void*) ptr + ofs);
      }
      catch (NullReferenceException ex)
      {
        throw new AccessViolationException();
      }
    }

    /// <summary>从非托管内存读取单个字节。</summary>
    /// <returns>从非托管内存读取的字节。</returns>
    /// <param name="ptr">非托管内存中开始读取的地址。</param>
    /// <exception cref="T:System.AccessViolationException">
    /// <paramref name="ptr" /> 不是识别的格式。- 或 -<paramref name="ptr" /> 为 null。- 或 -<paramref name="ptr" /> 无效。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static byte ReadByte(IntPtr ptr)
    {
      return Marshal.ReadByte(ptr, 0);
    }

    /// <summary>从非托管内存按给定的偏移量读取一个 16 位带符号整数。</summary>
    /// <returns>从非托管内存按给定的偏移量读取的 16 位带符号整数。</returns>
    /// <param name="ptr">非托管内存中源对象的基址。</param>
    /// <param name="ofs">额外的字节偏移量，在读取前添加到 <paramref name="ptr" /> 参数中。</param>
    /// <exception cref="T:System.AccessViolationException">基址 (<paramref name="ptr" />) 加上偏移字节(<paramref name="ofs" />) 可产生空或无效地址。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="ptr" /> 是 <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> 对象。此方法不接受 <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> 参数。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SuppressUnmanagedCodeSecurity]
    [SecurityCritical]
    [DllImport("mscoree.dll", EntryPoint = "ND_RI2")]
    public static extern short ReadInt16([MarshalAs(UnmanagedType.AsAny), In] object ptr, int ofs);

    /// <summary>从非托管内存按给定的偏移量读取一个 16 位带符号整数。</summary>
    /// <returns>从非托管内存按给定的偏移量读取的 16 位带符号整数。</returns>
    /// <param name="ptr">非托管内存中开始读取的基址。</param>
    /// <param name="ofs">额外的字节偏移量，在读取前添加到 <paramref name="ptr" /> 参数中。</param>
    /// <exception cref="T:System.AccessViolationException">基址 (<paramref name="ptr" />) 加上偏移字节(<paramref name="ofs" />) 可产生空或无效地址。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static unsafe short ReadInt16(IntPtr ptr, int ofs)
    {
      try
      {
        byte* numPtr1 = (byte*) ((IntPtr) (void*) ptr + ofs);
        if (((int) numPtr1 & 1) == 0)
          return *(short*) numPtr1;
        short num;
        byte* numPtr2 = (byte*) &num;
        *numPtr2 = *numPtr1;
        numPtr2[1] = numPtr1[1];
        return num;
      }
      catch (NullReferenceException ex)
      {
        throw new AccessViolationException();
      }
    }

    /// <summary>从非托管内存中读取一个 16 位带符号整数。</summary>
    /// <returns>从非托管内存中读取的 16 位带符号整数。</returns>
    /// <param name="ptr">非托管内存中开始读取的地址。</param>
    /// <exception cref="T:System.AccessViolationException">
    /// <paramref name="ptr" /> 不是识别的格式。- 或 -<paramref name="ptr" /> 为 null。- 或 -<paramref name="ptr" /> 无效。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static short ReadInt16(IntPtr ptr)
    {
      return Marshal.ReadInt16(ptr, 0);
    }

    /// <summary>从非托管内存按给定的偏移量读取一个 32 位带符号整数。</summary>
    /// <returns>从非托管内存按给定的偏移量读取的 32 位带符号整数。</returns>
    /// <param name="ptr">非托管内存中源对象的基址。</param>
    /// <param name="ofs">额外的字节偏移量，在读取前添加到 <paramref name="ptr" /> 参数中。</param>
    /// <exception cref="T:System.AccessViolationException">基址 (<paramref name="ptr" />) 加上偏移字节(<paramref name="ofs" />) 可产生空或无效地址。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="ptr" /> 是 <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> 对象。此方法不接受 <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> 参数。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [SuppressUnmanagedCodeSecurity]
    [SecurityCritical]
    [DllImport("mscoree.dll", EntryPoint = "ND_RI4")]
    public static extern int ReadInt32([MarshalAs(UnmanagedType.AsAny), In] object ptr, int ofs);

    /// <summary>从非托管内存按给定的偏移量读取一个 32 位带符号整数。</summary>
    /// <returns>从非托管内存中读取的 32 位带符号整数。</returns>
    /// <param name="ptr">非托管内存中开始读取的基址。</param>
    /// <param name="ofs">额外的字节偏移量，在读取前添加到 <paramref name="ptr" /> 参数中。</param>
    /// <exception cref="T:System.AccessViolationException">基址 (<paramref name="ptr" />) 加上偏移字节(<paramref name="ofs" />) 可产生空或无效地址。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    public static unsafe int ReadInt32(IntPtr ptr, int ofs)
    {
      try
      {
        byte* numPtr1 = (byte*) ((IntPtr) (void*) ptr + ofs);
        if (((int) numPtr1 & 3) == 0)
          return *(int*) numPtr1;
        int num;
        byte* numPtr2 = (byte*) &num;
        *numPtr2 = *numPtr1;
        numPtr2[1] = numPtr1[1];
        numPtr2[2] = numPtr1[2];
        numPtr2[3] = numPtr1[3];
        return num;
      }
      catch (NullReferenceException ex)
      {
        throw new AccessViolationException();
      }
    }

    /// <summary>从非托管内存中读取一个 32 位带符号整数。</summary>
    /// <returns>从非托管内存中读取的 32 位带符号整数。</returns>
    /// <param name="ptr">非托管内存中开始读取的地址。</param>
    /// <exception cref="T:System.AccessViolationException">
    /// <paramref name="ptr" /> 不是识别的格式。- 或 -<paramref name="ptr" /> 为 null。- 或 -<paramref name="ptr" /> 无效。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    public static int ReadInt32(IntPtr ptr)
    {
      return Marshal.ReadInt32(ptr, 0);
    }

    /// <summary>从非托管内存读取处理器本机大小的整数。</summary>
    /// <returns>从非托管内存按给定的偏移量读取的整数。</returns>
    /// <param name="ptr">非托管内存中源对象的基址。</param>
    /// <param name="ofs">额外的字节偏移量，在读取前添加到 <paramref name="ptr" /> 参数中。</param>
    /// <exception cref="T:System.AccessViolationException">基址 (<paramref name="ptr" />) 加上偏移字节(<paramref name="ofs" />) 可产生空或无效地址。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="ptr" /> 是 <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> 对象。此方法不接受 <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> 参数。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    public static IntPtr ReadIntPtr([MarshalAs(UnmanagedType.AsAny), In] object ptr, int ofs)
    {
      return (IntPtr) Marshal.ReadInt32(ptr, ofs);
    }

    /// <summary>从非托管内存按给定的偏移量读取处理器本机大小的整数。</summary>
    /// <returns>从非托管内存按给定的偏移量读取的整数。</returns>
    /// <param name="ptr">非托管内存中开始读取的基址。</param>
    /// <param name="ofs">额外的字节偏移量，在读取前添加到 <paramref name="ptr" /> 参数中。</param>
    /// <exception cref="T:System.AccessViolationException">基址 (<paramref name="ptr" />) 加上偏移字节(<paramref name="ofs" />) 可产生空或无效地址。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    public static IntPtr ReadIntPtr(IntPtr ptr, int ofs)
    {
      return (IntPtr) Marshal.ReadInt32(ptr, ofs);
    }

    /// <summary>从非托管内存读取处理器本机大小的整数。</summary>
    /// <returns>从非托管内存读取的整数。在 32 位计算机上返回 32 位整数，在 64 位计算机上返回 64 位整数。</returns>
    /// <param name="ptr">非托管内存中开始读取的地址。</param>
    /// <exception cref="T:System.AccessViolationException">
    /// <paramref name="ptr" /> 不是识别的格式。- 或 -<paramref name="ptr" /> 为 null。- 或 -<paramref name="ptr" /> 无效。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    public static IntPtr ReadIntPtr(IntPtr ptr)
    {
      return (IntPtr) Marshal.ReadInt32(ptr, 0);
    }

    /// <summary>从非托管内存按给定的偏移量读取一个 64 位带符号整数。</summary>
    /// <returns>从非托管内存按给定的偏移量读取的 64 位带符号整数。</returns>
    /// <param name="ptr">非托管内存中源对象的基址。</param>
    /// <param name="ofs">额外的字节偏移量，在读取前添加到 <paramref name="ptr" /> 参数中。</param>
    /// <exception cref="T:System.AccessViolationException">基址 (<paramref name="ptr" />) 加上偏移字节(<paramref name="ofs" />) 可产生空或无效地址。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="ptr" /> 是 <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> 对象。此方法不接受 <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> 参数。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [SuppressUnmanagedCodeSecurity]
    [SecurityCritical]
    [DllImport("mscoree.dll", EntryPoint = "ND_RI8")]
    public static extern long ReadInt64([MarshalAs(UnmanagedType.AsAny), In] object ptr, int ofs);

    /// <summary>从非托管内存按给定的偏移量读取一个 64 位带符号整数。</summary>
    /// <returns>从非托管内存按给定的偏移量读取的 64 位带符号整数。</returns>
    /// <param name="ptr">非托管内存中开始读取的基址。</param>
    /// <param name="ofs">额外的字节偏移量，在读取前添加到 <paramref name="ptr" /> 参数中。</param>
    /// <exception cref="T:System.AccessViolationException">基址 (<paramref name="ptr" />) 加上偏移字节(<paramref name="ofs" />) 可产生空或无效地址。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static unsafe long ReadInt64(IntPtr ptr, int ofs)
    {
      try
      {
        byte* numPtr1 = (byte*) ((IntPtr) (void*) ptr + ofs);
        if (((int) numPtr1 & 7) == 0)
          return *(long*) numPtr1;
        long num;
        byte* numPtr2 = (byte*) &num;
        *numPtr2 = *numPtr1;
        numPtr2[1] = numPtr1[1];
        numPtr2[2] = numPtr1[2];
        numPtr2[3] = numPtr1[3];
        numPtr2[4] = numPtr1[4];
        numPtr2[5] = numPtr1[5];
        numPtr2[6] = numPtr1[6];
        numPtr2[7] = numPtr1[7];
        return num;
      }
      catch (NullReferenceException ex)
      {
        throw new AccessViolationException();
      }
    }

    /// <summary>从非托管内存中读取一个 64 位带符号整数。</summary>
    /// <returns>从非托管内存中读取的 64 位带符号整数。</returns>
    /// <param name="ptr">非托管内存中开始读取的地址。</param>
    /// <exception cref="T:System.AccessViolationException">
    /// <paramref name="ptr" /> 不是识别的格式。- 或 -<paramref name="ptr" /> 为 null。- 或 -<paramref name="ptr" /> 无效。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    public static long ReadInt64(IntPtr ptr)
    {
      return Marshal.ReadInt64(ptr, 0);
    }

    /// <summary>按指定偏移量将单字节值写入非托管内存。</summary>
    /// <param name="ptr">非托管内存中要写入的基址。</param>
    /// <param name="ofs">额外的字节偏移量，在写入前添加到 <paramref name="ptr" /> 参数中。</param>
    /// <param name="val">要写入的值。</param>
    /// <exception cref="T:System.AccessViolationException">基址 (<paramref name="ptr" />) 加上偏移字节(<paramref name="ofs" />) 可产生空或无效地址。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static unsafe void WriteByte(IntPtr ptr, int ofs, byte val)
    {
      try
      {
        *(sbyte*) ((IntPtr) (void*) ptr + ofs) = (sbyte) val;
      }
      catch (NullReferenceException ex)
      {
        throw new AccessViolationException();
      }
    }

    /// <summary>按指定偏移量将单字节值写入非托管内存。</summary>
    /// <param name="ptr">非托管内存中目标对象的基址。</param>
    /// <param name="ofs">额外的字节偏移量，在写入前添加到 <paramref name="ptr" /> 参数中。</param>
    /// <param name="val">要写入的值。</param>
    /// <exception cref="T:System.AccessViolationException">基址 (<paramref name="ptr" />) 加上偏移字节(<paramref name="ofs" />) 可产生空或无效地址。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="ptr" /> 是 <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> 对象。此方法不接受 <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> 参数。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SuppressUnmanagedCodeSecurity]
    [SecurityCritical]
    [DllImport("mscoree.dll", EntryPoint = "ND_WU1")]
    public static extern void WriteByte([MarshalAs(UnmanagedType.AsAny), In, Out] object ptr, int ofs, byte val);

    /// <summary>将单个字节值写入到非托管内存。</summary>
    /// <param name="ptr">非托管内存中要写入的地址。</param>
    /// <param name="val">要写入的值。</param>
    /// <exception cref="T:System.AccessViolationException">
    /// <paramref name="ptr" /> 不是识别的格式。- 或 -<paramref name="ptr" /> 为 null。- 或 -<paramref name="ptr" /> 无效。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static void WriteByte(IntPtr ptr, byte val)
    {
      Marshal.WriteByte(ptr, 0, val);
    }

    /// <summary>按指定偏移量将 16 位带符号整数值写入非托管内存。</summary>
    /// <param name="ptr">非托管内存中要写入的基址。</param>
    /// <param name="ofs">额外的字节偏移量，在写入前添加到 <paramref name="ptr" /> 参数中。</param>
    /// <param name="val">要写入的值。</param>
    /// <exception cref="T:System.AccessViolationException">基址 (<paramref name="ptr" />) 加上偏移字节(<paramref name="ofs" />) 可产生空或无效地址。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static unsafe void WriteInt16(IntPtr ptr, int ofs, short val)
    {
      try
      {
        byte* numPtr1 = (byte*) ((IntPtr) (void*) ptr + ofs);
        if (((int) numPtr1 & 1) == 0)
        {
          *(short*) numPtr1 = val;
        }
        else
        {
          byte* numPtr2 = (byte*) &val;
          *numPtr1 = *numPtr2;
          numPtr1[1] = numPtr2[1];
        }
      }
      catch (NullReferenceException ex)
      {
        throw new AccessViolationException();
      }
    }

    /// <summary>按指定偏移量将 16 位带符号整数值写入非托管内存。</summary>
    /// <param name="ptr">非托管内存中目标对象的基址。</param>
    /// <param name="ofs">额外的字节偏移量，在写入前添加到 <paramref name="ptr" /> 参数中。</param>
    /// <param name="val">要写入的值。</param>
    /// <exception cref="T:System.AccessViolationException">基址 (<paramref name="ptr" />) 加上偏移字节(<paramref name="ofs" />) 可产生空或无效地址。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="ptr" /> 是 <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> 对象。此方法不接受 <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> 参数。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SuppressUnmanagedCodeSecurity]
    [SecurityCritical]
    [DllImport("mscoree.dll", EntryPoint = "ND_WI2")]
    public static extern void WriteInt16([MarshalAs(UnmanagedType.AsAny), In, Out] object ptr, int ofs, short val);

    /// <summary>将 16 位整数值写入非托管内存。</summary>
    /// <param name="ptr">非托管内存中要写入的地址。</param>
    /// <param name="val">要写入的值。</param>
    /// <exception cref="T:System.AccessViolationException">
    /// <paramref name="ptr" /> 不是识别的格式。- 或 -<paramref name="ptr" /> 为 null。- 或 -<paramref name="ptr" /> 无效。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static void WriteInt16(IntPtr ptr, short val)
    {
      Marshal.WriteInt16(ptr, 0, val);
    }

    /// <summary>按指定偏移量将 16 位带符号整数值写入非托管内存。</summary>
    /// <param name="ptr">本机堆中要写入的基址。</param>
    /// <param name="ofs">额外的字节偏移量，在写入前添加到 <paramref name="ptr" /> 参数中。</param>
    /// <param name="val">要写入的值。</param>
    /// <exception cref="T:System.AccessViolationException">基址 (<paramref name="ptr" />) 加上偏移字节(<paramref name="ofs" />) 可产生空或无效地址。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static void WriteInt16(IntPtr ptr, int ofs, char val)
    {
      Marshal.WriteInt16(ptr, ofs, (short) val);
    }

    /// <summary>按指定偏移量将 16 位带符号整数值写入非托管内存。</summary>
    /// <param name="ptr">非托管内存中目标对象的基址。</param>
    /// <param name="ofs">额外的字节偏移量，在写入前添加到 <paramref name="ptr" /> 参数中。</param>
    /// <param name="val">要写入的值。</param>
    /// <exception cref="T:System.AccessViolationException">基址 (<paramref name="ptr" />) 加上偏移字节(<paramref name="ofs" />) 可产生空或无效地址。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="ptr" /> 是 <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> 对象。此方法不接受 <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> 参数。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static void WriteInt16([In, Out] object ptr, int ofs, char val)
    {
      Marshal.WriteInt16(ptr, ofs, (short) val);
    }

    /// <summary>将一个字符作为 16 位整数值写入非托管内存。</summary>
    /// <param name="ptr">非托管内存中要写入的地址。</param>
    /// <param name="val">要写入的值。</param>
    /// <exception cref="T:System.AccessViolationException">
    /// <paramref name="ptr" /> 不是识别的格式。- 或 -<paramref name="ptr" /> 为 null。- 或 -<paramref name="ptr" /> 无效。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static void WriteInt16(IntPtr ptr, char val)
    {
      Marshal.WriteInt16(ptr, 0, (short) val);
    }

    /// <summary>按指定偏移量将 32 位带符号整数值写入非托管内存。</summary>
    /// <param name="ptr">非托管内存中要写入的基址。</param>
    /// <param name="ofs">额外的字节偏移量，在写入前添加到 <paramref name="ptr" /> 参数中。</param>
    /// <param name="val">要写入的值。</param>
    /// <exception cref="T:System.AccessViolationException">基址 (<paramref name="ptr" />) 加上偏移字节(<paramref name="ofs" />) 可产生空或无效地址。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static unsafe void WriteInt32(IntPtr ptr, int ofs, int val)
    {
      try
      {
        byte* numPtr1 = (byte*) ((IntPtr) (void*) ptr + ofs);
        if (((int) numPtr1 & 3) == 0)
        {
          *(int*) numPtr1 = val;
        }
        else
        {
          byte* numPtr2 = (byte*) &val;
          *numPtr1 = *numPtr2;
          numPtr1[1] = numPtr2[1];
          numPtr1[2] = numPtr2[2];
          numPtr1[3] = numPtr2[3];
        }
      }
      catch (NullReferenceException ex)
      {
        throw new AccessViolationException();
      }
    }

    /// <summary>按指定偏移量将 32 位带符号整数值写入非托管内存。</summary>
    /// <param name="ptr">非托管内存中目标对象的基址。</param>
    /// <param name="ofs">额外的字节偏移量，在写入前添加到 <paramref name="ptr" /> 参数中。</param>
    /// <param name="val">要写入的值。</param>
    /// <exception cref="T:System.AccessViolationException">基址 (<paramref name="ptr" />) 加上偏移字节(<paramref name="ofs" />) 可产生空或无效地址。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="ptr" /> 是 <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> 对象。此方法不接受 <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> 参数。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SuppressUnmanagedCodeSecurity]
    [SecurityCritical]
    [DllImport("mscoree.dll", EntryPoint = "ND_WI4")]
    public static extern void WriteInt32([MarshalAs(UnmanagedType.AsAny), In, Out] object ptr, int ofs, int val);

    /// <summary>将 32 位带符号整数值写入非托管内存。</summary>
    /// <param name="ptr">非托管内存中要写入的地址。</param>
    /// <param name="val">要写入的值。</param>
    /// <exception cref="T:System.AccessViolationException">
    /// <paramref name="ptr" /> 不是识别的格式。- 或 -<paramref name="ptr" /> 为 null。- 或 -<paramref name="ptr" /> 无效。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static void WriteInt32(IntPtr ptr, int val)
    {
      Marshal.WriteInt32(ptr, 0, val);
    }

    /// <summary>按指定的偏移量将一个处理器本机大小的整数值写入非托管内存。</summary>
    /// <param name="ptr">非托管内存中要写入的基址。</param>
    /// <param name="ofs">额外的字节偏移量，在写入前添加到 <paramref name="ptr" /> 参数中。</param>
    /// <param name="val">要写入的值。</param>
    /// <exception cref="T:System.AccessViolationException">基址 (<paramref name="ptr" />) 加上偏移字节(<paramref name="ofs" />) 可产生空或无效地址。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static void WriteIntPtr(IntPtr ptr, int ofs, IntPtr val)
    {
      Marshal.WriteInt32(ptr, ofs, (int) val);
    }

    /// <summary>将一个处理器本机大小的整数值写入非托管内存。</summary>
    /// <param name="ptr">非托管内存中目标对象的基址。</param>
    /// <param name="ofs">额外的字节偏移量，在写入前添加到 <paramref name="ptr" /> 参数中。</param>
    /// <param name="val">要写入的值。</param>
    /// <exception cref="T:System.AccessViolationException">基址 (<paramref name="ptr" />) 加上偏移字节(<paramref name="ofs" />) 可产生空或无效地址。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="ptr" /> 是 <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> 对象。此方法不接受 <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> 参数。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static void WriteIntPtr([MarshalAs(UnmanagedType.AsAny), In, Out] object ptr, int ofs, IntPtr val)
    {
      Marshal.WriteInt32(ptr, ofs, (int) val);
    }

    /// <summary>将一个处理器本机大小的整数值写入非托管内存。</summary>
    /// <param name="ptr">非托管内存中要写入的地址。</param>
    /// <param name="val">要写入的值。</param>
    /// <exception cref="T:System.AccessViolationException">
    /// <paramref name="ptr" /> 不是识别的格式。- 或 -<paramref name="ptr" /> 为 null。- 或 -<paramref name="ptr" /> 无效。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static void WriteIntPtr(IntPtr ptr, IntPtr val)
    {
      Marshal.WriteInt32(ptr, 0, (int) val);
    }

    /// <summary>按指定偏移量将 64 位带符号整数值写入非托管内存。</summary>
    /// <param name="ptr">非托管内存中要写入的基址。</param>
    /// <param name="ofs">额外的字节偏移量，在写入前添加到 <paramref name="ptr" /> 参数中。</param>
    /// <param name="val">要写入的值。</param>
    /// <exception cref="T:System.AccessViolationException">基址 (<paramref name="ptr" />) 加上偏移字节(<paramref name="ofs" />) 可产生空或无效地址。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static unsafe void WriteInt64(IntPtr ptr, int ofs, long val)
    {
      try
      {
        byte* numPtr1 = (byte*) ((IntPtr) (void*) ptr + ofs);
        if (((int) numPtr1 & 7) == 0)
        {
          *(long*) numPtr1 = val;
        }
        else
        {
          byte* numPtr2 = (byte*) &val;
          *numPtr1 = *numPtr2;
          numPtr1[1] = numPtr2[1];
          numPtr1[2] = numPtr2[2];
          numPtr1[3] = numPtr2[3];
          numPtr1[4] = numPtr2[4];
          numPtr1[5] = numPtr2[5];
          numPtr1[6] = numPtr2[6];
          numPtr1[7] = numPtr2[7];
        }
      }
      catch (NullReferenceException ex)
      {
        throw new AccessViolationException();
      }
    }

    /// <summary>按指定偏移量将 64 位带符号整数值写入非托管内存。</summary>
    /// <param name="ptr">非托管内存中目标对象的基址。</param>
    /// <param name="ofs">额外的字节偏移量，在写入前添加到 <paramref name="ptr" /> 参数中。</param>
    /// <param name="val">要写入的值。</param>
    /// <exception cref="T:System.AccessViolationException">基址 (<paramref name="ptr" />) 加上偏移字节(<paramref name="ofs" />) 可产生空或无效地址。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="ptr" /> 是 <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> 对象。此方法不接受 <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> 参数。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SuppressUnmanagedCodeSecurity]
    [SecurityCritical]
    [DllImport("mscoree.dll", EntryPoint = "ND_WI8")]
    public static extern void WriteInt64([MarshalAs(UnmanagedType.AsAny), In, Out] object ptr, int ofs, long val);

    /// <summary>将 64 位带符号整数值写入非托管内存。</summary>
    /// <param name="ptr">非托管内存中要写入的地址。</param>
    /// <param name="val">要写入的值。</param>
    /// <exception cref="T:System.AccessViolationException">
    /// <paramref name="ptr" /> 不是识别的格式。- 或 -<paramref name="ptr" /> 为 null。- 或 -<paramref name="ptr" /> 无效。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static void WriteInt64(IntPtr ptr, long val)
    {
      Marshal.WriteInt64(ptr, 0, val);
    }

    /// <summary>返回由上一个非托管函数返回的错误代码，该函数是使用设置了 <see cref="F:System.Runtime.InteropServices.DllImportAttribute.SetLastError" /> 标志的平台调用来调用的。</summary>
    /// <returns>通过调用 Win32 SetLastError 函数设置的最后一个错误代码。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern int GetLastWin32Error();

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void SetLastWin32Error(int error);

    /// <summary>返回 HRESULT，它对应于使用 <see cref="T:System.Runtime.InteropServices.Marshal" /> 执行的 Win32 代码引起的最后一个错误。</summary>
    /// <returns>对应于最后一个 Win32 错误代码的 HRESULT。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    public static int GetHRForLastWin32Error()
    {
      int lastWin32Error = Marshal.GetLastWin32Error();
      if (((long) lastWin32Error & 2147483648L) == 2147483648L)
        return lastWin32Error;
      return lastWin32Error & (int) ushort.MaxValue | -2147024896;
    }

    /// <summary>在不调用方法的情况下执行一次性方法设置任务。</summary>
    /// <param name="m">要检查的方法。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="m" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="m" /> 参数不是 <see cref="T:System.Reflection.MethodInfo" /> 对象。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static void Prelink(MethodInfo m)
    {
      if (m == (MethodInfo) null)
        throw new ArgumentNullException("m");
      RuntimeMethodInfo runtimeMethodInfo = m as RuntimeMethodInfo;
      // ISSUE: variable of the null type
      __Null local = null;
      if ((MethodInfo) runtimeMethodInfo == (MethodInfo) local)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeMethodInfo"));
      Marshal.InternalPrelink((IRuntimeMethodInfo) runtimeMethodInfo);
    }

    [SuppressUnmanagedCodeSecurity]
    [SecurityCritical]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void InternalPrelink(IRuntimeMethodInfo m);

    /// <summary>对类上的所有方法执行预链接检查。</summary>
    /// <param name="c">要检查其方法的类。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="c" /> 参数为 null。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static void PrelinkAll(Type c)
    {
      if (c == (Type) null)
        throw new ArgumentNullException("c");
      MethodInfo[] methods = c.GetMethods();
      if (methods == null)
        return;
      for (int index = 0; index < methods.Length; ++index)
        Marshal.Prelink(methods[index]);
    }

    /// <summary>计算在非托管内存中保存指定方法的参数所需要的字节数。</summary>
    /// <returns>在非托管内存中表示方法参数所需要的字节数。</returns>
    /// <param name="m">要检查的方法。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="m" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="m" /> 参数不是 <see cref="T:System.Reflection.MethodInfo" /> 对象。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static int NumParamBytes(MethodInfo m)
    {
      if (m == (MethodInfo) null)
        throw new ArgumentNullException("m");
      RuntimeMethodInfo runtimeMethodInfo = m as RuntimeMethodInfo;
      // ISSUE: variable of the null type
      __Null local = null;
      if ((MethodInfo) runtimeMethodInfo == (MethodInfo) local)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeMethodInfo"));
      return Marshal.InternalNumParamBytes((IRuntimeMethodInfo) runtimeMethodInfo);
    }

    [SuppressUnmanagedCodeSecurity]
    [SecurityCritical]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern int InternalNumParamBytes(IRuntimeMethodInfo m);

    /// <summary>检索与计算机无关的异常描述，以及有关异常发生时线程的状态信息。</summary>
    /// <returns>一个指向 EXCEPTION_POINTERS 结构的指针。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    [ComVisible(true)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern IntPtr GetExceptionPointers();

    /// <summary>检索标识所发生异常的类型的代码。</summary>
    /// <returns>异常的类型。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern int GetExceptionCode();

    /// <summary>将数据从托管对象封送到非托管内存块。</summary>
    /// <param name="structure">包含要封送的数据的托管对象。该对象必须是格式化类的结构或实例。</param>
    /// <param name="ptr">指向非托管内存块的指针，必须在调用此方法之前分配该指针。</param>
    /// <param name="fDeleteOld">如果在此方法复制该数据前在 <paramref name="ptr" /> 参数上调用 <see cref="M:System.Runtime.InteropServices.Marshal.DestroyStructure(System.IntPtr,System.Type)" />， 则为true 。该块必须包含有效的数据。请注意，在内存块已包含数据时传递 false 可能会导致内存泄漏。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="structure" /> 一个不是格式化类的引用类型。- 或 -<paramref name="structure" /> 是一个泛型类型。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [ComVisible(true)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern void StructureToPtr(object structure, IntPtr ptr, bool fDeleteOld);

    /// <summary>[在 .NET Framework 4.5.1 和更高版本中受支持]将数据从指定类型的托管内存块封送到非托管内存对象。</summary>
    /// <param name="structure">包含要封送的数据的托管对象。该对象必须是格式化类的结构或实例。</param>
    /// <param name="ptr">指向非托管内存块的指针，必须在调用此方法之前分配该指针。 </param>
    /// <param name="fDeleteOld">如果在此方法复制该数据前在 <paramref name="ptr" /> 参数上调用 <see cref="M:System.Runtime.InteropServices.Marshal.DestroyStructure``1(System.IntPtr)" />， 则为true 。该块必须包含有效的数据。请注意，在内存块已包含数据时传递 false 可能会导致内存泄漏。</param>
    /// <typeparam name="T">托管对象的类型。</typeparam>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="structure" /> 一个不是格式化类的引用类型。</exception>
    [SecurityCritical]
    public static void StructureToPtr<T>(T structure, IntPtr ptr, bool fDeleteOld)
    {
      Marshal.StructureToPtr((object) structure, ptr, fDeleteOld);
    }

    /// <summary>将数据从非托管内存块封送到托管对象。</summary>
    /// <param name="ptr">指向非托管内存块的指针。</param>
    /// <param name="structure">将数据复制到其中的对象。这必须是格式化类的实例。</param>
    /// <exception cref="T:System.ArgumentException">结构布局不是连续或显式的。- 或 -结构为装箱的值类型。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    [ComVisible(true)]
    public static void PtrToStructure(IntPtr ptr, object structure)
    {
      Marshal.PtrToStructureHelper(ptr, structure, false);
    }

    /// <summary>[在 .NET Framework 4.5.1 和更高版本中受支持]将数据从非托管内存块封送到指定类型的托管内存对象。</summary>
    /// <param name="ptr">指向非托管内存块的指针。</param>
    /// <param name="structure">将数据复制到其中的对象。</param>
    /// <typeparam name="T">
    /// <paramref name="structure" /> 的类型。这必须是格式化的类。</typeparam>
    /// <exception cref="T:System.ArgumentException">结构布局不是连续或显式的。 </exception>
    [SecurityCritical]
    public static void PtrToStructure<T>(IntPtr ptr, T structure)
    {
      Marshal.PtrToStructure(ptr, (object) structure);
    }

    /// <summary>将数据从非托管内存块封送到新分配的指定类型的托管对象。</summary>
    /// <returns>一个托管对象，包含 <paramref name="ptr" /> 参数指向的数据。</returns>
    /// <param name="ptr">指向非托管内存块的指针。</param>
    /// <param name="structureType">要创建的对象的类型。此对象必须表示格式化类或结构。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="structureType" /> 参数布局不是连续或显式的。- 或 -<paramref name="structureType" /> 参数是泛型类型。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="structureType" /> 为 null。</exception>
    /// <exception cref="T:System.MissingMethodException">
    /// <paramref name="structureType" /> 指定的类没有可访问的默认值构造函数。 </exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    [ComVisible(true)]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static object PtrToStructure(IntPtr ptr, Type structureType)
    {
      if (ptr == IntPtr.Zero)
        return (object) null;
      if (structureType == (Type) null)
        throw new ArgumentNullException("structureType");
      if (structureType.IsGenericType)
        throw new ArgumentException(Environment.GetResourceString("Argument_NeedNonGenericType"), "structureType");
      RuntimeType runtimeType = structureType.UnderlyingSystemType as RuntimeType;
      // ISSUE: variable of the null type
      __Null local = null;
      if (runtimeType == (RuntimeType) local)
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "type");
      StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
      int num1 = 0;
      int num2 = 0;
      int num3 = 0;
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      StackCrawlMark& stackMark = @stackCrawlMark;
      object instanceDefaultCtor = runtimeType.CreateInstanceDefaultCtor(num1 != 0, num2 != 0, num3 != 0, stackMark);
      Marshal.PtrToStructureHelper(ptr, instanceDefaultCtor, true);
      return instanceDefaultCtor;
    }

    /// <summary>[在 .NET Framework 4.5.1 和更高版本中受支持]将数据从非托管内存块封送到泛型类型参数指定的类型的新分配托管对象。</summary>
    /// <returns>一个托管对象，包含 <paramref name="ptr" /> 参数指向的数据。</returns>
    /// <param name="ptr">指向非托管内存块的指针。</param>
    /// <typeparam name="T">要将数据复制到其中的对象的类型。这必须是格式化类或结构。</typeparam>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="T" /> 的布局不是连续或显式的。</exception>
    /// <exception cref="T:System.MissingMethodException">
    /// <paramref name="T" /> 指定的类没有可访问的默认值构造函数。 </exception>
    [SecurityCritical]
    public static T PtrToStructure<T>(IntPtr ptr)
    {
      return (T) Marshal.PtrToStructure(ptr, typeof (T));
    }

    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void PtrToStructureHelper(IntPtr ptr, object structure, bool allowValueClasses);

    /// <summary>释放指定的非托管内存块所指向的所有子结构。</summary>
    /// <param name="ptr">指向非托管内存块的指针。</param>
    /// <param name="structuretype">格式化类的类型。该类型提供删除 <paramref name="ptr" /> 参数指向的缓冲区时必需的布局信息。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="structureType" /> 具有自动布局。但请使用连续或显式布局。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    [ComVisible(true)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern void DestroyStructure(IntPtr ptr, Type structuretype);

    /// <summary>[在 .NET Framework 4.5.1 和更高版本中受支持]释放指定的非托管内存块所指向的所有指定类型的子结构。</summary>
    /// <param name="ptr">指向非托管内存块的指针。 </param>
    /// <typeparam name="T">格式化结构的类型。该类型提供删除 <paramref name="ptr" /> 参数指向的缓冲区时必需的布局信息。</typeparam>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="T" /> 具有自动布局。但请使用连续或显式布局。</exception>
    [SecurityCritical]
    public static void DestroyStructure<T>(IntPtr ptr)
    {
      Marshal.DestroyStructure(ptr, typeof (T));
    }

    /// <summary>返回指定模块的实例句柄 (HINSTANCE)。</summary>
    /// <returns>
    /// <paramref name="m" /> 的 HINSTANCE；如果该模块没有 HINSTANCE，则为 -1。</returns>
    /// <param name="m">具有所需 HINSTANCE 的模块。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="m" /> 参数为 null。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static IntPtr GetHINSTANCE(Module m)
    {
      if (m == (Module) null)
        throw new ArgumentNullException("m");
      RuntimeModule runtimeModule = m as RuntimeModule;
      if ((Module) runtimeModule == (Module) null)
      {
        ModuleBuilder moduleBuilder = m as ModuleBuilder;
        if ((Module) moduleBuilder != (Module) null)
          runtimeModule = (RuntimeModule) moduleBuilder.InternalModule;
      }
      if ((Module) runtimeModule == (Module) null)
        throw new ArgumentNullException(Environment.GetResourceString("Argument_MustBeRuntimeModule"));
      return Marshal.GetHINSTANCE(runtimeModule.GetNativeHandle());
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern IntPtr GetHINSTANCE(RuntimeModule m);

    /// <summary>用特定的失败 HRESULT 值引发异常。</summary>
    /// <param name="errorCode">与所需异常相对应的 HRESULT。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static void ThrowExceptionForHR(int errorCode)
    {
      if (errorCode >= 0)
        return;
      Marshal.ThrowExceptionForHRInternal(errorCode, IntPtr.Zero);
    }

    /// <summary>基于指定的 IErrorInfo 接口，以特定的失败 HRESULT 引发异常。</summary>
    /// <param name="errorCode">与所需异常相对应的 HRESULT。</param>
    /// <param name="errorInfo">指向 IErrorInfo 接口的指针，该接口提供有关错误的更多信息。您可以指定 IntPtr(0) 以使用当前 IErrorInfo 接口，或者指定 IntPtr(-1) 以忽略当前 IErrorInfo 接口，并仅从错误代码构造异常。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static void ThrowExceptionForHR(int errorCode, IntPtr errorInfo)
    {
      if (errorCode >= 0)
        return;
      Marshal.ThrowExceptionForHRInternal(errorCode, errorInfo);
    }

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void ThrowExceptionForHRInternal(int errorCode, IntPtr errorInfo);

    /// <summary>将指定的 HRESULT 错误代码转换为对应的 <see cref="T:System.Exception" /> 对象。</summary>
    /// <returns>表示转换后的 HRESULT 的对象。</returns>
    /// <param name="errorCode">要转换的 HRESULT。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static Exception GetExceptionForHR(int errorCode)
    {
      if (errorCode < 0)
        return Marshal.GetExceptionForHRInternal(errorCode, IntPtr.Zero);
      return (Exception) null;
    }

    /// <summary>通过传入异常对象的 IErrorInfo 接口的附加错误消息，将指定的 HRESULT 错误代码转换为对应的 <see cref="T:System.Exception" /> 对象。</summary>
    /// <returns>一个对象，表示转换后的 HRESULT 以及从 <paramref name="errorInfo" /> 获取的信息。</returns>
    /// <param name="errorCode">要转换的 HRESULT。</param>
    /// <param name="errorInfo">指向 IErrorInfo 接口的指针，该接口提供有关错误的更多信息。您可以指定 IntPtr(0) 以使用当前 IErrorInfo 接口，或者指定 IntPtr(-1) 以忽略当前 IErrorInfo 接口，并仅从错误代码构造异常。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static Exception GetExceptionForHR(int errorCode, IntPtr errorInfo)
    {
      if (errorCode < 0)
        return Marshal.GetExceptionForHRInternal(errorCode, errorInfo);
      return (Exception) null;
    }

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern Exception GetExceptionForHRInternal(int errorCode, IntPtr errorInfo);

    /// <summary>将指定异常转换为 HRESULT。</summary>
    /// <returns>映射到所提供的异常的 HRESULT。</returns>
    /// <param name="e">要转换为 HRESULT 的异常。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern int GetHRForException(Exception e);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern int GetHRForException_WinRT(Exception e);

    /// <summary>获取指向运行时生成的函数的指针，该函数将调用从非托管代码封送到托管代码。</summary>
    /// <returns>指向一个函数的指针，该函数将封送从 <paramref name="pfnMethodToWrap" /> 到托管代码的调用。</returns>
    /// <param name="pfnMethodToWrap">指向要封送的方法的指针。</param>
    /// <param name="pbSignature">指向方法签名的指针。</param>
    /// <param name="cbSignature">
    /// <paramref name="pbSignature" /> 中的字节数。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    [Obsolete("The GetUnmanagedThunkForManagedMethodPtr method has been deprecated and will be removed in a future release.", false)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern IntPtr GetUnmanagedThunkForManagedMethodPtr(IntPtr pfnMethodToWrap, IntPtr pbSignature, int cbSignature);

    /// <summary>获取指向运行时生成的函数的指针，该函数将调用从托管代码封送到非托管代码。</summary>
    /// <returns>指向一个函数的指针，该函数将封送从 <paramref name="pfnMethodToWrap" /> 参数到非托管代码的调用。</returns>
    /// <param name="pfnMethodToWrap">指向要封送的方法的指针。</param>
    /// <param name="pbSignature">指向方法签名的指针。</param>
    /// <param name="cbSignature">
    /// <paramref name="pbSignature" /> 中的字节数。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    [Obsolete("The GetManagedThunkForUnmanagedMethodPtr method has been deprecated and will be removed in a future release.", false)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern IntPtr GetManagedThunkForUnmanagedMethodPtr(IntPtr pfnMethodToWrap, IntPtr pbSignature, int cbSignature);

    /// <summary>将纤程 Cookie 转换为相应的 <see cref="T:System.Threading.Thread" /> 实例。</summary>
    /// <returns>对应于 <paramref name="cookie" /> 参数的线程。</returns>
    /// <param name="cookie">表示纤程 Cookie 的整数。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="cookie" /> 参数为 0。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    [Obsolete("The GetThreadFromFiberCookie method has been deprecated.  Use the hosting API to perform this operation.", false)]
    public static Thread GetThreadFromFiberCookie(int cookie)
    {
      if (cookie == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_ArgumentZero"), "cookie");
      return Marshal.InternalGetThreadFromFiberCookie(cookie);
    }

    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern Thread InternalGetThreadFromFiberCookie(int cookie);

    /// <summary>通过使用指向指定字节数的指针，从进程的非托管内存中分配内存。</summary>
    /// <returns>指向新分配的内存的指针。必须使用 <see cref="M:System.Runtime.InteropServices.Marshal.FreeHGlobal(System.IntPtr)" /> 方法释放此内存。</returns>
    /// <param name="cb">内存中的所需字节数。</param>
    /// <exception cref="T:System.OutOfMemoryException">内存不足，无法满足请求。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    public static IntPtr AllocHGlobal(IntPtr cb)
    {
      IntPtr num1 = Win32Native.LocalAlloc_NoSafeHandle(0, new UIntPtr((uint) cb.ToInt32()));
      IntPtr num2 = IntPtr.Zero;
      if (!(num1 == num2))
        return num1;
      throw new OutOfMemoryException();
    }

    /// <summary>通过使用指定的字节数，从进程的非托管内存中分配内存。</summary>
    /// <returns>指向新分配的内存的指针。必须使用 <see cref="M:System.Runtime.InteropServices.Marshal.FreeHGlobal(System.IntPtr)" /> 方法释放此内存。</returns>
    /// <param name="cb">内存中的所需字节数。</param>
    /// <exception cref="T:System.OutOfMemoryException">内存不足，无法满足请求。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    public static IntPtr AllocHGlobal(int cb)
    {
      return Marshal.AllocHGlobal((IntPtr) cb);
    }

    /// <summary>释放以前从进程的非托管内存中分配的内存。</summary>
    /// <param name="hglobal">由对 <see cref="M:System.Runtime.InteropServices.Marshal.AllocHGlobal(System.IntPtr)" /> 的原始匹配调用返回的句柄。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    public static void FreeHGlobal(IntPtr hglobal)
    {
      if (!Marshal.IsNotWin32Atom(hglobal) || !(IntPtr.Zero != Win32Native.LocalFree(hglobal)))
        return;
      Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
    }

    /// <summary>调整以前用 <see cref="M:System.Runtime.InteropServices.Marshal.AllocHGlobal(System.IntPtr)" /> 分配的内存块的大小。</summary>
    /// <returns>指向重新分配的内存的指针。该内存必须用 <see cref="M:System.Runtime.InteropServices.Marshal.FreeHGlobal(System.IntPtr)" /> 来释放。</returns>
    /// <param name="pv">指向用 <see cref="M:System.Runtime.InteropServices.Marshal.AllocHGlobal(System.IntPtr)" /> 分配的内存的指针。</param>
    /// <param name="cb">已分配块的新大小。这不是指针；它是您请求的字节数，转换为类型 <see cref="T:System.IntPtr" />。如果你传递指针，则将其视为大小。</param>
    /// <exception cref="T:System.OutOfMemoryException">内存不足，无法满足请求。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static IntPtr ReAllocHGlobal(IntPtr pv, IntPtr cb)
    {
      IntPtr num1 = Win32Native.LocalReAlloc(pv, cb, 2);
      IntPtr num2 = IntPtr.Zero;
      if (!(num1 == num2))
        return num1;
      throw new OutOfMemoryException();
    }

    /// <summary>将托管 <see cref="T:System.String" /> 中的内容复制到非托管内存，并在复制时转换为 ANSI 格式。</summary>
    /// <returns>非托管内存中将 <paramref name="s" /> 复制到的地址；如果 <paramref name="s" /> 为 null，则为 0。</returns>
    /// <param name="s">要复制的托管字符串。</param>
    /// <exception cref="T:System.OutOfMemoryException">没有足够的可用内存。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="s" /> 参数超过了操作系统所允许的最大长度。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static unsafe IntPtr StringToHGlobalAnsi(string s)
    {
      if (s == null)
        return IntPtr.Zero;
      int cbNativeBuffer = (s.Length + 1) * Marshal.SystemMaxDBCSCharSize;
      if (cbNativeBuffer < s.Length)
        throw new ArgumentOutOfRangeException("s");
      IntPtr num = Win32Native.LocalAlloc_NoSafeHandle(0, new UIntPtr((uint) cbNativeBuffer));
      if (num == IntPtr.Zero)
        throw new OutOfMemoryException();
      s.ConvertToAnsi((byte*) (void*) num, cbNativeBuffer, false, false);
      return num;
    }

    /// <summary>向非托管内存复制托管 <see cref="T:System.String" /> 的内容。</summary>
    /// <returns>非托管内存中将 <paramref name="s" /> 复制到的地址；如果 <paramref name="s" /> 为 null，则为 0。</returns>
    /// <param name="s">要复制的托管字符串。</param>
    /// <exception cref="T:System.OutOfMemoryException">此方法未能分配足够的本机堆内存。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="s" /> 参数超过了操作系统所允许的最大长度。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static unsafe IntPtr StringToHGlobalUni(string s)
    {
      if (s == null)
        return IntPtr.Zero;
      int num1 = (s.Length + 1) * 2;
      if (num1 < s.Length)
        throw new ArgumentOutOfRangeException("s");
      IntPtr num2 = Win32Native.LocalAlloc_NoSafeHandle(0, new UIntPtr((uint) num1));
      if (num2 == IntPtr.Zero)
        throw new OutOfMemoryException();
      string str = s;
      char* smem = (char*) str;
      if ((IntPtr) smem != IntPtr.Zero)
        smem += RuntimeHelpers.OffsetToStringData;
      string.wstrcpy((char*) (void*) num2, smem, s.Length + 1);
      str = (string) null;
      return num2;
    }

    /// <summary>向非托管内存复制托管 <see cref="T:System.String" /> 的内容，并在需要时转换为 ANSI 格式。</summary>
    /// <returns>非托管内存中将字符串复制到的地址；如果 <paramref name="s" /> 为 null，则为 0。</returns>
    /// <param name="s">要复制的托管字符串。</param>
    /// <exception cref="T:System.OutOfMemoryException">没有足够的可用内存。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static IntPtr StringToHGlobalAuto(string s)
    {
      return Marshal.StringToHGlobalUni(s);
    }

    /// <summary>检索类型库的名称。</summary>
    /// <returns>
    /// <paramref name="pTLB" /> 参数指向的类型库的名称。</returns>
    /// <param name="pTLB">要检索其名称的类型库。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    [Obsolete("Use System.Runtime.InteropServices.Marshal.GetTypeLibName(ITypeLib pTLB) instead. http://go.microsoft.com/fwlink/?linkid=14202&ID=0000011.", false)]
    public static string GetTypeLibName(UCOMITypeLib pTLB)
    {
      return Marshal.GetTypeLibName((ITypeLib) pTLB);
    }

    /// <summary>检索类型库的名称。</summary>
    /// <returns>
    /// <paramref name="typelib" /> 参数指向的类型库的名称。</returns>
    /// <param name="typelib">要检索其名称的类型库。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="typelib" /> 参数为 null。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static string GetTypeLibName(ITypeLib typelib)
    {
      if (typelib == null)
        throw new ArgumentNullException("typelib");
      string strName = (string) null;
      string strDocString = (string) null;
      int dwHelpContext = 0;
      string strHelpFile = (string) null;
      typelib.GetDocumentation(-1, out strName, out strDocString, out dwHelpContext, out strHelpFile);
      return strName;
    }

    [SecurityCritical]
    internal static string GetTypeLibNameInternal(ITypeLib typelib)
    {
      if (typelib == null)
        throw new ArgumentNullException("typelib");
      ITypeLib2 typeLib2 = typelib as ITypeLib2;
      if (typeLib2 != null)
      {
        Guid guid = Marshal.ManagedNameGuid;
        object pVarVal;
        try
        {
          typeLib2.GetCustData(ref guid, out pVarVal);
        }
        catch (Exception ex)
        {
          pVarVal = (object) null;
        }
        if (pVarVal != null && pVarVal.GetType() == typeof (string))
        {
          string str = ((string) pVarVal).Trim();
          if (str.EndsWith(".DLL", StringComparison.OrdinalIgnoreCase))
            str = str.Substring(0, str.Length - 4);
          else if (str.EndsWith(".EXE", StringComparison.OrdinalIgnoreCase))
            str = str.Substring(0, str.Length - 4);
          return str;
        }
      }
      return Marshal.GetTypeLibName(typelib);
    }

    /// <summary>检索类型库的库标识符 (LIBID)。</summary>
    /// <returns>
    /// <paramref name="pTLB" /> 参数指向的类型库的 LIBID。</returns>
    /// <param name="pTLB">要检索其 LIBID 的类型库。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    [Obsolete("Use System.Runtime.InteropServices.Marshal.GetTypeLibGuid(ITypeLib pTLB) instead. http://go.microsoft.com/fwlink/?linkid=14202&ID=0000011.", false)]
    public static Guid GetTypeLibGuid(UCOMITypeLib pTLB)
    {
      return Marshal.GetTypeLibGuid((ITypeLib) pTLB);
    }

    /// <summary>检索类型库的库标识符 (LIBID)。</summary>
    /// <returns>指定类型库的 LIBID。</returns>
    /// <param name="typelib">要检索其 LIBID 的类型库。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static Guid GetTypeLibGuid(ITypeLib typelib)
    {
      Guid result = new Guid();
      Marshal.FCallGetTypeLibGuid(ref result, typelib);
      return result;
    }

    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void FCallGetTypeLibGuid(ref Guid result, ITypeLib pTLB);

    /// <summary>检索类型库的 LCID。</summary>
    /// <returns>
    /// <paramref name="pTLB" /> 参数指向的类型库的 LCID。</returns>
    /// <param name="pTLB">要检索其 LCID 的类型库。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    [Obsolete("Use System.Runtime.InteropServices.Marshal.GetTypeLibLcid(ITypeLib pTLB) instead. http://go.microsoft.com/fwlink/?linkid=14202&ID=0000011.", false)]
    public static int GetTypeLibLcid(UCOMITypeLib pTLB)
    {
      return Marshal.GetTypeLibLcid((ITypeLib) pTLB);
    }

    /// <summary>检索类型库的 LCID。</summary>
    /// <returns>
    /// <paramref name="typelib" /> 参数指向的类型库的 LCID。</returns>
    /// <param name="typelib">要检索其 LCID 的类型库。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern int GetTypeLibLcid(ITypeLib typelib);

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void GetTypeLibVersion(ITypeLib typeLibrary, out int major, out int minor);

    [SecurityCritical]
    internal static Guid GetTypeInfoGuid(ITypeInfo typeInfo)
    {
      Guid result = new Guid();
      Marshal.FCallGetTypeInfoGuid(ref result, typeInfo);
      return result;
    }

    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void FCallGetTypeInfoGuid(ref Guid result, ITypeInfo typeInfo);

    /// <summary>检索从指定程序集导出类型库时分配给该类型库的库标识符 (LIBID)。</summary>
    /// <returns>从指定的程序集导出类型库时分配给该类型库的 LIBID。</returns>
    /// <param name="asm">从其导出类型库的程序集。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="asm" /> 为 null。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static Guid GetTypeLibGuidForAssembly(Assembly asm)
    {
      if (asm == (Assembly) null)
        throw new ArgumentNullException("asm");
      RuntimeAssembly asm1 = asm as RuntimeAssembly;
      if ((Assembly) asm1 == (Assembly) null)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeAssembly"), "asm");
      Guid result = new Guid();
      Marshal.FCallGetTypeLibGuidForAssembly(ref result, asm1);
      return result;
    }

    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void FCallGetTypeLibGuidForAssembly(ref Guid result, RuntimeAssembly asm);

    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void _GetTypeLibVersionForAssembly(RuntimeAssembly inputAssembly, out int majorVersion, out int minorVersion);

    /// <summary>检索将从指定程序集导出的类型库的版本号。</summary>
    /// <param name="inputAssembly">托管程序集。</param>
    /// <param name="majorVersion">主版本号。</param>
    /// <param name="minorVersion">次版本号。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="inputAssembly" /> 为 null。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static void GetTypeLibVersionForAssembly(Assembly inputAssembly, out int majorVersion, out int minorVersion)
    {
      if (inputAssembly == (Assembly) null)
        throw new ArgumentNullException("inputAssembly");
      RuntimeAssembly inputAssembly1 = inputAssembly as RuntimeAssembly;
      // ISSUE: variable of the null type
      __Null local = null;
      if ((Assembly) inputAssembly1 == (Assembly) local)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeAssembly"), "inputAssembly");
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      int& majorVersion1 = @majorVersion;
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      int& minorVersion1 = @minorVersion;
      Marshal._GetTypeLibVersionForAssembly(inputAssembly1, majorVersion1, minorVersion1);
    }

    /// <summary>检索由 ITypeInfo 对象表示的类型的名称。</summary>
    /// <returns>
    /// <paramref name="pTI" /> 参数指向的类型的名称。</returns>
    /// <param name="pTI">表示 ITypeInfo 指针的对象。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    [Obsolete("Use System.Runtime.InteropServices.Marshal.GetTypeInfoName(ITypeInfo pTLB) instead. http://go.microsoft.com/fwlink/?linkid=14202&ID=0000011.", false)]
    public static string GetTypeInfoName(UCOMITypeInfo pTI)
    {
      return Marshal.GetTypeInfoName((ITypeInfo) pTI);
    }

    /// <summary>检索由 ITypeInfo 对象表示的类型的名称。</summary>
    /// <returns>
    /// <paramref name="typeInfo" /> 参数指向的类型的名称。</returns>
    /// <param name="typeInfo">表示 ITypeInfo 指针的对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="typeInfo" /> 参数为 null。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static string GetTypeInfoName(ITypeInfo typeInfo)
    {
      if (typeInfo == null)
        throw new ArgumentNullException("typeInfo");
      string strName = (string) null;
      string strDocString = (string) null;
      int dwHelpContext = 0;
      string strHelpFile = (string) null;
      typeInfo.GetDocumentation(-1, out strName, out strDocString, out dwHelpContext, out strHelpFile);
      return strName;
    }

    [SecurityCritical]
    internal static string GetTypeInfoNameInternal(ITypeInfo typeInfo, out bool hasManagedName)
    {
      if (typeInfo == null)
        throw new ArgumentNullException("typeInfo");
      ITypeInfo2 typeInfo2 = typeInfo as ITypeInfo2;
      if (typeInfo2 != null)
      {
        Guid guid = Marshal.ManagedNameGuid;
        object pVarVal;
        try
        {
          typeInfo2.GetCustData(ref guid, out pVarVal);
        }
        catch (Exception ex)
        {
          pVarVal = (object) null;
        }
        if (pVarVal != null && pVarVal.GetType() == typeof (string))
        {
          hasManagedName = true;
          return (string) pVarVal;
        }
      }
      hasManagedName = false;
      return Marshal.GetTypeInfoName(typeInfo);
    }

    [SecurityCritical]
    internal static string GetManagedTypeInfoNameInternal(ITypeLib typeLib, ITypeInfo typeInfo)
    {
      bool hasManagedName;
      string infoNameInternal = Marshal.GetTypeInfoNameInternal(typeInfo, out hasManagedName);
      if (hasManagedName)
        return infoNameInternal;
      return Marshal.GetTypeLibNameInternal(typeLib) + "." + infoNameInternal;
    }

    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern Type GetLoadedTypeForGUID(ref Guid guid);

    /// <summary>将非托管 ITypeInfo 对象转换为托管 <see cref="T:System.Type" /> 对象。</summary>
    /// <returns>表示非托管 ITypeInfo 对象的托管类型。</returns>
    /// <param name="piTypeInfo">要封送的 ITypeInfo 接口。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
    /// </PermissionSet>
    [SecurityCritical]
    public static Type GetTypeForITypeInfo(IntPtr piTypeInfo)
    {
      ITypeLib ppTLB = (ITypeLib) null;
      Assembly assembly = (Assembly) null;
      int pIndex = 0;
      if (piTypeInfo == IntPtr.Zero)
        return (Type) null;
      ITypeInfo typeInfo = (ITypeInfo) Marshal.GetObjectForIUnknown(piTypeInfo);
      Guid typeInfoGuid = Marshal.GetTypeInfoGuid(typeInfo);
      Type loadedTypeForGuid = Marshal.GetLoadedTypeForGUID(ref typeInfoGuid);
      if (loadedTypeForGuid != (Type) null)
        return loadedTypeForGuid;
      try
      {
        typeInfo.GetContainingTypeLib(out ppTLB, out pIndex);
      }
      catch (COMException ex)
      {
        ppTLB = (ITypeLib) null;
      }
      Type type;
      if (ppTLB != null)
      {
        string fullName = TypeLibConverter.GetAssemblyNameFromTypelib((object) ppTLB, (string) null, (byte[]) null, (StrongNameKeyPair) null, (Version) null, AssemblyNameFlags.None).FullName;
        Assembly[] assemblies = Thread.GetDomain().GetAssemblies();
        int length = assemblies.Length;
        for (int index = 0; index < length; ++index)
        {
          if (string.Compare(assemblies[index].FullName, fullName, StringComparison.Ordinal) == 0)
            assembly = assemblies[index];
        }
        if (assembly == (Assembly) null)
          assembly = (Assembly) new TypeLibConverter().ConvertTypeLibToAssembly((object) ppTLB, Marshal.GetTypeLibName(ppTLB) + ".dll", TypeLibImporterFlags.None, (ITypeLibImporterNotifySink) new ImporterCallback(), (byte[]) null, (StrongNameKeyPair) null, (string) null, (Version) null);
        type = assembly.GetType(Marshal.GetManagedTypeInfoNameInternal(ppTLB, typeInfo), true, false);
        if (type != (Type) null && !type.IsVisible)
          type = (Type) null;
      }
      else
        type = typeof (object);
      return type;
    }

    /// <summary>返回与指定类标识符 (CLSID) 关联的类型。</summary>
    /// <returns>System.__ComObject，无论 CLSID 是否有效。</returns>
    /// <param name="clsid">返回的类型的 CLSID。 </param>
    [SecuritySafeCritical]
    public static Type GetTypeFromCLSID(Guid clsid)
    {
      return RuntimeType.GetTypeFromCLSIDImpl(clsid, (string) null, false);
    }

    /// <summary>从托管类型返回一个 <see cref="T:System.Runtime.InteropServices.ComTypes.ITypeInfo" /> 接口。</summary>
    /// <returns>指向 <paramref name="t" /> 参数的 ITypeInfo 接口的指针。</returns>
    /// <param name="t">正在请求其 ITypeInfo 接口的类型。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="t" /> 不是 COM 可见的类型。- 或 -<paramref name="t" /> 是一种 Windows 运行时 类型。</exception>
    /// <exception cref="T:System.Runtime.InteropServices.COMException">为包含该类型的程序集注册了类型库，但无法找到类型定义。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern IntPtr GetITypeInfoForType(Type t);

    /// <summary>从托管对象返回 IUnknown 接口。</summary>
    /// <returns>
    /// <paramref name="o" /> 参数的 IUnknown 指针。</returns>
    /// <param name="o">其 IUnknown 接口被请求的对象。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    [__DynamicallyInvokable]
    public static IntPtr GetIUnknownForObject(object o)
    {
      return Marshal.GetIUnknownForObjectNative(o, false);
    }

    /// <summary>如果调用方与托管对象在同一上下文中，则从该对象返回一个 IUnknown 接口。</summary>
    /// <returns>指定对象的 IUnknown 指针；如果调用方与指定对象不在同一上下文中，则为 null。</returns>
    /// <param name="o">其 IUnknown 接口被请求的对象。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static IntPtr GetIUnknownForObjectInContext(object o)
    {
      return Marshal.GetIUnknownForObjectNative(o, true);
    }

    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern IntPtr GetIUnknownForObjectNative(object o, bool onlyInContext);

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern IntPtr GetRawIUnknownForComObjectNoAddRef(object o);

    /// <summary>从托管对象返回一个 IDispatch 接口。</summary>
    /// <returns>
    /// <paramref name="o" /> 参数的 IDispatch 指针。</returns>
    /// <param name="o">其 IDispatch 接口被请求的对象。</param>
    /// <exception cref="T:System.InvalidCastException">
    /// <paramref name="o" /> 不支持请求的接口。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    [__DynamicallyInvokable]
    public static IntPtr GetIDispatchForObject(object o)
    {
      return Marshal.GetIDispatchForObjectNative(o, false);
    }

    /// <summary>如果调用方与托管对象在同一上下文中，则从该对象返回一个 IDispatch 接口指针。</summary>
    /// <returns>指定对象的 IDispatch 接口指针；如果调用方与指定对象不在同一上下文中，则为 null。</returns>
    /// <param name="o">其 IDispatch 接口被请求的对象。</param>
    /// <exception cref="T:System.InvalidCastException">
    /// <paramref name="o" /> 不支持请求的接口。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="o" /> 为 null。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static IntPtr GetIDispatchForObjectInContext(object o)
    {
      return Marshal.GetIDispatchForObjectNative(o, true);
    }

    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern IntPtr GetIDispatchForObjectNative(object o, bool onlyInContext);

    /// <summary>返回一个指向 IUnknown 接口的指针，该指针表示指定对象上的指定接口。默认情况下，启用自定义查询接口访问。</summary>
    /// <returns>表示对象的指定接口的接口指针。</returns>
    /// <param name="o">提供接口的对象。</param>
    /// <param name="T">所请求接口的类型。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="T" /> 参数不是接口。- 或 -该类型对 COM 不可见。- 或 -<paramref name="T" /> 参数是泛型类型。</exception>
    /// <exception cref="T:System.InvalidCastException">
    /// <paramref name="o" /> 参数不支持请求的接口。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="o" /> 参数为 null。- 或 -<paramref name="T" /> 参数为 null。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static IntPtr GetComInterfaceForObject(object o, Type T)
    {
      return Marshal.GetComInterfaceForObjectNative(o, T, false, true);
    }

    /// <summary>[在 .NET Framework 4.5.1 和更高版本中受支持]返回指向 IUnknown 接口的指针，该指针表示指定类型的对象上的指定接口。默认情况下，启用自定义查询接口访问。</summary>
    /// <returns>表示 <paramref name="TInterface" /> 接口的接口指针。</returns>
    /// <param name="o">提供接口的对象。</param>
    /// <typeparam name="T">
    /// <paramref name="o" /> 的类型。</typeparam>
    /// <typeparam name="TInterface">要返回的接口的类型。</typeparam>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="TInterface" /> 参数不是接口。- 或 -该类型对 COM 不可见。- 或 -<paramref name="T" /> 参数是开放式泛型类型。</exception>
    /// <exception cref="T:System.InvalidCastException">
    /// <paramref name="o" /> 参数不支持 <paramref name="TInterface" /> 接口。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="o" /> 参数为 null。</exception>
    [SecurityCritical]
    public static IntPtr GetComInterfaceForObject<T, TInterface>(T o)
    {
      return Marshal.GetComInterfaceForObject((object) o, typeof (TInterface));
    }

    /// <summary>返回一个指向 IUnknown 接口的指针，该指针表示指定对象上的指定接口。自定义查询接口访问由指定的自定义模式控制。</summary>
    /// <returns>表示对象的接口的接口指针。</returns>
    /// <param name="o">提供接口的对象。</param>
    /// <param name="T">所请求接口的类型。</param>
    /// <param name="mode">指示是否要应用 <see cref="T:System.Runtime.InteropServices.ICustomQueryInterface" /> 提供的 IUnknown::QueryInterface 自定义的枚举值之一。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="T" /> 参数不是接口。- 或 -该类型对 COM 不可见。- 或 -<paramref name="T" /> 参数是泛型类型。</exception>
    /// <exception cref="T:System.InvalidCastException">对象 <paramref name="o" /> 不支持请求的接口。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="o" /> 参数为 null。- 或 -<paramref name="T" /> 参数为 null。</exception>
    [SecurityCritical]
    public static IntPtr GetComInterfaceForObject(object o, Type T, CustomQueryInterfaceMode mode)
    {
      bool fEnalbeCustomizedQueryInterface = mode == CustomQueryInterfaceMode.Allow;
      return Marshal.GetComInterfaceForObjectNative(o, T, false, fEnalbeCustomizedQueryInterface);
    }

    /// <summary>返回一个接口指针，该指针表示对象的指定接口（如果调用方与对象在同一上下文中）。</summary>
    /// <returns>由 <paramref name="t" /> 指定的接口指针，该指针表示指定对象的接口，如果调用方与指定对象不在同一上下文中，则为 null。</returns>
    /// <param name="o">提供接口的对象。</param>
    /// <param name="t">所请求接口的类型。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="t" /> 不是一个接口。- 或 -该类型对 COM 不可见。</exception>
    /// <exception cref="T:System.InvalidCastException">
    /// <paramref name="o" /> 不支持请求的接口。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="o" /> 为 null。- 或 - <paramref name="t" /> 为 null。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static IntPtr GetComInterfaceForObjectInContext(object o, Type t)
    {
      return Marshal.GetComInterfaceForObjectNative(o, t, true, true);
    }

    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern IntPtr GetComInterfaceForObjectNative(object o, Type t, bool onlyInContext, bool fEnalbeCustomizedQueryInterface);

    /// <summary>返回一个类型实例，该实例通过指向 COM 对象的 IUnknown 接口的指针表示该对象。</summary>
    /// <returns>一个对象，表示指定的非托管 COM 对象。</returns>
    /// <param name="pUnk">指向 IUnknown 接口的指针。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern object GetObjectForIUnknown(IntPtr pUnk);

    /// <summary>为给定的 IUnknown 接口创建唯一的 运行时可调用包装 (RCW) 对象。</summary>
    /// <returns>指定的 IUnknown 接口的唯一 RCW。</returns>
    /// <param name="unknown">指向 IUnknown 接口的托管指针。</param>
    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern object GetUniqueObjectForIUnknown(IntPtr unknown);

    /// <summary>返回表示 COM 对象的指定类型的托管对象。</summary>
    /// <returns>类的实例，与表示所请求的非托管 COM 对象的 <see cref="T:System.Type" /> 对象相对应。</returns>
    /// <param name="pUnk">指向非托管对象的 IUnknown 接口的一个指针。</param>
    /// <param name="t">请求的托管类的类型。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="t" /> 未用 <see cref="T:System.Runtime.InteropServices.ComImportAttribute" /> 进行特性化。- 或 -<paramref name="t" /> 是一种 Windows 运行时 类型。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern object GetTypedObjectForIUnknown(IntPtr pUnk, Type t);

    /// <summary>聚合托管对象和指定的 COM 对象。</summary>
    /// <returns>托管对象的内部 IUnknown 指针。</returns>
    /// <param name="pOuter">外部 IUnknown 指针。</param>
    /// <param name="o">要聚合的对象。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="o" /> 是 Windows 运行时 对象。</exception>
    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern IntPtr CreateAggregatedObject(IntPtr pOuter, object o);

    /// <summary>[在 .NET Framework 4.5.1 和更高版本中受支持]聚合指定类型的托管对象和指定的 COM 对象。</summary>
    /// <returns>托管对象的内部 IUnknown 指针。 </returns>
    /// <param name="pOuter">外部 IUnknown 指针。</param>
    /// <param name="o">要集合的托管对象。</param>
    /// <typeparam name="T">要聚合的托管对象的类型。</typeparam>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="o" /> 是 Windows 运行时 对象。</exception>
    [SecurityCritical]
    public static IntPtr CreateAggregatedObject<T>(IntPtr pOuter, T o)
    {
      return Marshal.CreateAggregatedObject(pOuter, (object) o);
    }

    /// <summary>通知运行时清理在当前上下文中分配的所有运行时可调用包装器 (RCW)。</summary>
    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern void CleanupUnusedObjectsInCurrentContext();

    /// <summary>指示是否可以清除任何上下文中的运行时可调用包装器 (RCW)。</summary>
    /// <returns>如果存在任何可清除的 RCW，则为 true；否则为 false。</returns>
    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern bool AreComObjectsAvailableForCleanup();

    /// <summary>指示指定对象是否表示 COM 对象。</summary>
    /// <returns>如果 <paramref name="o" /> 参数是 COM 类型，则为 true；否则为 false。</returns>
    /// <param name="o">要检查的对象。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern bool IsComObject(object o);

    /// <summary>从 COM 任务内存分配器分配指定大小的内存块。</summary>
    /// <returns>一个整数，表示分配的内存块的地址。该内存必须用 <see cref="M:System.Runtime.InteropServices.Marshal.FreeCoTaskMem(System.IntPtr)" /> 来释放。</returns>
    /// <param name="cb">要分配的内存块的大小。</param>
    /// <exception cref="T:System.OutOfMemoryException">内存不足，无法满足请求。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static IntPtr AllocCoTaskMem(int cb)
    {
      IntPtr num1 = Win32Native.CoTaskMemAlloc(new UIntPtr((uint) cb));
      IntPtr num2 = IntPtr.Zero;
      if (!(num1 == num2))
        return num1;
      throw new OutOfMemoryException();
    }

    /// <summary>将托管 <see cref="T:System.String" /> 的内容复制到从非托管 COM 任务分配器分配的内存块。</summary>
    /// <returns>一个整数，表示指向为字符串分配的内存块的指针；如果 s 为 null，则为 0。</returns>
    /// <param name="s">要复制的托管字符串。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="s" /> 参数超过了操作系统所允许的最大长度。</exception>
    /// <exception cref="T:System.OutOfMemoryException">没有足够的可用内存。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static unsafe IntPtr StringToCoTaskMemUni(string s)
    {
      if (s == null)
        return IntPtr.Zero;
      int num1 = (s.Length + 1) * 2;
      int length = s.Length;
      if (num1 < length)
        throw new ArgumentOutOfRangeException("s");
      IntPtr num2 = Win32Native.CoTaskMemAlloc(new UIntPtr((uint) num1));
      if (num2 == IntPtr.Zero)
        throw new OutOfMemoryException();
      string str = s;
      char* smem = (char*) str;
      if ((IntPtr) smem != IntPtr.Zero)
        smem += RuntimeHelpers.OffsetToStringData;
      string.wstrcpy((char*) (void*) num2, smem, s.Length + 1);
      str = (string) null;
      return num2;
    }

    /// <summary>将托管 <see cref="T:System.String" /> 的内容复制到从非托管 COM 任务分配器分配的内存块。</summary>
    /// <returns>已分配的内存块；如果 <paramref name="s" /> 为 null，则为 0。</returns>
    /// <param name="s">要复制的托管字符串。</param>
    /// <exception cref="T:System.OutOfMemoryException">没有足够的可用内存。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="s" /> 的长度超出范围。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static IntPtr StringToCoTaskMemAuto(string s)
    {
      return Marshal.StringToCoTaskMemUni(s);
    }

    /// <summary>将托管 <see cref="T:System.String" /> 的内容复制到从非托管 COM 任务分配器分配的内存块。</summary>
    /// <returns>一个整数，表示指向为字符串分配的内存块的指针；如果 <paramref name="s" /> 为 null，则为 0。</returns>
    /// <param name="s">要复制的托管字符串。</param>
    /// <exception cref="T:System.OutOfMemoryException">没有足够的可用内存。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="s" /> 参数超过了操作系统所允许的最大长度。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static unsafe IntPtr StringToCoTaskMemAnsi(string s)
    {
      if (s == null)
        return IntPtr.Zero;
      int cbNativeBuffer = (s.Length + 1) * Marshal.SystemMaxDBCSCharSize;
      if (cbNativeBuffer < s.Length)
        throw new ArgumentOutOfRangeException("s");
      IntPtr num = Win32Native.CoTaskMemAlloc(new UIntPtr((uint) cbNativeBuffer));
      if (num == IntPtr.Zero)
        throw new OutOfMemoryException();
      s.ConvertToAnsi((byte*) (void*) num, cbNativeBuffer, false, false);
      return num;
    }

    /// <summary>释放由非托管 COM 任务内存分配器分配的内存块。</summary>
    /// <param name="ptr">要释放的内存的地址。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static void FreeCoTaskMem(IntPtr ptr)
    {
      if (!Marshal.IsNotWin32Atom(ptr))
        return;
      Win32Native.CoTaskMemFree(ptr);
    }

    /// <summary>调整以前用 <see cref="M:System.Runtime.InteropServices.Marshal.AllocCoTaskMem(System.Int32)" /> 分配的内存块的大小。</summary>
    /// <returns>一个整数，表示重新分配的内存块的地址。该内存必须用 <see cref="M:System.Runtime.InteropServices.Marshal.FreeCoTaskMem(System.IntPtr)" /> 来释放。</returns>
    /// <param name="pv">指向用 <see cref="M:System.Runtime.InteropServices.Marshal.AllocCoTaskMem(System.Int32)" /> 分配的内存的指针。</param>
    /// <param name="cb">已分配块的新大小。</param>
    /// <exception cref="T:System.OutOfMemoryException">内存不足，无法满足请求。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static IntPtr ReAllocCoTaskMem(IntPtr pv, int cb)
    {
      IntPtr num1 = Win32Native.CoTaskMemRealloc(pv, new UIntPtr((uint) cb));
      IntPtr num2 = IntPtr.Zero;
      if (!(num1 == num2) || cb == 0)
        return num1;
      throw new OutOfMemoryException();
    }

    /// <summary>递减与指定的 COM 对象关联的指定 运行时可调用包装 (RCW) 的引用计数。</summary>
    /// <returns>与 <paramref name="o" /> 关联的 RCW 的引用计数的新值。此值通常为零，因为无论调用包装 COM 对象的托管客户端有多少，RCW 仅保留对该对象的一次引用。</returns>
    /// <param name="o">要释放的 COM 对象。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="o" /> 不是一个有效的 COM 对象。</exception>
    /// <exception cref="T:System.NullReferenceException">
    /// <paramref name="o" /> 为 null。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static int ReleaseComObject(object o)
    {
      __ComObject comObject;
      try
      {
        comObject = (__ComObject) o;
      }
      catch (InvalidCastException ex)
      {
        throw new ArgumentException(Environment.GetResourceString("Argument_ObjNotComObject"), "o");
      }
      return comObject.ReleaseSelf();
    }

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern int InternalReleaseComObject(object o);

    /// <summary>通过将 运行时可调用包装 (RCW) 的引用计数设置为 0，释放对它的所有引用。</summary>
    /// <returns>与 <paramref name="o" /> 参数关联的 RCW 的引用计数的新值，如果释放成功，则为 0（零）。</returns>
    /// <param name="o">要释放的 RCW。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="o" /> 不是一个有效的 COM 对象。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="o" /> 为 null。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static int FinalReleaseComObject(object o)
    {
      if (o == null)
        throw new ArgumentNullException("o");
      __ComObject comObject;
      try
      {
        comObject = (__ComObject) o;
      }
      catch (InvalidCastException ex)
      {
        throw new ArgumentException(Environment.GetResourceString("Argument_ObjNotComObject"), "o");
      }
      comObject.FinalReleaseSelf();
      return 0;
    }

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void InternalFinalReleaseComObject(object o);

    /// <summary>从指定的 COM 对象检索指定键所引用的数据。</summary>
    /// <returns>
    /// <paramref name="obj" /> 参数的内部哈希表中 <paramref name="key" /> 参数所表示的数据。</returns>
    /// <param name="obj">包含所需数据的 COM 对象。</param>
    /// <param name="key">要从中检索数据的 <paramref name="obj" /> 的内部哈希表中的键。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="obj" /> 为 null。- 或 - <paramref name="key" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="obj" /> 不是一个 COM 对象。- 或 -<paramref name="obj" /> 是 Windows 运行时 对象。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static object GetComObjectData(object obj, object key)
    {
      if (obj == null)
        throw new ArgumentNullException("obj");
      if (key == null)
        throw new ArgumentNullException("key");
      __ComObject comObject;
      try
      {
        comObject = (__ComObject) obj;
      }
      catch (InvalidCastException ex)
      {
        throw new ArgumentException(Environment.GetResourceString("Argument_ObjNotComObject"), "obj");
      }
      if (obj.GetType().IsWindowsRuntimeObject)
        throw new ArgumentException(Environment.GetResourceString("Argument_ObjIsWinRTObject"), "obj");
      return comObject.GetData(key);
    }

    /// <summary>设置由指定 COM 对象中的指定键引用的数据。</summary>
    /// <returns>如果数据设置成功，则为 true；否则为 false。</returns>
    /// <param name="obj">用于存储数据的 COM 对象。</param>
    /// <param name="key">用于存储数据的 COM 对象的内部哈希表中的键。</param>
    /// <param name="data">要设置的数据。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="obj" /> 为 null。- 或 - <paramref name="key" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="obj" /> 不是一个 COM 对象。- 或 -<paramref name="obj" /> 是 Windows 运行时 对象。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static bool SetComObjectData(object obj, object key, object data)
    {
      if (obj == null)
        throw new ArgumentNullException("obj");
      if (key == null)
        throw new ArgumentNullException("key");
      __ComObject comObject;
      try
      {
        comObject = (__ComObject) obj;
      }
      catch (InvalidCastException ex)
      {
        throw new ArgumentException(Environment.GetResourceString("Argument_ObjNotComObject"), "obj");
      }
      if (obj.GetType().IsWindowsRuntimeObject)
        throw new ArgumentException(Environment.GetResourceString("Argument_ObjIsWinRTObject"), "obj");
      return comObject.SetData(key, data);
    }

    /// <summary>在指定类型的对象中包装指定的 COM 对象。</summary>
    /// <returns>新包装的对象，该对象是所需类型的实例。</returns>
    /// <param name="o">要包装的对象。</param>
    /// <param name="t">要创建的包装器的类型。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="t" /> 必须从 __ComObject 派生。- 或 -<paramref name="t" /> 是一种 Windows 运行时 类型。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="t" /> 参数为 null。</exception>
    /// <exception cref="T:System.InvalidCastException">
    /// <paramref name="o" /> 无法转换为目标类型，因为它不支持所有所需的接口。 </exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static object CreateWrapperOfType(object o, Type t)
    {
      if (t == (Type) null)
        throw new ArgumentNullException("t");
      if (!t.IsCOMObject)
        throw new ArgumentException(Environment.GetResourceString("Argument_TypeNotComObject"), "t");
      if (t.IsGenericType)
        throw new ArgumentException(Environment.GetResourceString("Argument_NeedNonGenericType"), "t");
      if (t.IsWindowsRuntimeObject)
        throw new ArgumentException(Environment.GetResourceString("Argument_TypeIsWinRTType"), "t");
      if (o == null)
        return (object) null;
      if (!o.GetType().IsCOMObject)
        throw new ArgumentException(Environment.GetResourceString("Argument_ObjNotComObject"), "o");
      if (o.GetType().IsWindowsRuntimeObject)
        throw new ArgumentException(Environment.GetResourceString("Argument_ObjIsWinRTObject"), "o");
      if (o.GetType() == t)
        return o;
      object data = Marshal.GetComObjectData(o, (object) t);
      if (data == null)
      {
        data = Marshal.InternalCreateWrapperOfType(o, t);
        if (!Marshal.SetComObjectData(o, (object) t, data))
          data = Marshal.GetComObjectData(o, (object) t);
      }
      return data;
    }

    /// <summary>[在 .NET Framework 4.5.1 和更高版本中受支持]在指定类型的对象中包装指定的 COM 对象。</summary>
    /// <returns>新包装的对象。 </returns>
    /// <param name="o">要包装的对象。</param>
    /// <typeparam name="T">要包装的对象的类型。</typeparam>
    /// <typeparam name="TWrapper">要返回的对象的类型。</typeparam>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="T" /> 必须从 __ComObject 派生。- 或 -<paramref name="T" /> 是一种 Windows 运行时 类型。</exception>
    /// <exception cref="T:System.InvalidCastException">
    /// <paramref name="o" /> 无法转换为 <paramref name="TWrapper" />，因为它不支持所有需要的接口。 </exception>
    [SecurityCritical]
    public static TWrapper CreateWrapperOfType<T, TWrapper>(T o)
    {
      return (TWrapper) Marshal.CreateWrapperOfType((object) o, typeof (TWrapper));
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern object InternalCreateWrapperOfType(object o, Type t);

    /// <summary>释放线程缓存。</summary>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    [Obsolete("This API did not perform any operation and will be removed in future versions of the CLR.", false)]
    public static void ReleaseThreadCache()
    {
    }

    /// <summary>指示类型对 COM 客户端是否可见。</summary>
    /// <returns>如果该类型对 COM 是可见的，则为 true；否则为 false。</returns>
    /// <param name="t">要检查其 COM 可见性的类型。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern bool IsTypeVisibleFromCom(Type t);

    /// <summary>从 COM 对象请求指向指定接口的指针。</summary>
    /// <returns>一个 HRESULT，指示调用成功还是失败。</returns>
    /// <param name="pUnk">要查询的接口。</param>
    /// <param name="iid">所请求的接口的接口标识符 (IID)。</param>
    /// <param name="ppv">此方法返回时，包含对返回接口的引用。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern int QueryInterface(IntPtr pUnk, ref Guid iid, out IntPtr ppv);

    /// <summary>递增指定接口上的引用计数。</summary>
    /// <returns>
    /// <paramref name="pUnk" /> 参数上的引用计数的新值。</returns>
    /// <param name="pUnk">要递增的接口引用计数。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern int AddRef(IntPtr pUnk);

    /// <summary>递减指定接口上的引用计数。</summary>
    /// <returns>
    /// <paramref name="pUnk" /> 参数指定的接口上引用计数的新值。</returns>
    /// <param name="pUnk">要释放的接口。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern int Release(IntPtr pUnk);

    /// <summary>使用 COM SysFreeString 函数释放 BSTR。</summary>
    /// <param name="ptr">要释放的 BSTR 的地址。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    [__DynamicallyInvokable]
    public static void FreeBSTR(IntPtr ptr)
    {
      if (!Marshal.IsNotWin32Atom(ptr))
        return;
      Win32Native.SysFreeString(ptr);
    }

    /// <summary>分配 BSTR 并向其复制托管 <see cref="T:System.String" /> 的内容。</summary>
    /// <returns>指向 BSTR 的非托管指针；如果 <paramref name="s" /> 为 null，则为 0。</returns>
    /// <param name="s">要复制的托管字符串。</param>
    /// <exception cref="T:System.OutOfMemoryException">没有足够的可用内存。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="s" /> 的长度超出范围。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    [__DynamicallyInvokable]
    public static IntPtr StringToBSTR(string s)
    {
      if (s == null)
        return IntPtr.Zero;
      if (s.Length + 1 < s.Length)
        throw new ArgumentOutOfRangeException("s");
      string src = s;
      int length = src.Length;
      IntPtr num1 = Win32Native.SysAllocStringLen(src, length);
      IntPtr num2 = IntPtr.Zero;
      if (!(num1 == num2))
        return num1;
      throw new OutOfMemoryException();
    }

    /// <summary>分配托管 <see cref="T:System.String" />，并向其复制存储在非托管内存中的 BSTR 字符串。</summary>
    /// <returns>如果 <paramref name="ptr" /> 参数的值不是 null，则为包含非托管字符串副本的托管字符串；否则，此方法返回 null。</returns>
    /// <param name="ptr">非托管字符串的第一个字符的地址。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    [__DynamicallyInvokable]
    public static string PtrToStringBSTR(IntPtr ptr)
    {
      return Marshal.PtrToStringUni(ptr, (int) Win32Native.SysStringLen(ptr));
    }

    /// <summary>将对象转换为 COM VARIANT。</summary>
    /// <param name="obj">为其获取 COM VARIANT 的对象。</param>
    /// <param name="pDstNativeVariant">一个指针，接收对应于 <paramref name="obj" /> 参数的 VARIANT。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="obj" /> 参数是泛型类型。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern void GetNativeVariantForObject(object obj, IntPtr pDstNativeVariant);

    /// <summary>[在 .NET Framework 4.5.1 和更高版本中受支持]将指定类型的对象转换为 COM VARIANT。</summary>
    /// <param name="obj">为其获取 COM VARIANT 的对象。</param>
    /// <param name="pDstNativeVariant">一个指针，接收对应于 <paramref name="obj" /> 参数的 VARIANT。</param>
    /// <typeparam name="T">要转换的对象的类型。</typeparam>
    [SecurityCritical]
    public static void GetNativeVariantForObject<T>(T obj, IntPtr pDstNativeVariant)
    {
      Marshal.GetNativeVariantForObject((object) obj, pDstNativeVariant);
    }

    /// <summary>将 COM VARIANT 转换为对象。</summary>
    /// <returns>一个对象，对应于 <paramref name="pSrcNativeVariant" /> 参数。</returns>
    /// <param name="pSrcNativeVariant">指向 COM VARIANT 的指针。</param>
    /// <exception cref="T:System.Runtime.InteropServices.InvalidOleVariantTypeException">
    /// <paramref name="pSrcNativeVariant" /> 不是有效的 VARIANT 类型。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="pSrcNativeVariant" /> 包含不受支持的类型。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern object GetObjectForNativeVariant(IntPtr pSrcNativeVariant);

    /// <summary>[在 .NET Framework 4.5.1 和更高版本中受支持]将 COM VARIANT 转换为指定类型的对象。</summary>
    /// <returns>一个指定类型的对象，它与 <paramref name="pSrcNativeVariant" /> 参数对应。 </returns>
    /// <param name="pSrcNativeVariant">指向 COM VARIANT 的指针。</param>
    /// <typeparam name="T">要将 COM VARIANT 转换为的类型。</typeparam>
    /// <exception cref="T:System.Runtime.InteropServices.InvalidOleVariantTypeException">
    /// <paramref name="pSrcNativeVariant" /> 不是有效的 VARIANT 类型。 </exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="pSrcNativeVariant" /> 包含不受支持的类型。 </exception>
    [SecurityCritical]
    public static T GetObjectForNativeVariant<T>(IntPtr pSrcNativeVariant)
    {
      return (T) Marshal.GetObjectForNativeVariant(pSrcNativeVariant);
    }

    /// <summary>将 COM VARIANTs 数组转换为对象数组。</summary>
    /// <returns>一个对象数组，对应于 <paramref name="aSrcNativeVariant" />。</returns>
    /// <param name="aSrcNativeVariant">指向 COM VARIANT 数组中第一个元素的指针。</param>
    /// <param name="cVars">
    /// <paramref name="aSrcNativeVariant" /> 中的 COM VARIANT 的计数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="cVars" /> 是一个负数。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern object[] GetObjectsForNativeVariants(IntPtr aSrcNativeVariant, int cVars);

    /// <summary>[在 .NET Framework 4.5.1 和更高版本中受支持]将 COM VARIANT 数组转换为指定类型的数组。</summary>
    /// <returns>对应于 <paramref name="aSrcNativeVariant" /> 的 <paramref name="T" /> 对象的数组。 </returns>
    /// <param name="aSrcNativeVariant">指向 COM VARIANT 数组中第一个元素的指针。</param>
    /// <param name="cVars">
    /// <paramref name="aSrcNativeVariant" /> 中的 COM VARIANT 的计数。</param>
    /// <typeparam name="T">要返回的数组的类型。</typeparam>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="cVars" /> 是一个负数。 </exception>
    [SecurityCritical]
    public static T[] GetObjectsForNativeVariants<T>(IntPtr aSrcNativeVariant, int cVars)
    {
      object[] forNativeVariants = Marshal.GetObjectsForNativeVariants(aSrcNativeVariant, cVars);
      T[] objArray = (T[]) null;
      if (forNativeVariants != null)
      {
        objArray = new T[forNativeVariants.Length];
        Array.Copy((Array) forNativeVariants, (Array) objArray, forNativeVariants.Length);
      }
      return objArray;
    }

    /// <summary>获取虚拟功能表（v 表或 VTBL）中包含用户定义的方法的第一个槽。</summary>
    /// <returns>包含用户定义的方法的第一个 VTBL 槽。如果接口基于 IUnknown，则第一个槽为 3；如果接口基于 IDispatch，则为 7。</returns>
    /// <param name="t">表示接口的类型。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="t" /> 在 COM 中不可见。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern int GetStartComSlot(Type t);

    /// <summary>检索向 COM 公开时某个类型的虚拟功能表（v 表或 VTBL）中的最后一个槽。</summary>
    /// <returns>向 COM 公开时接口的最后一个 VTBL 槽。如果 <paramref name="t" /> 参数是类，则返回的 VTBL 槽是从该类生成的接口中的最后一个槽。</returns>
    /// <param name="t">表示接口或类的类型。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern int GetEndComSlot(Type t);

    /// <summary>检索指定的虚函数表（v 表或 VTBL）槽的 <see cref="T:System.Reflection.MemberInfo" /> 对象。</summary>
    /// <returns>表示指定 VTBL 槽上成员的对象。</returns>
    /// <param name="t">针对其检索 <see cref="T:System.Reflection.MemberInfo" /> 的类型。</param>
    /// <param name="slot">VTBL 槽。</param>
    /// <param name="memberType">在成功返回时，为指定成员类型的枚举值之一。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="t" /> 在 COM 中不可见。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern MemberInfo GetMethodInfoForComSlot(Type t, int slot, ref ComMemberType memberType);

    /// <summary>检索指定的 <see cref="T:System.Reflection.MemberInfo" /> 类型向 COM 公开时该类型的虚函数表（v 表或 VTBL）槽。</summary>
    /// <returns>向 COM 公开它时的 VTBL 槽 <paramref name="m" /> 标识符。</returns>
    /// <param name="m">表示接口方法的对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="m" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="m" /> 参数不是 <see cref="T:System.Reflection.MemberInfo" /> 对象。- 或 -参数 <paramref name="m" /> 不是接口方法。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static int GetComSlotForMethodInfo(MemberInfo m)
    {
      if (m == (MemberInfo) null)
        throw new ArgumentNullException("m");
      if (!(m is RuntimeMethodInfo))
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeMethodInfo"), "m");
      if (!m.DeclaringType.IsInterface)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeInterfaceMethod"), "m");
      if (m.DeclaringType.IsGenericType)
        throw new ArgumentException(Environment.GetResourceString("Argument_NeedNonGenericType"), "m");
      return Marshal.InternalGetComSlotForMethodInfo((IRuntimeMethodInfo) m);
    }

    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern int InternalGetComSlotForMethodInfo(IRuntimeMethodInfo m);

    /// <summary>返回指定类型的全局唯一标识符 (GUID)，或使用类型库导出程序 (Tlbexp.exe) 所用的算法生成 GUID。</summary>
    /// <returns>指定类型的标识符。</returns>
    /// <param name="type">要为其生成 GUID 的类型。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static Guid GenerateGuidForType(Type type)
    {
      Guid result = new Guid();
      Marshal.FCallGenerateGuidForType(ref result, type);
      return result;
    }

    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void FCallGenerateGuidForType(ref Guid result, Type type);

    /// <summary>返回指定类型的编程标识符 (ProgID)。</summary>
    /// <returns>指定类型的 ProgID。</returns>
    /// <param name="type">要获取其 ProgID 的类型。</param>
    /// <exception cref="T:System.ArgumentException">参数 <paramref name="type" /> 类无法由 COM 创建。该类必须是公共的，必须具有公共的默认构造函数，而且必须是 COM 可见的。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="type" /> 参数为 null。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static string GenerateProgIdForType(Type type)
    {
      if (type == (Type) null)
        throw new ArgumentNullException("type");
      if (type.IsImport)
        throw new ArgumentException(Environment.GetResourceString("Argument_TypeMustNotBeComImport"), "type");
      if (type.IsGenericType)
        throw new ArgumentException(Environment.GetResourceString("Argument_NeedNonGenericType"), "type");
      if (!RegistrationServices.TypeRequiresRegistrationHelper(type))
        throw new ArgumentException(Environment.GetResourceString("Argument_TypeMustBeComCreatable"), "type");
      IList<CustomAttributeData> customAttributes = CustomAttributeData.GetCustomAttributes((MemberInfo) type);
      for (int index = 0; index < customAttributes.Count; ++index)
      {
        if (customAttributes[index].Constructor.DeclaringType == typeof (ProgIdAttribute))
          return (string) customAttributes[index].ConstructorArguments[0].Value ?? string.Empty;
      }
      return type.FullName;
    }

    /// <summary>获取由指定的名字对象标识的接口指针。</summary>
    /// <returns>一个对象，它包含对由 <paramref name="monikerName" /> 参数标识的接口指针的引用。名字对象是一个名称，在此情况下，名字对象由接口定义。</returns>
    /// <param name="monikerName">与所需接口指针相对应的名字对象。</param>
    /// <exception cref="T:System.Runtime.InteropServices.COMException">非托管 BindToMoniker 方法返回一个无法识别的 HRESULT。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static object BindToMoniker(string monikerName)
    {
      object ppvResult = (object) null;
      IBindCtx ppbc = (IBindCtx) null;
      Marshal.CreateBindCtx(0U, out ppbc);
      IMoniker ppmk = (IMoniker) null;
      uint pchEaten;
      Marshal.MkParseDisplayName(ppbc, monikerName, out pchEaten, out ppmk);
      Marshal.BindMoniker(ppmk, 0U, ref Marshal.IID_IUnknown, out ppvResult);
      return ppvResult;
    }

    /// <summary>从运行对象表 (ROT) 获取指定对象的运行实例。</summary>
    /// <returns>所请求的对象；否则为 null。可将此对象转换为它支持的任何 COM 接口。</returns>
    /// <param name="progID">所请求的对象的编程标识符 (ProgID)。</param>
    /// <exception cref="T:System.Runtime.InteropServices.COMException">找不到该对象。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static object GetActiveObject(string progID)
    {
      object ppunk = (object) null;
      Guid clsid;
      try
      {
        Marshal.CLSIDFromProgIDEx(progID, out clsid);
      }
      catch (Exception ex)
      {
        Marshal.CLSIDFromProgID(progID, out clsid);
      }
      Marshal.GetActiveObject(ref clsid, IntPtr.Zero, out ppunk);
      return ppunk;
    }

    [SuppressUnmanagedCodeSecurity]
    [SecurityCritical]
    [DllImport("ole32.dll", PreserveSig = false)]
    private static extern void CLSIDFromProgIDEx([MarshalAs(UnmanagedType.LPWStr)] string progId, out Guid clsid);

    [SuppressUnmanagedCodeSecurity]
    [SecurityCritical]
    [DllImport("ole32.dll", PreserveSig = false)]
    private static extern void CLSIDFromProgID([MarshalAs(UnmanagedType.LPWStr)] string progId, out Guid clsid);

    [SuppressUnmanagedCodeSecurity]
    [SecurityCritical]
    [DllImport("ole32.dll", PreserveSig = false)]
    private static extern void CreateBindCtx(uint reserved, out IBindCtx ppbc);

    [SuppressUnmanagedCodeSecurity]
    [SecurityCritical]
    [DllImport("ole32.dll", PreserveSig = false)]
    private static extern void MkParseDisplayName(IBindCtx pbc, [MarshalAs(UnmanagedType.LPWStr)] string szUserName, out uint pchEaten, out IMoniker ppmk);

    [SuppressUnmanagedCodeSecurity]
    [SecurityCritical]
    [DllImport("ole32.dll", PreserveSig = false)]
    private static extern void BindMoniker(IMoniker pmk, uint grfOpt, ref Guid iidResult, [MarshalAs(UnmanagedType.Interface)] out object ppvResult);

    [SuppressUnmanagedCodeSecurity]
    [SecurityCritical]
    [DllImport("oleaut32.dll", PreserveSig = false)]
    private static extern void GetActiveObject(ref Guid rclsid, IntPtr reserved, [MarshalAs(UnmanagedType.Interface)] out object ppunk);

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool InternalSwitchCCW(object oldtp, object newtp);

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern object InternalWrapIUnknownWithComObject(IntPtr i);

    [SecurityCritical]
    private static IntPtr LoadLicenseManager()
    {
      Type type = Assembly.Load("System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089").GetType("System.ComponentModel.LicenseManager");
      if (type == (Type) null || !type.IsVisible)
        return IntPtr.Zero;
      return type.TypeHandle.Value;
    }

    /// <summary>更改对象的 COM 可调用包装 (CCW) 句柄的强度。</summary>
    /// <param name="otp">一个对象，其 CCW 包含带有引用计数的句柄。如果 CCW 上的引用计数大于零，则该句柄是强句柄；否则为弱句柄。</param>
    /// <param name="fIsWeak">为 true 时，忽略 <paramref name="otp" /> 的引用计数，将其句柄强度改为弱；为 false 时，重置要进行引用计数的 <paramref name="otp" /> 上的句柄强度。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern void ChangeWrapperHandleStrength(object otp, bool fIsWeak);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void InitializeWrapperForWinRT(object o, ref IntPtr pUnk);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void InitializeManagedWinRTFactoryObject(object o, RuntimeType runtimeClassType);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern object GetNativeActivationFactory(Type type);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void _GetInspectableIids(ObjectHandleOnStack obj, ObjectHandleOnStack guids);

    [SecurityCritical]
    internal static Guid[] GetInspectableIids(object obj)
    {
      Guid[] o1 = (Guid[]) null;
      __ComObject o2 = obj as __ComObject;
      if (o2 != null)
        Marshal._GetInspectableIids(JitHelpers.GetObjectHandleOnStack<__ComObject>(ref o2), JitHelpers.GetObjectHandleOnStack<Guid[]>(ref o1));
      return o1;
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void _GetCachedWinRTTypeByIid(ObjectHandleOnStack appDomainObj, Guid iid, out IntPtr rthHandle);

    [SecurityCritical]
    internal static Type GetCachedWinRTTypeByIid(AppDomain ad, Guid iid)
    {
      IntPtr rthHandle;
      Marshal._GetCachedWinRTTypeByIid(JitHelpers.GetObjectHandleOnStack<AppDomain>(ref ad), iid, out rthHandle);
      return (Type) Type.GetTypeFromHandleUnsafe(rthHandle);
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void _GetCachedWinRTTypes(ObjectHandleOnStack appDomainObj, ref int epoch, ObjectHandleOnStack winrtTypes);

    [SecurityCritical]
    internal static Type[] GetCachedWinRTTypes(AppDomain ad, ref int epoch)
    {
      IntPtr[] o = (IntPtr[]) null;
      Marshal._GetCachedWinRTTypes(JitHelpers.GetObjectHandleOnStack<AppDomain>(ref ad), ref epoch, JitHelpers.GetObjectHandleOnStack<IntPtr[]>(ref o));
      Type[] typeArray = new Type[o.Length];
      for (int index = 0; index < o.Length; ++index)
        typeArray[index] = (Type) Type.GetTypeFromHandleUnsafe(o[index]);
      return typeArray;
    }

    [SecurityCritical]
    internal static Type[] GetCachedWinRTTypes(AppDomain ad)
    {
      int epoch = 0;
      return Marshal.GetCachedWinRTTypes(ad, ref epoch);
    }

    /// <summary>将非托管函数指针转换为委托。</summary>
    /// <returns>可转换为适当的委托类型的委托实例。</returns>
    /// <param name="ptr">要转换的非托管函数指针。</param>
    /// <param name="t">要返回的委托的类型。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="t" /> 参数不是委托或泛型。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="ptr" /> 参数为 null。- 或 -<paramref name="t" /> 参数为 null。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static Delegate GetDelegateForFunctionPointer(IntPtr ptr, Type t)
    {
      if (ptr == IntPtr.Zero)
        throw new ArgumentNullException("ptr");
      if (t == (Type) null)
        throw new ArgumentNullException("t");
      if (t as RuntimeType == (RuntimeType) null)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"), "t");
      if (t.IsGenericType)
        throw new ArgumentException(Environment.GetResourceString("Argument_NeedNonGenericType"), "t");
      Type baseType = t.BaseType;
      if (baseType == (Type) null || baseType != typeof (Delegate) && baseType != typeof (MulticastDelegate))
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeDelegate"), "t");
      return Marshal.GetDelegateForFunctionPointerInternal(ptr, t);
    }

    /// <summary>[在 .NET Framework 4.5.1 和更高版本中受支持]将非托管函数指针转换为指定类型的委托。</summary>
    /// <returns>指定委托类型的实例。</returns>
    /// <param name="ptr">要转换的非托管函数指针。</param>
    /// <typeparam name="TDelegate">要返回的委托的类型。</typeparam>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="TDelegate" /> 泛型参数不是代理，或者它是开放式泛型类型。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="ptr" /> 参数为 null。</exception>
    [SecurityCritical]
    public static TDelegate GetDelegateForFunctionPointer<TDelegate>(IntPtr ptr)
    {
      return (TDelegate) Marshal.GetDelegateForFunctionPointer(ptr, typeof (TDelegate));
    }

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern Delegate GetDelegateForFunctionPointerInternal(IntPtr ptr, Type t);

    /// <summary>将委托转换为可从非托管代码调用的函数指针。</summary>
    /// <returns>一个可传递给非托管代码的值，非托管代码使用该值来调用基础托管委托。</returns>
    /// <param name="d">要传递给非托管代码的委托。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="d" /> 参数是泛型类型。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="d" /> 参数为 null。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static IntPtr GetFunctionPointerForDelegate(Delegate d)
    {
      if (d == null)
        throw new ArgumentNullException("d");
      return Marshal.GetFunctionPointerForDelegateInternal(d);
    }

    /// <summary>[在 .NET Framework 4.5.1 和更高版本中受支持]将指定类型的委托转换为可从非托管代码调用的函数指针。</summary>
    /// <returns>一个可传递给非托管代码的值，非托管代码使用该值来调用基础托管委托。</returns>
    /// <param name="d">要传递给非托管代码的委托。</param>
    /// <typeparam name="TDelegate">要转换的委托的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="d" /> 参数为 null。</exception>
    [SecurityCritical]
    public static IntPtr GetFunctionPointerForDelegate<TDelegate>(TDelegate d)
    {
      return Marshal.GetFunctionPointerForDelegate((Delegate) (object) d);
    }

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern IntPtr GetFunctionPointerForDelegateInternal(Delegate d);

    /// <summary>分配 BSTR 并向其复制托管 <see cref="T:System.Security.SecureString" /> 对象的内容。</summary>
    /// <returns>非托管内存中将 <paramref name="s" /> 参数复制到的地址；如果提供了 null 对象，则为 0。</returns>
    /// <param name="s">要复制的托管对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="s" /> 参数为 null。</exception>
    /// <exception cref="T:System.NotSupportedException">当前计算机运行的不是 Windows 2000 Service Pack 3 或更高版本。</exception>
    /// <exception cref="T:System.OutOfMemoryException">没有足够的可用内存。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static IntPtr SecureStringToBSTR(SecureString s)
    {
      if (s == null)
        throw new ArgumentNullException("s");
      return s.ToBSTR();
    }

    /// <summary>将托管 <see cref="T:System.Security.SecureString" /> 对象的内容复制到从非托管 COM 任务分配器分配的内存块。</summary>
    /// <returns>非托管内存中将 <paramref name="s" /> 参数复制到的地址；如果提供了 null 对象，则为 0。</returns>
    /// <param name="s">要复制的托管对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="s" /> 参数为 null。</exception>
    /// <exception cref="T:System.NotSupportedException">当前计算机运行的不是 Windows 2000 Service Pack 3 或更高版本。</exception>
    /// <exception cref="T:System.OutOfMemoryException">没有足够的可用内存。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static IntPtr SecureStringToCoTaskMemAnsi(SecureString s)
    {
      if (s == null)
        throw new ArgumentNullException("s");
      return s.ToAnsiStr(false);
    }

    /// <summary>将托管 <see cref="T:System.Security.SecureString" /> 对象的内容复制到从非托管 COM 任务分配器分配的内存块。</summary>
    /// <returns>非托管内存中将 <paramref name="s" /> 参数复制到的地址；如果提供了 null 对象，则为 0。</returns>
    /// <param name="s">要复制的托管对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="s" /> 参数为 null。</exception>
    /// <exception cref="T:System.NotSupportedException">当前计算机运行的不是 Windows 2000 Service Pack 3 或更高版本。</exception>
    /// <exception cref="T:System.OutOfMemoryException">没有足够的可用内存。</exception>
    [SecurityCritical]
    public static IntPtr SecureStringToCoTaskMemUnicode(SecureString s)
    {
      if (s == null)
        throw new ArgumentNullException("s");
      return s.ToUniStr(false);
    }

    /// <summary>释放 BSTR 指针，该指针是使用 <see cref="M:System.Runtime.InteropServices.Marshal.SecureStringToBSTR(System.Security.SecureString)" /> 方法分配的。</summary>
    /// <param name="s">要释放的 BSTR 的地址。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static void ZeroFreeBSTR(IntPtr s)
    {
      Win32Native.ZeroMemory(s, (UIntPtr) (Win32Native.SysStringLen(s) * 2U));
      Marshal.FreeBSTR(s);
    }

    /// <summary>释放非托管字符串指针，该指针是使用 <see cref="M:System.Runtime.InteropServices.Marshal.SecureStringToCoTaskMemAnsi(System.Security.SecureString)" /> 方法分配的。</summary>
    /// <param name="s">要释放的非托管字符串的地址。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static void ZeroFreeCoTaskMemAnsi(IntPtr s)
    {
      Win32Native.ZeroMemory(s, (UIntPtr) ((ulong) Win32Native.lstrlenA(s)));
      Marshal.FreeCoTaskMem(s);
    }

    /// <summary>释放非托管字符串指针，该指针是使用 <see cref="M:System.Runtime.InteropServices.Marshal.SecureStringToCoTaskMemUnicode(System.Security.SecureString)" /> 方法分配的。</summary>
    /// <param name="s">要释放的非托管字符串的地址。</param>
    [SecurityCritical]
    public static void ZeroFreeCoTaskMemUnicode(IntPtr s)
    {
      Win32Native.ZeroMemory(s, (UIntPtr) ((ulong) (Win32Native.lstrlenW(s) * 2)));
      Marshal.FreeCoTaskMem(s);
    }

    /// <summary>将托管 <see cref="T:System.Security.SecureString" /> 中的内容复制到非托管内存，并在复制时转换为 ANSI 格式。</summary>
    /// <returns>非托管内存中将 <paramref name="s" /> 参数复制到的地址，如果提供了 null 对象，则为 0。</returns>
    /// <param name="s">要复制的托管对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="s" /> 参数为 null。</exception>
    /// <exception cref="T:System.NotSupportedException">当前计算机运行的不是 Windows 2000 Service Pack 3 或更高版本。</exception>
    /// <exception cref="T:System.OutOfMemoryException">没有足够的可用内存。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static IntPtr SecureStringToGlobalAllocAnsi(SecureString s)
    {
      if (s == null)
        throw new ArgumentNullException("s");
      return s.ToAnsiStr(true);
    }

    /// <summary>向非托管内存复制托管 <see cref="T:System.Security.SecureString" /> 对象的内容。</summary>
    /// <returns>复制了 <paramref name="s" /> 的非托管内存中的地址，如果 <paramref name="s" /> 是个长度为 0 的 <see cref="T:System.Security.SecureString" /> 对象，则为 0。</returns>
    /// <param name="s">要复制的托管对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="s" /> 参数为 null。</exception>
    /// <exception cref="T:System.NotSupportedException">当前计算机运行的不是 Windows 2000 Service Pack 3 或更高版本。</exception>
    /// <exception cref="T:System.OutOfMemoryException">没有足够的可用内存。</exception>
    [SecurityCritical]
    public static IntPtr SecureStringToGlobalAllocUnicode(SecureString s)
    {
      if (s == null)
        throw new ArgumentNullException("s");
      return s.ToUniStr(true);
    }

    /// <summary>释放非托管字符串指针，该指针是使用 <see cref="M:System.Runtime.InteropServices.Marshal.SecureStringToGlobalAllocAnsi(System.Security.SecureString)" /> 方法分配的。</summary>
    /// <param name="s">要释放的非托管字符串的地址。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static void ZeroFreeGlobalAllocAnsi(IntPtr s)
    {
      Win32Native.ZeroMemory(s, (UIntPtr) ((ulong) Win32Native.lstrlenA(s)));
      Marshal.FreeHGlobal(s);
    }

    /// <summary>释放非托管字符串指针，该指针是使用 <see cref="M:System.Runtime.InteropServices.Marshal.SecureStringToGlobalAllocUnicode(System.Security.SecureString)" /> 方法分配的。</summary>
    /// <param name="s">要释放的非托管字符串的地址。</param>
    [SecurityCritical]
    public static void ZeroFreeGlobalAllocUnicode(IntPtr s)
    {
      Win32Native.ZeroMemory(s, (UIntPtr) ((ulong) (Win32Native.lstrlenW(s) * 2)));
      Marshal.FreeHGlobal(s);
    }
  }
}
