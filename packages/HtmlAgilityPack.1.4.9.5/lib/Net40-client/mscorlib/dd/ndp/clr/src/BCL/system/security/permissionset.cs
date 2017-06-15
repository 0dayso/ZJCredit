// Decompiled with JetBrains decompiler
// Type: System.Security.PermissionSet
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Security.Policy;
using System.Security.Util;
using System.Text;
using System.Threading;

namespace System.Security
{
  /// <summary>表示可以包含许多不同类型权限的集合。</summary>
  [ComVisible(true)]
  [Serializable]
  [StrongNameIdentityPermission(SecurityAction.InheritanceDemand, Name = "mscorlib", PublicKey = "0x00000000000000000400000000000000")]
  public class PermissionSet : ISecurityEncodable, ICollection, IEnumerable, IStackWalk, IDeserializationCallback
  {
    internal static readonly PermissionSet s_fullTrust = new PermissionSet(true);
    private bool m_Unrestricted;
    [OptionalField(VersionAdded = 2)]
    private bool m_allPermissionsDecoded;
    [OptionalField(VersionAdded = 2)]
    internal TokenBasedSet m_permSet;
    [OptionalField(VersionAdded = 2)]
    private bool m_ignoreTypeLoadFailures;
    [OptionalField(VersionAdded = 2)]
    private string m_serializedPermissionSet;
    [NonSerialized]
    private bool m_CheckedForNonCas;
    [NonSerialized]
    private bool m_ContainsCas;
    [NonSerialized]
    private bool m_ContainsNonCas;
    [NonSerialized]
    private TokenBasedSet m_permSetSaved;
    private bool readableonly;
    private TokenBasedSet m_unrestrictedPermSet;
    private TokenBasedSet m_normalPermSet;
    [OptionalField(VersionAdded = 2)]
    private bool m_canUnrestrictedOverride;
    private const string s_str_PermissionSet = "PermissionSet";
    private const string s_str_Permission = "Permission";
    private const string s_str_IPermission = "IPermission";
    private const string s_str_Unrestricted = "Unrestricted";
    private const string s_str_PermissionUnion = "PermissionUnion";
    private const string s_str_PermissionIntersection = "PermissionIntersection";
    private const string s_str_PermissionUnrestrictedUnion = "PermissionUnrestrictedUnion";
    private const string s_str_PermissionUnrestrictedIntersection = "PermissionUnrestrictedIntersection";

    /// <summary>获取当前集合的根对象。</summary>
    /// <returns>当前集合的根对象。</returns>
    public virtual object SyncRoot
    {
      get
      {
        return (object) this;
      }
    }

    /// <summary>获取一个值，该值指示是否保证该集合为线程安全的。</summary>
    /// <returns>始终为 false。</returns>
    public virtual bool IsSynchronized
    {
      get
      {
        return false;
      }
    }

    /// <summary>获取一个值，该值指示集合是否为只读。</summary>
    /// <returns>始终为 false。</returns>
    public virtual bool IsReadOnly
    {
      get
      {
        return false;
      }
    }

    /// <summary>获取权限集中包含的权限对象的数目。</summary>
    /// <returns>
    /// <see cref="T:System.Security.PermissionSet" /> 中包含的权限对象的数目。</returns>
    public virtual int Count
    {
      get
      {
        int num = 0;
        if (this.m_permSet != null)
          num += this.m_permSet.GetCount();
        return num;
      }
    }

    internal bool IgnoreTypeLoadFailures
    {
      set
      {
        this.m_ignoreTypeLoadFailures = value;
      }
    }

    internal PermissionSet()
    {
      this.Reset();
      this.m_Unrestricted = true;
    }

    internal PermissionSet(bool fUnrestricted)
      : this()
    {
      this.SetUnrestricted(fUnrestricted);
    }

    /// <summary>用指定的 <see cref="T:System.Security.Permissions.PermissionState" /> 初始化 <see cref="T:System.Security.PermissionSet" /> 类的新实例。</summary>
    /// <param name="state">枚举值之一，指定权限集对资源的访问。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="state" /> 参数不是有效的 <see cref="T:System.Security.Permissions.PermissionState" />。</exception>
    public PermissionSet(PermissionState state)
      : this()
    {
      if (state == PermissionState.Unrestricted)
      {
        this.SetUnrestricted(true);
      }
      else
      {
        if (state != PermissionState.None)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidPermissionState"));
        this.SetUnrestricted(false);
      }
    }

    /// <summary>使用从 <paramref name="permSet" /> 参数获取的初始值初始化 <see cref="T:System.Security.PermissionSet" /> 类的新实例。</summary>
    /// <param name="permSet">要从中获取新 <see cref="T:System.Security.PermissionSet" /> 的值的集合；或者，若要创建空的 <see cref="T:System.Security.PermissionSet" />，则为 null。</param>
    public PermissionSet(PermissionSet permSet)
      : this()
    {
      if (permSet == null)
      {
        this.Reset();
      }
      else
      {
        this.m_Unrestricted = permSet.m_Unrestricted;
        this.m_CheckedForNonCas = permSet.m_CheckedForNonCas;
        this.m_ContainsCas = permSet.m_ContainsCas;
        this.m_ContainsNonCas = permSet.m_ContainsNonCas;
        this.m_ignoreTypeLoadFailures = permSet.m_ignoreTypeLoadFailures;
        if (permSet.m_permSet == null)
          return;
        this.m_permSet = new TokenBasedSet(permSet.m_permSet);
        for (int startingIndex = this.m_permSet.GetStartingIndex(); startingIndex <= this.m_permSet.GetMaxUsedIndex(); ++startingIndex)
        {
          object obj = this.m_permSet.GetItem(startingIndex);
          IPermission permission = obj as IPermission;
          ISecurityElementFactory securityElementFactory = obj as ISecurityElementFactory;
          if (permission != null)
            this.m_permSet.SetItem(startingIndex, (object) permission.Copy());
          else if (securityElementFactory != null)
            this.m_permSet.SetItem(startingIndex, securityElementFactory.Copy());
        }
      }
    }

    private PermissionSet(object trash, object junk)
    {
      this.m_Unrestricted = false;
    }

    [Conditional("_DEBUG")]
    private static void DEBUG_WRITE(string str)
    {
    }

    [Conditional("_DEBUG")]
    private static void DEBUG_COND_WRITE(bool exp, string str)
    {
    }

    [Conditional("_DEBUG")]
    private static void DEBUG_PRINTSTACK(Exception e)
    {
    }

    [OnDeserializing]
    private void OnDeserializing(StreamingContext ctx)
    {
      this.Reset();
    }

    [OnDeserialized]
    private void OnDeserialized(StreamingContext ctx)
    {
      if (this.m_serializedPermissionSet != null)
        this.FromXml(SecurityElement.FromString(this.m_serializedPermissionSet));
      else if (this.m_normalPermSet != null)
        this.m_permSet = this.m_normalPermSet.SpecialUnion(this.m_unrestrictedPermSet);
      else if (this.m_unrestrictedPermSet != null)
        this.m_permSet = this.m_unrestrictedPermSet.SpecialUnion(this.m_normalPermSet);
      this.m_serializedPermissionSet = (string) null;
      this.m_normalPermSet = (TokenBasedSet) null;
      this.m_unrestrictedPermSet = (TokenBasedSet) null;
    }

    [OnSerializing]
    private void OnSerializing(StreamingContext ctx)
    {
      if ((ctx.State & ~(StreamingContextStates.Clone | StreamingContextStates.CrossAppDomain)) == (StreamingContextStates) 0)
        return;
      this.m_serializedPermissionSet = this.ToString();
      if (this.m_permSet != null)
        this.m_permSet.SpecialSplit(ref this.m_unrestrictedPermSet, ref this.m_normalPermSet, this.m_ignoreTypeLoadFailures);
      this.m_permSetSaved = this.m_permSet;
      this.m_permSet = (TokenBasedSet) null;
    }

    [OnSerialized]
    private void OnSerialized(StreamingContext context)
    {
      if ((context.State & ~(StreamingContextStates.Clone | StreamingContextStates.CrossAppDomain)) == (StreamingContextStates) 0)
        return;
      this.m_serializedPermissionSet = (string) null;
      this.m_permSet = this.m_permSetSaved;
      this.m_permSetSaved = (TokenBasedSet) null;
      this.m_unrestrictedPermSet = (TokenBasedSet) null;
      this.m_normalPermSet = (TokenBasedSet) null;
    }

    /// <summary>将该集合的权限对象复制到 <see cref="T:System.Array" /> 中的指示位置。</summary>
    /// <param name="array">要复制到的目标数组。</param>
    /// <param name="index">数组中开始复制的起始位置（从零开始）。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="array" /> 参数具有一个以上的维度。</exception>
    /// <exception cref="T:System.IndexOutOfRangeException">
    /// <paramref name="index" /> 参数超出了 <paramref name="array" /> 参数的范围。</exception>
    public virtual void CopyTo(Array array, int index)
    {
      if (array == null)
        throw new ArgumentNullException("array");
      PermissionSetEnumeratorInternal enumeratorInternal = new PermissionSetEnumeratorInternal(this);
      while (enumeratorInternal.MoveNext())
        array.SetValue(enumeratorInternal.Current, index++);
    }

    internal void Reset()
    {
      this.m_Unrestricted = false;
      this.m_allPermissionsDecoded = true;
      this.m_permSet = (TokenBasedSet) null;
      this.m_ignoreTypeLoadFailures = false;
      this.m_CheckedForNonCas = false;
      this.m_ContainsCas = false;
      this.m_ContainsNonCas = false;
      this.m_permSetSaved = (TokenBasedSet) null;
    }

    internal void CheckSet()
    {
      if (this.m_permSet != null)
        return;
      this.m_permSet = new TokenBasedSet();
    }

    /// <summary>获取一个值，该值指示 <see cref="T:System.Security.PermissionSet" /> 是否为空。</summary>
    /// <returns>如果 <see cref="T:System.Security.PermissionSet" /> 为空，则为 true；否则为 false。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public bool IsEmpty()
    {
      if (this.m_Unrestricted)
        return false;
      if (this.m_permSet == null || this.m_permSet.FastIsEmpty())
        return true;
      PermissionSetEnumeratorInternal enumeratorInternal = new PermissionSetEnumeratorInternal(this);
      while (enumeratorInternal.MoveNext())
      {
        if (!((IPermission) enumeratorInternal.Current).IsSubsetOf((IPermission) null))
          return false;
      }
      return true;
    }

    internal bool FastIsEmpty()
    {
      return !this.m_Unrestricted && (this.m_permSet == null || this.m_permSet.FastIsEmpty());
    }

    internal IPermission GetPermission(int index)
    {
      if (this.m_permSet == null)
        return (IPermission) null;
      object obj = this.m_permSet.GetItem(index);
      if (obj == null)
        return (IPermission) null;
      return obj as IPermission ?? this.CreatePermission(obj, index) ?? (IPermission) null;
    }

    internal IPermission GetPermission(PermissionToken permToken)
    {
      if (permToken == null)
        return (IPermission) null;
      return this.GetPermission(permToken.m_index);
    }

    internal IPermission GetPermission(IPermission perm)
    {
      if (perm == null)
        return (IPermission) null;
      return this.GetPermission(PermissionToken.GetToken(perm));
    }

    /// <summary>获取指定类型的权限对象（如果它存在于集合中）</summary>
    /// <returns>由包含在 <see cref="T:System.Security.PermissionSet" /> 中的 <paramref name="permClass" /> 参数指定的类型的权限对象的副本，或 null（如果权限对象不存在）。</returns>
    /// <param name="permClass">所需权限对象的类型。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public IPermission GetPermission(Type permClass)
    {
      return this.GetPermissionImpl(permClass);
    }

    /// <summary>获取指定类型的权限对象（如果它存在于集合中）</summary>
    /// <returns>由包含在 <see cref="T:System.Security.PermissionSet" /> 中的 <paramref name="permClass" /> 参数指定的类型的权限对象的副本，或 null（如果权限对象不存在）。</returns>
    /// <param name="permClass">权限对象的类型。</param>
    protected virtual IPermission GetPermissionImpl(Type permClass)
    {
      if (permClass == (Type) null)
        return (IPermission) null;
      return this.GetPermission(PermissionToken.FindToken(permClass));
    }

    /// <summary>将权限设置到 <see cref="T:System.Security.PermissionSet" />，同时替换同一类型的所有现有权限。</summary>
    /// <returns>集合权限。</returns>
    /// <param name="perm">要设置的权限。</param>
    /// <exception cref="T:System.InvalidOperationException">此方法是从 <see cref="T:System.Security.ReadOnlyPermissionSet" /> 调用的。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public IPermission SetPermission(IPermission perm)
    {
      return this.SetPermissionImpl(perm);
    }

    /// <summary>将权限设置到 <see cref="T:System.Security.PermissionSet" />，同时替换同一类型的所有现有权限。</summary>
    /// <returns>集合权限。</returns>
    /// <param name="perm">要设置的权限。</param>
    /// <exception cref="T:System.InvalidOperationException">此方法是从 <see cref="T:System.Security.ReadOnlyPermissionSet" /> 调用的。</exception>
    protected virtual IPermission SetPermissionImpl(IPermission perm)
    {
      if (perm == null)
        return (IPermission) null;
      PermissionToken token = PermissionToken.GetToken(perm);
      if ((token.m_type & PermissionTokenType.IUnrestricted) != (PermissionTokenType) 0)
        this.m_Unrestricted = false;
      this.CheckSet();
      this.GetPermission(token.m_index);
      this.m_CheckedForNonCas = false;
      this.m_permSet.SetItem(token.m_index, (object) perm);
      return perm;
    }

    /// <summary>向 <see cref="T:System.Security.PermissionSet" /> 添加指定权限。</summary>
    /// <returns>添加的权限和已存在于 <see cref="T:System.Security.PermissionSet" /> 中的所有相同类型权限的并集。</returns>
    /// <param name="perm">要添加的权限。</param>
    /// <exception cref="T:System.InvalidOperationException">此方法是从 <see cref="T:System.Security.ReadOnlyPermissionSet" /> 调用的。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public IPermission AddPermission(IPermission perm)
    {
      return this.AddPermissionImpl(perm);
    }

    /// <summary>向 <see cref="T:System.Security.PermissionSet" /> 添加指定权限。</summary>
    /// <returns>添加的权限和已存在于 <see cref="T:System.Security.PermissionSet" /> 中的所有相同类型权限的并集；或者 null（如果 <paramref name="perm" /> 为 null）。</returns>
    /// <param name="perm">要添加的权限。</param>
    /// <exception cref="T:System.InvalidOperationException">此方法是从 <see cref="T:System.Security.ReadOnlyPermissionSet" /> 调用的。</exception>
    protected virtual IPermission AddPermissionImpl(IPermission perm)
    {
      if (perm == null)
        return (IPermission) null;
      this.m_CheckedForNonCas = false;
      PermissionToken token = PermissionToken.GetToken(perm);
      if (this.IsUnrestricted() && (token.m_type & PermissionTokenType.IUnrestricted) != (PermissionTokenType) 0)
      {
        Type type = perm.GetType();
        object[] objArray = new object[1]{ (object) PermissionState.Unrestricted };
        int num = 28;
        // ISSUE: variable of the null type
        __Null local1 = null;
        object[] args = objArray;
        // ISSUE: variable of the null type
        __Null local2 = null;
        return (IPermission) Activator.CreateInstance(type, (BindingFlags) num, (Binder) local1, args, (CultureInfo) local2);
      }
      this.CheckSet();
      IPermission permission1 = this.GetPermission(token.m_index);
      if (permission1 != null)
      {
        IPermission permission2 = permission1.Union(perm);
        this.m_permSet.SetItem(token.m_index, (object) permission2);
        return permission2;
      }
      this.m_permSet.SetItem(token.m_index, (object) perm);
      return perm;
    }

    private IPermission RemovePermission(int index)
    {
      if (this.GetPermission(index) == null)
        return (IPermission) null;
      return (IPermission) this.m_permSet.RemoveItem(index);
    }

    /// <summary>从集合中移除某个类型的权限。</summary>
    /// <returns>从集合中移除的权限。</returns>
    /// <param name="permClass">要删除的权限的类型。</param>
    /// <exception cref="T:System.InvalidOperationException">此方法是从 <see cref="T:System.Security.ReadOnlyPermissionSet" /> 调用的。</exception>
    public IPermission RemovePermission(Type permClass)
    {
      return this.RemovePermissionImpl(permClass);
    }

    /// <summary>从集合中移除某个类型的权限。</summary>
    /// <returns>从集合中移除的权限。</returns>
    /// <param name="permClass">要移除的权限的类型。</param>
    /// <exception cref="T:System.InvalidOperationException">此方法是从 <see cref="T:System.Security.ReadOnlyPermissionSet" /> 调用的。</exception>
    protected virtual IPermission RemovePermissionImpl(Type permClass)
    {
      if (permClass == (Type) null)
        return (IPermission) null;
      PermissionToken token = PermissionToken.FindToken(permClass);
      if (token == null)
        return (IPermission) null;
      return this.RemovePermission(token.m_index);
    }

    internal void SetUnrestricted(bool unrestricted)
    {
      this.m_Unrestricted = unrestricted;
      if (!unrestricted)
        return;
      this.m_permSet = (TokenBasedSet) null;
    }

    /// <summary>确定 <see cref="T:System.Security.PermissionSet" /> 是否为 Unrestricted。</summary>
    /// <returns>如果 <see cref="T:System.Security.PermissionSet" /> 为 Unrestricted，则为 true；否则为 false。</returns>
    public bool IsUnrestricted()
    {
      return this.m_Unrestricted;
    }

    internal bool IsSubsetOfHelper(PermissionSet target, PermissionSet.IsSubsetOfType type, out IPermission firstPermThatFailed, bool ignoreNonCas)
    {
      firstPermThatFailed = (IPermission) null;
      if (target == null || target.FastIsEmpty())
      {
        if (this.IsEmpty())
          return true;
        firstPermThatFailed = this.GetFirstPerm();
        return false;
      }
      if (this.IsUnrestricted() && !target.IsUnrestricted())
        return false;
      if (this.m_permSet == null)
        return true;
      target.CheckSet();
      for (int startingIndex = this.m_permSet.GetStartingIndex(); startingIndex <= this.m_permSet.GetMaxUsedIndex(); ++startingIndex)
      {
        IPermission permission1 = this.GetPermission(startingIndex);
        if (permission1 != null && !permission1.IsSubsetOf((IPermission) null))
        {
          IPermission permission2 = target.GetPermission(startingIndex);
          if (!target.m_Unrestricted)
          {
            CodeAccessPermission accessPermission = permission1 as CodeAccessPermission;
            if (accessPermission == null)
            {
              if (!ignoreNonCas && !permission1.IsSubsetOf(permission2))
              {
                firstPermThatFailed = permission1;
                return false;
              }
            }
            else
            {
              firstPermThatFailed = permission1;
              switch (type)
              {
                case PermissionSet.IsSubsetOfType.Normal:
                  if (!permission1.IsSubsetOf(permission2))
                    return false;
                  break;
                case PermissionSet.IsSubsetOfType.CheckDemand:
                  if (!accessPermission.CheckDemand((CodeAccessPermission) permission2))
                    return false;
                  break;
                case PermissionSet.IsSubsetOfType.CheckPermitOnly:
                  if (!accessPermission.CheckPermitOnly((CodeAccessPermission) permission2))
                    return false;
                  break;
                case PermissionSet.IsSubsetOfType.CheckAssertion:
                  if (!accessPermission.CheckAssert((CodeAccessPermission) permission2))
                    return false;
                  break;
              }
              firstPermThatFailed = (IPermission) null;
            }
          }
        }
      }
      return true;
    }

    /// <summary>确定当前 <see cref="T:System.Security.PermissionSet" /> 是否为指定 <see cref="T:System.Security.PermissionSet" /> 的子集。</summary>
    /// <returns>如果当前 <see cref="T:System.Security.PermissionSet" /> 是 <paramref name="target" /> 参数的子集，则为 true；否则为 false。</returns>
    /// <param name="target">要测试子集关系的权限集。它必须是 <see cref="T:System.Security.PermissionSet" /> 或 <see cref="T:System.Security.NamedPermissionSet" />。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public bool IsSubsetOf(PermissionSet target)
    {
      IPermission firstPermThatFailed;
      return this.IsSubsetOfHelper(target, PermissionSet.IsSubsetOfType.Normal, out firstPermThatFailed, false);
    }

    internal bool CheckDemand(PermissionSet target, out IPermission firstPermThatFailed)
    {
      return this.IsSubsetOfHelper(target, PermissionSet.IsSubsetOfType.CheckDemand, out firstPermThatFailed, true);
    }

    internal bool CheckPermitOnly(PermissionSet target, out IPermission firstPermThatFailed)
    {
      return this.IsSubsetOfHelper(target, PermissionSet.IsSubsetOfType.CheckPermitOnly, out firstPermThatFailed, true);
    }

    internal bool CheckAssertion(PermissionSet target)
    {
      IPermission firstPermThatFailed;
      return this.IsSubsetOfHelper(target, PermissionSet.IsSubsetOfType.CheckAssertion, out firstPermThatFailed, true);
    }

    internal bool CheckDeny(PermissionSet deniedSet, out IPermission firstPermThatFailed)
    {
      firstPermThatFailed = (IPermission) null;
      if (deniedSet == null || deniedSet.FastIsEmpty() || this.FastIsEmpty())
        return true;
      if (this.m_Unrestricted && deniedSet.m_Unrestricted)
        return false;
      PermissionSetEnumeratorInternal enumeratorInternal1 = new PermissionSetEnumeratorInternal(this);
      while (enumeratorInternal1.MoveNext())
      {
        CodeAccessPermission accessPermission = enumeratorInternal1.Current as CodeAccessPermission;
        if (accessPermission != null && !accessPermission.IsSubsetOf((IPermission) null))
        {
          if (deniedSet.m_Unrestricted)
          {
            firstPermThatFailed = (IPermission) accessPermission;
            return false;
          }
          CodeAccessPermission denied = (CodeAccessPermission) deniedSet.GetPermission(enumeratorInternal1.GetCurrentIndex());
          if (!accessPermission.CheckDeny(denied))
          {
            firstPermThatFailed = (IPermission) accessPermission;
            return false;
          }
        }
      }
      if (this.m_Unrestricted)
      {
        PermissionSetEnumeratorInternal enumeratorInternal2 = new PermissionSetEnumeratorInternal(deniedSet);
        while (enumeratorInternal2.MoveNext())
        {
          if (enumeratorInternal2.Current is IPermission)
            return false;
        }
      }
      return true;
    }

    internal void CheckDecoded(CodeAccessPermission demandedPerm, PermissionToken tokenDemandedPerm)
    {
      if (this.m_allPermissionsDecoded || this.m_permSet == null)
        return;
      if (tokenDemandedPerm == null)
        tokenDemandedPerm = PermissionToken.GetToken((IPermission) demandedPerm);
      this.CheckDecoded(tokenDemandedPerm.m_index);
    }

    internal void CheckDecoded(int index)
    {
      if (this.m_allPermissionsDecoded || this.m_permSet == null)
        return;
      this.GetPermission(index);
    }

    internal void CheckDecoded(PermissionSet demandedSet)
    {
      if (this.m_allPermissionsDecoded || this.m_permSet == null)
        return;
      PermissionSetEnumeratorInternal enumeratorInternal = demandedSet.GetEnumeratorInternal();
      while (enumeratorInternal.MoveNext())
        this.CheckDecoded(enumeratorInternal.GetCurrentIndex());
    }

    internal static void SafeChildAdd(SecurityElement parent, ISecurityElementFactory child, bool copy)
    {
      if (child == parent)
        return;
      if (child.GetTag().Equals("IPermission") || child.GetTag().Equals("Permission"))
        parent.AddChild(child);
      else if (parent.Tag.Equals(child.GetTag()))
      {
        SecurityElement securityElement = (SecurityElement) child;
        for (int index = 0; index < securityElement.InternalChildren.Count; ++index)
        {
          ISecurityElementFactory child1 = (ISecurityElementFactory) securityElement.InternalChildren[index];
          parent.AddChildNoDuplicates(child1);
        }
      }
      else
        parent.AddChild(copy ? (ISecurityElementFactory) child.Copy() : child);
    }

    internal void InplaceIntersect(PermissionSet other)
    {
      Exception exception = (Exception) null;
      this.m_CheckedForNonCas = false;
      if (this == other)
        return;
      if (other == null || other.FastIsEmpty())
      {
        this.Reset();
      }
      else
      {
        if (this.FastIsEmpty())
          return;
        int num1 = this.m_permSet == null ? -1 : this.m_permSet.GetMaxUsedIndex();
        int num2 = other.m_permSet == null ? -1 : other.m_permSet.GetMaxUsedIndex();
        if (this.IsUnrestricted() && num1 < num2)
        {
          num1 = num2;
          this.CheckSet();
        }
        if (other.IsUnrestricted())
          other.CheckSet();
        for (int index = 0; index <= num1; ++index)
        {
          object obj1 = this.m_permSet.GetItem(index);
          IPermission permission1 = obj1 as IPermission;
          ISecurityElementFactory child1 = obj1 as ISecurityElementFactory;
          object obj2 = other.m_permSet.GetItem(index);
          IPermission target = obj2 as IPermission;
          ISecurityElementFactory child2 = obj2 as ISecurityElementFactory;
          if (obj1 != null || obj2 != null)
          {
            if (child1 != null && child2 != null)
            {
              if (child1.GetTag().Equals("PermissionIntersection") || child1.GetTag().Equals("PermissionUnrestrictedIntersection"))
              {
                PermissionSet.SafeChildAdd((SecurityElement) child1, child2, true);
              }
              else
              {
                bool copy = true;
                if (this.IsUnrestricted())
                {
                  SecurityElement parent = new SecurityElement("PermissionUnrestrictedUnion");
                  string name = "class";
                  string str = child1.Attribute("class");
                  parent.AddAttribute(name, str);
                  ISecurityElementFactory child3 = child1;
                  int num3 = 0;
                  PermissionSet.SafeChildAdd(parent, child3, num3 != 0);
                  child1 = (ISecurityElementFactory) parent;
                }
                if (other.IsUnrestricted())
                {
                  SecurityElement parent = new SecurityElement("PermissionUnrestrictedUnion");
                  string name = "class";
                  string str = child2.Attribute("class");
                  parent.AddAttribute(name, str);
                  ISecurityElementFactory child3 = child2;
                  int num3 = 1;
                  PermissionSet.SafeChildAdd(parent, child3, num3 != 0);
                  child2 = (ISecurityElementFactory) parent;
                  copy = false;
                }
                SecurityElement parent1 = new SecurityElement("PermissionIntersection");
                parent1.AddAttribute("class", child1.Attribute("class"));
                PermissionSet.SafeChildAdd(parent1, child1, false);
                PermissionSet.SafeChildAdd(parent1, child2, copy);
                this.m_permSet.SetItem(index, (object) parent1);
              }
            }
            else if (obj1 == null)
            {
              if (this.IsUnrestricted())
              {
                if (child2 != null)
                {
                  SecurityElement parent = new SecurityElement("PermissionUnrestrictedIntersection");
                  parent.AddAttribute("class", child2.Attribute("class"));
                  PermissionSet.SafeChildAdd(parent, child2, true);
                  this.m_permSet.SetItem(index, (object) parent);
                }
                else if ((((PermissionToken) PermissionToken.s_tokenSet.GetItem(index)).m_type & PermissionTokenType.IUnrestricted) != (PermissionTokenType) 0)
                  this.m_permSet.SetItem(index, (object) target.Copy());
              }
            }
            else if (obj2 == null)
            {
              if (other.IsUnrestricted())
              {
                if (child1 != null)
                {
                  SecurityElement parent = new SecurityElement("PermissionUnrestrictedIntersection");
                  parent.AddAttribute("class", child1.Attribute("class"));
                  PermissionSet.SafeChildAdd(parent, child1, false);
                  this.m_permSet.SetItem(index, (object) parent);
                }
                else if ((((PermissionToken) PermissionToken.s_tokenSet.GetItem(index)).m_type & PermissionTokenType.IUnrestricted) == (PermissionTokenType) 0)
                  this.m_permSet.SetItem(index, (object) null);
              }
              else
                this.m_permSet.SetItem(index, (object) null);
            }
            else
            {
              if (child1 != null)
                permission1 = this.CreatePermission((object) child1, index);
              if (child2 != null)
                target = other.CreatePermission((object) child2, index);
              try
              {
                IPermission permission2 = permission1 != null ? (target != null ? permission1.Intersect(target) : permission1) : target;
                this.m_permSet.SetItem(index, (object) permission2);
              }
              catch (Exception ex)
              {
                if (exception == null)
                  exception = ex;
              }
            }
          }
        }
        this.m_Unrestricted = this.m_Unrestricted && other.m_Unrestricted;
        if (exception != null)
          throw exception;
      }
    }

    /// <summary>创建并返回一个权限集，该权限集是当前 <see cref="T:System.Security.PermissionSet" /> 和指定 <see cref="T:System.Security.PermissionSet" /> 的交集。</summary>
    /// <returns>一个新权限集，表示当前 <see cref="T:System.Security.PermissionSet" /> 与指定目标的交集。如果交集为空，则此对象为 null。</returns>
    /// <param name="other">要与当前 <see cref="T:System.Security.PermissionSet" /> 相交的权限集。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public PermissionSet Intersect(PermissionSet other)
    {
      if (other == null || other.FastIsEmpty() || this.FastIsEmpty())
        return (PermissionSet) null;
      int num1 = this.m_permSet == null ? -1 : this.m_permSet.GetMaxUsedIndex();
      int num2 = other.m_permSet == null ? -1 : other.m_permSet.GetMaxUsedIndex();
      int num3 = num1 < num2 ? num1 : num2;
      if (this.IsUnrestricted() && num3 < num2)
      {
        num3 = num2;
        this.CheckSet();
      }
      if (other.IsUnrestricted() && num3 < num1)
      {
        num3 = num1;
        other.CheckSet();
      }
      PermissionSet permissionSet = new PermissionSet(false);
      if (num3 > -1)
        permissionSet.m_permSet = new TokenBasedSet();
      for (int index = 0; index <= num3; ++index)
      {
        object obj1 = this.m_permSet.GetItem(index);
        IPermission permission1 = obj1 as IPermission;
        ISecurityElementFactory child1 = obj1 as ISecurityElementFactory;
        object obj2 = other.m_permSet.GetItem(index);
        IPermission target = obj2 as IPermission;
        ISecurityElementFactory child2 = obj2 as ISecurityElementFactory;
        if (obj1 != null || obj2 != null)
        {
          if (child1 != null && child2 != null)
          {
            bool copy1 = true;
            bool copy2 = true;
            SecurityElement parent1 = new SecurityElement("PermissionIntersection");
            parent1.AddAttribute("class", child2.Attribute("class"));
            if (this.IsUnrestricted())
            {
              SecurityElement parent2 = new SecurityElement("PermissionUnrestrictedUnion");
              string name = "class";
              string str = child1.Attribute("class");
              parent2.AddAttribute(name, str);
              ISecurityElementFactory child3 = child1;
              int num4 = 1;
              PermissionSet.SafeChildAdd(parent2, child3, num4 != 0);
              copy2 = false;
              child1 = (ISecurityElementFactory) parent2;
            }
            if (other.IsUnrestricted())
            {
              SecurityElement parent2 = new SecurityElement("PermissionUnrestrictedUnion");
              string name = "class";
              string str = child2.Attribute("class");
              parent2.AddAttribute(name, str);
              ISecurityElementFactory child3 = child2;
              int num4 = 1;
              PermissionSet.SafeChildAdd(parent2, child3, num4 != 0);
              copy1 = false;
              child2 = (ISecurityElementFactory) parent2;
            }
            PermissionSet.SafeChildAdd(parent1, child2, copy1);
            PermissionSet.SafeChildAdd(parent1, child1, copy2);
            permissionSet.m_permSet.SetItem(index, (object) parent1);
          }
          else if (obj1 == null)
          {
            if (this.m_Unrestricted)
            {
              if (child2 != null)
              {
                SecurityElement parent = new SecurityElement("PermissionUnrestrictedIntersection");
                parent.AddAttribute("class", child2.Attribute("class"));
                PermissionSet.SafeChildAdd(parent, child2, true);
                permissionSet.m_permSet.SetItem(index, (object) parent);
              }
              else if (target != null && (((PermissionToken) PermissionToken.s_tokenSet.GetItem(index)).m_type & PermissionTokenType.IUnrestricted) != (PermissionTokenType) 0)
                permissionSet.m_permSet.SetItem(index, (object) target.Copy());
            }
          }
          else if (obj2 == null)
          {
            if (other.m_Unrestricted)
            {
              if (child1 != null)
              {
                SecurityElement parent = new SecurityElement("PermissionUnrestrictedIntersection");
                parent.AddAttribute("class", child1.Attribute("class"));
                PermissionSet.SafeChildAdd(parent, child1, true);
                permissionSet.m_permSet.SetItem(index, (object) parent);
              }
              else if (permission1 != null && (((PermissionToken) PermissionToken.s_tokenSet.GetItem(index)).m_type & PermissionTokenType.IUnrestricted) != (PermissionTokenType) 0)
                permissionSet.m_permSet.SetItem(index, (object) permission1.Copy());
            }
          }
          else
          {
            if (child1 != null)
              permission1 = this.CreatePermission((object) child1, index);
            if (child2 != null)
              target = other.CreatePermission((object) child2, index);
            IPermission permission2 = permission1 != null ? (target != null ? permission1.Intersect(target) : permission1) : target;
            permissionSet.m_permSet.SetItem(index, (object) permission2);
          }
        }
      }
      permissionSet.m_Unrestricted = this.m_Unrestricted && other.m_Unrestricted;
      if (permissionSet.FastIsEmpty())
        return (PermissionSet) null;
      return permissionSet;
    }

    internal void InplaceUnion(PermissionSet other)
    {
      if (this == other || other == null || other.FastIsEmpty())
        return;
      this.m_CheckedForNonCas = false;
      this.m_Unrestricted = this.m_Unrestricted || other.m_Unrestricted;
      if (this.m_Unrestricted)
      {
        this.m_permSet = (TokenBasedSet) null;
      }
      else
      {
        int num = -1;
        if (other.m_permSet != null)
        {
          num = other.m_permSet.GetMaxUsedIndex();
          this.CheckSet();
        }
        Exception exception = (Exception) null;
        for (int index = 0; index <= num; ++index)
        {
          object obj1 = this.m_permSet.GetItem(index);
          IPermission permission1 = obj1 as IPermission;
          ISecurityElementFactory child1 = obj1 as ISecurityElementFactory;
          object obj2 = other.m_permSet.GetItem(index);
          IPermission target = obj2 as IPermission;
          ISecurityElementFactory child2 = obj2 as ISecurityElementFactory;
          if (obj1 != null || obj2 != null)
          {
            if (child1 != null && child2 != null)
            {
              if (child1.GetTag().Equals("PermissionUnion") || child1.GetTag().Equals("PermissionUnrestrictedUnion"))
              {
                PermissionSet.SafeChildAdd((SecurityElement) child1, child2, true);
              }
              else
              {
                SecurityElement parent = this.IsUnrestricted() || other.IsUnrestricted() ? new SecurityElement("PermissionUnrestrictedUnion") : new SecurityElement("PermissionUnion");
                parent.AddAttribute("class", child1.Attribute("class"));
                PermissionSet.SafeChildAdd(parent, child1, false);
                PermissionSet.SafeChildAdd(parent, child2, true);
                this.m_permSet.SetItem(index, (object) parent);
              }
            }
            else if (obj1 == null)
            {
              if (child2 != null)
                this.m_permSet.SetItem(index, child2.Copy());
              else if (target != null && ((((PermissionToken) PermissionToken.s_tokenSet.GetItem(index)).m_type & PermissionTokenType.IUnrestricted) == (PermissionTokenType) 0 || !this.m_Unrestricted))
                this.m_permSet.SetItem(index, (object) target.Copy());
            }
            else if (obj2 != null)
            {
              if (child1 != null)
                permission1 = this.CreatePermission((object) child1, index);
              if (child2 != null)
                target = other.CreatePermission((object) child2, index);
              try
              {
                IPermission permission2 = permission1 != null ? (target != null ? permission1.Union(target) : permission1) : target;
                this.m_permSet.SetItem(index, (object) permission2);
              }
              catch (Exception ex)
              {
                if (exception == null)
                  exception = ex;
              }
            }
          }
        }
        if (exception != null)
          throw exception;
      }
    }

    /// <summary>创建一个 <see cref="T:System.Security.PermissionSet" />，它是当前 <see cref="T:System.Security.PermissionSet" /> 和指定 <see cref="T:System.Security.PermissionSet" /> 的并集。</summary>
    /// <returns>一个新权限集，表示当前 <see cref="T:System.Security.PermissionSet" /> 与指定 <see cref="T:System.Security.PermissionSet" /> 的并集。</returns>
    /// <param name="other">要与当前 <see cref="T:System.Security.PermissionSet" /> 构成并集的权限集。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public PermissionSet Union(PermissionSet other)
    {
      if (other == null || other.FastIsEmpty())
        return this.Copy();
      if (this.FastIsEmpty())
        return other.Copy();
      PermissionSet permissionSet = new PermissionSet();
      permissionSet.m_Unrestricted = this.m_Unrestricted || other.m_Unrestricted;
      if (permissionSet.m_Unrestricted)
        return permissionSet;
      this.CheckSet();
      other.CheckSet();
      int num = this.m_permSet.GetMaxUsedIndex() > other.m_permSet.GetMaxUsedIndex() ? this.m_permSet.GetMaxUsedIndex() : other.m_permSet.GetMaxUsedIndex();
      permissionSet.m_permSet = new TokenBasedSet();
      for (int index = 0; index <= num; ++index)
      {
        object obj1 = this.m_permSet.GetItem(index);
        IPermission permission1 = obj1 as IPermission;
        ISecurityElementFactory child1 = obj1 as ISecurityElementFactory;
        object obj2 = other.m_permSet.GetItem(index);
        IPermission target = obj2 as IPermission;
        ISecurityElementFactory child2 = obj2 as ISecurityElementFactory;
        if (obj1 != null || obj2 != null)
        {
          if (child1 != null && child2 != null)
          {
            SecurityElement parent = this.IsUnrestricted() || other.IsUnrestricted() ? new SecurityElement("PermissionUnrestrictedUnion") : new SecurityElement("PermissionUnion");
            parent.AddAttribute("class", child1.Attribute("class"));
            PermissionSet.SafeChildAdd(parent, child1, true);
            PermissionSet.SafeChildAdd(parent, child2, true);
            permissionSet.m_permSet.SetItem(index, (object) parent);
          }
          else if (obj1 == null)
          {
            if (child2 != null)
              permissionSet.m_permSet.SetItem(index, child2.Copy());
            else if (target != null && ((((PermissionToken) PermissionToken.s_tokenSet.GetItem(index)).m_type & PermissionTokenType.IUnrestricted) == (PermissionTokenType) 0 || !permissionSet.m_Unrestricted))
              permissionSet.m_permSet.SetItem(index, (object) target.Copy());
          }
          else if (obj2 == null)
          {
            if (child1 != null)
              permissionSet.m_permSet.SetItem(index, child1.Copy());
            else if (permission1 != null && ((((PermissionToken) PermissionToken.s_tokenSet.GetItem(index)).m_type & PermissionTokenType.IUnrestricted) == (PermissionTokenType) 0 || !permissionSet.m_Unrestricted))
              permissionSet.m_permSet.SetItem(index, (object) permission1.Copy());
          }
          else
          {
            if (child1 != null)
              permission1 = this.CreatePermission((object) child1, index);
            if (child2 != null)
              target = other.CreatePermission((object) child2, index);
            IPermission permission2 = permission1 != null ? (target != null ? permission1.Union(target) : permission1) : target;
            permissionSet.m_permSet.SetItem(index, (object) permission2);
          }
        }
      }
      return permissionSet;
    }

    internal void MergeDeniedSet(PermissionSet denied)
    {
      if (denied == null || denied.FastIsEmpty() || this.FastIsEmpty())
        return;
      this.m_CheckedForNonCas = false;
      if (this.m_permSet == null || denied.m_permSet == null)
        return;
      int num = denied.m_permSet.GetMaxUsedIndex() > this.m_permSet.GetMaxUsedIndex() ? this.m_permSet.GetMaxUsedIndex() : denied.m_permSet.GetMaxUsedIndex();
      for (int index = 0; index <= num; ++index)
      {
        IPermission target = denied.m_permSet.GetItem(index) as IPermission;
        if (target != null)
        {
          IPermission permission = this.m_permSet.GetItem(index) as IPermission;
          if (permission == null && !this.m_Unrestricted)
            denied.m_permSet.SetItem(index, (object) null);
          else if (permission != null && target != null && permission.IsSubsetOf(target))
          {
            this.m_permSet.SetItem(index, (object) null);
            denied.m_permSet.SetItem(index, (object) null);
          }
        }
      }
    }

    internal bool Contains(IPermission perm)
    {
      if (perm == null || this.m_Unrestricted)
        return true;
      if (this.FastIsEmpty())
        return false;
      PermissionToken token = PermissionToken.GetToken(perm);
      if (this.m_permSet.GetItem(token.m_index) == null)
        return perm.IsSubsetOf((IPermission) null);
      IPermission permission = this.GetPermission(token.m_index);
      if (permission != null)
        return perm.IsSubsetOf(permission);
      return perm.IsSubsetOf((IPermission) null);
    }

    /// <summary>确定指定的 <see cref="T:System.Security.PermissionSet" /> 或 <see cref="T:System.Security.NamedPermissionSet" /> 对象是否等于当前的 <see cref="T:System.Security.PermissionSet" />。</summary>
    /// <returns>如果指定的对象等于当前的 <see cref="T:System.Security.PermissionSet" /> 对象，则为 true；否则为 false。</returns>
    /// <param name="obj">将与当前 <see cref="T:System.Security.PermissionSet" /> 进行比较的对象。</param>
    [ComVisible(false)]
    public override bool Equals(object obj)
    {
      PermissionSet permissionSet = obj as PermissionSet;
      if (permissionSet == null || this.m_Unrestricted != permissionSet.m_Unrestricted)
        return false;
      this.CheckSet();
      permissionSet.CheckSet();
      this.DecodeAllPermissions();
      permissionSet.DecodeAllPermissions();
      int num = Math.Max(this.m_permSet.GetMaxUsedIndex(), permissionSet.m_permSet.GetMaxUsedIndex());
      for (int index = 0; index <= num; ++index)
      {
        IPermission permission1 = (IPermission) this.m_permSet.GetItem(index);
        IPermission permission2 = (IPermission) permissionSet.m_permSet.GetItem(index);
        if (permission1 != null || permission2 != null)
        {
          if (permission1 == null)
          {
            if (!permission2.IsSubsetOf((IPermission) null))
              return false;
          }
          else if (permission2 == null)
          {
            if (!permission1.IsSubsetOf((IPermission) null))
              return false;
          }
          else if (!permission1.Equals((object) permission2))
            return false;
        }
      }
      return true;
    }

    /// <summary>获取适合在哈希算法和类似哈希表的数据结构中使用的 <see cref="T:System.Security.PermissionSet" /> 对象的哈希代码。</summary>
    /// <returns>当前 <see cref="T:System.Security.PermissionSet" /> 对象的哈希代码。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [ComVisible(false)]
    public override int GetHashCode()
    {
      int num = this.m_Unrestricted ? -1 : 0;
      if (this.m_permSet != null)
      {
        this.DecodeAllPermissions();
        int maxUsedIndex = this.m_permSet.GetMaxUsedIndex();
        for (int startingIndex = this.m_permSet.GetStartingIndex(); startingIndex <= maxUsedIndex; ++startingIndex)
        {
          IPermission permission = (IPermission) this.m_permSet.GetItem(startingIndex);
          if (permission != null)
            num ^= permission.GetHashCode();
        }
      }
      return num;
    }

    /// <summary>如果尚未向调用堆栈中的所有较高位置调用方授予由当前实例指定的权限，则在运行时强制引发 <see cref="T:System.Security.SecurityException" />。</summary>
    /// <exception cref="T:System.Security.SecurityException">调用链中的调用方没有所需的权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public void Demand()
    {
      if (this.FastIsEmpty())
        return;
      this.ContainsNonCodeAccessPermissions();
      if (this.m_ContainsCas)
      {
        StackCrawlMark stackMark = StackCrawlMark.LookForMyCallersCaller;
        CodeAccessSecurityEngine.Check(this.GetCasOnlySet(), ref stackMark);
      }
      if (!this.m_ContainsNonCas)
        return;
      this.DemandNonCAS();
    }

    [SecurityCritical]
    internal void DemandNonCAS()
    {
      this.ContainsNonCodeAccessPermissions();
      if (!this.m_ContainsNonCas || this.m_permSet == null)
        return;
      this.CheckSet();
      for (int startingIndex = this.m_permSet.GetStartingIndex(); startingIndex <= this.m_permSet.GetMaxUsedIndex(); ++startingIndex)
      {
        IPermission permission = this.GetPermission(startingIndex);
        if (permission != null && !(permission is CodeAccessPermission))
          permission.Demand();
      }
    }

    /// <summary>声明调用代码能够通过调用此方法的代码，访问受权限请求保护的资源，即使堆栈上部的调用方未被授予访问该资源的权限。使用 <see cref="M:System.Security.PermissionSet.Assert" /> 会产生安全漏洞。</summary>
    /// <exception cref="T:System.Security.SecurityException">所断言的 <see cref="T:System.Security.PermissionSet" /> 实例还没有被授予断言代码。- 或 -当前框架已经有一个活动的 <see cref="M:System.Security.PermissionSet.Assert" />。</exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public void Assert()
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      SecurityRuntime.Assert(this, ref stackMark);
    }

    /// <summary>导致任何经过调用代码以请求某个权限且该权限与当前 <see cref="T:System.Security.PermissionSet" /> 中所包含的类型的权限有交集的 <see cref="M:System.Security.PermissionSet.Demand" /> 失败。</summary>
    /// <exception cref="T:System.Security.SecurityException">以前对 <see cref="M:System.Security.PermissionSet.Deny" /> 的调用已经限制了当前堆栈帧的权限。</exception>
    [SecuritySafeCritical]
    [Obsolete("Deny is obsolete and will be removed in a future release of the .NET Framework. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public void Deny()
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      SecurityRuntime.Deny(this, ref stackMark);
    }

    /// <summary>导致所有经过调用代码以请求不是当前 <see cref="T:System.Security.PermissionSet" /> 的子集的任何 <see cref="T:System.Security.PermissionSet" /> 的 <see cref="M:System.Security.PermissionSet.Demand" /> 失败。</summary>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public void PermitOnly()
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      SecurityRuntime.PermitOnly(this, ref stackMark);
    }

    internal IPermission GetFirstPerm()
    {
      IEnumerator enumerator = this.GetEnumerator();
      if (!enumerator.MoveNext())
        return (IPermission) null;
      return enumerator.Current as IPermission;
    }

    /// <summary>创建 <see cref="T:System.Security.PermissionSet" /> 的副本。</summary>
    /// <returns>
    /// <see cref="T:System.Security.PermissionSet" /> 的副本。</returns>
    public virtual PermissionSet Copy()
    {
      return new PermissionSet(this);
    }

    internal PermissionSet CopyWithNoIdentityPermissions()
    {
      PermissionSet permissionSet = new PermissionSet(this);
      Type permClass1 = typeof (GacIdentityPermission);
      permissionSet.RemovePermission(permClass1);
      Type permClass2 = typeof (PublisherIdentityPermission);
      permissionSet.RemovePermission(permClass2);
      Type permClass3 = typeof (StrongNameIdentityPermission);
      permissionSet.RemovePermission(permClass3);
      Type permClass4 = typeof (UrlIdentityPermission);
      permissionSet.RemovePermission(permClass4);
      Type permClass5 = typeof (ZoneIdentityPermission);
      permissionSet.RemovePermission(permClass5);
      return permissionSet;
    }

    /// <summary>返回集合的权限的枚举数。</summary>
    /// <returns>集合的权限的枚举数对象。</returns>
    public IEnumerator GetEnumerator()
    {
      return this.GetEnumeratorImpl();
    }

    /// <summary>返回集合的权限的枚举数。</summary>
    /// <returns>集合的权限的枚举数对象。</returns>
    protected virtual IEnumerator GetEnumeratorImpl()
    {
      return (IEnumerator) new PermissionSetEnumerator(this);
    }

    internal PermissionSetEnumeratorInternal GetEnumeratorInternal()
    {
      return new PermissionSetEnumeratorInternal(this);
    }

    /// <summary>返回 <see cref="T:System.Security.PermissionSet" /> 的字符串表示形式。</summary>
    /// <returns>
    /// <see cref="T:System.Security.PermissionSet" /> 的表示形式。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public override string ToString()
    {
      return this.ToXml().ToString();
    }

    private void NormalizePermissionSet()
    {
      PermissionSet permissionSet = new PermissionSet(false);
      permissionSet.m_Unrestricted = this.m_Unrestricted;
      if (this.m_permSet != null)
      {
        for (int startingIndex = this.m_permSet.GetStartingIndex(); startingIndex <= this.m_permSet.GetMaxUsedIndex(); ++startingIndex)
        {
          object obj = this.m_permSet.GetItem(startingIndex);
          IPermission perm = obj as IPermission;
          ISecurityElementFactory securityElementFactory = obj as ISecurityElementFactory;
          if (securityElementFactory != null)
            perm = this.CreatePerm((object) securityElementFactory);
          if (perm != null)
            permissionSet.SetPermission(perm);
        }
      }
      this.m_permSet = permissionSet.m_permSet;
    }

    private bool DecodeXml(byte[] data, HostProtectionResource fullTrustOnlyResources, HostProtectionResource inaccessibleResources)
    {
      if (data != null && data.Length != 0)
        this.FromXml(new Parser(data, Tokenizer.ByteTokenEncoding.UnicodeTokens).GetTopElement());
      this.FilterHostProtectionPermissions(fullTrustOnlyResources, inaccessibleResources);
      this.DecodeAllPermissions();
      return true;
    }

    private void DecodeAllPermissions()
    {
      if (this.m_permSet == null)
      {
        this.m_allPermissionsDecoded = true;
      }
      else
      {
        int maxUsedIndex = this.m_permSet.GetMaxUsedIndex();
        for (int index = 0; index <= maxUsedIndex; ++index)
          this.GetPermission(index);
        this.m_allPermissionsDecoded = true;
      }
    }

    internal void FilterHostProtectionPermissions(HostProtectionResource fullTrustOnly, HostProtectionResource inaccessible)
    {
      HostProtectionPermission.protectedResources = fullTrustOnly;
      HostProtectionPermission protectionPermission1 = (HostProtectionPermission) this.GetPermission(HostProtectionPermission.GetTokenIndex());
      if (protectionPermission1 == null)
        return;
      HostProtectionPermission protectionPermission2 = (HostProtectionPermission) protectionPermission1.Intersect((IPermission) new HostProtectionPermission(fullTrustOnly));
      if (protectionPermission2 == null)
      {
        this.RemovePermission(typeof (HostProtectionPermission));
      }
      else
      {
        if (protectionPermission2.Resources == protectionPermission1.Resources)
          return;
        this.SetPermission((IPermission) protectionPermission2);
      }
    }

    /// <summary>用 XML 编码重新构造具有指定状态的安全对象。</summary>
    /// <param name="et">用于重新构造安全对象的 XML 编码。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="et" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="et" /> 参数不是有效的权限元素。- 或 -<paramref name="et" /> 参数的版本号不受支持。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public virtual void FromXml(SecurityElement et)
    {
      this.FromXml(et, false, false);
    }

    internal static bool IsPermissionTag(string tag, bool allowInternalOnly)
    {
      return tag.Equals("Permission") || tag.Equals("IPermission") || allowInternalOnly && (tag.Equals("PermissionUnion") || tag.Equals("PermissionIntersection") || (tag.Equals("PermissionUnrestrictedIntersection") || tag.Equals("PermissionUnrestrictedUnion")));
    }

    internal virtual void FromXml(SecurityElement et, bool allowInternalOnly, bool ignoreTypeLoadFailures)
    {
      if (et == null)
        throw new ArgumentNullException("et");
      if (!et.Tag.Equals("PermissionSet"))
        throw new ArgumentException(string.Format((IFormatProvider) null, Environment.GetResourceString("Argument_InvalidXMLElement"), (object) "PermissionSet", (object) this.GetType().FullName));
      this.Reset();
      this.m_ignoreTypeLoadFailures = ignoreTypeLoadFailures;
      this.m_allPermissionsDecoded = false;
      this.m_Unrestricted = XMLUtil.IsUnrestricted(et);
      if (et.InternalChildren == null)
        return;
      int count = et.InternalChildren.Count;
      for (int index = 0; index < count; ++index)
      {
        SecurityElement securityElement = (SecurityElement) et.Children[index];
        if (PermissionSet.IsPermissionTag(securityElement.Tag, allowInternalOnly))
        {
          string typeStr = securityElement.Attribute("class");
          PermissionToken permissionToken;
          object obj;
          if (typeStr != null)
          {
            permissionToken = PermissionToken.GetToken(typeStr);
            if (permissionToken == null)
            {
              obj = (object) this.CreatePerm((object) securityElement);
              if (obj != null)
                permissionToken = PermissionToken.GetToken((IPermission) obj);
            }
            else
              obj = (object) securityElement;
          }
          else
          {
            IPermission perm = this.CreatePerm((object) securityElement);
            if (perm == null)
            {
              permissionToken = (PermissionToken) null;
              obj = (object) null;
            }
            else
            {
              permissionToken = PermissionToken.GetToken(perm);
              obj = (object) perm;
            }
          }
          if (permissionToken != null && obj != null)
          {
            if (this.m_permSet == null)
              this.m_permSet = new TokenBasedSet();
            if (this.m_permSet.GetItem(permissionToken.m_index) != null)
            {
              IPermission target = !(this.m_permSet.GetItem(permissionToken.m_index) is IPermission) ? this.CreatePerm((object) (SecurityElement) this.m_permSet.GetItem(permissionToken.m_index)) : (IPermission) this.m_permSet.GetItem(permissionToken.m_index);
              obj = !(obj is IPermission) ? (object) this.CreatePerm((object) (SecurityElement) obj).Union(target) : (object) ((IPermission) obj).Union(target);
            }
            if (this.m_Unrestricted && obj is IPermission)
              obj = (object) null;
            this.m_permSet.SetItem(permissionToken.m_index, obj);
          }
        }
      }
    }

    internal virtual void FromXml(SecurityDocument doc, int position, bool allowInternalOnly)
    {
      if (doc == null)
        throw new ArgumentNullException("doc");
      if (!doc.GetTagForElement(position).Equals("PermissionSet"))
        throw new ArgumentException(string.Format((IFormatProvider) null, Environment.GetResourceString("Argument_InvalidXMLElement"), (object) "PermissionSet", (object) this.GetType().FullName));
      this.Reset();
      this.m_allPermissionsDecoded = false;
      Exception exception = (Exception) null;
      string attributeForElement1 = doc.GetAttributeForElement(position, "Unrestricted");
      this.m_Unrestricted = attributeForElement1 != null && (attributeForElement1.Equals("True") || attributeForElement1.Equals("true") || attributeForElement1.Equals("TRUE"));
      ArrayList positionForElement = doc.GetChildrenPositionForElement(position);
      int count = positionForElement.Count;
      for (int index = 0; index < count; ++index)
      {
        int position1 = (int) positionForElement[index];
        if (PermissionSet.IsPermissionTag(doc.GetTagForElement(position1), allowInternalOnly))
        {
          try
          {
            string attributeForElement2 = doc.GetAttributeForElement(position1, "class");
            PermissionToken permissionToken;
            object obj;
            if (attributeForElement2 != null)
            {
              permissionToken = PermissionToken.GetToken(attributeForElement2);
              if (permissionToken == null)
              {
                obj = (object) this.CreatePerm((object) doc.GetElement(position1, true));
                if (obj != null)
                  permissionToken = PermissionToken.GetToken((IPermission) obj);
              }
              else
                obj = (object) ((ISecurityElementFactory) new SecurityDocumentElement(doc, position1)).CreateSecurityElement();
            }
            else
            {
              IPermission perm = this.CreatePerm((object) doc.GetElement(position1, true));
              if (perm == null)
              {
                permissionToken = (PermissionToken) null;
                obj = (object) null;
              }
              else
              {
                permissionToken = PermissionToken.GetToken(perm);
                obj = (object) perm;
              }
            }
            if (permissionToken != null)
            {
              if (obj != null)
              {
                if (this.m_permSet == null)
                  this.m_permSet = new TokenBasedSet();
                IPermission permission = (IPermission) null;
                if (this.m_permSet.GetItem(permissionToken.m_index) != null)
                  permission = !(this.m_permSet.GetItem(permissionToken.m_index) is IPermission) ? this.CreatePerm(this.m_permSet.GetItem(permissionToken.m_index)) : (IPermission) this.m_permSet.GetItem(permissionToken.m_index);
                if (permission != null)
                  obj = !(obj is IPermission) ? (object) permission.Union(this.CreatePerm(obj)) : (object) permission.Union((IPermission) obj);
                if (this.m_Unrestricted && obj is IPermission)
                  obj = (object) null;
                this.m_permSet.SetItem(permissionToken.m_index, obj);
              }
            }
          }
          catch (Exception ex)
          {
            if (exception == null)
              exception = ex;
          }
        }
      }
      if (exception != null)
        throw exception;
    }

    private IPermission CreatePerm(object obj)
    {
      return PermissionSet.CreatePerm(obj, this.m_ignoreTypeLoadFailures);
    }

    internal static IPermission CreatePerm(object obj, bool ignoreTypeLoadFailures)
    {
      SecurityElement securityElement = obj as SecurityElement;
      ISecurityElementFactory securityElementFactory = obj as ISecurityElementFactory;
      if (securityElement == null && securityElementFactory != null)
        securityElement = securityElementFactory.CreateSecurityElement();
      IPermission target = (IPermission) null;
      string tag = securityElement.Tag;
      if (!(tag == "PermissionUnion"))
      {
        if (!(tag == "PermissionIntersection"))
        {
          if (!(tag == "PermissionUnrestrictedUnion"))
          {
            if (!(tag == "PermissionUnrestrictedIntersection"))
            {
              if (tag == "IPermission" || tag == "Permission")
                target = securityElement.ToPermission(ignoreTypeLoadFailures);
            }
            else
            {
              foreach (SecurityElement child in securityElement.Children)
              {
                IPermission perm = PermissionSet.CreatePerm((object) child, ignoreTypeLoadFailures);
                if (perm == null)
                  return (IPermission) null;
                target = (PermissionToken.GetToken(perm).m_type & PermissionTokenType.IUnrestricted) == (PermissionTokenType) 0 ? (IPermission) null : (target == null ? perm : perm.Intersect(target));
                if (target == null)
                  return (IPermission) null;
              }
            }
          }
          else
          {
            IEnumerator enumerator = securityElement.Children.GetEnumerator();
            bool flag = true;
            while (enumerator.MoveNext())
            {
              IPermission perm = PermissionSet.CreatePerm((object) (SecurityElement) enumerator.Current, ignoreTypeLoadFailures);
              if (perm != null)
              {
                if ((PermissionToken.GetToken(perm).m_type & PermissionTokenType.IUnrestricted) != (PermissionTokenType) 0)
                {
                  target = XMLUtil.CreatePermission(PermissionSet.GetPermissionElement((SecurityElement) enumerator.Current), PermissionState.Unrestricted, ignoreTypeLoadFailures);
                  break;
                }
                target = !flag ? perm.Union(target) : perm;
                flag = false;
              }
            }
          }
        }
        else
        {
          foreach (SecurityElement child in securityElement.Children)
          {
            IPermission perm = PermissionSet.CreatePerm((object) child, ignoreTypeLoadFailures);
            target = target == null ? perm : target.Intersect(perm);
            if (target == null)
              return (IPermission) null;
          }
        }
      }
      else
      {
        foreach (SecurityElement child in securityElement.Children)
        {
          IPermission perm = PermissionSet.CreatePerm((object) child, ignoreTypeLoadFailures);
          target = target == null ? perm : target.Union(perm);
        }
      }
      return target;
    }

    internal IPermission CreatePermission(object obj, int index)
    {
      IPermission perm = this.CreatePerm(obj);
      if (perm == null)
        return (IPermission) null;
      if (this.m_Unrestricted)
        perm = (IPermission) null;
      this.CheckSet();
      this.m_permSet.SetItem(index, (object) perm);
      if (perm != null)
      {
        PermissionToken token = PermissionToken.GetToken(perm);
        if (token != null && token.m_index != index)
          throw new ArgumentException(Environment.GetResourceString("Argument_UnableToGeneratePermissionSet"));
      }
      return perm;
    }

    private static SecurityElement GetPermissionElement(SecurityElement el)
    {
      string tag = el.Tag;
      if (tag == "IPermission" || tag == "Permission")
        return el;
      IEnumerator enumerator = el.Children.GetEnumerator();
      if (enumerator.MoveNext())
        return PermissionSet.GetPermissionElement((SecurityElement) enumerator.Current);
      return (SecurityElement) null;
    }

    internal static SecurityElement CreateEmptyPermissionSetXml()
    {
      SecurityElement securityElement = new SecurityElement("PermissionSet");
      string name1 = "class";
      string str1 = "System.Security.PermissionSet";
      securityElement.AddAttribute(name1, str1);
      string name2 = "version";
      string str2 = "1";
      securityElement.AddAttribute(name2, str2);
      return securityElement;
    }

    internal SecurityElement ToXml(string permName)
    {
      SecurityElement securityElement = new SecurityElement("PermissionSet");
      securityElement.AddAttribute("class", permName);
      securityElement.AddAttribute("version", "1");
      PermissionSetEnumeratorInternal enumeratorInternal = new PermissionSetEnumeratorInternal(this);
      if (this.m_Unrestricted)
        securityElement.AddAttribute("Unrestricted", "true");
      while (enumeratorInternal.MoveNext())
      {
        IPermission permission = (IPermission) enumeratorInternal.Current;
        if (!this.m_Unrestricted)
          securityElement.AddChild(permission.ToXml());
      }
      return securityElement;
    }

    internal SecurityElement InternalToXml()
    {
      SecurityElement securityElement = new SecurityElement("PermissionSet");
      securityElement.AddAttribute("class", this.GetType().FullName);
      securityElement.AddAttribute("version", "1");
      if (this.m_Unrestricted)
        securityElement.AddAttribute("Unrestricted", "true");
      if (this.m_permSet != null)
      {
        int maxUsedIndex = this.m_permSet.GetMaxUsedIndex();
        for (int startingIndex = this.m_permSet.GetStartingIndex(); startingIndex <= maxUsedIndex; ++startingIndex)
        {
          object obj = this.m_permSet.GetItem(startingIndex);
          if (obj != null)
          {
            if (obj is IPermission)
            {
              if (!this.m_Unrestricted)
                securityElement.AddChild(((ISecurityEncodable) obj).ToXml());
            }
            else
              securityElement.AddChild((SecurityElement) obj);
          }
        }
      }
      return securityElement;
    }

    /// <summary>创建安全对象及其当前状态的 XML 编码。</summary>
    /// <returns>安全对象的 XML 编码，包括任何状态信息。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public virtual SecurityElement ToXml()
    {
      return this.ToXml("System.Security.PermissionSet");
    }

    internal byte[] EncodeXml()
    {
      MemoryStream memoryStream = new MemoryStream();
      Encoding unicode = Encoding.Unicode;
      BinaryWriter binaryWriter = new BinaryWriter((Stream) memoryStream, unicode);
      string @string = this.ToXml().ToString();
      binaryWriter.Write(@string);
      binaryWriter.Flush();
      long num = 2;
      memoryStream.Position = num;
      byte[] numArray = new byte[(int) memoryStream.Length - 2];
      byte[] buffer = numArray;
      int offset = 0;
      int length = numArray.Length;
      memoryStream.Read(buffer, offset, length);
      return numArray;
    }

    /// <summary>将经过编码的 <see cref="T:System.Security.PermissionSet" /> 从一种 XML 编码格式转换为另一种 XML 编码格式。</summary>
    /// <returns>一个具有指定输出格式的加密权限集。</returns>
    /// <param name="inFormat">一个字符串，表示以下编码格式之一：ASCII、Unicode 或 Binary。可能的值为“XMLASCII”或“XML”、“XMLUNICODE”和“BINARY”。</param>
    /// <param name="inData">一个 XML 编码的权限集。</param>
    /// <param name="outFormat">一个字符串，表示以下编码格式之一：ASCII、Unicode 或 Binary。可能的值为“XMLASCII”或“XML”、“XMLUNICODE”和“BINARY”。</param>
    /// <exception cref="T:System.NotImplementedException">在所有情况下。</exception>
    [Obsolete("This method is obsolete and shoud no longer be used.")]
    public static byte[] ConvertPermissionSet(string inFormat, byte[] inData, string outFormat)
    {
      throw new NotImplementedException();
    }

    /// <summary>获取一个值，该值指示 <see cref="T:System.Security.PermissionSet" /> 是否包含不是从 <see cref="T:System.Security.CodeAccessPermission" /> 派生的权限。</summary>
    /// <returns>如果 <see cref="T:System.Security.PermissionSet" /> 包含不是从 <see cref="T:System.Security.CodeAccessPermission" /> 派生的权限，则为 true；否则为 false。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public bool ContainsNonCodeAccessPermissions()
    {
      if (this.m_CheckedForNonCas)
        return this.m_ContainsNonCas;
      lock (this)
      {
        if (this.m_CheckedForNonCas)
          return this.m_ContainsNonCas;
        this.m_ContainsCas = false;
        this.m_ContainsNonCas = false;
        if (this.IsUnrestricted())
          this.m_ContainsCas = true;
        if (this.m_permSet != null)
        {
          PermissionSetEnumeratorInternal local_3 = new PermissionSetEnumeratorInternal(this);
          while (local_3.MoveNext() && (!this.m_ContainsCas || !this.m_ContainsNonCas))
          {
            IPermission local_4 = local_3.Current as IPermission;
            if (local_4 != null)
            {
              if (local_4 is CodeAccessPermission)
                this.m_ContainsCas = true;
              else
                this.m_ContainsNonCas = true;
            }
          }
        }
        this.m_CheckedForNonCas = true;
      }
      return this.m_ContainsNonCas;
    }

    private PermissionSet GetCasOnlySet()
    {
      if (!this.m_ContainsNonCas || this.IsUnrestricted())
        return this;
      PermissionSet permissionSet1 = new PermissionSet(false);
      PermissionSetEnumeratorInternal enumeratorInternal = new PermissionSetEnumeratorInternal(this);
      while (enumeratorInternal.MoveNext())
      {
        IPermission perm = (IPermission) enumeratorInternal.Current;
        if (perm is CodeAccessPermission)
          permissionSet1.AddPermission(perm);
      }
      permissionSet1.m_CheckedForNonCas = true;
      PermissionSet permissionSet2 = permissionSet1;
      int num = !permissionSet2.IsEmpty() ? 1 : 0;
      permissionSet2.m_ContainsCas = num != 0;
      permissionSet1.m_ContainsNonCas = false;
      return permissionSet1;
    }

    [SecurityCritical]
    private static void SetupSecurity()
    {
      PolicyLevel appDomainLevel = PolicyLevel.CreateAppDomainLevel();
      CodeGroup codeGroup = (CodeGroup) new UnionCodeGroup((IMembershipCondition) new AllMembershipCondition(), (PermissionSet) appDomainLevel.GetNamedPermissionSet("Execution"));
      CodeGroup group1 = (CodeGroup) new UnionCodeGroup((IMembershipCondition) new StrongNameMembershipCondition(new StrongNamePublicKeyBlob("002400000480000094000000060200000024000052534131000400000100010007D1FA57C4AED9F0A32E84AA0FAEFD0DE9E8FD6AEC8F87FB03766C834C99921EB23BE79AD9D5DCC1DD9AD236132102900B723CF980957FC4E177108FC607774F29E8320E92EA05ECE4E821C0A5EFE8F1645C4C0C93C1AB99285D622CAA652C1DFAD63D745D6F2DE5F17E5EAF0FC4963D261C8A12436518206DC093344D5AD293"), (string) null, (Version) null), (PermissionSet) appDomainLevel.GetNamedPermissionSet("FullTrust"));
      CodeGroup group2 = (CodeGroup) new UnionCodeGroup((IMembershipCondition) new StrongNameMembershipCondition(new StrongNamePublicKeyBlob("00000000000000000400000000000000"), (string) null, (Version) null), (PermissionSet) appDomainLevel.GetNamedPermissionSet("FullTrust"));
      CodeGroup group3 = (CodeGroup) new UnionCodeGroup((IMembershipCondition) new GacMembershipCondition(), (PermissionSet) appDomainLevel.GetNamedPermissionSet("FullTrust"));
      codeGroup.AddChild(group1);
      codeGroup.AddChild(group2);
      codeGroup.AddChild(group3);
      appDomainLevel.RootCodeGroup = codeGroup;
      try
      {
        AppDomain.CurrentDomain.SetAppDomainPolicy(appDomainLevel);
      }
      catch (PolicyException ex)
      {
      }
    }

    private static void MergePermission(IPermission perm, bool separateCasFromNonCas, ref PermissionSet casPset, ref PermissionSet nonCasPset)
    {
      if (perm == null)
        return;
      if (!separateCasFromNonCas || perm is CodeAccessPermission)
      {
        if (casPset == null)
          casPset = new PermissionSet(false);
        IPermission permission = casPset.GetPermission(perm);
        IPermission target = casPset.AddPermission(perm);
        if (permission != null && !permission.IsSubsetOf(target))
          throw new NotSupportedException(Environment.GetResourceString("NotSupported_DeclarativeUnion"));
      }
      else
      {
        if (nonCasPset == null)
          nonCasPset = new PermissionSet(false);
        IPermission permission = nonCasPset.GetPermission(perm);
        IPermission target = nonCasPset.AddPermission(perm);
        if (permission != null && !permission.IsSubsetOf(target))
          throw new NotSupportedException(Environment.GetResourceString("NotSupported_DeclarativeUnion"));
      }
    }

    private static byte[] CreateSerialized(object[] attrs, bool serialize, ref byte[] nonCasBlob, out PermissionSet casPset, HostProtectionResource fullTrustOnlyResources, bool allowEmptyPermissionSets)
    {
      casPset = (PermissionSet) null;
      PermissionSet nonCasPset = (PermissionSet) null;
      for (int index = 0; index < attrs.Length; ++index)
      {
        if (attrs[index] is PermissionSetAttribute)
        {
          PermissionSet permissionSet = ((PermissionSetAttribute) attrs[index]).CreatePermissionSet();
          if (permissionSet == null)
            throw new ArgumentException(Environment.GetResourceString("Argument_UnableToGeneratePermissionSet"));
          PermissionSetEnumeratorInternal enumeratorInternal = new PermissionSetEnumeratorInternal(permissionSet);
          while (enumeratorInternal.MoveNext())
            PermissionSet.MergePermission((IPermission) enumeratorInternal.Current, serialize, ref casPset, ref nonCasPset);
          if (casPset == null)
            casPset = new PermissionSet(false);
          if (permissionSet.IsUnrestricted())
            casPset.SetUnrestricted(true);
        }
        else
          PermissionSet.MergePermission(((SecurityAttribute) attrs[index]).CreatePermission(), serialize, ref casPset, ref nonCasPset);
      }
      if (casPset != null)
      {
        casPset.FilterHostProtectionPermissions(fullTrustOnlyResources, HostProtectionResource.None);
        casPset.ContainsNonCodeAccessPermissions();
        if (allowEmptyPermissionSets && casPset.IsEmpty())
          casPset = (PermissionSet) null;
      }
      if (nonCasPset != null)
      {
        nonCasPset.FilterHostProtectionPermissions(fullTrustOnlyResources, HostProtectionResource.None);
        nonCasPset.ContainsNonCodeAccessPermissions();
        if (allowEmptyPermissionSets && nonCasPset.IsEmpty())
          nonCasPset = (PermissionSet) null;
      }
      byte[] numArray = (byte[]) null;
      nonCasBlob = (byte[]) null;
      if (serialize)
      {
        if (casPset != null)
          numArray = casPset.EncodeXml();
        if (nonCasPset != null)
          nonCasBlob = nonCasPset.EncodeXml();
      }
      return numArray;
    }

    void IDeserializationCallback.OnDeserialization(object sender)
    {
      this.NormalizePermissionSet();
      this.m_CheckedForNonCas = false;
    }

    /// <summary>导致当前框架先前的所有 <see cref="M:System.Security.CodeAccessPermission.Assert" /> 都被移除，不再有效。</summary>
    /// <exception cref="T:System.InvalidOperationException">当前框架没有前一个 <see cref="M:System.Security.CodeAccessPermission.Assert" />。</exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void RevertAssert()
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      SecurityRuntime.RevertAssert(ref stackMark);
    }

    internal static PermissionSet RemoveRefusedPermissionSet(PermissionSet assertSet, PermissionSet refusedSet, out bool bFailedToCompress)
    {
      PermissionSet permissionSet = (PermissionSet) null;
      bFailedToCompress = false;
      if (assertSet == null)
        return (PermissionSet) null;
      if (refusedSet != null)
      {
        if (refusedSet.IsUnrestricted())
          return (PermissionSet) null;
        PermissionSetEnumeratorInternal enumeratorInternal = new PermissionSetEnumeratorInternal(refusedSet);
        while (enumeratorInternal.MoveNext())
        {
          CodeAccessPermission accessPermission1 = (CodeAccessPermission) enumeratorInternal.Current;
          int currentIndex = enumeratorInternal.GetCurrentIndex();
          if (accessPermission1 != null)
          {
            CodeAccessPermission accessPermission2 = (CodeAccessPermission) assertSet.GetPermission(currentIndex);
            try
            {
              if (accessPermission1.Intersect((IPermission) accessPermission2) != null)
              {
                if (accessPermission1.Equals((object) accessPermission2))
                {
                  if (permissionSet == null)
                    permissionSet = assertSet.Copy();
                  permissionSet.RemovePermission(currentIndex);
                }
                else
                {
                  bFailedToCompress = true;
                  return assertSet;
                }
              }
            }
            catch (ArgumentException ex)
            {
              if (permissionSet == null)
                permissionSet = assertSet.Copy();
              permissionSet.RemovePermission(currentIndex);
            }
          }
        }
      }
      return permissionSet ?? assertSet;
    }

    internal static void RemoveAssertedPermissionSet(PermissionSet demandSet, PermissionSet assertSet, out PermissionSet alteredDemandSet)
    {
      alteredDemandSet = (PermissionSet) null;
      PermissionSetEnumeratorInternal enumeratorInternal = new PermissionSetEnumeratorInternal(demandSet);
      while (enumeratorInternal.MoveNext())
      {
        CodeAccessPermission accessPermission = (CodeAccessPermission) enumeratorInternal.Current;
        int currentIndex = enumeratorInternal.GetCurrentIndex();
        if (accessPermission != null)
        {
          CodeAccessPermission asserted = (CodeAccessPermission) assertSet.GetPermission(currentIndex);
          try
          {
            if (accessPermission.CheckAssert(asserted))
            {
              if (alteredDemandSet == null)
                alteredDemandSet = demandSet.Copy();
              alteredDemandSet.RemovePermission(currentIndex);
            }
          }
          catch (ArgumentException ex)
          {
          }
        }
      }
    }

    internal static bool IsIntersectingAssertedPermissions(PermissionSet assertSet1, PermissionSet assertSet2)
    {
      bool flag = false;
      if (assertSet1 != null && assertSet2 != null)
      {
        PermissionSetEnumeratorInternal enumeratorInternal = new PermissionSetEnumeratorInternal(assertSet2);
        while (enumeratorInternal.MoveNext())
        {
          CodeAccessPermission accessPermission1 = (CodeAccessPermission) enumeratorInternal.Current;
          int currentIndex = enumeratorInternal.GetCurrentIndex();
          if (accessPermission1 != null)
          {
            CodeAccessPermission accessPermission2 = (CodeAccessPermission) assertSet1.GetPermission(currentIndex);
            try
            {
              if (accessPermission2 != null)
              {
                if (!accessPermission2.Equals((object) accessPermission1))
                  flag = true;
              }
            }
            catch (ArgumentException ex)
            {
              flag = true;
            }
          }
        }
      }
      return flag;
    }

    internal enum IsSubsetOfType
    {
      Normal,
      CheckDemand,
      CheckPermitOnly,
      CheckAssertion,
    }
  }
}
