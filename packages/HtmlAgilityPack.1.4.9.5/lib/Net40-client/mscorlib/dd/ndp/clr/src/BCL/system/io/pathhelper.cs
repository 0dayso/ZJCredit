// Decompiled with JetBrains decompiler
// Type: System.IO.PathHelper
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace System.IO
{
  internal struct PathHelper
  {
    private int m_capacity;
    private int m_length;
    private int m_maxPath;
    [SecurityCritical]
    private unsafe char* m_arrayPtr;
    private StringBuilder m_sb;
    private bool useStackAlloc;
    private bool doNotTryExpandShortFileName;

    internal int Length
    {
      get
      {
        if (this.useStackAlloc)
          return this.m_length;
        return this.m_sb.Length;
      }
      set
      {
        if (this.useStackAlloc)
          this.m_length = value;
        else
          this.m_sb.Length = value;
      }
    }

    internal int Capacity
    {
      get
      {
        return this.m_capacity;
      }
    }

    internal unsafe char this[int index]
    {
      [SecurityCritical] get
      {
        if (this.useStackAlloc)
          return this.m_arrayPtr[index];
        return this.m_sb[index];
      }
      [SecurityCritical] set
      {
        if (this.useStackAlloc)
          this.m_arrayPtr[index] = value;
        else
          this.m_sb[index] = value;
      }
    }

    [SecurityCritical]
    internal unsafe PathHelper(char* charArrayPtr, int length)
    {
      this.m_length = 0;
      this.m_sb = (StringBuilder) null;
      this.m_arrayPtr = charArrayPtr;
      this.m_capacity = length;
      this.m_maxPath = Path.MaxPath;
      this.useStackAlloc = true;
      this.doNotTryExpandShortFileName = false;
    }

    [SecurityCritical]
    internal unsafe PathHelper(int capacity, int maxPath)
    {
      this.m_length = 0;
      this.m_arrayPtr = (char*) null;
      this.useStackAlloc = false;
      this.m_sb = new StringBuilder(capacity);
      this.m_capacity = capacity;
      this.m_maxPath = maxPath;
      this.doNotTryExpandShortFileName = false;
    }

    [SecurityCritical]
    internal unsafe void Append(char value)
    {
      if (this.Length + 1 >= this.m_capacity)
        throw new PathTooLongException(Environment.GetResourceString("IO.PathTooLong"));
      if (this.useStackAlloc)
      {
        this.m_arrayPtr[this.Length] = value;
        this.m_length = this.m_length + 1;
      }
      else
        this.m_sb.Append(value);
    }

    [SecurityCritical]
    internal unsafe int GetFullPathName()
    {
      if (this.useStackAlloc)
      {
        char* chPtr = stackalloc char[Path.MaxPath + 1];
        int fullPathName = Win32Native.GetFullPathName(this.m_arrayPtr, Path.MaxPath + 1, chPtr, IntPtr.Zero);
        if (fullPathName > Path.MaxPath)
        {
          chPtr = stackalloc char[fullPathName];
          fullPathName = Win32Native.GetFullPathName(this.m_arrayPtr, fullPathName, chPtr, IntPtr.Zero);
        }
        if (fullPathName >= Path.MaxPath)
          throw new PathTooLongException(Environment.GetResourceString("IO.PathTooLong"));
        if (fullPathName == 0 && (int) *this.m_arrayPtr != 0)
          __Error.WinIOError();
        else if (fullPathName < Path.MaxPath)
          chPtr[fullPathName] = char.MinValue;
        this.doNotTryExpandShortFileName = false;
        string.wstrcpy(this.m_arrayPtr, chPtr, fullPathName);
        this.Length = fullPathName;
        return fullPathName;
      }
      StringBuilder buffer = new StringBuilder(this.m_capacity + 1);
      int fullPathName1 = Win32Native.GetFullPathName(this.m_sb.ToString(), this.m_capacity + 1, buffer, IntPtr.Zero);
      if (fullPathName1 > this.m_maxPath)
      {
        buffer.Length = fullPathName1;
        fullPathName1 = Win32Native.GetFullPathName(this.m_sb.ToString(), fullPathName1, buffer, IntPtr.Zero);
      }
      if (fullPathName1 >= this.m_maxPath)
        throw new PathTooLongException(Environment.GetResourceString("IO.PathTooLong"));
      if (fullPathName1 == 0 && (int) this.m_sb[0] != 0)
      {
        if (this.Length >= this.m_maxPath)
          throw new PathTooLongException(Environment.GetResourceString("IO.PathTooLong"));
        __Error.WinIOError();
      }
      this.doNotTryExpandShortFileName = false;
      this.m_sb = buffer;
      return fullPathName1;
    }

    [SecurityCritical]
    internal unsafe bool TryExpandShortFileName()
    {
      if (this.doNotTryExpandShortFileName)
        return false;
      if (this.useStackAlloc)
      {
        this.NullTerminate();
        char* arrayPtr = this.UnsafeGetArrayPtr();
        char* chPtr = stackalloc char[Path.MaxPath + 1];
        int longPathName = Win32Native.GetLongPathName(arrayPtr, chPtr, Path.MaxPath);
        if (longPathName >= Path.MaxPath)
          throw new PathTooLongException(Environment.GetResourceString("IO.PathTooLong"));
        if (longPathName == 0)
        {
          switch (Marshal.GetLastWin32Error())
          {
            case 2:
            case 3:
              this.doNotTryExpandShortFileName = true;
              break;
          }
          return false;
        }
        string.wstrcpy(arrayPtr, chPtr, longPathName);
        this.Length = longPathName;
        this.NullTerminate();
        return true;
      }
      StringBuilder stringBuilder = this.GetStringBuilder();
      string @string = stringBuilder.ToString();
      string path = @string;
      bool flag = false;
      if (path.Length > Path.MaxPath)
      {
        path = Path.AddLongPathPrefix(path);
        flag = true;
      }
      stringBuilder.Capacity = this.m_capacity;
      stringBuilder.Length = 0;
      int longPathName1 = Win32Native.GetLongPathName(path, stringBuilder, this.m_capacity);
      if (longPathName1 == 0)
      {
        int lastWin32Error = Marshal.GetLastWin32Error();
        if (2 == lastWin32Error || 3 == lastWin32Error)
          this.doNotTryExpandShortFileName = true;
        stringBuilder.Length = 0;
        stringBuilder.Append(@string);
        return false;
      }
      if (flag)
        longPathName1 -= 4;
      if (longPathName1 >= this.m_maxPath)
        throw new PathTooLongException(Environment.GetResourceString("IO.PathTooLong"));
      this.Length = Path.RemoveLongPathPrefix(stringBuilder).Length;
      return true;
    }

    [SecurityCritical]
    internal unsafe void Fixup(int lenSavedName, int lastSlash)
    {
      if (this.useStackAlloc)
      {
        char* chPtr = stackalloc char[lenSavedName];
        string.wstrcpy(chPtr, (char*) ((IntPtr) (this.m_arrayPtr + lastSlash) + 2), lenSavedName);
        this.Length = lastSlash;
        this.NullTerminate();
        this.doNotTryExpandShortFileName = false;
        this.TryExpandShortFileName();
        this.Append(Path.DirectorySeparatorChar);
        if (this.Length + lenSavedName >= Path.MaxPath)
          throw new PathTooLongException(Environment.GetResourceString("IO.PathTooLong"));
        string.wstrcpy(this.m_arrayPtr + this.Length, chPtr, lenSavedName);
        this.Length = this.Length + lenSavedName;
      }
      else
      {
        string @string = this.m_sb.ToString(lastSlash + 1, lenSavedName);
        this.Length = lastSlash;
        this.doNotTryExpandShortFileName = false;
        this.TryExpandShortFileName();
        this.Append(Path.DirectorySeparatorChar);
        if (this.Length + lenSavedName >= this.m_maxPath)
          throw new PathTooLongException(Environment.GetResourceString("IO.PathTooLong"));
        this.m_sb.Append(@string);
      }
    }

    [SecurityCritical]
    internal unsafe bool OrdinalStartsWith(string compareTo, bool ignoreCase)
    {
      if (this.Length < compareTo.Length)
        return false;
      if (this.useStackAlloc)
      {
        this.NullTerminate();
        if (ignoreCase)
        {
          string str = new string(this.m_arrayPtr, 0, compareTo.Length);
          return compareTo.Equals(str, StringComparison.OrdinalIgnoreCase);
        }
        for (int index = 0; index < compareTo.Length; ++index)
        {
          if ((int) this.m_arrayPtr[index] != (int) compareTo[index])
            return false;
        }
        return true;
      }
      if (ignoreCase)
        return this.m_sb.ToString().StartsWith(compareTo, StringComparison.OrdinalIgnoreCase);
      return this.m_sb.ToString().StartsWith(compareTo, StringComparison.Ordinal);
    }

    [SecuritySafeCritical]
    public override unsafe string ToString()
    {
      if (this.useStackAlloc)
        return new string(this.m_arrayPtr, 0, this.Length);
      return this.m_sb.ToString();
    }

    [SecurityCritical]
    private unsafe char* UnsafeGetArrayPtr()
    {
      return this.m_arrayPtr;
    }

    private StringBuilder GetStringBuilder()
    {
      return this.m_sb;
    }

    [SecurityCritical]
    private unsafe void NullTerminate()
    {
      this.m_arrayPtr[this.m_length] = char.MinValue;
    }
  }
}
