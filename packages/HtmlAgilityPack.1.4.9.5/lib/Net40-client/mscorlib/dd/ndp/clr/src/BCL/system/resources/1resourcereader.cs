// Decompiled with JetBrains decompiler
// Type: System.Resources.ResourceReader
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Collections.Generic;
using System.Configuration.Assemblies;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;
using System.Text;

namespace System.Resources
{
  /// <summary>通过读取顺序资源名称/值对枚举二进制资源 (.resources) 文件的资源。安全说明：在此类不受信任的数据中调用方法存在安全风险。仅在受信任的数据类中调用方法。有关详细信息，请参阅 Untrusted Data Security Risks。</summary>
  [ComVisible(true)]
  public sealed class ResourceReader : IResourceReader, IEnumerable, IDisposable
  {
    private static readonly string[] TypesSafeForDeserialization = new string[21]{ "System.String[], mscorlib, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.DateTime[], mscorlib, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Bitmap, System.Drawing, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Imaging.Metafile, System.Drawing, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Point, System.Drawing, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.PointF, System.Drawing, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Size, System.Drawing, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.SizeF, System.Drawing, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Font, System.Drawing, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Icon, System.Drawing, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Color, System.Drawing, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Windows.Forms.Cursor, System.Windows.Forms, Culture=neutral, PublicKeyToken=b77a5c561934e089", "System.Windows.Forms.Padding, System.Windows.Forms, Culture=neutral, PublicKeyToken=b77a5c561934e089", "System.Windows.Forms.LinkArea, System.Windows.Forms, Culture=neutral, PublicKeyToken=b77a5c561934e089", "System.Windows.Forms.ImageListStreamer, System.Windows.Forms, Culture=neutral, PublicKeyToken=b77a5c561934e089", "System.Windows.Forms.ListViewGroup, System.Windows.Forms, Culture=neutral, PublicKeyToken=b77a5c561934e089", "System.Windows.Forms.ListViewItem, System.Windows.Forms, Culture=neutral, PublicKeyToken=b77a5c561934e089", "System.Windows.Forms.ListViewItem+ListViewSubItem, System.Windows.Forms, Culture=neutral, PublicKeyToken=b77a5c561934e089", "System.Windows.Forms.ListViewItem+ListViewSubItem+SubItemStyle, System.Windows.Forms, Culture=neutral, PublicKeyToken=b77a5c561934e089", "System.Windows.Forms.OwnerDrawPropertyBag, System.Windows.Forms, Culture=neutral, PublicKeyToken=b77a5c561934e089", "System.Windows.Forms.TreeNode, System.Windows.Forms, Culture=neutral, PublicKeyToken=b77a5c561934e089" };
    private const int DefaultFileStreamBufferSize = 4096;
    private BinaryReader _store;
    internal Dictionary<string, ResourceLocator> _resCache;
    private long _nameSectionOffset;
    private long _dataSectionOffset;
    private int[] _nameHashes;
    [SecurityCritical]
    private unsafe int* _nameHashesPtr;
    private int[] _namePositions;
    [SecurityCritical]
    private unsafe int* _namePositionsPtr;
    private RuntimeType[] _typeTable;
    private int[] _typeNamePositions;
    private BinaryFormatter _objFormatter;
    private int _numResources;
    private UnmanagedMemoryStream _ums;
    private int _version;
    private bool[] _safeToDeserialize;
    private ResourceReader.TypeLimitingDeserializationBinder _typeLimitingBinder;

    /// <summary>为指定的资源文件初始化 <see cref="T:System.Resources.ResourceReader" /> 类的新实例。</summary>
    /// <param name="fileName">要读取的源文件的路径及名称。<paramref name="filename" /> 不区分大小写。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="fileName" /> 参数为 null。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">无法找到该文件。</exception>
    /// <exception cref="T:System.IO.IOException">出现 I/O 错误。</exception>
    /// <exception cref="T:System.BadImageFormatException">资源文件的格式无效。例如，文件的长度可能为零。</exception>
    [SecuritySafeCritical]
    public ResourceReader(string fileName)
    {
      this._resCache = new Dictionary<string, ResourceLocator>((IEqualityComparer<string>) FastResourceComparer.Default);
      this._store = new BinaryReader((Stream) new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, FileOptions.RandomAccess, Path.GetFileName(fileName), false), Encoding.UTF8);
      try
      {
        this.ReadResources();
      }
      catch
      {
        this._store.Close();
        throw;
      }
    }

    /// <summary>为指定的流初始化 <see cref="T:System.Resources.ResourceReader" /> 类的新实例。</summary>
    /// <param name="stream">用于读取资源的输入流。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="stream" /> 参数不可读。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="stream" /> 参数为 null。</exception>
    /// <exception cref="T:System.IO.IOException">在访问 <paramref name="stream" /> 时发生 I/O 错误。</exception>
    [SecurityCritical]
    public ResourceReader(Stream stream)
    {
      if (stream == null)
        throw new ArgumentNullException("stream");
      if (!stream.CanRead)
        throw new ArgumentException(Environment.GetResourceString("Argument_StreamNotReadable"));
      this._resCache = new Dictionary<string, ResourceLocator>((IEqualityComparer<string>) FastResourceComparer.Default);
      this._store = new BinaryReader(stream, Encoding.UTF8);
      this._ums = stream as UnmanagedMemoryStream;
      this.ReadResources();
    }

    [SecurityCritical]
    internal ResourceReader(Stream stream, Dictionary<string, ResourceLocator> resCache)
    {
      this._resCache = resCache;
      this._store = new BinaryReader(stream, Encoding.UTF8);
      this._ums = stream as UnmanagedMemoryStream;
      this.ReadResources();
    }

    /// <summary>释放与此 <see cref="T:System.Resources.ResourceReader" /> 对象相关联的所有操作系统资源。</summary>
    public void Close()
    {
      this.Dispose(true);
    }

    /// <summary>释放 <see cref="T:System.Resources.ResourceReader" /> 类的当前实例所使用的所有资源。</summary>
    public void Dispose()
    {
      this.Close();
    }

    [SecuritySafeCritical]
    private unsafe void Dispose(bool disposing)
    {
      if (this._store == null)
        return;
      this._resCache = (Dictionary<string, ResourceLocator>) null;
      if (disposing)
      {
        BinaryReader binaryReader = this._store;
        this._store = (BinaryReader) null;
        if (binaryReader != null)
          binaryReader.Close();
      }
      this._store = (BinaryReader) null;
      this._namePositions = (int[]) null;
      this._nameHashes = (int[]) null;
      this._ums = (UnmanagedMemoryStream) null;
      this._namePositionsPtr = (int*) null;
      this._nameHashesPtr = (int*) null;
    }

    [SecurityCritical]
    internal static unsafe int ReadUnalignedI4(int* p)
    {
      byte* numPtr = (byte*) p;
      return (int) *numPtr | (int) numPtr[1] << 8 | (int) numPtr[2] << 16 | (int) numPtr[3] << 24;
    }

    private void SkipInt32()
    {
      this._store.BaseStream.Seek(4L, SeekOrigin.Current);
    }

    private void SkipString()
    {
      int num = this._store.Read7BitEncodedInt();
      if (num < 0)
        throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_NegativeStringLength"));
      this._store.BaseStream.Seek((long) num, SeekOrigin.Current);
    }

    [SecuritySafeCritical]
    private unsafe int GetNameHash(int index)
    {
      if (this._ums == null)
        return this._nameHashes[index];
      return ResourceReader.ReadUnalignedI4(this._nameHashesPtr + index);
    }

    [SecuritySafeCritical]
    private unsafe int GetNamePosition(int index)
    {
      int num = this._ums != null ? ResourceReader.ReadUnalignedI4(this._namePositionsPtr + index) : this._namePositions[index];
      if (num < 0 || (long) num > this._dataSectionOffset - this._nameSectionOffset)
        throw new FormatException(Environment.GetResourceString("BadImageFormat_ResourcesNameInvalidOffset", (object) num));
      return num;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.GetEnumerator();
    }

    /// <summary>返回此 <see cref="T:System.Resources.ResourceReader" /> 对象的枚举器。</summary>
    /// <returns>此 <see cref="T:System.Resources.ResourceReader" /> 对象的枚举器。</returns>
    /// <exception cref="T:System.InvalidOperationException">读取器已关闭或已释放，因此无法访问。</exception>
    public IDictionaryEnumerator GetEnumerator()
    {
      if (this._resCache == null)
        throw new InvalidOperationException(Environment.GetResourceString("ResourceReaderIsClosed"));
      return (IDictionaryEnumerator) new ResourceReader.ResourceEnumerator(this);
    }

    internal ResourceReader.ResourceEnumerator GetEnumeratorInternal()
    {
      return new ResourceReader.ResourceEnumerator(this);
    }

    internal int FindPosForResource(string name)
    {
      int num1 = FastResourceComparer.HashFunction(name);
      int num2 = 0;
      int num3 = this._numResources - 1;
      int index = -1;
      bool flag = false;
      while (num2 <= num3)
      {
        index = num2 + num3 >> 1;
        int nameHash = this.GetNameHash(index);
        int num4 = nameHash != num1 ? (nameHash >= num1 ? 1 : -1) : 0;
        if (num4 == 0)
        {
          flag = true;
          break;
        }
        if (num4 < 0)
          num2 = index + 1;
        else
          num3 = index - 1;
      }
      if (!flag)
        return -1;
      if (num2 != index)
      {
        num2 = index;
        while (num2 > 0 && this.GetNameHash(num2 - 1) == num1)
          --num2;
      }
      if (num3 != index)
      {
        num3 = index;
        while (num3 < this._numResources - 1 && this.GetNameHash(num3 + 1) == num1)
          ++num3;
      }
      lock (this)
      {
        for (int local_9 = num2; local_9 <= num3; ++local_9)
        {
          this._store.BaseStream.Seek(this._nameSectionOffset + (long) this.GetNamePosition(local_9), SeekOrigin.Begin);
          if (this.CompareStringEqualsName(name))
          {
            int local_10 = this._store.ReadInt32();
            if (local_10 < 0 || (long) local_10 >= this._store.BaseStream.Length - this._dataSectionOffset)
              throw new FormatException(Environment.GetResourceString("BadImageFormat_ResourcesDataInvalidOffset", (object) local_10));
            return local_10;
          }
        }
      }
      return -1;
    }

    [SecuritySafeCritical]
    private unsafe bool CompareStringEqualsName(string name)
    {
      int length = this._store.Read7BitEncodedInt();
      if (length < 0)
        throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_NegativeStringLength"));
      if (this._ums != null)
      {
        byte* positionPointer = this._ums.PositionPointer;
        this._ums.Seek((long) length, SeekOrigin.Current);
        if (this._ums.Position > this._ums.Length)
          throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourcesNameTooLong"));
        int byteLen = length;
        string b = name;
        return FastResourceComparer.CompareOrdinal(positionPointer, byteLen, b) == 0;
      }
      byte[] numArray = new byte[length];
      int count = length;
      while (count > 0)
      {
        int num = this._store.Read(numArray, length - count, count);
        if (num == 0)
          throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourceNameCorrupted"));
        count -= num;
      }
      return FastResourceComparer.CompareOrdinal(numArray, length / 2, name) == 0;
    }

    [SecurityCritical]
    private unsafe string AllocateStringForNameIndex(int index, out int dataOffset)
    {
      long num = (long) this.GetNamePosition(index);
      int count;
      byte[] numArray;
      lock (this)
      {
        this._store.BaseStream.Seek(num + this._nameSectionOffset, SeekOrigin.Begin);
        count = this._store.Read7BitEncodedInt();
        if (count < 0)
          throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_NegativeStringLength"));
        if (this._ums != null)
        {
          if (this._ums.Position > this._ums.Length - (long) count)
            throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourcesIndexTooLong", (object) index));
          string local_6_1 = new string((char*) this._ums.PositionPointer, 0, count / 2);
          this._ums.Position += (long) count;
          dataOffset = this._store.ReadInt32();
          if (dataOffset < 0 || (long) dataOffset >= this._store.BaseStream.Length - this._dataSectionOffset)
            throw new FormatException(Environment.GetResourceString("BadImageFormat_ResourcesDataInvalidOffset", (object) dataOffset));
          return local_6_1;
        }
        numArray = new byte[count];
        int local_5 = count;
        while (local_5 > 0)
        {
          int local_9 = this._store.Read(numArray, count - local_5, local_5);
          if (local_9 == 0)
            throw new EndOfStreamException(Environment.GetResourceString("BadImageFormat_ResourceNameCorrupted_NameIndex", (object) index));
          local_5 -= local_9;
        }
        dataOffset = this._store.ReadInt32();
        if (dataOffset >= 0)
        {
          if ((long) dataOffset < this._store.BaseStream.Length - this._dataSectionOffset)
            goto label_20;
        }
        throw new FormatException(Environment.GetResourceString("BadImageFormat_ResourcesDataInvalidOffset", (object) dataOffset));
      }
label_20:
      return Encoding.Unicode.GetString(numArray, 0, count);
    }

    private object GetValueForNameIndex(int index)
    {
      long num = (long) this.GetNamePosition(index);
      lock (this)
      {
        this._store.BaseStream.Seek(num + this._nameSectionOffset, SeekOrigin.Begin);
        this.SkipString();
        int local_3 = this._store.ReadInt32();
        if (local_3 < 0 || (long) local_3 >= this._store.BaseStream.Length - this._dataSectionOffset)
          throw new FormatException(Environment.GetResourceString("BadImageFormat_ResourcesDataInvalidOffset", (object) local_3));
        if (this._version == 1)
          return this.LoadObjectV1(local_3);
        ResourceTypeCode local_4;
        return this.LoadObjectV2(local_3, out local_4);
      }
    }

    internal string LoadString(int pos)
    {
      this._store.BaseStream.Seek(this._dataSectionOffset + (long) pos, SeekOrigin.Begin);
      string str = (string) null;
      int typeIndex = this._store.Read7BitEncodedInt();
      if (this._version == 1)
      {
        if (typeIndex == -1)
          return (string) null;
        if ((Type) this.FindType(typeIndex) != typeof (string))
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ResourceNotString_Type", (object) this.FindType(typeIndex).FullName));
        str = this._store.ReadString();
      }
      else
      {
        ResourceTypeCode resourceTypeCode = (ResourceTypeCode) typeIndex;
        switch (resourceTypeCode)
        {
          case ResourceTypeCode.String:
          case ResourceTypeCode.Null:
            if (resourceTypeCode == ResourceTypeCode.String)
            {
              str = this._store.ReadString();
              break;
            }
            break;
          default:
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ResourceNotString_Type", (object) (resourceTypeCode >= ResourceTypeCode.StartOfUserTypes ? this.FindType((int) (resourceTypeCode - 64)).FullName : resourceTypeCode.ToString())));
        }
      }
      return str;
    }

    internal object LoadObject(int pos)
    {
      if (this._version == 1)
        return this.LoadObjectV1(pos);
      ResourceTypeCode typeCode;
      return this.LoadObjectV2(pos, out typeCode);
    }

    internal object LoadObject(int pos, out ResourceTypeCode typeCode)
    {
      if (this._version != 1)
        return this.LoadObjectV2(pos, out typeCode);
      object obj = this.LoadObjectV1(pos);
      typeCode = obj is string ? ResourceTypeCode.String : ResourceTypeCode.StartOfUserTypes;
      return obj;
    }

    internal object LoadObjectV1(int pos)
    {
      try
      {
        return this._LoadObjectV1(pos);
      }
      catch (EndOfStreamException ex)
      {
        throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_TypeMismatch"), (Exception) ex);
      }
      catch (ArgumentOutOfRangeException ex)
      {
        throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_TypeMismatch"), (Exception) ex);
      }
    }

    [SecuritySafeCritical]
    private object _LoadObjectV1(int pos)
    {
      this._store.BaseStream.Seek(this._dataSectionOffset + (long) pos, SeekOrigin.Begin);
      int typeIndex = this._store.Read7BitEncodedInt();
      if (typeIndex == -1)
        return (object) null;
      RuntimeType type = this.FindType(typeIndex);
      if ((Type) type == typeof (string))
        return (object) this._store.ReadString();
      if ((Type) type == typeof (int))
        return (object) this._store.ReadInt32();
      if ((Type) type == typeof (byte))
        return (object) this._store.ReadByte();
      if ((Type) type == typeof (sbyte))
        return (object) this._store.ReadSByte();
      if ((Type) type == typeof (short))
        return (object) this._store.ReadInt16();
      if ((Type) type == typeof (long))
        return (object) this._store.ReadInt64();
      if ((Type) type == typeof (ushort))
        return (object) this._store.ReadUInt16();
      if ((Type) type == typeof (uint))
        return (object) this._store.ReadUInt32();
      if ((Type) type == typeof (ulong))
        return (object) this._store.ReadUInt64();
      if ((Type) type == typeof (float))
        return (object) this._store.ReadSingle();
      if ((Type) type == typeof (double))
        return (object) this._store.ReadDouble();
      if ((Type) type == typeof (DateTime))
        return (object) new DateTime(this._store.ReadInt64());
      if ((Type) type == typeof (TimeSpan))
        return (object) new TimeSpan(this._store.ReadInt64());
      if (!((Type) type == typeof (Decimal)))
        return this.DeserializeObject(typeIndex);
      int[] bits = new int[4];
      for (int index = 0; index < bits.Length; ++index)
        bits[index] = this._store.ReadInt32();
      return (object) new Decimal(bits);
    }

    internal object LoadObjectV2(int pos, out ResourceTypeCode typeCode)
    {
      try
      {
        return this._LoadObjectV2(pos, out typeCode);
      }
      catch (EndOfStreamException ex)
      {
        throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_TypeMismatch"), (Exception) ex);
      }
      catch (ArgumentOutOfRangeException ex)
      {
        throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_TypeMismatch"), (Exception) ex);
      }
    }

    [SecuritySafeCritical]
    private unsafe object _LoadObjectV2(int pos, out ResourceTypeCode typeCode)
    {
      this._store.BaseStream.Seek(this._dataSectionOffset + (long) pos, SeekOrigin.Begin);
      typeCode = (ResourceTypeCode) this._store.Read7BitEncodedInt();
      switch (typeCode)
      {
        case ResourceTypeCode.Null:
          return (object) null;
        case ResourceTypeCode.String:
          return (object) this._store.ReadString();
        case ResourceTypeCode.Boolean:
          return (object) this._store.ReadBoolean();
        case ResourceTypeCode.Char:
          return (object) (char) this._store.ReadUInt16();
        case ResourceTypeCode.Byte:
          return (object) this._store.ReadByte();
        case ResourceTypeCode.SByte:
          return (object) this._store.ReadSByte();
        case ResourceTypeCode.Int16:
          return (object) this._store.ReadInt16();
        case ResourceTypeCode.UInt16:
          return (object) this._store.ReadUInt16();
        case ResourceTypeCode.Int32:
          return (object) this._store.ReadInt32();
        case ResourceTypeCode.UInt32:
          return (object) this._store.ReadUInt32();
        case ResourceTypeCode.Int64:
          return (object) this._store.ReadInt64();
        case ResourceTypeCode.UInt64:
          return (object) this._store.ReadUInt64();
        case ResourceTypeCode.Single:
          return (object) this._store.ReadSingle();
        case ResourceTypeCode.Double:
          return (object) this._store.ReadDouble();
        case ResourceTypeCode.Decimal:
          return (object) this._store.ReadDecimal();
        case ResourceTypeCode.DateTime:
          return (object) DateTime.FromBinary(this._store.ReadInt64());
        case ResourceTypeCode.TimeSpan:
          return (object) new TimeSpan(this._store.ReadInt64());
        case ResourceTypeCode.ByteArray:
          int count1 = this._store.ReadInt32();
          if (count1 < 0)
            throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourceDataLengthInvalid", (object) count1));
          if (this._ums == null)
          {
            if ((long) count1 > this._store.BaseStream.Length)
              throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourceDataLengthInvalid", (object) count1));
            return (object) this._store.ReadBytes(count1);
          }
          if ((long) count1 > this._ums.Length - this._ums.Position)
            throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourceDataLengthInvalid", (object) count1));
          byte[] buffer = new byte[count1];
          this._ums.Read(buffer, 0, count1);
          return (object) buffer;
        case ResourceTypeCode.Stream:
          int count2 = this._store.ReadInt32();
          if (count2 < 0)
            throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourceDataLengthInvalid", (object) count2));
          if (this._ums == null)
            return (object) new PinnedBufferMemoryStream(this._store.ReadBytes(count2));
          if ((long) count2 > this._ums.Length - this._ums.Position)
            throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourceDataLengthInvalid", (object) count2));
          return (object) new UnmanagedMemoryStream(this._ums.PositionPointer, (long) count2, (long) count2, FileAccess.Read, true);
        default:
          if (typeCode < ResourceTypeCode.StartOfUserTypes)
            throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_TypeMismatch"));
          return this.DeserializeObject((int) (typeCode - 64));
      }
    }

    [SecurityCritical]
    private object DeserializeObject(int typeIndex)
    {
      RuntimeType type = this.FindType(typeIndex);
      if (this._safeToDeserialize == null)
        this.InitSafeToDeserializeArray();
      object obj;
      if (this._safeToDeserialize[typeIndex])
      {
        this._objFormatter.Binder = (SerializationBinder) this._typeLimitingBinder;
        this._typeLimitingBinder.ExpectingToDeserialize(type);
        obj = this._objFormatter.UnsafeDeserialize(this._store.BaseStream, (HeaderHandler) null);
      }
      else
      {
        this._objFormatter.Binder = (SerializationBinder) null;
        obj = this._objFormatter.Deserialize(this._store.BaseStream);
      }
      if (obj.GetType() != (Type) type)
        throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResType&SerBlobMismatch", (object) type.FullName, (object) obj.GetType().FullName));
      return obj;
    }

    [SecurityCritical]
    private void ReadResources()
    {
      BinaryFormatter binaryFormatter = new BinaryFormatter((ISurrogateSelector) null, new StreamingContext(StreamingContextStates.File | StreamingContextStates.Persistence));
      this._typeLimitingBinder = new ResourceReader.TypeLimitingDeserializationBinder();
      binaryFormatter.Binder = (SerializationBinder) this._typeLimitingBinder;
      this._objFormatter = binaryFormatter;
      try
      {
        this._ReadResources();
      }
      catch (EndOfStreamException ex)
      {
        throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourcesHeaderCorrupted"), (Exception) ex);
      }
      catch (IndexOutOfRangeException ex)
      {
        throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourcesHeaderCorrupted"), (Exception) ex);
      }
    }

    [SecurityCritical]
    private unsafe void _ReadResources()
    {
      if (this._store.ReadInt32() != ResourceManager.MagicNumber)
        throw new ArgumentException(Environment.GetResourceString("Resources_StreamNotValid"));
      int num1 = this._store.ReadInt32();
      int num2 = this._store.ReadInt32();
      if (num2 < 0 || num1 < 0)
        throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourcesHeaderCorrupted"));
      if (num1 > 1)
      {
        this._store.BaseStream.Seek((long) num2, SeekOrigin.Current);
      }
      else
      {
        string asmTypeName1 = this._store.ReadString();
        AssemblyName asmName2 = new AssemblyName(ResourceManager.MscorlibName);
        if (!ResourceManager.CompareNames(asmTypeName1, ResourceManager.ResReaderTypeName, asmName2))
          throw new NotSupportedException(Environment.GetResourceString("NotSupported_WrongResourceReader_Type", (object) asmTypeName1));
        this.SkipString();
      }
      int num3 = this._store.ReadInt32();
      switch (num3)
      {
        case 2:
        case 1:
          this._version = num3;
          this._numResources = this._store.ReadInt32();
          if (this._numResources < 0)
            throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourcesHeaderCorrupted"));
          int length = this._store.ReadInt32();
          if (length < 0)
            throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourcesHeaderCorrupted"));
          this._typeTable = new RuntimeType[length];
          this._typeNamePositions = new int[length];
          for (int index = 0; index < length; ++index)
          {
            this._typeNamePositions[index] = (int) this._store.BaseStream.Position;
            this.SkipString();
          }
          int num4 = (int) this._store.BaseStream.Position & 7;
          if (num4 != 0)
          {
            for (int index = 0; index < 8 - num4; ++index)
            {
              int num5 = (int) this._store.ReadByte();
            }
          }
          if (this._ums == null)
          {
            this._nameHashes = new int[this._numResources];
            for (int index = 0; index < this._numResources; ++index)
              this._nameHashes[index] = this._store.ReadInt32();
          }
          else
          {
            int num5 = 4 * this._numResources;
            if (num5 < 0)
              throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourcesHeaderCorrupted"));
            this._nameHashesPtr = (int*) this._ums.PositionPointer;
            this._ums.Seek((long) num5, SeekOrigin.Current);
            byte* positionPointer = this._ums.PositionPointer;
          }
          if (this._ums == null)
          {
            this._namePositions = new int[this._numResources];
            for (int index = 0; index < this._numResources; ++index)
            {
              int num5 = this._store.ReadInt32();
              if (num5 < 0)
                throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourcesHeaderCorrupted"));
              this._namePositions[index] = num5;
            }
          }
          else
          {
            int num5 = 4 * this._numResources;
            if (num5 < 0)
              throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourcesHeaderCorrupted"));
            this._namePositionsPtr = (int*) this._ums.PositionPointer;
            this._ums.Seek((long) num5, SeekOrigin.Current);
            byte* positionPointer = this._ums.PositionPointer;
          }
          this._dataSectionOffset = (long) this._store.ReadInt32();
          if (this._dataSectionOffset < 0L)
            throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourcesHeaderCorrupted"));
          this._nameSectionOffset = this._store.BaseStream.Position;
          if (this._dataSectionOffset >= this._nameSectionOffset)
            break;
          throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourcesHeaderCorrupted"));
        default:
          throw new ArgumentException(Environment.GetResourceString("Arg_ResourceFileUnsupportedVersion", (object) 2, (object) num3));
      }
    }

    private RuntimeType FindType(int typeIndex)
    {
      if (typeIndex < 0 || typeIndex >= this._typeTable.Length)
        throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_InvalidType"));
      if (this._typeTable[typeIndex] == (RuntimeType) null)
      {
        long position = this._store.BaseStream.Position;
        try
        {
          this._store.BaseStream.Position = (long) this._typeNamePositions[typeIndex];
          string typeName = this._store.ReadString();
          this._typeTable[typeIndex] = (RuntimeType) Type.GetType(typeName, true);
        }
        finally
        {
          this._store.BaseStream.Position = position;
        }
      }
      return this._typeTable[typeIndex];
    }

    [SecurityCritical]
    private void InitSafeToDeserializeArray()
    {
      this._safeToDeserialize = new bool[this._typeTable.Length];
      for (int index = 0; index < this._typeTable.Length; ++index)
      {
        long position = this._store.BaseStream.Position;
        string typeName;
        try
        {
          this._store.BaseStream.Position = (long) this._typeNamePositions[index];
          typeName = this._store.ReadString();
        }
        finally
        {
          this._store.BaseStream.Position = position;
        }
        RuntimeType runtimeType = (RuntimeType) Type.GetType(typeName, false);
        AssemblyName asmName2;
        string typeName2;
        if (runtimeType == (RuntimeType) null)
        {
          asmName2 = (AssemblyName) null;
          typeName2 = typeName;
        }
        else
        {
          if (runtimeType.BaseType == typeof (Enum))
          {
            this._safeToDeserialize[index] = true;
            continue;
          }
          typeName2 = runtimeType.FullName;
          asmName2 = new AssemblyName();
          RuntimeAssembly runtimeAssembly = (RuntimeAssembly) runtimeType.Assembly;
          asmName2.Init(runtimeAssembly.GetSimpleName(), runtimeAssembly.GetPublicKey(), (byte[]) null, (Version) null, runtimeAssembly.GetLocale(), AssemblyHashAlgorithm.None, AssemblyVersionCompatibility.SameMachine, (string) null, AssemblyNameFlags.PublicKey, (StrongNameKeyPair) null);
        }
        foreach (string asmTypeName1 in ResourceReader.TypesSafeForDeserialization)
        {
          if (ResourceManager.CompareNames(asmTypeName1, typeName2, asmName2))
            this._safeToDeserialize[index] = true;
        }
      }
    }

    /// <summary>从打开的资源文件或流检索指定资源的类型名称和数据。</summary>
    /// <param name="resourceName">资源的名称。</param>
    /// <param name="resourceType">当此方法返回时，包含表示检索资源的类型名称的字符串 (有关详细信息，请参见注释部分)。此参数未经初始化即被传递。</param>
    /// <param name="resourceData">此方法返回时，包含一个字节数组，该字节数组为所检索类型的二进制表示形式。此参数未经初始化即被传递。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="resourceName" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="resourceName" />。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="resourceName" /> 具有无效的类型。</exception>
    /// <exception cref="T:System.FormatException">检索的资源数据已损坏。</exception>
    /// <exception cref="T:System.InvalidOperationException">当前 <see cref="T:System.Resources.ResourceReader" /> 对象未初始化，可能因被其已关闭。</exception>
    public void GetResourceData(string resourceName, out string resourceType, out byte[] resourceData)
    {
      if (resourceName == null)
        throw new ArgumentNullException("resourceName");
      if (this._resCache == null)
        throw new InvalidOperationException(Environment.GetResourceString("ResourceReaderIsClosed"));
      int[] array = new int[this._numResources];
      int posForResource = this.FindPosForResource(resourceName);
      if (posForResource == -1)
        throw new ArgumentException(Environment.GetResourceString("Arg_ResourceNameNotExist", (object) resourceName));
      lock (this)
      {
        for (int local_8 = 0; local_8 < this._numResources; ++local_8)
        {
          this._store.BaseStream.Position = this._nameSectionOffset + (long) this.GetNamePosition(local_8);
          int local_9 = this._store.Read7BitEncodedInt();
          if (local_9 < 0)
            throw new FormatException(Environment.GetResourceString("BadImageFormat_ResourcesNameInvalidOffset", (object) local_9));
          this._store.BaseStream.Position += (long) local_9;
          int local_10 = this._store.ReadInt32();
          if (local_10 < 0 || (long) local_10 >= this._store.BaseStream.Length - this._dataSectionOffset)
            throw new FormatException(Environment.GetResourceString("BadImageFormat_ResourcesDataInvalidOffset", (object) local_10));
          array[local_8] = local_10;
        }
        Array.Sort<int>(array);
        int local_4 = Array.BinarySearch<int>(array, posForResource);
        int local_5 = (int) ((local_4 < this._numResources - 1 ? (long) array[local_4 + 1] + this._dataSectionOffset : this._store.BaseStream.Length) - ((long) posForResource + this._dataSectionOffset));
        this._store.BaseStream.Position = this._dataSectionOffset + (long) posForResource;
        ResourceTypeCode local_6 = (ResourceTypeCode) this._store.Read7BitEncodedInt();
        if (local_6 < ResourceTypeCode.Null || local_6 >= (ResourceTypeCode) (64 + this._typeTable.Length))
          throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_InvalidType"));
        resourceType = this.TypeNameFromTypeCode(local_6);
        int local_5_1 = local_5 - (int) (this._store.BaseStream.Position - (this._dataSectionOffset + (long) posForResource));
        byte[] local_7 = this._store.ReadBytes(local_5_1);
        if (local_7.Length != local_5_1)
          throw new FormatException(Environment.GetResourceString("BadImageFormat_ResourceNameCorrupted"));
        resourceData = local_7;
      }
    }

    private string TypeNameFromTypeCode(ResourceTypeCode typeCode)
    {
      if (typeCode < ResourceTypeCode.StartOfUserTypes)
        return "ResourceTypeCode." + typeCode.ToString();
      int index = (int) (typeCode - 64);
      long position = this._store.BaseStream.Position;
      try
      {
        this._store.BaseStream.Position = (long) this._typeNamePositions[index];
        return this._store.ReadString();
      }
      finally
      {
        this._store.BaseStream.Position = position;
      }
    }

    internal sealed class TypeLimitingDeserializationBinder : SerializationBinder
    {
      private RuntimeType _typeToDeserialize;
      private ObjectReader _objectReader;

      internal ObjectReader ObjectReader
      {
        get
        {
          return this._objectReader;
        }
        set
        {
          this._objectReader = value;
        }
      }

      internal void ExpectingToDeserialize(RuntimeType type)
      {
        this._typeToDeserialize = type;
      }

      [SecuritySafeCritical]
      public override Type BindToType(string assemblyName, string typeName)
      {
        AssemblyName asmName2 = new AssemblyName(assemblyName);
        bool flag = false;
        foreach (string asmTypeName1 in ResourceReader.TypesSafeForDeserialization)
        {
          if (ResourceManager.CompareNames(asmTypeName1, typeName, asmName2))
          {
            flag = true;
            break;
          }
        }
        if (this.ObjectReader.FastBindToType(assemblyName, typeName).IsEnum)
          flag = true;
        if (flag)
          return (Type) null;
        throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResType&SerBlobMismatch", (object) this._typeToDeserialize.FullName, (object) typeName));
      }
    }

    internal sealed class ResourceEnumerator : IDictionaryEnumerator, IEnumerator
    {
      private const int ENUM_DONE = -2147483648;
      private const int ENUM_NOT_STARTED = -1;
      private ResourceReader _reader;
      private bool _currentIsValid;
      private int _currentName;
      private int _dataPosition;

      public object Key
      {
        [SecuritySafeCritical] get
        {
          if (this._currentName == int.MinValue)
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumEnded"));
          if (!this._currentIsValid)
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
          if (this._reader._resCache == null)
            throw new InvalidOperationException(Environment.GetResourceString("ResourceReaderIsClosed"));
          return (object) this._reader.AllocateStringForNameIndex(this._currentName, out this._dataPosition);
        }
      }

      public object Current
      {
        get
        {
          return (object) this.Entry;
        }
      }

      internal int DataPosition
      {
        get
        {
          return this._dataPosition;
        }
      }

      public DictionaryEntry Entry
      {
        [SecuritySafeCritical] get
        {
          if (this._currentName == int.MinValue)
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumEnded"));
          if (!this._currentIsValid)
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
          if (this._reader._resCache == null)
            throw new InvalidOperationException(Environment.GetResourceString("ResourceReaderIsClosed"));
          object obj = (object) null;
          string key;
          lock (this._reader)
          {
            lock (this._reader._resCache)
            {
              key = this._reader.AllocateStringForNameIndex(this._currentName, out this._dataPosition);
              ResourceLocator local_6;
              if (this._reader._resCache.TryGetValue(key, out local_6))
                obj = local_6.Value;
              if (obj == null)
                obj = this._dataPosition != -1 ? this._reader.LoadObject(this._dataPosition) : this._reader.GetValueForNameIndex(this._currentName);
            }
          }
          return new DictionaryEntry((object) key, obj);
        }
      }

      public object Value
      {
        get
        {
          if (this._currentName == int.MinValue)
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumEnded"));
          if (!this._currentIsValid)
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
          if (this._reader._resCache == null)
            throw new InvalidOperationException(Environment.GetResourceString("ResourceReaderIsClosed"));
          return this._reader.GetValueForNameIndex(this._currentName);
        }
      }

      internal ResourceEnumerator(ResourceReader reader)
      {
        this._currentName = -1;
        this._reader = reader;
        this._dataPosition = -2;
      }

      public bool MoveNext()
      {
        if (this._currentName == this._reader._numResources - 1 || this._currentName == int.MinValue)
        {
          this._currentIsValid = false;
          this._currentName = int.MinValue;
          return false;
        }
        this._currentIsValid = true;
        this._currentName = this._currentName + 1;
        return true;
      }

      public void Reset()
      {
        if (this._reader._resCache == null)
          throw new InvalidOperationException(Environment.GetResourceString("ResourceReaderIsClosed"));
        this._currentIsValid = false;
        this._currentName = -1;
      }
    }
  }
}
