// Decompiled with JetBrains decompiler
// Type: System.Reflection.DefaultMemberAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>定义某类型的成员，该成员是 <see cref="M:System.Type.InvokeMember(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Object,System.Object[],System.Reflection.ParameterModifier[],System.Globalization.CultureInfo,System.String[])" /> 使用的默认成员。</summary>
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class DefaultMemberAttribute : Attribute
  {
    private string m_memberName;

    /// <summary>从属性中获取名称。</summary>
    /// <returns>表示成员名称的字符串。</returns>
    [__DynamicallyInvokable]
    public string MemberName
    {
      [__DynamicallyInvokable] get
      {
        return this.m_memberName;
      }
    }

    /// <summary>初始化 <see cref="T:System.Reflection.DefaultMemberAttribute" /> 类的新实例。</summary>
    /// <param name="memberName">包含要调用的成员名称的 String。这可能是一个构造函数、方法、属性或字段。在调用成员时必须指定合适的调用属性。通过传递一个空 String 作为成员名称，可以指定类的默认成员。类型的默认成员由 DefaultMemberAttribute 自定义属性标记，或按通常的方法在 COM 中标记。</param>
    [__DynamicallyInvokable]
    public DefaultMemberAttribute(string memberName)
    {
      this.m_memberName = memberName;
    }
  }
}
