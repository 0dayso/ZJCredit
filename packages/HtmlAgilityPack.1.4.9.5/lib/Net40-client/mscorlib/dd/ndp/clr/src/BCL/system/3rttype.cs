// Decompiled with JetBrains decompiler
// Type: System.Reflection.CerHashtable`2
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Threading;

namespace System.Reflection
{
  internal struct CerHashtable<K, V> where K : class
  {
    private CerHashtable<K, V>.Table m_Table;
    private const int MinSize = 7;

    internal V this[K key]
    {
      get
      {
        CerHashtable<K, V>.Table table = Volatile.Read<CerHashtable<K, V>.Table>(ref this.m_Table);
        if (table == null)
          return default (V);
        int num = CerHashtable<K, V>.GetHashCodeHelper(key);
        if (num < 0)
          num = ~num;
        K[] kArray = table.m_keys;
        int index = num % kArray.Length;
        while (true)
        {
          do
          {
            K k = Volatile.Read<K>(ref kArray[index]);
            if ((object) k != null)
            {
              if (k.Equals((object) key))
                return table.m_values[index];
              ++index;
            }
            else
              goto label_10;
          }
          while (index < kArray.Length);
          index -= kArray.Length;
        }
label_10:
        return default (V);
      }
      set
      {
        CerHashtable<K, V>.Table table = this.m_Table;
        if (table != null)
        {
          int newSize = 2 * (table.m_count + 1);
          if (newSize >= table.m_keys.Length)
            this.Rehash(newSize);
        }
        else
          this.Rehash(7);
        this.m_Table.Insert(key, value);
      }
    }

    private static int GetHashCodeHelper(K key)
    {
      string str = (object) key as string;
      if (str == null)
        return key.GetHashCode();
      return str.GetLegacyNonRandomizedHashCode();
    }

    private void Rehash(int newSize)
    {
      CerHashtable<K, V>.Table table1 = new CerHashtable<K, V>.Table(newSize);
      CerHashtable<K, V>.Table table2 = this.m_Table;
      if (table2 != null)
      {
        K[] kArray = table2.m_keys;
        V[] vArray = table2.m_values;
        for (int index = 0; index < kArray.Length; ++index)
        {
          K key = kArray[index];
          if ((object) key != null)
            table1.Insert(key, vArray[index]);
        }
      }
      Volatile.Write<CerHashtable<K, V>.Table>(ref this.m_Table, table1);
    }

    private class Table
    {
      internal K[] m_keys;
      internal V[] m_values;
      internal int m_count;

      internal Table(int size)
      {
        size = HashHelpers.GetPrime(size);
        this.m_keys = new K[size];
        this.m_values = new V[size];
      }

      internal void Insert(K key, V value)
      {
        int num = CerHashtable<K, V>.GetHashCodeHelper(key);
        if (num < 0)
          num = ~num;
        K[] kArray = this.m_keys;
        int index = num % kArray.Length;
        while ((object) kArray[index] != null)
        {
          ++index;
          if (index >= kArray.Length)
            index -= kArray.Length;
        }
        this.m_count = this.m_count + 1;
        this.m_values[index] = value;
        Volatile.Write<K>(ref kArray[index], key);
      }
    }
  }
}
