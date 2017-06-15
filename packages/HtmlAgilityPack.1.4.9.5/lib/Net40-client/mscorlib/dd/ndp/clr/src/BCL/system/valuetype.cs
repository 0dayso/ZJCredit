// Decompiled with JetBrains decompiler
// Type: System.ValueType
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace System
{
  /// <summary>提供值类型的基类。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public abstract class ValueType
  {
    /// <summary>初始化 <see cref="T:System.ValueType" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    protected ValueType()
    {
    }

    /// <summary>指示此实例与指定对象是否相等。</summary>
    /// <returns>如果 <paramref name="obj" /> 和该实例具有相同的类型并表示相同的值，则为 true；否则为 false。</returns>
    /// <param name="obj">要与当前实例进行比较的对象。</param>
    /// <filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public override bool Equals(object obj)
    {
      if (obj == null)
        return false;
      RuntimeType runtimeType = (RuntimeType) this.GetType();
      if ((RuntimeType) obj.GetType() != runtimeType)
        return false;
      object a = (object) this;
      if (ValueType.CanCompareBits((object) this))
        return ValueType.FastEqualsCheck(a, obj);
      FieldInfo[] fields = runtimeType.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
      for (int index = 0; index < fields.Length; ++index)
      {
        object obj1 = ((RtFieldInfo) fields[index]).UnsafeGetValue(a);
        object obj2 = ((RtFieldInfo) fields[index]).UnsafeGetValue(obj);
        if (obj1 == null)
        {
          if (obj2 != null)
            return false;
        }
        else if (!obj1.Equals(obj2))
          return false;
      }
      return true;
    }

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern bool CanCompareBits(object obj);

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern bool FastEqualsCheck(object a, object b);

    /// <summary>返回此实例的哈希代码。</summary>
    /// <returns>一个 32 位有符号整数，它是该实例的哈希代码。</returns>
    /// <filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public override int GetHashCode();

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern int GetHashCodeOfPtr(IntPtr ptr);

    /// <summary>返回该实例的完全限定类型名。</summary>
    /// <returns>包含完全限定类型名的 <see cref="T:System.String" />。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public override string ToString()
    {
      return this.GetType().ToString();
    }
  }
}
