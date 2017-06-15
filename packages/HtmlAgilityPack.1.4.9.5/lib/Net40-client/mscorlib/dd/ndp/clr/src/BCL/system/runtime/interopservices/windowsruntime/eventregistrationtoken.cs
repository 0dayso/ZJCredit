// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.EventRegistrationToken
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices.WindowsRuntime
{
  /// <summary>在将事件处理程序添加到 Windows 运行时 事件中时，返回的标志。该标记用于从该事件后移除事件处理程序。</summary>
  [__DynamicallyInvokable]
  public struct EventRegistrationToken
  {
    internal ulong m_value;

    internal ulong Value
    {
      get
      {
        return this.m_value;
      }
    }

    internal EventRegistrationToken(ulong value)
    {
      this.m_value = value;
    }

    /// <summary>指示两个 <see cref="T:System.Runtime.InteropServices.WindowsRuntime.EventRegistrationToken" /> 实例是否相等。</summary>
    /// <returns>如果两个对象相等，则为 true；否则为 false。</returns>
    /// <param name="left">要比较的第一个实例。</param>
    /// <param name="right">要比较的第二个实例。</param>
    [__DynamicallyInvokable]
    public static bool operator ==(EventRegistrationToken left, EventRegistrationToken right)
    {
      return left.Equals((object) right);
    }

    /// <summary>指示两个 <see cref="T:System.Runtime.InteropServices.WindowsRuntime.EventRegistrationToken" /> 实例是否不相等。</summary>
    /// <returns>如果这两个实例不相等，则为 true；否则为 false。</returns>
    /// <param name="left">要比较的第一个实例。</param>
    /// <param name="right">要比较的第二个实例。</param>
    [__DynamicallyInvokable]
    public static bool operator !=(EventRegistrationToken left, EventRegistrationToken right)
    {
      return !left.Equals((object) right);
    }

    /// <summary>返回一个值，该值指示当前的对象是否与指定对象相等。</summary>
    /// <returns>如果当前对象与 <paramref name="obj" /> 相同，则为 true；否则为 false。</returns>
    /// <param name="obj">要比较的对象。</param>
    [__DynamicallyInvokable]
    public override bool Equals(object obj)
    {
      if (!(obj is EventRegistrationToken))
        return false;
      return (long) ((EventRegistrationToken) obj).Value == (long) this.Value;
    }

    /// <summary>返回此实例的哈希代码。</summary>
    /// <returns>此实例的哈希代码。</returns>
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      return this.m_value.GetHashCode();
    }
  }
}
