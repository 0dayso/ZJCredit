// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.DebuggerTypeProxyAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Diagnostics
{
  /// <summary>指定类型的显示代理。</summary>
  /// <filterpriority>1</filterpriority>
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class DebuggerTypeProxyAttribute : Attribute
  {
    private string typeName;
    private string targetName;
    private Type target;

    /// <summary>获取代理类型的类型名称。</summary>
    /// <returns>代理类型的类型名称。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public string ProxyTypeName
    {
      [__DynamicallyInvokable] get
      {
        return this.typeName;
      }
    }

    /// <summary>获取或设置属性的目标类型。</summary>
    /// <returns>特性的目标类型。</returns>
    /// <exception cref="T:System.ArgumentNullException">将 <see cref="P:System.Diagnostics.DebuggerTypeProxyAttribute.Target" /> 设置为 null。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public Type Target
    {
      [__DynamicallyInvokable] get
      {
        return this.target;
      }
      [__DynamicallyInvokable] set
      {
        if (value == (Type) null)
          throw new ArgumentNullException("value");
        this.targetName = value.AssemblyQualifiedName;
        this.target = value;
      }
    }

    /// <summary>获取或设置目标类型的名称。</summary>
    /// <returns>目标类型的名称。</returns>
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

    /// <summary>将使用此类型代理的 <see cref="T:System.Diagnostics.DebuggerTypeProxyAttribute" /> 类的新实例初始化。</summary>
    /// <param name="type">代理类型。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="type" /> 为 null。</exception>
    [__DynamicallyInvokable]
    public DebuggerTypeProxyAttribute(Type type)
    {
      if (type == (Type) null)
        throw new ArgumentNullException("type");
      this.typeName = type.AssemblyQualifiedName;
    }

    /// <summary>使用代理类型名称初始化 <see cref="T:System.Diagnostics.DebuggerTypeProxyAttribute" /> 类的新实例。</summary>
    /// <param name="typeName">代理类型的类型名称。</param>
    [__DynamicallyInvokable]
    public DebuggerTypeProxyAttribute(string typeName)
    {
      this.typeName = typeName;
    }
  }
}
