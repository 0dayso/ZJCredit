// Decompiled with JetBrains decompiler
// Type: System.Reflection.__Filters
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Reflection
{
  [Serializable]
  internal class __Filters
  {
    public virtual bool FilterTypeName(Type cls, object filterCriteria)
    {
      if (filterCriteria == null || !(filterCriteria is string))
        throw new InvalidFilterCriteriaException(Environment.GetResourceString("RFLCT.FltCritString"));
      string str1 = (string) filterCriteria;
      if (str1.Length > 0)
      {
        string str2 = str1;
        int index = str2.Length - 1;
        if ((int) str2[index] == 42)
        {
          string str3 = str1.Substring(0, str1.Length - 1);
          return cls.Name.StartsWith(str3, StringComparison.Ordinal);
        }
      }
      return cls.Name.Equals(str1);
    }

    public virtual bool FilterTypeNameIgnoreCase(Type cls, object filterCriteria)
    {
      if (filterCriteria == null || !(filterCriteria is string))
        throw new InvalidFilterCriteriaException(Environment.GetResourceString("RFLCT.FltCritString"));
      string strA = (string) filterCriteria;
      if (strA.Length > 0)
      {
        string str = strA;
        int index = str.Length - 1;
        if ((int) str[index] == 42)
        {
          string strB = strA.Substring(0, strA.Length - 1);
          string name = cls.Name;
          if (name.Length >= strB.Length)
            return string.Compare(name, 0, strB, 0, strB.Length, StringComparison.OrdinalIgnoreCase) == 0;
          return false;
        }
      }
      return string.Compare(strA, cls.Name, StringComparison.OrdinalIgnoreCase) == 0;
    }
  }
}
