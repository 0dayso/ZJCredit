// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.MemberHolder
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;

namespace System.Runtime.Serialization
{
  [Serializable]
  internal class MemberHolder
  {
    internal MemberInfo[] members;
    internal Type memberType;
    internal StreamingContext context;

    internal MemberHolder(Type type, StreamingContext ctx)
    {
      this.memberType = type;
      this.context = ctx;
    }

    public override int GetHashCode()
    {
      return this.memberType.GetHashCode();
    }

    public override bool Equals(object obj)
    {
      if (!(obj is MemberHolder))
        return false;
      MemberHolder memberHolder = (MemberHolder) obj;
      return memberHolder.memberType == this.memberType && memberHolder.context.State == this.context.State;
    }
  }
}
