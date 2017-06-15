// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.AuthorizationRuleCollection
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;

namespace System.Security.AccessControl
{
  /// <summary>表示 <see cref="T:System.Security.AccessControl.AuthorizationRule" /> 对象集合。</summary>
  public sealed class AuthorizationRuleCollection : ReadOnlyCollectionBase
  {
    /// <summary>获取集合中指定索引位置的 <see cref="T:System.Security.AccessControl.AuthorizationRule" /> 对象。</summary>
    /// <returns>位于指定索引处的 <see cref="T:System.Security.AccessControl.AuthorizationRule" /> 对象。</returns>
    /// <param name="index">要获取的 <see cref="T:System.Security.AccessControl.AuthorizationRule" /> 对象的从零开始的索引。</param>
    public AuthorizationRule this[int index]
    {
      get
      {
        return this.InnerList[index] as AuthorizationRule;
      }
    }

    /// <summary>将一个 <see cref="T:System.Web.Configuration.AuthorizationRule" /> 对象添加到集合中。</summary>
    /// <param name="rule">要添加到集合的 <see cref="T:System.Web.Configuration.AuthorizationRule" /> 对象。</param>
    public void AddRule(AuthorizationRule rule)
    {
      this.InnerList.Add((object) rule);
    }

    /// <summary>将该集合的内容复制到数组。</summary>
    /// <param name="rules">将集合内容复制到的目标数组。</param>
    /// <param name="index">从零开始的索引，从此处开始复制。</param>
    public void CopyTo(AuthorizationRule[] rules, int index)
    {
      this.CopyTo((Array) rules, index);
    }
  }
}
