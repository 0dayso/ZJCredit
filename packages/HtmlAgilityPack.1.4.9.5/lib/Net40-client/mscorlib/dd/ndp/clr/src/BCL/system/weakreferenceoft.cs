// Decompiled with JetBrains decompiler
// Type: System.WeakReference`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
  /// <summary>表示类型化弱引用，即在引用对象的同时仍然允许垃圾回收来回收该对象。</summary>
  /// <typeparam name="T">所引用对象的类型。</typeparam>
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class WeakReference<T> : ISerializable where T : class
  {
    internal IntPtr m_handle;

    private T Target { [SecuritySafeCritical, MethodImpl(MethodImplOptions.InternalCall)] get; [SecuritySafeCritical, MethodImpl(MethodImplOptions.InternalCall)] set; }

    /// <summary>初始化引用指定对象的 <see cref="T:System.WeakReference`1" /> 类的新实例。</summary>
    /// <param name="target">要引用的对象或为 null。</param>
    [__DynamicallyInvokable]
    public WeakReference(T target)
      : this(target, false)
    {
    }

    /// <summary>初始化 <see cref="T:System.WeakReference`1" /> 类的新实例，该类引用指定的对象并使用指定的复活跟踪。</summary>
    /// <param name="target">要引用的对象或为 null。</param>
    /// <param name="trackResurrection">若要在终结后跟踪对象，则为 true；若要仅在终结前跟踪对象，则为 false。</param>
    [__DynamicallyInvokable]
    public WeakReference(T target, bool trackResurrection)
    {
      this.Create(target, trackResurrection);
    }

    internal WeakReference(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException("info");
      this.Create((T) info.GetValue("TrackedObject", typeof (T)), info.GetBoolean("TrackResurrection"));
    }

    /// <summary>丢弃对当前 <see cref="T:System.WeakReference`1" /> 对象表示的目标的引用。</summary>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    ~WeakReference();

    /// <summary>尝试检索当前 <see cref="T:System.WeakReference`1" /> 对象引用的目标对象。</summary>
    /// <returns>如果该目标已检索，则为 true；否则为 false。</returns>
    /// <param name="target">当此方法返回时，包含目标对象（可用时）。该参数未经初始化即被处理。</param>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetTarget(out T target)
    {
      T target1 = this.Target;
      target = target1;
      return (object) target1 != null;
    }

    /// <summary>设置 <see cref="T:System.WeakReference`1" /> 对象引用的目标对象。</summary>
    /// <param name="target">新目标对象。</param>
    [__DynamicallyInvokable]
    public void SetTarget(T target)
    {
      this.Target = target;
    }

    /// <summary>用序列化当前 <see cref="T:System.WeakReference`1" /> 对象所需的所有数据填充 <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 对象。</summary>
    /// <param name="info">一个对象，保存序列化或反序列化当前的 <see cref="T:System.WeakReference`1" /> 对象所需的全部数据。</param>
    /// <param name="context">存储和检索序列化数据的位置。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="info" /> 为 null。</exception>
    [SecurityCritical]
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException("info");
      info.AddValue("TrackedObject", (object) this.Target, typeof (T));
      info.AddValue("TrackResurrection", this.IsTrackResurrection());
    }

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private void Create(T target, bool trackResurrection);

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private bool IsTrackResurrection();
  }
}
