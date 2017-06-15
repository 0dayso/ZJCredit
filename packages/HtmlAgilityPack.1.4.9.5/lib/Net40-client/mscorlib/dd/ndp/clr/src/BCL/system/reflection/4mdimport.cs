// Decompiled with JetBrains decompiler
// Type: System.Reflection.MetadataException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;

namespace System.Reflection
{
  internal class MetadataException : Exception
  {
    private int m_hr;

    internal MetadataException(int hr)
    {
      this.m_hr = hr;
    }

    public override string ToString()
    {
      return string.Format((IFormatProvider) CultureInfo.CurrentCulture, "MetadataException HResult = {0:x}.", (object) this.m_hr);
    }
  }
}
