// Decompiled with JetBrains decompiler
// Type: System.ConfigNodeType
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System
{
  [Serializable]
  internal enum ConfigNodeType
  {
    Element = 1,
    Attribute = 2,
    Pi = 3,
    XmlDecl = 4,
    DocType = 5,
    DTDAttribute = 6,
    EntityDecl = 7,
    ElementDecl = 8,
    AttlistDecl = 9,
    Notation = 10,
    Group = 11,
    IncludeSect = 12,
    PCData = 13,
    CData = 14,
    IgnoreSect = 15,
    Comment = 16,
    EntityRef = 17,
    Whitespace = 18,
    Name = 19,
    NMToken = 20,
    String = 21,
    Peref = 22,
    Model = 23,
    ATTDef = 24,
    ATTType = 25,
    ATTPresence = 26,
    DTDSubset = 27,
    LastNodeType = 28,
  }
}
