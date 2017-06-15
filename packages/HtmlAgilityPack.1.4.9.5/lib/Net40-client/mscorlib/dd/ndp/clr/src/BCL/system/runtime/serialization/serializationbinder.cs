// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.SerializationBinder
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
  /// <summary>允许用户控制类加载并指定要加载的类。</summary>
  [ComVisible(true)]
  [Serializable]
  public abstract class SerializationBinder
  {
    /// <summary>当在派生类中重写时，控制将序列化对象绑定到类型的过程。</summary>
    /// <param name="serializedType">格式化程序为其创建新实例的对象的类型。</param>
    /// <param name="assemblyName">指定序列化对象的 <see cref="T:System.Reflection.Assembly" /> 名称。</param>
    /// <param name="typeName">指定序列化对象的 <see cref="T:System.Type" /> 名称。</param>
    public virtual void BindToName(Type serializedType, out string assemblyName, out string typeName)
    {
      assemblyName = (string) null;
      typeName = (string) null;
    }

    /// <summary>当在派生类中重写时，控制将序列化对象绑定到类型的过程。</summary>
    /// <returns>格式化程序为其创建新实例的对象的类型。</returns>
    /// <param name="assemblyName">指定序列化对象的 <see cref="T:System.Reflection.Assembly" /> 名称。</param>
    /// <param name="typeName">指定序列化对象的 <see cref="T:System.Type" /> 名称。</param>
    public abstract Type BindToType(string assemblyName, string typeName);
  }
}
