// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Channels.CrossAppDomainSerializer
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.IO;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
  internal static class CrossAppDomainSerializer
  {
    [SecurityCritical]
    internal static MemoryStream SerializeMessage(IMessage msg)
    {
      MemoryStream memoryStream1 = new MemoryStream();
      RemotingSurrogateSelector surrogateSelector = new RemotingSurrogateSelector();
      BinaryFormatter binaryFormatter = new BinaryFormatter();
      binaryFormatter.SurrogateSelector = (ISurrogateSelector) surrogateSelector;
      StreamingContext streamingContext = new StreamingContext(StreamingContextStates.CrossAppDomain);
      binaryFormatter.Context = streamingContext;
      MemoryStream memoryStream2 = memoryStream1;
      IMessage message = msg;
      // ISSUE: variable of the null type
      __Null local = null;
      int num = 0;
      binaryFormatter.Serialize((Stream) memoryStream2, (object) message, (Header[]) local, num != 0);
      memoryStream1.Position = 0L;
      return memoryStream1;
    }

    [SecurityCritical]
    internal static MemoryStream SerializeMessageParts(ArrayList argsToSerialize)
    {
      MemoryStream memoryStream1 = new MemoryStream();
      BinaryFormatter binaryFormatter = new BinaryFormatter();
      binaryFormatter.SurrogateSelector = (ISurrogateSelector) new RemotingSurrogateSelector();
      binaryFormatter.Context = new StreamingContext(StreamingContextStates.CrossAppDomain);
      MemoryStream memoryStream2 = memoryStream1;
      ArrayList arrayList = argsToSerialize;
      // ISSUE: variable of the null type
      __Null local = null;
      int num = 0;
      binaryFormatter.Serialize((Stream) memoryStream2, (object) arrayList, (Header[]) local, num != 0);
      memoryStream1.Position = 0L;
      return memoryStream1;
    }

    [SecurityCritical]
    internal static void SerializeObject(object obj, MemoryStream stm)
    {
      BinaryFormatter binaryFormatter = new BinaryFormatter();
      binaryFormatter.SurrogateSelector = (ISurrogateSelector) new RemotingSurrogateSelector();
      binaryFormatter.Context = new StreamingContext(StreamingContextStates.CrossAppDomain);
      MemoryStream memoryStream = stm;
      object graph = obj;
      // ISSUE: variable of the null type
      __Null local = null;
      int num = 0;
      binaryFormatter.Serialize((Stream) memoryStream, graph, (Header[]) local, num != 0);
    }

    [SecurityCritical]
    internal static MemoryStream SerializeObject(object obj)
    {
      MemoryStream stm = new MemoryStream();
      CrossAppDomainSerializer.SerializeObject(obj, stm);
      stm.Position = 0L;
      return stm;
    }

    [SecurityCritical]
    internal static IMessage DeserializeMessage(MemoryStream stm)
    {
      return CrossAppDomainSerializer.DeserializeMessage(stm, (IMethodCallMessage) null);
    }

    [SecurityCritical]
    internal static IMessage DeserializeMessage(MemoryStream stm, IMethodCallMessage reqMsg)
    {
      if (stm == null)
        throw new ArgumentNullException("stm");
      stm.Position = 0L;
      BinaryFormatter binaryFormatter = new BinaryFormatter();
      binaryFormatter.SurrogateSelector = (ISurrogateSelector) null;
      StreamingContext streamingContext = new StreamingContext(StreamingContextStates.CrossAppDomain);
      binaryFormatter.Context = streamingContext;
      MemoryStream memoryStream = stm;
      // ISSUE: variable of the null type
      __Null local = null;
      int num1 = 0;
      int num2 = 1;
      IMethodCallMessage methodCallMessage = reqMsg;
      return (IMessage) binaryFormatter.Deserialize((Stream) memoryStream, (HeaderHandler) local, num1 != 0, num2 != 0, methodCallMessage);
    }

    [SecurityCritical]
    internal static ArrayList DeserializeMessageParts(MemoryStream stm)
    {
      return (ArrayList) CrossAppDomainSerializer.DeserializeObject(stm);
    }

    [SecurityCritical]
    internal static object DeserializeObject(MemoryStream stm)
    {
      stm.Position = 0L;
      BinaryFormatter binaryFormatter = new BinaryFormatter();
      binaryFormatter.Context = new StreamingContext(StreamingContextStates.CrossAppDomain);
      MemoryStream memoryStream = stm;
      // ISSUE: variable of the null type
      __Null local1 = null;
      int num1 = 0;
      int num2 = 1;
      // ISSUE: variable of the null type
      __Null local2 = null;
      return binaryFormatter.Deserialize((Stream) memoryStream, (HeaderHandler) local1, num1 != 0, num2 != 0, (IMethodCallMessage) local2);
    }
  }
}
