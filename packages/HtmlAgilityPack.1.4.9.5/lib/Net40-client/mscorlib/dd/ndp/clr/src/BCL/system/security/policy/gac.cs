// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.GacInstalled
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Security.Policy
{
  /// <summary>确认一个代码程序集在全局程序集缓存 (GAC) 中以策略评估证据的形式产生。此类不能被继承。</summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class GacInstalled : EvidenceBase, IIdentityPermissionFactory
  {
    /// <summary>创建与当前对象对应的新标识权限。</summary>
    /// <returns>与当前对象对应的新标识权限。</returns>
    /// <param name="evidence">构造标识权限所依据的 <see cref="T:System.Security.Policy.Evidence" />。</param>
    public IPermission CreateIdentityPermission(Evidence evidence)
    {
      return (IPermission) new GacIdentityPermission();
    }

    /// <summary>指示当前对象是否等效于指定的对象。</summary>
    /// <returns>如果 <paramref name="o" /> 是 <see cref="T:System.Security.Policy.GacInstalled" /> 对象，则为 true；否则为 false。</returns>
    /// <param name="o">要与当前对象进行比较的对象。</param>
    public override bool Equals(object o)
    {
      return o is GacInstalled;
    }

    /// <summary>返回当前对象的哈希代码。</summary>
    /// <returns>当前对象的哈希代码。</returns>
    public override int GetHashCode()
    {
      return 0;
    }

    /// <summary>创建作为当前实例副本的新对象。</summary>
    /// <returns>作为此实例副本的新对象。</returns>
    public override EvidenceBase Clone()
    {
      return (EvidenceBase) new GacInstalled();
    }

    /// <summary>创建当前对象的等效副本。</summary>
    /// <returns>
    /// <see cref="T:System.Security.Policy.GacInstalled" /> 的一个等效副本。</returns>
    public object Copy()
    {
      return (object) this.Clone();
    }

    internal SecurityElement ToXml()
    {
      SecurityElement securityElement = new SecurityElement(this.GetType().FullName);
      string name = "version";
      string str = "1";
      securityElement.AddAttribute(name, str);
      return securityElement;
    }

    /// <summary>返回当前  对象的字符串表示形式。</summary>
    /// <returns>当前对象的字符串表示形式。</returns>
    public override string ToString()
    {
      return this.ToXml().ToString();
    }
  }
}
