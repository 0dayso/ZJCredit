// Decompiled with JetBrains decompiler
// Type: System.Runtime.Hosting.ActivationArguments
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security.Policy;

namespace System.Runtime.Hosting
{
  /// <summary>为应用程序的基于清单的激活提供数据。此类不能被继承。</summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class ActivationArguments : EvidenceBase
  {
    private bool m_useFusionActivationContext;
    private bool m_activateInstance;
    private string m_appFullName;
    private string[] m_appManifestPaths;
    private string[] m_activationData;

    internal bool UseFusionActivationContext
    {
      get
      {
        return this.m_useFusionActivationContext;
      }
    }

    internal bool ActivateInstance
    {
      get
      {
        return this.m_activateInstance;
      }
      set
      {
        this.m_activateInstance = value;
      }
    }

    internal string ApplicationFullName
    {
      get
      {
        return this.m_appFullName;
      }
    }

    internal string[] ApplicationManifestPaths
    {
      get
      {
        return this.m_appManifestPaths;
      }
    }

    /// <summary>为清单激活的应用程序获取应用程序标识。</summary>
    /// <returns>一个对象，标识基于清单的激活应用程序。</returns>
    public ApplicationIdentity ApplicationIdentity
    {
      get
      {
        return new ApplicationIdentity(this.m_appFullName);
      }
    }

    /// <summary>为应用程序获取基于清单的激活的激活上下文。</summary>
    /// <returns>一个对象，标识基于清单的激活应用程序。</returns>
    public ActivationContext ActivationContext
    {
      get
      {
        if (!this.UseFusionActivationContext)
          return (ActivationContext) null;
        if (this.m_appManifestPaths == null)
          return new ActivationContext(new ApplicationIdentity(this.m_appFullName));
        return new ActivationContext(new ApplicationIdentity(this.m_appFullName), this.m_appManifestPaths);
      }
    }

    /// <summary>从宿主获取激活数据。</summary>
    /// <returns>包含宿主提供的激活数据的字符串数组。</returns>
    public string[] ActivationData
    {
      get
      {
        return this.m_activationData;
      }
    }

    private ActivationArguments()
    {
    }

    /// <summary>使用指定的应用程序标识初始化 <see cref="T:System.Runtime.Hosting.ActivationArguments" /> 类的新实例。</summary>
    /// <param name="applicationIdentity">一个对象，标识基于清单的激活应用程序。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="applicationIdentity" /> 为 null。</exception>
    public ActivationArguments(ApplicationIdentity applicationIdentity)
      : this(applicationIdentity, (string[]) null)
    {
    }

    /// <summary>用指定的应用程序标识和激活数据初始化 <see cref="T:System.Runtime.Hosting.ActivationArguments" /> 类的新实例。</summary>
    /// <param name="applicationIdentity">一个对象，标识基于清单的激活应用程序。</param>
    /// <param name="activationData">包含宿主提供的激活数据的字符串数组。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="applicationIdentity" /> 为 null。</exception>
    public ActivationArguments(ApplicationIdentity applicationIdentity, string[] activationData)
    {
      if (applicationIdentity == null)
        throw new ArgumentNullException("applicationIdentity");
      this.m_appFullName = applicationIdentity.FullName;
      this.m_activationData = activationData;
    }

    /// <summary>用指定的激活上下文初始化 <see cref="T:System.Runtime.Hosting.ActivationArguments" /> 类的新实例。</summary>
    /// <param name="activationData">一个对象，标识基于清单的激活应用程序。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="activationData" /> 为 null。</exception>
    public ActivationArguments(ActivationContext activationData)
      : this(activationData, (string[]) null)
    {
    }

    /// <summary>用指定的激活上下文和激活数据初始化 <see cref="T:System.Runtime.Hosting.ActivationArguments" /> 类的新实例。</summary>
    /// <param name="activationContext">一个对象，标识基于清单的激活应用程序。</param>
    /// <param name="activationData">包含宿主提供的激活数据的字符串数组。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="activationContext" /> 为 null。</exception>
    public ActivationArguments(ActivationContext activationContext, string[] activationData)
    {
      if (activationContext == null)
        throw new ArgumentNullException("activationContext");
      this.m_appFullName = activationContext.Identity.FullName;
      this.m_appManifestPaths = activationContext.ManifestPaths;
      this.m_activationData = activationData;
      this.m_useFusionActivationContext = true;
    }

    internal ActivationArguments(string appFullName, string[] appManifestPaths, string[] activationData)
    {
      if (appFullName == null)
        throw new ArgumentNullException("appFullName");
      this.m_appFullName = appFullName;
      this.m_appManifestPaths = appManifestPaths;
      this.m_activationData = activationData;
      this.m_useFusionActivationContext = true;
    }

    /// <summary>生成当前 <see cref="T:System.Runtime.Hosting.ActivationArguments" /> 对象的副本。</summary>
    /// <returns>当前对象的副本。</returns>
    public override EvidenceBase Clone()
    {
      ActivationArguments activationArguments = new ActivationArguments();
      activationArguments.m_useFusionActivationContext = this.m_useFusionActivationContext;
      activationArguments.m_activateInstance = this.m_activateInstance;
      activationArguments.m_appFullName = this.m_appFullName;
      if (this.m_appManifestPaths != null)
      {
        activationArguments.m_appManifestPaths = new string[this.m_appManifestPaths.Length];
        Array.Copy((Array) this.m_appManifestPaths, (Array) activationArguments.m_appManifestPaths, activationArguments.m_appManifestPaths.Length);
      }
      if (this.m_activationData != null)
      {
        activationArguments.m_activationData = new string[this.m_activationData.Length];
        Array.Copy((Array) this.m_activationData, (Array) activationArguments.m_activationData, activationArguments.m_activationData.Length);
      }
      activationArguments.m_activateInstance = this.m_activateInstance;
      activationArguments.m_appFullName = this.m_appFullName;
      activationArguments.m_useFusionActivationContext = this.m_useFusionActivationContext;
      return (EvidenceBase) activationArguments;
    }
  }
}
