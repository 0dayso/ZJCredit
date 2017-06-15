// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.Formatters.Binary.BinaryFormatter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
  /// <summary>以二进制格式将对象或整个连接对象图形序列化和反序列化。</summary>
  [ComVisible(true)]
  public sealed class BinaryFormatter : IRemotingFormatter, IFormatter
  {
    private static Dictionary<Type, TypeInformation> typeNameCache = new Dictionary<Type, TypeInformation>();
    internal FormatterTypeStyle m_typeFormat = FormatterTypeStyle.TypesAlways;
    internal TypeFilterLevel m_securityLevel = TypeFilterLevel.Full;
    internal ISurrogateSelector m_surrogates;
    internal StreamingContext m_context;
    internal SerializationBinder m_binder;
    internal FormatterAssemblyStyle m_assemblyFormat;
    internal object[] m_crossAppDomainArray;

    /// <summary>获取或设置类型说明在序列化流中的布局格式。</summary>
    /// <returns>要使用的类型布局的样式。</returns>
    public FormatterTypeStyle TypeFormat
    {
      get
      {
        return this.m_typeFormat;
      }
      set
      {
        this.m_typeFormat = value;
      }
    }

    /// <summary>获取或设置与查找加集有关的反序列化器行为。</summary>
    /// <returns>
    /// <see cref="T:System.Runtime.Serialization.Formatters.FormatterAssemblyStyle" /> 值之一，它指定反序列化器行为。</returns>
    public FormatterAssemblyStyle AssemblyFormat
    {
      get
      {
        return this.m_assemblyFormat;
      }
      set
      {
        this.m_assemblyFormat = value;
      }
    }

    /// <summary>获取或设置 <see cref="T:System.Runtime.Serialization.Formatters.Binary.BinaryFormatter" /> 所执行的自动反序列化的 <see cref="T:System.Runtime.Serialization.Formatters.TypeFilterLevel" />。</summary>
    /// <returns>表示当前自动反序列化级别的 <see cref="T:System.Runtime.Serialization.Formatters.TypeFilterLevel" />。</returns>
    public TypeFilterLevel FilterLevel
    {
      get
      {
        return this.m_securityLevel;
      }
      set
      {
        this.m_securityLevel = value;
      }
    }

    /// <summary>获取或设置控制序列化和反序列化过程的类型替换的 <see cref="T:System.Runtime.Serialization.ISurrogateSelector" />.</summary>
    /// <returns>要与此格式化程序一起使用的代理项选择器。</returns>
    public ISurrogateSelector SurrogateSelector
    {
      get
      {
        return this.m_surrogates;
      }
      set
      {
        this.m_surrogates = value;
      }
    }

    /// <summary>获取或设置控制将序列化对象绑定到类型的 <see cref="T:System.Runtime.Serialization.SerializationBinder" /> 类型的对象。</summary>
    /// <returns>要与此格式化程序一起使用的序列化联编程序。</returns>
    public SerializationBinder Binder
    {
      get
      {
        return this.m_binder;
      }
      set
      {
        this.m_binder = value;
      }
    }

    /// <summary>获取或设置此格式化程序的 <see cref="T:System.Runtime.Serialization.StreamingContext" />。</summary>
    /// <returns>要与此格式化程序一起使用的流上下文。</returns>
    public StreamingContext Context
    {
      get
      {
        return this.m_context;
      }
      set
      {
        this.m_context = value;
      }
    }

    /// <summary>使用默认值初始化 <see cref="T:System.Runtime.Serialization.Formatters.Binary.BinaryFormatter" /> 类的新实例。</summary>
    public BinaryFormatter()
    {
      this.m_surrogates = (ISurrogateSelector) null;
      this.m_context = new StreamingContext(StreamingContextStates.All);
    }

    /// <summary>使用给定的代理项选择器和流上下文来初始化 <see cref="T:System.Runtime.Serialization.Formatters.Binary.BinaryFormatter" /> 类的新实例。</summary>
    /// <param name="selector">要使用的 <see cref="T:System.Runtime.Serialization.ISurrogateSelector" />。可以为 null。</param>
    /// <param name="context">序列化数据的源和目标。</param>
    public BinaryFormatter(ISurrogateSelector selector, StreamingContext context)
    {
      this.m_surrogates = selector;
      this.m_context = context;
    }

    /// <summary>将指定的流反序列化为对象图形。</summary>
    /// <returns>对象图的顶级（根）。</returns>
    /// <param name="serializationStream">要从其中反序列化对象图形的流。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="serializationStream" /> 为 null。</exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">该 <paramref name="serializationStream" /> 支持查找，但其长度为 0。- 或 -目标类型为 <see cref="T:System.Decimal" />，但是值超出了 <see cref="T:System.Decimal" /> 类型的范围。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
    /// </PermissionSet>
    public object Deserialize(Stream serializationStream)
    {
      return this.Deserialize(serializationStream, (HeaderHandler) null);
    }

    [SecurityCritical]
    internal object Deserialize(Stream serializationStream, HeaderHandler handler, bool fCheck)
    {
      return this.Deserialize(serializationStream, handler, fCheck, (IMethodCallMessage) null);
    }

    /// <summary>将指定的流反序列化为对象图形。所提供的 <see cref="T:System.Runtime.Remoting.Messaging.HeaderHandler" /> 处理该流中的任何标题。</summary>
    /// <returns>反序列化的对象或对象图形的顶级对象（根）。</returns>
    /// <param name="serializationStream">要从其中反序列化对象图形的流。</param>
    /// <param name="handler">处理 <paramref name="serializationStream" /> 中的任何标题的 <see cref="T:System.Runtime.Remoting.Messaging.HeaderHandler" />。可以为 null。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="serializationStream" /> 为 null。</exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">该 <paramref name="serializationStream" /> 支持查找，但其长度为 0。- 或 -目标类型为 <see cref="T:System.Decimal" />，但是值超出了 <see cref="T:System.Decimal" /> 类型的范围。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public object Deserialize(Stream serializationStream, HeaderHandler handler)
    {
      return this.Deserialize(serializationStream, handler, true);
    }

    /// <summary>将对远程方法调用的响应从所提供的 <see cref="T:System.IO.Stream" /> 进行反序列化。</summary>
    /// <returns>对远程方法调用的反序列化响应。</returns>
    /// <param name="serializationStream">要从其中反序列化对象图形的流。</param>
    /// <param name="handler">处理 <paramref name="serializationStream" /> 中的任何标题的 <see cref="T:System.Runtime.Remoting.Messaging.HeaderHandler" />。可以为 null。</param>
    /// <param name="methodCallMessage">该 <see cref="T:System.Runtime.Remoting.Messaging.IMethodCallMessage" /> 包含有关调用出处的详细信息。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="serializationStream" /> 为 null。</exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">该 <paramref name="serializationStream" /> 支持查找，但其长度为 0。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public object DeserializeMethodResponse(Stream serializationStream, HeaderHandler handler, IMethodCallMessage methodCallMessage)
    {
      return this.Deserialize(serializationStream, handler, true, methodCallMessage);
    }

    /// <summary>将指定的流反序列化为对象图形。所提供的 <see cref="T:System.Runtime.Remoting.Messaging.HeaderHandler" /> 处理该流中的任何标题。</summary>
    /// <returns>反序列化的对象或对象图形的顶级对象（根）。</returns>
    /// <param name="serializationStream">要从其中反序列化对象图形的流。</param>
    /// <param name="handler">处理 <paramref name="serializationStream" /> 中的任何标题的 <see cref="T:System.Runtime.Remoting.Messaging.HeaderHandler" />。可以为 null。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="serializationStream" /> 为 null。</exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">该 <paramref name="serializationStream" /> 支持查找，但其长度为 0。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, SerializationFormatter" />
    /// </PermissionSet>
    [SecurityCritical]
    [ComVisible(false)]
    public object UnsafeDeserialize(Stream serializationStream, HeaderHandler handler)
    {
      return this.Deserialize(serializationStream, handler, false);
    }

    /// <summary>将对远程方法调用的响应从所提供的 <see cref="T:System.IO.Stream" /> 进行反序列化。</summary>
    /// <returns>对远程方法调用的反序列化响应。</returns>
    /// <param name="serializationStream">要从其中反序列化对象图形的流。</param>
    /// <param name="handler">处理 <paramref name="serializationStream" /> 中的任何标题的 <see cref="T:System.Runtime.Remoting.Messaging.HeaderHandler" />。可以为 null。</param>
    /// <param name="methodCallMessage">该 <see cref="T:System.Runtime.Remoting.Messaging.IMethodCallMessage" /> 包含有关调用出处的详细信息。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="serializationStream" /> 为 null。</exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">该 <paramref name="serializationStream" /> 支持查找，但其长度为 0。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, SerializationFormatter" />
    /// </PermissionSet>
    [SecurityCritical]
    [ComVisible(false)]
    public object UnsafeDeserializeMethodResponse(Stream serializationStream, HeaderHandler handler, IMethodCallMessage methodCallMessage)
    {
      return this.Deserialize(serializationStream, handler, false, methodCallMessage);
    }

    [SecurityCritical]
    internal object Deserialize(Stream serializationStream, HeaderHandler handler, bool fCheck, IMethodCallMessage methodCallMessage)
    {
      return this.Deserialize(serializationStream, handler, fCheck, false, methodCallMessage);
    }

    [SecurityCritical]
    internal object Deserialize(Stream serializationStream, HeaderHandler handler, bool fCheck, bool isCrossAppDomain, IMethodCallMessage methodCallMessage)
    {
      if (serializationStream == null)
        throw new ArgumentNullException("serializationStream", Environment.GetResourceString("ArgumentNull_WithParamName", (object) serializationStream));
      if (serializationStream.CanSeek && serializationStream.Length == 0L)
        throw new SerializationException(Environment.GetResourceString("Serialization_Stream"));
      ObjectReader objectReader = new ObjectReader(serializationStream, this.m_surrogates, this.m_context, new InternalFE() { FEtypeFormat = this.m_typeFormat, FEserializerTypeEnum = InternalSerializerTypeE.Binary, FEassemblyFormat = this.m_assemblyFormat, FEsecurityLevel = this.m_securityLevel }, this.m_binder);
      objectReader.crossAppDomainArray = this.m_crossAppDomainArray;
      return objectReader.Deserialize(handler, new __BinaryParser(serializationStream, objectReader), fCheck, isCrossAppDomain, methodCallMessage);
    }

    /// <summary>将对象或具有指定顶级（根）的对象图形序列化为给定流。</summary>
    /// <param name="serializationStream">图形要序列化为的流。</param>
    /// <param name="graph">位于要序列化图形的根位置的对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="serializationStream" /> 为 null。- 或 -<paramref name="graph" /> 为 null。</exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">序列化期间发生错误，如 <paramref name="graph" /> 参数中的某个对象未标记为可序列化。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    /// </PermissionSet>
    public void Serialize(Stream serializationStream, object graph)
    {
      this.Serialize(serializationStream, graph, (Header[]) null);
    }

    /// <summary>将对象或具有指定顶级（根）的对象图形序列化为附加所提供标题的给定流。</summary>
    /// <param name="serializationStream">对象要序列化为的流。</param>
    /// <param name="graph">位于要序列化图形的根位置的对象。</param>
    /// <param name="headers">将包括在序列化中的远程处理标题。可以为 null。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="serializationStream" /> 为 null。</exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">序列化期间发生错误，如 <paramref name="graph" /> 参数中的某个对象未标记为可序列化。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public void Serialize(Stream serializationStream, object graph, Header[] headers)
    {
      this.Serialize(serializationStream, graph, headers, true);
    }

    [SecurityCritical]
    internal void Serialize(Stream serializationStream, object graph, Header[] headers, bool fCheck)
    {
      if (serializationStream == null)
        throw new ArgumentNullException("serializationStream", Environment.GetResourceString("ArgumentNull_WithParamName", (object) serializationStream));
      ObjectWriter objectWriter = new ObjectWriter(this.m_surrogates, this.m_context, new InternalFE() { FEtypeFormat = this.m_typeFormat, FEserializerTypeEnum = InternalSerializerTypeE.Binary, FEassemblyFormat = this.m_assemblyFormat }, this.m_binder);
      __BinaryWriter serWriter = new __BinaryWriter(serializationStream, objectWriter, this.m_typeFormat);
      objectWriter.Serialize(graph, headers, serWriter, fCheck);
      this.m_crossAppDomainArray = objectWriter.crossAppDomainArray;
    }

    internal static TypeInformation GetTypeInformation(Type type)
    {
      lock (BinaryFormatter.typeNameCache)
      {
        TypeInformation local_2 = (TypeInformation) null;
        if (!BinaryFormatter.typeNameCache.TryGetValue(type, out local_2))
        {
          bool local_3;
          string local_4 = FormatterServices.GetClrAssemblyName(type, out local_3);
          local_2 = new TypeInformation(FormatterServices.GetClrTypeFullName(type), local_4, local_3);
          BinaryFormatter.typeNameCache.Add(type, local_2);
        }
        return local_2;
      }
    }
  }
}
