// Decompiled with JetBrains decompiler
// Type: System.Reflection.Pointer
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Reflection
{
  /// <summary>为指针提供包装类。</summary>
  [CLSCompliant(false)]
  [ComVisible(true)]
  [Serializable]
  public sealed class Pointer : ISerializable
  {
    [SecurityCritical]
    private unsafe void* _ptr;
    private RuntimeType _ptrType;

    private Pointer()
    {
    }

    [SecurityCritical]
    private unsafe Pointer(SerializationInfo info, StreamingContext context)
    {
      this._ptr = ((IntPtr) info.GetValue("_ptr", typeof (IntPtr))).ToPointer();
      this._ptrType = (RuntimeType) info.GetValue("_ptrType", typeof (RuntimeType));
    }

    /// <summary>将提供的非托管内存指针和与该指针关联的类型装箱到托管 <see cref="T:System.Reflection.Pointer" /> 包装对象中。该值和类型被保存以便可以在调用过程中从本机代码访问它们。</summary>
    /// <returns>指针对象。</returns>
    /// <param name="ptr">提供的非托管内存指针。</param>
    /// <param name="type">与 <paramref name="ptr" /> 参数关联的类型。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="type" /> 不是指针。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="type" /> 为 null。</exception>
    [SecurityCritical]
    public static unsafe object Box(void* ptr, Type type)
    {
      if (type == (Type) null)
        throw new ArgumentNullException("type");
      if (!type.IsPointer)
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBePointer"), "ptr");
      RuntimeType runtimeType = type as RuntimeType;
      if (runtimeType == (RuntimeType) null)
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBePointer"), "ptr");
      return (object) new Pointer() { _ptr = ptr, _ptrType = runtimeType };
    }

    /// <summary>返回存储指针。</summary>
    /// <returns>此方法返回 void。</returns>
    /// <param name="ptr">存储指针。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="ptr" /> 不是指针。</exception>
    [SecurityCritical]
    public static unsafe void* Unbox(object ptr)
    {
      if (!(ptr is Pointer))
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBePointer"), "ptr");
      return ((Pointer) ptr)._ptr;
    }

    internal RuntimeType GetPointerType()
    {
      return this._ptrType;
    }

    [SecurityCritical]
    internal unsafe object GetPointerValue()
    {
      return (object) (IntPtr) this._ptr;
    }

    [SecurityCritical]
    unsafe void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
    {
      info.AddValue("_ptr", (object) new IntPtr(this._ptr));
      info.AddValue("_ptrType", (object) this._ptrType);
    }
  }
}
