// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Contracts.ContractOptionAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Diagnostics.Contracts
{
  /// <summary>可以在程序集、类型或方法粒度方面设置协定和工具选项。</summary>
  [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = false)]
  [Conditional("CONTRACTS_FULL")]
  [__DynamicallyInvokable]
  public sealed class ContractOptionAttribute : Attribute
  {
    private string _category;
    private string _setting;
    private bool _enabled;
    private string _value;

    /// <summary>获取选项的类别。</summary>
    /// <returns>选项的类别。</returns>
    [__DynamicallyInvokable]
    public string Category
    {
      [__DynamicallyInvokable] get
      {
        return this._category;
      }
    }

    /// <summary>获得选项的设置。</summary>
    /// <returns>此选项的设置。</returns>
    [__DynamicallyInvokable]
    public string Setting
    {
      [__DynamicallyInvokable] get
      {
        return this._setting;
      }
    }

    /// <summary>确定选项是否启用。</summary>
    /// <returns>如果启用该选项，则为 true；否则，为 false。</returns>
    [__DynamicallyInvokable]
    public bool Enabled
    {
      [__DynamicallyInvokable] get
      {
        return this._enabled;
      }
    }

    /// <summary>获取选项的值。</summary>
    /// <returns>选项的值。</returns>
    [__DynamicallyInvokable]
    public string Value
    {
      [__DynamicallyInvokable] get
      {
        return this._value;
      }
    }

    /// <summary>使用提供的类别，设置和可用
    /// 或禁用值初始化 <see cref="T:System.Diagnostics.Contracts.ContractOptionAttribute" /> 类的新实例。</summary>
    /// <param name="category">要设置的选项的类别。</param>
    /// <param name="setting">选项设置。</param>
    /// <param name="enabled">启用选择，则为 true；禁用选择，则为 false。</param>
    [__DynamicallyInvokable]
    public ContractOptionAttribute(string category, string setting, bool enabled)
    {
      this._category = category;
      this._setting = setting;
      this._enabled = enabled;
    }

    /// <summary>使用提供的类别，设置和可用
    /// 或禁用值初始化 <see cref="T:System.Diagnostics.Contracts.ContractOptionAttribute" /> 类的新实例。</summary>
    /// <param name="category">要设置的选项的类别。</param>
    /// <param name="setting">选项设置。</param>
    /// <param name="value">此设置的值。</param>
    [__DynamicallyInvokable]
    public ContractOptionAttribute(string category, string setting, string value)
    {
      this._category = category;
      this._setting = setting;
      this._value = value;
    }
  }
}
