// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.Formatters.InternalST
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Serialization.Formatters
{
  /// <summary>在编译 .NET Framework 序列化基础结构时记录跟踪消息。</summary>
  [SecurityCritical]
  [ComVisible(true)]
  public sealed class InternalST
  {
    private InternalST()
    {
    }

    /// <summary>打印 SOAP 跟踪消息。</summary>
    /// <param name="messages">要打印的跟踪消息数组。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.StrongNameIdentityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PublicKeyBlob="002400000480000094000000060200000024000052534131000400000100010007D1FA57C4AED9F0A32E84AA0FAEFD0DE9E8FD6AEC8F87FB03766C834C99921EB23BE79AD9D5DCC1DD9AD236132102900B723CF980957FC4E177108FC607774F29E8320E92EA05ECE4E821C0A5EFE8F1645C4C0C93C1AB99285D622CAA652C1DFAD63D745D6F2DE5F17E5EAF0FC4963D261C8A12436518206DC093344D5AD293" Name="System.Runtime.Serialization.Formatters.Soap" />
    /// </PermissionSet>
    [Conditional("_LOGGING")]
    public static void InfoSoap(params object[] messages)
    {
    }

    /// <summary>检查是否启用了 SOAP 跟踪。</summary>
    /// <returns>如果已启用跟踪，则为 true；否则为 false。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.StrongNameIdentityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PublicKeyBlob="002400000480000094000000060200000024000052534131000400000100010007D1FA57C4AED9F0A32E84AA0FAEFD0DE9E8FD6AEC8F87FB03766C834C99921EB23BE79AD9D5DCC1DD9AD236132102900B723CF980957FC4E177108FC607774F29E8320E92EA05ECE4E821C0A5EFE8F1645C4C0C93C1AB99285D622CAA652C1DFAD63D745D6F2DE5F17E5EAF0FC4963D261C8A12436518206DC093344D5AD293" Name="System.Runtime.Serialization.Formatters.Soap" />
    /// </PermissionSet>
    public static bool SoapCheckEnabled()
    {
      return BCLDebug.CheckEnabled("Soap");
    }

    /// <summary>处理指定的消息数组。</summary>
    /// <param name="messages">要处理的消息数组。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.StrongNameIdentityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PublicKeyBlob="002400000480000094000000060200000024000052534131000400000100010007D1FA57C4AED9F0A32E84AA0FAEFD0DE9E8FD6AEC8F87FB03766C834C99921EB23BE79AD9D5DCC1DD9AD236132102900B723CF980957FC4E177108FC607774F29E8320E92EA05ECE4E821C0A5EFE8F1645C4C0C93C1AB99285D622CAA652C1DFAD63D745D6F2DE5F17E5EAF0FC4963D261C8A12436518206DC093344D5AD293" Name="System.Runtime.Serialization.Formatters.Soap" />
    /// </PermissionSet>
    [Conditional("SER_LOGGING")]
    public static void Soap(params object[] messages)
    {
      if (!(messages[0] is string))
        messages[0] = (object) (messages[0].GetType().Name + " ");
      else
        messages[0] = (object) (messages[0].ToString() + " ");
    }

    /// <summary>断言指定消息。</summary>
    /// <param name="condition">断言时要使用的布尔值。</param>
    /// <param name="message">断言时要使用的消息。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.StrongNameIdentityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PublicKeyBlob="002400000480000094000000060200000024000052534131000400000100010007D1FA57C4AED9F0A32E84AA0FAEFD0DE9E8FD6AEC8F87FB03766C834C99921EB23BE79AD9D5DCC1DD9AD236132102900B723CF980957FC4E177108FC607774F29E8320E92EA05ECE4E821C0A5EFE8F1645C4C0C93C1AB99285D622CAA652C1DFAD63D745D6F2DE5F17E5EAF0FC4963D261C8A12436518206DC093344D5AD293" Name="System.Runtime.Serialization.Formatters.Soap" />
    /// </PermissionSet>
    [Conditional("_DEBUG")]
    public static void SoapAssert(bool condition, string message)
    {
    }

    /// <summary>设置字段值。</summary>
    /// <param name="fi">包含有关目标字段的数据的 <see cref="T:System.Reflection.FieldInfo" />。</param>
    /// <param name="target">要更改的字段。</param>
    /// <param name="value">要设置的值。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.StrongNameIdentityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PublicKeyBlob="002400000480000094000000060200000024000052534131000400000100010007D1FA57C4AED9F0A32E84AA0FAEFD0DE9E8FD6AEC8F87FB03766C834C99921EB23BE79AD9D5DCC1DD9AD236132102900B723CF980957FC4E177108FC607774F29E8320E92EA05ECE4E821C0A5EFE8F1645C4C0C93C1AB99285D622CAA652C1DFAD63D745D6F2DE5F17E5EAF0FC4963D261C8A12436518206DC093344D5AD293" Name="System.Runtime.Serialization.Formatters.Soap" />
    /// </PermissionSet>
    public static void SerializationSetValue(FieldInfo fi, object target, object value)
    {
      if (fi == (FieldInfo) null)
        throw new ArgumentNullException("fi");
      if (target == null)
        throw new ArgumentNullException("target");
      if (value == null)
        throw new ArgumentNullException("value");
      FormatterServices.SerializationSetValue((MemberInfo) fi, target, value);
    }

    /// <summary>加载要调试的指定程序集。</summary>
    /// <returns>要调试的 <see cref="T:System.Reflection.Assembly" />。</returns>
    /// <param name="assemblyString">要加载的程序集的名称。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
    ///   <IPermission class="System.Security.Permissions.StrongNameIdentityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PublicKeyBlob="002400000480000094000000060200000024000052534131000400000100010007D1FA57C4AED9F0A32E84AA0FAEFD0DE9E8FD6AEC8F87FB03766C834C99921EB23BE79AD9D5DCC1DD9AD236132102900B723CF980957FC4E177108FC607774F29E8320E92EA05ECE4E821C0A5EFE8F1645C4C0C93C1AB99285D622CAA652C1DFAD63D745D6F2DE5F17E5EAF0FC4963D261C8A12436518206DC093344D5AD293" Name="System.Runtime.Serialization.Formatters.Soap" />
    /// </PermissionSet>
    public static Assembly LoadAssemblyFromString(string assemblyString)
    {
      return FormatterServices.LoadAssemblyFromString(assemblyString);
    }
  }
}
