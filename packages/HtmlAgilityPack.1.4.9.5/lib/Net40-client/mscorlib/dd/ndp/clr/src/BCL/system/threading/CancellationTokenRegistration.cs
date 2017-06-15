// Decompiled with JetBrains decompiler
// Type: System.Threading.CancellationTokenRegistration
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Security.Permissions;

namespace System.Threading
{
  /// <summary>表示已向 <see cref="T:System.Threading.CancellationToken" /> 注册的回调委托。</summary>
  [__DynamicallyInvokable]
  [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
  public struct CancellationTokenRegistration : IEquatable<CancellationTokenRegistration>, IDisposable
  {
    private readonly CancellationCallbackInfo m_callbackInfo;
    private readonly SparselyPopulatedArrayAddInfo<CancellationCallbackInfo> m_registrationInfo;

    internal CancellationTokenRegistration(CancellationCallbackInfo callbackInfo, SparselyPopulatedArrayAddInfo<CancellationCallbackInfo> registrationInfo)
    {
      this.m_callbackInfo = callbackInfo;
      this.m_registrationInfo = registrationInfo;
    }

    /// <summary>确定两个 <see cref="T:System.Threading.CancellationTokenRegistration" /> 实例是否相等。</summary>
    /// <returns>如果两个实例相等，则为 true；否则为 false。</returns>
    /// <param name="left">第一个实例。</param>
    /// <param name="right">第二个实例。</param>
    [__DynamicallyInvokable]
    public static bool operator ==(CancellationTokenRegistration left, CancellationTokenRegistration right)
    {
      return left.Equals(right);
    }

    /// <summary>确定两个 <see cref="T:System.Threading.CancellationTokenRegistration" /> 实例是否不相等。</summary>
    /// <returns>如果两个实例不相等，则为 true；否则为 false。</returns>
    /// <param name="left">第一个实例。</param>
    /// <param name="right">第二个实例。</param>
    [__DynamicallyInvokable]
    public static bool operator !=(CancellationTokenRegistration left, CancellationTokenRegistration right)
    {
      return !left.Equals(right);
    }

    [FriendAccessAllowed]
    internal bool TryDeregister()
    {
      return this.m_registrationInfo.Source != null && this.m_registrationInfo.Source.SafeAtomicRemove(this.m_registrationInfo.Index, this.m_callbackInfo) == this.m_callbackInfo;
    }

    /// <summary>释放由 <see cref="T:System.Threading.CancellationTokenRegistration" /> 类的当前实例占用的所有资源。</summary>
    [__DynamicallyInvokable]
    public void Dispose()
    {
      bool flag = this.TryDeregister();
      CancellationCallbackInfo cancellationCallbackInfo = this.m_callbackInfo;
      if (cancellationCallbackInfo == null)
        return;
      CancellationTokenSource cancellationTokenSource = cancellationCallbackInfo.CancellationTokenSource;
      if (!cancellationTokenSource.IsCancellationRequested || cancellationTokenSource.IsCancellationCompleted || (flag || cancellationTokenSource.ThreadIDExecutingCallbacks == Thread.CurrentThread.ManagedThreadId))
        return;
      cancellationTokenSource.WaitForCallbackToComplete(this.m_callbackInfo);
    }

    /// <summary>确定当前的 <see cref="T:System.Threading.CancellationTokenRegistration" /> 实例是否等于指定的 <see cref="T:System.Threading.CancellationTokenRegistration" />。</summary>
    /// <returns>如果此实例和 <paramref name="obj" /> 相等，则为 true。否则为 false。如果两个 <see cref="T:System.Threading.CancellationTokenRegistration" /> 实例均引用对相同 <see cref="T:System.Threading.CancellationToken" /> Register 方法的单一调用的输出，则这两个实例相等。</returns>
    /// <param name="obj">要与此实例进行比较的其他对象。</param>
    [__DynamicallyInvokable]
    public override bool Equals(object obj)
    {
      if (obj is CancellationTokenRegistration)
        return this.Equals((CancellationTokenRegistration) obj);
      return false;
    }

    /// <summary>确定当前的 <see cref="T:System.Threading.CancellationTokenRegistration" /> 实例是否等于指定的 <see cref="T:System.Threading.CancellationTokenRegistration" />。</summary>
    /// <returns>如果此实例和 <paramref name="other" /> 相等，则为 true。否则为 false。 如果两个 <see cref="T:System.Threading.CancellationTokenRegistration" /> 实例均引用对相同 <see cref="T:System.Threading.CancellationToken" /> Register 方法的单一调用的输出，则这两个实例相等。</returns>
    /// <param name="other">要与此实例进行比较的其他 <see cref="T:System.Threading.CancellationTokenRegistration" />。</param>
    [__DynamicallyInvokable]
    public bool Equals(CancellationTokenRegistration other)
    {
      if (this.m_callbackInfo == other.m_callbackInfo)
      {
        SparselyPopulatedArrayFragment<CancellationCallbackInfo> source1 = this.m_registrationInfo.Source;
        SparselyPopulatedArrayAddInfo<CancellationCallbackInfo> populatedArrayAddInfo = other.m_registrationInfo;
        SparselyPopulatedArrayFragment<CancellationCallbackInfo> source2 = populatedArrayAddInfo.Source;
        if (source1 == source2)
        {
          populatedArrayAddInfo = this.m_registrationInfo;
          int index1 = populatedArrayAddInfo.Index;
          populatedArrayAddInfo = other.m_registrationInfo;
          int index2 = populatedArrayAddInfo.Index;
          return index1 == index2;
        }
      }
      return false;
    }

    /// <summary>作为 <see cref="T:System.Threading.CancellationTokenRegistration" /> 的哈希函数。</summary>
    /// <returns>当前 <see cref="T:System.Threading.CancellationTokenRegistration" /> 实例的哈希代码。</returns>
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      if (this.m_registrationInfo.Source != null)
        return this.m_registrationInfo.Source.GetHashCode() ^ this.m_registrationInfo.Index.GetHashCode();
      return this.m_registrationInfo.Index.GetHashCode();
    }
  }
}
