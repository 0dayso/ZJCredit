// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.CodeGroupPositionMarker
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Security.Policy
{
  internal class CodeGroupPositionMarker
  {
    internal int elementIndex;
    internal int groupIndex;
    internal SecurityElement element;

    internal CodeGroupPositionMarker(int elementIndex, int groupIndex, SecurityElement element)
    {
      this.elementIndex = elementIndex;
      this.groupIndex = groupIndex;
      this.element = element;
    }
  }
}
