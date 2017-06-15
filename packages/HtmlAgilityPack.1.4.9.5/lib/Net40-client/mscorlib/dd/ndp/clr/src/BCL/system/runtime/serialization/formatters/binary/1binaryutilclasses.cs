// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.Formatters.Binary.SerStack
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;

namespace System.Runtime.Serialization.Formatters.Binary
{
  internal sealed class SerStack
  {
    internal object[] objects = new object[5];
    internal int top = -1;
    internal string stackId;
    internal int next;

    internal SerStack()
    {
      this.stackId = "System";
    }

    internal SerStack(string stackId)
    {
      this.stackId = stackId;
    }

    internal void Push(object obj)
    {
      if (this.top == this.objects.Length - 1)
        this.IncreaseCapacity();
      object[] objArray = this.objects;
      int num = this.top + 1;
      this.top = num;
      int index = num;
      object obj1 = obj;
      objArray[index] = obj1;
    }

    internal object Pop()
    {
      if (this.top < 0)
        return (object) null;
      object obj = this.objects[this.top];
      object[] objArray = this.objects;
      int num = this.top;
      this.top = num - 1;
      int index = num;
      // ISSUE: variable of the null type
      __Null local = null;
      objArray[index] = (object) local;
      return obj;
    }

    internal void IncreaseCapacity()
    {
      object[] objArray = new object[this.objects.Length * 2];
      Array.Copy((Array) this.objects, 0, (Array) objArray, 0, this.objects.Length);
      this.objects = objArray;
    }

    internal object Peek()
    {
      if (this.top < 0)
        return (object) null;
      return this.objects[this.top];
    }

    internal object PeekPeek()
    {
      if (this.top < 1)
        return (object) null;
      return this.objects[this.top - 1];
    }

    internal int Count()
    {
      return this.top + 1;
    }

    internal bool IsEmpty()
    {
      return this.top <= 0;
    }

    [Conditional("SER_LOGGING")]
    internal void Dump()
    {
      for (int index = 0; index < this.Count(); ++index)
      {
        object obj = this.objects[index];
      }
    }
  }
}
