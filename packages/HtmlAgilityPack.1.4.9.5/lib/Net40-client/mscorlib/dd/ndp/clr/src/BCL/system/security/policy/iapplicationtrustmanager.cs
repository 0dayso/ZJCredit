// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.TrustManagerContext
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Policy
{
  /// <summary>表示作出决定以运行应用程序时和为新的 <see cref="T:System.AppDomain" />（要在其中运行应用程序）建立安全时，信任关系管理器要考虑的上下文。</summary>
  [ComVisible(true)]
  public class TrustManagerContext
  {
    private bool m_ignorePersistedDecision;
    private TrustManagerUIContext m_uiContext;
    private bool m_noPrompt;
    private bool m_keepAlive;
    private bool m_persist;
    private ApplicationIdentity m_appId;

    /// <summary>获取或设置信任关系管理器应显示的用户界面类型。</summary>
    /// <returns>
    /// <see cref="T:System.Security.Policy.TrustManagerUIContext" /> 值之一。默认值为 <see cref="F:System.Security.Policy.TrustManagerUIContext.Run" />。</returns>
    public virtual TrustManagerUIContext UIContext
    {
      get
      {
        return this.m_uiContext;
      }
      set
      {
        this.m_uiContext = value;
      }
    }

    /// <summary>获取或设置一个值，该值指示信任关系管理器是否应提示用户以获取信任决定。</summary>
    /// <returns>如果不提示用户，则为 true；如果提示用户，则为 false。默认值为 false。</returns>
    public virtual bool NoPrompt
    {
      get
      {
        return this.m_noPrompt;
      }
      set
      {
        this.m_noPrompt = value;
      }
    }

    /// <summary>获取或设置一个值，该值指示应用程序安全管理器是否应忽略任何保留的决定并调用信任关系管理器。</summary>
    /// <returns>如果调用信任关系管理器，则为 true；否则为 false。</returns>
    public virtual bool IgnorePersistedDecision
    {
      get
      {
        return this.m_ignorePersistedDecision;
      }
      set
      {
        this.m_ignorePersistedDecision = value;
      }
    }

    /// <summary>获取或设置一个值，该值指示信任关系管理器是否应为此应用程序缓存状态，以便于将来的请求确定应用程序信任。</summary>
    /// <returns>如果缓存状态数据，则为 true；否则为 false。默认值为 false。</returns>
    public virtual bool KeepAlive
    {
      get
      {
        return this.m_keepAlive;
      }
      set
      {
        this.m_keepAlive = value;
      }
    }

    /// <summary>获取或设置一个值，该值指示是否应保留用户对同意对话框的响应。</summary>
    /// <returns>如果缓存状态数据，则为 true；否则为 false。默认值为 true。</returns>
    public virtual bool Persist
    {
      get
      {
        return this.m_persist;
      }
      set
      {
        this.m_persist = value;
      }
    }

    /// <summary>获取或设置上一个应用程序标识的标识。</summary>
    /// <returns>表示上一个 <see cref="T:System.ApplicationIdentity" /> 的 <see cref="T:System.ApplicationIdentity" /> 对象。</returns>
    public virtual ApplicationIdentity PreviousApplicationIdentity
    {
      get
      {
        return this.m_appId;
      }
      set
      {
        this.m_appId = value;
      }
    }

    /// <summary>初始化 <see cref="T:System.Security.Policy.TrustManagerContext" /> 类的新实例。</summary>
    public TrustManagerContext()
      : this(TrustManagerUIContext.Run)
    {
    }

    /// <summary>使用指定的 <see cref="T:System.Security.Policy.TrustManagerUIContext" /> 对象初始化 <see cref="T:System.Security.Policy.TrustManagerContext" /> 类的新实例。</summary>
    /// <param name="uiContext">
    /// <see cref="T:System.Security.Policy.TrustManagerUIContext" /> 值之一，该值指定要使用的信任关系管理器用户界面的类型。</param>
    public TrustManagerContext(TrustManagerUIContext uiContext)
    {
      this.m_ignorePersistedDecision = false;
      this.m_uiContext = uiContext;
      this.m_keepAlive = false;
      this.m_persist = true;
    }
  }
}
