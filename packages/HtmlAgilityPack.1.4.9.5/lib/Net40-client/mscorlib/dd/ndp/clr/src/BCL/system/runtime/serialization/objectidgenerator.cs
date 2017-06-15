// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.ObjectIDGenerator
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
  /// <summary>生成对象的 ID。</summary>
  [ComVisible(true)]
  [Serializable]
  public class ObjectIDGenerator
  {
    private static readonly int[] sizes = new int[21]{ 5, 11, 29, 47, 97, 197, 397, 797, 1597, 3203, 6421, 12853, 25717, 51437, 102877, 205759, 411527, 823117, 1646237, 3292489, 6584983 };
    private const int numbins = 4;
    internal int m_currentCount;
    internal int m_currentSize;
    internal long[] m_ids;
    internal object[] m_objs;

    /// <summary>初始化 <see cref="T:System.Runtime.Serialization.ObjectIDGenerator" /> 类的新实例。</summary>
    public ObjectIDGenerator()
    {
      this.m_currentCount = 1;
      this.m_currentSize = ObjectIDGenerator.sizes[0];
      this.m_ids = new long[this.m_currentSize * 4];
      this.m_objs = new object[this.m_currentSize * 4];
    }

    private int FindElement(object obj, out bool found)
    {
      int hashCode = RuntimeHelpers.GetHashCode(obj);
      int num1 = 1 + (hashCode & int.MaxValue) % (this.m_currentSize - 2);
      while (true)
      {
        int num2 = (hashCode & int.MaxValue) % this.m_currentSize * 4;
        for (int index = num2; index < num2 + 4; ++index)
        {
          if (this.m_objs[index] == null)
          {
            found = false;
            return index;
          }
          if (this.m_objs[index] == obj)
          {
            found = true;
            return index;
          }
        }
        hashCode += num1;
      }
    }

    /// <summary>返回指定对象的 ID，如果指定对象尚未由 <see cref="T:System.Runtime.Serialization.ObjectIDGenerator" /> 识别，则生成新的 ID。</summary>
    /// <returns>对象的 ID 用于序列化。如果是第一次识别对象，则 <paramref name="firstTime" /> 设置为 true；否则设置为 false。</returns>
    /// <param name="obj">需要其 ID 的对象。</param>
    /// <param name="firstTime">如果 <paramref name="obj" /> 先前对于 <see cref="T:System.Runtime.Serialization.ObjectIDGenerator" /> 未知，则为 true；否则为 false。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="obj" /> 参数为 null。</exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">已要求 <see cref="T:System.Runtime.Serialization.ObjectIDGenerator" /> 跟踪太多的对象。</exception>
    public virtual long GetId(object obj, out bool firstTime)
    {
      if (obj == null)
        throw new ArgumentNullException("obj", Environment.GetResourceString("ArgumentNull_Obj"));
      bool found;
      int element = this.FindElement(obj, out found);
      long num1;
      if (!found)
      {
        this.m_objs[element] = obj;
        long[] numArray = this.m_ids;
        int index = element;
        int num2 = this.m_currentCount;
        this.m_currentCount = num2 + 1;
        long num3 = (long) num2;
        numArray[index] = num3;
        num1 = this.m_ids[element];
        if (this.m_currentCount > this.m_currentSize * 4 / 2)
          this.Rehash();
      }
      else
        num1 = this.m_ids[element];
      firstTime = !found;
      return num1;
    }

    /// <summary>确定是否已经给对象分配 ID。</summary>
    /// <returns>如果先前对于 <see cref="T:System.Runtime.Serialization.ObjectIDGenerator" /> 已知，则为 <paramref name="obj" /> 的对象 ID；否则为零。</returns>
    /// <param name="obj">您所请求的对象。</param>
    /// <param name="firstTime">如果 <paramref name="obj" /> 先前对于 <see cref="T:System.Runtime.Serialization.ObjectIDGenerator" /> 未知，则为 true；否则为 false。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="obj" /> 参数为 null。</exception>
    public virtual long HasId(object obj, out bool firstTime)
    {
      if (obj == null)
        throw new ArgumentNullException("obj", Environment.GetResourceString("ArgumentNull_Obj"));
      bool found;
      int element = this.FindElement(obj, out found);
      if (found)
      {
        firstTime = false;
        return this.m_ids[element];
      }
      firstTime = true;
      return 0;
    }

    private void Rehash()
    {
      int index1 = 0;
      int num = this.m_currentSize;
      while (index1 < ObjectIDGenerator.sizes.Length && ObjectIDGenerator.sizes[index1] <= num)
        ++index1;
      if (index1 == ObjectIDGenerator.sizes.Length)
        throw new SerializationException(Environment.GetResourceString("Serialization_TooManyElements"));
      this.m_currentSize = ObjectIDGenerator.sizes[index1];
      long[] numArray1 = new long[this.m_currentSize * 4];
      object[] objArray1 = new object[this.m_currentSize * 4];
      long[] numArray2 = this.m_ids;
      object[] objArray2 = this.m_objs;
      this.m_ids = numArray1;
      this.m_objs = objArray1;
      for (int index2 = 0; index2 < objArray2.Length; ++index2)
      {
        if (objArray2[index2] != null)
        {
          bool found;
          int element = this.FindElement(objArray2[index2], out found);
          this.m_objs[element] = objArray2[index2];
          this.m_ids[element] = numArray2[index2];
        }
      }
    }
  }
}
