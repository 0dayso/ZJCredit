// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.DebuggerDisplayAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Diagnostics
{
  /// <summary>确定类或字段在调试器的变量窗口中的显示方式。</summary>
  /// <filterpriority>1</filterpriority>
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Delegate, AllowMultiple = true)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class DebuggerDisplayAttribute : Attribute
  {
    private string name;
    private string value;
    private string type;
    private string targetName;
    private System.Type target;

    /// <summary>获取要在调试器变量窗口的值列中显示的字符串。</summary>
    /// <returns>要在调试器变量窗口的值列中显示的字符串。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public string Value
    {
      [__DynamicallyInvokable] get
      {
        return this.value;
      }
    }

    /// <summary>获取或设置要在调试器的变量窗口中显示的名称。</summary>
    /// <returns>要在调试器的变量窗口中显示的名称。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public string Name
    {
      [__DynamicallyInvokable] get
      {
        return this.name;
      }
      [__DynamicallyInvokable] set
      {
        this.name = value;
      }
    }

    /// <summary>获取或设置要在调试器的变量窗口的类型列中显示的字符串。</summary>
    /// <returns>要在调试器的变量窗口的类型列中显示的字符串。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public string Type
    {
      [__DynamicallyInvokable] get
      {
        return this.type;
      }
      [__DynamicallyInvokable] set
      {
        this.type = value;
      }
    }

    /// <summary>获取或设置该属性的目标类型。</summary>
    /// <returns>该特性的目标类型。</returns>
    /// <exception cref="T:System.ArgumentNullException">将 <see cref="P:System.Diagnostics.DebuggerDisplayAttribute.Target" /> 设置为 null。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public System.Type Target
    {
      [__DynamicallyInvokable] get
      {
        return this.target;
      }
      [__DynamicallyInvokable] set
      {
        if (value == (System.Type) null)
          throw new ArgumentNullException("value");
        this.targetName = value.AssemblyQualifiedName;
        this.target = value;
      }
    }

    /// <summary>获取或设置该属性的目标类型的名称。</summary>
    /// <returns>该属性的目标类型的名称。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public string TargetTypeName
    {
      [__DynamicallyInvokable] get
      {
        return this.targetName;
      }
      [__DynamicallyInvokable] set
      {
        this.targetName = value;
      }
    }

    /// <summary>初始化 <see cref="T:System.Diagnostics.DebuggerDisplayAttribute" /> 类的新实例。</summary>
    /// <param name="value">要在值列中为该类型的实例显示的字符串；空字符串 ("") 将使值列隐藏。</param>
    [__DynamicallyInvokable]
    public DebuggerDisplayAttribute(string value)
    {
      this.value = value != null ? value : "";
      this.name = "";
      this.type = "";
    }
  }
}
