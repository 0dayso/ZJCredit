// Decompiled with JetBrains decompiler
// Type: System.BitConverter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;

namespace System
{
  /// <summary>将基础数据类型与字节数组相互转换。</summary>
  /// <filterpriority>2</filterpriority>
  [__DynamicallyInvokable]
  public static class BitConverter
  {
    /// <summary>指示数据在此计算机结构中存储时的字节顺序（“Endian”性质）。</summary>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static readonly bool IsLittleEndian = true;

    /// <summary>以字节数组的形式返回指定的布尔值。</summary>
    /// <returns>长度为 1 的字节数组。</returns>
    /// <param name="value">一个布尔值。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static byte[] GetBytes(bool value)
    {
      return new byte[1]
      {
        (byte) (value ? 1 : 0)
      };
    }

    /// <summary>以字节数组的形式返回指定的 Unicode 字符值。</summary>
    /// <returns>长度为 2 的字节数组。</returns>
    /// <param name="value">要转换的字符。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static byte[] GetBytes(char value)
    {
      return BitConverter.GetBytes((short) value);
    }

    /// <summary>以字节数组的形式返回指定的 16 位有符号整数值。</summary>
    /// <returns>长度为 2 的字节数组。</returns>
    /// <param name="value">要转换的数字。</param>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static unsafe byte[] GetBytes(short value)
    {
      byte[] numArray1;
      byte[] numArray2 = numArray1 = new byte[2];
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      // ISSUE: explicit reference operation
      fixed (byte* numPtr = &^(numArray1 == null || numArray2.Length == 0 ? (byte&) IntPtr.Zero : @numArray2[0]))
        *(short*) numPtr = value;
      return numArray1;
    }

    /// <summary>以字节数组的形式返回指定的 32 位有符号整数值。</summary>
    /// <returns>长度为 4 的字节数组。</returns>
    /// <param name="value">要转换的数字。</param>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static unsafe byte[] GetBytes(int value)
    {
      byte[] numArray1;
      byte[] numArray2 = numArray1 = new byte[4];
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      // ISSUE: explicit reference operation
      fixed (byte* numPtr = &^(numArray1 == null || numArray2.Length == 0 ? (byte&) IntPtr.Zero : @numArray2[0]))
        *(int*) numPtr = value;
      return numArray1;
    }

    /// <summary>以字节数组的形式返回指定的 64 位有符号整数值。</summary>
    /// <returns>长度为 8 的字节数组。</returns>
    /// <param name="value">要转换的数字。</param>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static unsafe byte[] GetBytes(long value)
    {
      byte[] numArray1;
      byte[] numArray2 = numArray1 = new byte[8];
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      // ISSUE: explicit reference operation
      fixed (byte* numPtr = &^(numArray1 == null || numArray2.Length == 0 ? (byte&) IntPtr.Zero : @numArray2[0]))
        *(long*) numPtr = value;
      return numArray1;
    }

    /// <summary>以字节数组的形式返回指定的 16 位无符号整数值。</summary>
    /// <returns>长度为 2 的字节数组。</returns>
    /// <param name="value">要转换的数字。</param>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static byte[] GetBytes(ushort value)
    {
      return BitConverter.GetBytes((short) value);
    }

    /// <summary>以字节数组的形式返回指定的 32 位无符号整数值。</summary>
    /// <returns>长度为 4 的字节数组。</returns>
    /// <param name="value">要转换的数字。</param>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static byte[] GetBytes(uint value)
    {
      return BitConverter.GetBytes((int) value);
    }

    /// <summary>以字节数组的形式返回指定的 64 位无符号整数值。</summary>
    /// <returns>长度为 8 的字节数组。</returns>
    /// <param name="value">要转换的数字。</param>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static byte[] GetBytes(ulong value)
    {
      return BitConverter.GetBytes((long) value);
    }

    /// <summary>以字节数组的形式返回指定的单精度浮点值。</summary>
    /// <returns>长度为 4 的字节数组。</returns>
    /// <param name="value">要转换的数字。</param>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static unsafe byte[] GetBytes(float value)
    {
      return BitConverter.GetBytes(*(int*) &value);
    }

    /// <summary>以字节数组的形式返回指定的双精度浮点值。</summary>
    /// <returns>长度为 8 的字节数组。</returns>
    /// <param name="value">要转换的数字。</param>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static unsafe byte[] GetBytes(double value)
    {
      return BitConverter.GetBytes(*(long*) &value);
    }

    /// <summary>返回由字节数组中指定位置的两个字节转换来的 Unicode 字符。</summary>
    /// <returns>由两个字节构成、从 <paramref name="startIndex" /> 开始的字符。</returns>
    /// <param name="value">数组。</param>
    /// <param name="startIndex">
    /// <paramref name="value" /> 内的起始位置。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="startIndex" /> 等于 <paramref name="value" /> 减 1 的长度。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="startIndex" /> 小于零或大于 <paramref name="value" /> 减 1 的长度。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static char ToChar(byte[] value, int startIndex)
    {
      if (value == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.value);
      if ((long) (uint) startIndex >= (long) value.Length)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.startIndex, ExceptionResource.ArgumentOutOfRange_Index);
      if (startIndex > value.Length - 2)
        ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
      return (char) BitConverter.ToInt16(value, startIndex);
    }

    /// <summary>返回由字节数组中指定位置的两个字节转换来的 16 位有符号整数。</summary>
    /// <returns>由两个字节构成、从 <paramref name="startIndex" /> 开始的 16 位有符号整数。</returns>
    /// <param name="value">字节数组。</param>
    /// <param name="startIndex">
    /// <paramref name="value" /> 内的起始位置。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="startIndex" /> 等于 <paramref name="value" /> 减 1 的长度。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="startIndex" /> 小于零或大于 <paramref name="value" /> 减 1 的长度。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static unsafe short ToInt16(byte[] value, int startIndex)
    {
      if (value == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.value);
      if ((long) (uint) startIndex >= (long) value.Length)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.startIndex, ExceptionResource.ArgumentOutOfRange_Index);
      if (startIndex > value.Length - 2)
        ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
      fixed (byte* numPtr = &value[startIndex])
      {
        if (startIndex % 2 == 0)
          return *(short*) numPtr;
        if (BitConverter.IsLittleEndian)
          return (short) ((int) *numPtr | (int) numPtr[1] << 8);
        return (short) ((int) *numPtr << 8 | (int) numPtr[1]);
      }
    }

    /// <summary>返回由字节数组中指定位置的四个字节转换来的 32 位有符号整数。</summary>
    /// <returns>由四个字节构成、从 <paramref name="startIndex" /> 开始的 32 位有符号整数。</returns>
    /// <param name="value">字节数组。</param>
    /// <param name="startIndex">
    /// <paramref name="value" /> 内的起始位置。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="startIndex" /> 大于等于 <paramref name="value" /> 减 3 的长度，且小于等于 <paramref name="value" /> 减 1 的长度。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="startIndex" /> 小于零或大于 <paramref name="value" /> 减 1 的长度。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static unsafe int ToInt32(byte[] value, int startIndex)
    {
      if (value == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.value);
      if ((long) (uint) startIndex >= (long) value.Length)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.startIndex, ExceptionResource.ArgumentOutOfRange_Index);
      if (startIndex > value.Length - 4)
        ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
      fixed (byte* numPtr = &value[startIndex])
      {
        if (startIndex % 4 == 0)
          return *(int*) numPtr;
        if (BitConverter.IsLittleEndian)
          return (int) *numPtr | (int) numPtr[1] << 8 | (int) numPtr[2] << 16 | (int) numPtr[3] << 24;
        return (int) *numPtr << 24 | (int) numPtr[1] << 16 | (int) numPtr[2] << 8 | (int) numPtr[3];
      }
    }

    /// <summary>返回由字节数组中指定位置的八个字节转换来的 64 位有符号整数。</summary>
    /// <returns>由八个字节构成、从 <paramref name="startIndex" /> 开始的 64 位有符号整数。</returns>
    /// <param name="value">字节数组。</param>
    /// <param name="startIndex">
    /// <paramref name="value" /> 内的起始位置。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="startIndex" /> 大于等于 <paramref name="value" /> 减 7 的长度，且小于等于 <paramref name="value" /> 减 1 的长度。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="startIndex" /> 小于零或大于 <paramref name="value" /> 减 1 的长度。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static unsafe long ToInt64(byte[] value, int startIndex)
    {
      if (value == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.value);
      if ((long) (uint) startIndex >= (long) value.Length)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.startIndex, ExceptionResource.ArgumentOutOfRange_Index);
      if (startIndex > value.Length - 8)
        ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
      fixed (byte* numPtr = &value[startIndex])
      {
        if (startIndex % 8 == 0)
          return *(long*) numPtr;
        if (BitConverter.IsLittleEndian)
          return (long) (uint) ((int) *numPtr | (int) numPtr[1] << 8 | (int) numPtr[2] << 16 | (int) numPtr[3] << 24) | (long) ((int) numPtr[4] | (int) numPtr[5] << 8 | (int) numPtr[6] << 16 | (int) numPtr[7] << 24) << 32;
        int num = (int) *numPtr << 24 | (int) numPtr[1] << 16 | (int) numPtr[2] << 8 | (int) numPtr[3];
        return (long) ((uint) ((int) numPtr[4] << 24 | (int) numPtr[5] << 16 | (int) numPtr[6] << 8) | (uint) numPtr[7]) | (long) num << 32;
      }
    }

    /// <summary>返回由字节数组中指定位置的两个字节转换来的 16 位无符号整数。</summary>
    /// <returns>由两个字节构成、从 <paramref name="startIndex" /> 开始的 16 位无符号整数。</returns>
    /// <param name="value">字节数组。</param>
    /// <param name="startIndex">
    /// <paramref name="value" /> 内的起始位置。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="startIndex" /> 等于 <paramref name="value" /> 减 1 的长度。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="startIndex" /> 小于零或大于 <paramref name="value" /> 减 1 的长度。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ushort ToUInt16(byte[] value, int startIndex)
    {
      if (value == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.value);
      if ((long) (uint) startIndex >= (long) value.Length)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.startIndex, ExceptionResource.ArgumentOutOfRange_Index);
      if (startIndex > value.Length - 2)
        ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
      return (ushort) BitConverter.ToInt16(value, startIndex);
    }

    /// <summary>返回由字节数组中指定位置的四个字节转换来的 32 位无符号整数。</summary>
    /// <returns>由四个字节构成、从 <paramref name="startIndex" /> 开始的 32 位无符号整数。</returns>
    /// <param name="value">字节数组。</param>
    /// <param name="startIndex">
    /// <paramref name="value" /> 内的起始位置。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="startIndex" /> 大于等于 <paramref name="value" /> 减 3 的长度，且小于等于 <paramref name="value" /> 减 1 的长度。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="startIndex" /> 小于零或大于 <paramref name="value" /> 减 1 的长度。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static uint ToUInt32(byte[] value, int startIndex)
    {
      if (value == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.value);
      if ((long) (uint) startIndex >= (long) value.Length)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.startIndex, ExceptionResource.ArgumentOutOfRange_Index);
      if (startIndex > value.Length - 4)
        ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
      return (uint) BitConverter.ToInt32(value, startIndex);
    }

    /// <summary>返回由字节数组中指定位置的八个字节转换来的 64 位无符号整数。</summary>
    /// <returns>由八个字节构成、从 <paramref name="startIndex" /> 开始的 64 位无符号整数。</returns>
    /// <param name="value">字节数组。</param>
    /// <param name="startIndex">
    /// <paramref name="value" /> 内的起始位置。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="startIndex" /> 大于等于 <paramref name="value" /> 减 7 的长度，且小于等于 <paramref name="value" /> 减 1 的长度。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="startIndex" /> 小于零或大于 <paramref name="value" /> 减 1 的长度。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ulong ToUInt64(byte[] value, int startIndex)
    {
      if (value == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.value);
      if ((long) (uint) startIndex >= (long) value.Length)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.startIndex, ExceptionResource.ArgumentOutOfRange_Index);
      if (startIndex > value.Length - 8)
        ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
      return (ulong) BitConverter.ToInt64(value, startIndex);
    }

    /// <summary>返回由字节数组中指定位置的四个字节转换来的单精度浮点数。</summary>
    /// <returns>由四个字节构成、从 <paramref name="startIndex" /> 开始的单精度浮点数。</returns>
    /// <param name="value">字节数组。</param>
    /// <param name="startIndex">
    /// <paramref name="value" /> 内的起始位置。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="startIndex" /> 大于等于 <paramref name="value" /> 减 3 的长度，且小于等于 <paramref name="value" /> 减 1 的长度。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="startIndex" /> 小于零或大于 <paramref name="value" /> 减 1 的长度。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static unsafe float ToSingle(byte[] value, int startIndex)
    {
      if (value == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.value);
      if ((long) (uint) startIndex >= (long) value.Length)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.startIndex, ExceptionResource.ArgumentOutOfRange_Index);
      if (startIndex > value.Length - 4)
        ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
      return *(float*) &BitConverter.ToInt32(value, startIndex);
    }

    /// <summary>返回由字节数组中指定位置的八个字节转换来的双精度浮点数。</summary>
    /// <returns>由八个字节构成、从 <paramref name="startIndex" /> 开始的双精度浮点数。</returns>
    /// <param name="value">字节数组。</param>
    /// <param name="startIndex">
    /// <paramref name="value" /> 内的起始位置。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="startIndex" /> 大于等于 <paramref name="value" /> 减 7 的长度，且小于等于 <paramref name="value" /> 减 1 的长度。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="startIndex" /> 小于零或大于 <paramref name="value" /> 减 1 的长度。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static unsafe double ToDouble(byte[] value, int startIndex)
    {
      if (value == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.value);
      if ((long) (uint) startIndex >= (long) value.Length)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.startIndex, ExceptionResource.ArgumentOutOfRange_Index);
      if (startIndex > value.Length - 8)
        ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
      return *(double*) &BitConverter.ToInt64(value, startIndex);
    }

    private static char GetHexValue(int i)
    {
      if (i < 10)
        return (char) (i + 48);
      return (char) (i - 10 + 65);
    }

    /// <summary>将指定的字节子数组的每个元素的数值转换为它的等效十六进制字符串表示形式。</summary>
    /// <returns>由以连字符分隔的十六进制对构成的字符串，其中每一对表示 <paramref name="value" /> 的子数组中对应的元素；例如“7F-2C-4A”。</returns>
    /// <param name="value">字节数组。</param>
    /// <param name="startIndex">
    /// <paramref name="value" /> 内的起始位置。</param>
    /// <param name="length">要转换的 <paramref name="value" /> 中的数组元素数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="startIndex" /> 或 <paramref name="length" /> 小于零。- 或 -<paramref name="startIndex" /> 大于零且大于等于 <paramref name="value" /> 的长度。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="startIndex" /> 和 <paramref name="length" /> 的组合不指定 <paramref name="value" /> 中的位置；也就是说，<paramref name="startIndex" /> 参数大于 <paramref name="value" /> 的长度减去 <paramref name="length" /> 参数。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static string ToString(byte[] value, int startIndex, int length)
    {
      if (value == null)
        throw new ArgumentNullException("byteArray");
      if (startIndex < 0 || startIndex >= value.Length && startIndex > 0)
        throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_StartIndex"));
      if (length < 0)
        throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("ArgumentOutOfRange_GenericPositive"));
      if (startIndex > value.Length - length)
        throw new ArgumentException(Environment.GetResourceString("Arg_ArrayPlusOffTooSmall"));
      if (length == 0)
        return string.Empty;
      if (length > 715827882)
        throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("ArgumentOutOfRange_LengthTooLarge", (object) 715827882));
      int length1 = length * 3;
      char[] chArray = new char[length1];
      int num1 = startIndex;
      int index = 0;
      while (index < length1)
      {
        byte num2 = value[num1++];
        chArray[index] = BitConverter.GetHexValue((int) num2 / 16);
        chArray[index + 1] = BitConverter.GetHexValue((int) num2 % 16);
        chArray[index + 2] = '-';
        index += 3;
      }
      return new string(chArray, 0, chArray.Length - 1);
    }

    /// <summary>将指定的字节数组的每个元素的数值转换为它的等效十六进制字符串表示形式。</summary>
    /// <returns>由以连字符分隔的十六进制对构成的字符串，其中每一对表示 <paramref name="value" /> 中对应的元素；例如“7F-2C-4A”。</returns>
    /// <param name="value">字节数组。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> 为 null。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static string ToString(byte[] value)
    {
      if (value == null)
        throw new ArgumentNullException("value");
      return BitConverter.ToString(value, 0, value.Length);
    }

    /// <summary>将指定的字节子数组的每个元素的数值转换为它的等效十六进制字符串表示形式。</summary>
    /// <returns>由以连字符分隔的十六进制对构成的字符串，其中每一对表示 <paramref name="value" /> 的子数组中对应的元素；例如“7F-2C-4A”。</returns>
    /// <param name="value">字节数组。</param>
    /// <param name="startIndex">
    /// <paramref name="value" /> 内的起始位置。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="startIndex" /> 小于零或大于 <paramref name="value" /> 减 1 的长度。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static string ToString(byte[] value, int startIndex)
    {
      if (value == null)
        throw new ArgumentNullException("value");
      return BitConverter.ToString(value, startIndex, value.Length - startIndex);
    }

    /// <summary>返回由字节数组中指定位置的一个字节转换来的布尔值。</summary>
    /// <returns>如果 <paramref name="value" /> 中的 <paramref name="startIndex" /> 处的字节非零，则为 true；否则为 false。</returns>
    /// <param name="value">字节数组。</param>
    /// <param name="startIndex">
    /// <paramref name="value" /> 内的起始位置。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="startIndex" /> 小于零或大于 <paramref name="value" /> 减 1 的长度。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static bool ToBoolean(byte[] value, int startIndex)
    {
      if (value == null)
        throw new ArgumentNullException("value");
      if (startIndex < 0)
        throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (startIndex > value.Length - 1)
        throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      return (int) value[startIndex] != 0;
    }

    /// <summary>将指定的双精度浮点数转换为 64 位有符号整数。</summary>
    /// <returns>64 位有符号整数，其值等于 <paramref name="value" />。</returns>
    /// <param name="value">要转换的数字。</param>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static unsafe long DoubleToInt64Bits(double value)
    {
      return *(long*) &value;
    }

    /// <summary>将指定的 64 位有符号整数转换成双精度浮点数。</summary>
    /// <returns>双精度浮点数，其值等于 <paramref name="value" />。</returns>
    /// <param name="value">要转换的数字。</param>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static unsafe double Int64BitsToDouble(long value)
    {
      return *(double*) &value;
    }
  }
}
