// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Contracts.Contract
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security;

namespace System.Diagnostics.Contracts
{
  /// <summary>包含用于表示程序协定（如前置条件、后置条件和对象固定）的静态方法。</summary>
  [__DynamicallyInvokable]
  public static class Contract
  {
    [ThreadStatic]
    private static bool _assertingMustUseRewriter;

    /// <summary>协定失败时发生。</summary>
    [__DynamicallyInvokable]
    public static event EventHandler<ContractFailedEventArgs> ContractFailed
    {
      [SecurityCritical, __DynamicallyInvokable] add
      {
        ContractHelper.InternalContractFailed += value;
      }
      [SecurityCritical, __DynamicallyInvokable] remove
      {
        ContractHelper.InternalContractFailed -= value;
      }
    }

    /// <summary>指示代码分析工具假设指定的条件为 true（即使无法静态地证明该条件始终为 true）。</summary>
    /// <param name="condition">假设为 true 的条件表达式。</param>
    [Conditional("DEBUG")]
    [Conditional("CONTRACTS_FULL")]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public static void Assume(bool condition)
    {
      if (condition)
        return;
      Contract.ReportFailure(ContractFailureKind.Assume, (string) null, (string) null, (Exception) null);
    }

    /// <summary>指示代码分析工具假设指定的条件为 true（即使无法静态地证明该条件始终为 true）并在假设失败时显示一条消息。</summary>
    /// <param name="condition">假设为 true 的条件表达式。</param>
    /// <param name="userMessage">假设失败时要发布的消息。</param>
    [Conditional("DEBUG")]
    [Conditional("CONTRACTS_FULL")]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public static void Assume(bool condition, string userMessage)
    {
      if (condition)
        return;
      Contract.ReportFailure(ContractFailureKind.Assume, userMessage, (string) null, (Exception) null);
    }

    /// <summary>检查条件；如果条件为 false，则遵循为分析器设置的升级策略。</summary>
    /// <param name="condition">要测试的条件表达式。</param>
    [Conditional("DEBUG")]
    [Conditional("CONTRACTS_FULL")]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public static void Assert(bool condition)
    {
      if (condition)
        return;
      Contract.ReportFailure(ContractFailureKind.Assert, (string) null, (string) null, (Exception) null);
    }

    /// <summary>检查条件；如果条件为 false，则遵循分析器设置的升级策略并显示指定消息。</summary>
    /// <param name="condition">要测试的条件表达式。</param>
    /// <param name="userMessage">在不满足条件时要显示的消息。</param>
    [Conditional("DEBUG")]
    [Conditional("CONTRACTS_FULL")]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public static void Assert(bool condition, string userMessage)
    {
      if (condition)
        return;
      Contract.ReportFailure(ContractFailureKind.Assert, userMessage, (string) null, (Exception) null);
    }

    /// <summary>为封闭方法或属性指定一个前置条件协定。</summary>
    /// <param name="condition">要测试的条件表达式。</param>
    [Conditional("CONTRACTS_FULL")]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public static void Requires(bool condition)
    {
      Contract.AssertMustUseRewriter(ContractFailureKind.Precondition, "Requires");
    }

    /// <summary>为封闭方法或属性指定一个前置条件协定，并在该协定的条件失败时显示一条消息。</summary>
    /// <param name="condition">要测试的条件表达式。</param>
    /// <param name="userMessage">条件为 false 时要显示的消息。</param>
    [Conditional("CONTRACTS_FULL")]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public static void Requires(bool condition, string userMessage)
    {
      Contract.AssertMustUseRewriter(ContractFailureKind.Precondition, "Requires");
    }

    /// <summary>为封闭方法或属性指定一个前置条件协定，并在该协定的条件失败时引发异常。</summary>
    /// <param name="condition">要测试的条件表达式。</param>
    /// <typeparam name="TException">条件为 false 时要引发的异常。</typeparam>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public static void Requires<TException>(bool condition) where TException : Exception
    {
      Contract.AssertMustUseRewriter(ContractFailureKind.Precondition, "Requires<TException>");
    }

    /// <summary>为封闭方法或属性指定一个前置条件协定，并在该协定的条件失败时引发包含提供的消息的异常。</summary>
    /// <param name="condition">要测试的条件表达式。</param>
    /// <param name="userMessage">条件为 false 时要显示的消息。</param>
    /// <typeparam name="TException">条件为 false 时要引发的异常。</typeparam>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public static void Requires<TException>(bool condition, string userMessage) where TException : Exception
    {
      Contract.AssertMustUseRewriter(ContractFailureKind.Precondition, "Requires<TException>");
    }

    /// <summary>为封闭方法或属性指定一个后置条件协定。</summary>
    /// <param name="condition">要测试的条件表达式。该表达式可以包括 <see cref="M:System.Diagnostics.Contracts.Contract.OldValue``1(``0)" />、<see cref="M:System.Diagnostics.Contracts.Contract.ValueAtReturn``1(``0@)" /> 和 <see cref="M:System.Diagnostics.Contracts.Contract.Result``1" /> 值。</param>
    [Conditional("CONTRACTS_FULL")]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public static void Ensures(bool condition)
    {
      Contract.AssertMustUseRewriter(ContractFailureKind.Postcondition, "Ensures");
    }

    /// <summary>为提供的退出条件指定后置条件协定，并指定条件为 false 时要显示的消息。</summary>
    /// <param name="condition">要测试的条件表达式。该表达式可以包括 <see cref="M:System.Diagnostics.Contracts.Contract.OldValue``1(``0)" /> 和 <see cref="M:System.Diagnostics.Contracts.Contract.Result``1" /> 值。</param>
    /// <param name="userMessage">表达式不为 true 时要显示的消息。</param>
    [Conditional("CONTRACTS_FULL")]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public static void Ensures(bool condition, string userMessage)
    {
      Contract.AssertMustUseRewriter(ContractFailureKind.Postcondition, "Ensures");
    }

    /// <summary>基于提供的异常和条件为封闭方法或属性指定一个后置条件协定。</summary>
    /// <param name="condition">要测试的条件表达式。</param>
    /// <typeparam name="TException">引发后置条件检查的异常的类型。</typeparam>
    [Conditional("CONTRACTS_FULL")]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public static void EnsuresOnThrow<TException>(bool condition) where TException : Exception
    {
      Contract.AssertMustUseRewriter(ContractFailureKind.PostconditionOnException, "EnsuresOnThrow");
    }

    /// <summary>基于提供的异常和条件为封闭方法或属性指定一个后置条件协定，并指定条件为 false 时要显示的消息。</summary>
    /// <param name="condition">要测试的条件表达式。</param>
    /// <param name="userMessage">表达式为 false 时要显示的消息。</param>
    /// <typeparam name="TException">引发后置条件检查的异常的类型。</typeparam>
    [Conditional("CONTRACTS_FULL")]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public static void EnsuresOnThrow<TException>(bool condition, string userMessage) where TException : Exception
    {
      Contract.AssertMustUseRewriter(ContractFailureKind.PostconditionOnException, "EnsuresOnThrow");
    }

    /// <summary>表示一个方法或属性的返回值。</summary>
    /// <returns>封闭方法或属性的返回值。</returns>
    /// <typeparam name="T">封闭方法或属性的返回值的类型。</typeparam>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public static T Result<T>()
    {
      return default (T);
    }

    /// <summary>表示从一个方法返回时 out 参数的最终（输出）值。</summary>
    /// <returns>out 参数的输出值。</returns>
    /// <param name="value">out 参数。</param>
    /// <typeparam name="T">out 参数的类型。</typeparam>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public static T ValueAtReturn<T>(out T value)
    {
      value = default (T);
      return value;
    }

    /// <summary>表示方法或属性开始时的值。</summary>
    /// <returns>一个方法或属性开始处的参数或字段的值。</returns>
    /// <param name="value">要表示的值（字段或参数）。</param>
    /// <typeparam name="T">值的类型。</typeparam>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public static T OldValue<T>(T value)
    {
      return default (T);
    }

    /// <summary>为封闭方法或属性指定一个固定的协定。</summary>
    /// <param name="condition">要测试的条件表达式。</param>
    [Conditional("CONTRACTS_FULL")]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public static void Invariant(bool condition)
    {
      Contract.AssertMustUseRewriter(ContractFailureKind.Invariant, "Invariant");
    }

    /// <summary>为封闭方法或属性指定一个固定协定，并在该协定的条件失败时显示一条消息。</summary>
    /// <param name="condition">要测试的条件表达式。</param>
    /// <param name="userMessage">条件为 false 时要显示的消息。</param>
    [Conditional("CONTRACTS_FULL")]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public static void Invariant(bool condition, string userMessage)
    {
      Contract.AssertMustUseRewriter(ContractFailureKind.Invariant, "Invariant");
    }

    /// <summary>确定某个特定条件是否对指定范围内的所有整数都有效。</summary>
    /// <returns>如果 <paramref name="predicate" /> 对于从 <paramref name="fromInclusive" /> 到 <paramref name="toExclusive" /> - 1 范围内的任何整数都返回 true，则为 true。</returns>
    /// <param name="fromInclusive">要传递给 <paramref name="predicate" /> 的第一个整数。</param>
    /// <param name="toExclusive">要传递给 <paramref name="predicate" /> 的最后一个整数加一。</param>
    /// <param name="predicate">要计算其中是否存在指定范围内的整数的函数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="predicate" /> is null.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="toExclusive " />is less than <paramref name="fromInclusive" />.</exception>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public static bool ForAll(int fromInclusive, int toExclusive, Predicate<int> predicate)
    {
      if (fromInclusive > toExclusive)
        throw new ArgumentException(Environment.GetResourceString("Argument_ToExclusiveLessThanFromExclusive"));
      if (predicate == null)
        throw new ArgumentNullException("predicate");
      for (int index = fromInclusive; index < toExclusive; ++index)
      {
        if (!predicate(index))
          return false;
      }
      return true;
    }

    /// <summary>确定函数中是否存在某个集合中的所有元素。</summary>
    /// <returns>当且仅当 <paramref name="predicate" /> 对于 <paramref name="collection" /> 中的 <paramref name="T" /> 类型的全部元素都返回 true 时，才为 true。</returns>
    /// <param name="collection">将从中提取 <paramref name="T" /> 类型的元素以将其传递给 <paramref name="predicate" /> 的集合。</param>
    /// <param name="predicate">用于计算 <paramref name="collection" /> 中所有元素是否存在的函数。</param>
    /// <typeparam name="T">
    /// <paramref name="collection" /> 中包含的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="collection" /> or <paramref name="predicate" /> is null.</exception>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public static bool ForAll<T>(IEnumerable<T> collection, Predicate<T> predicate)
    {
      if (collection == null)
        throw new ArgumentNullException("collection");
      if (predicate == null)
        throw new ArgumentNullException("predicate");
      foreach (T obj in collection)
      {
        if (!predicate(obj))
          return false;
      }
      return true;
    }

    /// <summary>确定指定的测试对某个整数范围中的任何整数是否都为 true。</summary>
    /// <returns>如果 <paramref name="predicate" /> 对于从 <paramref name="fromInclusive" /> 到 <paramref name="toExclusive" /> - 1 范围内的任何整数都返回 true，则为 true。</returns>
    /// <param name="fromInclusive">要传递给 <paramref name="predicate" /> 的第一个整数。</param>
    /// <param name="toExclusive">要传递给 <paramref name="predicate" /> 的最后一个整数加一。</param>
    /// <param name="predicate">用于计算指定范围内整数的任何值的函数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="predicate" /> is null.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="toExclusive " />is less than <paramref name="fromInclusive" />.</exception>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public static bool Exists(int fromInclusive, int toExclusive, Predicate<int> predicate)
    {
      if (fromInclusive > toExclusive)
        throw new ArgumentException(Environment.GetResourceString("Argument_ToExclusiveLessThanFromExclusive"));
      if (predicate == null)
        throw new ArgumentNullException("predicate");
      for (int index = fromInclusive; index < toExclusive; ++index)
      {
        if (predicate(index))
          return true;
      }
      return false;
    }

    /// <summary>确定函数中是否存在某个元素集合中的元素。</summary>
    /// <returns>当且仅当 <paramref name="predicate" /> 对于 <paramref name="collection" /> 中的 <paramref name="T" /> 类型的任何元素都返回 true 时，才为 true。</returns>
    /// <param name="collection">将从中提取 <paramref name="T" /> 类型的元素以将其传递给 <paramref name="predicate" /> 的集合。</param>
    /// <param name="predicate">用于计算 <paramref name="collection" /> 中某个元素的函数。</param>
    /// <typeparam name="T">
    /// <paramref name="collection" /> 中包含的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="collection" /> or <paramref name="predicate" /> is null.</exception>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public static bool Exists<T>(IEnumerable<T> collection, Predicate<T> predicate)
    {
      if (collection == null)
        throw new ArgumentNullException("collection");
      if (predicate == null)
        throw new ArgumentNullException("predicate");
      foreach (T obj in collection)
      {
        if (predicate(obj))
          return true;
      }
      return false;
    }

    /// <summary>当方法的协定仅包含 if-then-throw 形式的前置条件时，标记协定部分的结尾。</summary>
    [Conditional("CONTRACTS_FULL")]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public static void EndContractBlock()
    {
    }

    [DebuggerNonUserCode]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    private static void ReportFailure(ContractFailureKind failureKind, string userMessage, string conditionText, Exception innerException)
    {
      if (failureKind < ContractFailureKind.Precondition || failureKind > ContractFailureKind.Assume)
        throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", (object) failureKind), "failureKind");
      string displayMessage = ContractHelper.RaiseContractFailedEvent(failureKind, userMessage, conditionText, innerException);
      if (displayMessage == null)
        return;
      ContractHelper.TriggerFailure(failureKind, displayMessage, userMessage, conditionText, innerException);
    }

    [SecuritySafeCritical]
    private static void AssertMustUseRewriter(ContractFailureKind kind, string contractKind)
    {
      if (Contract._assertingMustUseRewriter)
        Assert.Fail("Asserting that we must use the rewriter went reentrant.", "Didn't rewrite this mscorlib?");
      Contract._assertingMustUseRewriter = true;
      Assembly assembly1 = typeof (Contract).Assembly;
      StackTrace stackTrace = new StackTrace();
      Assembly assembly2 = (Assembly) null;
      for (int index = 0; index < stackTrace.FrameCount; ++index)
      {
        Assembly assembly3 = stackTrace.GetFrame(index).GetMethod().DeclaringType.Assembly;
        if (assembly3 != assembly1)
        {
          assembly2 = assembly3;
          break;
        }
      }
      if (assembly2 == (Assembly) null)
        assembly2 = assembly1;
      string name = assembly2.GetName().Name;
      ContractHelper.TriggerFailure(kind, Environment.GetResourceString("MustUseCCRewrite", (object) contractKind, (object) name), (string) null, (string) null, (Exception) null);
      Contract._assertingMustUseRewriter = false;
    }
  }
}
