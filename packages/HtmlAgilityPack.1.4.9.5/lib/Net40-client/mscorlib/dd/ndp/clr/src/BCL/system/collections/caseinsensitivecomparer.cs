// Decompiled with JetBrains decompiler
// Type: System.Collections.CaseInsensitiveComparer
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Collections
{
  /// <summary>比较两个对象是否相等，比较时忽略字符串的大小写。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [Serializable]
  public class CaseInsensitiveComparer : IComparer
  {
    private CompareInfo m_compareInfo;
    private static volatile CaseInsensitiveComparer m_InvariantCaseInsensitiveComparer;

    /// <summary>获取 <see cref="T:System.Collections.CaseInsensitiveComparer" /> 的一个实例，该实例与当前线程的 <see cref="P:System.Threading.Thread.CurrentCulture" /> 关联并且始终可用。</summary>
    /// <returns>
    /// <see cref="T:System.Collections.CaseInsensitiveComparer" /> 的实例，它与当前线程的 <see cref="P:System.Threading.Thread.CurrentCulture" /> 关联。</returns>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public static CaseInsensitiveComparer Default
    {
      get
      {
        return new CaseInsensitiveComparer(CultureInfo.CurrentCulture);
      }
    }

    /// <summary>获取 <see cref="T:System.Collections.CaseInsensitiveComparer" /> 的一个实例，该实例与 <see cref="P:System.Globalization.CultureInfo.InvariantCulture" /> 关联并且始终可用。</summary>
    /// <returns>
    /// <see cref="T:System.Collections.CaseInsensitiveComparer" /> 的实例，它与 <see cref="P:System.Globalization.CultureInfo.InvariantCulture" /> 关联。</returns>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public static CaseInsensitiveComparer DefaultInvariant
    {
      get
      {
        if (CaseInsensitiveComparer.m_InvariantCaseInsensitiveComparer == null)
          CaseInsensitiveComparer.m_InvariantCaseInsensitiveComparer = new CaseInsensitiveComparer(CultureInfo.InvariantCulture);
        return CaseInsensitiveComparer.m_InvariantCaseInsensitiveComparer;
      }
    }

    /// <summary>使用当前线程的 <see cref="P:System.Threading.Thread.CurrentCulture" /> 初始化 <see cref="T:System.Collections.CaseInsensitiveComparer" /> 类的新实例。</summary>
    public CaseInsensitiveComparer()
    {
      this.m_compareInfo = CultureInfo.CurrentCulture.CompareInfo;
    }

    /// <summary>使用指定 <see cref="T:System.Globalization.CultureInfo" /> 初始化 <see cref="T:System.Collections.CaseInsensitiveComparer" /> 类的新实例。</summary>
    /// <param name="culture">要用于新 <see cref="T:System.Collections.CaseInsensitiveComparer" /> 的 <see cref="T:System.Globalization.CultureInfo" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="culture" /> 为 null。</exception>
    public CaseInsensitiveComparer(CultureInfo culture)
    {
      if (culture == null)
        throw new ArgumentNullException("culture");
      this.m_compareInfo = culture.CompareInfo;
    }

    /// <summary>对同一类型的两个对象执行不区分大小写的比较，并返回一个值，指示其中一个对象是小于、等于还是大于另一个对象。</summary>
    /// <returns>一个有符号整数，指示 <paramref name="a" /> 与 <paramref name="b" /> 的相对值，如下表所示。值含义小于零在忽略大小写的情况下，<paramref name="a" /> 小于 <paramref name="b" />。零在忽略大小写的情况下，<paramref name="a" /> 等于 <paramref name="b" />。大于零在忽略大小写的情况下，<paramref name="a" /> 大于 <paramref name="b" />。</returns>
    /// <param name="a">要比较的第一个对象。</param>
    /// <param name="b">要比较的第二个对象。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="a" /> 和 <paramref name="b" /> 都不实现 <see cref="T:System.IComparable" /> 接口。- 或 -<paramref name="a" /> 和 <paramref name="b" /> 的类型不同。</exception>
    /// <filterpriority>2</filterpriority>
    public int Compare(object a, object b)
    {
      string string1 = a as string;
      string string2 = b as string;
      if (string1 != null && string2 != null)
        return this.m_compareInfo.Compare(string1, string2, CompareOptions.IgnoreCase);
      return Comparer.Default.Compare(a, b);
    }
  }
}
