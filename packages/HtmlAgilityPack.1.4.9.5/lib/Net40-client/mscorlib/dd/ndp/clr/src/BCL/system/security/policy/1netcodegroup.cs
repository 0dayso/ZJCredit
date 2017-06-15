// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.NetCodeGroup
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;

namespace System.Security.Policy
{
  /// <summary>向从其下载程序集的站点授予 Web 权限。此类不能被继承。</summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class NetCodeGroup : CodeGroup, IUnionSemanticCodeGroup
  {
    private static readonly char[] c_SomeRegexChars = new char[12]{ '.', '-', '+', '[', ']', '{', '$', '^', '#', ')', '(', ' ' };
    /// <summary>包含一个值，用于指定任何其他未指定的原始方案。</summary>
    public static readonly string AnyOtherOriginScheme = CodeConnectAccess.AnyScheme;
    /// <summary>包含一个值，该值用于为具有未知或未能识别的原始方案的代码指定连接访问权限。</summary>
    public static readonly string AbsentOriginScheme = string.Empty;
    [OptionalField(VersionAdded = 2)]
    private ArrayList m_schemesList;
    [OptionalField(VersionAdded = 2)]
    private ArrayList m_accessList;
    private const string c_IgnoreUserInfo = "";
    private const string c_AnyScheme = "([0-9a-z+\\-\\.]+)://";

    /// <summary>获取用于合并组的逻辑。</summary>
    /// <returns>字符串“Union”。</returns>
    public override string MergeLogic
    {
      get
      {
        return Environment.GetResourceString("MergeLogic_Union");
      }
    }

    /// <summary>获取该代码组的 <see cref="T:System.Security.NamedPermissionSet" /> 的名称。</summary>
    /// <returns>始终为字符串“Same site Web”。</returns>
    public override string PermissionSetName
    {
      get
      {
        return Environment.GetResourceString("NetCodeGroup_PermissionSet");
      }
    }

    /// <summary>获取代码组策略声明的特性的字符串表示形式。</summary>
    /// <returns>始终为 null。</returns>
    public override string AttributeString
    {
      get
      {
        return (string) null;
      }
    }

    internal NetCodeGroup()
    {
      this.SetDefaults();
    }

    /// <summary>初始化 <see cref="T:System.Security.Policy.NetCodeGroup" /> 类的新实例。</summary>
    /// <param name="membershipCondition">成员条件，它测试证据以确定此代码组是否应用代码访问安全策略。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="membershipCondition" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="membershipCondition" /> 参数的类型无效。</exception>
    public NetCodeGroup(IMembershipCondition membershipCondition)
      : base(membershipCondition, (PolicyStatement) null)
    {
      this.SetDefaults();
    }

    [SecurityCritical]
    [Conditional("_DEBUG")]
    private static void DEBUG_OUT(string str)
    {
    }

    [OnDeserializing]
    private void OnDeserializing(StreamingContext ctx)
    {
      this.m_schemesList = (ArrayList) null;
      this.m_accessList = (ArrayList) null;
    }

    /// <summary>移除当前代码组的所有连接访问权限信息。</summary>
    public void ResetConnectAccess()
    {
      this.m_schemesList = (ArrayList) null;
      this.m_accessList = (ArrayList) null;
    }

    /// <summary>将指定的连接访问权限添加到当前代码组。</summary>
    /// <param name="originScheme">一个 <see cref="T:System.String" />，包含要与代码的方案进行匹配的方案。</param>
    /// <param name="connectAccess">一个 <see cref="T:System.Security.Policy.CodeConnectAccess" />，指定代码可用来连接回其原始服务器的方案和端口。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="originScheme" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">一个 <paramref name="originScheme" />，包含方案中不允许的字符。- 或 -<paramref name="originScheme" /> = <see cref="F:System.Security.Policy.NetCodeGroup.AbsentOriginScheme" /> 和 <paramref name="connectAccess" /> 指定 <see cref="F:System.Security.Policy.CodeConnectAccess.OriginScheme" /> 作为其方案。</exception>
    public void AddConnectAccess(string originScheme, CodeConnectAccess connectAccess)
    {
      if (originScheme == null)
        throw new ArgumentNullException("originScheme");
      if (originScheme != NetCodeGroup.AbsentOriginScheme && originScheme != NetCodeGroup.AnyOtherOriginScheme && !CodeConnectAccess.IsValidScheme(originScheme))
        throw new ArgumentOutOfRangeException("originScheme");
      if (originScheme == NetCodeGroup.AbsentOriginScheme && connectAccess.IsOriginScheme)
        throw new ArgumentOutOfRangeException("connectAccess");
      if (this.m_schemesList == null)
      {
        this.m_schemesList = new ArrayList();
        this.m_accessList = new ArrayList();
      }
      originScheme = originScheme.ToLower(CultureInfo.InvariantCulture);
      for (int index1 = 0; index1 < this.m_schemesList.Count; ++index1)
      {
        if ((string) this.m_schemesList[index1] == originScheme)
        {
          if (connectAccess == null)
            return;
          ArrayList arrayList = (ArrayList) this.m_accessList[index1];
          for (int index2 = 0; index2 < arrayList.Count; ++index2)
          {
            if (((CodeConnectAccess) arrayList[index2]).Equals((object) connectAccess))
              return;
          }
          arrayList.Add((object) connectAccess);
          return;
        }
      }
      this.m_schemesList.Add((object) originScheme);
      ArrayList arrayList1 = new ArrayList();
      this.m_accessList.Add((object) arrayList1);
      if (connectAccess == null)
        return;
      arrayList1.Add((object) connectAccess);
    }

    /// <summary>获取当前代码组的连接访问权限信息。</summary>
    /// <returns>一个 <see cref="T:System.Collections.DictionaryEntry" /> 数组，包含连接访问权限信息。</returns>
    public DictionaryEntry[] GetConnectAccessRules()
    {
      if (this.m_schemesList == null)
        return (DictionaryEntry[]) null;
      DictionaryEntry[] dictionaryEntryArray = new DictionaryEntry[this.m_schemesList.Count];
      for (int index = 0; index < dictionaryEntryArray.Length; ++index)
      {
        dictionaryEntryArray[index].Key = this.m_schemesList[index];
        dictionaryEntryArray[index].Value = (object) ((ArrayList) this.m_accessList[index]).ToArray(typeof (CodeConnectAccess));
      }
      return dictionaryEntryArray;
    }

    /// <summary>对一组证据解析代码组及其子代的策略。</summary>
    /// <returns>由具有可选特性的代码组授予的权限组成的 <see cref="T:System.Security.Policy.PolicyStatement" />；或者，如果代码组不适用（成员条件与指定的证据不匹配），则为 null。</returns>
    /// <param name="evidence">程序集的 <see cref="T:System.Security.Policy.Evidence" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="evidence" /> 参数为 null。</exception>
    /// <exception cref="T:System.Security.Policy.PolicyException">将不止一个代码组（包括父代码组和任何子代码组）标记为 <see cref="F:System.Security.Policy.PolicyStatementAttribute.Exclusive" />。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public override PolicyStatement Resolve(Evidence evidence)
    {
      if (evidence == null)
        throw new ArgumentNullException("evidence");
      object usedEvidence = (object) null;
      if (!PolicyManager.CheckMembershipCondition(this.MembershipCondition, evidence, out usedEvidence))
        return (PolicyStatement) null;
      PolicyStatement assemblyPolicy = this.CalculateAssemblyPolicy(evidence);
      IDelayEvaluatedEvidence dependentEvidence = usedEvidence as IDelayEvaluatedEvidence;
      if ((dependentEvidence == null ? 0 : (!dependentEvidence.IsVerified ? 1 : 0)) != 0)
        assemblyPolicy.AddDependentEvidence(dependentEvidence);
      bool flag = false;
      IEnumerator enumerator = this.Children.GetEnumerator();
      while (enumerator.MoveNext() && !flag)
      {
        PolicyStatement childPolicy = PolicyManager.ResolveCodeGroup(enumerator.Current as CodeGroup, evidence);
        if (childPolicy != null)
        {
          assemblyPolicy.InplaceUnion(childPolicy);
          if ((childPolicy.Attributes & PolicyStatementAttribute.Exclusive) == PolicyStatementAttribute.Exclusive)
            flag = true;
        }
      }
      return assemblyPolicy;
    }

    PolicyStatement IUnionSemanticCodeGroup.InternalResolve(Evidence evidence)
    {
      if (evidence == null)
        throw new ArgumentNullException("evidence");
      if (this.MembershipCondition.Check(evidence))
        return this.CalculateAssemblyPolicy(evidence);
      return (PolicyStatement) null;
    }

    /// <summary>解析匹配的代码组。</summary>
    /// <returns>证据匹配的完整代码组集。</returns>
    /// <param name="evidence">程序集的证据。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="evidence" /> 参数为 null。</exception>
    public override CodeGroup ResolveMatchingCodeGroups(Evidence evidence)
    {
      if (evidence == null)
        throw new ArgumentNullException("evidence");
      if (!this.MembershipCondition.Check(evidence))
        return (CodeGroup) null;
      CodeGroup codeGroup = this.Copy();
      codeGroup.Children = (IList) new ArrayList();
      foreach (CodeGroup child in (IEnumerable) this.Children)
      {
        CodeGroup group = child.ResolveMatchingCodeGroups(evidence);
        if (group != null)
          codeGroup.AddChild(group);
      }
      return codeGroup;
    }

    private string EscapeStringForRegex(string str)
    {
      int startIndex = 0;
      StringBuilder stringBuilder = (StringBuilder) null;
      int index;
      for (; startIndex < str.Length && (index = str.IndexOfAny(NetCodeGroup.c_SomeRegexChars, startIndex)) != -1; startIndex = index + 1)
      {
        if (stringBuilder == null)
          stringBuilder = new StringBuilder(str.Length * 2);
        stringBuilder.Append(str, startIndex, index - startIndex).Append('\\').Append(str[index]);
      }
      if (stringBuilder == null)
        return str;
      if (startIndex < str.Length)
        stringBuilder.Append(str, startIndex, str.Length - startIndex);
      return stringBuilder.ToString();
    }

    internal SecurityElement CreateWebPermission(string host, string scheme, string port, string assemblyOverride)
    {
      if (scheme == null)
        scheme = string.Empty;
      if (host == null || host.Length == 0)
        return (SecurityElement) null;
      host = host.ToLower(CultureInfo.InvariantCulture);
      scheme = scheme.ToLower(CultureInfo.InvariantCulture);
      int intPort = -1;
      if (port != null && port.Length != 0)
        intPort = int.Parse(port, (IFormatProvider) CultureInfo.InvariantCulture);
      else
        port = string.Empty;
      CodeConnectAccess[] accessRulesForScheme = this.FindAccessRulesForScheme(scheme);
      if (accessRulesForScheme == null || accessRulesForScheme.Length == 0)
        return (SecurityElement) null;
      SecurityElement securityElement = new SecurityElement("IPermission");
      string str1 = assemblyOverride == null ? "System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" : assemblyOverride;
      securityElement.AddAttribute("class", "System.Net.WebPermission, " + str1);
      securityElement.AddAttribute("version", "1");
      SecurityElement child1 = new SecurityElement("ConnectAccess");
      host = this.EscapeStringForRegex(host);
      scheme = this.EscapeStringForRegex(scheme);
      string str2 = this.TryPermissionAsOneString(accessRulesForScheme, scheme, host, intPort);
      if (str2 != null)
      {
        SecurityElement child2 = new SecurityElement("URI");
        child2.AddAttribute("uri", str2);
        child1.AddChild(child2);
      }
      else
      {
        if (port.Length != 0)
          port = ":" + port;
        for (int index = 0; index < accessRulesForScheme.Length; ++index)
        {
          string accessElementString = this.GetPermissionAccessElementString(accessRulesForScheme[index], scheme, host, port);
          SecurityElement child2 = new SecurityElement("URI");
          child2.AddAttribute("uri", accessElementString);
          child1.AddChild(child2);
        }
      }
      securityElement.AddChild(child1);
      return securityElement;
    }

    private CodeConnectAccess[] FindAccessRulesForScheme(string lowerCaseScheme)
    {
      if (this.m_schemesList == null)
        return (CodeConnectAccess[]) null;
      int index = this.m_schemesList.IndexOf((object) lowerCaseScheme);
      if (index == -1 && (lowerCaseScheme == NetCodeGroup.AbsentOriginScheme || (index = this.m_schemesList.IndexOf((object) NetCodeGroup.AnyOtherOriginScheme)) == -1))
        return (CodeConnectAccess[]) null;
      return (CodeConnectAccess[]) ((ArrayList) this.m_accessList[index]).ToArray(typeof (CodeConnectAccess));
    }

    private string TryPermissionAsOneString(CodeConnectAccess[] access, string escapedScheme, string escapedHost, int intPort)
    {
      bool flag1 = true;
      bool flag2 = true;
      bool flag3 = false;
      int num = -2;
      for (int index = 0; index < access.Length; ++index)
      {
        flag1 = ((flag1 ? 1 : 0) & (access[index].IsDefaultPort ? 1 : (!access[index].IsOriginPort ? 0 : (intPort == -1 ? 1 : 0)))) != 0;
        flag2 = ((flag2 ? 1 : 0) & (access[index].IsOriginPort ? 1 : (access[index].Port == intPort ? 1 : 0))) != 0;
        if (access[index].Port >= 0)
        {
          if (num == -2)
            num = access[index].Port;
          else if (access[index].Port != num)
            num = -1;
        }
        else
          num = -1;
        if (access[index].IsAnyScheme)
          flag3 = true;
      }
      if (!flag1 && !flag2 && num == -1)
        return (string) null;
      StringBuilder stringBuilder = new StringBuilder("([0-9a-z+\\-\\.]+)://".Length * access.Length + "".Length * 2 + escapedHost.Length);
      if (flag3)
      {
        stringBuilder.Append("([0-9a-z+\\-\\.]+)://");
      }
      else
      {
        stringBuilder.Append('(');
        for (int index1 = 0; index1 < access.Length; ++index1)
        {
          int index2 = 0;
          while (index2 < index1 && !(access[index1].Scheme == access[index2].Scheme))
            ++index2;
          if (index2 == index1)
          {
            if (index1 != 0)
              stringBuilder.Append('|');
            stringBuilder.Append(access[index1].IsOriginScheme ? escapedScheme : this.EscapeStringForRegex(access[index1].Scheme));
          }
        }
        stringBuilder.Append(")://");
      }
      stringBuilder.Append("").Append(escapedHost);
      if (!flag1)
      {
        if (flag2)
          stringBuilder.Append(':').Append(intPort);
        else
          stringBuilder.Append(':').Append(num);
      }
      stringBuilder.Append("/.*");
      return stringBuilder.ToString();
    }

    private string GetPermissionAccessElementString(CodeConnectAccess access, string escapedScheme, string escapedHost, string strPort)
    {
      StringBuilder stringBuilder = new StringBuilder("([0-9a-z+\\-\\.]+)://".Length * 2 + "".Length + escapedHost.Length);
      if (access.IsAnyScheme)
        stringBuilder.Append("([0-9a-z+\\-\\.]+)://");
      else if (access.IsOriginScheme)
        stringBuilder.Append(escapedScheme).Append("://");
      else
        stringBuilder.Append(this.EscapeStringForRegex(access.Scheme)).Append("://");
      stringBuilder.Append("").Append(escapedHost);
      if (!access.IsDefaultPort)
      {
        if (access.IsOriginPort)
          stringBuilder.Append(strPort);
        else
          stringBuilder.Append(':').Append(access.StrPort);
      }
      stringBuilder.Append("/.*");
      return stringBuilder.ToString();
    }

    internal PolicyStatement CalculatePolicy(string host, string scheme, string port)
    {
      SecurityElement webPermission = this.CreateWebPermission(host, scheme, port, (string) null);
      SecurityElement securityElement = new SecurityElement("PolicyStatement");
      SecurityElement child = new SecurityElement("PermissionSet");
      child.AddAttribute("class", "System.Security.PermissionSet");
      child.AddAttribute("version", "1");
      if (webPermission != null)
        child.AddChild(webPermission);
      securityElement.AddChild(child);
      PolicyStatement policyStatement = new PolicyStatement();
      SecurityElement et = securityElement;
      policyStatement.FromXml(et);
      return policyStatement;
    }

    private PolicyStatement CalculateAssemblyPolicy(Evidence evidence)
    {
      PolicyStatement policyStatement = (PolicyStatement) null;
      Url hostEvidence1 = evidence.GetHostEvidence<Url>();
      if (hostEvidence1 != null)
        policyStatement = this.CalculatePolicy(hostEvidence1.GetURLString().Host, hostEvidence1.GetURLString().Scheme, hostEvidence1.GetURLString().Port);
      if (policyStatement == null)
      {
        Site hostEvidence2 = evidence.GetHostEvidence<Site>();
        if (hostEvidence2 != null)
          policyStatement = this.CalculatePolicy(hostEvidence2.Name, (string) null, (string) null);
      }
      if (policyStatement == null)
        policyStatement = new PolicyStatement(new PermissionSet(false), PolicyStatementAttribute.Nothing);
      return policyStatement;
    }

    /// <summary>生成当前代码组的深层副本。</summary>
    /// <returns>当前代码组（包括其成员条件和子代码组）的等效副本。</returns>
    public override CodeGroup Copy()
    {
      NetCodeGroup netCodeGroup = new NetCodeGroup(this.MembershipCondition);
      netCodeGroup.Name = this.Name;
      netCodeGroup.Description = this.Description;
      if (this.m_schemesList != null)
      {
        netCodeGroup.m_schemesList = (ArrayList) this.m_schemesList.Clone();
        netCodeGroup.m_accessList = new ArrayList(this.m_accessList.Count);
        for (int index = 0; index < this.m_accessList.Count; ++index)
          netCodeGroup.m_accessList.Add(((ArrayList) this.m_accessList[index]).Clone());
      }
      foreach (CodeGroup child in (IEnumerable) this.Children)
        netCodeGroup.AddChild(child);
      return (CodeGroup) netCodeGroup;
    }

    /// <summary>确定指定的代码组是否等效于当前代码组。</summary>
    /// <returns>如果指定的代码组等效于当前代码组，则为 true；否则为 false。</returns>
    /// <param name="o">要与当前代码组进行比较的 <see cref="T:System.Security.Policy.NetCodeGroup" /> 对象。</param>
    public override bool Equals(object o)
    {
      if (this == o)
        return true;
      NetCodeGroup netCodeGroup = o as NetCodeGroup;
      if (netCodeGroup == null || !base.Equals((object) netCodeGroup) || this.m_schemesList == null != (netCodeGroup.m_schemesList == null))
        return false;
      if (this.m_schemesList == null)
        return true;
      if (this.m_schemesList.Count != netCodeGroup.m_schemesList.Count)
        return false;
      for (int index1 = 0; index1 < this.m_schemesList.Count; ++index1)
      {
        int index2 = netCodeGroup.m_schemesList.IndexOf(this.m_schemesList[index1]);
        if (index2 == -1)
          return false;
        ArrayList arrayList1 = (ArrayList) this.m_accessList[index1];
        ArrayList arrayList2 = (ArrayList) netCodeGroup.m_accessList[index2];
        if (arrayList1.Count != arrayList2.Count)
          return false;
        for (int index3 = 0; index3 < arrayList1.Count; ++index3)
        {
          if (!arrayList2.Contains(arrayList1[index3]))
            return false;
        }
      }
      return true;
    }

    /// <returns>当前代码组的哈希代码。</returns>
    public override int GetHashCode()
    {
      return base.GetHashCode() + this.GetRulesHashCode();
    }

    private int GetRulesHashCode()
    {
      if (this.m_schemesList == null)
        return 0;
      int num = 0;
      for (int index = 0; index < this.m_schemesList.Count; ++index)
        num += ((string) this.m_schemesList[index]).GetHashCode();
      foreach (ArrayList mAccess in this.m_accessList)
      {
        for (int index = 0; index < mAccess.Count; ++index)
          num += ((CodeConnectAccess) mAccess[index]).GetHashCode();
      }
      return num;
    }

    protected override void CreateXml(SecurityElement element, PolicyLevel level)
    {
      DictionaryEntry[] connectAccessRules = this.GetConnectAccessRules();
      if (connectAccessRules == null)
        return;
      SecurityElement child1 = new SecurityElement("connectAccessRules");
      foreach (DictionaryEntry dictionaryEntry in connectAccessRules)
      {
        SecurityElement child2 = new SecurityElement("codeOrigin");
        child2.AddAttribute("scheme", (string) dictionaryEntry.Key);
        foreach (CodeConnectAccess codeConnectAccess in (CodeConnectAccess[]) dictionaryEntry.Value)
        {
          SecurityElement child3 = new SecurityElement("connectAccess");
          child3.AddAttribute("scheme", codeConnectAccess.Scheme);
          child3.AddAttribute("port", codeConnectAccess.StrPort);
          child2.AddChild(child3);
        }
        child1.AddChild(child2);
      }
      element.AddChild(child1);
    }

    protected override void ParseXml(SecurityElement e, PolicyLevel level)
    {
      this.ResetConnectAccess();
      SecurityElement securityElement = e.SearchForChildByTag("connectAccessRules");
      if (securityElement == null || securityElement.Children == null)
      {
        this.SetDefaults();
      }
      else
      {
        foreach (SecurityElement child1 in securityElement.Children)
        {
          if (child1.Tag.Equals("codeOrigin"))
          {
            string originScheme = child1.Attribute("scheme");
            bool flag = false;
            if (child1.Children != null)
            {
              foreach (SecurityElement child2 in child1.Children)
              {
                if (child2.Tag.Equals("connectAccess"))
                {
                  string allowScheme = child2.Attribute("scheme");
                  string allowPort = child2.Attribute("port");
                  this.AddConnectAccess(originScheme, new CodeConnectAccess(allowScheme, allowPort));
                  flag = true;
                }
              }
            }
            if (!flag)
              this.AddConnectAccess(originScheme, (CodeConnectAccess) null);
          }
        }
      }
    }

    internal override string GetTypeName()
    {
      return "System.Security.Policy.NetCodeGroup";
    }

    private void SetDefaults()
    {
      this.AddConnectAccess("file", (CodeConnectAccess) null);
      this.AddConnectAccess("http", new CodeConnectAccess("http", CodeConnectAccess.OriginPort));
      this.AddConnectAccess("http", new CodeConnectAccess("https", CodeConnectAccess.OriginPort));
      this.AddConnectAccess("https", new CodeConnectAccess("https", CodeConnectAccess.OriginPort));
      this.AddConnectAccess(NetCodeGroup.AbsentOriginScheme, CodeConnectAccess.CreateAnySchemeAccess(CodeConnectAccess.OriginPort));
      this.AddConnectAccess(NetCodeGroup.AnyOtherOriginScheme, CodeConnectAccess.CreateOriginSchemeAccess(CodeConnectAccess.OriginPort));
    }
  }
}
