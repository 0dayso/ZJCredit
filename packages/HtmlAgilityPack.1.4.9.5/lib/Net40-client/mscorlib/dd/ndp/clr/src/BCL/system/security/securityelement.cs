// Decompiled with JetBrains decompiler
// Type: System.Security.SecurityElement
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Util;
using System.Text;

namespace System.Security
{
  /// <summary>表示编码安全对象的 XML 对象模型。此类不能被继承。</summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class SecurityElement : ISecurityElementFactory
  {
    private static readonly char[] s_tagIllegalCharacters = new char[3]{ ' ', '<', '>' };
    private static readonly char[] s_textIllegalCharacters = new char[2]{ '<', '>' };
    private static readonly char[] s_valueIllegalCharacters = new char[3]{ '<', '>', '"' };
    private static readonly string[] s_escapeStringPairs = new string[10]{ "<", "&lt;", ">", "&gt;", "\"", "&quot;", "'", "&apos;", "&", "&amp;" };
    private static readonly char[] s_escapeChars = new char[5]{ '<', '>', '"', '\'', '&' };
    internal string m_strTag;
    internal string m_strText;
    private ArrayList m_lChildren;
    internal ArrayList m_lAttributes;
    internal SecurityElementType m_type;
    private const string s_strIndent = "   ";
    private const int c_AttributesTypical = 8;
    private const int c_ChildrenTypical = 1;

    /// <summary>获取或设置 XML 元素的标记名称。</summary>
    /// <returns>XML 元素的标记名称。</returns>
    /// <exception cref="T:System.ArgumentNullException">标记为 null。</exception>
    /// <exception cref="T:System.ArgumentException">标记在 XML 中无效。</exception>
    public string Tag
    {
      get
      {
        return this.m_strTag;
      }
      set
      {
        if (value == null)
          throw new ArgumentNullException("Tag");
        if (!SecurityElement.IsValidTag(value))
          throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_InvalidElementTag"), (object) value));
        this.m_strTag = value;
      }
    }

    /// <summary>以名称/值对形式获取或设置 XML 元素特性。</summary>
    /// <returns>XML 元素特性值的 <see cref="T:System.Collections.Hashtable" /> 对象。</returns>
    /// <exception cref="T:System.InvalidCastException">
    /// <see cref="T:System.Collections.Hashtable" /> 对象的名称或值无效。</exception>
    /// <exception cref="T:System.ArgumentException">该名称不是有效的 XML 特性名称。</exception>
    public Hashtable Attributes
    {
      get
      {
        if (this.m_lAttributes == null || this.m_lAttributes.Count == 0)
          return (Hashtable) null;
        Hashtable hashtable = new Hashtable(this.m_lAttributes.Count / 2);
        int count = this.m_lAttributes.Count;
        int index = 0;
        while (index < count)
        {
          hashtable.Add(this.m_lAttributes[index], this.m_lAttributes[index + 1]);
          index += 2;
        }
        return hashtable;
      }
      set
      {
        if (value == null || value.Count == 0)
        {
          this.m_lAttributes = (ArrayList) null;
        }
        else
        {
          ArrayList arrayList = new ArrayList(value.Count);
          IDictionaryEnumerator enumerator = value.GetEnumerator();
          while (enumerator.MoveNext())
          {
            string name = (string) enumerator.Key;
            string str = (string) enumerator.Value;
            if (!SecurityElement.IsValidAttributeName(name))
              throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_InvalidElementName"), (object) (string) enumerator.Current));
            if (!SecurityElement.IsValidAttributeValue(str))
              throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_InvalidElementValue"), (object) (string) enumerator.Value));
            arrayList.Add((object) name);
            arrayList.Add((object) str);
          }
          this.m_lAttributes = arrayList;
        }
      }
    }

    /// <summary>获取或设置 XML 元素中的文本。</summary>
    /// <returns>XML 元素中文本的值。</returns>
    /// <exception cref="T:System.ArgumentException">文本在 XML 中无效。</exception>
    public string Text
    {
      get
      {
        return SecurityElement.Unescape(this.m_strText);
      }
      set
      {
        if (value == null)
        {
          this.m_strText = (string) null;
        }
        else
        {
          if (!SecurityElement.IsValidText(value))
            throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_InvalidElementTag"), (object) value));
          this.m_strText = value;
        }
      }
    }

    /// <summary>获取或设置 XML 元素子元素的数组。</summary>
    /// <returns>XML 元素中作为安全元素的已排序的子元素。</returns>
    /// <exception cref="T:System.ArgumentException">XML 父节点的子级是 null。</exception>
    public ArrayList Children
    {
      get
      {
        this.ConvertSecurityElementFactories();
        return this.m_lChildren;
      }
      set
      {
        if (value != null)
        {
          foreach (object obj in value)
          {
            if (obj == null)
              throw new ArgumentException(Environment.GetResourceString("ArgumentNull_Child"));
          }
        }
        this.m_lChildren = value;
      }
    }

    internal ArrayList InternalChildren
    {
      get
      {
        return this.m_lChildren;
      }
    }

    internal SecurityElement()
    {
    }

    /// <summary>使用指定的标记初始化 <see cref="T:System.Security.SecurityElement" /> 类的新实例。</summary>
    /// <param name="tag">XML 元素的标记名称。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="tag" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="tag" /> 参数在 XML 中无效。</exception>
    public SecurityElement(string tag)
    {
      if (tag == null)
        throw new ArgumentNullException("tag");
      if (!SecurityElement.IsValidTag(tag))
        throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_InvalidElementTag"), (object) tag));
      this.m_strTag = tag;
      this.m_strText = (string) null;
    }

    /// <summary>用指定的标记和文本初始化 <see cref="T:System.Security.SecurityElement" /> 类的新实例。</summary>
    /// <param name="tag">XML 元素的标记名称。</param>
    /// <param name="text">元素中的文本内容。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="tag" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="tag" /> 参数或 <paramref name="text" /> 参数在 XML 中无效。</exception>
    public SecurityElement(string tag, string text)
    {
      if (tag == null)
        throw new ArgumentNullException("tag");
      if (!SecurityElement.IsValidTag(tag))
        throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_InvalidElementTag"), (object) tag));
      if (text != null && !SecurityElement.IsValidText(text))
        throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_InvalidElementText"), (object) text));
      this.m_strTag = tag;
      this.m_strText = text;
    }

    SecurityElement ISecurityElementFactory.CreateSecurityElement()
    {
      return this;
    }

    string ISecurityElementFactory.GetTag()
    {
      return this.Tag;
    }

    object ISecurityElementFactory.Copy()
    {
      return (object) this.Copy();
    }

    string ISecurityElementFactory.Attribute(string attributeName)
    {
      return this.Attribute(attributeName);
    }

    /// <summary>用 XML 编码字符串创建安全元素。</summary>
    /// <returns>用 XML 创建的 <see cref="T:System.Security.SecurityElement" />。</returns>
    /// <param name="xml">用来创建安全元素的 XML 编码字符串。</param>
    /// <exception cref="T:System.Security.XmlSyntaxException">
    /// <paramref name="xml" /> 包含一个或多个单引号字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="xml" /> 为  null。</exception>
    public static SecurityElement FromString(string xml)
    {
      if (xml == null)
        throw new ArgumentNullException("xml");
      return new Parser(xml).GetTopElement();
    }

    internal void ConvertSecurityElementFactories()
    {
      if (this.m_lChildren == null)
        return;
      for (int index = 0; index < this.m_lChildren.Count; ++index)
      {
        ISecurityElementFactory securityElementFactory = this.m_lChildren[index] as ISecurityElementFactory;
        if (securityElementFactory != null && !(this.m_lChildren[index] is SecurityElement))
          this.m_lChildren[index] = (object) securityElementFactory.CreateSecurityElement();
      }
    }

    internal void AddAttributeSafe(string name, string value)
    {
      if (this.m_lAttributes == null)
      {
        this.m_lAttributes = new ArrayList(8);
      }
      else
      {
        int count = this.m_lAttributes.Count;
        int index = 0;
        while (index < count)
        {
          if (string.Equals((string) this.m_lAttributes[index], name))
            throw new ArgumentException(Environment.GetResourceString("Argument_AttributeNamesMustBeUnique"));
          index += 2;
        }
      }
      this.m_lAttributes.Add((object) name);
      this.m_lAttributes.Add((object) value);
    }

    /// <summary>将名称/值特性添加到 XML 元素。</summary>
    /// <param name="name">属性名。</param>
    /// <param name="value">属性的值。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 参数或 <paramref name="value" /> 参数是 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="name" /> 参数或 <paramref name="value" /> 参数在 XML 中无效。- 或 -具有由 <paramref name="name" /> 参数指定的名称的特性已存在。</exception>
    public void AddAttribute(string name, string value)
    {
      if (name == null)
        throw new ArgumentNullException("name");
      if (value == null)
        throw new ArgumentNullException("value");
      if (!SecurityElement.IsValidAttributeName(name))
        throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_InvalidElementName"), (object) name));
      if (!SecurityElement.IsValidAttributeValue(value))
        throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_InvalidElementValue"), (object) value));
      this.AddAttributeSafe(name, value);
    }

    /// <summary>将子元素添加到 XML 元素。</summary>
    /// <param name="child">要添加的子元素。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="child" /> 参数为 null。</exception>
    public void AddChild(SecurityElement child)
    {
      if (child == null)
        throw new ArgumentNullException("child");
      if (this.m_lChildren == null)
        this.m_lChildren = new ArrayList(1);
      this.m_lChildren.Add((object) child);
    }

    internal void AddChild(ISecurityElementFactory child)
    {
      if (child == null)
        throw new ArgumentNullException("child");
      if (this.m_lChildren == null)
        this.m_lChildren = new ArrayList(1);
      this.m_lChildren.Add((object) child);
    }

    internal void AddChildNoDuplicates(ISecurityElementFactory child)
    {
      if (child == null)
        throw new ArgumentNullException("child");
      if (this.m_lChildren == null)
      {
        this.m_lChildren = new ArrayList(1);
        this.m_lChildren.Add((object) child);
      }
      else
      {
        for (int index = 0; index < this.m_lChildren.Count; ++index)
        {
          if (this.m_lChildren[index] == child)
            return;
        }
        this.m_lChildren.Add((object) child);
      }
    }

    /// <summary>比较两个 XML 元素对象，确定它们是否相等。</summary>
    /// <returns>如果当前 XML 元素中的标记、特性名和值、子元素以及文本字段与 <paramref name="other" /> 参数中的对等部分相同，则为 true；否则为 false。</returns>
    /// <param name="other">要与当前 XML 元素对象进行比较的 XML 元素对象。</param>
    public bool Equal(SecurityElement other)
    {
      if (other == null || !string.Equals(this.m_strTag, other.m_strTag) || !string.Equals(this.m_strText, other.m_strText))
        return false;
      if (this.m_lAttributes == null || other.m_lAttributes == null)
      {
        if (this.m_lAttributes != other.m_lAttributes)
          return false;
      }
      else
      {
        int count = this.m_lAttributes.Count;
        if (count != other.m_lAttributes.Count)
          return false;
        for (int index = 0; index < count; ++index)
        {
          if (!string.Equals((string) this.m_lAttributes[index], (string) other.m_lAttributes[index]))
            return false;
        }
      }
      if (this.m_lChildren == null || other.m_lChildren == null)
      {
        if (this.m_lChildren != other.m_lChildren)
          return false;
      }
      else
      {
        if (this.m_lChildren.Count != other.m_lChildren.Count)
          return false;
        this.ConvertSecurityElementFactories();
        other.ConvertSecurityElementFactories();
        IEnumerator enumerator1 = this.m_lChildren.GetEnumerator();
        IEnumerator enumerator2 = other.m_lChildren.GetEnumerator();
        while (enumerator1.MoveNext())
        {
          enumerator2.MoveNext();
          SecurityElement securityElement = (SecurityElement) enumerator1.Current;
          SecurityElement other1 = (SecurityElement) enumerator2.Current;
          if (securityElement == null || !securityElement.Equal(other1))
            return false;
        }
      }
      return true;
    }

    /// <summary>创建并返回当前 <see cref="T:System.Security.SecurityElement" /> 对象的一个相同副本。</summary>
    /// <returns>当前 <see cref="T:System.Security.SecurityElement" /> 对象的副本。</returns>
    [ComVisible(false)]
    public SecurityElement Copy()
    {
      return new SecurityElement(this.m_strTag, this.m_strText) { m_lChildren = this.m_lChildren == null ? (ArrayList) null : new ArrayList((ICollection) this.m_lChildren), m_lAttributes = this.m_lAttributes == null ? (ArrayList) null : new ArrayList((ICollection) this.m_lAttributes) };
    }

    /// <summary>确定字符串是否是有效的标记。</summary>
    /// <returns>如果 <paramref name="tag" /> 参数是有效的 XML 标记，则为 true；否则为 false。</returns>
    /// <param name="tag">要测试其有效性的标记。</param>
    public static bool IsValidTag(string tag)
    {
      if (tag == null)
        return false;
      return tag.IndexOfAny(SecurityElement.s_tagIllegalCharacters) == -1;
    }

    /// <summary>确定字符串是否是 XML 元素中的有效文本。</summary>
    /// <returns>如果 <paramref name="text" /> 参数是有效的 XML 文本元素，则为 true；否则为 false。</returns>
    /// <param name="text">要测试其有效性的文本。</param>
    public static bool IsValidText(string text)
    {
      if (text == null)
        return false;
      return text.IndexOfAny(SecurityElement.s_textIllegalCharacters) == -1;
    }

    /// <summary>确定字符串是否是有效的特性名。</summary>
    /// <returns>如果 <paramref name="name" /> 参数是有效的 XML 特性名，则为 true；否则为 false。</returns>
    /// <param name="name">要测试其有效性的特性名。</param>
    public static bool IsValidAttributeName(string name)
    {
      return SecurityElement.IsValidTag(name);
    }

    /// <summary>确定字符串是否是有效的特性值。</summary>
    /// <returns>如果 <paramref name="value" /> 参数是有效的 XML 特性值，则为 true；否则为 false。</returns>
    /// <param name="value">要测试其有效性的特性值。</param>
    public static bool IsValidAttributeValue(string value)
    {
      if (value == null)
        return false;
      return value.IndexOfAny(SecurityElement.s_valueIllegalCharacters) == -1;
    }

    private static string GetEscapeSequence(char c)
    {
      int length = SecurityElement.s_escapeStringPairs.Length;
      int index1 = 0;
      while (index1 < length)
      {
        string str1 = SecurityElement.s_escapeStringPairs[index1];
        string str2 = SecurityElement.s_escapeStringPairs[index1 + 1];
        int index2 = 0;
        if ((int) str1[index2] == (int) c)
          return str2;
        index1 += 2;
      }
      return c.ToString();
    }

    /// <summary>将字符串中的无效 XML 字符替换为与其等效的有效 XML 字符。</summary>
    /// <returns>包含无效字符的输入字符串被替换。</returns>
    /// <param name="str">要对其中的无效字符进行转义的字符串。</param>
    public static string Escape(string str)
    {
      if (str == null)
        return (string) null;
      StringBuilder stringBuilder = (StringBuilder) null;
      int length = str.Length;
      int startIndex = 0;
      while (true)
      {
        int index = str.IndexOfAny(SecurityElement.s_escapeChars, startIndex);
        if (index != -1)
        {
          if (stringBuilder == null)
            stringBuilder = new StringBuilder();
          stringBuilder.Append(str, startIndex, index - startIndex);
          stringBuilder.Append(SecurityElement.GetEscapeSequence(str[index]));
          startIndex = index + 1;
        }
        else
          break;
      }
      if (stringBuilder == null)
        return str;
      stringBuilder.Append(str, startIndex, length - startIndex);
      return stringBuilder.ToString();
    }

    private static string GetUnescapeSequence(string str, int index, out int newIndex)
    {
      int num = str.Length - index;
      int length1 = SecurityElement.s_escapeStringPairs.Length;
      int index1 = 0;
      while (index1 < length1)
      {
        string str1 = SecurityElement.s_escapeStringPairs[index1];
        string strA = SecurityElement.s_escapeStringPairs[index1 + 1];
        int length2 = strA.Length;
        if (length2 <= num && string.Compare(strA, 0, str, index, length2, StringComparison.Ordinal) == 0)
        {
          newIndex = index + strA.Length;
          return str1;
        }
        index1 += 2;
      }
      newIndex = index + 1;
      return str[index].ToString();
    }

    private static string Unescape(string str)
    {
      if (str == null)
        return (string) null;
      StringBuilder stringBuilder = (StringBuilder) null;
      int length = str.Length;
      int newIndex = 0;
      while (true)
      {
        int index = str.IndexOf('&', newIndex);
        if (index != -1)
        {
          if (stringBuilder == null)
            stringBuilder = new StringBuilder();
          stringBuilder.Append(str, newIndex, index - newIndex);
          stringBuilder.Append(SecurityElement.GetUnescapeSequence(str, index, out newIndex));
        }
        else
          break;
      }
      if (stringBuilder == null)
        return str;
      stringBuilder.Append(str, newIndex, length - newIndex);
      return stringBuilder.ToString();
    }

    private static void ToStringHelperStringBuilder(object obj, string str)
    {
      ((StringBuilder) obj).Append(str);
    }

    private static void ToStringHelperStreamWriter(object obj, string str)
    {
      ((TextWriter) obj).Write(str);
    }

    /// <summary>产生 XML 元素及其构成特性、子元素和文本的字符串表示法。</summary>
    /// <returns>XML 元素及其内容。</returns>
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      this.ToString("", (object) stringBuilder, new SecurityElement.ToStringHelperFunc(SecurityElement.ToStringHelperStringBuilder));
      return stringBuilder.ToString();
    }

    internal void ToWriter(StreamWriter writer)
    {
      this.ToString("", (object) writer, new SecurityElement.ToStringHelperFunc(SecurityElement.ToStringHelperStreamWriter));
    }

    private void ToString(string indent, object obj, SecurityElement.ToStringHelperFunc func)
    {
      func(obj, "<");
      switch (this.m_type)
      {
        case SecurityElementType.Format:
          func(obj, "?");
          break;
        case SecurityElementType.Comment:
          func(obj, "!");
          break;
      }
      func(obj, this.m_strTag);
      if (this.m_lAttributes != null && this.m_lAttributes.Count > 0)
      {
        func(obj, " ");
        int count = this.m_lAttributes.Count;
        int index = 0;
        while (index < count)
        {
          string str1 = (string) this.m_lAttributes[index];
          string str2 = (string) this.m_lAttributes[index + 1];
          func(obj, str1);
          func(obj, "=\"");
          func(obj, str2);
          func(obj, "\"");
          if (index != this.m_lAttributes.Count - 2)
          {
            if (this.m_type == SecurityElementType.Regular)
              func(obj, Environment.NewLine);
            else
              func(obj, " ");
          }
          index += 2;
        }
      }
      if (this.m_strText == null && (this.m_lChildren == null || this.m_lChildren.Count == 0))
      {
        switch (this.m_type)
        {
          case SecurityElementType.Format:
            func(obj, " ?>");
            break;
          case SecurityElementType.Comment:
            func(obj, ">");
            break;
          default:
            func(obj, "/>");
            break;
        }
        func(obj, Environment.NewLine);
      }
      else
      {
        func(obj, ">");
        func(obj, this.m_strText);
        if (this.m_lChildren != null)
        {
          this.ConvertSecurityElementFactories();
          func(obj, Environment.NewLine);
          for (int index = 0; index < this.m_lChildren.Count; ++index)
            ((SecurityElement) this.m_lChildren[index]).ToString("", obj, func);
        }
        func(obj, "</");
        func(obj, this.m_strTag);
        func(obj, ">");
        func(obj, Environment.NewLine);
      }
    }

    /// <summary>根据名称在 XML 元素中查找特性。</summary>
    /// <returns>与命名特性相关的值，或者，如果不存在与 <paramref name="name" /> 相关的特性，则为 null。</returns>
    /// <param name="name">要搜索的特性名。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 参数为 null。</exception>
    public string Attribute(string name)
    {
      if (name == null)
        throw new ArgumentNullException("name");
      if (this.m_lAttributes == null)
        return (string) null;
      int count = this.m_lAttributes.Count;
      int index = 0;
      while (index < count)
      {
        if (string.Equals((string) this.m_lAttributes[index], name))
          return SecurityElement.Unescape((string) this.m_lAttributes[index + 1]);
        index += 2;
      }
      return (string) null;
    }

    /// <summary>根据标记名查找子级。</summary>
    /// <returns>具有指定标记值的第一个子 XML 元素，或者，如果具有 <paramref name="tag" /> 的子元素不存在，则为 null。</returns>
    /// <param name="tag">要在子元素中搜索的标记。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="tag" /> 参数为 null。</exception>
    public SecurityElement SearchForChildByTag(string tag)
    {
      if (tag == null)
        throw new ArgumentNullException("tag");
      if (this.m_lChildren == null)
        return (SecurityElement) null;
      foreach (SecurityElement mLChild in this.m_lChildren)
      {
        if (mLChild != null && string.Equals(mLChild.Tag, tag))
          return mLChild;
      }
      return (SecurityElement) null;
    }

    internal IPermission ToPermission(bool ignoreTypeLoadFailures)
    {
      IPermission permission = XMLUtil.CreatePermission(this, PermissionState.None, ignoreTypeLoadFailures);
      if (permission == null)
        return (IPermission) null;
      permission.FromXml(this);
      PermissionToken.GetToken(permission);
      return permission;
    }

    [SecurityCritical]
    internal object ToSecurityObject()
    {
      if (!(this.m_strTag == "PermissionSet"))
        return (object) this.ToPermission(false);
      PermissionSet permissionSet = new PermissionSet(PermissionState.None);
      permissionSet.FromXml(this);
      return (object) permissionSet;
    }

    internal string SearchForTextOfLocalName(string strLocalName)
    {
      if (strLocalName == null)
        throw new ArgumentNullException("strLocalName");
      if (this.m_strTag == null)
        return (string) null;
      if (this.m_strTag.Equals(strLocalName) || this.m_strTag.EndsWith(":" + strLocalName, StringComparison.Ordinal))
        return SecurityElement.Unescape(this.m_strText);
      if (this.m_lChildren == null)
        return (string) null;
      foreach (SecurityElement mLChild in this.m_lChildren)
      {
        string str = mLChild.SearchForTextOfLocalName(strLocalName);
        if (str != null)
          return str;
      }
      return (string) null;
    }

    /// <summary>根据标记名查找子级并返回所包含的文本。</summary>
    /// <returns>具有指定标记值的第一个子元素的文本内容。</returns>
    /// <param name="tag">要在子元素中搜索的标记。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="tag" /> 为 null。</exception>
    public string SearchForTextOfTag(string tag)
    {
      if (tag == null)
        throw new ArgumentNullException("tag");
      if (string.Equals(this.m_strTag, tag))
        return SecurityElement.Unescape(this.m_strText);
      if (this.m_lChildren == null)
        return (string) null;
      IEnumerator enumerator = this.m_lChildren.GetEnumerator();
      this.ConvertSecurityElementFactories();
      while (enumerator.MoveNext())
      {
        string str = ((SecurityElement) enumerator.Current).SearchForTextOfTag(tag);
        if (str != null)
          return str;
      }
      return (string) null;
    }

    private delegate void ToStringHelperFunc(object obj, string str);
  }
}
