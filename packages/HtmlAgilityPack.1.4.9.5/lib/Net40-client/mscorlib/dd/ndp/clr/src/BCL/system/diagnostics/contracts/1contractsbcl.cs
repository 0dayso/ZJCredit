// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Contracts.ContractException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.Serialization;
using System.Security;

namespace System.Diagnostics.Contracts
{
  [Serializable]
  internal sealed class ContractException : Exception
  {
    private readonly ContractFailureKind _Kind;
    private readonly string _UserMessage;
    private readonly string _Condition;

    public ContractFailureKind Kind
    {
      get
      {
        return this._Kind;
      }
    }

    public string Failure
    {
      get
      {
        return this.Message;
      }
    }

    public string UserMessage
    {
      get
      {
        return this._UserMessage;
      }
    }

    public string Condition
    {
      get
      {
        return this._Condition;
      }
    }

    private ContractException()
    {
      this.HResult = -2146233022;
    }

    public ContractException(ContractFailureKind kind, string failure, string userMessage, string condition, Exception innerException)
      : base(failure, innerException)
    {
      this.HResult = -2146233022;
      this._Kind = kind;
      this._UserMessage = userMessage;
      this._Condition = condition;
    }

    private ContractException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
      this._Kind = (ContractFailureKind) info.GetInt32("Kind");
      this._UserMessage = info.GetString("UserMessage");
      this._Condition = info.GetString("Condition");
    }

    [SecurityCritical]
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      base.GetObjectData(info, context);
      info.AddValue("Kind", (object) this._Kind);
      info.AddValue("UserMessage", (object) this._UserMessage);
      info.AddValue("Condition", (object) this._Condition);
    }
  }
}
