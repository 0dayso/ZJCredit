// Decompiled with JetBrains decompiler
// Type: System.Object
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Versioning;
using System.Security;

namespace System
{
  /// <summary>支持 .NET Framework 类层次结构中的所有类，并为派生类提供低级别服务。这是 .NET Framework 中所有类的最终基类；它是类型层次结构的根。若要浏览此类型的.NET Framework 源代码，请参阅 Reference Source。</summary>
  /// <filterpriority>1</filterpriority>
  [ClassInterface(ClassInterfaceType.AutoDual)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class Object
  {
    /// <summary>初始化 <see cref="T:System.Object" /> 类的新实例。</summary>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [NonVersionable]
    [__DynamicallyInvokable]
    public Object()
    {
    }

    /// <summary>在垃圾回收将某一对象回收前允许该对象尝试释放资源并执行其他清理操作。</summary>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [NonVersionable]
    [__DynamicallyInvokable]
    ~Object()
    {
    }

    /// <summary>返回表示当前对象的字符串。</summary>
    /// <returns>表示当前对象的字符串。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public virtual string ToString()
    {
      return this.GetType().ToString();
    }

    /// <summary>确定指定的对象是否等于当前对象。</summary>
    /// <returns>如果指定的对象等于当前对象，则为 true，否则为 false。</returns>
    /// <param name="obj">要与当前对象进行比较的对象。</param>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public virtual bool Equals(object obj)
    {
      return RuntimeHelpers.Equals(this, obj);
    }

    /// <summary>确定指定的对象实例是否被视为相等。</summary>
    /// <returns>如果对象被视为相等，则为 true，否则为 false。如果 <paramref name="objA" /> 和 <paramref name="objB" /> 均为 null，此方法将返回 true。</returns>
    /// <param name="objA">要比较的第一个对象。</param>
    /// <param name="objB">要比较的第二个对象。</param>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public static bool Equals(object objA, object objB)
    {
      if (objA == objB)
        return true;
      if (objA == null || objB == null)
        return false;
      return objA.Equals(objB);
    }

    /// <summary>确定指定的 <see cref="T:System.Object" /> 实例是否是相同的实例。</summary>
    /// <returns>如果 <paramref name="objA" /> 是与 <paramref name="objB" /> 相同的实例，或如果两者均为 null，则为 true，否则为 false。</returns>
    /// <param name="objA">要比较的第一个对象。</param>
    /// <param name="objB">要比较的第二个对象。</param>
    /// <filterpriority>2</filterpriority>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [NonVersionable]
    [__DynamicallyInvokable]
    public static bool ReferenceEquals(object objA, object objB)
    {
      return objA == objB;
    }

    /// <summary>作为默认哈希函数。</summary>
    /// <returns>当前对象的哈希代码。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public virtual int GetHashCode()
    {
      return RuntimeHelpers.GetHashCode(this);
    }

    /// <summary>获取当前实例的 <see cref="T:System.Type" />。</summary>
    /// <returns>当前实例的准确运行时类型。</returns>
    /// <filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public Type GetType();

    /// <summary>创建当前 <see cref="T:System.Object" /> 的浅表副本。</summary>
    /// <returns>当前 <see cref="T:System.Object" /> 的浅表副本。</returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    protected object MemberwiseClone();

    [SecurityCritical]
    private void FieldSetter(string typeName, string fieldName, object val)
    {
      FieldInfo fieldInfo = this.GetFieldInfo(typeName, fieldName);
      if (fieldInfo.IsInitOnly)
        throw new FieldAccessException(Environment.GetResourceString("FieldAccess_InitOnly"));
      Message.CoerceArg(val, fieldInfo.FieldType);
      fieldInfo.SetValue(this, val);
    }

    private void FieldGetter(string typeName, string fieldName, ref object val)
    {
      FieldInfo fieldInfo = this.GetFieldInfo(typeName, fieldName);
      val = fieldInfo.GetValue(this);
    }

    private FieldInfo GetFieldInfo(string typeName, string fieldName)
    {
      Type type = this.GetType();
      while ((Type) null != type && !type.FullName.Equals(typeName))
        type = type.BaseType;
      if ((Type) null == type)
        throw new RemotingException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_BadType"), (object) typeName));
      FieldInfo field = type.GetField(fieldName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
      if ((FieldInfo) null == field)
        throw new RemotingException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_BadField"), (object) fieldName, (object) typeName));
      return field;
    }
  }
}
