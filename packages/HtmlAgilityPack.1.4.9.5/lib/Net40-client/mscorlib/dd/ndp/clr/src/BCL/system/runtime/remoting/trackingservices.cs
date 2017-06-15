// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Services.TrackingServices
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;
using System.Threading;

namespace System.Runtime.Remoting.Services
{
  /// <summary>提供一种注册、注销和获取跟踪处理程序列表的方法。</summary>
  [SecurityCritical]
  [ComVisible(true)]
  public class TrackingServices
  {
    private static volatile ITrackingHandler[] _Handlers = new ITrackingHandler[0];
    private static volatile int _Size = 0;
    private static object s_TrackingServicesSyncObject = (object) null;

    private static object TrackingServicesSyncObject
    {
      get
      {
        if (TrackingServices.s_TrackingServicesSyncObject == null)
        {
          object obj = new object();
          Interlocked.CompareExchange(ref TrackingServices.s_TrackingServicesSyncObject, obj, (object) null);
        }
        return TrackingServices.s_TrackingServicesSyncObject;
      }
    }

    /// <summary>获取当前 <see cref="T:System.AppDomain" /> 中目前已注册到 <see cref="T:System.Runtime.Remoting.Services.TrackingServices" /> 的跟踪处理程序的数组。</summary>
    /// <returns>当前 <see cref="T:System.AppDomain" /> 中目前已注册到 <see cref="T:System.Runtime.Remoting.Services.TrackingServices" /> 的跟踪处理程序的数组。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public static ITrackingHandler[] RegisteredHandlers
    {
      [SecurityCritical] get
      {
        lock (TrackingServices.TrackingServicesSyncObject)
        {
          if (TrackingServices._Size == 0)
            return new ITrackingHandler[0];
          ITrackingHandler[] local_3 = new ITrackingHandler[TrackingServices._Size];
          for (int local_4 = 0; local_4 < TrackingServices._Size; ++local_4)
            local_3[local_4] = TrackingServices._Handlers[local_4];
          return local_3;
        }
      }
    }

    /// <summary>将新的跟踪处理程序注册到 <see cref="T:System.Runtime.Remoting.Services.TrackingServices" />。</summary>
    /// <param name="handler">要注册的跟踪处理程序。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="handler" /> 为 null。</exception>
    /// <exception cref="T:System.Runtime.Remoting.RemotingException">
    /// <paramref name="handler" /> 参数中所指示的处理程序已经注册到 <see cref="T:System.Runtime.Remoting.Services.TrackingServices" />。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public static void RegisterTrackingHandler(ITrackingHandler handler)
    {
      if (handler == null)
        throw new ArgumentNullException("handler");
      lock (TrackingServices.TrackingServicesSyncObject)
      {
        if (-1 == TrackingServices.Match(handler))
        {
          if (TrackingServices._Handlers == null || TrackingServices._Size == TrackingServices._Handlers.Length)
          {
            ITrackingHandler[] local_2 = new ITrackingHandler[TrackingServices._Size * 2 + 4];
            if (TrackingServices._Handlers != null)
              Array.Copy((Array) TrackingServices._Handlers, (Array) local_2, TrackingServices._Size);
            TrackingServices._Handlers = local_2;
          }
          Volatile.Write<ITrackingHandler>(ref TrackingServices._Handlers[TrackingServices._Size++], handler);
        }
        else
          throw new RemotingException(Environment.GetResourceString("Remoting_TrackingHandlerAlreadyRegistered", (object) "handler"));
      }
    }

    /// <summary>从 <see cref="T:System.Runtime.Remoting.Services.TrackingServices" /> 注销指定的跟踪处理程序。</summary>
    /// <param name="handler">要注销的处理程序。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="handler" /> 为 null。</exception>
    /// <exception cref="T:System.Runtime.Remoting.RemotingException">
    /// <paramref name="handler" /> 参数中所指示的处理程序未注册到 <see cref="T:System.Runtime.Remoting.Services.TrackingServices" />。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public static void UnregisterTrackingHandler(ITrackingHandler handler)
    {
      if (handler == null)
        throw new ArgumentNullException("handler");
      lock (TrackingServices.TrackingServicesSyncObject)
      {
        int local_2 = TrackingServices.Match(handler);
        if (-1 == local_2)
          throw new RemotingException(Environment.GetResourceString("Remoting_HandlerNotRegistered", (object) handler));
        Array.Copy((Array) TrackingServices._Handlers, local_2 + 1, (Array) TrackingServices._Handlers, local_2, TrackingServices._Size - local_2 - 1);
        --TrackingServices._Size;
      }
    }

    [SecurityCritical]
    internal static void MarshaledObject(object obj, ObjRef or)
    {
      try
      {
        ITrackingHandler[] trackingHandlerArray = TrackingServices._Handlers;
        for (int index = 0; index < TrackingServices._Size; ++index)
          Volatile.Read<ITrackingHandler>(ref trackingHandlerArray[index]).MarshaledObject(obj, or);
      }
      catch
      {
      }
    }

    [SecurityCritical]
    internal static void UnmarshaledObject(object obj, ObjRef or)
    {
      try
      {
        ITrackingHandler[] trackingHandlerArray = TrackingServices._Handlers;
        for (int index = 0; index < TrackingServices._Size; ++index)
          Volatile.Read<ITrackingHandler>(ref trackingHandlerArray[index]).UnmarshaledObject(obj, or);
      }
      catch
      {
      }
    }

    [SecurityCritical]
    internal static void DisconnectedObject(object obj)
    {
      try
      {
        ITrackingHandler[] trackingHandlerArray = TrackingServices._Handlers;
        for (int index = 0; index < TrackingServices._Size; ++index)
          Volatile.Read<ITrackingHandler>(ref trackingHandlerArray[index]).DisconnectedObject(obj);
      }
      catch
      {
      }
    }

    private static int Match(ITrackingHandler handler)
    {
      int num = -1;
      for (int index = 0; index < TrackingServices._Size; ++index)
      {
        if (TrackingServices._Handlers[index] == handler)
        {
          num = index;
          break;
        }
      }
      return num;
    }
  }
}
