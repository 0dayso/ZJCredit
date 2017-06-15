// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.PreserveSigAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;

namespace System.Runtime.InteropServices
{
  /// <summary>指示应取消在 COM 互操作调用期间发生的 HRESULT 或 retval 签名转换。</summary>
  [AttributeUsage(AttributeTargets.Method, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class PreserveSigAttribute : Attribute
  {
    /// <summary>初始化 <see cref="T:System.Runtime.InteropServices.PreserveSigAttribute" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public PreserveSigAttribute()
    {
    }

    internal static Attribute GetCustomAttribute(RuntimeMethodInfo method)
    {
      if ((method.GetMethodImplementationFlags() & MethodImplAttributes.PreserveSig) == MethodImplAttributes.IL)
        return (Attribute) null;
      return (Attribute) new PreserveSigAttribute();
    }

    internal static bool IsDefined(RuntimeMethodInfo method)
    {
      return (uint) (method.GetMethodImplementationFlags() & MethodImplAttributes.PreserveSig) > 0U;
    }
  }
}
