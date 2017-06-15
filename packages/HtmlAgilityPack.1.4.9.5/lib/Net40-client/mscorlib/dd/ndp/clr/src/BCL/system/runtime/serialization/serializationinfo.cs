// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.SerializationInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Security;

namespace System.Runtime.Serialization
{
  /// <summary>存储将对象序列化或反序列化所需的全部数据。此类不能被继承。</summary>
  [ComVisible(true)]
  public sealed class SerializationInfo
  {
    private const int defaultSize = 4;
    private const string s_mscorlibAssemblySimpleName = "mscorlib";
    private const string s_mscorlibFileName = "mscorlib.dll";
    internal string[] m_members;
    internal object[] m_data;
    internal Type[] m_types;
    private Dictionary<string, int> m_nameToIndex;
    internal int m_currMember;
    internal IFormatterConverter m_converter;
    private string m_fullTypeName;
    private string m_assemName;
    private Type objectType;
    private bool isFullTypeNameSetExplicit;
    private bool isAssemblyNameSetExplicit;
    private bool requireSameTokenInPartialTrust;

    /// <summary>获取或设置要序列化的 <see cref="T:System.Type" /> 的全名。</summary>
    /// <returns>要序列化的类型的全名。</returns>
    /// <exception cref="T:System.ArgumentNullException">此属性的设置值为 null。</exception>
    public string FullTypeName
    {
      get
      {
        return this.m_fullTypeName;
      }
      set
      {
        if (value == null)
          throw new ArgumentNullException("value");
        this.m_fullTypeName = value;
        this.isFullTypeNameSetExplicit = true;
      }
    }

    /// <summary>仅在序列化期间获取或设置要序列化的类型的程序集名称。</summary>
    /// <returns>要序列化的类型的程序集的全名。</returns>
    /// <exception cref="T:System.ArgumentNullException">该属性的设置值为 null。</exception>
    public string AssemblyName
    {
      get
      {
        return this.m_assemName;
      }
      [SecuritySafeCritical] set
      {
        if (value == null)
          throw new ArgumentNullException("value");
        if (this.requireSameTokenInPartialTrust)
          SerializationInfo.DemandForUnsafeAssemblyNameAssignments(this.m_assemName, value);
        this.m_assemName = value;
        this.isAssemblyNameSetExplicit = true;
      }
    }

    /// <summary>获取已添加到 <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 存储区中的成员的数目。</summary>
    /// <returns>已添加到当前 <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 中的成员的数目。</returns>
    public int MemberCount
    {
      get
      {
        return this.m_currMember;
      }
    }

    /// <summary>返回要序列化的对象的类型。</summary>
    /// <returns>序列化的对象的类型。</returns>
    public Type ObjectType
    {
      get
      {
        return this.objectType;
      }
    }

    /// <summary>获取完整类型名称是否已经显式设置。</summary>
    /// <returns>如果明确设置了完整类型，则为 True；否则为 false。</returns>
    public bool IsFullTypeNameSetExplicit
    {
      get
      {
        return this.isFullTypeNameSetExplicit;
      }
    }

    /// <summary>获取程序集名称是否已经显式设置。</summary>
    /// <returns>如果明确设置了程序集名称，则为 True；否则为 false。</returns>
    public bool IsAssemblyNameSetExplicit
    {
      get
      {
        return this.isAssemblyNameSetExplicit;
      }
    }

    internal string[] MemberNames
    {
      get
      {
        return this.m_members;
      }
    }

    internal object[] MemberValues
    {
      get
      {
        return this.m_data;
      }
    }

    /// <summary>创建 <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 类的新实例。</summary>
    /// <param name="type">要序列化的对象的 <see cref="T:System.Type" />。</param>
    /// <param name="converter">在反序列化过程中使用的 <see cref="T:System.Runtime.Serialization.IFormatterConverter" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="type" /> 或 <paramref name="converter" /> 为 null。</exception>
    [CLSCompliant(false)]
    public SerializationInfo(Type type, IFormatterConverter converter)
      : this(type, converter, false)
    {
    }

    /// <summary>初始化 <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 类的新实例。</summary>
    /// <param name="type">要序列化的对象的 <see cref="T:System.Type" />。</param>
    /// <param name="converter">在反序列化过程中使用的 <see cref="T:System.Runtime.Serialization.IFormatterConverter" />。</param>
    /// <param name="requireSameTokenInPartialTrust">指示对象是否需要部分信任的同一标记。</param>
    [CLSCompliant(false)]
    public SerializationInfo(Type type, IFormatterConverter converter, bool requireSameTokenInPartialTrust)
    {
      if (type == null)
        throw new ArgumentNullException("type");
      if (converter == null)
        throw new ArgumentNullException("converter");
      this.objectType = type;
      this.m_fullTypeName = type.FullName;
      this.m_assemName = type.Module.Assembly.FullName;
      this.m_members = new string[4];
      this.m_data = new object[4];
      this.m_types = new Type[4];
      this.m_nameToIndex = new Dictionary<string, int>();
      this.m_converter = converter;
      this.requireSameTokenInPartialTrust = requireSameTokenInPartialTrust;
    }

    /// <summary>设置要序列化的对象的 <see cref="T:System.Type" />。</summary>
    /// <param name="type">要序列化的对象的 <see cref="T:System.Type" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="type" /> 参数为 null。</exception>
    [SecuritySafeCritical]
    public void SetType(Type type)
    {
      if (type == null)
        throw new ArgumentNullException("type");
      if (this.requireSameTokenInPartialTrust)
        SerializationInfo.DemandForUnsafeAssemblyNameAssignments(this.ObjectType.Assembly.FullName, type.Assembly.FullName);
      if (this.objectType == type)
        return;
      this.objectType = type;
      this.m_fullTypeName = type.FullName;
      this.m_assemName = type.Module.Assembly.FullName;
      this.isFullTypeNameSetExplicit = false;
      this.isAssemblyNameSetExplicit = false;
    }

    private static bool Compare(byte[] a, byte[] b)
    {
      if (a == null || b == null || (a.Length == 0 || b.Length == 0) || a.Length != b.Length)
        return false;
      for (int index = 0; index < a.Length; ++index)
      {
        if ((int) a[index] != (int) b[index])
          return false;
      }
      return true;
    }

    [SecuritySafeCritical]
    internal static void DemandForUnsafeAssemblyNameAssignments(string originalAssemblyName, string newAssemblyName)
    {
      if (SerializationInfo.IsAssemblyNameAssignmentSafe(originalAssemblyName, newAssemblyName))
        return;
      CodeAccessPermission.Demand(PermissionType.SecuritySerialization);
    }

    internal static bool IsAssemblyNameAssignmentSafe(string originalAssemblyName, string newAssemblyName)
    {
      if (originalAssemblyName == newAssemblyName)
        return true;
      System.Reflection.AssemblyName assemblyName1 = new System.Reflection.AssemblyName(originalAssemblyName);
      System.Reflection.AssemblyName assemblyName2 = new System.Reflection.AssemblyName(newAssemblyName);
      if (string.Equals(assemblyName2.Name, "mscorlib", StringComparison.OrdinalIgnoreCase) || string.Equals(assemblyName2.Name, "mscorlib.dll", StringComparison.OrdinalIgnoreCase))
        return false;
      return SerializationInfo.Compare(assemblyName1.GetPublicKeyToken(), assemblyName2.GetPublicKeyToken());
    }

    /// <summary>返回一个 <see cref="T:System.Runtime.Serialization.SerializationInfoEnumerator" />，它用于循环访问 <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 存储区中的名称/值对。</summary>
    /// <returns>一个 <see cref="T:System.Runtime.Serialization.SerializationInfoEnumerator" />，用于分析此 <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 存储区中的名称/值对。</returns>
    public SerializationInfoEnumerator GetEnumerator()
    {
      return new SerializationInfoEnumerator(this.m_members, this.m_data, this.m_types, this.m_currMember);
    }

    private void ExpandArrays()
    {
      int length = this.m_currMember * 2;
      if (length < this.m_currMember && int.MaxValue > this.m_currMember)
        length = int.MaxValue;
      string[] strArray = new string[length];
      object[] objArray = new object[length];
      Type[] typeArray = new Type[length];
      Array.Copy((Array) this.m_members, (Array) strArray, this.m_currMember);
      Array.Copy((Array) this.m_data, (Array) objArray, this.m_currMember);
      Array.Copy((Array) this.m_types, (Array) typeArray, this.m_currMember);
      this.m_members = strArray;
      this.m_data = objArray;
      this.m_types = typeArray;
    }

    /// <summary>将一个值添加到 <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 存储区中，其中 <paramref name="value" /> 与 <paramref name="name" /> 相关联，并序列化为 <see cref="T:System.Type" /><paramref name="type" />。</summary>
    /// <param name="name">将与值关联的名称，因此它可在以后被反序列化。</param>
    /// <param name="value">将序列化的值。此对象的所有子级将自动被序列化。</param>
    /// <param name="type">要与当前对象相关联的 <see cref="T:System.Type" />。此参数必须始终是该对象本身的类型或其一个基类的类型。</param>
    /// <exception cref="T:System.ArgumentNullException">如果 <paramref name="name" /> 或 <paramref name="type" /> 为 null。</exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">已有值与 <paramref name="name" /> 相关联。</exception>
    public void AddValue(string name, object value, Type type)
    {
      if (name == null)
        throw new ArgumentNullException("name");
      if (type == null)
        throw new ArgumentNullException("type");
      this.AddValueInternal(name, value, type);
    }

    /// <summary>将指定的对象添加到与指定的名称关联的 <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 存储区。</summary>
    /// <param name="name">将与值关联的名称，因此它可在以后被反序列化。</param>
    /// <param name="value">将序列化的值。此对象的所有子级将自动被序列化。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">已有值与 <paramref name="name" /> 相关联。</exception>
    public void AddValue(string name, object value)
    {
      if (value == null)
      {
        this.AddValue(name, value, typeof (object));
      }
      else
      {
        string name1 = name;
        object obj = value;
        Type type = obj.GetType();
        this.AddValue(name1, obj, type);
      }
    }

    /// <summary>向 <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 存储区中添加一个布尔值。</summary>
    /// <param name="name">将与值关联的名称，因此它可在以后被反序列化。</param>
    /// <param name="value">要序列化的布尔值。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 参数为 null。</exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">已有值与 <paramref name="name" /> 相关联。</exception>
    public void AddValue(string name, bool value)
    {
      this.AddValue(name, (object) value, typeof (bool));
    }

    /// <summary>向 <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 存储区中添加一个 Unicode 字符值。</summary>
    /// <param name="name">将与值关联的名称，因此它可在以后被反序列化。</param>
    /// <param name="value">要序列化的字符值。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 参数为 null。</exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">已有值与 <paramref name="name" /> 相关联。</exception>
    public void AddValue(string name, char value)
    {
      this.AddValue(name, (object) value, typeof (char));
    }

    /// <summary>向 <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 存储区中添加一个 8 位有符号整数值。</summary>
    /// <param name="name">将与值关联的名称，因此它可在以后被反序列化。</param>
    /// <param name="value">要序列化的 Sbyte 值。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 参数为 null。</exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">已有值与 <paramref name="name" /> 相关联。</exception>
    [CLSCompliant(false)]
    public void AddValue(string name, sbyte value)
    {
      this.AddValue(name, (object) value, typeof (sbyte));
    }

    /// <summary>向 <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 存储区中添加一个 8 位无符号整数值。</summary>
    /// <param name="name">将与值关联的名称，因此它可在以后被反序列化。</param>
    /// <param name="value">要序列化的字节值。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 参数为 null。</exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">已有值与 <paramref name="name" /> 相关联。</exception>
    public void AddValue(string name, byte value)
    {
      this.AddValue(name, (object) value, typeof (byte));
    }

    /// <summary>向 <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 存储区中添加一个 16 位有符号整数值。</summary>
    /// <param name="name">将与值关联的名称，因此它可在以后被反序列化。</param>
    /// <param name="value">要序列化的 Int16 值。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 参数为 null。</exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">已有值与 <paramref name="name" /> 相关联。</exception>
    public void AddValue(string name, short value)
    {
      this.AddValue(name, (object) value, typeof (short));
    }

    /// <summary>向 <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 存储区中添加一个 16 位无符号整数值。</summary>
    /// <param name="name">将与值关联的名称，因此它可在以后被反序列化。</param>
    /// <param name="value">要序列化的 UInt16 值。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 参数为 null。</exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">已有值与 <paramref name="name" /> 相关联。</exception>
    [CLSCompliant(false)]
    public void AddValue(string name, ushort value)
    {
      this.AddValue(name, (object) value, typeof (ushort));
    }

    /// <summary>向 <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 存储区中添加一个 32 位有符号整数值。</summary>
    /// <param name="name">将与值关联的名称，因此它可在以后被反序列化。</param>
    /// <param name="value">要序列化的 Int32 值。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 参数为 null。</exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">已有值与 <paramref name="name" /> 相关联。</exception>
    public void AddValue(string name, int value)
    {
      this.AddValue(name, (object) value, typeof (int));
    }

    /// <summary>向 <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 存储区中添加一个 32 位无符号整数值。</summary>
    /// <param name="name">将与值关联的名称，因此它可在以后被反序列化。</param>
    /// <param name="value">要序列化的 UInt32 值。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 参数为 null。</exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">已有值与 <paramref name="name" /> 相关联。</exception>
    [CLSCompliant(false)]
    public void AddValue(string name, uint value)
    {
      this.AddValue(name, (object) value, typeof (uint));
    }

    /// <summary>向 <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 存储区中添加一个 64 位有符号整数值。</summary>
    /// <param name="name">将与值关联的名称，因此它可在以后被反序列化。</param>
    /// <param name="value">要序列化的 Int64 值。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 参数为 null。</exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">已有值与 <paramref name="name" /> 相关联。</exception>
    public void AddValue(string name, long value)
    {
      this.AddValue(name, (object) value, typeof (long));
    }

    /// <summary>向 <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 存储区中添加一个 64 位无符号整数值。</summary>
    /// <param name="name">将与值关联的名称，因此它可在以后被反序列化。</param>
    /// <param name="value">要序列化的 UInt64 值。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 参数为 null。</exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">已有值与 <paramref name="name" /> 相关联。</exception>
    [CLSCompliant(false)]
    public void AddValue(string name, ulong value)
    {
      this.AddValue(name, (object) value, typeof (ulong));
    }

    /// <summary>向 <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 存储区中添加一个单精度浮点值。</summary>
    /// <param name="name">将与值关联的名称，因此它可在以后被反序列化。</param>
    /// <param name="value">要序列化的 Single 值。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 参数为 null。</exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">已有值与 <paramref name="name" /> 相关联。</exception>
    public void AddValue(string name, float value)
    {
      this.AddValue(name, (object) value, typeof (float));
    }

    /// <summary>向 <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 存储区中添加一个双精度浮点值。</summary>
    /// <param name="name">将与值关联的名称，因此它可在以后被反序列化。</param>
    /// <param name="value">要序列化的 Double 值。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 参数为 null。</exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">已有值与 <paramref name="name" /> 相关联。</exception>
    public void AddValue(string name, double value)
    {
      this.AddValue(name, (object) value, typeof (double));
    }

    /// <summary>向 <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 存储区中添加一个十进制值。</summary>
    /// <param name="name">将与值关联的名称，因此它可在以后被反序列化。</param>
    /// <param name="value">要序列化的十进制值。</param>
    /// <exception cref="T:System.ArgumentNullException">如果 <paramref name="name" /> 参数为 null。</exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">如果已有值与 <paramref name="name" /> 相关联。</exception>
    public void AddValue(string name, Decimal value)
    {
      this.AddValue(name, (object) value, typeof (Decimal));
    }

    /// <summary>向 <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 存储区中添加一个 <see cref="T:System.DateTime" /> 值。</summary>
    /// <param name="name">将与值关联的名称，因此它可在以后被反序列化。</param>
    /// <param name="value">要序列化的 <see cref="T:System.DateTime" /> 值。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 参数为 null。</exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">已有值与 <paramref name="name" /> 相关联。</exception>
    public void AddValue(string name, DateTime value)
    {
      this.AddValue(name, (object) value, typeof (DateTime));
    }

    internal void AddValueInternal(string name, object value, Type type)
    {
      if (this.m_nameToIndex.ContainsKey(name))
        throw new SerializationException(Environment.GetResourceString("Serialization_SameNameTwice"));
      this.m_nameToIndex.Add(name, this.m_currMember);
      if (this.m_currMember >= this.m_members.Length)
        this.ExpandArrays();
      this.m_members[this.m_currMember] = name;
      this.m_data[this.m_currMember] = value;
      this.m_types[this.m_currMember] = type;
      this.m_currMember = this.m_currMember + 1;
    }

    internal void UpdateValue(string name, object value, Type type)
    {
      int element = this.FindElement(name);
      if (element < 0)
      {
        this.AddValueInternal(name, value, type);
      }
      else
      {
        this.m_data[element] = value;
        this.m_types[element] = type;
      }
    }

    private int FindElement(string name)
    {
      if (name == null)
        throw new ArgumentNullException("name");
      int num;
      if (this.m_nameToIndex.TryGetValue(name, out num))
        return num;
      return -1;
    }

    private object GetElement(string name, out Type foundType)
    {
      int element = this.FindElement(name);
      if (element == -1)
        throw new SerializationException(Environment.GetResourceString("Serialization_NotFound", (object) name));
      foundType = this.m_types[element];
      return this.m_data[element];
    }

    [ComVisible(true)]
    private object GetElementNoThrow(string name, out Type foundType)
    {
      int element = this.FindElement(name);
      if (element == -1)
      {
        foundType = (Type) null;
        return (object) null;
      }
      foundType = this.m_types[element];
      return this.m_data[element];
    }

    /// <summary>从 <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 存储区中检索一个值。</summary>
    /// <returns>与 <paramref name="name" /> 关联的指定 <see cref="T:System.Type" /> 的对象。</returns>
    /// <param name="name">与要检索的值关联的名称。</param>
    /// <param name="type">要检索的值的 <see cref="T:System.Type" />。如果存储的值不能转换为该类型，系统将引发 <see cref="T:System.InvalidCastException" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 或 <paramref name="type" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidCastException">与 <paramref name="name" /> 关联的值不能转换为 <paramref name="type" />。</exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">当前实例中没有找到具有指定名称的元素。</exception>
    [SecuritySafeCritical]
    public object GetValue(string name, Type type)
    {
      if (type == null)
        throw new ArgumentNullException("type");
      RuntimeType castType = type as RuntimeType;
      if (castType == (RuntimeType) null)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"));
      Type foundType;
      object element = this.GetElement(name, out foundType);
      if (RemotingServices.IsTransparentProxy(element))
      {
        if (RemotingServices.ProxyCheckCast(RemotingServices.GetRealProxy(element), castType))
          return element;
      }
      else if (foundType == type || type.IsAssignableFrom(foundType) || element == null)
        return element;
      return this.m_converter.Convert(element, type);
    }

    [SecuritySafeCritical]
    [ComVisible(true)]
    internal object GetValueNoThrow(string name, Type type)
    {
      Type foundType;
      object elementNoThrow = this.GetElementNoThrow(name, out foundType);
      if (elementNoThrow == null)
        return (object) null;
      if (RemotingServices.IsTransparentProxy(elementNoThrow))
      {
        if (RemotingServices.ProxyCheckCast(RemotingServices.GetRealProxy(elementNoThrow), (RuntimeType) type))
          return elementNoThrow;
      }
      else if (foundType == type || type.IsAssignableFrom(foundType) || elementNoThrow == null)
        return elementNoThrow;
      return this.m_converter.Convert(elementNoThrow, type);
    }

    /// <summary>从 <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 存储区中检索一个布尔值。</summary>
    /// <returns>与 <paramref name="name" /> 关联的布尔值。</returns>
    /// <param name="name">与要检索的值关联的名称。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidCastException">与 <paramref name="name" /> 关联的值不能转换为布尔值。</exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">当前实例中没有找到具有指定名称的元素。</exception>
    public bool GetBoolean(string name)
    {
      Type foundType;
      object element = this.GetElement(name, out foundType);
      if (foundType == typeof (bool))
        return (bool) element;
      return this.m_converter.ToBoolean(element);
    }

    /// <summary>从 <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 存储区中检索一个 Unicode 字符值。</summary>
    /// <returns>与 <paramref name="name" /> 关联的 Unicode 字符。</returns>
    /// <param name="name">与要检索的值关联的名称。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidCastException">与 <paramref name="name" /> 关联的值不能转换为 Unicode 字符。</exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">当前实例中没有找到具有指定名称的元素。</exception>
    public char GetChar(string name)
    {
      Type foundType;
      object element = this.GetElement(name, out foundType);
      if (foundType == typeof (char))
        return (char) element;
      return this.m_converter.ToChar(element);
    }

    /// <summary>从 <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 存储区中检索一个 8 位有符号整数值。</summary>
    /// <returns>与 <paramref name="name" /> 关联的 8 位有符号整数。</returns>
    /// <param name="name">与要检索的值关联的名称。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidCastException">与 <paramref name="name" /> 关联的值不能转换为 8 位有符号整数。</exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">当前实例中没有找到具有指定名称的元素。</exception>
    [CLSCompliant(false)]
    public sbyte GetSByte(string name)
    {
      Type foundType;
      object element = this.GetElement(name, out foundType);
      if (foundType == typeof (sbyte))
        return (sbyte) element;
      return this.m_converter.ToSByte(element);
    }

    /// <summary>从 <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 存储区中检索一个 8 位无符号整数值。</summary>
    /// <returns>与 <paramref name="name" /> 关联的 8 位无符号整数。</returns>
    /// <param name="name">与要检索的值关联的名称。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidCastException">与 <paramref name="name" /> 关联的值不能转换为 8 位无符号整数。</exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">当前实例中没有找到具有指定名称的元素。</exception>
    public byte GetByte(string name)
    {
      Type foundType;
      object element = this.GetElement(name, out foundType);
      if (foundType == typeof (byte))
        return (byte) element;
      return this.m_converter.ToByte(element);
    }

    /// <summary>从 <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 存储区中检索一个 16 位有符号整数值。</summary>
    /// <returns>与 <paramref name="name" /> 关联的 16 位有符号整数。</returns>
    /// <param name="name">与要检索的值关联的名称。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidCastException">与 <paramref name="name" /> 关联的值不能转换为 16 位有符号整数。</exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">当前实例中没有找到具有指定名称的元素。</exception>
    public short GetInt16(string name)
    {
      Type foundType;
      object element = this.GetElement(name, out foundType);
      if (foundType == typeof (short))
        return (short) element;
      return this.m_converter.ToInt16(element);
    }

    /// <summary>从 <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 存储区中检索一个 16 位无符号整数值。</summary>
    /// <returns>与 <paramref name="name" /> 关联的 16 位无符号整数。</returns>
    /// <param name="name">与要检索的值关联的名称。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidCastException">与 <paramref name="name" /> 关联的值不能转换为 16 位无符号整数。</exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">当前实例中没有找到具有指定名称的元素。</exception>
    [CLSCompliant(false)]
    public ushort GetUInt16(string name)
    {
      Type foundType;
      object element = this.GetElement(name, out foundType);
      if (foundType == typeof (ushort))
        return (ushort) element;
      return this.m_converter.ToUInt16(element);
    }

    /// <summary>从 <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 存储区中检索一个 32 位有符号整数值。</summary>
    /// <returns>与 <paramref name="name" /> 关联的 32 位有符号整数。</returns>
    /// <param name="name">要检索的值的名称。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidCastException">与 <paramref name="name" /> 关联的值不能转换为 32 位有符号整数。</exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">当前实例中没有找到具有指定名称的元素。</exception>
    public int GetInt32(string name)
    {
      Type foundType;
      object element = this.GetElement(name, out foundType);
      if (foundType == typeof (int))
        return (int) element;
      return this.m_converter.ToInt32(element);
    }

    /// <summary>从 <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 存储区中检索一个 32 位无符号整数值。</summary>
    /// <returns>与 <paramref name="name" /> 关联的 32 位无符号整数。</returns>
    /// <param name="name">与要检索的值关联的名称。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidCastException">与 <paramref name="name" /> 关联的值不能转换为 32 位无符号整数。</exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">当前实例中没有找到具有指定名称的元素。</exception>
    [CLSCompliant(false)]
    public uint GetUInt32(string name)
    {
      Type foundType;
      object element = this.GetElement(name, out foundType);
      if (foundType == typeof (uint))
        return (uint) element;
      return this.m_converter.ToUInt32(element);
    }

    /// <summary>从 <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 存储区中检索一个 64 位有符号整数值。</summary>
    /// <returns>与 <paramref name="name" /> 关联的 64 位有符号整数。</returns>
    /// <param name="name">与要检索的值关联的名称。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidCastException">与 <paramref name="name" /> 关联的值不能转换为 64 位有符号整数。</exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">当前实例中没有找到具有指定名称的元素。</exception>
    public long GetInt64(string name)
    {
      Type foundType;
      object element = this.GetElement(name, out foundType);
      if (foundType == typeof (long))
        return (long) element;
      return this.m_converter.ToInt64(element);
    }

    /// <summary>从 <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 存储区中检索一个 64 位无符号整数值。</summary>
    /// <returns>与 <paramref name="name" /> 关联的 64 位无符号整数。</returns>
    /// <param name="name">与要检索的值关联的名称。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidCastException">与 <paramref name="name" /> 关联的值不能转换为 64 位无符号整数。</exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">当前实例中没有找到具有指定名称的元素。</exception>
    [CLSCompliant(false)]
    public ulong GetUInt64(string name)
    {
      Type foundType;
      object element = this.GetElement(name, out foundType);
      if (foundType == typeof (ulong))
        return (ulong) element;
      return this.m_converter.ToUInt64(element);
    }

    /// <summary>从 <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 存储区中检索一个单精度浮点值。</summary>
    /// <returns>与 <paramref name="name" /> 关联的单精度浮点值。</returns>
    /// <param name="name">要检索的值的名称。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidCastException">与 <paramref name="name" /> 关联的值不能转换为单精度浮点值。</exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">当前实例中没有找到具有指定名称的元素。</exception>
    public float GetSingle(string name)
    {
      Type foundType;
      object element = this.GetElement(name, out foundType);
      if (foundType == typeof (float))
        return (float) element;
      return this.m_converter.ToSingle(element);
    }

    /// <summary>从 <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 存储区中检索一个双精度浮点值。</summary>
    /// <returns>与 <paramref name="name" /> 关联的双精度浮点值。</returns>
    /// <param name="name">与要检索的值关联的名称。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidCastException">与 <paramref name="name" /> 关联的值不能转换为双精度浮点值。</exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">当前实例中没有找到具有指定名称的元素。</exception>
    public double GetDouble(string name)
    {
      Type foundType;
      object element = this.GetElement(name, out foundType);
      if (foundType == typeof (double))
        return (double) element;
      return this.m_converter.ToDouble(element);
    }

    /// <summary>从 <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 存储区中检索一个十进制值。</summary>
    /// <returns>
    /// <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 中的十进制值。</returns>
    /// <param name="name">与要检索的值关联的名称。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidCastException">与 <paramref name="name" /> 关联的值不能转换为十进制值。</exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">当前实例中没有找到具有指定名称的元素。</exception>
    public Decimal GetDecimal(string name)
    {
      Type foundType;
      object element = this.GetElement(name, out foundType);
      if (foundType == typeof (Decimal))
        return (Decimal) element;
      return this.m_converter.ToDecimal(element);
    }

    /// <summary>从 <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 存储区中检索一个 <see cref="T:System.DateTime" /> 值。</summary>
    /// <returns>与 <paramref name="name" /> 关联的 <see cref="T:System.DateTime" /> 值。</returns>
    /// <param name="name">与要检索的值关联的名称。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidCastException">与 <paramref name="name" /> 关联的值不能转换为 <see cref="T:System.DateTime" /> 值。</exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">当前实例中没有找到具有指定名称的元素。</exception>
    public DateTime GetDateTime(string name)
    {
      Type foundType;
      object element = this.GetElement(name, out foundType);
      if (foundType == typeof (DateTime))
        return (DateTime) element;
      return this.m_converter.ToDateTime(element);
    }

    /// <summary>从 <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 存储区中检索一个 <see cref="T:System.String" /> 值。</summary>
    /// <returns>与 <paramref name="name" /> 关联的 <see cref="T:System.String" />。</returns>
    /// <param name="name">与要检索的值关联的名称。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidCastException">与 <paramref name="name" /> 关联的值不能转换为 <see cref="T:System.String" />。</exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">当前实例中没有找到具有指定名称的元素。</exception>
    public string GetString(string name)
    {
      Type foundType;
      object element = this.GetElement(name, out foundType);
      if (foundType == typeof (string) || element == null)
        return (string) element;
      return this.m_converter.ToString(element);
    }
  }
}
