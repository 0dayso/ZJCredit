// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.CodeAnalysis.SuppressMessageAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Diagnostics.CodeAnalysis
{
  /// <summary>取消报告特定的静态分析工具规则冲突，允许一个代码项目上应用多个取消报告设置。</summary>
  [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = false)]
  [Conditional("CODE_ANALYSIS")]
  [__DynamicallyInvokable]
  public sealed class SuppressMessageAttribute : Attribute
  {
    private string category;
    private string justification;
    private string checkId;
    private string scope;
    private string target;
    private string messageId;

    /// <summary>获取标识特性分类的类别。</summary>
    /// <returns>标识特性的类别。</returns>
    [__DynamicallyInvokable]
    public string Category
    {
      [__DynamicallyInvokable] get
      {
        return this.category;
      }
    }

    /// <summary>获取要取消的静态分析工具规则的标识符。</summary>
    /// <returns>要取消的静态分析工具规则的标识符。</returns>
    [__DynamicallyInvokable]
    public string CheckId
    {
      [__DynamicallyInvokable] get
      {
        return this.checkId;
      }
    }

    /// <summary>获取或设置与属性相关的代码的范围。</summary>
    /// <returns>与属性相关的代码的范围。</returns>
    [__DynamicallyInvokable]
    public string Scope
    {
      [__DynamicallyInvokable] get
      {
        return this.scope;
      }
      [__DynamicallyInvokable] set
      {
        this.scope = value;
      }
    }

    /// <summary>获取或设置表示属性目标的完全限定路径。</summary>
    /// <returns>表示属性目标的完全限定路径。</returns>
    [__DynamicallyInvokable]
    public string Target
    {
      [__DynamicallyInvokable] get
      {
        return this.target;
      }
      [__DynamicallyInvokable] set
      {
        this.target = value;
      }
    }

    /// <summary>获取或设置扩展排除条件的可选参数。</summary>
    /// <returns>一个包含扩展的排除条件的字符串。</returns>
    [__DynamicallyInvokable]
    public string MessageId
    {
      [__DynamicallyInvokable] get
      {
        return this.messageId;
      }
      [__DynamicallyInvokable] set
      {
        this.messageId = value;
      }
    }

    /// <summary>获取或设置用于取消代码分析消息的规则。</summary>
    /// <returns>用于取消消息的规则。</returns>
    [__DynamicallyInvokable]
    public string Justification
    {
      [__DynamicallyInvokable] get
      {
        return this.justification;
      }
      [__DynamicallyInvokable] set
      {
        this.justification = value;
      }
    }

    /// <summary>初始化 <see cref="T:System.Diagnostics.CodeAnalysis.SuppressMessageAttribute" /> 类的新实例，同时指定静态分析工具的类别和分析规则的标识符。</summary>
    /// <param name="category">该属性的类别。</param>
    /// <param name="checkId">应用该属性的分析工具规则的标识符。</param>
    [__DynamicallyInvokable]
    public SuppressMessageAttribute(string category, string checkId)
    {
      this.category = category;
      this.checkId = checkId;
    }
  }
}
