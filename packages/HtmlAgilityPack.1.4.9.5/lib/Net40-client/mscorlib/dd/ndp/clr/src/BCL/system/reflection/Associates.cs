// Decompiled with JetBrains decompiler
// Type: System.Reflection.Associates
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Security;

namespace System.Reflection
{
  internal static class Associates
  {
    internal static bool IncludeAccessor(MethodInfo associate, bool nonPublic)
    {
      return associate != null && (nonPublic || associate.IsPublic);
    }

    [SecurityCritical]
    private static RuntimeMethodInfo AssignAssociates(int tkMethod, RuntimeType declaredType, RuntimeType reflectedType)
    {
      if (MetadataToken.IsNullToken(tkMethod))
        return (RuntimeMethodInfo) null;
      bool flag = declaredType != reflectedType;
      IntPtr[] typeInstantiationContext = (IntPtr[]) null;
      int typeInstCount = 0;
      RuntimeType[] instantiationInternal = declaredType.GetTypeHandleInternal().GetInstantiationInternal();
      if (instantiationInternal != null)
      {
        typeInstCount = instantiationInternal.Length;
        typeInstantiationContext = new IntPtr[instantiationInternal.Length];
        for (int index = 0; index < instantiationInternal.Length; ++index)
          typeInstantiationContext[index] = instantiationInternal[index].GetTypeHandleInternal().Value;
      }
      RuntimeMethodHandleInternal methodHandleInternal = ModuleHandle.ResolveMethodHandleInternalCore(RuntimeTypeHandle.GetModule(declaredType), tkMethod, typeInstantiationContext, typeInstCount, (IntPtr[]) null, 0);
      if (flag)
      {
        MethodAttributes attributes = RuntimeMethodHandle.GetAttributes(methodHandleInternal);
        if (!CompatibilitySwitches.IsAppEarlierThanWindowsPhone8 && (attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Private)
          return (RuntimeMethodInfo) null;
        if ((attributes & MethodAttributes.Virtual) != MethodAttributes.PrivateScope && (RuntimeTypeHandle.GetAttributes(declaredType) & TypeAttributes.ClassSemanticsMask) == TypeAttributes.NotPublic)
        {
          int slot = RuntimeMethodHandle.GetSlot(methodHandleInternal);
          methodHandleInternal = RuntimeTypeHandle.GetMethodAt(reflectedType, slot);
        }
      }
      RuntimeMethodInfo runtimeMethodInfo = RuntimeType.GetMethodBase(reflectedType, methodHandleInternal) as RuntimeMethodInfo;
      if ((MethodInfo) runtimeMethodInfo == (MethodInfo) null)
        runtimeMethodInfo = reflectedType.Module.ResolveMethod(tkMethod, (Type[]) null, (Type[]) null) as RuntimeMethodInfo;
      return runtimeMethodInfo;
    }

    [SecurityCritical]
    internal static void AssignAssociates(MetadataImport scope, int mdPropEvent, RuntimeType declaringType, RuntimeType reflectedType, out RuntimeMethodInfo addOn, out RuntimeMethodInfo removeOn, out RuntimeMethodInfo fireOn, out RuntimeMethodInfo getter, out RuntimeMethodInfo setter, out MethodInfo[] other, out bool composedOfAllPrivateMethods, out BindingFlags bindingFlags)
    {
      addOn = removeOn = fireOn = getter = setter = (RuntimeMethodInfo) null;
      Associates.Attributes attributes1 = Associates.Attributes.ComposedOfAllVirtualMethods | Associates.Attributes.ComposedOfAllPrivateMethods | Associates.Attributes.ComposedOfNoPublicMembers | Associates.Attributes.ComposedOfNoStaticMembers;
      while (RuntimeTypeHandle.IsGenericVariable(reflectedType))
        reflectedType = (RuntimeType) reflectedType.BaseType;
      bool isInherited = declaringType != reflectedType;
      List<MethodInfo> methodInfoList = (List<MethodInfo>) null;
      MetadataEnumResult result;
      scope.Enum(MetadataTokenType.MethodDef, mdPropEvent, out result);
      int capacity = result.Length / 2;
      for (int index = 0; index < capacity; ++index)
      {
        int tkMethod = result[index * 2];
        MethodSemanticsAttributes semanticsAttributes = (MethodSemanticsAttributes) result[index * 2 + 1];
        RuntimeType declaredType = declaringType;
        RuntimeType reflectedType1 = reflectedType;
        RuntimeMethodInfo runtimeMethodInfo = Associates.AssignAssociates(tkMethod, declaredType, reflectedType1);
        if (!((MethodInfo) runtimeMethodInfo == (MethodInfo) null))
        {
          MethodAttributes attributes2 = runtimeMethodInfo.Attributes;
          bool flag1 = (attributes2 & MethodAttributes.MemberAccessMask) == MethodAttributes.Private;
          bool flag2 = (uint) (attributes2 & MethodAttributes.Virtual) > 0U;
          int num = (attributes2 & MethodAttributes.MemberAccessMask) == MethodAttributes.Public ? 1 : 0;
          bool flag3 = (uint) (attributes2 & MethodAttributes.Static) > 0U;
          if (num != 0)
            attributes1 = attributes1 & ~Associates.Attributes.ComposedOfNoPublicMembers & ~Associates.Attributes.ComposedOfAllPrivateMethods;
          else if (!flag1)
            attributes1 &= ~Associates.Attributes.ComposedOfAllPrivateMethods;
          if (flag3)
            attributes1 &= ~Associates.Attributes.ComposedOfNoStaticMembers;
          if (!flag2)
            attributes1 &= ~Associates.Attributes.ComposedOfAllVirtualMethods;
          if (semanticsAttributes == MethodSemanticsAttributes.Setter)
            setter = runtimeMethodInfo;
          else if (semanticsAttributes == MethodSemanticsAttributes.Getter)
            getter = runtimeMethodInfo;
          else if (semanticsAttributes == MethodSemanticsAttributes.Fire)
            fireOn = runtimeMethodInfo;
          else if (semanticsAttributes == MethodSemanticsAttributes.AddOn)
            addOn = runtimeMethodInfo;
          else if (semanticsAttributes == MethodSemanticsAttributes.RemoveOn)
          {
            removeOn = runtimeMethodInfo;
          }
          else
          {
            if (methodInfoList == null)
              methodInfoList = new List<MethodInfo>(capacity);
            methodInfoList.Add((MethodInfo) runtimeMethodInfo);
          }
        }
      }
      bool isPublic = (attributes1 & Associates.Attributes.ComposedOfNoPublicMembers) == (Associates.Attributes) 0;
      bool isStatic = (attributes1 & Associates.Attributes.ComposedOfNoStaticMembers) == (Associates.Attributes) 0;
      bindingFlags = RuntimeType.FilterPreCalculate(isPublic, isInherited, isStatic);
      composedOfAllPrivateMethods = (uint) (attributes1 & Associates.Attributes.ComposedOfAllPrivateMethods) > 0U;
      other = methodInfoList != null ? methodInfoList.ToArray() : (MethodInfo[]) null;
    }

    [Flags]
    internal enum Attributes
    {
      ComposedOfAllVirtualMethods = 1,
      ComposedOfAllPrivateMethods = 2,
      ComposedOfNoPublicMembers = 4,
      ComposedOfNoStaticMembers = 8,
    }
  }
}
