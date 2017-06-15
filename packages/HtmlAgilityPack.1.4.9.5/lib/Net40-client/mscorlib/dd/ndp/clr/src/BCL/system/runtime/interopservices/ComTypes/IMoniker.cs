// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComTypes.IMoniker
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices.ComTypes
{
  /// <summary>提供 IMoniker 接口的托管定义，具有 IPersist 和 IPersistStream 中的 COM 功能。</summary>
  [Guid("0000000f-0000-0000-C000-000000000046")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [__DynamicallyInvokable]
  [ComImport]
  public interface IMoniker
  {
    /// <summary>检索对象的类标识符 (CLSID)。</summary>
    /// <param name="pClassID">此方法返回时，包含 CLSID。该参数未经初始化即被传递。</param>
    [__DynamicallyInvokable]
    void GetClassID(out Guid pClassID);

    /// <summary>检查该对象自上次保存以来所发生的更改。</summary>
    /// <returns>如果该对象已更改，则为 S_OKHRESULT 值；否则为 S_FALSEHRESULT 值。</returns>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int IsDirty();

    /// <summary>从以前保存对象的流中初始化对象。</summary>
    /// <param name="pStm">从中加载对象的流。</param>
    [__DynamicallyInvokable]
    void Load(IStream pStm);

    /// <summary>将对象保存到指定流。</summary>
    /// <param name="pStm">将对象保存到的流。</param>
    /// <param name="fClearDirty">如果要在保存完成之后清除修改后的标志，则为 true；否则为 false</param>
    [__DynamicallyInvokable]
    void Save(IStream pStm, [MarshalAs(UnmanagedType.Bool)] bool fClearDirty);

    /// <summary>返回保存该对象所需的流的大小（以字节为单位）。</summary>
    /// <param name="pcbSize">此方法返回时，包含 long 值，该值指示保存此对象所需的流的大小（以字节为单位）。该参数未经初始化即被传递。</param>
    [__DynamicallyInvokable]
    void GetSizeMax(out long pcbSize);

    /// <summary>使用名字对象绑定到它所标识的对象。</summary>
    /// <param name="pbc">对在此绑定操作中使用的绑定上下文对象上的 IBindCtx 接口的引用。</param>
    /// <param name="pmkToLeft">如果名字对象是复合名字对象的一部分，则为对当前名字对象左边的名字对象的引用。</param>
    /// <param name="riidResult">接口的接口标识符 (IID)，客户端打算使用该接口与名字对象标识的对象进行通信。</param>
    /// <param name="ppvResult">此方法返回时，包含对 <paramref name="riidResult" /> 请求的接口的引用。该参数未经初始化即被传递。</param>
    [__DynamicallyInvokable]
    void BindToObject(IBindCtx pbc, IMoniker pmkToLeft, [In] ref Guid riidResult, [MarshalAs(UnmanagedType.Interface)] out object ppvResult);

    /// <summary>检索指向存储（该存储包含名字对象所标识的对象）的接口指针。</summary>
    /// <param name="pbc">对在此绑定操作过程中使用的绑定上下文对象上的 IBindCtx 接口的引用。</param>
    /// <param name="pmkToLeft">如果名字对象是复合名字对象的一部分，则为对当前名字对象左边的名字对象的引用。</param>
    /// <param name="riid">所请求的存储接口的接口标识符 (IID)。</param>
    /// <param name="ppvObj">此方法返回时，包含对 <paramref name="riid" /> 请求的接口的引用。该参数未经初始化即被传递。</param>
    [__DynamicallyInvokable]
    void BindToStorage(IBindCtx pbc, IMoniker pmkToLeft, [In] ref Guid riid, [MarshalAs(UnmanagedType.Interface)] out object ppvObj);

    /// <summary>返回简化的名字对象，它是与当前名字对象引用同一对象但能够用相等或更高的效率绑定的另一个名字对象。</summary>
    /// <param name="pbc">对在此绑定操作中使用的绑定上下文中的 IBindCtx 接口的引用。</param>
    /// <param name="dwReduceHowFar">指定当前名字对象简化程度的值。</param>
    /// <param name="ppmkToLeft">对当前名字对象左边的名字对象的引用。</param>
    /// <param name="ppmkReduced">此方法返回时，包含对当前名字对象的简化形式的引用（如果发生错误或当前名字对象被简化为无，则可以为 null）。该参数未经初始化即被传递。</param>
    [__DynamicallyInvokable]
    void Reduce(IBindCtx pbc, int dwReduceHowFar, ref IMoniker ppmkToLeft, out IMoniker ppmkReduced);

    /// <summary>将当前名字对象与另一名字对象组合，创建一个新的复合名字对象。</summary>
    /// <param name="pmkRight">对名字对象上的 IMoniker 接口的引用，该引用将追加到当前名字对象的末尾。</param>
    /// <param name="fOnlyIfNotGeneric">true 指示调用方需要非通用复合。仅当 <paramref name="pmkRight" /> 为当前名字对象可以采用不同于构成通用复合的方式与其组合的名字对象类时，该操作才继续。而 false 指示该方法可以在必要时创建通用复合。</param>
    /// <param name="ppmkComposite">此方法返回时，包含对结果复合名字对象的引用。该参数未经初始化即被传递。</param>
    [__DynamicallyInvokable]
    void ComposeWith(IMoniker pmkRight, [MarshalAs(UnmanagedType.Bool)] bool fOnlyIfNotGeneric, out IMoniker ppmkComposite);

    /// <summary>提供一个指向枚举器（该枚举器可枚举复合名字对象的组件）的指针。</summary>
    /// <param name="fForward">true 表示按从左到右的顺序枚举名字对象。而 false 表示按从右到左的顺序枚举。</param>
    /// <param name="ppenumMoniker">此方法返回时，包含对名字对象的枚举数对象的引用。该参数未经初始化即被传递。</param>
    [__DynamicallyInvokable]
    void Enum([MarshalAs(UnmanagedType.Bool)] bool fForward, out IEnumMoniker ppenumMoniker);

    /// <summary>将当前名字对象与指定的名字对象进行比较，并指示它们是否相同。</summary>
    /// <returns>如果名字对象相同，则为 S_OKHRESULT 值；否则为 S_FALSEHRESULT 值。</returns>
    /// <param name="pmkOtherMoniker">对用于比较的名字对象的引用。</param>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int IsEqual(IMoniker pmkOtherMoniker);

    /// <summary>使用该名字对象的内部状态计算 32 位整数。</summary>
    /// <param name="pdwHash">此方法返回时，包含此名字对象的哈希值。该参数未经初始化即被传递。</param>
    [__DynamicallyInvokable]
    void Hash(out int pdwHash);

    /// <summary>确定由当前名字对象标识的对象当前是否已加载并正在运行。</summary>
    /// <returns>如果名字对象处于运行状态，则为 S_OKHRESULT 值；如果名字对象不处于运行状态，则为 S_FALSEHRESULT 值；否则为 E_UNEXPECTEDHRESULT 值。</returns>
    /// <param name="pbc">对要在此绑定操作中使用的绑定上下文的引用。</param>
    /// <param name="pmkToLeft">如果当前名字对象是复合名字对象的一部分，则为对当前名字对象左边的名字对象的引用。</param>
    /// <param name="pmkNewlyRunning">对最近添加到运行对象表 (ROT) 的名字对象的引用。</param>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int IsRunning(IBindCtx pbc, IMoniker pmkToLeft, IMoniker pmkNewlyRunning);

    /// <summary>提供一个数字，该数字表示当前名字对象所标识的对象的上次更改时间。</summary>
    /// <param name="pbc">对要在此绑定操作中使用的绑定上下文的引用。</param>
    /// <param name="pmkToLeft">如果名字对象是复合名字对象的一部分，则为对当前名字对象左边的名字对象的引用。</param>
    /// <param name="pFileTime">此方法返回时，包含上次更改时间。该参数未经初始化即被传递。</param>
    [__DynamicallyInvokable]
    void GetTimeOfLastChange(IBindCtx pbc, IMoniker pmkToLeft, out FILETIME pFileTime);

    /// <summary>提供一个名字对象，该名字对象在被复合到当前名字对象或一个结构相似的名字对象的右边时，将不复合到任何对象。</summary>
    /// <param name="ppmk">此方法返回时，包含一个名字对象，它是当前名字对象的逆命题。该参数未经初始化即被传递。</param>
    [__DynamicallyInvokable]
    void Inverse(out IMoniker ppmk);

    /// <summary>基于此名字对象与另一名字对象共有的公共前缀创建新的名字对象。</summary>
    /// <param name="pmkOther">对另一名字对象上的 IMoniker 接口的引用，将使用该名字对象与当前名字对象进行比较，以获得公共前缀。</param>
    /// <param name="ppmkPrefix">此方法返回时，包含作为当前名字对象和 <paramref name="pmkOther" /> 的公共前缀的名字对象。该参数未经初始化即被传递。</param>
    [__DynamicallyInvokable]
    void CommonPrefixWith(IMoniker pmkOther, out IMoniker ppmkPrefix);

    /// <summary>提供一个名字对象，该名字对象在被追加到当前名字对象（或一个有相似结构的名字对象）时生成指定名字对象。</summary>
    /// <param name="pmkOther">对应该对其采用相对路径的名字对象的引用。</param>
    /// <param name="ppmkRelPath">此方法返回时，包含对相关名字对象的引用。该参数未经初始化即被传递。</param>
    [__DynamicallyInvokable]
    void RelativePathTo(IMoniker pmkOther, out IMoniker ppmkRelPath);

    /// <summary>获取显示名称，该名称是当前名字对象的用户可读表示形式。</summary>
    /// <param name="pbc">对在此操作中使用的绑定上下文的引用。</param>
    /// <param name="pmkToLeft">如果名字对象是复合名字对象的一部分，则为对当前名字对象左边的名字对象的引用。</param>
    /// <param name="ppszDisplayName">此方法返回时，包含显示名称字符串。该参数未经初始化即被传递。</param>
    [__DynamicallyInvokable]
    void GetDisplayName(IBindCtx pbc, IMoniker pmkToLeft, [MarshalAs(UnmanagedType.LPWStr)] out string ppszDisplayName);

    /// <summary>读取指定的显示名称中 <see cref="M:System.Runtime.InteropServices.ComTypes.IMoniker.ParseDisplayName(System.Runtime.InteropServices.ComTypes.IBindCtx,System.Runtime.InteropServices.ComTypes.IMoniker,System.String,System.Int32@,System.Runtime.InteropServices.ComTypes.IMoniker@)" /> 能够理解的全部字符并生成一个与读取的部分相对应的名字对象。</summary>
    /// <param name="pbc">对要在此绑定操作中使用的绑定上下文的引用。</param>
    /// <param name="pmkToLeft">对迄今为止已经从显示名称生成的名字对象的引用。</param>
    /// <param name="pszDisplayName">对包含要分析的剩余显示名称的字符串的引用。</param>
    /// <param name="pchEaten">此方法返回时，包含分析 <paramref name="pszDisplayName" /> 时所使用的字符数。该参数未经初始化即被传递。</param>
    /// <param name="ppmkOut">此方法返回时，包含对从 <paramref name="pszDisplayName" /> 生成的名字对象的引用。该参数未经初始化即被传递。</param>
    [__DynamicallyInvokable]
    void ParseDisplayName(IBindCtx pbc, IMoniker pmkToLeft, [MarshalAs(UnmanagedType.LPWStr)] string pszDisplayName, out int pchEaten, out IMoniker ppmkOut);

    /// <summary>指示该名字对象是否是系统提供的名字对象类之一的对象。</summary>
    /// <returns>如果名字对象为系统名字对象，则为 S_OKHRESULT 值；否则为 S_FALSEHRESULT 值。</returns>
    /// <param name="pdwMksys">此方法返回时，包含指向一个整数的指针，该整数是 MKSYS 枚举值之一并引用 COM 名字对象类之一。该参数未经初始化即被传递。</param>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int IsSystemMoniker(out int pdwMksys);
  }
}
