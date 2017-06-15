// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.DeriveBytes
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>表示抽象基类，导出指定长度字节序列的所有类都从该基类继承。</summary>
  [ComVisible(true)]
  public abstract class DeriveBytes : IDisposable
  {
    /// <summary>当在派生类中被重写时，返回伪随机密钥字节。</summary>
    /// <returns>由伪随机密钥字节组成的字节数组。</returns>
    /// <param name="cb">要生成的伪随机密钥字节数。</param>
    public abstract byte[] GetBytes(int cb);

    /// <summary>当在派生类中被重写时，重置操作的状态。</summary>
    public abstract void Reset();

    /// <summary>在派生类中重写时，释放由 <see cref="T:System.Security.Cryptography.DeriveBytes" /> 类的当前实例使用的所有资源。</summary>
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    /// <summary>在派生类中重写时，释放由 <see cref="T:System.Security.Cryptography.DeriveBytes" /> 类占用的非托管资源，还可以另外再释放托管资源。</summary>
    /// <param name="disposing">若要释放托管资源和非托管资源，则为 true；若仅释放非托管资源，则为 false。</param>
    protected virtual void Dispose(bool disposing)
    {
    }
  }
}
