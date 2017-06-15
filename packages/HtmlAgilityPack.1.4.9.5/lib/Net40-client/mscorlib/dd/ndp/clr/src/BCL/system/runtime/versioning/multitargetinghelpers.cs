// Decompiled with JetBrains decompiler
// Type: System.Runtime.Versioning.MultitargetingHelpers
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;
using System.Threading;

namespace System.Runtime.Versioning
{
  internal static class MultitargetingHelpers
  {
    private static Func<Type, string> defaultConverter = new Func<Type, string>(MultitargetingHelpers.\u003C\u003Ec.\u003C\u003E9.\u003C\u002Ecctor\u003Eb__4_0);

    internal static string GetAssemblyQualifiedName(Type type, Func<Type, string> converter)
    {
      string str = (string) null;
      if (type != (Type) null)
      {
        if (converter != null)
        {
          try
          {
            str = converter(type);
          }
          catch (Exception ex)
          {
            if (MultitargetingHelpers.IsSecurityOrCriticalException(ex))
              throw;
          }
        }
        if (str == null)
          str = MultitargetingHelpers.defaultConverter(type);
      }
      return str;
    }

    private static bool IsCriticalException(Exception ex)
    {
      if (!(ex is NullReferenceException) && !(ex is StackOverflowException) && (!(ex is OutOfMemoryException) && !(ex is ThreadAbortException)) && !(ex is IndexOutOfRangeException))
        return ex is AccessViolationException;
      return true;
    }

    private static bool IsSecurityOrCriticalException(Exception ex)
    {
      if (!(ex is SecurityException))
        return MultitargetingHelpers.IsCriticalException(ex);
      return true;
    }
  }
}
