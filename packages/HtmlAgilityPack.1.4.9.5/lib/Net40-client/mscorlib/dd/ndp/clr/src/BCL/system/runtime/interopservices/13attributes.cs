// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ImportedFromTypeLibAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>指示在程序集内定义的类型原来在类型库中定义。</summary>
  [AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
  [ComVisible(true)]
  public sealed class ImportedFromTypeLibAttribute : Attribute
  {
    internal string _val;

    /// <summary>获取原始类型库文件的名称。</summary>
    /// <returns>原始类型库文件的名称。</returns>
    public string Value
    {
      get
      {
        return this._val;
      }
    }

    /// <summary>用原始类型库文件的名称初始化 <see cref="T:System.Runtime.InteropServices.ImportedFromTypeLibAttribute" /> 类的新实例。</summary>
    /// <param name="tlbFile">原始类型库文件的位置。</param>
    public ImportedFromTypeLibAttribute(string tlbFile)
    {
      this._val = tlbFile;
    }
  }
}
