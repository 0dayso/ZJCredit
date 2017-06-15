// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.SignatureToken
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
  /// <summary>表示由元数据返回以表示签名的 Token。</summary>
  [ComVisible(true)]
  public struct SignatureToken
  {
    /// <summary>
    /// <see cref="P:System.Reflection.Emit.SignatureToken.Token" /> 值为 0 的默认 SignatureToken。</summary>
    public static readonly SignatureToken Empty;
    internal int m_signature;
    internal ModuleBuilder m_moduleBuilder;

    /// <summary>检索此方法的局部变量签名的元数据标记。</summary>
    /// <returns>只读。检索该签名的元数据标记。</returns>
    public int Token
    {
      get
      {
        return this.m_signature;
      }
    }

    internal SignatureToken(int str, ModuleBuilder mod)
    {
      this.m_signature = str;
      this.m_moduleBuilder = mod;
    }

    /// <summary>指示两个 <see cref="T:System.Reflection.Emit.SignatureToken" /> 结构是否等同。</summary>
    /// <returns>如果 <paramref name="a" /> 等于 <paramref name="b" />，则为 true；否则为 false。</returns>
    /// <param name="a">要与 <paramref name="b" /> 进行比较的 <see cref="T:System.Reflection.Emit.SignatureToken" />。</param>
    /// <param name="b">要与 <paramref name="a" /> 进行比较的 <see cref="T:System.Reflection.Emit.SignatureToken" />。</param>
    public static bool operator ==(SignatureToken a, SignatureToken b)
    {
      return a.Equals(b);
    }

    /// <summary>指示两个 <see cref="T:System.Reflection.Emit.SignatureToken" /> 结构是否不相等。</summary>
    /// <returns>如果 <paramref name="a" /> 不等于 <paramref name="b" />，则为 true；否则为 false。</returns>
    /// <param name="a">要与 <paramref name="b" /> 进行比较的 <see cref="T:System.Reflection.Emit.SignatureToken" />。</param>
    /// <param name="b">要与 <paramref name="a" /> 进行比较的 <see cref="T:System.Reflection.Emit.SignatureToken" />。</param>
    public static bool operator !=(SignatureToken a, SignatureToken b)
    {
      return !(a == b);
    }

    /// <summary>生成该签名的哈希代码。</summary>
    /// <returns>返回该签名的哈希代码。</returns>
    public override int GetHashCode()
    {
      return this.m_signature;
    }

    /// <summary>检查给定对象是否为 SignatureToken 的实例和是否等于此实例。</summary>
    /// <returns>如果 <paramref name="obj" /> 为 SignatureToken 的实例并且等于此对象，则为 true；否则为 false。</returns>
    /// <param name="obj">与此 SignatureToken 进行比较的对象。</param>
    public override bool Equals(object obj)
    {
      if (obj is SignatureToken)
        return this.Equals((SignatureToken) obj);
      return false;
    }

    /// <summary>指示当前实例是否等于指定的 <see cref="T:System.Reflection.Emit.SignatureToken" />。</summary>
    /// <returns>如果 <paramref name="obj" /> 的值等于当前实例的值，则为 true；否则为 false。</returns>
    /// <param name="obj">要与当前实例进行比较的 <see cref="T:System.Reflection.Emit.SignatureToken" />。</param>
    public bool Equals(SignatureToken obj)
    {
      return obj.m_signature == this.m_signature;
    }
  }
}
