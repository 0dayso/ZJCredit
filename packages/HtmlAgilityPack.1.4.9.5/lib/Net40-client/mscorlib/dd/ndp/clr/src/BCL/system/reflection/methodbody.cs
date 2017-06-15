// Decompiled with JetBrains decompiler
// Type: System.Reflection.ExceptionHandlingClause
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>表示结构化异常处理块中的子句。</summary>
  [ComVisible(true)]
  public class ExceptionHandlingClause
  {
    private MethodBody m_methodBody;
    private ExceptionHandlingClauseOptions m_flags;
    private int m_tryOffset;
    private int m_tryLength;
    private int m_handlerOffset;
    private int m_handlerLength;
    private int m_catchMetadataToken;
    private int m_filterOffset;

    /// <summary>获取一个值，该值指示此异常处理子句是 finally 子句、类型筛选的子句还是用户筛选的子句。</summary>
    /// <returns>一个 <see cref="T:System.Reflection.ExceptionHandlingClauseOptions" /> 值，它指示此子句执行何种操作。</returns>
    public virtual ExceptionHandlingClauseOptions Flags
    {
      get
      {
        return this.m_flags;
      }
    }

    /// <summary>包括此异常处理子句的 try 块在方法内的偏移量（以字节为单位）。</summary>
    /// <returns>一个整数，表示包括此异常处理子句的 try 块在方法内的偏移量（以字节为单位）。</returns>
    public virtual int TryOffset
    {
      get
      {
        return this.m_tryOffset;
      }
    }

    /// <summary>包括此异常处理子句的 try 块的总长度（以字节为单位）。</summary>
    /// <returns>包括此异常处理子句的 try 块的总长度（以字节为单位）。</returns>
    public virtual int TryLength
    {
      get
      {
        return this.m_tryLength;
      }
    }

    /// <summary>获取此异常处理子句在方法体内的偏移量（以字节为单位）。</summary>
    /// <returns>一个整数，表示此异常处理子句在方法体内的偏移量（以字节为单位）。</returns>
    public virtual int HandlerOffset
    {
      get
      {
        return this.m_handlerOffset;
      }
    }

    /// <summary>获取此异常处理子句的主体的长度（以字节为单位）。</summary>
    /// <returns>一个整数，表示形成此异常处理子句主体的 MSIL 的长度（以字节为单位）。</returns>
    public virtual int HandlerLength
    {
      get
      {
        return this.m_handlerLength;
      }
    }

    /// <summary>获取用户提供的筛选代码在方法体内的偏移量（以字节为单位）。</summary>
    /// <returns>用户提供的筛选代码在方法体内的偏移量（以字节为单位）。如果 <see cref="P:System.Reflection.ExceptionHandlingClause.Flags" /> 属性具有 <see cref="F:System.Reflection.ExceptionHandlingClauseOptions.Filter" /> 之外的任何值，则此属性的值没有任何意义。</returns>
    /// <exception cref="T:System.InvalidOperationException">由于异常处理子句不是一个筛选器，因此无法获取偏移量。</exception>
    public virtual int FilterOffset
    {
      get
      {
        if (this.m_flags != ExceptionHandlingClauseOptions.Filter)
          throw new InvalidOperationException(Environment.GetResourceString("Arg_EHClauseNotFilter"));
        return this.m_filterOffset;
      }
    }

    /// <summary>获取由此子句处理的异常类型。</summary>
    /// <returns>一个 <see cref="T:System.Type" /> 对象，表示由此子句处理的异常类型，如果 <see cref="P:System.Reflection.ExceptionHandlingClause.Flags" /> 属性为 <see cref="F:System.Reflection.ExceptionHandlingClauseOptions.Filter" /> 或 <see cref="F:System.Reflection.ExceptionHandlingClauseOptions.Finally" />，则为 null。</returns>
    /// <exception cref="T:System.InvalidOperationException">不能将属性用于对象的当前状态。</exception>
    public virtual Type CatchType
    {
      get
      {
        if (this.m_flags != ExceptionHandlingClauseOptions.Clause)
          throw new InvalidOperationException(Environment.GetResourceString("Arg_EHClauseNotClause"));
        Type type = (Type) null;
        if (!MetadataToken.IsNullToken(this.m_catchMetadataToken))
        {
          Type declaringType = this.m_methodBody.m_methodBase.DeclaringType;
          type = (declaringType == (Type) null ? this.m_methodBody.m_methodBase.Module : declaringType.Module).ResolveType(this.m_catchMetadataToken, declaringType == (Type) null ? (Type[]) null : declaringType.GetGenericArguments(), this.m_methodBody.m_methodBase is MethodInfo ? this.m_methodBody.m_methodBase.GetGenericArguments() : (Type[]) null);
        }
        return type;
      }
    }

    /// <summary>初始化 <see cref="T:System.Reflection.ExceptionHandlingClause" /> 类的新实例。</summary>
    protected ExceptionHandlingClause()
    {
    }

    /// <summary>异常处理子句的字符串表示形式。</summary>
    /// <returns>一个字符串，列出筛选器子句类型的相应属性值。</returns>
    public override string ToString()
    {
      if (this.Flags == ExceptionHandlingClauseOptions.Clause)
        return string.Format((IFormatProvider) CultureInfo.CurrentUICulture, "Flags={0}, TryOffset={1}, TryLength={2}, HandlerOffset={3}, HandlerLength={4}, CatchType={5}", (object) this.Flags, (object) this.TryOffset, (object) this.TryLength, (object) this.HandlerOffset, (object) this.HandlerLength, (object) this.CatchType);
      if (this.Flags == ExceptionHandlingClauseOptions.Filter)
        return string.Format((IFormatProvider) CultureInfo.CurrentUICulture, "Flags={0}, TryOffset={1}, TryLength={2}, HandlerOffset={3}, HandlerLength={4}, FilterOffset={5}", (object) this.Flags, (object) this.TryOffset, (object) this.TryLength, (object) this.HandlerOffset, (object) this.HandlerLength, (object) this.FilterOffset);
      return string.Format((IFormatProvider) CultureInfo.CurrentUICulture, "Flags={0}, TryOffset={1}, TryLength={2}, HandlerOffset={3}, HandlerLength={4}", (object) this.Flags, (object) this.TryOffset, (object) this.TryLength, (object) this.HandlerOffset, (object) this.HandlerLength);
    }
  }
}
