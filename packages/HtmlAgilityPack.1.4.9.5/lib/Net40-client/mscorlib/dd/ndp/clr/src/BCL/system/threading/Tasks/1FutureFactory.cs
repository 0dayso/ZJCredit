// Decompiled with JetBrains decompiler
// Type: System.Threading.Tasks.GenericDelegateCache`2
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Threading.Tasks
{
  internal static class GenericDelegateCache<TAntecedentResult, TResult>
  {
    internal static Func<Task<Task>, object, TResult> CWAnyFuncDelegate = new Func<Task<Task>, object, TResult>(GenericDelegateCache<TAntecedentResult, TResult>.\u003C\u003Ec.\u003C\u003E9.\u003C\u002Ecctor\u003Eb__4_0);
    internal static Func<Task<Task>, object, TResult> CWAnyActionDelegate = new Func<Task<Task>, object, TResult>(GenericDelegateCache<TAntecedentResult, TResult>.\u003C\u003Ec.\u003C\u003E9.\u003C\u002Ecctor\u003Eb__4_1);
    internal static Func<Task<Task<TAntecedentResult>[]>, object, TResult> CWAllFuncDelegate = new Func<Task<Task<TAntecedentResult>[]>, object, TResult>(GenericDelegateCache<TAntecedentResult, TResult>.\u003C\u003Ec.\u003C\u003E9.\u003C\u002Ecctor\u003Eb__4_2);
    internal static Func<Task<Task<TAntecedentResult>[]>, object, TResult> CWAllActionDelegate = new Func<Task<Task<TAntecedentResult>[]>, object, TResult>(GenericDelegateCache<TAntecedentResult, TResult>.\u003C\u003Ec.\u003C\u003E9.\u003C\u002Ecctor\u003Eb__4_3);
  }
}
