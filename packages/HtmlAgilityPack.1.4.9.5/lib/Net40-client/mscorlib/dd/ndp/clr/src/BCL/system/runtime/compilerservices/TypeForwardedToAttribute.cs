// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.TypeForwardedToAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;
using System.Security;

namespace System.Runtime.CompilerServices
{
  /// <summary>指定另一个程序集中的目标 <see cref="T:System.Type" />。</summary>
  [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
  [__DynamicallyInvokable]
  public sealed class TypeForwardedToAttribute : Attribute
  {
    private Type _destination;

    /// <summary>获取另一个程序集中的目标 <see cref="T:System.Type" />。</summary>
    /// <returns>另一个程序集中的目标 <see cref="T:System.Type" />。</returns>
    [__DynamicallyInvokable]
    public Type Destination
    {
      [__DynamicallyInvokable] get
      {
        return this._destination;
      }
    }

    /// <summary>初始化指定目标 <see cref="T:System.Type" /> 的 <see cref="T:System.Runtime.CompilerServices.TypeForwardedToAttribute" /> 类的新实例。</summary>
    /// <param name="destination">另一个程序集中的目标 <see cref="T:System.Type" />。</param>
    [__DynamicallyInvokable]
    public TypeForwardedToAttribute(Type destination)
    {
      this._destination = destination;
    }

    [SecurityCritical]
    internal static TypeForwardedToAttribute[] GetCustomAttribute(RuntimeAssembly assembly)
    {
      Type[] o = (Type[]) null;
      RuntimeAssembly.GetForwardedTypes(assembly.GetNativeHandle(), JitHelpers.GetObjectHandleOnStack<Type[]>(ref o));
      TypeForwardedToAttribute[] forwardedToAttributeArray = new TypeForwardedToAttribute[o.Length];
      for (int index = 0; index < o.Length; ++index)
        forwardedToAttributeArray[index] = new TypeForwardedToAttribute(o[index]);
      return forwardedToAttributeArray;
    }
  }
}
