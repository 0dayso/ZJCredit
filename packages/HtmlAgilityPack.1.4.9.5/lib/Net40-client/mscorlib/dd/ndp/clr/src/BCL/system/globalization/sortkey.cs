// Decompiled with JetBrains decompiler
// Type: System.Globalization.SortKey
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Globalization
{
  /// <summary>表示将字符串映射到其排序关键字的映射结果。</summary>
  [ComVisible(true)]
  [Serializable]
  public class SortKey
  {
    [OptionalField(VersionAdded = 3)]
    internal string localeName;
    [OptionalField(VersionAdded = 1)]
    internal int win32LCID;
    internal CompareOptions options;
    internal string m_String;
    internal byte[] m_KeyData;

    /// <summary>获取用于创建当前 <see cref="T:System.Globalization.SortKey" /> 对象的原始字符串。</summary>
    /// <returns>获取用于创建当前 <see cref="T:System.Globalization.SortKey" /> 对象的原始字符串。</returns>
    public virtual string OriginalString
    {
      get
      {
        return this.m_String;
      }
    }

    /// <summary>获取表示当前 <see cref="T:System.Globalization.SortKey" /> 对象的字节数组。</summary>
    /// <returns>表示当前 <see cref="T:System.Globalization.SortKey" /> 对象的字节数组。</returns>
    public virtual byte[] KeyData
    {
      get
      {
        return (byte[]) this.m_KeyData.Clone();
      }
    }

    internal SortKey(string localeName, string str, CompareOptions options, byte[] keyData)
    {
      this.m_KeyData = keyData;
      this.localeName = localeName;
      this.options = options;
      this.m_String = str;
    }

    [OnSerializing]
    private void OnSerializing(StreamingContext context)
    {
      if (this.win32LCID != 0)
        return;
      this.win32LCID = CultureInfo.GetCultureInfo(this.localeName).LCID;
    }

    [OnDeserialized]
    private void OnDeserialized(StreamingContext context)
    {
      if (!string.IsNullOrEmpty(this.localeName) || this.win32LCID == 0)
        return;
      this.localeName = CultureInfo.GetCultureInfo(this.win32LCID).Name;
    }

    /// <summary>比较两个排序关键字。</summary>
    /// <returns>一个有符号的整数，它指明 <paramref name="sortkey1" /> 与 <paramref name="sortkey2" /> 之间的关系。值条件小于零 <paramref name="sortkey1" /> 小于 <paramref name="sortkey2" />。零 <paramref name="sortkey1" /> 等于 <paramref name="sortkey2" />。大于零 <paramref name="sortkey1" /> 大于 <paramref name="sortkey2" />。</returns>
    /// <param name="sortkey1">要比较的第一个排序字符串。</param>
    /// <param name="sortkey2">要比较的第二个排序字符串。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="sortkey1" /> or <paramref name="sortkey2" /> is null.</exception>
    public static int Compare(SortKey sortkey1, SortKey sortkey2)
    {
      if (sortkey1 == null || sortkey2 == null)
        throw new ArgumentNullException(sortkey1 == null ? "sortkey1" : "sortkey2");
      byte[] numArray1 = sortkey1.m_KeyData;
      byte[] numArray2 = sortkey2.m_KeyData;
      if (numArray1.Length == 0)
        return numArray2.Length == 0 ? 0 : -1;
      if (numArray2.Length == 0)
        return 1;
      int num = numArray1.Length < numArray2.Length ? numArray1.Length : numArray2.Length;
      for (int index = 0; index < num; ++index)
      {
        if ((int) numArray1[index] > (int) numArray2[index])
          return 1;
        if ((int) numArray1[index] < (int) numArray2[index])
          return -1;
      }
      return 0;
    }

    /// <summary>确定指定的对象是否等于当前的 <see cref="T:System.Globalization.SortKey" /> 对象。</summary>
    /// <returns>如果 <paramref name="value" /> 参数等于当前的 <see cref="T:System.Globalization.SortKey" /> 对象，则为 true；否则为 false。</returns>
    /// <param name="value">将与当前 <see cref="T:System.Globalization.SortKey" /> 对象进行比较的对象。 </param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> is null.</exception>
    public override bool Equals(object value)
    {
      SortKey sortkey2 = value as SortKey;
      if (sortkey2 != null)
        return SortKey.Compare(this, sortkey2) == 0;
      return false;
    }

    /// <summary>用作当前 <see cref="T:System.Globalization.SortKey" /> 对象的哈希函数，适用于哈希算法和数据结构（例如哈希表）中。</summary>
    /// <returns>当前 <see cref="T:System.Globalization.SortKey" /> 对象的哈希代码。</returns>
    public override int GetHashCode()
    {
      return CompareInfo.GetCompareInfo(this.localeName).GetHashCodeOfString(this.m_String, this.options);
    }

    /// <summary>返回表示当前 <see cref="T:System.Globalization.SortKey" /> 对象的字符串。</summary>
    /// <returns>表示当前 <see cref="T:System.Globalization.SortKey" /> 对象的字符串。</returns>
    public override string ToString()
    {
      return "SortKey - " + this.localeName + ", " + (object) this.options + ", " + this.m_String;
    }
  }
}
