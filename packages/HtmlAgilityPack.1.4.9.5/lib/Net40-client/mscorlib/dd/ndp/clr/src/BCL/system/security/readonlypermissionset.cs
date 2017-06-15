// Decompiled with JetBrains decompiler
// Type: System.Security.ReadOnlyPermissionSet
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Runtime.Serialization;

namespace System.Security
{
  /// <summary>表示可以包含许多不同类型权限的只读集合。</summary>
  [Serializable]
  public sealed class ReadOnlyPermissionSet : PermissionSet
  {
    private SecurityElement m_originXml;
    [NonSerialized]
    private bool m_deserializing;

    /// <summary>获取一个值，该值指示该集合是否为只读集合。</summary>
    /// <returns>始终为 true。</returns>
    public override bool IsReadOnly
    {
      get
      {
        return true;
      }
    }

    /// <summary>初始化 <see cref="T:System.Security.ReadOnlyPermissionSet" /> 类的新实例。</summary>
    /// <param name="permissionSetXml">要从中提取新 <see cref="T:System.Security.ReadOnlyPermissionSet" /> 的值的 XML 元素。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="permissionSetXml" /> 为 null。</exception>
    public ReadOnlyPermissionSet(SecurityElement permissionSetXml)
    {
      if (permissionSetXml == null)
        throw new ArgumentNullException("permissionSetXml");
      this.m_originXml = permissionSetXml.Copy();
      base.FromXml(this.m_originXml);
    }

    [OnDeserializing]
    private void OnDeserializing(StreamingContext ctx)
    {
      this.m_deserializing = true;
    }

    [OnDeserialized]
    private void OnDeserialized(StreamingContext ctx)
    {
      this.m_deserializing = false;
    }

    /// <summary>创建 <see cref="T:System.Security.ReadOnlyPermissionSet" /> 的副本。</summary>
    /// <returns>只读权限集的副本。</returns>
    public override PermissionSet Copy()
    {
      return (PermissionSet) new ReadOnlyPermissionSet(this.m_originXml);
    }

    /// <summary>创建安全对象及其当前状态的 XML 编码。</summary>
    /// <returns>安全对象的 XML 编码，包括任何状态信息。</returns>
    public override SecurityElement ToXml()
    {
      return this.m_originXml.Copy();
    }

    protected override IEnumerator GetEnumeratorImpl()
    {
      return (IEnumerator) new ReadOnlyPermissionSetEnumerator(base.GetEnumeratorImpl());
    }

    protected override IPermission GetPermissionImpl(Type permClass)
    {
      IPermission permissionImpl = base.GetPermissionImpl(permClass);
      if (permissionImpl == null)
        return (IPermission) null;
      return permissionImpl.Copy();
    }

    protected override IPermission AddPermissionImpl(IPermission perm)
    {
      throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ModifyROPermSet"));
    }

    /// <summary>用 XML 编码重新构造具有指定状态的安全对象。</summary>
    /// <param name="et">用于重新构造安全对象的 XML 编码。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="et" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="et" /> 参数不是有效的权限元素。- 或 -<paramref name="et" /> 参数的版本号不受支持。</exception>
    /// <exception cref="T:System.InvalidOperationException">未反序列化该对象；即在反序列化过程中，<see cref="T:System.Security.PermissionSet" /> 不会回调到 <see cref="M:System.Security.ReadOnlyPermissionSet.FromXml(System.Security.SecurityElement)" /> 中。</exception>
    public override void FromXml(SecurityElement et)
    {
      if (!this.m_deserializing)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ModifyROPermSet"));
      base.FromXml(et);
    }

    protected override IPermission RemovePermissionImpl(Type permClass)
    {
      throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ModifyROPermSet"));
    }

    protected override IPermission SetPermissionImpl(IPermission perm)
    {
      throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ModifyROPermSet"));
    }
  }
}
