// Decompiled with JetBrains decompiler
// Type: System.Collections.CaseInsensitiveHashCodeProvider
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Collections
{
  /// <summary>使用忽略字符串大小写的哈希算法，为对象提供哈希代码。</summary>
  /// <filterpriority>2</filterpriority>
  [Obsolete("Please use StringComparer instead.")]
  [ComVisible(true)]
  [Serializable]
  public class CaseInsensitiveHashCodeProvider : IHashCodeProvider
  {
    private TextInfo m_text;
    private static volatile CaseInsensitiveHashCodeProvider m_InvariantCaseInsensitiveHashCodeProvider;

    /// <summary>获取 <see cref="T:System.Collections.CaseInsensitiveHashCodeProvider" /> 的一个实例，该实例与当前线程的 <see cref="P:System.Threading.Thread.CurrentCulture" /> 关联并且始终可用。</summary>
    /// <returns>
    /// <see cref="T:System.Collections.CaseInsensitiveHashCodeProvider" /> 的实例，它与当前线程的 <see cref="P:System.Threading.Thread.CurrentCulture" /> 关联。</returns>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public static CaseInsensitiveHashCodeProvider Default
    {
      get
      {
        return new CaseInsensitiveHashCodeProvider(CultureInfo.CurrentCulture);
      }
    }

    /// <summary>获取 <see cref="T:System.Collections.CaseInsensitiveHashCodeProvider" /> 的一个实例，该实例与 <see cref="P:System.Globalization.CultureInfo.InvariantCulture" /> 关联并且始终可用。</summary>
    /// <returns>
    /// <see cref="T:System.Collections.CaseInsensitiveHashCodeProvider" /> 的实例，它与 <see cref="P:System.Globalization.CultureInfo.InvariantCulture" /> 关联。</returns>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public static CaseInsensitiveHashCodeProvider DefaultInvariant
    {
      get
      {
        if (CaseInsensitiveHashCodeProvider.m_InvariantCaseInsensitiveHashCodeProvider == null)
          CaseInsensitiveHashCodeProvider.m_InvariantCaseInsensitiveHashCodeProvider = new CaseInsensitiveHashCodeProvider(CultureInfo.InvariantCulture);
        return CaseInsensitiveHashCodeProvider.m_InvariantCaseInsensitiveHashCodeProvider;
      }
    }

    /// <summary>使用当前线程的 <see cref="P:System.Threading.Thread.CurrentCulture" /> 初始化 <see cref="T:System.Collections.CaseInsensitiveHashCodeProvider" /> 类的新实例。</summary>
    public CaseInsensitiveHashCodeProvider()
    {
      this.m_text = CultureInfo.CurrentCulture.TextInfo;
    }

    /// <summary>使用指定 <see cref="T:System.Globalization.CultureInfo" /> 初始化 <see cref="T:System.Collections.CaseInsensitiveHashCodeProvider" /> 类的新实例。</summary>
    /// <param name="culture">要用于新 <see cref="T:System.Collections.CaseInsensitiveHashCodeProvider" /> 的 <see cref="T:System.Globalization.CultureInfo" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="culture" /> 为 null。</exception>
    public CaseInsensitiveHashCodeProvider(CultureInfo culture)
    {
      if (culture == null)
        throw new ArgumentNullException("culture");
      this.m_text = culture.TextInfo;
    }

    /// <summary>使用忽略字符串大小写的哈希算法返回给定对象的哈希代码。</summary>
    /// <returns>给定对象的哈希代码（使用忽略字符串大小写的哈希算法返回）。</returns>
    /// <param name="obj">
    /// <see cref="T:System.Object" />，将为其返回哈希代码。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="obj" /> 为 null。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public int GetHashCode(object obj)
    {
      if (obj == null)
        throw new ArgumentNullException("obj");
      string str = obj as string;
      if (str == null)
        return obj.GetHashCode();
      return this.m_text.GetCaseInsensitiveHashCode(str);
    }
  }
}
