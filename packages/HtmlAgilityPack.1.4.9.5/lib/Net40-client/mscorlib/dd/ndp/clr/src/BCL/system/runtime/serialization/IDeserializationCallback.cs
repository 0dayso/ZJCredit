// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.IDeserializationCallback
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
  /// <summary>指示在完成整个对象图形的反序列化时通知类。注意当用 XmlSerializer (System.Xml.Serialization.XmlSerializer) 反序列化时，不调用此接口。</summary>
  [ComVisible(true)]
  public interface IDeserializationCallback
  {
    /// <summary>在整个对象图形已经反序列化时运行。</summary>
    /// <param name="sender">开始回调的对象。当前未实现该参数的功能。</param>
    void OnDeserialization(object sender);
  }
}
