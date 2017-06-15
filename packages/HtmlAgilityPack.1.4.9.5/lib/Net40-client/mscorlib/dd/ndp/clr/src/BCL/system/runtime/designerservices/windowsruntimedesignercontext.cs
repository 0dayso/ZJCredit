// Decompiled with JetBrains decompiler
// Type: System.Runtime.DesignerServices.WindowsRuntimeDesignerContext
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Policy;
using System.Threading;

namespace System.Runtime.DesignerServices
{
  /// <summary>对用于创建 Windows 8.x 应用商店 应用的设计器提供自定义组件绑定。</summary>
  public sealed class WindowsRuntimeDesignerContext
  {
    private static object s_lock = new object();
    private static IntPtr s_sharedContext;
    private IntPtr m_contextObject;
    private string m_name;

    /// <summary>获取绑定上下文设计器的名称。</summary>
    /// <returns>上下文名称。</returns>
    public string Name
    {
      get
      {
        return this.m_name;
      }
    }

    [SecurityCritical]
    private WindowsRuntimeDesignerContext(IEnumerable<string> paths, string name, bool designModeRequired)
    {
      if (name == null)
        throw new ArgumentNullException("name");
      if (paths == null)
        throw new ArgumentNullException("paths");
      if (!AppDomain.CurrentDomain.IsDefaultAppDomain())
        throw new NotSupportedException();
      if (!AppDomain.IsAppXModel())
        throw new NotSupportedException();
      if (designModeRequired && !AppDomain.IsAppXDesignMode())
        throw new NotSupportedException();
      this.m_name = name;
      lock (WindowsRuntimeDesignerContext.s_lock)
      {
        if (WindowsRuntimeDesignerContext.s_sharedContext == IntPtr.Zero)
          WindowsRuntimeDesignerContext.InitializeSharedContext((IEnumerable<string>) new string[0]);
      }
      this.m_contextObject = WindowsRuntimeDesignerContext.CreateDesignerContext(paths, false);
    }

    /// <summary>初始化 <see cref="T:System.Runtime.DesignerServices.WindowsRuntimeDesignerContext" /> 类的新实例,该实例指定搜索第三方 Windows 运行时 类型和托管程序的路径集，并指定上下文的名称。</summary>
    /// <param name="paths">要搜索的路径。</param>
    /// <param name="name">上下文名称。</param>
    /// <exception cref="T:System.NotSupportedException">当前应用程序域不是默认应用程序域。- 或 -该过程在应用程序容器中不运行。- 或 -计算机不含开发人员许可证。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 或 <paramref name="paths" /> 为 null。</exception>
    [SecurityCritical]
    public WindowsRuntimeDesignerContext(IEnumerable<string> paths, string name)
      : this(paths, name, true)
    {
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern IntPtr CreateDesignerContext([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1, ArraySubType = UnmanagedType.LPWStr)] string[] paths, int count, bool shared);

    [SecurityCritical]
    internal static IntPtr CreateDesignerContext(IEnumerable<string> paths, [MarshalAs(UnmanagedType.Bool)] bool shared)
    {
      string[] array = new List<string>(paths).ToArray();
      foreach (string path in array)
      {
        if (path == null)
          throw new ArgumentNullException(Environment.GetResourceString("ArgumentNull_Path"));
        if (Path.IsRelative(path))
          throw new ArgumentException(Environment.GetResourceString("Argument_AbsolutePathRequired"));
      }
      string[] paths1 = array;
      int length = paths1.Length;
      int num = shared ? 1 : 0;
      return WindowsRuntimeDesignerContext.CreateDesignerContext(paths1, length, num != 0);
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern void SetCurrentContext([MarshalAs(UnmanagedType.Bool)] bool isDesignerContext, IntPtr context);

    /// <summary>创建上下文并将其设置为共享上下文。</summary>
    /// <param name="paths">用于迭代上下文不能满足的解析绑定请求的路径的可枚举集合。</param>
    /// <exception cref="T:System.NotSupportedException">在此应用程序域中已经设置的共享上下文。- 或 -当前应用程序域不是默认应用程序域。- 或 -该过程在应用程序容器中不运行。- 或 -计算机不含开发人员许可证。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="paths" /> 为 null。</exception>
    [SecurityCritical]
    public static void InitializeSharedContext(IEnumerable<string> paths)
    {
      if (!AppDomain.CurrentDomain.IsDefaultAppDomain())
        throw new NotSupportedException();
      if (paths == null)
        throw new ArgumentNullException("paths");
      lock (WindowsRuntimeDesignerContext.s_lock)
      {
        if (WindowsRuntimeDesignerContext.s_sharedContext != IntPtr.Zero)
          throw new NotSupportedException();
        IntPtr local_2 = WindowsRuntimeDesignerContext.CreateDesignerContext(paths, true);
        WindowsRuntimeDesignerContext.SetCurrentContext(false, local_2);
        WindowsRuntimeDesignerContext.s_sharedContext = local_2;
      }
    }

    /// <summary>因为设在计过程中程序集是重新编译的，设置上下文以处理程序集绑定请求的迭代。</summary>
    /// <param name="context">处理程序集绑定请求的迭代的上下文。</param>
    /// <exception cref="T:System.NotSupportedException">当前应用程序域不是默认应用程序域。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="context" /> 为 null。</exception>
    [SecurityCritical]
    public static void SetIterationContext(WindowsRuntimeDesignerContext context)
    {
      if (!AppDomain.CurrentDomain.IsDefaultAppDomain())
        throw new NotSupportedException();
      if (context == null)
        throw new ArgumentNullException("context");
      lock (WindowsRuntimeDesignerContext.s_lock)
        WindowsRuntimeDesignerContext.SetCurrentContext(true, context.m_contextObject);
    }

    /// <summary>从当前上下文加载指定程序集。</summary>
    /// <returns>如果在当前上下文中发现程序集，则为该程序集；否则为 null。</returns>
    /// <param name="assemblyName">要加载的程序集的全名。有关完整的程序集名称的描述，请参见 <see cref="P:System.Reflection.Assembly.FullName" /> 属性。</param>
    [SecurityCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Assembly GetAssembly(string assemblyName)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Assembly) RuntimeAssembly.InternalLoad(assemblyName, (Evidence) null, ref stackMark, this.m_contextObject, false);
    }

    /// <summary>从当前上下文加载指定类型。</summary>
    /// <returns>如果在集合中发现当前上下文的类型；否则为 null。</returns>
    /// <param name="typeName">要加载的类型的程序集限定名称。有关程序集限定的名称的描述，请参见 <see cref="P:System.Type.AssemblyQualifiedName" /> 属性。</param>
    [SecurityCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Type GetType(string typeName)
    {
      if (typeName == null)
        throw new ArgumentNullException("typeName");
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Type) RuntimeTypeHandle.GetTypeByName(typeName, false, false, false, ref stackMark, this.m_contextObject, false);
    }
  }
}
