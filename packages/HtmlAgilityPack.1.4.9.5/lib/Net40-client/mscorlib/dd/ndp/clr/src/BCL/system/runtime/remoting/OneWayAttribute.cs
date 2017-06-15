// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Messaging.OneWayAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Messaging
{
  /// <summary>将方法标记为单向方法，无返回值和 out 或 ref 参数。</summary>
  [AttributeUsage(AttributeTargets.Method)]
  [ComVisible(true)]
  public class OneWayAttribute : Attribute
  {
  }
}
