// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ProgIdAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>允许用户指定类的 ProgID。</summary>
  [AttributeUsage(AttributeTargets.Class, Inherited = false)]
  [ComVisible(true)]
  public sealed class ProgIdAttribute : Attribute
  {
    internal string _val;

    /// <summary>获取类的 ProgID。</summary>
    /// <returns>类的 ProgID。</returns>
    public string Value
    {
      get
      {
        return this._val;
      }
    }

    /// <summary>用指定的 ProgID 初始化 ProgIdAttribute 的新实例。</summary>
    /// <param name="progId">要分配给类的 ProgID。</param>
    public ProgIdAttribute(string progId)
    {
      this._val = progId;
    }
  }
}
