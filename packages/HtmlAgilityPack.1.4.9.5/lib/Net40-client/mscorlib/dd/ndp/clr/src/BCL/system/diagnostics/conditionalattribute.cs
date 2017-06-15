// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.ConditionalAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Diagnostics
{
  /// <summary>指示编译器应忽略方法调用或属性，除非已定义指定的条件编译符号。</summary>
  /// <filterpriority>1</filterpriority>
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class ConditionalAttribute : Attribute
  {
    private string m_conditionString;

    /// <summary>获取与 <see cref="T:System.Diagnostics.ConditionalAttribute" /> 属性相关的条件编译符号。</summary>
    /// <returns>一个字符串，它指定与 <see cref="T:System.Diagnostics.ConditionalAttribute" /> 属性关联的区分大小写的条件编译符号。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public string ConditionString
    {
      [__DynamicallyInvokable] get
      {
        return this.m_conditionString;
      }
    }

    /// <summary>初始化 <see cref="T:System.Diagnostics.ConditionalAttribute" /> 类的新实例。</summary>
    /// <param name="conditionString">一个字符串，它指定与此属性关联的区分大小写的条件编译符号。</param>
    [__DynamicallyInvokable]
    public ConditionalAttribute(string conditionString)
    {
      this.m_conditionString = conditionString;
    }
  }
}
