// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.Expando.IExpando
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;

namespace System.Runtime.InteropServices.Expando
{
  /// <summary>允许通过添加和移除成员来修改对象，由 <see cref="T:System.Reflection.MemberInfo" /> 对象表示。</summary>
  [Guid("AFBF15E6-C37C-11d2-B88E-00A0C9B471B8")]
  [ComVisible(true)]
  public interface IExpando : IReflect
  {
    /// <summary>将命名的字段添加到反射对象。</summary>
    /// <returns>表示所添加字段的 <see cref="T:System.Reflection.FieldInfo" /> 对象。</returns>
    /// <param name="name">字段名。</param>
    /// <exception cref="T:System.NotSupportedException">IExpando 对象不支持此方法。</exception>
    FieldInfo AddField(string name);

    /// <summary>将命名的属性添加到反射对象。</summary>
    /// <returns>表示所添加属性的 PropertyInfo 对象。</returns>
    /// <param name="name">属性的名称。</param>
    /// <exception cref="T:System.NotSupportedException">IExpando 对象不支持此方法。</exception>
    PropertyInfo AddProperty(string name);

    /// <summary>将命名的方法添加到反射对象。</summary>
    /// <returns>表示所添加方法的 MethodInfo 对象。</returns>
    /// <param name="name">方法的名称。</param>
    /// <param name="method">该方法的委托。</param>
    /// <exception cref="T:System.NotSupportedException">IExpando 对象不支持此方法。</exception>
    MethodInfo AddMethod(string name, Delegate method);

    /// <summary>移除指定的成员。</summary>
    /// <param name="m">要移除的成员。</param>
    /// <exception cref="T:System.NotSupportedException">IExpando 对象不支持此方法。</exception>
    void RemoveMember(MemberInfo m);
  }
}
