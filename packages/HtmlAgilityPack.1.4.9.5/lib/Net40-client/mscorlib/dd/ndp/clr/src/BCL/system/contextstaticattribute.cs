// Decompiled with JetBrains decompiler
// Type: System.ContextStaticAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System
{
  /// <summary>指示静态字段的值是特定上下文的唯一值。</summary>
  /// <filterpriority>1</filterpriority>
  [AttributeUsage(AttributeTargets.Field, Inherited = false)]
  [ComVisible(true)]
  [Serializable]
  public class ContextStaticAttribute : Attribute
  {
  }
}
