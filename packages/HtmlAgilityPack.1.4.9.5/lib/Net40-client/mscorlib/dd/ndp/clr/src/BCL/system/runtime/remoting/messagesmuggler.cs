// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Messaging.MessageSmuggler
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Runtime.Remoting.Proxies;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
  internal class MessageSmuggler
  {
    private static bool CanSmuggleObjectDirectly(object obj)
    {
      return obj is string || obj.GetType() == typeof (void) || obj.GetType().IsPrimitive;
    }

    [SecurityCritical]
    protected static object[] FixupArgs(object[] args, ref ArrayList argsToSerialize)
    {
      object[] objArray = new object[args.Length];
      int length = args.Length;
      for (int index = 0; index < length; ++index)
        objArray[index] = MessageSmuggler.FixupArg(args[index], ref argsToSerialize);
      return objArray;
    }

    [SecurityCritical]
    protected static object FixupArg(object arg, ref ArrayList argsToSerialize)
    {
      if (arg == null)
        return (object) null;
      MarshalByRefObject marshalByRefObject = arg as MarshalByRefObject;
      if (marshalByRefObject != null)
      {
        if (!RemotingServices.IsTransparentProxy((object) marshalByRefObject) || RemotingServices.GetRealProxy((object) marshalByRefObject) is RemotingProxy)
        {
          ObjRef objRef = RemotingServices.MarshalInternal(marshalByRefObject, (string) null, (Type) null);
          if (objRef.CanSmuggle())
          {
            if (!RemotingServices.IsTransparentProxy((object) marshalByRefObject))
            {
              ServerIdentity serverIdentity = (ServerIdentity) MarshalByRefObject.GetIdentity(marshalByRefObject);
              serverIdentity.SetHandle();
              objRef.SetServerIdentity(serverIdentity.GetHandle());
              objRef.SetDomainID(AppDomain.CurrentDomain.GetId());
            }
            ObjRef smuggleableCopy = objRef.CreateSmuggleableCopy();
            smuggleableCopy.SetMarshaledObject();
            return (object) new SmuggledObjRef(smuggleableCopy);
          }
        }
        if (argsToSerialize == null)
          argsToSerialize = new ArrayList();
        int count = argsToSerialize.Count;
        argsToSerialize.Add(arg);
        return (object) new MessageSmuggler.SerializedArg(count);
      }
      if (MessageSmuggler.CanSmuggleObjectDirectly(arg))
        return arg;
      Array array = arg as Array;
      if (array != null)
      {
        Type elementType = array.GetType().GetElementType();
        if (elementType.IsPrimitive || elementType == typeof (string))
          return array.Clone();
      }
      if (argsToSerialize == null)
        argsToSerialize = new ArrayList();
      int count1 = argsToSerialize.Count;
      argsToSerialize.Add(arg);
      return (object) new MessageSmuggler.SerializedArg(count1);
    }

    [SecurityCritical]
    protected static object[] UndoFixupArgs(object[] args, ArrayList deserializedArgs)
    {
      object[] objArray = new object[args.Length];
      int length = args.Length;
      for (int index = 0; index < length; ++index)
        objArray[index] = MessageSmuggler.UndoFixupArg(args[index], deserializedArgs);
      return objArray;
    }

    [SecurityCritical]
    protected static object UndoFixupArg(object arg, ArrayList deserializedArgs)
    {
      SmuggledObjRef smuggledObjRef = arg as SmuggledObjRef;
      if (smuggledObjRef != null)
        return smuggledObjRef.ObjRef.GetRealObjectHelper();
      MessageSmuggler.SerializedArg serializedArg = arg as MessageSmuggler.SerializedArg;
      if (serializedArg != null)
        return deserializedArgs[serializedArg.Index];
      return arg;
    }

    [SecurityCritical]
    protected static int StoreUserPropertiesForMethodMessage(IMethodMessage msg, ref ArrayList argsToSerialize)
    {
      IDictionary properties = msg.Properties;
      MessageDictionary messageDictionary = properties as MessageDictionary;
      if (messageDictionary != null)
      {
        if (!messageDictionary.HasUserData())
          return 0;
        int num = 0;
        foreach (DictionaryEntry @internal in messageDictionary.InternalDictionary)
        {
          if (argsToSerialize == null)
            argsToSerialize = new ArrayList();
          argsToSerialize.Add((object) @internal);
          ++num;
        }
        return num;
      }
      int num1 = 0;
      foreach (DictionaryEntry dictionaryEntry in properties)
      {
        if (argsToSerialize == null)
          argsToSerialize = new ArrayList();
        argsToSerialize.Add((object) dictionaryEntry);
        ++num1;
      }
      return num1;
    }

    protected class SerializedArg
    {
      private int _index;

      public int Index
      {
        get
        {
          return this._index;
        }
      }

      public SerializedArg(int index)
      {
        this._index = index;
      }
    }
  }
}
