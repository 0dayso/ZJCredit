// Decompiled with JetBrains decompiler
// Type: System.Security.Principal.IdentityReferenceCollection
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Security.Principal
{
  /// <summary>表示 <see cref="T:System.Security.Principal.IdentityReference" /> 对象的集合，并提供一种方法将 <see cref="T:System.Security.Principal.IdentityReference" /> 派生的对象集转换为 <see cref="T:System.Security.Principal.IdentityReference" /> 派生的类型。</summary>
  [ComVisible(false)]
  public class IdentityReferenceCollection : ICollection<IdentityReference>, IEnumerable<IdentityReference>, IEnumerable
  {
    private List<IdentityReference> _Identities;

    /// <summary>获取 <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> 集合中项的数目。</summary>
    /// <returns>
    /// <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> 集合中 <see cref="T:System.Security.Principal.IdentityReference" /> 对象的数目。</returns>
    public int Count
    {
      get
      {
        return this._Identities.Count;
      }
    }

    /// <summary>获取一个值，该值指示 <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> 集合是否是只读的。</summary>
    /// <returns>如果 <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> 集合为只读，则为 true。</returns>
    public bool IsReadOnly
    {
      get
      {
        return false;
      }
    }

    /// <summary>获取或设置 <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> 集合中指定索引处的节点。</summary>
    /// <returns>位于集合中指定索引处的 <see cref="T:System.Security.Principal.IdentityReference" />。如果 <paramref name="index" /> 大于或等于集合中的节点数，则返回值为 null。</returns>
    /// <param name="index">
    /// <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> 集合中的从零开始的索引。</param>
    public IdentityReference this[int index]
    {
      get
      {
        return this._Identities[index];
      }
      set
      {
        if (value == (IdentityReference) null)
          throw new ArgumentNullException("value");
        this._Identities[index] = value;
      }
    }

    internal List<IdentityReference> Identities
    {
      get
      {
        return this._Identities;
      }
    }

    /// <summary>用集合中的零项初始化 <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> 类的新实例。</summary>
    public IdentityReferenceCollection()
      : this(0)
    {
    }

    /// <summary>使用指定的初始大小初始化 <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> 类的新实例。</summary>
    /// <param name="capacity">集合中的初始项数。<paramref name="capacity" /> 的值仅是一个提示，它不一定是创建的最大项数。</param>
    public IdentityReferenceCollection(int capacity)
    {
      this._Identities = new List<IdentityReference>(capacity);
    }

    /// <summary>从指定的索引开始，将 <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> 集合复制到一个 <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> 数组中。</summary>
    /// <param name="array">要将 <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> 集合复制到其中的 <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> 数组对象。</param>
    /// <param name="offset">
    /// <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> 集合要复制到的 <paramref name="array" /> 中从零开始的索引。</param>
    public void CopyTo(IdentityReference[] array, int offset)
    {
      this._Identities.CopyTo(0, array, offset, this.Count);
    }

    /// <summary>将 <see cref="T:System.Security.Principal.IdentityReference" /> 对象添加到 <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> 集合中。</summary>
    /// <param name="identity">要添加到集合的 <see cref="T:System.Security.Principal.IdentityReference" /> 对象。</param>
    public void Add(IdentityReference identity)
    {
      if (identity == (IdentityReference) null)
        throw new ArgumentNullException("identity");
      this._Identities.Add(identity);
    }

    /// <summary>从集合中移除指定的 <see cref="T:System.Security.Principal.IdentityReference" /> 对象。</summary>
    /// <returns>如果从集合中移除了指定的对象，则为 true。</returns>
    /// <param name="identity">要移除的 <see cref="T:System.Security.Principal.IdentityReference" /> 对象。</param>
    public bool Remove(IdentityReference identity)
    {
      if (identity == (IdentityReference) null)
        throw new ArgumentNullException("identity");
      if (!this.Contains(identity))
        return false;
      this._Identities.Remove(identity);
      return true;
    }

    /// <summary>从 <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> 集合中清除所有 <see cref="T:System.Security.Principal.IdentityReference" /> 对象。</summary>
    public void Clear()
    {
      this._Identities.Clear();
    }

    /// <summary>指示 <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> 集合是否包含指定的 <see cref="T:System.Security.Principal.IdentityReference" /> 对象。</summary>
    /// <returns>如果集合包含指定的对象，则为 true。</returns>
    /// <param name="identity">要检查的 <see cref="T:System.Security.Principal.IdentityReference" /> 对象。</param>
    public bool Contains(IdentityReference identity)
    {
      if (identity == (IdentityReference) null)
        throw new ArgumentNullException("identity");
      return this._Identities.Contains(identity);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.GetEnumerator();
    }

    /// <summary>获取一个可用于循环访问 <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> 集合的枚举数。</summary>
    /// <returns>
    /// <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> 集合的枚举数。</returns>
    public IEnumerator<IdentityReference> GetEnumerator()
    {
      return (IEnumerator<IdentityReference>) new IdentityReferenceEnumerator(this);
    }

    /// <summary>将集合中的对象转换为指定类型。调用此方法与调用第二个参数设置为 false 的 <see cref="M:System.Security.Principal.IdentityReferenceCollection.Translate(System.Type,System.Boolean)" /> 一样，这意味着对于转换失败的项不会引发异常。</summary>
    /// <returns>一个 <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> 集合，表示原始集合的转换后的内容。</returns>
    /// <param name="targetType">要将集合中的项转换到的目标类型。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    /// </PermissionSet>
    public IdentityReferenceCollection Translate(Type targetType)
    {
      return this.Translate(targetType, false);
    }

    /// <summary>将集合中的对象转换为指定类型，并使用指定容错机制处理或忽略与不具有转换映射的类型相关联的错误。</summary>
    /// <returns>一个 <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> 集合，表示原始集合的转换后的内容。</returns>
    /// <param name="targetType">要将集合中的项转换到的目标类型。</param>
    /// <param name="forceSuccess">一个布尔值，确定如何处理转换错误。如果 <paramref name="forceSuccess" /> 为 true，则由于未能为转换找到映射而发生的转换错误会导致转换失败并引发异常。如果 <paramref name="forceSuccess" /> 为 false，则因未为转换找到映射而未能转换的类型会在不进行转换的情况下被复制到返回的集合中。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, ControlPrincipal = true)]
    public IdentityReferenceCollection Translate(Type targetType, bool forceSuccess)
    {
      if (targetType == (Type) null)
        throw new ArgumentNullException("targetType");
      if (!targetType.IsSubclassOf(typeof (IdentityReference)))
        throw new ArgumentException(Environment.GetResourceString("IdentityReference_MustBeIdentityReference"), "targetType");
      if (this.Identities.Count == 0)
        return new IdentityReferenceCollection();
      int capacity1 = 0;
      int capacity2 = 0;
      for (int index = 0; index < this.Identities.Count; ++index)
      {
        Type type = this.Identities[index].GetType();
        if (!(type == targetType))
        {
          if (type == typeof (SecurityIdentifier))
          {
            ++capacity1;
          }
          else
          {
            if (!(type == typeof (NTAccount)))
              throw new SystemException();
            ++capacity2;
          }
        }
      }
      bool flag = false;
      IdentityReferenceCollection sourceSids = (IdentityReferenceCollection) null;
      IdentityReferenceCollection sourceAccounts = (IdentityReferenceCollection) null;
      if (capacity1 == this.Count)
      {
        flag = true;
        sourceSids = this;
      }
      else if (capacity1 > 0)
        sourceSids = new IdentityReferenceCollection(capacity1);
      if (capacity2 == this.Count)
      {
        flag = true;
        sourceAccounts = this;
      }
      else if (capacity2 > 0)
        sourceAccounts = new IdentityReferenceCollection(capacity2);
      IdentityReferenceCollection referenceCollection1 = (IdentityReferenceCollection) null;
      if (!flag)
      {
        referenceCollection1 = new IdentityReferenceCollection(this.Identities.Count);
        for (int index = 0; index < this.Identities.Count; ++index)
        {
          IdentityReference identity = this[index];
          Type type = identity.GetType();
          if (!(type == targetType))
          {
            if (type == typeof (SecurityIdentifier))
            {
              sourceSids.Add(identity);
            }
            else
            {
              if (!(type == typeof (NTAccount)))
                throw new SystemException();
              sourceAccounts.Add(identity);
            }
          }
        }
      }
      bool someFailed = false;
      IdentityReferenceCollection referenceCollection2 = (IdentityReferenceCollection) null;
      IdentityReferenceCollection referenceCollection3 = (IdentityReferenceCollection) null;
      if (capacity1 > 0)
      {
        referenceCollection2 = SecurityIdentifier.Translate(sourceSids, targetType, out someFailed);
        if (flag && !(forceSuccess & someFailed))
          referenceCollection1 = referenceCollection2;
      }
      if (capacity2 > 0)
      {
        referenceCollection3 = NTAccount.Translate(sourceAccounts, targetType, out someFailed);
        if (flag && !(forceSuccess & someFailed))
          referenceCollection1 = referenceCollection3;
      }
      if (forceSuccess & someFailed)
      {
        IdentityReferenceCollection unmappedIdentities = new IdentityReferenceCollection();
        if (referenceCollection2 != null)
        {
          foreach (IdentityReference identity in referenceCollection2)
          {
            if (identity.GetType() != targetType)
              unmappedIdentities.Add(identity);
          }
        }
        if (referenceCollection3 != null)
        {
          foreach (IdentityReference identity in referenceCollection3)
          {
            if (identity.GetType() != targetType)
              unmappedIdentities.Add(identity);
          }
        }
        throw new IdentityNotMappedException(Environment.GetResourceString("IdentityReference_IdentityNotMapped"), unmappedIdentities);
      }
      if (!flag)
      {
        int num1 = 0;
        int num2 = 0;
        referenceCollection1 = new IdentityReferenceCollection(this.Identities.Count);
        for (int index = 0; index < this.Identities.Count; ++index)
        {
          IdentityReference identity = this[index];
          Type type = identity.GetType();
          if (type == targetType)
            referenceCollection1.Add(identity);
          else if (type == typeof (SecurityIdentifier))
          {
            referenceCollection1.Add(referenceCollection2[num1++]);
          }
          else
          {
            if (!(type == typeof (NTAccount)))
              throw new SystemException();
            referenceCollection1.Add(referenceCollection3[num2++]);
          }
        }
      }
      return referenceCollection1;
    }
  }
}
