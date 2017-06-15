// Decompiled with JetBrains decompiler
// Type: System.Text.StringBuilderCache
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Text
{
  internal static class StringBuilderCache
  {
    private const int MAX_BUILDER_SIZE = 360;
    [ThreadStatic]
    private static StringBuilder CachedInstance;

    public static StringBuilder Acquire(int capacity = 16)
    {
      if (capacity <= 360)
      {
        StringBuilder stringBuilder = StringBuilderCache.CachedInstance;
        if (stringBuilder != null && capacity <= stringBuilder.Capacity)
        {
          StringBuilderCache.CachedInstance = (StringBuilder) null;
          stringBuilder.Clear();
          return stringBuilder;
        }
      }
      return new StringBuilder(capacity);
    }

    public static void Release(StringBuilder sb)
    {
      if (sb.Capacity > 360)
        return;
      StringBuilderCache.CachedInstance = sb;
    }

    public static string GetStringAndRelease(StringBuilder sb)
    {
      string @string = sb.ToString();
      StringBuilderCache.Release(sb);
      return @string;
    }
  }
}
