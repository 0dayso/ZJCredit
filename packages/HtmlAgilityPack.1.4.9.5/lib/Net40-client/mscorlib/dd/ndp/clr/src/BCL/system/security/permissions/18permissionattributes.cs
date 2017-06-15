// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.PermissionSetAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.IO;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Security.Util;
using System.Text;

namespace System.Security.Permissions
{
  /// <summary>允许对使用声明安全性应用到代码中的 <see cref="T:System.Security.PermissionSet" /> 进行安全操作。此类不能被继承。</summary>
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
  [ComVisible(true)]
  [Serializable]
  public sealed class PermissionSetAttribute : CodeAccessSecurityAttribute
  {
    private string m_file;
    private string m_name;
    private bool m_unicode;
    private string m_xml;
    private string m_hex;

    /// <summary>获取或设置一个文件，该文件包含要声明的自定义权限集的 XML 表示形式。</summary>
    /// <returns>包含该权限集 XML 表示形式的文件的物理路径。</returns>
    public string File
    {
      get
      {
        return this.m_file;
      }
      set
      {
        this.m_file = value;
      }
    }

    /// <summary>获取或设置一个值，该值指示 <see cref="P:System.Security.Permissions.PermissionSetAttribute.File" /> 所指定的文件是 Unicode 编码还是 ASCII 编码。</summary>
    /// <returns>如果文件是 Unicode 编码，则为 true，否则为 false。</returns>
    public bool UnicodeEncoded
    {
      get
      {
        return this.m_unicode;
      }
      set
      {
        this.m_unicode = value;
      }
    }

    /// <summary>获取或设置权限集的名称。</summary>
    /// <returns>不可更改的 <see cref="T:System.Security.NamedPermissionSet" /> 的名称（它是包含在默认策略中的多个权限集之一，而且不能更改）。</returns>
    public string Name
    {
      get
      {
        return this.m_name;
      }
      set
      {
        this.m_name = value;
      }
    }

    /// <summary>获取或设置权限集的 XML 表示形式。</summary>
    /// <returns>权限集的 XML 表示形式。</returns>
    public string XML
    {
      get
      {
        return this.m_xml;
      }
      set
      {
        this.m_xml = value;
      }
    }

    /// <summary>获取或设置 XML 编码的权限集的十六进制表示形式。</summary>
    /// <returns>XML 编码的权限集的十六进制表示形式。</returns>
    public string Hex
    {
      get
      {
        return this.m_hex;
      }
      set
      {
        this.m_hex = value;
      }
    }

    /// <summary>使用指定的安全操作初始化 <see cref="T:System.Security.Permissions.PermissionSetAttribute" /> 类的新实例。</summary>
    /// <param name="action">指定安全操作的枚举值之一。</param>
    public PermissionSetAttribute(SecurityAction action)
      : base(action)
    {
      this.m_unicode = false;
    }

    /// <summary>未使用此方法。</summary>
    /// <returns>在所有情况下均为空引用（在 Visual Basic 中为 nothing）。</returns>
    public override IPermission CreatePermission()
    {
      return (IPermission) null;
    }

    private PermissionSet BruteForceParseStream(Stream stream)
    {
      Encoding[] encodingArray = new Encoding[3]{ Encoding.UTF8, Encoding.ASCII, Encoding.Unicode };
      StreamReader input = (StreamReader) null;
      Exception exception = (Exception) null;
      int index = 0;
      while (input == null)
      {
        if (index < encodingArray.Length)
        {
          try
          {
            stream.Position = 0L;
            input = new StreamReader(stream, encodingArray[index]);
            return this.ParsePermissionSet(new Parser(input));
          }
          catch (Exception ex)
          {
            if (exception == null)
              exception = ex;
          }
          ++index;
        }
        else
          break;
      }
      throw exception;
    }

    private PermissionSet ParsePermissionSet(Parser parser)
    {
      SecurityElement topElement = parser.GetTopElement();
      PermissionSet permissionSet = new PermissionSet(PermissionState.None);
      SecurityElement et = topElement;
      permissionSet.FromXml(et);
      return permissionSet;
    }

    /// <summary>创建并返回基于此权限集特性对象的新权限集。</summary>
    /// <returns>新权限集。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public PermissionSet CreatePermissionSet()
    {
      if (this.m_unrestricted)
        return new PermissionSet(PermissionState.Unrestricted);
      if (this.m_name != null)
        return PolicyLevel.GetBuiltInSet(this.m_name);
      if (this.m_xml != null)
        return this.ParsePermissionSet(new Parser(this.m_xml.ToCharArray()));
      if (this.m_hex != null)
        return this.BruteForceParseStream((Stream) new MemoryStream(System.Security.Util.Hex.DecodeHexString(this.m_hex)));
      if (this.m_file != null)
        return this.BruteForceParseStream((Stream) new FileStream(this.m_file, FileMode.Open, FileAccess.Read));
      return new PermissionSet(PermissionState.None);
    }
  }
}
