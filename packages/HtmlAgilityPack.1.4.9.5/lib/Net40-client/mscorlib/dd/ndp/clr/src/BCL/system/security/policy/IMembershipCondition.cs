// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.IMembershipCondition
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Policy
{
  /// <summary>定义测试以确定代码程序集是否是代码组的成员。</summary>
  [ComVisible(true)]
  public interface IMembershipCondition : ISecurityEncodable, ISecurityPolicyEncodable
  {
    /// <summary>确定指定的证据是否能满足成员条件。</summary>
    /// <returns>如果指定的证据满足成员条件，则为 true；否则为 false。</returns>
    /// <param name="evidence">证据集，将根据它进行测试。</param>
    bool Check(Evidence evidence);

    /// <summary>创建成员条件的等效副本。</summary>
    /// <returns>当前成员条件的完全相同的新副本。</returns>
    IMembershipCondition Copy();

    /// <summary>创建并返回成员条件的字符串表示形式。</summary>
    /// <returns>当前成员条件状态的字符串表示形式。</returns>
    string ToString();

    /// <summary>确定指定的 <see cref="T:System.Object" /> 是否等于当前的 <see cref="T:System.Object" />。</summary>
    /// <returns>如果指定的 <see cref="T:System.Object" /> 等于当前的 <see cref="T:System.Object" />，则为 true；否则为 false。</returns>
    /// <param name="obj">与当前的 <see cref="T:System.Object" /> 进行比较的 <see cref="T:System.Object" />。</param>
    bool Equals(object obj);
  }
}
