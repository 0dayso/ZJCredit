// Decompiled with JetBrains decompiler
// Type: System.Reflection.ReflectionTypeLoadException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Reflection
{
  /// <summary>当模块中有任何类无法加载时由 <see cref="M:System.Reflection.Module.GetTypes" /> 方法引发的异常。此类不能被继承。</summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class ReflectionTypeLoadException : SystemException, ISerializable
  {
    private Type[] _classes;
    private Exception[] _exceptions;

    /// <summary>获取模块中定义并加载的类的数组。</summary>
    /// <returns>Type 类型的数组，其中包含在模块中定义并加载的类。该数组可以包含一些 null 值。</returns>
    [__DynamicallyInvokable]
    public Type[] Types
    {
      [__DynamicallyInvokable] get
      {
        return this._classes;
      }
    }

    /// <summary>获取类加载程序引发的异常数组。</summary>
    /// <returns>Exception 类型的数组，其中包含由类加载程序引发的异常。此实例的 <paramref name="classes" /> 数组中的空值也属于该数组中的异常。</returns>
    [__DynamicallyInvokable]
    public Exception[] LoaderExceptions
    {
      [__DynamicallyInvokable] get
      {
        return this._exceptions;
      }
    }

    private ReflectionTypeLoadException()
      : base(Environment.GetResourceString("ReflectionTypeLoad_LoadFailed"))
    {
      this.SetErrorCode(-2146232830);
    }

    private ReflectionTypeLoadException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146232830);
    }

    /// <summary>用给定类及其关联的异常初始化 <see cref="T:System.Reflection.ReflectionTypeLoadException" /> 类的新实例。</summary>
    /// <param name="classes">Type 类型的数组，其中包含在模块中定义并加载的类。该数组可以包含空引用（在 Visual Basic 中为 Nothing）值。</param>
    /// <param name="exceptions">Exception 类型的数组，其中包含由类加载程序引发的异常。<paramref name="classes" /> 数组中的空引用（在 Visual Basic 中为 Nothing）值与此 <paramref name="exceptions" /> 数组中的异常保持对应。</param>
    [__DynamicallyInvokable]
    public ReflectionTypeLoadException(Type[] classes, Exception[] exceptions)
      : base((string) null)
    {
      this._classes = classes;
      this._exceptions = exceptions;
      this.SetErrorCode(-2146232830);
    }

    /// <summary>用给定类、与此类关联的异常以及异常说明初始化 <see cref="T:System.Reflection.ReflectionTypeLoadException" /> 类的新实例。</summary>
    /// <param name="classes">Type 类型的数组，其中包含在模块中定义并加载的类。该数组可以包含空引用（在 Visual Basic 中为 Nothing）值。</param>
    /// <param name="exceptions">Exception 类型的数组，其中包含由类加载程序引发的异常。<paramref name="classes" /> 数组中的空引用（在 Visual Basic 中为 Nothing）值与此 <paramref name="exceptions" /> 数组中的异常保持对应。</param>
    /// <param name="message">描述此异常的引发原因的 String。</param>
    [__DynamicallyInvokable]
    public ReflectionTypeLoadException(Type[] classes, Exception[] exceptions, string message)
      : base(message)
    {
      this._classes = classes;
      this._exceptions = exceptions;
      this.SetErrorCode(-2146232830);
    }

    internal ReflectionTypeLoadException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
      this._classes = (Type[]) info.GetValue("Types", typeof (Type[]));
      this._exceptions = (Exception[]) info.GetValue("Exceptions", typeof (Exception[]));
    }

    /// <summary>提供序列化对象的 <see cref="T:System.Runtime.Serialization.ISerializable" /> 实现。</summary>
    /// <param name="info">序列化或反序列化对象所需的信息和数据。</param>
    /// <param name="context">序列化的上下文。</param>
    /// <exception cref="T:System.ArgumentNullException">info 为 null。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
    /// </PermissionSet>
    [SecurityCritical]
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException("info");
      base.GetObjectData(info, context);
      info.AddValue("Types", (object) this._classes, typeof (Type[]));
      info.AddValue("Exceptions", (object) this._exceptions, typeof (Exception[]));
    }
  }
}
