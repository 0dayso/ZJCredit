// Decompiled with JetBrains decompiler
// Type: System.Reflection.ICustomAttributeProvider
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>为支持自定义属性的反映对象提供自定义属性。</summary>
  [ComVisible(true)]
  public interface ICustomAttributeProvider
  {
    /// <summary>返回此成员上定义的自定义属性的数组（由类型标识），如果该类型没有自定义属性，则返回空数组。</summary>
    /// <returns>表示自定义属性的对象的数组或空数组。</returns>
    /// <param name="attributeType">自定义属性的类型。</param>
    /// <param name="inherit">当为 true 时，查找继承的自定义属性的层次结构链。</param>
    /// <exception cref="T:System.TypeLoadException">无法加载自定义特性类型。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="attributeType" /> 为 null。</exception>
    object[] GetCustomAttributes(Type attributeType, bool inherit);

    /// <summary>返回在此成员上定义的所有自定义属性（命名属性除外）的数组，或如果没有自定义属性，返回一个空数组。</summary>
    /// <returns>表示自定义属性的对象的数组或空数组。</returns>
    /// <param name="inherit">当为 true 时，查找继承的自定义属性的层次结构链。</param>
    /// <exception cref="T:System.TypeLoadException">无法加载自定义特性类型。</exception>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">此成员上定义的 <paramref name="attributeType" /> 类型属性不止一个。</exception>
    object[] GetCustomAttributes(bool inherit);

    /// <summary>指示是否在此成员上定义一个或多个 <paramref name="attributeType" /> 的实例。</summary>
    /// <returns>如果在此成员上定义 <paramref name="attributeType" />，则为 true；否则为 false。</returns>
    /// <param name="attributeType">自定义属性的类型。</param>
    /// <param name="inherit">当为 true 时，查找继承的自定义属性的层次结构链。</param>
    bool IsDefined(Type attributeType, bool inherit);
  }
}
