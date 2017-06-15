// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.Formatters.IFieldInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Serialization.Formatters
{
  /// <summary>允许访问支持 <see cref="T:System.Runtime.Serialization.ISerializable" /> 接口的对象的字段名称和字段类型。</summary>
  [ComVisible(true)]
  public interface IFieldInfo
  {
    /// <summary>获取或设置序列化对象的字段名称。</summary>
    /// <returns>序列化对象的字段名称。</returns>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter" />
    /// </PermissionSet>
    string[] FieldNames { [SecurityCritical] get; [SecurityCritical] set; }

    /// <summary>获取或设置序列化对象的字段类型。</summary>
    /// <returns>序列化对象的字段类型。</returns>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter" />
    /// </PermissionSet>
    Type[] FieldTypes { [SecurityCritical] get; [SecurityCritical] set; }
  }
}
