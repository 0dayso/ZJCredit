// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.EventListener
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Threading;

namespace System.Diagnostics.Tracing
{
  /// <summary>提供用于启用和禁用事件源中事件的方法。</summary>
  [__DynamicallyInvokable]
  public abstract class EventListener : IDisposable
  {
    internal volatile EventListener m_Next;
    internal ActivityFilter m_activityFilter;
    internal static EventListener s_Listeners;
    internal static List<WeakReference> s_EventSources;
    private static bool s_CreatingListener;
    private static bool s_EventSourceShutdownRegistered;

    internal static object EventListenersLock
    {
      get
      {
        if (EventListener.s_EventSources == null)
          Interlocked.CompareExchange<List<WeakReference>>(ref EventListener.s_EventSources, new List<WeakReference>(2), (List<WeakReference>) null);
        return (object) EventListener.s_EventSources;
      }
    }

    /// <summary>创建 <see cref="T:System.Diagnostics.Tracing.EventListener" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    protected EventListener()
    {
      lock (EventListener.EventListenersLock)
      {
        if (EventListener.s_CreatingListener)
          throw new InvalidOperationException(Environment.GetResourceString("EventSource_ListenerCreatedInsideCallback"));
        try
        {
          EventListener.s_CreatingListener = true;
          this.m_Next = EventListener.s_Listeners;
          EventListener.s_Listeners = this;
          foreach (WeakReference item_0 in EventListener.s_EventSources.ToArray())
          {
            EventSource local_4 = item_0.Target as EventSource;
            if (local_4 != null)
              local_4.AddListener(this);
          }
        }
        finally
        {
          EventListener.s_CreatingListener = false;
        }
      }
    }

    /// <summary>释放由 <see cref="T:System.Diagnostics.Tracing.EventListener" /> 类的当前实例占用的资源。</summary>
    [__DynamicallyInvokable]
    public virtual void Dispose()
    {
      lock (EventListener.EventListenersLock)
      {
        if (EventListener.s_Listeners == null)
          return;
        if (this == EventListener.s_Listeners)
        {
          EventListener temp_20 = EventListener.s_Listeners;
          EventListener.s_Listeners = this.m_Next;
          EventListener.RemoveReferencesToListenerInEventSources(temp_20);
        }
        else
        {
          EventListener local_2 = EventListener.s_Listeners;
          EventListener local_3;
          while (true)
          {
            local_3 = local_2.m_Next;
            if (local_3 != null)
            {
              if (local_3 != this)
                local_2 = local_3;
              else
                goto label_7;
            }
            else
              break;
          }
          return;
label_7:
          local_2.m_Next = local_3.m_Next;
          EventListener.RemoveReferencesToListenerInEventSources(local_3);
        }
      }
    }

    /// <summary>启用具有指定详细级别或更低详细级别的指定事件源的事件。</summary>
    /// <param name="eventSource">要启用其事件的事件源。</param>
    /// <param name="level">要启用的事件级别。</param>
    [__DynamicallyInvokable]
    public void EnableEvents(EventSource eventSource, EventLevel level)
    {
      this.EnableEvents(eventSource, level, EventKeywords.None);
    }

    /// <summary>启动具有指定详细级别或更低详细级别且与关键字标志匹配的指定事件源的事件。</summary>
    /// <param name="eventSource">要启用其事件的事件源。</param>
    /// <param name="level">要启用的事件级别。</param>
    /// <param name="matchAnyKeyword">启用事件所需的关键字标志。</param>
    [__DynamicallyInvokable]
    public void EnableEvents(EventSource eventSource, EventLevel level, EventKeywords matchAnyKeyword)
    {
      this.EnableEvents(eventSource, level, matchAnyKeyword, (IDictionary<string, string>) null);
    }

    /// <summary>启动具有指定详细级别或更低详细级别且与关键字标志和参数匹配的指定事件源的事件。</summary>
    /// <param name="eventSource">要启用其事件的事件源。</param>
    /// <param name="level">要启用的事件级别。</param>
    /// <param name="matchAnyKeyword">启用事件所需的关键字标志。</param>
    /// <param name="arguments">需匹配以启用事件的参数。</param>
    [__DynamicallyInvokable]
    public void EnableEvents(EventSource eventSource, EventLevel level, EventKeywords matchAnyKeyword, IDictionary<string, string> arguments)
    {
      if (eventSource == null)
        throw new ArgumentNullException("eventSource");
      eventSource.SendCommand(this, 0, 0, EventCommand.Update, true, level, matchAnyKeyword, arguments);
    }

    /// <summary>禁用指定事件源的所有事件。</summary>
    /// <param name="eventSource">要禁用其事件的事件源。</param>
    [__DynamicallyInvokable]
    public void DisableEvents(EventSource eventSource)
    {
      if (eventSource == null)
        throw new ArgumentNullException("eventSource");
      eventSource.SendCommand(this, 0, 0, EventCommand.Update, false, EventLevel.LogAlways, EventKeywords.None, (IDictionary<string, string>) null);
    }

    /// <summary>当创建该事件侦听器且将新事件源附加到侦听器时，对所有现有事件源执行了调用。</summary>
    /// <param name="eventSource">事件源。</param>
    [__DynamicallyInvokable]
    protected internal virtual void OnEventSourceCreated(EventSource eventSource)
    {
    }

    /// <summary>每次事件源写入事件时都执行调用，其中事件侦听器为事件源启用了事件。</summary>
    /// <param name="eventData">描述该事件的事件参数。</param>
    [__DynamicallyInvokable]
    protected internal abstract void OnEventWritten(EventWrittenEventArgs eventData);

    /// <summary>获取表示指定事件源的较小非负数。</summary>
    /// <returns>表示指定的事件源的较小非负数。</returns>
    /// <param name="eventSource">要查找其索引的事件源。</param>
    [__DynamicallyInvokable]
    protected static int EventSourceIndex(EventSource eventSource)
    {
      return eventSource.m_id;
    }

    internal static void AddEventSource(EventSource newEventSource)
    {
      lock (EventListener.EventListenersLock)
      {
        if (EventListener.s_EventSources == null)
          EventListener.s_EventSources = new List<WeakReference>(2);
        if (!EventListener.s_EventSourceShutdownRegistered)
        {
          EventListener.s_EventSourceShutdownRegistered = true;
          AppDomain.CurrentDomain.ProcessExit += new EventHandler(EventListener.DisposeOnShutdown);
          AppDomain.CurrentDomain.DomainUnload += new EventHandler(EventListener.DisposeOnShutdown);
        }
        int local_2 = -1;
        if (EventListener.s_EventSources.Count % 64 == 63)
        {
          int local_3 = EventListener.s_EventSources.Count;
          while (0 < local_3)
          {
            --local_3;
            WeakReference local_4 = EventListener.s_EventSources[local_3];
            if (!local_4.IsAlive)
            {
              local_2 = local_3;
              local_4.Target = (object) newEventSource;
              break;
            }
          }
        }
        if (local_2 < 0)
        {
          local_2 = EventListener.s_EventSources.Count;
          EventListener.s_EventSources.Add(new WeakReference((object) newEventSource));
        }
        newEventSource.m_id = local_2;
        for (EventListener local_5 = EventListener.s_Listeners; local_5 != null; local_5 = local_5.m_Next)
          newEventSource.AddListener(local_5);
      }
    }

    private static void DisposeOnShutdown(object sender, EventArgs e)
    {
      foreach (WeakReference sEventSource in EventListener.s_EventSources)
      {
        EventSource eventSource = sEventSource.Target as EventSource;
        if (eventSource != null)
          eventSource.Dispose();
      }
    }

    private static void RemoveReferencesToListenerInEventSources(EventListener listenerToRemove)
    {
      using (List<WeakReference>.Enumerator enumerator = EventListener.s_EventSources.GetEnumerator())
      {
label_10:
        while (enumerator.MoveNext())
        {
          EventSource eventSource1 = enumerator.Current.Target as EventSource;
          if (eventSource1 != null)
          {
            if (eventSource1.m_Dispatchers.m_Listener == listenerToRemove)
            {
              EventSource eventSource2 = eventSource1;
              EventDispatcher eventDispatcher = eventSource2.m_Dispatchers.m_Next;
              eventSource2.m_Dispatchers = eventDispatcher;
            }
            else
            {
              EventDispatcher eventDispatcher1 = eventSource1.m_Dispatchers;
              EventDispatcher eventDispatcher2;
              while (true)
              {
                eventDispatcher2 = eventDispatcher1.m_Next;
                if (eventDispatcher2 != null)
                {
                  if (eventDispatcher2.m_Listener != listenerToRemove)
                    eventDispatcher1 = eventDispatcher2;
                  else
                    break;
                }
                else
                  goto label_10;
              }
              eventDispatcher1.m_Next = eventDispatcher2.m_Next;
            }
          }
        }
      }
    }

    [Conditional("DEBUG")]
    internal static void Validate()
    {
      lock (EventListener.EventListenersLock)
      {
        Dictionary<EventListener, bool> local_2 = new Dictionary<EventListener, bool>();
        for (EventListener local_3 = EventListener.s_Listeners; local_3 != null; local_3 = local_3.m_Next)
          local_2.Add(local_3, true);
        int local_4 = -1;
        foreach (WeakReference item_0 in EventListener.s_EventSources)
        {
          ++local_4;
          EventSource local_6 = item_0.Target as EventSource;
          if (local_6 != null)
          {
            EventDispatcher local_7 = local_6.m_Dispatchers;
            while (local_7 != null)
              local_7 = local_7.m_Next;
            using (Dictionary<EventListener, bool>.KeyCollection.Enumerator resource_0 = local_2.Keys.GetEnumerator())
            {
label_15:
              while (resource_0.MoveNext())
              {
                EventListener local_9 = resource_0.Current;
                EventDispatcher local_7_1 = local_6.m_Dispatchers;
                while (true)
                {
                  if (local_7_1.m_Listener != local_9)
                    local_7_1 = local_7_1.m_Next;
                  else
                    goto label_15;
                }
              }
            }
          }
        }
      }
    }
  }
}
