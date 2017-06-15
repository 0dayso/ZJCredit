// Decompiled with JetBrains decompiler
// Type: System.ActivationContext
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Deployment.Internal.Isolation;
using System.Deployment.Internal.Isolation.Manifest;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
  /// <summary>标识当前应用程序的激活上下文。此类不能被继承。</summary>
  /// <filterpriority>1</filterpriority>
  [ComVisible(false)]
  [Serializable]
  public sealed class ActivationContext : IDisposable, ISerializable
  {
    private ApplicationIdentity _applicationIdentity;
    private ArrayList _definitionIdentities;
    private ArrayList _manifests;
    private string[] _manifestPaths;
    private ActivationContext.ContextForm _form;
    private ActivationContext.ApplicationStateDisposition _appRunState;
    private IActContext _actContext;
    private const int DefaultComponentCount = 2;

    /// <summary>获取当前应用程序的应用程序标识。</summary>
    /// <returns>
    /// <see cref="T:System.ApplicationIdentity" /> 对象，用于标识当前应用程序。</returns>
    /// <filterpriority>1</filterpriority>
    public ApplicationIdentity Identity
    {
      get
      {
        return this._applicationIdentity;
      }
    }

    /// <summary>获取当前应用程序的窗体或存储区上下文。</summary>
    /// <returns>枚举值之一。</returns>
    /// <filterpriority>1</filterpriority>
    public ActivationContext.ContextForm Form
    {
      get
      {
        return this._form;
      }
    }

    /// <summary>获取当前应用程序的 ClickOnce 应用程序清单。</summary>
    /// <returns>一个字节数组，它包含与此 <see cref="T:System.ActivationContext" /> 关联的应用程序的 ClickOnce 应用程序清单。</returns>
    public byte[] ApplicationManifestBytes
    {
      get
      {
        return this.GetApplicationManifestBytes();
      }
    }

    /// <summary>获取当前应用程序的 ClickOnce 部署清单。</summary>
    /// <returns>一个字节数组，它包含与此 <see cref="T:System.ActivationContext" /> 关联的应用程序的 ClickOnce 部署清单。</returns>
    public byte[] DeploymentManifestBytes
    {
      get
      {
        return this.GetDeploymentManifestBytes();
      }
    }

    internal string[] ManifestPaths
    {
      get
      {
        return this._manifestPaths;
      }
    }

    internal string ApplicationDirectory
    {
      [SecurityCritical] get
      {
        if (this._form == ActivationContext.ContextForm.Loose)
          return Path.GetDirectoryName(this._manifestPaths[this._manifestPaths.Length - 1]);
        string ApplicationPath;
        this._actContext.ApplicationBasePath(0U, out ApplicationPath);
        return ApplicationPath;
      }
    }

    internal string DataDirectory
    {
      [SecurityCritical] get
      {
        if (this._form == ActivationContext.ContextForm.Loose)
          return (string) null;
        string ppszPath;
        this._actContext.GetApplicationStateFilesystemLocation(1U, UIntPtr.Zero, IntPtr.Zero, out ppszPath);
        return ppszPath;
      }
    }

    internal ICMS ActivationContextData
    {
      [SecurityCritical] get
      {
        return this.ApplicationComponentManifest;
      }
    }

    internal ICMS DeploymentComponentManifest
    {
      [SecurityCritical] get
      {
        if (this._form == ActivationContext.ContextForm.Loose)
          return (ICMS) this._manifests[0];
        return this.GetComponentManifest((IDefinitionIdentity) this._definitionIdentities[0]);
      }
    }

    internal ICMS ApplicationComponentManifest
    {
      [SecurityCritical] get
      {
        if (this._form == ActivationContext.ContextForm.Loose)
          return (ICMS) this._manifests[this._manifests.Count - 1];
        return this.GetComponentManifest((IDefinitionIdentity) this._definitionIdentities[this._definitionIdentities.Count - 1]);
      }
    }

    internal ActivationContext.ApplicationStateDisposition LastApplicationStateResult
    {
      get
      {
        return this._appRunState;
      }
    }

    private ActivationContext()
    {
    }

    [SecurityCritical]
    private ActivationContext(SerializationInfo info, StreamingContext context)
    {
      string applicationIdentityFullName = (string) info.GetValue("FullName", typeof (string));
      string[] manifestPaths = (string[]) info.GetValue("ManifestPaths", typeof (string[]));
      if (manifestPaths == null)
        this.CreateFromName(new ApplicationIdentity(applicationIdentityFullName));
      else
        this.CreateFromNameAndManifests(new ApplicationIdentity(applicationIdentityFullName), manifestPaths);
    }

    internal ActivationContext(ApplicationIdentity applicationIdentity)
    {
      this.CreateFromName(applicationIdentity);
    }

    internal ActivationContext(ApplicationIdentity applicationIdentity, string[] manifestPaths)
    {
      this.CreateFromNameAndManifests(applicationIdentity, manifestPaths);
    }

    /// <summary>使 <see cref="T:System.ActivationContext" /> 对象在垃圾回收已收回 <see cref="T:System.ActivationContext" /> 之前尝试释放资源并执行其他清理操作。</summary>
    ~ActivationContext()
    {
      this.Dispose(false);
    }

    [SecuritySafeCritical]
    private void CreateFromName(ApplicationIdentity applicationIdentity)
    {
      if (applicationIdentity == null)
        throw new ArgumentNullException("applicationIdentity");
      this._applicationIdentity = applicationIdentity;
      IEnumDefinitionIdentity definitionIdentity = this._applicationIdentity.Identity.EnumAppPath();
      this._definitionIdentities = new ArrayList(2);
      IDefinitionIdentity[] DefinitionIdentity = new IDefinitionIdentity[1];
      while ((int) definitionIdentity.Next(1U, DefinitionIdentity) == 1)
        this._definitionIdentities.Add((object) DefinitionIdentity[0]);
      this._definitionIdentities.TrimToSize();
      if (this._definitionIdentities.Count <= 1)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidAppId"));
      this._manifestPaths = (string[]) null;
      this._manifests = (ArrayList) null;
      this._actContext = IsolationInterop.CreateActContext(this._applicationIdentity.Identity);
      this._form = ActivationContext.ContextForm.StoreBounded;
      this._appRunState = ActivationContext.ApplicationStateDisposition.Undefined;
    }

    [SecuritySafeCritical]
    private void CreateFromNameAndManifests(ApplicationIdentity applicationIdentity, string[] manifestPaths)
    {
      if (applicationIdentity == null)
        throw new ArgumentNullException("applicationIdentity");
      if (manifestPaths == null)
        throw new ArgumentNullException("manifestPaths");
      this._applicationIdentity = applicationIdentity;
      IEnumDefinitionIdentity definitionIdentity = this._applicationIdentity.Identity.EnumAppPath();
      this._manifests = new ArrayList(2);
      this._manifestPaths = new string[manifestPaths.Length];
      IDefinitionIdentity[] DefinitionIdentity = new IDefinitionIdentity[1];
      int index = 0;
      while ((int) definitionIdentity.Next(1U, DefinitionIdentity) == 1)
      {
        ICMS cms = (ICMS) IsolationInterop.ParseManifest(manifestPaths[index], (IManifestParseErrorCallback) null, ref IsolationInterop.IID_ICMS);
        if (!IsolationInterop.IdentityAuthority.AreDefinitionsEqual(0U, cms.Identity, DefinitionIdentity[0]))
          throw new ArgumentException(Environment.GetResourceString("Argument_IllegalAppIdMismatch"));
        this._manifests.Add((object) cms);
        this._manifestPaths[index] = manifestPaths[index];
        ++index;
      }
      if (index != manifestPaths.Length)
        throw new ArgumentException(Environment.GetResourceString("Argument_IllegalAppId"));
      this._manifests.TrimToSize();
      if (this._manifests.Count <= 1)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidAppId"));
      this._definitionIdentities = (ArrayList) null;
      this._actContext = (IActContext) null;
      this._form = ActivationContext.ContextForm.Loose;
      this._appRunState = ActivationContext.ApplicationStateDisposition.Undefined;
    }

    /// <summary>使用指定的应用程序标识初始化 <see cref="T:System.ActivationContext" /> 类的新实例。</summary>
    /// <returns>具有指定应用程序标识的对象。</returns>
    /// <param name="identity">一个对象，用于标识应用程序。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="identity" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">在 <paramref name="identity" /> 中没有指定部署标识或应用程序标识。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public static ActivationContext CreatePartialActivationContext(ApplicationIdentity identity)
    {
      return new ActivationContext(identity);
    }

    /// <summary>使用指定的应用程序标识和清单路径数组来初始化 <see cref="T:System.ActivationContext" /> 类的新实例。</summary>
    /// <returns>具有指定应用程序标识和清单路径数组的对象。</returns>
    /// <param name="identity">一个对象，用于标识应用程序。</param>
    /// <param name="manifestPaths">应用程序的清单路径的字符串数组。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="identity" /> 为 null。- 或 -<paramref name="manifestPaths" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">在 <paramref name="identity" /> 中没有指定部署标识或应用程序标识。- 或 -<paramref name="identity" /> 与清单中的标识不匹配。- 或 -<paramref name="identity" /> 没有与清单路径相同的组件数量。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public static ActivationContext CreatePartialActivationContext(ApplicationIdentity identity, string[] manifestPaths)
    {
      return new ActivationContext(identity, manifestPaths);
    }

    /// <summary>释放由 <see cref="T:System.ActivationContext" /> 使用的所有资源。</summary>
    /// <filterpriority>1</filterpriority>
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    [SecurityCritical]
    internal ICMS GetComponentManifest(IDefinitionIdentity component)
    {
      object ManifestInteface;
      this._actContext.GetComponentManifest(0U, component, ref IsolationInterop.IID_ICMS, out ManifestInteface);
      return ManifestInteface as ICMS;
    }

    [SecuritySafeCritical]
    internal byte[] GetDeploymentManifestBytes()
    {
      string FullPath;
      if (this._form == ActivationContext.ContextForm.Loose)
      {
        FullPath = this._manifestPaths[0];
      }
      else
      {
        object ManifestInteface;
        this._actContext.GetComponentManifest(0U, (IDefinitionIdentity) this._definitionIdentities[0], ref IsolationInterop.IID_IManifestInformation, out ManifestInteface);
        ((IManifestInformation) ManifestInteface).get_FullPath(out FullPath);
        Marshal.ReleaseComObject(ManifestInteface);
      }
      return ActivationContext.ReadBytesFromFile(FullPath);
    }

    [SecuritySafeCritical]
    internal byte[] GetApplicationManifestBytes()
    {
      string FullPath;
      if (this._form == ActivationContext.ContextForm.Loose)
      {
        FullPath = this._manifestPaths[this._manifests.Count - 1];
      }
      else
      {
        object ManifestInteface;
        this._actContext.GetComponentManifest(0U, (IDefinitionIdentity) this._definitionIdentities[1], ref IsolationInterop.IID_IManifestInformation, out ManifestInteface);
        ((IManifestInformation) ManifestInteface).get_FullPath(out FullPath);
        Marshal.ReleaseComObject(ManifestInteface);
      }
      return ActivationContext.ReadBytesFromFile(FullPath);
    }

    [SecuritySafeCritical]
    internal void PrepareForExecution()
    {
      if (this._form == ActivationContext.ContextForm.Loose)
        return;
      this._actContext.PrepareForExecution(IntPtr.Zero, IntPtr.Zero);
    }

    [SecuritySafeCritical]
    internal ActivationContext.ApplicationStateDisposition SetApplicationState(ActivationContext.ApplicationState s)
    {
      if (this._form == ActivationContext.ContextForm.Loose)
        return ActivationContext.ApplicationStateDisposition.Undefined;
      uint ulDisposition;
      this._actContext.SetApplicationRunningState(0U, (uint) s, out ulDisposition);
      this._appRunState = (ActivationContext.ApplicationStateDisposition) ulDisposition;
      return this._appRunState;
    }

    [SecuritySafeCritical]
    private void Dispose(bool fDisposing)
    {
      this._applicationIdentity = (ApplicationIdentity) null;
      this._definitionIdentities = (ArrayList) null;
      this._manifests = (ArrayList) null;
      this._manifestPaths = (string[]) null;
      if (this._actContext == null)
        return;
      Marshal.ReleaseComObject((object) this._actContext);
    }

    private static byte[] ReadBytesFromFile(string manifestPath)
    {
      byte[] buffer = (byte[]) null;
      using (FileStream fileStream = new FileStream(manifestPath, FileMode.Open, FileAccess.Read))
      {
        int count = (int) fileStream.Length;
        buffer = new byte[count];
        if (fileStream.CanSeek)
          fileStream.Seek(0L, SeekOrigin.Begin);
        fileStream.Read(buffer, 0, count);
      }
      return buffer;
    }

    [SecurityCritical]
    void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (this._applicationIdentity != null)
        info.AddValue("FullName", (object) this._applicationIdentity.FullName, typeof (string));
      if (this._manifestPaths == null)
        return;
      info.AddValue("ManifestPaths", (object) this._manifestPaths, typeof (string[]));
    }

    /// <summary>指示清单激活的应用程序的上下文。</summary>
    public enum ContextForm
    {
      Loose,
      StoreBounded,
    }

    internal enum ApplicationState
    {
      Undefined,
      Starting,
      Running,
    }

    internal enum ApplicationStateDisposition
    {
      Undefined = 0,
      Starting = 1,
      Running = 2,
      StartingMigrated = 65537,
      RunningFirstTime = 131074,
    }
  }
}
