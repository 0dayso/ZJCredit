// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComEventsHelper
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.Remoting;
using System.Security;

namespace System.Runtime.InteropServices
{
  /// <summary>提供允许将处理事件的 .NET Framework 委托添加到 COM 对象和从 COM 对象中删除这些委托的方法。</summary>
  [__DynamicallyInvokable]
  public static class ComEventsHelper
  {
    /// <summary>将委托添加到源自 COM 对象的事件的调用列表。</summary>
    /// <param name="rcw">触发事件的 COM 对象，调用方希望响应这些事件。</param>
    /// <param name="iid">COM 对象用来触发事件的源接口的标识符。</param>
    /// <param name="dispid">源接口上的方法的调度标识符。</param>
    /// <param name="d">要在激发 COM 事件时调用的委托。</param>
    [SecurityCritical]
    [__DynamicallyInvokable]
    public static void Combine(object rcw, Guid iid, int dispid, Delegate d)
    {
      rcw = ComEventsHelper.UnwrapIfTransparentProxy(rcw);
      lock (rcw)
      {
        ComEventsInfo local_2 = ComEventsInfo.FromObject(rcw);
        ComEventsSink local_3 = local_2.FindSink(ref iid) ?? local_2.AddSink(ref iid);
        (local_3.FindMethod(dispid) ?? local_3.AddMethod(dispid)).AddDelegate(d);
      }
    }

    /// <summary>从源自 COM 对象的事件的调用列表中移除委托。</summary>
    /// <returns>已从调用列表中移除的委托。</returns>
    /// <param name="rcw">委托附加到的 COM 对象。</param>
    /// <param name="iid">COM 对象用来触发事件的源接口的标识符。</param>
    /// <param name="dispid">源接口上的方法的调度标识符。</param>
    /// <param name="d">要从调用列表中移除的委托。</param>
    [SecurityCritical]
    [__DynamicallyInvokable]
    public static Delegate Remove(object rcw, Guid iid, int dispid, Delegate d)
    {
      rcw = ComEventsHelper.UnwrapIfTransparentProxy(rcw);
      lock (rcw)
      {
        ComEventsInfo local_2 = ComEventsInfo.Find(rcw);
        if (local_2 == null)
          return (Delegate) null;
        ComEventsSink local_3 = local_2.FindSink(ref iid);
        if (local_3 == null)
          return (Delegate) null;
        ComEventsMethod local_4 = local_3.FindMethod(dispid);
        if (local_4 == null)
          return (Delegate) null;
        local_4.RemoveDelegate(d);
        if (local_4.Empty)
          local_4 = local_3.RemoveMethod(local_4);
        if (local_4 == null)
          local_3 = local_2.RemoveSink(local_3);
        if (local_3 == null)
        {
          Marshal.SetComObjectData(rcw, (object) typeof (ComEventsInfo), (object) null);
          GC.SuppressFinalize((object) local_2);
        }
        return d;
      }
    }

    [SecurityCritical]
    internal static object UnwrapIfTransparentProxy(object rcw)
    {
      if (RemotingServices.IsTransparentProxy(rcw))
      {
        IntPtr iunknownForObject = Marshal.GetIUnknownForObject(rcw);
        try
        {
          rcw = Marshal.GetObjectForIUnknown(iunknownForObject);
        }
        finally
        {
          Marshal.Release(iunknownForObject);
        }
      }
      return rcw;
    }
  }
}
