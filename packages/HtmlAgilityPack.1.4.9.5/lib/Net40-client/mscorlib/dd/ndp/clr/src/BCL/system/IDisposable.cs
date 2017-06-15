// Decompiled with JetBrains decompiler
// Type: System.IDisposable
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System
{
  /// <summary>提供一种用于释放非托管资源的机制。若要浏览此类型的 .NET Framework 源代码，请参阅引用源。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public interface IDisposable
  {
    /// <summary>执行与释放或重置非托管资源关联的应用程序定义的任务。</summary>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    void Dispose();
  }
}
