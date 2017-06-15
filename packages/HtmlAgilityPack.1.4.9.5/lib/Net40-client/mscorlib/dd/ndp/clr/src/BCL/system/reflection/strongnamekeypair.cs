// Decompiled with JetBrains decompiler
// Type: System.Reflection.StrongNameKeyPair
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Runtime.Hosting;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;

namespace System.Reflection
{
  /// <summary>封装对公钥或私钥对的访问，该公钥或私钥对用于为强名称程序集创建签名。</summary>
  [ComVisible(true)]
  [Serializable]
  public class StrongNameKeyPair : IDeserializationCallback, ISerializable
  {
    private bool _keyPairExported;
    private byte[] _keyPairArray;
    private string _keyPairContainer;
    private byte[] _publicKey;

    /// <summary>获取密钥对的公钥或公钥标记的公共部分。</summary>
    /// <returns>一个 byte 类型的数组，其中包含密钥对的公钥或公钥标记。</returns>
    public byte[] PublicKey
    {
      [SecuritySafeCritical] get
      {
        if (this._publicKey == null)
          this._publicKey = this.ComputePublicKey();
        byte[] numArray = new byte[this._publicKey.Length];
        Array.Copy((Array) this._publicKey, (Array) numArray, this._publicKey.Length);
        return numArray;
      }
    }

    /// <summary>初始化 <see cref="T:System.Reflection.StrongNameKeyPair" /> 类的新实例，同时从 FileStream 生成密钥对。</summary>
    /// <param name="keyPairFile">包含密钥对的 FileStream。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="keyPairFile" /> 为 null。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public StrongNameKeyPair(FileStream keyPairFile)
    {
      if (keyPairFile == null)
        throw new ArgumentNullException("keyPairFile");
      int count = (int) keyPairFile.Length;
      this._keyPairArray = new byte[count];
      keyPairFile.Read(this._keyPairArray, 0, count);
      this._keyPairExported = true;
    }

    /// <summary>初始化 <see cref="T:System.Reflection.StrongNameKeyPair" /> 类的新实例，同时从 byte 数组生成密钥对。</summary>
    /// <param name="keyPairArray">包含密钥对的 byte 类型数组。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="keyPairArray" /> 为 null。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public StrongNameKeyPair(byte[] keyPairArray)
    {
      if (keyPairArray == null)
        throw new ArgumentNullException("keyPairArray");
      this._keyPairArray = new byte[keyPairArray.Length];
      Array.Copy((Array) keyPairArray, (Array) this._keyPairArray, keyPairArray.Length);
      this._keyPairExported = true;
    }

    /// <summary>初始化 <see cref="T:System.Reflection.StrongNameKeyPair" /> 类的新实例，同时从 String 生成密钥对。</summary>
    /// <param name="keyPairContainer">包含密钥对的字符串。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="keyPairContainer" /> 为 null。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public StrongNameKeyPair(string keyPairContainer)
    {
      if (keyPairContainer == null)
        throw new ArgumentNullException("keyPairContainer");
      this._keyPairContainer = keyPairContainer;
      this._keyPairExported = false;
    }

    /// <summary>初始化 <see cref="T:System.Reflection.StrongNameKeyPair" /> 类的新实例，从序列化数据生成密钥对。</summary>
    /// <param name="info">保留序列化对象数据的 <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 对象。</param>
    /// <param name="context">一个包含有关源或目标的上下文信息的 <see cref="T:System.Runtime.Serialization.StreamingContext" /> 对象。</param>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    protected StrongNameKeyPair(SerializationInfo info, StreamingContext context)
    {
      this._keyPairExported = (bool) info.GetValue("_keyPairExported", typeof (bool));
      this._keyPairArray = (byte[]) info.GetValue("_keyPairArray", typeof (byte[]));
      this._keyPairContainer = (string) info.GetValue("_keyPairContainer", typeof (string));
      this._publicKey = (byte[]) info.GetValue("_publicKey", typeof (byte[]));
    }

    [SecurityCritical]
    private unsafe byte[] ComputePublicKey()
    {
      byte[] dest = (byte[]) null;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
      }
      finally
      {
        IntPtr ppbPublicKeyBlob = IntPtr.Zero;
        int pcbPublicKeyBlob = 0;
        try
        {
          if (!(!this._keyPairExported ? StrongNameHelpers.StrongNameGetPublicKey(this._keyPairContainer, (byte[]) null, 0, out ppbPublicKeyBlob, out pcbPublicKeyBlob) : StrongNameHelpers.StrongNameGetPublicKey((string) null, this._keyPairArray, this._keyPairArray.Length, out ppbPublicKeyBlob, out pcbPublicKeyBlob)))
            throw new ArgumentException(Environment.GetResourceString("Argument_StrongNameGetPublicKey"));
          dest = new byte[pcbPublicKeyBlob];
          Buffer.Memcpy(dest, 0, (byte*) ppbPublicKeyBlob.ToPointer(), 0, pcbPublicKeyBlob);
        }
        finally
        {
          if (ppbPublicKeyBlob != IntPtr.Zero)
            StrongNameHelpers.StrongNameFreeBuffer(ppbPublicKeyBlob);
        }
      }
      return dest;
    }

    [SecurityCritical]
    void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
    {
      info.AddValue("_keyPairExported", this._keyPairExported);
      info.AddValue("_keyPairArray", (object) this._keyPairArray);
      info.AddValue("_keyPairContainer", (object) this._keyPairContainer);
      info.AddValue("_publicKey", (object) this._publicKey);
    }

    void IDeserializationCallback.OnDeserialization(object sender)
    {
    }

    private bool GetKeyPair(out object arrayOrContainer)
    {
      arrayOrContainer = this._keyPairExported ? (object) this._keyPairArray : (object) this._keyPairContainer;
      return this._keyPairExported;
    }
  }
}
