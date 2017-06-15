// Decompiled with JetBrains decompiler
// Type: System.Security.Util.TokenBasedSet
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Threading;

namespace System.Security.Util
{
  [Serializable]
  internal class TokenBasedSet
  {
    private int m_initSize = 24;
    private int m_increment = 8;
    private object[] m_objSet;
    [OptionalField(VersionAdded = 2)]
    private volatile object m_Obj;
    [OptionalField(VersionAdded = 2)]
    private volatile object[] m_Set;
    private int m_cElt;
    private volatile int m_maxIndex;

    internal TokenBasedSet()
    {
      this.Reset();
    }

    internal TokenBasedSet(TokenBasedSet tbSet)
    {
      if (tbSet == null)
      {
        this.Reset();
      }
      else
      {
        if (tbSet.m_cElt > 1)
        {
          object[] objArray1 = tbSet.m_Set;
          int length1 = objArray1.Length;
          object[] objArray2 = new object[length1];
          int sourceIndex = 0;
          object[] objArray3 = objArray2;
          int destinationIndex = 0;
          int length2 = length1;
          Array.Copy((Array) objArray1, sourceIndex, (Array) objArray3, destinationIndex, length2);
          this.m_Set = objArray2;
        }
        else
          this.m_Obj = tbSet.m_Obj;
        this.m_cElt = tbSet.m_cElt;
        this.m_maxIndex = tbSet.m_maxIndex;
      }
    }

    [OnDeserialized]
    private void OnDeserialized(StreamingContext ctx)
    {
      this.OnDeserializedInternal();
    }

    private void OnDeserializedInternal()
    {
      if (this.m_objSet == null)
        return;
      if (this.m_cElt == 1)
        this.m_Obj = this.m_objSet[this.m_maxIndex];
      else
        this.m_Set = this.m_objSet;
      this.m_objSet = (object[]) null;
    }

    [OnSerializing]
    private void OnSerializing(StreamingContext ctx)
    {
      if ((ctx.State & ~(StreamingContextStates.Clone | StreamingContextStates.CrossAppDomain)) == (StreamingContextStates) 0)
        return;
      if (this.m_cElt == 1)
      {
        this.m_objSet = new object[this.m_maxIndex + 1];
        this.m_objSet[this.m_maxIndex] = this.m_Obj;
      }
      else
      {
        if (this.m_cElt <= 0)
          return;
        this.m_objSet = this.m_Set;
      }
    }

    [OnSerialized]
    private void OnSerialized(StreamingContext ctx)
    {
      if ((ctx.State & ~(StreamingContextStates.Clone | StreamingContextStates.CrossAppDomain)) == (StreamingContextStates) 0)
        return;
      this.m_objSet = (object[]) null;
    }

    internal bool MoveNext(ref TokenBasedSetEnumerator e)
    {
      switch (this.m_cElt)
      {
        case 0:
          return false;
        case 1:
          if (e.Index == -1)
          {
            e.Index = this.m_maxIndex;
            e.Current = this.m_Obj;
            return true;
          }
          e.Index = (int) (short) (this.m_maxIndex + 1);
          e.Current = (object) null;
          return false;
        default:
          do
          {
            // ISSUE: explicit reference operation
            // ISSUE: variable of a reference type
            int& local = @e.Index;
            // ISSUE: explicit reference operation
            int num1 = ^local + 1;
            int num2 = num1;
            // ISSUE: explicit reference operation
            ^local = num2;
            if (num1 <= this.m_maxIndex)
              e.Current = Volatile.Read<object>(ref this.m_Set[e.Index]);
            else
              goto label_8;
          }
          while (e.Current == null);
          return true;
label_8:
          e.Current = (object) null;
          return false;
      }
    }

    internal void Reset()
    {
      this.m_Obj = (object) null;
      this.m_Set = (object[]) null;
      this.m_cElt = 0;
      this.m_maxIndex = -1;
    }

    internal void SetItem(int index, object item)
    {
      if (item == null)
      {
        this.RemoveItem(index);
      }
      else
      {
        switch (this.m_cElt)
        {
          case 0:
            this.m_cElt = 1;
            this.m_maxIndex = (int) (short) index;
            this.m_Obj = item;
            break;
          case 1:
            if (index == this.m_maxIndex)
            {
              this.m_Obj = item;
              break;
            }
            object obj = this.m_Obj;
            int num = Math.Max(this.m_maxIndex, index);
            object[] objArray1 = new object[num + 1];
            objArray1[this.m_maxIndex] = obj;
            objArray1[index] = item;
            this.m_maxIndex = (int) (short) num;
            this.m_cElt = 2;
            this.m_Set = objArray1;
            this.m_Obj = (object) null;
            break;
          default:
            object[] objArray2 = this.m_Set;
            if (index >= objArray2.Length)
            {
              object[] objArray3 = new object[index + 1];
              Array.Copy((Array) objArray2, 0, (Array) objArray3, 0, this.m_maxIndex + 1);
              this.m_maxIndex = (int) (short) index;
              objArray3[index] = item;
              this.m_Set = objArray3;
              this.m_cElt = this.m_cElt + 1;
              break;
            }
            if (objArray2[index] == null)
              this.m_cElt = this.m_cElt + 1;
            objArray2[index] = item;
            if (index <= this.m_maxIndex)
              break;
            this.m_maxIndex = (int) (short) index;
            break;
        }
      }
    }

    internal object GetItem(int index)
    {
      switch (this.m_cElt)
      {
        case 0:
          return (object) null;
        case 1:
          if (index == this.m_maxIndex)
            return this.m_Obj;
          return (object) null;
        default:
          if (index < this.m_Set.Length)
            return Volatile.Read<object>(ref this.m_Set[index]);
          return (object) null;
      }
    }

    internal object RemoveItem(int index)
    {
      object obj = (object) null;
      switch (this.m_cElt)
      {
        case 0:
          obj = (object) null;
          break;
        case 1:
          if (index != this.m_maxIndex)
          {
            obj = (object) null;
            break;
          }
          obj = this.m_Obj;
          this.Reset();
          break;
        default:
          if (index < this.m_Set.Length && (obj = Volatile.Read<object>(ref this.m_Set[index])) != null)
          {
            Volatile.Write<object>(ref this.m_Set[index], (object) null);
            this.m_cElt = this.m_cElt - 1;
            if (index == this.m_maxIndex)
              this.ResetMaxIndex(this.m_Set);
            if (this.m_cElt == 1)
            {
              this.m_Obj = Volatile.Read<object>(ref this.m_Set[this.m_maxIndex]);
              this.m_Set = (object[]) null;
              break;
            }
            break;
          }
          break;
      }
      return obj;
    }

    private void ResetMaxIndex(object[] aObj)
    {
      for (int index = aObj.Length - 1; index >= 0; --index)
      {
        if (aObj[index] != null)
        {
          this.m_maxIndex = (int) (short) index;
          return;
        }
      }
      this.m_maxIndex = -1;
    }

    internal int GetStartingIndex()
    {
      if (this.m_cElt <= 1)
        return this.m_maxIndex;
      return 0;
    }

    internal int GetCount()
    {
      return this.m_cElt;
    }

    internal int GetMaxUsedIndex()
    {
      return this.m_maxIndex;
    }

    internal bool FastIsEmpty()
    {
      return this.m_cElt == 0;
    }

    internal TokenBasedSet SpecialUnion(TokenBasedSet other)
    {
      this.OnDeserializedInternal();
      TokenBasedSet tokenBasedSet = new TokenBasedSet();
      int num;
      if (other != null)
      {
        other.OnDeserializedInternal();
        num = this.GetMaxUsedIndex() > other.GetMaxUsedIndex() ? this.GetMaxUsedIndex() : other.GetMaxUsedIndex();
      }
      else
        num = this.GetMaxUsedIndex();
      for (int index = 0; index <= num; ++index)
      {
        object obj1 = this.GetItem(index);
        IPermission perm1 = obj1 as IPermission;
        ISecurityElementFactory securityElementFactory1 = obj1 as ISecurityElementFactory;
        object obj2 = other != null ? other.GetItem(index) : (object) null;
        IPermission perm2 = obj2 as IPermission;
        ISecurityElementFactory securityElementFactory2 = obj2 as ISecurityElementFactory;
        if (obj1 != null || obj2 != null)
        {
          if (obj1 == null)
          {
            if (securityElementFactory2 != null)
              perm2 = PermissionSet.CreatePerm((object) securityElementFactory2, false);
            PermissionToken token = PermissionToken.GetToken(perm2);
            if (token == null)
              throw new SerializationException(Environment.GetResourceString("Serialization_InsufficientState"));
            tokenBasedSet.SetItem(token.m_index, (object) perm2);
          }
          else if (obj2 == null)
          {
            if (securityElementFactory1 != null)
              perm1 = PermissionSet.CreatePerm((object) securityElementFactory1, false);
            PermissionToken token = PermissionToken.GetToken(perm1);
            if (token == null)
              throw new SerializationException(Environment.GetResourceString("Serialization_InsufficientState"));
            tokenBasedSet.SetItem(token.m_index, (object) perm1);
          }
        }
      }
      return tokenBasedSet;
    }

    internal void SpecialSplit(ref TokenBasedSet unrestrictedPermSet, ref TokenBasedSet normalPermSet, bool ignoreTypeLoadFailures)
    {
      int maxUsedIndex = this.GetMaxUsedIndex();
      for (int startingIndex = this.GetStartingIndex(); startingIndex <= maxUsedIndex; ++startingIndex)
      {
        object obj = this.GetItem(startingIndex);
        if (obj != null)
        {
          IPermission perm = obj as IPermission ?? PermissionSet.CreatePerm(obj, ignoreTypeLoadFailures);
          PermissionToken token = PermissionToken.GetToken(perm);
          if (perm != null && token != null)
          {
            if (perm is IUnrestrictedPermission)
            {
              if (unrestrictedPermSet == null)
                unrestrictedPermSet = new TokenBasedSet();
              unrestrictedPermSet.SetItem(token.m_index, (object) perm);
            }
            else
            {
              if (normalPermSet == null)
                normalPermSet = new TokenBasedSet();
              normalPermSet.SetItem(token.m_index, (object) perm);
            }
          }
        }
      }
    }
  }
}
