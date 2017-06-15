// Decompiled with JetBrains decompiler
// Type: System.ObsoleteAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System
{
  /// <summary>标记不再使用的程序元素。此类不能被继承。</summary>
  /// <filterpriority>1</filterpriority>
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface | AttributeTargets.Delegate, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class ObsoleteAttribute : Attribute
  {
    private string _message;
    private bool _error;

    /// <summary>获取变通方法消息，包括对可选程序元素的说明。</summary>
    /// <returns>变通方法文本字符串。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public string Message
    {
      [__DynamicallyInvokable] get
      {
        return this._message;
      }
    }

    /// <summary>获取指示编译器是否将使用已过时的程序元素视为错误的布尔值。</summary>
    /// <returns>如果将使用已过时的元素视为错误，则为 true；否则为 false。默认值为 false。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public bool IsError
    {
      [__DynamicallyInvokable] get
      {
        return this._error;
      }
    }

    /// <summary>使用默认属性初始化 <see cref="T:System.ObsoleteAttribute" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public ObsoleteAttribute()
    {
      this._message = (string) null;
      this._error = false;
    }

    /// <summary>使用指定的变通方法消息初始化 <see cref="T:System.ObsoleteAttribute" /> 类的新实例。</summary>
    /// <param name="message">描述可选的变通方法的文本字符串。</param>
    [__DynamicallyInvokable]
    public ObsoleteAttribute(string message)
    {
      this._message = message;
      this._error = false;
    }

    /// <summary>使用变通方法消息和布尔值初始化 <see cref="T:System.ObsoleteAttribute" /> 类的新实例，该布尔值指示是否将使用已过时的元素视为错误。</summary>
    /// <param name="message">描述可选的变通方法的文本字符串。</param>
    /// <param name="error">指示是否将使用已过时的元素视为错误的布尔值。</param>
    [__DynamicallyInvokable]
    public ObsoleteAttribute(string message, bool error)
    {
      this._message = message;
      this._error = error;
    }
  }
}
