// Decompiled with JetBrains decompiler
// Type: System.IFormatProvider
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System
{
  /// <summary>提供用于检索控制格式化的对象的机制。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public interface IFormatProvider
  {
    /// <summary>返回一个对象，该对象为指定类型提供格式设置服务。</summary>
    /// <returns>如果 <see cref="T:System.IFormatProvider" /> 实现能够提供该类型的对象，则为 <paramref name="formatType" /> 所指定对象的实例；否则为 null。</returns>
    /// <param name="formatType">一个对象，该对象指定要返回的格式对象的类型。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    object GetFormat(Type formatType);
  }
}
