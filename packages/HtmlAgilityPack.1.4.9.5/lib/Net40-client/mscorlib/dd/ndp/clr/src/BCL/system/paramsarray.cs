// Decompiled with JetBrains decompiler
// Type: System.ParamsArray
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System
{
  internal struct ParamsArray
  {
    private static readonly object[] oneArgArray = new object[1];
    private static readonly object[] twoArgArray = new object[2];
    private static readonly object[] threeArgArray = new object[3];
    private readonly object arg0;
    private readonly object arg1;
    private readonly object arg2;
    private readonly object[] args;

    public int Length
    {
      get
      {
        return this.args.Length;
      }
    }

    public object this[int index]
    {
      get
      {
        if (index != 0)
          return this.GetAtSlow(index);
        return this.arg0;
      }
    }

    public ParamsArray(object arg0)
    {
      this.arg0 = arg0;
      this.arg1 = (object) null;
      this.arg2 = (object) null;
      this.args = ParamsArray.oneArgArray;
    }

    public ParamsArray(object arg0, object arg1)
    {
      this.arg0 = arg0;
      this.arg1 = arg1;
      this.arg2 = (object) null;
      this.args = ParamsArray.twoArgArray;
    }

    public ParamsArray(object arg0, object arg1, object arg2)
    {
      this.arg0 = arg0;
      this.arg1 = arg1;
      this.arg2 = arg2;
      this.args = ParamsArray.threeArgArray;
    }

    public ParamsArray(object[] args)
    {
      int length = args.Length;
      this.arg0 = length > 0 ? args[0] : (object) null;
      this.arg1 = length > 1 ? args[1] : (object) null;
      this.arg2 = length > 2 ? args[2] : (object) null;
      this.args = args;
    }

    private object GetAtSlow(int index)
    {
      if (index == 1)
        return this.arg1;
      if (index == 2)
        return this.arg2;
      return this.args[index];
    }
  }
}
