﻿// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.CapiNative
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  internal static class CapiNative
  {
    [SecurityCritical]
    internal static SafeCspHandle AcquireCsp(string keyContainer, string providerName, CapiNative.ProviderType providerType, CapiNative.CryptAcquireContextFlags flags)
    {
      if ((flags & CapiNative.CryptAcquireContextFlags.VerifyContext) == CapiNative.CryptAcquireContextFlags.VerifyContext && (flags & CapiNative.CryptAcquireContextFlags.MachineKeyset) == CapiNative.CryptAcquireContextFlags.MachineKeyset)
        flags &= ~CapiNative.CryptAcquireContextFlags.MachineKeyset;
      SafeCspHandle phProv = (SafeCspHandle) null;
      if (!CapiNative.UnsafeNativeMethods.CryptAcquireContext(out phProv, keyContainer, providerName, providerType, flags))
        throw new CryptographicException(Marshal.GetLastWin32Error());
      return phProv;
    }

    [SecurityCritical]
    internal static SafeCspHashHandle CreateHashAlgorithm(SafeCspHandle cspHandle, CapiNative.AlgorithmID algorithm)
    {
      SafeCspHashHandle phHash = (SafeCspHashHandle) null;
      if (!CapiNative.UnsafeNativeMethods.CryptCreateHash(cspHandle, algorithm, IntPtr.Zero, 0, out phHash))
        throw new CryptographicException(Marshal.GetLastWin32Error());
      return phHash;
    }

    [SecurityCritical]
    internal static void GenerateRandomBytes(SafeCspHandle cspHandle, byte[] buffer)
    {
      if (!CapiNative.UnsafeNativeMethods.CryptGenRandom(cspHandle, buffer.Length, buffer))
        throw new CryptographicException(Marshal.GetLastWin32Error());
    }

    [SecurityCritical]
    internal static unsafe void GenerateRandomBytes(SafeCspHandle cspHandle, byte[] buffer, int offset, int count)
    {
      fixed (byte* pbBuffer = &buffer[offset])
      {
        if (!CapiNative.UnsafeNativeMethods.CryptGenRandom(cspHandle, count, pbBuffer))
          throw new CryptographicException(Marshal.GetLastWin32Error());
      }
    }

    [SecurityCritical]
    internal static int GetHashPropertyInt32(SafeCspHashHandle hashHandle, CapiNative.HashProperty property)
    {
      byte[] hashProperty = CapiNative.GetHashProperty(hashHandle, property);
      if (hashProperty.Length != 4)
        return 0;
      return BitConverter.ToInt32(hashProperty, 0);
    }

    [SecurityCritical]
    internal static byte[] GetHashProperty(SafeCspHashHandle hashHandle, CapiNative.HashProperty property)
    {
      int pdwDataLen = 0;
      byte[] pbData1 = (byte[]) null;
      if (!CapiNative.UnsafeNativeMethods.CryptGetHashParam(hashHandle, property, pbData1, out pdwDataLen, 0))
      {
        int lastWin32Error = Marshal.GetLastWin32Error();
        if (lastWin32Error != 234)
          throw new CryptographicException(lastWin32Error);
      }
      byte[] pbData2 = new byte[pdwDataLen];
      if (!CapiNative.UnsafeNativeMethods.CryptGetHashParam(hashHandle, property, pbData2, out pdwDataLen, 0))
        throw new CryptographicException(Marshal.GetLastWin32Error());
      return pbData2;
    }

    [SecurityCritical]
    internal static int GetKeyPropertyInt32(SafeCspKeyHandle keyHandle, CapiNative.KeyProperty property)
    {
      byte[] keyProperty = CapiNative.GetKeyProperty(keyHandle, property);
      if (keyProperty.Length != 4)
        return 0;
      return BitConverter.ToInt32(keyProperty, 0);
    }

    [SecurityCritical]
    internal static byte[] GetKeyProperty(SafeCspKeyHandle keyHandle, CapiNative.KeyProperty property)
    {
      int pdwDataLen = 0;
      byte[] pbData1 = (byte[]) null;
      if (!CapiNative.UnsafeNativeMethods.CryptGetKeyParam(keyHandle, property, pbData1, out pdwDataLen, 0))
      {
        int lastWin32Error = Marshal.GetLastWin32Error();
        if (lastWin32Error != 234)
          throw new CryptographicException(lastWin32Error);
      }
      byte[] pbData2 = new byte[pdwDataLen];
      if (!CapiNative.UnsafeNativeMethods.CryptGetKeyParam(keyHandle, property, pbData2, out pdwDataLen, 0))
        throw new CryptographicException(Marshal.GetLastWin32Error());
      return pbData2;
    }

    [SecurityCritical]
    internal static void SetHashProperty(SafeCspHashHandle hashHandle, CapiNative.HashProperty property, byte[] value)
    {
      if (!CapiNative.UnsafeNativeMethods.CryptSetHashParam(hashHandle, property, value, 0))
        throw new CryptographicException(Marshal.GetLastWin32Error());
    }

    [SecurityCritical]
    internal static bool VerifySignature(SafeCspHandle cspHandle, SafeCspKeyHandle keyHandle, CapiNative.AlgorithmID signatureAlgorithm, CapiNative.AlgorithmID hashAlgorithm, byte[] hashValue, byte[] signature)
    {
      byte[] numArray = new byte[signature.Length];
      Array.Copy((Array) signature, (Array) numArray, numArray.Length);
      Array.Reverse((Array) numArray);
      using (SafeCspHashHandle hashAlgorithm1 = CapiNative.CreateHashAlgorithm(cspHandle, hashAlgorithm))
      {
        if (hashValue.Length != CapiNative.GetHashPropertyInt32(hashAlgorithm1, CapiNative.HashProperty.HashSize))
          throw new CryptographicException(-2146893822);
        CapiNative.SetHashProperty(hashAlgorithm1, CapiNative.HashProperty.HashValue, hashValue);
        SafeCspHashHandle hHash = hashAlgorithm1;
        byte[] pbSignature = numArray;
        int length = pbSignature.Length;
        SafeCspKeyHandle hPubKey = keyHandle;
        // ISSUE: variable of the null type
        __Null local = null;
        int dwFlags = 0;
        if (CapiNative.UnsafeNativeMethods.CryptVerifySignature(hHash, pbSignature, length, hPubKey, (string) local, dwFlags))
          return true;
        int lastWin32Error = Marshal.GetLastWin32Error();
        if (lastWin32Error != -2146893818)
          throw new CryptographicException(lastWin32Error);
        return false;
      }
    }

    internal enum AlgorithmClass
    {
      Any = 0,
      Signature = 8192,
      Hash = 32768,
      KeyExchange = 40960,
    }

    internal enum AlgorithmType
    {
      Any = 0,
      Rsa = 1024,
    }

    internal enum AlgorithmSubId
    {
      Any = 0,
      RsaAny = 0,
      Sha1 = 4,
      Sha256 = 12,
      Sha384 = 13,
      Sha512 = 14,
    }

    internal enum AlgorithmID
    {
      None = 0,
      RsaSign = 9216,
      Sha1 = 32772,
      Sha256 = 32780,
      Sha384 = 32781,
      Sha512 = 32782,
      RsaKeyExchange = 41984,
    }

    [Flags]
    internal enum CryptAcquireContextFlags
    {
      None = 0,
      NewKeyset = 8,
      DeleteKeyset = 16,
      MachineKeyset = 32,
      Silent = 64,
      VerifyContext = -268435456,
    }

    internal enum ErrorCode
    {
      BadHash = -2146893822,
      BadData = -2146893819,
      BadSignature = -2146893818,
      NoKey = -2146893811,
      Ok = 0,
      MoreData = 234,
    }

    internal enum HashProperty
    {
      None = 0,
      HashValue = 2,
      HashSize = 4,
    }

    [Flags]
    internal enum KeyGenerationFlags
    {
      None = 0,
      Exportable = 1,
      UserProtected = 2,
      Archivable = 16384,
    }

    internal enum KeyProperty
    {
      None = 0,
      AlgorithmID = 7,
      KeyLength = 9,
    }

    internal enum KeySpec
    {
      KeyExchange = 1,
      Signature = 2,
    }

    internal static class ProviderNames
    {
      internal const string MicrosoftEnhanced = "Microsoft Enhanced Cryptographic Provider v1.0";
    }

    internal enum ProviderType
    {
      RsaFull = 1,
    }

    [SecurityCritical]
    internal static class UnsafeNativeMethods
    {
      [DllImport("advapi32", CharSet = CharSet.Unicode, SetLastError = true)]
      [return: MarshalAs(UnmanagedType.Bool)]
      internal static extern bool CryptAcquireContext(out SafeCspHandle phProv, string pszContainer, string pszProvider, CapiNative.ProviderType dwProvType, CapiNative.CryptAcquireContextFlags dwFlags);

      [DllImport("advapi32", SetLastError = true)]
      [return: MarshalAs(UnmanagedType.Bool)]
      internal static extern bool CryptCreateHash(SafeCspHandle hProv, CapiNative.AlgorithmID Algid, IntPtr hKey, int dwFlags, out SafeCspHashHandle phHash);

      [DllImport("advapi32", SetLastError = true)]
      [return: MarshalAs(UnmanagedType.Bool)]
      internal static extern bool CryptGenKey(SafeCspHandle hProv, int Algid, uint dwFlags, out SafeCspKeyHandle phKey);

      [DllImport("advapi32", SetLastError = true)]
      [return: MarshalAs(UnmanagedType.Bool)]
      internal static extern bool CryptGenRandom(SafeCspHandle hProv, int dwLen, [MarshalAs(UnmanagedType.LPArray), In, Out] byte[] pbBuffer);

      [DllImport("advapi32", SetLastError = true)]
      [return: MarshalAs(UnmanagedType.Bool)]
      internal static extern unsafe bool CryptGenRandom(SafeCspHandle hProv, int dwLen, byte* pbBuffer);

      [DllImport("advapi32", SetLastError = true)]
      [return: MarshalAs(UnmanagedType.Bool)]
      internal static extern bool CryptGetHashParam(SafeCspHashHandle hHash, CapiNative.HashProperty dwParam, [MarshalAs(UnmanagedType.LPArray), In, Out] byte[] pbData, [In, Out] ref int pdwDataLen, int dwFlags);

      [DllImport("advapi32", SetLastError = true)]
      [return: MarshalAs(UnmanagedType.Bool)]
      internal static extern bool CryptGetKeyParam(SafeCspKeyHandle hKey, CapiNative.KeyProperty dwParam, [MarshalAs(UnmanagedType.LPArray), In, Out] byte[] pbData, [In, Out] ref int pdwDataLen, int dwFlags);

      [DllImport("advapi32", SetLastError = true)]
      [return: MarshalAs(UnmanagedType.Bool)]
      internal static extern bool CryptImportKey(SafeCspHandle hProv, [MarshalAs(UnmanagedType.LPArray), In] byte[] pbData, int pdwDataLen, IntPtr hPubKey, CapiNative.KeyGenerationFlags dwFlags, out SafeCspKeyHandle phKey);

      [DllImport("advapi32", SetLastError = true)]
      [return: MarshalAs(UnmanagedType.Bool)]
      internal static extern bool CryptSetHashParam(SafeCspHashHandle hHash, CapiNative.HashProperty dwParam, [MarshalAs(UnmanagedType.LPArray), In] byte[] pbData, int dwFlags);

      [DllImport("advapi32", CharSet = CharSet.Unicode, SetLastError = true)]
      [return: MarshalAs(UnmanagedType.Bool)]
      internal static extern bool CryptVerifySignature(SafeCspHashHandle hHash, [MarshalAs(UnmanagedType.LPArray), In] byte[] pbSignature, int dwSigLen, SafeCspKeyHandle hPubKey, string sDescription, int dwFlags);
    }
  }
}
