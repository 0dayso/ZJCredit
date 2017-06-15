// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.LocalBuilder
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
  /// <summary>表示方法或构造函数内的局部变量。</summary>
  [ClassInterface(ClassInterfaceType.None)]
  [ComDefaultInterface(typeof (_LocalBuilder))]
  [ComVisible(true)]
  public sealed class LocalBuilder : LocalVariableInfo, _LocalBuilder
  {
    private int m_localIndex;
    private Type m_localType;
    private MethodInfo m_methodBuilder;
    private bool m_isPinned;

    /// <summary>获取一个值，该值指示局部变量引用的对象是否固定于内存中。</summary>
    /// <returns>如果局部变量引用的对象固定于内存中，则为 true；否则为 false。</returns>
    public override bool IsPinned
    {
      get
      {
        return this.m_isPinned;
      }
    }

    /// <summary>获取局部变量的类型。</summary>
    /// <returns>局部变量的 <see cref="T:System.Type" />。</returns>
    public override Type LocalType
    {
      get
      {
        return this.m_localType;
      }
    }

    /// <summary>在方法体中获取局部变量的从零开始的索引。</summary>
    /// <returns>一个整数值，表示方法体内局部变量的声明顺序。</returns>
    public override int LocalIndex
    {
      get
      {
        return this.m_localIndex;
      }
    }

    private LocalBuilder()
    {
    }

    internal LocalBuilder(int localIndex, Type localType, MethodInfo methodBuilder)
      : this(localIndex, localType, methodBuilder, false)
    {
    }

    internal LocalBuilder(int localIndex, Type localType, MethodInfo methodBuilder, bool isPinned)
    {
      this.m_isPinned = isPinned;
      this.m_localIndex = localIndex;
      this.m_localType = localType;
      this.m_methodBuilder = methodBuilder;
    }

    internal int GetLocalIndex()
    {
      return this.m_localIndex;
    }

    internal MethodInfo GetMethodBuilder()
    {
      return this.m_methodBuilder;
    }

    /// <summary>设置该局部变量的名称。</summary>
    /// <param name="name">局部变量的名称。</param>
    /// <exception cref="T:System.InvalidOperationException">已经用 <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> 创建了该包含类型。- 或 -没有为包含模块定义的符号编写器。</exception>
    /// <exception cref="T:System.NotSupportedException">此局部变量是使用动态方法（而不是动态类型的方法）来定义的。</exception>
    public void SetLocalSymInfo(string name)
    {
      this.SetLocalSymInfo(name, 0, 0);
    }

    /// <summary>设置该局部变量的名称和词法范围。</summary>
    /// <param name="name">局部变量的名称。</param>
    /// <param name="startOffset">局部变量词法范围的开始偏移量。</param>
    /// <param name="endOffset">局部变量词法范围的结束偏移量。</param>
    /// <exception cref="T:System.InvalidOperationException">已经用 <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> 创建了该包含类型。- 或 -没有为包含模块定义的符号编写器。</exception>
    /// <exception cref="T:System.NotSupportedException">此局部变量是使用动态方法（而不是动态类型的方法）来定义的。</exception>
    public void SetLocalSymInfo(string name, int startOffset, int endOffset)
    {
      MethodBuilder methodBuilder = this.m_methodBuilder as MethodBuilder;
      if ((MethodInfo) methodBuilder == (MethodInfo) null)
        throw new NotSupportedException();
      ModuleBuilder moduleBuilder = (ModuleBuilder) methodBuilder.Module;
      if (methodBuilder.IsTypeCreated())
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_TypeHasBeenCreated"));
      if (moduleBuilder.GetSymWriter() == null)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotADebugModule"));
      SignatureHelper fieldSigHelper = SignatureHelper.GetFieldSigHelper((Module) moduleBuilder);
      Type clsArgument = this.m_localType;
      fieldSigHelper.AddArgument(clsArgument);
      int num;
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      int& length1 = @num;
      byte[] signature1 = fieldSigHelper.InternalGetSignature(length1);
      byte[] signature2 = new byte[num - 1];
      int sourceIndex = 1;
      byte[] numArray = signature2;
      int destinationIndex = 0;
      int length2 = num - 1;
      Array.Copy((Array) signature1, sourceIndex, (Array) numArray, destinationIndex, length2);
      if (methodBuilder.GetILGenerator().m_ScopeTree.GetCurrentActiveScopeIndex() == -1)
        methodBuilder.m_localSymInfo.AddLocalSymInfo(name, signature2, this.m_localIndex, startOffset, endOffset);
      else
        methodBuilder.GetILGenerator().m_ScopeTree.AddLocalSymInfoToCurrentScope(name, signature2, this.m_localIndex, startOffset, endOffset);
    }

    void _LocalBuilder.GetTypeInfoCount(out uint pcTInfo)
    {
      throw new NotImplementedException();
    }

    void _LocalBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
    {
      throw new NotImplementedException();
    }

    void _LocalBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
    {
      throw new NotImplementedException();
    }

    void _LocalBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
    {
      throw new NotImplementedException();
    }
  }
}
