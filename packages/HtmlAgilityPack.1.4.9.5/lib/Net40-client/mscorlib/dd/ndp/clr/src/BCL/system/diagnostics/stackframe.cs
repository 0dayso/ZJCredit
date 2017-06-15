// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.StackFrame
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;

namespace System.Diagnostics
{
  /// <summary>提供关于 <see cref="T:System.Diagnostics.StackFrame" />（表示当前线程的调用堆栈中的一个函数调用）的信息。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [Serializable]
  [SecurityPermission(SecurityAction.InheritanceDemand, UnmanagedCode = true)]
  public class StackFrame
  {
    private MethodBase method;
    private int offset;
    private int ILOffset;
    private string strFileName;
    private int iLineNumber;
    private int iColumnNumber;
    [OptionalField]
    private bool fIsLastFrameFromForeignExceptionStackTrace;
    /// <summary>定义当本机或 Microsoft 中间语言 (Microsoft Intermediate Language, MSIL) 偏移量未知时从 <see cref="M:System.Diagnostics.StackFrame.GetNativeOffset" /> 或 <see cref="M:System.Diagnostics.StackFrame.GetILOffset" /> 方法返回的值。此字段为常数。</summary>
    /// <filterpriority>1</filterpriority>
    public const int OFFSET_UNKNOWN = -1;

    /// <summary>初始化 <see cref="T:System.Diagnostics.StackFrame" /> 类的新实例。</summary>
    public StackFrame()
    {
      this.InitMembers();
      this.BuildStackFrame(0, false);
    }

    /// <summary>初始化 <see cref="T:System.Diagnostics.StackFrame" /> 类的新实例，可以选择捕获源信息。</summary>
    /// <param name="fNeedFileInfo">若要捕获堆栈帧的文件名、行号和列号，则为 true；否则为 false。</param>
    public StackFrame(bool fNeedFileInfo)
    {
      this.InitMembers();
      this.BuildStackFrame(0, fNeedFileInfo);
    }

    /// <summary>初始化与当前堆栈帧之上的帧对应的 <see cref="T:System.Diagnostics.StackFrame" /> 类的新实例。</summary>
    /// <param name="skipFrames">堆栈上要跳过的帧数。</param>
    public StackFrame(int skipFrames)
    {
      this.InitMembers();
      this.BuildStackFrame(skipFrames + 0, false);
    }

    /// <summary>初始化与当前堆栈帧之上的帧对应的 <see cref="T:System.Diagnostics.StackFrame" /> 类的新实例，可以选择捕获源信息。</summary>
    /// <param name="skipFrames">堆栈上要跳过的帧数。 </param>
    /// <param name="fNeedFileInfo">若要捕获堆栈帧的文件名、行号和列号，则为 true；否则为 false。</param>
    public StackFrame(int skipFrames, bool fNeedFileInfo)
    {
      this.InitMembers();
      this.BuildStackFrame(skipFrames + 0, fNeedFileInfo);
    }

    internal StackFrame(bool DummyFlag1, bool DummyFlag2)
    {
      this.InitMembers();
    }

    /// <summary>初始化只包含给定文件名和行号的 <see cref="T:System.Diagnostics.StackFrame" /> 类的新实例。</summary>
    /// <param name="fileName">文件名。</param>
    /// <param name="lineNumber">指定文件中的行号。</param>
    public StackFrame(string fileName, int lineNumber)
    {
      this.InitMembers();
      this.BuildStackFrame(0, false);
      this.strFileName = fileName;
      this.iLineNumber = lineNumber;
      this.iColumnNumber = 0;
    }

    /// <summary>初始化只包含给定文件名、行号和列号的 <see cref="T:System.Diagnostics.StackFrame" /> 类的新实例。</summary>
    /// <param name="fileName">文件名。</param>
    /// <param name="lineNumber">指定文件中的行号。</param>
    /// <param name="colNumber">指定文件中的列号。</param>
    public StackFrame(string fileName, int lineNumber, int colNumber)
    {
      this.InitMembers();
      this.BuildStackFrame(0, false);
      this.strFileName = fileName;
      this.iLineNumber = lineNumber;
      this.iColumnNumber = colNumber;
    }

    internal void InitMembers()
    {
      this.method = (MethodBase) null;
      this.offset = -1;
      this.ILOffset = -1;
      this.strFileName = (string) null;
      this.iLineNumber = 0;
      this.iColumnNumber = 0;
      this.fIsLastFrameFromForeignExceptionStackTrace = false;
    }

    internal virtual void SetMethodBase(MethodBase mb)
    {
      this.method = mb;
    }

    internal virtual void SetOffset(int iOffset)
    {
      this.offset = iOffset;
    }

    internal virtual void SetILOffset(int iOffset)
    {
      this.ILOffset = iOffset;
    }

    internal virtual void SetFileName(string strFName)
    {
      this.strFileName = strFName;
    }

    internal virtual void SetLineNumber(int iLine)
    {
      this.iLineNumber = iLine;
    }

    internal virtual void SetColumnNumber(int iCol)
    {
      this.iColumnNumber = iCol;
    }

    internal virtual void SetIsLastFrameFromForeignExceptionStackTrace(bool fIsLastFrame)
    {
      this.fIsLastFrameFromForeignExceptionStackTrace = fIsLastFrame;
    }

    internal virtual bool GetIsLastFrameFromForeignExceptionStackTrace()
    {
      return this.fIsLastFrameFromForeignExceptionStackTrace;
    }

    /// <summary>获取在其中执行帧的方法。</summary>
    /// <returns>在其中执行帧的方法。</returns>
    /// <filterpriority>2</filterpriority>
    public virtual MethodBase GetMethod()
    {
      return this.method;
    }

    /// <summary>获取相对于所执行方法的本机实时 (JIT) 编译代码开头的偏移量。该调试信息的生成由 <see cref="T:System.Diagnostics.DebuggableAttribute" /> 类控制。</summary>
    /// <returns>相对于所执行方法的 JIT 编译代码开头的偏移量。</returns>
    /// <filterpriority>2</filterpriority>
    public virtual int GetNativeOffset()
    {
      return this.offset;
    }

    /// <summary>获取离开所执行方法的 Microsoft 中间语言 (Microsoft Intermediate Language, MSIL) 代码开头的偏移量。根据实时 (JIT) 编译器是否正在生成调试代码，此偏移量可能是近似量。该调试信息的生成受 <see cref="T:System.Diagnostics.DebuggableAttribute" /> 控制。</summary>
    /// <returns>离开所执行方法的 MSIL 代码开头的偏移量。</returns>
    /// <filterpriority>2</filterpriority>
    public virtual int GetILOffset()
    {
      return this.ILOffset;
    }

    /// <summary>获取包含所执行代码的文件名。该信息通常从可执行文件的调试符号中提取。</summary>
    /// <returns>文件名；如果无法确定文件名，则为 null。</returns>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public virtual string GetFileName()
    {
      if (this.strFileName != null)
        new FileIOPermission(PermissionState.None)
        {
          AllFiles = FileIOPermissionAccess.PathDiscovery
        }.Demand();
      return this.strFileName;
    }

    /// <summary>获取文件中包含所执行代码的行号。该信息通常从可执行文件的调试符号中提取。</summary>
    /// <returns>文件行号；如果无法确定文件行号，则为 0（零）。</returns>
    /// <filterpriority>2</filterpriority>
    public virtual int GetFileLineNumber()
    {
      return this.iLineNumber;
    }

    /// <summary>获取文件中包含所执行代码的列号。该信息通常从可执行文件的调试符号中提取。</summary>
    /// <returns>文件列号；如果无法确定文件列号，则为 0（零）。</returns>
    /// <filterpriority>2</filterpriority>
    public virtual int GetFileColumnNumber()
    {
      return this.iColumnNumber;
    }

    /// <summary>生成堆栈跟踪的可读表示形式。</summary>
    /// <returns>堆栈帧的可读表示形式。</returns>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder((int) byte.MaxValue);
      if (this.method != (MethodBase) null)
      {
        stringBuilder.Append(this.method.Name);
        if (this.method is MethodInfo && this.method.IsGenericMethod)
        {
          Type[] genericArguments = this.method.GetGenericArguments();
          stringBuilder.Append("<");
          int index = 0;
          bool flag = true;
          for (; index < genericArguments.Length; ++index)
          {
            if (!flag)
              stringBuilder.Append(",");
            else
              flag = false;
            stringBuilder.Append(genericArguments[index].Name);
          }
          stringBuilder.Append(">");
        }
        stringBuilder.Append(" at offset ");
        if (this.offset == -1)
          stringBuilder.Append("<offset unknown>");
        else
          stringBuilder.Append(this.offset);
        stringBuilder.Append(" in file:line:column ");
        bool flag1 = this.strFileName != null;
        if (flag1)
        {
          try
          {
            new FileIOPermission(PermissionState.None)
            {
              AllFiles = FileIOPermissionAccess.PathDiscovery
            }.Demand();
          }
          catch (SecurityException ex)
          {
            flag1 = false;
          }
        }
        if (!flag1)
          stringBuilder.Append("<filename unknown>");
        else
          stringBuilder.Append(this.strFileName);
        stringBuilder.Append(":");
        stringBuilder.Append(this.iLineNumber);
        stringBuilder.Append(":");
        stringBuilder.Append(this.iColumnNumber);
      }
      else
        stringBuilder.Append("<null>");
      stringBuilder.Append(Environment.NewLine);
      return stringBuilder.ToString();
    }

    private void BuildStackFrame(int skipFrames, bool fNeedFileInfo)
    {
      StackFrameHelper stackFrameHelper = new StackFrameHelper(fNeedFileInfo, (Thread) null);
      StackTrace.GetStackFramesInternal(stackFrameHelper, 0, (Exception) null);
      int numberOfFrames = stackFrameHelper.GetNumberOfFrames();
      skipFrames += StackTrace.CalculateFramesToSkip(stackFrameHelper, numberOfFrames);
      if (numberOfFrames - skipFrames <= 0)
        return;
      this.method = stackFrameHelper.GetMethodBase(skipFrames);
      this.offset = stackFrameHelper.GetOffset(skipFrames);
      this.ILOffset = stackFrameHelper.GetILOffset(skipFrames);
      if (!fNeedFileInfo)
        return;
      this.strFileName = stackFrameHelper.GetFilename(skipFrames);
      this.iLineNumber = stackFrameHelper.GetLineNumber(skipFrames);
      this.iColumnNumber = stackFrameHelper.GetColumnNumber(skipFrames);
    }
  }
}
