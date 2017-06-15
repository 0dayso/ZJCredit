// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.ActivatedServiceTypeEntry
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Threading;

namespace System.Runtime.Remoting
{
  /// <summary>将在服务端注册的对象类型的值保存为可以应客户端请求激活的类型。</summary>
  [ComVisible(true)]
  public class ActivatedServiceTypeEntry : TypeEntry
  {
    private IContextAttribute[] _contextAttributes;

    /// <summary>获取客户端激活服务类型的 <see cref="T:System.Type" />。</summary>
    /// <returns>客户端激活服务类型的 <see cref="T:System.Type" />。</returns>
    public Type ObjectType
    {
      [MethodImpl(MethodImplOptions.NoInlining)] get
      {
        StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
        return RuntimeTypeHandle.GetTypeByName(this.TypeName + ", " + this.AssemblyName, ref stackMark);
      }
    }

    /// <summary>获取或设置客户端激活服务类型的上下文特性。</summary>
    /// <returns>客户端激活服务类型的上下文特性。</returns>
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

    /// <summary>用给定的类型名称和程序集名称初始化 <see cref="T:System.Runtime.Remoting.ActivatedServiceTypeEntry" /> 类的新实例。</summary>
    /// <param name="typeName">客户端激活服务类型的类型名称。</param>
    /// <param name="assemblyName">客户端激活服务类型的程序集名称。</param>
    public ActivatedServiceTypeEntry(string typeName, string assemblyName)
    {
      if (typeName == null)
        throw new ArgumentNullException("typeName");
      if (assemblyName == null)
        throw new ArgumentNullException("assemblyName");
      this.TypeName = typeName;
      this.AssemblyName = assemblyName;
    }

    /// <summary>用给定的 <see cref="T:System.Type" /> 初始化 <see cref="T:System.Runtime.Remoting.ActivatedServiceTypeEntry" /> 类的新实例。</summary>
    /// <param name="type">客户端激活服务类型的 <see cref="T:System.Type" />。</param>
    public ActivatedServiceTypeEntry(Type type)
    {
      if (type == (Type) null)
        throw new ArgumentNullException("type");
      RuntimeType runtimeType = type as RuntimeType;
      if (runtimeType == (RuntimeType) null)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeAssembly"));
      this.TypeName = type.FullName;
      this.AssemblyName = runtimeType.GetRuntimeAssembly().GetSimpleName();
    }

    /// <summary>以 <see cref="T:System.String" /> 的形式返回客户端激活服务类型的类型名称和程序集名称。</summary>
    /// <returns>
    /// <see cref="T:System.String" /> 形式的客户端激活服务类型的类型名称和程序集名称。</returns>
    public override string ToString()
    {
      return "type='" + this.TypeName + ", " + this.AssemblyName + "'";
    }
  }
}
