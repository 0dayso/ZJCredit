// Decompiled with JetBrains decompiler
// Type: System.Runtime.Versioning.VersioningHelper
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;

namespace System.Runtime.Versioning
{
  /// <summary>提供方法以帮助开发人员编写版本安全的代码。此类不能被继承。</summary>
  public static class VersioningHelper
  {
    private const ResourceScope ResTypeMask = ResourceScope.Machine | ResourceScope.Process | ResourceScope.AppDomain | ResourceScope.Library;
    private const ResourceScope VisibilityMask = ResourceScope.Private | ResourceScope.Assembly;

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern int GetRuntimeId();

    /// <summary>根据指定的资源名称和目标资源占用源返回版本安全的名称。</summary>
    /// <returns>版本安全的名称。</returns>
    /// <param name="name">资源的名称。</param>
    /// <param name="from">资源的范围。</param>
    /// <param name="to">所需的资源占用范围。</param>
    public static string MakeVersionSafeName(string name, ResourceScope from, ResourceScope to)
    {
      return VersioningHelper.MakeVersionSafeName(name, from, to, (Type) null);
    }

    /// <summary>根据指定的资源名称、目标资源占用源以及使用资源的类型返回版本安全的名称。</summary>
    /// <returns>版本安全的名称。</returns>
    /// <param name="name">资源的名称。</param>
    /// <param name="from">范围的开始。</param>
    /// <param name="to">范围的结束。</param>
    /// <param name="type">资源的 <see cref="T:System.Type" />。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="from " /> 和 <paramref name="to " /> 的值无效。<see cref="T:System.Runtime.Versioning.ResourceScope" /> 枚举中的资源类型正从更严格的资源类型变为更普通的资源类型。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="type " /> 为  null。</exception>
    [SecuritySafeCritical]
    public static string MakeVersionSafeName(string name, ResourceScope from, ResourceScope to, Type type)
    {
      ResourceScope resourceScope1 = from & (ResourceScope.Machine | ResourceScope.Process | ResourceScope.AppDomain | ResourceScope.Library);
      ResourceScope resourceScope2 = to & (ResourceScope.Machine | ResourceScope.Process | ResourceScope.AppDomain | ResourceScope.Library);
      if (resourceScope1 > resourceScope2)
        throw new ArgumentException(Environment.GetResourceString("Argument_ResourceScopeWrongDirection", (object) resourceScope1, (object) resourceScope2), "from");
      int num1 = (int) VersioningHelper.GetRequirements(to, from);
      int num2 = 24;
      if ((num1 & num2) != 0 && type == (Type) null)
        throw new ArgumentNullException("type", Environment.GetResourceString("ArgumentNull_TypeRequiredByResourceScope"));
      StringBuilder stringBuilder = new StringBuilder(name);
      char ch = '_';
      int num3 = 2;
      if ((num1 & num3) != 0)
      {
        stringBuilder.Append(ch);
        stringBuilder.Append('p');
        stringBuilder.Append(Win32Native.GetCurrentProcessId());
      }
      int num4 = 4;
      if ((num1 & num4) != 0)
      {
        string clrInstanceString = VersioningHelper.GetCLRInstanceString();
        stringBuilder.Append(ch);
        stringBuilder.Append('r');
        stringBuilder.Append(clrInstanceString);
      }
      int num5 = 1;
      if ((num1 & num5) != 0)
      {
        stringBuilder.Append(ch);
        stringBuilder.Append("ad");
        stringBuilder.Append(AppDomain.CurrentDomain.Id);
      }
      int num6 = 16;
      if ((num1 & num6) != 0)
      {
        stringBuilder.Append(ch);
        stringBuilder.Append(type.Name);
      }
      int num7 = 8;
      if ((num1 & num7) != 0)
      {
        stringBuilder.Append(ch);
        stringBuilder.Append(type.Assembly.FullName);
      }
      return stringBuilder.ToString();
    }

    private static string GetCLRInstanceString()
    {
      return VersioningHelper.GetRuntimeId().ToString((IFormatProvider) CultureInfo.InvariantCulture);
    }

    private static SxSRequirements GetRequirements(ResourceScope consumeAsScope, ResourceScope calleeScope)
    {
      SxSRequirements sxSrequirements = SxSRequirements.None;
      switch (calleeScope & (ResourceScope.Machine | ResourceScope.Process | ResourceScope.AppDomain | ResourceScope.Library))
      {
        case ResourceScope.Machine:
          switch (consumeAsScope & (ResourceScope.Machine | ResourceScope.Process | ResourceScope.AppDomain | ResourceScope.Library))
          {
            case ResourceScope.Machine:
              goto label_8;
            case ResourceScope.Process:
              sxSrequirements |= SxSRequirements.ProcessID;
              goto label_8;
            case ResourceScope.AppDomain:
              sxSrequirements |= SxSRequirements.AppDomainID | SxSRequirements.ProcessID | SxSRequirements.CLRInstanceID;
              goto label_8;
            default:
              throw new ArgumentException(Environment.GetResourceString("Argument_BadResourceScopeTypeBits", (object) consumeAsScope), "consumeAsScope");
          }
        case ResourceScope.Process:
          if ((consumeAsScope & ResourceScope.AppDomain) != ResourceScope.None)
          {
            sxSrequirements |= SxSRequirements.AppDomainID | SxSRequirements.CLRInstanceID;
            goto case ResourceScope.AppDomain;
          }
          else
            goto case ResourceScope.AppDomain;
        case ResourceScope.AppDomain:
label_8:
          switch (calleeScope & (ResourceScope.Private | ResourceScope.Assembly))
          {
            case ResourceScope.None:
              switch (consumeAsScope & (ResourceScope.Private | ResourceScope.Assembly))
              {
                case ResourceScope.None:
                  goto label_16;
                case ResourceScope.Private:
                  sxSrequirements |= SxSRequirements.AssemblyName | SxSRequirements.TypeName;
                  goto label_16;
                case ResourceScope.Assembly:
                  sxSrequirements |= SxSRequirements.AssemblyName;
                  goto label_16;
                default:
                  throw new ArgumentException(Environment.GetResourceString("Argument_BadResourceScopeVisibilityBits", (object) consumeAsScope), "consumeAsScope");
              }
            case ResourceScope.Private:
label_16:
              return sxSrequirements;
            case ResourceScope.Assembly:
              if ((consumeAsScope & ResourceScope.Private) != ResourceScope.None)
              {
                sxSrequirements |= SxSRequirements.TypeName;
                goto case ResourceScope.Private;
              }
              else
                goto case ResourceScope.Private;
            default:
              throw new ArgumentException(Environment.GetResourceString("Argument_BadResourceScopeVisibilityBits", (object) calleeScope), "calleeScope");
          }
        default:
          throw new ArgumentException(Environment.GetResourceString("Argument_BadResourceScopeTypeBits", (object) calleeScope), "calleeScope");
      }
    }
  }
}
