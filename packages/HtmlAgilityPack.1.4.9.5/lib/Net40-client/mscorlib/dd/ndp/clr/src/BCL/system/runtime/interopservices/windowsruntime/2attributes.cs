// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.InterfaceImplementedInVersionAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices.WindowsRuntime
{
  /// <summary>指定首次实现指定接口的目标类型版本。</summary>
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true, Inherited = false)]
  [__DynamicallyInvokable]
  public sealed class InterfaceImplementedInVersionAttribute : Attribute
  {
    private Type m_interfaceType;
    private byte m_majorVersion;
    private byte m_minorVersion;
    private byte m_buildVersion;
    private byte m_revisionVersion;

    /// <summary>获取目标类型实现的接口的类型。</summary>
    /// <returns>接口的类型。</returns>
    [__DynamicallyInvokable]
    public Type InterfaceType
    {
      [__DynamicallyInvokable] get
      {
        return this.m_interfaceType;
      }
    }

    /// <summary>获取首次实现该接口的目标类型的主版本号。</summary>
    /// <returns>版本的主要组件。</returns>
    [__DynamicallyInvokable]
    public byte MajorVersion
    {
      [__DynamicallyInvokable] get
      {
        return this.m_majorVersion;
      }
    }

    /// <summary>获取首次实现该接口的目标类型的次版本号。</summary>
    /// <returns>版本的次要部分。</returns>
    [__DynamicallyInvokable]
    public byte MinorVersion
    {
      [__DynamicallyInvokable] get
      {
        return this.m_minorVersion;
      }
    }

    /// <summary>获取首次实现接口的目标类型版本的生成组件。</summary>
    /// <returns>版本的生成组件。</returns>
    [__DynamicallyInvokable]
    public byte BuildVersion
    {
      [__DynamicallyInvokable] get
      {
        return this.m_buildVersion;
      }
    }

    /// <summary>获取首次实现该接口的目标类型的修订本号。</summary>
    /// <returns>版本的版本组件。</returns>
    [__DynamicallyInvokable]
    public byte RevisionVersion
    {
      [__DynamicallyInvokable] get
      {
        return this.m_revisionVersion;
      }
    }

    /// <summary>初始化 <see cref="T:System.Runtime.InteropServices.WindowsRuntime.InterfaceImplementedInVersionAttribute" /> 类的新实例，该实例指定目标类型实现的接口和第一个接口实现的版本。</summary>
    /// <param name="interfaceType">在指定目标类型版本中首次实现的接口。</param>
    /// <param name="majorVersion">首次实现的 <paramref name="interfaceType" /> 的目标类型的版本的主要组件。</param>
    /// <param name="minorVersion">首次实现 <paramref name="interfaceType" /> 的目标类型的版本的次要组件。</param>
    /// <param name="buildVersion">首次实现 <paramref name="interfaceType" /> 的目标类型的版本的生成组件。</param>
    /// <param name="revisionVersion">首次实现 <paramref name="interfaceType" /> 的目标类型的版本的修订组件。</param>
    [__DynamicallyInvokable]
    public InterfaceImplementedInVersionAttribute(Type interfaceType, byte majorVersion, byte minorVersion, byte buildVersion, byte revisionVersion)
    {
      this.m_interfaceType = interfaceType;
      this.m_majorVersion = majorVersion;
      this.m_minorVersion = minorVersion;
      this.m_buildVersion = buildVersion;
      this.m_revisionVersion = revisionVersion;
    }
  }
}
