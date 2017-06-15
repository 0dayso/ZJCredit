// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Contexts.DynamicPropertyHolder
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting.Contexts
{
  internal class DynamicPropertyHolder
  {
    private const int GROW_BY = 8;
    private IDynamicProperty[] _props;
    private int _numProps;
    private IDynamicMessageSink[] _sinks;

    internal virtual IDynamicProperty[] DynamicProperties
    {
      get
      {
        if (this._props == null)
          return (IDynamicProperty[]) null;
        lock (this)
        {
          IDynamicProperty[] local_2 = new IDynamicProperty[this._numProps];
          Array.Copy((Array) this._props, (Array) local_2, this._numProps);
          return local_2;
        }
      }
    }

    internal virtual ArrayWithSize DynamicSinks
    {
      [SecurityCritical] get
      {
        if (this._numProps == 0)
          return (ArrayWithSize) null;
        lock (this)
        {
          if (this._sinks == null)
          {
            this._sinks = new IDynamicMessageSink[this._numProps + 8];
            for (int local_2 = 0; local_2 < this._numProps; ++local_2)
              this._sinks[local_2] = ((IContributeDynamicSink) this._props[local_2]).GetDynamicSink();
          }
        }
        return new ArrayWithSize(this._sinks, this._numProps);
      }
    }

    [SecurityCritical]
    internal virtual bool AddDynamicProperty(IDynamicProperty prop)
    {
      lock (this)
      {
        DynamicPropertyHolder.CheckPropertyNameClash(prop.Name, this._props, this._numProps);
        bool local_2 = false;
        if (this._props == null || this._numProps == this._props.Length)
        {
          this._props = DynamicPropertyHolder.GrowPropertiesArray(this._props);
          local_2 = true;
        }
        IDynamicProperty[] temp_19 = this._props;
        int local_3 = this._numProps;
        this._numProps = local_3 + 1;
        int temp_26 = local_3;
        IDynamicProperty temp_27 = prop;
        temp_19[temp_26] = temp_27;
        if (local_2)
          this._sinks = DynamicPropertyHolder.GrowDynamicSinksArray(this._sinks);
        if (this._sinks == null)
        {
          this._sinks = new IDynamicMessageSink[this._props.Length];
          for (int local_4 = 0; local_4 < this._numProps; ++local_4)
            this._sinks[local_4] = ((IContributeDynamicSink) this._props[local_4]).GetDynamicSink();
        }
        else
          this._sinks[this._numProps - 1] = ((IContributeDynamicSink) prop).GetDynamicSink();
        return true;
      }
    }

    [SecurityCritical]
    internal virtual bool RemoveDynamicProperty(string name)
    {
      lock (this)
      {
        for (int local_2 = 0; local_2 < this._numProps; ++local_2)
        {
          if (this._props[local_2].Name.Equals(name))
          {
            this._props[local_2] = this._props[this._numProps - 1];
            this._numProps = this._numProps - 1;
            this._sinks = (IDynamicMessageSink[]) null;
            return true;
          }
        }
        throw new RemotingException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Contexts_NoProperty"), (object) name));
      }
    }

    private static IDynamicMessageSink[] GrowDynamicSinksArray(IDynamicMessageSink[] sinks)
    {
      IDynamicMessageSink[] dynamicMessageSinkArray = new IDynamicMessageSink[(sinks != null ? sinks.Length : 0) + 8];
      if (sinks != null)
        Array.Copy((Array) sinks, (Array) dynamicMessageSinkArray, sinks.Length);
      return dynamicMessageSinkArray;
    }

    [SecurityCritical]
    internal static void NotifyDynamicSinks(IMessage msg, ArrayWithSize dynSinks, bool bCliSide, bool bStart, bool bAsync)
    {
      for (int index = 0; index < dynSinks.Count; ++index)
      {
        if (bStart)
          dynSinks.Sinks[index].ProcessMessageStart(msg, bCliSide, bAsync);
        else
          dynSinks.Sinks[index].ProcessMessageFinish(msg, bCliSide, bAsync);
      }
    }

    [SecurityCritical]
    internal static void CheckPropertyNameClash(string name, IDynamicProperty[] props, int count)
    {
      for (int index = 0; index < count; ++index)
      {
        if (props[index].Name.Equals(name))
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_DuplicatePropertyName"));
      }
    }

    internal static IDynamicProperty[] GrowPropertiesArray(IDynamicProperty[] props)
    {
      IDynamicProperty[] dynamicPropertyArray = new IDynamicProperty[(props != null ? props.Length : 0) + 8];
      if (props != null)
        Array.Copy((Array) props, (Array) dynamicPropertyArray, props.Length);
      return dynamicPropertyArray;
    }
  }
}
