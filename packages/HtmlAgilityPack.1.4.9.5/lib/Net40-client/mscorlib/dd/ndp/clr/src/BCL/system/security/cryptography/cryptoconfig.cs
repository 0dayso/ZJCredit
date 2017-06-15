// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.CryptoConfig
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Security.Util;
using System.Threading;

namespace System.Security.Cryptography
{
  /// <summary>访问加密配置信息。</summary>
  [ComVisible(true)]
  public class CryptoConfig
  {
    private static volatile Dictionary<string, string> defaultOidHT = (Dictionary<string, string>) null;
    private static volatile Dictionary<string, object> defaultNameHT = (Dictionary<string, object>) null;
    private static volatile Dictionary<string, string> machineOidHT = (Dictionary<string, string>) null;
    private static volatile Dictionary<string, string> machineNameHT = (Dictionary<string, string>) null;
    private static volatile Dictionary<string, Type> appNameHT = new Dictionary<string, Type>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    private static volatile Dictionary<string, string> appOidHT = new Dictionary<string, string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    private static volatile string version = (string) null;
    private const string MachineConfigFilename = "machine.config";
    private static volatile bool s_fipsAlgorithmPolicy;
    private static volatile bool s_haveFipsAlgorithmPolicy;
    private static object s_InternalSyncObject;

    /// <summary>指示运行时是否应强制实施该策略，以便仅创建经美国联邦信息处理标准 (FIPS) 认证的算法。</summary>
    /// <returns>若强制实施该策略，则为 true；否则为 false。</returns>
    public static bool AllowOnlyFipsAlgorithms
    {
      [SecuritySafeCritical] get
      {
        if (!CryptoConfig.s_haveFipsAlgorithmPolicy)
        {
          if (Utils._GetEnforceFipsPolicySetting())
          {
            if (Environment.OSVersion.Version.Major >= 6)
            {
              bool pfEnabled;
              uint fipsAlgorithmMode = Win32Native.BCryptGetFipsAlgorithmMode(out pfEnabled);
              CryptoConfig.s_fipsAlgorithmPolicy = ((int) fipsAlgorithmMode == 0 ? 1 : ((int) fipsAlgorithmMode == -1073741772 ? 1 : 0)) == 0 | pfEnabled;
              CryptoConfig.s_haveFipsAlgorithmPolicy = true;
            }
            else
            {
              CryptoConfig.s_fipsAlgorithmPolicy = Utils.ReadLegacyFipsPolicy();
              CryptoConfig.s_haveFipsAlgorithmPolicy = true;
            }
          }
          else
          {
            CryptoConfig.s_fipsAlgorithmPolicy = false;
            CryptoConfig.s_haveFipsAlgorithmPolicy = true;
          }
        }
        return CryptoConfig.s_fipsAlgorithmPolicy;
      }
    }

    private static string Version
    {
      [SecurityCritical] get
      {
        if (CryptoConfig.version == null)
          CryptoConfig.version = ((RuntimeType) typeof (CryptoConfig)).GetRuntimeAssembly().GetVersion().ToString();
        return CryptoConfig.version;
      }
    }

    private static object InternalSyncObject
    {
      get
      {
        if (CryptoConfig.s_InternalSyncObject == null)
        {
          object obj = new object();
          Interlocked.CompareExchange(ref CryptoConfig.s_InternalSyncObject, obj, (object) null);
        }
        return CryptoConfig.s_InternalSyncObject;
      }
    }

    private static Dictionary<string, string> DefaultOidHT
    {
      get
      {
        if (CryptoConfig.defaultOidHT == null)
          CryptoConfig.defaultOidHT = new Dictionary<string, string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase)
          {
            {
              "SHA",
              "1.3.14.3.2.26"
            },
            {
              "SHA1",
              "1.3.14.3.2.26"
            },
            {
              "System.Security.Cryptography.SHA1",
              "1.3.14.3.2.26"
            },
            {
              "System.Security.Cryptography.SHA1CryptoServiceProvider",
              "1.3.14.3.2.26"
            },
            {
              "System.Security.Cryptography.SHA1Managed",
              "1.3.14.3.2.26"
            },
            {
              "SHA256",
              "2.16.840.1.101.3.4.2.1"
            },
            {
              "System.Security.Cryptography.SHA256",
              "2.16.840.1.101.3.4.2.1"
            },
            {
              "System.Security.Cryptography.SHA256CryptoServiceProvider",
              "2.16.840.1.101.3.4.2.1"
            },
            {
              "System.Security.Cryptography.SHA256Cng",
              "2.16.840.1.101.3.4.2.1"
            },
            {
              "System.Security.Cryptography.SHA256Managed",
              "2.16.840.1.101.3.4.2.1"
            },
            {
              "SHA384",
              "2.16.840.1.101.3.4.2.2"
            },
            {
              "System.Security.Cryptography.SHA384",
              "2.16.840.1.101.3.4.2.2"
            },
            {
              "System.Security.Cryptography.SHA384CryptoServiceProvider",
              "2.16.840.1.101.3.4.2.2"
            },
            {
              "System.Security.Cryptography.SHA384Cng",
              "2.16.840.1.101.3.4.2.2"
            },
            {
              "System.Security.Cryptography.SHA384Managed",
              "2.16.840.1.101.3.4.2.2"
            },
            {
              "SHA512",
              "2.16.840.1.101.3.4.2.3"
            },
            {
              "System.Security.Cryptography.SHA512",
              "2.16.840.1.101.3.4.2.3"
            },
            {
              "System.Security.Cryptography.SHA512CryptoServiceProvider",
              "2.16.840.1.101.3.4.2.3"
            },
            {
              "System.Security.Cryptography.SHA512Cng",
              "2.16.840.1.101.3.4.2.3"
            },
            {
              "System.Security.Cryptography.SHA512Managed",
              "2.16.840.1.101.3.4.2.3"
            },
            {
              "RIPEMD160",
              "1.3.36.3.2.1"
            },
            {
              "System.Security.Cryptography.RIPEMD160",
              "1.3.36.3.2.1"
            },
            {
              "System.Security.Cryptography.RIPEMD160Managed",
              "1.3.36.3.2.1"
            },
            {
              "MD5",
              "1.2.840.113549.2.5"
            },
            {
              "System.Security.Cryptography.MD5",
              "1.2.840.113549.2.5"
            },
            {
              "System.Security.Cryptography.MD5CryptoServiceProvider",
              "1.2.840.113549.2.5"
            },
            {
              "System.Security.Cryptography.MD5Managed",
              "1.2.840.113549.2.5"
            },
            {
              "TripleDESKeyWrap",
              "1.2.840.113549.1.9.16.3.6"
            },
            {
              "RC2",
              "1.2.840.113549.3.2"
            },
            {
              "System.Security.Cryptography.RC2CryptoServiceProvider",
              "1.2.840.113549.3.2"
            },
            {
              "DES",
              "1.3.14.3.2.7"
            },
            {
              "System.Security.Cryptography.DESCryptoServiceProvider",
              "1.3.14.3.2.7"
            },
            {
              "TripleDES",
              "1.2.840.113549.3.7"
            },
            {
              "System.Security.Cryptography.TripleDESCryptoServiceProvider",
              "1.2.840.113549.3.7"
            }
          };
        return CryptoConfig.defaultOidHT;
      }
    }

    private static Dictionary<string, object> DefaultNameHT
    {
      get
      {
        if (CryptoConfig.defaultNameHT == null)
        {
          Dictionary<string, object> dictionary = new Dictionary<string, object>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
          Type type1 = typeof (SHA1CryptoServiceProvider);
          Type type2 = typeof (MD5CryptoServiceProvider);
          Type type3 = typeof (SHA256Managed);
          Type type4 = typeof (SHA384Managed);
          Type type5 = typeof (SHA512Managed);
          Type type6 = typeof (RIPEMD160Managed);
          Type type7 = typeof (HMACMD5);
          Type type8 = typeof (HMACRIPEMD160);
          Type type9 = typeof (HMACSHA1);
          Type type10 = typeof (HMACSHA256);
          Type type11 = typeof (HMACSHA384);
          Type type12 = typeof (HMACSHA512);
          Type type13 = typeof (MACTripleDES);
          Type type14 = typeof (RSACryptoServiceProvider);
          Type type15 = typeof (DSACryptoServiceProvider);
          Type type16 = typeof (DESCryptoServiceProvider);
          Type type17 = typeof (TripleDESCryptoServiceProvider);
          Type type18 = typeof (RC2CryptoServiceProvider);
          Type type19 = typeof (RijndaelManaged);
          Type type20 = typeof (DSASignatureDescription);
          Type type21 = typeof (RSAPKCS1SHA1SignatureDescription);
          Type type22 = typeof (RNGCryptoServiceProvider);
          string str1 = "System.Security.Cryptography.AesCryptoServiceProvider, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
          string str2 = "System.Security.Cryptography.AesManaged, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
          string str3 = "System.Security.Cryptography.ECDiffieHellmanCng, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
          string str4 = "System.Security.Cryptography.ECDsaCng, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
          string str5 = "System.Security.Cryptography.MD5Cng, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
          string str6 = "System.Security.Cryptography.SHA1Cng, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
          string str7 = "System.Security.Cryptography.SHA256Cng, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
          string str8 = "System.Security.Cryptography.SHA256CryptoServiceProvider, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
          string str9 = "System.Security.Cryptography.SHA384Cng, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
          string str10 = "System.Security.Cryptography.SHA384CryptoServiceProvider, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
          string str11 = "System.Security.Cryptography.SHA512Cng, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
          string str12 = "System.Security.Cryptography.SHA512CryptoServiceProvider, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
          string str13 = "System.Security.Cryptography.DpapiDataProtector, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";
          string key1 = "RandomNumberGenerator";
          Type type23 = type22;
          dictionary.Add(key1, (object) type23);
          string key2 = "System.Security.Cryptography.RandomNumberGenerator";
          Type type24 = type22;
          dictionary.Add(key2, (object) type24);
          string key3 = "SHA";
          Type type25 = type1;
          dictionary.Add(key3, (object) type25);
          string key4 = "SHA1";
          Type type26 = type1;
          dictionary.Add(key4, (object) type26);
          string key5 = "System.Security.Cryptography.SHA1";
          Type type27 = type1;
          dictionary.Add(key5, (object) type27);
          string key6 = "System.Security.Cryptography.SHA1Cng";
          string str14 = str6;
          dictionary.Add(key6, (object) str14);
          string key7 = "System.Security.Cryptography.HashAlgorithm";
          Type type28 = type1;
          dictionary.Add(key7, (object) type28);
          string key8 = "MD5";
          Type type29 = type2;
          dictionary.Add(key8, (object) type29);
          string key9 = "System.Security.Cryptography.MD5";
          Type type30 = type2;
          dictionary.Add(key9, (object) type30);
          string key10 = "System.Security.Cryptography.MD5Cng";
          string str15 = str5;
          dictionary.Add(key10, (object) str15);
          string key11 = "SHA256";
          Type type31 = type3;
          dictionary.Add(key11, (object) type31);
          string key12 = "SHA-256";
          Type type32 = type3;
          dictionary.Add(key12, (object) type32);
          string key13 = "System.Security.Cryptography.SHA256";
          Type type33 = type3;
          dictionary.Add(key13, (object) type33);
          string key14 = "System.Security.Cryptography.SHA256Cng";
          string str16 = str7;
          dictionary.Add(key14, (object) str16);
          string key15 = "System.Security.Cryptography.SHA256CryptoServiceProvider";
          string str17 = str8;
          dictionary.Add(key15, (object) str17);
          string key16 = "SHA384";
          Type type34 = type4;
          dictionary.Add(key16, (object) type34);
          string key17 = "SHA-384";
          Type type35 = type4;
          dictionary.Add(key17, (object) type35);
          string key18 = "System.Security.Cryptography.SHA384";
          Type type36 = type4;
          dictionary.Add(key18, (object) type36);
          string key19 = "System.Security.Cryptography.SHA384Cng";
          string str18 = str9;
          dictionary.Add(key19, (object) str18);
          string key20 = "System.Security.Cryptography.SHA384CryptoServiceProvider";
          string str19 = str10;
          dictionary.Add(key20, (object) str19);
          string key21 = "SHA512";
          Type type37 = type5;
          dictionary.Add(key21, (object) type37);
          string key22 = "SHA-512";
          Type type38 = type5;
          dictionary.Add(key22, (object) type38);
          string key23 = "System.Security.Cryptography.SHA512";
          Type type39 = type5;
          dictionary.Add(key23, (object) type39);
          string key24 = "System.Security.Cryptography.SHA512Cng";
          string str20 = str11;
          dictionary.Add(key24, (object) str20);
          string key25 = "System.Security.Cryptography.SHA512CryptoServiceProvider";
          string str21 = str12;
          dictionary.Add(key25, (object) str21);
          string key26 = "RIPEMD160";
          Type type40 = type6;
          dictionary.Add(key26, (object) type40);
          string key27 = "RIPEMD-160";
          Type type41 = type6;
          dictionary.Add(key27, (object) type41);
          string key28 = "System.Security.Cryptography.RIPEMD160";
          Type type42 = type6;
          dictionary.Add(key28, (object) type42);
          string key29 = "System.Security.Cryptography.RIPEMD160Managed";
          Type type43 = type6;
          dictionary.Add(key29, (object) type43);
          string key30 = "System.Security.Cryptography.HMAC";
          Type type44 = type9;
          dictionary.Add(key30, (object) type44);
          string key31 = "System.Security.Cryptography.KeyedHashAlgorithm";
          Type type45 = type9;
          dictionary.Add(key31, (object) type45);
          string key32 = "HMACMD5";
          Type type46 = type7;
          dictionary.Add(key32, (object) type46);
          string key33 = "System.Security.Cryptography.HMACMD5";
          Type type47 = type7;
          dictionary.Add(key33, (object) type47);
          string key34 = "HMACRIPEMD160";
          Type type48 = type8;
          dictionary.Add(key34, (object) type48);
          string key35 = "System.Security.Cryptography.HMACRIPEMD160";
          Type type49 = type8;
          dictionary.Add(key35, (object) type49);
          string key36 = "HMACSHA1";
          Type type50 = type9;
          dictionary.Add(key36, (object) type50);
          string key37 = "System.Security.Cryptography.HMACSHA1";
          Type type51 = type9;
          dictionary.Add(key37, (object) type51);
          string key38 = "HMACSHA256";
          Type type52 = type10;
          dictionary.Add(key38, (object) type52);
          string key39 = "System.Security.Cryptography.HMACSHA256";
          Type type53 = type10;
          dictionary.Add(key39, (object) type53);
          string key40 = "HMACSHA384";
          Type type54 = type11;
          dictionary.Add(key40, (object) type54);
          string key41 = "System.Security.Cryptography.HMACSHA384";
          Type type55 = type11;
          dictionary.Add(key41, (object) type55);
          string key42 = "HMACSHA512";
          Type type56 = type12;
          dictionary.Add(key42, (object) type56);
          string key43 = "System.Security.Cryptography.HMACSHA512";
          Type type57 = type12;
          dictionary.Add(key43, (object) type57);
          string key44 = "MACTripleDES";
          Type type58 = type13;
          dictionary.Add(key44, (object) type58);
          string key45 = "System.Security.Cryptography.MACTripleDES";
          Type type59 = type13;
          dictionary.Add(key45, (object) type59);
          string key46 = "RSA";
          Type type60 = type14;
          dictionary.Add(key46, (object) type60);
          string key47 = "System.Security.Cryptography.RSA";
          Type type61 = type14;
          dictionary.Add(key47, (object) type61);
          string key48 = "System.Security.Cryptography.AsymmetricAlgorithm";
          Type type62 = type14;
          dictionary.Add(key48, (object) type62);
          string key49 = "DSA";
          Type type63 = type15;
          dictionary.Add(key49, (object) type63);
          string key50 = "System.Security.Cryptography.DSA";
          Type type64 = type15;
          dictionary.Add(key50, (object) type64);
          string key51 = "ECDsa";
          string str22 = str4;
          dictionary.Add(key51, (object) str22);
          string key52 = "ECDsaCng";
          string str23 = str4;
          dictionary.Add(key52, (object) str23);
          string key53 = "System.Security.Cryptography.ECDsaCng";
          string str24 = str4;
          dictionary.Add(key53, (object) str24);
          string key54 = "ECDH";
          string str25 = str3;
          dictionary.Add(key54, (object) str25);
          string key55 = "ECDiffieHellman";
          string str26 = str3;
          dictionary.Add(key55, (object) str26);
          string key56 = "ECDiffieHellmanCng";
          string str27 = str3;
          dictionary.Add(key56, (object) str27);
          string key57 = "System.Security.Cryptography.ECDiffieHellmanCng";
          string str28 = str3;
          dictionary.Add(key57, (object) str28);
          string key58 = "DES";
          Type type65 = type16;
          dictionary.Add(key58, (object) type65);
          string key59 = "System.Security.Cryptography.DES";
          Type type66 = type16;
          dictionary.Add(key59, (object) type66);
          string key60 = "3DES";
          Type type67 = type17;
          dictionary.Add(key60, (object) type67);
          string key61 = "TripleDES";
          Type type68 = type17;
          dictionary.Add(key61, (object) type68);
          string key62 = "Triple DES";
          Type type69 = type17;
          dictionary.Add(key62, (object) type69);
          string key63 = "System.Security.Cryptography.TripleDES";
          Type type70 = type17;
          dictionary.Add(key63, (object) type70);
          string key64 = "RC2";
          Type type71 = type18;
          dictionary.Add(key64, (object) type71);
          string key65 = "System.Security.Cryptography.RC2";
          Type type72 = type18;
          dictionary.Add(key65, (object) type72);
          string key66 = "Rijndael";
          Type type73 = type19;
          dictionary.Add(key66, (object) type73);
          string key67 = "System.Security.Cryptography.Rijndael";
          Type type74 = type19;
          dictionary.Add(key67, (object) type74);
          string key68 = "System.Security.Cryptography.SymmetricAlgorithm";
          Type type75 = type19;
          dictionary.Add(key68, (object) type75);
          string key69 = "AES";
          string str29 = str1;
          dictionary.Add(key69, (object) str29);
          string key70 = "AesCryptoServiceProvider";
          string str30 = str1;
          dictionary.Add(key70, (object) str30);
          string key71 = "System.Security.Cryptography.AesCryptoServiceProvider";
          string str31 = str1;
          dictionary.Add(key71, (object) str31);
          string key72 = "AesManaged";
          string str32 = str2;
          dictionary.Add(key72, (object) str32);
          string key73 = "System.Security.Cryptography.AesManaged";
          string str33 = str2;
          dictionary.Add(key73, (object) str33);
          string key74 = "DpapiDataProtector";
          string str34 = str13;
          dictionary.Add(key74, (object) str34);
          string key75 = "System.Security.Cryptography.DpapiDataProtector";
          string str35 = str13;
          dictionary.Add(key75, (object) str35);
          string key76 = "http://www.w3.org/2000/09/xmldsig#dsa-sha1";
          Type type76 = type20;
          dictionary.Add(key76, (object) type76);
          string key77 = "System.Security.Cryptography.DSASignatureDescription";
          Type type77 = type20;
          dictionary.Add(key77, (object) type77);
          string key78 = "http://www.w3.org/2000/09/xmldsig#rsa-sha1";
          Type type78 = type21;
          dictionary.Add(key78, (object) type78);
          string key79 = "System.Security.Cryptography.RSASignatureDescription";
          Type type79 = type21;
          dictionary.Add(key79, (object) type79);
          string key80 = "http://www.w3.org/2000/09/xmldsig#sha1";
          Type type80 = type1;
          dictionary.Add(key80, (object) type80);
          string key81 = "http://www.w3.org/2001/04/xmlenc#sha256";
          Type type81 = type3;
          dictionary.Add(key81, (object) type81);
          string key82 = "http://www.w3.org/2001/04/xmlenc#sha512";
          Type type82 = type5;
          dictionary.Add(key82, (object) type82);
          string key83 = "http://www.w3.org/2001/04/xmlenc#ripemd160";
          Type type83 = type6;
          dictionary.Add(key83, (object) type83);
          string key84 = "http://www.w3.org/2001/04/xmlenc#des-cbc";
          Type type84 = type16;
          dictionary.Add(key84, (object) type84);
          string key85 = "http://www.w3.org/2001/04/xmlenc#tripledes-cbc";
          Type type85 = type17;
          dictionary.Add(key85, (object) type85);
          string key86 = "http://www.w3.org/2001/04/xmlenc#kw-tripledes";
          Type type86 = type17;
          dictionary.Add(key86, (object) type86);
          string key87 = "http://www.w3.org/2001/04/xmlenc#aes128-cbc";
          Type type87 = type19;
          dictionary.Add(key87, (object) type87);
          string key88 = "http://www.w3.org/2001/04/xmlenc#kw-aes128";
          Type type88 = type19;
          dictionary.Add(key88, (object) type88);
          string key89 = "http://www.w3.org/2001/04/xmlenc#aes192-cbc";
          Type type89 = type19;
          dictionary.Add(key89, (object) type89);
          string key90 = "http://www.w3.org/2001/04/xmlenc#kw-aes192";
          Type type90 = type19;
          dictionary.Add(key90, (object) type90);
          string key91 = "http://www.w3.org/2001/04/xmlenc#aes256-cbc";
          Type type91 = type19;
          dictionary.Add(key91, (object) type91);
          string key92 = "http://www.w3.org/2001/04/xmlenc#kw-aes256";
          Type type92 = type19;
          dictionary.Add(key92, (object) type92);
          string key93 = "http://www.w3.org/TR/2001/REC-xml-c14n-20010315";
          string str36 = "System.Security.Cryptography.Xml.XmlDsigC14NTransform, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";
          dictionary.Add(key93, (object) str36);
          string key94 = "http://www.w3.org/TR/2001/REC-xml-c14n-20010315#WithComments";
          string str37 = "System.Security.Cryptography.Xml.XmlDsigC14NWithCommentsTransform, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";
          dictionary.Add(key94, (object) str37);
          string key95 = "http://www.w3.org/2001/10/xml-exc-c14n#";
          string str38 = "System.Security.Cryptography.Xml.XmlDsigExcC14NTransform, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";
          dictionary.Add(key95, (object) str38);
          string key96 = "http://www.w3.org/2001/10/xml-exc-c14n#WithComments";
          string str39 = "System.Security.Cryptography.Xml.XmlDsigExcC14NWithCommentsTransform, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";
          dictionary.Add(key96, (object) str39);
          string key97 = "http://www.w3.org/2000/09/xmldsig#base64";
          string str40 = "System.Security.Cryptography.Xml.XmlDsigBase64Transform, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";
          dictionary.Add(key97, (object) str40);
          string key98 = "http://www.w3.org/TR/1999/REC-xpath-19991116";
          string str41 = "System.Security.Cryptography.Xml.XmlDsigXPathTransform, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";
          dictionary.Add(key98, (object) str41);
          string key99 = "http://www.w3.org/TR/1999/REC-xslt-19991116";
          string str42 = "System.Security.Cryptography.Xml.XmlDsigXsltTransform, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";
          dictionary.Add(key99, (object) str42);
          string key100 = "http://www.w3.org/2000/09/xmldsig#enveloped-signature";
          string str43 = "System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";
          dictionary.Add(key100, (object) str43);
          string key101 = "http://www.w3.org/2002/07/decrypt#XML";
          string str44 = "System.Security.Cryptography.Xml.XmlDecryptionTransform, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";
          dictionary.Add(key101, (object) str44);
          string key102 = "urn:mpeg:mpeg21:2003:01-REL-R-NS:licenseTransform";
          string str45 = "System.Security.Cryptography.Xml.XmlLicenseTransform, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";
          dictionary.Add(key102, (object) str45);
          string key103 = "http://www.w3.org/2000/09/xmldsig# X509Data";
          string str46 = "System.Security.Cryptography.Xml.KeyInfoX509Data, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";
          dictionary.Add(key103, (object) str46);
          string key104 = "http://www.w3.org/2000/09/xmldsig# KeyName";
          string str47 = "System.Security.Cryptography.Xml.KeyInfoName, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";
          dictionary.Add(key104, (object) str47);
          string key105 = "http://www.w3.org/2000/09/xmldsig# KeyValue/DSAKeyValue";
          string str48 = "System.Security.Cryptography.Xml.DSAKeyValue, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";
          dictionary.Add(key105, (object) str48);
          string key106 = "http://www.w3.org/2000/09/xmldsig# KeyValue/RSAKeyValue";
          string str49 = "System.Security.Cryptography.Xml.RSAKeyValue, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";
          dictionary.Add(key106, (object) str49);
          string key107 = "http://www.w3.org/2000/09/xmldsig# RetrievalMethod";
          string str50 = "System.Security.Cryptography.Xml.KeyInfoRetrievalMethod, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";
          dictionary.Add(key107, (object) str50);
          string key108 = "http://www.w3.org/2001/04/xmlenc# EncryptedKey";
          string str51 = "System.Security.Cryptography.Xml.KeyInfoEncryptedKey, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";
          dictionary.Add(key108, (object) str51);
          string key109 = "http://www.w3.org/2000/09/xmldsig#hmac-sha1";
          Type type93 = type9;
          dictionary.Add(key109, (object) type93);
          string key110 = "http://www.w3.org/2001/04/xmldsig-more#md5";
          Type type94 = type2;
          dictionary.Add(key110, (object) type94);
          string key111 = "http://www.w3.org/2001/04/xmldsig-more#sha384";
          Type type95 = type4;
          dictionary.Add(key111, (object) type95);
          string key112 = "http://www.w3.org/2001/04/xmldsig-more#hmac-md5";
          Type type96 = type7;
          dictionary.Add(key112, (object) type96);
          string key113 = "http://www.w3.org/2001/04/xmldsig-more#hmac-ripemd160";
          Type type97 = type8;
          dictionary.Add(key113, (object) type97);
          string key114 = "http://www.w3.org/2001/04/xmldsig-more#hmac-sha256";
          Type type98 = type10;
          dictionary.Add(key114, (object) type98);
          string key115 = "http://www.w3.org/2001/04/xmldsig-more#hmac-sha384";
          Type type99 = type11;
          dictionary.Add(key115, (object) type99);
          string key116 = "http://www.w3.org/2001/04/xmldsig-more#hmac-sha512";
          Type type100 = type12;
          dictionary.Add(key116, (object) type100);
          string key117 = "2.5.29.10";
          string str52 = "System.Security.Cryptography.X509Certificates.X509BasicConstraintsExtension, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
          dictionary.Add(key117, (object) str52);
          string key118 = "2.5.29.19";
          string str53 = "System.Security.Cryptography.X509Certificates.X509BasicConstraintsExtension, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
          dictionary.Add(key118, (object) str53);
          string key119 = "2.5.29.14";
          string str54 = "System.Security.Cryptography.X509Certificates.X509SubjectKeyIdentifierExtension, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
          dictionary.Add(key119, (object) str54);
          string key120 = "2.5.29.15";
          string str55 = "System.Security.Cryptography.X509Certificates.X509KeyUsageExtension, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
          dictionary.Add(key120, (object) str55);
          string key121 = "2.5.29.37";
          string str56 = "System.Security.Cryptography.X509Certificates.X509EnhancedKeyUsageExtension, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
          dictionary.Add(key121, (object) str56);
          string key122 = "X509Chain";
          string str57 = "System.Security.Cryptography.X509Certificates.X509Chain, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
          dictionary.Add(key122, (object) str57);
          string key123 = "1.2.840.113549.1.9.3";
          string str58 = "System.Security.Cryptography.Pkcs.Pkcs9ContentType, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";
          dictionary.Add(key123, (object) str58);
          string key124 = "1.2.840.113549.1.9.4";
          string str59 = "System.Security.Cryptography.Pkcs.Pkcs9MessageDigest, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";
          dictionary.Add(key124, (object) str59);
          string key125 = "1.2.840.113549.1.9.5";
          string str60 = "System.Security.Cryptography.Pkcs.Pkcs9SigningTime, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";
          dictionary.Add(key125, (object) str60);
          string key126 = "1.3.6.1.4.1.311.88.2.1";
          string str61 = "System.Security.Cryptography.Pkcs.Pkcs9DocumentName, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";
          dictionary.Add(key126, (object) str61);
          string key127 = "1.3.6.1.4.1.311.88.2.2";
          string str62 = "System.Security.Cryptography.Pkcs.Pkcs9DocumentDescription, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";
          dictionary.Add(key127, (object) str62);
          CryptoConfig.defaultNameHT = dictionary;
        }
        return CryptoConfig.defaultNameHT;
      }
    }

    [SecurityCritical]
    private static void InitializeConfigInfo()
    {
      if (CryptoConfig.machineNameHT != null)
        return;
      lock (CryptoConfig.InternalSyncObject)
      {
        if (CryptoConfig.machineNameHT != null)
          return;
        ConfigNode local_2 = CryptoConfig.OpenCryptoConfig();
        if (local_2 != null)
        {
          foreach (ConfigNode item_0 in local_2.Children)
          {
            if (CryptoConfig.machineNameHT != null)
            {
              if (CryptoConfig.machineOidHT != null)
                break;
            }
            if (CryptoConfig.machineNameHT == null && string.Compare(item_0.Name, "cryptoNameMapping", StringComparison.Ordinal) == 0)
              CryptoConfig.machineNameHT = CryptoConfig.InitializeNameMappings(item_0);
            else if (CryptoConfig.machineOidHT == null && string.Compare(item_0.Name, "oidMap", StringComparison.Ordinal) == 0)
              CryptoConfig.machineOidHT = CryptoConfig.InitializeOidMappings(item_0);
          }
        }
        if (CryptoConfig.machineNameHT == null)
          CryptoConfig.machineNameHT = new Dictionary<string, string>();
        if (CryptoConfig.machineOidHT != null)
          return;
        CryptoConfig.machineOidHT = new Dictionary<string, string>();
      }
    }

    /// <summary>将一组名称添加到要用于当前应用程序域的算法映射。</summary>
    /// <param name="algorithm">要映射到的算法。</param>
    /// <param name="names">要映射到该算法的名称的数组。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name=" algorithm" /> 或 <paramref name="names" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="algorithm" /> 不能从该程序集外部访问。- 或 -<paramref name="names" /> 参数中的一个条目是空的或为 null。</exception>
    [SecurityCritical]
    public static void AddAlgorithm(Type algorithm, params string[] names)
    {
      if (algorithm == (Type) null)
        throw new ArgumentNullException("algorithm");
      if (!algorithm.IsVisible)
        throw new ArgumentException(Environment.GetResourceString("Cryptography_AlgorithmTypesMustBeVisible"), "algorithm");
      if (names == null)
        throw new ArgumentNullException("names");
      string[] strArray = new string[names.Length];
      Array.Copy((Array) names, (Array) strArray, strArray.Length);
      foreach (string str in strArray)
      {
        if (string.IsNullOrEmpty(str))
          throw new ArgumentException(Environment.GetResourceString("Cryptography_AddNullOrEmptyName"));
      }
      lock (CryptoConfig.InternalSyncObject)
      {
        foreach (string item_1 in strArray)
          CryptoConfig.appNameHT[item_1] = algorithm;
      }
    }

    /// <summary>用指定的参数创建指定的加密对象的新实例。</summary>
    /// <returns>指定的加密对象的新实例。</returns>
    /// <param name="name">将创建其实例的加密对象的简单名称。</param>
    /// <param name="args">用于创建指定的加密对象的参数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 参数为 null。</exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">由 <paramref name="name" /> 参数描述的算法在使用中已启用联邦信息处理标准 (FIPS) 模式，但与 FIPS 不兼容。</exception>
    [SecuritySafeCritical]
    public static object CreateFromName(string name, params object[] args)
    {
      if (name == null)
        throw new ArgumentNullException("name");
      Type type = (Type) null;
      CryptoConfig.InitializeConfigInfo();
      lock (CryptoConfig.InternalSyncObject)
        type = CryptoConfig.appNameHT.GetValueOrDefault(name);
      if (type == (Type) null)
      {
        string valueOrDefault = CryptoConfig.machineNameHT.GetValueOrDefault(name);
        if (valueOrDefault != null)
        {
          type = Type.GetType(valueOrDefault, false, false);
          if (type != (Type) null && !type.IsVisible)
            type = (Type) null;
        }
      }
      if (type == (Type) null)
      {
        object valueOrDefault = CryptoConfig.DefaultNameHT.GetValueOrDefault(name);
        if (valueOrDefault != null)
        {
          if (valueOrDefault is Type)
            type = (Type) valueOrDefault;
          else if (valueOrDefault is string)
          {
            type = Type.GetType((string) valueOrDefault, false, false);
            if (type != (Type) null && !type.IsVisible)
              type = (Type) null;
          }
        }
      }
      if (type == (Type) null)
      {
        type = Type.GetType(name, false, false);
        if (type != (Type) null && !type.IsVisible)
          type = (Type) null;
      }
      if (type == (Type) null)
        return (object) null;
      RuntimeType runtimeType = type as RuntimeType;
      if (runtimeType == (RuntimeType) null)
        return (object) null;
      if (args == null)
        args = new object[0];
      MethodBase[] methodBaseArray = (MethodBase[]) runtimeType.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance);
      if (methodBaseArray == null)
        return (object) null;
      List<MethodBase> methodBaseList = new List<MethodBase>();
      for (int index = 0; index < methodBaseArray.Length; ++index)
      {
        MethodBase methodBase = methodBaseArray[index];
        if (methodBase.GetParameters().Length == args.Length)
          methodBaseList.Add(methodBase);
      }
      if (methodBaseList.Count == 0)
        return (object) null;
      object state;
      RuntimeConstructorInfo runtimeConstructorInfo = Type.DefaultBinder.BindToMethod(BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance, methodBaseList.ToArray(), ref args, (ParameterModifier[]) null, (CultureInfo) null, (string[]) null, out state) as RuntimeConstructorInfo;
      if ((ConstructorInfo) runtimeConstructorInfo == (ConstructorInfo) null || typeof (Delegate).IsAssignableFrom(runtimeConstructorInfo.DeclaringType))
        return (object) null;
      object obj = runtimeConstructorInfo.Invoke(BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance, Type.DefaultBinder, args, (CultureInfo) null);
      if (state == null)
        return obj;
      Type.DefaultBinder.ReorderArgumentArray(ref args, state);
      return obj;
    }

    /// <summary>创建指定的加密对象的新实例。</summary>
    /// <returns>指定的加密对象的新实例。</returns>
    /// <param name="name">将创建其实例的加密对象的简单名称。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 参数为 null。</exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">由 <paramref name="name" /> 参数描述的算法在使用中已启用联邦信息处理标准 (FIPS) 模式，但与 FIPS 不兼容。</exception>
    public static object CreateFromName(string name)
    {
      return CryptoConfig.CreateFromName(name, (object[]) null);
    }

    /// <summary>将一组名称添加到要用于当前应用程序域的对象标识符 (OID) 映射。</summary>
    /// <param name="oid">要映射到的对象标识符 (OID)。</param>
    /// <param name="names">要映射到该 OID 的名称的数组。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name=" oid" /> 或 <paramref name="names" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="names" /> 参数中的一个条目是空的或为 null。</exception>
    [SecurityCritical]
    public static void AddOID(string oid, params string[] names)
    {
      if (oid == null)
        throw new ArgumentNullException("oid");
      if (names == null)
        throw new ArgumentNullException("names");
      string[] strArray = new string[names.Length];
      Array.Copy((Array) names, (Array) strArray, strArray.Length);
      foreach (string str in strArray)
      {
        if (string.IsNullOrEmpty(str))
          throw new ArgumentException(Environment.GetResourceString("Cryptography_AddNullOrEmptyName"));
      }
      lock (CryptoConfig.InternalSyncObject)
      {
        foreach (string item_1 in strArray)
          CryptoConfig.appOidHT[item_1] = oid;
      }
    }

    /// <summary>获取与指定的简单名称对应的算法的对象标识符 (OID)。</summary>
    /// <returns>指定算法的 OID。</returns>
    /// <param name="name">获取其 OID 的算法的简单名称。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 参数为 null。</exception>
    public static string MapNameToOID(string name)
    {
      return CryptoConfig.MapNameToOID(name, OidGroup.AllGroups);
    }

    [SecuritySafeCritical]
    internal static string MapNameToOID(string name, OidGroup oidGroup)
    {
      if (name == null)
        throw new ArgumentNullException("name");
      CryptoConfig.InitializeConfigInfo();
      string str = (string) null;
      lock (CryptoConfig.InternalSyncObject)
        str = CryptoConfig.appOidHT.GetValueOrDefault(name);
      if (str == null)
        str = CryptoConfig.machineOidHT.GetValueOrDefault(name);
      if (str == null)
        str = CryptoConfig.DefaultOidHT.GetValueOrDefault(name);
      if (str == null)
        str = X509Utils.GetOidFromFriendlyName(name, oidGroup);
      return str;
    }

    /// <summary>对指定的对象标识符 (OID) 进行编码。</summary>
    /// <returns>包含编码 OID 的字节数组。</returns>
    /// <param name="str">要进行编码的 OID。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="str" /> 参数为 null。</exception>
    /// <exception cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException">对 OID 进行编码时出现错误。</exception>
    public static byte[] EncodeOID(string str)
    {
      if (str == null)
        throw new ArgumentNullException("str");
      char[] chArray = new char[1]{ '.' };
      string[] strArray = str.Split(chArray);
      uint[] numArray1 = new uint[strArray.Length];
      for (int index = 0; index < strArray.Length; ++index)
        numArray1[index] = (uint) int.Parse(strArray[index], (IFormatProvider) CultureInfo.InvariantCulture);
      byte[] numArray2 = new byte[numArray1.Length * 5];
      int destinationIndex = 0;
      if (numArray1.Length < 2)
        throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("Cryptography_InvalidOID"));
      byte[] numArray3 = CryptoConfig.EncodeSingleOIDNum(numArray1[0] * 40U + numArray1[1]);
      Array.Copy((Array) numArray3, 0, (Array) numArray2, destinationIndex, numArray3.Length);
      int num = destinationIndex + numArray3.Length;
      for (int index = 2; index < numArray1.Length; ++index)
      {
        byte[] numArray4 = CryptoConfig.EncodeSingleOIDNum(numArray1[index]);
        Buffer.InternalBlockCopy((Array) numArray4, 0, (Array) numArray2, num, numArray4.Length);
        num += numArray4.Length;
      }
      if (num > (int) sbyte.MaxValue)
        throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("Cryptography_Config_EncodedOIDError"));
      byte[] numArray5 = new byte[num + 2];
      numArray5[0] = (byte) 6;
      numArray5[1] = (byte) num;
      Buffer.InternalBlockCopy((Array) numArray2, 0, (Array) numArray5, 2, num);
      return numArray5;
    }

    private static byte[] EncodeSingleOIDNum(uint dwValue)
    {
      if ((int) dwValue < 128)
        return new byte[1]{ (byte) dwValue };
      if (dwValue < 16384U)
        return new byte[2]{ (byte) (dwValue >> 7 | 128U), (byte) (dwValue & (uint) sbyte.MaxValue) };
      if (dwValue < 2097152U)
        return new byte[3]{ (byte) (dwValue >> 14 | 128U), (byte) (dwValue >> 7 | 128U), (byte) (dwValue & (uint) sbyte.MaxValue) };
      if (dwValue < 268435456U)
        return new byte[4]{ (byte) (dwValue >> 21 | 128U), (byte) (dwValue >> 14 | 128U), (byte) (dwValue >> 7 | 128U), (byte) (dwValue & (uint) sbyte.MaxValue) };
      return new byte[5]{ (byte) (dwValue >> 28 | 128U), (byte) (dwValue >> 21 | 128U), (byte) (dwValue >> 14 | 128U), (byte) (dwValue >> 7 | 128U), (byte) (dwValue & (uint) sbyte.MaxValue) };
    }

    private static Dictionary<string, string> InitializeNameMappings(ConfigNode nameMappingNode)
    {
      Dictionary<string, string> dictionary1 = new Dictionary<string, string>();
      Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
      foreach (ConfigNode child1 in nameMappingNode.Children)
      {
        if (string.Compare(child1.Name, "cryptoClasses", StringComparison.Ordinal) == 0)
        {
          foreach (ConfigNode child2 in child1.Children)
          {
            if (string.Compare(child2.Name, "cryptoClass", StringComparison.Ordinal) == 0 && child2.Attributes.Count > 0)
            {
              DictionaryEntry dictionaryEntry = child2.Attributes[0];
              dictionary2.Add((string) dictionaryEntry.Key, (string) dictionaryEntry.Value);
            }
          }
        }
        else if (string.Compare(child1.Name, "nameEntry", StringComparison.Ordinal) == 0)
        {
          string key1 = (string) null;
          string key2 = (string) null;
          foreach (DictionaryEntry attribute in child1.Attributes)
          {
            if (string.Compare((string) attribute.Key, "name", StringComparison.Ordinal) == 0)
              key1 = (string) attribute.Value;
            else if (string.Compare((string) attribute.Key, "class", StringComparison.Ordinal) == 0)
              key2 = (string) attribute.Value;
          }
          if (key1 != null && key2 != null)
          {
            string valueOrDefault = dictionary2.GetValueOrDefault(key2);
            if (valueOrDefault != null)
              dictionary1.Add(key1, valueOrDefault);
          }
        }
      }
      return dictionary1;
    }

    private static Dictionary<string, string> InitializeOidMappings(ConfigNode oidMappingNode)
    {
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      foreach (ConfigNode child in oidMappingNode.Children)
      {
        if (string.Compare(child.Name, "oidEntry", StringComparison.Ordinal) == 0)
        {
          string str = (string) null;
          string key = (string) null;
          foreach (DictionaryEntry attribute in child.Attributes)
          {
            if (string.Compare((string) attribute.Key, "OID", StringComparison.Ordinal) == 0)
              str = (string) attribute.Value;
            else if (string.Compare((string) attribute.Key, "name", StringComparison.Ordinal) == 0)
              key = (string) attribute.Value;
          }
          if (key != null && str != null)
            dictionary.Add(key, str);
        }
      }
      return dictionary;
    }

    [SecurityCritical]
    private static ConfigNode OpenCryptoConfig()
    {
      string str = Config.MachineDirectory + "machine.config";
      new FileIOPermission(FileIOPermissionAccess.Read, str).Assert();
      if (!File.Exists(str))
        return (ConfigNode) null;
      CodeAccessPermission.RevertAssert();
      ConfigNode configNode1 = new ConfigTreeParser().Parse(str, "configuration", true);
      if (configNode1 == null)
        return (ConfigNode) null;
      ConfigNode configNode2 = (ConfigNode) null;
      foreach (ConfigNode child in configNode1.Children)
      {
        bool flag = false;
        if (string.Compare(child.Name, "mscorlib", StringComparison.Ordinal) == 0)
        {
          foreach (DictionaryEntry attribute in child.Attributes)
          {
            if (string.Compare((string) attribute.Key, "version", StringComparison.Ordinal) == 0)
            {
              flag = true;
              if (string.Compare((string) attribute.Value, CryptoConfig.Version, StringComparison.Ordinal) == 0)
              {
                configNode2 = child;
                break;
              }
            }
          }
          if (!flag)
            configNode2 = child;
        }
        if (configNode2 != null)
          break;
      }
      if (configNode2 == null)
        return (ConfigNode) null;
      foreach (ConfigNode child in configNode2.Children)
      {
        if (string.Compare(child.Name, "cryptographySettings", StringComparison.Ordinal) == 0)
          return child;
      }
      return (ConfigNode) null;
    }
  }
}
