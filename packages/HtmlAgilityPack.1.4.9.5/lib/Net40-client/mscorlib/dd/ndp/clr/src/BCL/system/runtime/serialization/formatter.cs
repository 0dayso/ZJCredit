// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.Formatter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
  /// <summary>提供公共语言运行时序列化格式化程序的基本功能。</summary>
  [CLSCompliant(false)]
  [ComVisible(true)]
  [Serializable]
  public abstract class Formatter : IFormatter
  {
    /// <summary>包含与当前格式化程序一起使用的 <see cref="T:System.Runtime.Serialization.ObjectIDGenerator" />。</summary>
    protected ObjectIDGenerator m_idGenerator;
    /// <summary>包含要序列化的对象的 <see cref="T:System.Collections.Queue" />。</summary>
    protected Queue m_objectQueue;

    /// <summary>当在派生类中重写时，获取或设置与当前格式化程序一起使用的 <see cref="T:System.Runtime.Serialization.ISurrogateSelector" />。</summary>
    /// <returns>与当前格式化程序一起使用的 <see cref="T:System.Runtime.Serialization.ISurrogateSelector" />。</returns>
    public abstract ISurrogateSelector SurrogateSelector { get; set; }

    /// <summary>当在派生类中重写时，获取或设置与当前格式化程序一起使用的 <see cref="T:System.Runtime.Serialization.SerializationBinder" />。</summary>
    /// <returns>与当前格式化程序一起使用的 <see cref="T:System.Runtime.Serialization.SerializationBinder" />。</returns>
    public abstract SerializationBinder Binder { get; set; }

    /// <summary>当在派生类中重写时，获取或设置用于当前序列化的 <see cref="T:System.Runtime.Serialization.StreamingContext" />。</summary>
    /// <returns>用于当前序列化的 <see cref="T:System.Runtime.Serialization.StreamingContext" />。</returns>
    public abstract StreamingContext Context { get; set; }

    /// <summary>初始化 <see cref="T:System.Runtime.Serialization.Formatter" /> 类的新实例。</summary>
    protected Formatter()
    {
      this.m_objectQueue = new Queue();
      this.m_idGenerator = new ObjectIDGenerator();
    }

    /// <summary>当在派生类中重写时，将连接到格式化程序的流反序列化（在创建该流时），以创建与最初序列化为该流的图形相同的对象图形。</summary>
    /// <returns>对象的反序列化图形的顶级对象。</returns>
    /// <param name="serializationStream">要反序列化的流。</param>
    public abstract object Deserialize(Stream serializationStream);

    /// <summary>从格式化程序的内部工作队列返回下一个要序列化的对象。</summary>
    /// <returns>下一个要序列化的对象。</returns>
    /// <param name="objID">要在序列化过程中分配给当前对象的 ID。</param>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">从工作队列检索的下一个对象不具有已分配的 ID。</exception>
    protected virtual object GetNext(out long objID)
    {
      if (this.m_objectQueue.Count == 0)
      {
        objID = 0L;
        return (object) null;
      }
      object obj = this.m_objectQueue.Dequeue();
      bool firstTime;
      objID = this.m_idGenerator.HasId(obj, out firstTime);
      if (firstTime)
        throw new SerializationException(Environment.GetResourceString("Serialization_NoID"));
      return obj;
    }

    /// <summary>安排对象在稍后进行序列化。</summary>
    /// <returns>要分配给对象的对象 ID。</returns>
    /// <param name="obj">要安排序列化的对象。</param>
    protected virtual long Schedule(object obj)
    {
      if (obj == null)
        return 0;
      bool firstTime;
      long id = this.m_idGenerator.GetId(obj, out firstTime);
      if (!firstTime)
        return id;
      this.m_objectQueue.Enqueue(obj);
      return id;
    }

    /// <summary>当在派生类中重写时，将具有指定根的对象图形序列化为已经连接到格式化程序的流。</summary>
    /// <param name="serializationStream">对象要序列化为的流。</param>
    /// <param name="graph">位于要序列化图形的根位置的对象。</param>
    public abstract void Serialize(Stream serializationStream, object graph);

    /// <summary>当在派生类中重写时，向已经连接到格式化程序的流中写入一个数组。</summary>
    /// <param name="obj">要写入的数组。</param>
    /// <param name="name">数组的名称。</param>
    /// <param name="memberType">数组包含的元素的类型。</param>
    protected abstract void WriteArray(object obj, string name, Type memberType);

    /// <summary>当在派生类中重写时，向已经连接到格式化程序的流中写入一个 Boolean 值。</summary>
    /// <param name="val">要写入的值。</param>
    /// <param name="name">成员的名称。</param>
    protected abstract void WriteBoolean(bool val, string name);

    /// <summary>当在派生类中重写时，向已经连接到格式化程序的流中写入一个 8 位无符号整数。</summary>
    /// <param name="val">要写入的值。</param>
    /// <param name="name">成员的名称。</param>
    protected abstract void WriteByte(byte val, string name);

    /// <summary>当在派生类中重写时，向已经连接到格式化程序的流中写入一个 Unicode 字符。</summary>
    /// <param name="val">要写入的值。</param>
    /// <param name="name">成员的名称。</param>
    protected abstract void WriteChar(char val, string name);

    /// <summary>当在派生类中重写时，向已经连接到格式化程序的流中写入一个 <see cref="T:System.DateTime" /> 值。</summary>
    /// <param name="val">要写入的值。</param>
    /// <param name="name">成员的名称。</param>
    protected abstract void WriteDateTime(DateTime val, string name);

    /// <summary>当在派生类中重写时，向已经连接到格式化程序的流中写入一个 <see cref="T:System.Decimal" /> 值。</summary>
    /// <param name="val">要写入的值。</param>
    /// <param name="name">成员的名称。</param>
    protected abstract void WriteDecimal(Decimal val, string name);

    /// <summary>当在派生类中重写时，向已经连接到格式化程序的流中写入一个双精度浮点数字。</summary>
    /// <param name="val">要写入的值。</param>
    /// <param name="name">成员的名称。</param>
    protected abstract void WriteDouble(double val, string name);

    /// <summary>当在派生类中重写时，向已经连接到格式化程序的流中写入一个 16 位有符号整数。</summary>
    /// <param name="val">要写入的值。</param>
    /// <param name="name">成员的名称。</param>
    protected abstract void WriteInt16(short val, string name);

    /// <summary>当在派生类中重写时，向流中写入一个 32 位有符号整数。</summary>
    /// <param name="val">要写入的值。</param>
    /// <param name="name">成员的名称。</param>
    protected abstract void WriteInt32(int val, string name);

    /// <summary>当在派生类中重写时，向流中写入一个 64 位有符号整数。</summary>
    /// <param name="val">要写入的值。</param>
    /// <param name="name">成员的名称。</param>
    protected abstract void WriteInt64(long val, string name);

    /// <summary>当在派生类中重写时，向已经连接到格式化程序的流中写入一个对象引用。</summary>
    /// <param name="obj">要写入的对象引用。</param>
    /// <param name="name">成员的名称。</param>
    /// <param name="memberType">引用指向的对象的类型。</param>
    protected abstract void WriteObjectRef(object obj, string name, Type memberType);

    /// <summary>检查所接收的数据的类型，并调用相应的 Write 方法向已经连接到格式化程序的流中写入对象。</summary>
    /// <param name="memberName">要序列化的成员的名称。</param>
    /// <param name="data">要向连接到格式化程序的流中写入的对象。</param>
    protected virtual void WriteMember(string memberName, object data)
    {
      if (data == null)
      {
        this.WriteObjectRef(data, memberName, typeof (object));
      }
      else
      {
        Type type = data.GetType();
        if (type == typeof (bool))
          this.WriteBoolean(Convert.ToBoolean(data, (IFormatProvider) CultureInfo.InvariantCulture), memberName);
        else if (type == typeof (char))
          this.WriteChar(Convert.ToChar(data, (IFormatProvider) CultureInfo.InvariantCulture), memberName);
        else if (type == typeof (sbyte))
          this.WriteSByte(Convert.ToSByte(data, (IFormatProvider) CultureInfo.InvariantCulture), memberName);
        else if (type == typeof (byte))
          this.WriteByte(Convert.ToByte(data, (IFormatProvider) CultureInfo.InvariantCulture), memberName);
        else if (type == typeof (short))
          this.WriteInt16(Convert.ToInt16(data, (IFormatProvider) CultureInfo.InvariantCulture), memberName);
        else if (type == typeof (int))
          this.WriteInt32(Convert.ToInt32(data, (IFormatProvider) CultureInfo.InvariantCulture), memberName);
        else if (type == typeof (long))
          this.WriteInt64(Convert.ToInt64(data, (IFormatProvider) CultureInfo.InvariantCulture), memberName);
        else if (type == typeof (float))
          this.WriteSingle(Convert.ToSingle(data, (IFormatProvider) CultureInfo.InvariantCulture), memberName);
        else if (type == typeof (double))
          this.WriteDouble(Convert.ToDouble(data, (IFormatProvider) CultureInfo.InvariantCulture), memberName);
        else if (type == typeof (DateTime))
          this.WriteDateTime(Convert.ToDateTime(data, (IFormatProvider) CultureInfo.InvariantCulture), memberName);
        else if (type == typeof (Decimal))
          this.WriteDecimal(Convert.ToDecimal(data, (IFormatProvider) CultureInfo.InvariantCulture), memberName);
        else if (type == typeof (ushort))
          this.WriteUInt16(Convert.ToUInt16(data, (IFormatProvider) CultureInfo.InvariantCulture), memberName);
        else if (type == typeof (uint))
          this.WriteUInt32(Convert.ToUInt32(data, (IFormatProvider) CultureInfo.InvariantCulture), memberName);
        else if (type == typeof (ulong))
          this.WriteUInt64(Convert.ToUInt64(data, (IFormatProvider) CultureInfo.InvariantCulture), memberName);
        else if (type.IsArray)
          this.WriteArray(data, memberName, type);
        else if (type.IsValueType)
          this.WriteValueType(data, memberName, type);
        else
          this.WriteObjectRef(data, memberName, type);
      }
    }

    /// <summary>当在派生类中重写时，向已经连接到格式化程序的流中写入一个 8 位有符号整数。</summary>
    /// <param name="val">要写入的值。</param>
    /// <param name="name">成员的名称。</param>
    [CLSCompliant(false)]
    protected abstract void WriteSByte(sbyte val, string name);

    /// <summary>当在派生类中重写时，向已经连接到格式化程序的流中写入一个单精度浮点数字。</summary>
    /// <param name="val">要写入的值。</param>
    /// <param name="name">成员的名称。</param>
    protected abstract void WriteSingle(float val, string name);

    /// <summary>当在派生类中重写时，向已经连接到格式化程序的流中写入一个 <see cref="T:System.TimeSpan" /> 值。</summary>
    /// <param name="val">要写入的值。</param>
    /// <param name="name">成员的名称。</param>
    protected abstract void WriteTimeSpan(TimeSpan val, string name);

    /// <summary>当在派生类中重写时，向已经连接到格式化程序的流中写入一个 16 位无符号整数。</summary>
    /// <param name="val">要写入的值。</param>
    /// <param name="name">成员的名称。</param>
    [CLSCompliant(false)]
    protected abstract void WriteUInt16(ushort val, string name);

    /// <summary>当在派生类中重写时，向已经连接到格式化程序的流中写入一个 32 位无符号整数。</summary>
    /// <param name="val">要写入的值。</param>
    /// <param name="name">成员的名称。</param>
    [CLSCompliant(false)]
    protected abstract void WriteUInt32(uint val, string name);

    /// <summary>当在派生类中重写时，向已经连接到格式化程序的流中写入一个 64 位无符号整数。</summary>
    /// <param name="val">要写入的值。</param>
    /// <param name="name">成员的名称。</param>
    [CLSCompliant(false)]
    protected abstract void WriteUInt64(ulong val, string name);

    /// <summary>当在派生类中重写时，向已经连接到格式化程序的流中写入给定类型的值。</summary>
    /// <param name="obj">表示值类型的对象。</param>
    /// <param name="name">成员的名称。</param>
    /// <param name="memberType">值类型的 <see cref="T:System.Type" />。</param>
    protected abstract void WriteValueType(object obj, string name, Type memberType);
  }
}
