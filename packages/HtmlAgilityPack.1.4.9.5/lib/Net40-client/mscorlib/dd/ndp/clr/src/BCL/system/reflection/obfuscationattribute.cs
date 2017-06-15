// Decompiled with JetBrains decompiler
// Type: System.Reflection.ObfuscationAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>指示模糊处理工具对程序集、类型或成员采取指定的操作。</summary>
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface | AttributeTargets.Parameter | AttributeTargets.Delegate, AllowMultiple = true, Inherited = false)]
  [ComVisible(true)]
  public sealed class ObfuscationAttribute : Attribute
  {
    private bool m_strip = true;
    private bool m_exclude = true;
    private bool m_applyToMembers = true;
    private string m_feature = "all";

    /// <summary>获取或设置一个 <see cref="T:System.Boolean" /> 值，该值指示模糊处理工具是否应在处理后移除此特性。</summary>
    /// <returns>如果模糊处理工具应在处理后移除该特性，则为 true；否则为 false。默认值为 true。</returns>
    public bool StripAfterObfuscation
    {
      get
      {
        return this.m_strip;
      }
      set
      {
        this.m_strip = value;
      }
    }

    /// <summary>获取或设置一个 <see cref="T:System.Boolean" /> 值，该值指示模糊处理工具是否应将类型或成员从模糊处理中排除。</summary>
    /// <returns>如果应用该特性的类型或成员应从模糊处理中排除，则为 true；否则为 false。默认值为 true。</returns>
    public bool Exclude
    {
      get
      {
        return this.m_exclude;
      }
      set
      {
        this.m_exclude = value;
      }
    }

    /// <summary>获取或设置一个 <see cref="T:System.Boolean" /> 值，该值指示某一类型的特性是否应用到该类型的成员。</summary>
    /// <returns>如果该特性要应用到类型的成员，则为 true；否则为 false。默认值为 true。</returns>
    public bool ApplyToMembers
    {
      get
      {
        return this.m_applyToMembers;
      }
      set
      {
        this.m_applyToMembers = value;
      }
    }

    /// <summary>获取或设置一个字符串值，该字符串值可由模糊处理工具识别并指定处理选项。</summary>
    /// <returns>一个字符串值，可由模糊处理工具识别并指定处理选项。默认为“all”。</returns>
    public string Feature
    {
      get
      {
        return this.m_feature;
      }
      set
      {
        this.m_feature = value;
      }
    }
  }
}
