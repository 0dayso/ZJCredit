// Decompiled with JetBrains decompiler
// Type: System.Resolver
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;
using System.Security;
using System.Threading;

namespace System
{
  internal abstract class Resolver
  {
    internal abstract RuntimeType GetJitContext(ref int securityControlFlags);

    internal abstract byte[] GetCodeInfo(ref int stackSize, ref int initLocals, ref int EHCount);

    internal abstract byte[] GetLocalsSignature();

    [SecurityCritical]
    internal abstract unsafe void GetEHInfo(int EHNumber, void* exception);

    internal abstract byte[] GetRawEHInfo();

    internal abstract string GetStringLiteral(int token);

    [SecurityCritical]
    internal abstract void ResolveToken(int token, out IntPtr typeHandle, out IntPtr methodHandle, out IntPtr fieldHandle);

    internal abstract byte[] ResolveSignature(int token, int fromMethod);

    internal abstract MethodInfo GetDynamicMethod();

    internal abstract CompressedStack GetSecurityContext();

    internal struct CORINFO_EH_CLAUSE
    {
      internal int Flags;
      internal int TryOffset;
      internal int TryLength;
      internal int HandlerOffset;
      internal int HandlerLength;
      internal int ClassTokenOrFilterOffset;
    }
  }
}
