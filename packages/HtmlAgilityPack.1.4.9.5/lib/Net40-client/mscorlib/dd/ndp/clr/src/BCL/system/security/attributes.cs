// Decompiled with JetBrains decompiler
// Type: System.Security.AllowPartiallyTrustedCallersAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security
{
  /// <summary>允许部分受信任的代码调用某个程序集。如果没有此声明，则只有完全受信任的调用方才可以使用该程序集。此类不能被继承。</summary>
  [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class AllowPartiallyTrustedCallersAttribute : Attribute
  {
    private PartialTrustVisibilityLevel _visibilityLevel;

    /// <summary>获取或设置用 <see cref="T:System.Security.AllowPartiallyTrustedCallersAttribute" /> (APTCA) 特性标记的代码的默认部分信任可见性。</summary>
    /// <returns>枚举值之一。默认值为 <see cref="F:System.Security.PartialTrustVisibilityLevel.VisibleToAllHosts" />。</returns>
    public PartialTrustVisibilityLevel PartialTrustVisibilityLevel
    {
      get
      {
        return this._visibilityLevel;
      }
      set
      {
        this._visibilityLevel = value;
      }
    }

    /// <summary>初始化 <see cref="T:System.Security.AllowPartiallyTrustedCallersAttribute" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public AllowPartiallyTrustedCallersAttribute()
    {
    }
  }
}
