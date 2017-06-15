// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.WellKnownClientTypeEntry
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Runtime.Remoting
{
  /// <summary>将在客户端注册的对象类型的值保存为服务器激活类型（单个调用或 singleton）。</summary>
  [ComVisible(true)]
  public class WellKnownClientTypeEntry : TypeEntry
  {
    private string _objectUrl;
    private string _appUrl;

    /// <summary>获取服务器激活客户端对象的 URL。</summary>
    /// <returns>服务器激活客户端对象的 URL。</returns>
    public string ObjectUrl
    {
      get
      {
        return this._objectUrl;
      }
    }

    /// <summary>获取服务器激活客户端类型的 <see cref="T:System.Type" />。</summary>
    /// <returns>获取服务器激活客户端类型的 <see cref="T:System.Type" />。</returns>
    public Type ObjectType
    {
      [MethodImpl(MethodImplOptions.NoInlining)] get
      {
        StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
        return RuntimeTypeHandle.GetTypeByName(this.TypeName + ", " + this.AssemblyName, ref stackMark);
      }
    }

    /// <summary>获取或设置在其中激活该类型的应用程序的 URL。</summary>
    /// <returns>在其中激活该类型的应用程序的 URL。</returns>
    public string ApplicationUrl
    {
      get
      {
        return this._appUrl;
      }
      set
      {
        this._appUrl = value;
      }
    }

    /// <summary>使用给定的类型、程序集名称和 URL，初始化 <see cref="T:System.Runtime.Remoting.WellKnownClientTypeEntry" /> 类的新实例。</summary>
    /// <param name="typeName">服务器激活类型的类型名称。</param>
    /// <param name="assemblyName">服务器激活类型的程序集名称。</param>
    /// <param name="objectUrl">服务器激活类型的 URL。</param>
    public WellKnownClientTypeEntry(string typeName, string assemblyName, string objectUrl)
    {
      if (typeName == null)
        throw new ArgumentNullException("typeName");
      if (assemblyName == null)
        throw new ArgumentNullException("assemblyName");
      if (objectUrl == null)
        throw new ArgumentNullException("objectUrl");
      this.TypeName = typeName;
      this.AssemblyName = assemblyName;
      this._objectUrl = objectUrl;
    }

    /// <summary>使用给定的类型和 URL 初始化 <see cref="T:System.Runtime.Remoting.WellKnownClientTypeEntry" /> 类的新实例。</summary>
    /// <param name="type">服务器激活类型的 <see cref="T:System.Type" />。</param>
    /// <param name="objectUrl">服务器激活类型的 URL。</param>
    public WellKnownClientTypeEntry(Type type, string objectUrl)
    {
      if (type == (Type) null)
        throw new ArgumentNullException("type");
      if (objectUrl == null)
        throw new ArgumentNullException("objectUrl");
      RuntimeType runtimeType = type as RuntimeType;
      if (runtimeType == (RuntimeType) null)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"));
      this.TypeName = type.FullName;
      this.AssemblyName = runtimeType.GetRuntimeAssembly().GetSimpleName();
      this._objectUrl = objectUrl;
    }

    /// <summary>以 <see cref="T:System.String" /> 的形式返回服务器激活客户端类型的完整类型名称、程序集名称和对象 URL。</summary>
    /// <returns>
    /// <see cref="T:System.String" /> 形式的服务器激活客户端类型的完整类型名称、程序集名称和对象 URL。</returns>
    public override string ToString()
    {
      string str = "type='" + this.TypeName + ", " + this.AssemblyName + "'; url=" + this._objectUrl;
      if (this._appUrl != null)
        str = str + "; appUrl=" + this._appUrl;
      return str;
    }
  }
}
