// Decompiled with JetBrains decompiler
// Type: System.Threading.Tasks.BeginEndAwaitableAdapter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Security;

namespace System.Threading.Tasks
{
  internal sealed class BeginEndAwaitableAdapter : ICriticalNotifyCompletion, INotifyCompletion
  {
    private static readonly Action CALLBACK_RAN = new Action(BeginEndAwaitableAdapter.\u003C\u003Ec.\u003C\u003E9.\u003C\u002Ecctor\u003Eb__11_0);
    public static readonly AsyncCallback Callback = new AsyncCallback(BeginEndAwaitableAdapter.\u003C\u003Ec.\u003C\u003E9.\u003C\u002Ecctor\u003Eb__11_1);
    private IAsyncResult _asyncResult;
    private Action _continuation;

    public bool IsCompleted
    {
      get
      {
        return this._continuation == BeginEndAwaitableAdapter.CALLBACK_RAN;
      }
    }

    public BeginEndAwaitableAdapter GetAwaiter()
    {
      return this;
    }

    [SecurityCritical]
    public void UnsafeOnCompleted(Action continuation)
    {
      this.OnCompleted(continuation);
    }

    public void OnCompleted(Action continuation)
    {
      if (!(this._continuation == BeginEndAwaitableAdapter.CALLBACK_RAN) && !(Interlocked.CompareExchange<Action>(ref this._continuation, continuation, (Action) null) == BeginEndAwaitableAdapter.CALLBACK_RAN))
        return;
      Task.Run(continuation);
    }

    public IAsyncResult GetResult()
    {
      IAsyncResult asyncResult = this._asyncResult;
      this._asyncResult = (IAsyncResult) null;
      this._continuation = (Action) null;
      return asyncResult;
    }
  }
}
