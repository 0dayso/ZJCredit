// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.UnmanagedMarshal
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Reflection.Emit
{
  /// <summary>表示说明如何将字段从托管代码封送为非托管代码的类。此类不能被继承。</summary>
  [ComVisible(true)]
  [Obsolete("An alternate API is available: Emit the MarshalAs custom attribute instead. http://go.microsoft.com/fwlink/?linkid=14202")]
  [Serializable]
  [HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
  public sealed class UnmanagedMarshal
  {
    internal UnmanagedType m_unmanagedType;
    internal Guid m_guid;
    internal int m_numElem;
    internal UnmanagedType m_baseType;

    /// <summary>指示非托管类型。此属性为只读。</summary>
    /// <returns>
    /// <see cref="T:System.Runtime.InteropServices.UnmanagedType" /> 对象。</returns>
    public UnmanagedType GetUnmanagedType
    {
      get
      {
        return this.m_unmanagedType;
      }
    }

    /// <summary>获取 GUID。此属性为只读。</summary>
    /// <returns>一个 <see cref="T:System.Guid" /> 对象。</returns>
    /// <exception cref="T:System.ArgumentException">该参数不是自定义封送拆收器。</exception>
    public Guid IIDGuid
    {
      get
      {
        if (this.m_unmanagedType == UnmanagedType.CustomMarshaler)
          return this.m_guid;
        throw new ArgumentException(Environment.GetResourceString("Argument_NotACustomMarshaler"));
      }
    }

    /// <summary>获取数字元素。此属性为只读。</summary>
    /// <returns>指示元素计数的整数。</returns>
    /// <exception cref="T:System.ArgumentException">该参数不是非托管元素计数。</exception>
    public int ElementCount
    {
      get
      {
        if (this.m_unmanagedType != UnmanagedType.ByValArray && this.m_unmanagedType != UnmanagedType.ByValTStr)
          throw new ArgumentException(Environment.GetResourceString("Argument_NoUnmanagedElementCount"));
        return this.m_numElem;
      }
    }

    /// <summary>获取非托管基类型。此属性为只读。</summary>
    /// <returns>UnmanagedType 对象。</returns>
    /// <exception cref="T:System.ArgumentException">非托管类型不是 LPArray 或 SafeArray。</exception>
    public UnmanagedType BaseType
    {
      get
      {
        if (this.m_unmanagedType != UnmanagedType.LPArray && this.m_unmanagedType != UnmanagedType.SafeArray)
          throw new ArgumentException(Environment.GetResourceString("Argument_NoNestedMarshal"));
        return this.m_baseType;
      }
    }

    private UnmanagedMarshal(UnmanagedType unmanagedType, Guid guid, int numElem, UnmanagedType type)
    {
      this.m_unmanagedType = unmanagedType;
      this.m_guid = guid;
      this.m_numElem = numElem;
      this.m_baseType = type;
    }

    /// <summary>指定要封送为非托管代码的给定类型。</summary>
    /// <returns>
    /// <see cref="T:System.Reflection.Emit.UnmanagedMarshal" /> 对象。</returns>
    /// <param name="unmanagedType">要将该类型封送到的非托管类型。</param>
    /// <exception cref="T:System.ArgumentException">该参数不是简单本机类型。</exception>
    public static UnmanagedMarshal DefineUnmanagedMarshal(UnmanagedType unmanagedType)
    {
      if (unmanagedType == UnmanagedType.ByValTStr || unmanagedType == UnmanagedType.SafeArray || (unmanagedType == UnmanagedType.CustomMarshaler || unmanagedType == UnmanagedType.ByValArray) || unmanagedType == UnmanagedType.LPArray)
        throw new ArgumentException(Environment.GetResourceString("Argument_NotASimpleNativeType"));
      return new UnmanagedMarshal(unmanagedType, Guid.Empty, 0, (UnmanagedType) 0);
    }

    /// <summary>指定要封送为非托管代码的固定数组缓冲区 (ByValTStr) 中的字符串。</summary>
    /// <returns>
    /// <see cref="T:System.Reflection.Emit.UnmanagedMarshal" /> 对象。</returns>
    /// <param name="elemCount">固定数组缓冲区中的元素数目。</param>
    /// <exception cref="T:System.ArgumentException">该参数不是简单本机类型。</exception>
    public static UnmanagedMarshal DefineByValTStr(int elemCount)
    {
      return new UnmanagedMarshal(UnmanagedType.ByValTStr, Guid.Empty, elemCount, (UnmanagedType) 0);
    }

    /// <summary>指定要封送为非托管代码的 SafeArray。</summary>
    /// <returns>
    /// <see cref="T:System.Reflection.Emit.UnmanagedMarshal" /> 对象。</returns>
    /// <param name="elemType">数组的每个元素的基类型或 UnmanagedType。</param>
    /// <exception cref="T:System.ArgumentException">该参数不是简单本机类型。</exception>
    public static UnmanagedMarshal DefineSafeArray(UnmanagedType elemType)
    {
      return new UnmanagedMarshal(UnmanagedType.SafeArray, Guid.Empty, 0, elemType);
    }

    /// <summary>指定要封送为非托管代码的固定长度的数组 (ByValArray)。</summary>
    /// <returns>
    /// <see cref="T:System.Reflection.Emit.UnmanagedMarshal" /> 对象。</returns>
    /// <param name="elemCount">固定长度数组中的元素数目。</param>
    /// <exception cref="T:System.ArgumentException">该参数不是简单本机类型。</exception>
    public static UnmanagedMarshal DefineByValArray(int elemCount)
    {
      return new UnmanagedMarshal(UnmanagedType.ByValArray, Guid.Empty, elemCount, (UnmanagedType) 0);
    }

    /// <summary>指定要封送为非托管代码的 LPArray。LPArray 的长度在运行时由实际的已封送数组的大小确定。</summary>
    /// <returns>
    /// <see cref="T:System.Reflection.Emit.UnmanagedMarshal" /> 对象。</returns>
    /// <param name="elemType">要将数组封送为的非托管类型。</param>
    /// <exception cref="T:System.ArgumentException">该参数不是简单本机类型。</exception>
    public static UnmanagedMarshal DefineLPArray(UnmanagedType elemType)
    {
      return new UnmanagedMarshal(UnmanagedType.LPArray, Guid.Empty, 0, elemType);
    }

    internal byte[] InternalGetBytes()
    {
      if (this.m_unmanagedType == UnmanagedType.SafeArray || this.m_unmanagedType == UnmanagedType.LPArray)
        return new byte[2]{ (byte) this.m_unmanagedType, (byte) this.m_baseType };
      if (this.m_unmanagedType == UnmanagedType.ByValArray || this.m_unmanagedType == UnmanagedType.ByValTStr)
      {
        int num1 = 0;
        byte[] numArray1 = new byte[(this.m_numElem > (int) sbyte.MaxValue ? (this.m_numElem > 16383 ? 4 : 2) : 1) + 1];
        byte[] numArray2 = numArray1;
        int index1 = num1;
        int num2 = 1;
        int num3 = index1 + num2;
        int num4 = (int) (byte) this.m_unmanagedType;
        numArray2[index1] = (byte) num4;
        int num5;
        if (this.m_numElem <= (int) sbyte.MaxValue)
        {
          byte[] numArray3 = numArray1;
          int index2 = num3;
          int num6 = 1;
          num5 = index2 + num6;
          int num7 = (int) (byte) (this.m_numElem & (int) byte.MaxValue);
          numArray3[index2] = (byte) num7;
        }
        else if (this.m_numElem <= 16383)
        {
          byte[] numArray3 = numArray1;
          int index2 = num3;
          int num6 = 1;
          int num7 = index2 + num6;
          int num8 = (int) (byte) (this.m_numElem >> 8 | 128);
          numArray3[index2] = (byte) num8;
          byte[] numArray4 = numArray1;
          int index3 = num7;
          int num9 = 1;
          num5 = index3 + num9;
          int num10 = (int) (byte) (this.m_numElem & (int) byte.MaxValue);
          numArray4[index3] = (byte) num10;
        }
        else if (this.m_numElem <= 536870911)
        {
          byte[] numArray3 = numArray1;
          int index2 = num3;
          int num6 = 1;
          int num7 = index2 + num6;
          int num8 = (int) (byte) (this.m_numElem >> 24 | 192);
          numArray3[index2] = (byte) num8;
          byte[] numArray4 = numArray1;
          int index3 = num7;
          int num9 = 1;
          int num10 = index3 + num9;
          int num11 = (int) (byte) (this.m_numElem >> 16 & (int) byte.MaxValue);
          numArray4[index3] = (byte) num11;
          byte[] numArray5 = numArray1;
          int index4 = num10;
          int num12 = 1;
          int num13 = index4 + num12;
          int num14 = (int) (byte) (this.m_numElem >> 8 & (int) byte.MaxValue);
          numArray5[index4] = (byte) num14;
          byte[] numArray6 = numArray1;
          int index5 = num13;
          int num15 = 1;
          num5 = index5 + num15;
          int num16 = (int) (byte) (this.m_numElem & (int) byte.MaxValue);
          numArray6[index5] = (byte) num16;
        }
        return numArray1;
      }
      return new byte[1]{ (byte) this.m_unmanagedType };
    }
  }
}
