// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.DispIdAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>指定方法、字段或属性的 COM 调度标识符 (DISPID)。</summary>
  [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class DispIdAttribute : Attribute
  {
    internal int _val;

    /// <summary>获取成员的 DISPID。</summary>
    /// <returns>成员的 DISPID。</returns>
    [__DynamicallyInvokable]
    public int Value
    {
      [__DynamicallyInvokable] get
      {
        return this._val;
      }
    }

    /// <summary>用指定的 DISPID 初始化 DispIdAttribute 类的新实例。</summary>
    /// <param name="dispId">成员的 DISPID。</param>
    [__DynamicallyInvokable]
    public DispIdAttribute(int dispId)
    {
      this._val = dispId;
    }
  }
}
