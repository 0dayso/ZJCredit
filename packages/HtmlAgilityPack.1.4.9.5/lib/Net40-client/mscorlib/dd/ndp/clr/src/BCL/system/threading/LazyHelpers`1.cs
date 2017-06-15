// Decompiled with JetBrains decompiler
// Type: System.Threading.LazyHelpers`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Threading
{
  internal static class LazyHelpers<T>
  {
    internal static Func<T> s_activatorFactorySelector = new Func<T>(LazyHelpers<T>.ActivatorFactorySelector);

    private static T ActivatorFactorySelector()
    {
      try
      {
        return (T) Activator.CreateInstance(typeof (T));
      }
      catch (MissingMethodException ex)
      {
        throw new MissingMemberException(Environment.GetResourceString("Lazy_CreateValue_NoParameterlessCtorForT"));
      }
    }
  }
}
