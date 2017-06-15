// Decompiled with JetBrains decompiler
// Type: System.Array
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security;
using System.Security.Permissions;

namespace System
{
  /// <summary>提供一些方法，用于创建、处理、搜索数组并对数组进行排序，从而充当公共语言运行时中所有数组的基类。若要浏览此类型的.NET Framework 源代码，请参阅参考源。</summary>
  /// <filterpriority>1</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public abstract class Array : ICloneable, IList, ICollection, IEnumerable, IStructuralComparable, IStructuralEquatable
  {
    internal const int MaxArrayLength = 2146435071;
    internal const int MaxByteArrayLength = 2147483591;

    /// <summary>获得一个 32 位整数，该整数表示 <see cref="T:System.Array" /> 的所有维数中元素的总数。</summary>
    /// <returns>一个 32 位整数，该整数表示 <see cref="T:System.Array" /> 的所有维数中的元素总数；如果数组中没有元素，则该值为零。</returns>
    /// <exception cref="T:System.OverflowException">数组是多维的，并且包括多于 <see cref="F:System.Int32.MaxValue" /> 个元素。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public int Length { [SecuritySafeCritical, ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success), __DynamicallyInvokable, MethodImpl(MethodImplOptions.InternalCall)] get; }

    /// <summary>获得一个 64 位整数，该整数表示 <see cref="T:System.Array" /> 的所有维数中元素的总数。</summary>
    /// <returns>一个 64 位整数，表示 <see cref="T:System.Array" /> 的所有维数中元素的总数。</returns>
    /// <filterpriority>2</filterpriority>
    [ComVisible(false)]
    public long LongLength { [SecuritySafeCritical, ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success), MethodImpl(MethodImplOptions.InternalCall)] get; }

    /// <summary>获取 <see cref="T:System.Array" /> 的秩（维数）。例如，一维数组返回 1，二维数组返回 2，依次类推。</summary>
    /// <returns>该 <see cref="T:System.Array" /> 的秩（维数）。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public int Rank { [SecuritySafeCritical, ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success), __DynamicallyInvokable, MethodImpl(MethodImplOptions.InternalCall)] get; }

    [__DynamicallyInvokable]
    int ICollection.Count
    {
      [__DynamicallyInvokable] get
      {
        return this.Length;
      }
    }

    /// <summary>获取一个可用于同步对 <see cref="T:System.Array" /> 的访问的对象。</summary>
    /// <returns>一个可用于同步对 <see cref="T:System.Array" /> 的访问的对象。</returns>
    /// <filterpriority>2</filterpriority>
    public object SyncRoot
    {
      get
      {
        return (object) this;
      }
    }

    /// <summary>获取一个值，该值指示 <see cref="T:System.Array" /> 是否为只读。</summary>
    /// <returns>此属性对于所有数组总是 false。</returns>
    /// <filterpriority>2</filterpriority>
    public bool IsReadOnly
    {
      get
      {
        return false;
      }
    }

    /// <summary>获取一个值，该值指示 <see cref="T:System.Array" /> 是否具有固定大小。</summary>
    /// <returns>此属性对于所有数组总是 true。</returns>
    /// <filterpriority>2</filterpriority>
    public bool IsFixedSize
    {
      get
      {
        return true;
      }
    }

    /// <summary>获取一个值，该值指示是否同步对 <see cref="T:System.Array" /> 的访问（线程安全）。</summary>
    /// <returns>此属性对于所有数组总是 false。</returns>
    /// <filterpriority>2</filterpriority>
    public bool IsSynchronized
    {
      get
      {
        return false;
      }
    }

    [__DynamicallyInvokable]
    object IList.this[int index]
    {
      [__DynamicallyInvokable] get
      {
        return this.GetValue(index);
      }
      [__DynamicallyInvokable] set
      {
        this.SetValue(value, index);
      }
    }

    internal Array()
    {
    }

    /// <summary>返回指定数组的只读包装。</summary>
    /// <returns>指定数组的只读 <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> 包装。</returns>
    /// <param name="array">要包装在只读 <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> 包装中的从零开始的一维数组。</param>
    /// <typeparam name="T">数组元素的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> 为 null。</exception>
    public static ReadOnlyCollection<T> AsReadOnly<T>(T[] array)
    {
      if (array == null)
        throw new ArgumentNullException("array");
      return new ReadOnlyCollection<T>((IList<T>) array);
    }

    /// <summary>将一维数组的元素数更改为指定的新大小。</summary>
    /// <param name="array">要调整大小的一维数组，该数组从零开始；如果为 null 则新建具有指定大小的数组。</param>
    /// <param name="newSize">新数组的大小。</param>
    /// <typeparam name="T">数组元素的类型。</typeparam>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="newSize" /> 小于零。</exception>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public static void Resize<T>(ref T[] array, int newSize)
    {
      if (newSize < 0)
        throw new ArgumentOutOfRangeException("newSize", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      T[] objArray1 = array;
      if (objArray1 == null)
      {
        array = new T[newSize];
      }
      else
      {
        if (objArray1.Length == newSize)
          return;
        T[] objArray2 = new T[newSize];
        Array.Copy((Array) objArray1, 0, (Array) objArray2, 0, objArray1.Length > newSize ? newSize : objArray1.Length);
        array = objArray2;
      }
    }

    /// <summary>创建使用从零开始的索引、具有指定 <see cref="T:System.Type" /> 和长度的一维 <see cref="T:System.Array" />。</summary>
    /// <returns>使用从零开始的索引、具有指定 <see cref="T:System.Type" /> 和指定长度的新的一维 <see cref="T:System.Array" />。</returns>
    /// <param name="elementType">要创建的 <see cref="T:System.Array" /> 的 <see cref="T:System.Type" />。</param>
    /// <param name="length">要创建的 <see cref="T:System.Array" /> 的大小。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="elementType" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="elementType" /> 不是有效的 <see cref="T:System.Type" />。</exception>
    /// <exception cref="T:System.NotSupportedException">不支持 <paramref name="elementType" />。例如，不支持 <see cref="T:System.Void" />。- 或 -<paramref name="elementType" /> 是一个开放式泛型类型。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="length" /> 小于零。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static unsafe Array CreateInstance(Type elementType, int length)
    {
      if (elementType == null)
        throw new ArgumentNullException("elementType");
      if (length < 0)
        throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      RuntimeType runtimeType = elementType.UnderlyingSystemType as RuntimeType;
      // ISSUE: variable of the null type
      __Null local = null;
      if (runtimeType == (RuntimeType) local)
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "elementType");
      return Array.InternalCreate((void*) runtimeType.TypeHandle.Value, 1, &length, (int*) null);
    }

    /// <summary>创建使用从零开始的索引、具有指定 <see cref="T:System.Type" /> 和维长的二维 <see cref="T:System.Array" />。</summary>
    /// <returns>使用从零开始的索引、具有指定 <see cref="T:System.Type" /> 的新的二维 <see cref="T:System.Array" />，其每个维度都为指定的长度。</returns>
    /// <param name="elementType">要创建的 <see cref="T:System.Array" /> 的 <see cref="T:System.Type" />。</param>
    /// <param name="length1">要创建的 <see cref="T:System.Array" /> 的第一维的大小。</param>
    /// <param name="length2">要创建的 <see cref="T:System.Array" /> 的第二维的大小。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="elementType" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="elementType" /> 不是有效的 <see cref="T:System.Type" />。</exception>
    /// <exception cref="T:System.NotSupportedException">不支持 <paramref name="elementType" />。例如，不支持 <see cref="T:System.Void" />。- 或 -<paramref name="elementType" /> 是一个开放式泛型类型。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="length1" /> 小于零。- 或 -<paramref name="length2" /> 小于零。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    public static unsafe Array CreateInstance(Type elementType, int length1, int length2)
    {
      if (elementType == null)
        throw new ArgumentNullException("elementType");
      if (length1 < 0 || length2 < 0)
        throw new ArgumentOutOfRangeException(length1 < 0 ? "length1" : "length2", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      RuntimeType runtimeType = elementType.UnderlyingSystemType as RuntimeType;
      if (runtimeType == (RuntimeType) null)
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "elementType");
      int* pLengths = stackalloc int[2];
      pLengths[0] = length1;
      *(int*) ((IntPtr) pLengths + 4) = length2;
      return Array.InternalCreate((void*) runtimeType.TypeHandle.Value, 2, pLengths, (int*) null);
    }

    /// <summary>创建使用从零开始的索引、具有指定 <see cref="T:System.Type" /> 和维长的三维 <see cref="T:System.Array" />。</summary>
    /// <returns>使用从零开始的索引、具有指定 <see cref="T:System.Type" /> 的新的三维 <see cref="T:System.Array" />，每个维度都为指定的长度。</returns>
    /// <param name="elementType">要创建的 <see cref="T:System.Array" /> 的 <see cref="T:System.Type" />。</param>
    /// <param name="length1">要创建的 <see cref="T:System.Array" /> 的第一维的大小。</param>
    /// <param name="length2">要创建的 <see cref="T:System.Array" /> 的第二维的大小。</param>
    /// <param name="length3">要创建的 <see cref="T:System.Array" /> 的第三维的大小。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="elementType" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="elementType" /> 不是有效的 <see cref="T:System.Type" />。</exception>
    /// <exception cref="T:System.NotSupportedException">不支持 <paramref name="elementType" />。例如，不支持 <see cref="T:System.Void" />。- 或 -<paramref name="elementType" /> 是一个开放式泛型类型。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="length1" /> 小于零。- 或 -<paramref name="length2" /> 小于零。- 或 -<paramref name="length3" /> 小于零。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    public static unsafe Array CreateInstance(Type elementType, int length1, int length2, int length3)
    {
      if (elementType == null)
        throw new ArgumentNullException("elementType");
      if (length1 < 0)
        throw new ArgumentOutOfRangeException("length1", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (length2 < 0)
        throw new ArgumentOutOfRangeException("length2", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (length3 < 0)
        throw new ArgumentOutOfRangeException("length3", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      RuntimeType runtimeType = elementType.UnderlyingSystemType as RuntimeType;
      if (runtimeType == (RuntimeType) null)
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "elementType");
      int* pLengths = stackalloc int[3];
      pLengths[0] = length1;
      *(int*) ((IntPtr) pLengths + 4) = length2;
      pLengths[2] = length3;
      return Array.InternalCreate((void*) runtimeType.TypeHandle.Value, 3, pLengths, (int*) null);
    }

    /// <summary>创建使用从零开始的索引、具有指定 <see cref="T:System.Type" /> 和维长的多维 <see cref="T:System.Array" />。维的长度在一个 32 位整数数组中指定。</summary>
    /// <returns>使用从零开始的索引、具有指定 <see cref="T:System.Type" /> 的新的多维 <see cref="T:System.Array" />，其每个维度都为指定的长度。</returns>
    /// <param name="elementType">要创建的 <see cref="T:System.Array" /> 的 <see cref="T:System.Type" />。</param>
    /// <param name="lengths">一个 32 位整数数组，它表示要创建的 <see cref="T:System.Array" /> 中每个维的大小。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="elementType" /> 为 null。- 或 -<paramref name="lengths" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="elementType" /> 不是有效的 <see cref="T:System.Type" />。- 或 -<paramref name="lengths" /> 数组包含的元素少于一个。</exception>
    /// <exception cref="T:System.NotSupportedException">不支持 <paramref name="elementType" />。例如，不支持 <see cref="T:System.Void" />。- 或 -<paramref name="elementType" /> 是一个开放式泛型类型。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="lengths" /> 中的任何值都小于零。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static unsafe Array CreateInstance(Type elementType, params int[] lengths)
    {
      if (elementType == null)
        throw new ArgumentNullException("elementType");
      if (lengths == null)
        throw new ArgumentNullException("lengths");
      if (lengths.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Arg_NeedAtLeast1Rank"));
      RuntimeType runtimeType = elementType.UnderlyingSystemType as RuntimeType;
      if (runtimeType == (RuntimeType) null)
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "elementType");
      for (int index = 0; index < lengths.Length; ++index)
      {
        if (lengths[index] < 0)
          throw new ArgumentOutOfRangeException("lengths[" + (object) index + "]", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      }
      fixed (int* pLengths = lengths)
        return Array.InternalCreate((void*) runtimeType.TypeHandle.Value, lengths.Length, pLengths, (int*) null);
    }

    /// <summary>创建使用从零开始的索引、具有指定 <see cref="T:System.Type" /> 和维长的多维 <see cref="T:System.Array" />。维的长度在一个 64 位整数数组中指定。</summary>
    /// <returns>使用从零开始的索引、具有指定 <see cref="T:System.Type" /> 的新的多维 <see cref="T:System.Array" />，其每个维度都为指定的长度。</returns>
    /// <param name="elementType">要创建的 <see cref="T:System.Array" /> 的 <see cref="T:System.Type" />。</param>
    /// <param name="lengths">一个 64 位整数数组，它表示要创建的 <see cref="T:System.Array" /> 中每个维的大小。数组中的每个整数都必须介于零和 <see cref="F:System.Int32.MaxValue" /> 之间，（包括零和 System.Int32.MaxValue）。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="elementType" /> 为 null。- 或 -<paramref name="lengths" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="elementType" /> 不是有效的 <see cref="T:System.Type" />。- 或 -<paramref name="lengths" /> 数组包含的元素少于一个。</exception>
    /// <exception cref="T:System.NotSupportedException">不支持 <paramref name="elementType" />。例如，不支持 <see cref="T:System.Void" />。- 或 -<paramref name="elementType" /> 是一个开放式泛型类型。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="lengths" /> 中的所有值都小于零或大于 <see cref="F:System.Int32.MaxValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    public static Array CreateInstance(Type elementType, params long[] lengths)
    {
      if (lengths == null)
        throw new ArgumentNullException("lengths");
      if (lengths.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Arg_NeedAtLeast1Rank"));
      int[] numArray = new int[lengths.Length];
      for (int index = 0; index < lengths.Length; ++index)
      {
        long num = lengths[index];
        if (num > (long) int.MaxValue || num < (long) int.MinValue)
          throw new ArgumentOutOfRangeException("len", Environment.GetResourceString("ArgumentOutOfRange_HugeArrayNotSupported"));
        numArray[index] = (int) num;
      }
      return Array.CreateInstance(elementType, numArray);
    }

    /// <summary>创建具有指定下限、指定 <see cref="T:System.Type" /> 和维长的多维 <see cref="T:System.Array" />。</summary>
    /// <returns>新的指定 <see cref="T:System.Type" /> 的多维 <see cref="T:System.Array" />，每个维度都有指定的长度和下限。</returns>
    /// <param name="elementType">要创建的 <see cref="T:System.Array" /> 的 <see cref="T:System.Type" />。</param>
    /// <param name="lengths">一维数组，它包含要创建的 <see cref="T:System.Array" /> 的每个维度的大小。</param>
    /// <param name="lowerBounds">一维数组，它包含要创建的 <see cref="T:System.Array" /> 的每个维度的下限（起始索引）。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="elementType" /> 为 null。- 或 -<paramref name="lengths" /> 为 null。- 或 -<paramref name="lowerBounds" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="elementType" /> 不是有效的 <see cref="T:System.Type" />。- 或 -<paramref name="lengths" /> 数组包含的元素少于一个。- 或 -<paramref name="lengths" /> 和 <paramref name="lowerBounds" /> 数组包含的元素数不同。</exception>
    /// <exception cref="T:System.NotSupportedException">不支持 <paramref name="elementType" />。例如，不支持 <see cref="T:System.Void" />。- 或 -<paramref name="elementType" /> 是一个开放式泛型类型。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="lengths" /> 中的任何值都小于零。- 或 -<paramref name="lowerBounds" /> 中的任意一个值都很大，因此，维的下限和长度的和大于 <see cref="F:System.Int32.MaxValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static unsafe Array CreateInstance(Type elementType, int[] lengths, int[] lowerBounds)
    {
      if (elementType == (Type) null)
        throw new ArgumentNullException("elementType");
      if (lengths == null)
        throw new ArgumentNullException("lengths");
      if (lowerBounds == null)
        throw new ArgumentNullException("lowerBounds");
      if (lengths.Length != lowerBounds.Length)
        throw new ArgumentException(Environment.GetResourceString("Arg_RanksAndBounds"));
      if (lengths.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Arg_NeedAtLeast1Rank"));
      RuntimeType runtimeType = elementType.UnderlyingSystemType as RuntimeType;
      if (runtimeType == (RuntimeType) null)
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "elementType");
      for (int index = 0; index < lengths.Length; ++index)
      {
        if (lengths[index] < 0)
          throw new ArgumentOutOfRangeException("lengths[" + (object) index + "]", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      }
      fixed (int* pLengths = lengths)
        fixed (int* pLowerBounds = lowerBounds)
          return Array.InternalCreate((void*) runtimeType.TypeHandle.Value, lengths.Length, pLengths, pLowerBounds);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern unsafe Array InternalCreate(void* elementType, int rank, int* pLengths, int* pLowerBounds);

    [SecurityCritical]
    [PermissionSet(SecurityAction.Assert, Unrestricted = true)]
    internal static Array UnsafeCreateInstance(Type elementType, int length)
    {
      return Array.CreateInstance(elementType, length);
    }

    [SecurityCritical]
    [PermissionSet(SecurityAction.Assert, Unrestricted = true)]
    internal static Array UnsafeCreateInstance(Type elementType, int length1, int length2)
    {
      return Array.CreateInstance(elementType, length1, length2);
    }

    [SecurityCritical]
    [PermissionSet(SecurityAction.Assert, Unrestricted = true)]
    internal static Array UnsafeCreateInstance(Type elementType, params int[] lengths)
    {
      return Array.CreateInstance(elementType, lengths);
    }

    [SecurityCritical]
    [PermissionSet(SecurityAction.Assert, Unrestricted = true)]
    internal static Array UnsafeCreateInstance(Type elementType, int[] lengths, int[] lowerBounds)
    {
      return Array.CreateInstance(elementType, lengths, lowerBounds);
    }

    /// <summary>从第一个元素开始复制 <see cref="T:System.Array" /> 中的一系列元素，将它们粘贴到另一 <see cref="T:System.Array" /> 中（从第一个元素开始）。长度指定为 32 位整数。</summary>
    /// <param name="sourceArray">
    /// <see cref="T:System.Array" />，它包含要复制的数据。</param>
    /// <param name="destinationArray">
    /// <see cref="T:System.Array" />，它接收数据。</param>
    /// <param name="length">一个 32 位整数，它表示要复制的元素数目。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="sourceArray" /> 为 null。- 或 -<paramref name="destinationArray" /> 为 null。</exception>
    /// <exception cref="T:System.RankException">
    /// <paramref name="sourceArray" /> 和 <paramref name="destinationArray" /> 的秩不同。</exception>
    /// <exception cref="T:System.ArrayTypeMismatchException">
    /// <paramref name="sourceArray" /> 和 <paramref name="destinationArray" /> 是不兼容的类型。</exception>
    /// <exception cref="T:System.InvalidCastException">
    /// <paramref name="sourceArray" /> 中的至少一个元素无法强制转换为 <paramref name="destinationArray" /> 类型。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="length" /> 小于零。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="length" /> 大于 <paramref name="sourceArray" /> 中的元素数。- 或 -<paramref name="length" /> 大于 <paramref name="destinationArray" /> 中的元素数。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
    [__DynamicallyInvokable]
    public static void Copy(Array sourceArray, Array destinationArray, int length)
    {
      if (sourceArray == null)
        throw new ArgumentNullException("sourceArray");
      if (destinationArray == null)
        throw new ArgumentNullException("destinationArray");
      Array sourceArray1 = sourceArray;
      int dimension1 = 0;
      int lowerBound1 = sourceArray1.GetLowerBound(dimension1);
      Array destinationArray1 = destinationArray;
      int dimension2 = 0;
      int lowerBound2 = destinationArray1.GetLowerBound(dimension2);
      int length1 = length;
      int num = 0;
      Array.Copy(sourceArray1, lowerBound1, destinationArray1, lowerBound2, length1, num != 0);
    }

    /// <summary>从指定的源索引开始，复制 <see cref="T:System.Array" /> 中的一系列元素，将它们粘贴到另一 <see cref="T:System.Array" /> 中（从指定的目标索引开始）。长度和索引指定为 32 位整数。</summary>
    /// <param name="sourceArray">
    /// <see cref="T:System.Array" />，它包含要复制的数据。</param>
    /// <param name="sourceIndex">一个 32 位整数，它表示 <paramref name="sourceArray" /> 中复制开始处的索引。</param>
    /// <param name="destinationArray">
    /// <see cref="T:System.Array" />，它接收数据。</param>
    /// <param name="destinationIndex">一个 32 位整数，它表示 <paramref name="destinationArray" /> 中存储开始处的索引。</param>
    /// <param name="length">一个 32 位整数，它表示要复制的元素数目。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="sourceArray" /> 为 null。- 或 -<paramref name="destinationArray" /> 为 null。</exception>
    /// <exception cref="T:System.RankException">
    /// <paramref name="sourceArray" /> 和 <paramref name="destinationArray" /> 的秩不同。</exception>
    /// <exception cref="T:System.ArrayTypeMismatchException">
    /// <paramref name="sourceArray" /> 和 <paramref name="destinationArray" /> 是不兼容的类型。</exception>
    /// <exception cref="T:System.InvalidCastException">
    /// <paramref name="sourceArray" /> 中的至少一个元素无法强制转换为 <paramref name="destinationArray" /> 类型。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="sourceIndex" /> 小于 <paramref name="sourceArray" /> 的第一维的下限。- 或 -<paramref name="destinationIndex" /> 小于 <paramref name="destinationArray" /> 的第一维的下限。- 或 -<paramref name="length" /> 小于零。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="length" /> 大于从 <paramref name="sourceIndex" /> 到 <paramref name="sourceArray" /> 末尾的元素数。- 或 -<paramref name="length" /> 大于从 <paramref name="destinationIndex" /> 到 <paramref name="destinationArray" /> 末尾的元素数。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
    [__DynamicallyInvokable]
    public static void Copy(Array sourceArray, int sourceIndex, Array destinationArray, int destinationIndex, int length)
    {
      Array.Copy(sourceArray, sourceIndex, destinationArray, destinationIndex, length, false);
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void Copy(Array sourceArray, int sourceIndex, Array destinationArray, int destinationIndex, int length, bool reliable);

    /// <summary>从指定的源索引开始，复制 <see cref="T:System.Array" /> 中的一系列元素，将它们粘贴到另一 <see cref="T:System.Array" /> 中（从指定的目标索引开始）。保证在复制未成功完成的情况下撤消所有更改。</summary>
    /// <param name="sourceArray">
    /// <see cref="T:System.Array" />，它包含要复制的数据。</param>
    /// <param name="sourceIndex">一个 32 位整数，它表示 <paramref name="sourceArray" /> 中复制开始处的索引。</param>
    /// <param name="destinationArray">
    /// <see cref="T:System.Array" />，它接收数据。</param>
    /// <param name="destinationIndex">一个 32 位整数，它表示 <paramref name="destinationArray" /> 中存储开始处的索引。</param>
    /// <param name="length">一个 32 位整数，它表示要复制的元素数目。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="sourceArray" /> 为 null。- 或 -<paramref name="destinationArray" /> 为 null。</exception>
    /// <exception cref="T:System.RankException">
    /// <paramref name="sourceArray" /> 和 <paramref name="destinationArray" /> 的秩不同。</exception>
    /// <exception cref="T:System.ArrayTypeMismatchException">
    /// <paramref name="sourceArray" /> 类型不同于并且不是从 <paramref name="destinationArray" /> 类型派生的。</exception>
    /// <exception cref="T:System.InvalidCastException">
    /// <paramref name="sourceArray" /> 中的至少一个元素无法强制转换为 <paramref name="destinationArray" /> 类型。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="sourceIndex" /> 小于 <paramref name="sourceArray" /> 的第一维的下限。- 或 -<paramref name="destinationIndex" /> 小于 <paramref name="destinationArray" /> 的第一维的下限。- 或 -<paramref name="length" /> 小于零。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="length" /> 大于从 <paramref name="sourceIndex" /> 到 <paramref name="sourceArray" /> 末尾的元素数。- 或 -<paramref name="length" /> 大于从 <paramref name="destinationIndex" /> 到 <paramref name="destinationArray" /> 末尾的元素数。</exception>
    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public static void ConstrainedCopy(Array sourceArray, int sourceIndex, Array destinationArray, int destinationIndex, int length)
    {
      Array.Copy(sourceArray, sourceIndex, destinationArray, destinationIndex, length, true);
    }

    /// <summary>从第一个元素开始复制 <see cref="T:System.Array" /> 中的一系列元素，将它们粘贴到另一 <see cref="T:System.Array" /> 中（从第一个元素开始）。长度指定为 64 位整数。</summary>
    /// <param name="sourceArray">
    /// <see cref="T:System.Array" />，它包含要复制的数据。</param>
    /// <param name="destinationArray">
    /// <see cref="T:System.Array" />，它接收数据。</param>
    /// <param name="length">一个 64 位整数，它表示要复制的元素数目。该整数必须介于零和 <see cref="F:System.Int32.MaxValue" /> 之间（包括这两个值）。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="sourceArray" /> 为 null。- 或 -<paramref name="destinationArray" /> 为 null。</exception>
    /// <exception cref="T:System.RankException">
    /// <paramref name="sourceArray" /> 和 <paramref name="destinationArray" /> 的秩不同。</exception>
    /// <exception cref="T:System.ArrayTypeMismatchException">
    /// <paramref name="sourceArray" /> 和 <paramref name="destinationArray" /> 是不兼容的类型。</exception>
    /// <exception cref="T:System.InvalidCastException">
    /// <paramref name="sourceArray" /> 中的至少一个元素无法强制转换为 <paramref name="destinationArray" /> 类型。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="length" /> 小于 0 或大于 <see cref="F:System.Int32.MaxValue" />。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="length" /> 大于 <paramref name="sourceArray" /> 中的元素数。- 或 -<paramref name="length" /> 大于 <paramref name="destinationArray" /> 中的元素数。</exception>
    /// <filterpriority>1</filterpriority>
    [ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
    public static void Copy(Array sourceArray, Array destinationArray, long length)
    {
      if (length > (long) int.MaxValue || length < (long) int.MinValue)
        throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("ArgumentOutOfRange_HugeArrayNotSupported"));
      Array.Copy(sourceArray, destinationArray, (int) length);
    }

    /// <summary>从指定的源索引开始，复制 <see cref="T:System.Array" /> 中的一系列元素，将它们粘贴到另一 <see cref="T:System.Array" /> 中（从指定的目标索引开始）。长度和索引指定为 64 位整数。</summary>
    /// <param name="sourceArray">
    /// <see cref="T:System.Array" />，它包含要复制的数据。</param>
    /// <param name="sourceIndex">一个 64 位整数，它表示 <paramref name="sourceArray" /> 中复制开始处的索引。</param>
    /// <param name="destinationArray">
    /// <see cref="T:System.Array" />，它接收数据。</param>
    /// <param name="destinationIndex">一个 64 位整数，它表示 <paramref name="destinationArray" /> 中存储开始处的索引。</param>
    /// <param name="length">一个 64 位整数，它表示要复制的元素数目。该整数必须介于零和 <see cref="F:System.Int32.MaxValue" /> 之间（包括这两个值）。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="sourceArray" /> 为 null。- 或 -<paramref name="destinationArray" /> 为 null。</exception>
    /// <exception cref="T:System.RankException">
    /// <paramref name="sourceArray" /> 和 <paramref name="destinationArray" /> 的秩不同。</exception>
    /// <exception cref="T:System.ArrayTypeMismatchException">
    /// <paramref name="sourceArray" /> 和 <paramref name="destinationArray" /> 是不兼容的类型。</exception>
    /// <exception cref="T:System.InvalidCastException">
    /// <paramref name="sourceArray" /> 中的至少一个元素无法强制转换为 <paramref name="destinationArray" /> 类型。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="sourceIndex" /> 不在 <paramref name="sourceArray" /> 的有效索引范围内。- 或 -<paramref name="destinationIndex" /> 不在 <paramref name="destinationArray" /> 的有效索引范围内。- 或 -<paramref name="length" /> 小于 0 或大于 <see cref="F:System.Int32.MaxValue" />。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="length" /> 大于从 <paramref name="sourceIndex" /> 到 <paramref name="sourceArray" /> 末尾的元素数。- 或 -<paramref name="length" /> 大于从 <paramref name="destinationIndex" /> 到 <paramref name="destinationArray" /> 末尾的元素数。</exception>
    /// <filterpriority>1</filterpriority>
    [ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
    public static void Copy(Array sourceArray, long sourceIndex, Array destinationArray, long destinationIndex, long length)
    {
      if (sourceIndex > (long) int.MaxValue || sourceIndex < (long) int.MinValue)
        throw new ArgumentOutOfRangeException("sourceIndex", Environment.GetResourceString("ArgumentOutOfRange_HugeArrayNotSupported"));
      if (destinationIndex > (long) int.MaxValue || destinationIndex < (long) int.MinValue)
        throw new ArgumentOutOfRangeException("destinationIndex", Environment.GetResourceString("ArgumentOutOfRange_HugeArrayNotSupported"));
      if (length > (long) int.MaxValue || length < (long) int.MinValue)
        throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("ArgumentOutOfRange_HugeArrayNotSupported"));
      Array.Copy(sourceArray, (int) sourceIndex, destinationArray, (int) destinationIndex, (int) length);
    }

    /// <summary>将数组中的某个范围的元素设置为每个元素类型的默认值。</summary>
    /// <param name="array">需要清除其元素的数组。</param>
    /// <param name="index">要清除的一系列元素的起始索引。</param>
    /// <param name="length">要清除的元素数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> 为 null。</exception>
    /// <exception cref="T:System.IndexOutOfRangeException">
    /// <paramref name="index" /> 小于 <paramref name="array" /> 的下限。- 或 -<paramref name="length" /> 小于零。- 或 -<paramref name="index" /> 与 <paramref name="length" /> 之和大于 <paramref name="array" /> 的大小。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern void Clear(Array array, int index, int length);

    /// <summary>获取多维 <see cref="T:System.Array" /> 中指定位置的值。索引指定为一个 32 位整数数组。</summary>
    /// <returns>多维 <see cref="T:System.Array" /> 中指定位置的值。</returns>
    /// <param name="indices">32 位整数的一维数组，它表示用于指定要获取的 <see cref="T:System.Array" /> 元素的位置的索引。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="indices" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">当前 <see cref="T:System.Array" /> 中的维数不等于 <paramref name="indices" /> 中的元素数。</exception>
    /// <exception cref="T:System.IndexOutOfRangeException">
    /// <paramref name="indices" /> 中的任何元素都超出当前 <see cref="T:System.Array" /> 的对应维度的有效索引的范围。</exception>
    /// <filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public unsafe object GetValue(params int[] indices)
    {
      if (indices == null)
        throw new ArgumentNullException("indices");
      if (this.Rank != indices.Length)
        throw new ArgumentException(Environment.GetResourceString("Arg_RankIndices"));
      TypedReference typedReference = new TypedReference();
      fixed (int* pIndices = indices)
        this.InternalGetReference((void*) &typedReference, indices.Length, pIndices);
      return TypedReference.InternalToObject((void*) &typedReference);
    }

    /// <summary>获取一维 <see cref="T:System.Array" /> 中指定位置的值。索引指定为 32 位整数。</summary>
    /// <returns>一维 <see cref="T:System.Array" /> 中指定位置的值。</returns>
    /// <param name="index">一个 32 位整数，它表示要获取的 <see cref="T:System.Array" /> 元素的位置。</param>
    /// <exception cref="T:System.ArgumentException">当前的 <see cref="T:System.Array" /> 不是正好有一维。</exception>
    /// <exception cref="T:System.IndexOutOfRangeException">
    /// <paramref name="index" /> 超出当前 <see cref="T:System.Array" /> 的有效索引的范围。</exception>
    /// <filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public unsafe object GetValue(int index)
    {
      if (this.Rank != 1)
        throw new ArgumentException(Environment.GetResourceString("Arg_Need1DArray"));
      TypedReference typedReference = new TypedReference();
      this.InternalGetReference((void*) &typedReference, 1, &index);
      return TypedReference.InternalToObject((void*) &typedReference);
    }

    /// <summary>获取二维 <see cref="T:System.Array" /> 中指定位置的值。索引指定为 32 位整数。</summary>
    /// <returns>二维 <see cref="T:System.Array" /> 中指定位置的值。</returns>
    /// <param name="index1">一个 32 位整数，它表示要获取的 <see cref="T:System.Array" /> 元素的第一维索引。</param>
    /// <param name="index2">一个 32 位整数，它表示要获取的 <see cref="T:System.Array" /> 元素的第二维索引。</param>
    /// <exception cref="T:System.ArgumentException">当前的 <see cref="T:System.Array" /> 不是正好有两维。</exception>
    /// <exception cref="T:System.IndexOutOfRangeException">
    /// <paramref name="index1" /> 或 <paramref name="index2" /> 超出当前 <see cref="T:System.Array" /> 的对应维度的有效索引的范围。</exception>
    /// <filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    public unsafe object GetValue(int index1, int index2)
    {
      if (this.Rank != 2)
        throw new ArgumentException(Environment.GetResourceString("Arg_Need2DArray"));
      int* pIndices = stackalloc int[2];
      pIndices[0] = index1;
      *(int*) ((IntPtr) pIndices + 4) = index2;
      TypedReference typedReference = new TypedReference();
      this.InternalGetReference((void*) &typedReference, 2, pIndices);
      return TypedReference.InternalToObject((void*) &typedReference);
    }

    /// <summary>获取三维 <see cref="T:System.Array" /> 中指定位置的值。索引指定为 32 位整数。</summary>
    /// <returns>三维 <see cref="T:System.Array" /> 中指定位置的值。</returns>
    /// <param name="index1">一个 32 位整数，它表示要获取的 <see cref="T:System.Array" /> 元素的第一维索引。</param>
    /// <param name="index2">一个 32 位整数，它表示要获取的 <see cref="T:System.Array" /> 元素的第二维索引。</param>
    /// <param name="index3">一个 32 位整数，它表示要获取的 <see cref="T:System.Array" /> 元素的第三维索引。</param>
    /// <exception cref="T:System.ArgumentException">当前的 <see cref="T:System.Array" /> 不是正好有三维。</exception>
    /// <exception cref="T:System.IndexOutOfRangeException">
    /// <paramref name="index1" /> 或 <paramref name="index2" /> 或 <paramref name="index3" /> 超出当前 <see cref="T:System.Array" /> 的对应维度的有效索引的范围。</exception>
    /// <filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    public unsafe object GetValue(int index1, int index2, int index3)
    {
      if (this.Rank != 3)
        throw new ArgumentException(Environment.GetResourceString("Arg_Need3DArray"));
      int* pIndices = stackalloc int[3];
      pIndices[0] = index1;
      *(int*) ((IntPtr) pIndices + 4) = index2;
      pIndices[2] = index3;
      TypedReference typedReference = new TypedReference();
      this.InternalGetReference((void*) &typedReference, 3, pIndices);
      return TypedReference.InternalToObject((void*) &typedReference);
    }

    /// <summary>获取一维 <see cref="T:System.Array" /> 中指定位置的值。索引指定为 64 位整数。</summary>
    /// <returns>一维 <see cref="T:System.Array" /> 中指定位置的值。</returns>
    /// <param name="index">一个 64 位整数，它表示要获取的 <see cref="T:System.Array" /> 元素的位置。</param>
    /// <exception cref="T:System.ArgumentException">当前的 <see cref="T:System.Array" /> 不是正好有一维。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 超出当前 <see cref="T:System.Array" /> 的有效索引的范围。</exception>
    /// <filterpriority>2</filterpriority>
    [ComVisible(false)]
    public object GetValue(long index)
    {
      if (index > (long) int.MaxValue || index < (long) int.MinValue)
        throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_HugeArrayNotSupported"));
      return this.GetValue((int) index);
    }

    /// <summary>获取二维 <see cref="T:System.Array" /> 中指定位置的值。索引指定为 64 位整数。</summary>
    /// <returns>二维 <see cref="T:System.Array" /> 中指定位置的值。</returns>
    /// <param name="index1">一个 64 位整数，它表示要获取的 <see cref="T:System.Array" /> 元素的第一维索引。</param>
    /// <param name="index2">一个 64 位整数，它表示要获取的 <see cref="T:System.Array" /> 元素的第二维索引。</param>
    /// <exception cref="T:System.ArgumentException">当前的 <see cref="T:System.Array" /> 不是正好有两维。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index1" /> 或 <paramref name="index2" /> 超出当前 <see cref="T:System.Array" /> 的对应维度的有效索引的范围。</exception>
    /// <filterpriority>2</filterpriority>
    [ComVisible(false)]
    public object GetValue(long index1, long index2)
    {
      if (index1 > (long) int.MaxValue || index1 < (long) int.MinValue)
        throw new ArgumentOutOfRangeException("index1", Environment.GetResourceString("ArgumentOutOfRange_HugeArrayNotSupported"));
      if (index2 > (long) int.MaxValue || index2 < (long) int.MinValue)
        throw new ArgumentOutOfRangeException("index2", Environment.GetResourceString("ArgumentOutOfRange_HugeArrayNotSupported"));
      return this.GetValue((int) index1, (int) index2);
    }

    /// <summary>获取三维 <see cref="T:System.Array" /> 中指定位置的值。索引指定为 64 位整数。</summary>
    /// <returns>三维 <see cref="T:System.Array" /> 中指定位置的值。</returns>
    /// <param name="index1">一个 64 位整数，它表示要获取的 <see cref="T:System.Array" /> 元素的第一维索引。</param>
    /// <param name="index2">一个 64 位整数，它表示要获取的 <see cref="T:System.Array" /> 元素的第二维索引。</param>
    /// <param name="index3">一个 64 位整数，它表示要获取的 <see cref="T:System.Array" /> 元素的第三维索引。</param>
    /// <exception cref="T:System.ArgumentException">当前的 <see cref="T:System.Array" /> 不是正好有三维。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index1" /> 或 <paramref name="index2" /> 或 <paramref name="index3" /> 超出当前 <see cref="T:System.Array" /> 的对应维度的有效索引的范围。</exception>
    /// <filterpriority>2</filterpriority>
    [ComVisible(false)]
    public object GetValue(long index1, long index2, long index3)
    {
      if (index1 > (long) int.MaxValue || index1 < (long) int.MinValue)
        throw new ArgumentOutOfRangeException("index1", Environment.GetResourceString("ArgumentOutOfRange_HugeArrayNotSupported"));
      if (index2 > (long) int.MaxValue || index2 < (long) int.MinValue)
        throw new ArgumentOutOfRangeException("index2", Environment.GetResourceString("ArgumentOutOfRange_HugeArrayNotSupported"));
      if (index3 > (long) int.MaxValue || index3 < (long) int.MinValue)
        throw new ArgumentOutOfRangeException("index3", Environment.GetResourceString("ArgumentOutOfRange_HugeArrayNotSupported"));
      return this.GetValue((int) index1, (int) index2, (int) index3);
    }

    /// <summary>获取多维 <see cref="T:System.Array" /> 中指定位置的值。索引指定为一个 64 位整数数组。</summary>
    /// <returns>多维 <see cref="T:System.Array" /> 中指定位置的值。</returns>
    /// <param name="indices">64 位整数的一维数组，它表示用于指定要获取的 <see cref="T:System.Array" /> 元素的位置的索引。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="indices" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">当前 <see cref="T:System.Array" /> 中的维数不等于 <paramref name="indices" /> 中的元素数。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="indices" /> 中的任何元素都超出当前 <see cref="T:System.Array" /> 的对应维度的有效索引的范围。</exception>
    /// <filterpriority>2</filterpriority>
    [ComVisible(false)]
    public object GetValue(params long[] indices)
    {
      if (indices == null)
        throw new ArgumentNullException("indices");
      if (this.Rank != indices.Length)
        throw new ArgumentException(Environment.GetResourceString("Arg_RankIndices"));
      int[] numArray = new int[indices.Length];
      for (int index = 0; index < indices.Length; ++index)
      {
        long num = indices[index];
        if (num > (long) int.MaxValue || num < (long) int.MinValue)
          throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_HugeArrayNotSupported"));
        numArray[index] = (int) num;
      }
      return this.GetValue(numArray);
    }

    /// <summary>将某值设置给一维 <see cref="T:System.Array" /> 中指定位置的元素。索引指定为 32 位整数。</summary>
    /// <param name="value">指定元素的新值。</param>
    /// <param name="index">一个 32 位整数，它表示要设置的 <see cref="T:System.Array" /> 元素的位置。</param>
    /// <exception cref="T:System.ArgumentException">当前的 <see cref="T:System.Array" /> 不是正好有一维。</exception>
    /// <exception cref="T:System.InvalidCastException">
    /// <paramref name="value" /> 不能强制转换为当前 <see cref="T:System.Array" /> 的元素类型。</exception>
    /// <exception cref="T:System.IndexOutOfRangeException">
    /// <paramref name="index" /> 超出当前 <see cref="T:System.Array" /> 的有效索引的范围。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public unsafe void SetValue(object value, int index)
    {
      if (this.Rank != 1)
        throw new ArgumentException(Environment.GetResourceString("Arg_Need1DArray"));
      TypedReference typedReference = new TypedReference();
      this.InternalGetReference((void*) &typedReference, 1, &index);
      Array.InternalSetValue((void*) &typedReference, value);
    }

    /// <summary>将某值设置给二维 <see cref="T:System.Array" /> 中指定位置的元素。索引指定为 32 位整数。</summary>
    /// <param name="value">指定元素的新值。</param>
    /// <param name="index1">一个 32 位整数，它表示要设置的 <see cref="T:System.Array" /> 元素的第一维索引。</param>
    /// <param name="index2">一个 32 位整数，它表示要设置的 <see cref="T:System.Array" /> 元素的第二维索引。</param>
    /// <exception cref="T:System.ArgumentException">当前的 <see cref="T:System.Array" /> 不是正好有两维。</exception>
    /// <exception cref="T:System.InvalidCastException">
    /// <paramref name="value" /> 不能强制转换为当前 <see cref="T:System.Array" /> 的元素类型。</exception>
    /// <exception cref="T:System.IndexOutOfRangeException">
    /// <paramref name="index1" /> 或 <paramref name="index2" /> 超出当前 <see cref="T:System.Array" /> 的对应维度的有效索引的范围。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    public unsafe void SetValue(object value, int index1, int index2)
    {
      if (this.Rank != 2)
        throw new ArgumentException(Environment.GetResourceString("Arg_Need2DArray"));
      int* pIndices = stackalloc int[2];
      pIndices[0] = index1;
      *(int*) ((IntPtr) pIndices + 4) = index2;
      TypedReference typedReference = new TypedReference();
      this.InternalGetReference((void*) &typedReference, 2, pIndices);
      Array.InternalSetValue((void*) &typedReference, value);
    }

    /// <summary>将某值设置给三维 <see cref="T:System.Array" /> 中指定位置的元素。索引指定为 32 位整数。</summary>
    /// <param name="value">指定元素的新值。</param>
    /// <param name="index1">一个 32 位整数，它表示要设置的 <see cref="T:System.Array" /> 元素的第一维索引。</param>
    /// <param name="index2">一个 32 位整数，它表示要设置的 <see cref="T:System.Array" /> 元素的第二维索引。</param>
    /// <param name="index3">一个 32 位整数，它表示要设置的 <see cref="T:System.Array" /> 元素的第三维索引。</param>
    /// <exception cref="T:System.ArgumentException">当前的 <see cref="T:System.Array" /> 不是正好有三维。</exception>
    /// <exception cref="T:System.InvalidCastException">
    /// <paramref name="value" /> 不能强制转换为当前 <see cref="T:System.Array" /> 的元素类型。</exception>
    /// <exception cref="T:System.IndexOutOfRangeException">
    /// <paramref name="index1" /> 或 <paramref name="index2" /> 或 <paramref name="index3" /> 超出当前 <see cref="T:System.Array" /> 的对应维度的有效索引的范围。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    public unsafe void SetValue(object value, int index1, int index2, int index3)
    {
      if (this.Rank != 3)
        throw new ArgumentException(Environment.GetResourceString("Arg_Need3DArray"));
      int* pIndices = stackalloc int[3];
      pIndices[0] = index1;
      *(int*) ((IntPtr) pIndices + 4) = index2;
      pIndices[2] = index3;
      TypedReference typedReference = new TypedReference();
      this.InternalGetReference((void*) &typedReference, 3, pIndices);
      Array.InternalSetValue((void*) &typedReference, value);
    }

    /// <summary>将某值设置给多维 <see cref="T:System.Array" /> 中指定位置的元素。索引指定为一个 32 位整数数组。</summary>
    /// <param name="value">指定元素的新值。</param>
    /// <param name="indices">32 位整数的一维数组，它表示用于指定要设置的元素的位置的索引。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="indices" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">当前 <see cref="T:System.Array" /> 中的维数不等于 <paramref name="indices" /> 中的元素数。</exception>
    /// <exception cref="T:System.InvalidCastException">
    /// <paramref name="value" /> 不能强制转换为当前 <see cref="T:System.Array" /> 的元素类型。</exception>
    /// <exception cref="T:System.IndexOutOfRangeException">
    /// <paramref name="indices" /> 中的任何元素都超出当前 <see cref="T:System.Array" /> 的对应维度的有效索引的范围。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public unsafe void SetValue(object value, params int[] indices)
    {
      if (indices == null)
        throw new ArgumentNullException("indices");
      if (this.Rank != indices.Length)
        throw new ArgumentException(Environment.GetResourceString("Arg_RankIndices"));
      TypedReference typedReference = new TypedReference();
      fixed (int* pIndices = indices)
        this.InternalGetReference((void*) &typedReference, indices.Length, pIndices);
      Array.InternalSetValue((void*) &typedReference, value);
    }

    /// <summary>将某值设置给一维 <see cref="T:System.Array" /> 中指定位置的元素。索引指定为 64 位整数。</summary>
    /// <param name="value">指定元素的新值。</param>
    /// <param name="index">一个 64 位整数，它表示要设置的 <see cref="T:System.Array" /> 元素的位置。</param>
    /// <exception cref="T:System.ArgumentException">当前的 <see cref="T:System.Array" /> 不是正好有一维。</exception>
    /// <exception cref="T:System.InvalidCastException">
    /// <paramref name="value" /> 不能强制转换为当前 <see cref="T:System.Array" /> 的元素类型。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 超出当前 <see cref="T:System.Array" /> 的有效索引的范围。</exception>
    /// <filterpriority>1</filterpriority>
    [ComVisible(false)]
    public void SetValue(object value, long index)
    {
      if (index > (long) int.MaxValue || index < (long) int.MinValue)
        throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_HugeArrayNotSupported"));
      this.SetValue(value, (int) index);
    }

    /// <summary>将某值设置给二维 <see cref="T:System.Array" /> 中指定位置的元素。索引指定为 64 位整数。</summary>
    /// <param name="value">指定元素的新值。</param>
    /// <param name="index1">一个 64 位整数，它表示要设置的 <see cref="T:System.Array" /> 元素的第一维索引。</param>
    /// <param name="index2">一个 64 位整数，它表示要设置的 <see cref="T:System.Array" /> 元素的第二维索引。</param>
    /// <exception cref="T:System.ArgumentException">当前的 <see cref="T:System.Array" /> 不是正好有两维。</exception>
    /// <exception cref="T:System.InvalidCastException">
    /// <paramref name="value" /> 不能强制转换为当前 <see cref="T:System.Array" /> 的元素类型。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index1" /> 或 <paramref name="index2" /> 超出当前 <see cref="T:System.Array" /> 的对应维度的有效索引的范围。</exception>
    /// <filterpriority>1</filterpriority>
    [ComVisible(false)]
    public void SetValue(object value, long index1, long index2)
    {
      if (index1 > (long) int.MaxValue || index1 < (long) int.MinValue)
        throw new ArgumentOutOfRangeException("index1", Environment.GetResourceString("ArgumentOutOfRange_HugeArrayNotSupported"));
      if (index2 > (long) int.MaxValue || index2 < (long) int.MinValue)
        throw new ArgumentOutOfRangeException("index2", Environment.GetResourceString("ArgumentOutOfRange_HugeArrayNotSupported"));
      this.SetValue(value, (int) index1, (int) index2);
    }

    /// <summary>将某值设置给三维 <see cref="T:System.Array" /> 中指定位置的元素。索引指定为 64 位整数。</summary>
    /// <param name="value">指定元素的新值。</param>
    /// <param name="index1">一个 64 位整数，它表示要设置的 <see cref="T:System.Array" /> 元素的第一维索引。</param>
    /// <param name="index2">一个 64 位整数，它表示要设置的 <see cref="T:System.Array" /> 元素的第二维索引。</param>
    /// <param name="index3">一个 64 位整数，它表示要设置的 <see cref="T:System.Array" /> 元素的第三维索引。</param>
    /// <exception cref="T:System.ArgumentException">当前的 <see cref="T:System.Array" /> 不是正好有三维。</exception>
    /// <exception cref="T:System.InvalidCastException">
    /// <paramref name="value" /> 不能强制转换为当前 <see cref="T:System.Array" /> 的元素类型。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index1" /> 或 <paramref name="index2" /> 或 <paramref name="index3" /> 超出当前 <see cref="T:System.Array" /> 的对应维度的有效索引的范围。</exception>
    /// <filterpriority>1</filterpriority>
    [ComVisible(false)]
    public void SetValue(object value, long index1, long index2, long index3)
    {
      if (index1 > (long) int.MaxValue || index1 < (long) int.MinValue)
        throw new ArgumentOutOfRangeException("index1", Environment.GetResourceString("ArgumentOutOfRange_HugeArrayNotSupported"));
      if (index2 > (long) int.MaxValue || index2 < (long) int.MinValue)
        throw new ArgumentOutOfRangeException("index2", Environment.GetResourceString("ArgumentOutOfRange_HugeArrayNotSupported"));
      if (index3 > (long) int.MaxValue || index3 < (long) int.MinValue)
        throw new ArgumentOutOfRangeException("index3", Environment.GetResourceString("ArgumentOutOfRange_HugeArrayNotSupported"));
      this.SetValue(value, (int) index1, (int) index2, (int) index3);
    }

    /// <summary>将某值设置给多维 <see cref="T:System.Array" /> 中指定位置的元素。索引指定为一个 64 位整数数组。</summary>
    /// <param name="value">指定元素的新值。</param>
    /// <param name="indices">64 位整数的一维数组，它表示用于指定要设置的元素的位置的索引。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="indices" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">当前 <see cref="T:System.Array" /> 中的维数不等于 <paramref name="indices" /> 中的元素数。</exception>
    /// <exception cref="T:System.InvalidCastException">
    /// <paramref name="value" /> 不能强制转换为当前 <see cref="T:System.Array" /> 的元素类型。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="indices" /> 中的任何元素都超出当前 <see cref="T:System.Array" /> 的对应维度的有效索引的范围。</exception>
    /// <filterpriority>1</filterpriority>
    [ComVisible(false)]
    public void SetValue(object value, params long[] indices)
    {
      if (indices == null)
        throw new ArgumentNullException("indices");
      if (this.Rank != indices.Length)
        throw new ArgumentException(Environment.GetResourceString("Arg_RankIndices"));
      int[] numArray = new int[indices.Length];
      for (int index = 0; index < indices.Length; ++index)
      {
        long num = indices[index];
        if (num > (long) int.MaxValue || num < (long) int.MinValue)
          throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_HugeArrayNotSupported"));
        numArray[index] = (int) num;
      }
      this.SetValue(value, numArray);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private unsafe void InternalGetReference(void* elemRef, int rank, int* pIndices);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern unsafe void InternalSetValue(void* target, object value);

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    private static int GetMedian(int low, int hi)
    {
      return low + (hi - low >> 1);
    }

    /// <summary>获取一个 32 位整数，该整数表示 <see cref="T:System.Array" /> 的指定维中的元素数。</summary>
    /// <returns>一个 32 位整数，它表示指定维中的元素数。</returns>
    /// <param name="dimension">
    /// <see cref="T:System.Array" /> 的从零开始的维度，其长度需要确定。</param>
    /// <exception cref="T:System.IndexOutOfRangeException">
    /// <paramref name="dimension" /> 小于零。- 或 -<paramref name="dimension" /> 等于或大于 <see cref="P:System.Array.Rank" />。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public int GetLength(int dimension);

    /// <summary>获取一个 64 位整数，该整数表示 <see cref="T:System.Array" /> 的指定维中的元素数。</summary>
    /// <returns>一个 64 位整数，它表示指定维中的元素数。</returns>
    /// <param name="dimension">
    /// <see cref="T:System.Array" /> 的从零开始的维度，其长度需要确定。</param>
    /// <exception cref="T:System.IndexOutOfRangeException">
    /// <paramref name="dimension" /> 小于零。- 或 -<paramref name="dimension" /> 等于或大于 <see cref="P:System.Array.Rank" />。</exception>
    /// <filterpriority>2</filterpriority>
    [ComVisible(false)]
    public long GetLongLength(int dimension)
    {
      return (long) this.GetLength(dimension);
    }

    /// <summary>获取数组中指定维度最后一个元素的索引。</summary>
    /// <returns>数组中指定维度最后一个元素的索引，或 -1（如果指定维度为空）。 </returns>
    /// <param name="dimension">数组的从零开始的维度，其上限需要确定。</param>
    /// <exception cref="T:System.IndexOutOfRangeException">
    /// <paramref name="dimension" /> 小于零。- 或 -<paramref name="dimension" /> 等于或大于 <see cref="P:System.Array.Rank" />。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public int GetUpperBound(int dimension);

    /// <summary>获取数组中指定维度第一个元素的索引。</summary>
    /// <returns>数组中指定维度第一个元素的索引。</returns>
    /// <param name="dimension">数组的从零开始的维度，其起始索引需要确定。</param>
    /// <exception cref="T:System.IndexOutOfRangeException">
    /// <paramref name="dimension" /> 小于零。- 或 -<paramref name="dimension" /> 等于或大于 <see cref="P:System.Array.Rank" />。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public int GetLowerBound(int dimension);

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal int GetDataPtrOffsetInternal();

    [__DynamicallyInvokable]
    int IList.Add(object value)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
    }

    [__DynamicallyInvokable]
    bool IList.Contains(object value)
    {
      return Array.IndexOf(this, value) >= this.GetLowerBound(0);
    }

    [__DynamicallyInvokable]
    void IList.Clear()
    {
      Array.Clear(this, this.GetLowerBound(0), this.Length);
    }

    [__DynamicallyInvokable]
    int IList.IndexOf(object value)
    {
      return Array.IndexOf(this, value);
    }

    [__DynamicallyInvokable]
    void IList.Insert(int index, object value)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
    }

    [__DynamicallyInvokable]
    void IList.Remove(object value)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
    }

    [__DynamicallyInvokable]
    void IList.RemoveAt(int index)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
    }

    /// <summary>创建 <see cref="T:System.Array" /> 的浅表副本。</summary>
    /// <returns>
    /// <see cref="T:System.Array" /> 的浅表副本。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public object Clone()
    {
      return this.MemberwiseClone();
    }

    [__DynamicallyInvokable]
    int IStructuralComparable.CompareTo(object other, IComparer comparer)
    {
      if (other == null)
        return 1;
      Array array = other as Array;
      if (array == null || this.Length != array.Length)
        throw new ArgumentException(Environment.GetResourceString("ArgumentException_OtherNotArrayOfCorrectLength"), "other");
      int index = 0;
      int num;
      for (num = 0; index < array.Length && num == 0; ++index)
      {
        object x = this.GetValue(index);
        object y = array.GetValue(index);
        num = comparer.Compare(x, y);
      }
      return num;
    }

    [__DynamicallyInvokable]
    bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
    {
      if (other == null)
        return false;
      if (this == other)
        return true;
      Array array = other as Array;
      if (array == null || array.Length != this.Length)
        return false;
      for (int index = 0; index < array.Length; ++index)
      {
        object x = this.GetValue(index);
        object y = array.GetValue(index);
        if (!comparer.Equals(x, y))
          return false;
      }
      return true;
    }

    internal static int CombineHashCodes(int h1, int h2)
    {
      return (h1 << 5) + h1 ^ h2;
    }

    [__DynamicallyInvokable]
    int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
    {
      if (comparer == null)
        throw new ArgumentNullException("comparer");
      int h1 = 0;
      for (int index = this.Length >= 8 ? this.Length - 8 : 0; index < this.Length; ++index)
        h1 = Array.CombineHashCodes(h1, comparer.GetHashCode(this.GetValue(index)));
      return h1;
    }

    /// <summary>使用由数组中每个元素和指定对象实现的 <see cref="T:System.IComparable" /> 接口，在整个一维排序数组中搜索值。</summary>
    /// <returns>如果找到 <paramref name="value" />，则为指定 <paramref name="array" /> 中的指定 <paramref name="value" /> 的索引。如果找不到 <paramref name="value" /> 且 <paramref name="value" /> 小于 <paramref name="array" /> 中的一个或多个元素，则为一个负数，该负数是大于 <paramref name="value" /> 的第一个元素的索引的按位求补。如果找不到 <paramref name="value" /> 且 <paramref name="value" /> 大于 <paramref name="array" /> 中的任何元素，则为一个负数，该负数是（最后一个元素的索引加 1）的按位求补。</returns>
    /// <param name="array">要搜索的已排序一维 <see cref="T:System.Array" />。</param>
    /// <param name="value">要搜索的对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> 为 null。</exception>
    /// <exception cref="T:System.RankException">
    /// <paramref name="array" /> 是多维的。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="value" /> 是不与 <paramref name="array" /> 的元素兼容的类型。</exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// <paramref name="value" /> 没有实现 <see cref="T:System.IComparable" /> 接口，并且搜索时遇到没有实现 <see cref="T:System.IComparable" /> 接口的元素。</exception>
    /// <filterpriority>1</filterpriority>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public static int BinarySearch(Array array, object value)
    {
      if (array == null)
        throw new ArgumentNullException("array");
      int lowerBound = array.GetLowerBound(0);
      return Array.BinarySearch(array, lowerBound, array.Length, value, (IComparer) null);
    }

    /// <summary>使用由数组中每个元素和指定值实现的 <see cref="T:System.IComparable" /> 接口，在一维排序数组的某个范围中搜索值。</summary>
    /// <returns>如果找到 <paramref name="value" />，则为指定 <paramref name="array" /> 中的指定 <paramref name="value" /> 的索引。如果找不到 <paramref name="value" /> 且 <paramref name="value" /> 小于 <paramref name="array" /> 中的一个或多个元素，则为一个负数，该负数是大于 <paramref name="value" /> 的第一个元素的索引的按位求补。如果找不到 <paramref name="value" /> 且 <paramref name="value" /> 大于 <paramref name="array" /> 中的任何元素，则为一个负数，该负数是（最后一个元素的索引加 1）的按位求补。</returns>
    /// <param name="array">要搜索的已排序一维 <see cref="T:System.Array" />。</param>
    /// <param name="index">要搜索的范围的起始索引。</param>
    /// <param name="length">要搜索的范围的长度。</param>
    /// <param name="value">要搜索的对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> 为 null。</exception>
    /// <exception cref="T:System.RankException">
    /// <paramref name="array" /> 是多维的。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 小于 <paramref name="array" /> 的下限。- 或 -<paramref name="length" /> 小于零。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="index" /> 和 <paramref name="length" /> 不指定 <paramref name="array" /> 中的有效范围。- 或 -<paramref name="value" /> 是不与 <paramref name="array" /> 的元素兼容的类型。</exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// <paramref name="value" /> 没有实现 <see cref="T:System.IComparable" /> 接口，并且搜索时遇到没有实现 <see cref="T:System.IComparable" /> 接口的元素。</exception>
    /// <filterpriority>1</filterpriority>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public static int BinarySearch(Array array, int index, int length, object value)
    {
      return Array.BinarySearch(array, index, length, value, (IComparer) null);
    }

    /// <summary>使用指定 <see cref="T:System.Collections.IComparer" /> 接口，在整个一维排序数组中搜索值。</summary>
    /// <returns>如果找到 <paramref name="value" />，则为指定 <paramref name="array" /> 中的指定 <paramref name="value" /> 的索引。如果找不到 <paramref name="value" /> 且 <paramref name="value" /> 小于 <paramref name="array" /> 中的一个或多个元素，则为一个负数，该负数是大于 <paramref name="value" /> 的第一个元素的索引的按位求补。如果找不到 <paramref name="value" /> 且 <paramref name="value" /> 大于 <paramref name="array" /> 中的任何元素，则为一个负数，该负数是（最后一个元素的索引加 1）的按位求补。</returns>
    /// <param name="array">要搜索的已排序一维 <see cref="T:System.Array" />。</param>
    /// <param name="value">要搜索的对象。</param>
    /// <param name="comparer">比较元素时要使用的 <see cref="T:System.Collections.IComparer" /> 实现。- 或 - 若为 null，则使用每个元素的 <see cref="T:System.IComparable" /> 实现。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> 为 null。</exception>
    /// <exception cref="T:System.RankException">
    /// <paramref name="array" /> 是多维的。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="comparer" /> 是 null，而 <paramref name="value" /> 是不与 <paramref name="array" /> 的元素兼容的类型。</exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// <paramref name="comparer" /> 为 null，<paramref name="value" /> 没有实现 <see cref="T:System.IComparable" /> 接口，并且搜索时遇到没有实现 <see cref="T:System.IComparable" /> 接口的元素。</exception>
    /// <filterpriority>1</filterpriority>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public static int BinarySearch(Array array, object value, IComparer comparer)
    {
      if (array == null)
        throw new ArgumentNullException("array");
      int lowerBound = array.GetLowerBound(0);
      return Array.BinarySearch(array, lowerBound, array.Length, value, comparer);
    }

    /// <summary>使用指定 <see cref="T:System.Collections.IComparer" /> 接口，在一维排序数组的某个元素范围中搜索值。</summary>
    /// <returns>如果找到 <paramref name="value" />，则为指定 <paramref name="array" /> 中的指定 <paramref name="value" /> 的索引。如果找不到 <paramref name="value" /> 且 <paramref name="value" /> 小于 <paramref name="array" /> 中的一个或多个元素，则为一个负数，该负数是大于 <paramref name="value" /> 的第一个元素的索引的按位求补。如果找不到 <paramref name="value" /> 且 <paramref name="value" /> 大于 <paramref name="array" /> 中的任何元素，则为一个负数，该负数是（最后一个元素的索引加 1）的按位求补。</returns>
    /// <param name="array">要搜索的已排序一维 <see cref="T:System.Array" />。</param>
    /// <param name="index">要搜索的范围的起始索引。</param>
    /// <param name="length">要搜索的范围的长度。</param>
    /// <param name="value">要搜索的对象。</param>
    /// <param name="comparer">比较元素时要使用的 <see cref="T:System.Collections.IComparer" /> 实现。- 或 - 若为 null，则使用每个元素的 <see cref="T:System.IComparable" /> 实现。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> 为 null。</exception>
    /// <exception cref="T:System.RankException">
    /// <paramref name="array" /> 是多维的。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 小于 <paramref name="array" /> 的下限。- 或 -<paramref name="length" /> 小于零。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="index" /> 和 <paramref name="length" /> 不指定 <paramref name="array" /> 中的有效范围。- 或 -<paramref name="comparer" /> 是 null，而 <paramref name="value" /> 是不与 <paramref name="array" /> 的元素兼容的类型。</exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// <paramref name="comparer" /> 为 null，<paramref name="value" /> 没有实现 <see cref="T:System.IComparable" /> 接口，并且搜索时遇到没有实现 <see cref="T:System.IComparable" /> 接口的元素。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public static int BinarySearch(Array array, int index, int length, object value, IComparer comparer)
    {
      if (array == null)
        throw new ArgumentNullException("array");
      int lowerBound = array.GetLowerBound(0);
      if (index < lowerBound || length < 0)
        throw new ArgumentOutOfRangeException(index < lowerBound ? "index" : "length", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (array.Length - (index - lowerBound) < length)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      if (array.Rank != 1)
        throw new RankException(Environment.GetResourceString("Rank_MultiDimNotSupported"));
      if (comparer == null)
        comparer = (IComparer) Comparer.Default;
      int retVal;
      if (comparer == Comparer.Default && Array.TrySZBinarySearch(array, index, length, value, out retVal))
        return retVal;
      int low = index;
      int hi = index + length - 1;
      object[] objArray = array as object[];
      if (objArray != null)
      {
        while (low <= hi)
        {
          int median = Array.GetMedian(low, hi);
          int num;
          try
          {
            num = comparer.Compare(objArray[median], value);
          }
          catch (Exception ex)
          {
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_IComparerFailed"), ex);
          }
          if (num == 0)
            return median;
          if (num < 0)
            low = median + 1;
          else
            hi = median - 1;
        }
      }
      else
      {
        while (low <= hi)
        {
          int median = Array.GetMedian(low, hi);
          int num;
          try
          {
            num = comparer.Compare(array.GetValue(median), value);
          }
          catch (Exception ex)
          {
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_IComparerFailed"), ex);
          }
          if (num == 0)
            return median;
          if (num < 0)
            low = median + 1;
          else
            hi = median - 1;
        }
      }
      return ~low;
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern bool TrySZBinarySearch(Array sourceArray, int sourceIndex, int count, object value, out int retVal);

    /// <summary>使用由 <see cref="T:System.Array" /> 中每个元素和指定对象实现的 <see cref="T:System.IComparable`1" /> 泛型接口，在整个一维排序数组中搜索值。</summary>
    /// <returns>如果找到 <paramref name="value" />，则为指定 <paramref name="array" /> 中的指定 <paramref name="value" /> 的索引。如果找不到 <paramref name="value" /> 且 <paramref name="value" /> 小于 <paramref name="array" /> 中的一个或多个元素，则为一个负数，该负数是大于 <paramref name="value" /> 的第一个元素的索引的按位求补。如果找不到 <paramref name="value" /> 且 <paramref name="value" /> 大于 <paramref name="array" /> 中的任何元素，则为一个负数，该负数是（最后一个元素的索引加 1）的按位求补。</returns>
    /// <param name="array">要搜索的从零开始的一维排序 <see cref="T:System.Array" />。</param>
    /// <param name="value">要搜索的对象。</param>
    /// <typeparam name="T">数组元素的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// <paramref name="T" /> 未实现 <see cref="T:System.IComparable`1" /> 泛型接口。</exception>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public static int BinarySearch<T>(T[] array, T value)
    {
      if (array == null)
        throw new ArgumentNullException("array");
      return Array.BinarySearch<T>(array, 0, array.Length, value, (IComparer<T>) null);
    }

    /// <summary>使用指定的 <see cref="T:System.Collections.Generic.IComparer`1" /> 泛型接口，在整个一维排序数组中搜索值。</summary>
    /// <returns>如果找到 <paramref name="value" />，则为指定 <paramref name="array" /> 中的指定 <paramref name="value" /> 的索引。如果找不到 <paramref name="value" /> 且 <paramref name="value" /> 小于 <paramref name="array" /> 中的一个或多个元素，则为一个负数，该负数是大于 <paramref name="value" /> 的第一个元素的索引的按位求补。如果找不到 <paramref name="value" /> 且 <paramref name="value" /> 大于 <paramref name="array" /> 中的任何元素，则为一个负数，该负数是（最后一个元素的索引加 1）的按位求补。</returns>
    /// <param name="array">要搜索的从零开始的一维排序 <see cref="T:System.Array" />。</param>
    /// <param name="value">要搜索的对象。</param>
    /// <param name="comparer">比较元素时要使用的 <see cref="T:System.Collections.Generic.IComparer`1" /> 实现。- 或 - 若为 null，则使用每个元素的 <see cref="T:System.IComparable`1" /> 实现。</param>
    /// <typeparam name="T">数组元素的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="comparer" /> 是 null，而 <paramref name="value" /> 是不与 <paramref name="array" /> 的元素兼容的类型。</exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// <paramref name="comparer" /> 为 null，且 <paramref name="T" /> 未实现 <see cref="T:System.IComparable`1" /> 泛型接口</exception>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public static int BinarySearch<T>(T[] array, T value, IComparer<T> comparer)
    {
      if (array == null)
        throw new ArgumentNullException("array");
      return Array.BinarySearch<T>(array, 0, array.Length, value, comparer);
    }

    /// <summary>使用由 <see cref="T:System.Array" /> 中每个元素和指定值实现的 <see cref="T:System.IComparable`1" /> 泛型接口，在一维排序数组的某个范围中搜索值。</summary>
    /// <returns>如果找到 <paramref name="value" />，则为指定 <paramref name="array" /> 中的指定 <paramref name="value" /> 的索引。如果找不到 <paramref name="value" /> 且 <paramref name="value" /> 小于 <paramref name="array" /> 中的一个或多个元素，则为一个负数，该负数是大于 <paramref name="value" /> 的第一个元素的索引的按位求补。如果找不到 <paramref name="value" /> 且 <paramref name="value" /> 大于 <paramref name="array" /> 中的任何元素，则为一个负数，该负数是（最后一个元素的索引加 1）的按位求补。</returns>
    /// <param name="array">要搜索的从零开始的一维排序 <see cref="T:System.Array" />。</param>
    /// <param name="index">要搜索的范围的起始索引。</param>
    /// <param name="length">要搜索的范围的长度。</param>
    /// <param name="value">要搜索的对象。</param>
    /// <typeparam name="T">数组元素的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 小于 <paramref name="array" /> 的下限。- 或 -<paramref name="length" /> 小于零。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="index" /> 和 <paramref name="length" /> 不指定 <paramref name="array" /> 中的有效范围。- 或 -<paramref name="value" /> 是不与 <paramref name="array" /> 的元素兼容的类型。</exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// <paramref name="T" /> 未实现 <see cref="T:System.IComparable`1" /> 泛型接口。</exception>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public static int BinarySearch<T>(T[] array, int index, int length, T value)
    {
      return Array.BinarySearch<T>(array, index, length, value, (IComparer<T>) null);
    }

    /// <summary>使用指定的 <see cref="T:System.Collections.Generic.IComparer`1" /> 泛型接口，在一维排序数组的某个元素范围中搜索值。</summary>
    /// <returns>如果找到 <paramref name="value" />，则为指定 <paramref name="array" /> 中的指定 <paramref name="value" /> 的索引。如果找不到 <paramref name="value" /> 且 <paramref name="value" /> 小于 <paramref name="array" /> 中的一个或多个元素，则为一个负数，该负数是大于 <paramref name="value" /> 的第一个元素的索引的按位求补。如果找不到 <paramref name="value" /> 且 <paramref name="value" /> 大于 <paramref name="array" /> 中的任何元素，则为一个负数，该负数是（最后一个元素的索引加 1）的按位求补。</returns>
    /// <param name="array">要搜索的从零开始的一维排序 <see cref="T:System.Array" />。</param>
    /// <param name="index">要搜索的范围的起始索引。</param>
    /// <param name="length">要搜索的范围的长度。</param>
    /// <param name="value">要搜索的对象。</param>
    /// <param name="comparer">比较元素时要使用的 <see cref="T:System.Collections.Generic.IComparer`1" /> 实现。- 或 - 若为 null，则使用每个元素的 <see cref="T:System.IComparable`1" /> 实现。</param>
    /// <typeparam name="T">数组元素的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 小于 <paramref name="array" /> 的下限。- 或 -<paramref name="length" /> 小于零。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="index" /> 和 <paramref name="length" /> 不指定 <paramref name="array" /> 中的有效范围。- 或 -<paramref name="comparer" /> 是 null，而 <paramref name="value" /> 是不与 <paramref name="array" /> 的元素兼容的类型。</exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// <paramref name="comparer" /> 为 null，且 <paramref name="T" /> 未实现 <see cref="T:System.IComparable`1" /> 泛型接口。</exception>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public static int BinarySearch<T>(T[] array, int index, int length, T value, IComparer<T> comparer)
    {
      if (array == null)
        throw new ArgumentNullException("array");
      if (index < 0 || length < 0)
        throw new ArgumentOutOfRangeException(index < 0 ? "index" : "length", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (array.Length - index < length)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      return ArraySortHelper<T>.Default.BinarySearch(array, index, length, value, comparer);
    }

    /// <summary>将一种类型的数组转换为另一种类型的数组。</summary>
    /// <returns>目标类型的数组，包含从源数组转换而来的元素。</returns>
    /// <param name="array">要转换为目标类型的从零开始的一维 <see cref="T:System.Array" />。</param>
    /// <param name="converter">一个 <see cref="T:System.Converter`2" />，用于将每个元素从一种类型转换为另一种类型。</param>
    /// <typeparam name="TInput">源数组元素的类型。</typeparam>
    /// <typeparam name="TOutput">目标数组元素的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> 为 null。- 或 -<paramref name="converter" /> 为 null。</exception>
    public static TOutput[] ConvertAll<TInput, TOutput>(TInput[] array, Converter<TInput, TOutput> converter)
    {
      if (array == null)
        throw new ArgumentNullException("array");
      if (converter == null)
        throw new ArgumentNullException("converter");
      TOutput[] outputArray = new TOutput[array.Length];
      for (int index = 0; index < array.Length; ++index)
        outputArray[index] = converter(array[index]);
      return outputArray;
    }

    /// <summary>从指定的目标数组索引处开始，将当前一维数组的所有元素复制到指定的一维数组中。索引指定为 32 位整数。</summary>
    /// <param name="array">一维数组，它是从当前数组复制的元素的目标。</param>
    /// <param name="index">一个 32 位整数，它表示 <paramref name="array" /> 中复制开始处的索引。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 小于 <paramref name="array" /> 的下限。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="array" /> 是多维的。- 或 -源数组段中的元素数大于从 <paramref name="index" /> 到目标 <paramref name="array" /> 末尾处的可用空间。</exception>
    /// <exception cref="T:System.ArrayTypeMismatchException">源 <see cref="T:System.Array" /> 的类型无法自动转换为目标 <paramref name="array" /> 的类型。</exception>
    /// <exception cref="T:System.RankException">源数组是多维的。</exception>
    /// <exception cref="T:System.InvalidCastException">源 <see cref="T:System.Array" /> 中至少有一个元素无法强制转换为目标 <paramref name="array" /> 的类型。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public void CopyTo(Array array, int index)
    {
      if (array != null && array.Rank != 1)
        throw new ArgumentException(Environment.GetResourceString("Arg_RankMultiDimNotSupported"));
      Array.Copy(this, this.GetLowerBound(0), array, index, this.Length);
    }

    /// <summary>从指定的目标数组索引处开始，将当前一维数组的所有元素复制到指定的一维数组中。索引指定为 64 位整数。</summary>
    /// <param name="array">一维数组，它是从当前数组复制的元素的目标。</param>
    /// <param name="index">一个 64 位整数，它表示 <paramref name="array" /> 中复制开始处的索引。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 超出了 <paramref name="array" /> 的有效索引范围。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="array" /> 是多维的。- 或 -源数组段中的元素数大于从 <paramref name="index" /> 到目标 <paramref name="array" /> 末尾处的可用空间。</exception>
    /// <exception cref="T:System.ArrayTypeMismatchException">源 <see cref="T:System.Array" /> 的类型无法自动转换为目标 <paramref name="array" /> 的类型。</exception>
    /// <exception cref="T:System.RankException">源 <see cref="T:System.Array" /> 是多维的。</exception>
    /// <exception cref="T:System.InvalidCastException">源 <see cref="T:System.Array" /> 中至少有一个元素无法强制转换为目标 <paramref name="array" /> 的类型。</exception>
    /// <filterpriority>2</filterpriority>
    [ComVisible(false)]
    public void CopyTo(Array array, long index)
    {
      if (index > (long) int.MaxValue || index < (long) int.MinValue)
        throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_HugeArrayNotSupported"));
      this.CopyTo(array, (int) index);
    }

    /// <summary>返回一个空数组。</summary>
    /// <returns>返回一个空<see cref="T:System.Array" />。</returns>
    /// <typeparam name="T">数组元素的类型。</typeparam>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public static T[] Empty<T>()
    {
      return EmptyArray<T>.Value;
    }

    /// <summary>确定指定数组包含的元素是否与指定谓词定义的条件匹配。</summary>
    /// <returns>如果 <paramref name="array" /> 包含一个或多个元素与指定谓词定义的条件匹配，则为 true；否则为 false。</returns>
    /// <param name="array">要搜索的从零开始的一维 <see cref="T:System.Array" />。</param>
    /// <param name="match">
    /// <see cref="T:System.Predicate`1" />，定义要搜索的元素的条件。</param>
    /// <typeparam name="T">数组元素的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> 为 null。- 或 -<paramref name="match" /> 为 null。</exception>
    [__DynamicallyInvokable]
    public static bool Exists<T>(T[] array, Predicate<T> match)
    {
      return Array.FindIndex<T>(array, match) != -1;
    }

    /// <summary>搜索与指定谓词所定义的条件相匹配的元素，并返回整个 <see cref="T:System.Array" /> 中的第一个匹配元素。</summary>
    /// <returns>如果找到与指定谓词定义的条件匹配的第一个元素，则为该元素；否则为类型 <paramref name="T" /> 的默认值。</returns>
    /// <param name="array">要搜索的从零开始的一维数组。</param>
    /// <param name="match">用于定义要搜索的元素的条件的谓词。</param>
    /// <typeparam name="T">数组元素的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> 为 null。- 或 -<paramref name="match" /> 为 null。</exception>
    [__DynamicallyInvokable]
    public static T Find<T>(T[] array, Predicate<T> match)
    {
      if (array == null)
        throw new ArgumentNullException("array");
      if (match == null)
        throw new ArgumentNullException("match");
      for (int index = 0; index < array.Length; ++index)
      {
        if (match(array[index]))
          return array[index];
      }
      return default (T);
    }

    /// <summary>检索与指定谓词定义的条件匹配的所有元素。</summary>
    /// <returns>如果找到一个其中所有元素均与指定谓词定义的条件匹配的 <see cref="T:System.Array" />，则为该数组；否则为一个空 <see cref="T:System.Array" />。</returns>
    /// <param name="array">要搜索的从零开始的一维 <see cref="T:System.Array" />。</param>
    /// <param name="match">
    /// <see cref="T:System.Predicate`1" />，定义要搜索的元素的条件。</param>
    /// <typeparam name="T">数组元素的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> 为 null。- 或 -<paramref name="match" /> 为 null。</exception>
    [__DynamicallyInvokable]
    public static T[] FindAll<T>(T[] array, Predicate<T> match)
    {
      if (array == null)
        throw new ArgumentNullException("array");
      if (match == null)
        throw new ArgumentNullException("match");
      List<T> objList = new List<T>();
      for (int index = 0; index < array.Length; ++index)
      {
        if (match(array[index]))
          objList.Add(array[index]);
      }
      return objList.ToArray();
    }

    /// <summary>搜索与指定谓词所定义的条件相匹配的元素，并返回整个 <see cref="T:System.Array" /> 中第一个匹配元素的从零开始的索引。</summary>
    /// <returns>如果找到与 <paramref name="match" /> 定义的条件相匹配的第一个元素，则为该元素的从零开始的索引；否则为 -1。</returns>
    /// <param name="array">要搜索的从零开始的一维 <see cref="T:System.Array" />。</param>
    /// <param name="match">
    /// <see cref="T:System.Predicate`1" />，定义要搜索的元素的条件。</param>
    /// <typeparam name="T">数组元素的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> 为 null。- 或 -<paramref name="match" /> 为 null。</exception>
    [__DynamicallyInvokable]
    public static int FindIndex<T>(T[] array, Predicate<T> match)
    {
      if (array == null)
        throw new ArgumentNullException("array");
      return Array.FindIndex<T>(array, 0, array.Length, match);
    }

    /// <summary>搜索与指定谓词所定义的条件相匹配的元素，并返回 <see cref="T:System.Array" /> 中从指定索引到最后一个元素的元素范围内第一个匹配项的从零开始的索引。</summary>
    /// <returns>如果找到与 <paramref name="match" /> 定义的条件相匹配的第一个元素，则为该元素的从零开始的索引；否则为 -1。</returns>
    /// <param name="array">要搜索的从零开始的一维 <see cref="T:System.Array" />。</param>
    /// <param name="startIndex">从零开始的搜索的起始索引。</param>
    /// <param name="match">
    /// <see cref="T:System.Predicate`1" />，定义要搜索的元素的条件。</param>
    /// <typeparam name="T">数组元素的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> 为 null。- 或 -<paramref name="match" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="startIndex" /> 超出了 <paramref name="array" /> 的有效索引范围。</exception>
    [__DynamicallyInvokable]
    public static int FindIndex<T>(T[] array, int startIndex, Predicate<T> match)
    {
      if (array == null)
        throw new ArgumentNullException("array");
      return Array.FindIndex<T>(array, startIndex, array.Length - startIndex, match);
    }

    /// <summary>搜索与指定谓词所定义的条件相匹配的一个元素，并返回 <see cref="T:System.Array" /> 中从指定的索引开始、包含指定元素个数的元素范围内第一个匹配项的从零开始的索引。</summary>
    /// <returns>如果找到与 <paramref name="match" /> 定义的条件相匹配的第一个元素，则为该元素的从零开始的索引；否则为 -1。</returns>
    /// <param name="array">要搜索的从零开始的一维 <see cref="T:System.Array" />。</param>
    /// <param name="startIndex">从零开始的搜索的起始索引。</param>
    /// <param name="count">要搜索的部分中的元素数。</param>
    /// <param name="match">
    /// <see cref="T:System.Predicate`1" />，定义要搜索的元素的条件。</param>
    /// <typeparam name="T">数组元素的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> 为 null。- 或 -<paramref name="match" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="startIndex" /> 超出了 <paramref name="array" /> 的有效索引范围。- 或 -<paramref name="count" /> 小于零。- 或 -<paramref name="startIndex" /> 和 <paramref name="count" /> 指定的不是 <paramref name="array" /> 中的有效部分。</exception>
    [__DynamicallyInvokable]
    public static int FindIndex<T>(T[] array, int startIndex, int count, Predicate<T> match)
    {
      if (array == null)
        throw new ArgumentNullException("array");
      if (startIndex < 0 || startIndex > array.Length)
        throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (count < 0 || startIndex > array.Length - count)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Count"));
      if (match == null)
        throw new ArgumentNullException("match");
      int num = startIndex + count;
      for (int index = startIndex; index < num; ++index)
      {
        if (match(array[index]))
          return index;
      }
      return -1;
    }

    /// <summary>搜索与指定谓词所定义的条件相匹配的元素，并返回整个 <see cref="T:System.Array" /> 中的最后一个匹配元素。</summary>
    /// <returns>如果找到，则为与指定谓词所定义的条件相匹配的最后一个元素；否则为类型 <paramref name="T" /> 的默认值。</returns>
    /// <param name="array">要搜索的从零开始的一维 <see cref="T:System.Array" />。</param>
    /// <param name="match">
    /// <see cref="T:System.Predicate`1" />，定义要搜索的元素的条件。</param>
    /// <typeparam name="T">数组元素的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> 为 null。- 或 -<paramref name="match" /> 为 null。</exception>
    [__DynamicallyInvokable]
    public static T FindLast<T>(T[] array, Predicate<T> match)
    {
      if (array == null)
        throw new ArgumentNullException("array");
      if (match == null)
        throw new ArgumentNullException("match");
      for (int index = array.Length - 1; index >= 0; --index)
      {
        if (match(array[index]))
          return array[index];
      }
      return default (T);
    }

    /// <summary>搜索与指定谓词所定义的条件相匹配的元素，并返回整个 <see cref="T:System.Array" /> 中最后一个匹配元素的从零开始的索引。</summary>
    /// <returns>如果找到与 <paramref name="match" /> 定义的条件相匹配的最后一个元素，则为该元素的从零开始的索引；否则为 -1。</returns>
    /// <param name="array">要搜索的从零开始的一维 <see cref="T:System.Array" />。</param>
    /// <param name="match">
    /// <see cref="T:System.Predicate`1" />，定义要搜索的元素的条件。</param>
    /// <typeparam name="T">数组元素的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> 为 null。- 或 -<paramref name="match" /> 为 null。</exception>
    [__DynamicallyInvokable]
    public static int FindLastIndex<T>(T[] array, Predicate<T> match)
    {
      if (array == null)
        throw new ArgumentNullException("array");
      T[] array1 = array;
      int startIndex = array1.Length - 1;
      int length = array.Length;
      Predicate<T> match1 = match;
      return Array.FindLastIndex<T>(array1, startIndex, length, match1);
    }

    /// <summary>搜索与由指定谓词定义的条件相匹配的元素，并返回 <see cref="T:System.Array" /> 中从第一个元素到指定索引的元素范围内最后一个匹配项的从零开始的索引。</summary>
    /// <returns>如果找到与 <paramref name="match" /> 定义的条件相匹配的最后一个元素，则为该元素的从零开始的索引；否则为 -1。</returns>
    /// <param name="array">要搜索的从零开始的一维 <see cref="T:System.Array" />。</param>
    /// <param name="startIndex">向后搜索的从零开始的起始索引。</param>
    /// <param name="match">
    /// <see cref="T:System.Predicate`1" />，定义要搜索的元素的条件。</param>
    /// <typeparam name="T">数组元素的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> 为 null。- 或 -<paramref name="match" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="startIndex" /> 超出了 <paramref name="array" /> 的有效索引范围。</exception>
    [__DynamicallyInvokable]
    public static int FindLastIndex<T>(T[] array, int startIndex, Predicate<T> match)
    {
      if (array == null)
        throw new ArgumentNullException("array");
      T[] array1 = array;
      int startIndex1 = startIndex;
      int num = 1;
      int count = startIndex1 + num;
      Predicate<T> match1 = match;
      return Array.FindLastIndex<T>(array1, startIndex1, count, match1);
    }

    /// <summary>搜索与指定谓词所定义的条件相匹配的元素，并返回 <see cref="T:System.Array" /> 中包含指定元素个数、到指定索引结束的元素范围内最后一个匹配项的从零开始的索引。</summary>
    /// <returns>如果找到与 <paramref name="match" /> 定义的条件相匹配的最后一个元素，则为该元素的从零开始的索引；否则为 -1。</returns>
    /// <param name="array">要搜索的从零开始的一维 <see cref="T:System.Array" />。</param>
    /// <param name="startIndex">向后搜索的从零开始的起始索引。</param>
    /// <param name="count">要搜索的部分中的元素数。</param>
    /// <param name="match">
    /// <see cref="T:System.Predicate`1" />，定义要搜索的元素的条件。</param>
    /// <typeparam name="T">数组元素的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> 为 null。- 或 -<paramref name="match" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="startIndex" /> 超出了 <paramref name="array" /> 的有效索引范围。- 或 -<paramref name="count" /> 小于零。- 或 -<paramref name="startIndex" /> 和 <paramref name="count" /> 指定的不是 <paramref name="array" /> 中的有效部分。</exception>
    [__DynamicallyInvokable]
    public static int FindLastIndex<T>(T[] array, int startIndex, int count, Predicate<T> match)
    {
      if (array == null)
        throw new ArgumentNullException("array");
      if (match == null)
        throw new ArgumentNullException("match");
      if (array.Length == 0)
      {
        if (startIndex != -1)
          throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      }
      else if (startIndex < 0 || startIndex >= array.Length)
        throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (count < 0 || startIndex - count + 1 < 0)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Count"));
      int num = startIndex - count;
      for (int index = startIndex; index > num; --index)
      {
        if (match(array[index]))
          return index;
      }
      return -1;
    }

    /// <summary>对指定数组的每个元素执行指定操作。</summary>
    /// <param name="array">从零开始的一维 <see cref="T:System.Array" />，要对其元素执行操作。</param>
    /// <param name="action">要对 <paramref name="array" /> 的每个元素执行的 <see cref="T:System.Action`1" />。</param>
    /// <typeparam name="T">数组元素的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> 为 null。- 或 -<paramref name="action" /> 为 null。</exception>
    public static void ForEach<T>(T[] array, Action<T> action)
    {
      if (array == null)
        throw new ArgumentNullException("array");
      if (action == null)
        throw new ArgumentNullException("action");
      for (int index = 0; index < array.Length; ++index)
        action(array[index]);
    }

    /// <summary>返回 <see cref="T:System.Array" /> 的 <see cref="T:System.Collections.IEnumerator" />。</summary>
    /// <returns>用于 <see cref="T:System.Array" /> 的 <see cref="T:System.Collections.IEnumerator" />。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public IEnumerator GetEnumerator()
    {
      int lowerBound = this.GetLowerBound(0);
      if (this.Rank == 1 && lowerBound == 0)
        return (IEnumerator) new Array.SZArrayEnumerator(this);
      return (IEnumerator) new Array.ArrayEnumerator(this, lowerBound, this.Length);
    }

    /// <summary>在一个一维数组中搜索指定对象，并返回其首个匹配项的索引。</summary>
    /// <returns>第一个匹配项的索引<paramref name="value" />中<paramref name="array" />，如果找到，则否则的下限减 1 的数组。</returns>
    /// <param name="array">要搜索的一维数组。</param>
    /// <param name="value">要在 <paramref name="array" /> 中查找的对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> 为 null。</exception>
    /// <exception cref="T:System.RankException">
    /// <paramref name="array" /> 是多维的。</exception>
    /// <filterpriority>1</filterpriority>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public static int IndexOf(Array array, object value)
    {
      if (array == null)
        throw new ArgumentNullException("array");
      int lowerBound = array.GetLowerBound(0);
      return Array.IndexOf(array, value, lowerBound, array.Length);
    }

    /// <summary>在一个一维数组的一系列元素中搜索指定对象，然后返回其首个匹配项的索引。该元素系列的范围为从指定索引到该数组结尾。</summary>
    /// <returns>第一个匹配项的索引<paramref name="value" />，如果找到了），并让它在范围中的元素内<paramref name="array" />用于扩展从<paramref name="startIndex" />到最后一个元素 ；否则的下限减 1 的数组。</returns>
    /// <param name="array">要搜索的一维数组。</param>
    /// <param name="value">要在 <paramref name="array" /> 中查找的对象。</param>
    /// <param name="startIndex">搜索的起始索引。空数组中 0（零）为有效值。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="startIndex" /> 超出了 <paramref name="array" /> 的有效索引范围。</exception>
    /// <exception cref="T:System.RankException">
    /// <paramref name="array" /> 是多维的。</exception>
    /// <filterpriority>1</filterpriority>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public static int IndexOf(Array array, object value, int startIndex)
    {
      if (array == null)
        throw new ArgumentNullException("array");
      int lowerBound = array.GetLowerBound(0);
      return Array.IndexOf(array, value, startIndex, array.Length - startIndex + lowerBound);
    }

    /// <summary>在一个一维数组的一系列元素中搜索指定对象，然后返回其首个匹配项的索引。该元素系列的范围从指定数量的元素的指定索引开始。</summary>
    /// <returns>第一个匹配项的索引<paramref name="value" />，如果它找到，则在<paramref name="array" />从索引<paramref name="startIndex" />到<paramref name="startIndex" />+ <paramref name="count" /> -1 ；否则的下限减 1 的数组。</returns>
    /// <param name="array">要搜索的一维数组。</param>
    /// <param name="value">要在 <paramref name="array" /> 中查找的对象。</param>
    /// <param name="startIndex">搜索的起始索引。空数组中 0（零）为有效值。</param>
    /// <param name="count">要搜索的元素数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="startIndex" /> 超出了 <paramref name="array" /> 的有效索引范围。- 或 -<paramref name="count" /> 小于零。- 或 -<paramref name="startIndex" /> 和 <paramref name="count" /> 指定的不是 <paramref name="array" /> 中的有效部分。</exception>
    /// <exception cref="T:System.RankException">
    /// <paramref name="array" /> 是多维的。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public static int IndexOf(Array array, object value, int startIndex, int count)
    {
      if (array == null)
        throw new ArgumentNullException("array");
      if (array.Rank != 1)
        throw new RankException(Environment.GetResourceString("Rank_MultiDimNotSupported"));
      int lowerBound = array.GetLowerBound(0);
      if (startIndex < lowerBound || startIndex > array.Length + lowerBound)
        throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (count < 0 || count > array.Length - startIndex + lowerBound)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Count"));
      int retVal;
      if (Array.TrySZIndexOf(array, startIndex, count, value, out retVal))
        return retVal;
      object[] objArray = array as object[];
      int num = startIndex + count;
      if (objArray != null)
      {
        if (value == null)
        {
          for (int index = startIndex; index < num; ++index)
          {
            if (objArray[index] == null)
              return index;
          }
        }
        else
        {
          for (int index = startIndex; index < num; ++index)
          {
            object obj = objArray[index];
            if (obj != null && obj.Equals(value))
              return index;
          }
        }
      }
      else
      {
        for (int index = startIndex; index < num; ++index)
        {
          object obj = array.GetValue(index);
          if (obj == null)
          {
            if (value == null)
              return index;
          }
          else if (obj.Equals(value))
            return index;
        }
      }
      return lowerBound - 1;
    }

    /// <summary>在一个一维数组中搜索指定对象，并返回其首个匹配项的索引。</summary>
    /// <returns>第一个匹配项的从零开始的索引<paramref name="value" />在整个<paramref name="array" />，如果找到，则否则为 – 1。</returns>
    /// <param name="array">要搜索的从零开始的一维数组。</param>
    /// <param name="value">要在 <paramref name="array" /> 中查找的对象。</param>
    /// <typeparam name="T">数组元素的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> 为 null。</exception>
    [__DynamicallyInvokable]
    public static int IndexOf<T>(T[] array, T value)
    {
      if (array == null)
        throw new ArgumentNullException("array");
      return Array.IndexOf<T>(array, value, 0, array.Length);
    }

    /// <summary>在一个一维数组的一系列元素中搜索指定对象，然后返回其首个匹配项的索引。该元素系列的范围为从指定索引到该数组结尾。</summary>
    /// <returns>如果在 <paramref name="array" /> 中从 <paramref name="startIndex" /> 到最后一个元素这部分元素中找到 <paramref name="value" /> 的匹配项，则为第一个匹配项的从零开始的索引；否则为 -1。</returns>
    /// <param name="array">要搜索的从零开始的一维数组。</param>
    /// <param name="value">要在 <paramref name="array" /> 中查找的对象。</param>
    /// <param name="startIndex">从零开始的搜索的起始索引。空数组中 0（零）为有效值。</param>
    /// <typeparam name="T">数组元素的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="startIndex" /> 超出了 <paramref name="array" /> 的有效索引范围。</exception>
    [__DynamicallyInvokable]
    public static int IndexOf<T>(T[] array, T value, int startIndex)
    {
      if (array == null)
        throw new ArgumentNullException("array");
      return Array.IndexOf<T>(array, value, startIndex, array.Length - startIndex);
    }

    /// <summary>在一个一维数组的一系列元素中搜索指定对象，然后返回其首个匹配项的索引。该元素系列的范围从指定数量的元素的指定索引开始。</summary>
    /// <returns>如果在 <paramref name="array" /> 中从 <paramref name="startIndex" /> 开始、包含 <paramref name="count" /> 所指定的元素个数的这部分元素中，找到 <paramref name="value" /> 的匹配项，则为第一个匹配项的从零开始的索引；否则为 -1。</returns>
    /// <param name="array">要搜索的从零开始的一维数组。</param>
    /// <param name="value">要在 <paramref name="array" /> 中查找的对象。</param>
    /// <param name="startIndex">从零开始的搜索的起始索引。空数组中 0（零）为有效值。</param>
    /// <param name="count">要搜索的部分中的元素数。</param>
    /// <typeparam name="T">数组元素的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="startIndex" /> 超出了 <paramref name="array" /> 的有效索引范围。- 或 -<paramref name="count" /> 小于零。- 或 -<paramref name="startIndex" /> 和 <paramref name="count" /> 指定的不是 <paramref name="array" /> 中的有效部分。</exception>
    [__DynamicallyInvokable]
    public static int IndexOf<T>(T[] array, T value, int startIndex, int count)
    {
      if (array == null)
        throw new ArgumentNullException("array");
      if (startIndex < 0 || startIndex > array.Length)
        throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (count < 0 || count > array.Length - startIndex)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Count"));
      return EqualityComparer<T>.Default.IndexOf(array, value, startIndex, count);
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern bool TrySZIndexOf(Array sourceArray, int sourceIndex, int count, object value, out int retVal);

    /// <summary>搜索指定的对象，并返回整个一维 <see cref="T:System.Array" /> 中最后一个匹配项的索引。</summary>
    /// <returns>如果在整个 <paramref name="array" /> 中找到 <paramref name="value" /> 的匹配项，则为最后一个匹配项的索引；否则为该数组的下限减 1。</returns>
    /// <param name="array">要搜索的一维 <see cref="T:System.Array" />。</param>
    /// <param name="value">要在 <paramref name="array" /> 中查找的对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> 为 null。</exception>
    /// <exception cref="T:System.RankException">
    /// <paramref name="array" /> 是多维的。</exception>
    /// <filterpriority>1</filterpriority>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public static int LastIndexOf(Array array, object value)
    {
      if (array == null)
        throw new ArgumentNullException("array");
      int lowerBound = array.GetLowerBound(0);
      return Array.LastIndexOf(array, value, array.Length - 1 + lowerBound, array.Length);
    }

    /// <summary>搜索指定的对象，并返回一维 <see cref="T:System.Array" /> 中从第一个元素到指定索引这部分元素中最后一个匹配项的索引。</summary>
    /// <returns>如果在 <paramref name="array" /> 中从第一个元素到 <paramref name="startIndex" /> 这部分元素中找到 <paramref name="value" /> 的匹配项，则为最后一个匹配项的索引；否则为该数组的下限减 1。</returns>
    /// <param name="array">要搜索的一维 <see cref="T:System.Array" />。</param>
    /// <param name="value">要在 <paramref name="array" /> 中查找的对象。</param>
    /// <param name="startIndex">向后搜索的起始索引。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="startIndex" /> 超出了 <paramref name="array" /> 的有效索引范围。</exception>
    /// <exception cref="T:System.RankException">
    /// <paramref name="array" /> 是多维的。</exception>
    /// <filterpriority>1</filterpriority>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public static int LastIndexOf(Array array, object value, int startIndex)
    {
      if (array == null)
        throw new ArgumentNullException("array");
      int lowerBound = array.GetLowerBound(0);
      Array array1 = array;
      object obj = value;
      int startIndex1 = startIndex;
      int num = 1;
      int count = startIndex1 + num - lowerBound;
      return Array.LastIndexOf(array1, obj, startIndex1, count);
    }

    /// <summary>搜索指定的对象，并返回一维 <see cref="T:System.Array" /> 中到指定索引为止包含指定个元素的这部分元素中最后一个匹配项的索引。</summary>
    /// <returns>如果在 <paramref name="array" /> 中到 <paramref name="startIndex" /> 为止并且包含的元素个数为在 <paramref name="count" /> 中指定的个数的这部分元素中找到 <paramref name="value" /> 的匹配项，则为最后一个匹配项的索引；否则为该数组的下限减 1。</returns>
    /// <param name="array">要搜索的一维 <see cref="T:System.Array" />。</param>
    /// <param name="value">要在 <paramref name="array" /> 中查找的对象。</param>
    /// <param name="startIndex">向后搜索的起始索引。</param>
    /// <param name="count">要搜索的部分中的元素数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="startIndex" /> 超出了 <paramref name="array" /> 的有效索引范围。- 或 -<paramref name="count" /> 小于零。- 或 -<paramref name="startIndex" /> 和 <paramref name="count" /> 指定的不是 <paramref name="array" /> 中的有效部分。</exception>
    /// <exception cref="T:System.RankException">
    /// <paramref name="array" /> 是多维的。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public static int LastIndexOf(Array array, object value, int startIndex, int count)
    {
      if (array == null)
        throw new ArgumentNullException("array");
      int lowerBound = array.GetLowerBound(0);
      if (array.Length == 0)
        return lowerBound - 1;
      if (startIndex < lowerBound || startIndex >= array.Length + lowerBound)
        throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (count < 0)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Count"));
      if (count > startIndex - lowerBound + 1)
        throw new ArgumentOutOfRangeException("endIndex", Environment.GetResourceString("ArgumentOutOfRange_EndIndexStartIndex"));
      if (array.Rank != 1)
        throw new RankException(Environment.GetResourceString("Rank_MultiDimNotSupported"));
      int retVal;
      if (Array.TrySZLastIndexOf(array, startIndex, count, value, out retVal))
        return retVal;
      object[] objArray = array as object[];
      int num = startIndex - count + 1;
      if (objArray != null)
      {
        if (value == null)
        {
          for (int index = startIndex; index >= num; --index)
          {
            if (objArray[index] == null)
              return index;
          }
        }
        else
        {
          for (int index = startIndex; index >= num; --index)
          {
            object obj = objArray[index];
            if (obj != null && obj.Equals(value))
              return index;
          }
        }
      }
      else
      {
        for (int index = startIndex; index >= num; --index)
        {
          object obj = array.GetValue(index);
          if (obj == null)
          {
            if (value == null)
              return index;
          }
          else if (obj.Equals(value))
            return index;
        }
      }
      return lowerBound - 1;
    }

    /// <summary>搜索指定的对象，并返回整个 <see cref="T:System.Array" /> 中最后一个匹配项的索引。</summary>
    /// <returns>如果在整个 <paramref name="array" /> 中找到 <paramref name="value" /> 的匹配项，则为最后一个匹配项的从零开始的索引；否则为 -1。</returns>
    /// <param name="array">要搜索的从零开始的一维 <see cref="T:System.Array" />。</param>
    /// <param name="value">要在 <paramref name="array" /> 中查找的对象。</param>
    /// <typeparam name="T">数组元素的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> 为 null。</exception>
    [__DynamicallyInvokable]
    public static int LastIndexOf<T>(T[] array, T value)
    {
      if (array == null)
        throw new ArgumentNullException("array");
      return Array.LastIndexOf<T>(array, value, array.Length - 1, array.Length);
    }

    /// <summary>搜索指定的对象，并返回 <see cref="T:System.Array" /> 中从第一个元素到指定索引这部分元素中最后一个匹配项的索引。</summary>
    /// <returns>如果在 <paramref name="array" /> 中从第一个元素到 <paramref name="startIndex" /> 这部分元素中找到 <paramref name="value" /> 的匹配项，则为最后一个匹配项的从零开始的索引；否则为 -1。</returns>
    /// <param name="array">要搜索的从零开始的一维 <see cref="T:System.Array" />。</param>
    /// <param name="value">要在 <paramref name="array" /> 中查找的对象。</param>
    /// <param name="startIndex">向后搜索的从零开始的起始索引。</param>
    /// <typeparam name="T">数组元素的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="startIndex" /> 超出了 <paramref name="array" /> 的有效索引范围。</exception>
    [__DynamicallyInvokable]
    public static int LastIndexOf<T>(T[] array, T value, int startIndex)
    {
      if (array == null)
        throw new ArgumentNullException("array");
      return Array.LastIndexOf<T>(array, value, startIndex, array.Length == 0 ? 0 : startIndex + 1);
    }

    /// <summary>搜索指定的对象，并返回 <see cref="T:System.Array" /> 中到指定索引为止包含指定个元素的这部分元素中最后一个匹配项的索引。</summary>
    /// <returns>如果在 <paramref name="array" /> 中到 <paramref name="startIndex" /> 为止、包含 <paramref name="count" /> 所指定的元素个数的这部分元素中，找到 <paramref name="value" /> 的匹配项，则为最后一个匹配项的从零开始的索引；否则为 -1。</returns>
    /// <param name="array">要搜索的从零开始的一维 <see cref="T:System.Array" />。</param>
    /// <param name="value">要在 <paramref name="array" /> 中查找的对象。</param>
    /// <param name="startIndex">向后搜索的从零开始的起始索引。</param>
    /// <param name="count">要搜索的部分中的元素数。</param>
    /// <typeparam name="T">数组元素的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="startIndex" /> 超出了 <paramref name="array" /> 的有效索引范围。- 或 -<paramref name="count" /> 小于零。- 或 -<paramref name="startIndex" /> 和 <paramref name="count" /> 指定的不是 <paramref name="array" /> 中的有效部分。</exception>
    [__DynamicallyInvokable]
    public static int LastIndexOf<T>(T[] array, T value, int startIndex, int count)
    {
      if (array == null)
        throw new ArgumentNullException("array");
      if (array.Length == 0)
      {
        if (startIndex != -1 && startIndex != 0)
          throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
        if (count != 0)
          throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Count"));
        return -1;
      }
      if (startIndex < 0 || startIndex >= array.Length)
        throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (count < 0 || startIndex - count + 1 < 0)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Count"));
      return EqualityComparer<T>.Default.LastIndexOf(array, value, startIndex, count);
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern bool TrySZLastIndexOf(Array sourceArray, int sourceIndex, int count, object value, out int retVal);

    /// <summary>反转整个一维 <see cref="T:System.Array" /> 中元素的顺序。</summary>
    /// <param name="array">要反转的一维 <see cref="T:System.Array" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> 为 null。 </exception>
    /// <exception cref="T:System.RankException">
    /// <paramref name="array" /> 是多维的。</exception>
    /// <filterpriority>1</filterpriority>
    [ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
    [__DynamicallyInvokable]
    public static void Reverse(Array array)
    {
      if (array == null)
        throw new ArgumentNullException("array");
      Array array1 = array;
      int dimension = 0;
      int lowerBound = array1.GetLowerBound(dimension);
      int length = array.Length;
      Array.Reverse(array1, lowerBound, length);
    }

    /// <summary>反转一维 <see cref="T:System.Array" /> 中某部分元素的元素顺序。</summary>
    /// <param name="array">要反转的一维 <see cref="T:System.Array" />。</param>
    /// <param name="index">要反转的部分的起始索引。</param>
    /// <param name="length">要反转的部分中的元素数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> 为 null。</exception>
    /// <exception cref="T:System.RankException">
    /// <paramref name="array" /> 是多维的。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 小于 <paramref name="array" /> 的下限。- 或 -<paramref name="length" /> 小于零。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="index" /> 和 <paramref name="length" /> 不指定 <paramref name="array" /> 中的有效范围。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
    [__DynamicallyInvokable]
    public static void Reverse(Array array, int index, int length)
    {
      if (array == null)
        throw new ArgumentNullException("array");
      if (index < array.GetLowerBound(0) || length < 0)
        throw new ArgumentOutOfRangeException(index < 0 ? "index" : "length", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (array.Length - (index - array.GetLowerBound(0)) < length)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      if (array.Rank != 1)
        throw new RankException(Environment.GetResourceString("Rank_MultiDimNotSupported"));
      if (Array.TrySZReverse(array, index, length))
        return;
      int index1 = index;
      int index2 = index + length - 1;
      object[] objArray = array as object[];
      if (objArray != null)
      {
        for (; index1 < index2; --index2)
        {
          object obj = objArray[index1];
          objArray[index1] = objArray[index2];
          objArray[index2] = obj;
          ++index1;
        }
      }
      else
      {
        for (; index1 < index2; --index2)
        {
          object obj1 = array.GetValue(index1);
          Array array1 = array;
          int index3 = index2;
          object obj2 = array1.GetValue(index3);
          int index4 = index1;
          array1.SetValue(obj2, index4);
          array.SetValue(obj1, index2);
          ++index1;
        }
      }
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern bool TrySZReverse(Array array, int index, int count);

    /// <summary>使用 <see cref="T:System.Array" /> 中每个元素的 <see cref="T:System.IComparable" /> 实现，对整个一维 <see cref="T:System.Array" /> 中的元素进行排序。</summary>
    /// <param name="array">要排序的一维 <see cref="T:System.Array" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> 为 null。</exception>
    /// <exception cref="T:System.RankException">
    /// <paramref name="array" /> 是多维的。</exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// <paramref name="array" /> 中的一个或多个元素未实现 <see cref="T:System.IComparable" /> 接口。</exception>
    /// <filterpriority>1</filterpriority>
    [ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
    [__DynamicallyInvokable]
    public static void Sort(Array array)
    {
      if (array == null)
        throw new ArgumentNullException("array");
      Array.Sort(array, (Array) null, array.GetLowerBound(0), array.Length, (IComparer) null);
    }

    /// <summary>基于第一个 <see cref="T:System.Array" /> 中的关键字，使用每个关键字的 <see cref="T:System.IComparable" /> 实现，对两个一维 <see cref="T:System.Array" /> 对象（一个包含关键字，另一个包含对应的项）进行排序。</summary>
    /// <param name="keys">一维 <see cref="T:System.Array" />，它包含要排序的关键字。</param>
    /// <param name="items">一维 <see cref="T:System.Array" />，其中包含与 <paramref name="keys" /><see cref="T:System.Array" /> 中每个键对应的项。- 或 -null 则只对 <paramref name="keys" /><see cref="T:System.Array" /> 进行排序。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="keys" /> 为 null。</exception>
    /// <exception cref="T:System.RankException">
    /// <paramref name="keys" />
    /// <see cref="T:System.Array" /> 是多维的。- 或 -<paramref name="items" /><see cref="T:System.Array" /> 是多维的。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="items" /> 不是 null，并且 <paramref name="keys" /> 的长度大于 <paramref name="items" /> 的长度。</exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// <paramref name="keys" />
    /// <see cref="T:System.Array" /> 中的一个或多个元素未实现 <see cref="T:System.IComparable" /> 接口。</exception>
    /// <filterpriority>1</filterpriority>
    [ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
    [__DynamicallyInvokable]
    public static void Sort(Array keys, Array items)
    {
      if (keys == null)
        throw new ArgumentNullException("keys");
      Array.Sort(keys, items, keys.GetLowerBound(0), keys.Length, (IComparer) null);
    }

    /// <summary>使用 <see cref="T:System.Array" /> 中每个元素的 <see cref="T:System.IComparable" /> 实现，对一维 <see cref="T:System.Array" /> 中某部分元素进行排序。</summary>
    /// <param name="array">要排序的一维 <see cref="T:System.Array" />。</param>
    /// <param name="index">排序范围的起始索引。</param>
    /// <param name="length">排序范围内的元素数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> 为 null。</exception>
    /// <exception cref="T:System.RankException">
    /// <paramref name="array" /> 是多维的。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 小于 <paramref name="array" /> 的下限。- 或 -<paramref name="length" /> 小于零。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="index" /> 和 <paramref name="length" /> 不指定 <paramref name="array" /> 中的有效范围。</exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// <paramref name="array" /> 中的一个或多个元素未实现 <see cref="T:System.IComparable" /> 接口。</exception>
    /// <filterpriority>1</filterpriority>
    [ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
    [__DynamicallyInvokable]
    public static void Sort(Array array, int index, int length)
    {
      Array.Sort(array, (Array) null, index, length, (IComparer) null);
    }

    /// <summary>基于第一个 <see cref="T:System.Array" /> 中的关键字，使用每个关键字的 <see cref="T:System.IComparable" /> 实现，对两个一维 <see cref="T:System.Array" /> 对象（一个包含关键字，另一个包含对应的项）的部分元素进行排序。</summary>
    /// <param name="keys">一维 <see cref="T:System.Array" />，它包含要排序的关键字。</param>
    /// <param name="items">一维 <see cref="T:System.Array" />，其中包含与 <paramref name="keys" /><see cref="T:System.Array" /> 中每个键对应的项。- 或 -null 则只对 <paramref name="keys" /><see cref="T:System.Array" /> 进行排序。</param>
    /// <param name="index">排序范围的起始索引。</param>
    /// <param name="length">排序范围内的元素数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="keys" /> 为 null。</exception>
    /// <exception cref="T:System.RankException">
    /// <paramref name="keys" />
    /// <see cref="T:System.Array" /> 是多维的。- 或 -<paramref name="items" /><see cref="T:System.Array" /> 是多维的。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 小于 <paramref name="keys" /> 的下限。- 或 -<paramref name="length" /> 小于零。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="items" /> 不是 null，并且 <paramref name="keys" /> 的长度大于 <paramref name="items" /> 的长度。- 或 -<paramref name="index" /> 和 <paramref name="length" /> 未指定 <paramref name="keys" /><see cref="T:System.Array" /> 中的有效范围。- 或 -<paramref name="items" /> 不为 null，并且 <paramref name="index" /> 和 <paramref name="length" /> 未在 <paramref name="items" /><see cref="T:System.Array" /> 中指定有效范围。</exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// <paramref name="keys" />
    /// <see cref="T:System.Array" /> 中的一个或多个元素未实现 <see cref="T:System.IComparable" /> 接口。</exception>
    /// <filterpriority>1</filterpriority>
    [ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
    [__DynamicallyInvokable]
    public static void Sort(Array keys, Array items, int index, int length)
    {
      Array.Sort(keys, items, index, length, (IComparer) null);
    }

    /// <summary>使用指定的 <see cref="T:System.Collections.IComparer" />，对一维 <see cref="T:System.Array" /> 中的元素进行排序。</summary>
    /// <param name="array">要排序的一维数组。</param>
    /// <param name="comparer">比较元素时要使用的实现。- 或 -若为 null，则使用每个元素的 <see cref="T:System.IComparable" /> 实现。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> 为 null。</exception>
    /// <exception cref="T:System.RankException">
    /// <paramref name="array" /> 是多维的。</exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// <paramref name="comparer" /> 为 null，<paramref name="array" /> 中的一个或多个元素不实现 <see cref="T:System.IComparable" /> 接口。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="comparer" /> 的实现导致排序时出现错误。例如，将某个项与其自身进行比较时，<paramref name="comparer" /> 可能不返回 0。</exception>
    /// <filterpriority>1</filterpriority>
    [ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
    [__DynamicallyInvokable]
    public static void Sort(Array array, IComparer comparer)
    {
      if (array == null)
        throw new ArgumentNullException("array");
      Array.Sort(array, (Array) null, array.GetLowerBound(0), array.Length, comparer);
    }

    /// <summary>基于第一个 <see cref="T:System.Array" /> 中的关键字，使用指定的 <see cref="T:System.Collections.IComparer" />，对两个一维 <see cref="T:System.Array" /> 对象（一个包含关键字，另一个包含对应的项）进行排序。</summary>
    /// <param name="keys">一维 <see cref="T:System.Array" />，它包含要排序的关键字。</param>
    /// <param name="items">一维 <see cref="T:System.Array" />，其中包含与 <paramref name="keys" /><see cref="T:System.Array" /> 中每个键对应的项。- 或 -null 则只对 <paramref name="keys" /><see cref="T:System.Array" /> 进行排序。</param>
    /// <param name="comparer">比较元素时要使用的 <see cref="T:System.Collections.IComparer" /> 实现。- 或 -若为 null，则使用每个元素的 <see cref="T:System.IComparable" /> 实现。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="keys" /> 为 null。</exception>
    /// <exception cref="T:System.RankException">
    /// <paramref name="keys" />
    /// <see cref="T:System.Array" /> 是多维的。- 或 -<paramref name="items" /><see cref="T:System.Array" /> 是多维的。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="items" /> 不是 null，并且 <paramref name="keys" /> 的长度大于 <paramref name="items" /> 的长度。- 或 -<paramref name="comparer" /> 的实现导致排序时出现错误。例如，将某个项与其自身进行比较时，<paramref name="comparer" /> 可能不返回 0。</exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// <paramref name="comparer" /> 为 null，并且 <paramref name="keys" /><see cref="T:System.Array" /> 中的一个或多个元素不实现 <see cref="T:System.IComparable" /> 接口。</exception>
    /// <filterpriority>1</filterpriority>
    [ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
    [__DynamicallyInvokable]
    public static void Sort(Array keys, Array items, IComparer comparer)
    {
      if (keys == null)
        throw new ArgumentNullException("keys");
      Array.Sort(keys, items, keys.GetLowerBound(0), keys.Length, comparer);
    }

    /// <summary>使用指定的 <see cref="T:System.Collections.IComparer" />，对一维 <see cref="T:System.Array" /> 的部分元素进行排序。</summary>
    /// <param name="array">要排序的一维 <see cref="T:System.Array" />。</param>
    /// <param name="index">排序范围的起始索引。</param>
    /// <param name="length">排序范围内的元素数。</param>
    /// <param name="comparer">比较元素时要使用的 <see cref="T:System.Collections.IComparer" /> 实现。- 或 -若为 null，则使用每个元素的 <see cref="T:System.IComparable" /> 实现。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> 为 null。</exception>
    /// <exception cref="T:System.RankException">
    /// <paramref name="array" /> 是多维的。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 小于 <paramref name="array" /> 的下限。- 或 -<paramref name="length" /> 小于零。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="index" /> 和 <paramref name="length" /> 不指定 <paramref name="array" /> 中的有效范围。- 或 -<paramref name="comparer" /> 的实现导致排序时出现错误。例如，将某个项与其自身进行比较时，<paramref name="comparer" /> 可能不返回 0。</exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// <paramref name="comparer" /> 为 null，<paramref name="array" /> 中的一个或多个元素不实现 <see cref="T:System.IComparable" /> 接口。</exception>
    /// <filterpriority>1</filterpriority>
    [ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
    [__DynamicallyInvokable]
    public static void Sort(Array array, int index, int length, IComparer comparer)
    {
      Array.Sort(array, (Array) null, index, length, comparer);
    }

    /// <summary>基于第一个 <see cref="T:System.Array" /> 中的关键字，使用指定的 <see cref="T:System.Collections.IComparer" />，对两个一维 <see cref="T:System.Array" /> 对象（一个包含关键字，另一个包含对应的项）的部分元素进行排序。</summary>
    /// <param name="keys">一维 <see cref="T:System.Array" />，它包含要排序的关键字。</param>
    /// <param name="items">一维 <see cref="T:System.Array" />，其中包含与 <paramref name="keys" /><see cref="T:System.Array" /> 中每个键对应的项。- 或 -null 则只对 <paramref name="keys" /><see cref="T:System.Array" /> 进行排序。</param>
    /// <param name="index">排序范围的起始索引。</param>
    /// <param name="length">排序范围内的元素数。</param>
    /// <param name="comparer">比较元素时要使用的 <see cref="T:System.Collections.IComparer" /> 实现。- 或 -若为 null，则使用每个元素的 <see cref="T:System.IComparable" /> 实现。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="keys" /> 为 null。</exception>
    /// <exception cref="T:System.RankException">
    /// <paramref name="keys" />
    /// <see cref="T:System.Array" /> 是多维的。- 或 -<paramref name="items" /><see cref="T:System.Array" /> 是多维的。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 小于 <paramref name="keys" /> 的下限。- 或 -<paramref name="length" /> 小于零。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="items" /> 不是 null，且 <paramref name="keys" /> 的下限与 <paramref name="items" /> 的下限不匹配。- 或 -<paramref name="items" /> 不是 null，并且 <paramref name="keys" /> 的长度大于 <paramref name="items" /> 的长度。- 或 -<paramref name="index" /> 和 <paramref name="length" /> 未指定 <paramref name="keys" /><see cref="T:System.Array" /> 中的有效范围。- 或 -<paramref name="items" /> 不为 null，并且 <paramref name="index" /> 和 <paramref name="length" /> 未在 <paramref name="items" /><see cref="T:System.Array" /> 中指定有效范围。- 或 -<paramref name="comparer" /> 的实现导致排序时出现错误。例如，将某个项与其自身进行比较时，<paramref name="comparer" /> 可能不返回 0。</exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// <paramref name="comparer" /> 为 null，并且 <paramref name="keys" /><see cref="T:System.Array" /> 中的一个或多个元素不实现 <see cref="T:System.IComparable" /> 接口。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
    [__DynamicallyInvokable]
    public static void Sort(Array keys, Array items, int index, int length, IComparer comparer)
    {
      if (keys == null)
        throw new ArgumentNullException("keys");
      if (keys.Rank != 1 || items != null && items.Rank != 1)
        throw new RankException(Environment.GetResourceString("Rank_MultiDimNotSupported"));
      if (items != null && keys.GetLowerBound(0) != items.GetLowerBound(0))
        throw new ArgumentException(Environment.GetResourceString("Arg_LowerBoundsMustMatch"));
      if (index < keys.GetLowerBound(0) || length < 0)
        throw new ArgumentOutOfRangeException(length < 0 ? "length" : "index", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (keys.Length - (index - keys.GetLowerBound(0)) < length || items != null && index - items.GetLowerBound(0) > items.Length - length)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      if (length <= 1)
        return;
      if (comparer == Comparer.Default || comparer == null)
      {
        Array keys1 = keys;
        Array items1 = items;
        int left = index;
        int num = length;
        int right = left + num - 1;
        if (Array.TrySZSort(keys1, items1, left, right))
          return;
      }
      object[] keys2 = keys as object[];
      object[] items2 = (object[]) null;
      if (keys2 != null)
        items2 = items as object[];
      if (keys2 != null && (items == null || items2 != null))
        new Array.SorterObjectArray(keys2, items2, comparer).Sort(index, length);
      else
        new Array.SorterGenericArray(keys, items, comparer).Sort(index, length);
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern bool TrySZSort(Array keys, Array items, int left, int right);

    /// <summary>使用 <see cref="T:System.Array" /> 的每个元素的 <see cref="T:System.IComparable`1" /> 泛型接口实现，对整个 <see cref="T:System.Array" /> 中的元素进行排序。</summary>
    /// <param name="array">要排序的从零开始的一维 <see cref="T:System.Array" />。</param>
    /// <typeparam name="T">数组元素的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// <paramref name="array" /> 中的一个或多个元素未实现 <see cref="T:System.IComparable`1" /> 泛型接口。</exception>
    [ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
    [__DynamicallyInvokable]
    public static void Sort<T>(T[] array)
    {
      if (array == null)
        throw new ArgumentNullException("array");
      T[] array1 = array;
      int dimension = 0;
      int lowerBound = array1.GetLowerBound(dimension);
      int length = array.Length;
      // ISSUE: variable of the null type
      __Null local = null;
      Array.Sort<T>(array1, lowerBound, length, (IComparer<T>) local);
    }

    /// <summary>基于第一个 <see cref="T:System.Array" /> 中的关键字，使用每个关键字的 <see cref="T:System.IComparable`1" /> 泛型接口实现，对两个 <see cref="T:System.Array" /> 对象（一个包含关键字，另一个包含对应的项）进行排序。</summary>
    /// <param name="keys">从零开始的一维 <see cref="T:System.Array" />，它包含要排序的关键字。</param>
    /// <param name="items">从零开始的一维 <see cref="T:System.Array" />，其中包含与 <paramref name="keys" /> 中的关键字对应的项；如果为 null，则只对 <paramref name="keys" /> 进行排序。</param>
    /// <typeparam name="TKey">关键字数组元素的类型。</typeparam>
    /// <typeparam name="TValue">项数组元素的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="keys" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="items" /> 不是 null，且 <paramref name="keys" /> 的下限与 <paramref name="items" /> 的下限不匹配。- 或 -<paramref name="items" /> 不是 null，并且 <paramref name="keys" /> 的长度大于 <paramref name="items" /> 的长度。</exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// <paramref name="keys" />
    /// <see cref="T:System.Array" /> 中的一个或多个元素未实现 <see cref="T:System.IComparable`1" /> 泛型接口。</exception>
    [ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
    [__DynamicallyInvokable]
    public static void Sort<TKey, TValue>(TKey[] keys, TValue[] items)
    {
      if (keys == null)
        throw new ArgumentNullException("keys");
      Array.Sort<TKey, TValue>(keys, items, 0, keys.Length, (IComparer<TKey>) null);
    }

    /// <summary>使用 <see cref="T:System.Array" /> 的每个元素的 <see cref="T:System.IComparable`1" /> 泛型接口实现，对 <see cref="T:System.Array" /> 中某个元素范围内的元素进行排序。</summary>
    /// <param name="array">要排序的从零开始的一维 <see cref="T:System.Array" />。</param>
    /// <param name="index">排序范围的起始索引。</param>
    /// <param name="length">排序范围内的元素数。</param>
    /// <typeparam name="T">数组元素的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 小于 <paramref name="array" /> 的下限。- 或 -<paramref name="length" /> 小于零。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="index" /> 和 <paramref name="length" /> 不指定 <paramref name="array" /> 中的有效范围。</exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// <paramref name="array" /> 中的一个或多个元素未实现 <see cref="T:System.IComparable`1" /> 泛型接口。</exception>
    [ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
    [__DynamicallyInvokable]
    public static void Sort<T>(T[] array, int index, int length)
    {
      Array.Sort<T>(array, index, length, (IComparer<T>) null);
    }

    /// <summary>基于第一个 <see cref="T:System.Array" /> 中的关键字，使用每个关键字的 <see cref="T:System.IComparable`1" /> 泛型接口实现，对两个 <see cref="T:System.Array" /> 对象（一个包含关键字，另一个包含对应的项）的部分元素进行排序。</summary>
    /// <param name="keys">从零开始的一维 <see cref="T:System.Array" />，它包含要排序的关键字。</param>
    /// <param name="items">从零开始的一维 <see cref="T:System.Array" />，其中包含与 <paramref name="keys" /> 中的关键字对应的项；如果为 null，则只对 <paramref name="keys" /> 进行排序。</param>
    /// <param name="index">排序范围的起始索引。</param>
    /// <param name="length">排序范围内的元素数。</param>
    /// <typeparam name="TKey">关键字数组元素的类型。</typeparam>
    /// <typeparam name="TValue">项数组元素的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="keys" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 小于 <paramref name="keys" /> 的下限。- 或 -<paramref name="length" /> 小于零。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="items" /> 不是 null，且 <paramref name="keys" /> 的下限与 <paramref name="items" /> 的下限不匹配。- 或 -<paramref name="items" /> 不是 null，并且 <paramref name="keys" /> 的长度大于 <paramref name="items" /> 的长度。- 或 -<paramref name="index" /> 和 <paramref name="length" /> 未指定 <paramref name="keys" /><see cref="T:System.Array" /> 中的有效范围。- 或 -<paramref name="items" /> 不为 null，并且 <paramref name="index" /> 和 <paramref name="length" /> 未在 <paramref name="items" /><see cref="T:System.Array" /> 中指定有效范围。</exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// <paramref name="keys" />
    /// <see cref="T:System.Array" /> 中的一个或多个元素未实现 <see cref="T:System.IComparable`1" /> 泛型接口。</exception>
    [ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
    [__DynamicallyInvokable]
    public static void Sort<TKey, TValue>(TKey[] keys, TValue[] items, int index, int length)
    {
      Array.Sort<TKey, TValue>(keys, items, index, length, (IComparer<TKey>) null);
    }

    /// <summary>使用指定的 <see cref="T:System.Collections.Generic.IComparer`1" /> 泛型接口，对 <see cref="T:System.Array" /> 中的元素进行排序。</summary>
    /// <param name="array">要排序的从零开始的一维 <see cref="T:System.Array" /></param>
    /// <param name="comparer">比较元素时使用的 <see cref="T:System.Collections.Generic.IComparer`1" /> 泛型接口实现；如果为 null，则使用每个元素的 <see cref="T:System.IComparable`1" /> 泛型接口实现。</param>
    /// <typeparam name="T">数组元素的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// <paramref name="comparer" /> 为 null，并且 <paramref name="array" /> 中的一个或多个元素未实现 <see cref="T:System.IComparable`1" /> 泛型接口。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="comparer" /> 的实现导致排序时出现错误。例如，将某个项与其自身进行比较时，<paramref name="comparer" /> 可能不返回 0。</exception>
    [ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
    [__DynamicallyInvokable]
    public static void Sort<T>(T[] array, IComparer<T> comparer)
    {
      if (array == null)
        throw new ArgumentNullException("array");
      Array.Sort<T>(array, 0, array.Length, comparer);
    }

    /// <summary>基于第一个 <see cref="T:System.Array" /> 中的关键字，使用指定的 <see cref="T:System.Collections.Generic.IComparer`1" /> 泛型接口，对两个 <see cref="T:System.Array" /> 对象（一个包含关键字，另一个包含对应的项）进行排序。</summary>
    /// <param name="keys">从零开始的一维 <see cref="T:System.Array" />，它包含要排序的关键字。</param>
    /// <param name="items">从零开始的一维 <see cref="T:System.Array" />，其中包含与 <paramref name="keys" /> 中的关键字对应的项；如果为 null，则只对 <paramref name="keys" /> 进行排序。</param>
    /// <param name="comparer">比较元素时使用的 <see cref="T:System.Collections.Generic.IComparer`1" /> 泛型接口实现；如果为 null，则使用每个元素的 <see cref="T:System.IComparable`1" /> 泛型接口实现。</param>
    /// <typeparam name="TKey">关键字数组元素的类型。</typeparam>
    /// <typeparam name="TValue">项数组元素的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="keys" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="items" /> 不是 null，且 <paramref name="keys" /> 的下限与 <paramref name="items" /> 的下限不匹配。- 或 -<paramref name="items" /> 不是 null，并且 <paramref name="keys" /> 的长度大于 <paramref name="items" /> 的长度。- 或 -<paramref name="comparer" /> 的实现导致排序时出现错误。例如，将某个项与其自身进行比较时，<paramref name="comparer" /> 可能不返回 0。</exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// <paramref name="comparer" /> 为 null，并且 <paramref name="keys" /><see cref="T:System.Array" /> 中的一个或多个元素未实现 <see cref="T:System.IComparable`1" /> 泛型接口。</exception>
    [ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
    [__DynamicallyInvokable]
    public static void Sort<TKey, TValue>(TKey[] keys, TValue[] items, IComparer<TKey> comparer)
    {
      if (keys == null)
        throw new ArgumentNullException("keys");
      Array.Sort<TKey, TValue>(keys, items, 0, keys.Length, comparer);
    }

    /// <summary>使用指定的 <see cref="T:System.Collections.Generic.IComparer`1" /> 泛型接口，对 <see cref="T:System.Array" /> 中某个元素范围内的元素进行排序。</summary>
    /// <param name="array">要排序的从零开始的一维 <see cref="T:System.Array" />。</param>
    /// <param name="index">排序范围的起始索引。</param>
    /// <param name="length">排序范围内的元素数。</param>
    /// <param name="comparer">比较元素时使用的 <see cref="T:System.Collections.Generic.IComparer`1" /> 泛型接口实现；如果为 null，则使用每个元素的 <see cref="T:System.IComparable`1" /> 泛型接口实现。</param>
    /// <typeparam name="T">数组元素的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 小于 <paramref name="array" /> 的下限。- 或 -<paramref name="length" /> 小于零。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="index" /> 和 <paramref name="length" /> 不指定 <paramref name="array" /> 中的有效范围。- 或 -<paramref name="comparer" /> 的实现导致排序时出现错误。例如，将某个项与其自身进行比较时，<paramref name="comparer" /> 可能不返回 0。</exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// <paramref name="comparer" /> 为 null，并且 <paramref name="array" /> 中的一个或多个元素未实现 <see cref="T:System.IComparable`1" /> 泛型接口。</exception>
    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
    [__DynamicallyInvokable]
    public static void Sort<T>(T[] array, int index, int length, IComparer<T> comparer)
    {
      if (array == null)
        throw new ArgumentNullException("array");
      if (index < 0 || length < 0)
        throw new ArgumentOutOfRangeException(length < 0 ? "length" : "index", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (array.Length - index < length)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      if (length <= 1)
        return;
      if (comparer == null || comparer == Comparer<T>.Default)
      {
        T[] objArray = array;
        // ISSUE: variable of the null type
        __Null local = null;
        int left = index;
        int num = length;
        int right = left + num - 1;
        if (Array.TrySZSort((Array) objArray, (Array) local, left, right))
          return;
      }
      ArraySortHelper<T>.Default.Sort(array, index, length, comparer);
    }

    /// <summary>基于第一个 <see cref="T:System.Array" /> 中的关键字，使用指定的 <see cref="T:System.Collections.Generic.IComparer`1" /> 泛型接口，对两个 <see cref="T:System.Array" /> 对象（一个包含关键字，另一个包含对应的项）的部分元素进行排序。</summary>
    /// <param name="keys">从零开始的一维 <see cref="T:System.Array" />，它包含要排序的关键字。</param>
    /// <param name="items">从零开始的一维 <see cref="T:System.Array" />，其中包含与 <paramref name="keys" /> 中的关键字对应的项；如果为 null，则只对 <paramref name="keys" /> 进行排序。</param>
    /// <param name="index">排序范围的起始索引。</param>
    /// <param name="length">排序范围内的元素数。</param>
    /// <param name="comparer">比较元素时使用的 <see cref="T:System.Collections.Generic.IComparer`1" /> 泛型接口实现；如果为 null，则使用每个元素的 <see cref="T:System.IComparable`1" /> 泛型接口实现。</param>
    /// <typeparam name="TKey">关键字数组元素的类型。</typeparam>
    /// <typeparam name="TValue">项数组元素的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="keys" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 小于 <paramref name="keys" /> 的下限。- 或 -<paramref name="length" /> 小于零。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="items" /> 不是 null，且 <paramref name="keys" /> 的下限与 <paramref name="items" /> 的下限不匹配。- 或 -<paramref name="items" /> 不是 null，并且 <paramref name="keys" /> 的长度大于 <paramref name="items" /> 的长度。- 或 -<paramref name="index" /> 和 <paramref name="length" /> 未指定 <paramref name="keys" /><see cref="T:System.Array" /> 中的有效范围。- 或 -<paramref name="items" /> 不为 null，并且 <paramref name="index" /> 和 <paramref name="length" /> 未在 <paramref name="items" /><see cref="T:System.Array" /> 中指定有效范围。- 或 -<paramref name="comparer" /> 的实现导致排序时出现错误。例如，将某个项与其自身进行比较时，<paramref name="comparer" /> 可能不返回 0。</exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// <paramref name="comparer" /> 为 null，并且 <paramref name="keys" /><see cref="T:System.Array" /> 中的一个或多个元素未实现 <see cref="T:System.IComparable`1" /> 泛型接口。</exception>
    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
    [__DynamicallyInvokable]
    public static void Sort<TKey, TValue>(TKey[] keys, TValue[] items, int index, int length, IComparer<TKey> comparer)
    {
      if (keys == null)
        throw new ArgumentNullException("keys");
      if (index < 0 || length < 0)
        throw new ArgumentOutOfRangeException(length < 0 ? "length" : "index", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (keys.Length - index < length || items != null && index > items.Length - length)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      if (length <= 1)
        return;
      if (comparer == null || comparer == Comparer<TKey>.Default)
      {
        TKey[] keyArray = keys;
        TValue[] objArray = items;
        int left = index;
        int num = length;
        int right = left + num - 1;
        if (Array.TrySZSort((Array) keyArray, (Array) objArray, left, right))
          return;
      }
      if (items == null)
        Array.Sort<TKey>(keys, index, length, comparer);
      else
        ArraySortHelper<TKey, TValue>.Default.Sort(keys, items, index, length, comparer);
    }

    /// <summary>使用指定的 <see cref="T:System.Comparison`1" /> 对 <see cref="T:System.Array" /> 中的元素进行排序。</summary>
    /// <param name="array">要排序的从零开始的一维 <see cref="T:System.Array" />。</param>
    /// <param name="comparison">比较元素时要使用的 <see cref="T:System.Comparison`1" />。</param>
    /// <typeparam name="T">数组元素的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> 为 null。- 或 -<paramref name="comparison" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="comparison" /> 的实现导致排序时出现错误。例如，将某个项与其自身进行比较时，<paramref name="comparison" /> 可能不返回 0。</exception>
    [__DynamicallyInvokable]
    public static void Sort<T>(T[] array, Comparison<T> comparison)
    {
      if (array == null)
        throw new ArgumentNullException("array");
      if (comparison == null)
        throw new ArgumentNullException("comparison");
      IComparer<T> comparer = (IComparer<T>) new Array.FunctorComparer<T>(comparison);
      Array.Sort<T>(array, comparer);
    }

    /// <summary>确定数组中的每个元素是否都与指定谓词定义的条件匹配。</summary>
    /// <returns>如果 <paramref name="array" /> 中的每个元素都与指定谓词定义的条件匹配，则为 true；否则为 false。如果数组中没有元素，则返回值为 true。</returns>
    /// <param name="array">要对照条件进行检查的从零开始的一维 <see cref="T:System.Array" />。</param>
    /// <param name="match">用于定义检查元素时要对照的条件的谓词。</param>
    /// <typeparam name="T">数组元素的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> 为 null。- 或 -<paramref name="match" /> 为 null。</exception>
    [__DynamicallyInvokable]
    public static bool TrueForAll<T>(T[] array, Predicate<T> match)
    {
      if (array == null)
        throw new ArgumentNullException("array");
      if (match == null)
        throw new ArgumentNullException("match");
      for (int index = 0; index < array.Length; ++index)
      {
        if (!match(array[index]))
          return false;
      }
      return true;
    }

    /// <summary>通过调用值类型的默认构造函数，初始化值类型 <see cref="T:System.Array" /> 的每一个元素。</summary>
    /// <filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public void Initialize();

    internal sealed class FunctorComparer<T> : IComparer<T>
    {
      private Comparison<T> comparison;

      public FunctorComparer(Comparison<T> comparison)
      {
        this.comparison = comparison;
      }

      public int Compare(T x, T y)
      {
        return this.comparison(x, y);
      }
    }

    private struct SorterObjectArray
    {
      private object[] keys;
      private object[] items;
      private IComparer comparer;

      internal SorterObjectArray(object[] keys, object[] items, IComparer comparer)
      {
        if (comparer == null)
          comparer = (IComparer) Comparer.Default;
        this.keys = keys;
        this.items = items;
        this.comparer = comparer;
      }

      internal void SwapIfGreaterWithItems(int a, int b)
      {
        if (a == b || this.comparer.Compare(this.keys[a], this.keys[b]) <= 0)
          return;
        object obj1 = this.keys[a];
        this.keys[a] = this.keys[b];
        this.keys[b] = obj1;
        if (this.items == null)
          return;
        object obj2 = this.items[a];
        this.items[a] = this.items[b];
        this.items[b] = obj2;
      }

      private void Swap(int i, int j)
      {
        object obj1 = this.keys[i];
        this.keys[i] = this.keys[j];
        this.keys[j] = obj1;
        if (this.items == null)
          return;
        object obj2 = this.items[i];
        this.items[i] = this.items[j];
        this.items[j] = obj2;
      }

      internal void Sort(int left, int length)
      {
        if (BinaryCompatibility.TargetsAtLeast_Desktop_V4_5)
          this.IntrospectiveSort(left, length);
        else
          this.DepthLimitedQuickSort(left, length + left - 1, 32);
      }

      private void DepthLimitedQuickSort(int left, int right, int depthLimit)
      {
        while (depthLimit != 0)
        {
          int index1 = left;
          int index2 = right;
          int median = Array.GetMedian(index1, index2);
          try
          {
            this.SwapIfGreaterWithItems(index1, median);
            this.SwapIfGreaterWithItems(index1, index2);
            this.SwapIfGreaterWithItems(median, index2);
          }
          catch (Exception ex)
          {
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_IComparerFailed"), ex);
          }
          object obj1 = this.keys[median];
          do
          {
            try
            {
              while (this.comparer.Compare(this.keys[index1], obj1) < 0)
                ++index1;
              while (this.comparer.Compare(obj1, this.keys[index2]) < 0)
                --index2;
            }
            catch (IndexOutOfRangeException ex)
            {
              throw new ArgumentException(Environment.GetResourceString("Arg_BogusIComparer", (object) this.comparer));
            }
            catch (Exception ex)
            {
              throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_IComparerFailed"), ex);
            }
            if (index1 <= index2)
            {
              if (index1 < index2)
              {
                object obj2 = this.keys[index1];
                this.keys[index1] = this.keys[index2];
                this.keys[index2] = obj2;
                if (this.items != null)
                {
                  object obj3 = this.items[index1];
                  this.items[index1] = this.items[index2];
                  this.items[index2] = obj3;
                }
              }
              ++index1;
              --index2;
            }
            else
              break;
          }
          while (index1 <= index2);
          --depthLimit;
          if (index2 - left <= right - index1)
          {
            if (left < index2)
              this.DepthLimitedQuickSort(left, index2, depthLimit);
            left = index1;
          }
          else
          {
            if (index1 < right)
              this.DepthLimitedQuickSort(index1, right, depthLimit);
            right = index2;
          }
          if (left >= right)
            return;
        }
        try
        {
          this.Heapsort(left, right);
        }
        catch (IndexOutOfRangeException ex)
        {
          throw new ArgumentException(Environment.GetResourceString("Arg_BogusIComparer", (object) this.comparer));
        }
        catch (Exception ex)
        {
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_IComparerFailed"), ex);
        }
      }

      private void IntrospectiveSort(int left, int length)
      {
        if (length < 2)
          return;
        try
        {
          this.IntroSort(left, length + left - 1, 2 * IntrospectiveSortUtilities.FloorLog2(this.keys.Length));
        }
        catch (IndexOutOfRangeException ex)
        {
          IntrospectiveSortUtilities.ThrowOrIgnoreBadComparer((object) this.comparer);
        }
        catch (Exception ex)
        {
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_IComparerFailed"), ex);
        }
      }

      private void IntroSort(int lo, int hi, int depthLimit)
      {
        int num1;
        for (; hi > lo; hi = num1 - 1)
        {
          int num2 = hi - lo + 1;
          if (num2 <= 16)
          {
            if (num2 == 1)
              break;
            if (num2 == 2)
            {
              this.SwapIfGreaterWithItems(lo, hi);
              break;
            }
            if (num2 == 3)
            {
              this.SwapIfGreaterWithItems(lo, hi - 1);
              this.SwapIfGreaterWithItems(lo, hi);
              this.SwapIfGreaterWithItems(hi - 1, hi);
              break;
            }
            this.InsertionSort(lo, hi);
            break;
          }
          if (depthLimit == 0)
          {
            this.Heapsort(lo, hi);
            break;
          }
          --depthLimit;
          num1 = this.PickPivotAndPartition(lo, hi);
          this.IntroSort(num1 + 1, hi, depthLimit);
        }
      }

      private int PickPivotAndPartition(int lo, int hi)
      {
        int index = lo + (hi - lo) / 2;
        this.SwapIfGreaterWithItems(lo, index);
        this.SwapIfGreaterWithItems(lo, hi);
        this.SwapIfGreaterWithItems(index, hi);
        object obj = this.keys[index];
        this.Swap(index, hi - 1);
        int i = lo;
        int j = hi - 1;
        while (i < j)
        {
          do
            ;
          while (this.comparer.Compare(this.keys[++i], obj) < 0);
          do
            ;
          while (this.comparer.Compare(obj, this.keys[--j]) < 0);
          if (i < j)
            this.Swap(i, j);
          else
            break;
        }
        this.Swap(i, hi - 1);
        return i;
      }

      private void Heapsort(int lo, int hi)
      {
        int n = hi - lo + 1;
        for (int i = n / 2; i >= 1; --i)
          this.DownHeap(i, n, lo);
        for (int index = n; index > 1; --index)
        {
          int i = lo;
          int num = index;
          int j = i + num - 1;
          this.Swap(i, j);
          this.DownHeap(1, index - 1, lo);
        }
      }

      private void DownHeap(int i, int n, int lo)
      {
        object x = this.keys[lo + i - 1];
        object obj = this.items != null ? this.items[lo + i - 1] : (object) null;
        int num;
        for (; i <= n / 2; i = num)
        {
          num = 2 * i;
          if (num < n && this.comparer.Compare(this.keys[lo + num - 1], this.keys[lo + num]) < 0)
            ++num;
          if (this.comparer.Compare(x, this.keys[lo + num - 1]) < 0)
          {
            this.keys[lo + i - 1] = this.keys[lo + num - 1];
            if (this.items != null)
              this.items[lo + i - 1] = this.items[lo + num - 1];
          }
          else
            break;
        }
        this.keys[lo + i - 1] = x;
        if (this.items == null)
          return;
        this.items[lo + i - 1] = obj;
      }

      private void InsertionSort(int lo, int hi)
      {
        for (int index1 = lo; index1 < hi; ++index1)
        {
          int index2 = index1;
          object x = this.keys[index1 + 1];
          object obj = this.items != null ? this.items[index1 + 1] : (object) null;
          for (; index2 >= lo && this.comparer.Compare(x, this.keys[index2]) < 0; --index2)
          {
            this.keys[index2 + 1] = this.keys[index2];
            if (this.items != null)
              this.items[index2 + 1] = this.items[index2];
          }
          this.keys[index2 + 1] = x;
          if (this.items != null)
            this.items[index2 + 1] = obj;
        }
      }
    }

    private struct SorterGenericArray
    {
      private Array keys;
      private Array items;
      private IComparer comparer;

      internal SorterGenericArray(Array keys, Array items, IComparer comparer)
      {
        if (comparer == null)
          comparer = (IComparer) Comparer.Default;
        this.keys = keys;
        this.items = items;
        this.comparer = comparer;
      }

      internal void SwapIfGreaterWithItems(int a, int b)
      {
        if (a == b || this.comparer.Compare(this.keys.GetValue(a), this.keys.GetValue(b)) <= 0)
          return;
        object obj1 = this.keys.GetValue(a);
        this.keys.SetValue(this.keys.GetValue(b), a);
        this.keys.SetValue(obj1, b);
        if (this.items == null)
          return;
        object obj2 = this.items.GetValue(a);
        this.items.SetValue(this.items.GetValue(b), a);
        this.items.SetValue(obj2, b);
      }

      private void Swap(int i, int j)
      {
        object obj1 = this.keys.GetValue(i);
        this.keys.SetValue(this.keys.GetValue(j), i);
        this.keys.SetValue(obj1, j);
        if (this.items == null)
          return;
        object obj2 = this.items.GetValue(i);
        this.items.SetValue(this.items.GetValue(j), i);
        this.items.SetValue(obj2, j);
      }

      internal void Sort(int left, int length)
      {
        if (BinaryCompatibility.TargetsAtLeast_Desktop_V4_5)
          this.IntrospectiveSort(left, length);
        else
          this.DepthLimitedQuickSort(left, length + left - 1, 32);
      }

      private void DepthLimitedQuickSort(int left, int right, int depthLimit)
      {
        while (depthLimit != 0)
        {
          int num1 = left;
          int num2 = right;
          int median = Array.GetMedian(num1, num2);
          try
          {
            this.SwapIfGreaterWithItems(num1, median);
            this.SwapIfGreaterWithItems(num1, num2);
            this.SwapIfGreaterWithItems(median, num2);
          }
          catch (Exception ex)
          {
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_IComparerFailed"), ex);
          }
          object obj1 = this.keys.GetValue(median);
          do
          {
            try
            {
              while (this.comparer.Compare(this.keys.GetValue(num1), obj1) < 0)
                ++num1;
              while (this.comparer.Compare(obj1, this.keys.GetValue(num2)) < 0)
                --num2;
            }
            catch (IndexOutOfRangeException ex)
            {
              throw new ArgumentException(Environment.GetResourceString("Arg_BogusIComparer", (object) this.comparer));
            }
            catch (Exception ex)
            {
              throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_IComparerFailed"), ex);
            }
            if (num1 <= num2)
            {
              if (num1 < num2)
              {
                object obj2 = this.keys.GetValue(num1);
                this.keys.SetValue(this.keys.GetValue(num2), num1);
                this.keys.SetValue(obj2, num2);
                if (this.items != null)
                {
                  object obj3 = this.items.GetValue(num1);
                  this.items.SetValue(this.items.GetValue(num2), num1);
                  this.items.SetValue(obj3, num2);
                }
              }
              if (num1 != int.MaxValue)
                ++num1;
              if (num2 != int.MinValue)
                --num2;
            }
            else
              break;
          }
          while (num1 <= num2);
          --depthLimit;
          if (num2 - left <= right - num1)
          {
            if (left < num2)
              this.DepthLimitedQuickSort(left, num2, depthLimit);
            left = num1;
          }
          else
          {
            if (num1 < right)
              this.DepthLimitedQuickSort(num1, right, depthLimit);
            right = num2;
          }
          if (left >= right)
            return;
        }
        try
        {
          this.Heapsort(left, right);
        }
        catch (IndexOutOfRangeException ex)
        {
          throw new ArgumentException(Environment.GetResourceString("Arg_BogusIComparer", (object) this.comparer));
        }
        catch (Exception ex)
        {
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_IComparerFailed"), ex);
        }
      }

      private void IntrospectiveSort(int left, int length)
      {
        if (length < 2)
          return;
        try
        {
          this.IntroSort(left, length + left - 1, 2 * IntrospectiveSortUtilities.FloorLog2(this.keys.Length));
        }
        catch (IndexOutOfRangeException ex)
        {
          IntrospectiveSortUtilities.ThrowOrIgnoreBadComparer((object) this.comparer);
        }
        catch (Exception ex)
        {
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_IComparerFailed"), ex);
        }
      }

      private void IntroSort(int lo, int hi, int depthLimit)
      {
        int num1;
        for (; hi > lo; hi = num1 - 1)
        {
          int num2 = hi - lo + 1;
          if (num2 <= 16)
          {
            if (num2 == 1)
              break;
            if (num2 == 2)
            {
              this.SwapIfGreaterWithItems(lo, hi);
              break;
            }
            if (num2 == 3)
            {
              this.SwapIfGreaterWithItems(lo, hi - 1);
              this.SwapIfGreaterWithItems(lo, hi);
              this.SwapIfGreaterWithItems(hi - 1, hi);
              break;
            }
            this.InsertionSort(lo, hi);
            break;
          }
          if (depthLimit == 0)
          {
            this.Heapsort(lo, hi);
            break;
          }
          --depthLimit;
          num1 = this.PickPivotAndPartition(lo, hi);
          this.IntroSort(num1 + 1, hi, depthLimit);
        }
      }

      private int PickPivotAndPartition(int lo, int hi)
      {
        int num = lo + (hi - lo) / 2;
        this.SwapIfGreaterWithItems(lo, num);
        this.SwapIfGreaterWithItems(lo, hi);
        this.SwapIfGreaterWithItems(num, hi);
        object obj = this.keys.GetValue(num);
        this.Swap(num, hi - 1);
        int i = lo;
        int j = hi - 1;
        while (i < j)
        {
          do
            ;
          while (this.comparer.Compare(this.keys.GetValue(++i), obj) < 0);
          do
            ;
          while (this.comparer.Compare(obj, this.keys.GetValue(--j)) < 0);
          if (i < j)
            this.Swap(i, j);
          else
            break;
        }
        this.Swap(i, hi - 1);
        return i;
      }

      private void Heapsort(int lo, int hi)
      {
        int n = hi - lo + 1;
        for (int i = n / 2; i >= 1; --i)
          this.DownHeap(i, n, lo);
        for (int index = n; index > 1; --index)
        {
          int i = lo;
          int num = index;
          int j = i + num - 1;
          this.Swap(i, j);
          this.DownHeap(1, index - 1, lo);
        }
      }

      private void DownHeap(int i, int n, int lo)
      {
        object x = this.keys.GetValue(lo + i - 1);
        object obj = this.items != null ? this.items.GetValue(lo + i - 1) : (object) null;
        int num;
        for (; i <= n / 2; i = num)
        {
          num = 2 * i;
          if (num < n && this.comparer.Compare(this.keys.GetValue(lo + num - 1), this.keys.GetValue(lo + num)) < 0)
            ++num;
          if (this.comparer.Compare(x, this.keys.GetValue(lo + num - 1)) < 0)
          {
            this.keys.SetValue(this.keys.GetValue(lo + num - 1), lo + i - 1);
            if (this.items != null)
              this.items.SetValue(this.items.GetValue(lo + num - 1), lo + i - 1);
          }
          else
            break;
        }
        this.keys.SetValue(x, lo + i - 1);
        if (this.items == null)
          return;
        this.items.SetValue(obj, lo + i - 1);
      }

      private void InsertionSort(int lo, int hi)
      {
        for (int index1 = lo; index1 < hi; ++index1)
        {
          int index2 = index1;
          object x = this.keys.GetValue(index1 + 1);
          object obj = this.items != null ? this.items.GetValue(index1 + 1) : (object) null;
          for (; index2 >= lo && this.comparer.Compare(x, this.keys.GetValue(index2)) < 0; --index2)
          {
            this.keys.SetValue(this.keys.GetValue(index2), index2 + 1);
            if (this.items != null)
              this.items.SetValue(this.items.GetValue(index2), index2 + 1);
          }
          this.keys.SetValue(x, index2 + 1);
          if (this.items != null)
            this.items.SetValue(obj, index2 + 1);
        }
      }
    }

    [Serializable]
    private sealed class SZArrayEnumerator : IEnumerator, ICloneable
    {
      private Array _array;
      private int _index;
      private int _endIndex;

      public object Current
      {
        get
        {
          if (this._index < 0)
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
          if (this._index >= this._endIndex)
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumEnded"));
          return this._array.GetValue(this._index);
        }
      }

      internal SZArrayEnumerator(Array array)
      {
        this._array = array;
        this._index = -1;
        this._endIndex = array.Length;
      }

      public object Clone()
      {
        return this.MemberwiseClone();
      }

      public bool MoveNext()
      {
        if (this._index >= this._endIndex)
          return false;
        this._index = this._index + 1;
        return this._index < this._endIndex;
      }

      public void Reset()
      {
        this._index = -1;
      }
    }

    [Serializable]
    private sealed class ArrayEnumerator : IEnumerator, ICloneable
    {
      private Array array;
      private int index;
      private int endIndex;
      private int startIndex;
      private int[] _indices;
      private bool _complete;

      public object Current
      {
        get
        {
          if (this.index < this.startIndex)
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
          if (this._complete)
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumEnded"));
          return this.array.GetValue(this._indices);
        }
      }

      internal ArrayEnumerator(Array array, int index, int count)
      {
        this.array = array;
        this.index = index - 1;
        this.startIndex = index;
        this.endIndex = index + count;
        this._indices = new int[array.Rank];
        int num = 1;
        for (int dimension = 0; dimension < array.Rank; ++dimension)
        {
          this._indices[dimension] = array.GetLowerBound(dimension);
          num *= array.GetLength(dimension);
        }
        --this._indices[this._indices.Length - 1];
        this._complete = num == 0;
      }

      private void IncArray()
      {
        int rank = this.array.Rank;
        ++this._indices[rank - 1];
        for (int dimension1 = rank - 1; dimension1 >= 0; --dimension1)
        {
          if (this._indices[dimension1] > this.array.GetUpperBound(dimension1))
          {
            if (dimension1 == 0)
            {
              this._complete = true;
              break;
            }
            for (int dimension2 = dimension1; dimension2 < rank; ++dimension2)
              this._indices[dimension2] = this.array.GetLowerBound(dimension2);
            ++this._indices[dimension1 - 1];
          }
        }
      }

      public object Clone()
      {
        return this.MemberwiseClone();
      }

      public bool MoveNext()
      {
        if (this._complete)
        {
          this.index = this.endIndex;
          return false;
        }
        this.index = this.index + 1;
        this.IncArray();
        return !this._complete;
      }

      public void Reset()
      {
        this.index = this.startIndex - 1;
        int num = 1;
        for (int dimension = 0; dimension < this.array.Rank; ++dimension)
        {
          this._indices[dimension] = this.array.GetLowerBound(dimension);
          num *= this.array.GetLength(dimension);
        }
        this._complete = num == 0;
        --this._indices[this._indices.Length - 1];
      }
    }
  }
}
