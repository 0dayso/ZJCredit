// Decompiled with JetBrains decompiler
// Type: System.Reflection.ParameterModifier
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>在参数中附加修饰符，以便绑定能够处理在其中修改了类型的参数签名。</summary>
  [ComVisible(true)]
  [Serializable]
  public struct ParameterModifier
  {
    private bool[] _byRef;

    internal bool[] IsByRefArray
    {
      get
      {
        return this._byRef;
      }
    }

    /// <summary>获取或设置一个值，该值指定位于指定索引位置的参数是否由当前 <see cref="T:System.Reflection.ParameterModifier" /> 修改。</summary>
    /// <returns>如果此索引位置的参数将由此 <see cref="T:System.Reflection.ParameterModifier" /> 修改，则为 true；否则为 false。</returns>
    /// <param name="index">正在检查或设置其修改状态的参数的索引位置。</param>
    public bool this[int index]
    {
      get
      {
        return this._byRef[index];
      }
      set
      {
        this._byRef[index] = value;
      }
    }

    /// <summary>初始化表示指定参数数目的 <see cref="T:System.Reflection.ParameterModifier" /> 结构的新实例。</summary>
    /// <param name="parameterCount">参数数目。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="parameterCount" /> 为负数。</exception>
    public ParameterModifier(int parameterCount)
    {
      if (parameterCount <= 0)
        throw new ArgumentException(Environment.GetResourceString("Arg_ParmArraySize"));
      this._byRef = new bool[parameterCount];
    }
  }
}
