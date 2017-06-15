// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.DynamicILInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Reflection.Emit
{
  /// <summary>提供对多种用来为动态方法生成 Microsoft 中间语言 (MSIL) 和元数据的其他方式的支持，包括用来创建标记和插入代码的方法、异常处理，以及局部变量签名 Blob。</summary>
  [ComVisible(true)]
  public class DynamicILInfo
  {
    private DynamicMethod m_method;
    private DynamicScope m_scope;
    private byte[] m_exceptions;
    private byte[] m_code;
    private byte[] m_localSignature;
    private int m_maxStackSize;
    private int m_methodSignature;

    internal byte[] LocalSignature
    {
      get
      {
        if (this.m_localSignature == null)
          this.m_localSignature = SignatureHelper.GetLocalVarSigHelper().InternalGetSignatureArray();
        return this.m_localSignature;
      }
    }

    internal byte[] Exceptions
    {
      get
      {
        return this.m_exceptions;
      }
    }

    internal byte[] Code
    {
      get
      {
        return this.m_code;
      }
    }

    internal int MaxStackSize
    {
      get
      {
        return this.m_maxStackSize;
      }
    }

    /// <summary>获取动态方法，该方法的方法体由当前实例生成。</summary>
    /// <returns>一个 <see cref="T:System.Reflection.Emit.DynamicMethod" /> 对象，当前 <see cref="T:System.Reflection.Emit.DynamicILInfo" /> 对象正在为该对象所表示的动态方法生成代码。</returns>
    public DynamicMethod DynamicMethod
    {
      get
      {
        return this.m_method;
      }
    }

    internal DynamicScope DynamicScope
    {
      get
      {
        return this.m_scope;
      }
    }

    internal DynamicILInfo(DynamicScope scope, DynamicMethod method, byte[] methodSignature)
    {
      this.m_method = method;
      this.m_scope = scope;
      this.m_methodSignature = this.m_scope.GetTokenFor(methodSignature);
      this.m_exceptions = EmptyArray<byte>.Value;
      this.m_code = EmptyArray<byte>.Value;
      this.m_localSignature = EmptyArray<byte>.Value;
    }

    [SecurityCritical]
    internal void GetCallableMethod(RuntimeModule module, DynamicMethod dm)
    {
      dm.m_methodHandle = ModuleHandle.GetDynamicMethod(dm, module, this.m_method.Name, (byte[]) this.m_scope[this.m_methodSignature], (Resolver) new DynamicResolver(this));
    }

    /// <summary>设置关联动态方法的代码体。</summary>
    /// <param name="code">包含 MSIL 流的数组。</param>
    /// <param name="maxStackSize">执行方法时操作数堆栈上的项的最大数目。</param>
    public void SetCode(byte[] code, int maxStackSize)
    {
      this.m_code = code != null ? (byte[]) code.Clone() : EmptyArray<byte>.Value;
      this.m_maxStackSize = maxStackSize;
    }

    /// <summary>设置关联动态方法的代码体。</summary>
    /// <param name="code">指向包含 MSIL 流的字节数组的指针。</param>
    /// <param name="codeSize">MSIL 流中的字节数。</param>
    /// <param name="maxStackSize">执行方法时操作数堆栈上的项的最大数目。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="code" /> 为 null 且 <paramref name="codeSize" /> 大于零。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="codeSize" /> 小于 0。</exception>
    [SecurityCritical]
    [CLSCompliant(false)]
    public unsafe void SetCode(byte* code, int codeSize, int maxStackSize)
    {
      if (codeSize < 0)
        throw new ArgumentOutOfRangeException("codeSize", Environment.GetResourceString("ArgumentOutOfRange_GenericPositive"));
      if (codeSize > 0 && (IntPtr) code == IntPtr.Zero)
        throw new ArgumentNullException("code");
      this.m_code = new byte[codeSize];
      for (int index = 0; index < codeSize; ++index)
      {
        this.m_code[index] = *code;
        ++code;
      }
      this.m_maxStackSize = maxStackSize;
    }

    /// <summary>设置关联动态方法的异常元数据。</summary>
    /// <param name="exceptions">包含异常元数据的数组。</param>
    public void SetExceptions(byte[] exceptions)
    {
      this.m_exceptions = exceptions != null ? (byte[]) exceptions.Clone() : EmptyArray<byte>.Value;
    }

    /// <summary>设置关联动态方法的异常元数据。</summary>
    /// <param name="exceptions">指向包含异常元数据的字节数组的指针。</param>
    /// <param name="exceptionsSize">异常元数据的字节数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="exceptions" /> 为 null 且 <paramref name="exceptionSize" /> 大于零。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="exceptionSize" /> 小于 0。</exception>
    [SecurityCritical]
    [CLSCompliant(false)]
    public unsafe void SetExceptions(byte* exceptions, int exceptionsSize)
    {
      if (exceptionsSize < 0)
        throw new ArgumentOutOfRangeException("exceptionsSize", Environment.GetResourceString("ArgumentOutOfRange_GenericPositive"));
      if (exceptionsSize > 0 && (IntPtr) exceptions == IntPtr.Zero)
        throw new ArgumentNullException("exceptions");
      this.m_exceptions = new byte[exceptionsSize];
      for (int index = 0; index < exceptionsSize; ++index)
      {
        this.m_exceptions[index] = *exceptions;
        ++exceptions;
      }
    }

    /// <summary>设置描述关联动态方法的局部变量布局的局部变量签名。</summary>
    /// <param name="localSignature">一个数组，其中包含关联 <see cref="T:System.Reflection.Emit.DynamicMethod" /> 的局部变量布局。</param>
    public void SetLocalSignature(byte[] localSignature)
    {
      this.m_localSignature = localSignature != null ? (byte[]) localSignature.Clone() : EmptyArray<byte>.Value;
    }

    /// <summary>设置描述关联动态方法的局部变量布局的局部变量签名。</summary>
    /// <param name="localSignature">一个数组，其中包含关联 <see cref="T:System.Reflection.Emit.DynamicMethod" /> 的局部变量布局。</param>
    /// <param name="signatureSize">签名中的字节数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="localSignature" /> 为 null 且 <paramref name="signatureSize" /> 大于零。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="signatureSize" /> 小于 0。</exception>
    [SecurityCritical]
    [CLSCompliant(false)]
    public unsafe void SetLocalSignature(byte* localSignature, int signatureSize)
    {
      if (signatureSize < 0)
        throw new ArgumentOutOfRangeException("signatureSize", Environment.GetResourceString("ArgumentOutOfRange_GenericPositive"));
      if (signatureSize > 0 && (IntPtr) localSignature == IntPtr.Zero)
        throw new ArgumentNullException("localSignature");
      this.m_localSignature = new byte[signatureSize];
      for (int index = 0; index < signatureSize; ++index)
      {
        this.m_localSignature[index] = *localSignature;
        ++localSignature;
      }
    }

    /// <summary>获取一个在当前 <see cref="T:System.Reflection.Emit.DynamicILInfo" /> 的范围内有效的标记，它表示将从关联动态方法访问的方法。</summary>
    /// <returns>当前 <see cref="T:System.Reflection.Emit.DynamicILInfo" /> 对象的范围内的一个标记，可以用作访问方法的 MSIL 指令的操作数（如 <see cref="F:System.Reflection.Emit.OpCodes.Call" /> 或 <see cref="F:System.Reflection.Emit.OpCodes.Ldtoken" />）。</returns>
    /// <param name="method">要访问的方法。</param>
    [SecuritySafeCritical]
    public int GetTokenFor(RuntimeMethodHandle method)
    {
      return this.DynamicScope.GetTokenFor(method);
    }

    /// <summary>获取一个在当前 <see cref="T:System.Reflection.Emit.DynamicILInfo" /> 的范围内有效的标记，它表示将从关联方法调用的动态方法。</summary>
    /// <returns>一个标记，可以嵌入关联动态方法的 MSIL 流中，作为 MSIL 指令的目标。</returns>
    /// <param name="method">要调用的动态方法。</param>
    public int GetTokenFor(DynamicMethod method)
    {
      return this.DynamicScope.GetTokenFor(method);
    }

    /// <summary>获取一个在当前 <see cref="T:System.Reflection.Emit.DynamicILInfo" /> 的范围内有效的标记，它表示泛型类型上的一个方法。</summary>
    /// <returns>当前 <see cref="T:System.Reflection.Emit.DynamicILInfo" /> 对象的范围内的一个标记，可以用作访问方法的 MSIL 指令的操作数（如 <see cref="F:System.Reflection.Emit.OpCodes.Call" /> 或 <see cref="F:System.Reflection.Emit.OpCodes.Ldtoken" />）。</returns>
    /// <param name="method">方法。</param>
    /// <param name="contextType">该方法所属的泛型类型。</param>
    public int GetTokenFor(RuntimeMethodHandle method, RuntimeTypeHandle contextType)
    {
      return this.DynamicScope.GetTokenFor(method, contextType);
    }

    /// <summary>获取一个在当前 <see cref="T:System.Reflection.Emit.DynamicILInfo" /> 的范围内有效的标记，它表示将从关联动态方法访问的字段。</summary>
    /// <returns>当前 <see cref="T:System.Reflection.Emit.DynamicILInfo" /> 对象的范围内的一个标记，可用作访问字段的 MSIL 指令的操作数。</returns>
    /// <param name="field">要访问的字段。</param>
    public int GetTokenFor(RuntimeFieldHandle field)
    {
      return this.DynamicScope.GetTokenFor(field);
    }

    /// <summary>获取一个在当前 <see cref="T:System.Reflection.Emit.DynamicILInfo" /> 的范围内有效的标记，它表示将从关联动态方法访问的字段；该字段在指定的泛型类型上。</summary>
    /// <returns>当前 <see cref="T:System.Reflection.Emit.DynamicILInfo" /> 对象的范围内的一个标记，可用作访问字段的 MSIL 指令的操作数。</returns>
    /// <param name="field">要访问的字段。</param>
    /// <param name="contextType">该字段所属的泛型类型。</param>
    public int GetTokenFor(RuntimeFieldHandle field, RuntimeTypeHandle contextType)
    {
      return this.DynamicScope.GetTokenFor(field, contextType);
    }

    /// <summary>获取一个在当前 <see cref="T:System.Reflection.Emit.DynamicILInfo" /> 的范围内有效的标记，它表示将在关联动态方法中使用的类型。</summary>
    /// <returns>当前 <see cref="T:System.Reflection.Emit.DynamicILInfo" /> 对象的范围内的一个标记，可以用作需要一个类型的 MSIL 指令的操作数。</returns>
    /// <param name="type">要使用的类型。</param>
    public int GetTokenFor(RuntimeTypeHandle type)
    {
      return this.DynamicScope.GetTokenFor(type);
    }

    /// <summary>获取一个在当前 <see cref="T:System.Reflection.Emit.DynamicILInfo" /> 的范围内有效的标记，它表示将在关联动态方法中使用的字符串。</summary>
    /// <returns>当前 <see cref="T:System.Reflection.Emit.DynamicILInfo" /> 对象的范围内的一个标记，可用作需要一个字符串的 MSIL 指令的操作数。</returns>
    /// <param name="literal">要使用的字符串。</param>
    public int GetTokenFor(string literal)
    {
      return this.DynamicScope.GetTokenFor(literal);
    }

    /// <summary>获取一个在当前 <see cref="T:System.Reflection.Emit.DynamicILInfo" /> 的范围内有效的标记，它表示关联动态方法的签名。</summary>
    /// <returns>一个标记，该标记可嵌入关联动态方法的元数据和 MSIL 流中。</returns>
    /// <param name="signature">包含签名的数组。</param>
    public int GetTokenFor(byte[] signature)
    {
      return this.DynamicScope.GetTokenFor(signature);
    }
  }
}
