// Decompiled with JetBrains decompiler
// Type: System.Collections.StructuralComparisons
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Collections
{
  /// <summary>提供用于对两个集合对象执行结构比较的对象。</summary>
  [__DynamicallyInvokable]
  public static class StructuralComparisons
  {
    private static volatile IComparer s_StructuralComparer;
    private static volatile IEqualityComparer s_StructuralEqualityComparer;

    /// <summary>获取可执行两个对象的结构比较的预定义对象。</summary>
    /// <returns>一个用于执行两个集合对象的结构比较的预定义对象。</returns>
    [__DynamicallyInvokable]
    public static IComparer StructuralComparer
    {
      [__DynamicallyInvokable] get
      {
        IComparer comparer = StructuralComparisons.s_StructuralComparer;
        if (comparer == null)
        {
          comparer = (IComparer) new System.Collections.StructuralComparer();
          StructuralComparisons.s_StructuralComparer = comparer;
        }
        return comparer;
      }
    }

    /// <summary>获取一个可比较两个对象的结构是否相等的预定义对象。</summary>
    /// <returns>一个用于比较两个集合对象的结构是否相等的预定义对象。</returns>
    [__DynamicallyInvokable]
    public static IEqualityComparer StructuralEqualityComparer
    {
      [__DynamicallyInvokable] get
      {
        IEqualityComparer equalityComparer = StructuralComparisons.s_StructuralEqualityComparer;
        if (equalityComparer == null)
        {
          equalityComparer = (IEqualityComparer) new System.Collections.StructuralEqualityComparer();
          StructuralComparisons.s_StructuralEqualityComparer = equalityComparer;
        }
        return equalityComparer;
      }
    }
  }
}
