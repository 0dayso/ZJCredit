// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.EventSource
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Reflection;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Diagnostics.Tracing
{
  /// <summary>提供为 Windows 事件跟踪 (ETW) 创建事件的功能。</summary>
  [__DynamicallyInvokable]
  public class EventSource : IDisposable
  {
    [ThreadStatic]
    private static byte m_EventSourceExceptionRecurenceCount = 0;
    private static readonly byte[] namespaceBytes = new byte[16]{ (byte) 72, (byte) 44, (byte) 45, (byte) 178, (byte) 195, (byte) 144, (byte) 71, (byte) 200, (byte) 135, (byte) 248, (byte) 26, (byte) 21, (byte) 191, (byte) 193, (byte) 48, (byte) 251 };
    private string m_name;
    internal int m_id;
    private Guid m_guid;
    internal volatile EventSource.EventMetadata[] m_eventData;
    private volatile byte[] m_rawManifest;
    private EventSourceSettings m_config;
    private bool m_eventSourceEnabled;
    internal EventLevel m_level;
    internal EventKeywords m_matchAnyKeyword;
    internal volatile EventDispatcher m_Dispatchers;
    private volatile EventSource.OverideEventProvider m_provider;
    private bool m_completelyInited;
    private Exception m_constructionException;
    private byte m_outOfBandMessageCount;
    private EventCommandEventArgs m_deferredCommands;
    private string[] m_traits;
    internal static uint s_currentPid;
    internal volatile ulong[] m_channelData;
    private SessionMask m_curLiveSessions;
    private EtwSession[] m_etwSessionIdMap;
    private List<EtwSession> m_legacySessions;
    internal long m_keywordTriggers;
    internal SessionMask m_activityFilteringForETWEnabled;
    internal static Action<Guid> s_activityDying;
    private ActivityTracker m_activityTracker;
    internal const string s_ActivityStartSuffix = "Start";
    internal const string s_ActivityStopSuffix = "Stop";
    private byte[] providerMetadata;

    /// <summary>从事件源中派生出来的类的友好名称。</summary>
    /// <returns>派生类的友好名称。默认值为类的简单名称。</returns>
    [__DynamicallyInvokable]
    public string Name
    {
      [__DynamicallyInvokable] get
      {
        return this.m_name;
      }
    }

    /// <summary>此事件源的唯一标识符。</summary>
    /// <returns>此事件源的唯一标识符。</returns>
    [__DynamicallyInvokable]
    public Guid Guid
    {
      [__DynamicallyInvokable] get
      {
        return this.m_guid;
      }
    }

    /// <summary>获取应用于此事件源的设置。</summary>
    /// <returns>应用于此事件源的设置。</returns>
    [__DynamicallyInvokable]
    public EventSourceSettings Settings
    {
      [__DynamicallyInvokable] get
      {
        return this.m_config;
      }
    }

    /// <summary>[在 .NET Framework 4.5.1 和更高版本中受支持] 获取当前线程的活动 ID。</summary>
    /// <returns>当前线程的活动 ID。</returns>
    [__DynamicallyInvokable]
    public static Guid CurrentThreadActivityId
    {
      [SecurityCritical, __DynamicallyInvokable] get
      {
        Guid ActivityId = new Guid();
        UnsafeNativeMethods.ManifestEtw.EventActivityIdControl(UnsafeNativeMethods.ManifestEtw.ActivityControl.EVENT_ACTIVITY_CTRL_GET_ID, out ActivityId);
        return ActivityId;
      }
    }

    internal static Guid InternalCurrentThreadActivityId
    {
      [SecurityCritical] get
      {
        Guid guid = EventSource.CurrentThreadActivityId;
        if (guid == Guid.Empty)
          guid = EventSource.FallbackActivityId;
        return guid;
      }
    }

    internal static Guid FallbackActivityId
    {
      [SecurityCritical] get
      {
        return new Guid((uint) AppDomain.GetCurrentThreadId(), (ushort) EventSource.s_currentPid, (ushort) (EventSource.s_currentPid >> 16), (byte) 148, (byte) 27, (byte) 135, (byte) 213, (byte) 166, (byte) 92, (byte) 54, (byte) 100);
      }
    }

    /// <summary>[在 .NET Framework 4.5.1 和更高版本中受支持] 获取在事件源的构造过程中引发的任何异常。</summary>
    /// <returns>在事件源的构造过程中引发的异常；如果没有引发异常，则为 null。</returns>
    [__DynamicallyInvokable]
    public Exception ConstructionException
    {
      [__DynamicallyInvokable] get
      {
        return this.m_constructionException;
      }
    }

    private bool IsDisposed
    {
      get
      {
        if (this.m_provider != null)
          return this.m_provider.m_disposed;
        return true;
      }
    }

    private bool ThrowOnEventWriteErrors
    {
      get
      {
        return (uint) (this.m_config & EventSourceSettings.ThrowOnEventWriteErrors) > 0U;
      }
      set
      {
        if (value)
          this.m_config = this.m_config | EventSourceSettings.ThrowOnEventWriteErrors;
        else
          this.m_config = this.m_config & ~EventSourceSettings.ThrowOnEventWriteErrors;
      }
    }

    private bool SelfDescribingEvents
    {
      get
      {
        return (uint) (this.m_config & EventSourceSettings.EtwSelfDescribingEventFormat) > 0U;
      }
      set
      {
        if (!value)
        {
          this.m_config = this.m_config | EventSourceSettings.EtwManifestEventFormat;
          this.m_config = this.m_config & ~EventSourceSettings.EtwSelfDescribingEventFormat;
        }
        else
        {
          this.m_config = this.m_config | EventSourceSettings.EtwSelfDescribingEventFormat;
          this.m_config = this.m_config & ~EventSourceSettings.EtwManifestEventFormat;
        }
      }
    }

    /// <summary>创建 <see cref="T:System.Diagnostics.Tracing.EventSource" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    protected EventSource()
      : this(EventSourceSettings.EtwManifestEventFormat)
    {
    }

    /// <summary>创建 <see cref="T:System.Diagnostics.Tracing.EventSource" /> 类的新实例，并指定在 Windows 基础代码发生错误时是否引发异常。</summary>
    /// <param name="throwOnEventWriteErrors">若在 Windows 基础代码发生错误时要引发异常，则为 true；否则为 false。</param>
    [__DynamicallyInvokable]
    protected EventSource(bool throwOnEventWriteErrors)
      : this((EventSourceSettings) (4 | (throwOnEventWriteErrors ? 1 : 0)))
    {
    }

    /// <summary>使用指定的配置设置创建 <see cref="T:System.Diagnostics.Tracing.EventSource" /> 类的新实例。</summary>
    /// <param name="settings">一个枚举值的按位组合，这些枚举值指定要应用于事件源的配置设置。</param>
    [__DynamicallyInvokable]
    protected EventSource(EventSourceSettings settings)
      : this(settings, (string[]) null)
    {
    }

    /// <summary>初始化 <see cref="T:System.Diagnostics.Tracing.EventSource" /> 的新实例，以用于其中包含指定设置和特性的非约定事件。</summary>
    /// <param name="settings">一个枚举值的按位组合，这些枚举值指定要应用于事件源的配置设置。</param>
    /// <param name="traits">指定事件源特性的键值对。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="traits" /> is not specified in key-value pairs.</exception>
    [__DynamicallyInvokable]
    protected EventSource(EventSourceSettings settings, params string[] traits)
    {
      this.m_config = this.ValidateSettings(settings);
      Type type = this.GetType();
      this.Initialize(EventSource.GetGuid(type), EventSource.GetName(type), traits);
    }

    internal EventSource(Guid eventSourceGuid, string eventSourceName)
      : this(eventSourceGuid, eventSourceName, EventSourceSettings.EtwManifestEventFormat, (string[]) null)
    {
    }

    internal EventSource(Guid eventSourceGuid, string eventSourceName, EventSourceSettings settings, string[] traits = null)
    {
      this.m_config = this.ValidateSettings(settings);
      this.Initialize(eventSourceGuid, eventSourceName, traits);
    }

    /// <summary>使用指定的名称创建 <see cref="T:System.Diagnostics.Tracing.EventSource" /> 类的新实例。</summary>
    /// <param name="eventSourceName">要应用于事件源的名称。不得为 null。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="eventSourceName" /> is null.</exception>
    [__DynamicallyInvokable]
    public EventSource(string eventSourceName)
      : this(eventSourceName, EventSourceSettings.EtwSelfDescribingEventFormat)
    {
    }

    /// <summary>使用指定的名称和设置创建 <see cref="T:System.Diagnostics.Tracing.EventSource" /> 类的新实例。</summary>
    /// <param name="eventSourceName">要应用于事件源的名称。不得为 null。</param>
    /// <param name="config">一个枚举值的按位组合，这些枚举值指定要应用于事件源的配置设置。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="eventSourceName" /> is null.</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="eventSourceName" /> is null.</exception>
    [__DynamicallyInvokable]
    public EventSource(string eventSourceName, EventSourceSettings config)
      : this(eventSourceName, config, (string[]) null)
    {
    }

    /// <summary>使用指定的配置设置创建 <see cref="T:System.Diagnostics.Tracing.EventSource" /> 类的新实例。</summary>
    /// <param name="eventSourceName">要应用于事件源的名称。不得为 null。</param>
    /// <param name="config">一个枚举值的按位组合，这些枚举值指定要应用于事件源的配置设置。</param>
    /// <param name="traits">指定事件源特性的键值对。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="eventSourceName" /> is null.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="traits" /> is not specified in key-value pairs.</exception>
    [__DynamicallyInvokable]
    public EventSource(string eventSourceName, EventSourceSettings config, params string[] traits)
      : this(eventSourceName == null ? new Guid() : EventSource.GenerateGuidFromName(eventSourceName.ToUpperInvariant()), eventSourceName, config, traits)
    {
      if (eventSourceName == null)
        throw new ArgumentNullException("eventSourceName");
    }

    /// <summary>允许 <see cref="T:System.Diagnostics.Tracing.EventSource" /> 对象在被垃圾回收之前尝试释放资源并执行其他清理操作。</summary>
    [__DynamicallyInvokable]
    ~EventSource()
    {
      this.Dispose(false);
    }

    /// <summary>确定是否已启用当前事件源。</summary>
    /// <returns>如果启用了当前事件源，则为 true；否则为 false。</returns>
    [__DynamicallyInvokable]
    public bool IsEnabled()
    {
      return this.m_eventSourceEnabled;
    }

    /// <summary>确定是否已启用具有指定级别和关键字的当前事件源。</summary>
    /// <returns>如果启用了事件源，则为 true；否则为 false。</returns>
    /// <param name="level">事件源级别。</param>
    /// <param name="keywords">事件源的关键字。</param>
    [__DynamicallyInvokable]
    public bool IsEnabled(EventLevel level, EventKeywords keywords)
    {
      return this.IsEnabled(level, keywords, EventChannel.None);
    }

    /// <summary>确定是否对带有指定级别、关键字和通道的事件启用了当前事件源。</summary>
    /// <returns>如果对指定的事件级别、关键字和通道启用了事件源，则为 true；否则为 false。通过此方法的结果仅可大概了解特定的事件是否处于活动状态。使用它可避免在禁用了记录的情况下因记录造成昂贵的计算费用。事件源可能具有确定其活动的其他筛选。</returns>
    /// <param name="level">要检查的事件级别。当事件源的级别大于或等于 <paramref name="level" /> 时，将其视为已启用。</param>
    /// <param name="keywords">要检查的事件关键字。</param>
    /// <param name="channel">要检查的事件通道。</param>
    [__DynamicallyInvokable]
    public bool IsEnabled(EventLevel level, EventKeywords keywords, EventChannel channel)
    {
      return this.m_eventSourceEnabled && this.IsEnabledCommon(this.m_eventSourceEnabled, this.m_level, this.m_matchAnyKeyword, level, keywords, channel);
    }

    /// <summary>获取事件源的实现的唯一标识符。</summary>
    /// <returns>此事件源类型的唯一标识符。</returns>
    /// <param name="eventSourceType">事件源的类型。</param>
    [__DynamicallyInvokable]
    public static Guid GetGuid(Type eventSourceType)
    {
      if (eventSourceType == (Type) null)
        throw new ArgumentNullException("eventSourceType");
      EventSourceAttribute eventSourceAttribute = (EventSourceAttribute) EventSource.GetCustomAttributeHelper((MemberInfo) eventSourceType, typeof (EventSourceAttribute), EventManifestOptions.None);
      string name = eventSourceType.Name;
      if (eventSourceAttribute != null)
      {
        if (eventSourceAttribute.Guid != null)
        {
          Guid result = Guid.Empty;
          if (Guid.TryParse(eventSourceAttribute.Guid, out result))
            return result;
        }
        if (eventSourceAttribute.Name != null)
          name = eventSourceAttribute.Name;
      }
      if (name == null)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidTypeName"), "eventSourceType");
      return EventSource.GenerateGuidFromName(name.ToUpperInvariant());
    }

    /// <summary>获取事件源的好友名称。</summary>
    /// <returns>事件源的友好名称。默认值为类的简单名称。</returns>
    /// <param name="eventSourceType">事件源的类型。</param>
    [__DynamicallyInvokable]
    public static string GetName(Type eventSourceType)
    {
      return EventSource.GetName(eventSourceType, EventManifestOptions.None);
    }

    /// <summary>返回与当前事件源关联的 XML 清单的字符串。</summary>
    /// <returns>XML 数据字符串。</returns>
    /// <param name="eventSourceType">事件源的类型。</param>
    /// <param name="assemblyPathToIncludeInManifest">要包含在清单的 provider 元素中的程序集文件 (.dll) 的路径。</param>
    [__DynamicallyInvokable]
    public static string GenerateManifest(Type eventSourceType, string assemblyPathToIncludeInManifest)
    {
      return EventSource.GenerateManifest(eventSourceType, assemblyPathToIncludeInManifest, EventManifestOptions.None);
    }

    /// <summary>返回与当前事件源关联的 XML 清单的字符串。</summary>
    /// <returns>XML 数据字符串或 null（请参见“备注”）。</returns>
    /// <param name="eventSourceType">事件源的类型。</param>
    /// <param name="assemblyPathToIncludeInManifest">要包含在清单的 provider 元素中的程序集文件 (.dll) 的路径。</param>
    /// <param name="flags">一个枚举值的按位组合，这些枚举值指定如何生成清单。</param>
    [__DynamicallyInvokable]
    public static string GenerateManifest(Type eventSourceType, string assemblyPathToIncludeInManifest, EventManifestOptions flags)
    {
      if (eventSourceType == (Type) null)
        throw new ArgumentNullException("eventSourceType");
      byte[] manifestAndDescriptors = EventSource.CreateManifestAndDescriptors(eventSourceType, assemblyPathToIncludeInManifest, (EventSource) null, flags);
      if (manifestAndDescriptors != null)
        return Encoding.UTF8.GetString(manifestAndDescriptors, 0, manifestAndDescriptors.Length);
      return (string) null;
    }

    /// <summary>获取应用程序域的所有事件源的快照。</summary>
    /// <returns>应用程序域中所有事件源的枚举。</returns>
    [__DynamicallyInvokable]
    public static IEnumerable<EventSource> GetSources()
    {
      List<EventSource> eventSourceList = new List<EventSource>();
      lock (EventListener.EventListenersLock)
      {
        foreach (WeakReference item_0 in EventListener.s_EventSources)
        {
          EventSource local_4 = item_0.Target as EventSource;
          if (local_4 != null && !local_4.IsDisposed)
            eventSourceList.Add(local_4);
        }
      }
      return (IEnumerable<EventSource>) eventSourceList;
    }

    /// <summary>发送命令到指定的事件源。</summary>
    /// <param name="eventSource">对其发送命令的事件源。</param>
    /// <param name="command">要发送的事件命令。</param>
    /// <param name="commandArguments">事件命令的参数。</param>
    [__DynamicallyInvokable]
    public static void SendCommand(EventSource eventSource, EventCommand command, IDictionary<string, string> commandArguments)
    {
      if (eventSource == null)
        throw new ArgumentNullException("eventSource");
      if (command <= EventCommand.Update && command != EventCommand.SendManifest)
        throw new ArgumentException(Environment.GetResourceString("EventSource_InvalidCommand"), "command");
      eventSource.SendCommand((EventListener) null, 0, 0, command, true, EventLevel.LogAlways, EventKeywords.None, commandArguments);
    }

    /// <summary>[在 .NET Framework 4.5.1 和更高版本中受支持] 在当前线程上设置活动 ID。</summary>
    /// <param name="activityId">当前线程的新活动 ID；或者为 <see cref="F:System.Guid.Empty" /> 以指示当前线程上的工作与任何活动都不关联。</param>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static void SetCurrentThreadActivityId(Guid activityId)
    {
      Guid guid = activityId;
      if (UnsafeNativeMethods.ManifestEtw.EventActivityIdControl(UnsafeNativeMethods.ManifestEtw.ActivityControl.EVENT_ACTIVITY_CTRL_GET_SET_ID, out activityId) == 0)
      {
        Action<Guid> action = EventSource.s_activityDying;
        if (action != null && guid != activityId)
        {
          if (activityId == Guid.Empty)
            activityId = EventSource.FallbackActivityId;
          action(activityId);
        }
      }
      if (TplEtwProvider.Log == null)
        return;
      TplEtwProvider.Log.SetActivityId(activityId);
    }

    /// <summary>[在 .NET Framework 4.5.1 和更高版本中受支持] 在当前线程上设置活动 ID 并返回以前的活动 ID。</summary>
    /// <param name="activityId">当前线程的新活动 ID；或者为 <see cref="F:System.Guid.Empty" /> 以指示当前线程上的工作与任何活动都不关联。</param>
    /// <param name="oldActivityThatWillContinue">当此方法返回时，将包含当前线程上以前的活动 ID。</param>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static void SetCurrentThreadActivityId(Guid activityId, out Guid oldActivityThatWillContinue)
    {
      oldActivityThatWillContinue = activityId;
      UnsafeNativeMethods.ManifestEtw.EventActivityIdControl(UnsafeNativeMethods.ManifestEtw.ActivityControl.EVENT_ACTIVITY_CTRL_GET_SET_ID, out oldActivityThatWillContinue);
      if (TplEtwProvider.Log == null)
        return;
      TplEtwProvider.Log.SetActivityId(activityId);
    }

    /// <summary>获取与指定键关联的特性值。</summary>
    /// <returns>与指定的键相关联的特性值。如果未找到该键，则返回 null。</returns>
    /// <param name="key">要获取的特性的键。</param>
    [__DynamicallyInvokable]
    public string GetTrait(string key)
    {
      if (this.m_traits != null)
      {
        int index = 0;
        while (index < this.m_traits.Length - 1)
        {
          if (this.m_traits[index] == key)
            return this.m_traits[index + 1];
          index += 2;
        }
      }
      return (string) null;
    }

    /// <summary>获得当前事件源实例的字符串表示形式。</summary>
    /// <returns>标识当前事件源的名称和唯一标识符。</returns>
    [__DynamicallyInvokable]
    public override string ToString()
    {
      return Environment.GetResourceString("EventSource_ToString", (object) this.Name, (object) this.Guid);
    }

    /// <summary>当该控制器更新当前事件源时调用。</summary>
    /// <param name="command">事件的参数。</param>
    [__DynamicallyInvokable]
    protected virtual void OnEventCommand(EventCommandEventArgs command)
    {
    }

    /// <summary>通过使用提供的事件标识符写入事件。</summary>
    /// <param name="eventId">事件标识符。该值应介于 0 到 65535 之间。</param>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    protected unsafe void WriteEvent(int eventId)
    {
      this.WriteEventCore(eventId, 0, (EventSource.EventData*) null);
    }

    /// <summary>通过使用提供的事件标识符和 32 位整数参数写入事件。</summary>
    /// <param name="eventId">事件标识符。该值应介于 0 到 65535 之间。</param>
    /// <param name="arg1">一个整数参数。</param>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    protected unsafe void WriteEvent(int eventId, int arg1)
    {
      if (!this.m_eventSourceEnabled)
        return;
      EventSource.EventData* data = stackalloc EventSource.EventData[1];
      data->DataPointer = (IntPtr) ((void*) &arg1);
      data->Size = 4;
      this.WriteEventCore(eventId, 1, data);
    }

    /// <summary>通过使用提供的事件标识符和 32 位整数参数写入事件。</summary>
    /// <param name="eventId">事件标识符。该值应介于 0 到 65535 之间。</param>
    /// <param name="arg1">一个整数参数。</param>
    /// <param name="arg2">一个整数参数。</param>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    protected unsafe void WriteEvent(int eventId, int arg1, int arg2)
    {
      if (!this.m_eventSourceEnabled)
        return;
      EventSource.EventData* data = stackalloc EventSource.EventData[2];
      data->DataPointer = (IntPtr) ((void*) &arg1);
      data->Size = 4;
      data[1].DataPointer = (IntPtr) ((void*) &arg2);
      data[1].Size = 4;
      this.WriteEventCore(eventId, 2, data);
    }

    /// <summary>通过使用提供的事件标识符和 32 位整数参数写入事件。</summary>
    /// <param name="eventId">事件标识符。该值应介于 0 到 65535 之间。</param>
    /// <param name="arg1">一个整数参数。</param>
    /// <param name="arg2">一个整数参数。</param>
    /// <param name="arg3">一个整数参数。</param>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    protected unsafe void WriteEvent(int eventId, int arg1, int arg2, int arg3)
    {
      if (!this.m_eventSourceEnabled)
        return;
      EventSource.EventData* data = stackalloc EventSource.EventData[3];
      data->DataPointer = (IntPtr) ((void*) &arg1);
      data->Size = 4;
      data[1].DataPointer = (IntPtr) ((void*) &arg2);
      data[1].Size = 4;
      data[2].DataPointer = (IntPtr) ((void*) &arg3);
      data[2].Size = 4;
      this.WriteEventCore(eventId, 3, data);
    }

    /// <summary>通过使用提供的事件标识符和 64 位整数参数写入事件。</summary>
    /// <param name="eventId">事件标识符。该值应介于 0 到 65535 之间。</param>
    /// <param name="arg1">64 位整数参数。</param>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    protected unsafe void WriteEvent(int eventId, long arg1)
    {
      if (!this.m_eventSourceEnabled)
        return;
      EventSource.EventData* data = stackalloc EventSource.EventData[1];
      data->DataPointer = (IntPtr) ((void*) &arg1);
      data->Size = 8;
      this.WriteEventCore(eventId, 1, data);
    }

    /// <summary>通过使用提供的事件标识符和 64 位参数写入事件。</summary>
    /// <param name="eventId">事件标识符。该值应介于 0 到 65535 之间。</param>
    /// <param name="arg1">64 位整数参数。</param>
    /// <param name="arg2">64 位整数参数。</param>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    protected unsafe void WriteEvent(int eventId, long arg1, long arg2)
    {
      if (!this.m_eventSourceEnabled)
        return;
      EventSource.EventData* data = stackalloc EventSource.EventData[2];
      data->DataPointer = (IntPtr) ((void*) &arg1);
      data->Size = 8;
      data[1].DataPointer = (IntPtr) ((void*) &arg2);
      data[1].Size = 8;
      this.WriteEventCore(eventId, 2, data);
    }

    /// <summary>通过使用提供的事件标识符和 64 位参数写入事件。</summary>
    /// <param name="eventId">事件标识符。该值应介于 0 到 65535 之间。</param>
    /// <param name="arg1">64 位整数参数。</param>
    /// <param name="arg2">64 位整数参数。</param>
    /// <param name="arg3">64 位整数参数。</param>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    protected unsafe void WriteEvent(int eventId, long arg1, long arg2, long arg3)
    {
      if (!this.m_eventSourceEnabled)
        return;
      EventSource.EventData* data = stackalloc EventSource.EventData[3];
      data->DataPointer = (IntPtr) ((void*) &arg1);
      data->Size = 8;
      data[1].DataPointer = (IntPtr) ((void*) &arg2);
      data[1].Size = 8;
      data[2].DataPointer = (IntPtr) ((void*) &arg3);
      data[2].Size = 8;
      this.WriteEventCore(eventId, 3, data);
    }

    /// <summary>通过使用提供的事件标识符和字符串参数写入事件。</summary>
    /// <param name="eventId">事件标识符。该值应介于 0 到 65535 之间。</param>
    /// <param name="arg1">一个字符串参数。</param>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    protected unsafe void WriteEvent(int eventId, string arg1)
    {
      if (!this.m_eventSourceEnabled)
        return;
      if (arg1 == null)
        arg1 = "";
      string str = arg1;
      char* chPtr = (char*) str;
      if ((IntPtr) chPtr != IntPtr.Zero)
        chPtr += RuntimeHelpers.OffsetToStringData;
      EventSource.EventData* data = stackalloc EventSource.EventData[1];
      data->DataPointer = (IntPtr) ((void*) chPtr);
      data->Size = (arg1.Length + 1) * 2;
      this.WriteEventCore(eventId, 1, data);
      str = (string) null;
    }

    /// <summary>通过使用提供的事件标识符和字符串参数写入事件。</summary>
    /// <param name="eventId">事件标识符。该值应介于 0 到 65535 之间。</param>
    /// <param name="arg1">一个字符串参数。</param>
    /// <param name="arg2">一个字符串参数。</param>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    protected unsafe void WriteEvent(int eventId, string arg1, string arg2)
    {
      if (!this.m_eventSourceEnabled)
        return;
      if (arg1 == null)
        arg1 = "";
      if (arg2 == null)
        arg2 = "";
      string str1 = arg1;
      char* chPtr1 = (char*) str1;
      if ((IntPtr) chPtr1 != IntPtr.Zero)
        chPtr1 += RuntimeHelpers.OffsetToStringData;
      string str2 = arg2;
      char* chPtr2 = (char*) str2;
      if ((IntPtr) chPtr2 != IntPtr.Zero)
        chPtr2 += RuntimeHelpers.OffsetToStringData;
      EventSource.EventData* data = stackalloc EventSource.EventData[2];
      data->DataPointer = (IntPtr) ((void*) chPtr1);
      data->Size = (arg1.Length + 1) * 2;
      data[1].DataPointer = (IntPtr) ((void*) chPtr2);
      data[1].Size = (arg2.Length + 1) * 2;
      this.WriteEventCore(eventId, 2, data);
      str2 = (string) null;
      str1 = (string) null;
    }

    /// <summary>通过使用提供的事件标识符和字符串参数写入事件。</summary>
    /// <param name="eventId">事件标识符。该值应介于 0 到 65535 之间。</param>
    /// <param name="arg1">一个字符串参数。</param>
    /// <param name="arg2">一个字符串参数。</param>
    /// <param name="arg3">一个字符串参数。</param>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    protected unsafe void WriteEvent(int eventId, string arg1, string arg2, string arg3)
    {
      if (!this.m_eventSourceEnabled)
        return;
      if (arg1 == null)
        arg1 = "";
      if (arg2 == null)
        arg2 = "";
      if (arg3 == null)
        arg3 = "";
      string str1 = arg1;
      char* chPtr1 = (char*) str1;
      if ((IntPtr) chPtr1 != IntPtr.Zero)
        chPtr1 += RuntimeHelpers.OffsetToStringData;
      string str2 = arg2;
      char* chPtr2 = (char*) str2;
      if ((IntPtr) chPtr2 != IntPtr.Zero)
        chPtr2 += RuntimeHelpers.OffsetToStringData;
      string str3 = arg3;
      char* chPtr3 = (char*) str3;
      if ((IntPtr) chPtr3 != IntPtr.Zero)
        chPtr3 += RuntimeHelpers.OffsetToStringData;
      EventSource.EventData* data = stackalloc EventSource.EventData[3];
      data->DataPointer = (IntPtr) ((void*) chPtr1);
      data->Size = (arg1.Length + 1) * 2;
      data[1].DataPointer = (IntPtr) ((void*) chPtr2);
      data[1].Size = (arg2.Length + 1) * 2;
      data[2].DataPointer = (IntPtr) ((void*) chPtr3);
      data[2].Size = (arg3.Length + 1) * 2;
      this.WriteEventCore(eventId, 3, data);
      str3 = (string) null;
      str2 = (string) null;
      str1 = (string) null;
    }

    /// <summary>通过使用提供的事件标识符和参数写入事件。</summary>
    /// <param name="eventId">事件标识符。该值应介于 0 到 65535 之间。</param>
    /// <param name="arg1">一个字符串参数。</param>
    /// <param name="arg2">32 位整数参数。</param>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    protected unsafe void WriteEvent(int eventId, string arg1, int arg2)
    {
      if (!this.m_eventSourceEnabled)
        return;
      if (arg1 == null)
        arg1 = "";
      string str = arg1;
      char* chPtr = (char*) str;
      if ((IntPtr) chPtr != IntPtr.Zero)
        chPtr += RuntimeHelpers.OffsetToStringData;
      EventSource.EventData* data = stackalloc EventSource.EventData[2];
      data->DataPointer = (IntPtr) ((void*) chPtr);
      data->Size = (arg1.Length + 1) * 2;
      data[1].DataPointer = (IntPtr) ((void*) &arg2);
      data[1].Size = 4;
      this.WriteEventCore(eventId, 2, data);
      str = (string) null;
    }

    /// <summary>通过使用提供的事件标识符和参数写入事件。</summary>
    /// <param name="eventId">事件标识符。该值应介于 0 到 65535 之间。</param>
    /// <param name="arg1">一个字符串参数。</param>
    /// <param name="arg2">32 位整数参数。</param>
    /// <param name="arg3">32 位整数参数。</param>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    protected unsafe void WriteEvent(int eventId, string arg1, int arg2, int arg3)
    {
      if (!this.m_eventSourceEnabled)
        return;
      if (arg1 == null)
        arg1 = "";
      string str = arg1;
      char* chPtr = (char*) str;
      if ((IntPtr) chPtr != IntPtr.Zero)
        chPtr += RuntimeHelpers.OffsetToStringData;
      EventSource.EventData* data = stackalloc EventSource.EventData[3];
      data->DataPointer = (IntPtr) ((void*) chPtr);
      data->Size = (arg1.Length + 1) * 2;
      data[1].DataPointer = (IntPtr) ((void*) &arg2);
      data[1].Size = 4;
      data[2].DataPointer = (IntPtr) ((void*) &arg3);
      data[2].Size = 4;
      this.WriteEventCore(eventId, 3, data);
      str = (string) null;
    }

    /// <summary>通过使用提供的事件标识符和参数写入事件。</summary>
    /// <param name="eventId">事件标识符。该值应介于 0 到 65535 之间。</param>
    /// <param name="arg1">一个字符串参数。</param>
    /// <param name="arg2">64 位整数参数。</param>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    protected unsafe void WriteEvent(int eventId, string arg1, long arg2)
    {
      if (!this.m_eventSourceEnabled)
        return;
      if (arg1 == null)
        arg1 = "";
      string str = arg1;
      char* chPtr = (char*) str;
      if ((IntPtr) chPtr != IntPtr.Zero)
        chPtr += RuntimeHelpers.OffsetToStringData;
      EventSource.EventData* data = stackalloc EventSource.EventData[2];
      data->DataPointer = (IntPtr) ((void*) chPtr);
      data->Size = (arg1.Length + 1) * 2;
      data[1].DataPointer = (IntPtr) ((void*) &arg2);
      data[1].Size = 8;
      this.WriteEventCore(eventId, 2, data);
      str = (string) null;
    }

    /// <summary>使用提供的事件标识符、64 位整数和字符串参数写入事件。</summary>
    /// <param name="eventId">事件标识符。该值应介于 0 到 65535 之间。</param>
    /// <param name="arg1">64 位整数参数。</param>
    /// <param name="arg2">一个字符串参数。</param>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    protected unsafe void WriteEvent(int eventId, long arg1, string arg2)
    {
      if (!this.m_eventSourceEnabled)
        return;
      if (arg2 == null)
        arg2 = "";
      string str = arg2;
      char* chPtr = (char*) str;
      if ((IntPtr) chPtr != IntPtr.Zero)
        chPtr += RuntimeHelpers.OffsetToStringData;
      EventSource.EventData* data = stackalloc EventSource.EventData[2];
      data->DataPointer = (IntPtr) ((void*) &arg1);
      data->Size = 8;
      data[1].DataPointer = (IntPtr) ((void*) chPtr);
      data[1].Size = (arg2.Length + 1) * 2;
      this.WriteEventCore(eventId, 2, data);
      str = (string) null;
    }

    /// <summary>使用提供的事件标识符、32 位整数和字符串参数写入事件。</summary>
    /// <param name="eventId">事件标识符。该值应介于 0 到 65535 之间。</param>
    /// <param name="arg1">32 位整数参数。</param>
    /// <param name="arg2">一个字符串参数。</param>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    protected unsafe void WriteEvent(int eventId, int arg1, string arg2)
    {
      if (!this.m_eventSourceEnabled)
        return;
      if (arg2 == null)
        arg2 = "";
      string str = arg2;
      char* chPtr = (char*) str;
      if ((IntPtr) chPtr != IntPtr.Zero)
        chPtr += RuntimeHelpers.OffsetToStringData;
      EventSource.EventData* data = stackalloc EventSource.EventData[2];
      data->DataPointer = (IntPtr) ((void*) &arg1);
      data->Size = 4;
      data[1].DataPointer = (IntPtr) ((void*) chPtr);
      data[1].Size = (arg2.Length + 1) * 2;
      this.WriteEventCore(eventId, 2, data);
      str = (string) null;
    }

    /// <summary>通过使用提供的事件标识符和字节数组参数写入事件。</summary>
    /// <param name="eventId">事件标识符。该值应介于 0 到 65535 之间。</param>
    /// <param name="arg1">字节数组参数。</param>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    protected unsafe void WriteEvent(int eventId, byte[] arg1)
    {
      if (!this.m_eventSourceEnabled)
        return;
      if (arg1 == null)
        arg1 = new byte[0];
      int length = arg1.Length;
      fixed (byte* numPtr = &arg1[0])
      {
        EventSource.EventData* data = stackalloc EventSource.EventData[2];
        data->DataPointer = (IntPtr) ((void*) &length);
        data->Size = 4;
        data[1].DataPointer = (IntPtr) ((void*) numPtr);
        data[1].Size = length;
        this.WriteEventCore(eventId, 2, data);
      }
    }

    /// <summary>使用指定的标识符、64 位整数和字节数组参数写入事件数据。</summary>
    /// <param name="eventId">事件标识符。该值应介于 0 到 65535 之间。</param>
    /// <param name="arg1">64 位整数参数。</param>
    /// <param name="arg2">字节数组参数。</param>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    protected unsafe void WriteEvent(int eventId, long arg1, byte[] arg2)
    {
      if (!this.m_eventSourceEnabled)
        return;
      if (arg2 == null)
        arg2 = new byte[0];
      int length = arg2.Length;
      fixed (byte* numPtr = &arg2[0])
      {
        EventSource.EventData* data = stackalloc EventSource.EventData[3];
        data->DataPointer = (IntPtr) ((void*) &arg1);
        data->Size = 8;
        data[1].DataPointer = (IntPtr) ((void*) &length);
        data[1].Size = 4;
        data[2].DataPointer = (IntPtr) ((void*) numPtr);
        data[2].Size = length;
        this.WriteEventCore(eventId, 3, data);
      }
    }

    /// <summary>通过使用提供的事件标识符和事件数据，创建新的 <see cref="Overload:System.Diagnostics.Tracing.EventSource.WriteEvent" /> 重载。</summary>
    /// <param name="eventId">事件标识符。</param>
    /// <param name="eventDataCount">事件数据项的数目。</param>
    /// <param name="data">包含事件数据的结构。</param>
    [SecurityCritical]
    [CLSCompliant(false)]
    protected unsafe void WriteEventCore(int eventId, int eventDataCount, EventSource.EventData* data)
    {
      this.WriteEventWithRelatedActivityIdCore(eventId, (Guid*) null, eventDataCount, data);
    }

    /// <summary>[在 .NET Framework 4.5.1 和更高版本中受支持] 写入一个指示当前活动与其他活动相关的事件。</summary>
    /// <param name="eventId">在 <see cref="T:System.Diagnostics.Tracing.EventSource" /> 中唯一标识此事件的标识符。</param>
    /// <param name="relatedActivityId">指向相关活动 ID 的 GUID 的指针。</param>
    /// <param name="eventDataCount">
    /// <paramref name="data" /> 字段中的项数。</param>
    /// <param name="data">指向事件数据字段中第一个项的指针。</param>
    [SecurityCritical]
    [CLSCompliant(false)]
    protected unsafe void WriteEventWithRelatedActivityIdCore(int eventId, Guid* relatedActivityId, int eventDataCount, EventSource.EventData* data)
    {
      if (!this.m_eventSourceEnabled)
        return;
      try
      {
        if ((IntPtr) relatedActivityId != IntPtr.Zero)
          this.ValidateEventOpcodeForTransfer(ref this.m_eventData[eventId]);
        if (this.m_eventData[eventId].EnabledForETW)
        {
          EventOpcode eventOpcode = (EventOpcode) this.m_eventData[eventId].Descriptor.Opcode;
          EventActivityOptions eventActivityOptions = this.m_eventData[eventId].ActivityOptions;
          Guid* activityID = (Guid*) null;
          Guid activityId = Guid.Empty;
          Guid relatedActivityId1 = Guid.Empty;
          if (eventOpcode != EventOpcode.Info && (IntPtr) relatedActivityId == IntPtr.Zero && (eventActivityOptions & EventActivityOptions.Disable) == EventActivityOptions.None)
          {
            if (eventOpcode == EventOpcode.Start)
              this.m_activityTracker.OnStart(this.m_name, this.m_eventData[eventId].Name, this.m_eventData[eventId].Descriptor.Task, ref activityId, ref relatedActivityId1, this.m_eventData[eventId].ActivityOptions);
            else if (eventOpcode == EventOpcode.Stop)
              this.m_activityTracker.OnStop(this.m_name, this.m_eventData[eventId].Name, this.m_eventData[eventId].Descriptor.Task, ref activityId);
            if (activityId != Guid.Empty)
              activityID = &activityId;
            if (relatedActivityId1 != Guid.Empty)
              relatedActivityId = &relatedActivityId1;
          }
          SessionMask sessionMask = SessionMask.All;
          if ((long) (ulong) this.m_curLiveSessions != 0L)
            sessionMask = this.GetEtwSessionMask(eventId, relatedActivityId);
          if ((long) (ulong) sessionMask != 0L || this.m_legacySessions != null && this.m_legacySessions.Count > 0)
          {
            if (!this.SelfDescribingEvents)
            {
              if (sessionMask.IsEqualOrSupersetOf(this.m_curLiveSessions))
              {
                if (!this.m_provider.WriteEvent(ref this.m_eventData[eventId].Descriptor, activityID, relatedActivityId, eventDataCount, (IntPtr) ((void*) data)))
                  this.ThrowEventSourceException((Exception) null);
              }
              else
              {
                long num = this.m_eventData[eventId].Descriptor.Keywords & ~(long) SessionMask.All.ToEventKeywords();
                EventDescriptor eventDescriptor = new EventDescriptor(this.m_eventData[eventId].Descriptor.EventId, this.m_eventData[eventId].Descriptor.Version, this.m_eventData[eventId].Descriptor.Channel, this.m_eventData[eventId].Descriptor.Level, this.m_eventData[eventId].Descriptor.Opcode, this.m_eventData[eventId].Descriptor.Task, (long) sessionMask.ToEventKeywords() | num);
                if (!this.m_provider.WriteEvent(ref eventDescriptor, activityID, relatedActivityId, eventDataCount, (IntPtr) ((void*) data)))
                  this.ThrowEventSourceException((Exception) null);
              }
            }
            else
            {
              TraceLoggingEventTypes eventTypes = this.m_eventData[eventId].TraceLoggingEventTypes;
              if (eventTypes == null)
              {
                eventTypes = new TraceLoggingEventTypes(this.m_eventData[eventId].Name, EventTags.None, this.m_eventData[eventId].Parameters);
                Interlocked.CompareExchange<TraceLoggingEventTypes>(ref this.m_eventData[eventId].TraceLoggingEventTypes, eventTypes, (TraceLoggingEventTypes) null);
              }
              long num = this.m_eventData[eventId].Descriptor.Keywords & ~(long) SessionMask.All.ToEventKeywords();
              EventSourceOptions options = new EventSourceOptions() { Keywords = (EventKeywords) ((long) sessionMask.ToEventKeywords() | num), Level = (EventLevel) this.m_eventData[eventId].Descriptor.Level, Opcode = (EventOpcode) this.m_eventData[eventId].Descriptor.Opcode };
              this.WriteMultiMerge(this.m_eventData[eventId].Name, ref options, eventTypes, activityID, relatedActivityId, data);
            }
          }
        }
        if (this.m_Dispatchers == null || !this.m_eventData[eventId].EnabledForAnyListener)
          return;
        this.WriteToAllListeners(eventId, relatedActivityId, eventDataCount, data);
      }
      catch (Exception ex)
      {
        if (ex is EventSourceException)
          throw;
        else
          this.ThrowEventSourceException(ex);
      }
    }

    /// <summary>通过使用提供的事件标识符和参数数组写入事件。</summary>
    /// <param name="eventId">事件标识符。该值应介于 0 到 65535 之间。</param>
    /// <param name="args">对象数组。</param>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    protected unsafe void WriteEvent(int eventId, params object[] args)
    {
      this.WriteEventVarargs(eventId, (Guid*) null, args);
    }

    /// <summary>[在 .NET Framework 4.5.1 和更高版本中受支持] 写入一个指示当前活动与其他活动相关的事件。</summary>
    /// <param name="eventId">在 <see cref="T:System.Diagnostics.Tracing.EventSource" /> 中唯一标识此事件的标识符。</param>
    /// <param name="relatedActivityId">相关的活动标识符。</param>
    /// <param name="args">包含与事件相关的数据的对象数组。</param>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    protected unsafe void WriteEventWithRelatedActivityId(int eventId, Guid relatedActivityId, params object[] args)
    {
      this.WriteEventVarargs(eventId, &relatedActivityId, args);
    }

    /// <summary>释放由 <see cref="T:System.Diagnostics.Tracing.EventSource" /> 类的当前实例占用的所有资源。</summary>
    [__DynamicallyInvokable]
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    /// <summary>释放 <see cref="T:System.Diagnostics.Tracing.EventSource" /> 类使用的非托管资源，并可以选择释放托管资源。</summary>
    /// <param name="disposing">若要释放托管资源和非托管资源，则为 true；若仅释放非托管资源，则为 false。</param>
    [__DynamicallyInvokable]
    protected virtual void Dispose(bool disposing)
    {
      if (disposing)
      {
        if (this.m_eventSourceEnabled)
        {
          try
          {
            this.SendManifest(this.m_rawManifest);
          }
          catch (Exception ex)
          {
          }
          this.m_eventSourceEnabled = false;
        }
        if (this.m_provider != null)
        {
          this.m_provider.Dispose();
          this.m_provider = (EventSource.OverideEventProvider) null;
        }
      }
      this.m_eventSourceEnabled = false;
    }

    internal void WriteStringToListener(EventListener listener, string msg, SessionMask m)
    {
      if (!this.m_eventSourceEnabled)
        return;
      if (listener == null)
        this.WriteEventString(EventLevel.LogAlways, (long) m.ToEventKeywords(), msg);
      else
        listener.OnEventWritten(new EventWrittenEventArgs(this)
        {
          EventId = 0,
          Payload = new ReadOnlyCollection<object>((IList<object>) new List<object>()
          {
            (object) msg
          })
        });
    }

    [SecurityCritical]
    private unsafe void WriteEventRaw(ref EventDescriptor eventDescriptor, Guid* activityID, Guid* relatedActivityID, int dataCount, IntPtr data)
    {
      if (this.m_provider == null)
      {
        this.ThrowEventSourceException((Exception) null);
      }
      else
      {
        if (this.m_provider.WriteEventRaw(ref eventDescriptor, activityID, relatedActivityID, dataCount, data))
          return;
        this.ThrowEventSourceException((Exception) null);
      }
    }

    [SecuritySafeCritical]
    private unsafe void Initialize(Guid eventSourceGuid, string eventSourceName, string[] traits)
    {
      try
      {
        this.m_traits = traits;
        if (this.m_traits != null && this.m_traits.Length % 2 != 0)
          throw new ArgumentException(Environment.GetResourceString("TraitEven"), "traits");
        if (eventSourceGuid == Guid.Empty)
          throw new ArgumentException(Environment.GetResourceString("EventSource_NeedGuid"));
        if (eventSourceName == null)
          throw new ArgumentException(Environment.GetResourceString("EventSource_NeedName"));
        this.m_name = eventSourceName;
        this.m_guid = eventSourceGuid;
        this.m_curLiveSessions = new SessionMask(0U);
        this.m_etwSessionIdMap = new EtwSession[4];
        this.m_activityTracker = ActivityTracker.Instance;
        this.InitializeProviderMetadata();
        EventSource.OverideEventProvider overideEventProvider = new EventSource.OverideEventProvider(this);
        overideEventProvider.Register(eventSourceGuid);
        EventListener.AddEventSource(this);
        this.m_provider = overideEventProvider;
        if (this.Name != "System.Diagnostics.Eventing.FrameworkEventSource" || Environment.OSVersion.Version.Major * 10 + Environment.OSVersion.Version.Minor >= 62)
        {
          fixed (byte* numPtr = this.providerMetadata)
            this.m_provider.SetInformation(UnsafeNativeMethods.ManifestEtw.EVENT_INFO_CLASS.SetTraits, (void*) numPtr, this.providerMetadata.Length);
        }
        this.m_completelyInited = true;
      }
      catch (Exception ex)
      {
        if (this.m_constructionException == null)
          this.m_constructionException = ex;
        this.ReportOutOfBandMessage("ERROR: Exception during construction of EventSource " + this.Name + ": " + ex.Message, true);
      }
      lock (EventListener.EventListenersLock)
      {
        for (; this.m_deferredCommands != null; this.m_deferredCommands = this.m_deferredCommands.nextCommand)
          this.DoCommand(this.m_deferredCommands);
      }
    }

    private static string GetName(Type eventSourceType, EventManifestOptions flags)
    {
      if (eventSourceType == (Type) null)
        throw new ArgumentNullException("eventSourceType");
      EventSourceAttribute eventSourceAttribute = (EventSourceAttribute) EventSource.GetCustomAttributeHelper((MemberInfo) eventSourceType, typeof (EventSourceAttribute), flags);
      if (eventSourceAttribute != null && eventSourceAttribute.Name != null)
        return eventSourceAttribute.Name;
      return eventSourceType.Name;
    }

    private static Guid GenerateGuidFromName(string name)
    {
      byte[] bytes = Encoding.BigEndianUnicode.GetBytes(name);
      EventSource.Sha1ForNonSecretPurposes nonSecretPurposes = new EventSource.Sha1ForNonSecretPurposes();
      nonSecretPurposes.Start();
      nonSecretPurposes.Append(EventSource.namespaceBytes);
      nonSecretPurposes.Append(bytes);
      Array.Resize<byte>(ref bytes, 16);
      nonSecretPurposes.Finish(bytes);
      bytes[7] = (byte) ((int) bytes[7] & 15 | 80);
      return new Guid(bytes);
    }

    [SecurityCritical]
    private unsafe object DecodeObject(int eventId, int parameterId, ref EventSource.EventData* data)
    {
      IntPtr dataPointer1 = data->DataPointer;
      ++data;
      for (Type type = this.m_eventData[eventId].Parameters[parameterId].ParameterType; !(type == typeof (IntPtr)); type = Enum.GetUnderlyingType(type))
      {
        if (type == typeof (int))
          return (object) *(int*) (void*) dataPointer1;
        if (type == typeof (uint))
          return (object) *(uint*) (void*) dataPointer1;
        if (type == typeof (long))
          return (object) *(long*) (void*) dataPointer1;
        if (type == typeof (ulong))
          return (object) (ulong) *(long*) (void*) dataPointer1;
        if (type == typeof (byte))
          return (object) *(byte*) (void*) dataPointer1;
        if (type == typeof (sbyte))
          return (object) *(sbyte*) (void*) dataPointer1;
        if (type == typeof (short))
          return (object) *(short*) (void*) dataPointer1;
        if (type == typeof (ushort))
          return (object) *(ushort*) (void*) dataPointer1;
        if (type == typeof (float))
          return (object) *(float*) (void*) dataPointer1;
        if (type == typeof (double))
          return (object) *(double*) (void*) dataPointer1;
        if (type == typeof (Decimal))
          return (object) *(Decimal*) (void*) dataPointer1;
        if (type == typeof (bool))
        {
          if (*(int*) (void*) dataPointer1 == 1)
            return (object) true;
          return (object) false;
        }
        if (type == typeof (Guid))
          return (object) *(Guid*) (void*) dataPointer1;
        if (type == typeof (char))
          return (object) (char) *(ushort*) (void*) dataPointer1;
        if (type == typeof (DateTime))
          return (object) DateTime.FromFileTimeUtc(*(long*) (void*) dataPointer1);
        if (type == typeof (byte[]))
        {
          int length = *(int*) (void*) dataPointer1;
          byte[] numArray = new byte[length];
          IntPtr dataPointer2 = data->DataPointer;
          ++data;
          for (int index = 0; index < length; ++index)
            numArray[index] = *(byte*) (void*) dataPointer2;
          return (object) numArray;
        }
        if (type == typeof (byte*))
          return (object) null;
        if (!type.IsEnum())
          return (object) Marshal.PtrToStringUni(dataPointer1);
      }
      return (object) *(IntPtr*) (void*) dataPointer1;
    }

    private EventDispatcher GetDispatcher(EventListener listener)
    {
      EventDispatcher eventDispatcher = this.m_Dispatchers;
      while (eventDispatcher != null && eventDispatcher.m_Listener != listener)
        eventDispatcher = eventDispatcher.m_Next;
      return eventDispatcher;
    }

    [SecurityCritical]
    private unsafe void WriteEventVarargs(int eventId, Guid* childActivityID, object[] args)
    {
      if (!this.m_eventSourceEnabled)
        return;
      try
      {
        if ((IntPtr) childActivityID != IntPtr.Zero)
          this.ValidateEventOpcodeForTransfer(ref this.m_eventData[eventId]);
        if (this.m_eventData[eventId].EnabledForETW)
        {
          Guid* activityID = (Guid*) null;
          Guid activityId = Guid.Empty;
          Guid relatedActivityId = Guid.Empty;
          EventOpcode eventOpcode = (EventOpcode) this.m_eventData[eventId].Descriptor.Opcode;
          EventActivityOptions eventActivityOptions = this.m_eventData[eventId].ActivityOptions;
          if ((IntPtr) childActivityID == IntPtr.Zero && (eventActivityOptions & EventActivityOptions.Disable) == EventActivityOptions.None)
          {
            if (eventOpcode == EventOpcode.Start)
              this.m_activityTracker.OnStart(this.m_name, this.m_eventData[eventId].Name, this.m_eventData[eventId].Descriptor.Task, ref activityId, ref relatedActivityId, this.m_eventData[eventId].ActivityOptions);
            else if (eventOpcode == EventOpcode.Stop)
              this.m_activityTracker.OnStop(this.m_name, this.m_eventData[eventId].Name, this.m_eventData[eventId].Descriptor.Task, ref activityId);
            if (activityId != Guid.Empty)
              activityID = &activityId;
            if (relatedActivityId != Guid.Empty)
              childActivityID = &relatedActivityId;
          }
          SessionMask sessionMask = SessionMask.All;
          if ((long) (ulong) this.m_curLiveSessions != 0L)
            sessionMask = this.GetEtwSessionMask(eventId, childActivityID);
          if ((long) (ulong) sessionMask != 0L || this.m_legacySessions != null && this.m_legacySessions.Count > 0)
          {
            if (!this.SelfDescribingEvents)
            {
              if (sessionMask.IsEqualOrSupersetOf(this.m_curLiveSessions))
              {
                if (!this.m_provider.WriteEvent(ref this.m_eventData[eventId].Descriptor, activityID, childActivityID, args))
                  this.ThrowEventSourceException((Exception) null);
              }
              else
              {
                long num = this.m_eventData[eventId].Descriptor.Keywords & ~(long) SessionMask.All.ToEventKeywords();
                EventDescriptor eventDescriptor = new EventDescriptor(this.m_eventData[eventId].Descriptor.EventId, this.m_eventData[eventId].Descriptor.Version, this.m_eventData[eventId].Descriptor.Channel, this.m_eventData[eventId].Descriptor.Level, this.m_eventData[eventId].Descriptor.Opcode, this.m_eventData[eventId].Descriptor.Task, (long) (ulong) sessionMask | num);
                if (!this.m_provider.WriteEvent(ref eventDescriptor, activityID, childActivityID, args))
                  this.ThrowEventSourceException((Exception) null);
              }
            }
            else
            {
              TraceLoggingEventTypes eventTypes = this.m_eventData[eventId].TraceLoggingEventTypes;
              if (eventTypes == null)
              {
                eventTypes = new TraceLoggingEventTypes(this.m_eventData[eventId].Name, EventTags.None, this.m_eventData[eventId].Parameters);
                Interlocked.CompareExchange<TraceLoggingEventTypes>(ref this.m_eventData[eventId].TraceLoggingEventTypes, eventTypes, (TraceLoggingEventTypes) null);
              }
              long num = this.m_eventData[eventId].Descriptor.Keywords & ~(long) SessionMask.All.ToEventKeywords();
              EventSourceOptions options = new EventSourceOptions() { Keywords = (EventKeywords) ((long) (ulong) sessionMask | num), Level = (EventLevel) this.m_eventData[eventId].Descriptor.Level, Opcode = (EventOpcode) this.m_eventData[eventId].Descriptor.Opcode };
              this.WriteMultiMerge(this.m_eventData[eventId].Name, ref options, eventTypes, activityID, childActivityID, args);
            }
          }
        }
        if (this.m_Dispatchers == null || !this.m_eventData[eventId].EnabledForAnyListener)
          return;
        if (AppContextSwitches.PreserveEventListnerObjectIdentity)
        {
          this.WriteToAllListeners(eventId, childActivityID, args);
        }
        else
        {
          object[] objArray = this.SerializeEventArgs(eventId, args);
          this.WriteToAllListeners(eventId, childActivityID, objArray);
        }
      }
      catch (Exception ex)
      {
        if (ex is EventSourceException)
          throw;
        else
          this.ThrowEventSourceException(ex);
      }
    }

    [SecurityCritical]
    private object[] SerializeEventArgs(int eventId, object[] args)
    {
      TraceLoggingEventTypes loggingEventTypes = this.m_eventData[eventId].TraceLoggingEventTypes;
      if (loggingEventTypes == null)
      {
        loggingEventTypes = new TraceLoggingEventTypes(this.m_eventData[eventId].Name, EventTags.None, this.m_eventData[eventId].Parameters);
        Interlocked.CompareExchange<TraceLoggingEventTypes>(ref this.m_eventData[eventId].TraceLoggingEventTypes, loggingEventTypes, (TraceLoggingEventTypes) null);
      }
      object[] objArray = new object[loggingEventTypes.typeInfos.Length];
      for (int index = 0; index < loggingEventTypes.typeInfos.Length; ++index)
        objArray[index] = loggingEventTypes.typeInfos[index].GetData(args[index]);
      return objArray;
    }

    [SecurityCritical]
    private unsafe void WriteToAllListeners(int eventId, Guid* childActivityID, int eventDataCount, EventSource.EventData* data)
    {
      int val1 = this.m_eventData[eventId].Parameters.Length;
      if (eventDataCount != val1)
      {
        this.ReportOutOfBandMessage(Environment.GetResourceString("EventSource_EventParametersMismatch", (object) eventId, (object) eventDataCount, (object) val1), true);
        val1 = Math.Min(val1, eventDataCount);
      }
      object[] objArray = new object[val1];
      EventSource.EventData* data1 = data;
      for (int parameterId = 0; parameterId < val1; ++parameterId)
        objArray[parameterId] = this.DecodeObject(eventId, parameterId, ref data1);
      this.WriteToAllListeners(eventId, childActivityID, objArray);
    }

    [SecurityCritical]
    private unsafe void WriteToAllListeners(int eventId, Guid* childActivityID, params object[] args)
    {
      EventWrittenEventArgs eventCallbackArgs = new EventWrittenEventArgs(this);
      eventCallbackArgs.EventId = eventId;
      if ((IntPtr) childActivityID != IntPtr.Zero)
        eventCallbackArgs.RelatedActivityId = *childActivityID;
      eventCallbackArgs.EventName = this.m_eventData[eventId].Name;
      eventCallbackArgs.Message = this.m_eventData[eventId].Message;
      eventCallbackArgs.Payload = new ReadOnlyCollection<object>((IList<object>) args);
      this.DisptachToAllListeners(eventId, childActivityID, eventCallbackArgs);
    }

    [SecurityCritical]
    private unsafe void DisptachToAllListeners(int eventId, Guid* childActivityID, EventWrittenEventArgs eventCallbackArgs)
    {
      Exception innerException = (Exception) null;
      for (EventDispatcher eventDispatcher = this.m_Dispatchers; eventDispatcher != null; eventDispatcher = eventDispatcher.m_Next)
      {
        if (eventId == -1 || eventDispatcher.m_EventEnabled[eventId])
        {
          ActivityFilter filterList = eventDispatcher.m_Listener.m_activityFilter;
          if (filterList == null || ActivityFilter.PassesActivityFilter(filterList, childActivityID, (int) this.m_eventData[eventId].TriggersActivityTracking > 0, this, eventId) || !eventDispatcher.m_activityFilteringEnabled)
          {
            try
            {
              eventDispatcher.m_Listener.OnEventWritten(eventCallbackArgs);
            }
            catch (Exception ex)
            {
              this.ReportOutOfBandMessage("ERROR: Exception during EventSource.OnEventWritten: " + ex.Message, false);
              innerException = ex;
            }
          }
        }
      }
      if (innerException != null)
        throw new EventSourceException(innerException);
    }

    [SecuritySafeCritical]
    private unsafe void WriteEventString(EventLevel level, long keywords, string msgString)
    {
      if (this.m_provider == null)
        return;
      string str1 = "EventSourceMessage";
      if (this.SelfDescribingEvents)
      {
        EventSourceOptions options = new EventSourceOptions() { Keywords = (EventKeywords) keywords, Level = level };
        var data = new{ message = msgString };
        TraceLoggingEventTypes eventTypes = new TraceLoggingEventTypes(str1, EventTags.None, new Type[1]{ data.GetType() });
        this.WriteMultiMergeInner(str1, ref options, eventTypes, (Guid*) IntPtr.Zero, (Guid*) IntPtr.Zero, (object) data);
      }
      else
      {
        if (this.m_rawManifest == null && (int) this.m_outOfBandMessageCount == 1)
        {
          ManifestBuilder manifestBuilder = new ManifestBuilder(this.Name, this.Guid, this.Name, (ResourceManager) null, EventManifestOptions.None);
          manifestBuilder.StartEvent(str1, new EventAttribute(0)
          {
            Level = EventLevel.LogAlways,
            Task = (EventTask) 65534
          });
          manifestBuilder.AddEventParameter(typeof (string), "message");
          manifestBuilder.EndEvent();
          this.SendManifest(manifestBuilder.CreateManifest());
        }
        string str2 = msgString;
        char* chPtr = (char*) str2;
        if ((IntPtr) chPtr != IntPtr.Zero)
          chPtr += RuntimeHelpers.OffsetToStringData;
        EventDescriptor eventDescriptor = new EventDescriptor(0, (byte) 0, (byte) 0, (byte) level, (byte) 0, 0, keywords);
        this.m_provider.WriteEvent(ref eventDescriptor, (Guid*) null, (Guid*) null, 1, (IntPtr) ((void*) &new EventProvider.EventData()
        {
          Ptr = (ulong) chPtr,
          Size = (uint) (2 * (msgString.Length + 1)),
          Reserved = 0U
        }));
        str2 = (string) null;
      }
    }

    private void WriteStringToAllListeners(string eventName, string msg)
    {
      EventWrittenEventArgs eventData = new EventWrittenEventArgs(this);
      eventData.EventId = 0;
      eventData.Message = msg;
      eventData.Payload = new ReadOnlyCollection<object>((IList<object>) new List<object>()
      {
        (object) msg
      });
      eventData.PayloadNames = new ReadOnlyCollection<string>((IList<string>) new List<string>()
      {
        "message"
      });
      eventData.EventName = eventName;
      for (EventDispatcher eventDispatcher = this.m_Dispatchers; eventDispatcher != null; eventDispatcher = eventDispatcher.m_Next)
      {
        bool flag = false;
        if (eventDispatcher.m_EventEnabled == null)
        {
          flag = true;
        }
        else
        {
          for (int index = 0; index < eventDispatcher.m_EventEnabled.Length; ++index)
          {
            if (eventDispatcher.m_EventEnabled[index])
            {
              flag = true;
              break;
            }
          }
        }
        try
        {
          if (flag)
            eventDispatcher.m_Listener.OnEventWritten(eventData);
        }
        catch
        {
        }
      }
    }

    [SecurityCritical]
    private unsafe SessionMask GetEtwSessionMask(int eventId, Guid* childActivityID)
    {
      SessionMask sessionMask = new SessionMask();
      for (int index = 0; (long) index < 4L; ++index)
      {
        EtwSession etwSession = this.m_etwSessionIdMap[index];
        if (etwSession != null)
        {
          ActivityFilter filterList = etwSession.m_activityFilter;
          if (filterList == null && !this.m_activityFilteringForETWEnabled[index] || filterList != null && ActivityFilter.PassesActivityFilter(filterList, childActivityID, (int) this.m_eventData[eventId].TriggersActivityTracking > 0, this, eventId) || !this.m_activityFilteringForETWEnabled[index])
            sessionMask[index] = true;
        }
      }
      if (this.m_legacySessions != null && this.m_legacySessions.Count > 0 && (int) this.m_eventData[eventId].Descriptor.Opcode == 9)
      {
        Guid* currentActivityId = (Guid*) null;
        foreach (EtwSession mLegacySession in this.m_legacySessions)
        {
          if (mLegacySession != null)
          {
            ActivityFilter filterList = mLegacySession.m_activityFilter;
            if (filterList != null)
            {
              if ((IntPtr) currentActivityId == IntPtr.Zero)
                currentActivityId = &EventSource.InternalCurrentThreadActivityId;
              ActivityFilter.FlowActivityIfNeeded(filterList, currentActivityId, childActivityID);
            }
          }
        }
      }
      return sessionMask;
    }

    private bool IsEnabledByDefault(int eventNum, bool enable, EventLevel currentLevel, EventKeywords currentMatchAnyKeyword)
    {
      if (!enable)
        return false;
      EventLevel eventLevel = (EventLevel) this.m_eventData[eventNum].Descriptor.Level;
      EventKeywords eventKeywords = (EventKeywords) (this.m_eventData[eventNum].Descriptor.Keywords & ~(long) SessionMask.All.ToEventKeywords());
      EventChannel eventChannel = (EventChannel) this.m_eventData[eventNum].Descriptor.Channel;
      return this.IsEnabledCommon(enable, currentLevel, currentMatchAnyKeyword, eventLevel, eventKeywords, eventChannel);
    }

    private bool IsEnabledCommon(bool enabled, EventLevel currentLevel, EventKeywords currentMatchAnyKeyword, EventLevel eventLevel, EventKeywords eventKeywords, EventChannel eventChannel)
    {
      if (!enabled || currentLevel != EventLevel.LogAlways && currentLevel < eventLevel)
        return false;
      if (currentMatchAnyKeyword != EventKeywords.None && eventKeywords != EventKeywords.None)
      {
        if (eventChannel != EventChannel.None && this.m_channelData != null && (EventChannel) this.m_channelData.Length > eventChannel)
        {
          EventKeywords eventKeywords1 = (EventKeywords) this.m_channelData[(int) eventChannel] | eventKeywords;
          if (eventKeywords1 != EventKeywords.None && (eventKeywords1 & currentMatchAnyKeyword) == EventKeywords.None)
            return false;
        }
        else if ((eventKeywords & currentMatchAnyKeyword) == EventKeywords.None)
          return false;
      }
      return true;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private void ThrowEventSourceException(Exception innerEx = null)
    {
      if ((int) EventSource.m_EventSourceExceptionRecurenceCount > 0)
        return;
      try
      {
        ++EventSource.m_EventSourceExceptionRecurenceCount;
        switch (EventProvider.GetLastWriteEventError())
        {
          case EventProvider.WriteEventErrorCode.NoFreeBuffers:
            this.ReportOutOfBandMessage("EventSourceException: " + Environment.GetResourceString("EventSource_NoFreeBuffers"), true);
            if (!this.ThrowOnEventWriteErrors)
              break;
            throw new EventSourceException(Environment.GetResourceString("EventSource_NoFreeBuffers"), innerEx);
          case EventProvider.WriteEventErrorCode.EventTooBig:
            this.ReportOutOfBandMessage("EventSourceException: " + Environment.GetResourceString("EventSource_EventTooBig"), true);
            if (!this.ThrowOnEventWriteErrors)
              break;
            throw new EventSourceException(Environment.GetResourceString("EventSource_EventTooBig"), innerEx);
          case EventProvider.WriteEventErrorCode.NullInput:
            this.ReportOutOfBandMessage("EventSourceException: " + Environment.GetResourceString("EventSource_NullInput"), true);
            if (!this.ThrowOnEventWriteErrors)
              break;
            throw new EventSourceException(Environment.GetResourceString("EventSource_NullInput"), innerEx);
          case EventProvider.WriteEventErrorCode.TooManyArgs:
            this.ReportOutOfBandMessage("EventSourceException: " + Environment.GetResourceString("EventSource_TooManyArgs"), true);
            if (!this.ThrowOnEventWriteErrors)
              break;
            throw new EventSourceException(Environment.GetResourceString("EventSource_TooManyArgs"), innerEx);
          default:
            if (innerEx != null)
              this.ReportOutOfBandMessage("EventSourceException: " + (object) innerEx.GetType() + ":" + innerEx.Message, true);
            else
              this.ReportOutOfBandMessage("EventSourceException", true);
            if (!this.ThrowOnEventWriteErrors)
              break;
            throw new EventSourceException(innerEx);
        }
      }
      finally
      {
        --EventSource.m_EventSourceExceptionRecurenceCount;
      }
    }

    private void ValidateEventOpcodeForTransfer(ref EventSource.EventMetadata eventData)
    {
      if ((int) eventData.Descriptor.Opcode == 9 || (int) eventData.Descriptor.Opcode == 240)
        return;
      this.ThrowEventSourceException((Exception) null);
    }

    internal static EventOpcode GetOpcodeWithDefault(EventOpcode opcode, string eventName)
    {
      if (opcode == EventOpcode.Info)
      {
        if (eventName.EndsWith("Start"))
          return EventOpcode.Start;
        if (eventName.EndsWith("Stop"))
          return EventOpcode.Stop;
      }
      return opcode;
    }

    internal void SendCommand(EventListener listener, int perEventSourceSessionId, int etwSessionId, EventCommand command, bool enable, EventLevel level, EventKeywords matchAnyKeyword, IDictionary<string, string> commandArguments)
    {
      EventCommandEventArgs commandArgs = new EventCommandEventArgs(command, commandArguments, this, listener, perEventSourceSessionId, etwSessionId, enable, level, matchAnyKeyword);
      lock (EventListener.EventListenersLock)
      {
        if (this.m_completelyInited)
        {
          this.DoCommand(commandArgs);
        }
        else
        {
          commandArgs.nextCommand = this.m_deferredCommands;
          this.m_deferredCommands = commandArgs;
        }
      }
    }

    internal void DoCommand(EventCommandEventArgs commandArgs)
    {
      if (this.m_provider == null)
        return;
      this.m_outOfBandMessageCount = (byte) 0;
      bool flag1 = commandArgs.perEventSourceSessionId > 0 && (long) commandArgs.perEventSourceSessionId <= 4L;
      try
      {
        this.EnsureDescriptorsInitialized();
        commandArgs.dispatcher = this.GetDispatcher(commandArgs.listener);
        if (commandArgs.dispatcher == null && commandArgs.listener != null)
          throw new ArgumentException(Environment.GetResourceString("EventSource_ListenerNotFound"));
        if (commandArgs.Arguments == null)
          commandArgs.Arguments = (IDictionary<string, string>) new Dictionary<string, string>();
        if (commandArgs.Command == EventCommand.Update)
        {
          for (int index = 0; index < this.m_eventData.Length; ++index)
            this.EnableEventForDispatcher(commandArgs.dispatcher, index, this.IsEnabledByDefault(index, commandArgs.enable, commandArgs.level, commandArgs.matchAnyKeyword));
          if (commandArgs.enable)
          {
            if (!this.m_eventSourceEnabled)
            {
              this.m_level = commandArgs.level;
              this.m_matchAnyKeyword = commandArgs.matchAnyKeyword;
            }
            else
            {
              if (commandArgs.level > this.m_level)
                this.m_level = commandArgs.level;
              if (commandArgs.matchAnyKeyword == EventKeywords.None)
                this.m_matchAnyKeyword = EventKeywords.None;
              else if (this.m_matchAnyKeyword != EventKeywords.None)
                this.m_matchAnyKeyword = this.m_matchAnyKeyword | commandArgs.matchAnyKeyword;
            }
          }
          bool flag2 = commandArgs.perEventSourceSessionId >= 0;
          if (commandArgs.perEventSourceSessionId == 0 && !commandArgs.enable)
            flag2 = false;
          if (commandArgs.listener == null)
          {
            if (!flag2)
            {
              EventCommandEventArgs commandEventArgs = commandArgs;
              int num = -commandEventArgs.perEventSourceSessionId;
              commandEventArgs.perEventSourceSessionId = num;
            }
            --commandArgs.perEventSourceSessionId;
          }
          commandArgs.Command = flag2 ? EventCommand.Enable : EventCommand.Disable;
          if (flag2 && commandArgs.dispatcher == null && !this.SelfDescribingEvents)
            this.SendManifest(this.m_rawManifest);
          if (flag2 && commandArgs.perEventSourceSessionId != -1)
          {
            bool participateInSampling = false;
            string activityFilters;
            int sessionIdBit;
            EventSource.ParseCommandArgs(commandArgs.Arguments, out participateInSampling, out activityFilters, out sessionIdBit);
            if (commandArgs.listener == null && commandArgs.Arguments.Count > 0 && commandArgs.perEventSourceSessionId != sessionIdBit)
              throw new ArgumentException(Environment.GetResourceString("EventSource_SessionIdError", (object) (commandArgs.perEventSourceSessionId + 44), (object) (sessionIdBit + 44)));
            if (commandArgs.listener == null)
            {
              this.UpdateEtwSession(commandArgs.perEventSourceSessionId, commandArgs.etwSessionId, true, activityFilters, participateInSampling);
            }
            else
            {
              ActivityFilter.UpdateFilter(ref commandArgs.listener.m_activityFilter, this, 0, activityFilters);
              commandArgs.dispatcher.m_activityFilteringEnabled = participateInSampling;
            }
          }
          else if (!flag2 && commandArgs.listener == null && (commandArgs.perEventSourceSessionId >= 0 && (long) commandArgs.perEventSourceSessionId < 4L))
            commandArgs.Arguments["EtwSessionKeyword"] = (commandArgs.perEventSourceSessionId + 44).ToString((IFormatProvider) CultureInfo.InvariantCulture);
          if (commandArgs.enable)
            this.m_eventSourceEnabled = true;
          this.OnEventCommand(commandArgs);
          if (commandArgs.listener == null && !flag2 && commandArgs.perEventSourceSessionId != -1)
            this.UpdateEtwSession(commandArgs.perEventSourceSessionId, commandArgs.etwSessionId, false, (string) null, false);
          if (!commandArgs.enable)
          {
            if (commandArgs.listener == null)
            {
              for (int index = 0; (long) index < 4L; ++index)
              {
                EtwSession etwSession = this.m_etwSessionIdMap[index];
                if (etwSession != null)
                  ActivityFilter.DisableFilter(ref etwSession.m_activityFilter, this);
              }
              this.m_activityFilteringForETWEnabled = new SessionMask(0U);
              this.m_curLiveSessions = new SessionMask(0U);
              if (this.m_etwSessionIdMap != null)
              {
                for (int index = 0; (long) index < 4L; ++index)
                  this.m_etwSessionIdMap[index] = (EtwSession) null;
              }
              if (this.m_legacySessions != null)
                this.m_legacySessions.Clear();
            }
            else
            {
              ActivityFilter.DisableFilter(ref commandArgs.listener.m_activityFilter, this);
              commandArgs.dispatcher.m_activityFilteringEnabled = false;
            }
            for (int index = 0; index < this.m_eventData.Length; ++index)
            {
              bool flag3 = false;
              for (EventDispatcher eventDispatcher = this.m_Dispatchers; eventDispatcher != null; eventDispatcher = eventDispatcher.m_Next)
              {
                if (eventDispatcher.m_EventEnabled[index])
                {
                  flag3 = true;
                  break;
                }
              }
              this.m_eventData[index].EnabledForAnyListener = flag3;
            }
            if (!this.AnyEventEnabled())
            {
              this.m_level = EventLevel.LogAlways;
              this.m_matchAnyKeyword = EventKeywords.None;
              this.m_eventSourceEnabled = false;
            }
          }
          this.UpdateKwdTriggers(commandArgs.enable);
        }
        else
        {
          if (commandArgs.Command == EventCommand.SendManifest && this.m_rawManifest != null)
            this.SendManifest(this.m_rawManifest);
          this.OnEventCommand(commandArgs);
        }
        if (!this.m_completelyInited || !(commandArgs.listener != null | flag1))
          return;
        SessionMask sessions = SessionMask.FromId(commandArgs.perEventSourceSessionId);
        this.ReportActivitySamplingInfo(commandArgs.listener, sessions);
      }
      catch (Exception ex)
      {
        this.ReportOutOfBandMessage("ERROR: Exception in Command Processing for EventSource " + this.Name + ": " + ex.Message, true);
      }
    }

    internal void UpdateEtwSession(int sessionIdBit, int etwSessionId, bool bEnable, string activityFilters, bool participateInSampling)
    {
      if ((long) sessionIdBit < 4L)
      {
        if (bEnable)
        {
          EtwSession etwSession = EtwSession.GetEtwSession(etwSessionId, true);
          ActivityFilter.UpdateFilter(ref etwSession.m_activityFilter, this, sessionIdBit, activityFilters);
          this.m_etwSessionIdMap[sessionIdBit] = etwSession;
          this.m_activityFilteringForETWEnabled[sessionIdBit] = participateInSampling;
        }
        else
        {
          EtwSession etwSession = EtwSession.GetEtwSession(etwSessionId, false);
          this.m_etwSessionIdMap[sessionIdBit] = (EtwSession) null;
          this.m_activityFilteringForETWEnabled[sessionIdBit] = false;
          if (etwSession != null)
          {
            ActivityFilter.DisableFilter(ref etwSession.m_activityFilter, this);
            EtwSession.RemoveEtwSession(etwSession);
          }
        }
        this.m_curLiveSessions[sessionIdBit] = bEnable;
      }
      else if (bEnable)
      {
        if (this.m_legacySessions == null)
          this.m_legacySessions = new List<EtwSession>(8);
        EtwSession etwSession = EtwSession.GetEtwSession(etwSessionId, true);
        if (this.m_legacySessions.Contains(etwSession))
          return;
        this.m_legacySessions.Add(etwSession);
      }
      else
      {
        EtwSession etwSession = EtwSession.GetEtwSession(etwSessionId, false);
        if (etwSession == null)
          return;
        if (this.m_legacySessions != null)
          this.m_legacySessions.Remove(etwSession);
        EtwSession.RemoveEtwSession(etwSession);
      }
    }

    internal static bool ParseCommandArgs(IDictionary<string, string> commandArguments, out bool participateInSampling, out string activityFilters, out int sessionIdBit)
    {
      bool flag = true;
      participateInSampling = false;
      if (commandArguments.TryGetValue("ActivitySamplingStartEvent", out activityFilters))
        participateInSampling = true;
      string strA;
      if (commandArguments.TryGetValue("ActivitySampling", out strA))
        participateInSampling = string.Compare(strA, "false", StringComparison.OrdinalIgnoreCase) != 0 && !(strA == "0");
      int result = -1;
      string s;
      if (!commandArguments.TryGetValue("EtwSessionKeyword", out s) || !int.TryParse(s, out result) || (result < 44 || (long) result >= 48L))
      {
        sessionIdBit = -1;
        flag = false;
      }
      else
        sessionIdBit = result - 44;
      return flag;
    }

    internal void UpdateKwdTriggers(bool enable)
    {
      if (enable)
      {
        ulong num = (ulong) this.m_matchAnyKeyword;
        if ((long) num == 0L)
          num = ulong.MaxValue;
        this.m_keywordTriggers = 0L;
        for (int index = 0; (long) index < 4L; ++index)
        {
          EtwSession etwSession = this.m_etwSessionIdMap[index];
          if (etwSession != null)
            ActivityFilter.UpdateKwdTriggers(etwSession.m_activityFilter, this.m_guid, this, (EventKeywords) num);
        }
      }
      else
        this.m_keywordTriggers = 0L;
    }

    internal bool EnableEventForDispatcher(EventDispatcher dispatcher, int eventId, bool value)
    {
      if (dispatcher == null)
      {
        if (eventId >= this.m_eventData.Length)
          return false;
        if (this.m_provider != null)
          this.m_eventData[eventId].EnabledForETW = value;
      }
      else
      {
        if (eventId >= dispatcher.m_EventEnabled.Length)
          return false;
        dispatcher.m_EventEnabled[eventId] = value;
        if (value)
          this.m_eventData[eventId].EnabledForAnyListener = true;
      }
      return true;
    }

    private bool AnyEventEnabled()
    {
      for (int index = 0; index < this.m_eventData.Length; ++index)
      {
        if (this.m_eventData[index].EnabledForETW || this.m_eventData[index].EnabledForAnyListener)
          return true;
      }
      return false;
    }

    [SecuritySafeCritical]
    private void EnsureDescriptorsInitialized()
    {
      if (this.m_eventData == null)
      {
        this.m_rawManifest = EventSource.CreateManifestAndDescriptors(this.GetType(), this.Name, this, EventManifestOptions.None);
        foreach (WeakReference sEventSource in EventListener.s_EventSources)
        {
          EventSource eventSource = sEventSource.Target as EventSource;
          if (eventSource != null && eventSource.Guid == this.m_guid && (!eventSource.IsDisposed && eventSource != this))
            throw new ArgumentException(Environment.GetResourceString("EventSource_EventSourceGuidInUse", (object) this.m_guid));
        }
        for (EventDispatcher eventDispatcher = this.m_Dispatchers; eventDispatcher != null; eventDispatcher = eventDispatcher.m_Next)
        {
          if (eventDispatcher.m_EventEnabled == null)
            eventDispatcher.m_EventEnabled = new bool[this.m_eventData.Length];
        }
      }
      if ((int) EventSource.s_currentPid != 0)
        return;
      EventSource.s_currentPid = Win32Native.GetCurrentProcessId();
    }

    [SecuritySafeCritical]
    private unsafe bool SendManifest(byte[] rawManifest)
    {
      bool flag = true;
      if (rawManifest == null)
        return false;
      fixed (byte* numPtr = rawManifest)
      {
        EventDescriptor eventDescriptor = new EventDescriptor(65534, (byte) 1, (byte) 0, (byte) 0, (byte) 254, 65534, 72057594037927935L);
        ManifestEnvelope manifestEnvelope = new ManifestEnvelope();
        manifestEnvelope.Format = ManifestEnvelope.ManifestFormats.SimpleXmlFormat;
        manifestEnvelope.MajorVersion = (byte) 1;
        manifestEnvelope.MinorVersion = (byte) 0;
        manifestEnvelope.Magic = (byte) 91;
        int length = rawManifest.Length;
        manifestEnvelope.ChunkNumber = (ushort) 0;
        EventProvider.EventData* eventDataPtr = stackalloc EventProvider.EventData[2];
        eventDataPtr->Ptr = (ulong) &manifestEnvelope;
        eventDataPtr->Size = (uint) sizeof (ManifestEnvelope);
        eventDataPtr->Reserved = 0U;
        eventDataPtr[1].Ptr = (ulong) numPtr;
        eventDataPtr[1].Reserved = 0U;
        int val2 = 65280;
label_3:
        manifestEnvelope.TotalChunks = (ushort) ((length + (val2 - 1)) / val2);
        while (length > 0)
        {
          eventDataPtr[1].Size = (uint) Math.Min(length, val2);
          if (this.m_provider != null && !this.m_provider.WriteEvent(ref eventDescriptor, (Guid*) null, (Guid*) null, 2, (IntPtr) ((void*) eventDataPtr)))
          {
            if (EventProvider.GetLastWriteEventError() == EventProvider.WriteEventErrorCode.EventTooBig && (int) manifestEnvelope.ChunkNumber == 0 && val2 > 256)
            {
              val2 /= 2;
              goto label_3;
            }
            else
            {
              flag = false;
              if (this.ThrowOnEventWriteErrors)
              {
                this.ThrowEventSourceException((Exception) null);
                break;
              }
              break;
            }
          }
          else
          {
            length -= val2;
            eventDataPtr[1].Ptr += (ulong) (uint) val2;
            ++manifestEnvelope.ChunkNumber;
          }
        }
      }
      return flag;
    }

    internal static Attribute GetCustomAttributeHelper(MemberInfo member, Type attributeType, EventManifestOptions flags = EventManifestOptions.None)
    {
      if (!member.Module.Assembly.ReflectionOnly() && (flags & EventManifestOptions.AllowEventSourceOverride) == EventManifestOptions.None)
      {
        Attribute attribute = (Attribute) null;
        object[] customAttributes = member.GetCustomAttributes(attributeType, false);
        int index = 0;
        if (index < customAttributes.Length)
          attribute = (Attribute) customAttributes[index];
        return attribute;
      }
      string fullName = attributeType.FullName;
      foreach (CustomAttributeData customAttribute in (IEnumerable<CustomAttributeData>) CustomAttributeData.GetCustomAttributes(member))
      {
        if (EventSource.AttributeTypeNamesMatch(attributeType, customAttribute.Constructor.ReflectedType))
        {
          Attribute attribute = (Attribute) null;
          CustomAttributeTypedArgument attributeTypedArgument;
          if (customAttribute.ConstructorArguments.Count == 1)
          {
            Type type = attributeType;
            object[] objArray = new object[1];
            int index = 0;
            attributeTypedArgument = customAttribute.ConstructorArguments[0];
            object obj = attributeTypedArgument.Value;
            objArray[index] = obj;
            attribute = (Attribute) Activator.CreateInstance(type, objArray);
          }
          else if (customAttribute.ConstructorArguments.Count == 0)
            attribute = (Attribute) Activator.CreateInstance(attributeType);
          if (attribute != null)
          {
            Type type = attribute.GetType();
            foreach (CustomAttributeNamedArgument namedArgument in (IEnumerable<CustomAttributeNamedArgument>) customAttribute.NamedArguments)
            {
              PropertyInfo property = type.GetProperty(namedArgument.MemberInfo.Name, BindingFlags.Instance | BindingFlags.Public);
              attributeTypedArgument = namedArgument.TypedValue;
              object obj = attributeTypedArgument.Value;
              if (property.PropertyType.IsEnum)
                obj = Enum.Parse(property.PropertyType, obj.ToString());
              property.SetValue((object) attribute, obj, (object[]) null);
            }
            return attribute;
          }
        }
      }
      return (Attribute) null;
    }

    private static bool AttributeTypeNamesMatch(Type attributeType, Type reflectedAttributeType)
    {
      if (attributeType == reflectedAttributeType || string.Equals(attributeType.FullName, reflectedAttributeType.FullName, StringComparison.Ordinal))
        return true;
      if (string.Equals(attributeType.Name, reflectedAttributeType.Name, StringComparison.Ordinal) && attributeType.Namespace.EndsWith("Diagnostics.Tracing"))
        return reflectedAttributeType.Namespace.EndsWith("Diagnostics.Tracing");
      return false;
    }

    private static Type GetEventSourceBaseType(Type eventSourceType, bool allowEventSourceOverride, bool reflectionOnly)
    {
      if (eventSourceType.BaseType() == (Type) null)
        return (Type) null;
      do
      {
        eventSourceType = eventSourceType.BaseType();
      }
      while (eventSourceType != (Type) null && eventSourceType.IsAbstract());
      if (eventSourceType != (Type) null)
      {
        if (!allowEventSourceOverride)
        {
          if (reflectionOnly && eventSourceType.FullName != typeof (EventSource).FullName || !reflectionOnly && eventSourceType != typeof (EventSource))
            return (Type) null;
        }
        else if (eventSourceType.Name != "EventSource")
          return (Type) null;
      }
      return eventSourceType;
    }

    private static byte[] CreateManifestAndDescriptors(Type eventSourceType, string eventSourceDllName, EventSource source, EventManifestOptions flags = EventManifestOptions.None)
    {
      ManifestBuilder manifest = (ManifestBuilder) null;
      bool flag1 = source == null || !source.SelfDescribingEvents;
      Exception innerException = (Exception) null;
      byte[] numArray = (byte[]) null;
      if (eventSourceType.IsAbstract() && (flags & EventManifestOptions.Strict) == EventManifestOptions.None)
        return (byte[]) null;
      try
      {
        MethodInfo[] methods = eventSourceType.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        int eventId = 1;
        EventSource.EventMetadata[] eventData = (EventSource.EventMetadata[]) null;
        Dictionary<string, string> eventsByName = (Dictionary<string, string>) null;
        if (source != null || (flags & EventManifestOptions.Strict) != EventManifestOptions.None)
        {
          eventData = new EventSource.EventMetadata[methods.Length + 1];
          eventData[0].Name = "";
        }
        ResourceManager resources = (ResourceManager) null;
        EventSourceAttribute eventSourceAttribute = (EventSourceAttribute) EventSource.GetCustomAttributeHelper((MemberInfo) eventSourceType, typeof (EventSourceAttribute), flags);
        if (eventSourceAttribute != null && eventSourceAttribute.LocalizationResources != null)
          resources = new ResourceManager(eventSourceAttribute.LocalizationResources, eventSourceType.Assembly());
        manifest = new ManifestBuilder(EventSource.GetName(eventSourceType, flags), EventSource.GetGuid(eventSourceType), eventSourceDllName, resources, flags);
        manifest.StartEvent("EventSourceMessage", new EventAttribute(0)
        {
          Level = EventLevel.LogAlways,
          Task = (EventTask) 65534
        });
        manifest.AddEventParameter(typeof (string), "message");
        manifest.EndEvent();
        if ((flags & EventManifestOptions.Strict) != EventManifestOptions.None)
        {
          if (!(EventSource.GetEventSourceBaseType(eventSourceType, (uint) (flags & EventManifestOptions.AllowEventSourceOverride) > 0U, eventSourceType.Assembly().ReflectionOnly()) != (Type) null))
            manifest.ManifestError(Environment.GetResourceString("EventSource_TypeMustDeriveFromEventSource"), false);
          if (!eventSourceType.IsAbstract() && !eventSourceType.IsSealed())
            manifest.ManifestError(Environment.GetResourceString("EventSource_TypeMustBeSealedOrAbstract"), false);
        }
        string[] strArray = new string[3]{ "Keywords", "Tasks", "Opcodes" };
        foreach (string str in strArray)
        {
          Type nestedType = eventSourceType.GetNestedType(str);
          if (nestedType != (Type) null)
          {
            if (eventSourceType.IsAbstract())
            {
              manifest.ManifestError(Environment.GetResourceString("EventSource_AbstractMustNotDeclareKTOC", (object) nestedType.Name), 0 != 0);
            }
            else
            {
              foreach (FieldInfo field in nestedType.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
                EventSource.AddProviderEnumKind(manifest, field, str);
            }
          }
        }
        manifest.AddKeyword("Session3", 17592186044416UL);
        manifest.AddKeyword("Session2", 35184372088832UL);
        manifest.AddKeyword("Session1", 70368744177664UL);
        manifest.AddKeyword("Session0", 140737488355328UL);
        if (eventSourceType.Name != "EventSource")
        {
          for (int index1 = 0; index1 < methods.Length; ++index1)
          {
            MethodInfo method = methods[index1];
            ParameterInfo[] parameters = method.GetParameters();
            EventAttribute eventAttribute = (EventAttribute) EventSource.GetCustomAttributeHelper((MemberInfo) method, typeof (EventAttribute), flags);
            if (!method.IsStatic)
            {
              if (eventSourceType.IsAbstract())
              {
                if (eventAttribute != null)
                  manifest.ManifestError(Environment.GetResourceString("EventSource_AbstractMustNotDeclareEventMethods", (object) method.Name, (object) eventAttribute.EventId), 0 != 0);
              }
              else
              {
                if (eventAttribute == null)
                {
                  if (!(method.ReturnType != typeof (void)) && !method.IsVirtual && EventSource.GetCustomAttributeHelper((MemberInfo) method, typeof (NonEventAttribute), flags) == null)
                    eventAttribute = new EventAttribute(eventId);
                  else
                    continue;
                }
                else if (eventAttribute.EventId <= 0)
                {
                  manifest.ManifestError(Environment.GetResourceString("EventSource_NeedPositiveId", (object) method.Name), 1 != 0);
                  continue;
                }
                if (method.Name.LastIndexOf('.') >= 0)
                  manifest.ManifestError(Environment.GetResourceString("EventSource_EventMustNotBeExplicitImplementation", (object) method.Name, (object) eventAttribute.EventId), 0 != 0);
                ++eventId;
                string name = method.Name;
                if (!eventAttribute.IsOpcodeSet)
                {
                  bool flag2 = eventAttribute.Task == EventTask.None;
                  if (eventAttribute.Task == EventTask.None)
                    eventAttribute.Task = (EventTask) (65534 - eventAttribute.EventId);
                  eventAttribute.Opcode = EventSource.GetOpcodeWithDefault(EventOpcode.Info, name);
                  if (flag2)
                  {
                    if (eventAttribute.Opcode == EventOpcode.Start)
                    {
                      string str = name.Substring(0, name.Length - "Start".Length);
                      if (string.Compare(name, 0, str, 0, str.Length) == 0 && string.Compare(name, str.Length, "Start", 0, Math.Max(name.Length - str.Length, "Start".Length)) == 0)
                        manifest.AddTask(str, (int) eventAttribute.Task);
                    }
                    else if (eventAttribute.Opcode == EventOpcode.Stop)
                    {
                      int index2 = eventAttribute.EventId - 1;
                      if (eventData != null && index2 < eventData.Length)
                      {
                        EventSource.EventMetadata eventMetadata = eventData[index2];
                        string strB = name.Substring(0, name.Length - "Stop".Length);
                        if ((int) eventMetadata.Descriptor.Opcode == 1 && string.Compare(eventMetadata.Name, 0, strB, 0, strB.Length) == 0 && string.Compare(eventMetadata.Name, strB.Length, "Start", 0, Math.Max(eventMetadata.Name.Length - strB.Length, "Start".Length)) == 0)
                        {
                          eventAttribute.Task = (EventTask) eventMetadata.Descriptor.Task;
                          flag2 = false;
                        }
                      }
                      if (flag2 && (flags & EventManifestOptions.Strict) != EventManifestOptions.None)
                        throw new ArgumentException(Environment.GetResourceString("EventSource_StopsFollowStarts"));
                    }
                  }
                }
                EventSource.RemoveFirstArgIfRelatedActivityId(ref parameters);
                if (source == null || !source.SelfDescribingEvents)
                {
                  manifest.StartEvent(name, eventAttribute);
                  for (int index2 = 0; index2 < parameters.Length; ++index2)
                    manifest.AddEventParameter(parameters[index2].ParameterType, parameters[index2].Name);
                  manifest.EndEvent();
                }
                if (source != null || (flags & EventManifestOptions.Strict) != EventManifestOptions.None)
                {
                  EventSource.DebugCheckEvent(ref eventsByName, eventData, method, eventAttribute, manifest);
                  if (eventAttribute.Channel != EventChannel.None)
                    eventAttribute.Keywords |= (EventKeywords) manifest.GetChannelKeyword(eventAttribute.Channel);
                  string key = "event_" + name;
                  string localizedMessage = manifest.GetLocalizedMessage(key, CultureInfo.CurrentUICulture, false);
                  if (localizedMessage != null)
                    eventAttribute.Message = localizedMessage;
                  EventSource.AddEventDescriptor(ref eventData, name, eventAttribute, parameters);
                }
              }
            }
          }
        }
        NameInfo.ReserveEventIDsBelow(eventId);
        if (source != null)
        {
          EventSource.TrimEventDescriptors(ref eventData);
          source.m_eventData = eventData;
          source.m_channelData = manifest.GetChannelData();
        }
        if (!eventSourceType.IsAbstract())
        {
          if (source != null)
          {
            if (source.SelfDescribingEvents)
              goto label_70;
          }
          flag1 = (flags & EventManifestOptions.OnlyIfNeededForRegistration) == EventManifestOptions.None || (uint) manifest.GetChannelData().Length > 0U;
          if (!flag1 && (flags & EventManifestOptions.Strict) == EventManifestOptions.None)
            return (byte[]) null;
          numArray = manifest.CreateManifest();
        }
      }
      catch (Exception ex)
      {
        if ((flags & EventManifestOptions.Strict) == EventManifestOptions.None)
          throw;
        else
          innerException = ex;
      }
label_70:
      if ((flags & EventManifestOptions.Strict) != EventManifestOptions.None && (manifest.Errors.Count > 0 || innerException != null))
      {
        string message = string.Empty;
        if (manifest.Errors.Count > 0)
        {
          bool flag2 = true;
          foreach (string error in (IEnumerable<string>) manifest.Errors)
          {
            if (!flag2)
              message += Environment.NewLine;
            flag2 = false;
            message += error;
          }
        }
        else
          message = "Unexpected error: " + innerException.Message;
        throw new ArgumentException(message, innerException);
      }
      if (!flag1)
        return (byte[]) null;
      return numArray;
    }

    private static void RemoveFirstArgIfRelatedActivityId(ref ParameterInfo[] args)
    {
      if (args.Length == 0 || !(args[0].ParameterType == typeof (Guid)) || string.Compare(args[0].Name, "relatedActivityId", StringComparison.OrdinalIgnoreCase) != 0)
        return;
      ParameterInfo[] parameterInfoArray = new ParameterInfo[args.Length - 1];
      Array.Copy((Array) args, 1, (Array) parameterInfoArray, 0, args.Length - 1);
      args = parameterInfoArray;
    }

    private static void AddProviderEnumKind(ManifestBuilder manifest, FieldInfo staticField, string providerEnumKind)
    {
      bool flag = staticField.Module.Assembly.ReflectionOnly();
      Type fieldType = staticField.FieldType;
      if (!flag && fieldType == typeof (EventOpcode) || EventSource.AttributeTypeNamesMatch(fieldType, typeof (EventOpcode)))
      {
        if (!(providerEnumKind != "Opcodes"))
        {
          int num = (int) staticField.GetRawConstantValue();
          manifest.AddOpcode(staticField.Name, num);
          return;
        }
      }
      else if (!flag && fieldType == typeof (EventTask) || EventSource.AttributeTypeNamesMatch(fieldType, typeof (EventTask)))
      {
        if (!(providerEnumKind != "Tasks"))
        {
          int num = (int) staticField.GetRawConstantValue();
          manifest.AddTask(staticField.Name, num);
          return;
        }
      }
      else
      {
        if ((flag || !(fieldType == typeof (EventKeywords))) && !EventSource.AttributeTypeNamesMatch(fieldType, typeof (EventKeywords)))
          return;
        if (!(providerEnumKind != "Keywords"))
        {
          ulong num = (ulong) (long) staticField.GetRawConstantValue();
          manifest.AddKeyword(staticField.Name, num);
          return;
        }
      }
      manifest.ManifestError(Environment.GetResourceString("EventSource_EnumKindMismatch", (object) staticField.Name, (object) staticField.FieldType.Name, (object) providerEnumKind), 0 != 0);
    }

    private static void AddEventDescriptor(ref EventSource.EventMetadata[] eventData, string eventName, EventAttribute eventAttribute, ParameterInfo[] eventParameters)
    {
      if (eventData == null || eventData.Length <= eventAttribute.EventId)
      {
        EventSource.EventMetadata[] eventMetadataArray = new EventSource.EventMetadata[Math.Max(eventData.Length + 16, eventAttribute.EventId + 1)];
        Array.Copy((Array) eventData, (Array) eventMetadataArray, eventData.Length);
        eventData = eventMetadataArray;
      }
      eventData[eventAttribute.EventId].Descriptor = new EventDescriptor(eventAttribute.EventId, eventAttribute.Version, (byte) eventAttribute.Channel, (byte) eventAttribute.Level, (byte) eventAttribute.Opcode, (int) eventAttribute.Task, (long) (eventAttribute.Keywords | (EventKeywords) SessionMask.All.ToEventKeywords()));
      eventData[eventAttribute.EventId].Tags = eventAttribute.Tags;
      eventData[eventAttribute.EventId].Name = eventName;
      eventData[eventAttribute.EventId].Parameters = eventParameters;
      eventData[eventAttribute.EventId].Message = eventAttribute.Message;
      eventData[eventAttribute.EventId].ActivityOptions = eventAttribute.ActivityOptions;
    }

    private static void TrimEventDescriptors(ref EventSource.EventMetadata[] eventData)
    {
      int length = eventData.Length;
      while (0 < length)
      {
        --length;
        if (eventData[length].Descriptor.EventId != 0)
          break;
      }
      if (eventData.Length - length <= 2)
        return;
      EventSource.EventMetadata[] eventMetadataArray = new EventSource.EventMetadata[length + 1];
      Array.Copy((Array) eventData, (Array) eventMetadataArray, eventMetadataArray.Length);
      eventData = eventMetadataArray;
    }

    internal void AddListener(EventListener listener)
    {
      lock (EventListener.EventListenersLock)
      {
        bool[] local_2 = (bool[]) null;
        if (this.m_eventData != null)
          local_2 = new bool[this.m_eventData.Length];
        this.m_Dispatchers = new EventDispatcher(this.m_Dispatchers, local_2, listener);
        listener.OnEventSourceCreated(this);
      }
    }

    private static void DebugCheckEvent(ref Dictionary<string, string> eventsByName, EventSource.EventMetadata[] eventData, MethodInfo method, EventAttribute eventAttribute, ManifestBuilder manifest)
    {
      int eventId = eventAttribute.EventId;
      string name = method.Name;
      int helperCallFirstArg = EventSource.GetHelperCallFirstArg(method);
      if (helperCallFirstArg >= 0 && eventId != helperCallFirstArg)
        manifest.ManifestError(Environment.GetResourceString("EventSource_MismatchIdToWriteEvent", (object) name, (object) eventId, (object) helperCallFirstArg), 1 != 0);
      if (eventId < eventData.Length && eventData[eventId].Descriptor.EventId != 0)
        manifest.ManifestError(Environment.GetResourceString("EventSource_EventIdReused", (object) name, (object) eventId, (object) eventData[eventId].Name), 1 != 0);
      for (int index = 0; index < eventData.Length; ++index)
      {
        if (eventData[index].Name != null && (EventTask) eventData[index].Descriptor.Task == eventAttribute.Task && (EventOpcode) eventData[index].Descriptor.Opcode == eventAttribute.Opcode)
          manifest.ManifestError(Environment.GetResourceString("EventSource_TaskOpcodePairReused", (object) name, (object) eventId, (object) eventData[index].Name, (object) index), 0 != 0);
      }
      if (eventAttribute.Opcode != EventOpcode.Info && (eventAttribute.Task == EventTask.None || eventAttribute.Task == (EventTask) (65534 - eventId)))
        manifest.ManifestError(Environment.GetResourceString("EventSource_EventMustHaveTaskIfNonDefaultOpcode", (object) name, (object) eventId), 0 != 0);
      if (eventsByName == null)
        eventsByName = new Dictionary<string, string>();
      if (eventsByName.ContainsKey(name))
        manifest.ManifestError(Environment.GetResourceString("EventSource_EventNameReused", (object) name), 0 != 0);
      string index1;
      eventsByName[index1] = index1 = name;
    }

    [SecuritySafeCritical]
    private static int GetHelperCallFirstArg(MethodInfo method)
    {
      new ReflectionPermission(ReflectionPermissionFlag.MemberAccess).Assert();
      byte[] ilAsByteArray = method.GetMethodBody().GetILAsByteArray();
      int num1 = -1;
      for (int index1 = 0; index1 < ilAsByteArray.Length; ++index1)
      {
        byte num2 = ilAsByteArray[index1];
        if ((uint) num2 <= 140U)
        {
          switch (num2)
          {
            case 0:
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
            case 7:
            case 8:
            case 9:
            case 10:
            case 11:
            case 12:
            case 13:
            case 20:
            case 37:
            case 103:
            case 104:
            case 105:
            case 106:
            case 109:
            case 110:
              continue;
            case 14:
            case 16:
              ++index1;
              continue;
            case 21:
            case 22:
            case 23:
            case 24:
            case 25:
            case 26:
            case 27:
            case 28:
            case 29:
            case 30:
              if (index1 > 0 && (int) ilAsByteArray[index1 - 1] == 2)
              {
                num1 = (int) ilAsByteArray[index1] - 22;
                continue;
              }
              continue;
            case 31:
              if (index1 > 0 && (int) ilAsByteArray[index1 - 1] == 2)
                num1 = (int) ilAsByteArray[index1 + 1];
              ++index1;
              continue;
            case 32:
              index1 += 4;
              continue;
            case 40:
              index1 += 4;
              if (num1 >= 0)
              {
                for (int index2 = index1 + 1; index2 < ilAsByteArray.Length; ++index2)
                {
                  if ((int) ilAsByteArray[index2] == 42)
                    return num1;
                  if ((int) ilAsByteArray[index2] != 0)
                    break;
                }
              }
              num1 = -1;
              continue;
            case 44:
            case 45:
              num1 = -1;
              ++index1;
              continue;
            case 57:
            case 58:
              num1 = -1;
              index1 += 4;
              continue;
            case 140:
              break;
            default:
              goto label_25;
          }
        }
        else if ((int) num2 != 141)
        {
          if ((int) num2 != 162)
          {
            if ((int) num2 == 254)
            {
              ++index1;
              if (index1 >= ilAsByteArray.Length || (int) ilAsByteArray[index1] >= 6)
                goto label_25;
              else
                continue;
            }
            else
              goto label_25;
          }
          else
            continue;
        }
        index1 += 4;
        continue;
label_25:
        return -1;
      }
      return -1;
    }

    internal void ReportOutOfBandMessage(string msg, bool flush)
    {
      try
      {
        Debugger.Log(0, (string) null, msg + "\r\n");
        if ((int) this.m_outOfBandMessageCount < 254)
        {
          this.m_outOfBandMessageCount = (byte) ((uint) this.m_outOfBandMessageCount + 1U);
        }
        else
        {
          if ((int) this.m_outOfBandMessageCount == (int) byte.MaxValue)
            return;
          this.m_outOfBandMessageCount = byte.MaxValue;
          msg = "Reached message limit.   End of EventSource error messages.";
        }
        this.WriteEventString(EventLevel.LogAlways, -1L, msg);
        this.WriteStringToAllListeners("EventSourceMessage", msg);
      }
      catch (Exception ex)
      {
      }
    }

    private EventSourceSettings ValidateSettings(EventSourceSettings settings)
    {
      EventSourceSettings eventSourceSettings = EventSourceSettings.EtwManifestEventFormat | EventSourceSettings.EtwSelfDescribingEventFormat;
      if ((settings & eventSourceSettings) == eventSourceSettings)
        throw new ArgumentException(Environment.GetResourceString("EventSource_InvalidEventFormat"), "settings");
      if ((settings & eventSourceSettings) == EventSourceSettings.Default)
        settings |= EventSourceSettings.EtwSelfDescribingEventFormat;
      return settings;
    }

    private void ReportActivitySamplingInfo(EventListener listener, SessionMask sessions)
    {
      for (int index = 0; (long) index < 4L; ++index)
      {
        if (sessions[index])
        {
          ActivityFilter activityFilter = listener != null ? listener.m_activityFilter : this.m_etwSessionIdMap[index].m_activityFilter;
          if (activityFilter != null)
          {
            SessionMask m = new SessionMask();
            m[index] = true;
            foreach (Tuple<int, int> tuple in activityFilter.GetFilterAsTuple(this.m_guid))
              this.WriteStringToListener(listener, string.Format((IFormatProvider) CultureInfo.InvariantCulture, "Session {0}: {1} = {2}", (object) index, (object) tuple.Item1, (object) tuple.Item2), m);
            bool flag = listener == null ? this.m_activityFilteringForETWEnabled[index] : this.GetDispatcher(listener).m_activityFilteringEnabled;
            this.WriteStringToListener(listener, string.Format((IFormatProvider) CultureInfo.InvariantCulture, "Session {0}: Activity Sampling support: {1}", (object) index, flag ? (object) "enabled" : (object) "disabled"), m);
          }
        }
      }
    }

    /// <summary>使用指定的名称和默认选项而非字段来写入事件。</summary>
    /// <param name="eventName">要写入的事件的名称。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="eventName" /> is null.</exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public unsafe void Write(string eventName)
    {
      if (eventName == null)
        throw new ArgumentNullException("eventName");
      if (!this.IsEnabled())
        return;
      EventSourceOptions options = new EventSourceOptions();
      EmptyStruct data = new EmptyStruct();
      this.WriteImpl<EmptyStruct>(eventName, ref options, ref data, (Guid*) null, (Guid*) null);
    }

    /// <summary>使用指定的名称和选项而非字段来写入事件。</summary>
    /// <param name="eventName">要写入的事件的名称。</param>
    /// <param name="options">事件的级别、关键字和操作代码等选项。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="eventName" /> is null.</exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public unsafe void Write(string eventName, EventSourceOptions options)
    {
      if (eventName == null)
        throw new ArgumentNullException("eventName");
      if (!this.IsEnabled())
        return;
      EmptyStruct data = new EmptyStruct();
      this.WriteImpl<EmptyStruct>(eventName, ref options, ref data, (Guid*) null, (Guid*) null);
    }

    /// <summary>使用指定的名称和数据写入事件。</summary>
    /// <param name="eventName">事件的名称。</param>
    /// <param name="data">事件数据。此类型必须为匿名类型或以 <see cref="T:System.Diagnostics.Tracing.EventDataAttribute" /> 属性进行标记。</param>
    /// <typeparam name="T">定义事件及其关联数据的类型。此类型必须为匿名类型或以 <see cref="T:System.Diagnostics.Tracing.EventSourceAttribute" /> 属性进行标记。</typeparam>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public unsafe void Write<T>(string eventName, T data)
    {
      if (!this.IsEnabled())
        return;
      EventSourceOptions options = new EventSourceOptions();
      this.WriteImpl<T>(eventName, ref options, ref data, (Guid*) null, (Guid*) null);
    }

    /// <summary>使用指定的名称、事件数据和选项写入事件。</summary>
    /// <param name="eventName">事件的名称。</param>
    /// <param name="options">事件选项。</param>
    /// <param name="data">事件数据。此类型必须为匿名类型或以 <see cref="T:System.Diagnostics.Tracing.EventDataAttribute" /> 属性进行标记。</param>
    /// <typeparam name="T">定义事件及其关联数据的类型。此类型必须为匿名类型或以 <see cref="T:System.Diagnostics.Tracing.EventSourceAttribute" /> 属性进行标记。</typeparam>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public unsafe void Write<T>(string eventName, EventSourceOptions options, T data)
    {
      if (!this.IsEnabled())
        return;
      this.WriteImpl<T>(eventName, ref options, ref data, (Guid*) null, (Guid*) null);
    }

    /// <summary>使用指定的名称、选项和事件数据写入事件。</summary>
    /// <param name="eventName">事件的名称。</param>
    /// <param name="options">事件选项。</param>
    /// <param name="data">事件数据。此类型必须为匿名类型或以 <see cref="T:System.Diagnostics.Tracing.EventDataAttribute" /> 属性进行标记。</param>
    /// <typeparam name="T">定义事件及其关联数据的类型。此类型必须为匿名类型或以 <see cref="T:System.Diagnostics.Tracing.EventSourceAttribute" /> 属性进行标记。</typeparam>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public unsafe void Write<T>(string eventName, ref EventSourceOptions options, ref T data)
    {
      if (!this.IsEnabled())
        return;
      this.WriteImpl<T>(eventName, ref options, ref data, (Guid*) null, (Guid*) null);
    }

    /// <summary>使用指定的名称、选项、相关活动和事件数据写入事件。</summary>
    /// <param name="eventName">事件的名称。</param>
    /// <param name="options">事件选项。</param>
    /// <param name="activityId">与事件关联的活动的 ID。</param>
    /// <param name="relatedActivityId">关联活动的 ID；如果没有关联活动，则为 <see cref="F:System.Guid.Empty" />。</param>
    /// <param name="data">事件数据。此类型必须为匿名类型或以 <see cref="T:System.Diagnostics.Tracing.EventDataAttribute" /> 属性进行标记。</param>
    /// <typeparam name="T">定义事件及其关联数据的类型。此类型必须为匿名类型或以 <see cref="T:System.Diagnostics.Tracing.EventSourceAttribute" /> 属性进行标记。</typeparam>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public unsafe void Write<T>(string eventName, ref EventSourceOptions options, ref Guid activityId, ref Guid relatedActivityId, ref T data)
    {
      if (!this.IsEnabled())
        return;
      fixed (Guid* pActivityId = &activityId)
        fixed (Guid* guidPtr = &relatedActivityId)
          this.WriteImpl<T>(eventName, ref options, ref data, pActivityId, relatedActivityId == Guid.Empty ? (Guid*) null : guidPtr);
    }

    [SecuritySafeCritical]
    private unsafe void WriteMultiMerge(string eventName, ref EventSourceOptions options, TraceLoggingEventTypes eventTypes, Guid* activityID, Guid* childActivityID, params object[] values)
    {
      if (!this.IsEnabled() || !this.IsEnabled(((int) options.valuesSet & 4) != 0 ? (EventLevel) options.level : (EventLevel) eventTypes.level, ((int) options.valuesSet & 1) != 0 ? options.keywords : eventTypes.keywords))
        return;
      this.WriteMultiMergeInner(eventName, ref options, eventTypes, activityID, childActivityID, values);
    }

    [SecuritySafeCritical]
    private unsafe void WriteMultiMergeInner(string eventName, ref EventSourceOptions options, TraceLoggingEventTypes eventTypes, Guid* activityID, Guid* childActivityID, params object[] values)
    {
      byte level = ((int) options.valuesSet & 4) != 0 ? options.level : eventTypes.level;
      byte opcode = ((int) options.valuesSet & 8) != 0 ? options.opcode : eventTypes.opcode;
      EventTags tags = ((int) options.valuesSet & 2) != 0 ? options.tags : eventTypes.Tags;
      EventKeywords eventKeywords = ((int) options.valuesSet & 1) != 0 ? options.keywords : eventTypes.keywords;
      NameInfo nameInfo = eventTypes.GetNameInfo(eventName ?? eventTypes.Name, tags);
      if (nameInfo == null)
        return;
      EventDescriptor eventDescriptor = new EventDescriptor(nameInfo.identity, level, opcode, (long) eventKeywords);
      int num = eventTypes.pinCount;
      byte* scratch = stackalloc byte[eventTypes.scratchSize];
      EventSource.EventData* eventDataPtr = stackalloc EventSource.EventData[eventTypes.dataCount + 3];
      GCHandle* gcHandlePtr = stackalloc GCHandle[num];
      fixed (byte* pointer1 = this.providerMetadata)
        fixed (byte* pointer2 = nameInfo.nameMetadata)
          fixed (byte* pointer3 = eventTypes.typeMetadata)
          {
            eventDataPtr->SetMetadata(pointer1, this.providerMetadata.Length, 2);
            eventDataPtr[1].SetMetadata(pointer2, nameInfo.nameMetadata.Length, 1);
            eventDataPtr[2].SetMetadata(pointer3, eventTypes.typeMetadata.Length, 1);
            RuntimeHelpers.PrepareConstrainedRegions();
            try
            {
              DataCollector.ThreadInstance.Enable(scratch, eventTypes.scratchSize, eventDataPtr + 3, eventTypes.dataCount, gcHandlePtr, num);
              for (int index = 0; index < eventTypes.typeInfos.Length; ++index)
                eventTypes.typeInfos[index].WriteObjectData(TraceLoggingDataCollector.Instance, values[index]);
              this.WriteEventRaw(ref eventDescriptor, activityID, childActivityID, (int) (DataCollector.ThreadInstance.Finish() - eventDataPtr), (IntPtr) ((void*) eventDataPtr));
            }
            finally
            {
              this.WriteCleanup(gcHandlePtr, num);
            }
          }
    }

    [SecuritySafeCritical]
    internal unsafe void WriteMultiMerge(string eventName, ref EventSourceOptions options, TraceLoggingEventTypes eventTypes, Guid* activityID, Guid* childActivityID, EventSource.EventData* data)
    {
      if (!this.IsEnabled())
        return;
      fixed (EventSourceOptions* eventSourceOptionsPtr = &options)
      {
        EventDescriptor descriptor;
        NameInfo nameInfo = this.UpdateDescriptor(eventName, eventTypes, ref options, out descriptor);
        if (nameInfo == null)
          return;
        EventSource.EventData* eventDataPtr = stackalloc EventSource.EventData[eventTypes.dataCount + eventTypes.typeInfos.Length * 2 + 3];
        fixed (byte* pointer1 = this.providerMetadata)
          fixed (byte* pointer2 = nameInfo.nameMetadata)
            fixed (byte* pointer3 = eventTypes.typeMetadata)
            {
              eventDataPtr->SetMetadata(pointer1, this.providerMetadata.Length, 2);
              eventDataPtr[1].SetMetadata(pointer2, nameInfo.nameMetadata.Length, 1);
              eventDataPtr[2].SetMetadata(pointer3, eventTypes.typeMetadata.Length, 1);
              int dataCount = 3;
              for (int index1 = 0; index1 < eventTypes.typeInfos.Length; ++index1)
              {
                if (eventTypes.typeInfos[index1].DataType == typeof (string))
                {
                  eventDataPtr[dataCount].m_Ptr = (long) &eventDataPtr[dataCount + 1].m_Size;
                  eventDataPtr[dataCount].m_Size = 2;
                  int index2 = dataCount + 1;
                  eventDataPtr[index2].m_Ptr = data[index1].m_Ptr;
                  eventDataPtr[index2].m_Size = data[index1].m_Size - 2;
                  dataCount = index2 + 1;
                }
                else
                {
                  eventDataPtr[dataCount].m_Ptr = data[index1].m_Ptr;
                  eventDataPtr[dataCount].m_Size = data[index1].m_Size;
                  if (data[index1].m_Size == 4 && eventTypes.typeInfos[index1].DataType == typeof (bool))
                    eventDataPtr[dataCount].m_Size = 1;
                  ++dataCount;
                }
              }
              this.WriteEventRaw(ref descriptor, activityID, childActivityID, dataCount, (IntPtr) ((void*) eventDataPtr));
            }
      }
    }

    [SecuritySafeCritical]
    private unsafe void WriteImpl<T>(string eventName, ref EventSourceOptions options, ref T data, Guid* pActivityId, Guid* pRelatedActivityId)
    {
      try
      {
        SimpleEventTypes<T> instance = SimpleEventTypes<T>.Instance;
        fixed (EventSourceOptions* eventSourceOptionsPtr = &options)
        {
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          EventSourceOptions& local = @options;
          // ISSUE: explicit reference operation
          int num1 = (^local).IsOpcodeSet ? (int) options.Opcode : (int) EventSource.GetOpcodeWithDefault(options.Opcode, eventName);
          // ISSUE: explicit reference operation
          (^local).Opcode = (EventOpcode) num1;
          EventDescriptor descriptor;
          NameInfo nameInfo = this.UpdateDescriptor(eventName, (TraceLoggingEventTypes) instance, ref options, out descriptor);
          if (nameInfo == null)
            return;
          int num2 = instance.pinCount;
          byte* scratch = stackalloc byte[instance.scratchSize];
          EventSource.EventData* eventDataPtr = stackalloc EventSource.EventData[instance.dataCount + 3];
          GCHandle* gcHandlePtr = stackalloc GCHandle[num2];
          fixed (byte* pointer1 = this.providerMetadata)
            fixed (byte* pointer2 = nameInfo.nameMetadata)
              fixed (byte* pointer3 = instance.typeMetadata)
              {
                eventDataPtr->SetMetadata(pointer1, this.providerMetadata.Length, 2);
                eventDataPtr[1].SetMetadata(pointer2, nameInfo.nameMetadata.Length, 1);
                eventDataPtr[2].SetMetadata(pointer3, instance.typeMetadata.Length, 1);
                RuntimeHelpers.PrepareConstrainedRegions();
                EventOpcode eventOpcode = (EventOpcode) descriptor.Opcode;
                Guid activityId = Guid.Empty;
                Guid relatedActivityId = Guid.Empty;
                if ((IntPtr) pActivityId == IntPtr.Zero && (IntPtr) pRelatedActivityId == IntPtr.Zero && (options.ActivityOptions & EventActivityOptions.Disable) == EventActivityOptions.None)
                {
                  if (eventOpcode == EventOpcode.Start)
                    this.m_activityTracker.OnStart(this.m_name, eventName, 0, ref activityId, ref relatedActivityId, options.ActivityOptions);
                  else if (eventOpcode == EventOpcode.Stop)
                    this.m_activityTracker.OnStop(this.m_name, eventName, 0, ref activityId);
                  if (activityId != Guid.Empty)
                    pActivityId = &activityId;
                  if (relatedActivityId != Guid.Empty)
                    pRelatedActivityId = &relatedActivityId;
                }
                try
                {
                  DataCollector.ThreadInstance.Enable(scratch, instance.scratchSize, eventDataPtr + 3, instance.dataCount, gcHandlePtr, num2);
                  instance.typeInfo.WriteData(TraceLoggingDataCollector.Instance, ref data);
                  this.WriteEventRaw(ref descriptor, pActivityId, pRelatedActivityId, (int) (DataCollector.ThreadInstance.Finish() - eventDataPtr), (IntPtr) ((void*) eventDataPtr));
                  if (this.m_Dispatchers == null)
                    return;
                  EventPayload payload = (EventPayload) instance.typeInfo.GetData((object) data);
                  this.WriteToAllListeners(eventName, ref descriptor, nameInfo.tags, pActivityId, payload);
                }
                catch (Exception ex)
                {
                  if (ex is EventSourceException)
                    throw;
                  else
                    this.ThrowEventSourceException(ex);
                }
                finally
                {
                  this.WriteCleanup(gcHandlePtr, num2);
                }
              }
        }
      }
      catch (Exception ex)
      {
        if (ex is EventSourceException)
          throw;
        else
          this.ThrowEventSourceException(ex);
      }
    }

    [SecurityCritical]
    private unsafe void WriteToAllListeners(string eventName, ref EventDescriptor eventDescriptor, EventTags tags, Guid* pActivityId, EventPayload payload)
    {
      EventWrittenEventArgs eventCallbackArgs = new EventWrittenEventArgs(this);
      eventCallbackArgs.EventName = eventName;
      eventCallbackArgs.m_keywords = (EventKeywords) eventDescriptor.Keywords;
      eventCallbackArgs.m_opcode = (EventOpcode) eventDescriptor.Opcode;
      eventCallbackArgs.m_tags = tags;
      eventCallbackArgs.EventId = -1;
      if ((IntPtr) pActivityId != IntPtr.Zero)
        eventCallbackArgs.RelatedActivityId = *pActivityId;
      if (payload != null)
      {
        eventCallbackArgs.Payload = new ReadOnlyCollection<object>((IList<object>) payload.Values);
        eventCallbackArgs.PayloadNames = new ReadOnlyCollection<string>((IList<string>) payload.Keys);
      }
      this.DisptachToAllListeners(-1, pActivityId, eventCallbackArgs);
    }

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [SecurityCritical]
    [NonEvent]
    private unsafe void WriteCleanup(GCHandle* pPins, int cPins)
    {
      DataCollector.ThreadInstance.Disable();
      for (int index = 0; index != cPins; ++index)
      {
        if (IntPtr.Zero != (IntPtr) pPins[index])
          pPins[index].Free();
      }
    }

    private void InitializeProviderMetadata()
    {
      if (this.m_traits != null)
      {
        List<byte> metaData = new List<byte>(100);
        int index = 0;
        while (index < this.m_traits.Length - 1)
        {
          if (this.m_traits[index].StartsWith("ETW_"))
          {
            string s = this.m_traits[index].Substring(4);
            byte result;
            if (!byte.TryParse(s, out result))
            {
              if (s == "GROUP")
                result = (byte) 1;
              else
                throw new ArgumentException(Environment.GetResourceString("UnknownEtwTrait", (object) s), "traits");
            }
            string str = this.m_traits[index + 1];
            int count = metaData.Count;
            metaData.Add((byte) 0);
            metaData.Add((byte) 0);
            metaData.Add(result);
            int num = EventSource.AddValueToMetaData(metaData, str) + 3;
            metaData[count] = (byte) num;
            metaData[count + 1] = (byte) (num >> 8);
          }
          index += 2;
        }
        this.providerMetadata = Statics.MetadataForString(this.Name, 0, metaData.Count, 0);
        int num1 = this.providerMetadata.Length - metaData.Count;
        foreach (byte num2 in metaData)
          this.providerMetadata[num1++] = num2;
      }
      else
        this.providerMetadata = Statics.MetadataForString(this.Name, 0, 0, 0);
    }

    private static int AddValueToMetaData(List<byte> metaData, string value)
    {
      if (value.Length == 0)
        return 0;
      int count = metaData.Count;
      char ch = value[0];
      switch (ch)
      {
        case '@':
          metaData.AddRange((IEnumerable<byte>) Encoding.UTF8.GetBytes(value.Substring(1)));
          break;
        case '{':
          metaData.AddRange((IEnumerable<byte>) new Guid(value).ToByteArray());
          break;
        case '#':
          for (int index = 1; index < value.Length; ++index)
          {
            if ((int) value[index] != 32)
            {
              if (index + 1 >= value.Length)
                throw new ArgumentException(Environment.GetResourceString("EvenHexDigits"), "traits");
              metaData.Add((byte) (EventSource.HexDigit(value[index]) * 16 + EventSource.HexDigit(value[index + 1])));
              ++index;
            }
          }
          break;
        default:
          if (32 <= (int) ch)
          {
            metaData.AddRange((IEnumerable<byte>) Encoding.UTF8.GetBytes(value));
            break;
          }
          throw new ArgumentException(Environment.GetResourceString("IllegalValue", (object) value), "traits");
      }
      return metaData.Count - count;
    }

    private static int HexDigit(char c)
    {
      if (48 <= (int) c && (int) c <= 57)
        return (int) c - 48;
      if (97 <= (int) c)
        c -= ' ';
      if (65 <= (int) c && (int) c <= 70)
        return (int) c - 65 + 10;
      throw new ArgumentException(Environment.GetResourceString("BadHexDigit", (object) c), "traits");
    }

    private NameInfo UpdateDescriptor(string name, TraceLoggingEventTypes eventInfo, ref EventSourceOptions options, out EventDescriptor descriptor)
    {
      NameInfo nameInfo = (NameInfo) null;
      int traceloggingId = 0;
      byte level = ((int) options.valuesSet & 4) != 0 ? options.level : eventInfo.level;
      byte opcode = ((int) options.valuesSet & 8) != 0 ? options.opcode : eventInfo.opcode;
      EventTags tags = ((int) options.valuesSet & 2) != 0 ? options.tags : eventInfo.Tags;
      EventKeywords keywords = ((int) options.valuesSet & 1) != 0 ? options.keywords : eventInfo.keywords;
      if (this.IsEnabled((EventLevel) level, keywords))
      {
        nameInfo = eventInfo.GetNameInfo(name ?? eventInfo.Name, tags);
        traceloggingId = nameInfo.identity;
      }
      descriptor = new EventDescriptor(traceloggingId, level, opcode, (long) keywords);
      return nameInfo;
    }

    /// <summary>提供事件数据用于创建快速 <see cref="Overload:System.Diagnostics.Tracing.EventSource.WriteEvent" /> 过载，方法是使用 <see cref="M:System.Diagnostics.Tracing.EventSource.WriteEventCore(System.Int32,System.Int32,System.Diagnostics.Tracing.EventSource.EventData*)" /> 方法。</summary>
    [__DynamicallyInvokable]
    protected internal struct EventData
    {
      internal long m_Ptr;
      internal int m_Size;
      internal int m_Reserved;

      /// <summary>获取或设置新的 <see cref="Overload:System.Diagnostics.Tracing.EventSource.WriteEvent" /> 重载的数据的指针。</summary>
      /// <returns>数据的指针。</returns>
      [__DynamicallyInvokable]
      public IntPtr DataPointer
      {
        get
        {
          return (IntPtr) this.m_Ptr;
        }
        set
        {
          this.m_Ptr = (long) value;
        }
      }

      /// <summary>获取或设置新的 <see cref="Overload:System.Diagnostics.Tracing.EventSource.WriteEvent" /> 重载中的项目的负载数量。</summary>
      /// <returns>在新的重载中的负载项的数目。</returns>
      [__DynamicallyInvokable]
      public int Size
      {
        [__DynamicallyInvokable] get
        {
          return this.m_Size;
        }
        [__DynamicallyInvokable] set
        {
          this.m_Size = value;
        }
      }

      [SecurityCritical]
      internal unsafe void SetMetadata(byte* pointer, int size, int reserved)
      {
        this.m_Ptr = (long) (ulong) (UIntPtr) ((void*) pointer);
        this.m_Size = size;
        this.m_Reserved = reserved;
      }
    }

    private struct Sha1ForNonSecretPurposes
    {
      private long length;
      private uint[] w;
      private int pos;

      public void Start()
      {
        if (this.w == null)
          this.w = new uint[85];
        this.length = 0L;
        this.pos = 0;
        this.w[80] = 1732584193U;
        this.w[81] = 4023233417U;
        this.w[82] = 2562383102U;
        this.w[83] = 271733878U;
        this.w[84] = 3285377520U;
      }

      public void Append(byte input)
      {
        this.w[this.pos / 4] = this.w[this.pos / 4] << 8 | (uint) input;
        int num1 = 64;
        int num2 = this.pos + 1;
        this.pos = num2;
        int num3 = num2;
        if (num1 != num3)
          return;
        this.Drain();
      }

      public void Append(byte[] input)
      {
        foreach (byte input1 in input)
          this.Append(input1);
      }

      public void Finish(byte[] output)
      {
        long num1 = this.length + (long) (8 * this.pos);
        this.Append((byte) 128);
        while (this.pos != 56)
          this.Append((byte) 0);
        this.Append((byte) (num1 >> 56));
        this.Append((byte) (num1 >> 48));
        this.Append((byte) (num1 >> 40));
        this.Append((byte) (num1 >> 32));
        this.Append((byte) (num1 >> 24));
        this.Append((byte) (num1 >> 16));
        this.Append((byte) (num1 >> 8));
        this.Append((byte) num1);
        int num2 = output.Length < 20 ? output.Length : 20;
        for (int index = 0; index != num2; ++index)
        {
          uint num3 = this.w[80 + index / 4];
          output[index] = (byte) (num3 >> 24);
          this.w[80 + index / 4] = num3 << 8;
        }
      }

      private void Drain()
      {
        for (int index = 16; index != 80; ++index)
          this.w[index] = EventSource.Sha1ForNonSecretPurposes.Rol1(this.w[index - 3] ^ this.w[index - 8] ^ this.w[index - 14] ^ this.w[index - 16]);
        uint input1 = this.w[80];
        uint input2 = this.w[81];
        uint num1 = this.w[82];
        uint num2 = this.w[83];
        uint num3 = this.w[84];
        for (int index = 0; index != 20; ++index)
        {
          uint num4 = (uint) ((int) input2 & (int) num1 | ~(int) input2 & (int) num2);
          int num5 = (int) EventSource.Sha1ForNonSecretPurposes.Rol5(input1) + (int) num4 + (int) num3 + 1518500249 + (int) this.w[index];
          num3 = num2;
          num2 = num1;
          num1 = EventSource.Sha1ForNonSecretPurposes.Rol30(input2);
          input2 = input1;
          input1 = (uint) num5;
        }
        for (int index = 20; index != 40; ++index)
        {
          uint num4 = input2 ^ num1 ^ num2;
          int num5 = (int) EventSource.Sha1ForNonSecretPurposes.Rol5(input1) + (int) num4 + (int) num3 + 1859775393 + (int) this.w[index];
          num3 = num2;
          num2 = num1;
          num1 = EventSource.Sha1ForNonSecretPurposes.Rol30(input2);
          input2 = input1;
          input1 = (uint) num5;
        }
        for (int index = 40; index != 60; ++index)
        {
          uint num4 = (uint) ((int) input2 & (int) num1 | (int) input2 & (int) num2 | (int) num1 & (int) num2);
          int num5 = (int) EventSource.Sha1ForNonSecretPurposes.Rol5(input1) + (int) num4 + (int) num3 - 1894007588 + (int) this.w[index];
          num3 = num2;
          num2 = num1;
          num1 = EventSource.Sha1ForNonSecretPurposes.Rol30(input2);
          input2 = input1;
          input1 = (uint) num5;
        }
        for (int index = 60; index != 80; ++index)
        {
          uint num4 = input2 ^ num1 ^ num2;
          int num5 = (int) EventSource.Sha1ForNonSecretPurposes.Rol5(input1) + (int) num4 + (int) num3 - 899497514 + (int) this.w[index];
          num3 = num2;
          num2 = num1;
          num1 = EventSource.Sha1ForNonSecretPurposes.Rol30(input2);
          input2 = input1;
          input1 = (uint) num5;
        }
        this.w[80] += input1;
        this.w[81] += input2;
        this.w[82] += num1;
        this.w[83] += num2;
        this.w[84] += num3;
        this.length = this.length + 512L;
        this.pos = 0;
      }

      private static uint Rol1(uint input)
      {
        return input << 1 | input >> 31;
      }

      private static uint Rol5(uint input)
      {
        return input << 5 | input >> 27;
      }

      private static uint Rol30(uint input)
      {
        return input << 30 | input >> 2;
      }
    }

    private class OverideEventProvider : EventProvider
    {
      private EventSource m_eventSource;

      public OverideEventProvider(EventSource eventSource)
      {
        this.m_eventSource = eventSource;
      }

      protected override void OnControllerCommand(ControllerCommand command, IDictionary<string, string> arguments, int perEventSourceSessionId, int etwSessionId)
      {
        this.m_eventSource.SendCommand((EventListener) null, perEventSourceSessionId, etwSessionId, (EventCommand) command, this.IsEnabled(), this.Level, this.MatchAnyKeyword, arguments);
      }
    }

    internal struct EventMetadata
    {
      public EventDescriptor Descriptor;
      public EventTags Tags;
      public bool EnabledForAnyListener;
      public bool EnabledForETW;
      public byte TriggersActivityTracking;
      public string Name;
      public string Message;
      public ParameterInfo[] Parameters;
      public TraceLoggingEventTypes TraceLoggingEventTypes;
      public EventActivityOptions ActivityOptions;
    }
  }
}
