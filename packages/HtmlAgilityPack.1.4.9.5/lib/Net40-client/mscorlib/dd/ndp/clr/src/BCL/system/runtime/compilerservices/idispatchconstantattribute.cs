// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.IDispatchConstantAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
  /// <summary>指示特性化字段或参数的默认值是 <see cref="T:System.Runtime.InteropServices.DispatchWrapper" /> 的实例，其中 <see cref="P:System.Runtime.InteropServices.DispatchWrapper.WrappedObject" /> 为 null。</summary>
  [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter, Inherited = false)]
  [ComVisible(true)]
  [Serializable]
  public sealed class IDispatchConstantAttribute : CustomConstantAttribute
  {
    /// <summary>获取存储在此特性中的 IDispatch 常数。</summary>
    /// <returns>存储在此特性中的 IDispatch 常数。只有 null 可以作为 IDispatch 常数的值。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public override object Value
    {
      get
      {
        return (object) new DispatchWrapper((object) null);
      }
    }
  }
}
