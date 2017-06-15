// Decompiled with JetBrains decompiler
// Type: System.Resources.ResourceLocator
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Resources
{
  internal struct ResourceLocator
  {
    internal object _value;
    internal int _dataPos;

    internal int DataPosition
    {
      get
      {
        return this._dataPos;
      }
    }

    internal object Value
    {
      get
      {
        return this._value;
      }
      set
      {
        this._value = value;
      }
    }

    internal ResourceLocator(int dataPos, object value)
    {
      this._dataPos = dataPos;
      this._value = value;
    }

    internal static bool CanCache(ResourceTypeCode value)
    {
      return value <= ResourceTypeCode.TimeSpan;
    }
  }
}
