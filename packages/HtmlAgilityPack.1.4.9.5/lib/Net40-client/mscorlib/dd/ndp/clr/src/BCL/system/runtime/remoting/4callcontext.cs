// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Messaging.CallContextRemotingData
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.Remoting.Messaging
{
  [Serializable]
  internal class CallContextRemotingData : ICloneable
  {
    private string _logicalCallID;

    internal string LogicalCallID
    {
      get
      {
        return this._logicalCallID;
      }
      set
      {
        this._logicalCallID = value;
      }
    }

    internal bool HasInfo
    {
      get
      {
        return this._logicalCallID != null;
      }
    }

    public object Clone()
    {
      return (object) new CallContextRemotingData() { LogicalCallID = this.LogicalCallID };
    }
  }
}
