// Decompiled with JetBrains decompiler
// Type: System.Security.SecurityRulesAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Security
{
  /// <summary>指示公共语言运行时应该对程序集强制的一组安全规则。</summary>
  [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
  public sealed class SecurityRulesAttribute : Attribute
  {
    private SecurityRuleSet m_ruleSet;
    private bool m_skipVerificationInFullTrust;

    /// <summary>确定完全信任的透明代码是否应该跳过 Microsoft 中间语言 (MSIL) 验证。</summary>
    /// <returns>如果应该跳过 MSIL 验证，则为 true；否则为 false。默认值为 false。</returns>
    public bool SkipVerificationInFullTrust
    {
      get
      {
        return this.m_skipVerificationInFullTrust;
      }
      set
      {
        this.m_skipVerificationInFullTrust = value;
      }
    }

    /// <summary>获取要应用的规则集。</summary>
    /// <returns>用于指定要应用的透明规则的枚举值之一。</returns>
    public SecurityRuleSet RuleSet
    {
      get
      {
        return this.m_ruleSet;
      }
    }

    /// <summary>使用指定的规则集值初始化 <see cref="T:System.Security.SecurityRulesAttribute" /> 类的新实例。</summary>
    /// <param name="ruleSet">用于指定透明规则集的枚举值之一。</param>
    public SecurityRulesAttribute(SecurityRuleSet ruleSet)
    {
      this.m_ruleSet = ruleSet;
    }
  }
}
