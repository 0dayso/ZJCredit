// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.EventBuilder
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Reflection.Emit
{
  /// <summary>定义类的事件。</summary>
  [ClassInterface(ClassInterfaceType.None)]
  [ComDefaultInterface(typeof (_EventBuilder))]
  [ComVisible(true)]
  [HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
  public sealed class EventBuilder : _EventBuilder
  {
    private string m_name;
    private EventToken m_evToken;
    private ModuleBuilder m_module;
    private EventAttributes m_attributes;
    private TypeBuilder m_type;

    private EventBuilder()
    {
    }

    internal EventBuilder(ModuleBuilder mod, string name, EventAttributes attr, TypeBuilder type, EventToken evToken)
    {
      this.m_name = name;
      this.m_module = mod;
      this.m_attributes = attr;
      this.m_evToken = evToken;
      this.m_type = type;
    }

    /// <summary>返回该事件的标记。</summary>
    /// <returns>返回该事件的 EventToken。</returns>
    public EventToken GetEventToken()
    {
      return this.m_evToken;
    }

    [SecurityCritical]
    private void SetMethodSemantics(MethodBuilder mdBuilder, MethodSemanticsAttributes semantics)
    {
      if ((MethodInfo) mdBuilder == (MethodInfo) null)
        throw new ArgumentNullException("mdBuilder");
      this.m_type.ThrowIfCreated();
      TypeBuilder.DefineMethodSemantics(this.m_module.GetNativeHandle(), this.m_evToken.Token, semantics, mdBuilder.GetToken().Token);
    }

    /// <summary>设置用于预订该事件的方法。</summary>
    /// <param name="mdBuilder">MethodBuilder 对象，表示用于预订该事件的方法。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="mdBuilder" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">已对封闭类型调用了 <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />。</exception>
    [SecuritySafeCritical]
    public void SetAddOnMethod(MethodBuilder mdBuilder)
    {
      this.SetMethodSemantics(mdBuilder, MethodSemanticsAttributes.AddOn);
    }

    /// <summary>设置用于取消预订该事件的方法。</summary>
    /// <param name="mdBuilder">MethodBuilder 对象，表示用于取消预订该事件的方法。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="mdBuilder" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">已对封闭类型调用了 <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />。</exception>
    [SecuritySafeCritical]
    public void SetRemoveOnMethod(MethodBuilder mdBuilder)
    {
      this.SetMethodSemantics(mdBuilder, MethodSemanticsAttributes.RemoveOn);
    }

    /// <summary>设置用于引发该事件的方法。</summary>
    /// <param name="mdBuilder">MethodBuilder 对象，表示用于引发该事件的方法。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="mdBuilder" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">已对封闭类型调用了 <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />。</exception>
    [SecuritySafeCritical]
    public void SetRaiseMethod(MethodBuilder mdBuilder)
    {
      this.SetMethodSemantics(mdBuilder, MethodSemanticsAttributes.Fire);
    }

    /// <summary>添加与该事件关联的“其他”方法之一。“其他”方法是与该事件关联的、除了“开”(on) 和“引发”(raise) 方法以外的方法。可以多次调用此函数，以添加一样多的“其他”方法。</summary>
    /// <param name="mdBuilder">一个表示另一个方法的 MethodBuilder 对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="mdBuilder" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">已对封闭类型调用了 <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />。</exception>
    [SecuritySafeCritical]
    public void AddOtherMethod(MethodBuilder mdBuilder)
    {
      this.SetMethodSemantics(mdBuilder, MethodSemanticsAttributes.Other);
    }

    /// <summary>使用指定的自定义属性 Blob 设置自定义属性。</summary>
    /// <param name="con">自定义属性的构造函数。</param>
    /// <param name="binaryAttribute">表示属性的字节 Blob。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="con" /> 或 <paramref name="binaryAttribute" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">已对封闭类型调用了 <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />。</exception>
    [SecuritySafeCritical]
    [ComVisible(true)]
    public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
    {
      if (con == (ConstructorInfo) null)
        throw new ArgumentNullException("con");
      if (binaryAttribute == null)
        throw new ArgumentNullException("binaryAttribute");
      this.m_type.ThrowIfCreated();
      TypeBuilder.DefineCustomAttribute(this.m_module, this.m_evToken.Token, this.m_module.GetConstructorToken(con).Token, binaryAttribute, false, false);
    }

    /// <summary>使用自定义属性生成器设置自定义属性。</summary>
    /// <param name="customBuilder">对自定义属性进行描述的帮助器类的实例。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="con" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">已对封闭类型调用了 <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />。</exception>
    [SecuritySafeCritical]
    public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
    {
      if (customBuilder == null)
        throw new ArgumentNullException("customBuilder");
      this.m_type.ThrowIfCreated();
      customBuilder.CreateCustomAttribute(this.m_module, this.m_evToken.Token);
    }

    void _EventBuilder.GetTypeInfoCount(out uint pcTInfo)
    {
      throw new NotImplementedException();
    }

    void _EventBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
    {
      throw new NotImplementedException();
    }

    void _EventBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
    {
      throw new NotImplementedException();
    }

    void _EventBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
    {
      throw new NotImplementedException();
    }
  }
}
