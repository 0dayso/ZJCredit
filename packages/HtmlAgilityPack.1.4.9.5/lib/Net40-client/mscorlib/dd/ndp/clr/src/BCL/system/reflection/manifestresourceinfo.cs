// Decompiled with JetBrains decompiler
// Type: System.Reflection.ManifestResourceInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>提供对清单资源的访问，这些资源是描述应用程序依赖项的 XML 文件。</summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public class ManifestResourceInfo
  {
    private Assembly _containingAssembly;
    private string _containingFileName;
    private ResourceLocation _resourceLocation;

    /// <summary>获取包含清单资源的程序集。</summary>
    /// <returns>包含清单资源的程序集。</returns>
    [__DynamicallyInvokable]
    public virtual Assembly ReferencedAssembly
    {
      [__DynamicallyInvokable] get
      {
        return this._containingAssembly;
      }
    }

    /// <summary>获取包含清单资源的文件名（如果该文件与清单文件不同）。</summary>
    /// <returns>清单资源的文件名。</returns>
    [__DynamicallyInvokable]
    public virtual string FileName
    {
      [__DynamicallyInvokable] get
      {
        return this._containingFileName;
      }
    }

    /// <summary>获取清单资源的位置。</summary>
    /// <returns>指示清单资源位置的 <see cref="T:System.Reflection.ResourceLocation" /> 标志的按位组合。</returns>
    [__DynamicallyInvokable]
    public virtual ResourceLocation ResourceLocation
    {
      [__DynamicallyInvokable] get
      {
        return this._resourceLocation;
      }
    }

    /// <summary>为由指定的程序集和文件包含且具有指定位置的资源初始化 <see cref="T:System.Reflection.ManifestResourceInfo" /> 类的新实例。</summary>
    /// <param name="containingAssembly">包含清单资源的程序集。</param>
    /// <param name="containingFileName">包含清单资源的文件名（如果该文件与清单文件不同）。</param>
    /// <param name="resourceLocation">一个枚举值的按位组合，提供有关清单资源位置的信息。</param>
    [__DynamicallyInvokable]
    public ManifestResourceInfo(Assembly containingAssembly, string containingFileName, ResourceLocation resourceLocation)
    {
      this._containingAssembly = containingAssembly;
      this._containingFileName = containingFileName;
      this._resourceLocation = resourceLocation;
    }
  }
}
