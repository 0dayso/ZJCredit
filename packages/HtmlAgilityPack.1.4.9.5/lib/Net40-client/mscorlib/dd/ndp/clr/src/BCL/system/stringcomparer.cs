// Decompiled with JetBrains decompiler
// Type: System.StringComparer
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System
{
  /// <summary>表示一种字符串比较操作，该操作使用特定的大小写以及基于区域性的比较规则或序号比较规则。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public abstract class StringComparer : IComparer, IEqualityComparer, IComparer<string>, IEqualityComparer<string>
  {
    private static readonly StringComparer _invariantCulture = (StringComparer) new CultureAwareComparer(CultureInfo.InvariantCulture, false);
    private static readonly StringComparer _invariantCultureIgnoreCase = (StringComparer) new CultureAwareComparer(CultureInfo.InvariantCulture, true);
    private static readonly StringComparer _ordinal = (StringComparer) new OrdinalComparer(false);
    private static readonly StringComparer _ordinalIgnoreCase = (StringComparer) new OrdinalComparer(true);

    /// <summary>获取一个 <see cref="T:System.StringComparer" /> 对象，该对象使用固定区域性的单词比较规则执行区分大小写的字符串比较。</summary>
    /// <returns>一个新 <see cref="T:System.StringComparer" /> 对象。</returns>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public static StringComparer InvariantCulture
    {
      get
      {
        return StringComparer._invariantCulture;
      }
    }

    /// <summary>获取一个 <see cref="T:System.StringComparer" /> 对象，该对象使用固定区域性的单词比较规则执行不区分大小写的字符串比较。</summary>
    /// <returns>一个新 <see cref="T:System.StringComparer" /> 对象。</returns>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public static StringComparer InvariantCultureIgnoreCase
    {
      get
      {
        return StringComparer._invariantCultureIgnoreCase;
      }
    }

    /// <summary>获取一个 <see cref="T:System.StringComparer" /> 对象，该对象使用当前区域性的单词比较规则执行区分大小写的字符串比较。</summary>
    /// <returns>一个新 <see cref="T:System.StringComparer" /> 对象。</returns>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [__DynamicallyInvokable]
    public static StringComparer CurrentCulture
    {
      [__DynamicallyInvokable] get
      {
        return (StringComparer) new CultureAwareComparer(CultureInfo.CurrentCulture, false);
      }
    }

    /// <summary>获取一个 <see cref="T:System.StringComparer" /> 对象，该对象使用当前区域性的单词比较规则执行不区分大小写的字符串比较。</summary>
    /// <returns>一个新 <see cref="T:System.StringComparer" /> 对象。</returns>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [__DynamicallyInvokable]
    public static StringComparer CurrentCultureIgnoreCase
    {
      [__DynamicallyInvokable] get
      {
        return (StringComparer) new CultureAwareComparer(CultureInfo.CurrentCulture, true);
      }
    }

    /// <summary>获取一个 <see cref="T:System.StringComparer" /> 对象，该对象执行区分大小写的序号字符串比较。</summary>
    /// <returns>一个 <see cref="T:System.StringComparer" /> 对象。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static StringComparer Ordinal
    {
      [__DynamicallyInvokable] get
      {
        return StringComparer._ordinal;
      }
    }

    /// <summary>获取一个 <see cref="T:System.StringComparer" /> 对象，该对象执行不区分大小写的序号字符串比较。</summary>
    /// <returns>一个 <see cref="T:System.StringComparer" /> 对象。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static StringComparer OrdinalIgnoreCase
    {
      [__DynamicallyInvokable] get
      {
        return StringComparer._ordinalIgnoreCase;
      }
    }

    /// <summary>初始化 <see cref="T:System.StringComparer" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    protected StringComparer()
    {
    }

    /// <summary>创建 <see cref="T:System.StringComparer" /> 对象，该对象根据指定区域性的规则对字符串进行比较。</summary>
    /// <returns>一个新 <see cref="T:System.StringComparer" /> 对象，该对象根据 <paramref name="culture" /> 参数使用的比较规则以及 <paramref name="ignoreCase" /> 参数指定的大小写规则执行字符串比较。</returns>
    /// <param name="culture">一个区域性，其语言规则用于执行字符串比较。</param>
    /// <param name="ignoreCase">true 指定比较操作不区分大小写；false 指定比较操作区分大小写。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="culture" /> 为 null。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static StringComparer Create(CultureInfo culture, bool ignoreCase)
    {
      if (culture == null)
        throw new ArgumentNullException("culture");
      return (StringComparer) new CultureAwareComparer(culture, ignoreCase);
    }

    /// <summary>当在派生类中重写时，将比较两个对象并返回其相对排序顺序的指示。</summary>
    /// <returns>一个有符号整数，指示 <paramref name="x" /> 和 <paramref name="y" /> 的相对值，如下表所示。值含义小于零<paramref name="x" /> 在排序顺序中位于 <paramref name="y" /> 之前。- 或 -<paramref name="x" /> 是 null，且 <paramref name="y" /> 不是 null。零<paramref name="x" /> 等于 <paramref name="y" />。- 或 -<paramref name="x" /> 和 <paramref name="y" /> 均为 null。大于零<paramref name="x" /> 在排序顺序中位于 <paramref name="y" /> 之后。- 或 -<paramref name="y" /> 是 null，且 <paramref name="x" /> 不是 null。</returns>
    /// <param name="x">要与 <paramref name="y" /> 比较的对象。</param>
    /// <param name="y">要与 <paramref name="x" /> 比较的对象。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="x" /> 或 <paramref name="y" /> 都不是 <see cref="T:System.String" /> 对象，并且 <paramref name="x" /> 或 <paramref name="y" /> 都不实现 <see cref="T:System.IComparable" /> 接口。</exception>
    /// <filterpriority>1</filterpriority>
    public int Compare(object x, object y)
    {
      if (x == y)
        return 0;
      if (x == null)
        return -1;
      if (y == null)
        return 1;
      string x1 = x as string;
      if (x1 != null)
      {
        string y1 = y as string;
        if (y1 != null)
          return this.Compare(x1, y1);
      }
      IComparable comparable = x as IComparable;
      if (comparable != null)
        return comparable.CompareTo(y);
      throw new ArgumentException(Environment.GetResourceString("Argument_ImplementIComparable"));
    }

    /// <summary>当在派生类中重写时，指示两个对象是否相等。</summary>
    /// <returns>如果 true 和 <paramref name="x" /> 引用同一对象，或者 <paramref name="y" /> 和 <paramref name="x" /> 是相同的对象类型且这些对象相等，或者 <paramref name="y" /> 和 <paramref name="x" /> 都是 <paramref name="y" />，则为 null；否则为 false。</returns>
    /// <param name="x">要与 <paramref name="y" /> 比较的对象。</param>
    /// <param name="y">要与 <paramref name="x" /> 比较的对象。</param>
    /// <filterpriority>1</filterpriority>
    public bool Equals(object x, object y)
    {
      if (x == y)
        return true;
      if (x == null || y == null)
        return false;
      string x1 = x as string;
      if (x1 != null)
      {
        string y1 = y as string;
        if (y1 != null)
          return this.Equals(x1, y1);
      }
      return x.Equals(y);
    }

    /// <summary>当在派生类中重写时，将获取指定对象的哈希代码。</summary>
    /// <returns>根据 <paramref name="obj" /> 参数的值计算出的 32 位有符号哈希代码。</returns>
    /// <param name="obj">一个对象。</param>
    /// <exception cref="T:System.ArgumentException">没有足够的内存可用于分配计算哈希代码所需的缓冲区。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="obj" /> 为 null。</exception>
    /// <filterpriority>2</filterpriority>
    public int GetHashCode(object obj)
    {
      if (obj == null)
        throw new ArgumentNullException("obj");
      string str = obj as string;
      if (str != null)
        return this.GetHashCode(str);
      return obj.GetHashCode();
    }

    /// <summary>当在派生类中重写时，将比较两个字符串并返回其相对排序顺序的指示。</summary>
    /// <returns>一个有符号整数，指示 <paramref name="x" /> 和 <paramref name="y" /> 的相对值，如下表所示。值含义小于零<paramref name="x" /> 在排序顺序中位于 <paramref name="y" /> 之前。- 或 -<paramref name="x" /> 是 null，且 <paramref name="y" /> 不是 null。零<paramref name="x" /> 等于 <paramref name="y" />。- 或 -<paramref name="x" /> 和 <paramref name="y" /> 均为 null。大于零<paramref name="x" /> 在排序顺序中位于 <paramref name="y" /> 之后。- 或 -<paramref name="y" /> 是 null，且 <paramref name="x" /> 不是 null。</returns>
    /// <param name="x">要与 <paramref name="y" /> 比较的字符串。</param>
    /// <param name="y">要与 <paramref name="x" /> 比较的字符串。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public abstract int Compare(string x, string y);

    /// <summary>当在派生类中重写时，指示两个字符串是否相等。</summary>
    /// <returns>如果 true 和 <paramref name="x" /> 引用同一对象，或者 <paramref name="y" /> 和 <paramref name="x" /> 相等，或者 <paramref name="y" /> 和 <paramref name="x" /> 都是 <paramref name="y" />，则为 null；否则为 false。</returns>
    /// <param name="x">要与 <paramref name="y" /> 比较的字符串。</param>
    /// <param name="y">要与 <paramref name="x" /> 比较的字符串。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public abstract bool Equals(string x, string y);

    /// <summary>当在派生类中重写时，将获取指定字符串的哈希代码。</summary>
    /// <returns>根据 <paramref name="obj" /> 参数的值计算出的 32 位有符号哈希代码。</returns>
    /// <param name="obj">一个字符串。</param>
    /// <exception cref="T:System.ArgumentException">没有足够的内存可用于分配计算哈希代码所需的缓冲区。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="obj" /> 为 null。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public abstract int GetHashCode(string obj);
  }
}
