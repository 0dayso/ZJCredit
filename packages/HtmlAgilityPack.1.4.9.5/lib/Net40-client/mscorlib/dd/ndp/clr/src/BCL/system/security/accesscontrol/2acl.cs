// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.RawAcl
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;

namespace System.Security.AccessControl
{
  /// <summary>表示访问控制列表 (ACL)。</summary>
  public sealed class RawAcl : GenericAcl
  {
    private byte _revision;
    private ArrayList _aces;

    /// <summary>获取 <see cref="T:System.Security.AccessControl.RawAcl" /> 的修订级别。</summary>
    /// <returns>一个指定 <see cref="T:System.Security.AccessControl.RawAcl" /> 的修订级别的字节值。</returns>
    public override byte Revision
    {
      get
      {
        return this._revision;
      }
    }

    /// <summary>获取当前 <see cref="T:System.Security.AccessControl.RawAcl" /> 对象中访问控制项 (ACE) 的数量。</summary>
    /// <returns>当前 <see cref="T:System.Security.AccessControl.RawAcl" /> 对象中 ACE 的数量。</returns>
    public override int Count
    {
      get
      {
        return this._aces.Count;
      }
    }

    /// <summary>获取当前 <see cref="T:System.Security.AccessControl.RawAcl" /> 对象的二进制表示形式的长度（以字节为单位）。在使用 <see cref="M:System.Security.AccessControl.RawAcl.GetBinaryForm" /> 方法将 ACL 封送到二进制数组中之前，应使用该长度。</summary>
    /// <returns>当前 <see cref="T:System.Security.AccessControl.RawAcl" /> 对象的二进制表示形式的长度（以字节为单位）。</returns>
    public override int BinaryLength
    {
      get
      {
        int num = 8;
        for (int index = 0; index < this.Count; ++index)
        {
          GenericAce genericAce = this._aces[index] as GenericAce;
          num += genericAce.BinaryLength;
        }
        return num;
      }
    }

    /// <summary>获取或设置指定索引处的访问控制项 (ACE)。</summary>
    /// <returns>指定索引处的 ACE。</returns>
    /// <param name="index">要获取或设置的 ACE 的从零开始的索引。</param>
    public override GenericAce this[int index]
    {
      get
      {
        return this._aces[index] as GenericAce;
      }
      set
      {
        if (value == (GenericAce) null)
          throw new ArgumentNullException("value");
        if (value.BinaryLength % 4 != 0)
          throw new SystemException();
        if (this.BinaryLength - (index < this._aces.Count ? (this._aces[index] as GenericAce).BinaryLength : 0) + value.BinaryLength > GenericAcl.MaxBinaryLength)
          throw new OverflowException(Environment.GetResourceString("AccessControl_AclTooLong"));
        this._aces[index] = (object) value;
      }
    }

    /// <summary>使用指定的修订级别初始化 <see cref="T:System.Security.AccessControl.RawAcl" /> 类的新实例。</summary>
    /// <param name="revision">新的访问控制列表 (ACL) 的修订级别。</param>
    /// <param name="capacity">此 <see cref="T:System.Security.AccessControl.RawAcl" /> 对象可包含的访问控制项 (ACE) 的数量。此数量只作为一种提示。</param>
    public RawAcl(byte revision, int capacity)
    {
      this._revision = revision;
      this._aces = new ArrayList(capacity);
    }

    /// <summary>使用指定的二进制格式初始化 <see cref="T:System.Security.AccessControl.RawAcl" /> 类的新实例。</summary>
    /// <param name="binaryForm">表示访问控制列表 (ACL) 的字节值数组。</param>
    /// <param name="offset">
    /// <paramref name="binaryForm" /> 参数中第一个要取消封送的数据的偏移量。</param>
    public RawAcl(byte[] binaryForm, int offset)
    {
      this.SetBinaryForm(binaryForm, offset);
    }

    private static void VerifyHeader(byte[] binaryForm, int offset, out byte revision, out int count, out int length)
    {
      if (binaryForm == null)
        throw new ArgumentNullException("binaryForm");
      if (offset < 0)
        throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (binaryForm.Length - offset >= 8)
      {
        revision = binaryForm[offset + 0];
        length = (int) binaryForm[offset + 2] + ((int) binaryForm[offset + 3] << 8);
        count = (int) binaryForm[offset + 4] + ((int) binaryForm[offset + 5] << 8);
        if (length <= binaryForm.Length - offset)
          return;
      }
      throw new ArgumentOutOfRangeException("binaryForm", Environment.GetResourceString("ArgumentOutOfRange_ArrayTooSmall"));
    }

    private void MarshalHeader(byte[] binaryForm, int offset)
    {
      if (binaryForm == null)
        throw new ArgumentNullException("binaryForm");
      if (offset < 0)
        throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (this.BinaryLength > GenericAcl.MaxBinaryLength)
        throw new InvalidOperationException(Environment.GetResourceString("AccessControl_AclTooLong"));
      if (binaryForm.Length - offset < this.BinaryLength)
        throw new ArgumentOutOfRangeException("binaryForm", Environment.GetResourceString("ArgumentOutOfRange_ArrayTooSmall"));
      binaryForm[offset + 0] = this.Revision;
      binaryForm[offset + 1] = (byte) 0;
      binaryForm[offset + 2] = (byte) this.BinaryLength;
      binaryForm[offset + 3] = (byte) (this.BinaryLength >> 8);
      binaryForm[offset + 4] = (byte) this.Count;
      binaryForm[offset + 5] = (byte) (this.Count >> 8);
      binaryForm[offset + 6] = (byte) 0;
      binaryForm[offset + 7] = (byte) 0;
    }

    internal void SetBinaryForm(byte[] binaryForm, int offset)
    {
      int count;
      int length;
      RawAcl.VerifyHeader(binaryForm, offset, out this._revision, out count, out length);
      int num1 = length + offset;
      offset += 8;
      this._aces = new ArrayList(count);
      int num2 = 8;
      for (int index = 0; index < count; ++index)
      {
        GenericAce fromBinaryForm = GenericAce.CreateFromBinaryForm(binaryForm, offset);
        int binaryLength = fromBinaryForm.BinaryLength;
        if (num2 + binaryLength > GenericAcl.MaxBinaryLength)
          throw new ArgumentException(Environment.GetResourceString("ArgumentException_InvalidAclBinaryForm"), "binaryForm");
        this._aces.Add((object) fromBinaryForm);
        if (binaryLength % 4 != 0)
          throw new SystemException();
        num2 += binaryLength;
        if ((int) this._revision == (int) GenericAcl.AclRevisionDS)
          offset += (int) binaryForm[offset + 2] + ((int) binaryForm[offset + 3] << 8);
        else
          offset += binaryLength;
        if (offset > num1)
          throw new ArgumentException(Environment.GetResourceString("ArgumentException_InvalidAclBinaryForm"), "binaryForm");
      }
    }

    /// <summary>将 <see cref="T:System.Security.AccessControl.RawAcl" /> 对象的内容封送到指定字节数组中，其位置从指定的偏移量开始。</summary>
    /// <param name="binaryForm">将 <see cref="T:System.Security.AccessControl.RawAcl" /> 的内容封送到的字节数组。</param>
    /// <param name="offset">开始封送的偏移量。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="offset" /> 如果为负数或过高，则会将整个 <see cref="T:System.Security.AccessControl.RawAcl" /> 复制到 <paramref name="array" />。</exception>
    public override void GetBinaryForm(byte[] binaryForm, int offset)
    {
      this.MarshalHeader(binaryForm, offset);
      offset += 8;
      for (int index = 0; index < this.Count; ++index)
      {
        GenericAce genericAce = this._aces[index] as GenericAce;
        byte[] binaryForm1 = binaryForm;
        int offset1 = offset;
        genericAce.GetBinaryForm(binaryForm1, offset1);
        int binaryLength = genericAce.BinaryLength;
        if (binaryLength % 4 != 0)
          throw new SystemException();
        offset += binaryLength;
      }
    }

    /// <summary>在指定的索引处插入指定的访问控制项 (ACE)。</summary>
    /// <param name="index">要添加新 ACE 的位置。指定 <see cref="P:System.Security.AccessControl.RawAcl.Count" /> 属性的值，以便在 <see cref="T:System.Security.AccessControl.RawAcl" /> 对象的末尾插入一个 ACE。</param>
    /// <param name="ace">要插入的 ACE。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="offset" /> 如果为负数或过高，则会将整个 <see cref="T:System.Security.AccessControl.GenericAcl" /> 复制到 <paramref name="array" />。</exception>
    public void InsertAce(int index, GenericAce ace)
    {
      if (ace == (GenericAce) null)
        throw new ArgumentNullException("ace");
      if (this.BinaryLength + ace.BinaryLength > GenericAcl.MaxBinaryLength)
        throw new OverflowException(Environment.GetResourceString("AccessControl_AclTooLong"));
      this._aces.Insert(index, (object) ace);
    }

    /// <summary>移除指定位置处的访问控制项 (ACE)。</summary>
    /// <param name="index">要移除的 ACE 的从零开始的索引。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 参数的值大于 <see cref="P:System.Security.AccessControl.RawAcl.Count" /> 属性的值减去一，或者为负值。</exception>
    public void RemoveAce(int index)
    {
      object obj = this._aces[index];
      this._aces.RemoveAt(index);
    }
  }
}
