// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.EventRegistrationTokenTable`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Runtime.InteropServices.WindowsRuntime
{
  /// <summary>储存在委托和事件象征之间的影射，托管代码中支持 Windows 运行时 事件的实现。</summary>
  /// <typeparam name="T">一个特殊事件的事件处理程序委托的类型。</typeparam>
  [__DynamicallyInvokable]
  public sealed class EventRegistrationTokenTable<T> where T : class
  {
    private Dictionary<EventRegistrationToken, T> m_tokens = new Dictionary<EventRegistrationToken, T>();
    private volatile T m_invokeList;

    /// <summary>获取或设置类型 <paramref name="T" /> 的委托，它的调用列表包括所有已被添加而尚未删除的事件处理程序委托。调用委托调用所有的事件处理程序。</summary>
    /// <returns>表示所有当前注册事件的事件处理程序的类型 <paramref name="T" /> 的委托。</returns>
    [__DynamicallyInvokable]
    public T InvocationList
    {
      [__DynamicallyInvokable] get
      {
        return this.m_invokeList;
      }
      [__DynamicallyInvokable] set
      {
        lock (this.m_tokens)
        {
          this.m_tokens.Clear();
          this.m_invokeList = default (T);
          if ((object) value == null)
            return;
          this.AddEventHandlerNoLock(value);
        }
      }
    }

    /// <summary>初始化 <see cref="T:System.Runtime.InteropServices.WindowsRuntime.EventRegistrationTokenTable`1" /> 类的新实例。</summary>
    /// <exception cref="T:System.InvalidOperationException">
    /// <paramref name="T" /> 不是委托类型。</exception>
    [__DynamicallyInvokable]
    public EventRegistrationTokenTable()
    {
      if (!typeof (Delegate).IsAssignableFrom(typeof (T)))
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EventTokenTableRequiresDelegate", (object) typeof (T)));
    }

    /// <summary>添加指定的事件处理程序到该表和调用列表，并返回可用于移除该事件处理程序的标志。</summary>
    /// <returns>可用于将事件处理程序从表和调用列表中移除的标志。</returns>
    /// <param name="handler">要添加的事件处理程序。</param>
    [__DynamicallyInvokable]
    public EventRegistrationToken AddEventHandler(T handler)
    {
      if ((object) handler == null)
        return new EventRegistrationToken(0UL);
      lock (this.m_tokens)
        return this.AddEventHandlerNoLock(handler);
    }

    private EventRegistrationToken AddEventHandlerNoLock(T handler)
    {
      EventRegistrationToken key = EventRegistrationTokenTable<T>.GetPreferredToken(handler);
      while (this.m_tokens.ContainsKey(key))
        key = new EventRegistrationToken(key.Value + 1UL);
      this.m_tokens[key] = handler;
      this.m_invokeList = (T) Delegate.Combine((Delegate) (object) this.m_invokeList, (Delegate) (object) handler);
      return key;
    }

    [FriendAccessAllowed]
    internal T ExtractHandler(EventRegistrationToken token)
    {
      T obj = default (T);
      lock (this.m_tokens)
      {
        if (this.m_tokens.TryGetValue(token, out obj))
          this.RemoveEventHandlerNoLock(token);
      }
      return obj;
    }

    private static EventRegistrationToken GetPreferredToken(T handler)
    {
      Delegate[] invocationList = ((Delegate) (object) handler).GetInvocationList();
      return new EventRegistrationToken((ulong) (uint) typeof (T).MetadataToken << 32 | (invocationList.Length != 1 ? (ulong) (uint) handler.GetHashCode() : (ulong) (uint) invocationList[0].Method.GetHashCode()));
    }

    /// <summary>移除事件处理程序，其与表中和调用表中指定标记关联。</summary>
    /// <param name="token">添加了事件处理程序，返回标记。</param>
    [__DynamicallyInvokable]
    public void RemoveEventHandler(EventRegistrationToken token)
    {
      if ((long) token.Value == 0L)
        return;
      lock (this.m_tokens)
        this.RemoveEventHandlerNoLock(token);
    }

    /// <summary>从表格和调用列表移除指定的事件处理程序委托。</summary>
    /// <param name="handler">要移除的事件处理程序。</param>
    [__DynamicallyInvokable]
    public void RemoveEventHandler(T handler)
    {
      if ((object) handler == null)
        return;
      lock (this.m_tokens)
      {
        EventRegistrationToken local_2 = EventRegistrationTokenTable<T>.GetPreferredToken(handler);
        T local_3;
        if (this.m_tokens.TryGetValue(local_2, out local_3) && (object) local_3 == (object) handler)
        {
          this.RemoveEventHandlerNoLock(local_2);
        }
        else
        {
          foreach (KeyValuePair<EventRegistrationToken, T> item_0 in this.m_tokens)
          {
            if ((object) item_0.Value == (object) handler)
            {
              this.RemoveEventHandlerNoLock(item_0.Key);
              break;
            }
          }
        }
      }
    }

    private void RemoveEventHandlerNoLock(EventRegistrationToken token)
    {
      T obj;
      if (!this.m_tokens.TryGetValue(token, out obj))
        return;
      this.m_tokens.Remove(token);
      this.m_invokeList = (T) Delegate.Remove((Delegate) (object) this.m_invokeList, (Delegate) (object) obj);
    }

    /// <summary>如果不是 null 则返回指定的事件注册标记表；否则返回新的事件注册标记表。</summary>
    /// <returns>如果不为 null，则是由 <paramref name="refEventTable" />指定的事件注册标记表；否则为新的事件注册标记表。</returns>
    /// <param name="refEventTable">事件注册标记表，由引用传递。</param>
    [__DynamicallyInvokable]
    public static EventRegistrationTokenTable<T> GetOrCreateEventRegistrationTokenTable(ref EventRegistrationTokenTable<T> refEventTable)
    {
      if (refEventTable == null)
        Interlocked.CompareExchange<EventRegistrationTokenTable<T>>(ref refEventTable, new EventRegistrationTokenTable<T>(), (EventRegistrationTokenTable<T>) null);
      return refEventTable;
    }
  }
}
