// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.UIPermissionClipboard
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
  /// <summary>指定允许调用代码的剪贴板访问权限的类型。</summary>
  [ComVisible(true)]
  [Serializable]
  public enum UIPermissionClipboard
  {
    NoClipboard,
    OwnClipboard,
    AllClipboard,
  }
}
