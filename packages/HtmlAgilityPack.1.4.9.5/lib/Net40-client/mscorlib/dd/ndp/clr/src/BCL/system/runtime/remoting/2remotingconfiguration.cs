// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.ActivatedClientTypeEntry
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Threading;

namespace System.Runtime.Remoting
{
  /// <summary>将在客户端注册的对象类型的值保存为可以在服务器上激活的类型。</summary>
  [ComVisible(true)]
  public class ActivatedClientTypeEntry : TypeEntry
  {
    private string _appUrl;
    private IContextAttribute[] _contextAttributes;

    /// <summary>获取在其中激活该类型的应用程序的 URL。</summary>
    /// <returns>在其中激活该类型的应用程序的 URL。</returns>
    public string ApplicationUrl
    {
      get
      {
        return this._appUrl;
      }
    }

    /// <summary>获取客户端激活类型的 <see cref="T:System.Type" />。</summary>
    /// <returns>获取客户端激活类型的 <see cref="T:System.Type" />。</returns>
    public Type ObjectType
    {
      [MethodImpl(MethodImplOptions.NoInlining)] get
      {
        StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
        return RuntimeTypeHandle.GetTypeByName(this.TypeName + ", " + this.AssemblyName, ref stackMark);
      }
    }

    /// <summary>获取或设置客户端激活类型的上下文特性。</summary>
    /// <returns>客户端激活类型的上下文特性。</returns>
    public IContextAttribute[] ContextAttributes
    {
      get
      {
        return this._contextAttributes;
      }
      set
      {
        this._contextAttributes = value;
      }
    }

    /// <summary>使用给定的类型名称、程序集名称和应用程序 URL 初始化 <see cref="T:System.Runtime.Remoting.ActivatedClientTypeEntry" /> 类的新实例。</summary>
    /// <param name="typeName">客户端激活类型的类型名称。</param>
    /// <param name="assemblyName">客户端激活类型的程序集名称。</param>
    /// <param name="appUrl">在其中激活该类型的应用程序的 URL。</param>
    public ActivatedClientTypeEntry(string typeName, string assemblyName, string appUrl)
    {
      if (typeName == null)
        throw new ArgumentNullException("typeName");
      if (assemblyName == null)
        throw new ArgumentNullException("assemblyName");
      if (appUrl == null)
        throw new ArgumentNullException("appUrl");
      this.TypeName = typeName;
      this.AssemblyName = assemblyName;
      this._appUrl = appUrl;
    }

    /// <summary>使用给定的 <see cref="T:System.Type" /> 和应用程序 URL 初始化 <see cref="T:System.Runtime.Remoting.ActivatedClientTypeEntry" /> 类的新实例。</summary>
    /// <param name="type">客户端激活类型的 <see cref="T:System.Type" />。</param>
    /// <param name="appUrl">在其中激活该类型的应用程序的 URL。</param>
    public ActivatedClientTypeEntry(Type type, string appUrl)
    {
      if (type == (Type) null)
        throw new ArgumentNullException("type");
      if (appUrl == null)
        throw new ArgumentNullException("appUrl");
      RuntimeType runtimeType = type as RuntimeType;
      if (runtimeType == (RuntimeType) null)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeAssembly"));
      this.TypeName = type.FullName;
      this.AssemblyName = runtimeType.GetRuntimeAssembly().GetSimpleName();
      this._appUrl = appUrl;
    }

    /// <summary>以 <see cref="T:System.String" /> 的形式返回客户端激活类型的类型名称、程序集名称和应用程序 URL。</summary>
    /// <returns>由 <see cref="T:System.String" /> 对象表示的客户端激活类型的类型名称、程序集名称和应用程序 URL。</returns>
    public override string ToString()
    {
      return "type='" + this.TypeName + ", " + this.AssemblyName + "'; appUrl=" + this._appUrl;
    }
  }
}
