// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.OpCode
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Threading;

namespace System.Reflection.Emit
{
  /// <summary>描述中间语言 (IL) 指令。</summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public struct OpCode
  {
    internal const int OperandTypeMask = 31;
    internal const int FlowControlShift = 5;
    internal const int FlowControlMask = 15;
    internal const int OpCodeTypeShift = 9;
    internal const int OpCodeTypeMask = 7;
    internal const int StackBehaviourPopShift = 12;
    internal const int StackBehaviourPushShift = 17;
    internal const int StackBehaviourMask = 31;
    internal const int SizeShift = 22;
    internal const int SizeMask = 3;
    internal const int EndsUncondJmpBlkFlag = 16777216;
    internal const int StackChangeShift = 28;
    private string m_stringname;
    private StackBehaviour m_pop;
    private StackBehaviour m_push;
    private OperandType m_operand;
    private OpCodeType m_type;
    private int m_size;
    private byte m_s1;
    private byte m_s2;
    private FlowControl m_ctrl;
    private bool m_endsUncondJmpBlk;
    private int m_stackChange;
    private static volatile string[] g_nameCache;

    /// <summary>中间语言 (IL) 指令的操作数类型。</summary>
    /// <returns>只读。IL 指令的操作数类型。</returns>
    [__DynamicallyInvokable]
    public OperandType OperandType
    {
      [__DynamicallyInvokable] get
      {
        return this.m_operand;
      }
    }

    /// <summary>中间语言 (IL) 指令的流控制特性。</summary>
    /// <returns>只读。流控制的类型。</returns>
    [__DynamicallyInvokable]
    public FlowControl FlowControl
    {
      [__DynamicallyInvokable] get
      {
        return this.m_ctrl;
      }
    }

    /// <summary>中间语言 (IL) 指令的类型。</summary>
    /// <returns>只读。中间语言 (IL) 指令的类型。</returns>
    [__DynamicallyInvokable]
    public OpCodeType OpCodeType
    {
      [__DynamicallyInvokable] get
      {
        return this.m_type;
      }
    }

    /// <summary>中间语言 (IL) 指令弹出堆栈的方式。</summary>
    /// <returns>只读。IL 指令弹出堆栈的方式。</returns>
    [__DynamicallyInvokable]
    public StackBehaviour StackBehaviourPop
    {
      [__DynamicallyInvokable] get
      {
        return this.m_pop;
      }
    }

    /// <summary>中间语言 (IL) 指令将操作数推到堆栈上的方式。</summary>
    /// <returns>只读。IL 指令将操作数推到堆栈上的方式。</returns>
    [__DynamicallyInvokable]
    public StackBehaviour StackBehaviourPush
    {
      [__DynamicallyInvokable] get
      {
        return this.m_push;
      }
    }

    /// <summary>中间语言 (IL) 指令的大小。</summary>
    /// <returns>只读。IL 指令的大小。</returns>
    [__DynamicallyInvokable]
    public int Size
    {
      [__DynamicallyInvokable] get
      {
        return this.m_size;
      }
    }

    /// <summary>获取中间语言 (IL) 指令的数值。</summary>
    /// <returns>只读。IL 指令的数值。</returns>
    [__DynamicallyInvokable]
    public short Value
    {
      [__DynamicallyInvokable] get
      {
        if (this.m_size == 2)
          return (short) ((int) this.m_s1 << 8 | (int) this.m_s2);
        return (short) this.m_s2;
      }
    }

    /// <summary>中间语言 (IL) 指令的名称。</summary>
    /// <returns>只读。IL 指令的名称。</returns>
    [__DynamicallyInvokable]
    public string Name
    {
      [__DynamicallyInvokable] get
      {
        if (this.Size == 0)
          return (string) null;
        string[] strArray = OpCode.g_nameCache;
        if (strArray == null)
        {
          strArray = new string[287];
          OpCode.g_nameCache = strArray;
        }
        OpCodeValues opCodeValues = (OpCodeValues) (ushort) this.Value;
        int index = (int) opCodeValues;
        if (index > (int) byte.MaxValue)
        {
          if (index < 65024 || index > 65054)
            return (string) null;
          index = 256 + (index - 65024);
        }
        string str1 = Volatile.Read<string>(ref strArray[index]);
        if (str1 != null)
          return str1;
        string str2 = Enum.GetName(typeof (OpCodeValues), (object) opCodeValues).ToLowerInvariant().Replace("_", ".");
        Volatile.Write<string>(ref strArray[index], str2);
        return str2;
      }
    }

    internal OpCode(OpCodeValues value, int flags)
    {
      this.m_stringname = (string) null;
      this.m_pop = (StackBehaviour) (flags >> 12 & 31);
      this.m_push = (StackBehaviour) (flags >> 17 & 31);
      this.m_operand = (OperandType) (flags & 31);
      this.m_type = (OpCodeType) (flags >> 9 & 7);
      this.m_size = flags >> 22 & 3;
      this.m_s1 = (byte) ((uint) value >> 8);
      this.m_s2 = (byte) value;
      this.m_ctrl = (FlowControl) (flags >> 5 & 15);
      this.m_endsUncondJmpBlk = (uint) (flags & 16777216) > 0U;
      this.m_stackChange = flags >> 28;
    }

    /// <summary>指示两个 <see cref="T:System.Reflection.Emit.OpCode" /> 结构是否等同。</summary>
    /// <returns>如果 <paramref name="a" /> 等于 <paramref name="b" />，则为 true；否则为 false。</returns>
    /// <param name="a">要与 <paramref name="b" /> 进行比较的 <see cref="T:System.Reflection.Emit.OpCode" />。</param>
    /// <param name="b">要与 <paramref name="a" /> 进行比较的 <see cref="T:System.Reflection.Emit.OpCode" />。</param>
    [__DynamicallyInvokable]
    public static bool operator ==(OpCode a, OpCode b)
    {
      return a.Equals(b);
    }

    /// <summary>指示两个 <see cref="T:System.Reflection.Emit.OpCode" /> 结构是否不相等。</summary>
    /// <returns>如果 <paramref name="a" /> 不等于 <paramref name="b" />，则为 true；否则为 false。</returns>
    /// <param name="a">要与 <paramref name="b" /> 进行比较的 <see cref="T:System.Reflection.Emit.OpCode" />。</param>
    /// <param name="b">要与 <paramref name="a" /> 进行比较的 <see cref="T:System.Reflection.Emit.OpCode" />。</param>
    [__DynamicallyInvokable]
    public static bool operator !=(OpCode a, OpCode b)
    {
      return !(a == b);
    }

    internal bool EndsUncondJmpBlk()
    {
      return this.m_endsUncondJmpBlk;
    }

    internal int StackChange()
    {
      return this.m_stackChange;
    }

    /// <summary>测试给定对象是否等于此 Opcode。</summary>
    /// <returns>true if <paramref name="obj" /> is an instance of Opcode and is equal to this object; otherwise, false.</returns>
    /// <param name="obj">要与此对象比较的对象。 </param>
    [__DynamicallyInvokable]
    public override bool Equals(object obj)
    {
      if (obj is OpCode)
        return this.Equals((OpCode) obj);
      return false;
    }

    /// <summary>指示当前实例是否等于指定的 <see cref="T:System.Reflection.Emit.OpCode" />。</summary>
    /// <returns>如果 <paramref name="obj" /> 的值等于当前实例的值，则为 true；否则为 false。</returns>
    /// <param name="obj">要与当前实例进行比较的 <see cref="T:System.Reflection.Emit.OpCode" />。</param>
    [__DynamicallyInvokable]
    public bool Equals(OpCode obj)
    {
      return (int) obj.Value == (int) this.Value;
    }

    /// <summary>返回为此 Opcode 生成的哈希代码。</summary>
    /// <returns>返回此实例的哈希代码。</returns>
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      return (int) this.Value;
    }

    /// <summary>以 <see cref="T:System.String" /> 的形式返回此 Opcode。</summary>
    /// <returns>返回包含此 Opcode 的名称的 <see cref="T:System.String" />。</returns>
    [__DynamicallyInvokable]
    public override string ToString()
    {
      return this.Name;
    }
  }
}
