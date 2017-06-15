// Decompiled with JetBrains decompiler
// Type: System.ICloneable
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System
{
  /// <summary>支持克隆，即用与现有实例相同的值创建类的新实例。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  public interface ICloneable
  {
    /// <summary>创建作为当前实例副本的新对象。</summary>
    /// <returns>作为此实例副本的新对象。</returns>
    /// <filterpriority>2</filterpriority>
    object Clone();
  }
}
