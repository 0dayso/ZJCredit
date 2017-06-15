// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.TCEAdapterGen.NameSpaceExtractor
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices.TCEAdapterGen
{
  internal static class NameSpaceExtractor
  {
    private static char NameSpaceSeperator = '.';

    public static string ExtractNameSpace(string FullyQualifiedTypeName)
    {
      int length = FullyQualifiedTypeName.LastIndexOf(NameSpaceExtractor.NameSpaceSeperator);
      if (length == -1)
        return "";
      return FullyQualifiedTypeName.Substring(0, length);
    }
  }
}
