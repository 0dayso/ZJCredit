// Decompiled with JetBrains decompiler
// Type: System.Security.IEvidenceFactory
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security.Policy;

namespace System.Security
{
  /// <summary>获取对象的 <see cref="T:System.Security.Policy.Evidence" />。</summary>
  [ComVisible(true)]
  public interface IEvidenceFactory
  {
    /// <summary>获取验证当前对象标识的 <see cref="T:System.Security.Policy.Evidence" />。</summary>
    /// <returns>当前对象标识的 <see cref="T:System.Security.Policy.Evidence" />。</returns>
    Evidence Evidence { get; }
  }
}
