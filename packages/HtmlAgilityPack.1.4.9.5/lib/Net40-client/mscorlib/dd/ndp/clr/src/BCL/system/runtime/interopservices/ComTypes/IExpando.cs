﻿// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComTypes.IExpando
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;

namespace System.Runtime.InteropServices.ComTypes
{
  [Guid("AFBF15E6-C37C-11d2-B88E-00A0C9B471B8")]
  internal interface IExpando : IReflect
  {
    FieldInfo AddField(string name);

    PropertyInfo AddProperty(string name);

    MethodInfo AddMethod(string name, Delegate method);

    void RemoveMember(MemberInfo m);
  }
}
