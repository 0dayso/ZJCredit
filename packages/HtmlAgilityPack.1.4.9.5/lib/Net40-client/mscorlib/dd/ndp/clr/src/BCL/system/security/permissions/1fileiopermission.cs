﻿// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.FileIOAccess
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Permissions
{
  [Serializable]
  internal sealed class FileIOAccess
  {
    private bool m_ignoreCase = true;
    private StringExpressionSet m_set;
    private bool m_allFiles;
    private bool m_allLocalFiles;
    private bool m_pathDiscovery;
    private const string m_strAllFiles = "*AllFiles*";
    private const string m_strAllLocalFiles = "*AllLocalFiles*";

    public bool AllFiles
    {
      get
      {
        return this.m_allFiles;
      }
      set
      {
        this.m_allFiles = value;
      }
    }

    public bool AllLocalFiles
    {
      get
      {
        return this.m_allLocalFiles;
      }
      set
      {
        this.m_allLocalFiles = value;
      }
    }

    public bool PathDiscovery
    {
      set
      {
        this.m_pathDiscovery = value;
      }
    }

    public FileIOAccess()
    {
      this.m_set = new StringExpressionSet(this.m_ignoreCase, true);
      this.m_allFiles = false;
      this.m_allLocalFiles = false;
      this.m_pathDiscovery = false;
    }

    public FileIOAccess(bool pathDiscovery)
    {
      this.m_set = new StringExpressionSet(this.m_ignoreCase, true);
      this.m_allFiles = false;
      this.m_allLocalFiles = false;
      this.m_pathDiscovery = pathDiscovery;
    }

    [SecurityCritical]
    public FileIOAccess(string value)
    {
      if (value == null)
      {
        this.m_set = new StringExpressionSet(this.m_ignoreCase, true);
        this.m_allFiles = false;
        this.m_allLocalFiles = false;
      }
      else if (value.Length >= "*AllFiles*".Length && string.Compare("*AllFiles*", value, StringComparison.Ordinal) == 0)
      {
        this.m_set = new StringExpressionSet(this.m_ignoreCase, true);
        this.m_allFiles = true;
        this.m_allLocalFiles = false;
      }
      else if (value.Length >= "*AllLocalFiles*".Length && string.Compare("*AllLocalFiles*", 0, value, 0, "*AllLocalFiles*".Length, StringComparison.Ordinal) == 0)
      {
        this.m_set = new StringExpressionSet(this.m_ignoreCase, value.Substring("*AllLocalFiles*".Length), true);
        this.m_allFiles = false;
        this.m_allLocalFiles = true;
      }
      else
      {
        this.m_set = new StringExpressionSet(this.m_ignoreCase, value, true);
        this.m_allFiles = false;
        this.m_allLocalFiles = false;
      }
      this.m_pathDiscovery = false;
    }

    public FileIOAccess(bool allFiles, bool allLocalFiles, bool pathDiscovery)
    {
      this.m_set = new StringExpressionSet(this.m_ignoreCase, true);
      this.m_allFiles = allFiles;
      this.m_allLocalFiles = allLocalFiles;
      this.m_pathDiscovery = pathDiscovery;
    }

    public FileIOAccess(StringExpressionSet set, bool allFiles, bool allLocalFiles, bool pathDiscovery)
    {
      this.m_set = set;
      this.m_set.SetThrowOnRelative(true);
      this.m_allFiles = allFiles;
      this.m_allLocalFiles = allLocalFiles;
      this.m_pathDiscovery = pathDiscovery;
    }

    private FileIOAccess(FileIOAccess operand)
    {
      this.m_set = operand.m_set.Copy();
      this.m_allFiles = operand.m_allFiles;
      this.m_allLocalFiles = operand.m_allLocalFiles;
      this.m_pathDiscovery = operand.m_pathDiscovery;
    }

    [SecurityCritical]
    public void AddExpressions(ArrayList values, bool checkForDuplicates)
    {
      this.m_allFiles = false;
      this.m_set.AddExpressions(values, checkForDuplicates);
    }

    public bool IsEmpty()
    {
      if (this.m_allFiles || this.m_allLocalFiles)
        return false;
      if (this.m_set != null)
        return this.m_set.IsEmpty();
      return true;
    }

    public FileIOAccess Copy()
    {
      return new FileIOAccess(this);
    }

    [SecuritySafeCritical]
    public FileIOAccess Union(FileIOAccess operand)
    {
      if (operand == null)
      {
        if (!this.IsEmpty())
          return this.Copy();
        return (FileIOAccess) null;
      }
      if (this.m_allFiles || operand.m_allFiles)
        return new FileIOAccess(true, false, this.m_pathDiscovery);
      return new FileIOAccess(this.m_set.Union(operand.m_set), false, this.m_allLocalFiles || operand.m_allLocalFiles, this.m_pathDiscovery);
    }

    [SecuritySafeCritical]
    public FileIOAccess Intersect(FileIOAccess operand)
    {
      if (operand == null)
        return (FileIOAccess) null;
      if (this.m_allFiles)
      {
        if (operand.m_allFiles)
          return new FileIOAccess(true, false, this.m_pathDiscovery);
        return new FileIOAccess(operand.m_set.Copy(), false, operand.m_allLocalFiles, this.m_pathDiscovery);
      }
      if (operand.m_allFiles)
        return new FileIOAccess(this.m_set.Copy(), false, this.m_allLocalFiles, this.m_pathDiscovery);
      StringExpressionSet set = new StringExpressionSet(this.m_ignoreCase, true);
      if (this.m_allLocalFiles)
      {
        string[] stringArray = operand.m_set.UnsafeToStringArray();
        if (stringArray != null)
        {
          for (int index = 0; index < stringArray.Length; ++index)
          {
            string root = FileIOAccess.GetRoot(stringArray[index]);
            if (root != null && FileIOAccess.IsLocalDrive(FileIOAccess.GetRoot(root)))
              set.AddExpressions(new string[1]
              {
                stringArray[index]
              }, 1 != 0, 0 != 0);
          }
        }
      }
      if (operand.m_allLocalFiles)
      {
        string[] stringArray = this.m_set.UnsafeToStringArray();
        if (stringArray != null)
        {
          for (int index = 0; index < stringArray.Length; ++index)
          {
            string root = FileIOAccess.GetRoot(stringArray[index]);
            if (root != null && FileIOAccess.IsLocalDrive(FileIOAccess.GetRoot(root)))
              set.AddExpressions(new string[1]
              {
                stringArray[index]
              }, 1 != 0, 0 != 0);
          }
        }
      }
      string[] stringArray1 = this.m_set.Intersect(operand.m_set).UnsafeToStringArray();
      if (stringArray1 != null)
        set.AddExpressions(stringArray1, !set.IsEmpty(), false);
      return new FileIOAccess(set, false, this.m_allLocalFiles && operand.m_allLocalFiles, this.m_pathDiscovery);
    }

    [SecuritySafeCritical]
    public bool IsSubsetOf(FileIOAccess operand)
    {
      if (operand == null)
        return this.IsEmpty();
      if (operand.m_allFiles || this.m_pathDiscovery && this.m_set.IsSubsetOfPathDiscovery(operand.m_set) || this.m_set.IsSubsetOf(operand.m_set))
        return true;
      if (!operand.m_allLocalFiles)
        return false;
      foreach (string @string in this.m_set.UnsafeToStringArray())
      {
        string root = FileIOAccess.GetRoot(@string);
        if (root == null || !FileIOAccess.IsLocalDrive(FileIOAccess.GetRoot(root)))
          return false;
      }
      return true;
    }

    private static string GetRoot(string path)
    {
      string str = path.Substring(0, 3);
      if (str.EndsWith(":\\", StringComparison.Ordinal))
        return str;
      return (string) null;
    }

    [SecuritySafeCritical]
    public override string ToString()
    {
      if (this.m_allFiles)
        return "*AllFiles*";
      if (!this.m_allLocalFiles)
        return this.m_set.UnsafeToString();
      string str = "*AllLocalFiles*";
      string @string = this.m_set.UnsafeToString();
      if (@string != null && @string.Length > 0)
        str = str + ";" + @string;
      return str;
    }

    [SecuritySafeCritical]
    public string[] ToStringArray()
    {
      return this.m_set.UnsafeToStringArray();
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern bool IsLocalDrive(string path);

    [SecuritySafeCritical]
    public override bool Equals(object obj)
    {
      FileIOAccess operand = obj as FileIOAccess;
      if (operand == null)
      {
        if (this.IsEmpty())
          return obj == null;
        return false;
      }
      if (this.m_pathDiscovery)
        return this.m_allFiles && operand.m_allFiles || this.m_allLocalFiles == operand.m_allLocalFiles && this.m_set.IsSubsetOf(operand.m_set) && operand.m_set.IsSubsetOf(this.m_set);
      return this.IsSubsetOf(operand) && operand.IsSubsetOf(this);
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }
  }
}
