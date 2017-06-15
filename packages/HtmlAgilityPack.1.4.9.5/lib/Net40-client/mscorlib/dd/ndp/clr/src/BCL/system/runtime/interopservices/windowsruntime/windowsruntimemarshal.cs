// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.WindowsRuntimeMarshal
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Policy;
using System.Threading;

namespace System.Runtime.InteropServices.WindowsRuntime
{
  /// <summary>为整理 .NET Framework 和 Windows 运行时 之间的数据提供帮助程序方法。</summary>
  public static class WindowsRuntimeMarshal
  {
    private static bool s_haveBlueErrorApis = true;
    private static Guid s_iidIErrorInfo = new Guid(485667104, (short) 21629, (short) 4123, (byte) 142, (byte) 101, (byte) 8, (byte) 0, (byte) 43, (byte) 43, (byte) 209, (byte) 25);
    private static IntPtr s_pClassActivator = IntPtr.Zero;

    /// <summary>添加指定的事件处理程序到 Windows 运行时 事件。</summary>
    /// <param name="addMethod">表示向 Windows 运行时 事件添加事件处理程序的方法的委托。</param>
    /// <param name="removeMethod">表示从 Windows 运行时 事件中移除事件处理程序的方法的委托。</param>
    /// <param name="handler">表示添加的事件处理程序的委托。</param>
    /// <typeparam name="T">代表事件处理程序委托的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="addMethod" /> 为 null。- 或 -<paramref name="removeMethod" /> 为 null。</exception>
    [SecurityCritical]
    public static void AddEventHandler<T>(Func<T, EventRegistrationToken> addMethod, Action<EventRegistrationToken> removeMethod, T handler)
    {
      if (addMethod == null)
        throw new ArgumentNullException("addMethod");
      if (removeMethod == null)
        throw new ArgumentNullException("removeMethod");
      if ((object) handler == null)
        return;
      object target = removeMethod.Target;
      if (target == null || Marshal.IsComObject(target))
        WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.AddEventHandler<T>(addMethod, removeMethod, handler);
      else
        WindowsRuntimeMarshal.ManagedEventRegistrationImpl.AddEventHandler<T>(addMethod, removeMethod, handler);
    }

    /// <summary>从 Windows 运行时 事件中移除指定事件处理程序。</summary>
    /// <param name="removeMethod">表示从 Windows 运行时 事件中移除事件处理程序的方法的委托。</param>
    /// <param name="handler">要移除的事件处理程序。</param>
    /// <typeparam name="T">代表事件处理程序委托的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="removeMethod" /> 为 null。</exception>
    [SecurityCritical]
    public static void RemoveEventHandler<T>(Action<EventRegistrationToken> removeMethod, T handler)
    {
      if (removeMethod == null)
        throw new ArgumentNullException("removeMethod");
      if ((object) handler == null)
        return;
      object target = removeMethod.Target;
      if (target == null || Marshal.IsComObject(target))
        WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.RemoveEventHandler<T>(removeMethod, handler);
      else
        WindowsRuntimeMarshal.ManagedEventRegistrationImpl.RemoveEventHandler<T>(removeMethod, handler);
    }

    /// <summary>移除所有事件处理程序，它们都可使用指定方法移除。</summary>
    /// <param name="removeMethod">表示从 Windows 运行时 事件中移除事件处理程序的方法的委托。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="removeMethod" /> 为 null。</exception>
    [SecurityCritical]
    public static void RemoveAllEventHandlers(Action<EventRegistrationToken> removeMethod)
    {
      if (removeMethod == null)
        throw new ArgumentNullException("removeMethod");
      object target = removeMethod.Target;
      if (target == null || Marshal.IsComObject(target))
        WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.RemoveAllEventHandlers(removeMethod);
      else
        WindowsRuntimeMarshal.ManagedEventRegistrationImpl.RemoveAllEventHandlers(removeMethod);
    }

    internal static int GetRegistrationTokenCacheSize()
    {
      int num = 0;
      if (WindowsRuntimeMarshal.ManagedEventRegistrationImpl.s_eventRegistrations != null)
      {
        lock (WindowsRuntimeMarshal.ManagedEventRegistrationImpl.s_eventRegistrations)
          num += WindowsRuntimeMarshal.ManagedEventRegistrationImpl.s_eventRegistrations.Keys.Count;
      }
      if (WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventRegistrations != null)
      {
        lock (WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventRegistrations)
          num += WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventRegistrations.Count;
      }
      return num;
    }

    internal static void CallRemoveMethods(Action<EventRegistrationToken> removeMethod, List<EventRegistrationToken> tokensToRemove)
    {
      List<Exception> exceptionList = new List<Exception>();
      foreach (EventRegistrationToken registrationToken in tokensToRemove)
      {
        try
        {
          removeMethod(registrationToken);
        }
        catch (Exception ex)
        {
          exceptionList.Add(ex);
        }
      }
      if (exceptionList.Count > 0)
        throw new AggregateException(exceptionList.ToArray());
    }

    [SecurityCritical]
    internal static unsafe string HStringToString(IntPtr hstring)
    {
      if (hstring == IntPtr.Zero)
        return string.Empty;
      uint num;
      return new string(UnsafeNativeMethods.WindowsGetStringRawBuffer(hstring, &num), 0, checked ((int) num));
    }

    internal static Exception GetExceptionForHR(int hresult, Exception innerException, string messageResource)
    {
      Exception exception;
      if (innerException != null)
      {
        string message = innerException.Message;
        if (message == null && messageResource != null)
          message = Environment.GetResourceString(messageResource);
        exception = new Exception(message, innerException);
      }
      else
        exception = new Exception(messageResource != null ? Environment.GetResourceString(messageResource) : (string) null);
      exception.SetErrorCode(hresult);
      return exception;
    }

    internal static Exception GetExceptionForHR(int hresult, Exception innerException)
    {
      return WindowsRuntimeMarshal.GetExceptionForHR(hresult, innerException, (string) null);
    }

    [SecurityCritical]
    private static bool RoOriginateLanguageException(int error, string message, IntPtr languageException)
    {
      if (WindowsRuntimeMarshal.s_haveBlueErrorApis)
      {
        try
        {
          return UnsafeNativeMethods.RoOriginateLanguageException(error, message, languageException);
        }
        catch (EntryPointNotFoundException ex)
        {
          WindowsRuntimeMarshal.s_haveBlueErrorApis = false;
        }
      }
      return false;
    }

    [SecurityCritical]
    private static void RoReportUnhandledError(IRestrictedErrorInfo error)
    {
      if (!WindowsRuntimeMarshal.s_haveBlueErrorApis)
        return;
      try
      {
        UnsafeNativeMethods.RoReportUnhandledError(error);
      }
      catch (EntryPointNotFoundException ex)
      {
        WindowsRuntimeMarshal.s_haveBlueErrorApis = false;
      }
    }

    [FriendAccessAllowed]
    [SecuritySafeCritical]
    internal static bool ReportUnhandledError(Exception e)
    {
      if (!AppDomain.IsAppXModel() || !WindowsRuntimeMarshal.s_haveBlueErrorApis || e == null)
        return false;
      IntPtr pUnk = IntPtr.Zero;
      IntPtr ppv = IntPtr.Zero;
      try
      {
        pUnk = Marshal.GetIUnknownForObject((object) e);
        if (pUnk != IntPtr.Zero)
        {
          Marshal.QueryInterface(pUnk, ref WindowsRuntimeMarshal.s_iidIErrorInfo, out ppv);
          if (ppv != IntPtr.Zero)
          {
            if (WindowsRuntimeMarshal.RoOriginateLanguageException(Marshal.GetHRForException_WinRT(e), e.Message, ppv))
            {
              IRestrictedErrorInfo restrictedErrorInfo = UnsafeNativeMethods.GetRestrictedErrorInfo();
              if (restrictedErrorInfo != null)
              {
                WindowsRuntimeMarshal.RoReportUnhandledError(restrictedErrorInfo);
                return true;
              }
            }
          }
        }
      }
      finally
      {
        if (ppv != IntPtr.Zero)
          Marshal.Release(ppv);
        if (pUnk != IntPtr.Zero)
          Marshal.Release(pUnk);
      }
      return false;
    }

    [SecurityCritical]
    internal static IntPtr GetActivationFactoryForType(Type type)
    {
      return Marshal.GetComInterfaceForObject((object) WindowsRuntimeMarshal.GetManagedActivationFactory(type), typeof (IActivationFactory));
    }

    [SecurityCritical]
    internal static ManagedActivationFactory GetManagedActivationFactory(Type type)
    {
      ManagedActivationFactory activationFactory = new ManagedActivationFactory(type);
      RuntimeType runtimeClassType = (RuntimeType) type;
      Marshal.InitializeManagedWinRTFactoryObject((object) activationFactory, runtimeClassType);
      return activationFactory;
    }

    [SecurityCritical]
    internal static IntPtr GetClassActivatorForApplication(string appBase)
    {
      if (WindowsRuntimeMarshal.s_pClassActivator == IntPtr.Zero)
      {
        AppDomainSetup info = new AppDomainSetup() { ApplicationBase = appBase };
        AppDomain domain = AppDomain.CreateDomain(Environment.GetResourceString("WinRTHostDomainName", (object) appBase), (Evidence) null, info);
        IntPtr rtClassActivator = ((WinRTClassActivator) domain.CreateInstanceAndUnwrap(typeof (WinRTClassActivator).Assembly.FullName, typeof (WinRTClassActivator).FullName)).GetIWinRTClassActivator();
        if (Interlocked.CompareExchange(ref WindowsRuntimeMarshal.s_pClassActivator, rtClassActivator, IntPtr.Zero) != IntPtr.Zero)
        {
          Marshal.Release(rtClassActivator);
          try
          {
            AppDomain.Unload(domain);
          }
          catch (CannotUnloadAppDomainException ex)
          {
          }
        }
      }
      Marshal.AddRef(WindowsRuntimeMarshal.s_pClassActivator);
      return WindowsRuntimeMarshal.s_pClassActivator;
    }

    /// <summary>返回指定的 Windows 运行时 类型实现工厂接口激活的对象。</summary>
    /// <returns>一个对象，实现激活工厂接口。</returns>
    /// <param name="type">获取激活接口的对象的 Windows 运行时 类型。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="type" /> 不表示 Windows 运行时 类型（即属于 Windows 运行时 或定义在 Windows 运行时 组件中）。- 或 -公共语言执行类型系统没有提供用于 <paramref name="type" /> 的指定的该对象。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="type" /> 为 null。</exception>
    /// <exception cref="T:System.TypeLoadException">指定的 Windows 运行时 类没有正确注册。例如，.winmd 文件被定位，但 Windows 运行时 定位实现失败。</exception>
    [SecurityCritical]
    public static IActivationFactory GetActivationFactory(Type type)
    {
      if (type == (Type) null)
        throw new ArgumentNullException("type");
      if (type.IsWindowsRuntimeObject && type.IsImport)
        return (IActivationFactory) Marshal.GetNativeActivationFactory(type);
      return (IActivationFactory) WindowsRuntimeMarshal.GetManagedActivationFactory(type);
    }

    /// <summary>分配 Windows 运行时 HSTRING 并为其复制指定的托管字符串。</summary>
    /// <returns>复制的新 HSTRING 的非托管指针，或者 <see cref="F:System.IntPtr.Zero" /> 如果 <paramref name="s" /> 是 <see cref="F:System.String.Empty" />。</returns>
    /// <param name="s">要复制的托管字符串。</param>
    /// <exception cref="T:System.PlatformNotSupportedException">操作系统的当前版本不支持 Windows 运行时。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="s" /> 为 null。</exception>
    [SecurityCritical]
    public static unsafe IntPtr StringToHString(string s)
    {
      if (!Environment.IsWinRTSupported)
        throw new PlatformNotSupportedException(Environment.GetResourceString("PlatformNotSupported_WinRT"));
      if (s == null)
        throw new ArgumentNullException("s");
      string sourceString = s;
      int length = sourceString.Length;
      IntPtr num1;
      IntPtr num2 = (IntPtr) &num1;
      Marshal.ThrowExceptionForHR(UnsafeNativeMethods.WindowsCreateString(sourceString, length, (IntPtr*) num2), new IntPtr(-1));
      return num1;
    }

    /// <summary>返回包含指定 Windows 运行时 HSTRING 副本的托管字符串.</summary>
    /// <returns>如果 <paramref name="ptr" /> 不是 <see cref="F:System.IntPtr.Zero" />，则托管字符串包含 HSTRING 副本；否则为 <see cref="F:System.String.Empty" />。</returns>
    /// <param name="ptr">复制的 HSTRING 非托管指针。</param>
    /// <exception cref="T:System.PlatformNotSupportedException">操作系统的当前版本不支持 Windows 运行时。</exception>
    [SecurityCritical]
    public static string PtrToStringHString(IntPtr ptr)
    {
      if (!Environment.IsWinRTSupported)
        throw new PlatformNotSupportedException(Environment.GetResourceString("PlatformNotSupported_WinRT"));
      return WindowsRuntimeMarshal.HStringToString(ptr);
    }

    /// <summary>释放指定的 Windows 运行时 HSTRING。</summary>
    /// <param name="ptr">要释放的 HSTRING 的地址。</param>
    /// <exception cref="T:System.PlatformNotSupportedException">操作系统的当前版本不支持 Windows 运行时。</exception>
    [SecurityCritical]
    public static void FreeHString(IntPtr ptr)
    {
      if (!Environment.IsWinRTSupported)
        throw new PlatformNotSupportedException(Environment.GetResourceString("PlatformNotSupported_WinRT"));
      if (!(ptr != IntPtr.Zero))
        return;
      UnsafeNativeMethods.WindowsDeleteString(ptr);
    }

    internal struct EventRegistrationTokenList
    {
      private EventRegistrationToken firstToken;
      private List<EventRegistrationToken> restTokens;

      internal EventRegistrationTokenList(EventRegistrationToken token)
      {
        this.firstToken = token;
        this.restTokens = (List<EventRegistrationToken>) null;
      }

      internal EventRegistrationTokenList(WindowsRuntimeMarshal.EventRegistrationTokenList list)
      {
        this.firstToken = list.firstToken;
        this.restTokens = list.restTokens;
      }

      public bool Push(EventRegistrationToken token)
      {
        bool flag = false;
        if (this.restTokens == null)
        {
          this.restTokens = new List<EventRegistrationToken>();
          flag = true;
        }
        this.restTokens.Add(token);
        return flag;
      }

      public bool Pop(out EventRegistrationToken token)
      {
        if (this.restTokens == null || this.restTokens.Count == 0)
        {
          token = this.firstToken;
          return false;
        }
        int index = this.restTokens.Count - 1;
        token = this.restTokens[index];
        this.restTokens.RemoveAt(index);
        return true;
      }

      public void CopyTo(List<EventRegistrationToken> tokens)
      {
        tokens.Add(this.firstToken);
        if (this.restTokens == null)
          return;
        tokens.AddRange((IEnumerable<EventRegistrationToken>) this.restTokens);
      }
    }

    internal static class ManagedEventRegistrationImpl
    {
      internal static volatile ConditionalWeakTable<object, Dictionary<MethodInfo, Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList>>> s_eventRegistrations = new ConditionalWeakTable<object, Dictionary<MethodInfo, Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList>>>();

      [SecurityCritical]
      internal static void AddEventHandler<T>(Func<T, EventRegistrationToken> addMethod, Action<EventRegistrationToken> removeMethod, T handler)
      {
        Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList> registrationTokenTable = WindowsRuntimeMarshal.ManagedEventRegistrationImpl.GetEventRegistrationTokenTable(removeMethod.Target, removeMethod);
        EventRegistrationToken token = addMethod(handler);
        lock (registrationTokenTable)
        {
          WindowsRuntimeMarshal.EventRegistrationTokenList local_4;
          if (!registrationTokenTable.TryGetValue((object) handler, out local_4))
          {
            local_4 = new WindowsRuntimeMarshal.EventRegistrationTokenList(token);
            registrationTokenTable[(object) handler] = local_4;
          }
          else
          {
            if (!local_4.Push(token))
              return;
            registrationTokenTable[(object) handler] = local_4;
          }
        }
      }

      private static Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList> GetEventRegistrationTokenTable(object instance, Action<EventRegistrationToken> removeMethod)
      {
        lock (WindowsRuntimeMarshal.ManagedEventRegistrationImpl.s_eventRegistrations)
        {
          Dictionary<MethodInfo, Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList>> local_2 = (Dictionary<MethodInfo, Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList>>) null;
          if (!WindowsRuntimeMarshal.ManagedEventRegistrationImpl.s_eventRegistrations.TryGetValue(instance, out local_2))
          {
            local_2 = new Dictionary<MethodInfo, Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList>>();
            WindowsRuntimeMarshal.ManagedEventRegistrationImpl.s_eventRegistrations.Add(instance, local_2);
          }
          Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList> local_3 = (Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList>) null;
          if (!local_2.TryGetValue(removeMethod.Method, out local_3))
          {
            local_3 = new Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList>();
            local_2.Add(removeMethod.Method, local_3);
          }
          return local_3;
        }
      }

      [SecurityCritical]
      internal static void RemoveEventHandler<T>(Action<EventRegistrationToken> removeMethod, T handler)
      {
        Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList> registrationTokenTable = WindowsRuntimeMarshal.ManagedEventRegistrationImpl.GetEventRegistrationTokenTable(removeMethod.Target, removeMethod);
        EventRegistrationToken token;
        lock (registrationTokenTable)
        {
          WindowsRuntimeMarshal.EventRegistrationTokenList local_4;
          if (!registrationTokenTable.TryGetValue((object) handler, out local_4))
            return;
          if (!local_4.Pop(out token))
            registrationTokenTable.Remove((object) handler);
        }
        removeMethod(token);
      }

      [SecurityCritical]
      internal static void RemoveAllEventHandlers(Action<EventRegistrationToken> removeMethod)
      {
        Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList> registrationTokenTable = WindowsRuntimeMarshal.ManagedEventRegistrationImpl.GetEventRegistrationTokenTable(removeMethod.Target, removeMethod);
        List<EventRegistrationToken> registrationTokenList = new List<EventRegistrationToken>();
        lock (registrationTokenTable)
        {
          foreach (WindowsRuntimeMarshal.EventRegistrationTokenList item_0 in registrationTokenTable.Values)
            item_0.CopyTo(registrationTokenList);
          registrationTokenTable.Clear();
        }
        WindowsRuntimeMarshal.CallRemoveMethods(removeMethod, registrationTokenList);
      }
    }

    internal static class NativeOrStaticEventRegistrationImpl
    {
      internal static volatile Dictionary<WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKey, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheEntry> s_eventRegistrations = new Dictionary<WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKey, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheEntry>((IEqualityComparer<WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKey>) new WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKeyEqualityComparer());
      private static volatile WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.MyReaderWriterLock s_eventCacheRWLock = new WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.MyReaderWriterLock();

      [SecuritySafeCritical]
      private static object GetInstanceKey(Action<EventRegistrationToken> removeMethod)
      {
        object target = removeMethod.Target;
        if (target == null)
          return (object) removeMethod.Method.DeclaringType;
        return (object) Marshal.GetRawIUnknownForComObjectNoAddRef(target);
      }

      [SecurityCritical]
      internal static void AddEventHandler<T>(Func<T, EventRegistrationToken> addMethod, Action<EventRegistrationToken> removeMethod, T handler)
      {
        object instanceKey = WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.GetInstanceKey(removeMethod);
        EventRegistrationToken token = addMethod(handler);
        bool flag = false;
        try
        {
          WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventCacheRWLock.AcquireReaderLock(-1);
          try
          {
            WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.TokenListCount tokenListCount;
            ConditionalWeakTable<object, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount> registrationTokenTable = WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.GetOrCreateEventRegistrationTokenTable(instanceKey, removeMethod, out tokenListCount);
            lock (registrationTokenTable)
            {
              WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount local_3;
              if (registrationTokenTable.FindEquivalentKeyUnsafe((object) handler, out local_3) == null)
              {
                local_3 = new WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount(tokenListCount, token);
                registrationTokenTable.Add((object) handler, local_3);
              }
              else
                local_3.Push(token);
              flag = true;
            }
          }
          finally
          {
            WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventCacheRWLock.ReleaseReaderLock();
          }
        }
        catch (Exception ex)
        {
          if (!flag)
            removeMethod(token);
          throw;
        }
      }

      private static ConditionalWeakTable<object, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount> GetEventRegistrationTokenTableNoCreate(object instance, Action<EventRegistrationToken> removeMethod, out WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.TokenListCount tokenListCount)
      {
        return WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.GetEventRegistrationTokenTableInternal(instance, removeMethod, out tokenListCount, false);
      }

      private static ConditionalWeakTable<object, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount> GetOrCreateEventRegistrationTokenTable(object instance, Action<EventRegistrationToken> removeMethod, out WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.TokenListCount tokenListCount)
      {
        return WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.GetEventRegistrationTokenTableInternal(instance, removeMethod, out tokenListCount, true);
      }

      private static ConditionalWeakTable<object, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount> GetEventRegistrationTokenTableInternal(object instance, Action<EventRegistrationToken> removeMethod, out WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.TokenListCount tokenListCount, bool createIfNotFound)
      {
        WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKey key;
        key.target = instance;
        key.method = removeMethod.Method;
        lock (WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventRegistrations)
        {
          WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheEntry local_3;
          if (!WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventRegistrations.TryGetValue(key, out local_3))
          {
            if (!createIfNotFound)
            {
              tokenListCount = (WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.TokenListCount) null;
              return (ConditionalWeakTable<object, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount>) null;
            }
            local_3 = new WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheEntry();
            local_3.registrationTable = new ConditionalWeakTable<object, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount>();
            local_3.tokenListCount = new WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.TokenListCount(key);
            WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventRegistrations.Add(key, local_3);
          }
          tokenListCount = local_3.tokenListCount;
          return local_3.registrationTable;
        }
      }

      [SecurityCritical]
      internal static void RemoveEventHandler<T>(Action<EventRegistrationToken> removeMethod, T handler)
      {
        object instanceKey = WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.GetInstanceKey(removeMethod);
        WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventCacheRWLock.AcquireReaderLock(-1);
        EventRegistrationToken token;
        try
        {
          WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.TokenListCount tokenListCount;
          ConditionalWeakTable<object, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount> tokenTableNoCreate = WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.GetEventRegistrationTokenTableNoCreate(instanceKey, removeMethod, out tokenListCount);
          if (tokenTableNoCreate == null)
            return;
          lock (tokenTableNoCreate)
          {
            WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount local_6;
            object local_7 = tokenTableNoCreate.FindEquivalentKeyUnsafe((object) handler, out local_6);
            if (local_6 == null)
              return;
            if (!local_6.Pop(out token))
              tokenTableNoCreate.Remove(local_7);
          }
        }
        finally
        {
          WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventCacheRWLock.ReleaseReaderLock();
        }
        removeMethod(token);
      }

      [SecurityCritical]
      internal static void RemoveAllEventHandlers(Action<EventRegistrationToken> removeMethod)
      {
        object instanceKey = WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.GetInstanceKey(removeMethod);
        List<EventRegistrationToken> registrationTokenList = new List<EventRegistrationToken>();
        WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventCacheRWLock.AcquireReaderLock(-1);
        try
        {
          WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.TokenListCount tokenListCount;
          ConditionalWeakTable<object, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount> tokenTableNoCreate = WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.GetEventRegistrationTokenTableNoCreate(instanceKey, removeMethod, out tokenListCount);
          if (tokenTableNoCreate == null)
            return;
          lock (tokenTableNoCreate)
          {
            foreach (WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount item_0 in (IEnumerable<WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount>) tokenTableNoCreate.Values)
              item_0.CopyTo(registrationTokenList);
            tokenTableNoCreate.Clear();
          }
        }
        finally
        {
          WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventCacheRWLock.ReleaseReaderLock();
        }
        WindowsRuntimeMarshal.CallRemoveMethods(removeMethod, registrationTokenList);
      }

      internal struct EventCacheKey
      {
        internal object target;
        internal MethodInfo method;

        public override string ToString()
        {
          return "(" + this.target + ", " + (object) this.method + ")";
        }
      }

      internal class EventCacheKeyEqualityComparer : IEqualityComparer<WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKey>
      {
        public bool Equals(WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKey lhs, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKey rhs)
        {
          if (object.Equals(lhs.target, rhs.target))
            return object.Equals((object) lhs.method, (object) rhs.method);
          return false;
        }

        public int GetHashCode(WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKey key)
        {
          return key.target.GetHashCode() ^ key.method.GetHashCode();
        }
      }

      internal class EventRegistrationTokenListWithCount
      {
        private WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.TokenListCount _tokenListCount;
        private WindowsRuntimeMarshal.EventRegistrationTokenList _tokenList;

        internal EventRegistrationTokenListWithCount(WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.TokenListCount tokenListCount, EventRegistrationToken token)
        {
          this._tokenListCount = tokenListCount;
          this._tokenListCount.Inc();
          this._tokenList = new WindowsRuntimeMarshal.EventRegistrationTokenList(token);
        }

        ~EventRegistrationTokenListWithCount()
        {
          this._tokenListCount.Dec();
        }

        public void Push(EventRegistrationToken token)
        {
          this._tokenList.Push(token);
        }

        public bool Pop(out EventRegistrationToken token)
        {
          return this._tokenList.Pop(out token);
        }

        public void CopyTo(List<EventRegistrationToken> tokens)
        {
          this._tokenList.CopyTo(tokens);
        }
      }

      internal class TokenListCount
      {
        private int _count;
        private WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKey _key;

        internal WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKey Key
        {
          get
          {
            return this._key;
          }
        }

        internal TokenListCount(WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKey key)
        {
          this._key = key;
        }

        internal void Inc()
        {
          Interlocked.Increment(ref this._count);
        }

        internal void Dec()
        {
          WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventCacheRWLock.AcquireWriterLock(-1);
          try
          {
            if (Interlocked.Decrement(ref this._count) != 0)
              return;
            this.CleanupCache();
          }
          finally
          {
            WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventCacheRWLock.ReleaseWriterLock();
          }
        }

        private void CleanupCache()
        {
          WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventRegistrations.Remove(this._key);
        }
      }

      internal struct EventCacheEntry
      {
        internal ConditionalWeakTable<object, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount> registrationTable;
        internal WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.TokenListCount tokenListCount;
      }

      internal class ReaderWriterLockTimedOutException : ApplicationException
      {
      }

      internal class MyReaderWriterLock
      {
        private int myLock;
        private int owners;
        private uint numWriteWaiters;
        private uint numReadWaiters;
        private EventWaitHandle writeEvent;
        private EventWaitHandle readEvent;

        internal MyReaderWriterLock()
        {
        }

        internal void AcquireReaderLock(int millisecondsTimeout)
        {
          this.EnterMyLock();
          while (this.owners < 0 || (int) this.numWriteWaiters != 0)
          {
            if (this.readEvent == null)
              this.LazyCreateEvent(ref this.readEvent, false);
            else
              this.WaitOnEvent(this.readEvent, ref this.numReadWaiters, millisecondsTimeout);
          }
          this.owners = this.owners + 1;
          this.ExitMyLock();
        }

        internal void AcquireWriterLock(int millisecondsTimeout)
        {
          this.EnterMyLock();
          while (this.owners != 0)
          {
            if (this.writeEvent == null)
              this.LazyCreateEvent(ref this.writeEvent, true);
            else
              this.WaitOnEvent(this.writeEvent, ref this.numWriteWaiters, millisecondsTimeout);
          }
          this.owners = -1;
          this.ExitMyLock();
        }

        internal void ReleaseReaderLock()
        {
          this.EnterMyLock();
          this.owners = this.owners - 1;
          this.ExitAndWakeUpAppropriateWaiters();
        }

        internal void ReleaseWriterLock()
        {
          this.EnterMyLock();
          this.owners = this.owners + 1;
          this.ExitAndWakeUpAppropriateWaiters();
        }

        private void LazyCreateEvent(ref EventWaitHandle waitEvent, bool makeAutoResetEvent)
        {
          this.ExitMyLock();
          EventWaitHandle eventWaitHandle = !makeAutoResetEvent ? (EventWaitHandle) new ManualResetEvent(false) : (EventWaitHandle) new AutoResetEvent(false);
          this.EnterMyLock();
          if (waitEvent != null)
            return;
          waitEvent = eventWaitHandle;
        }

        private void WaitOnEvent(EventWaitHandle waitEvent, ref uint numWaiters, int millisecondsTimeout)
        {
          waitEvent.Reset();
          ++numWaiters;
          bool flag = false;
          this.ExitMyLock();
          try
          {
            if (!waitEvent.WaitOne(millisecondsTimeout, false))
              throw new WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.ReaderWriterLockTimedOutException();
            flag = true;
          }
          finally
          {
            this.EnterMyLock();
            --numWaiters;
            if (!flag)
              this.ExitMyLock();
          }
        }

        private void ExitAndWakeUpAppropriateWaiters()
        {
          if (this.owners == 0 && this.numWriteWaiters > 0U)
          {
            this.ExitMyLock();
            this.writeEvent.Set();
          }
          else if (this.owners >= 0 && (int) this.numReadWaiters != 0)
          {
            this.ExitMyLock();
            this.readEvent.Set();
          }
          else
            this.ExitMyLock();
        }

        private void EnterMyLock()
        {
          if (Interlocked.CompareExchange(ref this.myLock, 1, 0) == 0)
            return;
          this.EnterMyLockSpin();
        }

        private void EnterMyLockSpin()
        {
          int num = 0;
          while (true)
          {
            if (num < 3 && Environment.ProcessorCount > 1)
              Thread.SpinWait(20);
            else
              Thread.Sleep(0);
            if (Interlocked.CompareExchange(ref this.myLock, 1, 0) != 0)
              ++num;
            else
              break;
          }
        }

        private void ExitMyLock()
        {
          this.myLock = 0;
        }
      }
    }
  }
}
