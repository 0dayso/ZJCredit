// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.WellKnownServiceTypeEntry
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Threading;

namespace System.Runtime.Remoting
{
  /// <summary>将在服务端注册的对象类型的值保存为服务器激活类型对象（单个调用或 singleton）。</summary>
  [ComVisible(true)]
  public class WellKnownServiceTypeEntry : TypeEntry
  {
    private string _objectUri;
    private WellKnownObjectMode _mode;
    private IContextAttribute[] _contextAttributes;

    /// <summary>获取已知服务类型的 URI。</summary>
    /// <returns>服务器激活服务类型的 URI。</returns>
    public string ObjectUri
    {
      get
      {
        return this._objectUri;
      }
    }

    /// <summary>获取服务器激活服务类型的 <see cref="T:System.Runtime.Remoting.WellKnownObjectMode" />。</summary>
    /// <returns>服务器激活服务类型的 <see cref="T:System.Runtime.Remoting.WellKnownObjectMode" />。</returns>
    public WellKnownObjectMode Mode
    {
      get
      {
        return this._mode;
      }
    }

    /// <summary>获取服务器激活服务类型的 <see cref="T:System.Type" />。</summary>
    /// <returns>服务器激活服务类型的 <see cref="T:System.Type" />。</returns>
    public Type ObjectType
    {
      [MethodImpl(MethodImplOptions.NoInlining)] get
      {
        StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
        return RuntimeTypeHandle.GetTypeByName(this.TypeName + ", " + this.AssemblyName, ref stackMark);
      }
    }

    /// <summary>获取或设置服务器激活服务类型的上下文特性。</summary>
    /// <returns>获取或设置服务器激活服务类型的上下文特性。</returns>
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

    /// <summary>使用给定的类型名称、程序集名称、对象 URI 和 <see cref="T:System.Runtime.Remoting.WellKnownObjectMode" /> 初始化 <see cref="T:System.Runtime.Remoting.WellKnownServiceTypeEntry" /> 类的新实例。</summary>
    /// <param name="typeName">服务器激活服务类型的完整类型名称。</param>
    /// <param name="assemblyName">服务器激活服务类型的程序集名称。</param>
    /// <param name="objectUri">服务器激活对象的 URI。</param>
    /// <param name="mode">类型的 <see cref="T:System.Runtime.Remoting.WellKnownObjectMode" />，它定义如何激活该对象。</param>
    public WellKnownServiceTypeEntry(string typeName, string assemblyName, string objectUri, WellKnownObjectMode mode)
    {
      if (typeName == null)
        throw new ArgumentNullException("typeName");
      if (assemblyName == null)
        throw new ArgumentNullException("assemblyName");
      if (objectUri == null)
        throw new ArgumentNullException("objectUri");
      this.TypeName = typeName;
      this.AssemblyName = assemblyName;
      this._objectUri = objectUri;
      this._mode = mode;
    }

    /// <summary>使用给定的 <see cref="T:System.Type" />、对象 URI 和 <see cref="T:System.Runtime.Remoting.WellKnownObjectMode" /> 初始化 <see cref="T:System.Runtime.Remoting.WellKnownServiceTypeEntry" /> 类的新实例。</summary>
    /// <param name="type">服务器激活服务类型对象的 <see cref="T:System.Type" />。</param>
    /// <param name="objectUri">服务器激活类型的 URI。</param>
    /// <param name="mode">类型的 <see cref="T:System.Runtime.Remoting.WellKnownObjectMode" />，它定义如何激活该对象。</param>
    public WellKnownServiceTypeEntry(Type type, string objectUri, WellKnownObjectMode mode)
    {
      if (type == (Type) null)
        throw new ArgumentNullException("type");
      if (objectUri == null)
        throw new ArgumentNullException("objectUri");
      if (!(type is RuntimeType))
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"));
      this.TypeName = type.FullName;
      this.AssemblyName = type.Module.Assembly.FullName;
      this._objectUri = objectUri;
      this._mode = mode;
    }

    /// <summary>以 <see cref="T:System.String" /> 的形式返回服务器激活类型的类型名称、程序集名称、对象 URI 和 <see cref="T:System.Runtime.Remoting.WellKnownObjectMode" />。</summary>
    /// <returns>
    /// <see cref="T:System.String" /> 形式的服务器激活类型的类型名称、程序集名称、对象 URI 和 <see cref="T:System.Runtime.Remoting.WellKnownObjectMode" />。</returns>
    public override string ToString()
    {
      return "type='" + this.TypeName + ", " + this.AssemblyName + "'; objectUri=" + this._objectUri + "; mode=" + this._mode.ToString();
    }
  }
}
