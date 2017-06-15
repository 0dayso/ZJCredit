// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.SoapServices
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Metadata;
using System.Security;
using System.Text;

namespace System.Runtime.Remoting
{
  /// <summary>提供若干使用和发布 SOAP 格式的远程对象的方法。</summary>
  [SecurityCritical]
  [ComVisible(true)]
  public class SoapServices
  {
    private static Hashtable _interopXmlElementToType = Hashtable.Synchronized(new Hashtable());
    private static Hashtable _interopTypeToXmlElement = Hashtable.Synchronized(new Hashtable());
    private static Hashtable _interopXmlTypeToType = Hashtable.Synchronized(new Hashtable());
    private static Hashtable _interopTypeToXmlType = Hashtable.Synchronized(new Hashtable());
    private static Hashtable _xmlToFieldTypeMap = Hashtable.Synchronized(new Hashtable());
    private static Hashtable _methodBaseToSoapAction = Hashtable.Synchronized(new Hashtable());
    private static Hashtable _soapActionToMethodBase = Hashtable.Synchronized(new Hashtable());
    internal static string startNS = "http://schemas.microsoft.com/clr/";
    internal static string assemblyNS = "http://schemas.microsoft.com/clr/assem/";
    internal static string namespaceNS = "http://schemas.microsoft.com/clr/ns/";
    internal static string fullNS = "http://schemas.microsoft.com/clr/nsassem/";

    /// <summary>获取公共语言运行时类型的 XML 命名空间前缀。</summary>
    /// <returns>公共语言运行时类型的 XML 命名空间前缀。</returns>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public static string XmlNsForClrType
    {
      get
      {
        return SoapServices.startNS;
      }
    }

    /// <summary>获取默认的 XML 命名空间前缀，该前缀应该用于具有程序集但没有本机命名空间的公共语言运行时类的 XML 编码。</summary>
    /// <returns>默认的 XML 命名空间前缀，该前缀应该用于具有程序集但没有本机命名空间的公共语言运行时类的 XML 编码。</returns>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public static string XmlNsForClrTypeWithAssembly
    {
      get
      {
        return SoapServices.assemblyNS;
      }
    }

    /// <summary>获取 XML 命名空间前缀，该前缀应该用于作为 mscorlib.dll 文件组成部分的公共语言运行时类的 XML 编码。</summary>
    /// <returns>XML 命名空间前缀，该前缀应该用于作为 mscorlib.dll 文件组成部分的公共语言运行时类的 XML 编码。</returns>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public static string XmlNsForClrTypeWithNs
    {
      get
      {
        return SoapServices.namespaceNS;
      }
    }

    /// <summary>获取默认的 XML 命名空间前缀，该前缀应该用于既具有公共语言运行时命名空间也具有程序集的公共语言运行时类的 XML 编码。</summary>
    /// <returns>默认的 XML 命名空间前缀，该前缀应该用于既具有公共语言运行时命名空间也具有程序集的公共语言运行时类的 XML 编码。</returns>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public static string XmlNsForClrTypeWithNsAndAssembly
    {
      get
      {
        return SoapServices.fullNS;
      }
    }

    private SoapServices()
    {
    }

    private static string CreateKey(string elementName, string elementNamespace)
    {
      if (elementNamespace == null)
        return elementName;
      return elementName + " " + elementNamespace;
    }

    /// <summary>将给定 XML 元素名和命名空间与应该用于反序列化的运行时类型相关联。</summary>
    /// <param name="xmlElement">在反序列化中使用的 XML 元素名。</param>
    /// <param name="xmlNamespace">在反序列化中使用的 XML 命名空间。</param>
    /// <param name="type">在反序列化中使用的运行时 <see cref="T:System.Type" />。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public static void RegisterInteropXmlElement(string xmlElement, string xmlNamespace, Type type)
    {
      SoapServices._interopXmlElementToType[(object) SoapServices.CreateKey(xmlElement, xmlNamespace)] = (object) type;
      SoapServices._interopTypeToXmlElement[(object) type] = (object) new SoapServices.XmlEntry(xmlElement, xmlNamespace);
    }

    /// <summary>将给定 XML 类型名称和命名空间与应该用于反序列化的运行时类型关联。</summary>
    /// <param name="xmlType">在反序列化中使用的 XML 类型。</param>
    /// <param name="xmlTypeNamespace">在反序列化中使用的 XML 命名空间。</param>
    /// <param name="type">在反序列化中使用的运行时 <see cref="T:System.Type" />。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public static void RegisterInteropXmlType(string xmlType, string xmlTypeNamespace, Type type)
    {
      SoapServices._interopXmlTypeToType[(object) SoapServices.CreateKey(xmlType, xmlTypeNamespace)] = (object) type;
      SoapServices._interopTypeToXmlType[(object) type] = (object) new SoapServices.XmlEntry(xmlType, xmlTypeNamespace);
    }

    /// <summary>根据在类型的 <see cref="T:System.Runtime.Remoting.Metadata.SoapTypeAttribute" /> 中设置的值预加载给定的 <see cref="T:System.Type" />。</summary>
    /// <param name="type">要预加载的 <see cref="T:System.Type" />。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public static void PreLoad(Type type)
    {
      if (type == (Type) null)
        throw new ArgumentNullException("type");
      if (!(type is RuntimeType))
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"));
      foreach (MethodBase method in type.GetMethods())
        SoapServices.RegisterSoapActionForMethodBase(method);
      SoapTypeAttribute soapTypeAttribute = (SoapTypeAttribute) InternalRemotingServices.GetCachedSoapAttribute((object) type);
      if (soapTypeAttribute.IsInteropXmlElement())
        SoapServices.RegisterInteropXmlElement(soapTypeAttribute.XmlElementName, soapTypeAttribute.XmlNamespace, type);
      if (soapTypeAttribute.IsInteropXmlType())
        SoapServices.RegisterInteropXmlType(soapTypeAttribute.XmlTypeName, soapTypeAttribute.XmlTypeNamespace, type);
      int num = 0;
      SoapServices.XmlToFieldTypeMap xmlToFieldTypeMap = new SoapServices.XmlToFieldTypeMap();
      foreach (FieldInfo field in type.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
      {
        SoapFieldAttribute soapFieldAttribute = (SoapFieldAttribute) InternalRemotingServices.GetCachedSoapAttribute((object) field);
        if (soapFieldAttribute.IsInteropXmlElement())
        {
          string xmlElementName = soapFieldAttribute.XmlElementName;
          string xmlNamespace = soapFieldAttribute.XmlNamespace;
          if (soapFieldAttribute.UseAttribute)
            xmlToFieldTypeMap.AddXmlAttribute(field.FieldType, field.Name, xmlElementName, xmlNamespace);
          else
            xmlToFieldTypeMap.AddXmlElement(field.FieldType, field.Name, xmlElementName, xmlNamespace);
          ++num;
        }
      }
      if (num <= 0)
        return;
      SoapServices._xmlToFieldTypeMap[(object) type] = (object) xmlToFieldTypeMap;
    }

    /// <summary>根据在与每个类型关联的 <see cref="T:System.Runtime.Remoting.Metadata.SoapTypeAttribute" /> 中的信息预加载位于指定 <see cref="T:System.Reflection.Assembly" /> 中的每个 <see cref="T:System.Type" />。</summary>
    /// <param name="assembly">为其每种类型均调用 <see cref="M:System.Runtime.Remoting.SoapServices.PreLoad(System.Type)" /> 的 <see cref="T:System.Reflection.Assembly" />。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public static void PreLoad(Assembly assembly)
    {
      if (assembly == (Assembly) null)
        throw new ArgumentNullException("assembly");
      if (!(assembly is RuntimeAssembly))
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeAssembly"), "assembly");
      foreach (Type type in assembly.GetTypes())
        SoapServices.PreLoad(type);
    }

    /// <summary>检索在反序列化具有给定 XML 元素名和命名空间的无法识别的对象类型时应使用的 <see cref="T:System.Type" />。</summary>
    /// <returns>与指定的 XML 元素名和命名空间关联的对象的 <see cref="T:System.Type" />。</returns>
    /// <param name="xmlElement">未知对象类型的 XML 元素名。</param>
    /// <param name="xmlNamespace">未知对象类型的 XML 命名空间。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public static Type GetInteropTypeFromXmlElement(string xmlElement, string xmlNamespace)
    {
      return (Type) SoapServices._interopXmlElementToType[(object) SoapServices.CreateKey(xmlElement, xmlNamespace)];
    }

    /// <summary>在反序列化具有给定 XML 类型名称和命名空间的无法识别的对象类型时，检索应使用的对象 <see cref="T:System.Type" />。</summary>
    /// <returns>与指定的 XML 类型名称和命名空间关联的对象的 <see cref="T:System.Type" />。</returns>
    /// <param name="xmlType">未知对象类型的 XML 类型。</param>
    /// <param name="xmlTypeNamespace">未知对象类型的 XML 类型命名空间。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public static Type GetInteropTypeFromXmlType(string xmlType, string xmlTypeNamespace)
    {
      return (Type) SoapServices._interopXmlTypeToType[(object) SoapServices.CreateKey(xmlType, xmlTypeNamespace)];
    }

    /// <summary>检索提供的 XML 元素名、命名空间和包含类型中的某字段的 <see cref="T:System.Type" /> 和名称。</summary>
    /// <param name="containingType">包含该字段的对象的 <see cref="T:System.Type" />。</param>
    /// <param name="xmlElement">字段的 XML 元素名。</param>
    /// <param name="xmlNamespace">该字段类型的 XML 命名空间。</param>
    /// <param name="type">此方法返回时，包含该字段的 <see cref="T:System.Type" />。该参数未经初始化即被传递。</param>
    /// <param name="name">此方法返回时，包含保存该字段名的 <see cref="T:System.String" />。该参数未经初始化即被传递。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public static void GetInteropFieldTypeAndNameFromXmlElement(Type containingType, string xmlElement, string xmlNamespace, out Type type, out string name)
    {
      if (containingType == (Type) null)
      {
        type = (Type) null;
        name = (string) null;
      }
      else
      {
        SoapServices.XmlToFieldTypeMap xmlToFieldTypeMap = (SoapServices.XmlToFieldTypeMap) SoapServices._xmlToFieldTypeMap[(object) containingType];
        if (xmlToFieldTypeMap != null)
        {
          xmlToFieldTypeMap.GetFieldTypeAndNameFromXmlElement(xmlElement, xmlNamespace, out type, out name);
        }
        else
        {
          type = (Type) null;
          name = (string) null;
        }
      }
    }

    /// <summary>从 XML 特性名、命名空间和包含对象的 <see cref="T:System.Type" /> 中检索字段类型。</summary>
    /// <param name="containingType">包含该字段的对象的 <see cref="T:System.Type" />。</param>
    /// <param name="xmlAttribute">该字段类型的 XML 特性名。</param>
    /// <param name="xmlNamespace">该字段类型的 XML 命名空间。</param>
    /// <param name="type">此方法返回时，包含该字段的 <see cref="T:System.Type" />。该参数未经初始化即被传递。</param>
    /// <param name="name">此方法返回时，包含保存该字段名的 <see cref="T:System.String" />。该参数未经初始化即被传递。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public static void GetInteropFieldTypeAndNameFromXmlAttribute(Type containingType, string xmlAttribute, string xmlNamespace, out Type type, out string name)
    {
      if (containingType == (Type) null)
      {
        type = (Type) null;
        name = (string) null;
      }
      else
      {
        SoapServices.XmlToFieldTypeMap xmlToFieldTypeMap = (SoapServices.XmlToFieldTypeMap) SoapServices._xmlToFieldTypeMap[(object) containingType];
        if (xmlToFieldTypeMap != null)
        {
          xmlToFieldTypeMap.GetFieldTypeAndNameFromXmlAttribute(xmlAttribute, xmlNamespace, out type, out name);
        }
        else
        {
          type = (Type) null;
          name = (string) null;
        }
      }
    }

    /// <summary>返回在序列化给定类型时应使用的 XML 元素信息。</summary>
    /// <returns>如果请求值已用 <see cref="T:System.Runtime.Remoting.Metadata.SoapTypeAttribute" /> 设置了标志，则为 true；否则为 false。</returns>
    /// <param name="type">为其请求 XML 元素和命名空间名称的对象 <see cref="T:System.Type" />。</param>
    /// <param name="xmlElement">当此方法返回时，该参数包含保存了指定对象类型的 XML 元素名的 <see cref="T:System.String" />。该参数未经初始化即被传递。</param>
    /// <param name="xmlNamespace">当此方法返回时，该参数包含保存了指定对象类型的 XML 命名空间名称的 <see cref="T:System.String" />。该参数未经初始化即被传递。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public static bool GetXmlElementForInteropType(Type type, out string xmlElement, out string xmlNamespace)
    {
      SoapServices.XmlEntry xmlEntry = (SoapServices.XmlEntry) SoapServices._interopTypeToXmlElement[(object) type];
      if (xmlEntry != null)
      {
        xmlElement = xmlEntry.Name;
        xmlNamespace = xmlEntry.Namespace;
        return true;
      }
      SoapTypeAttribute soapTypeAttribute = (SoapTypeAttribute) InternalRemotingServices.GetCachedSoapAttribute((object) type);
      if (soapTypeAttribute.IsInteropXmlElement())
      {
        xmlElement = soapTypeAttribute.XmlElementName;
        xmlNamespace = soapTypeAttribute.XmlNamespace;
        return true;
      }
      xmlElement = (string) null;
      xmlNamespace = (string) null;
      return false;
    }

    /// <summary>返回在序列化给定 <see cref="T:System.Type" /> 时应使用的 XML 类型信息。</summary>
    /// <returns>如果请求值已用 <see cref="T:System.Runtime.Remoting.Metadata.SoapTypeAttribute" /> 设置了标志，则为 true；否则为 false。</returns>
    /// <param name="type">为其请求 XML 元素和命名空间名称的对象 <see cref="T:System.Type" />。</param>
    /// <param name="xmlType">指定对象 <see cref="T:System.Type" /> 的 XML 类型。</param>
    /// <param name="xmlTypeNamespace">指定对象 <see cref="T:System.Type" /> 的 XML 类型命名空间。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public static bool GetXmlTypeForInteropType(Type type, out string xmlType, out string xmlTypeNamespace)
    {
      SoapServices.XmlEntry xmlEntry = (SoapServices.XmlEntry) SoapServices._interopTypeToXmlType[(object) type];
      if (xmlEntry != null)
      {
        xmlType = xmlEntry.Name;
        xmlTypeNamespace = xmlEntry.Namespace;
        return true;
      }
      SoapTypeAttribute soapTypeAttribute = (SoapTypeAttribute) InternalRemotingServices.GetCachedSoapAttribute((object) type);
      if (soapTypeAttribute.IsInteropXmlType())
      {
        xmlType = soapTypeAttribute.XmlTypeName;
        xmlTypeNamespace = soapTypeAttribute.XmlTypeNamespace;
        return true;
      }
      xmlType = (string) null;
      xmlTypeNamespace = (string) null;
      return false;
    }

    /// <summary>检索在远程调用给定 <see cref="T:System.Reflection.MethodBase" /> 中指定的方法时使用的 XML 命名空间。</summary>
    /// <returns>在远程调用指定方法时使用的 XML 命名空间。</returns>
    /// <param name="mb">为其请求 XML 命名空间的方法的 <see cref="T:System.Reflection.MethodBase" />。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public static string GetXmlNamespaceForMethodCall(MethodBase mb)
    {
      return InternalRemotingServices.GetCachedSoapAttribute((object) mb).XmlNamespace;
    }

    /// <summary>检索在生成对远程调用（即调用在给定 <see cref="T:System.Reflection.MethodBase" /> 中指定的方法）的响应时使用的 XML 命名空间。</summary>
    /// <returns>在生成对远程方法调用的响应时使用的 XML 命名空间。</returns>
    /// <param name="mb">为其请求 XML 命名空间的方法的 <see cref="T:System.Reflection.MethodBase" />。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public static string GetXmlNamespaceForMethodResponse(MethodBase mb)
    {
      return ((SoapMethodAttribute) InternalRemotingServices.GetCachedSoapAttribute((object) mb)).ResponseXmlNamespace;
    }

    /// <summary>将指定的 <see cref="T:System.Reflection.MethodBase" /> 与用其缓存的 SOAPAction 关联。</summary>
    /// <param name="mb">方法的 <see cref="T:System.Reflection.MethodBase" />，与用其缓存的 SOAPAction 关联。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public static void RegisterSoapActionForMethodBase(MethodBase mb)
    {
      SoapMethodAttribute soapMethodAttribute = (SoapMethodAttribute) InternalRemotingServices.GetCachedSoapAttribute((object) mb);
      if (!soapMethodAttribute.SoapActionExplicitySet)
        return;
      SoapServices.RegisterSoapActionForMethodBase(mb, soapMethodAttribute.SoapAction);
    }

    /// <summary>将提供的 SOAPAction 值与用于信道接收的给定 <see cref="T:System.Reflection.MethodBase" /> 关联。</summary>
    /// <param name="mb">与提供的 SOAPAction 关联的 <see cref="T:System.Reflection.MethodBase" />。</param>
    /// <param name="soapAction">与给定 <see cref="T:System.Reflection.MethodBase" /> 关联的 SOAPAction 值。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public static void RegisterSoapActionForMethodBase(MethodBase mb, string soapAction)
    {
      if (soapAction == null)
        return;
      SoapServices._methodBaseToSoapAction[(object) mb] = (object) soapAction;
      ArrayList arrayList = (ArrayList) SoapServices._soapActionToMethodBase[(object) soapAction];
      if (arrayList == null)
      {
        lock (SoapServices._soapActionToMethodBase)
        {
          arrayList = ArrayList.Synchronized(new ArrayList());
          SoapServices._soapActionToMethodBase[(object) soapAction] = (object) arrayList;
        }
      }
      arrayList.Add((object) mb);
    }

    /// <summary>返回与给定 <see cref="T:System.Reflection.MethodBase" /> 中指定的方法关联的 SOAPAction 值。</summary>
    /// <returns>与给定 <see cref="T:System.Reflection.MethodBase" /> 中指定的方法关联的 SOAPAction 值。</returns>
    /// <param name="mb">包含为其请求 SOAPAction 的方法的 <see cref="T:System.Reflection.MethodBase" />。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public static string GetSoapActionFromMethodBase(MethodBase mb)
    {
      return (string) SoapServices._methodBaseToSoapAction[(object) mb] ?? ((SoapMethodAttribute) InternalRemotingServices.GetCachedSoapAttribute((object) mb)).SoapAction;
    }

    /// <summary>确定给定的 <see cref="T:System.Reflection.MethodBase" /> 是否可以接受指定的 SOAPAction。</summary>
    /// <returns>如果给定的 <see cref="T:System.Reflection.MethodBase" /> 可以接受指定的 SOAPAction，则为 true；否则为 false。</returns>
    /// <param name="soapAction">要根据给定的 <see cref="T:System.Reflection.MethodBase" /> 进行检查的 SOAPAction。</param>
    /// <param name="mb">指定的 SOAPAction 检查所依据的 <see cref="T:System.Reflection.MethodBase" />。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public static bool IsSoapActionValidForMethodBase(string soapAction, MethodBase mb)
    {
      if (mb == (MethodBase) null)
        throw new ArgumentNullException("mb");
      if ((int) soapAction[0] == 34)
      {
        string str = soapAction;
        int index = str.Length - 1;
        if ((int) str[index] == 34)
          soapAction = soapAction.Substring(1, soapAction.Length - 2);
      }
      if (string.CompareOrdinal(((SoapMethodAttribute) InternalRemotingServices.GetCachedSoapAttribute((object) mb)).SoapAction, soapAction) == 0)
        return true;
      string strA = (string) SoapServices._methodBaseToSoapAction[(object) mb];
      if (strA != null && string.CompareOrdinal(strA, soapAction) == 0)
        return true;
      string[] strArray = soapAction.Split('#');
      if (strArray.Length != 2)
        return false;
      bool assemblyIncluded;
      string soapActionNamespace = XmlNamespaceEncoder.GetTypeNameForSoapActionNamespace(strArray[0], out assemblyIncluded);
      if (soapActionNamespace == null)
        return false;
      string str1 = strArray[1];
      RuntimeMethodInfo runtimeMethodInfo = mb as RuntimeMethodInfo;
      RuntimeConstructorInfo runtimeConstructorInfo = mb as RuntimeConstructorInfo;
      RuntimeModule runtimeModule;
      if ((MethodInfo) runtimeMethodInfo != (MethodInfo) null)
      {
        runtimeModule = runtimeMethodInfo.GetRuntimeModule();
      }
      else
      {
        if (!((ConstructorInfo) runtimeConstructorInfo != (ConstructorInfo) null))
          throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeReflectionObject"));
        runtimeModule = runtimeConstructorInfo.GetRuntimeModule();
      }
      string str2 = mb.DeclaringType.FullName;
      if (assemblyIncluded)
        str2 = str2 + ", " + runtimeModule.GetRuntimeAssembly().GetSimpleName();
      if (str2.Equals(soapActionNamespace))
        return mb.Name.Equals(str1);
      return false;
    }

    /// <summary>确定与指定的 SOAPAction 值关联的方法的类型和方法名。</summary>
    /// <returns>如果类型和方法名成功恢复，则为 true；否则为 false。</returns>
    /// <param name="soapAction">为其请求类型和方法名的方法的 SOAPAction。</param>
    /// <param name="typeName">当此方法返回时，该参数包含保存了相关方法的类型名称的 <see cref="T:System.String" />。该参数未经初始化即被传递。</param>
    /// <param name="methodName">当此方法返回时，该参数包含保存了相关方法的方法名的 <see cref="T:System.String" />。该参数未经初始化即被传递。</param>
    /// <exception cref="T:System.Runtime.Remoting.RemotingException">SOAPAction 值不以引号开头和结尾。</exception>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public static bool GetTypeAndMethodNameFromSoapAction(string soapAction, out string typeName, out string methodName)
    {
      if ((int) soapAction[0] == 34)
      {
        string str = soapAction;
        int index = str.Length - 1;
        if ((int) str[index] == 34)
          soapAction = soapAction.Substring(1, soapAction.Length - 2);
      }
      ArrayList arrayList = (ArrayList) SoapServices._soapActionToMethodBase[(object) soapAction];
      if (arrayList != null)
      {
        if (arrayList.Count > 1)
        {
          typeName = (string) null;
          methodName = (string) null;
          return false;
        }
        MethodBase methodBase = (MethodBase) arrayList[0];
        if (methodBase != (MethodBase) null)
        {
          RuntimeMethodInfo runtimeMethodInfo = methodBase as RuntimeMethodInfo;
          RuntimeConstructorInfo runtimeConstructorInfo = methodBase as RuntimeConstructorInfo;
          RuntimeModule runtimeModule;
          if ((MethodInfo) runtimeMethodInfo != (MethodInfo) null)
          {
            runtimeModule = runtimeMethodInfo.GetRuntimeModule();
          }
          else
          {
            if (!((ConstructorInfo) runtimeConstructorInfo != (ConstructorInfo) null))
              throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeReflectionObject"));
            runtimeModule = runtimeConstructorInfo.GetRuntimeModule();
          }
          typeName = methodBase.DeclaringType.FullName + ", " + runtimeModule.GetRuntimeAssembly().GetSimpleName();
          methodName = methodBase.Name;
          return true;
        }
      }
      string[] strArray = soapAction.Split('#');
      if (strArray.Length == 2)
      {
        bool assemblyIncluded;
        typeName = XmlNamespaceEncoder.GetTypeNameForSoapActionNamespace(strArray[0], out assemblyIncluded);
        if (typeName == null)
        {
          methodName = (string) null;
          return false;
        }
        methodName = strArray[1];
        return true;
      }
      typeName = (string) null;
      methodName = (string) null;
      return false;
    }

    /// <summary>返回一个布尔值，该值指示指定的命名空间是否是公共语言运行时的本机命名空间。</summary>
    /// <returns>如果给定的命名空间是公共语言运行时的本机命名空间，则为 true；否则为 false。</returns>
    /// <param name="namespaceString">要签入公共语言运行时的命名空间。</param>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public static bool IsClrTypeNamespace(string namespaceString)
    {
      return namespaceString.StartsWith(SoapServices.startNS, StringComparison.Ordinal);
    }

    /// <summary>返回提供的命名空间和程序集名称中的公共语言运行时类型命名空间名称。</summary>
    /// <returns>提供的命名空间和程序集名称中的公共语言运行时类型命名空间名称。</returns>
    /// <param name="typeNamespace">要编码的命名空间。</param>
    /// <param name="assemblyName">要编码的程序集的名称。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="assemblyName" /> 和 <paramref name="typeNamespace" /> 参数同为 null 或空。</exception>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public static string CodeXmlNamespaceForClrTypeNamespace(string typeNamespace, string assemblyName)
    {
      StringBuilder sb = new StringBuilder(256);
      if (SoapServices.IsNameNull(typeNamespace))
      {
        if (SoapServices.IsNameNull(assemblyName))
          throw new ArgumentNullException("typeNamespace,assemblyName");
        sb.Append(SoapServices.assemblyNS);
        SoapServices.UriEncode(assemblyName, sb);
      }
      else if (SoapServices.IsNameNull(assemblyName))
      {
        sb.Append(SoapServices.namespaceNS);
        sb.Append(typeNamespace);
      }
      else
      {
        sb.Append(SoapServices.fullNS);
        if ((int) typeNamespace[0] == 46)
          sb.Append(typeNamespace.Substring(1));
        else
          sb.Append(typeNamespace);
        sb.Append('/');
        SoapServices.UriEncode(assemblyName, sb);
      }
      return sb.ToString();
    }

    /// <summary>对提供的公共语言运行时命名空间中的 XML 命名空间和程序集名称进行解码。</summary>
    /// <returns>如果命名空间和程序集名称被成功解码，则为 true；否则为 false。</returns>
    /// <param name="inNamespace">公共语言运行时命名空间。</param>
    /// <param name="typeNamespace">当此方法返回时，该参数包含保存了已解码命名空间名称的 <see cref="T:System.String" />。该参数未经初始化即被传递。</param>
    /// <param name="assemblyName">当此方法返回时，该参数包含保存已解码程序集名称的 <see cref="T:System.String" />。该参数未经初始化即被传递。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="inNamespace" /> 参数为 null 或空。</exception>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public static bool DecodeXmlNamespaceForClrTypeNamespace(string inNamespace, out string typeNamespace, out string assemblyName)
    {
      if (SoapServices.IsNameNull(inNamespace))
        throw new ArgumentNullException("inNamespace");
      assemblyName = (string) null;
      typeNamespace = "";
      if (inNamespace.StartsWith(SoapServices.assemblyNS, StringComparison.Ordinal))
        assemblyName = SoapServices.UriDecode(inNamespace.Substring(SoapServices.assemblyNS.Length));
      else if (inNamespace.StartsWith(SoapServices.namespaceNS, StringComparison.Ordinal))
      {
        typeNamespace = inNamespace.Substring(SoapServices.namespaceNS.Length);
      }
      else
      {
        if (!inNamespace.StartsWith(SoapServices.fullNS, StringComparison.Ordinal))
          return false;
        int num = inNamespace.IndexOf("/", SoapServices.fullNS.Length);
        typeNamespace = inNamespace.Substring(SoapServices.fullNS.Length, num - SoapServices.fullNS.Length);
        assemblyName = SoapServices.UriDecode(inNamespace.Substring(num + 1));
      }
      return true;
    }

    internal static void UriEncode(string value, StringBuilder sb)
    {
      if (value == null || value.Length == 0)
        return;
      for (int index = 0; index < value.Length; ++index)
      {
        if ((int) value[index] == 32)
          sb.Append("%20");
        else if ((int) value[index] == 61)
          sb.Append("%3D");
        else if ((int) value[index] == 44)
          sb.Append("%2C");
        else
          sb.Append(value[index]);
      }
    }

    internal static string UriDecode(string value)
    {
      if (value == null || value.Length == 0)
        return value;
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < value.Length; ++index)
      {
        if ((int) value[index] == 37 && value.Length - index >= 3)
        {
          if ((int) value[index + 1] == 50 && (int) value[index + 2] == 48)
          {
            stringBuilder.Append(' ');
            index += 2;
          }
          else if ((int) value[index + 1] == 51 && (int) value[index + 2] == 68)
          {
            stringBuilder.Append('=');
            index += 2;
          }
          else if ((int) value[index + 1] == 50 && (int) value[index + 2] == 67)
          {
            stringBuilder.Append(',');
            index += 2;
          }
          else
            stringBuilder.Append(value[index]);
        }
        else
          stringBuilder.Append(value[index]);
      }
      return stringBuilder.ToString();
    }

    private static bool IsNameNull(string name)
    {
      return name == null || name.Length == 0;
    }

    private class XmlEntry
    {
      public string Name;
      public string Namespace;

      public XmlEntry(string name, string xmlNamespace)
      {
        this.Name = name;
        this.Namespace = xmlNamespace;
      }
    }

    private class XmlToFieldTypeMap
    {
      private Hashtable _attributes = new Hashtable();
      private Hashtable _elements = new Hashtable();

      [SecurityCritical]
      public void AddXmlElement(Type fieldType, string fieldName, string xmlElement, string xmlNamespace)
      {
        this._elements[(object) SoapServices.CreateKey(xmlElement, xmlNamespace)] = (object) new SoapServices.XmlToFieldTypeMap.FieldEntry(fieldType, fieldName);
      }

      [SecurityCritical]
      public void AddXmlAttribute(Type fieldType, string fieldName, string xmlAttribute, string xmlNamespace)
      {
        this._attributes[(object) SoapServices.CreateKey(xmlAttribute, xmlNamespace)] = (object) new SoapServices.XmlToFieldTypeMap.FieldEntry(fieldType, fieldName);
      }

      [SecurityCritical]
      public void GetFieldTypeAndNameFromXmlElement(string xmlElement, string xmlNamespace, out Type type, out string name)
      {
        SoapServices.XmlToFieldTypeMap.FieldEntry fieldEntry = (SoapServices.XmlToFieldTypeMap.FieldEntry) this._elements[(object) SoapServices.CreateKey(xmlElement, xmlNamespace)];
        if (fieldEntry != null)
        {
          type = fieldEntry.Type;
          name = fieldEntry.Name;
        }
        else
        {
          type = (Type) null;
          name = (string) null;
        }
      }

      [SecurityCritical]
      public void GetFieldTypeAndNameFromXmlAttribute(string xmlAttribute, string xmlNamespace, out Type type, out string name)
      {
        SoapServices.XmlToFieldTypeMap.FieldEntry fieldEntry = (SoapServices.XmlToFieldTypeMap.FieldEntry) this._attributes[(object) SoapServices.CreateKey(xmlAttribute, xmlNamespace)];
        if (fieldEntry != null)
        {
          type = fieldEntry.Type;
          name = fieldEntry.Name;
        }
        else
        {
          type = (Type) null;
          name = (string) null;
        }
      }

      private class FieldEntry
      {
        public Type Type;
        public string Name;

        public FieldEntry(Type type, string name)
        {
          this.Type = type;
          this.Name = name;
        }
      }
    }
  }
}
