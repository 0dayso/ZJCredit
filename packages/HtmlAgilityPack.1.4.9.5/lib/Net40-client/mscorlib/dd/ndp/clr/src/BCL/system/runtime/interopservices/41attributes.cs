// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ManagedToNativeComInteropStubAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>为用户在托管与 COM 互操作方案中自定义互操作存根提供支持。</summary>
  [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
  [ComVisible(false)]
  public sealed class ManagedToNativeComInteropStubAttribute : Attribute
  {
    internal Type _classType;
    internal string _methodName;

    /// <summary>获取包含所需的存根方法的类。</summary>
    /// <returns>包含自定义的互操作存根的类。</returns>
    public Type ClassType
    {
      get
      {
        return this._classType;
      }
    }

    /// <summary>获取存根方法的名称。</summary>
    /// <returns>自定义的互操作存根的名称。</returns>
    public string MethodName
    {
      get
      {
        return this._methodName;
      }
    }

    /// <summary>使用指定的类类型和方法名称初始化 <see cref="T:System.Runtime.InteropServices.ManagedToNativeComInteropStubAttribute" /> 类的新实例。</summary>
    /// <param name="classType">包含所需的存根方法的类。</param>
    /// <param name="methodName">存根方法的名称。</param>
    /// <exception cref="T:System.ArgumentException">存根方法不在包含托管互操作方法的接口所在的程序集内。- 或 -<paramref name="classType" /> 是一个泛型类型。- 或 -<paramref name="classType" /> 是一个接口。</exception>
    /// <exception cref="T:System.ArgumentException">找不到 <paramref name="methodName" />。- 或 -该方法不是静态或非泛型方法。- 或 -该方法的参数列表与存根的预期参数列表不匹配。</exception>
    /// <exception cref="T:System.MethodAccessException">由于存根方法具有私有或受保护的可访问性，或由于安全问题，包含托管互操作方法的接口无权访问存根方法。</exception>
    public ManagedToNativeComInteropStubAttribute(Type classType, string methodName)
    {
      this._classType = classType;
      this._methodName = methodName;
    }
  }
}
