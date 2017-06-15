// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.DecimalConstantAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
  /// <summary>在元数据中存储 <see cref="T:System.Decimal" /> 常数的值。此类不能被继承。</summary>
  [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class DecimalConstantAttribute : Attribute
  {
    private Decimal dec;

    /// <summary>获取存储在此属性中的十进制常数。</summary>
    /// <returns>存储在此属性中的十进制常数。</returns>
    [__DynamicallyInvokable]
    public Decimal Value
    {
      [__DynamicallyInvokable] get
      {
        return this.dec;
      }
    }

    /// <summary>使用指定的无符号整数值初始化 <see cref="T:System.Runtime.CompilerServices.DecimalConstantAttribute" /> 类的新实例。</summary>
    /// <param name="scale">比例因子（10 的幂），它指示小数点右边的数字位数。有效值为从 0 到 28（含）。</param>
    /// <param name="sign">值 0 指示正值，值 1 指示负值。</param>
    /// <param name="hi">96 位 <see cref="P:System.Runtime.CompilerServices.DecimalConstantAttribute.Value" /> 的高 32 位。</param>
    /// <param name="mid">96 位 <see cref="P:System.Runtime.CompilerServices.DecimalConstantAttribute.Value" /> 的中间 32 位。</param>
    /// <param name="low">96 位 <see cref="P:System.Runtime.CompilerServices.DecimalConstantAttribute.Value" /> 的低 32 位。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="scale" /> &gt; 28.</exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public DecimalConstantAttribute(byte scale, byte sign, uint hi, uint mid, uint low)
    {
      this.dec = new Decimal((int) low, (int) mid, (int) hi, (uint) sign > 0U, scale);
    }

    /// <summary>使用指定的有符号整数值初始化 <see cref="T:System.Runtime.CompilerServices.DecimalConstantAttribute" /> 类的新实例。</summary>
    /// <param name="scale">比例因子（10 的幂），它指示小数点右边的数字位数。有效值为从 0 到 28（含）。</param>
    /// <param name="sign">值 0 指示正值，值 1 指示负值。</param>
    /// <param name="hi">96 位 <see cref="P:System.Runtime.CompilerServices.DecimalConstantAttribute.Value" /> 的高 32 位。</param>
    /// <param name="mid">96 位 <see cref="P:System.Runtime.CompilerServices.DecimalConstantAttribute.Value" /> 的中间 32 位。</param>
    /// <param name="low">96 位 <see cref="P:System.Runtime.CompilerServices.DecimalConstantAttribute.Value" /> 的低 32 位。</param>
    [__DynamicallyInvokable]
    public DecimalConstantAttribute(byte scale, byte sign, int hi, int mid, int low)
    {
      this.dec = new Decimal(low, mid, hi, (uint) sign > 0U, scale);
    }

    internal static Decimal GetRawDecimalConstant(CustomAttributeData attr)
    {
      foreach (CustomAttributeNamedArgument namedArgument in (IEnumerable<CustomAttributeNamedArgument>) attr.NamedArguments)
      {
        if (namedArgument.MemberInfo.Name.Equals("Value"))
          return (Decimal) namedArgument.TypedValue.Value;
      }
      ParameterInfo[] parameters = attr.Constructor.GetParameters();
      IList<CustomAttributeTypedArgument> constructorArguments = attr.ConstructorArguments;
      int index = 2;
      if (parameters[index].ParameterType == typeof (uint))
      {
        CustomAttributeTypedArgument attributeTypedArgument = constructorArguments[4];
        int lo = (int) (uint) attributeTypedArgument.Value;
        attributeTypedArgument = constructorArguments[3];
        int mid = (int) (uint) attributeTypedArgument.Value;
        attributeTypedArgument = constructorArguments[2];
        int hi = (int) (uint) attributeTypedArgument.Value;
        attributeTypedArgument = constructorArguments[1];
        byte num = (byte) attributeTypedArgument.Value;
        attributeTypedArgument = constructorArguments[0];
        byte scale = (byte) attributeTypedArgument.Value;
        return new Decimal(lo, mid, hi, (uint) num > 0U, scale);
      }
      CustomAttributeTypedArgument attributeTypedArgument1 = constructorArguments[4];
      int lo1 = (int) attributeTypedArgument1.Value;
      attributeTypedArgument1 = constructorArguments[3];
      int mid1 = (int) attributeTypedArgument1.Value;
      attributeTypedArgument1 = constructorArguments[2];
      int hi1 = (int) attributeTypedArgument1.Value;
      attributeTypedArgument1 = constructorArguments[1];
      byte num1 = (byte) attributeTypedArgument1.Value;
      attributeTypedArgument1 = constructorArguments[0];
      byte scale1 = (byte) attributeTypedArgument1.Value;
      return new Decimal(lo1, mid1, hi1, (uint) num1 > 0U, scale1);
    }
  }
}
