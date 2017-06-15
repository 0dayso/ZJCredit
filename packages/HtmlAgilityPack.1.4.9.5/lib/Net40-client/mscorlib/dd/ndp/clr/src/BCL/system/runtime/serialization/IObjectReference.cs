// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.IObjectReference
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Serialization
{
  /// <summary>指示当前接口实施者是对另一个对象的引用。</summary>
  [ComVisible(true)]
  public interface IObjectReference
  {
    /// <summary>返回应进行反序列化的真实对象（而不是序列化流指定的对象）。</summary>
    /// <returns>返回放入图形中的实际对象。</returns>
    /// <param name="context">当前对象从其中进行反序列化的 <see cref="T:System.Runtime.Serialization.StreamingContext" />。</param>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。无法对中等信任的服务器进行调用。</exception>
    [SecurityCritical]
    object GetRealObject(StreamingContext context);
  }
}
