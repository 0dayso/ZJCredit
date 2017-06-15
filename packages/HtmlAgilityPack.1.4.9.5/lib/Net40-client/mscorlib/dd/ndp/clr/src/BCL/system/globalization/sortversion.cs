// Decompiled with JetBrains decompiler
// Type: System.Globalization.SortVersion
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Globalization
{
  /// <summary>提供有关用于比较和排序字符串的 Unicode 版本的信息串。</summary>
  [Serializable]
  public sealed class SortVersion : IEquatable<SortVersion>
  {
    private int m_NlsVersion;
    private Guid m_SortId;

    /// <summary>获取 <see cref="T:System.Globalization.SortVersion" /> 对象的全版本号。</summary>
    /// <returns>此 <see cref="T:System.Globalization.SortVersion" /> 对象的版本号。</returns>
    public int FullVersion
    {
      get
      {
        return this.m_NlsVersion;
      }
    }

    /// <summary>获取此 <see cref="T:System.Globalization.SortVersion" /> 对象的全局唯一标识符。</summary>
    /// <returns>此 <see cref="T:System.Globalization.SortVersion" /> 对象的全局唯一标识符。</returns>
    public Guid SortId
    {
      get
      {
        return this.m_SortId;
      }
    }

    /// <summary>创建 <see cref="T:System.Globalization.SortVersion" /> 类的新实例。</summary>
    /// <param name="fullVersion">版本号。</param>
    /// <param name="sortId">排序 ID。</param>
    public SortVersion(int fullVersion, Guid sortId)
    {
      this.m_SortId = sortId;
      this.m_NlsVersion = fullVersion;
    }

    internal SortVersion(int nlsVersion, int effectiveId, Guid customVersion)
    {
      this.m_NlsVersion = nlsVersion;
      if (customVersion == Guid.Empty)
      {
        BitConverter.GetBytes(effectiveId);
        customVersion = new Guid(0, (short) 0, (short) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) ((uint) effectiveId >> 24), (byte) ((effectiveId & 16711680) >> 16), (byte) ((effectiveId & 65280) >> 8), (byte) (effectiveId & (int) byte.MaxValue));
      }
      this.m_SortId = customVersion;
    }

    /// <summary>指示两个 <see cref="T:System.Globalization.SortVersion" /> 实例是否相等。</summary>
    /// <returns>如果 <paramref name="left" /> 和 <paramref name="right" /> 的值相等，则为 true；否则为 false。</returns>
    /// <param name="left">要比较的第一个实例。</param>
    /// <param name="right">要比较的第二个实例。</param>
    public static bool operator ==(SortVersion left, SortVersion right)
    {
      if (left != null)
        return left.Equals(right);
      if (right != null)
        return right.Equals(left);
      return true;
    }

    /// <summary>指示两个 <see cref="T:System.Globalization.SortVersion" /> 实例是否不相等。</summary>
    /// <returns>如果 <paramref name="left" /> 和 <paramref name="right" /> 的值不相等，则为 true；否则为 false。</returns>
    /// <param name="left">要比较的第一个实例。</param>
    /// <param name="right">要比较的第二个实例。</param>
    public static bool operator !=(SortVersion left, SortVersion right)
    {
      return !(left == right);
    }

    /// <summary>返回一个值，该值指示此 <see cref="T:System.Globalization.SortVersion" /> 实例是否与指定的对象相等。</summary>
    /// <returns>如果 <paramref name="obj" /> 是表示与此实例版本相同的 <see cref="T:System.Globalization.SortVersion" /> 对象，则为 true；否则为 false。</returns>
    /// <param name="obj">与此实例进行比较的对象。</param>
    public override bool Equals(object obj)
    {
      SortVersion other = obj as SortVersion;
      if (other != (SortVersion) null)
        return this.Equals(other);
      return false;
    }

    /// <summary>返回一个值，该值指示此 <see cref="T:System.Globalization.SortVersion" /> 实例是否与指定的 <see cref="T:System.Globalization.SortVersion" /> 对象相等。</summary>
    /// <returns>如果 <paramref name="other" /> 与此实例的版本相同，则为 true；否则为 false。</returns>
    /// <param name="other">与该实例进行比较的对象。</param>
    public bool Equals(SortVersion other)
    {
      if (other == (SortVersion) null || this.m_NlsVersion != other.m_NlsVersion)
        return false;
      return this.m_SortId == other.m_SortId;
    }

    /// <summary>返回此实例的哈希代码。</summary>
    /// <returns>32 位有符号整数哈希代码。</returns>
    public override int GetHashCode()
    {
      return this.m_NlsVersion * 7 | this.m_SortId.GetHashCode();
    }
  }
}
