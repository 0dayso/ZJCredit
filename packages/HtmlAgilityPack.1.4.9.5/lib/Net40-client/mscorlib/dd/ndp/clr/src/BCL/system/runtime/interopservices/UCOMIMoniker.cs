// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.UCOMIMoniker
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices
{
  /// <summary>请改用 <see cref="T:System.Runtime.InteropServices.ComTypes.IMoniker" />。</summary>
  [Obsolete("Use System.Runtime.InteropServices.ComTypes.IMoniker instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
  [Guid("0000000f-0000-0000-C000-000000000046")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  public interface UCOMIMoniker
  {
    /// <summary>检索对象的类标识符 (CLSID)。</summary>
    /// <param name="pClassID">成功返回时，包含 CLSID。</param>
    void GetClassID(out Guid pClassID);

    /// <summary>检查该对象自上次保存以来所发生的更改。</summary>
    /// <returns>如果该对象已更改，则为 S_OKHRESULT 值；否则为 S_FALSEHRESULT 值。</returns>
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int IsDirty();

    /// <summary>从以前保存对象的流中初始化对象。</summary>
    /// <param name="pStm">从中加载该对象的流。</param>
    void Load(UCOMIStream pStm);

    /// <summary>将对象保存到指定流。</summary>
    /// <param name="pStm">将对象保存到的流。</param>
    /// <param name="fClearDirty">指示在保存完成后是否清除修改标志。</param>
    void Save(UCOMIStream pStm, [MarshalAs(UnmanagedType.Bool)] bool fClearDirty);

    /// <summary>返回保存该对象所需的流的大小（以字节为单位）。</summary>
    /// <param name="pcbSize">成功返回时，包含 long 值，该值指示保存该对象所需的流的大小（以字节为单位）。</param>
    void GetSizeMax(out long pcbSize);

    /// <summary>使用名字对象绑定到它所标识的对象。</summary>
    /// <param name="pbc">对在此绑定操作中使用的绑定上下文对象上的 IBindCtx 接口的引用。</param>
    /// <param name="pmkToLeft">如果名字对象是复合名字对象的一部分，则为对该名字对象左边的名字对象的引用。</param>
    /// <param name="riidResult">接口的接口标识符 (IID)，客户端打算使用该接口与名字对象标识的对象进行通信。</param>
    /// <param name="ppvResult">成功返回时，对 <paramref name="riidResult" /> 所请求的接口的引用。</param>
    void BindToObject(UCOMIBindCtx pbc, UCOMIMoniker pmkToLeft, [In] ref Guid riidResult, [MarshalAs(UnmanagedType.Interface)] out object ppvResult);

    /// <summary>检索指向存储（该存储包含名字对象所标识的对象）的接口指针。</summary>
    /// <param name="pbc">对在此绑定操作过程中使用的绑定上下文对象上的 IBindCtx 接口的引用。</param>
    /// <param name="pmkToLeft">如果名字对象是复合名字对象的一部分，则为对该名字对象左边的名字对象的引用。</param>
    /// <param name="riid">所请求的存储接口的接口标识符 (IID)。</param>
    /// <param name="ppvObj">成功返回时，对 <paramref name="riid" /> 所请求的接口的引用。</param>
    void BindToStorage(UCOMIBindCtx pbc, UCOMIMoniker pmkToLeft, [In] ref Guid riid, [MarshalAs(UnmanagedType.Interface)] out object ppvObj);

    /// <summary>返回简化的名字对象，它是与该名字对象引用同一对象但能够用相等或更高的效率绑定的另一个名字对象。</summary>
    /// <param name="pbc">对要在此绑定操作中使用的绑定上下文中的 IBindCtx 接口的引用。</param>
    /// <param name="dwReduceHowFar">指定该名字对象应该简化的程度。</param>
    /// <param name="ppmkToLeft">对该名字对象左边的名字对象的引用。</param>
    /// <param name="ppmkReduced">成功返回时，对该名字对象的简化形式的引用（如果发生错误或该名字对象被简化为无，则可以为 null）。</param>
    void Reduce(UCOMIBindCtx pbc, int dwReduceHowFar, ref UCOMIMoniker ppmkToLeft, out UCOMIMoniker ppmkReduced);

    /// <summary>将当前名字对象与另一名字对象组合，创建一个新的复合名字对象。</summary>
    /// <param name="pmkRight">对名字对象上的 IMoniker 接口的引用，该引用将复合到此名字对象的结尾。</param>
    /// <param name="fOnlyIfNotGeneric">如果为 true，则调用方请求非一般复合，以便仅当 <paramref name="pmkRight" /> 是一个符合下列条件的名字对象类时该操作才能继续执行，该条件为：此名字对象可以通过除构成一般复合名字对象以外的方式与该名字对象类复合。如果为 false，则此方法可在必要时创建一个一般复合名字对象。</param>
    /// <param name="ppmkComposite">成功返回时，对得到的复合名字对象的引用。</param>
    void ComposeWith(UCOMIMoniker pmkRight, [MarshalAs(UnmanagedType.Bool)] bool fOnlyIfNotGeneric, out UCOMIMoniker ppmkComposite);

    /// <summary>提供一个指向枚举器（该枚举器可枚举复合名字对象的组件）的指针。</summary>
    /// <param name="fForward">如果为 true，则从左向右枚举名字对象。如果为 false，则从右向左进行枚举。</param>
    /// <param name="ppenumMoniker">成功返回时，引用该名字对象的枚举器对象。</param>
    void Enum([MarshalAs(UnmanagedType.Bool)] bool fForward, out UCOMIEnumMoniker ppenumMoniker);

    /// <summary>将该名字对象与指定的名字对象进行比较并指示它们是否相同。</summary>
    /// <param name="pmkOtherMoniker">对用于比较的名字对象的引用。</param>
    void IsEqual(UCOMIMoniker pmkOtherMoniker);

    /// <summary>使用该名字对象的内部状态计算 32 位整数。</summary>
    /// <param name="pdwHash">成功返回时，包含此名字对象的哈希值。</param>
    void Hash(out int pdwHash);

    /// <summary>确定由该名字对象标识的对象当前是否已加载并正在运行。</summary>
    /// <param name="pbc">对要用在此绑定操作中的绑定上下文的引用。</param>
    /// <param name="pmkToLeft">如果该名字对象是复合名字对象的一部分，则为对该名字对象左边的名字对象的引用。</param>
    /// <param name="pmkNewlyRunning">对最近添加到运行对象表的名字对象的引用。</param>
    void IsRunning(UCOMIBindCtx pbc, UCOMIMoniker pmkToLeft, UCOMIMoniker pmkNewlyRunning);

    /// <summary>提供一个数字，该数字表示此名字对象所标识的对象的上次更改时间。</summary>
    /// <param name="pbc">对要用在此绑定操作中的绑定上下文的引用。</param>
    /// <param name="pmkToLeft">如果名字对象是复合名字对象的一部分，则为对该名字对象左边的名字对象的引用。</param>
    /// <param name="pFileTime">成功返回时，包含上次更改的时间。</param>
    void GetTimeOfLastChange(UCOMIBindCtx pbc, UCOMIMoniker pmkToLeft, out FILETIME pFileTime);

    /// <summary>提供一个名字对象，该名字对象在被复合到此名字对象或一个结构相似的名字对象的右边时，将不复合到任何对象。</summary>
    /// <param name="ppmk">成功返回时，包含该名字对象的反向名字对象。</param>
    void Inverse(out UCOMIMoniker ppmk);

    /// <summary>基于此名字对象与另一名字对象共有的公共前缀创建新的名字对象。</summary>
    /// <param name="pmkOther">对另一名字对象（要将其与此名字对象进行比较以获得公共前缀）上的 IMoniker 接口的引用。</param>
    /// <param name="ppmkPrefix">成功返回时，包含作为此名字对象和 <paramref name="pmkOther" /> 的公共前缀的名字对象。</param>
    void CommonPrefixWith(UCOMIMoniker pmkOther, out UCOMIMoniker ppmkPrefix);

    /// <summary>提供一个名字对象，该名字对象在被追加到此名字对象（或一个有相似结构的名字对象）时生成指定名字对象。</summary>
    /// <param name="pmkOther">对应该对其采用相对路径的名字对象的引用。</param>
    /// <param name="ppmkRelPath">成功返回时，对相对名字对象的引用。</param>
    void RelativePathTo(UCOMIMoniker pmkOther, out UCOMIMoniker ppmkRelPath);

    /// <summary>获取显示名称，该名称是此名字对象的用户可读表示形式。</summary>
    /// <param name="pbc">对在此操作中使用的绑定上下文的引用。</param>
    /// <param name="pmkToLeft">如果名字对象是复合名字对象的一部分，则为对该名字对象左边的名字对象的引用。</param>
    /// <param name="ppszDisplayName">成功返回时，包含显示名称字符串。</param>
    void GetDisplayName(UCOMIBindCtx pbc, UCOMIMoniker pmkToLeft, [MarshalAs(UnmanagedType.LPWStr)] out string ppszDisplayName);

    /// <summary>读取指定的显示名称中它能够理解的全部字符并生成一个与读取的部分相对应的名字对象。</summary>
    /// <param name="pbc">对要用在此绑定操作中的绑定上下文的引用。</param>
    /// <param name="pmkToLeft">对迄今为止已经从显示名称生成的名字对象的引用。</param>
    /// <param name="pszDisplayName">对包含要分析的剩余显示名称的字符串的引用。</param>
    /// <param name="pchEaten">成功返回时，包含在此步骤中使用的 <paramref name="pszDisplayName" /> 中的字符数。</param>
    /// <param name="ppmkOut">对从 <paramref name="pszDisplayName" /> 生成的名字对象的引用。</param>
    void ParseDisplayName(UCOMIBindCtx pbc, UCOMIMoniker pmkToLeft, [MarshalAs(UnmanagedType.LPWStr)] string pszDisplayName, out int pchEaten, out UCOMIMoniker ppmkOut);

    /// <summary>指示该名字对象是否是系统提供的名字对象类之一的对象。</summary>
    /// <param name="pdwMksys">指向一个整数的指针，该整数是 MKSYS 枚举值之一并引用 COM 名字对象类之一。</param>
    void IsSystemMoniker(out int pdwMksys);
  }
}
