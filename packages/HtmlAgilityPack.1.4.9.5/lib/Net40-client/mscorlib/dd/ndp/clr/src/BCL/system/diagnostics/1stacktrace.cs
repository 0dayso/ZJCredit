// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.StackTrace
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;

namespace System.Diagnostics
{
  /// <summary>表示一个堆栈跟踪，它是一个或多个堆栈帧的有序集合。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [Serializable]
  [SecurityPermission(SecurityAction.InheritanceDemand, UnmanagedCode = true)]
  public class StackTrace
  {
    private StackFrame[] frames;
    private int m_iNumOfFrames;
    /// <summary>定义要从堆栈跟踪中省略的默认方法数。此字段为常数。</summary>
    /// <filterpriority>1</filterpriority>
    public const int METHODS_TO_SKIP = 0;
    private int m_iMethodsToSkip;

    /// <summary>获取堆栈跟踪中的帧数。</summary>
    /// <returns>堆栈跟踪中的帧数。</returns>
    /// <filterpriority>2</filterpriority>
    public virtual int FrameCount
    {
      get
      {
        return this.m_iNumOfFrames;
      }
    }

    /// <summary>用调用方的帧初始化 <see cref="T:System.Diagnostics.StackTrace" /> 类的新实例。</summary>
    public StackTrace()
    {
      this.m_iNumOfFrames = 0;
      this.m_iMethodsToSkip = 0;
      this.CaptureStackTrace(0, false, (Thread) null, (Exception) null);
    }

    /// <summary>用调用方的帧初始化 <see cref="T:System.Diagnostics.StackTrace" /> 类的新实例，可以选择捕获源信息。</summary>
    /// <param name="fNeedFileInfo">如果为 true，则捕获文件名、行号和列号；否则为 false。</param>
    public StackTrace(bool fNeedFileInfo)
    {
      this.m_iNumOfFrames = 0;
      this.m_iMethodsToSkip = 0;
      this.CaptureStackTrace(0, fNeedFileInfo, (Thread) null, (Exception) null);
    }

    /// <summary>从调用方的帧初始化 <see cref="T:System.Diagnostics.StackTrace" /> 类的新实例，跳过指定的帧数。</summary>
    /// <param name="skipFrames">堆栈中的帧数，将从其上开始跟踪。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="skipFrames" /> 参数为负数。</exception>
    public StackTrace(int skipFrames)
    {
      if (skipFrames < 0)
        throw new ArgumentOutOfRangeException("skipFrames", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      this.m_iNumOfFrames = 0;
      this.m_iMethodsToSkip = 0;
      this.CaptureStackTrace(skipFrames + 0, false, (Thread) null, (Exception) null);
    }

    /// <summary>从调用方的帧初始化 <see cref="T:System.Diagnostics.StackTrace" /> 类的新实例，跳过指定的帧数并可以选择捕获源信息。</summary>
    /// <param name="skipFrames">堆栈中的帧数，将从其上开始跟踪。</param>
    /// <param name="fNeedFileInfo">如果为 true，则捕获文件名、行号和列号；否则为 false。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="skipFrames" /> 参数为负数。</exception>
    public StackTrace(int skipFrames, bool fNeedFileInfo)
    {
      if (skipFrames < 0)
        throw new ArgumentOutOfRangeException("skipFrames", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      this.m_iNumOfFrames = 0;
      this.m_iMethodsToSkip = 0;
      this.CaptureStackTrace(skipFrames + 0, fNeedFileInfo, (Thread) null, (Exception) null);
    }

    /// <summary>使用提供的异常对象初始化 <see cref="T:System.Diagnostics.StackTrace" /> 类的新实例。</summary>
    /// <param name="e">从其构造堆栈跟踪的异常对象。</param>
    /// <exception cref="T:System.ArgumentNullException">参数 <paramref name="e" /> 为 null。</exception>
    public StackTrace(Exception e)
    {
      if (e == null)
        throw new ArgumentNullException("e");
      this.m_iNumOfFrames = 0;
      this.m_iMethodsToSkip = 0;
      this.CaptureStackTrace(0, false, (Thread) null, e);
    }

    /// <summary>使用所提供的异常对象初始化 <see cref="T:System.Diagnostics.StackTrace" /> 类的新实例，可以选择捕获源信息。</summary>
    /// <param name="e">从其构造堆栈跟踪的异常对象。</param>
    /// <param name="fNeedFileInfo">如果为 true，则捕获文件名、行号和列号；否则为 false。</param>
    /// <exception cref="T:System.ArgumentNullException">参数 <paramref name="e" /> 为 null。</exception>
    public StackTrace(Exception e, bool fNeedFileInfo)
    {
      if (e == null)
        throw new ArgumentNullException("e");
      this.m_iNumOfFrames = 0;
      this.m_iMethodsToSkip = 0;
      this.CaptureStackTrace(0, fNeedFileInfo, (Thread) null, e);
    }

    /// <summary>使用提供的异常对象初始化 <see cref="T:System.Diagnostics.StackTrace" /> 类的新实例，并跳过指定的帧数。</summary>
    /// <param name="e">从其构造堆栈跟踪的异常对象。</param>
    /// <param name="skipFrames">堆栈中的帧数，将从其上开始跟踪。</param>
    /// <exception cref="T:System.ArgumentNullException">参数 <paramref name="e" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="skipFrames" /> 参数为负数。</exception>
    public StackTrace(Exception e, int skipFrames)
    {
      if (e == null)
        throw new ArgumentNullException("e");
      if (skipFrames < 0)
        throw new ArgumentOutOfRangeException("skipFrames", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      this.m_iNumOfFrames = 0;
      this.m_iMethodsToSkip = 0;
      this.CaptureStackTrace(skipFrames + 0, false, (Thread) null, e);
    }

    /// <summary>使用提供的异常对象初始化 <see cref="T:System.Diagnostics.StackTrace" /> 类的新实例，跳过指定的帧数并可以选择捕获源信息。</summary>
    /// <param name="e">从其构造堆栈跟踪的异常对象。</param>
    /// <param name="skipFrames">堆栈中的帧数，将从其上开始跟踪。</param>
    /// <param name="fNeedFileInfo">如果为 true，则捕获文件名、行号和列号；否则为 false。</param>
    /// <exception cref="T:System.ArgumentNullException">参数 <paramref name="e" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="skipFrames" /> 参数为负数。</exception>
    public StackTrace(Exception e, int skipFrames, bool fNeedFileInfo)
    {
      if (e == null)
        throw new ArgumentNullException("e");
      if (skipFrames < 0)
        throw new ArgumentOutOfRangeException("skipFrames", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      this.m_iNumOfFrames = 0;
      this.m_iMethodsToSkip = 0;
      this.CaptureStackTrace(skipFrames + 0, fNeedFileInfo, (Thread) null, e);
    }

    /// <summary>初始化包含单个帧的 <see cref="T:System.Diagnostics.StackTrace" /> 类的新实例。</summary>
    /// <param name="frame">
    /// <see cref="T:System.Diagnostics.StackTrace" /> 对象应包含的帧。</param>
    public StackTrace(StackFrame frame)
    {
      this.frames = new StackFrame[1];
      this.frames[0] = frame;
      this.m_iMethodsToSkip = 0;
      this.m_iNumOfFrames = 1;
    }

    /// <summary>初始化特定线程的 <see cref="T:System.Diagnostics.StackTrace" /> 类的新实例，可以选择捕获源信息。不要使用此构造函数重载。</summary>
    /// <param name="targetThread">请求其堆栈跟踪的线程。</param>
    /// <param name="needFileInfo">如果为 true，则捕获文件名、行号和列号；否则为 false。</param>
    /// <exception cref="T:System.Threading.ThreadStateException">
    /// <paramref name="targetThread" /> 线程未暂停。</exception>
    [Obsolete("This constructor has been deprecated.  Please use a constructor that does not require a Thread parameter.  http://go.microsoft.com/fwlink/?linkid=14202")]
    public StackTrace(Thread targetThread, bool needFileInfo)
    {
      this.m_iNumOfFrames = 0;
      this.m_iMethodsToSkip = 0;
      this.CaptureStackTrace(0, needFileInfo, targetThread, (Exception) null);
    }

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void GetStackFramesInternal(StackFrameHelper sfh, int iSkip, Exception e);

    internal static int CalculateFramesToSkip(StackFrameHelper StackF, int iNumFrames)
    {
      int num = 0;
      string strB = "System.Diagnostics";
      for (int i = 0; i < iNumFrames; ++i)
      {
        MethodBase methodBase = StackF.GetMethodBase(i);
        if (methodBase != (MethodBase) null)
        {
          Type declaringType = methodBase.DeclaringType;
          if (!(declaringType == (Type) null))
          {
            string @namespace = declaringType.Namespace;
            if (@namespace == null || string.Compare(@namespace, strB, StringComparison.Ordinal) != 0)
              break;
          }
          else
            break;
        }
        ++num;
      }
      return num;
    }

    private void CaptureStackTrace(int iSkip, bool fNeedFileInfo, Thread targetThread, Exception e)
    {
      this.m_iMethodsToSkip = this.m_iMethodsToSkip + iSkip;
      StackFrameHelper stackFrameHelper = new StackFrameHelper(fNeedFileInfo, targetThread);
      StackTrace.GetStackFramesInternal(stackFrameHelper, 0, e);
      this.m_iNumOfFrames = stackFrameHelper.GetNumberOfFrames();
      if (this.m_iMethodsToSkip > this.m_iNumOfFrames)
        this.m_iMethodsToSkip = this.m_iNumOfFrames;
      if (this.m_iNumOfFrames != 0)
      {
        this.frames = new StackFrame[this.m_iNumOfFrames];
        for (int i = 0; i < this.m_iNumOfFrames; ++i)
        {
          StackFrame stackFrame = new StackFrame(true, true);
          stackFrame.SetMethodBase(stackFrameHelper.GetMethodBase(i));
          stackFrame.SetOffset(stackFrameHelper.GetOffset(i));
          stackFrame.SetILOffset(stackFrameHelper.GetILOffset(i));
          stackFrame.SetIsLastFrameFromForeignExceptionStackTrace(stackFrameHelper.IsLastFrameFromForeignExceptionStackTrace(i));
          if (fNeedFileInfo)
          {
            stackFrame.SetFileName(stackFrameHelper.GetFilename(i));
            stackFrame.SetLineNumber(stackFrameHelper.GetLineNumber(i));
            stackFrame.SetColumnNumber(stackFrameHelper.GetColumnNumber(i));
          }
          this.frames[i] = stackFrame;
        }
        if (e == null)
          this.m_iMethodsToSkip = this.m_iMethodsToSkip + StackTrace.CalculateFramesToSkip(stackFrameHelper, this.m_iNumOfFrames);
        this.m_iNumOfFrames = this.m_iNumOfFrames - this.m_iMethodsToSkip;
        if (this.m_iNumOfFrames >= 0)
          return;
        this.m_iNumOfFrames = 0;
      }
      else
        this.frames = (StackFrame[]) null;
    }

    /// <summary>获取指定的堆栈帧。</summary>
    /// <returns>指定的堆栈帧。</returns>
    /// <param name="index">所请求的堆栈帧的索引。</param>
    /// <filterpriority>2</filterpriority>
    public virtual StackFrame GetFrame(int index)
    {
      if (this.frames != null && index < this.m_iNumOfFrames && index >= 0)
        return this.frames[index + this.m_iMethodsToSkip];
      return (StackFrame) null;
    }

    /// <summary>返回当前堆栈跟踪中所有堆栈帧的副本。</summary>
    /// <returns>
    /// <see cref="T:System.Diagnostics.StackFrame" /> 类型的数组，表示堆栈跟踪中的函数调用。</returns>
    /// <filterpriority>2</filterpriority>
    [ComVisible(false)]
    public virtual StackFrame[] GetFrames()
    {
      if (this.frames == null || this.m_iNumOfFrames <= 0)
        return (StackFrame[]) null;
      StackFrame[] stackFrameArray = new StackFrame[this.m_iNumOfFrames];
      Array.Copy((Array) this.frames, this.m_iMethodsToSkip, (Array) stackFrameArray, 0, this.m_iNumOfFrames);
      return stackFrameArray;
    }

    /// <summary>生成堆栈跟踪的可读表示形式。</summary>
    /// <returns>堆栈帧的可读表示形式。</returns>
    /// <filterpriority>2</filterpriority>
    public override string ToString()
    {
      return this.ToString(StackTrace.TraceFormat.TrailingNewLine);
    }

    internal string ToString(StackTrace.TraceFormat traceFormat)
    {
      bool flag1 = true;
      string str1 = "at";
      string format = "in {0}:line {1}";
      if (traceFormat != StackTrace.TraceFormat.NoResourceLookup)
      {
        str1 = Environment.GetResourceString("Word_At");
        format = Environment.GetResourceString("StackTrace_InFileLineNumber");
      }
      bool flag2 = true;
      StringBuilder stringBuilder = new StringBuilder((int) byte.MaxValue);
      for (int index1 = 0; index1 < this.m_iNumOfFrames; ++index1)
      {
        StackFrame frame = this.GetFrame(index1);
        MethodBase method = frame.GetMethod();
        if (method != (MethodBase) null)
        {
          if (flag2)
            flag2 = false;
          else
            stringBuilder.Append(Environment.NewLine);
          stringBuilder.AppendFormat((IFormatProvider) CultureInfo.InvariantCulture, "   {0} ", (object) str1);
          Type declaringType = method.DeclaringType;
          if (declaringType != (Type) null)
          {
            stringBuilder.Append(declaringType.FullName.Replace('+', '.'));
            stringBuilder.Append(".");
          }
          stringBuilder.Append(method.Name);
          if (method is MethodInfo && method.IsGenericMethod)
          {
            Type[] genericArguments = method.GetGenericArguments();
            stringBuilder.Append("[");
            int index2 = 0;
            bool flag3 = true;
            for (; index2 < genericArguments.Length; ++index2)
            {
              if (!flag3)
                stringBuilder.Append(",");
              else
                flag3 = false;
              stringBuilder.Append(genericArguments[index2].Name);
            }
            stringBuilder.Append("]");
          }
          stringBuilder.Append("(");
          ParameterInfo[] parameters = method.GetParameters();
          bool flag4 = true;
          for (int index2 = 0; index2 < parameters.Length; ++index2)
          {
            if (!flag4)
              stringBuilder.Append(", ");
            else
              flag4 = false;
            string str2 = "<UnknownType>";
            if (parameters[index2].ParameterType != (Type) null)
              str2 = parameters[index2].ParameterType.Name;
            stringBuilder.Append(str2 + " " + parameters[index2].Name);
          }
          stringBuilder.Append(")");
          if (flag1 && frame.GetILOffset() != -1)
          {
            string str2 = (string) null;
            try
            {
              str2 = frame.GetFileName();
            }
            catch (NotSupportedException ex)
            {
              flag1 = false;
            }
            catch (SecurityException ex)
            {
              flag1 = false;
            }
            if (str2 != null)
            {
              stringBuilder.Append(' ');
              stringBuilder.AppendFormat((IFormatProvider) CultureInfo.InvariantCulture, format, (object) str2, (object) frame.GetFileLineNumber());
            }
          }
          if (frame.GetIsLastFrameFromForeignExceptionStackTrace())
          {
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(Environment.GetResourceString("Exception_EndStackTraceFromPreviousThrow"));
          }
        }
      }
      if (traceFormat == StackTrace.TraceFormat.TrailingNewLine)
        stringBuilder.Append(Environment.NewLine);
      return stringBuilder.ToString();
    }

    private static string GetManagedStackTraceStringHelper(bool fNeedFileInfo)
    {
      return new StackTrace(0, fNeedFileInfo).ToString();
    }

    internal enum TraceFormat
    {
      Normal,
      TrailingNewLine,
      NoResourceLookup,
    }
  }
}
