// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.AceEnumerator
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;

namespace System.Security.AccessControl
{
  /// <summary>提供遍历访问控制列表 (ACL) 中的访问控制项 (ACE) 的能力。</summary>
  public sealed class AceEnumerator : IEnumerator
  {
    private int _current;
    private readonly GenericAcl _acl;

    object IEnumerator.Current
    {
      get
      {
        if (this._current == -1 || this._current >= this._acl.Count)
          throw new InvalidOperationException(Environment.GetResourceString("Arg_InvalidOperationException"));
        return (object) this._acl[this._current];
      }
    }

    /// <summary>获取 <see cref="T:System.Security.AccessControl.GenericAce" /> 集合中的当前元素。此属性获取对象的类型易于转换的版本。</summary>
    /// <returns>
    /// <see cref="T:System.Security.AccessControl.GenericAce" /> 集合中的当前元素。</returns>
    public GenericAce Current
    {
      get
      {
        return ((IEnumerator) this).Current as GenericAce;
      }
    }

    internal AceEnumerator(GenericAcl collection)
    {
      if (collection == null)
        throw new ArgumentNullException("collection");
      this._acl = collection;
      this.Reset();
    }

    /// <summary>将枚举器前进到 <see cref="T:System.Security.AccessControl.GenericAce" /> 集合的下一个元素。</summary>
    /// <returns>如果枚举数成功地推进到下一个元素，则为 true；如果枚举数越过集合的结尾，则为 false。</returns>
    /// <exception cref="T:System.InvalidOperationException">在创建了枚举数后集合被修改了。</exception>
    public bool MoveNext()
    {
      this._current = this._current + 1;
      return this._current < this._acl.Count;
    }

    /// <summary>将枚举数设置为其初始位置，该位置位于 <see cref="T:System.Security.AccessControl.GenericAce" /> 集合中第一个元素之前。</summary>
    /// <exception cref="T:System.InvalidOperationException">在创建了枚举数后集合被修改了。</exception>
    public void Reset()
    {
      this._current = -1;
    }
  }
}
