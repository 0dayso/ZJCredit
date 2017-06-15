// Decompiled with JetBrains decompiler
// Type: System.Runtime.AssemblyTargetedPatchBandAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime
{
  /// <summary>为 .NET Framework 的目标修补指定修补程序带区信息。</summary>
  [AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
  public sealed class AssemblyTargetedPatchBandAttribute : Attribute
  {
    private string m_targetedPatchBand;

    /// <summary>获取修补程序带区。</summary>
    /// <returns>修补程序带区信息。</returns>
    public string TargetedPatchBand
    {
      get
      {
        return this.m_targetedPatchBand;
      }
    }

    /// <summary>初始化 <see cref="T:System.Runtime.AssemblyTargetedPatchBandAttribute" /> 类的新实例。</summary>
    /// <param name="targetedPatchBand">修补程序带区。</param>
    public AssemblyTargetedPatchBandAttribute(string targetedPatchBand)
    {
      this.m_targetedPatchBand = targetedPatchBand;
    }
  }
}
