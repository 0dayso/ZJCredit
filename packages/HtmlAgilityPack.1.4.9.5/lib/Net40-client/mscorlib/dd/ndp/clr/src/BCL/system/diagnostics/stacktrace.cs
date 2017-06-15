// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.StackFrameHelper
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;
using System.Runtime.Serialization;
using System.Security;
using System.Threading;

namespace System.Diagnostics
{
  [Serializable]
  internal class StackFrameHelper
  {
    [NonSerialized]
    private Thread targetThread;
    private int[] rgiOffset;
    private int[] rgiILOffset;
    private MethodBase[] rgMethodBase;
    private object dynamicMethods;
    [NonSerialized]
    private IntPtr[] rgMethodHandle;
    private string[] rgFilename;
    private int[] rgiLineNumber;
    private int[] rgiColumnNumber;
    [OptionalField]
    private bool[] rgiLastFrameFromForeignExceptionStackTrace;
    private int iFrameCount;
    private bool fNeedFileInfo;

    public StackFrameHelper(bool fNeedFileLineColInfo, Thread target)
    {
      this.targetThread = target;
      this.rgMethodBase = (MethodBase[]) null;
      this.rgMethodHandle = (IntPtr[]) null;
      this.rgiOffset = (int[]) null;
      this.rgiILOffset = (int[]) null;
      this.rgFilename = (string[]) null;
      this.rgiLineNumber = (int[]) null;
      this.rgiColumnNumber = (int[]) null;
      this.dynamicMethods = (object) null;
      this.rgiLastFrameFromForeignExceptionStackTrace = (bool[]) null;
      this.iFrameCount = 0;
      this.fNeedFileInfo = fNeedFileLineColInfo;
    }

    [SecuritySafeCritical]
    public virtual MethodBase GetMethodBase(int i)
    {
      IntPtr methodHandleValue = this.rgMethodHandle[i];
      if (methodHandleValue.IsNull())
        return (MethodBase) null;
      return RuntimeType.GetMethodBase(RuntimeMethodHandle.GetTypicalMethodDefinition((IRuntimeMethodInfo) new RuntimeMethodInfoStub(methodHandleValue, (object) this)));
    }

    public virtual int GetOffset(int i)
    {
      return this.rgiOffset[i];
    }

    public virtual int GetILOffset(int i)
    {
      return this.rgiILOffset[i];
    }

    public virtual string GetFilename(int i)
    {
      return this.rgFilename[i];
    }

    public virtual int GetLineNumber(int i)
    {
      return this.rgiLineNumber[i];
    }

    public virtual int GetColumnNumber(int i)
    {
      return this.rgiColumnNumber[i];
    }

    public virtual bool IsLastFrameFromForeignExceptionStackTrace(int i)
    {
      if (this.rgiLastFrameFromForeignExceptionStackTrace != null)
        return this.rgiLastFrameFromForeignExceptionStackTrace[i];
      return false;
    }

    public virtual int GetNumberOfFrames()
    {
      return this.iFrameCount;
    }

    public virtual void SetNumberOfFrames(int i)
    {
      this.iFrameCount = i;
    }

    [OnSerializing]
    [SecuritySafeCritical]
    private void OnSerializing(StreamingContext context)
    {
      this.rgMethodBase = this.rgMethodHandle == null ? (MethodBase[]) null : new MethodBase[this.rgMethodHandle.Length];
      if (this.rgMethodHandle == null)
        return;
      for (int index = 0; index < this.rgMethodHandle.Length; ++index)
      {
        if (!this.rgMethodHandle[index].IsNull())
          this.rgMethodBase[index] = RuntimeType.GetMethodBase((IRuntimeMethodInfo) new RuntimeMethodInfoStub(this.rgMethodHandle[index], (object) this));
      }
    }

    [OnSerialized]
    private void OnSerialized(StreamingContext context)
    {
      this.rgMethodBase = (MethodBase[]) null;
    }

    [OnDeserialized]
    [SecuritySafeCritical]
    private void OnDeserialized(StreamingContext context)
    {
      this.rgMethodHandle = this.rgMethodBase == null ? (IntPtr[]) null : new IntPtr[this.rgMethodBase.Length];
      if (this.rgMethodBase != null)
      {
        for (int index = 0; index < this.rgMethodBase.Length; ++index)
        {
          if (this.rgMethodBase[index] != (MethodBase) null)
            this.rgMethodHandle[index] = this.rgMethodBase[index].MethodHandle.Value;
        }
      }
      this.rgMethodBase = (MethodBase[]) null;
    }
  }
}
