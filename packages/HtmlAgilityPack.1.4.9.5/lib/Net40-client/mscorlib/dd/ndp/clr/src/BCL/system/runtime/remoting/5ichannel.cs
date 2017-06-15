// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Channels.BaseChannelObjectWithProperties
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Channels
{
  /// <summary>提供信道对象的基实现，该对象要向其属性公开字典接口。</summary>
  [SecurityCritical]
  [ComVisible(true)]
  [SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
  public abstract class BaseChannelObjectWithProperties : IDictionary, ICollection, IEnumerable
  {
    /// <summary>获取与信道对象关联的信道属性的 <see cref="T:System.Collections.IDictionary" />。</summary>
    /// <returns>与信道对象关联的信道属性的 <see cref="T:System.Collections.IDictionary" />。</returns>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    public virtual IDictionary Properties
    {
      [SecurityCritical] get
      {
        return (IDictionary) this;
      }
    }

    /// <summary>在派生类中重写时，获取或设置与指定键关联的属性。</summary>
    /// <returns>与指定键关联的属性。</returns>
    /// <param name="key">要获取或设置的属性的键。</param>
    /// <exception cref="T:System.NotImplementedException">属性被访问。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public virtual object this[object key]
    {
      [SecuritySafeCritical] get
      {
        return (object) null;
      }
      [SecuritySafeCritical] set
      {
        throw new NotImplementedException();
      }
    }

    /// <summary>当在派生类中重写时，获取与信道对象属性关联的键的 <see cref="T:System.Collections.ICollection" />。</summary>
    /// <returns>与信道对象属性关联的键的 <see cref="T:System.Collections.ICollection" />。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public virtual ICollection Keys
    {
      [SecuritySafeCritical] get
      {
        return (ICollection) null;
      }
    }

    /// <summary>获取与信道对象关联的属性值的 <see cref="T:System.Collections.ICollection" />。</summary>
    /// <returns>与信道对象关联的属性值的 <see cref="T:System.Collections.ICollection" />。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public virtual ICollection Values
    {
      [SecuritySafeCritical] get
      {
        ICollection keys = this.Keys;
        if (keys == null)
          return (ICollection) null;
        ArrayList arrayList = new ArrayList();
        foreach (object index in (IEnumerable) keys)
          arrayList.Add(this[index]);
        return (ICollection) arrayList;
      }
    }

    /// <summary>获取一个值，该值指示信道对象中的属性集合是否是只读的。</summary>
    /// <returns>如果信道对象中的属性集合是只读的，则为 true；否则为 false。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public virtual bool IsReadOnly
    {
      [SecuritySafeCritical] get
      {
        return false;
      }
    }

    /// <summary>获取一个值，该值指示可以输入到信道对象中的属性的数目是否是固定的。</summary>
    /// <returns>如果可在信道对象中输入的属性的数目是固定的，则为 true；否则为 false。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public virtual bool IsFixedSize
    {
      [SecuritySafeCritical] get
      {
        return true;
      }
    }

    /// <summary>获取与该信道对象关联的属性的数目。</summary>
    /// <returns>与该信道对象关联的属性的数目。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public virtual int Count
    {
      [SecuritySafeCritical] get
      {
        ICollection keys = this.Keys;
        if (keys == null)
          return 0;
        return keys.Count;
      }
    }

    /// <summary>获取一个对象，该对象用于同步对 <see cref="T:System.Runtime.Remoting.Channels.BaseChannelObjectWithProperties" /> 的访问。</summary>
    /// <returns>一个对象，该对象用于同步对 <see cref="T:System.Runtime.Remoting.Channels.BaseChannelObjectWithProperties" /> 的访问。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public virtual object SyncRoot
    {
      [SecuritySafeCritical] get
      {
        return (object) this;
      }
    }

    /// <summary>获取一个值，该值指示信道对象属性的字典是否同步。</summary>
    /// <returns>如果信道对象属性的字典是同步的，则为 true；否则为 false。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public virtual bool IsSynchronized
    {
      [SecuritySafeCritical] get
      {
        return false;
      }
    }

    /// <summary>返回一个值，该值指示信道对象是否包含与指定键关联的属性。</summary>
    /// <returns>如果信道对象包含与指定键关联的属性，则为 true；否则为 false。</returns>
    /// <param name="key">要查找的属性的键。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public virtual bool Contains(object key)
    {
      if (key == null)
        return false;
      ICollection keys = this.Keys;
      if (keys == null)
        return false;
      string strA = key as string;
      foreach (object obj in (IEnumerable) keys)
      {
        if (strA != null)
        {
          string strB = obj as string;
          if (strB != null)
          {
            if (string.Compare(strA, strB, StringComparison.OrdinalIgnoreCase) == 0)
              return true;
            continue;
          }
        }
        if (key.Equals(obj))
          return true;
      }
      return false;
    }

    /// <summary>引发 <see cref="T:System.NotSupportedException" />。</summary>
    /// <param name="key">与 <paramref name="value" /> 参数中的对象关联的键。</param>
    /// <param name="value">要添加的值。</param>
    /// <exception cref="T:System.NotSupportedException">调用该方法。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public virtual void Add(object key, object value)
    {
      throw new NotSupportedException();
    }

    /// <summary>引发 <see cref="T:System.NotSupportedException" />。</summary>
    /// <exception cref="T:System.NotSupportedException">调用该方法。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public virtual void Clear()
    {
      throw new NotSupportedException();
    }

    /// <summary>引发 <see cref="T:System.NotSupportedException" />。</summary>
    /// <param name="key">要移除的对象的键。</param>
    /// <exception cref="T:System.NotSupportedException">调用该方法。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public virtual void Remove(object key)
    {
      throw new NotSupportedException();
    }

    /// <summary>返回 <see cref="T:System.Collections.IDictionaryEnumerator" />，它枚举与该信道对象关联的所有属性。</summary>
    /// <returns>枚举与该信道对象关联的所有属性的 <see cref="T:System.Collections.IDictionaryEnumerator" />。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public virtual IDictionaryEnumerator GetEnumerator()
    {
      return (IDictionaryEnumerator) new DictionaryEnumeratorByKeys((IDictionary) this);
    }

    /// <summary>引发 <see cref="T:System.NotSupportedException" />。</summary>
    /// <param name="array">要将属性复制到的数组。</param>
    /// <param name="index">开始复制的索引位置。</param>
    /// <exception cref="T:System.NotSupportedException">调用该方法。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public virtual void CopyTo(Array array, int index)
    {
      throw new NotSupportedException();
    }

    [SecuritySafeCritical]
    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) new DictionaryEnumeratorByKeys((IDictionary) this);
    }
  }
}
