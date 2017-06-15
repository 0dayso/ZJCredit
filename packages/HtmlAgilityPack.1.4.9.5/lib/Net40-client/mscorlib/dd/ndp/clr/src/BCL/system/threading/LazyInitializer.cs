// Decompiled with JetBrains decompiler
// Type: System.Threading.LazyInitializer
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security.Permissions;

namespace System.Threading
{
  /// <summary>提供延迟初始化例程。</summary>
  [__DynamicallyInvokable]
  [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
  public static class LazyInitializer
  {
    /// <summary>在目标引用或值类型尚未初始化的情况下，使用其默认构造函数初始化目标引用或值类型。</summary>
    /// <returns>类型 <paramref name="T" /> 的初始化引用。</returns>
    /// <param name="target">在类型尚未初始化的情况下，要初始化的类型 <paramref name="T" /> 的引用。</param>
    /// <typeparam name="T">要初始化的引用的类型。</typeparam>
    /// <exception cref="T:System.MemberAccessException">缺少访问类型 <paramref name="T" /> 的构造函数的权限。</exception>
    /// <exception cref="T:System.MissingMemberException">类型 <paramref name="T" /> 没有默认的构造函数。</exception>
    [__DynamicallyInvokable]
    public static T EnsureInitialized<T>(ref T target) where T : class
    {
      if ((object) Volatile.Read<T>(ref target) != null)
        return target;
      return LazyInitializer.EnsureInitializedCore<T>(ref target, LazyHelpers<T>.s_activatorFactorySelector);
    }

    /// <summary>在目标引用类型尚未初始化的情况下，使用指定函数初始化目标引用类型。</summary>
    /// <returns>类型 <paramref name="T" /> 的初始化值。</returns>
    /// <param name="target">在类型尚未初始化的情况下，要初始化的类型 <paramref name="T" /> 的引用。</param>
    /// <param name="valueFactory">调用函数以初始化该引用。</param>
    /// <typeparam name="T">要初始化的引用的引用类型。</typeparam>
    /// <exception cref="T:System.MissingMemberException">类型 <paramref name="T" /> 没有默认的构造函数。</exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// <paramref name="valueFactory" /> 返回 null（在 Visual Basic 中为 Nothing）。</exception>
    [__DynamicallyInvokable]
    public static T EnsureInitialized<T>(ref T target, Func<T> valueFactory) where T : class
    {
      if ((object) Volatile.Read<T>(ref target) != null)
        return target;
      return LazyInitializer.EnsureInitializedCore<T>(ref target, valueFactory);
    }

    private static T EnsureInitializedCore<T>(ref T target, Func<T> valueFactory) where T : class
    {
      T obj = valueFactory();
      if ((object) obj == null)
        throw new InvalidOperationException(Environment.GetResourceString("Lazy_StaticInit_InvalidOperation"));
      Interlocked.CompareExchange<T>(ref target, obj, default (T));
      return target;
    }

    /// <summary>在目标引用或值类型尚未初始化的情况下，使用其默认构造函数初始化目标引用或值类型。</summary>
    /// <returns>类型 <paramref name="T" /> 的初始化值。</returns>
    /// <param name="target">在尚未初始化的情况下要初始化的类型 <paramref name="T" /> 的引用或值。</param>
    /// <param name="initialized">对布尔值的引用，该值确定目标是否已初始化。</param>
    /// <param name="syncLock">对用作相互排斥锁的对象的引用，用于初始化 <paramref name="target" />。如果 <paramref name="syncLock" /> 为 null，则新的对象将被实例化。</param>
    /// <typeparam name="T">要初始化的引用的类型。</typeparam>
    /// <exception cref="T:System.MemberAccessException">缺少访问类型 <paramref name="T" /> 的构造函数的权限。</exception>
    /// <exception cref="T:System.MissingMemberException">类型 <paramref name="T" /> 没有默认的构造函数。</exception>
    [__DynamicallyInvokable]
    public static T EnsureInitialized<T>(ref T target, ref bool initialized, ref object syncLock)
    {
      if (Volatile.Read(ref initialized))
        return target;
      return LazyInitializer.EnsureInitializedCore<T>(ref target, ref initialized, ref syncLock, LazyHelpers<T>.s_activatorFactorySelector);
    }

    /// <summary>在目标引用或值类型尚未初始化的情况下，使用指定函数初始化目标引用或值类型。</summary>
    /// <returns>类型 <paramref name="T" /> 的初始化值。</returns>
    /// <param name="target">在尚未初始化的情况下要初始化的类型 <paramref name="T" /> 的引用或值。</param>
    /// <param name="initialized">对布尔值的引用，该值确定目标是否已初始化。</param>
    /// <param name="syncLock">对用作相互排斥锁的对象的引用，用于初始化 <paramref name="target" />。如果 <paramref name="syncLock" /> 为 null，则新的对象将被实例化。</param>
    /// <param name="valueFactory">调用函数以初始化该引用或值。</param>
    /// <typeparam name="T">要初始化的引用的类型。</typeparam>
    /// <exception cref="T:System.MemberAccessException">缺少访问类型 <paramref name="T" /> 的构造函数的权限。</exception>
    /// <exception cref="T:System.MissingMemberException">类型 <paramref name="T" /> 没有默认的构造函数。</exception>
    [__DynamicallyInvokable]
    public static T EnsureInitialized<T>(ref T target, ref bool initialized, ref object syncLock, Func<T> valueFactory)
    {
      if (Volatile.Read(ref initialized))
        return target;
      return LazyInitializer.EnsureInitializedCore<T>(ref target, ref initialized, ref syncLock, valueFactory);
    }

    private static T EnsureInitializedCore<T>(ref T target, ref bool initialized, ref object syncLock, Func<T> valueFactory)
    {
      object obj1 = syncLock;
      if (obj1 == null)
      {
        object obj2 = new object();
        obj1 = Interlocked.CompareExchange(ref syncLock, obj2, (object) null) ?? obj2;
      }
      lock (obj1)
      {
        if (!Volatile.Read(ref initialized))
        {
          target = valueFactory();
          Volatile.Write(ref initialized, true);
        }
      }
      return target;
    }
  }
}
