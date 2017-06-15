// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.CodeGroupStack
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;

namespace System.Security.Policy
{
  internal sealed class CodeGroupStack
  {
    private ArrayList m_array;

    internal CodeGroupStack()
    {
      this.m_array = new ArrayList();
    }

    internal void Push(CodeGroupStackFrame element)
    {
      this.m_array.Add((object) element);
    }

    internal CodeGroupStackFrame Pop()
    {
      if (this.IsEmpty())
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EmptyStack"));
      int count = this.m_array.Count;
      CodeGroupStackFrame codeGroupStackFrame = (CodeGroupStackFrame) this.m_array[count - 1];
      this.m_array.RemoveAt(count - 1);
      return codeGroupStackFrame;
    }

    internal bool IsEmpty()
    {
      return this.m_array.Count == 0;
    }
  }
}
