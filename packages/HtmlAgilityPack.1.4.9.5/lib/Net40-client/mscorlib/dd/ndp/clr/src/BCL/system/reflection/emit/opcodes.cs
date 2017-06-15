// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.OpCodes
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
  /// <summary>通过 <see cref="T:System.Reflection.Emit.ILGenerator" /> 类成员（例如 <see cref="M:System.Reflection.Emit.ILGenerator.Emit(System.Reflection.Emit.OpCode)" />）为发出提供 Microsoft 中间语言 (MSIL) 指令的字段表示形式。</summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public class OpCodes
  {
    /// <summary>如果修补操作码，则填充空间。尽管可能消耗处理周期，但未执行任何有意义的操作。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Nop = new OpCode(OpCodeValues.Nop, 6556325);
    /// <summary>向公共语言结构 (CLI) 发出信号以通知调试器已撞上了一个断点。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Break = new OpCode(OpCodeValues.Break, 6556197);
    /// <summary>将索引为 0 的参数加载到计算堆栈上。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldarg_0 = new OpCode(OpCodeValues.Ldarg_0, 275120805);
    /// <summary>将索引为 1 的参数加载到计算堆栈上。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldarg_1 = new OpCode(OpCodeValues.Ldarg_1, 275120805);
    /// <summary>将索引为 2 的参数加载到计算堆栈上。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldarg_2 = new OpCode(OpCodeValues.Ldarg_2, 275120805);
    /// <summary>将索引为 3 的参数加载到计算堆栈上。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldarg_3 = new OpCode(OpCodeValues.Ldarg_3, 275120805);
    /// <summary>将索引 0 处的局部变量加载到计算堆栈上。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldloc_0 = new OpCode(OpCodeValues.Ldloc_0, 275120805);
    /// <summary>将索引 1 处的局部变量加载到计算堆栈上。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldloc_1 = new OpCode(OpCodeValues.Ldloc_1, 275120805);
    /// <summary>将索引 2 处的局部变量加载到计算堆栈上。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldloc_2 = new OpCode(OpCodeValues.Ldloc_2, 275120805);
    /// <summary>将索引 3 处的局部变量加载到计算堆栈上。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldloc_3 = new OpCode(OpCodeValues.Ldloc_3, 275120805);
    /// <summary>从计算堆栈的顶部弹出当前值并将其存储到索引 0 处的局部变量列表中。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Stloc_0 = new OpCode(OpCodeValues.Stloc_0, -261877083);
    /// <summary>从计算堆栈的顶部弹出当前值并将其存储到索引 1 处的局部变量列表中。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Stloc_1 = new OpCode(OpCodeValues.Stloc_1, -261877083);
    /// <summary>从计算堆栈的顶部弹出当前值并将其存储到索引 2 处的局部变量列表中。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Stloc_2 = new OpCode(OpCodeValues.Stloc_2, -261877083);
    /// <summary>从计算堆栈的顶部弹出当前值并将其存储到索引 3 处的局部变量列表中。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Stloc_3 = new OpCode(OpCodeValues.Stloc_3, -261877083);
    /// <summary>将参数（由指定的短格式索引引用）加载到计算堆栈上。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldarg_S = new OpCode(OpCodeValues.Ldarg_S, 275120818);
    /// <summary>以短格式将参数地址加载到计算堆栈上。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldarga_S = new OpCode(OpCodeValues.Ldarga_S, 275382962);
    /// <summary>将位于计算堆栈顶部的值存储在参数槽中的指定索引处（短格式）。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Starg_S = new OpCode(OpCodeValues.Starg_S, -261877070);
    /// <summary>将特定索引处的局部变量加载到计算堆栈上（短格式）。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldloc_S = new OpCode(OpCodeValues.Ldloc_S, 275120818);
    /// <summary>将位于特定索引处的局部变量的地址加载到计算堆栈上（短格式）。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldloca_S = new OpCode(OpCodeValues.Ldloca_S, 275382962);
    /// <summary>从计算堆栈的顶部弹出当前值并将其存储在局部变量列表中的 <paramref name="index" /> 处（短格式）。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Stloc_S = new OpCode(OpCodeValues.Stloc_S, -261877070);
    /// <summary>将空引用（O 类型）推送到计算堆栈上。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldnull = new OpCode(OpCodeValues.Ldnull, 275909285);
    /// <summary>将整数值 -1 作为 int32 推送到计算堆栈上。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldc_I4_M1 = new OpCode(OpCodeValues.Ldc_I4_M1, 275382949);
    /// <summary>将整数值 0 作为 int32 推送到计算堆栈上。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldc_I4_0 = new OpCode(OpCodeValues.Ldc_I4_0, 275382949);
    /// <summary>将整数值 1 作为 int32 推送到计算堆栈上。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldc_I4_1 = new OpCode(OpCodeValues.Ldc_I4_1, 275382949);
    /// <summary>将整数值 2 作为 int32 推送到计算堆栈上。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldc_I4_2 = new OpCode(OpCodeValues.Ldc_I4_2, 275382949);
    /// <summary>将整数值 3 作为 int32 推送到计算堆栈上。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldc_I4_3 = new OpCode(OpCodeValues.Ldc_I4_3, 275382949);
    /// <summary>将整数值 4 作为 int32 推送到计算堆栈上。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldc_I4_4 = new OpCode(OpCodeValues.Ldc_I4_4, 275382949);
    /// <summary>将整数值 5 作为 int32 推送到计算堆栈上。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldc_I4_5 = new OpCode(OpCodeValues.Ldc_I4_5, 275382949);
    /// <summary>将整数值 6 作为 int32 推送到计算堆栈上。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldc_I4_6 = new OpCode(OpCodeValues.Ldc_I4_6, 275382949);
    /// <summary>将整数值 7 作为 int32 推送到计算堆栈上。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldc_I4_7 = new OpCode(OpCodeValues.Ldc_I4_7, 275382949);
    /// <summary>将整数值 8 作为 int32 推送到计算堆栈上。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldc_I4_8 = new OpCode(OpCodeValues.Ldc_I4_8, 275382949);
    /// <summary>将提供的 int8 值作为 int32 推送到计算堆栈上（短格式）。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldc_I4_S = new OpCode(OpCodeValues.Ldc_I4_S, 275382960);
    /// <summary>将所提供的 int32 类型的值作为 int32 推送到计算堆栈上。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldc_I4 = new OpCode(OpCodeValues.Ldc_I4, 275384994);
    /// <summary>将所提供的 int64 类型的值作为 int64 推送到计算堆栈上。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldc_I8 = new OpCode(OpCodeValues.Ldc_I8, 275516067);
    /// <summary>将所提供的 float32 类型的值作为 F (float) 类型推送到计算堆栈上。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldc_R4 = new OpCode(OpCodeValues.Ldc_R4, 275647153);
    /// <summary>将所提供的 float64 类型的值作为 F (float) 类型推送到计算堆栈上。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldc_R8 = new OpCode(OpCodeValues.Ldc_R8, 275778215);
    /// <summary>复制计算堆栈上当前最顶端的值，然后将副本推送到计算堆栈上。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Dup = new OpCode(OpCodeValues.Dup, 275258021);
    /// <summary>移除当前位于计算堆栈顶部的值。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Pop = new OpCode(OpCodeValues.Pop, -261875035);
    /// <summary>退出当前方法并跳至指定方法。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Jmp = new OpCode(OpCodeValues.Jmp, 23333444);
    /// <summary>调用由传递的方法说明符指示的方法。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Call = new OpCode(OpCodeValues.Call, 7842372);
    /// <summary>通过调用约定描述的参数调用在计算堆栈上指示的方法（作为指向入口点的指针）。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Calli = new OpCode(OpCodeValues.Calli, 7842377);
    /// <summary>从当前方法返回，并将返回值（如果存在）从被调用方的计算堆栈推送到调用方的计算堆栈上。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ret = new OpCode(OpCodeValues.Ret, 23440101);
    /// <summary>无条件地将控制转移到目标指令（短格式）。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Br_S = new OpCode(OpCodeValues.Br_S, 23331343);
    /// <summary>如果 <paramref name="value" /> 为 false、空引用或零，则将控制转移到目标指令。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Brfalse_S = new OpCode(OpCodeValues.Brfalse_S, -261868945);
    /// <summary>如果 <paramref name="value" /> 为 true、非空或非零，则将控制转移到目标指令（短格式）。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Brtrue_S = new OpCode(OpCodeValues.Brtrue_S, -261868945);
    /// <summary>如果两个值相等，则将控制转移到目标指令（短格式）。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Beq_S = new OpCode(OpCodeValues.Beq_S, -530308497);
    /// <summary>如果第一个值大于或等于第二个值，则将控制转移到目标指令（短格式）。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Bge_S = new OpCode(OpCodeValues.Bge_S, -530308497);
    /// <summary>如果第一个值大于第二个值，则将控制转移到目标指令（短格式）。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Bgt_S = new OpCode(OpCodeValues.Bgt_S, -530308497);
    /// <summary>如果第一个值小于或等于第二个值，则将控制转移到目标指令（短格式）。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ble_S = new OpCode(OpCodeValues.Ble_S, -530308497);
    /// <summary>如果第一个值小于第二个值，则将控制转移到目标指令（短格式）。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Blt_S = new OpCode(OpCodeValues.Blt_S, -530308497);
    /// <summary>当两个无符号整数值或未经排序的浮点值不相等时，将控制转移到目标指令（短格式）。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Bne_Un_S = new OpCode(OpCodeValues.Bne_Un_S, -530308497);
    /// <summary>当比较无符号整数值或未经排序的浮点值时，如果第一个值大于第二个值，则将控制转移到目标指令（短格式）。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Bge_Un_S = new OpCode(OpCodeValues.Bge_Un_S, -530308497);
    /// <summary>当比较无符号整数值或未经排序的浮点值时，如果第一个值大于第二个值，则将控制转移到目标指令（短格式）。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Bgt_Un_S = new OpCode(OpCodeValues.Bgt_Un_S, -530308497);
    /// <summary>当比较无符号整数值或未经排序的浮点值时，如果第一个值小于或等于第二个值，则将控制转移到目标指令（短格式）。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ble_Un_S = new OpCode(OpCodeValues.Ble_Un_S, -530308497);
    /// <summary>当比较无符号整数值或未经排序的浮点值时，如果第一个值小于第二个值，则将控制转移到目标指令（短格式）。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Blt_Un_S = new OpCode(OpCodeValues.Blt_Un_S, -530308497);
    /// <summary>无条件地将控制转移到目标指令。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Br = new OpCode(OpCodeValues.Br, 23333376);
    /// <summary>如果 <paramref name="value" /> 为 false、空引用（Visual Basic 中的 Nothing）或零，则将控制转移到目标指令。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Brfalse = new OpCode(OpCodeValues.Brfalse, -261866912);
    /// <summary>如果 <paramref name="value" /> 为 true、非空或非零，则将控制转移到目标指令。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Brtrue = new OpCode(OpCodeValues.Brtrue, -261866912);
    /// <summary>如果两个值相等，则将控制转移到目标指令。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Beq = new OpCode(OpCodeValues.Beq, -530308512);
    /// <summary>如果第一个值大于或等于第二个值，则将控制转移到目标指令。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Bge = new OpCode(OpCodeValues.Bge, -530308512);
    /// <summary>如果第一个值大于第二个值，则将控制转移到目标指令。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Bgt = new OpCode(OpCodeValues.Bgt, -530308512);
    /// <summary>如果第一个值小于或等于第二个值，则将控制转移到目标指令。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ble = new OpCode(OpCodeValues.Ble, -530308512);
    /// <summary>如果第一个值小于第二个值，则将控制转移到目标指令。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Blt = new OpCode(OpCodeValues.Blt, -530308512);
    /// <summary>当两个无符号整数值或未经排序的浮点值不相等时，将控制转移到目标指令。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Bne_Un = new OpCode(OpCodeValues.Bne_Un, -530308512);
    /// <summary>当比较无符号整数值或未经排序的浮点值时，如果第一个值大于第二个值，则将控制转移到目标指令。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Bge_Un = new OpCode(OpCodeValues.Bge_Un, -530308512);
    /// <summary>当比较无符号整数值或未经排序的浮点值时，如果第一个值大于第二个值，则将控制转移到目标指令。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Bgt_Un = new OpCode(OpCodeValues.Bgt_Un, -530308512);
    /// <summary>当比较无符号整数值或未经排序的浮点值时，如果第一个值小于或等于第二个值，则将控制转移到目标指令。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ble_Un = new OpCode(OpCodeValues.Ble_Un, -530308512);
    /// <summary>当比较无符号整数值或未经排序的浮点值时，如果第一个值小于第二个值，则将控制转移到目标指令。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Blt_Un = new OpCode(OpCodeValues.Blt_Un, -530308512);
    /// <summary>实现跳转表。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Switch = new OpCode(OpCodeValues.Switch, -261866901);
    /// <summary>将 int8 类型的值作为 int32 间接加载到计算堆栈上。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldind_I1 = new OpCode(OpCodeValues.Ldind_I1, 6961829);
    /// <summary>将 unsigned int8 类型的值作为 int32 间接加载到计算堆栈上。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldind_U1 = new OpCode(OpCodeValues.Ldind_U1, 6961829);
    /// <summary>将 int16 类型的值作为 int32 间接加载到计算堆栈上。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldind_I2 = new OpCode(OpCodeValues.Ldind_I2, 6961829);
    /// <summary>将 unsigned int16 类型的值作为 int32 间接加载到计算堆栈上。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldind_U2 = new OpCode(OpCodeValues.Ldind_U2, 6961829);
    /// <summary>将 int32 类型的值作为 int32 间接加载到计算堆栈上。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldind_I4 = new OpCode(OpCodeValues.Ldind_I4, 6961829);
    /// <summary>将 unsigned int32 类型的值作为 int32 间接加载到计算堆栈上。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldind_U4 = new OpCode(OpCodeValues.Ldind_U4, 6961829);
    /// <summary>将 int64 类型的值作为 int64 间接加载到计算堆栈上。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldind_I8 = new OpCode(OpCodeValues.Ldind_I8, 7092901);
    /// <summary>将 native int 类型的值作为 native int 间接加载到计算堆栈上。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldind_I = new OpCode(OpCodeValues.Ldind_I, 6961829);
    /// <summary>将 float32 类型的值作为 F (float) 类型间接加载到计算堆栈上。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldind_R4 = new OpCode(OpCodeValues.Ldind_R4, 7223973);
    /// <summary>将 float64 类型的值作为 F (float) 类型间接加载到计算堆栈上。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldind_R8 = new OpCode(OpCodeValues.Ldind_R8, 7355045);
    /// <summary>将对象引用作为 O（对象引用）类型间接加载到计算堆栈上。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldind_Ref = new OpCode(OpCodeValues.Ldind_Ref, 7486117);
    /// <summary>存储所提供地址处的对象引用值。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Stind_Ref = new OpCode(OpCodeValues.Stind_Ref, -530294107);
    /// <summary>在所提供的地址存储 int8 类型的值。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Stind_I1 = new OpCode(OpCodeValues.Stind_I1, -530294107);
    /// <summary>在所提供的地址存储 int16 类型的值。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Stind_I2 = new OpCode(OpCodeValues.Stind_I2, -530294107);
    /// <summary>在所提供的地址存储 int32 类型的值。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Stind_I4 = new OpCode(OpCodeValues.Stind_I4, -530294107);
    /// <summary>在所提供的地址存储 int64 类型的值。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Stind_I8 = new OpCode(OpCodeValues.Stind_I8, -530290011);
    /// <summary>在所提供的地址存储 float32 类型的值。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Stind_R4 = new OpCode(OpCodeValues.Stind_R4, -530281819);
    /// <summary>在所提供的地址存储 float64 类型的值。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Stind_R8 = new OpCode(OpCodeValues.Stind_R8, -530277723);
    /// <summary>将两个值相加并将结果推送到计算堆栈上。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Add = new OpCode(OpCodeValues.Add, -261739867);
    /// <summary>从其他值中减去一个值并将结果推送到计算堆栈上。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Sub = new OpCode(OpCodeValues.Sub, -261739867);
    /// <summary>将两个值相乘并将结果推送到计算堆栈上。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Mul = new OpCode(OpCodeValues.Mul, -261739867);
    /// <summary>将两个值相除并将结果作为浮点（F 类型）或商（int32 类型）推送到计算堆栈上。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Div = new OpCode(OpCodeValues.Div, -261739867);
    /// <summary>两个无符号整数值相除并将结果 (int32) 推送到计算堆栈上。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Div_Un = new OpCode(OpCodeValues.Div_Un, -261739867);
    /// <summary>将两个值相除并将余数推送到计算堆栈上。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Rem = new OpCode(OpCodeValues.Rem, -261739867);
    /// <summary>将两个无符号值相除并将余数推送到计算堆栈上。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Rem_Un = new OpCode(OpCodeValues.Rem_Un, -261739867);
    /// <summary>计算两个值的按位“与”并将结果推送到计算堆栈上。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode And = new OpCode(OpCodeValues.And, -261739867);
    /// <summary>计算位于堆栈顶部的两个整数值的按位求补并将结果推送到计算堆栈上。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Or = new OpCode(OpCodeValues.Or, -261739867);
    /// <summary>计算位于计算堆栈顶部的两个值的按位异或，并且将结果推送到计算堆栈上。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Xor = new OpCode(OpCodeValues.Xor, -261739867);
    /// <summary>将整数值左移（用零填充）指定的位数，并将结果推送到计算堆栈上。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Shl = new OpCode(OpCodeValues.Shl, -261739867);
    /// <summary>将整数值右移（保留符号）指定的位数，并将结果推送到计算堆栈上。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Shr = new OpCode(OpCodeValues.Shr, -261739867);
    /// <summary>将无符号整数值右移（用零填充）指定的位数，并将结果推送到计算堆栈上。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Shr_Un = new OpCode(OpCodeValues.Shr_Un, -261739867);
    /// <summary>对一个值执行求反并将结果推送到计算堆栈上。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Neg = new OpCode(OpCodeValues.Neg, 6691493);
    /// <summary>计算堆栈顶部整数值的按位求补并将结果作为相同的类型推送到计算堆栈上。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Not = new OpCode(OpCodeValues.Not, 6691493);
    /// <summary>将位于计算堆栈顶部的值转换为 int8，然后将其扩展（填充）为 int32。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Conv_I1 = new OpCode(OpCodeValues.Conv_I1, 6953637);
    /// <summary>将位于计算堆栈顶部的值转换为 int16，然后将其扩展（填充）为 int32。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Conv_I2 = new OpCode(OpCodeValues.Conv_I2, 6953637);
    /// <summary>将位于计算堆栈顶部的值转换为 int32。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Conv_I4 = new OpCode(OpCodeValues.Conv_I4, 6953637);
    /// <summary>将位于计算堆栈顶部的值转换为 int64。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Conv_I8 = new OpCode(OpCodeValues.Conv_I8, 7084709);
    /// <summary>将位于计算堆栈顶部的值转换为 float32。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Conv_R4 = new OpCode(OpCodeValues.Conv_R4, 7215781);
    /// <summary>将位于计算堆栈顶部的值转换为 float64。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Conv_R8 = new OpCode(OpCodeValues.Conv_R8, 7346853);
    /// <summary>将位于计算堆栈顶部的值转换为 unsigned int32，然后将其扩展为 int32。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Conv_U4 = new OpCode(OpCodeValues.Conv_U4, 6953637);
    /// <summary>将位于计算堆栈顶部的值转换为 unsigned int64，然后将其扩展为 int64。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Conv_U8 = new OpCode(OpCodeValues.Conv_U8, 7084709);
    /// <summary>对对象调用后期绑定方法，并且将返回值推送到计算堆栈上。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Callvirt = new OpCode(OpCodeValues.Callvirt, 7841348);
    /// <summary>将位于对象（&amp;、* 或 native int 类型）地址的值类型复制到目标对象（&amp;、* 或 native int 类型）的地址。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Cpobj = new OpCode(OpCodeValues.Cpobj, -530295123);
    /// <summary>将地址指向的值类型对象复制到计算堆栈的顶部。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldobj = new OpCode(OpCodeValues.Ldobj, 6698669);
    /// <summary>推送对元数据中存储的字符串的新对象引用。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldstr = new OpCode(OpCodeValues.Ldstr, 275908266);
    /// <summary>创建一个值类型的新对象或新实例，并将对象引用（O 类型）推送到计算堆栈上。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Newobj = new OpCode(OpCodeValues.Newobj, 276014660);
    /// <summary>尝试将引用传递的对象转换为指定的类。</summary>
    [ComVisible(true)]
    [__DynamicallyInvokable]
    public static readonly OpCode Castclass = new OpCode(OpCodeValues.Castclass, 7513773);
    /// <summary>测试对象引用（O 类型）是否为特定类的实例。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Isinst = new OpCode(OpCodeValues.Isinst, 6989485);
    /// <summary>将位于计算堆栈顶部的无符号整数值转换为 float32。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Conv_R_Un = new OpCode(OpCodeValues.Conv_R_Un, 7346853);
    /// <summary>将值类型的已装箱的表示形式转换为其未装箱的形式。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Unbox = new OpCode(OpCodeValues.Unbox, 6990509);
    /// <summary>引发当前位于计算堆栈上的异常对象。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Throw = new OpCode(OpCodeValues.Throw, -245061883);
    /// <summary>查找对象中其引用当前位于计算堆栈的字段的值。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldfld = new OpCode(OpCodeValues.Ldfld, 6727329);
    /// <summary>查找对象中其引用当前位于计算堆栈的字段的地址。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldflda = new OpCode(OpCodeValues.Ldflda, 6989473);
    /// <summary>用新值替换在对象引用或指针的字段中存储的值。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Stfld = new OpCode(OpCodeValues.Stfld, -530270559);
    /// <summary>将静态字段的值推送到计算堆栈上。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldsfld = new OpCode(OpCodeValues.Ldsfld, 275121825);
    /// <summary>将静态字段的地址推送到计算堆栈上。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldsflda = new OpCode(OpCodeValues.Ldsflda, 275383969);
    /// <summary>用来自计算堆栈的值替换静态字段的值。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Stsfld = new OpCode(OpCodeValues.Stsfld, -261876063);
    /// <summary>将指定类型的值从计算堆栈复制到所提供的内存地址中。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Stobj = new OpCode(OpCodeValues.Stobj, -530298195);
    /// <summary>将位于计算堆栈顶部的无符号值转换为有符号 int8 并将其扩展为 int32，并在溢出时引发 <see cref="T:System.OverflowException" />。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Conv_Ovf_I1_Un = new OpCode(OpCodeValues.Conv_Ovf_I1_Un, 6953637);
    /// <summary>将位于计算堆栈顶部的无符号值转换为有符号 int16 并将其扩展为 int32，并在溢出时引发 <see cref="T:System.OverflowException" />。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Conv_Ovf_I2_Un = new OpCode(OpCodeValues.Conv_Ovf_I2_Un, 6953637);
    /// <summary>将位于计算堆栈顶部的无符号值转换为有符号 int32，并在溢出时引发 <see cref="T:System.OverflowException" />。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Conv_Ovf_I4_Un = new OpCode(OpCodeValues.Conv_Ovf_I4_Un, 6953637);
    /// <summary>将位于计算堆栈顶部的无符号值转换为有符号 int64，并在溢出时引发 <see cref="T:System.OverflowException" />。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Conv_Ovf_I8_Un = new OpCode(OpCodeValues.Conv_Ovf_I8_Un, 7084709);
    /// <summary>将位于计算堆栈顶部的无符号值转换为 unsigned int8 并将其扩展为 int32，并在溢出时引发 <see cref="T:System.OverflowException" />。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Conv_Ovf_U1_Un = new OpCode(OpCodeValues.Conv_Ovf_U1_Un, 6953637);
    /// <summary>将位于计算堆栈顶部的无符号值转换为 unsigned int16 并将其扩展为 int32，并在溢出时引发 <see cref="T:System.OverflowException" />。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Conv_Ovf_U2_Un = new OpCode(OpCodeValues.Conv_Ovf_U2_Un, 6953637);
    /// <summary>将位于计算堆栈顶部的无符号值转换为 unsigned int32，并在溢出时引发 <see cref="T:System.OverflowException" />。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Conv_Ovf_U4_Un = new OpCode(OpCodeValues.Conv_Ovf_U4_Un, 6953637);
    /// <summary>将位于计算堆栈顶部的无符号值转换为 unsigned int64，并在溢出时引发 <see cref="T:System.OverflowException" />。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Conv_Ovf_U8_Un = new OpCode(OpCodeValues.Conv_Ovf_U8_Un, 7084709);
    /// <summary>将位于计算堆栈顶部的无符号值转换为有符号 native int，并在溢出时引发 <see cref="T:System.OverflowException" />。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Conv_Ovf_I_Un = new OpCode(OpCodeValues.Conv_Ovf_I_Un, 6953637);
    /// <summary>将位于计算堆栈顶部的无符号值转换为 unsigned native int，并在溢出时引发 <see cref="T:System.OverflowException" />。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Conv_Ovf_U_Un = new OpCode(OpCodeValues.Conv_Ovf_U_Un, 6953637);
    /// <summary>将值类转换为对象引用（O 类型）。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Box = new OpCode(OpCodeValues.Box, 7477933);
    /// <summary>将对新的从零开始的一维数组（其元素属于特定类型）的对象引用推送到计算堆栈上。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Newarr = new OpCode(OpCodeValues.Newarr, 7485101);
    /// <summary>将从零开始的、一维数组的元素的数目推送到计算堆栈上。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldlen = new OpCode(OpCodeValues.Ldlen, 6989477);
    /// <summary>将位于指定数组索引的数组元素的地址作为 &amp; 类型（托管指针）加载到计算堆栈的顶部。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldelema = new OpCode(OpCodeValues.Ldelema, -261437779);
    /// <summary>将位于指定数组索引处的 int8 类型的元素作为 int32 加载到计算堆栈的顶部。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldelem_I1 = new OpCode(OpCodeValues.Ldelem_I1, -261437787);
    /// <summary>将位于指定数组索引处的 unsigned int8 类型的元素作为 int32 加载到计算堆栈的顶部。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldelem_U1 = new OpCode(OpCodeValues.Ldelem_U1, -261437787);
    /// <summary>将位于指定数组索引处的 int16 类型的元素作为 int32 加载到计算堆栈的顶部。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldelem_I2 = new OpCode(OpCodeValues.Ldelem_I2, -261437787);
    /// <summary>将位于指定数组索引处的 unsigned int16 类型的元素作为 int32 加载到计算堆栈的顶部。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldelem_U2 = new OpCode(OpCodeValues.Ldelem_U2, -261437787);
    /// <summary>将位于指定数组索引处的 int32 类型的元素作为 int32 加载到计算堆栈的顶部。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldelem_I4 = new OpCode(OpCodeValues.Ldelem_I4, -261437787);
    /// <summary>将位于指定数组索引处的 unsigned int32 类型的元素作为 int32 加载到计算堆栈的顶部。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldelem_U4 = new OpCode(OpCodeValues.Ldelem_U4, -261437787);
    /// <summary>将位于指定数组索引处的 int64 类型的元素作为 int64 加载到计算堆栈的顶部。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldelem_I8 = new OpCode(OpCodeValues.Ldelem_I8, -261306715);
    /// <summary>将位于指定数组索引处的 native int 类型的元素作为 native int 加载到计算堆栈的顶部。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldelem_I = new OpCode(OpCodeValues.Ldelem_I, -261437787);
    /// <summary>将位于指定数组索引处的 float32 类型的元素作为 F 类型（浮点型）加载到计算堆栈的顶部。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldelem_R4 = new OpCode(OpCodeValues.Ldelem_R4, -261175643);
    /// <summary>将位于指定数组索引处的 float64 类型的元素作为 F 类型（浮点型）加载到计算堆栈的顶部。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldelem_R8 = new OpCode(OpCodeValues.Ldelem_R8, -261044571);
    /// <summary>将位于指定数组索引处的包含对象引用的元素作为 O 类型（对象引用）加载到计算堆栈的顶部。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldelem_Ref = new OpCode(OpCodeValues.Ldelem_Ref, -260913499);
    /// <summary>用计算堆栈上的 native int 值替换给定索引处的数组元素。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Stelem_I = new OpCode(OpCodeValues.Stelem_I, -798697819);
    /// <summary>用计算堆栈上的 int8 值替换给定索引处的数组元素。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Stelem_I1 = new OpCode(OpCodeValues.Stelem_I1, -798697819);
    /// <summary>用计算堆栈上的 int16 值替换给定索引处的数组元素。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Stelem_I2 = new OpCode(OpCodeValues.Stelem_I2, -798697819);
    /// <summary>用计算堆栈上的 int32 值替换给定索引处的数组元素。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Stelem_I4 = new OpCode(OpCodeValues.Stelem_I4, -798697819);
    /// <summary>用计算堆栈上的 int64 值替换给定索引处的数组元素。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Stelem_I8 = new OpCode(OpCodeValues.Stelem_I8, -798693723);
    /// <summary>用计算堆栈上的 float32 值替换给定索引处的数组元素。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Stelem_R4 = new OpCode(OpCodeValues.Stelem_R4, -798689627);
    /// <summary>用计算堆栈上的 float64 值替换给定索引处的数组元素。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Stelem_R8 = new OpCode(OpCodeValues.Stelem_R8, -798685531);
    /// <summary>用计算堆栈上的对象 ref 值（O 类型）替换给定索引处的数组元素。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Stelem_Ref = new OpCode(OpCodeValues.Stelem_Ref, -798681435);
    /// <summary>按照指令中指定的类型，将指定数组索引中的元素加载到计算堆栈的顶部。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldelem = new OpCode(OpCodeValues.Ldelem, -261699923);
    /// <summary>用计算堆栈中的值替换给定索引处的数组元素，其类型在指令中指定。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Stelem = new OpCode(OpCodeValues.Stelem, 6669997);
    /// <summary>将指令中指定类型的已装箱的表示形式转换成未装箱形式。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Unbox_Any = new OpCode(OpCodeValues.Unbox_Any, 6727341);
    /// <summary>将位于计算堆栈顶部的有符号值转换为有符号的 int8 并将其扩展为 int32，并在溢出时引发 <see cref="T:System.OverflowException" />。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Conv_Ovf_I1 = new OpCode(OpCodeValues.Conv_Ovf_I1, 6953637);
    /// <summary>将位于计算堆栈顶部的有符号值转换为 unsigned int8 并将其扩展为 int32，并在溢出时引发 <see cref="T:System.OverflowException" />。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Conv_Ovf_U1 = new OpCode(OpCodeValues.Conv_Ovf_U1, 6953637);
    /// <summary>将位于计算堆栈顶部的有符号值转换为有符号的 int16 并将其扩展为 int32，并在溢出时引发 <see cref="T:System.OverflowException" />。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Conv_Ovf_I2 = new OpCode(OpCodeValues.Conv_Ovf_I2, 6953637);
    /// <summary>将位于计算堆栈顶部的有符号值转换为 unsigned int16 并将其扩展为 int32，并在溢出时引发 <see cref="T:System.OverflowException" />。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Conv_Ovf_U2 = new OpCode(OpCodeValues.Conv_Ovf_U2, 6953637);
    /// <summary>将位于计算堆栈顶部的有符号值转换为有符号 int32，并在溢出时引发 <see cref="T:System.OverflowException" />。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Conv_Ovf_I4 = new OpCode(OpCodeValues.Conv_Ovf_I4, 6953637);
    /// <summary>将位于计算堆栈顶部的有符号值转换为 unsigned int32，并在溢出时引发 <see cref="T:System.OverflowException" />。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Conv_Ovf_U4 = new OpCode(OpCodeValues.Conv_Ovf_U4, 6953637);
    /// <summary>将位于计算堆栈顶部的有符号值转换为有符号 int64，并在溢出时引发 <see cref="T:System.OverflowException" />。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Conv_Ovf_I8 = new OpCode(OpCodeValues.Conv_Ovf_I8, 7084709);
    /// <summary>将位于计算堆栈顶部的有符号值转换为 unsigned int64，并在溢出时引发 <see cref="T:System.OverflowException" />。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Conv_Ovf_U8 = new OpCode(OpCodeValues.Conv_Ovf_U8, 7084709);
    /// <summary>检索嵌入在类型化引用内的地址（&amp; 类型）。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Refanyval = new OpCode(OpCodeValues.Refanyval, 6953645);
    /// <summary>如果值不是有限数，则引发 <see cref="T:System.ArithmeticException" />。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ckfinite = new OpCode(OpCodeValues.Ckfinite, 7346853);
    /// <summary>将对特定类型实例的类型化引用推送到计算堆栈上。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Mkrefany = new OpCode(OpCodeValues.Mkrefany, 6699693);
    /// <summary>将元数据标记转换为其运行时表示形式，并将其推送到计算堆栈上。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldtoken = new OpCode(OpCodeValues.Ldtoken, 275385004);
    /// <summary>将位于计算堆栈顶部的值转换为 unsigned int16，然后将其扩展为 int32。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Conv_U2 = new OpCode(OpCodeValues.Conv_U2, 6953637);
    /// <summary>将位于计算堆栈顶部的值转换为 unsigned int8，然后将其扩展为 int32。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Conv_U1 = new OpCode(OpCodeValues.Conv_U1, 6953637);
    /// <summary>将位于计算堆栈顶部的值转换为 native int。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Conv_I = new OpCode(OpCodeValues.Conv_I, 6953637);
    /// <summary>将位于计算堆栈顶部的有符号值转换为有符号 native int，并在溢出时引发 <see cref="T:System.OverflowException" />。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Conv_Ovf_I = new OpCode(OpCodeValues.Conv_Ovf_I, 6953637);
    /// <summary>将位于计算堆栈顶部的有符号值转换为 unsigned native int，并在溢出时引发 <see cref="T:System.OverflowException" />。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Conv_Ovf_U = new OpCode(OpCodeValues.Conv_Ovf_U, 6953637);
    /// <summary>将两个整数相加，执行溢出检查，并且将结果推送到计算堆栈上。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Add_Ovf = new OpCode(OpCodeValues.Add_Ovf, -261739867);
    /// <summary>将两个无符号整数值相加，执行溢出检查，并且将结果推送到计算堆栈上。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Add_Ovf_Un = new OpCode(OpCodeValues.Add_Ovf_Un, -261739867);
    /// <summary>将两个整数值相乘，执行溢出检查，并将结果推送到计算堆栈上。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Mul_Ovf = new OpCode(OpCodeValues.Mul_Ovf, -261739867);
    /// <summary>将两个无符号整数值相乘，执行溢出检查，并将结果推送到计算堆栈上。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Mul_Ovf_Un = new OpCode(OpCodeValues.Mul_Ovf_Un, -261739867);
    /// <summary>从另一值中减去一个整数值，执行溢出检查，并且将结果推送到计算堆栈上。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Sub_Ovf = new OpCode(OpCodeValues.Sub_Ovf, -261739867);
    /// <summary>从另一值中减去一个无符号整数值，执行溢出检查，并且将结果推送到计算堆栈上。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Sub_Ovf_Un = new OpCode(OpCodeValues.Sub_Ovf_Un, -261739867);
    /// <summary>将控制从异常块的 fault 或 finally 子句转移回公共语言结构 (CLI) 异常处理程序。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Endfinally = new OpCode(OpCodeValues.Endfinally, 23333605);
    /// <summary>退出受保护的代码区域，无条件将控制转移到特定目标指令。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Leave = new OpCode(OpCodeValues.Leave, 23333376);
    /// <summary>退出受保护的代码区域，无条件将控制转移到目标指令（缩写形式）。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Leave_S = new OpCode(OpCodeValues.Leave_S, 23333391);
    /// <summary>在所提供的地址存储 native int 类型的值。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Stind_I = new OpCode(OpCodeValues.Stind_I, -530294107);
    /// <summary>将位于计算堆栈顶部的值转换为 unsigned native int，然后将其扩展为 native int。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Conv_U = new OpCode(OpCodeValues.Conv_U, 6953637);
    /// <summary>此指令为保留指令。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Prefix7 = new OpCode(OpCodeValues.Prefix7, 6554757);
    /// <summary>此指令为保留指令。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Prefix6 = new OpCode(OpCodeValues.Prefix6, 6554757);
    /// <summary>此指令为保留指令。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Prefix5 = new OpCode(OpCodeValues.Prefix5, 6554757);
    /// <summary>此指令为保留指令。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Prefix4 = new OpCode(OpCodeValues.Prefix4, 6554757);
    /// <summary>此指令为保留指令。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Prefix3 = new OpCode(OpCodeValues.Prefix3, 6554757);
    /// <summary>此指令为保留指令。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Prefix2 = new OpCode(OpCodeValues.Prefix2, 6554757);
    /// <summary>此指令为保留指令。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Prefix1 = new OpCode(OpCodeValues.Prefix1, 6554757);
    /// <summary>此指令为保留指令。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Prefixref = new OpCode(OpCodeValues.Prefixref, 6554757);
    /// <summary>返回指向当前方法的参数列表的非托管指针。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Arglist = new OpCode(OpCodeValues.Arglist, 279579301);
    /// <summary>比较两个值。如果这两个值相等，则将整数值 1 (int32) 推送到计算堆栈上；否则，将 0 (int32) 推送到计算堆栈上。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ceq = new OpCode(OpCodeValues.Ceq, -257283419);
    /// <summary>比较两个值。如果第一个值大于第二个值，则将整数值 1 (int32) 推送到计算堆栈上；反之，将 0 (int32) 推送到计算堆栈上。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Cgt = new OpCode(OpCodeValues.Cgt, -257283419);
    /// <summary>比较两个无符号的或未经排序的值。如果第一个值大于第二个值，则将整数值 1 (int32) 推送到计算堆栈上；反之，将 0 (int32) 推送到计算堆栈上。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Cgt_Un = new OpCode(OpCodeValues.Cgt_Un, -257283419);
    /// <summary>比较两个值。如果第一个值小于第二个值，则将整数值 1 (int32) 推送到计算堆栈上；反之，将 0 (int32) 推送到计算堆栈上。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Clt = new OpCode(OpCodeValues.Clt, -257283419);
    /// <summary>比较无符号的或未经排序的值 <paramref name="value1" /> 和 <paramref name="value2" />。如果 <paramref name="value1" /> 小于 <paramref name="value2" />，则将整数值 1 (int32) 推送到计算堆栈上；反之，将 0 (int32) 推送到计算堆栈上。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Clt_Un = new OpCode(OpCodeValues.Clt_Un, -257283419);
    /// <summary>将指向实现特定方法的本机代码的非托管指针（native int 类型）推送到计算堆栈上。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldftn = new OpCode(OpCodeValues.Ldftn, 279579300);
    /// <summary>将指向实现与指定对象关联的特定虚方法的本机代码的非托管指针（native int 类型）推送到计算堆栈上。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldvirtftn = new OpCode(OpCodeValues.Ldvirtftn, 11184804);
    /// <summary>将参数（由指定索引值引用）加载到堆栈上。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldarg = new OpCode(OpCodeValues.Ldarg, 279317166);
    /// <summary>将参数地址加载到计算堆栈上。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldarga = new OpCode(OpCodeValues.Ldarga, 279579310);
    /// <summary>将位于计算堆栈顶部的值存储到位于指定索引的参数槽中。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Starg = new OpCode(OpCodeValues.Starg, -257680722);
    /// <summary>将指定索引处的局部变量加载到计算堆栈上。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldloc = new OpCode(OpCodeValues.Ldloc, 279317166);
    /// <summary>将位于特定索引处的局部变量的地址加载到计算堆栈上。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Ldloca = new OpCode(OpCodeValues.Ldloca, 279579310);
    /// <summary>从计算堆栈的顶部弹出当前值并将其存储到指定索引处的局部变量列表中。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Stloc = new OpCode(OpCodeValues.Stloc, -257680722);
    /// <summary>从本地动态内存池分配特定数目的字节并将第一个分配的字节的地址（瞬态指针，* 类型）推送到计算堆栈上。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Localloc = new OpCode(OpCodeValues.Localloc, 11156133);
    /// <summary>将控制从异常的 filter 子句转移回公共语言结构 (CLI) 异常处理程序。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Endfilter = new OpCode(OpCodeValues.Endfilter, -240895259);
    /// <summary>指示当前位于计算堆栈上的地址可能没有与紧接的 ldind、stind、ldfld、stfld、ldobj、stobj、initblk 或 cpblk 指令的自然大小对齐。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Unaligned = new OpCode(OpCodeValues.Unaligned_, 10750096);
    /// <summary>指定当前位于计算堆栈顶部的地址可以是易失的，并且读取该位置的结果不能被缓存，或者对该地址的多个存储区不能被取消。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Volatile = new OpCode(OpCodeValues.Volatile_, 10750085);
    /// <summary>执行后缀的方法调用指令，以便在执行实际调用指令前移除当前方法的堆栈帧。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Tailcall = new OpCode(OpCodeValues.Tail_, 10750085);
    /// <summary>将位于指定地址的值类型的每个字段初始化为空引用或适当的基元类型的 0。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Initobj = new OpCode(OpCodeValues.Initobj, -257673555);
    /// <summary>约束要对其进行虚方法调用的类型。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Constrained = new OpCode(OpCodeValues.Constrained_, 10750093);
    /// <summary>将指定数目的字节从源地址复制到目标地址。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Cpblk = new OpCode(OpCodeValues.Cpblk, -794527067);
    /// <summary>将位于特定地址的内存的指定块初始化为给定大小和初始值。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Initblk = new OpCode(OpCodeValues.Initblk, -794527067);
    /// <summary>再次引发当前异常。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Rethrow = new OpCode(OpCodeValues.Rethrow, 27526917);
    /// <summary>将提供的值类型的大小（以字节为单位）推送到计算堆栈上。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Sizeof = new OpCode(OpCodeValues.Sizeof, 279579309);
    /// <summary>检索嵌入在类型化引用内的类型标记。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Refanytype = new OpCode(OpCodeValues.Refanytype, 11147941);
    /// <summary>指定后面的数组地址操作在运行时不执行类型检查，并且返回可变性受限的托管指针。</summary>
    [__DynamicallyInvokable]
    public static readonly OpCode Readonly = new OpCode(OpCodeValues.Readonly_, 10750085);

    private OpCodes()
    {
    }

    /// <summary>如果提供的操作码采用单字节参数则返回真或假。</summary>
    /// <returns>True 或 false。</returns>
    /// <param name="inst">操作码对象的实例。 </param>
    [__DynamicallyInvokable]
    public static bool TakesSingleByteArgument(OpCode inst)
    {
      switch (inst.OperandType)
      {
        case OperandType.ShortInlineBrTarget:
        case OperandType.ShortInlineI:
        case OperandType.ShortInlineVar:
          return true;
        default:
          return false;
      }
    }
  }
}
