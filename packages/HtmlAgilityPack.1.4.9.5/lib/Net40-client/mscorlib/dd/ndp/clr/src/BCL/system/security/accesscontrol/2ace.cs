// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.CustomAce
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;

namespace System.Security.AccessControl
{
  /// <summary>表示未由 <see cref="T:System.Security.AccessControl.AceType" /> 枚举的成员之一定义的访问控制项 (ACE)。</summary>
  public sealed class CustomAce : GenericAce
  {
    /// <summary>返回此 <see cref="T:System.Security.AccessControl.CustomAce" /> 对象的不透明数据 Blob 的最大允许长度。</summary>
    public static readonly int MaxOpaqueLength = 65531;
    private byte[] _opaque;

    /// <summary>获取与此 <see cref="T:System.Security.AccessControl.CustomAce" /> 对象关联的不透明数据的长度。</summary>
    /// <returns>不透明回调数据的长度。</returns>
    public int OpaqueLength
    {
      get
      {
        if (this._opaque == null)
          return 0;
        return this._opaque.Length;
      }
    }

    /// <summary>获取当前 <see cref="T:System.Security.AccessControl.CustomAce" /> 对象的二进制表示形式的长度（以字节为单位）。在使用 <see cref="M:System.Security.AccessControl.CustomAce.GetBinaryForm" /> 方法将 ACL 封送到二进制数组中之前，应使用该长度。</summary>
    /// <returns>当前 <see cref="T:System.Security.AccessControl.CustomAce" /> 对象的二进制表示形式的长度（以字节为单位）。</returns>
    public override int BinaryLength
    {
      get
      {
        return 4 + this.OpaqueLength;
      }
    }

    /// <summary>初始化 <see cref="T:System.Security.AccessControl.CustomAce" /> 类的新实例。</summary>
    /// <param name="type">新的访问控制项 (ACE) 的类型。该值必须大于 <see cref="F:System.Security.AccessControl.AceType.MaxDefinedAceType" />。</param>
    /// <param name="flags">为新的 ACE 指定有关继承、继承传播和审核条件的信息的标志。</param>
    /// <param name="opaque">一个包含新的 ACE 的数据的字节值数组。此值可为 null。此数组的长度一定不能大于 <see cref="F:System.Security.AccessControl.CustomAce.MaxOpaqueLength" /> 字段的值，并且必须是四的倍数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="type" /> 参数的值不大于 <see cref="F:System.Security.AccessControl.AceType.MaxDefinedAceType" />，或者 <paramref name="opaque" /> 数组的长度大于 <see cref="F:System.Security.AccessControl.CustomAce.MaxOpaqueLength" /> 字段的值或不是四的倍数。</exception>
    public CustomAce(AceType type, AceFlags flags, byte[] opaque)
      : base(type, flags)
    {
      if (type <= AceType.SystemAlarmCallbackObject)
        throw new ArgumentOutOfRangeException("type", Environment.GetResourceString("ArgumentOutOfRange_InvalidUserDefinedAceType"));
      this.SetOpaque(opaque);
    }

    /// <summary>返回与此 <see cref="T:System.Security.AccessControl.CustomAce" /> 对象关联的不透明数据。</summary>
    /// <returns>一个字节值数组，表示与此 <see cref="T:System.Security.AccessControl.CustomAce" /> 对象关联的不透明数据。</returns>
    public byte[] GetOpaque()
    {
      return this._opaque;
    }

    /// <summary>设置与此 <see cref="T:System.Security.AccessControl.CustomAce" /> 对象关联的不透明回调数据。</summary>
    /// <param name="opaque">一个字节值数组，表示此 <see cref="T:System.Security.AccessControl.CustomAce" /> 对象的不透明回调数据。</param>
    public void SetOpaque(byte[] opaque)
    {
      if (opaque != null)
      {
        if (opaque.Length > CustomAce.MaxOpaqueLength)
          throw new ArgumentOutOfRangeException("opaque", string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_ArrayLength"), (object) 0, (object) CustomAce.MaxOpaqueLength));
        if (opaque.Length % 4 != 0)
          throw new ArgumentOutOfRangeException("opaque", string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_ArrayLengthMultiple"), (object) 4));
      }
      this._opaque = opaque;
    }

    /// <summary>将 <see cref="T:System.Security.AccessControl.CustomAce" /> 对象的内容封送到指定字节数组中，其位置从指定的偏移量开始。</summary>
    /// <param name="binaryForm">将 <see cref="T:System.Security.AccessControl.CustomAce" /> 的内容封送到的字节数组。</param>
    /// <param name="offset">开始封送的偏移量。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="offset" /> 如果为负数或过高，则会将整个 <see cref="T:System.Security.AccessControl.CustomAce" /> 复制到 <paramref name="array" />。</exception>
    public override void GetBinaryForm(byte[] binaryForm, int offset)
    {
      this.MarshalHeader(binaryForm, offset);
      offset += 4;
      if (this.OpaqueLength == 0)
        return;
      if (this.OpaqueLength > CustomAce.MaxOpaqueLength)
        throw new SystemException();
      this.GetOpaque().CopyTo((Array) binaryForm, offset);
    }
  }
}
