// Decompiled with JetBrains decompiler
// Type: System.Security.ISecurityPolicyEncodable
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security.Policy;

namespace System.Security
{
  /// <summary>支持使权限对象状态与 XML 元素表示形式进行相互转换的方法。</summary>
  [ComVisible(true)]
  public interface ISecurityPolicyEncodable
  {
    /// <summary>创建安全对象及其当前状态的 XML 编码。</summary>
    /// <returns>策略对象的 XML 表示形式的根元素。</returns>
    /// <param name="level">解析命名权限集引用的策略级上下文。</param>
    SecurityElement ToXml(PolicyLevel level);

    /// <summary>用 XML 编码重新构造具有指定状态的安全对象。</summary>
    /// <param name="e">用于重新构造安全对象的 XML 编码。</param>
    /// <param name="level">解析命名权限集引用的策略级上下文。</param>
    void FromXml(SecurityElement e, PolicyLevel level);
  }
}
