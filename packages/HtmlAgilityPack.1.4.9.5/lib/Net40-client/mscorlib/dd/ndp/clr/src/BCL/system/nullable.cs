// Decompiled with JetBrains decompiler
// Type: System.Nullable`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.Versioning;

namespace System
{
  /// <summary>表示可分配有 null 的值类型。</summary>
  /// <typeparam name="T">
  /// <see cref="T:System.Nullable`1" /> 泛型类型的基础值类型。</typeparam>
  /// <filterpriority>1</filterpriority>
  [NonVersionable]
  [__DynamicallyInvokable]
  [Serializable]
  public struct Nullable<T> where T : struct
  {
    private bool hasValue;
    internal T value;

    /// <summary>获取一个值，该值指示 <see cref="T:System.Nullable`1" /> 对象是否具有基础类型的有效值。</summary>
    /// <returns>如果当前的 true 对象具有值，则为 <see cref="T:System.Nullable`1" />；如果当前的 <see cref="T:System.Nullable`1" /> 对象没有值，则为 false。</returns>
    [__DynamicallyInvokable]
    public bool HasValue
    {
      [NonVersionable, __DynamicallyInvokable] get
      {
        return this.hasValue;
      }
    }

    /// <summary>获取当前 <see cref="T:System.Nullable`1" /> 对象的值，如果它已被分配了有效的基础值。</summary>
    /// <returns>如果 <see cref="P:System.Nullable`1.HasValue" /> 属性为 true，则为当前 <see cref="T:System.Nullable`1" /> 对象的值。如果 <see cref="P:System.Nullable`1.HasValue" /> 属性为 false，则将引发异常。</returns>
    /// <exception cref="T:System.InvalidOperationException">
    /// <see cref="P:System.Nullable`1.HasValue" /> 属性为 false。</exception>
    [__DynamicallyInvokable]
    public T Value
    {
      [__DynamicallyInvokable] get
      {
        if (!this.hasValue)
          ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_NoValue);
        return this.value;
      }
    }

    /// <summary>将 <see cref="T:System.Nullable`1" /> 结构的新实例初始化为指定值。</summary>
    /// <param name="value">一个值类型。</param>
    [NonVersionable]
    [__DynamicallyInvokable]
    public Nullable(T value)
    {
      this.value = value;
      this.hasValue = true;
    }

    /// <summary>创建一个新的 <see cref="T:System.Nullable`1" /> 对象，并将其初始化为指定的值。</summary>
    /// <returns>一个 <see cref="T:System.Nullable`1" /> 对象，其 <see cref="P:System.Nullable`1.Value" /> 属性使用 <paramref name="value" /> 参数进行初始化。</returns>
    /// <param name="value">一个值类型。</param>
    [NonVersionable]
    [__DynamicallyInvokable]
    public static implicit operator T?(T value)
    {
      return new T?(value);
    }

    /// <summary>定义 <see cref="T:System.Nullable`1" /> 实例到其基础值的显式转换。</summary>
    /// <returns>
    /// <paramref name="value" /> 参数的 <see cref="P:System.Nullable`1.Value" /> 属性的值。</returns>
    /// <param name="value">可以为 Null 的值。</param>
    [NonVersionable]
    [__DynamicallyInvokable]
    public static explicit operator T(T? value)
    {
      return value.Value;
    }

    /// <summary>检索当前 <see cref="T:System.Nullable`1" /> 对象的值，或该对象的默认值。</summary>
    /// <returns>如果 <see cref="P:System.Nullable`1.HasValue" /> 属性为 true，则为 <see cref="P:System.Nullable`1.Value" /> 属性的值；否则为当前 <see cref="T:System.Nullable`1" /> 对象的默认值。默认值的类型为当前 <see cref="T:System.Nullable`1" /> 对象的类型参数，而默认值的值中只包含二进制零。</returns>
    [NonVersionable]
    [__DynamicallyInvokable]
    public T GetValueOrDefault()
    {
      return this.value;
    }

    /// <summary>检索当前 <see cref="T:System.Nullable`1" /> 对象的值或指定的默认值。</summary>
    /// <returns>如果 <see cref="P:System.Nullable`1.HasValue" /> 属性为 true，则为 <see cref="P:System.Nullable`1.Value" /> 属性的值；否则为 <paramref name="defaultValue" /> 参数。</returns>
    /// <param name="defaultValue">如果 <see cref="P:System.Nullable`1.HasValue" /> 属性为 false，则为一个返回值。</param>
    [NonVersionable]
    [__DynamicallyInvokable]
    public T GetValueOrDefault(T defaultValue)
    {
      if (!this.hasValue)
        return defaultValue;
      return this.value;
    }

    /// <summary>指示当前 <see cref="T:System.Nullable`1" /> 对象是否与指定的对象相等。</summary>
    /// <returns>如果 <paramref name="other" /> 参数等于当前的 <see cref="T:System.Nullable`1" /> 对象，则为 true；否则为 false。此表描述如何定义所比较值的相等性： 返回值描述true<see cref="P:System.Nullable`1.HasValue" /> 属性为 false，并且 <paramref name="other" /> 参数为 null。即，根据定义，两个 null 值相等。- 或 -<see cref="P:System.Nullable`1.HasValue" /> 属性为 true，并且 <see cref="P:System.Nullable`1.Value" /> 属性返回的值等于 <paramref name="other" /> 参数。false当前 <see cref="P:System.Nullable`1.HasValue" /> 结构的 <see cref="T:System.Nullable`1" /> 属性为 true，并且 <paramref name="other" /> 参数为 null。- 或 -当前 <see cref="P:System.Nullable`1.HasValue" /> 结构的 <see cref="T:System.Nullable`1" /> 属性为 false，并且 <paramref name="other" /> 参数不为 null。- 或 -当前 <see cref="P:System.Nullable`1.HasValue" /> 结构的 <see cref="T:System.Nullable`1" /> 属性为 true，并且 <see cref="P:System.Nullable`1.Value" /> 属性返回的值不等于 <paramref name="other" /> 参数。</returns>
    /// <param name="other">一个对象。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public override bool Equals(object other)
    {
      if (!this.hasValue)
        return other == null;
      if (other == null)
        return false;
      return this.value.Equals(other);
    }

    /// <summary>检索由 <see cref="P:System.Nullable`1.Value" /> 属性返回的对象的哈希代码。</summary>
    /// <returns>如果 <see cref="P:System.Nullable`1.HasValue" /> 属性为 true，则为 <see cref="P:System.Nullable`1.Value" /> 属性返回的对象的哈希代码；如果 <see cref="P:System.Nullable`1.HasValue" /> 属性为 false，则为零。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      if (!this.hasValue)
        return 0;
      return this.value.GetHashCode();
    }

    /// <summary>返回当前 <see cref="T:System.Nullable`1" /> 对象的值的文本表示形式。</summary>
    /// <returns>如果 <see cref="P:System.Nullable`1.HasValue" /> 属性为 true，则是当前 <see cref="T:System.Nullable`1" /> 对象的值的文本表示形式；如果 <see cref="P:System.Nullable`1.HasValue" /> 属性为 false，则是一个空字符串 ("")。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public override string ToString()
    {
      if (!this.hasValue)
        return "";
      return this.value.ToString();
    }
  }
}
