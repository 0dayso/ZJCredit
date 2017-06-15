// Decompiled with JetBrains decompiler
// Type: System.RuntimeArgumentHandle
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System
{
  /// <summary>引用变长参数列表。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  public struct RuntimeArgumentHandle
  {
    private IntPtr m_ptr;

    internal IntPtr Value
    {
      get
      {
        return this.m_ptr;
      }
    }
  }
}
