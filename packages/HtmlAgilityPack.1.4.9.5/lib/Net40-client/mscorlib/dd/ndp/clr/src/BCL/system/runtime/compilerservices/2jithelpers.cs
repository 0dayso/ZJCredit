// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.StackCrawlMarkHandle
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.CompilerServices
{
  internal struct StackCrawlMarkHandle
  {
    private IntPtr m_ptr;

    internal StackCrawlMarkHandle(IntPtr stackMark)
    {
      this.m_ptr = stackMark;
    }
  }
}
