// Decompiled with JetBrains decompiler
// Type: System.Security.Principal.IdentityReference
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Principal
{
  /// <summary>表示一个标识，为 <see cref="T:System.Security.Principal.NTAccount" /> 和 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 类的基类。此类不提供公共构造函数，因为不能被继承。</summary>
  [ComVisible(false)]
  public abstract class IdentityReference
  {
    /// <summary>获取 <see cref="T:System.Security.Principal.IdentityReference" /> 对象表示的标识的字符串值。</summary>
    /// <returns>
    /// <see cref="T:System.Security.Principal.IdentityReference" /> 对象表示的标识的字符串值。</returns>
    public abstract string Value { get; }

    internal IdentityReference()
    {
    }

    /// <summary>比较两个 <see cref="T:System.Security.Principal.IdentityReference" /> 对象以确定它们是否相等。如果这两个对象具有与 <see cref="P:System.Security.Principal.IdentityReference.Value" /> 属性返回的规范名称表示形式相同的规范名称表示形式，或是都为 null，则将它们视为相等。</summary>
    /// <returns>如果 <paramref name="left" /> 和 <paramref name="right" /> 相等，则为 true；否则为 false。</returns>
    /// <param name="left">用于相等比较的左 <see cref="T:System.Security.Principal.IdentityReference" /> 操作数。此参数可以为 null。</param>
    /// <param name="right">用于相等比较的右 <see cref="T:System.Security.Principal.IdentityReference" /> 操作数。此参数可以为 null。</param>
    public static bool operator ==(IdentityReference left, IdentityReference right)
    {
      object obj1 = (object) left;
      object obj2 = (object) right;
      if (obj1 == null && obj2 == null)
        return true;
      if (obj1 == null || obj2 == null)
        return false;
      return left.Equals((object) right);
    }

    /// <summary>比较两个 <see cref="T:System.Security.Principal.IdentityReference" /> 对象以确定它们是否不相等。如果它们的规范名称表示形式与 <see cref="P:System.Security.Principal.IdentityReference.Value" /> 属性返回的表示形式不同，或其中一个对象为 null 而另一个对象不是，它们将被视为不相等。</summary>
    /// <returns>如果 <paramref name="left" /> 与 <paramref name="right" /> 不相等，则为 true；否则为 false。</returns>
    /// <param name="left">用于不相等比较的左 <see cref="T:System.Security.Principal.IdentityReference" /> 操作数。此参数可以为 null。</param>
    /// <param name="right">用于不相等比较的右 <see cref="T:System.Security.Principal.IdentityReference" /> 操作数。此参数可以为 null。</param>
    public static bool operator !=(IdentityReference left, IdentityReference right)
    {
      return !(left == right);
    }

    /// <summary>返回一个值，该值指示指定类型是否为 <see cref="T:System.Security.Principal.IdentityReference" /> 类的有效转换类型。</summary>
    /// <returns>如果 <paramref name="targetType" /> 为 <see cref="T:System.Security.Principal.IdentityReference" /> 类的有效转换类型，则为 true；否则为 false。</returns>
    /// <param name="targetType">查询其能否作为 <see cref="T:System.Security.Principal.IdentityReference" /> 的有效转换类型的类型。以下目标类型是有效的：<see cref="T:System.Security.Principal.NTAccount" /><see cref="T:System.Security.Principal.SecurityIdentifier" /></param>
    public abstract bool IsValidTargetType(Type targetType);

    /// <summary>将 <see cref="T:System.Security.Principal.IdentityReference" /> 对象表示的帐户名转换为另一 <see cref="T:System.Security.Principal.IdentityReference" /> 派生类型。</summary>
    /// <returns>转换后的标识。</returns>
    /// <param name="targetType">从 <see cref="T:System.Security.Principal.IdentityReference" /> 进行的转换的目标类型。</param>
    public abstract IdentityReference Translate(Type targetType);

    /// <summary>返回一个值，该值指示指定对象是否等于 <see cref="T:System.Security.Principal.IdentityReference" /> 类的此实例。</summary>
    /// <returns>如果 <paramref name="o" /> 是与此 <see cref="T:System.Security.Principal.IdentityReference" /> 实例有相同基础类型和值的对象，则为 true；否则为 false。</returns>
    /// <param name="o">要与此 <see cref="T:System.Security.Principal.IdentityReference" /> 实例比较的对象，或一个空引用。</param>
    public abstract override bool Equals(object o);

    /// <summary>作为 <see cref="T:System.Security.Principal.IdentityReference" /> 的哈希函数。<see cref="M:System.Security.Principal.IdentityReference.GetHashCode" /> 适用于哈希算法和哈希表之类的数据结构。</summary>
    /// <returns>此 <see cref="T:System.Security.Principal.IdentityReference" /> 对象的哈希代码。</returns>
    public abstract override int GetHashCode();

    /// <summary>返回 <see cref="T:System.Security.Principal.IdentityReference" /> 对象表示的标识的字符串表示形式。</summary>
    /// <returns>字符串格式的标识。</returns>
    public abstract override string ToString();
  }
}
