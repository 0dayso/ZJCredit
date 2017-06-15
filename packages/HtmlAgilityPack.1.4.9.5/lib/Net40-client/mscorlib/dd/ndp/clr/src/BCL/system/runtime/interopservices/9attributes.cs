// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.LCIDConversionAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>指示方法的非托管签名需要区域设置标识符 (LCID) 参数。</summary>
  [AttributeUsage(AttributeTargets.Method, Inherited = false)]
  [ComVisible(true)]
  public sealed class LCIDConversionAttribute : Attribute
  {
    internal int _val;

    /// <summary>获取非托管签名中 LCID 参数的位置。</summary>
    /// <returns>非托管签名中 LCID 参数的位置，其中 0 是第一个参数。</returns>
    public int Value
    {
      get
      {
        return this._val;
      }
    }

    /// <summary>用非托管签名中 LCID 的位置初始化 LCIDConversionAttribute 类的新实例。</summary>
    /// <param name="lcid">指示非托管签名中 LCID 参数的位置，其中 0 是第一个参数。</param>
    public LCIDConversionAttribute(int lcid)
    {
      this._val = lcid;
    }
  }
}
