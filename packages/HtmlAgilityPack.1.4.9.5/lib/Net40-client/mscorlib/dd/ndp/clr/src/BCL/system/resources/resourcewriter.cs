// Decompiled with JetBrains decompiler
// Type: System.Resources.ResourceWriter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Versioning;
using System.Security;
using System.Security.Permissions;
using System.Text;

namespace System.Resources
{
  /// <summary>以系统默认的格式将资源写入输出文件或输出流。此类不能被继承。</summary>
  [ComVisible(true)]
  public sealed class ResourceWriter : IResourceWriter, IDisposable
  {
    private Func<Type, string> typeConverter;
    private const int _ExpectedNumberOfResources = 1000;
    private const int AverageNameSize = 40;
    private const int AverageValueSize = 40;
    private Dictionary<string, object> _resourceList;
    private Stream _output;
    private Dictionary<string, object> _caseInsensitiveDups;
    private Dictionary<string, ResourceWriter.PrecannedResource> _preserializedData;
    private const int _DefaultBufferSize = 4096;

    /// <summary>获取或设置一个委托，通过该委托，可以使用限定的程序集名称以 .NET Framework 4 之前的 .NET Framework 目标版本编写资源程序集。</summary>
    /// <returns>由委托封装的类型。</returns>
    public Func<Type, string> TypeNameConverter
    {
      get
      {
        return this.typeConverter;
      }
      set
      {
        this.typeConverter = value;
      }
    }

    /// <summary>初始化 <see cref="T:System.Resources.ResourceWriter" /> 类的新实例，它将资源写入到指定文件中。</summary>
    /// <param name="fileName">输出文件名。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="fileName" /> 参数为 null。</exception>
    public ResourceWriter(string fileName)
    {
      if (fileName == null)
        throw new ArgumentNullException("fileName");
      this._output = (Stream) new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None);
      this._resourceList = new Dictionary<string, object>(1000, (IEqualityComparer<string>) FastResourceComparer.Default);
      this._caseInsensitiveDups = new Dictionary<string, object>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    }

    /// <summary>初始化 <see cref="T:System.Resources.ResourceWriter" /> 类的新实例，它将资源写入到提供的流中。</summary>
    /// <param name="stream">输出流。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="stream" /> 参数不可写。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="stream" /> 参数为 null。</exception>
    public ResourceWriter(Stream stream)
    {
      if (stream == null)
        throw new ArgumentNullException("stream");
      if (!stream.CanWrite)
        throw new ArgumentException(Environment.GetResourceString("Argument_StreamNotWritable"));
      this._output = stream;
      this._resourceList = new Dictionary<string, object>(1000, (IEqualityComparer<string>) FastResourceComparer.Default);
      this._caseInsensitiveDups = new Dictionary<string, object>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    }

    /// <summary>向要写入的资源的列表中添加字符串资源。</summary>
    /// <param name="name">资源的名称。</param>
    /// <param name="value">资源的值。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="name" />（或仅大小写不同的名称）已被添加到该 ResourceWriter 中。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 参数为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">该 <see cref="T:System.Resources.ResourceWriter" /> 已关闭，并且哈希表不可用。</exception>
    public void AddResource(string name, string value)
    {
      if (name == null)
        throw new ArgumentNullException("name");
      if (this._resourceList == null)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ResourceWriterSaved"));
      this._caseInsensitiveDups.Add(name, (object) null);
      this._resourceList.Add(name, (object) value);
    }

    /// <summary>将指定为对象的已命名资源添加到要写入的资源列表中。</summary>
    /// <param name="name">资源的名称。</param>
    /// <param name="value">资源的值。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="name" />（或仅大小写不同的名称）已被添加到该 <see cref="T:System.Resources.ResourceWriter" />。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 参数为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">该 <see cref="T:System.Resources.ResourceWriter" /> 已关闭，并且哈希表不可用。</exception>
    public void AddResource(string name, object value)
    {
      if (name == null)
        throw new ArgumentNullException("name");
      if (this._resourceList == null)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ResourceWriterSaved"));
      if (value != null && value is Stream)
      {
        this.AddResourceInternal(name, (Stream) value, false);
      }
      else
      {
        this._caseInsensitiveDups.Add(name, (object) null);
        this._resourceList.Add(name, value);
      }
    }

    /// <summary>将指定的命名资源以流的形式添加到要写入的资源列表中。</summary>
    /// <param name="name">要添加的资源的名称。</param>
    /// <param name="value">要添加的资源的值。该资源必须支持 <see cref="P:System.IO.Stream.Length" /> 属性。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="name" />（或仅大小写不同的名称）已被添加到该 <see cref="T:System.Resources.ResourceWriter" />。- 或 -流不支持 <see cref="P:System.IO.Stream.Length" /> 属性。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 或 <paramref name="value" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">此 <see cref="T:System.Resources.ResourceWriter" /> 已关闭。</exception>
    public void AddResource(string name, Stream value)
    {
      if (name == null)
        throw new ArgumentNullException("name");
      if (this._resourceList == null)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ResourceWriterSaved"));
      this.AddResourceInternal(name, value, false);
    }

    /// <summary>将指定的命名资源以流的形式添加到要写入的资源列表中，并指定是否应在调用 <see cref="M:System.Resources.ResourceWriter.Generate" /> 方法后关闭该流。</summary>
    /// <param name="name">要添加的资源的名称。</param>
    /// <param name="value">要添加的资源的值。该资源必须支持 <see cref="P:System.IO.Stream.Length" /> 属性。</param>
    /// <param name="closeAfterWrite">如果在调用 <see cref="M:System.Resources.ResourceWriter.Generate" /> 方法后关闭该流，则为 true；否则为 false。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="name" />（或仅大小写不同的名称）已被添加到该 <see cref="T:System.Resources.ResourceWriter" />。- 或 -流不支持 <see cref="P:System.IO.Stream.Length" /> 属性。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 或 <paramref name="value" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">此 <see cref="T:System.Resources.ResourceWriter" /> 已关闭。</exception>
    public void AddResource(string name, Stream value, bool closeAfterWrite)
    {
      if (name == null)
        throw new ArgumentNullException("name");
      if (this._resourceList == null)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ResourceWriterSaved"));
      this.AddResourceInternal(name, value, closeAfterWrite);
    }

    private void AddResourceInternal(string name, Stream value, bool closeAfterWrite)
    {
      if (value == null)
      {
        this._caseInsensitiveDups.Add(name, (object) null);
        this._resourceList.Add(name, (object) value);
      }
      else
      {
        if (!value.CanSeek)
          throw new ArgumentException(Environment.GetResourceString("NotSupported_UnseekableStream"));
        this._caseInsensitiveDups.Add(name, (object) null);
        this._resourceList.Add(name, (object) new ResourceWriter.StreamWrapper(value, closeAfterWrite));
      }
    }

    /// <summary>将指定为字节数组的已命名资源添加到要写入的资源列表中。</summary>
    /// <param name="name">资源的名称。</param>
    /// <param name="value">8 位无符号整数数组形式的资源值。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="name" />（或仅大小写不同的名称）已被添加到该 <see cref="T:System.Resources.ResourceWriter" />。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 参数为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">该 <see cref="T:System.Resources.ResourceWriter" /> 已关闭，并且哈希表不可用。</exception>
    public void AddResource(string name, byte[] value)
    {
      if (name == null)
        throw new ArgumentNullException("name");
      if (this._resourceList == null)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ResourceWriterSaved"));
      this._caseInsensitiveDups.Add(name, (object) null);
      this._resourceList.Add(name, (object) value);
    }

    /// <summary>将数据单位作为资源添加到要写入的资源列表中。</summary>
    /// <param name="name">用于标识包含已添加数据的资源的名称。</param>
    /// <param name="typeName">已添加数据的类型名称。有关更多信息，请参见“备注”一节。</param>
    /// <param name="serializedData">包含已添加数据的二进制表示形式的字节数组。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" />、<paramref name="typeName" /> 或 <paramref name="serializedData" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="name" />（或仅大小写不同的名称）已被添加到此 <see cref="T:System.Resources.ResourceWriter" /> 对象中。</exception>
    /// <exception cref="T:System.InvalidOperationException">未初始化当前 <see cref="T:System.Resources.ResourceWriter" /> 对象。可能的原因是 <see cref="T:System.Resources.ResourceWriter" /> 对象已关闭。</exception>
    public void AddResourceData(string name, string typeName, byte[] serializedData)
    {
      if (name == null)
        throw new ArgumentNullException("name");
      if (typeName == null)
        throw new ArgumentNullException("typeName");
      if (serializedData == null)
        throw new ArgumentNullException("serializedData");
      if (this._resourceList == null)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ResourceWriterSaved"));
      this._caseInsensitiveDups.Add(name, (object) null);
      if (this._preserializedData == null)
        this._preserializedData = new Dictionary<string, ResourceWriter.PrecannedResource>((IEqualityComparer<string>) FastResourceComparer.Default);
      this._preserializedData.Add(name, new ResourceWriter.PrecannedResource(typeName, serializedData));
    }

    /// <summary>将资源保存到输出流，然后关闭输出流。</summary>
    /// <exception cref="T:System.IO.IOException">出现 I/O 错误。</exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">对象序列化期间出现错误。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public void Close()
    {
      this.Dispose(true);
    }

    private void Dispose(bool disposing)
    {
      if (disposing)
      {
        if (this._resourceList != null)
          this.Generate();
        if (this._output != null)
          this._output.Close();
      }
      this._output = (Stream) null;
      this._caseInsensitiveDups = (Dictionary<string, object>) null;
    }

    /// <summary>允许用户关闭资源文件或流，从而显式地释放资源。</summary>
    /// <exception cref="T:System.IO.IOException">出现 I/O 错误。</exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">对象序列化期间出现错误。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public void Dispose()
    {
      this.Dispose(true);
    }

    /// <summary>将所有资源以系统默认格式保存到输出流。</summary>
    /// <exception cref="T:System.IO.IOException">发生了 I/O 错误。</exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">对象序列化期间出现错误。</exception>
    /// <exception cref="T:System.InvalidOperationException">该 <see cref="T:System.Resources.ResourceWriter" /> 已关闭，并且哈希表不可用。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public void Generate()
    {
      if (this._resourceList == null)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ResourceWriterSaved"));
      BinaryWriter binaryWriter1 = new BinaryWriter(this._output, Encoding.UTF8);
      List<string> types = new List<string>();
      binaryWriter1.Write(ResourceManager.MagicNumber);
      binaryWriter1.Write(ResourceManager.HeaderVersionNumber);
      MemoryStream memoryStream1 = new MemoryStream(240);
      BinaryWriter binaryWriter2 = new BinaryWriter((Stream) memoryStream1);
      string assemblyQualifiedName = MultitargetingHelpers.GetAssemblyQualifiedName(typeof (ResourceReader), this.typeConverter);
      binaryWriter2.Write(assemblyQualifiedName);
      string str = ResourceManager.ResSetTypeName;
      binaryWriter2.Write(str);
      binaryWriter2.Flush();
      binaryWriter1.Write((int) memoryStream1.Length);
      binaryWriter1.Write(memoryStream1.GetBuffer(), 0, (int) memoryStream1.Length);
      binaryWriter1.Write(2);
      int count = this._resourceList.Count;
      if (this._preserializedData != null)
        count += this._preserializedData.Count;
      binaryWriter1.Write(count);
      int[] keys = new int[count];
      int[] items = new int[count];
      int index1 = 0;
      MemoryStream memoryStream2 = new MemoryStream(count * 40);
      BinaryWriter binaryWriter3 = new BinaryWriter((Stream) memoryStream2, Encoding.Unicode);
      Stream output = (Stream) null;
      PermissionSet permissionSet = new PermissionSet(PermissionState.None);
      permissionSet.AddPermission((IPermission) new EnvironmentPermission(PermissionState.Unrestricted));
      permissionSet.AddPermission((IPermission) new FileIOPermission(PermissionState.Unrestricted));
      try
      {
        permissionSet.Assert();
        string tempFileName = Path.GetTempFileName();
        int num1 = 8448;
        File.SetAttributes(tempFileName, (FileAttributes) num1);
        int num2 = 3;
        int num3 = 3;
        int num4 = 1;
        int bufferSize = 4096;
        int num5 = 201326592;
        output = (Stream) new FileStream(tempFileName, (FileMode) num2, (FileAccess) num3, (FileShare) num4, bufferSize, (FileOptions) num5);
      }
      catch (UnauthorizedAccessException ex)
      {
        output = (Stream) new MemoryStream();
      }
      catch (IOException ex)
      {
        output = (Stream) new MemoryStream();
      }
      finally
      {
        PermissionSet.RevertAssert();
      }
      using (output)
      {
        BinaryWriter binaryWriter4 = new BinaryWriter(output, Encoding.UTF8);
        IFormatter objFormatter = (IFormatter) new BinaryFormatter((ISurrogateSelector) null, new StreamingContext(StreamingContextStates.File | StreamingContextStates.Persistence));
        SortedList sortedList = new SortedList((IDictionary) this._resourceList, (IComparer) FastResourceComparer.Default);
        if (this._preserializedData != null)
        {
          foreach (KeyValuePair<string, ResourceWriter.PrecannedResource> keyValuePair in this._preserializedData)
            sortedList.Add((object) keyValuePair.Key, (object) keyValuePair.Value);
        }
        IDictionaryEnumerator enumerator = sortedList.GetEnumerator();
        while (enumerator.MoveNext())
        {
          keys[index1] = FastResourceComparer.HashFunction((string) enumerator.Key);
          items[index1++] = (int) binaryWriter3.Seek(0, SeekOrigin.Current);
          binaryWriter3.Write((string) enumerator.Key);
          binaryWriter3.Write((int) binaryWriter4.Seek(0, SeekOrigin.Current));
          object obj = enumerator.Value;
          ResourceTypeCode typeCode = this.FindTypeCode(obj, types);
          ResourceWriter.Write7BitEncodedInt(binaryWriter4, (int) typeCode);
          ResourceWriter.PrecannedResource precannedResource = obj as ResourceWriter.PrecannedResource;
          if (precannedResource != null)
            binaryWriter4.Write(precannedResource.Data);
          else
            this.WriteValue(typeCode, obj, binaryWriter4, objFormatter);
        }
        binaryWriter1.Write(types.Count);
        for (int index2 = 0; index2 < types.Count; ++index2)
          binaryWriter1.Write(types[index2]);
        Array.Sort<int, int>(keys, items);
        binaryWriter1.Flush();
        int num1 = (int) binaryWriter1.BaseStream.Position & 7;
        if (num1 > 0)
        {
          for (int index2 = 0; index2 < 8 - num1; ++index2)
            binaryWriter1.Write("PAD"[index2 % 3]);
        }
        foreach (int num2 in keys)
          binaryWriter1.Write(num2);
        foreach (int num2 in items)
          binaryWriter1.Write(num2);
        binaryWriter1.Flush();
        binaryWriter3.Flush();
        binaryWriter4.Flush();
        int num3 = (int) (binaryWriter1.Seek(0, SeekOrigin.Current) + memoryStream2.Length) + 4;
        binaryWriter1.Write(num3);
        binaryWriter1.Write(memoryStream2.GetBuffer(), 0, (int) memoryStream2.Length);
        binaryWriter3.Close();
        output.Position = 0L;
        output.CopyTo(binaryWriter1.BaseStream);
        binaryWriter4.Close();
      }
      binaryWriter1.Flush();
      this._resourceList = (Dictionary<string, object>) null;
    }

    private ResourceTypeCode FindTypeCode(object value, List<string> types)
    {
      if (value == null)
        return ResourceTypeCode.Null;
      Type type = value.GetType();
      if (type == typeof (string))
        return ResourceTypeCode.String;
      if (type == typeof (int))
        return ResourceTypeCode.Int32;
      if (type == typeof (bool))
        return ResourceTypeCode.Boolean;
      if (type == typeof (char))
        return ResourceTypeCode.Char;
      if (type == typeof (byte))
        return ResourceTypeCode.Byte;
      if (type == typeof (sbyte))
        return ResourceTypeCode.SByte;
      if (type == typeof (short))
        return ResourceTypeCode.Int16;
      if (type == typeof (long))
        return ResourceTypeCode.Int64;
      if (type == typeof (ushort))
        return ResourceTypeCode.UInt16;
      if (type == typeof (uint))
        return ResourceTypeCode.UInt32;
      if (type == typeof (ulong))
        return ResourceTypeCode.UInt64;
      if (type == typeof (float))
        return ResourceTypeCode.Single;
      if (type == typeof (double))
        return ResourceTypeCode.Double;
      if (type == typeof (Decimal))
        return ResourceTypeCode.Decimal;
      if (type == typeof (DateTime))
        return ResourceTypeCode.DateTime;
      if (type == typeof (TimeSpan))
        return ResourceTypeCode.TimeSpan;
      if (type == typeof (byte[]))
        return ResourceTypeCode.ByteArray;
      if (type == typeof (ResourceWriter.StreamWrapper))
        return ResourceTypeCode.Stream;
      string str;
      if (type == typeof (ResourceWriter.PrecannedResource))
      {
        str = ((ResourceWriter.PrecannedResource) value).TypeName;
        if (str.StartsWith("ResourceTypeCode.", StringComparison.Ordinal))
          return (ResourceTypeCode) Enum.Parse(typeof (ResourceTypeCode), str.Substring(17));
      }
      else
        str = MultitargetingHelpers.GetAssemblyQualifiedName(type, this.typeConverter);
      int num = types.IndexOf(str);
      if (num == -1)
      {
        num = types.Count;
        types.Add(str);
      }
      return (ResourceTypeCode) (num + 64);
    }

    private void WriteValue(ResourceTypeCode typeCode, object value, BinaryWriter writer, IFormatter objFormatter)
    {
      switch (typeCode)
      {
        case ResourceTypeCode.Null:
          break;
        case ResourceTypeCode.String:
          writer.Write((string) value);
          break;
        case ResourceTypeCode.Boolean:
          writer.Write((bool) value);
          break;
        case ResourceTypeCode.Char:
          writer.Write((ushort) (char) value);
          break;
        case ResourceTypeCode.Byte:
          writer.Write((byte) value);
          break;
        case ResourceTypeCode.SByte:
          writer.Write((sbyte) value);
          break;
        case ResourceTypeCode.Int16:
          writer.Write((short) value);
          break;
        case ResourceTypeCode.UInt16:
          writer.Write((ushort) value);
          break;
        case ResourceTypeCode.Int32:
          writer.Write((int) value);
          break;
        case ResourceTypeCode.UInt32:
          writer.Write((uint) value);
          break;
        case ResourceTypeCode.Int64:
          writer.Write((long) value);
          break;
        case ResourceTypeCode.UInt64:
          writer.Write((ulong) value);
          break;
        case ResourceTypeCode.Single:
          writer.Write((float) value);
          break;
        case ResourceTypeCode.Double:
          writer.Write((double) value);
          break;
        case ResourceTypeCode.Decimal:
          writer.Write((Decimal) value);
          break;
        case ResourceTypeCode.DateTime:
          long binary = ((DateTime) value).ToBinary();
          writer.Write(binary);
          break;
        case ResourceTypeCode.TimeSpan:
          writer.Write(((TimeSpan) value).Ticks);
          break;
        case ResourceTypeCode.ByteArray:
          byte[] buffer1 = (byte[]) value;
          writer.Write(buffer1.Length);
          writer.Write(buffer1, 0, buffer1.Length);
          break;
        case ResourceTypeCode.Stream:
          ResourceWriter.StreamWrapper streamWrapper = (ResourceWriter.StreamWrapper) value;
          if (streamWrapper.m_stream.GetType() == typeof (MemoryStream))
          {
            MemoryStream memoryStream = (MemoryStream) streamWrapper.m_stream;
            if (memoryStream.Length > (long) int.MaxValue)
              throw new ArgumentException(Environment.GetResourceString("ArgumentOutOfRange_StreamLength"));
            int index;
            // ISSUE: explicit reference operation
            // ISSUE: variable of a reference type
            int& origin = @index;
            int count;
            // ISSUE: explicit reference operation
            // ISSUE: variable of a reference type
            int& length = @count;
            memoryStream.InternalGetOriginAndLength(origin, length);
            byte[] buffer2 = memoryStream.InternalGetBuffer();
            writer.Write(count);
            writer.Write(buffer2, index, count);
            break;
          }
          Stream stream = streamWrapper.m_stream;
          if (stream.Length > (long) int.MaxValue)
            throw new ArgumentException(Environment.GetResourceString("ArgumentOutOfRange_StreamLength"));
          stream.Position = 0L;
          writer.Write((int) stream.Length);
          byte[] buffer3 = new byte[4096];
          int count1;
          while ((count1 = stream.Read(buffer3, 0, buffer3.Length)) != 0)
            writer.Write(buffer3, 0, count1);
          if (!streamWrapper.m_closeAfterWrite)
            break;
          stream.Close();
          break;
        default:
          objFormatter.Serialize(writer.BaseStream, value);
          break;
      }
    }

    private static void Write7BitEncodedInt(BinaryWriter store, int value)
    {
      uint num = (uint) value;
      while (num >= 128U)
      {
        store.Write((byte) (num | 128U));
        num >>= 7;
      }
      store.Write((byte) num);
    }

    private class PrecannedResource
    {
      internal string TypeName;
      internal byte[] Data;

      internal PrecannedResource(string typeName, byte[] data)
      {
        this.TypeName = typeName;
        this.Data = data;
      }
    }

    private class StreamWrapper
    {
      internal Stream m_stream;
      internal bool m_closeAfterWrite;

      internal StreamWrapper(Stream s, bool closeAfterWrite)
      {
        this.m_stream = s;
        this.m_closeAfterWrite = closeAfterWrite;
      }
    }
  }
}
