// Decompiled with JetBrains decompiler
// Type: System.IO.IsolatedStorage.INormalizeForIsolatedStorage
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.IO.IsolatedStorage
{
  /// <summary>启用独立存储和应用程序域与程序集的证据之间的比较。</summary>
  [ComVisible(true)]
  public interface INormalizeForIsolatedStorage
  {
    /// <summary>当在派生类中重写时，返回在其上调用它的对象的正常化副本。</summary>
    /// <returns>一个正常化的对象，它表示在其上调用该方法的实例。该实例可以是字符串、流或任何可序列化的对象。</returns>
    object Normalize();
  }
}
