﻿// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Messaging.MessageDictionaryEnumerator
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;

namespace System.Runtime.Remoting.Messaging
{
  internal class MessageDictionaryEnumerator : IDictionaryEnumerator, IEnumerator
  {
    private int i = -1;
    private IDictionaryEnumerator _enumHash;
    private MessageDictionary _md;

    public object Key
    {
      get
      {
        if (this.i < 0)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_InternalState"));
        if (this.i < this._md._keys.Length)
          return (object) this._md._keys[this.i];
        return this._enumHash.Key;
      }
    }

    public object Value
    {
      get
      {
        if (this.i < 0)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_InternalState"));
        if (this.i < this._md._keys.Length)
          return this._md.GetMessageValue(this.i);
        return this._enumHash.Value;
      }
    }

    public object Current
    {
      get
      {
        return (object) this.Entry;
      }
    }

    public DictionaryEntry Entry
    {
      get
      {
        return new DictionaryEntry(this.Key, this.Value);
      }
    }

    public MessageDictionaryEnumerator(MessageDictionary md, IDictionary hashtable)
    {
      this._md = md;
      if (hashtable != null)
        this._enumHash = hashtable.GetEnumerator();
      else
        this._enumHash = (IDictionaryEnumerator) null;
    }

    public bool MoveNext()
    {
      if (this.i == -2)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_InternalState"));
      this.i = this.i + 1;
      if (this.i < this._md._keys.Length || this._enumHash != null && this._enumHash.MoveNext())
        return true;
      this.i = -2;
      return false;
    }

    public void Reset()
    {
      this.i = -1;
      if (this._enumHash == null)
        return;
      this._enumHash.Reset();
    }
  }
}
