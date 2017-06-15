// Decompiled with JetBrains decompiler
// Type: System.Security.SecureString
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace System.Security
{
  /// <summary>表示应保密的文本，例如在不再需要时将其从计算机内存中删除。此类不能被继承。</summary>
  public sealed class SecureString : IDisposable
  {
    private static bool supportedOnCurrentPlatform = SecureString.EncryptionSupported();
    [SecurityCritical]
    private SafeBSTRHandle m_buffer;
    private int m_length;
    private bool m_readOnly;
    private bool m_encrypted;
    private const int BlockSize = 8;
    private const int MaxLength = 65536;
    private const uint ProtectionScope = 0;

    /// <summary>获取当前安全字符串中的字符数。</summary>
    /// <returns>此安全字符串中 <see cref="T:System.Char" /> 对象的数目。</returns>
    /// <exception cref="T:System.ObjectDisposedException">This secure string has already been disposed.</exception>
    public int Length
    {
      [SecuritySafeCritical, MethodImpl(MethodImplOptions.Synchronized)] get
      {
        this.EnsureNotDisposed();
        return this.m_length;
      }
    }

    private int BufferLength
    {
      [SecurityCritical] get
      {
        return this.m_buffer.Length;
      }
    }

    [SecuritySafeCritical]
    static SecureString()
    {
    }

    [SecurityCritical]
    internal SecureString(SecureString str)
    {
      this.AllocateBuffer(str.BufferLength);
      SafeBSTRHandle.Copy(str.m_buffer, this.m_buffer);
      this.m_length = str.m_length;
      this.m_encrypted = str.m_encrypted;
    }

    /// <summary>初始化 <see cref="T:System.Security.SecureString" /> 类的新实例。</summary>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">An error occurred while protecting or unprotecting the value of this instance.</exception>
    /// <exception cref="T:System.NotSupportedException">This operation is not supported on this platform.</exception>
    [SecuritySafeCritical]
    public SecureString()
    {
      this.CheckSupportedOnCurrentPlatform();
      this.AllocateBuffer(8);
      this.m_length = 0;
    }

    /// <summary>用 <see cref="T:System.Char" /> 对象的子数组初始化 <see cref="T:System.Security.SecureString" /> 类的新实例。</summary>
    /// <param name="value">指向 <see cref="T:System.Char" /> 对象的数组的指针。</param>
    /// <param name="length">要包括到新实例中的 <paramref name="value" /> 的元素数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> is null.</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="length" /> is less than zero or greater than 65,536.</exception>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">An error occurred while protecting or unprotecting the value of this secure string. </exception>
    /// <exception cref="T:System.NotSupportedException">This operation is not supported on this platform.</exception>
    [SecurityCritical]
    [CLSCompliant(false)]
    public unsafe SecureString(char* value, int length)
    {
      if ((IntPtr) value == IntPtr.Zero)
        throw new ArgumentNullException("value");
      if (length < 0)
        throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (length > 65536)
        throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("ArgumentOutOfRange_Length"));
      this.InitializeSecureString(value, length);
    }

    [SecurityCritical]
    private static bool EncryptionSupported()
    {
      bool flag = true;
      try
      {
        Win32Native.SystemFunction041(SafeBSTRHandle.Allocate((string) null, 16U), 16U, 0U);
      }
      catch (EntryPointNotFoundException ex)
      {
        flag = false;
      }
      return flag;
    }

    [SecurityCritical]
    [HandleProcessCorruptedStateExceptions]
    private unsafe void InitializeSecureString(char* value, int length)
    {
      this.CheckSupportedOnCurrentPlatform();
      this.AllocateBuffer(length);
      this.m_length = length;
      byte* pointer = (byte*) null;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this.m_buffer.AcquirePointer(ref pointer);
        Buffer.Memcpy(pointer, (byte*) value, length * 2);
      }
      catch (Exception ex)
      {
        this.ProtectMemory();
        throw;
      }
      finally
      {
        if ((IntPtr) pointer != IntPtr.Zero)
          this.m_buffer.ReleasePointer();
      }
      this.ProtectMemory();
    }

    /// <summary>在当前安全字符串的末尾追加一个字符。</summary>
    /// <param name="c">要追加到此安全字符串的字符。</param>
    /// <exception cref="T:System.ObjectDisposedException">This secure string has already been disposed.</exception>
    /// <exception cref="T:System.InvalidOperationException">This secure string is read-only.</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">Performing this operation would make the length of this secure string greater than 65,536 characters.</exception>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">An error occurred while protecting or unprotecting the value of this secure string.</exception>
    [SecuritySafeCritical]
    [HandleProcessCorruptedStateExceptions]
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void AppendChar(char c)
    {
      this.EnsureNotDisposed();
      this.EnsureNotReadOnly();
      this.EnsureCapacity(this.m_length + 1);
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this.UnProtectMemory();
        this.m_buffer.Write<char>((ulong) (uint) (this.m_length * 2), c);
        this.m_length = this.m_length + 1;
      }
      catch (Exception ex)
      {
        this.ProtectMemory();
        throw;
      }
      finally
      {
        this.ProtectMemory();
      }
    }

    /// <summary>删除当前安全字符串的值。</summary>
    /// <exception cref="T:System.ObjectDisposedException">This secure string has already been disposed.</exception>
    /// <exception cref="T:System.InvalidOperationException">This secure string is read-only.</exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Clear()
    {
      this.EnsureNotDisposed();
      this.EnsureNotReadOnly();
      this.m_length = 0;
      this.m_buffer.ClearBuffer();
      this.m_encrypted = false;
    }

    /// <summary>创建当前安全字符串的副本。</summary>
    /// <returns>此安全字符串的副本。</returns>
    /// <exception cref="T:System.ObjectDisposedException">This secure string has already been disposed.</exception>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">An error occurred while protecting or unprotecting the value of this secure string.</exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.Synchronized)]
    public SecureString Copy()
    {
      this.EnsureNotDisposed();
      return new SecureString(this);
    }

    /// <summary>释放由当前 <see cref="T:System.Security.SecureString" /> 对象使用的所有资源。</summary>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Dispose()
    {
      if (this.m_buffer == null || this.m_buffer.IsInvalid)
        return;
      this.m_buffer.Close();
      this.m_buffer = (SafeBSTRHandle) null;
    }

    /// <summary>在此安全字符串中的指定索引位置插入一个字符。</summary>
    /// <param name="index">插入参数 <paramref name="c" /> 的索引位置。</param>
    /// <param name="c">要插入的字符。</param>
    /// <exception cref="T:System.ObjectDisposedException">This secure string has already been disposed.</exception>
    /// <exception cref="T:System.InvalidOperationException">This secure string is read-only.</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> is less than zero, or greater than the length of this secure string.-or-Performing this operation would make the length of this secure string greater than 65,536 characters.</exception>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">An error occurred while protecting or unprotecting the value of this secure string.</exception>
    [SecuritySafeCritical]
    [HandleProcessCorruptedStateExceptions]
    [MethodImpl(MethodImplOptions.Synchronized)]
    public unsafe void InsertAt(int index, char c)
    {
      if (index < 0 || index > this.m_length)
        throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_IndexString"));
      this.EnsureNotDisposed();
      this.EnsureNotReadOnly();
      this.EnsureCapacity(this.m_length + 1);
      byte* pointer = (byte*) null;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this.UnProtectMemory();
        this.m_buffer.AcquirePointer(ref pointer);
        char* chPtr = (char*) pointer;
        for (int index1 = this.m_length; index1 > index; --index1)
          chPtr[index1] = chPtr[index1 - 1];
        chPtr[index] = c;
        this.m_length = this.m_length + 1;
      }
      catch (Exception ex)
      {
        this.ProtectMemory();
        throw;
      }
      finally
      {
        this.ProtectMemory();
        if ((IntPtr) pointer != IntPtr.Zero)
          this.m_buffer.ReleasePointer();
      }
    }

    /// <summary>指示此安全字符串是否标记为只读。</summary>
    /// <returns>如果此安全字符串标记为只读，则为 true；否则为 false。</returns>
    /// <exception cref="T:System.ObjectDisposedException">This secure string has already been disposed.</exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.Synchronized)]
    public bool IsReadOnly()
    {
      this.EnsureNotDisposed();
      return this.m_readOnly;
    }

    /// <summary>将此安全字符串的文本值设置为只读。  </summary>
    /// <exception cref="T:System.ObjectDisposedException">This secure string has already been disposed.</exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void MakeReadOnly()
    {
      this.EnsureNotDisposed();
      this.m_readOnly = true;
    }

    /// <summary>从此安全字符串中的指定索引位置移除字符。</summary>
    /// <param name="index">此安全字符串中的字符的索引位置。</param>
    /// <exception cref="T:System.ObjectDisposedException">This secure string has already been disposed.</exception>
    /// <exception cref="T:System.InvalidOperationException">This secure string is read-only.</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> is less than zero, or greater than or equal to the length of this secure string.</exception>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">An error occurred while protecting or unprotecting the value of this secure string.</exception>
    [SecuritySafeCritical]
    [HandleProcessCorruptedStateExceptions]
    [MethodImpl(MethodImplOptions.Synchronized)]
    public unsafe void RemoveAt(int index)
    {
      this.EnsureNotDisposed();
      this.EnsureNotReadOnly();
      if (index < 0 || index >= this.m_length)
        throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_IndexString"));
      byte* pointer = (byte*) null;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this.UnProtectMemory();
        this.m_buffer.AcquirePointer(ref pointer);
        char* chPtr1 = (char*) pointer;
        for (int index1 = index; index1 < this.m_length - 1; ++index1)
          chPtr1[index1] = chPtr1[index1 + 1];
        char* chPtr2 = chPtr1;
        int num1 = this.m_length - 1;
        this.m_length = num1;
        IntPtr num2 = (IntPtr) num1 * 2;
        *(short*) ((IntPtr) chPtr2 + num2) = (short) 0;
      }
      catch (Exception ex)
      {
        this.ProtectMemory();
        throw;
      }
      finally
      {
        this.ProtectMemory();
        if ((IntPtr) pointer != IntPtr.Zero)
          this.m_buffer.ReleasePointer();
      }
    }

    /// <summary>将指定索引位置上的现有字符替换为其他字符。</summary>
    /// <param name="index">此安全字符串中的某个现有字符的索引位置。</param>
    /// <param name="c">替换现有字符的字符。</param>
    /// <exception cref="T:System.ObjectDisposedException">This secure string has already been disposed.</exception>
    /// <exception cref="T:System.InvalidOperationException">This secure string is read-only.</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> is less than zero, or greater than or equal to the length of this secure string.</exception>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">An error occurred while protecting or unprotecting the value of this secure string.</exception>
    [SecuritySafeCritical]
    [HandleProcessCorruptedStateExceptions]
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void SetAt(int index, char c)
    {
      if (index < 0 || index >= this.m_length)
        throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_IndexString"));
      this.EnsureNotDisposed();
      this.EnsureNotReadOnly();
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this.UnProtectMemory();
        this.m_buffer.Write<char>((ulong) (uint) (index * 2), c);
      }
      catch (Exception ex)
      {
        this.ProtectMemory();
        throw;
      }
      finally
      {
        this.ProtectMemory();
      }
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    private void AllocateBuffer(int size)
    {
      this.m_buffer = SafeBSTRHandle.Allocate((string) null, SecureString.GetAlignedSize(size));
      if (this.m_buffer.IsInvalid)
        throw new OutOfMemoryException();
    }

    private void CheckSupportedOnCurrentPlatform()
    {
      if (!SecureString.supportedOnCurrentPlatform)
        throw new NotSupportedException(Environment.GetResourceString("Arg_PlatformSecureString"));
    }

    [SecurityCritical]
    private void EnsureCapacity(int capacity)
    {
      if (capacity > 65536)
        throw new ArgumentOutOfRangeException("capacity", Environment.GetResourceString("ArgumentOutOfRange_Capacity"));
      if (capacity <= this.m_buffer.Length)
        return;
      SafeBSTRHandle target = SafeBSTRHandle.Allocate((string) null, SecureString.GetAlignedSize(capacity));
      if (target.IsInvalid)
        throw new OutOfMemoryException();
      SafeBSTRHandle.Copy(this.m_buffer, target);
      this.m_buffer.Close();
      this.m_buffer = target;
    }

    [SecurityCritical]
    private void EnsureNotDisposed()
    {
      if (this.m_buffer == null)
        throw new ObjectDisposedException((string) null);
    }

    private void EnsureNotReadOnly()
    {
      if (this.m_readOnly)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
    }

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    private static uint GetAlignedSize(int size)
    {
      uint num = (uint) size / 8U * 8U;
      if (size % 8 != 0 || size == 0)
        num += 8U;
      return num;
    }

    [SecurityCritical]
    private unsafe int GetAnsiByteCount()
    {
      uint flags = 1024;
      uint num = 63;
      byte* pointer = (byte*) null;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this.m_buffer.AcquirePointer(ref pointer);
        return Win32Native.WideCharToMultiByte(0U, flags, (char*) pointer, this.m_length, (byte*) null, 0, IntPtr.Zero, new IntPtr((void*) &num));
      }
      finally
      {
        if ((IntPtr) pointer != IntPtr.Zero)
          this.m_buffer.ReleasePointer();
      }
    }

    [SecurityCritical]
    private unsafe void GetAnsiBytes(byte* ansiStrPtr, int byteCount)
    {
      uint flags = 1024;
      uint num = 63;
      byte* pointer = (byte*) null;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this.m_buffer.AcquirePointer(ref pointer);
        Win32Native.WideCharToMultiByte(0U, flags, (char*) pointer, this.m_length, ansiStrPtr, byteCount - 1, IntPtr.Zero, new IntPtr((void*) &num));
        *(ansiStrPtr + byteCount - 1) = (byte) 0;
      }
      finally
      {
        if ((IntPtr) pointer != IntPtr.Zero)
          this.m_buffer.ReleasePointer();
      }
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
    private void ProtectMemory()
    {
      if (this.m_length == 0 || this.m_encrypted)
        return;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
      }
      finally
      {
        int status = Win32Native.SystemFunction040(this.m_buffer, (uint) (this.m_buffer.Length * 2), 0U);
        if (status < 0)
          throw new CryptographicException(Win32Native.LsaNtStatusToWinError(status));
        this.m_encrypted = true;
      }
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [HandleProcessCorruptedStateExceptions]
    [MethodImpl(MethodImplOptions.Synchronized)]
    internal unsafe IntPtr ToBSTR()
    {
      this.EnsureNotDisposed();
      int len = this.m_length;
      IntPtr num1 = IntPtr.Zero;
      IntPtr num2 = IntPtr.Zero;
      byte* pointer = (byte*) null;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        RuntimeHelpers.PrepareConstrainedRegions();
        try
        {
        }
        finally
        {
          num1 = Win32Native.SysAllocStringLen((string) null, len);
        }
        if (num1 == IntPtr.Zero)
          throw new OutOfMemoryException();
        this.UnProtectMemory();
        this.m_buffer.AcquirePointer(ref pointer);
        Buffer.Memcpy((byte*) num1.ToPointer(), pointer, len * 2);
        num2 = num1;
        return num2;
      }
      catch (Exception ex)
      {
        this.ProtectMemory();
        throw;
      }
      finally
      {
        this.ProtectMemory();
        if (num2 == IntPtr.Zero && num1 != IntPtr.Zero)
        {
          Win32Native.ZeroMemory(num1, (UIntPtr) ((ulong) (len * 2)));
          Win32Native.SysFreeString(num1);
        }
        if ((IntPtr) pointer != IntPtr.Zero)
          this.m_buffer.ReleasePointer();
      }
    }

    [SecurityCritical]
    [HandleProcessCorruptedStateExceptions]
    [MethodImpl(MethodImplOptions.Synchronized)]
    internal unsafe IntPtr ToUniStr(bool allocateFromHeap)
    {
      this.EnsureNotDisposed();
      int num1 = this.m_length;
      IntPtr num2 = IntPtr.Zero;
      IntPtr num3 = IntPtr.Zero;
      byte* pointer = (byte*) null;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        RuntimeHelpers.PrepareConstrainedRegions();
        try
        {
        }
        finally
        {
          num2 = !allocateFromHeap ? Marshal.AllocCoTaskMem((num1 + 1) * 2) : Marshal.AllocHGlobal((num1 + 1) * 2);
        }
        if (num2 == IntPtr.Zero)
          throw new OutOfMemoryException();
        this.UnProtectMemory();
        this.m_buffer.AcquirePointer(ref pointer);
        Buffer.Memcpy((byte*) num2.ToPointer(), pointer, num1 * 2);
        *(short*) ((IntPtr) num2.ToPointer() + (IntPtr) num1 * 2) = (short) 0;
        num3 = num2;
        return num3;
      }
      catch (Exception ex)
      {
        this.ProtectMemory();
        throw;
      }
      finally
      {
        this.ProtectMemory();
        if (num3 == IntPtr.Zero && num2 != IntPtr.Zero)
        {
          Win32Native.ZeroMemory(num2, (UIntPtr) ((ulong) (num1 * 2)));
          if (allocateFromHeap)
            Marshal.FreeHGlobal(num2);
          else
            Marshal.FreeCoTaskMem(num2);
        }
        if ((IntPtr) pointer != IntPtr.Zero)
          this.m_buffer.ReleasePointer();
      }
    }

    [SecurityCritical]
    [HandleProcessCorruptedStateExceptions]
    [MethodImpl(MethodImplOptions.Synchronized)]
    internal unsafe IntPtr ToAnsiStr(bool allocateFromHeap)
    {
      this.EnsureNotDisposed();
      IntPtr num1 = IntPtr.Zero;
      IntPtr num2 = IntPtr.Zero;
      int num3 = 0;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this.UnProtectMemory();
        num3 = this.GetAnsiByteCount() + 1;
        RuntimeHelpers.PrepareConstrainedRegions();
        try
        {
        }
        finally
        {
          num1 = !allocateFromHeap ? Marshal.AllocCoTaskMem(num3) : Marshal.AllocHGlobal(num3);
        }
        if (num1 == IntPtr.Zero)
          throw new OutOfMemoryException();
        this.GetAnsiBytes((byte*) num1.ToPointer(), num3);
        num2 = num1;
        return num2;
      }
      catch (Exception ex)
      {
        this.ProtectMemory();
        throw;
      }
      finally
      {
        this.ProtectMemory();
        if (num2 == IntPtr.Zero && num1 != IntPtr.Zero)
        {
          Win32Native.ZeroMemory(num1, (UIntPtr) ((ulong) num3));
          if (allocateFromHeap)
            Marshal.FreeHGlobal(num1);
          else
            Marshal.FreeCoTaskMem(num1);
        }
      }
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    private void UnProtectMemory()
    {
      if (this.m_length == 0)
        return;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
      }
      finally
      {
        if (this.m_encrypted)
        {
          int status = Win32Native.SystemFunction041(this.m_buffer, (uint) (this.m_buffer.Length * 2), 0U);
          if (status < 0)
            throw new CryptographicException(Win32Native.LsaNtStatusToWinError(status));
          this.m_encrypted = false;
        }
      }
    }
  }
}
