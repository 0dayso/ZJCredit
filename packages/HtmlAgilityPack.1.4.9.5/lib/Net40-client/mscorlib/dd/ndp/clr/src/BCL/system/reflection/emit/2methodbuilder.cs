// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.ExceptionHandler
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
  /// <summary>表示异常处理程序，其采用 IL 字节数组形式，传递至方法，例如 <see cref="M:System.Reflection.Emit.MethodBuilder.SetMethodBody(System.Byte[],System.Int32,System.Byte[],System.Collections.Generic.IEnumerable{System.Reflection.Emit.ExceptionHandler},System.Collections.Generic.IEnumerable{System.Int32})" />。</summary>
  [ComVisible(false)]
  public struct ExceptionHandler : IEquatable<ExceptionHandler>
  {
    internal readonly int m_exceptionClass;
    internal readonly int m_tryStartOffset;
    internal readonly int m_tryEndOffset;
    internal readonly int m_filterOffset;
    internal readonly int m_handlerStartOffset;
    internal readonly int m_handlerEndOffset;
    internal readonly ExceptionHandlingClauseOptions m_kind;

    /// <summary>获取该处理程序处理的异常标记类型。</summary>
    /// <returns>该异常处理程序处理的异常标记类型或 0，如果不存在。</returns>
    public int ExceptionTypeToken
    {
      get
      {
        return this.m_exceptionClass;
      }
    }

    /// <summary>获取此异常处理程序保护的代码开头的字节偏移量。</summary>
    /// <returns>在受异常处理程序保护的代码开始时的字节偏移量。</returns>
    public int TryOffset
    {
      get
      {
        return this.m_tryStartOffset;
      }
    }

    /// <summary>获取由该异常处理程序所保护的代码长度，以字节为单位。</summary>
    /// <returns>由该异常处理程序保护的代码以字节为单位的长度。</returns>
    public int TryLength
    {
      get
      {
        return this.m_tryEndOffset - this.m_tryStartOffset;
      }
    }

    /// <summary>获取此异常处理程序的筛选器代码开头的字节偏移量。</summary>
    /// <returns>筛选器代码开头处的字节偏移量；如果筛选器不存在,则为 0。</returns>
    public int FilterOffset
    {
      get
      {
        return this.m_filterOffset;
      }
    }

    /// <summary>获取此异常处理程序的首次说明的字节偏移量。</summary>
    /// <returns>异常处理程序的第一个指令的字节偏移量。</returns>
    public int HandlerOffset
    {
      get
      {
        return this.m_handlerStartOffset;
      }
    }

    /// <summary>获取异常处理的长度（以字节表示）。</summary>
    /// <returns>异常处理程序的长度（以字节为单位）。</returns>
    public int HandlerLength
    {
      get
      {
        return this.m_handlerEndOffset - this.m_handlerStartOffset;
      }
    }

    /// <summary>获取表示此对象表示的这类异常处理的值。</summary>
    /// <returns>枚举值之一，指定异常处理程序的类型。</returns>
    public ExceptionHandlingClauseOptions Kind
    {
      get
      {
        return this.m_kind;
      }
    }

    /// <summary>使用指定的参数初始化 <see cref="T:System.Reflection.Emit.ExceptionHandler" /> 类的新实例。</summary>
    /// <param name="tryOffset">此异常处理程序受保护的第一个指令的字节偏移量。</param>
    /// <param name="tryLength">由此异常处理程序保护的字节数。</param>
    /// <param name="filterOffset">筛选器代码的开始的字节偏移量。筛选器代码在处理程序块的第一条指令结束。对于非筛选异常处理程序，把此参数指定为0。</param>
    /// <param name="handlerOffset">此异常处理程序的第一个指令的字节偏移量。</param>
    /// <param name="handlerLength">在此异常处理程序中的字节数。</param>
    /// <param name="kind">枚举值之一，指定异常处理程序的类型。</param>
    /// <param name="exceptionTypeToken">该异常处理程序处理的异常标记类型。如果不适用，则指定 0（零）。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="tryOffset" />，<paramref name="filterOffset" />，<paramref name="handlerOffset" />，<paramref name="tryLength" /> 或 <paramref name="handlerLength" /> 为负。</exception>
    public ExceptionHandler(int tryOffset, int tryLength, int filterOffset, int handlerOffset, int handlerLength, ExceptionHandlingClauseOptions kind, int exceptionTypeToken)
    {
      if (tryOffset < 0)
        throw new ArgumentOutOfRangeException("tryOffset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (tryLength < 0)
        throw new ArgumentOutOfRangeException("tryLength", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (filterOffset < 0)
        throw new ArgumentOutOfRangeException("filterOffset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (handlerOffset < 0)
        throw new ArgumentOutOfRangeException("handlerOffset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (handlerLength < 0)
        throw new ArgumentOutOfRangeException("handlerLength", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if ((long) tryOffset + (long) tryLength > (long) int.MaxValue)
        throw new ArgumentOutOfRangeException("tryLength", Environment.GetResourceString("ArgumentOutOfRange_Range", (object) 0, (object) (int.MaxValue - tryOffset)));
      if ((long) handlerOffset + (long) handlerLength > (long) int.MaxValue)
        throw new ArgumentOutOfRangeException("handlerLength", Environment.GetResourceString("ArgumentOutOfRange_Range", (object) 0, (object) (int.MaxValue - handlerOffset)));
      if (kind == ExceptionHandlingClauseOptions.Clause && (exceptionTypeToken & 16777215) == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidTypeToken", (object) exceptionTypeToken), "exceptionTypeToken");
      if (!ExceptionHandler.IsValidKind(kind))
        throw new ArgumentOutOfRangeException("kind", Environment.GetResourceString("ArgumentOutOfRange_Enum"));
      this.m_tryStartOffset = tryOffset;
      this.m_tryEndOffset = tryOffset + tryLength;
      this.m_filterOffset = filterOffset;
      this.m_handlerStartOffset = handlerOffset;
      this.m_handlerEndOffset = handlerOffset + handlerLength;
      this.m_kind = kind;
      this.m_exceptionClass = exceptionTypeToken;
    }

    internal ExceptionHandler(int tryStartOffset, int tryEndOffset, int filterOffset, int handlerStartOffset, int handlerEndOffset, int kind, int exceptionTypeToken)
    {
      this.m_tryStartOffset = tryStartOffset;
      this.m_tryEndOffset = tryEndOffset;
      this.m_filterOffset = filterOffset;
      this.m_handlerStartOffset = handlerStartOffset;
      this.m_handlerEndOffset = handlerEndOffset;
      this.m_kind = (ExceptionHandlingClauseOptions) kind;
      this.m_exceptionClass = exceptionTypeToken;
    }

    /// <summary>确定 <see cref="T:System.Reflection.Emit.ExceptionHandler" /> 的两个指定的实例是否相等。</summary>
    /// <returns>如果 <paramref name="left" /> 和 <paramref name="right" /> 相等，则为 true；否则为 false。</returns>
    /// <param name="left">要比较的第一个对象。</param>
    /// <param name="right">要比较的第二个对象。</param>
    public static bool operator ==(ExceptionHandler left, ExceptionHandler right)
    {
      return left.Equals(right);
    }

    /// <summary>确定 <see cref="T:System.Reflection.Emit.ExceptionHandler" /> 的两个指定的实例是否不等。</summary>
    /// <returns>如果 <paramref name="left" /> 与 <paramref name="right" /> 不相等，则为 true；否则为 false。</returns>
    /// <param name="left">要比较的第一个对象。</param>
    /// <param name="right">要比较的第二个对象。</param>
    public static bool operator !=(ExceptionHandler left, ExceptionHandler right)
    {
      return !left.Equals(right);
    }

    private static bool IsValidKind(ExceptionHandlingClauseOptions kind)
    {
      switch (kind)
      {
        case ExceptionHandlingClauseOptions.Clause:
        case ExceptionHandlingClauseOptions.Filter:
        case ExceptionHandlingClauseOptions.Finally:
        case ExceptionHandlingClauseOptions.Fault:
          return true;
        default:
          return false;
      }
    }

    public override int GetHashCode()
    {
      return (int) ((ExceptionHandlingClauseOptions) (this.m_exceptionClass ^ this.m_tryStartOffset ^ this.m_tryEndOffset ^ this.m_filterOffset ^ this.m_handlerStartOffset ^ this.m_handlerEndOffset) ^ this.m_kind);
    }

    /// <summary>指示此实例是否与指定的<see cref="T:System.Reflection.Emit.ExceptionHandler" />对象相等。</summary>
    /// <returns>如果 <paramref name="obj" /> 和此实例相等，则为 true；否则为 false。</returns>
    /// <param name="obj">要与此实例对比的对象。</param>
    public override bool Equals(object obj)
    {
      if (obj is ExceptionHandler)
        return this.Equals((ExceptionHandler) obj);
      return false;
    }

    /// <summary>指示 <see cref="T:System.Reflection.Emit.ExceptionHandler" /> 对象的实例是否等同于另一个 <see cref="T:System.Reflection.Emit.ExceptionHandler" /> 对象的实例。</summary>
    /// <returns>如果 <paramref name="other" /> 和此实例相等，则为 true；否则为 false。</returns>
    /// <param name="other">要与此实例进行比较的异常处理程序对象。</param>
    public bool Equals(ExceptionHandler other)
    {
      if (other.m_exceptionClass == this.m_exceptionClass && other.m_tryStartOffset == this.m_tryStartOffset && (other.m_tryEndOffset == this.m_tryEndOffset && other.m_filterOffset == this.m_filterOffset) && (other.m_handlerStartOffset == this.m_handlerStartOffset && other.m_handlerEndOffset == this.m_handlerEndOffset))
        return other.m_kind == this.m_kind;
      return false;
    }
  }
}
