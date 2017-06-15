// Decompiled with JetBrains decompiler
// Type: System.Collections.Comparer
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Collections
{
  /// <summary>比较两个对象是否相等，其中字符串比较是区分大小写的。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [Serializable]
  public sealed class Comparer : IComparer, ISerializable
  {
    /// <summary>表示 <see cref="T:System.Collections.Comparer" /> 的实例，它与当前线程的 <see cref="P:System.Threading.Thread.CurrentCulture" /> 关联。此字段为只读。</summary>
    /// <filterpriority>1</filterpriority>
    public static readonly Comparer Default = new Comparer(CultureInfo.CurrentCulture);
    /// <summary>表示 <see cref="T:System.Collections.Comparer" /> 的实例，它与 <see cref="P:System.Globalization.CultureInfo.InvariantCulture" /> 关联。此字段为只读。</summary>
    /// <filterpriority>1</filterpriority>
    public static readonly Comparer DefaultInvariant = new Comparer(CultureInfo.InvariantCulture);
    private CompareInfo m_compareInfo;
    private const string CompareInfoName = "CompareInfo";

    private Comparer()
    {
      this.m_compareInfo = (CompareInfo) null;
    }

    /// <summary>使用指定 <see cref="T:System.Globalization.CultureInfo" /> 初始化 <see cref="T:System.Collections.Comparer" /> 类的新实例。</summary>
    /// <param name="culture">要用于新 <see cref="T:System.Collections.Comparer" /> 的 <see cref="T:System.Globalization.CultureInfo" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="culture" /> 为 null。</exception>
    public Comparer(CultureInfo culture)
    {
      if (culture == null)
        throw new ArgumentNullException("culture");
      this.m_compareInfo = culture.CompareInfo;
    }

    private Comparer(SerializationInfo info, StreamingContext context)
    {
      this.m_compareInfo = (CompareInfo) null;
      SerializationInfoEnumerator enumerator = info.GetEnumerator();
      while (enumerator.MoveNext())
      {
        if (enumerator.Name == "CompareInfo")
          this.m_compareInfo = (CompareInfo) info.GetValue("CompareInfo", typeof (CompareInfo));
      }
    }

    /// <summary>对同一类型的两个对象执行区分大小写的比较，并返回一个值，指示其中一个对象小于、等于还是大于另一个对象。</summary>
    /// <returns>一个有符号整数，指示 <paramref name="a" /> 与 <paramref name="b" /> 的相对值，如下表所示。值含义小于零<paramref name="a" /> 小于 <paramref name="b" />。零<paramref name="a" /> 等于 <paramref name="b" />。大于零<paramref name="a" /> 大于 <paramref name="b" />。</returns>
    /// <param name="a">要比较的第一个对象。</param>
    /// <param name="b">要比较的第二个对象。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="a" /> 和 <paramref name="b" /> 都不实现 <see cref="T:System.IComparable" /> 接口。- 或 -<paramref name="a" /> 和 <paramref name="b" /> 的类型不同，它们都无法处理与另一个进行的比较。</exception>
    /// <filterpriority>2</filterpriority>
    public int Compare(object a, object b)
    {
      if (a == b)
        return 0;
      if (a == null)
        return -1;
      if (b == null)
        return 1;
      if (this.m_compareInfo != null)
      {
        string string1 = a as string;
        string string2 = b as string;
        if (string1 != null && string2 != null)
          return this.m_compareInfo.Compare(string1, string2);
      }
      IComparable comparable1 = a as IComparable;
      if (comparable1 != null)
        return comparable1.CompareTo(b);
      IComparable comparable2 = b as IComparable;
      if (comparable2 != null)
        return -comparable2.CompareTo(a);
      throw new ArgumentException(Environment.GetResourceString("Argument_ImplementIComparable"));
    }

    /// <summary>用序列化所需的数据填充 <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 对象。</summary>
    /// <param name="info">要填充数据的对象。</param>
    /// <param name="context">有关序列化的源或目标的上下文信息。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="info" /> 为 null。</exception>
    /// <filterpriority>2</filterpriority>
    [SecurityCritical]
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException("info");
      if (this.m_compareInfo == null)
        return;
      info.AddValue("CompareInfo", (object) this.m_compareInfo);
    }
  }
}
