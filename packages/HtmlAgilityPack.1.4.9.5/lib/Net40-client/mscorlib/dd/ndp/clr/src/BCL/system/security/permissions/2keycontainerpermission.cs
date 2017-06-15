// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.KeyContainerPermissionAccessEntryEnumerator
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
  /// <summary>表示 <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntryCollection" /> 中的 <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> 对象的枚举数。</summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class KeyContainerPermissionAccessEntryEnumerator : IEnumerator
  {
    private KeyContainerPermissionAccessEntryCollection m_entries;
    private int m_current;

    /// <summary>获取集合中的当前项。</summary>
    /// <returns>集合中的当前 <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> 对象。</returns>
    /// <exception cref="T:System.InvalidOperationException">在第一次调用 <see cref="M:System.Security.Permissions.KeyContainerPermissionAccessEntryEnumerator.MoveNext" /> 方法之前会访问 <see cref="P:System.Security.Permissions.KeyContainerPermissionAccessEntryEnumerator.Current" /> 属性。光标位于集合中的第一个对象之前。- 或 -在调用 <see cref="M:System.Security.Permissions.KeyContainerPermissionAccessEntryEnumerator.MoveNext" /> 方法返回 false（这表明光标位于集合的最后一个对象之后）后会访问 <see cref="P:System.Security.Permissions.KeyContainerPermissionAccessEntryEnumerator.Current" /> 属性。</exception>
    public KeyContainerPermissionAccessEntry Current
    {
      get
      {
        return this.m_entries[this.m_current];
      }
    }

    object IEnumerator.Current
    {
      get
      {
        return (object) this.m_entries[this.m_current];
      }
    }

    private KeyContainerPermissionAccessEntryEnumerator()
    {
    }

    internal KeyContainerPermissionAccessEntryEnumerator(KeyContainerPermissionAccessEntryCollection entries)
    {
      this.m_entries = entries;
      this.m_current = -1;
    }

    /// <summary>移至集合中的下一元素。</summary>
    /// <returns>如果枚举数成功地推进到下一个元素，则为 true；如果枚举数越过集合的结尾，则为 false。</returns>
    public bool MoveNext()
    {
      if (this.m_current == this.m_entries.Count - 1)
        return false;
      this.m_current = this.m_current + 1;
      return true;
    }

    /// <summary>将枚举数重置到集合的开头。</summary>
    public void Reset()
    {
      this.m_current = -1;
    }
  }
}
