// Decompiled with JetBrains decompiler
// Type: System.Text.EUCJPEncoding
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Security;

namespace System.Text
{
  [Serializable]
  internal class EUCJPEncoding : DBCSCodePageEncoding
  {
    [SecurityCritical]
    public EUCJPEncoding()
      : base(51932, 932)
    {
      this.m_bUseMlangTypeForSerialization = true;
    }

    [SecurityCritical]
    protected override unsafe string GetMemorySectionName()
    {
      return string.Format((IFormatProvider) CultureInfo.InvariantCulture, "CodePage_{0}_{1}_{2}_{3}_{4}_EUCJP", (object) (this.bFlagDataTable ? this.dataTableCodePage : this.CodePage), (object) this.pCodePage->VersionMajor, (object) this.pCodePage->VersionMinor, (object) this.pCodePage->VersionRevision, (object) this.pCodePage->VersionBuild);
    }

    protected override bool CleanUpBytes(ref int bytes)
    {
      if (bytes >= 256)
      {
        if (bytes >= 64064 && bytes <= 64587)
        {
          if (bytes >= 64064 && bytes <= 64091)
          {
            if (bytes <= 64073)
              bytes -= 2897;
            else if (bytes >= 64074 && bytes <= 64083)
              bytes -= 29430;
            else if (bytes >= 64084 && bytes <= 64087)
              bytes -= 2907;
            else if (bytes == 64088)
              bytes = 34698;
            else if (bytes == 64089)
              bytes = 34690;
            else if (bytes == 64090)
              bytes = 34692;
            else if (bytes == 64091)
              bytes = 34714;
          }
          else if (bytes >= 64092 && bytes <= 64587)
          {
            byte num = (byte) bytes;
            if ((int) num < 92)
              bytes -= 3423;
            else if ((int) num >= 128 && (int) num <= 155)
              bytes -= 3357;
            else
              bytes -= 3356;
          }
        }
        byte num1 = (byte) (bytes >> 8);
        byte num2 = (byte) bytes;
        byte num3 = (byte) (((int) (byte) ((int) num1 - ((int) num1 > 159 ? 177 : 113)) << 1) + 1);
        byte num4;
        if ((int) num2 > 158)
        {
          num4 = (byte) ((uint) num2 - 126U);
          ++num3;
        }
        else
        {
          if ((int) num2 > 126)
            --num2;
          num4 = (byte) ((uint) num2 - 31U);
        }
        bytes = (int) num3 << 8 | (int) num4 | 32896;
        if ((bytes & 65280) < 41216 || (bytes & 65280) > 65024 || ((bytes & (int) byte.MaxValue) < 161 || (bytes & (int) byte.MaxValue) > 254))
          return false;
      }
      else
      {
        if (bytes >= 161 && bytes <= 223)
        {
          bytes |= 36352;
          return true;
        }
        if (bytes >= 129 && bytes != 160 && bytes != (int) byte.MaxValue)
          return false;
      }
      return true;
    }

    [SecurityCritical]
    protected override unsafe void CleanUpEndBytes(char* chars)
    {
      for (int index = 161; index <= 254; ++index)
        chars[index] = '\xFFFE';
      chars[142] = '\xFFFE';
    }
  }
}
