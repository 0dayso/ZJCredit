// Decompiled with JetBrains decompiler
// Type: System.Globalization.DaylightTime
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Globalization
{
  /// <summary>定义夏时制周期。</summary>
  [ComVisible(true)]
  [Serializable]
  public class DaylightTime
  {
    internal DateTime m_start;
    internal DateTime m_end;
    internal TimeSpan m_delta;

    /// <summary>获取表示夏时制周期开始的日期和时间的对象。</summary>
    /// <returns>夏时制周期开始时表示日期和时间的对象。该值是本地时间。</returns>
    public DateTime Start
    {
      get
      {
        return this.m_start;
      }
    }

    /// <summary>获取表示夏时制周期结束的日期和时间的对象。</summary>
    /// <returns>夏时制周期结束时表示日期和时间的对象。该值是本地时间。</returns>
    public DateTime End
    {
      get
      {
        return this.m_end;
      }
    }

    /// <summary>获取表示标准时间与夏时制之间的时间间隔 。</summary>
    /// <returns>表示标准时间与夏时制之间的时间间隔 。</returns>
    public TimeSpan Delta
    {
      get
      {
        return this.m_delta;
      }
    }

    private DaylightTime()
    {
    }

    /// <summary>使用指定的开始，结束和时差信息初始化 <see cref="T:System.Globalization.DaylightTime" /> 类的新实例。</summary>
    /// <param name="start">夏时制开始时表示日期和时间的对象。该值必须是本地时间。</param>
    /// <param name="end">夏时制结束时表示日期和时间的对象。该值必须是本地时间。</param>
    /// <param name="delta">表示标准时间和夏时制之间的时差的的对象（以计时周期计算）。</param>
    public DaylightTime(DateTime start, DateTime end, TimeSpan delta)
    {
      this.m_start = start;
      this.m_end = end;
      this.m_delta = delta;
    }
  }
}
