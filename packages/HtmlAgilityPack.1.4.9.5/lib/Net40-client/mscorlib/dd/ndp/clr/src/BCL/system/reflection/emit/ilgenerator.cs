// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.ILGenerator
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Reflection.Emit
{
  /// <summary>生成 Microsoft 中间语言 (MSIL) 指令。</summary>
  [ClassInterface(ClassInterfaceType.None)]
  [ComDefaultInterface(typeof (_ILGenerator))]
  [ComVisible(true)]
  public class ILGenerator : _ILGenerator
  {
    private const int defaultSize = 16;
    private const int DefaultFixupArraySize = 8;
    private const int DefaultLabelArraySize = 4;
    private const int DefaultExceptionArraySize = 2;
    private int m_length;
    private byte[] m_ILStream;
    private int[] m_labelList;
    private int m_labelCount;
    private __FixupData[] m_fixupData;
    private int m_fixupCount;
    private int[] m_RelocFixupList;
    private int m_RelocFixupCount;
    private int m_exceptionCount;
    private int m_currExcStackCount;
    private __ExceptionInfo[] m_exceptions;
    private __ExceptionInfo[] m_currExcStack;
    internal ScopeTree m_ScopeTree;
    internal LineNumberInfo m_LineNumberInfo;
    internal MethodInfo m_methodBuilder;
    internal int m_localCount;
    internal SignatureHelper m_localSignature;
    private int m_maxStackSize;
    private int m_maxMidStack;
    private int m_maxMidStackCur;

    internal int CurrExcStackCount
    {
      get
      {
        return this.m_currExcStackCount;
      }
    }

    internal __ExceptionInfo[] CurrExcStack
    {
      get
      {
        return this.m_currExcStack;
      }
    }

    /// <summary>获取由 <see cref="T:System.Reflection.Emit.ILGenerator" /> 发出的 Microsoft 中间语言 (MSIL) 流中的当前偏移量（以字节为单位）。</summary>
    /// <returns>MSIL 流中的偏移量，将在此处发出下一个指令。</returns>
    public virtual int ILOffset
    {
      get
      {
        return this.m_length;
      }
    }

    internal ILGenerator(MethodInfo methodBuilder)
      : this(methodBuilder, 64)
    {
    }

    internal ILGenerator(MethodInfo methodBuilder, int size)
    {
      this.m_ILStream = size >= 16 ? new byte[size] : new byte[16];
      this.m_length = 0;
      this.m_labelCount = 0;
      this.m_fixupCount = 0;
      this.m_labelList = (int[]) null;
      this.m_fixupData = (__FixupData[]) null;
      this.m_exceptions = (__ExceptionInfo[]) null;
      this.m_exceptionCount = 0;
      this.m_currExcStack = (__ExceptionInfo[]) null;
      this.m_currExcStackCount = 0;
      this.m_RelocFixupList = (int[]) null;
      this.m_RelocFixupCount = 0;
      this.m_ScopeTree = new ScopeTree();
      this.m_LineNumberInfo = new LineNumberInfo();
      this.m_methodBuilder = methodBuilder;
      this.m_localCount = 0;
      MethodBuilder methodBuilder1 = this.m_methodBuilder as MethodBuilder;
      if ((MethodInfo) methodBuilder1 == (MethodInfo) null)
        this.m_localSignature = SignatureHelper.GetLocalVarSigHelper((Module) null);
      else
        this.m_localSignature = SignatureHelper.GetLocalVarSigHelper(methodBuilder1.GetTypeBuilder().Module);
    }

    internal static int[] EnlargeArray(int[] incoming)
    {
      int[] numArray = new int[incoming.Length * 2];
      Array.Copy((Array) incoming, (Array) numArray, incoming.Length);
      return numArray;
    }

    private static byte[] EnlargeArray(byte[] incoming)
    {
      byte[] numArray = new byte[incoming.Length * 2];
      Array.Copy((Array) incoming, (Array) numArray, incoming.Length);
      return numArray;
    }

    private static byte[] EnlargeArray(byte[] incoming, int requiredSize)
    {
      byte[] numArray = new byte[requiredSize];
      Array.Copy((Array) incoming, (Array) numArray, incoming.Length);
      return numArray;
    }

    private static __FixupData[] EnlargeArray(__FixupData[] incoming)
    {
      __FixupData[] fixupDataArray = new __FixupData[incoming.Length * 2];
      Array.Copy((Array) incoming, (Array) fixupDataArray, incoming.Length);
      return fixupDataArray;
    }

    private static __ExceptionInfo[] EnlargeArray(__ExceptionInfo[] incoming)
    {
      __ExceptionInfo[] exceptionInfoArray = new __ExceptionInfo[incoming.Length * 2];
      Array.Copy((Array) incoming, (Array) exceptionInfoArray, incoming.Length);
      return exceptionInfoArray;
    }

    internal virtual void RecordTokenFixup()
    {
      if (this.m_RelocFixupList == null)
        this.m_RelocFixupList = new int[8];
      else if (this.m_RelocFixupList.Length <= this.m_RelocFixupCount)
        this.m_RelocFixupList = ILGenerator.EnlargeArray(this.m_RelocFixupList);
      int[] numArray = this.m_RelocFixupList;
      int num1 = this.m_RelocFixupCount;
      this.m_RelocFixupCount = num1 + 1;
      int index = num1;
      int num2 = this.m_length;
      numArray[index] = num2;
    }

    internal void InternalEmit(OpCode opcode)
    {
      if (opcode.Size != 1)
      {
        byte[] numArray = this.m_ILStream;
        int num1 = this.m_length;
        this.m_length = num1 + 1;
        int index = num1;
        int num2 = (int) (byte) ((uint) opcode.Value >> 8);
        numArray[index] = (byte) num2;
      }
      byte[] numArray1 = this.m_ILStream;
      int num3 = this.m_length;
      this.m_length = num3 + 1;
      int index1 = num3;
      int num4 = (int) (byte) opcode.Value;
      numArray1[index1] = (byte) num4;
      this.UpdateStackSize(opcode, opcode.StackChange());
    }

    internal void UpdateStackSize(OpCode opcode, int stackchange)
    {
      this.m_maxMidStackCur = this.m_maxMidStackCur + stackchange;
      if (this.m_maxMidStackCur > this.m_maxMidStack)
        this.m_maxMidStack = this.m_maxMidStackCur;
      else if (this.m_maxMidStackCur < 0)
        this.m_maxMidStackCur = 0;
      if (!opcode.EndsUncondJmpBlk())
        return;
      this.m_maxStackSize = this.m_maxStackSize + this.m_maxMidStack;
      this.m_maxMidStack = 0;
      this.m_maxMidStackCur = 0;
    }

    [SecurityCritical]
    private int GetMethodToken(MethodBase method, Type[] optionalParameterTypes, bool useMethodDef)
    {
      return ((ModuleBuilder) this.m_methodBuilder.Module).GetMethodTokenInternal(method, (IEnumerable<Type>) optionalParameterTypes, useMethodDef);
    }

    [SecurityCritical]
    internal virtual SignatureHelper GetMemberRefSignature(CallingConventions call, Type returnType, Type[] parameterTypes, Type[] optionalParameterTypes)
    {
      return this.GetMemberRefSignature(call, returnType, parameterTypes, optionalParameterTypes, 0);
    }

    [SecurityCritical]
    private SignatureHelper GetMemberRefSignature(CallingConventions call, Type returnType, Type[] parameterTypes, Type[] optionalParameterTypes, int cGenericParameters)
    {
      return ((ModuleBuilder) this.m_methodBuilder.Module).GetMemberRefSignature(call, returnType, parameterTypes, (IEnumerable<Type>) optionalParameterTypes, cGenericParameters);
    }

    internal byte[] BakeByteArray()
    {
      if (this.m_currExcStackCount != 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_UnclosedExceptionBlock"));
      if (this.m_length == 0)
        return (byte[]) null;
      int length = this.m_length;
      byte[] array = new byte[length];
      Array.Copy((Array) this.m_ILStream, (Array) array, length);
      for (int index = 0; index < this.m_fixupCount; ++index)
      {
        int num = this.GetLabelPos(this.m_fixupData[index].m_fixupLabel) - (this.m_fixupData[index].m_fixupPos + this.m_fixupData[index].m_fixupInstSize);
        if (this.m_fixupData[index].m_fixupInstSize == 1)
        {
          if (num < (int) sbyte.MinValue || num > (int) sbyte.MaxValue)
            throw new NotSupportedException(Environment.GetResourceString("NotSupported_IllegalOneByteBranch", (object) this.m_fixupData[index].m_fixupPos, (object) num));
          array[this.m_fixupData[index].m_fixupPos] = num >= 0 ? (byte) num : (byte) (256 + num);
        }
        else
          ILGenerator.PutInteger4InArray(num, this.m_fixupData[index].m_fixupPos, array);
      }
      return array;
    }

    internal __ExceptionInfo[] GetExceptions()
    {
      if (this.m_currExcStackCount != 0)
        throw new NotSupportedException(Environment.GetResourceString("Argument_UnclosedExceptionBlock"));
      if (this.m_exceptionCount == 0)
        return (__ExceptionInfo[]) null;
      __ExceptionInfo[] exceptions = new __ExceptionInfo[this.m_exceptionCount];
      Array.Copy((Array) this.m_exceptions, (Array) exceptions, this.m_exceptionCount);
      ILGenerator.SortExceptions(exceptions);
      return exceptions;
    }

    internal void EnsureCapacity(int size)
    {
      if (this.m_length + size < this.m_ILStream.Length)
        return;
      if (this.m_length + size >= 2 * this.m_ILStream.Length)
        this.m_ILStream = ILGenerator.EnlargeArray(this.m_ILStream, this.m_length + size);
      else
        this.m_ILStream = ILGenerator.EnlargeArray(this.m_ILStream);
    }

    internal void PutInteger4(int value)
    {
      this.m_length = ILGenerator.PutInteger4InArray(value, this.m_length, this.m_ILStream);
    }

    private static int PutInteger4InArray(int value, int startPos, byte[] array)
    {
      array[startPos++] = (byte) value;
      array[startPos++] = (byte) (value >> 8);
      array[startPos++] = (byte) (value >> 16);
      array[startPos++] = (byte) (value >> 24);
      return startPos;
    }

    private int GetLabelPos(Label lbl)
    {
      int labelValue = lbl.GetLabelValue();
      if (labelValue < 0 || labelValue >= this.m_labelCount)
        throw new ArgumentException(Environment.GetResourceString("Argument_BadLabel"));
      if (this.m_labelList[labelValue] < 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_BadLabelContent"));
      return this.m_labelList[labelValue];
    }

    private void AddFixup(Label lbl, int pos, int instSize)
    {
      if (this.m_fixupData == null)
        this.m_fixupData = new __FixupData[8];
      else if (this.m_fixupData.Length <= this.m_fixupCount)
        this.m_fixupData = ILGenerator.EnlargeArray(this.m_fixupData);
      this.m_fixupData[this.m_fixupCount].m_fixupPos = pos;
      this.m_fixupData[this.m_fixupCount].m_fixupLabel = lbl;
      this.m_fixupData[this.m_fixupCount].m_fixupInstSize = instSize;
      this.m_fixupCount = this.m_fixupCount + 1;
    }

    internal int GetMaxStackSize()
    {
      return this.m_maxStackSize;
    }

    private static void SortExceptions(__ExceptionInfo[] exceptions)
    {
      int length = exceptions.Length;
      for (int index1 = 0; index1 < length; ++index1)
      {
        int index2 = index1;
        for (int index3 = index1 + 1; index3 < length; ++index3)
        {
          if (exceptions[index2].IsInner(exceptions[index3]))
            index2 = index3;
        }
        __ExceptionInfo exceptionInfo = exceptions[index1];
        exceptions[index1] = exceptions[index2];
        exceptions[index2] = exceptionInfo;
      }
    }

    internal int[] GetTokenFixups()
    {
      if (this.m_RelocFixupCount == 0)
        return (int[]) null;
      int[] numArray = new int[this.m_RelocFixupCount];
      Array.Copy((Array) this.m_RelocFixupList, (Array) numArray, this.m_RelocFixupCount);
      return numArray;
    }

    /// <summary>将指定的指令放到指令流上。</summary>
    /// <param name="opcode">要放到流上的 Microsoft 中间语言 (MSIL) 指令。</param>
    public virtual void Emit(OpCode opcode)
    {
      this.EnsureCapacity(3);
      this.InternalEmit(opcode);
    }

    /// <summary>将指定的指令和字符参数放在 Microsoft 中间语言 (MSIL) 指令流上。</summary>
    /// <param name="opcode">要放到流上的 MSIL 指令。</param>
    /// <param name="arg">紧接着该指令推到流中的字符参数。</param>
    public virtual void Emit(OpCode opcode, byte arg)
    {
      this.EnsureCapacity(4);
      this.InternalEmit(opcode);
      byte[] numArray = this.m_ILStream;
      int num1 = this.m_length;
      this.m_length = num1 + 1;
      int index = num1;
      int num2 = (int) arg;
      numArray[index] = (byte) num2;
    }

    /// <summary>将指定的指令和字符参数放在 Microsoft 中间语言 (MSIL) 指令流上。</summary>
    /// <param name="opcode">要放到流上的 MSIL 指令。</param>
    /// <param name="arg">紧接着该指令推到流中的字符参数。</param>
    [CLSCompliant(false)]
    public void Emit(OpCode opcode, sbyte arg)
    {
      this.EnsureCapacity(4);
      this.InternalEmit(opcode);
      if ((int) arg < 0)
      {
        byte[] numArray = this.m_ILStream;
        int num1 = this.m_length;
        this.m_length = num1 + 1;
        int index = num1;
        int num2 = (int) (byte) (256U + (uint) arg);
        numArray[index] = (byte) num2;
      }
      else
      {
        byte[] numArray = this.m_ILStream;
        int num1 = this.m_length;
        this.m_length = num1 + 1;
        int index = num1;
        int num2 = (int) (byte) arg;
        numArray[index] = (byte) num2;
      }
    }

    /// <summary>将指定的指令和数值参数放在 Microsoft 中间语言 (MSIL) 指令流上。</summary>
    /// <param name="opcode">要发到流中的 MSIL 指令。</param>
    /// <param name="arg">紧接着该指令推到流中的 Int 参数。</param>
    public virtual void Emit(OpCode opcode, short arg)
    {
      this.EnsureCapacity(5);
      this.InternalEmit(opcode);
      byte[] numArray1 = this.m_ILStream;
      int num1 = this.m_length;
      this.m_length = num1 + 1;
      int index1 = num1;
      int num2 = (int) (byte) arg;
      numArray1[index1] = (byte) num2;
      byte[] numArray2 = this.m_ILStream;
      int num3 = this.m_length;
      this.m_length = num3 + 1;
      int index2 = num3;
      int num4 = (int) (byte) ((uint) arg >> 8);
      numArray2[index2] = (byte) num4;
    }

    /// <summary>将指定的指令和数值参数放在 Microsoft 中间语言 (MSIL) 指令流上。</summary>
    /// <param name="opcode">要放到流上的 MSIL 指令。</param>
    /// <param name="arg">紧接着该指令推到流中的数字参数。</param>
    public virtual void Emit(OpCode opcode, int arg)
    {
      this.EnsureCapacity(7);
      this.InternalEmit(opcode);
      this.PutInteger4(arg);
    }

    /// <summary>将指定的指令放到 Microsoft 中间语言 (MSIL) 流上，后跟给定方法的元数据标记。</summary>
    /// <param name="opcode">要发到流中的 MSIL 指令。</param>
    /// <param name="meth">表示方法的 MethodInfo。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="meth" /> 为 null。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="meth" /> 为泛型方法，其 <see cref="P:System.Reflection.MethodInfo.IsGenericMethodDefinition" /> 属性为 false。</exception>
    [SecuritySafeCritical]
    public virtual void Emit(OpCode opcode, MethodInfo meth)
    {
      if (meth == (MethodInfo) null)
        throw new ArgumentNullException("meth");
      if (opcode.Equals(OpCodes.Call) || opcode.Equals(OpCodes.Callvirt) || opcode.Equals(OpCodes.Newobj))
      {
        this.EmitCall(opcode, meth, (Type[]) null);
      }
      else
      {
        int stackchange = 0;
        bool useMethodDef = opcode.Equals(OpCodes.Ldtoken) || opcode.Equals(OpCodes.Ldftn) || opcode.Equals(OpCodes.Ldvirtftn);
        int methodToken = this.GetMethodToken((MethodBase) meth, (Type[]) null, useMethodDef);
        this.EnsureCapacity(7);
        this.InternalEmit(opcode);
        this.UpdateStackSize(opcode, stackchange);
        this.RecordTokenFixup();
        this.PutInteger4(methodToken);
      }
    }

    /// <summary>将 <see cref="F:System.Reflection.Emit.OpCodes.Calli" /> 指令放到 Microsoft 中间语言 (MSIL) 流，并指定间接调用的托管调用约定。</summary>
    /// <param name="opcode">要发到流中的 MSIL 指令。必须为 <see cref="F:System.Reflection.Emit.OpCodes.Calli" />。</param>
    /// <param name="callingConvention">要使用的托管调用约定。</param>
    /// <param name="returnType">结果的 <see cref="T:System.Type" />。</param>
    /// <param name="parameterTypes">指令的必选参数的类型。</param>
    /// <param name="optionalParameterTypes">varargs 调用的可选参数的类型。</param>
    /// <exception cref="T:System.InvalidOperationException">
    /// <paramref name="optionalParameterTypes" /> 不为 null，但 <paramref name="callingConvention" /> 不包括 <see cref="F:System.Reflection.CallingConventions.VarArgs" /> 标志。</exception>
    [SecuritySafeCritical]
    public virtual void EmitCalli(OpCode opcode, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, Type[] optionalParameterTypes)
    {
      int num = 0;
      if (optionalParameterTypes != null && (callingConvention & CallingConventions.VarArgs) == (CallingConventions) 0)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotAVarArgCallingConvention"));
      ModuleBuilder moduleBuilder = (ModuleBuilder) this.m_methodBuilder.Module;
      SignatureHelper memberRefSignature = this.GetMemberRefSignature(callingConvention, returnType, parameterTypes, optionalParameterTypes);
      this.EnsureCapacity(7);
      this.Emit(OpCodes.Calli);
      if (returnType != typeof (void))
        ++num;
      if (parameterTypes != null)
        num -= parameterTypes.Length;
      if (optionalParameterTypes != null)
        num -= optionalParameterTypes.Length;
      if ((callingConvention & CallingConventions.HasThis) == CallingConventions.HasThis)
        --num;
      int stackchange = num - 1;
      this.UpdateStackSize(OpCodes.Calli, stackchange);
      this.RecordTokenFixup();
      this.PutInteger4(moduleBuilder.GetSignatureToken(memberRefSignature).Token);
    }

    /// <summary>将 <see cref="F:System.Reflection.Emit.OpCodes.Calli" /> 指令放到 Microsoft 中间语言 (MSIL) 流，并指定间接调用的非托管调用约定。</summary>
    /// <param name="opcode">要发到流中的 MSIL 指令。必须为 <see cref="F:System.Reflection.Emit.OpCodes.Calli" />。</param>
    /// <param name="unmanagedCallConv">要使用的非托管调用约定。</param>
    /// <param name="returnType">结果的 <see cref="T:System.Type" />。</param>
    /// <param name="parameterTypes">指令的必选参数的类型。</param>
    public virtual void EmitCalli(OpCode opcode, CallingConvention unmanagedCallConv, Type returnType, Type[] parameterTypes)
    {
      int num1 = 0;
      int num2 = 0;
      ModuleBuilder moduleBuilder = (ModuleBuilder) this.m_methodBuilder.Module;
      if (parameterTypes != null)
        num2 = parameterTypes.Length;
      SignatureHelper methodSigHelper = SignatureHelper.GetMethodSigHelper((Module) moduleBuilder, unmanagedCallConv, returnType);
      if (parameterTypes != null)
      {
        for (int index = 0; index < num2; ++index)
          methodSigHelper.AddArgument(parameterTypes[index]);
      }
      if (returnType != typeof (void))
        ++num1;
      if (parameterTypes != null)
        num1 -= num2;
      int stackchange = num1 - 1;
      this.UpdateStackSize(OpCodes.Calli, stackchange);
      this.EnsureCapacity(7);
      this.Emit(OpCodes.Calli);
      this.RecordTokenFixup();
      this.PutInteger4(moduleBuilder.GetSignatureToken(methodSigHelper).Token);
    }

    /// <summary>将 call 或 callvirt 指令放到 Microsoft 中间语言 (MSIL) 流上，以便调用 varargs 方法。</summary>
    /// <param name="opcode">要发到流中的 MSIL 指令。必须为 <see cref="F:System.Reflection.Emit.OpCodes.Call" />、<see cref="F:System.Reflection.Emit.OpCodes.Callvirt" /> 或 <see cref="F:System.Reflection.Emit.OpCodes.Newobj" />。</param>
    /// <param name="methodInfo">要调用的 varargs 方法。</param>
    /// <param name="optionalParameterTypes">如果该方法是 varargs 方法，则为可选参数的类型；否则为 null。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="opcode" /> 未指定方法调用。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="methodInfo" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">此方法的调用约定不是 varargs，但是提供了可选的参数类型。在 .NET Framework 1.0 版和 1.1 版中会引发此异常。在后续版本中，则不会引发任何异常。</exception>
    [SecuritySafeCritical]
    public virtual void EmitCall(OpCode opcode, MethodInfo methodInfo, Type[] optionalParameterTypes)
    {
      if (methodInfo == (MethodInfo) null)
        throw new ArgumentNullException("methodInfo");
      if (!opcode.Equals(OpCodes.Call) && !opcode.Equals(OpCodes.Callvirt) && !opcode.Equals(OpCodes.Newobj))
        throw new ArgumentException(Environment.GetResourceString("Argument_NotMethodCallOpcode"), "opcode");
      int stackchange = 0;
      int methodToken = this.GetMethodToken((MethodBase) methodInfo, optionalParameterTypes, false);
      this.EnsureCapacity(7);
      this.InternalEmit(opcode);
      if (methodInfo.ReturnType != typeof (void))
        ++stackchange;
      Type[] parameterTypes = methodInfo.GetParameterTypes();
      if (parameterTypes != null)
        stackchange -= parameterTypes.Length;
      if (!(methodInfo is SymbolMethod) && !methodInfo.IsStatic && !opcode.Equals(OpCodes.Newobj))
        --stackchange;
      if (optionalParameterTypes != null)
        stackchange -= optionalParameterTypes.Length;
      this.UpdateStackSize(opcode, stackchange);
      this.RecordTokenFixup();
      this.PutInteger4(methodToken);
    }

    /// <summary>将指定的指令和签名标记放在 Microsoft 中间语言 (MSIL) 指令流上。</summary>
    /// <param name="opcode">要发到流中的 MSIL 指令。</param>
    /// <param name="signature">用于构造签名标记的帮助器。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="signature" /> 为 null。</exception>
    public virtual void Emit(OpCode opcode, SignatureHelper signature)
    {
      if (signature == null)
        throw new ArgumentNullException("signature");
      int num = 0;
      int token = ((ModuleBuilder) this.m_methodBuilder.Module).GetSignatureToken(signature).Token;
      this.EnsureCapacity(7);
      this.InternalEmit(opcode);
      if (opcode.StackBehaviourPop == StackBehaviour.Varpop)
      {
        int stackchange = num - signature.ArgumentCount - 1;
        this.UpdateStackSize(opcode, stackchange);
      }
      this.RecordTokenFixup();
      this.PutInteger4(token);
    }

    /// <summary>将指定构造函数的指定指令和元数据标记放到 Microsoft 中间语言 (MSIL) 指令流上。</summary>
    /// <param name="opcode">要发到流中的 MSIL 指令。</param>
    /// <param name="con">表示构造函数的 ConstructorInfo。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="con" /> 为 null。此异常是 .NET Framework 4 中新出现的。</exception>
    [SecuritySafeCritical]
    [ComVisible(true)]
    public virtual void Emit(OpCode opcode, ConstructorInfo con)
    {
      if (con == (ConstructorInfo) null)
        throw new ArgumentNullException("con");
      int stackchange = 0;
      int methodToken = this.GetMethodToken((MethodBase) con, (Type[]) null, true);
      this.EnsureCapacity(7);
      this.InternalEmit(opcode);
      if (opcode.StackBehaviourPush == StackBehaviour.Varpush)
        ++stackchange;
      if (opcode.StackBehaviourPop == StackBehaviour.Varpop)
      {
        Type[] parameterTypes = con.GetParameterTypes();
        if (parameterTypes != null)
          stackchange -= parameterTypes.Length;
      }
      this.UpdateStackSize(opcode, stackchange);
      this.RecordTokenFixup();
      this.PutInteger4(methodToken);
    }

    /// <summary>将指定的指令放到 Microsoft 中间语言 (MSIL) 流上，后跟给定类型的元数据标记。</summary>
    /// <param name="opcode">要放到流上的 MSIL 指令。</param>
    /// <param name="cls">Type。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="cls" /> 为 null。</exception>
    [SecuritySafeCritical]
    public virtual void Emit(OpCode opcode, Type cls)
    {
      ModuleBuilder moduleBuilder = (ModuleBuilder) this.m_methodBuilder.Module;
      int num = !(opcode == OpCodes.Ldtoken) || !(cls != (Type) null) || !cls.IsGenericTypeDefinition ? moduleBuilder.GetTypeTokenInternal(cls).Token : moduleBuilder.GetTypeToken(cls).Token;
      this.EnsureCapacity(7);
      this.InternalEmit(opcode);
      this.RecordTokenFixup();
      this.PutInteger4(num);
    }

    /// <summary>将指定的指令和数值参数放在 Microsoft 中间语言 (MSIL) 指令流上。</summary>
    /// <param name="opcode">要放到流上的 MSIL 指令。</param>
    /// <param name="arg">紧接着该指令推到流中的数字参数。</param>
    public virtual void Emit(OpCode opcode, long arg)
    {
      this.EnsureCapacity(11);
      this.InternalEmit(opcode);
      byte[] numArray1 = this.m_ILStream;
      int num1 = this.m_length;
      this.m_length = num1 + 1;
      int index1 = num1;
      int num2 = (int) (byte) arg;
      numArray1[index1] = (byte) num2;
      byte[] numArray2 = this.m_ILStream;
      int num3 = this.m_length;
      this.m_length = num3 + 1;
      int index2 = num3;
      int num4 = (int) (byte) (arg >> 8);
      numArray2[index2] = (byte) num4;
      byte[] numArray3 = this.m_ILStream;
      int num5 = this.m_length;
      this.m_length = num5 + 1;
      int index3 = num5;
      int num6 = (int) (byte) (arg >> 16);
      numArray3[index3] = (byte) num6;
      byte[] numArray4 = this.m_ILStream;
      int num7 = this.m_length;
      this.m_length = num7 + 1;
      int index4 = num7;
      int num8 = (int) (byte) (arg >> 24);
      numArray4[index4] = (byte) num8;
      byte[] numArray5 = this.m_ILStream;
      int num9 = this.m_length;
      this.m_length = num9 + 1;
      int index5 = num9;
      int num10 = (int) (byte) (arg >> 32);
      numArray5[index5] = (byte) num10;
      byte[] numArray6 = this.m_ILStream;
      int num11 = this.m_length;
      this.m_length = num11 + 1;
      int index6 = num11;
      int num12 = (int) (byte) (arg >> 40);
      numArray6[index6] = (byte) num12;
      byte[] numArray7 = this.m_ILStream;
      int num13 = this.m_length;
      this.m_length = num13 + 1;
      int index7 = num13;
      int num14 = (int) (byte) (arg >> 48);
      numArray7[index7] = (byte) num14;
      byte[] numArray8 = this.m_ILStream;
      int num15 = this.m_length;
      this.m_length = num15 + 1;
      int index8 = num15;
      int num16 = (int) (byte) (arg >> 56);
      numArray8[index8] = (byte) num16;
    }

    /// <summary>将指定的指令和数值参数放在 Microsoft 中间语言 (MSIL) 指令流上。</summary>
    /// <param name="opcode">要放到流上的 MSIL 指令。</param>
    /// <param name="arg">紧接着该指令推到流上的 Single 参数。</param>
    [SecuritySafeCritical]
    public virtual unsafe void Emit(OpCode opcode, float arg)
    {
      this.EnsureCapacity(7);
      this.InternalEmit(opcode);
      uint num1 = *(uint*) &arg;
      byte[] numArray1 = this.m_ILStream;
      int num2 = this.m_length;
      this.m_length = num2 + 1;
      int index1 = num2;
      int num3 = (int) (byte) num1;
      numArray1[index1] = (byte) num3;
      byte[] numArray2 = this.m_ILStream;
      int num4 = this.m_length;
      this.m_length = num4 + 1;
      int index2 = num4;
      int num5 = (int) (byte) (num1 >> 8);
      numArray2[index2] = (byte) num5;
      byte[] numArray3 = this.m_ILStream;
      int num6 = this.m_length;
      this.m_length = num6 + 1;
      int index3 = num6;
      int num7 = (int) (byte) (num1 >> 16);
      numArray3[index3] = (byte) num7;
      byte[] numArray4 = this.m_ILStream;
      int num8 = this.m_length;
      this.m_length = num8 + 1;
      int index4 = num8;
      int num9 = (int) (byte) (num1 >> 24);
      numArray4[index4] = (byte) num9;
    }

    /// <summary>将指定的指令和数值参数放在 Microsoft 中间语言 (MSIL) 指令流上。</summary>
    /// <param name="opcode">要放到流上的 MSIL 指令。在 OpCodes 枚举中定义。</param>
    /// <param name="arg">紧接着该指令推到流中的数字参数。</param>
    [SecuritySafeCritical]
    public virtual unsafe void Emit(OpCode opcode, double arg)
    {
      this.EnsureCapacity(11);
      this.InternalEmit(opcode);
      ulong num1 = (ulong) *(long*) &arg;
      byte[] numArray1 = this.m_ILStream;
      int num2 = this.m_length;
      this.m_length = num2 + 1;
      int index1 = num2;
      int num3 = (int) (byte) num1;
      numArray1[index1] = (byte) num3;
      byte[] numArray2 = this.m_ILStream;
      int num4 = this.m_length;
      this.m_length = num4 + 1;
      int index2 = num4;
      int num5 = (int) (byte) (num1 >> 8);
      numArray2[index2] = (byte) num5;
      byte[] numArray3 = this.m_ILStream;
      int num6 = this.m_length;
      this.m_length = num6 + 1;
      int index3 = num6;
      int num7 = (int) (byte) (num1 >> 16);
      numArray3[index3] = (byte) num7;
      byte[] numArray4 = this.m_ILStream;
      int num8 = this.m_length;
      this.m_length = num8 + 1;
      int index4 = num8;
      int num9 = (int) (byte) (num1 >> 24);
      numArray4[index4] = (byte) num9;
      byte[] numArray5 = this.m_ILStream;
      int num10 = this.m_length;
      this.m_length = num10 + 1;
      int index5 = num10;
      int num11 = (int) (byte) (num1 >> 32);
      numArray5[index5] = (byte) num11;
      byte[] numArray6 = this.m_ILStream;
      int num12 = this.m_length;
      this.m_length = num12 + 1;
      int index6 = num12;
      int num13 = (int) (byte) (num1 >> 40);
      numArray6[index6] = (byte) num13;
      byte[] numArray7 = this.m_ILStream;
      int num14 = this.m_length;
      this.m_length = num14 + 1;
      int index7 = num14;
      int num15 = (int) (byte) (num1 >> 48);
      numArray7[index7] = (byte) num15;
      byte[] numArray8 = this.m_ILStream;
      int num16 = this.m_length;
      this.m_length = num16 + 1;
      int index8 = num16;
      int num17 = (int) (byte) (num1 >> 56);
      numArray8[index8] = (byte) num17;
    }

    /// <summary>将指定的指令放在 Microsoft 中间语言 (MSIL) 流上，并留出在完成修正时加上标签所需的空白。</summary>
    /// <param name="opcode">要发到流中的 MSIL 指令。</param>
    /// <param name="label">从此位置分支到的标签。</param>
    public virtual void Emit(OpCode opcode, Label label)
    {
      label.GetLabelValue();
      this.EnsureCapacity(7);
      this.InternalEmit(opcode);
      if (OpCodes.TakesSingleByteArgument(opcode))
      {
        this.AddFixup(label, this.m_length, 1);
        this.m_length = this.m_length + 1;
      }
      else
      {
        this.AddFixup(label, this.m_length, 4);
        this.m_length = this.m_length + 4;
      }
    }

    /// <summary>将指定的指令放在 Microsoft 中间语言 (MSIL) 流上，并留出在完成修正时加上标签所需的空白。</summary>
    /// <param name="opcode">要发到流中的 MSIL 指令。</param>
    /// <param name="labels">从此位置分支到的标签对象的数组。将使用所有标签。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="con" /> 为 null。此异常是 .NET Framework 4 中新出现的。</exception>
    public virtual void Emit(OpCode opcode, Label[] labels)
    {
      if (labels == null)
        throw new ArgumentNullException("labels");
      int length = labels.Length;
      this.EnsureCapacity(length * 4 + 7);
      this.InternalEmit(opcode);
      this.PutInteger4(length);
      int instSize = length * 4;
      int index = 0;
      while (instSize > 0)
      {
        this.AddFixup(labels[index], this.m_length, instSize);
        this.m_length = this.m_length + 4;
        instSize -= 4;
        ++index;
      }
    }

    /// <summary>将指定字段的指定指令和元数据标记放到 Microsoft 中间语言 (MSIL) 指令流上。</summary>
    /// <param name="opcode">要发到流中的 MSIL 指令。</param>
    /// <param name="field">表示字段的 FieldInfo。</param>
    public virtual void Emit(OpCode opcode, FieldInfo field)
    {
      int token = ((ModuleBuilder) this.m_methodBuilder.Module).GetFieldToken(field).Token;
      this.EnsureCapacity(7);
      this.InternalEmit(opcode);
      this.RecordTokenFixup();
      this.PutInteger4(token);
    }

    /// <summary>将指定的指令放到 Microsoft 中间语言 (MSIL) 流上，后跟给定字符串的元数据标记。</summary>
    /// <param name="opcode">要发到流中的 MSIL 指令。</param>
    /// <param name="str">要发出的 String。</param>
    public virtual void Emit(OpCode opcode, string str)
    {
      int token = ((ModuleBuilder) this.m_methodBuilder.Module).GetStringConstant(str).Token;
      this.EnsureCapacity(7);
      this.InternalEmit(opcode);
      this.PutInteger4(token);
    }

    /// <summary>将指定的指令放到 Microsoft 中间语言 (MSIL) 流上，后跟给定局部变量的索引。</summary>
    /// <param name="opcode">要发到流中的 MSIL 指令。</param>
    /// <param name="local">局部变量。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="local" /> 参数的父方法与此 <see cref="T:System.Reflection.Emit.ILGenerator" /> 关联的方法不匹配。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="local" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// <paramref name="opcode" /> 是单字节指令，并且 <paramref name="local" /> 表示索引大于 Byte.MaxValue 的局部变量。</exception>
    public virtual void Emit(OpCode opcode, LocalBuilder local)
    {
      if (local == null)
        throw new ArgumentNullException("local");
      int localIndex = local.GetLocalIndex();
      if (local.GetMethodBuilder() != this.m_methodBuilder)
        throw new ArgumentException(Environment.GetResourceString("Argument_UnmatchedMethodForLocal"), "local");
      if (opcode.Equals(OpCodes.Ldloc))
      {
        switch (localIndex)
        {
          case 0:
            opcode = OpCodes.Ldloc_0;
            break;
          case 1:
            opcode = OpCodes.Ldloc_1;
            break;
          case 2:
            opcode = OpCodes.Ldloc_2;
            break;
          case 3:
            opcode = OpCodes.Ldloc_3;
            break;
          default:
            if (localIndex <= (int) byte.MaxValue)
            {
              opcode = OpCodes.Ldloc_S;
              break;
            }
            break;
        }
      }
      else if (opcode.Equals(OpCodes.Stloc))
      {
        switch (localIndex)
        {
          case 0:
            opcode = OpCodes.Stloc_0;
            break;
          case 1:
            opcode = OpCodes.Stloc_1;
            break;
          case 2:
            opcode = OpCodes.Stloc_2;
            break;
          case 3:
            opcode = OpCodes.Stloc_3;
            break;
          default:
            if (localIndex <= (int) byte.MaxValue)
            {
              opcode = OpCodes.Stloc_S;
              break;
            }
            break;
        }
      }
      else if (opcode.Equals(OpCodes.Ldloca) && localIndex <= (int) byte.MaxValue)
        opcode = OpCodes.Ldloca_S;
      this.EnsureCapacity(7);
      this.InternalEmit(opcode);
      if (opcode.OperandType == OperandType.InlineNone)
        return;
      if (!OpCodes.TakesSingleByteArgument(opcode))
      {
        byte[] numArray1 = this.m_ILStream;
        int num1 = this.m_length;
        this.m_length = num1 + 1;
        int index1 = num1;
        int num2 = (int) (byte) localIndex;
        numArray1[index1] = (byte) num2;
        byte[] numArray2 = this.m_ILStream;
        int num3 = this.m_length;
        this.m_length = num3 + 1;
        int index2 = num3;
        int num4 = (int) (byte) (localIndex >> 8);
        numArray2[index2] = (byte) num4;
      }
      else
      {
        if (localIndex > (int) byte.MaxValue)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_BadInstructionOrIndexOutOfBound"));
        byte[] numArray = this.m_ILStream;
        int num1 = this.m_length;
        this.m_length = num1 + 1;
        int index = num1;
        int num2 = (int) (byte) localIndex;
        numArray[index] = (byte) num2;
      }
    }

    /// <summary>开始非筛选异常的异常块。</summary>
    /// <returns>块结尾的标签。这将使您停在正确的位置执行 Finally 块或完成 Try 块。</returns>
    public virtual Label BeginExceptionBlock()
    {
      if (this.m_exceptions == null)
        this.m_exceptions = new __ExceptionInfo[2];
      if (this.m_currExcStack == null)
        this.m_currExcStack = new __ExceptionInfo[2];
      if (this.m_exceptionCount >= this.m_exceptions.Length)
        this.m_exceptions = ILGenerator.EnlargeArray(this.m_exceptions);
      if (this.m_currExcStackCount >= this.m_currExcStack.Length)
        this.m_currExcStack = ILGenerator.EnlargeArray(this.m_currExcStack);
      Label endLabel = this.DefineLabel();
      __ExceptionInfo exceptionInfo1 = new __ExceptionInfo(this.m_length, endLabel);
      __ExceptionInfo[] exceptionInfoArray1 = this.m_exceptions;
      int num1 = this.m_exceptionCount;
      this.m_exceptionCount = num1 + 1;
      int index1 = num1;
      __ExceptionInfo exceptionInfo2 = exceptionInfo1;
      exceptionInfoArray1[index1] = exceptionInfo2;
      __ExceptionInfo[] exceptionInfoArray2 = this.m_currExcStack;
      int num2 = this.m_currExcStackCount;
      this.m_currExcStackCount = num2 + 1;
      int index2 = num2;
      __ExceptionInfo exceptionInfo3 = exceptionInfo1;
      exceptionInfoArray2[index2] = exceptionInfo3;
      return endLabel;
    }

    /// <summary>结束异常块。</summary>
    /// <exception cref="T:System.InvalidOperationException">结束异常块在代码流中的意外位置出现。</exception>
    /// <exception cref="T:System.NotSupportedException">要生成的 Microsoft 中间语言 (MSIL) 当前不在异常块中。</exception>
    public virtual void EndExceptionBlock()
    {
      if (this.m_currExcStackCount == 0)
        throw new NotSupportedException(Environment.GetResourceString("Argument_NotInExceptionBlock"));
      __ExceptionInfo exceptionInfo = this.m_currExcStack[this.m_currExcStackCount - 1];
      this.m_currExcStack[this.m_currExcStackCount - 1] = (__ExceptionInfo) null;
      this.m_currExcStackCount = this.m_currExcStackCount - 1;
      Label endLabel = exceptionInfo.GetEndLabel();
      switch (exceptionInfo.GetCurrentState())
      {
        case 1:
        case 0:
          throw new InvalidOperationException(Environment.GetResourceString("Argument_BadExceptionCodeGen"));
        case 2:
          this.Emit(OpCodes.Leave, endLabel);
          break;
        case 3:
        case 4:
          this.Emit(OpCodes.Endfinally);
          break;
      }
      if (this.m_labelList[endLabel.GetLabelValue()] == -1)
        this.MarkLabel(endLabel);
      else
        this.MarkLabel(exceptionInfo.GetFinallyEndLabel());
      exceptionInfo.Done(this.m_length);
    }

    /// <summary>开始已筛选异常的异常块。</summary>
    /// <exception cref="T:System.NotSupportedException">要生成的 Microsoft 中间语言 (MSIL) 当前不在异常块中。- 或 -此 <see cref="T:System.Reflection.Emit.ILGenerator" /> 属于某个 <see cref="T:System.Reflection.Emit.DynamicMethod" />。</exception>
    public virtual void BeginExceptFilterBlock()
    {
      if (this.m_currExcStackCount == 0)
        throw new NotSupportedException(Environment.GetResourceString("Argument_NotInExceptionBlock"));
      __ExceptionInfo exceptionInfo = this.m_currExcStack[this.m_currExcStackCount - 1];
      Label endLabel = exceptionInfo.GetEndLabel();
      this.Emit(OpCodes.Leave, endLabel);
      int filterAddr = this.m_length;
      exceptionInfo.MarkFilterAddr(filterAddr);
    }

    /// <summary>开始 Catch 块。</summary>
    /// <param name="exceptionType">表示异常的 <see cref="T:System.Type" /> 对象。</param>
    /// <exception cref="T:System.ArgumentException">Catch 块在已筛选的异常中。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="exceptionType" /> 为 null，并且异常筛选器块没有返回一个值，该值指示在找到此 Catch 块之前一直运行 Finally 块。</exception>
    /// <exception cref="T:System.NotSupportedException">要生成的 Microsoft 中间语言 (MSIL) 当前不在异常块中。</exception>
    public virtual void BeginCatchBlock(Type exceptionType)
    {
      if (this.m_currExcStackCount == 0)
        throw new NotSupportedException(Environment.GetResourceString("Argument_NotInExceptionBlock"));
      __ExceptionInfo exceptionInfo = this.m_currExcStack[this.m_currExcStackCount - 1];
      if (exceptionInfo.GetCurrentState() == 1)
      {
        if (exceptionType != (Type) null)
          throw new ArgumentException(Environment.GetResourceString("Argument_ShouldNotSpecifyExceptionType"));
        this.Emit(OpCodes.Endfilter);
      }
      else
      {
        if (exceptionType == (Type) null)
          throw new ArgumentNullException("exceptionType");
        Label endLabel = exceptionInfo.GetEndLabel();
        this.Emit(OpCodes.Leave, endLabel);
      }
      exceptionInfo.MarkCatchAddr(this.m_length, exceptionType);
    }

    /// <summary>在 Microsoft 中间语言 (MSIL) 流中开始一个异常错误块。</summary>
    /// <exception cref="T:System.NotSupportedException">生成的 MSIL 当前不在异常块中。- 或 -此 <see cref="T:System.Reflection.Emit.ILGenerator" /> 属于某个 <see cref="T:System.Reflection.Emit.DynamicMethod" />。</exception>
    public virtual void BeginFaultBlock()
    {
      if (this.m_currExcStackCount == 0)
        throw new NotSupportedException(Environment.GetResourceString("Argument_NotInExceptionBlock"));
      __ExceptionInfo exceptionInfo = this.m_currExcStack[this.m_currExcStackCount - 1];
      Label endLabel = exceptionInfo.GetEndLabel();
      this.Emit(OpCodes.Leave, endLabel);
      int faultAddr = this.m_length;
      exceptionInfo.MarkFaultAddr(faultAddr);
    }

    /// <summary>在 Microsoft 中间语言 (MSIL) 指令流中开始一个 Finally 块。</summary>
    /// <exception cref="T:System.NotSupportedException">生成的 MSIL 当前不在异常块中。</exception>
    public virtual void BeginFinallyBlock()
    {
      if (this.m_currExcStackCount == 0)
        throw new NotSupportedException(Environment.GetResourceString("Argument_NotInExceptionBlock"));
      __ExceptionInfo exceptionInfo = this.m_currExcStack[this.m_currExcStackCount - 1];
      int currentState = exceptionInfo.GetCurrentState();
      Label endLabel = exceptionInfo.GetEndLabel();
      int num = 0;
      if (currentState != 0)
      {
        this.Emit(OpCodes.Leave, endLabel);
        num = this.m_length;
      }
      this.MarkLabel(endLabel);
      Label label = this.DefineLabel();
      Label lbl = label;
      exceptionInfo.SetFinallyEndLabel(lbl);
      this.Emit(OpCodes.Leave, label);
      if (num == 0)
        num = this.m_length;
      int finallyAddr = this.m_length;
      int endCatchAddr = num;
      exceptionInfo.MarkFinallyAddr(finallyAddr, endCatchAddr);
    }

    /// <summary>声明新标签。</summary>
    /// <returns>返回可用作分支标记的新标签。</returns>
    public virtual Label DefineLabel()
    {
      if (this.m_labelList == null)
        this.m_labelList = new int[4];
      if (this.m_labelCount >= this.m_labelList.Length)
        this.m_labelList = ILGenerator.EnlargeArray(this.m_labelList);
      this.m_labelList[this.m_labelCount] = -1;
      int label = this.m_labelCount;
      this.m_labelCount = label + 1;
      return new Label(label);
    }

    /// <summary>用给定标签标记 Microsoft 中间语言 (MSIL) 流的当前位置。</summary>
    /// <param name="loc">为其设置索引的标签。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="loc" /> 表示标签数组中的无效索引。- 或 -已定义了 <paramref name="loc" /> 的索引。</exception>
    public virtual void MarkLabel(Label loc)
    {
      int labelValue = loc.GetLabelValue();
      if (labelValue < 0 || labelValue >= this.m_labelList.Length)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidLabel"));
      if (this.m_labelList[labelValue] != -1)
        throw new ArgumentException(Environment.GetResourceString("Argument_RedefinedLabel"));
      this.m_labelList[labelValue] = this.m_length;
    }

    /// <summary>发出指令以引发异常。</summary>
    /// <param name="excType">要引发的异常类型的类。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="excType" /> 不是 <see cref="T:System.Exception" /> 类或 <see cref="T:System.Exception" /> 的派生类。- 或 -此类型没有默认的构造函数。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="excType" /> 为 null。</exception>
    public virtual void ThrowException(Type excType)
    {
      if (excType == (Type) null)
        throw new ArgumentNullException("excType");
      if (!excType.IsSubclassOf(typeof (Exception)) && excType != typeof (Exception))
        throw new ArgumentException(Environment.GetResourceString("Argument_NotExceptionType"));
      ConstructorInfo constructor = excType.GetConstructor(Type.EmptyTypes);
      if (constructor == (ConstructorInfo) null)
        throw new ArgumentException(Environment.GetResourceString("Argument_MissingDefaultConstructor"));
      this.Emit(OpCodes.Newobj, constructor);
      this.Emit(OpCodes.Throw);
    }

    /// <summary>发出 Microsoft 中间语言 (MSIL) 以用字符串调用 <see cref="Overload:System.Console.WriteLine" />。</summary>
    /// <param name="value">要打印的字符串。</param>
    public virtual void EmitWriteLine(string value)
    {
      this.Emit(OpCodes.Ldstr, value);
      MethodInfo method = typeof (Console).GetMethod("WriteLine", new Type[1]{ typeof (string) });
      this.Emit(OpCodes.Call, method);
    }

    /// <summary>发出用给定局部变量调用 <see cref="Overload:System.Console.WriteLine" /> 所需的 Microsoft 中间语言 (MSIL)。</summary>
    /// <param name="localBuilder">其值要被写到控制台的局部变量。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="localBuilder" /> 的类型为 <see cref="T:System.Reflection.Emit.TypeBuilder" /> 或 <see cref="T:System.Reflection.Emit.EnumBuilder" />，这两种类型都不受支持。- 或 -不存在接受 <paramref name="localBuilder" /> 的类型的 <see cref="Overload:System.Console.WriteLine" /> 重载。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="localBuilder" /> 为 null。</exception>
    public virtual void EmitWriteLine(LocalBuilder localBuilder)
    {
      if (this.m_methodBuilder == (MethodInfo) null)
        throw new ArgumentException(Environment.GetResourceString("InvalidOperation_BadILGeneratorUsage"));
      MethodInfo method1 = typeof (Console).GetMethod("get_Out");
      this.Emit(OpCodes.Call, method1);
      this.Emit(OpCodes.Ldloc, localBuilder);
      Type[] types = new Type[1];
      object obj = (object) localBuilder.LocalType;
      if (obj is TypeBuilder || obj is EnumBuilder)
        throw new ArgumentException(Environment.GetResourceString("NotSupported_OutputStreamUsingTypeBuilder"));
      types[0] = (Type) obj;
      MethodInfo method2 = typeof (TextWriter).GetMethod("WriteLine", types);
      if (method2 == (MethodInfo) null)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmitWriteLineType"), "localBuilder");
      this.Emit(OpCodes.Callvirt, method2);
    }

    /// <summary>发出用给定字段调用 <see cref="Overload:System.Console.WriteLine" /> 所需的 Microsoft 中间语言 (MSIL)。</summary>
    /// <param name="fld">其值要被写到控制台的字段。</param>
    /// <exception cref="T:System.ArgumentException">不存在接受指定字段类型的 <see cref="Overload:System.Console.WriteLine" /> 方法重载。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="fld" /> 为 null。</exception>
    /// <exception cref="T:System.NotSupportedException">字段类型为 <see cref="T:System.Reflection.Emit.TypeBuilder" /> 或 <see cref="T:System.Reflection.Emit.EnumBuilder" />，这两种类型都不受支持。</exception>
    public virtual void EmitWriteLine(FieldInfo fld)
    {
      if (fld == (FieldInfo) null)
        throw new ArgumentNullException("fld");
      MethodInfo method1 = typeof (Console).GetMethod("get_Out");
      this.Emit(OpCodes.Call, method1);
      if ((fld.Attributes & FieldAttributes.Static) != FieldAttributes.PrivateScope)
      {
        this.Emit(OpCodes.Ldsfld, fld);
      }
      else
      {
        this.Emit(OpCodes.Ldarg, (short) 0);
        this.Emit(OpCodes.Ldfld, fld);
      }
      Type[] types = new Type[1];
      object obj = (object) fld.FieldType;
      if (obj is TypeBuilder || obj is EnumBuilder)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_OutputStreamUsingTypeBuilder"));
      types[0] = (Type) obj;
      MethodInfo method2 = typeof (TextWriter).GetMethod("WriteLine", types);
      if (method2 == (MethodInfo) null)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmitWriteLineType"), "fld");
      this.Emit(OpCodes.Callvirt, method2);
    }

    /// <summary>声明指定类型的局部变量。</summary>
    /// <returns>已声明的局部变量。</returns>
    /// <param name="localType">一个 <see cref="T:System.Type" /> 对象，表示局部变量的类型。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="localType" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">包含类型已由 <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> 方法创建。</exception>
    public virtual LocalBuilder DeclareLocal(Type localType)
    {
      return this.DeclareLocal(localType, false);
    }

    /// <summary>声明指定类型的局部变量，还可以选择固定该变量所引用的对象。</summary>
    /// <returns>一个 <see cref="T:System.Reflection.Emit.LocalBuilder" /> 对象，表示局部变量。</returns>
    /// <param name="localType">一个 <see cref="T:System.Type" /> 对象，表示局部变量的类型。</param>
    /// <param name="pinned">如果要将对象固定在内存中，则为 true；否则为 false。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="localType" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">包含类型已由 <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> 方法创建。- 或 -封闭方法的方法体已由 <see cref="M:System.Reflection.Emit.MethodBuilder.CreateMethodBody(System.Byte[],System.Int32)" /> 方法创建。</exception>
    /// <exception cref="T:System.NotSupportedException">与此 <see cref="T:System.Reflection.Emit.ILGenerator" /> 关联的方法不由 <see cref="T:System.Reflection.Emit.MethodBuilder" /> 来表示。</exception>
    public virtual LocalBuilder DeclareLocal(Type localType, bool pinned)
    {
      MethodBuilder methodBuilder = this.m_methodBuilder as MethodBuilder;
      if ((MethodInfo) methodBuilder == (MethodInfo) null)
        throw new NotSupportedException();
      if (methodBuilder.IsTypeCreated())
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_TypeHasBeenCreated"));
      if (localType == (Type) null)
        throw new ArgumentNullException("localType");
      if (methodBuilder.m_bIsBaked)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_MethodBaked"));
      this.m_localSignature.AddArgument(localType, pinned);
      LocalBuilder localBuilder = new LocalBuilder(this.m_localCount, localType, (MethodInfo) methodBuilder, pinned);
      this.m_localCount = this.m_localCount + 1;
      return localBuilder;
    }

    /// <summary>指定用于计算当前活动词法范围的局部变量和监视值的命名空间。</summary>
    /// <param name="usingNamespace">用于计算当前活动词法范围的局部变量和监视值的命名空间。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="usingNamespace" /> 的长度为零。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="usingNamespace" /> 为 null。</exception>
    /// <exception cref="T:System.NotSupportedException">此 <see cref="T:System.Reflection.Emit.ILGenerator" /> 属于某个 <see cref="T:System.Reflection.Emit.DynamicMethod" />。</exception>
    public virtual void UsingNamespace(string usingNamespace)
    {
      if (usingNamespace == null)
        throw new ArgumentNullException("usingNamespace");
      if (usingNamespace.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), "usingNamespace");
      MethodBuilder methodBuilder = this.m_methodBuilder as MethodBuilder;
      if ((MethodInfo) methodBuilder == (MethodInfo) null)
        throw new NotSupportedException();
      if (methodBuilder.GetILGenerator().m_ScopeTree.GetCurrentActiveScopeIndex() == -1)
        methodBuilder.m_localSymInfo.AddUsingNamespace(usingNamespace);
      else
        this.m_ScopeTree.AddUsingNamespaceToCurrentScope(usingNamespace);
    }

    /// <summary>在 Microsoft 中间语言 (MSIL) 流中标记序列点。</summary>
    /// <param name="document">为其定义序列点的文档。</param>
    /// <param name="startLine">序列点开始的行。</param>
    /// <param name="startColumn">序列点开始的行中的列。</param>
    /// <param name="endLine">序列点结束的行。</param>
    /// <param name="endColumn">序列点结束的行中的列。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="startLine" /> 或 <paramref name="endLine" /> 为 &lt;= 0。</exception>
    /// <exception cref="T:System.NotSupportedException">此 <see cref="T:System.Reflection.Emit.ILGenerator" /> 属于某个 <see cref="T:System.Reflection.Emit.DynamicMethod" />。</exception>
    public virtual void MarkSequencePoint(ISymbolDocumentWriter document, int startLine, int startColumn, int endLine, int endColumn)
    {
      if (startLine == 0 || startLine < 0 || (endLine == 0 || endLine < 0))
        throw new ArgumentOutOfRangeException("startLine");
      this.m_LineNumberInfo.AddLineNumberInfo(document, this.m_length, startLine, startColumn, endLine, endColumn);
    }

    /// <summary>开始词法范围。</summary>
    /// <exception cref="T:System.NotSupportedException">此 <see cref="T:System.Reflection.Emit.ILGenerator" /> 属于某个 <see cref="T:System.Reflection.Emit.DynamicMethod" />。</exception>
    public virtual void BeginScope()
    {
      this.m_ScopeTree.AddScopeInfo(ScopeAction.Open, this.m_length);
    }

    /// <summary>结束词法范围。</summary>
    /// <exception cref="T:System.NotSupportedException">此 <see cref="T:System.Reflection.Emit.ILGenerator" /> 属于某个 <see cref="T:System.Reflection.Emit.DynamicMethod" />。</exception>
    public virtual void EndScope()
    {
      this.m_ScopeTree.AddScopeInfo(ScopeAction.Close, this.m_length);
    }

    void _ILGenerator.GetTypeInfoCount(out uint pcTInfo)
    {
      throw new NotImplementedException();
    }

    void _ILGenerator.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
    {
      throw new NotImplementedException();
    }

    void _ILGenerator.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
    {
      throw new NotImplementedException();
    }

    void _ILGenerator.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
    {
      throw new NotImplementedException();
    }
  }
}
