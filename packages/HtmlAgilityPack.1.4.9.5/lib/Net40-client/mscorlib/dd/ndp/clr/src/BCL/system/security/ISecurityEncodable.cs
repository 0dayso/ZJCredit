// Decompiled with JetBrains decompiler
// Type: System.Security.ISecurityEncodable
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security
{
  /// <summary>定义使权限对象状态与 XML 元素表示形式进行相互转换的方法。</summary>
  [ComVisible(true)]
  public interface ISecurityEncodable
  {
    /// <summary>创建安全对象及其当前状态的 XML 编码。</summary>
    /// <returns>安全对象的 XML 编码，包括任何状态信息。</returns>
    SecurityElement ToXml();

    /// <summary>用 XML 编码重新构造具有指定状态的安全对象。</summary>
    /// <param name="e">用于重新构造安全对象的 XML 编码。</param>
    void FromXml(SecurityElement e);
  }
}
