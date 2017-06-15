// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.ApplicationDirectory
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Policy
{
  /// <summary>提供应用程序目录作为策略评估的证据。此类不能被继承。</summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class ApplicationDirectory : EvidenceBase
  {
    private URLString m_appDirectory;

    /// <summary>获取应用程序目录的路径。</summary>
    /// <returns>应用程序目录的路径。</returns>
    public string Directory
    {
      get
      {
        return this.m_appDirectory.ToString();
      }
    }

    /// <summary>初始化 <see cref="T:System.Security.Policy.ApplicationDirectory" /> 类的新实例。</summary>
    /// <param name="name">应用程序目录的路径。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 参数为 null。</exception>
    public ApplicationDirectory(string name)
    {
      if (name == null)
        throw new ArgumentNullException("name");
      this.m_appDirectory = new URLString(name);
    }

    private ApplicationDirectory(URLString appDirectory)
    {
      this.m_appDirectory = appDirectory;
    }

    /// <summary>确定证据对象相同类型的实例是否是等效的。</summary>
    /// <returns>如果两个实例等效，则为 true；否则为 false。</returns>
    /// <param name="o">与当前证据对象类型相同的对象。</param>
    public override bool Equals(object o)
    {
      ApplicationDirectory applicationDirectory = o as ApplicationDirectory;
      if (applicationDirectory == null)
        return false;
      return this.m_appDirectory.Equals(applicationDirectory.m_appDirectory);
    }

    /// <summary>获取当前应用程序目录的哈希代码。</summary>
    /// <returns>当前应用程序目录的哈希代码。</returns>
    public override int GetHashCode()
    {
      return this.m_appDirectory.GetHashCode();
    }

    /// <summary>创建作为当前实例副本的新对象。</summary>
    /// <returns>作为此实例副本的新对象。</returns>
    public override EvidenceBase Clone()
    {
      return (EvidenceBase) new ApplicationDirectory(this.m_appDirectory);
    }

    /// <summary>创建 <see cref="T:System.Security.Policy.ApplicationDirectory" /> 的新副本。</summary>
    /// <returns>
    /// <see cref="T:System.Security.Policy.ApplicationDirectory" /> 的相同的新副本。</returns>
    public object Copy()
    {
      return (object) this.Clone();
    }

    internal SecurityElement ToXml()
    {
      SecurityElement securityElement = new SecurityElement("System.Security.Policy.ApplicationDirectory");
      securityElement.AddAttribute("version", "1");
      if (this.m_appDirectory != null)
        securityElement.AddChild(new SecurityElement("Directory", this.m_appDirectory.ToString()));
      return securityElement;
    }

    /// <summary>获取 <see cref="T:System.Security.Policy.ApplicationDirectory" /> 证据对象的状态的字符串表示形式。</summary>
    /// <returns>
    /// <see cref="T:System.Security.Policy.ApplicationDirectory" /> 证据对象的状态的字符串表示形式。</returns>
    public override string ToString()
    {
      return this.ToXml().ToString();
    }
  }
}
