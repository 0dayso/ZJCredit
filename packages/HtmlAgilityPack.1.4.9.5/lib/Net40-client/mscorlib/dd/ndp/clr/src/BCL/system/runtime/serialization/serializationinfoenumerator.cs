// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.SerializationEntry
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
  /// <summary>包含序列化对象的 <see cref="T:System.Type" /> 值以及名称。</summary>
  [ComVisible(true)]
  public struct SerializationEntry
  {
    private Type m_type;
    private object m_value;
    private string m_name;

    /// <summary>获取对象中包含的值。</summary>
    /// <returns>对象中包含的值。</returns>
    public object Value
    {
      get
      {
        return this.m_value;
      }
    }

    /// <summary>获取对象的名称。</summary>
    /// <returns>对象的名称。</returns>
    public string Name
    {
      get
      {
        return this.m_name;
      }
    }

    /// <summary>获取对象的 <see cref="T:System.Type" />。</summary>
    /// <returns>对象的 <see cref="T:System.Type" />。</returns>
    public Type ObjectType
    {
      get
      {
        return this.m_type;
      }
    }

    internal SerializationEntry(string entryName, object entryValue, Type entryType)
    {
      this.m_value = entryValue;
      this.m_name = entryName;
      this.m_type = entryType;
    }
  }
}
