// Decompiled with JetBrains decompiler
// Type: System.Globalization.CodePageDataItem
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;

namespace System.Globalization
{
  [Serializable]
  internal class CodePageDataItem
  {
    internal int m_dataIndex;
    internal int m_uiFamilyCodePage;
    internal string m_webName;
    internal string m_headerName;
    internal string m_bodyName;
    internal uint m_flags;

    public unsafe string WebName
    {
      [SecuritySafeCritical] get
      {
        if (this.m_webName == null)
          this.m_webName = CodePageDataItem.CreateString(EncodingTable.codePageDataPtr[this.m_dataIndex].Names, 0U);
        return this.m_webName;
      }
    }

    public virtual int UIFamilyCodePage
    {
      get
      {
        return this.m_uiFamilyCodePage;
      }
    }

    public unsafe string HeaderName
    {
      [SecuritySafeCritical] get
      {
        if (this.m_headerName == null)
          this.m_headerName = CodePageDataItem.CreateString(EncodingTable.codePageDataPtr[this.m_dataIndex].Names, 1U);
        return this.m_headerName;
      }
    }

    public unsafe string BodyName
    {
      [SecuritySafeCritical] get
      {
        if (this.m_bodyName == null)
          this.m_bodyName = CodePageDataItem.CreateString(EncodingTable.codePageDataPtr[this.m_dataIndex].Names, 2U);
        return this.m_bodyName;
      }
    }

    public uint Flags
    {
      get
      {
        return this.m_flags;
      }
    }

    [SecurityCritical]
    internal unsafe CodePageDataItem(int dataIndex)
    {
      this.m_dataIndex = dataIndex;
      this.m_uiFamilyCodePage = (int) EncodingTable.codePageDataPtr[dataIndex].uiFamilyCodePage;
      this.m_flags = EncodingTable.codePageDataPtr[dataIndex].flags;
    }

    [SecurityCritical]
    internal static unsafe string CreateString(sbyte* pStrings, uint index)
    {
      if ((int) *pStrings == 124)
      {
        int startIndex = 1;
        int index1 = 1;
        while (true)
        {
          sbyte num = pStrings[index1];
          switch (num)
          {
            case 124:
            case 0:
              if ((int) index != 0)
              {
                --index;
                startIndex = index1 + 1;
                if ((int) num == 0)
                  goto label_7;
                else
                  break;
              }
              else
                goto label_4;
          }
          ++index1;
        }
label_4:
        return new string(pStrings, startIndex, index1 - startIndex);
label_7:
        throw new ArgumentException("pStrings");
      }
      return new string(pStrings);
    }
  }
}
