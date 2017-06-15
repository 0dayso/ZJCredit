// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.RegistrationServices
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices
{
  /// <summary>提供一组用于注册和注销托管程序集以供从 COM 使用的服务。</summary>
  [Guid("475E398F-8AFA-43a7-A3BE-F4EF8D6787C9")]
  [ClassInterface(ClassInterfaceType.None)]
  [ComVisible(true)]
  public class RegistrationServices : IRegistrationServices
  {
    private static Guid s_ManagedCategoryGuid = new Guid("{62C8FE65-4EBB-45e7-B440-6E39B2CDBF29}");
    private const string strManagedCategoryGuid = "{62C8FE65-4EBB-45e7-B440-6E39B2CDBF29}";
    private const string strDocStringPrefix = "";
    private const string strManagedTypeThreadingModel = "Both";
    private const string strComponentCategorySubKey = "Component Categories";
    private const string strManagedCategoryDescription = ".NET Category";
    private const string strImplementedCategoriesSubKey = "Implemented Categories";
    private const string strMsCorEEFileName = "mscoree.dll";
    private const string strRecordRootName = "Record";
    private const string strClsIdRootName = "CLSID";
    private const string strTlbRootName = "TypeLib";

    /// <summary>注册托管程序集中的类以便能够从 COM 创建。</summary>
    /// <returns>如果 <paramref name="assembly" /> 包含已成功注册的类型，则为 true；否则，如果程序集不包含符合条件的类型，则为 false。</returns>
    /// <param name="assembly">要注册的程序集。</param>
    /// <param name="flags">
    /// <see cref="T:System.Runtime.InteropServices.AssemblyRegistrationFlags" /> 值，该值指示在注册 <paramref name="assembly" /> 时使用的任何特殊设置。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="assembly" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// <paramref name="assembly" /> 的全名为 null。- 或 -用 <see cref="T:System.Runtime.InteropServices.ComRegisterFunctionAttribute" /> 标记的方法不是 static 方法。- 或 -在层次结构的给定级别有多个用 <see cref="T:System.Runtime.InteropServices.ComRegisterFunctionAttribute" /> 标记的方法。- 或 -用 <see cref="T:System.Runtime.InteropServices.ComRegisterFunctionAttribute" /> 标记的方法的签名无效。</exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">一个用户定义的自定义注册函数（使用 <see cref="T:System.Runtime.InteropServices.ComRegisterFunctionAttribute" /> 特性标记）引发异常。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public virtual bool RegisterAssembly(Assembly assembly, AssemblyRegistrationFlags flags)
    {
      if (assembly == (Assembly) null)
        throw new ArgumentNullException("assembly");
      if (assembly.ReflectionOnly)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_AsmLoadedForReflectionOnly"));
      RuntimeAssembly assembly1 = assembly as RuntimeAssembly;
      if ((Assembly) assembly1 == (Assembly) null)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeAssembly"));
      string fullName = assembly.FullName;
      if (fullName == null)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NoAsmName"));
      string strAsmCodeBase = (string) null;
      if ((flags & AssemblyRegistrationFlags.SetCodeBase) != AssemblyRegistrationFlags.None)
      {
        strAsmCodeBase = assembly1.GetCodeBase(false);
        if (strAsmCodeBase == null)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NoAsmCodeBase"));
      }
      Type[] registrableTypesInAssembly = this.GetRegistrableTypesInAssembly(assembly);
      int length1 = registrableTypesInAssembly.Length;
      string @string = assembly1.GetVersion().ToString();
      string imageRuntimeVersion = assembly.ImageRuntimeVersion;
      for (int index = 0; index < length1; ++index)
      {
        if (this.IsRegisteredAsValueType(registrableTypesInAssembly[index]))
          this.RegisterValueType(registrableTypesInAssembly[index], fullName, @string, strAsmCodeBase, imageRuntimeVersion);
        else if (this.TypeRepresentsComType(registrableTypesInAssembly[index]))
          this.RegisterComImportedType(registrableTypesInAssembly[index], fullName, @string, strAsmCodeBase, imageRuntimeVersion);
        else
          this.RegisterManagedType(registrableTypesInAssembly[index], fullName, @string, strAsmCodeBase, imageRuntimeVersion);
        this.CallUserDefinedRegistrationMethod(registrableTypesInAssembly[index], true);
      }
      object[] customAttributes = assembly.GetCustomAttributes(typeof (PrimaryInteropAssemblyAttribute), false);
      int length2 = customAttributes.Length;
      for (int index = 0; index < length2; ++index)
        this.RegisterPrimaryInteropAssembly(assembly1, strAsmCodeBase, (PrimaryInteropAssemblyAttribute) customAttributes[index]);
      return registrableTypesInAssembly.Length != 0 || length2 > 0;
    }

    /// <summary>注销托管程序集中的类。</summary>
    /// <returns>如果 <paramref name="assembly" /> 包含已成功注销的类型，则为 true；否则，如果程序集不包含符合条件的类型，则为 false。</returns>
    /// <param name="assembly">要注销的程序集。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="assembly" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// <paramref name="assembly" /> 的全名为 null。- 或 -用 <see cref="T:System.Runtime.InteropServices.ComUnregisterFunctionAttribute" /> 标记的方法不是 static 方法。- 或 -在层次结构的给定级别有多个用 <see cref="T:System.Runtime.InteropServices.ComUnregisterFunctionAttribute" /> 标记的方法。- 或 -用 <see cref="T:System.Runtime.InteropServices.ComUnregisterFunctionAttribute" /> 标记的方法的签名无效。</exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">一个用户定义的自定义注销函数（使用 <see cref="T:System.Runtime.InteropServices.ComUnregisterFunctionAttribute" /> 特性标记）引发异常。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public virtual bool UnregisterAssembly(Assembly assembly)
    {
      if (assembly == (Assembly) null)
        throw new ArgumentNullException("assembly");
      if (assembly.ReflectionOnly)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_AsmLoadedForReflectionOnly"));
      RuntimeAssembly runtimeAssembly = assembly as RuntimeAssembly;
      // ISSUE: variable of the null type
      __Null local = null;
      if ((Assembly) runtimeAssembly == (Assembly) local)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeAssembly"));
      bool flag = true;
      Type[] registrableTypesInAssembly = this.GetRegistrableTypesInAssembly(assembly);
      int length1 = registrableTypesInAssembly.Length;
      string @string = runtimeAssembly.GetVersion().ToString();
      for (int index = 0; index < length1; ++index)
      {
        this.CallUserDefinedRegistrationMethod(registrableTypesInAssembly[index], false);
        if (this.IsRegisteredAsValueType(registrableTypesInAssembly[index]))
        {
          if (!this.UnregisterValueType(registrableTypesInAssembly[index], @string))
            flag = false;
        }
        else if (this.TypeRepresentsComType(registrableTypesInAssembly[index]))
        {
          if (!this.UnregisterComImportedType(registrableTypesInAssembly[index], @string))
            flag = false;
        }
        else if (!this.UnregisterManagedType(registrableTypesInAssembly[index], @string))
          flag = false;
      }
      object[] customAttributes = assembly.GetCustomAttributes(typeof (PrimaryInteropAssemblyAttribute), false);
      int length2 = customAttributes.Length;
      if (flag)
      {
        for (int index = 0; index < length2; ++index)
          this.UnregisterPrimaryInteropAssembly(assembly, (PrimaryInteropAssemblyAttribute) customAttributes[index]);
      }
      return registrableTypesInAssembly.Length != 0 || length2 > 0;
    }

    /// <summary>在通过调用 <see cref="M:System.Runtime.InteropServices.RegistrationServices.RegisterAssembly(System.Reflection.Assembly,System.Runtime.InteropServices.AssemblyRegistrationFlags)" /> 而注册的程序集中检索类的列表。</summary>
    /// <returns>
    /// <see cref="T:System.Type" /> 数组，它包含 <paramref name="assembly" /> 中的类的列表。</returns>
    /// <param name="assembly">要搜索类的程序集。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="assembly" /> 参数为 null。</exception>
    [SecurityCritical]
    public virtual Type[] GetRegistrableTypesInAssembly(Assembly assembly)
    {
      if (assembly == (Assembly) null)
        throw new ArgumentNullException("assembly");
      if (!(assembly is RuntimeAssembly))
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeAssembly"), "assembly");
      Type[] exportedTypes = assembly.GetExportedTypes();
      int length = exportedTypes.Length;
      ArrayList arrayList = new ArrayList();
      for (int index = 0; index < length; ++index)
      {
        Type type = exportedTypes[index];
        if (this.TypeRequiresRegistration(type))
          arrayList.Add((object) type);
      }
      Type[] typeArray = new Type[arrayList.Count];
      arrayList.CopyTo((Array) typeArray);
      return typeArray;
    }

    /// <summary>检索指定类型的 COM ProgID。</summary>
    /// <returns>指定类型的 ProgID。</returns>
    /// <param name="type">与正被请求的 ProgID 相对应的类型。</param>
    [SecurityCritical]
    public virtual string GetProgIdForType(Type type)
    {
      return Marshal.GenerateProgIdForType(type);
    }

    /// <summary>使用指定的 GUID 向 COM 注册指定的类型。</summary>
    /// <param name="type">要注册以供从 COM 使用的 <see cref="T:System.Type" />。</param>
    /// <param name="g">用于注册指定类型的 <see cref="T:System.Guid" />。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="type" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentNullException">无法创建 <paramref name="type" /> 参数。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public virtual void RegisterTypeForComClients(Type type, ref Guid g)
    {
      if (type == (Type) null)
        throw new ArgumentNullException("type");
      if (type as RuntimeType == (RuntimeType) null)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"), "type");
      if (!this.TypeRequiresRegistration(type))
        throw new ArgumentException(Environment.GetResourceString("Argument_TypeMustBeComCreatable"), "type");
      RegistrationServices.RegisterTypeForComClientsNative(type, ref g);
    }

    /// <summary>返回包含托管类的 COM 类别的 GUID。</summary>
    /// <returns>包含托管类的 COM 类别的 GUID。</returns>
    public virtual Guid GetManagedCategoryGuid()
    {
      return RegistrationServices.s_ManagedCategoryGuid;
    }

    /// <summary>确定指定的类型是否需要注册。</summary>
    /// <returns>如果该类型必须注册以供从 COM 使用，则为 true；否则为 false。</returns>
    /// <param name="type">要检查其 COM 注册要求的类型。</param>
    [SecurityCritical]
    public virtual bool TypeRequiresRegistration(Type type)
    {
      return RegistrationServices.TypeRequiresRegistrationHelper(type);
    }

    /// <summary>指示类型是否用 <see cref="T:System.Runtime.InteropServices.ComImportAttribute" /> 进行了标记，或者派生带有 <see cref="T:System.Runtime.InteropServices.ComImportAttribute" /> 标记的类型并且与父级具有相同的 GUID。</summary>
    /// <returns>如果类型带有 <see cref="T:System.Runtime.InteropServices.ComImportAttribute" /> 标记，或者派生自带有 <see cref="T:System.Runtime.InteropServices.ComImportAttribute" /> 标记的类型并与父级具有相同的 GUID，则为 true；否则，为 false。</returns>
    /// <param name="type">要检查其是否为 COM 类型的类型。</param>
    [SecuritySafeCritical]
    public virtual bool TypeRepresentsComType(Type type)
    {
      if (!type.IsCOMObject)
        return false;
      if (type.IsImport)
        return true;
      Type baseComImportType = this.GetBaseComImportType(type);
      return Marshal.GenerateGuidForType(type) == Marshal.GenerateGuidForType(baseComImportType);
    }

    /// <summary>使用指定的执行上下文和连接类型向 COM 注册指定的类型。</summary>
    /// <returns>表示一个 Cookie 值的整数。</returns>
    /// <param name="type">要进行注册以供从 COM 使用的 <see cref="T:System.Type" /> 对象。</param>
    /// <param name="classContext">
    /// <see cref="T:System.Runtime.InteropServices.RegistrationClassContext" /> 值之一，指示将在其中运行可执行代码的上下文。</param>
    /// <param name="flags">
    /// <see cref="T:System.Runtime.InteropServices.RegistrationConnectionType" /> 值之一，指定如何建立到类对象的连接。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="type" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentNullException">无法创建 <paramref name="type" /> 参数。</exception>
    [SecurityCritical]
    [ComVisible(false)]
    public virtual int RegisterTypeForComClients(Type type, RegistrationClassContext classContext, RegistrationConnectionType flags)
    {
      if (type == (Type) null)
        throw new ArgumentNullException("type");
      if (type as RuntimeType == (RuntimeType) null)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"), "type");
      if (!this.TypeRequiresRegistration(type))
        throw new ArgumentException(Environment.GetResourceString("Argument_TypeMustBeComCreatable"), "type");
      return RegistrationServices.RegisterTypeForComClientsExNative(type, classContext, flags);
    }

    /// <summary>移除对使用 <see cref="M:System.Runtime.InteropServices.RegistrationServices.RegisterTypeForComClients(System.Type,System.Runtime.InteropServices.RegistrationClassContext,System.Runtime.InteropServices.RegistrationConnectionType)" /> 方法注册的类型的引用。</summary>
    /// <param name="cookie">对 <see cref="M:System.Runtime.InteropServices.RegistrationServices.RegisterTypeForComClients(System.Type,System.Runtime.InteropServices.RegistrationClassContext,System.Runtime.InteropServices.RegistrationConnectionType)" /> 方法重载的先前调用所返回的 Cookie 值。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    [ComVisible(false)]
    public virtual void UnregisterTypeForComClients(int cookie)
    {
      RegistrationServices.CoRevokeClassObject(cookie);
    }

    [SecurityCritical]
    internal static bool TypeRequiresRegistrationHelper(Type type)
    {
      if (!type.IsClass && !type.IsValueType || type.IsAbstract || !type.IsValueType && type.GetConstructor(BindingFlags.Instance | BindingFlags.Public, (Binder) null, new Type[0], (ParameterModifier[]) null) == (ConstructorInfo) null)
        return false;
      return Marshal.IsTypeVisibleFromCom(type);
    }

    [SecurityCritical]
    private void RegisterValueType(Type type, string strAsmName, string strAsmVersion, string strAsmCodeBase, string strRuntimeVersion)
    {
      string subkey = "{" + Marshal.GenerateGuidForType(type).ToString().ToUpper(CultureInfo.InvariantCulture) + "}";
      using (RegistryKey subKey1 = Registry.ClassesRoot.CreateSubKey("Record"))
      {
        using (RegistryKey subKey2 = subKey1.CreateSubKey(subkey))
        {
          using (RegistryKey subKey3 = subKey2.CreateSubKey(strAsmVersion))
          {
            subKey3.SetValue("Class", (object) type.FullName);
            subKey3.SetValue("Assembly", (object) strAsmName);
            subKey3.SetValue("RuntimeVersion", (object) strRuntimeVersion);
            if (strAsmCodeBase == null)
              return;
            subKey3.SetValue("CodeBase", (object) strAsmCodeBase);
          }
        }
      }
    }

    [SecurityCritical]
    private void RegisterManagedType(Type type, string strAsmName, string strAsmVersion, string strAsmCodeBase, string strRuntimeVersion)
    {
      string str = type.FullName ?? "";
      string subkey = "{" + Marshal.GenerateGuidForType(type).ToString().ToUpper(CultureInfo.InvariantCulture) + "}";
      string progIdForType = this.GetProgIdForType(type);
      if (progIdForType != string.Empty)
      {
        using (RegistryKey subKey1 = Registry.ClassesRoot.CreateSubKey(progIdForType))
        {
          subKey1.SetValue("", (object) str);
          using (RegistryKey subKey2 = subKey1.CreateSubKey("CLSID"))
            subKey2.SetValue("", (object) subkey);
        }
      }
      using (RegistryKey subKey1 = Registry.ClassesRoot.CreateSubKey("CLSID"))
      {
        using (RegistryKey subKey2 = subKey1.CreateSubKey(subkey))
        {
          subKey2.SetValue("", (object) str);
          using (RegistryKey subKey3 = subKey2.CreateSubKey("InprocServer32"))
          {
            subKey3.SetValue("", (object) "mscoree.dll");
            subKey3.SetValue("ThreadingModel", (object) "Both");
            subKey3.SetValue("Class", (object) type.FullName);
            subKey3.SetValue("Assembly", (object) strAsmName);
            subKey3.SetValue("RuntimeVersion", (object) strRuntimeVersion);
            if (strAsmCodeBase != null)
              subKey3.SetValue("CodeBase", (object) strAsmCodeBase);
            using (RegistryKey subKey4 = subKey3.CreateSubKey(strAsmVersion))
            {
              subKey4.SetValue("Class", (object) type.FullName);
              subKey4.SetValue("Assembly", (object) strAsmName);
              subKey4.SetValue("RuntimeVersion", (object) strRuntimeVersion);
              if (strAsmCodeBase != null)
                subKey4.SetValue("CodeBase", (object) strAsmCodeBase);
            }
            if (progIdForType != string.Empty)
            {
              using (RegistryKey subKey4 = subKey2.CreateSubKey("ProgId"))
                subKey4.SetValue("", (object) progIdForType);
            }
          }
          using (RegistryKey subKey3 = subKey2.CreateSubKey("Implemented Categories"))
          {
            using (subKey3.CreateSubKey("{62C8FE65-4EBB-45e7-B440-6E39B2CDBF29}"))
              ;
          }
        }
      }
      this.EnsureManagedCategoryExists();
    }

    [SecurityCritical]
    private void RegisterComImportedType(Type type, string strAsmName, string strAsmVersion, string strAsmCodeBase, string strRuntimeVersion)
    {
      string subkey = "{" + Marshal.GenerateGuidForType(type).ToString().ToUpper(CultureInfo.InvariantCulture) + "}";
      using (RegistryKey subKey1 = Registry.ClassesRoot.CreateSubKey("CLSID"))
      {
        using (RegistryKey subKey2 = subKey1.CreateSubKey(subkey))
        {
          using (RegistryKey subKey3 = subKey2.CreateSubKey("InprocServer32"))
          {
            subKey3.SetValue("Class", (object) type.FullName);
            subKey3.SetValue("Assembly", (object) strAsmName);
            subKey3.SetValue("RuntimeVersion", (object) strRuntimeVersion);
            if (strAsmCodeBase != null)
              subKey3.SetValue("CodeBase", (object) strAsmCodeBase);
            using (RegistryKey subKey4 = subKey3.CreateSubKey(strAsmVersion))
            {
              subKey4.SetValue("Class", (object) type.FullName);
              subKey4.SetValue("Assembly", (object) strAsmName);
              subKey4.SetValue("RuntimeVersion", (object) strRuntimeVersion);
              if (strAsmCodeBase == null)
                return;
              subKey4.SetValue("CodeBase", (object) strAsmCodeBase);
            }
          }
        }
      }
    }

    [SecurityCritical]
    private bool UnregisterValueType(Type type, string strAsmVersion)
    {
      bool flag = true;
      string str = "{" + Marshal.GenerateGuidForType(type).ToString().ToUpper(CultureInfo.InvariantCulture) + "}";
      using (RegistryKey registryKey1 = Registry.ClassesRoot.OpenSubKey("Record", true))
      {
        if (registryKey1 != null)
        {
          using (RegistryKey registryKey2 = registryKey1.OpenSubKey(str, true))
          {
            if (registryKey2 != null)
            {
              using (RegistryKey registryKey3 = registryKey2.OpenSubKey(strAsmVersion, true))
              {
                if (registryKey3 != null)
                {
                  registryKey3.DeleteValue("Assembly", false);
                  registryKey3.DeleteValue("Class", false);
                  registryKey3.DeleteValue("CodeBase", false);
                  registryKey3.DeleteValue("RuntimeVersion", false);
                  if (registryKey3.SubKeyCount == 0)
                  {
                    if (registryKey3.ValueCount == 0)
                      registryKey2.DeleteSubKey(strAsmVersion);
                  }
                }
              }
              if (registryKey2.SubKeyCount != 0)
                flag = false;
              if (registryKey2.SubKeyCount == 0)
              {
                if (registryKey2.ValueCount == 0)
                  registryKey1.DeleteSubKey(str);
              }
            }
          }
          if (registryKey1.SubKeyCount == 0)
          {
            if (registryKey1.ValueCount == 0)
              Registry.ClassesRoot.DeleteSubKey("Record");
          }
        }
      }
      return flag;
    }

    [SecurityCritical]
    private bool UnregisterManagedType(Type type, string strAsmVersion)
    {
      bool flag = true;
      string str = "{" + Marshal.GenerateGuidForType(type).ToString().ToUpper(CultureInfo.InvariantCulture) + "}";
      string progIdForType = this.GetProgIdForType(type);
      using (RegistryKey registryKey1 = Registry.ClassesRoot.OpenSubKey("CLSID", true))
      {
        if (registryKey1 != null)
        {
          using (RegistryKey registryKey2 = registryKey1.OpenSubKey(str, true))
          {
            if (registryKey2 != null)
            {
              using (RegistryKey registryKey3 = registryKey2.OpenSubKey("InprocServer32", true))
              {
                if (registryKey3 != null)
                {
                  using (RegistryKey registryKey4 = registryKey3.OpenSubKey(strAsmVersion, true))
                  {
                    if (registryKey4 != null)
                    {
                      registryKey4.DeleteValue("Assembly", false);
                      registryKey4.DeleteValue("Class", false);
                      registryKey4.DeleteValue("RuntimeVersion", false);
                      registryKey4.DeleteValue("CodeBase", false);
                      if (registryKey4.SubKeyCount == 0)
                      {
                        if (registryKey4.ValueCount == 0)
                          registryKey3.DeleteSubKey(strAsmVersion);
                      }
                    }
                  }
                  if (registryKey3.SubKeyCount != 0)
                    flag = false;
                  if (flag)
                  {
                    registryKey3.DeleteValue("", false);
                    registryKey3.DeleteValue("ThreadingModel", false);
                  }
                  registryKey3.DeleteValue("Assembly", false);
                  registryKey3.DeleteValue("Class", false);
                  registryKey3.DeleteValue("RuntimeVersion", false);
                  registryKey3.DeleteValue("CodeBase", false);
                  if (registryKey3.SubKeyCount == 0)
                  {
                    if (registryKey3.ValueCount == 0)
                      registryKey2.DeleteSubKey("InprocServer32");
                  }
                }
              }
              if (flag)
              {
                registryKey2.DeleteValue("", false);
                if (progIdForType != string.Empty)
                {
                  using (RegistryKey registryKey3 = registryKey2.OpenSubKey("ProgId", true))
                  {
                    if (registryKey3 != null)
                    {
                      registryKey3.DeleteValue("", false);
                      if (registryKey3.SubKeyCount == 0)
                      {
                        if (registryKey3.ValueCount == 0)
                          registryKey2.DeleteSubKey("ProgId");
                      }
                    }
                  }
                }
                using (RegistryKey registryKey3 = registryKey2.OpenSubKey("Implemented Categories", true))
                {
                  if (registryKey3 != null)
                  {
                    using (RegistryKey registryKey4 = registryKey3.OpenSubKey("{62C8FE65-4EBB-45e7-B440-6E39B2CDBF29}", true))
                    {
                      if (registryKey4 != null)
                      {
                        if (registryKey4.SubKeyCount == 0)
                        {
                          if (registryKey4.ValueCount == 0)
                            registryKey3.DeleteSubKey("{62C8FE65-4EBB-45e7-B440-6E39B2CDBF29}");
                        }
                      }
                    }
                    if (registryKey3.SubKeyCount == 0)
                    {
                      if (registryKey3.ValueCount == 0)
                        registryKey2.DeleteSubKey("Implemented Categories");
                    }
                  }
                }
              }
              if (registryKey2.SubKeyCount == 0)
              {
                if (registryKey2.ValueCount == 0)
                  registryKey1.DeleteSubKey(str);
              }
            }
          }
          if (registryKey1.SubKeyCount == 0 && registryKey1.ValueCount == 0)
            Registry.ClassesRoot.DeleteSubKey("CLSID");
        }
        if (flag)
        {
          if (progIdForType != string.Empty)
          {
            using (RegistryKey registryKey2 = Registry.ClassesRoot.OpenSubKey(progIdForType, true))
            {
              if (registryKey2 != null)
              {
                registryKey2.DeleteValue("", false);
                using (RegistryKey registryKey3 = registryKey2.OpenSubKey("CLSID", true))
                {
                  if (registryKey3 != null)
                  {
                    registryKey3.DeleteValue("", false);
                    if (registryKey3.SubKeyCount == 0)
                    {
                      if (registryKey3.ValueCount == 0)
                        registryKey2.DeleteSubKey("CLSID");
                    }
                  }
                }
                if (registryKey2.SubKeyCount == 0)
                {
                  if (registryKey2.ValueCount == 0)
                    Registry.ClassesRoot.DeleteSubKey(progIdForType);
                }
              }
            }
          }
        }
      }
      return flag;
    }

    [SecurityCritical]
    private bool UnregisterComImportedType(Type type, string strAsmVersion)
    {
      bool flag = true;
      string str = "{" + Marshal.GenerateGuidForType(type).ToString().ToUpper(CultureInfo.InvariantCulture) + "}";
      using (RegistryKey registryKey1 = Registry.ClassesRoot.OpenSubKey("CLSID", true))
      {
        if (registryKey1 != null)
        {
          using (RegistryKey registryKey2 = registryKey1.OpenSubKey(str, true))
          {
            if (registryKey2 != null)
            {
              using (RegistryKey registryKey3 = registryKey2.OpenSubKey("InprocServer32", true))
              {
                if (registryKey3 != null)
                {
                  registryKey3.DeleteValue("Assembly", false);
                  registryKey3.DeleteValue("Class", false);
                  registryKey3.DeleteValue("RuntimeVersion", false);
                  registryKey3.DeleteValue("CodeBase", false);
                  using (RegistryKey registryKey4 = registryKey3.OpenSubKey(strAsmVersion, true))
                  {
                    if (registryKey4 != null)
                    {
                      registryKey4.DeleteValue("Assembly", false);
                      registryKey4.DeleteValue("Class", false);
                      registryKey4.DeleteValue("RuntimeVersion", false);
                      registryKey4.DeleteValue("CodeBase", false);
                      if (registryKey4.SubKeyCount == 0)
                      {
                        if (registryKey4.ValueCount == 0)
                          registryKey3.DeleteSubKey(strAsmVersion);
                      }
                    }
                  }
                  if (registryKey3.SubKeyCount != 0)
                    flag = false;
                  if (registryKey3.SubKeyCount == 0)
                  {
                    if (registryKey3.ValueCount == 0)
                      registryKey2.DeleteSubKey("InprocServer32");
                  }
                }
              }
              if (registryKey2.SubKeyCount == 0)
              {
                if (registryKey2.ValueCount == 0)
                  registryKey1.DeleteSubKey(str);
              }
            }
          }
          if (registryKey1.SubKeyCount == 0)
          {
            if (registryKey1.ValueCount == 0)
              Registry.ClassesRoot.DeleteSubKey("CLSID");
          }
        }
      }
      return flag;
    }

    [SecurityCritical]
    private void RegisterPrimaryInteropAssembly(RuntimeAssembly assembly, string strAsmCodeBase, PrimaryInteropAssemblyAttribute attr)
    {
      if (assembly.GetPublicKey().Length == 0)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_PIAMustBeStrongNamed"));
      string subkey1 = "{" + Marshal.GetTypeLibGuidForAssembly((Assembly) assembly).ToString().ToUpper(CultureInfo.InvariantCulture) + "}";
      string subkey2 = attr.MajorVersion.ToString("x", (IFormatProvider) CultureInfo.InvariantCulture) + "." + attr.MinorVersion.ToString("x", (IFormatProvider) CultureInfo.InvariantCulture);
      using (RegistryKey subKey1 = Registry.ClassesRoot.CreateSubKey("TypeLib"))
      {
        using (RegistryKey subKey2 = subKey1.CreateSubKey(subkey1))
        {
          using (RegistryKey subKey3 = subKey2.CreateSubKey(subkey2))
          {
            subKey3.SetValue("PrimaryInteropAssemblyName", (object) assembly.FullName);
            if (strAsmCodeBase == null)
              return;
            subKey3.SetValue("PrimaryInteropAssemblyCodeBase", (object) strAsmCodeBase);
          }
        }
      }
    }

    [SecurityCritical]
    private void UnregisterPrimaryInteropAssembly(Assembly assembly, PrimaryInteropAssemblyAttribute attr)
    {
      string str1 = "{" + Marshal.GetTypeLibGuidForAssembly(assembly).ToString().ToUpper(CultureInfo.InvariantCulture) + "}";
      int num = attr.MajorVersion;
      string string1 = num.ToString("x", (IFormatProvider) CultureInfo.InvariantCulture);
      string str2 = ".";
      num = attr.MinorVersion;
      string string2 = num.ToString("x", (IFormatProvider) CultureInfo.InvariantCulture);
      string str3 = string1 + str2 + string2;
      using (RegistryKey registryKey1 = Registry.ClassesRoot.OpenSubKey("TypeLib", true))
      {
        if (registryKey1 == null)
          return;
        using (RegistryKey registryKey2 = registryKey1.OpenSubKey(str1, true))
        {
          if (registryKey2 != null)
          {
            using (RegistryKey registryKey3 = registryKey2.OpenSubKey(str3, true))
            {
              if (registryKey3 != null)
              {
                registryKey3.DeleteValue("PrimaryInteropAssemblyName", false);
                registryKey3.DeleteValue("PrimaryInteropAssemblyCodeBase", false);
                if (registryKey3.SubKeyCount == 0)
                {
                  if (registryKey3.ValueCount == 0)
                    registryKey2.DeleteSubKey(str3);
                }
              }
            }
            if (registryKey2.SubKeyCount == 0)
            {
              if (registryKey2.ValueCount == 0)
                registryKey1.DeleteSubKey(str1);
            }
          }
        }
        if (registryKey1.SubKeyCount != 0 || registryKey1.ValueCount != 0)
          return;
        Registry.ClassesRoot.DeleteSubKey("TypeLib");
      }
    }

    private void EnsureManagedCategoryExists()
    {
      if (RegistrationServices.ManagedCategoryExists())
        return;
      using (RegistryKey subKey1 = Registry.ClassesRoot.CreateSubKey("Component Categories"))
      {
        using (RegistryKey subKey2 = subKey1.CreateSubKey("{62C8FE65-4EBB-45e7-B440-6E39B2CDBF29}"))
          subKey2.SetValue("0", (object) ".NET Category");
      }
    }

    private static bool ManagedCategoryExists()
    {
      using (RegistryKey registryKey1 = Registry.ClassesRoot.OpenSubKey("Component Categories", RegistryKeyPermissionCheck.ReadSubTree))
      {
        if (registryKey1 == null)
          return false;
        using (RegistryKey registryKey2 = registryKey1.OpenSubKey("{62C8FE65-4EBB-45e7-B440-6E39B2CDBF29}", RegistryKeyPermissionCheck.ReadSubTree))
        {
          if (registryKey2 == null)
            return false;
          object obj = registryKey2.GetValue("0");
          if (obj == null || obj.GetType() != typeof (string))
            return false;
          if ((string) obj != ".NET Category")
            return false;
        }
      }
      return true;
    }

    [SecurityCritical]
    private void CallUserDefinedRegistrationMethod(Type type, bool bRegister)
    {
      bool flag = false;
      Type attributeType = !bRegister ? typeof (ComUnregisterFunctionAttribute) : typeof (ComRegisterFunctionAttribute);
      for (Type type1 = type; !flag && type1 != (Type) null; type1 = type1.BaseType)
      {
        MethodInfo[] methods = type1.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        int length = methods.Length;
        for (int index = 0; index < length; ++index)
        {
          MethodInfo methodInfo = methods[index];
          if (methodInfo.GetCustomAttributes(attributeType, true).Length != 0)
          {
            if (!methodInfo.IsStatic)
            {
              if (bRegister)
                throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NonStaticComRegFunction", (object) methodInfo.Name, (object) type1.Name));
              throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NonStaticComUnRegFunction", (object) methodInfo.Name, (object) type1.Name));
            }
            ParameterInfo[] parameters1 = methodInfo.GetParameters();
            if (methodInfo.ReturnType != typeof (void) || parameters1 == null || parameters1.Length != 1 || parameters1[0].ParameterType != typeof (string) && parameters1[0].ParameterType != typeof (Type))
            {
              if (bRegister)
                throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_InvalidComRegFunctionSig", (object) methodInfo.Name, (object) type1.Name));
              throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_InvalidComUnRegFunctionSig", (object) methodInfo.Name, (object) type1.Name));
            }
            if (flag)
            {
              if (bRegister)
                throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_MultipleComRegFunctions", (object) type1.Name));
              throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_MultipleComUnRegFunctions", (object) type1.Name));
            }
            object[] parameters2 = new object[1]{ !(parameters1[0].ParameterType == typeof (string)) ? (object) type : (object) ("HKEY_CLASSES_ROOT\\CLSID\\{" + Marshal.GenerateGuidForType(type).ToString().ToUpper(CultureInfo.InvariantCulture) + "}") };
            methodInfo.Invoke((object) null, parameters2);
            flag = true;
          }
        }
      }
    }

    private Type GetBaseComImportType(Type type)
    {
      while (type != (Type) null && !type.IsImport)
        type = type.BaseType;
      return type;
    }

    private bool IsRegisteredAsValueType(Type type)
    {
      return type.IsValueType;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void RegisterTypeForComClientsNative(Type type, ref Guid g);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern int RegisterTypeForComClientsExNative(Type t, RegistrationClassContext clsContext, RegistrationConnectionType flags);

    [DllImport("ole32.dll", CharSet = CharSet.Auto, PreserveSig = false)]
    private static extern void CoRevokeClassObject(int cookie);
  }
}
