// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.PrivilegeNotHeldException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.Serialization;

namespace System.Security.AccessControl
{
  /// <summary>当 <see cref="N:System.Security.AccessControl" /> 命名空间中的方法尝试启用它所不具备的特权时引发的异常。</summary>
  [Serializable]
  public sealed class PrivilegeNotHeldException : UnauthorizedAccessException, ISerializable
  {
    private readonly string _privilegeName;

    /// <summary>获取未启用的特权的名称。</summary>
    /// <returns>此方法未能启用的特权的名称。</returns>
    public string PrivilegeName
    {
      get
      {
        return this._privilegeName;
      }
    }

    /// <summary>初始化 <see cref="T:System.Security.AccessControl.PrivilegeNotHeldException" /> 类的新实例。</summary>
    public PrivilegeNotHeldException()
      : base(Environment.GetResourceString("PrivilegeNotHeld_Default"))
    {
    }

    /// <summary>使用指定的特权初始化 <see cref="T:System.Security.AccessControl.PrivilegeNotHeldException" /> 类的新实例。</summary>
    /// <param name="privilege">未启用的特权。</param>
    public PrivilegeNotHeldException(string privilege)
      : base(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("PrivilegeNotHeld_Named"), (object) privilege))
    {
      this._privilegeName = privilege;
    }

    /// <summary>使用指定的异常初始化 <see cref="T:System.Security.AccessControl.PrivilegeNotHeldException" /> 类的新实例。</summary>
    /// <param name="privilege">未启用的特权。</param>
    /// <param name="inner">导致当前异常的异常。如果 <paramref name="innerException" /> 参数不是空引用（在 Visual Basic 中为 Nothing），则在处理内部异常的 catch 块中引发当前异常。</param>
    public PrivilegeNotHeldException(string privilege, Exception inner)
      : base(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("PrivilegeNotHeld_Named"), (object) privilege), inner)
    {
      this._privilegeName = privilege;
    }

    internal PrivilegeNotHeldException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
      this._privilegeName = info.GetString("PrivilegeName");
    }

    /// <summary>使用有关异常的信息设置 <paramref name="info" /> 参数。</summary>
    /// <param name="info">
    /// <see cref="T:System.Runtime.Serialization.SerializationInfo" />，它存有有关所引发的异常的序列化对象数据。</param>
    /// <param name="context">
    /// <see cref="T:System.Runtime.Serialization.StreamingContext" />，它包含有关源或目标的上下文信息。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
    /// </PermissionSet>
    [SecurityCritical]
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException("info");
      base.GetObjectData(info, context);
      info.AddValue("PrivilegeName", (object) this._privilegeName, typeof (string));
    }
  }
}
