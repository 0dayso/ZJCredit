// Decompiled with JetBrains decompiler
// Type: System.Reflection.MethodBody
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>提供对用于方法体的元数据和 MSIL 的访问。</summary>
  [ComVisible(true)]
  public class MethodBody
  {
    private byte[] m_IL;
    private ExceptionHandlingClause[] m_exceptionHandlingClauses;
    private LocalVariableInfo[] m_localVariables;
    internal MethodBase m_methodBase;
    private int m_localSignatureMetadataToken;
    private int m_maxStackSize;
    private bool m_initLocals;

    /// <summary>获取签名的元数据标记，该签名在元数据中描述方法的局部变量。</summary>
    /// <returns>表示元数据标记的整数。</returns>
    public virtual int LocalSignatureMetadataToken
    {
      get
      {
        return this.m_localSignatureMetadataToken;
      }
    }

    /// <summary>获取在方法体中声明的局部变量的列表。</summary>
    /// <returns>
    /// <see cref="T:System.Reflection.LocalVariableInfo" /> 对象的一个 <see cref="T:System.Collections.Generic.IList`1" />，这些对象描述在方法体中声明的局部变量。</returns>
    public virtual IList<LocalVariableInfo> LocalVariables
    {
      get
      {
        return (IList<LocalVariableInfo>) Array.AsReadOnly<LocalVariableInfo>(this.m_localVariables);
      }
    }

    /// <summary>获取执行方法时操作数堆栈上的项的最大数目。</summary>
    /// <returns>执行方法时操作数堆栈上的项的最大数目。</returns>
    public virtual int MaxStackSize
    {
      get
      {
        return this.m_maxStackSize;
      }
    }

    /// <summary>获取一个值，该值指示方法体中的局部变量是否初始化为相应类型的默认值。</summary>
    /// <returns>如果方法体包含用于将局部变量初始化为 null（对于引用类型）或者零初始值（对于值类型）的代码，则为 true；否则为 false。</returns>
    public virtual bool InitLocals
    {
      get
      {
        return this.m_initLocals;
      }
    }

    /// <summary>获取一个列表，该列表包括方法体中的所有异常处理子句。</summary>
    /// <returns>
    /// <see cref="T:System.Reflection.ExceptionHandlingClause" /> 对象的一个 <see cref="T:System.Collections.Generic.IList`1" />，这些对象表示方法体中的异常处理子句。</returns>
    public virtual IList<ExceptionHandlingClause> ExceptionHandlingClauses
    {
      get
      {
        return (IList<ExceptionHandlingClause>) Array.AsReadOnly<ExceptionHandlingClause>(this.m_exceptionHandlingClauses);
      }
    }

    /// <summary>初始化 <see cref="T:System.Reflection.MethodBody" /> 类的新实例。</summary>
    protected MethodBody()
    {
    }

    /// <summary>以字节数组的形式返回用于方法体的 MSIL。</summary>
    /// <returns>
    /// <see cref="T:System.Byte" /> 类型的一个数组，它包含用于方法体的 MSIL。</returns>
    public virtual byte[] GetILAsByteArray()
    {
      return this.m_IL;
    }
  }
}
