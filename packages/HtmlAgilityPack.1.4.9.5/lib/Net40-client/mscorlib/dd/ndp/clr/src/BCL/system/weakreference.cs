// Decompiled with JetBrains decompiler
// Type: System.WeakReference
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;

namespace System
{
  /// <summary>表示弱引用，即在引用对象的同时仍然允许通过垃圾回收来回收该对象。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  [SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
  public class WeakReference : ISerializable
  {
    internal IntPtr m_handle;

    /// <summary>获取当前 <see cref="T:System.WeakReference" /> 对象引用的对象是否已被垃圾回收的指示。</summary>
    /// <returns>如果当前 <see cref="T:System.WeakReference" /> 对象引用的对象尚未被垃圾回收且仍然可访问，则为 true；否则为 false。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public virtual bool IsAlive { [SecuritySafeCritical, __DynamicallyInvokable, MethodImpl(MethodImplOptions.InternalCall)] get; }

    /// <summary>获取当前 <see cref="T:System.WeakReference" /> 对象引用的对象在终止后是否会被跟踪的指示。</summary>
    /// <returns>如果当前 <see cref="T:System.WeakReference" /> 对象引用的对象在终止后会被跟踪，则为 true；否则，如果该对象仅在终止前被跟踪，则为 false。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public virtual bool TrackResurrection
    {
      [__DynamicallyInvokable] get
      {
        return this.IsTrackResurrection();
      }
    }

    /// <summary>获取或设置当前 <see cref="T:System.WeakReference" /> 对象引用的对象（目标）。</summary>
    /// <returns>如果当前 <see cref="T:System.WeakReference" /> 对象引用的对象已被垃圾回收，则为 null；否则为对该对象（当前 <see cref="T:System.WeakReference" /> 对象引用的对象）的引用。</returns>
    /// <exception cref="T:System.InvalidOperationException">对目标对象的引用无效。如果值为 null 引用或者已经在设置操作过程中完成对象，则设置该属性时可能引发此异常。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public virtual object Target { [SecuritySafeCritical, __DynamicallyInvokable, MethodImpl(MethodImplOptions.InternalCall)] get; [SecuritySafeCritical, __DynamicallyInvokable, MethodImpl(MethodImplOptions.InternalCall)] set; }

    /// <summary>引用指定的对象初始化 <see cref="T:System.WeakReference" /> 类的新实例。</summary>
    /// <param name="target">要跟踪的对象或为 null。</param>
    [__DynamicallyInvokable]
    public WeakReference(object target)
      : this(target, false)
    {
    }

    /// <summary>初始化 <see cref="T:System.WeakReference" /> 类的新实例，引用指定的对象并使用指定的复活跟踪。</summary>
    /// <param name="target">要跟踪的对象。</param>
    /// <param name="trackResurrection">指示何时停止跟踪对象。如果为 true，则在终结后跟踪对象；如果为 false，则仅在终结前跟踪对象。</param>
    [__DynamicallyInvokable]
    public WeakReference(object target, bool trackResurrection)
    {
      this.Create(target, trackResurrection);
    }

    /// <summary>使用从指定的序列化和流对象反序列化的数据，初始化 <see cref="T:System.WeakReference" /> 类的新实例。</summary>
    /// <param name="info">保存序列化或反序列化当前的 <see cref="T:System.WeakReference" /> 对象所需的全部数据的对象。</param>
    /// <param name="context">（保留）描述由 <paramref name="info" /> 指定的序列化流的源和目标。 </param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="info" /> 为 null。</exception>
    protected WeakReference(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException("info");
      this.Create(info.GetValue("TrackedObject", typeof (object)), info.GetBoolean("TrackResurrection"));
    }

    /// <summary>丢弃对当前 <see cref="T:System.WeakReference" /> 对象表示的目标的引用。</summary>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    ~WeakReference();

    /// <summary>使用序列化当前的 <see cref="T:System.WeakReference" /> 对象所需的所有数据填充 <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 对象。</summary>
    /// <param name="info">保存序列化或反序列化当前的 <see cref="T:System.WeakReference" /> 对象所需的全部数据的对象。</param>
    /// <param name="context">（保留）存储和检索序列化数据的位置。 </param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="info" /> 为 null。</exception>
    /// <filterpriority>2</filterpriority>
    [SecurityCritical]
    public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException("info");
      info.AddValue("TrackedObject", this.Target, typeof (object));
      info.AddValue("TrackResurrection", this.IsTrackResurrection());
    }

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private void Create(object target, bool trackResurrection);

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private bool IsTrackResurrection();
  }
}
