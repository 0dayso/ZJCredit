﻿// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.Formatters.Binary.__BinaryParser
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.IO;
using System.Security;
using System.Text;

namespace System.Runtime.Serialization.Formatters.Binary
{
  internal sealed class __BinaryParser
  {
    private static Encoding encoding = (Encoding) new UTF8Encoding(false, true);
    internal SerStack stack = new SerStack("ObjectProgressStack");
    internal BinaryTypeEnum expectedType = BinaryTypeEnum.ObjectUrt;
    internal ObjectReader objectReader;
    internal Stream input;
    internal long topId;
    internal long headerId;
    internal SizedArray objectMapIdTable;
    internal SizedArray assemIdToAssemblyTable;
    internal object expectedTypeInformation;
    internal ParseRecord PRS;
    private BinaryAssemblyInfo systemAssemblyInfo;
    private BinaryReader dataReader;
    private SerStack opPool;
    private BinaryObject binaryObject;
    private BinaryObjectWithMap bowm;
    private BinaryObjectWithMapTyped bowmt;
    internal BinaryObjectString objectString;
    internal BinaryCrossAppDomainString crossAppDomainString;
    internal MemberPrimitiveTyped memberPrimitiveTyped;
    private byte[] byteBuffer;
    private const int chunkSize = 4096;
    internal MemberPrimitiveUnTyped memberPrimitiveUnTyped;
    internal MemberReference memberReference;
    internal ObjectNull objectNull;
    internal static volatile MessageEnd messageEnd;

    internal BinaryAssemblyInfo SystemAssemblyInfo
    {
      get
      {
        if (this.systemAssemblyInfo == null)
          this.systemAssemblyInfo = new BinaryAssemblyInfo(Converter.urtAssemblyString, Converter.urtAssembly);
        return this.systemAssemblyInfo;
      }
    }

    internal SizedArray ObjectMapIdTable
    {
      get
      {
        if (this.objectMapIdTable == null)
          this.objectMapIdTable = new SizedArray();
        return this.objectMapIdTable;
      }
    }

    internal SizedArray AssemIdToAssemblyTable
    {
      get
      {
        if (this.assemIdToAssemblyTable == null)
          this.assemIdToAssemblyTable = new SizedArray(2);
        return this.assemIdToAssemblyTable;
      }
    }

    internal ParseRecord prs
    {
      get
      {
        if (this.PRS == null)
          this.PRS = new ParseRecord();
        return this.PRS;
      }
    }

    internal __BinaryParser(Stream stream, ObjectReader objectReader)
    {
      this.input = stream;
      this.objectReader = objectReader;
      this.dataReader = new BinaryReader(this.input, __BinaryParser.encoding);
    }

    [SecurityCritical]
    internal void Run()
    {
      try
      {
        bool flag1 = true;
        this.ReadBegin();
        this.ReadSerializationHeaderRecord();
        while (flag1)
        {
          BinaryHeaderEnum binaryHeaderEnum = BinaryHeaderEnum.Object;
          switch (this.expectedType)
          {
            case BinaryTypeEnum.Primitive:
              this.ReadMemberPrimitiveUnTyped();
              break;
            case BinaryTypeEnum.String:
            case BinaryTypeEnum.Object:
            case BinaryTypeEnum.ObjectUrt:
            case BinaryTypeEnum.ObjectUser:
            case BinaryTypeEnum.ObjectArray:
            case BinaryTypeEnum.StringArray:
            case BinaryTypeEnum.PrimitiveArray:
              byte num = this.dataReader.ReadByte();
              binaryHeaderEnum = (BinaryHeaderEnum) num;
              switch (binaryHeaderEnum)
              {
                case BinaryHeaderEnum.Object:
                  this.ReadObject();
                  break;
                case BinaryHeaderEnum.ObjectWithMap:
                case BinaryHeaderEnum.ObjectWithMapAssemId:
                  this.ReadObjectWithMap(binaryHeaderEnum);
                  break;
                case BinaryHeaderEnum.ObjectWithMapTyped:
                case BinaryHeaderEnum.ObjectWithMapTypedAssemId:
                  this.ReadObjectWithMapTyped(binaryHeaderEnum);
                  break;
                case BinaryHeaderEnum.ObjectString:
                case BinaryHeaderEnum.CrossAppDomainString:
                  this.ReadObjectString(binaryHeaderEnum);
                  break;
                case BinaryHeaderEnum.Array:
                case BinaryHeaderEnum.ArraySinglePrimitive:
                case BinaryHeaderEnum.ArraySingleObject:
                case BinaryHeaderEnum.ArraySingleString:
                  this.ReadArray(binaryHeaderEnum);
                  break;
                case BinaryHeaderEnum.MemberPrimitiveTyped:
                  this.ReadMemberPrimitiveTyped();
                  break;
                case BinaryHeaderEnum.MemberReference:
                  this.ReadMemberReference();
                  break;
                case BinaryHeaderEnum.ObjectNull:
                case BinaryHeaderEnum.ObjectNullMultiple256:
                case BinaryHeaderEnum.ObjectNullMultiple:
                  this.ReadObjectNull(binaryHeaderEnum);
                  break;
                case BinaryHeaderEnum.MessageEnd:
                  flag1 = false;
                  this.ReadMessageEnd();
                  this.ReadEnd();
                  break;
                case BinaryHeaderEnum.Assembly:
                case BinaryHeaderEnum.CrossAppDomainAssembly:
                  this.ReadAssembly(binaryHeaderEnum);
                  break;
                case BinaryHeaderEnum.CrossAppDomainMap:
                  this.ReadCrossAppDomainMap();
                  break;
                case BinaryHeaderEnum.MethodCall:
                case BinaryHeaderEnum.MethodReturn:
                  this.ReadMethodObject(binaryHeaderEnum);
                  break;
                default:
                  throw new SerializationException(Environment.GetResourceString("Serialization_BinaryHeader", (object) num));
              }
            default:
              throw new SerializationException(Environment.GetResourceString("Serialization_TypeExpected"));
          }
          if (binaryHeaderEnum != BinaryHeaderEnum.Assembly)
          {
            bool flag2 = false;
            while (!flag2)
            {
              ObjectProgress op = (ObjectProgress) this.stack.Peek();
              if (op == null)
              {
                this.expectedType = BinaryTypeEnum.ObjectUrt;
                this.expectedTypeInformation = (object) null;
                flag2 = true;
              }
              else
              {
                ObjectProgress objectProgress = op;
                // ISSUE: explicit reference operation
                // ISSUE: variable of a reference type
                BinaryTypeEnum& outBinaryTypeEnum = @objectProgress.expectedType;
                // ISSUE: explicit reference operation
                // ISSUE: variable of a reference type
                object& outTypeInformation = @op.expectedTypeInformation;
                flag2 = objectProgress.GetNext(outBinaryTypeEnum, outTypeInformation);
                this.expectedType = op.expectedType;
                this.expectedTypeInformation = op.expectedTypeInformation;
                if (!flag2)
                {
                  this.prs.Init();
                  if (op.memberValueEnum == InternalMemberValueE.Nested)
                  {
                    this.prs.PRparseTypeEnum = InternalParseTypeE.MemberEnd;
                    this.prs.PRmemberTypeEnum = op.memberTypeEnum;
                    this.prs.PRmemberValueEnum = op.memberValueEnum;
                    this.objectReader.Parse(this.prs);
                  }
                  else
                  {
                    this.prs.PRparseTypeEnum = InternalParseTypeE.ObjectEnd;
                    this.prs.PRmemberTypeEnum = op.memberTypeEnum;
                    this.prs.PRmemberValueEnum = op.memberValueEnum;
                    this.objectReader.Parse(this.prs);
                  }
                  this.stack.Pop();
                  this.PutOp(op);
                }
              }
            }
          }
        }
      }
      catch (EndOfStreamException ex)
      {
        throw new SerializationException(Environment.GetResourceString("Serialization_StreamEnd"));
      }
    }

    internal void ReadBegin()
    {
    }

    internal void ReadEnd()
    {
    }

    internal bool ReadBoolean()
    {
      return this.dataReader.ReadBoolean();
    }

    internal byte ReadByte()
    {
      return this.dataReader.ReadByte();
    }

    internal byte[] ReadBytes(int length)
    {
      return this.dataReader.ReadBytes(length);
    }

    internal void ReadBytes(byte[] byteA, int offset, int size)
    {
      while (size > 0)
      {
        int num = this.dataReader.Read(byteA, offset, size);
        if (num == 0)
          __Error.EndOfFile();
        offset += num;
        size -= num;
      }
    }

    internal char ReadChar()
    {
      return this.dataReader.ReadChar();
    }

    internal char[] ReadChars(int length)
    {
      return this.dataReader.ReadChars(length);
    }

    internal Decimal ReadDecimal()
    {
      return Decimal.Parse(this.dataReader.ReadString(), (IFormatProvider) CultureInfo.InvariantCulture);
    }

    internal float ReadSingle()
    {
      return this.dataReader.ReadSingle();
    }

    internal double ReadDouble()
    {
      return this.dataReader.ReadDouble();
    }

    internal short ReadInt16()
    {
      return this.dataReader.ReadInt16();
    }

    internal int ReadInt32()
    {
      return this.dataReader.ReadInt32();
    }

    internal long ReadInt64()
    {
      return this.dataReader.ReadInt64();
    }

    internal sbyte ReadSByte()
    {
      return (sbyte) this.ReadByte();
    }

    internal string ReadString()
    {
      return this.dataReader.ReadString();
    }

    internal TimeSpan ReadTimeSpan()
    {
      return new TimeSpan(this.ReadInt64());
    }

    internal DateTime ReadDateTime()
    {
      return DateTime.FromBinaryRaw(this.ReadInt64());
    }

    internal ushort ReadUInt16()
    {
      return this.dataReader.ReadUInt16();
    }

    internal uint ReadUInt32()
    {
      return this.dataReader.ReadUInt32();
    }

    internal ulong ReadUInt64()
    {
      return this.dataReader.ReadUInt64();
    }

    [SecurityCritical]
    internal void ReadSerializationHeaderRecord()
    {
      SerializationHeaderRecord serializationHeaderRecord = new SerializationHeaderRecord();
      serializationHeaderRecord.Read(this);
      serializationHeaderRecord.Dump();
      this.topId = serializationHeaderRecord.topId > 0 ? this.objectReader.GetId((long) serializationHeaderRecord.topId) : (long) serializationHeaderRecord.topId;
      this.headerId = serializationHeaderRecord.headerId > 0 ? this.objectReader.GetId((long) serializationHeaderRecord.headerId) : (long) serializationHeaderRecord.headerId;
    }

    [SecurityCritical]
    internal void ReadAssembly(BinaryHeaderEnum binaryHeaderEnum)
    {
      BinaryAssembly binaryAssembly = new BinaryAssembly();
      if (binaryHeaderEnum == BinaryHeaderEnum.CrossAppDomainAssembly)
      {
        BinaryCrossAppDomainAssembly appDomainAssembly = new BinaryCrossAppDomainAssembly();
        appDomainAssembly.Read(this);
        appDomainAssembly.Dump();
        binaryAssembly.assemId = appDomainAssembly.assemId;
        binaryAssembly.assemblyString = this.objectReader.CrossAppDomainArray(appDomainAssembly.assemblyIndex) as string;
        if (binaryAssembly.assemblyString == null)
          throw new SerializationException(Environment.GetResourceString("Serialization_CrossAppDomainError", (object) "String", (object) appDomainAssembly.assemblyIndex));
      }
      else
      {
        binaryAssembly.Read(this);
        binaryAssembly.Dump();
      }
      this.AssemIdToAssemblyTable[binaryAssembly.assemId] = (object) new BinaryAssemblyInfo(binaryAssembly.assemblyString);
    }

    [SecurityCritical]
    internal void ReadMethodObject(BinaryHeaderEnum binaryHeaderEnum)
    {
      if (binaryHeaderEnum == BinaryHeaderEnum.MethodCall)
      {
        BinaryMethodCall binaryMethodCall = new BinaryMethodCall();
        binaryMethodCall.Read(this);
        binaryMethodCall.Dump();
        this.objectReader.SetMethodCall(binaryMethodCall);
      }
      else
      {
        BinaryMethodReturn binaryMethodReturn = new BinaryMethodReturn();
        binaryMethodReturn.Read(this);
        binaryMethodReturn.Dump();
        this.objectReader.SetMethodReturn(binaryMethodReturn);
      }
    }

    [SecurityCritical]
    private void ReadObject()
    {
      if (this.binaryObject == null)
        this.binaryObject = new BinaryObject();
      this.binaryObject.Read(this);
      this.binaryObject.Dump();
      ObjectMap objectMap = (ObjectMap) this.ObjectMapIdTable[this.binaryObject.mapId];
      if (objectMap == null)
        throw new SerializationException(Environment.GetResourceString("Serialization_Map", (object) this.binaryObject.mapId));
      ObjectProgress op = this.GetOp();
      ParseRecord pr = op.pr;
      this.stack.Push((object) op);
      op.objectTypeEnum = InternalObjectTypeE.Object;
      op.binaryTypeEnumA = objectMap.binaryTypeEnumA;
      op.memberNames = objectMap.memberNames;
      op.memberTypes = objectMap.memberTypes;
      op.typeInformationA = objectMap.typeInformationA;
      ObjectProgress objectProgress1 = op;
      int length = objectProgress1.binaryTypeEnumA.Length;
      objectProgress1.memberLength = length;
      ObjectProgress objectProgress2 = (ObjectProgress) this.stack.PeekPeek();
      if (objectProgress2 == null || objectProgress2.isInitial)
      {
        op.name = objectMap.objectName;
        pr.PRparseTypeEnum = InternalParseTypeE.Object;
        op.memberValueEnum = InternalMemberValueE.Empty;
      }
      else
      {
        pr.PRparseTypeEnum = InternalParseTypeE.Member;
        pr.PRmemberValueEnum = InternalMemberValueE.Nested;
        op.memberValueEnum = InternalMemberValueE.Nested;
        switch (objectProgress2.objectTypeEnum)
        {
          case InternalObjectTypeE.Object:
            pr.PRname = objectProgress2.name;
            pr.PRmemberTypeEnum = InternalMemberTypeE.Field;
            op.memberTypeEnum = InternalMemberTypeE.Field;
            break;
          case InternalObjectTypeE.Array:
            pr.PRmemberTypeEnum = InternalMemberTypeE.Item;
            op.memberTypeEnum = InternalMemberTypeE.Item;
            break;
          default:
            throw new SerializationException(Environment.GetResourceString("Serialization_Map", (object) objectProgress2.objectTypeEnum.ToString()));
        }
      }
      pr.PRobjectId = this.objectReader.GetId((long) this.binaryObject.objectId);
      pr.PRobjectInfo = objectMap.CreateObjectInfo(ref pr.PRsi, ref pr.PRmemberData);
      if (pr.PRobjectId == this.topId)
        pr.PRobjectPositionEnum = InternalObjectPositionE.Top;
      pr.PRobjectTypeEnum = InternalObjectTypeE.Object;
      pr.PRkeyDt = objectMap.objectName;
      pr.PRdtType = objectMap.objectType;
      pr.PRdtTypeCode = InternalPrimitiveTypeE.Invalid;
      this.objectReader.Parse(pr);
    }

    [SecurityCritical]
    internal void ReadCrossAppDomainMap()
    {
      BinaryCrossAppDomainMap crossAppDomainMap = new BinaryCrossAppDomainMap();
      crossAppDomainMap.Read(this);
      crossAppDomainMap.Dump();
      object obj = this.objectReader.CrossAppDomainArray(crossAppDomainMap.crossAppDomainArrayIndex);
      BinaryObjectWithMap record1 = obj as BinaryObjectWithMap;
      if (record1 != null)
      {
        record1.Dump();
        this.ReadObjectWithMap(record1);
      }
      else
      {
        BinaryObjectWithMapTyped record2 = obj as BinaryObjectWithMapTyped;
        if (record2 != null)
          this.ReadObjectWithMapTyped(record2);
        else
          throw new SerializationException(Environment.GetResourceString("Serialization_CrossAppDomainError", (object) "BinaryObjectMap", obj));
      }
    }

    [SecurityCritical]
    internal void ReadObjectWithMap(BinaryHeaderEnum binaryHeaderEnum)
    {
      if (this.bowm == null)
        this.bowm = new BinaryObjectWithMap(binaryHeaderEnum);
      else
        this.bowm.binaryHeaderEnum = binaryHeaderEnum;
      this.bowm.Read(this);
      this.bowm.Dump();
      this.ReadObjectWithMap(this.bowm);
    }

    [SecurityCritical]
    private void ReadObjectWithMap(BinaryObjectWithMap record)
    {
      BinaryAssemblyInfo assemblyInfo = (BinaryAssemblyInfo) null;
      ObjectProgress op = this.GetOp();
      ParseRecord pr = op.pr;
      this.stack.Push((object) op);
      if (record.binaryHeaderEnum == BinaryHeaderEnum.ObjectWithMapAssemId)
      {
        if (record.assemId < 1)
          throw new SerializationException(Environment.GetResourceString("Serialization_Assembly", (object) record.name));
        assemblyInfo = (BinaryAssemblyInfo) this.AssemIdToAssemblyTable[record.assemId];
        if (assemblyInfo == null)
          throw new SerializationException(Environment.GetResourceString("Serialization_Assembly", (object) (record.assemId.ToString() + " " + record.name)));
      }
      else if (record.binaryHeaderEnum == BinaryHeaderEnum.ObjectWithMap)
        assemblyInfo = this.SystemAssemblyInfo;
      Type type = this.objectReader.GetType(assemblyInfo, record.name);
      ObjectMap objectMap = ObjectMap.Create(record.name, type, record.memberNames, this.objectReader, record.objectId, assemblyInfo);
      this.ObjectMapIdTable[record.objectId] = (object) objectMap;
      op.objectTypeEnum = InternalObjectTypeE.Object;
      op.binaryTypeEnumA = objectMap.binaryTypeEnumA;
      op.typeInformationA = objectMap.typeInformationA;
      ObjectProgress objectProgress1 = op;
      int length = objectProgress1.binaryTypeEnumA.Length;
      objectProgress1.memberLength = length;
      op.memberNames = objectMap.memberNames;
      op.memberTypes = objectMap.memberTypes;
      ObjectProgress objectProgress2 = (ObjectProgress) this.stack.PeekPeek();
      if (objectProgress2 == null || objectProgress2.isInitial)
      {
        op.name = record.name;
        pr.PRparseTypeEnum = InternalParseTypeE.Object;
        op.memberValueEnum = InternalMemberValueE.Empty;
      }
      else
      {
        pr.PRparseTypeEnum = InternalParseTypeE.Member;
        pr.PRmemberValueEnum = InternalMemberValueE.Nested;
        op.memberValueEnum = InternalMemberValueE.Nested;
        switch (objectProgress2.objectTypeEnum)
        {
          case InternalObjectTypeE.Object:
            pr.PRname = objectProgress2.name;
            pr.PRmemberTypeEnum = InternalMemberTypeE.Field;
            op.memberTypeEnum = InternalMemberTypeE.Field;
            break;
          case InternalObjectTypeE.Array:
            pr.PRmemberTypeEnum = InternalMemberTypeE.Item;
            op.memberTypeEnum = InternalMemberTypeE.Field;
            break;
          default:
            throw new SerializationException(Environment.GetResourceString("Serialization_ObjectTypeEnum", (object) objectProgress2.objectTypeEnum.ToString()));
        }
      }
      pr.PRobjectTypeEnum = InternalObjectTypeE.Object;
      pr.PRobjectId = this.objectReader.GetId((long) record.objectId);
      pr.PRobjectInfo = objectMap.CreateObjectInfo(ref pr.PRsi, ref pr.PRmemberData);
      if (pr.PRobjectId == this.topId)
        pr.PRobjectPositionEnum = InternalObjectPositionE.Top;
      pr.PRkeyDt = record.name;
      pr.PRdtType = objectMap.objectType;
      pr.PRdtTypeCode = InternalPrimitiveTypeE.Invalid;
      this.objectReader.Parse(pr);
    }

    [SecurityCritical]
    internal void ReadObjectWithMapTyped(BinaryHeaderEnum binaryHeaderEnum)
    {
      if (this.bowmt == null)
        this.bowmt = new BinaryObjectWithMapTyped(binaryHeaderEnum);
      else
        this.bowmt.binaryHeaderEnum = binaryHeaderEnum;
      this.bowmt.Read(this);
      this.ReadObjectWithMapTyped(this.bowmt);
    }

    [SecurityCritical]
    private void ReadObjectWithMapTyped(BinaryObjectWithMapTyped record)
    {
      BinaryAssemblyInfo assemblyInfo = (BinaryAssemblyInfo) null;
      ObjectProgress op = this.GetOp();
      ParseRecord pr = op.pr;
      this.stack.Push((object) op);
      if (record.binaryHeaderEnum == BinaryHeaderEnum.ObjectWithMapTypedAssemId)
      {
        if (record.assemId < 1)
          throw new SerializationException(Environment.GetResourceString("Serialization_AssemblyId", (object) record.name));
        assemblyInfo = (BinaryAssemblyInfo) this.AssemIdToAssemblyTable[record.assemId];
        if (assemblyInfo == null)
          throw new SerializationException(Environment.GetResourceString("Serialization_AssemblyId", (object) (record.assemId.ToString() + " " + record.name)));
      }
      else if (record.binaryHeaderEnum == BinaryHeaderEnum.ObjectWithMapTyped)
        assemblyInfo = this.SystemAssemblyInfo;
      ObjectMap objectMap = ObjectMap.Create(record.name, record.memberNames, record.binaryTypeEnumA, record.typeInformationA, record.memberAssemIds, this.objectReader, record.objectId, assemblyInfo, this.AssemIdToAssemblyTable);
      this.ObjectMapIdTable[record.objectId] = (object) objectMap;
      op.objectTypeEnum = InternalObjectTypeE.Object;
      op.binaryTypeEnumA = objectMap.binaryTypeEnumA;
      op.typeInformationA = objectMap.typeInformationA;
      ObjectProgress objectProgress1 = op;
      int length = objectProgress1.binaryTypeEnumA.Length;
      objectProgress1.memberLength = length;
      op.memberNames = objectMap.memberNames;
      op.memberTypes = objectMap.memberTypes;
      ObjectProgress objectProgress2 = (ObjectProgress) this.stack.PeekPeek();
      if (objectProgress2 == null || objectProgress2.isInitial)
      {
        op.name = record.name;
        pr.PRparseTypeEnum = InternalParseTypeE.Object;
        op.memberValueEnum = InternalMemberValueE.Empty;
      }
      else
      {
        pr.PRparseTypeEnum = InternalParseTypeE.Member;
        pr.PRmemberValueEnum = InternalMemberValueE.Nested;
        op.memberValueEnum = InternalMemberValueE.Nested;
        switch (objectProgress2.objectTypeEnum)
        {
          case InternalObjectTypeE.Object:
            pr.PRname = objectProgress2.name;
            pr.PRmemberTypeEnum = InternalMemberTypeE.Field;
            op.memberTypeEnum = InternalMemberTypeE.Field;
            break;
          case InternalObjectTypeE.Array:
            pr.PRmemberTypeEnum = InternalMemberTypeE.Item;
            op.memberTypeEnum = InternalMemberTypeE.Item;
            break;
          default:
            throw new SerializationException(Environment.GetResourceString("Serialization_ObjectTypeEnum", (object) objectProgress2.objectTypeEnum.ToString()));
        }
      }
      pr.PRobjectTypeEnum = InternalObjectTypeE.Object;
      pr.PRobjectInfo = objectMap.CreateObjectInfo(ref pr.PRsi, ref pr.PRmemberData);
      pr.PRobjectId = this.objectReader.GetId((long) record.objectId);
      if (pr.PRobjectId == this.topId)
        pr.PRobjectPositionEnum = InternalObjectPositionE.Top;
      pr.PRkeyDt = record.name;
      pr.PRdtType = objectMap.objectType;
      pr.PRdtTypeCode = InternalPrimitiveTypeE.Invalid;
      this.objectReader.Parse(pr);
    }

    [SecurityCritical]
    private void ReadObjectString(BinaryHeaderEnum binaryHeaderEnum)
    {
      if (this.objectString == null)
        this.objectString = new BinaryObjectString();
      if (binaryHeaderEnum == BinaryHeaderEnum.ObjectString)
      {
        this.objectString.Read(this);
        this.objectString.Dump();
      }
      else
      {
        if (this.crossAppDomainString == null)
          this.crossAppDomainString = new BinaryCrossAppDomainString();
        this.crossAppDomainString.Read(this);
        this.crossAppDomainString.Dump();
        this.objectString.value = this.objectReader.CrossAppDomainArray(this.crossAppDomainString.value) as string;
        if (this.objectString.value == null)
          throw new SerializationException(Environment.GetResourceString("Serialization_CrossAppDomainError", (object) "String", (object) this.crossAppDomainString.value));
        this.objectString.objectId = this.crossAppDomainString.objectId;
      }
      this.prs.Init();
      this.prs.PRparseTypeEnum = InternalParseTypeE.Object;
      this.prs.PRobjectId = this.objectReader.GetId((long) this.objectString.objectId);
      if (this.prs.PRobjectId == this.topId)
        this.prs.PRobjectPositionEnum = InternalObjectPositionE.Top;
      this.prs.PRobjectTypeEnum = InternalObjectTypeE.Object;
      ObjectProgress objectProgress = (ObjectProgress) this.stack.Peek();
      this.prs.PRvalue = this.objectString.value;
      this.prs.PRkeyDt = "System.String";
      this.prs.PRdtType = Converter.typeofString;
      this.prs.PRdtTypeCode = InternalPrimitiveTypeE.Invalid;
      this.prs.PRvarValue = (object) this.objectString.value;
      if (objectProgress == null)
      {
        this.prs.PRparseTypeEnum = InternalParseTypeE.Object;
        this.prs.PRname = "System.String";
      }
      else
      {
        this.prs.PRparseTypeEnum = InternalParseTypeE.Member;
        this.prs.PRmemberValueEnum = InternalMemberValueE.InlineValue;
        switch (objectProgress.objectTypeEnum)
        {
          case InternalObjectTypeE.Object:
            this.prs.PRname = objectProgress.name;
            this.prs.PRmemberTypeEnum = InternalMemberTypeE.Field;
            break;
          case InternalObjectTypeE.Array:
            this.prs.PRmemberTypeEnum = InternalMemberTypeE.Item;
            break;
          default:
            throw new SerializationException(Environment.GetResourceString("Serialization_ObjectTypeEnum", (object) objectProgress.objectTypeEnum.ToString()));
        }
      }
      this.objectReader.Parse(this.prs);
    }

    [SecurityCritical]
    private void ReadMemberPrimitiveTyped()
    {
      if (this.memberPrimitiveTyped == null)
        this.memberPrimitiveTyped = new MemberPrimitiveTyped();
      this.memberPrimitiveTyped.Read(this);
      this.memberPrimitiveTyped.Dump();
      this.prs.PRobjectTypeEnum = InternalObjectTypeE.Object;
      ObjectProgress objectProgress = (ObjectProgress) this.stack.Peek();
      this.prs.Init();
      this.prs.PRvarValue = this.memberPrimitiveTyped.value;
      this.prs.PRkeyDt = Converter.ToComType(this.memberPrimitiveTyped.primitiveTypeEnum);
      this.prs.PRdtType = Converter.ToType(this.memberPrimitiveTyped.primitiveTypeEnum);
      this.prs.PRdtTypeCode = this.memberPrimitiveTyped.primitiveTypeEnum;
      if (objectProgress == null)
      {
        this.prs.PRparseTypeEnum = InternalParseTypeE.Object;
        this.prs.PRname = "System.Variant";
      }
      else
      {
        this.prs.PRparseTypeEnum = InternalParseTypeE.Member;
        this.prs.PRmemberValueEnum = InternalMemberValueE.InlineValue;
        switch (objectProgress.objectTypeEnum)
        {
          case InternalObjectTypeE.Object:
            this.prs.PRname = objectProgress.name;
            this.prs.PRmemberTypeEnum = InternalMemberTypeE.Field;
            break;
          case InternalObjectTypeE.Array:
            this.prs.PRmemberTypeEnum = InternalMemberTypeE.Item;
            break;
          default:
            throw new SerializationException(Environment.GetResourceString("Serialization_ObjectTypeEnum", (object) objectProgress.objectTypeEnum.ToString()));
        }
      }
      this.objectReader.Parse(this.prs);
    }

    [SecurityCritical]
    private void ReadArray(BinaryHeaderEnum binaryHeaderEnum)
    {
      BinaryArray binaryArray = new BinaryArray(binaryHeaderEnum);
      binaryArray.Read(this);
      BinaryAssemblyInfo assemblyInfo;
      if (binaryArray.binaryTypeEnum == BinaryTypeEnum.ObjectUser)
      {
        if (binaryArray.assemId < 1)
          throw new SerializationException(Environment.GetResourceString("Serialization_AssemblyId", binaryArray.typeInformation));
        assemblyInfo = (BinaryAssemblyInfo) this.AssemIdToAssemblyTable[binaryArray.assemId];
      }
      else
        assemblyInfo = this.SystemAssemblyInfo;
      ObjectProgress op = this.GetOp();
      ParseRecord pr = op.pr;
      op.objectTypeEnum = InternalObjectTypeE.Array;
      op.binaryTypeEnum = binaryArray.binaryTypeEnum;
      op.typeInformation = binaryArray.typeInformation;
      ObjectProgress objectProgress = (ObjectProgress) this.stack.PeekPeek();
      if (objectProgress == null || binaryArray.objectId > 0)
      {
        op.name = "System.Array";
        pr.PRparseTypeEnum = InternalParseTypeE.Object;
        op.memberValueEnum = InternalMemberValueE.Empty;
      }
      else
      {
        pr.PRparseTypeEnum = InternalParseTypeE.Member;
        pr.PRmemberValueEnum = InternalMemberValueE.Nested;
        op.memberValueEnum = InternalMemberValueE.Nested;
        switch (objectProgress.objectTypeEnum)
        {
          case InternalObjectTypeE.Object:
            pr.PRname = objectProgress.name;
            pr.PRmemberTypeEnum = InternalMemberTypeE.Field;
            op.memberTypeEnum = InternalMemberTypeE.Field;
            pr.PRkeyDt = objectProgress.name;
            pr.PRdtType = objectProgress.dtType;
            break;
          case InternalObjectTypeE.Array:
            pr.PRmemberTypeEnum = InternalMemberTypeE.Item;
            op.memberTypeEnum = InternalMemberTypeE.Item;
            break;
          default:
            throw new SerializationException(Environment.GetResourceString("Serialization_ObjectTypeEnum", (object) objectProgress.objectTypeEnum.ToString()));
        }
      }
      pr.PRobjectId = this.objectReader.GetId((long) binaryArray.objectId);
      pr.PRobjectPositionEnum = pr.PRobjectId != this.topId ? (this.headerId <= 0L || pr.PRobjectId != this.headerId ? InternalObjectPositionE.Child : InternalObjectPositionE.Headers) : InternalObjectPositionE.Top;
      pr.PRobjectTypeEnum = InternalObjectTypeE.Array;
      BinaryConverter.TypeFromInfo(binaryArray.binaryTypeEnum, binaryArray.typeInformation, this.objectReader, assemblyInfo, out pr.PRarrayElementTypeCode, out pr.PRarrayElementTypeString, out pr.PRarrayElementType, out pr.PRisArrayVariant);
      pr.PRdtTypeCode = InternalPrimitiveTypeE.Invalid;
      pr.PRrank = binaryArray.rank;
      pr.PRlengthA = binaryArray.lengthA;
      pr.PRlowerBoundA = binaryArray.lowerBoundA;
      bool flag = false;
      switch (binaryArray.binaryArrayTypeEnum)
      {
        case BinaryArrayTypeEnum.Single:
        case BinaryArrayTypeEnum.SingleOffset:
          op.numItems = binaryArray.lengthA[0];
          pr.PRarrayTypeEnum = InternalArrayTypeE.Single;
          if (Converter.IsWriteAsByteArray(pr.PRarrayElementTypeCode) && binaryArray.lowerBoundA[0] == 0)
          {
            flag = true;
            this.ReadArrayAsBytes(pr);
            break;
          }
          break;
        case BinaryArrayTypeEnum.Jagged:
        case BinaryArrayTypeEnum.JaggedOffset:
          op.numItems = binaryArray.lengthA[0];
          pr.PRarrayTypeEnum = InternalArrayTypeE.Jagged;
          break;
        case BinaryArrayTypeEnum.Rectangular:
        case BinaryArrayTypeEnum.RectangularOffset:
          int num = 1;
          for (int index = 0; index < binaryArray.rank; ++index)
            num *= binaryArray.lengthA[index];
          op.numItems = num;
          pr.PRarrayTypeEnum = InternalArrayTypeE.Rectangular;
          break;
        default:
          throw new SerializationException(Environment.GetResourceString("Serialization_ArrayType", (object) binaryArray.binaryArrayTypeEnum.ToString()));
      }
      if (!flag)
        this.stack.Push((object) op);
      else
        this.PutOp(op);
      this.objectReader.Parse(pr);
      if (!flag)
        return;
      pr.PRparseTypeEnum = InternalParseTypeE.ObjectEnd;
      this.objectReader.Parse(pr);
    }

    [SecurityCritical]
    private void ReadArrayAsBytes(ParseRecord pr)
    {
      if (pr.PRarrayElementTypeCode == InternalPrimitiveTypeE.Byte)
        pr.PRnewObj = (object) this.ReadBytes(pr.PRlengthA[0]);
      else if (pr.PRarrayElementTypeCode == InternalPrimitiveTypeE.Char)
      {
        pr.PRnewObj = (object) this.ReadChars(pr.PRlengthA[0]);
      }
      else
      {
        int num1 = Converter.TypeLength(pr.PRarrayElementTypeCode);
        pr.PRnewObj = (object) Converter.CreatePrimitiveArray(pr.PRarrayElementTypeCode, pr.PRlengthA[0]);
        Array dst = (Array) pr.PRnewObj;
        int num2 = 0;
        if (this.byteBuffer == null)
          this.byteBuffer = new byte[4096];
        while (num2 < dst.Length)
        {
          int num3 = Math.Min(4096 / num1, dst.Length - num2);
          int num4 = num3 * num1;
          this.ReadBytes(this.byteBuffer, 0, num4);
          Buffer.InternalBlockCopy((Array) this.byteBuffer, 0, dst, num2 * num1, num4);
          num2 += num3;
        }
      }
    }

    [SecurityCritical]
    private void ReadMemberPrimitiveUnTyped()
    {
      ObjectProgress objectProgress = (ObjectProgress) this.stack.Peek();
      if (this.memberPrimitiveUnTyped == null)
        this.memberPrimitiveUnTyped = new MemberPrimitiveUnTyped();
      this.memberPrimitiveUnTyped.Set((InternalPrimitiveTypeE) this.expectedTypeInformation);
      this.memberPrimitiveUnTyped.Read(this);
      this.memberPrimitiveUnTyped.Dump();
      this.prs.Init();
      this.prs.PRvarValue = this.memberPrimitiveUnTyped.value;
      this.prs.PRdtTypeCode = (InternalPrimitiveTypeE) this.expectedTypeInformation;
      this.prs.PRdtType = Converter.ToType(this.prs.PRdtTypeCode);
      this.prs.PRparseTypeEnum = InternalParseTypeE.Member;
      this.prs.PRmemberValueEnum = InternalMemberValueE.InlineValue;
      if (objectProgress.objectTypeEnum == InternalObjectTypeE.Object)
      {
        this.prs.PRmemberTypeEnum = InternalMemberTypeE.Field;
        this.prs.PRname = objectProgress.name;
      }
      else
        this.prs.PRmemberTypeEnum = InternalMemberTypeE.Item;
      this.objectReader.Parse(this.prs);
    }

    [SecurityCritical]
    private void ReadMemberReference()
    {
      if (this.memberReference == null)
        this.memberReference = new MemberReference();
      this.memberReference.Read(this);
      this.memberReference.Dump();
      ObjectProgress objectProgress = (ObjectProgress) this.stack.Peek();
      this.prs.Init();
      this.prs.PRidRef = this.objectReader.GetId((long) this.memberReference.idRef);
      this.prs.PRparseTypeEnum = InternalParseTypeE.Member;
      this.prs.PRmemberValueEnum = InternalMemberValueE.Reference;
      if (objectProgress.objectTypeEnum == InternalObjectTypeE.Object)
      {
        this.prs.PRmemberTypeEnum = InternalMemberTypeE.Field;
        this.prs.PRname = objectProgress.name;
        this.prs.PRdtType = objectProgress.dtType;
      }
      else
        this.prs.PRmemberTypeEnum = InternalMemberTypeE.Item;
      this.objectReader.Parse(this.prs);
    }

    [SecurityCritical]
    private void ReadObjectNull(BinaryHeaderEnum binaryHeaderEnum)
    {
      if (this.objectNull == null)
        this.objectNull = new ObjectNull();
      this.objectNull.Read(this, binaryHeaderEnum);
      this.objectNull.Dump();
      ObjectProgress objectProgress = (ObjectProgress) this.stack.Peek();
      this.prs.Init();
      this.prs.PRparseTypeEnum = InternalParseTypeE.Member;
      this.prs.PRmemberValueEnum = InternalMemberValueE.Null;
      if (objectProgress.objectTypeEnum == InternalObjectTypeE.Object)
      {
        this.prs.PRmemberTypeEnum = InternalMemberTypeE.Field;
        this.prs.PRname = objectProgress.name;
        this.prs.PRdtType = objectProgress.dtType;
      }
      else
      {
        this.prs.PRmemberTypeEnum = InternalMemberTypeE.Item;
        this.prs.PRnullCount = this.objectNull.nullCount;
        objectProgress.ArrayCountIncrement(this.objectNull.nullCount - 1);
      }
      this.objectReader.Parse(this.prs);
    }

    [SecurityCritical]
    private void ReadMessageEnd()
    {
      if (__BinaryParser.messageEnd == null)
        __BinaryParser.messageEnd = new MessageEnd();
      __BinaryParser.messageEnd.Read(this);
      __BinaryParser.messageEnd.Dump();
      if (!this.stack.IsEmpty())
        throw new SerializationException(Environment.GetResourceString("Serialization_StreamEnd"));
    }

    internal object ReadValue(InternalPrimitiveTypeE code)
    {
      switch (code)
      {
        case InternalPrimitiveTypeE.Boolean:
          return (object) this.ReadBoolean();
        case InternalPrimitiveTypeE.Byte:
          return (object) this.ReadByte();
        case InternalPrimitiveTypeE.Char:
          return (object) this.ReadChar();
        case InternalPrimitiveTypeE.Decimal:
          return (object) this.ReadDecimal();
        case InternalPrimitiveTypeE.Double:
          return (object) this.ReadDouble();
        case InternalPrimitiveTypeE.Int16:
          return (object) this.ReadInt16();
        case InternalPrimitiveTypeE.Int32:
          return (object) this.ReadInt32();
        case InternalPrimitiveTypeE.Int64:
          return (object) this.ReadInt64();
        case InternalPrimitiveTypeE.SByte:
          return (object) this.ReadSByte();
        case InternalPrimitiveTypeE.Single:
          return (object) this.ReadSingle();
        case InternalPrimitiveTypeE.TimeSpan:
          return (object) this.ReadTimeSpan();
        case InternalPrimitiveTypeE.DateTime:
          return (object) this.ReadDateTime();
        case InternalPrimitiveTypeE.UInt16:
          return (object) this.ReadUInt16();
        case InternalPrimitiveTypeE.UInt32:
          return (object) this.ReadUInt32();
        case InternalPrimitiveTypeE.UInt64:
          return (object) this.ReadUInt64();
        default:
          throw new SerializationException(Environment.GetResourceString("Serialization_TypeCode", (object) code.ToString()));
      }
    }

    private ObjectProgress GetOp()
    {
      ObjectProgress objectProgress;
      if (this.opPool != null && !this.opPool.IsEmpty())
      {
        objectProgress = (ObjectProgress) this.opPool.Pop();
        objectProgress.Init();
      }
      else
        objectProgress = new ObjectProgress();
      return objectProgress;
    }

    private void PutOp(ObjectProgress op)
    {
      if (this.opPool == null)
        this.opPool = new SerStack("opPool");
      this.opPool.Push((object) op);
    }
  }
}
