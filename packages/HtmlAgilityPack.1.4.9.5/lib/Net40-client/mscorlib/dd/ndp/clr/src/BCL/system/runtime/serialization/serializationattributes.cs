// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.OptionalFieldAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
  /// <summary>指定序列化流中可以缺少一个字段，这样 <see cref="T:System.Runtime.Serialization.Formatters.Binary.BinaryFormatter" /> 和 <see cref="T:System.Runtime.Serialization.Formatters.Soap.SoapFormatter" /> 就不会引发异常。</summary>
  [AttributeUsage(AttributeTargets.Field, Inherited = false)]
  [ComVisible(true)]
  public sealed class OptionalFieldAttribute : Attribute
  {
    private int versionAdded = 1;

    /// <summary>此属性未使用，并且被保留。</summary>
    /// <returns>此属性被保留。</returns>
    public int VersionAdded
    {
      get
      {
        return this.versionAdded;
      }
      set
      {
        if (value < 1)
          throw new ArgumentException(Environment.GetResourceString("Serialization_OptionalFieldVersionValue"));
        this.versionAdded = value;
      }
    }
  }
}
