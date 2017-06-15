// Decompiled with JetBrains decompiler
// Type: System.LoaderOptimizationAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System
{
  /// <summary>用于为可执行应用程序的主方法设置默认的加载程序优化策略。</summary>
  /// <filterpriority>1</filterpriority>
  [AttributeUsage(AttributeTargets.Method)]
  [ComVisible(true)]
  public sealed class LoaderOptimizationAttribute : Attribute
  {
    internal byte _val;

    /// <summary>获取此实例的当前 <see cref="T:System.LoaderOptimization" /> 值。</summary>
    /// <returns>一个 <see cref="T:System.LoaderOptimization" /> 常数。</returns>
    /// <filterpriority>2</filterpriority>
    public LoaderOptimization Value
    {
      get
      {
        return (LoaderOptimization) this._val;
      }
    }

    /// <summary>将 <see cref="T:System.LoaderOptimizationAttribute" /> 类的新实例初始化为指定值。</summary>
    /// <param name="value">等效于 <see cref="T:System.LoaderOptimization" /> 常数的值。</param>
    public LoaderOptimizationAttribute(byte value)
    {
      this._val = value;
    }

    /// <summary>将 <see cref="T:System.LoaderOptimizationAttribute" /> 类的新实例初始化为指定值。</summary>
    /// <param name="value">一个 <see cref="T:System.LoaderOptimization" /> 常数。</param>
    public LoaderOptimizationAttribute(LoaderOptimization value)
    {
      this._val = (byte) value;
    }
  }
}
