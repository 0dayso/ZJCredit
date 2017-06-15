// Decompiled with JetBrains decompiler
// Type: System.Reflection.LocalVariableInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>发现局部变量的属性并提供对局部变量元数据的访问。</summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public class LocalVariableInfo
  {
    private RuntimeType m_type;
    private int m_isPinned;
    private int m_localIndex;

    /// <summary>获取局部变量的类型。</summary>
    /// <returns>局部变量的类型。</returns>
    [__DynamicallyInvokable]
    public virtual Type LocalType
    {
      [__DynamicallyInvokable] get
      {
        return (Type) this.m_type;
      }
    }

    /// <summary>获取一个 <see cref="T:System.Boolean" /> 值，该值指示由局部变量引用的对象是否被固定在内存中。</summary>
    /// <returns>如果由变量引用的对象被固定在内存中，则为 true；否则为 false。</returns>
    [__DynamicallyInvokable]
    public virtual bool IsPinned
    {
      [__DynamicallyInvokable] get
      {
        return (uint) this.m_isPinned > 0U;
      }
    }

    /// <summary>获取方法体内局部变量的索引。</summary>
    /// <returns>一个整数值，表示方法体内局部变量的声明顺序。</returns>
    [__DynamicallyInvokable]
    public virtual int LocalIndex
    {
      [__DynamicallyInvokable] get
      {
        return this.m_localIndex;
      }
    }

    /// <summary>初始化 <see cref="T:System.Reflection.LocalVariableInfo" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    protected LocalVariableInfo()
    {
    }

    /// <summary>返回一个描述局部变量的用户可读的字符串。</summary>
    /// <returns>一个字符串，显示有关局部变量的信息，包括类型名称、索引和固定状态。</returns>
    [__DynamicallyInvokable]
    public override string ToString()
    {
      string str = this.LocalType.ToString() + " (" + (object) this.LocalIndex + ")";
      if (this.IsPinned)
        str += " (pinned)";
      return str;
    }
  }
}
