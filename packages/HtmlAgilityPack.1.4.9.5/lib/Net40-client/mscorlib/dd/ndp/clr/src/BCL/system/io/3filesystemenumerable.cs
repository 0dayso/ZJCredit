// Decompiled with JetBrains decompiler
// Type: System.IO.StringResultHandler
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;

namespace System.IO
{
  internal class StringResultHandler : SearchResultHandler<string>
  {
    private bool _includeFiles;
    private bool _includeDirs;

    internal StringResultHandler(bool includeFiles, bool includeDirs)
    {
      this._includeFiles = includeFiles;
      this._includeDirs = includeDirs;
    }

    [SecurityCritical]
    internal override bool IsResultIncluded(SearchResult result)
    {
      return ((!this._includeFiles ? 0 : (FileSystemEnumerableHelpers.IsFile(result.FindData) ? 1 : 0)) | (!this._includeDirs ? (false ? 1 : 0) : (FileSystemEnumerableHelpers.IsDir(result.FindData) ? 1 : 0))) != 0;
    }

    [SecurityCritical]
    internal override string CreateObject(SearchResult result)
    {
      return result.UserPath;
    }
  }
}
