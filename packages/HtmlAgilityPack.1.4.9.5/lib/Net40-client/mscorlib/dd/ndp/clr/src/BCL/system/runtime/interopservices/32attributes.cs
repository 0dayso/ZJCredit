// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.AutomationProxyAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>指定是否应该使用自动化封送拆收器或自定义代理及存根 (Stub) 对该类型进行封送处理。</summary>
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Interface, Inherited = false)]
  [ComVisible(true)]
  public sealed class AutomationProxyAttribute : Attribute
  {
    internal bool _val;

    /// <summary>获取一个值，该值指示要使用的封送拆收器的类型。</summary>
    /// <returns>如果应使用“自动化封送拆收器”封送类，则为 true；如果应使用代理存根 (proxy stub) 封送拆收器，则为 false。</returns>
    public bool Value
    {
      get
      {
        return this._val;
      }
    }

    /// <summary>初始化 <see cref="T:System.Runtime.InteropServices.AutomationProxyAttribute" /> 类的新实例。</summary>
    /// <param name="val">如果应使用“自动化封送拆收器”封送类，则为 true；如果应使用代理存根 (proxy stub) 封送拆收器，则为 false。</param>
    public AutomationProxyAttribute(bool val)
    {
      this._val = val;
    }
  }
}
