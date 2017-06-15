// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.DateTimeConstantAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
  /// <summary>为字段或参数永久保存一个 8 字节的 <see cref="T:System.DateTime" /> 常数。</summary>
  [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class DateTimeConstantAttribute : CustomConstantAttribute
  {
    private DateTime date;

    /// <summary>获取表示此实例日期和时间的以 100 毫微秒为单位的数字。</summary>
    /// <returns>表示此实例日期和时间的以 100 毫微秒为单位的数字。</returns>
    [__DynamicallyInvokable]
    public override object Value
    {
      [__DynamicallyInvokable] get
      {
        return (object) this.date;
      }
    }

    /// <summary>用表示此实例日期和时间的以 100 毫微秒为单位的数字初始化 DateTimeConstantAttribute 类的新实例。</summary>
    /// <param name="ticks">表示此实例日期和时间的以 100 毫微秒为单位的数字。</param>
    [__DynamicallyInvokable]
    public DateTimeConstantAttribute(long ticks)
    {
      this.date = new DateTime(ticks);
    }

    internal static DateTime GetRawDateTimeConstant(CustomAttributeData attr)
    {
      foreach (CustomAttributeNamedArgument namedArgument in (IEnumerable<CustomAttributeNamedArgument>) attr.NamedArguments)
      {
        if (namedArgument.MemberInfo.Name.Equals("Value"))
          return new DateTime((long) namedArgument.TypedValue.Value);
      }
      return new DateTime((long) attr.ConstructorArguments[0].Value);
    }
  }
}
