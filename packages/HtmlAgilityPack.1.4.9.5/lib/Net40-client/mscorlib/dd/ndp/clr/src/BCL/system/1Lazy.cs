// Decompiled with JetBrains decompiler
// Type: System.Lazy`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Threading;

namespace System
{
  /// <summary>提供对延迟初始化的支持。</summary>
  /// <typeparam name="T">被延迟初始化的对象的类型。</typeparam>
  [ComVisible(false)]
  [DebuggerTypeProxy(typeof (System_LazyDebugView<>))]
  [DebuggerDisplay("ThreadSafetyMode={Mode}, IsValueCreated={IsValueCreated}, IsValueFaulted={IsValueFaulted}, Value={ValueForDebugDisplay}")]
  [__DynamicallyInvokable]
  [Serializable]
  [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
  public class Lazy<T>
  {
    private static readonly Func<T> ALREADY_INVOKED_SENTINEL = new Func<T>(Lazy<T>.\u003C\u003Ec.\u003C\u003E9.\u003C\u002Ecctor\u003Eb__27_0);
    private object m_boxed;
    [NonSerialized]
    private Func<T> m_valueFactory;
    [NonSerialized]
    private object m_threadSafeObj;

    internal T ValueForDebugDisplay
    {
      get
      {
        if (!this.IsValueCreated)
          return default (T);
        return ((Lazy<T>.Boxed) this.m_boxed).m_value;
      }
    }

    internal LazyThreadSafetyMode Mode
    {
      get
      {
        if (this.m_threadSafeObj == null)
          return LazyThreadSafetyMode.None;
        return this.m_threadSafeObj == LazyHelpers.PUBLICATION_ONLY_SENTINEL ? LazyThreadSafetyMode.PublicationOnly : LazyThreadSafetyMode.ExecutionAndPublication;
      }
    }

    internal bool IsValueFaulted
    {
      get
      {
        return this.m_boxed is Lazy<T>.LazyInternalExceptionHolder;
      }
    }

    /// <summary>获取一个值，该值指示是否已为此 <see cref="T:System.Lazy`1" /> 实例创建一个值。</summary>
    /// <returns>如果已为此 <see cref="T:System.Lazy`1" /> 实例创建了一个值，则为 true；否则为 false。</returns>
    [__DynamicallyInvokable]
    public bool IsValueCreated
    {
      [__DynamicallyInvokable] get
      {
        if (this.m_boxed != null)
          return this.m_boxed is Lazy<T>.Boxed;
        return false;
      }
    }

    /// <summary>获取当前 <see cref="T:System.Lazy`1" /> 实例的延迟初始化值。</summary>
    /// <returns>当前 <see cref="T:System.Lazy`1" /> 实例的延迟初始化值。</returns>
    /// <exception cref="T:System.MemberAccessException">
    /// <see cref="T:System.Lazy`1" /> 实例初始化为使用正在延迟初始化的类型的默认构造函数，并且缺少访问该构造函数的权限。</exception>
    /// <exception cref="T:System.MissingMemberException">
    /// <see cref="T:System.Lazy`1" /> 实例初始化为使用正在惰性初始化的类型的默认构造函数，并且该类型没有无参数的公共构造函数。</exception>
    /// <exception cref="T:System.InvalidOperationException">初始化函数尝试访问此实例上的 <see cref="P:System.Lazy`1.Value" />。</exception>
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    [__DynamicallyInvokable]
    public T Value
    {
      [__DynamicallyInvokable] get
      {
        if (this.m_boxed != null)
        {
          Lazy<T>.Boxed boxed = this.m_boxed as Lazy<T>.Boxed;
          if (boxed != null)
            return boxed.m_value;
          (this.m_boxed as Lazy<T>.LazyInternalExceptionHolder).m_edi.Throw();
        }
        Debugger.NotifyOfCrossThreadDependency();
        return this.LazyInitValue();
      }
    }

    /// <summary>初始化 <see cref="T:System.Lazy`1" /> 类的新实例。发生延迟初始化时，使用目标类型的默认构造函数。</summary>
    [__DynamicallyInvokable]
    public Lazy()
      : this(LazyThreadSafetyMode.ExecutionAndPublication)
    {
    }

    /// <summary>初始化 <see cref="T:System.Lazy`1" /> 类的新实例。发生延迟初始化时，使用指定的初始化函数。</summary>
    /// <param name="valueFactory">在需要时被调用以产生延迟初始化值的委托。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="valueFactory" /> 为 null。</exception>
    [__DynamicallyInvokable]
    public Lazy(Func<T> valueFactory)
      : this(valueFactory, LazyThreadSafetyMode.ExecutionAndPublication)
    {
    }

    /// <summary>初始化 <see cref="T:System.Lazy`1" /> 类的新实例。发生延迟初始化时，使用目标类型的默认构造函数和指定的初始化模式。</summary>
    /// <param name="isThreadSafe">true 表示此示例可由多个线程同时使用；false 表示此实例一次只能由一个线程使用。</param>
    [__DynamicallyInvokable]
    public Lazy(bool isThreadSafe)
      : this(isThreadSafe ? LazyThreadSafetyMode.ExecutionAndPublication : LazyThreadSafetyMode.None)
    {
    }

    /// <summary>初始化 <see cref="T:System.Lazy`1" /> 类的新实例，其中使用 <paramref name="T" /> 的默认构造函数和指定的线程安全性模式。</summary>
    /// <param name="mode">指定线程安全模式的枚举值之一。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="mode" /> 包含无效值。</exception>
    [__DynamicallyInvokable]
    public Lazy(LazyThreadSafetyMode mode)
    {
      this.m_threadSafeObj = Lazy<T>.GetObjectFromMode(mode);
    }

    /// <summary>初始化 <see cref="T:System.Lazy`1" /> 类的新实例。发生延迟初始化时，使用指定的初始化函数和初始化模式。</summary>
    /// <param name="valueFactory">在需要时被调用以产生延迟初始化值的委托。</param>
    /// <param name="isThreadSafe">true 表示此示例可由多个线程同时使用；false 表示此实例一次只能由一个线程使用。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="valueFactory" /> 为 null。</exception>
    [__DynamicallyInvokable]
    public Lazy(Func<T> valueFactory, bool isThreadSafe)
      : this(valueFactory, isThreadSafe ? LazyThreadSafetyMode.ExecutionAndPublication : LazyThreadSafetyMode.None)
    {
    }

    /// <summary>初始化 <see cref="T:System.Lazy`1" /> 类的新实例，其中使用指定的初始化函数和线程安全性模式。</summary>
    /// <param name="valueFactory">在需要时被调用以产生延迟初始化值的委托。</param>
    /// <param name="mode">指定线程安全模式的枚举值之一。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="mode" /> 包含无效值。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="valueFactory" /> 为 null。</exception>
    [__DynamicallyInvokable]
    public Lazy(Func<T> valueFactory, LazyThreadSafetyMode mode)
    {
      if (valueFactory == null)
        throw new ArgumentNullException("valueFactory");
      this.m_threadSafeObj = Lazy<T>.GetObjectFromMode(mode);
      this.m_valueFactory = valueFactory;
    }

    private static object GetObjectFromMode(LazyThreadSafetyMode mode)
    {
      if (mode == LazyThreadSafetyMode.ExecutionAndPublication)
        return new object();
      if (mode == LazyThreadSafetyMode.PublicationOnly)
        return LazyHelpers.PUBLICATION_ONLY_SENTINEL;
      if (mode != LazyThreadSafetyMode.None)
        throw new ArgumentOutOfRangeException("mode", Environment.GetResourceString("Lazy_ctor_ModeInvalid"));
      return (object) null;
    }

    [OnSerializing]
    private void OnSerializing(StreamingContext context)
    {
      T obj = this.Value;
    }

    /// <summary>创建并返回此实例的 <see cref="P:System.Lazy`1.Value" /> 属性的字符串表示形式。</summary>
    /// <returns>如果已创建该值（即，如果 <see cref="P:System.Lazy`1.IsValueCreated" /> 属性返回 true），则为对此实例的 <see cref="P:System.Lazy`1.Value" /> 属性调用 <see cref="M:System.Object.ToString" /> 方法所获得的结果。否则为一个指示尚未创建该值的字符串。</returns>
    /// <exception cref="T:System.NullReferenceException">
    /// <see cref="P:System.Lazy`1.Value" /> 属性为 null。</exception>
    [__DynamicallyInvokable]
    public override string ToString()
    {
      if (!this.IsValueCreated)
        return Environment.GetResourceString("Lazy_ToString_ValueNotCreated");
      return this.Value.ToString();
    }

    private T LazyInitValue()
    {
      Lazy<T>.Boxed boxed = (Lazy<T>.Boxed) null;
      switch (this.Mode)
      {
        case LazyThreadSafetyMode.None:
          boxed = this.CreateValue();
          this.m_boxed = (object) boxed;
          break;
        case LazyThreadSafetyMode.PublicationOnly:
          boxed = this.CreateValue();
          if (boxed == null || Interlocked.CompareExchange(ref this.m_boxed, (object) boxed, (object) null) != null)
          {
            boxed = (Lazy<T>.Boxed) this.m_boxed;
            break;
          }
          this.m_valueFactory = Lazy<T>.ALREADY_INVOKED_SENTINEL;
          break;
        default:
          object obj = Volatile.Read<object>(ref this.m_threadSafeObj);
          bool lockTaken = false;
          try
          {
            if (obj != Lazy<T>.ALREADY_INVOKED_SENTINEL)
              Monitor.Enter(obj, ref lockTaken);
            if (this.m_boxed == null)
            {
              boxed = this.CreateValue();
              this.m_boxed = (object) boxed;
              Volatile.Write<object>(ref this.m_threadSafeObj, (object) Lazy<T>.ALREADY_INVOKED_SENTINEL);
              break;
            }
            boxed = this.m_boxed as Lazy<T>.Boxed;
            if (boxed == null)
            {
              (this.m_boxed as Lazy<T>.LazyInternalExceptionHolder).m_edi.Throw();
              break;
            }
            break;
          }
          finally
          {
            if (lockTaken)
              Monitor.Exit(obj);
          }
      }
      return boxed.m_value;
    }

    private Lazy<T>.Boxed CreateValue()
    {
      LazyThreadSafetyMode mode = this.Mode;
      if (this.m_valueFactory != null)
      {
        try
        {
          if (mode != LazyThreadSafetyMode.PublicationOnly && this.m_valueFactory == Lazy<T>.ALREADY_INVOKED_SENTINEL)
            throw new InvalidOperationException(Environment.GetResourceString("Lazy_Value_RecursiveCallsToValue"));
          Func<T> func = this.m_valueFactory;
          if (mode != LazyThreadSafetyMode.PublicationOnly)
            this.m_valueFactory = Lazy<T>.ALREADY_INVOKED_SENTINEL;
          else if (func == Lazy<T>.ALREADY_INVOKED_SENTINEL)
            return (Lazy<T>.Boxed) null;
          return new Lazy<T>.Boxed(func());
        }
        catch (Exception ex)
        {
          if (mode != LazyThreadSafetyMode.PublicationOnly)
            this.m_boxed = (object) new Lazy<T>.LazyInternalExceptionHolder(ex);
          throw;
        }
      }
      else
      {
        try
        {
          return new Lazy<T>.Boxed((T) Activator.CreateInstance(typeof (T)));
        }
        catch (MissingMethodException ex1)
        {
          Exception ex2 = (Exception) new MissingMemberException(Environment.GetResourceString("Lazy_CreateValue_NoParameterlessCtorForT"));
          if (mode != LazyThreadSafetyMode.PublicationOnly)
            this.m_boxed = (object) new Lazy<T>.LazyInternalExceptionHolder(ex2);
          throw ex2;
        }
      }
    }

    [Serializable]
    private class Boxed
    {
      internal T m_value;

      internal Boxed(T value)
      {
        this.m_value = value;
      }
    }

    private class LazyInternalExceptionHolder
    {
      internal ExceptionDispatchInfo m_edi;

      internal LazyInternalExceptionHolder(Exception ex)
      {
        this.m_edi = ExceptionDispatchInfo.Capture(ex);
      }
    }
  }
}
