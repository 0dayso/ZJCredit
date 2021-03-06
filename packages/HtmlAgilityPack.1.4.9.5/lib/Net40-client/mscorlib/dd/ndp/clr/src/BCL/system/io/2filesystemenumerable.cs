﻿// Decompiled with JetBrains decompiler
// Type: System.IO.FileSystemEnumerableIterator`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.IO
{
  internal class FileSystemEnumerableIterator<TSource> : Iterator<TSource>
  {
    private const int STATE_INIT = 1;
    private const int STATE_SEARCH_NEXT_DIR = 2;
    private const int STATE_FIND_NEXT_FILE = 3;
    private const int STATE_FINISH = 4;
    private SearchResultHandler<TSource> _resultHandler;
    private List<Directory.SearchData> searchStack;
    private Directory.SearchData searchData;
    private string searchCriteria;
    [SecurityCritical]
    private SafeFindHandle _hnd;
    private bool needsParentPathDiscoveryDemand;
    private bool empty;
    private string userPath;
    private SearchOption searchOption;
    private string fullPath;
    private string normalizedSearchPath;
    private int oldMode;
    private bool _checkHost;

    [SecuritySafeCritical]
    internal FileSystemEnumerableIterator(string path, string originalUserPath, string searchPattern, SearchOption searchOption, SearchResultHandler<TSource> resultHandler, bool checkHost)
    {
      this.oldMode = Win32Native.SetErrorMode(1);
      this.searchStack = new List<Directory.SearchData>();
      string str = FileSystemEnumerableIterator<TSource>.NormalizeSearchPattern(searchPattern);
      if (str.Length == 0)
      {
        this.empty = true;
      }
      else
      {
        this._resultHandler = resultHandler;
        this.searchOption = searchOption;
        this.fullPath = Path.GetFullPathInternal(path);
        string fullSearchString = FileSystemEnumerableIterator<TSource>.GetFullSearchString(this.fullPath, str);
        this.normalizedSearchPath = Path.GetDirectoryName(fullSearchString);
        string[] pathList = new string[2]{ Directory.GetDemandDir(this.fullPath, true), Directory.GetDemandDir(this.normalizedSearchPath, true) };
        this._checkHost = checkHost;
        new FileIOPermission(FileIOPermissionAccess.PathDiscovery, pathList, false, false).Demand();
        this.searchCriteria = FileSystemEnumerableIterator<TSource>.GetNormalizedSearchCriteria(fullSearchString, this.normalizedSearchPath);
        string directoryName = Path.GetDirectoryName(str);
        string path1 = originalUserPath;
        if (directoryName != null && directoryName.Length != 0)
          path1 = Path.Combine(path1, directoryName);
        this.userPath = path1;
        this.searchData = new Directory.SearchData(this.normalizedSearchPath, this.userPath, searchOption);
        this.CommonInit();
      }
    }

    [SecuritySafeCritical]
    private FileSystemEnumerableIterator(string fullPath, string normalizedSearchPath, string searchCriteria, string userPath, SearchOption searchOption, SearchResultHandler<TSource> resultHandler, bool checkHost)
    {
      this.fullPath = fullPath;
      this.normalizedSearchPath = normalizedSearchPath;
      this.searchCriteria = searchCriteria;
      this._resultHandler = resultHandler;
      this.userPath = userPath;
      this.searchOption = searchOption;
      this._checkHost = checkHost;
      this.searchStack = new List<Directory.SearchData>();
      if (searchCriteria != null)
      {
        new FileIOPermission(FileIOPermissionAccess.PathDiscovery, new string[2]
        {
          Directory.GetDemandDir(fullPath, true),
          Directory.GetDemandDir(normalizedSearchPath, true)
        }, false, false).Demand();
        this.searchData = new Directory.SearchData(normalizedSearchPath, userPath, searchOption);
        this.CommonInit();
      }
      else
        this.empty = true;
    }

    [SecurityCritical]
    private void CommonInit()
    {
      string fileName = Path.InternalCombine(this.searchData.fullPath, this.searchCriteria);
      Win32Native.WIN32_FIND_DATA wiN32FindData = new Win32Native.WIN32_FIND_DATA();
      this._hnd = Win32Native.FindFirstFile(fileName, wiN32FindData);
      if (this._hnd.IsInvalid)
      {
        int lastWin32Error = Marshal.GetLastWin32Error();
        switch (lastWin32Error)
        {
          case 2:
          case 18:
            this.empty = this.searchData.searchOption == SearchOption.TopDirectoryOnly;
            break;
          default:
            this.HandleError(lastWin32Error, this.searchData.fullPath);
            break;
        }
      }
      if (this.searchData.searchOption == SearchOption.TopDirectoryOnly)
      {
        if (this.empty)
        {
          this._hnd.Dispose();
        }
        else
        {
          SearchResult searchResult = this.CreateSearchResult(this.searchData, wiN32FindData);
          if (!this._resultHandler.IsResultIncluded(searchResult))
            return;
          this.current = this._resultHandler.CreateObject(searchResult);
        }
      }
      else
      {
        this._hnd.Dispose();
        this.searchStack.Add(this.searchData);
      }
    }

    protected override Iterator<TSource> Clone()
    {
      return (Iterator<TSource>) new FileSystemEnumerableIterator<TSource>(this.fullPath, this.normalizedSearchPath, this.searchCriteria, this.userPath, this.searchOption, this._resultHandler, this._checkHost);
    }

    [SecuritySafeCritical]
    protected override void Dispose(bool disposing)
    {
      try
      {
        if (this._hnd == null)
          return;
        this._hnd.Dispose();
      }
      finally
      {
        Win32Native.SetErrorMode(this.oldMode);
        base.Dispose(disposing);
      }
    }

    [SecuritySafeCritical]
    public override bool MoveNext()
    {
      Win32Native.WIN32_FIND_DATA wiN32FindData = new Win32Native.WIN32_FIND_DATA();
      switch (this.state)
      {
        case 1:
          if (this.empty)
          {
            this.state = 4;
            goto case 4;
          }
          else if (this.searchData.searchOption == SearchOption.TopDirectoryOnly)
          {
            this.state = 3;
            if ((object) this.current != null)
              return true;
            goto case 3;
          }
          else
          {
            this.state = 2;
            goto case 2;
          }
        case 2:
          while (this.searchStack.Count > 0)
          {
            this.searchData = this.searchStack[0];
            this.searchStack.RemoveAt(0);
            this.AddSearchableDirsToStack(this.searchData);
            this._hnd = Win32Native.FindFirstFile(Path.InternalCombine(this.searchData.fullPath, this.searchCriteria), wiN32FindData);
            if (this._hnd.IsInvalid)
            {
              int lastWin32Error = Marshal.GetLastWin32Error();
              switch (lastWin32Error)
              {
                case 2:
                case 18:
                case 3:
                  continue;
                default:
                  this._hnd.Dispose();
                  this.HandleError(lastWin32Error, this.searchData.fullPath);
                  break;
              }
            }
            this.state = 3;
            this.needsParentPathDiscoveryDemand = true;
            SearchResult searchResult = this.CreateSearchResult(this.searchData, wiN32FindData);
            if (this._resultHandler.IsResultIncluded(searchResult))
            {
              if (this.needsParentPathDiscoveryDemand)
              {
                this.DoDemand(this.searchData.fullPath);
                this.needsParentPathDiscoveryDemand = false;
              }
              this.current = this._resultHandler.CreateObject(searchResult);
              return true;
            }
            goto case 3;
          }
          this.state = 4;
          goto case 4;
        case 3:
          if (this.searchData != null && this._hnd != null)
          {
            while (Win32Native.FindNextFile(this._hnd, wiN32FindData))
            {
              SearchResult searchResult = this.CreateSearchResult(this.searchData, wiN32FindData);
              if (this._resultHandler.IsResultIncluded(searchResult))
              {
                if (this.needsParentPathDiscoveryDemand)
                {
                  this.DoDemand(this.searchData.fullPath);
                  this.needsParentPathDiscoveryDemand = false;
                }
                this.current = this._resultHandler.CreateObject(searchResult);
                return true;
              }
            }
            int lastWin32Error = Marshal.GetLastWin32Error();
            if (this._hnd != null)
              this._hnd.Dispose();
            if (lastWin32Error != 0 && lastWin32Error != 18 && lastWin32Error != 2)
              this.HandleError(lastWin32Error, this.searchData.fullPath);
          }
          if (this.searchData.searchOption == SearchOption.TopDirectoryOnly)
          {
            this.state = 4;
            goto case 4;
          }
          else
          {
            this.state = 2;
            goto case 2;
          }
        case 4:
          this.Dispose();
          break;
      }
      return false;
    }

    [SecurityCritical]
    private SearchResult CreateSearchResult(Directory.SearchData localSearchData, Win32Native.WIN32_FIND_DATA findData)
    {
      string userPath = Path.InternalCombine(localSearchData.userPath, findData.cFileName);
      return new SearchResult(Path.InternalCombine(localSearchData.fullPath, findData.cFileName), userPath, findData);
    }

    [SecurityCritical]
    private void HandleError(int hr, string path)
    {
      this.Dispose();
      __Error.WinIOError(hr, path);
    }

    [SecurityCritical]
    private void AddSearchableDirsToStack(Directory.SearchData localSearchData)
    {
      string fileName = Path.InternalCombine(localSearchData.fullPath, "*");
      SafeFindHandle hndFindFile = (SafeFindHandle) null;
      Win32Native.WIN32_FIND_DATA wiN32FindData = new Win32Native.WIN32_FIND_DATA();
      try
      {
        hndFindFile = Win32Native.FindFirstFile(fileName, wiN32FindData);
        if (hndFindFile.IsInvalid)
        {
          int lastWin32Error = Marshal.GetLastWin32Error();
          switch (lastWin32Error)
          {
            case 2:
              return;
            case 18:
              return;
            case 3:
              return;
            default:
              this.HandleError(lastWin32Error, localSearchData.fullPath);
              break;
          }
        }
        int num1 = 0;
        do
        {
          if (FileSystemEnumerableHelpers.IsDir(wiN32FindData))
          {
            string fullPath = Path.InternalCombine(localSearchData.fullPath, wiN32FindData.cFileName);
            string str = Path.InternalCombine(localSearchData.userPath, wiN32FindData.cFileName);
            SearchOption searchOption = localSearchData.searchOption;
            string userPath = str;
            int num2 = (int) searchOption;
            Directory.SearchData searchData = new Directory.SearchData(fullPath, userPath, (SearchOption) num2);
            this.searchStack.Insert(num1++, searchData);
          }
        }
        while (Win32Native.FindNextFile(hndFindFile, wiN32FindData));
      }
      finally
      {
        if (hndFindFile != null)
          hndFindFile.Dispose();
      }
    }

    [SecurityCritical]
    internal void DoDemand(string fullPathToDemand)
    {
      new FileIOPermission(FileIOPermissionAccess.PathDiscovery, new string[1]
      {
        Directory.GetDemandDir(fullPathToDemand, true)
      }, false, false).Demand();
    }

    private static string NormalizeSearchPattern(string searchPattern)
    {
      string searchPattern1 = searchPattern.TrimEnd(Path.TrimEndChars);
      if (searchPattern1.Equals("."))
        searchPattern1 = "*";
      Path.CheckSearchPattern(searchPattern1);
      return searchPattern1;
    }

    private static string GetNormalizedSearchCriteria(string fullSearchString, string fullPathMod)
    {
      string str = fullPathMod;
      int index = str.Length - 1;
      return !Path.IsDirectorySeparator(str[index]) ? fullSearchString.Substring(fullPathMod.Length + 1) : fullSearchString.Substring(fullPathMod.Length);
    }

    private static string GetFullSearchString(string fullPath, string searchPattern)
    {
      string str1 = Path.InternalCombine(fullPath, searchPattern);
      string str2 = str1;
      int index = str2.Length - 1;
      char c = str2[index];
      if (Path.IsDirectorySeparator(c) || (int) c == (int) Path.VolumeSeparatorChar)
        str1 += "*";
      return str1;
    }
  }
}
