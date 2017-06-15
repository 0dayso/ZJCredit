// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.WriteOnlyArrayAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices.WindowsRuntime
{
  /// <summary>当应用于 Windows 运行时 元素的数组参数，指定传递给该参数数组的内容只用于输出。调用方不保证对内容进行了初始化，因此，所调用的方法不应读取内容。有关使用托管代码写入的调用方的重要信息，请参见“备注”一节。</summary>
  [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
  [__DynamicallyInvokable]
  public sealed class WriteOnlyArrayAttribute : Attribute
  {
    /// <summary>初始化 <see cref="T:System.Runtime.InteropServices.WindowsRuntime.WriteOnlyArrayAttribute" /> 类的新实例。 </summary>
    [__DynamicallyInvokable]
    public WriteOnlyArrayAttribute()
    {
    }
  }
}
