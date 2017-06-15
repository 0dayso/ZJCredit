// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.Hash
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Security.Util;

namespace System.Security.Policy
{
  /// <summary>提供有关程序集的哈希值的证据。此类不能被继承。</summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class Hash : EvidenceBase, ISerializable
  {
    private RuntimeAssembly m_assembly;
    private Dictionary<Type, byte[]> m_hashes;
    private WeakReference m_rawData;

    /// <summary>获取程序集的 <see cref="T:System.Security.Cryptography.SHA1" /> 哈希值。</summary>
    /// <returns>一个字节数组，它表示程序集的 <see cref="T:System.Security.Cryptography.SHA1" /> 哈希值。</returns>
    public byte[] SHA1
    {
      get
      {
        byte[] numArray1 = (byte[]) null;
        if (!this.m_hashes.TryGetValue(typeof (System.Security.Cryptography.SHA1), out numArray1))
          numArray1 = this.GenerateHash(Hash.GetDefaultHashImplementationOrFallback(typeof (System.Security.Cryptography.SHA1), typeof (System.Security.Cryptography.SHA1)));
        byte[] numArray2 = new byte[numArray1.Length];
        Array.Copy((Array) numArray1, (Array) numArray2, numArray2.Length);
        return numArray2;
      }
    }

    /// <summary>获取程序集的 <see cref="T:System.Security.Cryptography.SHA256" /> 哈希值。</summary>
    /// <returns>一个字节数组，表示程序集的 <see cref="T:System.Security.Cryptography.SHA256" /> 哈希值。</returns>
    public byte[] SHA256
    {
      get
      {
        byte[] numArray1 = (byte[]) null;
        if (!this.m_hashes.TryGetValue(typeof (System.Security.Cryptography.SHA256), out numArray1))
          numArray1 = this.GenerateHash(Hash.GetDefaultHashImplementationOrFallback(typeof (System.Security.Cryptography.SHA256), typeof (System.Security.Cryptography.SHA256)));
        byte[] numArray2 = new byte[numArray1.Length];
        Array.Copy((Array) numArray1, (Array) numArray2, numArray2.Length);
        return numArray2;
      }
    }

    /// <summary>获取程序集的 <see cref="T:System.Security.Cryptography.MD5" /> 哈希值。</summary>
    /// <returns>一个字节数组，表示程序集的 <see cref="T:System.Security.Cryptography.MD5" /> 哈希值。</returns>
    public byte[] MD5
    {
      get
      {
        byte[] numArray1 = (byte[]) null;
        if (!this.m_hashes.TryGetValue(typeof (System.Security.Cryptography.MD5), out numArray1))
          numArray1 = this.GenerateHash(Hash.GetDefaultHashImplementationOrFallback(typeof (System.Security.Cryptography.MD5), typeof (System.Security.Cryptography.MD5)));
        byte[] numArray2 = new byte[numArray1.Length];
        Array.Copy((Array) numArray1, (Array) numArray2, numArray2.Length);
        return numArray2;
      }
    }

    [SecurityCritical]
    internal Hash(SerializationInfo info, StreamingContext context)
    {
      Dictionary<Type, byte[]> dictionary = info.GetValueNoThrow("Hashes", typeof (Dictionary<Type, byte[]>)) as Dictionary<Type, byte[]>;
      if (dictionary != null)
      {
        this.m_hashes = dictionary;
      }
      else
      {
        this.m_hashes = new Dictionary<Type, byte[]>();
        byte[] numArray1 = info.GetValueNoThrow("Md5", typeof (byte[])) as byte[];
        if (numArray1 != null)
          this.m_hashes[typeof (System.Security.Cryptography.MD5)] = numArray1;
        byte[] numArray2 = info.GetValueNoThrow("Sha1", typeof (byte[])) as byte[];
        if (numArray2 != null)
          this.m_hashes[typeof (System.Security.Cryptography.SHA1)] = numArray2;
        byte[] assemblyBytes = info.GetValueNoThrow("RawData", typeof (byte[])) as byte[];
        if (assemblyBytes == null)
          return;
        this.GenerateDefaultHashes(assemblyBytes);
      }
    }

    /// <summary>初始化 <see cref="T:System.Security.Policy.Hash" /> 类的新实例。</summary>
    /// <param name="assembly">计算其哈希值的程序集。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="assembly" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">无法生成哈希。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="assembly" /> 不是运行时 <see cref="T:System.Reflection.Assembly" /> 对象。</exception>
    public Hash(Assembly assembly)
    {
      if (assembly == (Assembly) null)
        throw new ArgumentNullException("assembly");
      if (assembly.IsDynamic)
        throw new ArgumentException(Environment.GetResourceString("Security_CannotGenerateHash"), "assembly");
      this.m_hashes = new Dictionary<Type, byte[]>();
      this.m_assembly = assembly as RuntimeAssembly;
      if ((Assembly) this.m_assembly == (Assembly) null)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeAssembly"), "assembly");
    }

    private Hash(Hash hash)
    {
      this.m_assembly = hash.m_assembly;
      this.m_rawData = hash.m_rawData;
      this.m_hashes = new Dictionary<Type, byte[]>((IDictionary<Type, byte[]>) hash.m_hashes);
    }

    private Hash(Type hashType, byte[] hashValue)
    {
      this.m_hashes = new Dictionary<Type, byte[]>();
      byte[] numArray = new byte[hashValue.Length];
      Array.Copy((Array) hashValue, (Array) numArray, numArray.Length);
      this.m_hashes[hashType] = hashValue;
    }

    /// <summary>创建一个包含 <see cref="T:System.Security.Cryptography.SHA1" /> 哈希值的 <see cref="T:System.Security.Policy.Hash" /> 对象。</summary>
    /// <returns>一个对象，包含由 <paramref name="sha1" /> 参数提供的哈希值。</returns>
    /// <param name="sha1">一个包含 <see cref="T:System.Security.Cryptography.SHA1" /> 哈希值的字节数组。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="sha1" /> 参数为 null。</exception>
    public static Hash CreateSHA1(byte[] sha1)
    {
      if (sha1 == null)
        throw new ArgumentNullException("sha1");
      return new Hash(typeof (System.Security.Cryptography.SHA1), sha1);
    }

    /// <summary>创建一个包含 <see cref="T:System.Security.Cryptography.SHA256" /> 哈希值的 <see cref="T:System.Security.Policy.Hash" /> 对象。</summary>
    /// <returns>一个哈希对象，包含由 <paramref name="sha256" /> 参数提供的哈希值。</returns>
    /// <param name="sha256">一个包含 <see cref="T:System.Security.Cryptography.SHA256" /> 哈希值的字节数组。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="sha256" /> 参数为 null。</exception>
    public static Hash CreateSHA256(byte[] sha256)
    {
      if (sha256 == null)
        throw new ArgumentNullException("sha256");
      return new Hash(typeof (System.Security.Cryptography.SHA256), sha256);
    }

    /// <summary>创建一个包含 <see cref="T:System.Security.Cryptography.MD5" /> 哈希值的 <see cref="T:System.Security.Policy.Hash" /> 对象。</summary>
    /// <returns>一个对象，包含由 <paramref name="md5" /> 参数提供的哈希值。</returns>
    /// <param name="md5">一个包含 <see cref="T:System.Security.Cryptography.MD5" /> 哈希值的字节数组。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="md5" /> 参数为 null。</exception>
    public static Hash CreateMD5(byte[] md5)
    {
      if (md5 == null)
        throw new ArgumentNullException("md5");
      return new Hash(typeof (System.Security.Cryptography.MD5), md5);
    }

    /// <summary>创建作为当前实例副本的新对象。</summary>
    /// <returns>作为此实例副本的新对象。</returns>
    public override EvidenceBase Clone()
    {
      return (EvidenceBase) new Hash(this);
    }

    [OnSerializing]
    private void OnSerializing(StreamingContext ctx)
    {
      this.GenerateDefaultHashes();
    }

    /// <summary>获取带有参数名和附加异常信息的 <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 对象。</summary>
    /// <param name="info">承载序列化对象数据的对象。</param>
    /// <param name="context">关于来源和目标的上下文信息</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      this.GenerateDefaultHashes();
      byte[] numArray1;
      if (this.m_hashes.TryGetValue(typeof (System.Security.Cryptography.MD5), out numArray1))
        info.AddValue("Md5", (object) numArray1);
      byte[] numArray2;
      if (this.m_hashes.TryGetValue(typeof (System.Security.Cryptography.SHA1), out numArray2))
        info.AddValue("Sha1", (object) numArray2);
      info.AddValue("RawData", (object) null);
      info.AddValue("PEFile", (object) IntPtr.Zero);
      info.AddValue("Hashes", (object) this.m_hashes);
    }

    /// <summary>使用指定的哈希算法计算程序集的哈希值。</summary>
    /// <returns>一个字节数组，它表示程序集的哈希值。</returns>
    /// <param name="hashAlg">将用于计算程序集的哈希值的哈希算法。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="hashAlg" /> 参数为 null。</exception>
    /// <exception cref="T:System.Security.SecurityException">无法生成程序集的哈希值。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    ///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public byte[] GenerateHash(HashAlgorithm hashAlg)
    {
      if (hashAlg == null)
        throw new ArgumentNullException("hashAlg");
      byte[] hash = this.GenerateHash(hashAlg.GetType());
      byte[] numArray1 = new byte[hash.Length];
      byte[] numArray2 = numArray1;
      int length = numArray1.Length;
      Array.Copy((Array) hash, (Array) numArray2, length);
      return numArray1;
    }

    private byte[] GenerateHash(Type hashType)
    {
      Type hashIndexType = Hash.GetHashIndexType(hashType);
      byte[] numArray = (byte[]) null;
      if (!this.m_hashes.TryGetValue(hashIndexType, out numArray))
      {
        if ((Assembly) this.m_assembly == (Assembly) null)
          throw new InvalidOperationException(Environment.GetResourceString("Security_CannotGenerateHash"));
        numArray = Hash.GenerateHash(hashType, this.GetRawData());
        this.m_hashes[hashIndexType] = numArray;
      }
      return numArray;
    }

    private static byte[] GenerateHash(Type hashType, byte[] assemblyBytes)
    {
      using (HashAlgorithm hashAlgorithm = HashAlgorithm.Create(hashType.FullName))
        return hashAlgorithm.ComputeHash(assemblyBytes);
    }

    private void GenerateDefaultHashes()
    {
      if (!((Assembly) this.m_assembly != (Assembly) null))
        return;
      this.GenerateDefaultHashes(this.GetRawData());
    }

    private void GenerateDefaultHashes(byte[] assemblyBytes)
    {
      Type[] typeArray = new Type[3]{ Hash.GetHashIndexType(typeof (System.Security.Cryptography.SHA1)), Hash.GetHashIndexType(typeof (System.Security.Cryptography.SHA256)), Hash.GetHashIndexType(typeof (System.Security.Cryptography.MD5)) };
      foreach (Type index in typeArray)
      {
        Type hashImplementation = Hash.GetDefaultHashImplementation(index);
        if (hashImplementation != (Type) null && !this.m_hashes.ContainsKey(index))
          this.m_hashes[index] = Hash.GenerateHash(hashImplementation, assemblyBytes);
      }
    }

    private static Type GetDefaultHashImplementationOrFallback(Type hashAlgorithm, Type fallbackImplementation)
    {
      Type hashImplementation = Hash.GetDefaultHashImplementation(hashAlgorithm);
      if (!(hashImplementation != (Type) null))
        return fallbackImplementation;
      return hashImplementation;
    }

    private static Type GetDefaultHashImplementation(Type hashAlgorithm)
    {
      if (hashAlgorithm.IsAssignableFrom(typeof (System.Security.Cryptography.MD5)))
      {
        if (!CryptoConfig.AllowOnlyFipsAlgorithms)
          return typeof (MD5CryptoServiceProvider);
        return (Type) null;
      }
      if (hashAlgorithm.IsAssignableFrom(typeof (System.Security.Cryptography.SHA256)))
        return Type.GetType("System.Security.Cryptography.SHA256CryptoServiceProvider, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
      return hashAlgorithm;
    }

    private static Type GetHashIndexType(Type hashType)
    {
      Type type = hashType;
      while (type != (Type) null && type.BaseType != typeof (HashAlgorithm))
        type = type.BaseType;
      if (type == (Type) null)
        type = typeof (HashAlgorithm);
      return type;
    }

    private byte[] GetRawData()
    {
      byte[] numArray = (byte[]) null;
      if ((Assembly) this.m_assembly != (Assembly) null)
      {
        if (this.m_rawData != null)
          numArray = this.m_rawData.Target as byte[];
        if (numArray == null)
        {
          numArray = this.m_assembly.GetRawBytes();
          this.m_rawData = new WeakReference((object) numArray);
        }
      }
      return numArray;
    }

    private SecurityElement ToXml()
    {
      this.GenerateDefaultHashes();
      SecurityElement securityElement = new SecurityElement("System.Security.Policy.Hash");
      securityElement.AddAttribute("version", "2");
      foreach (KeyValuePair<Type, byte[]> mHash in this.m_hashes)
      {
        SecurityElement child = new SecurityElement("hash");
        child.AddAttribute("algorithm", mHash.Key.Name);
        child.AddAttribute("value", Hex.EncodeHexString(mHash.Value));
        securityElement.AddChild(child);
      }
      return securityElement;
    }

    /// <summary>返回当前 <see cref="T:System.Security.Policy.Hash" /> 的字符串表示形式。</summary>
    /// <returns>当前 <see cref="T:System.Security.Policy.Hash" /> 的表示形式。</returns>
    public override string ToString()
    {
      return this.ToXml().ToString();
    }
  }
}
