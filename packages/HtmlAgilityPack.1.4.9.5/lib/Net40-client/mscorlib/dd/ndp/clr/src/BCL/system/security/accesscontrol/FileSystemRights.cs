// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.FileSystemRights
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Security.AccessControl
{
  /// <summary>定义要在创建访问和审核规则时使用的访问权限。</summary>
  [Flags]
  public enum FileSystemRights
  {
    ReadData = 1,
    ListDirectory = ReadData,
    WriteData = 2,
    CreateFiles = WriteData,
    AppendData = 4,
    CreateDirectories = AppendData,
    ReadExtendedAttributes = 8,
    WriteExtendedAttributes = 16,
    ExecuteFile = 32,
    Traverse = ExecuteFile,
    DeleteSubdirectoriesAndFiles = 64,
    ReadAttributes = 128,
    WriteAttributes = 256,
    Delete = 65536,
    ReadPermissions = 131072,
    ChangePermissions = 262144,
    TakeOwnership = 524288,
    Synchronize = 1048576,
    FullControl = Synchronize | TakeOwnership | ChangePermissions | ReadPermissions | Delete | WriteAttributes | ReadAttributes | DeleteSubdirectoriesAndFiles | Traverse | WriteExtendedAttributes | ReadExtendedAttributes | CreateDirectories | CreateFiles | ListDirectory,
    Read = ReadPermissions | ReadAttributes | ReadExtendedAttributes | ListDirectory,
    ReadAndExecute = Read | Traverse,
    Write = WriteAttributes | WriteExtendedAttributes | CreateDirectories | CreateFiles,
    Modify = Write | ReadAndExecute | Delete,
  }
}
