// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.IUnknownConstantAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
  /// <summary>指示特性化字段或参数的默认值是 <see cref="T:System.Runtime.InteropServices.UnknownWrapper" /> 的实例，其中 <see cref="P:System.Runtime.InteropServices.UnknownWrapper.WrappedObject" /> 为 null。此类不能被继承。</summary>
  [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter, Inherited = false)]
  [ComVisible(true)]
  [Serializable]
  public sealed class IUnknownConstantAttribute : CustomConstantAttribute
  {
    /// <summary>获取存储在此特性中的 IUnknown 常数。</summary>
    /// <returns>存储在此特性中的 IUnknown 常数。只有 null 可以作为 IUnknown 常数的值。</returns>
    public override object Value
    {
      get
      {
        return (object) new UnknownWrapper((object) null);
      }
    }
  }
}
