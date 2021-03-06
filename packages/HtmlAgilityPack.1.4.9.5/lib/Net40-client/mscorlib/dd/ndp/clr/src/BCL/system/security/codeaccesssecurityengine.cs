﻿// Decompiled with JetBrains decompiler
// Type: System.Security.CodeAccessSecurityEngine
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using System.Security.Policy;
using System.Threading;

namespace System.Security
{
  internal static class CodeAccessSecurityEngine
  {
    internal static SecurityPermission AssertPermission = new SecurityPermission(SecurityPermissionFlag.Assertion);
    internal static PermissionToken AssertPermissionToken = PermissionToken.GetToken((IPermission) CodeAccessSecurityEngine.AssertPermission);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void SpecialDemand(PermissionType whatPermission, ref StackCrawlMark stackMark);

    [SecurityCritical]
    [Conditional("_DEBUG")]
    private static void DEBUG_OUT(string str)
    {
    }

    [SecurityCritical]
    private static void ThrowSecurityException(RuntimeAssembly asm, PermissionSet granted, PermissionSet refused, RuntimeMethodHandleInternal rmh, SecurityAction action, object demand, IPermission permThatFailed)
    {
      AssemblyName asmName = (AssemblyName) null;
      Evidence asmEvidence = (Evidence) null;
      if ((Assembly) asm != (Assembly) null)
      {
        PermissionSet.s_fullTrust.Assert();
        asmName = asm.GetName();
        if ((Assembly) asm != Assembly.GetExecutingAssembly())
          asmEvidence = asm.Evidence;
      }
      throw SecurityException.MakeSecurityException(asmName, asmEvidence, granted, refused, rmh, action, demand, permThatFailed);
    }

    [SecurityCritical]
    private static void ThrowSecurityException(object assemblyOrString, PermissionSet granted, PermissionSet refused, RuntimeMethodHandleInternal rmh, SecurityAction action, object demand, IPermission permThatFailed)
    {
      if (assemblyOrString != null && !(assemblyOrString is RuntimeAssembly))
        throw SecurityException.MakeSecurityException(new AssemblyName((string) assemblyOrString), (Evidence) null, granted, refused, rmh, action, demand, permThatFailed);
      CodeAccessSecurityEngine.ThrowSecurityException((RuntimeAssembly) assemblyOrString, granted, refused, rmh, action, demand, permThatFailed);
    }

    [SecurityCritical]
    internal static void CheckSetHelper(CompressedStack cs, PermissionSet grants, PermissionSet refused, PermissionSet demands, RuntimeMethodHandleInternal rmh, RuntimeAssembly asm, SecurityAction action)
    {
      if (cs != null)
        cs.CheckSetDemand(demands, rmh);
      else
        CodeAccessSecurityEngine.CheckSetHelper(grants, refused, demands, rmh, (object) asm, action, true);
    }

    [SecurityCritical]
    internal static bool CheckSetHelper(PermissionSet grants, PermissionSet refused, PermissionSet demands, RuntimeMethodHandleInternal rmh, object assemblyOrString, SecurityAction action, bool throwException)
    {
      IPermission firstPermThatFailed = (IPermission) null;
      if (grants != null)
        grants.CheckDecoded(demands);
      if (refused != null)
        refused.CheckDecoded(demands);
      bool flag = SecurityManager._SetThreadSecurity(false);
      try
      {
        if (!demands.CheckDemand(grants, out firstPermThatFailed))
        {
          if (!throwException)
            return false;
          CodeAccessSecurityEngine.ThrowSecurityException(assemblyOrString, grants, refused, rmh, action, (object) demands, firstPermThatFailed);
        }
        if (!demands.CheckDeny(refused, out firstPermThatFailed))
        {
          if (!throwException)
            return false;
          CodeAccessSecurityEngine.ThrowSecurityException(assemblyOrString, grants, refused, rmh, action, (object) demands, firstPermThatFailed);
        }
      }
      catch (SecurityException ex)
      {
        throw;
      }
      catch (Exception ex)
      {
        if (!throwException)
          return false;
        CodeAccessSecurityEngine.ThrowSecurityException(assemblyOrString, grants, refused, rmh, action, (object) demands, firstPermThatFailed);
      }
      finally
      {
        if (flag)
          SecurityManager._SetThreadSecurity(true);
      }
      return true;
    }

    [SecurityCritical]
    internal static void CheckHelper(CompressedStack cs, PermissionSet grantedSet, PermissionSet refusedSet, CodeAccessPermission demand, PermissionToken permToken, RuntimeMethodHandleInternal rmh, RuntimeAssembly asm, SecurityAction action)
    {
      if (cs != null)
        cs.CheckDemand(demand, permToken, rmh);
      else
        CodeAccessSecurityEngine.CheckHelper(grantedSet, refusedSet, demand, permToken, rmh, (object) asm, action, true);
    }

    [SecurityCritical]
    internal static bool CheckHelper(PermissionSet grantedSet, PermissionSet refusedSet, CodeAccessPermission demand, PermissionToken permToken, RuntimeMethodHandleInternal rmh, object assemblyOrString, SecurityAction action, bool throwException)
    {
      if (permToken == null)
        permToken = PermissionToken.GetToken((IPermission) demand);
      if (grantedSet != null)
        grantedSet.CheckDecoded(permToken.m_index);
      if (refusedSet != null)
        refusedSet.CheckDecoded(permToken.m_index);
      bool flag = SecurityManager._SetThreadSecurity(false);
      try
      {
        if (grantedSet == null)
        {
          if (!throwException)
            return false;
          CodeAccessSecurityEngine.ThrowSecurityException(assemblyOrString, grantedSet, refusedSet, rmh, action, (object) demand, (IPermission) demand);
        }
        else if (!grantedSet.IsUnrestricted())
        {
          CodeAccessPermission grant = (CodeAccessPermission) grantedSet.GetPermission(permToken);
          if (!demand.CheckDemand(grant))
          {
            if (!throwException)
              return false;
            CodeAccessSecurityEngine.ThrowSecurityException(assemblyOrString, grantedSet, refusedSet, rmh, action, (object) demand, (IPermission) demand);
          }
        }
        if (refusedSet != null)
        {
          CodeAccessPermission accessPermission = (CodeAccessPermission) refusedSet.GetPermission(permToken);
          if (accessPermission != null && !accessPermission.CheckDeny(demand))
          {
            if (!throwException)
              return false;
            CodeAccessSecurityEngine.ThrowSecurityException(assemblyOrString, grantedSet, refusedSet, rmh, action, (object) demand, (IPermission) demand);
          }
          if (refusedSet.IsUnrestricted())
          {
            if (!throwException)
              return false;
            CodeAccessSecurityEngine.ThrowSecurityException(assemblyOrString, grantedSet, refusedSet, rmh, action, (object) demand, (IPermission) demand);
          }
        }
      }
      catch (SecurityException ex)
      {
        throw;
      }
      catch (Exception ex)
      {
        if (!throwException)
          return false;
        CodeAccessSecurityEngine.ThrowSecurityException(assemblyOrString, grantedSet, refusedSet, rmh, action, (object) demand, (IPermission) demand);
      }
      finally
      {
        if (flag)
          SecurityManager._SetThreadSecurity(true);
      }
      return true;
    }

    [SecurityCritical]
    private static void CheckGrantSetHelper(PermissionSet grantSet)
    {
      grantSet.CopyWithNoIdentityPermissions().Demand();
    }

    [SecurityCritical]
    internal static void ReflectionTargetDemandHelper(PermissionType permission, PermissionSet targetGrant)
    {
      CodeAccessSecurityEngine.ReflectionTargetDemandHelper((int) permission, targetGrant);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    private static void ReflectionTargetDemandHelper(int permission, PermissionSet targetGrant)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      CompressedStack compressedStack = CompressedStack.GetCompressedStack(ref stackMark);
      CodeAccessSecurityEngine.ReflectionTargetDemandHelper(permission, targetGrant, compressedStack);
    }

    [SecurityCritical]
    private static void ReflectionTargetDemandHelper(int permission, PermissionSet targetGrant, Resolver accessContext)
    {
      CodeAccessSecurityEngine.ReflectionTargetDemandHelper(permission, targetGrant, accessContext.GetSecurityContext());
    }

    [SecurityCritical]
    private static void ReflectionTargetDemandHelper(int permission, PermissionSet targetGrant, CompressedStack securityContext)
    {
      PermissionSet grantSet;
      if (targetGrant == null)
      {
        grantSet = new PermissionSet(PermissionState.Unrestricted);
      }
      else
      {
        grantSet = targetGrant.CopyWithNoIdentityPermissions();
        grantSet.AddPermission((IPermission) new ReflectionPermission(ReflectionPermissionFlag.RestrictedMemberAccess));
      }
      securityContext.DemandFlagsOrGrantSet(1 << permission, grantSet);
    }

    [SecurityCritical]
    internal static void GetZoneAndOriginHelper(CompressedStack cs, PermissionSet grantSet, PermissionSet refusedSet, ArrayList zoneList, ArrayList originList)
    {
      if (cs != null)
      {
        cs.GetZoneAndOrigin(zoneList, originList, PermissionToken.GetToken(typeof (ZoneIdentityPermission)), PermissionToken.GetToken(typeof (UrlIdentityPermission)));
      }
      else
      {
        ZoneIdentityPermission identityPermission1 = (ZoneIdentityPermission) grantSet.GetPermission(typeof (ZoneIdentityPermission));
        UrlIdentityPermission identityPermission2 = (UrlIdentityPermission) grantSet.GetPermission(typeof (UrlIdentityPermission));
        if (identityPermission1 != null)
          zoneList.Add((object) identityPermission1.SecurityZone);
        if (identityPermission2 == null)
          return;
        originList.Add((object) identityPermission2.Url);
      }
    }

    [SecurityCritical]
    internal static void GetZoneAndOrigin(ref StackCrawlMark mark, out ArrayList zone, out ArrayList origin)
    {
      zone = new ArrayList();
      origin = new ArrayList();
      CodeAccessSecurityEngine.GetZoneAndOriginInternal(zone, origin, ref mark);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void GetZoneAndOriginInternal(ArrayList zoneList, ArrayList originList, ref StackCrawlMark stackMark);

    [SecurityCritical]
    internal static void CheckAssembly(RuntimeAssembly asm, CodeAccessPermission demand)
    {
      PermissionSet newGrant;
      PermissionSet newDenied;
      asm.GetGrantSet(out newGrant, out newDenied);
      CodeAccessSecurityEngine.CheckHelper(newGrant, newDenied, demand, PermissionToken.GetToken((IPermission) demand), RuntimeMethodHandleInternal.EmptyHandle, (object) asm, SecurityAction.Demand, true);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void Check(object demand, ref StackCrawlMark stackMark, bool isPermSet);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool QuickCheckForAllDemands();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool AllDomainsHomogeneousWithNoStackModifiers();

    [SecurityCritical]
    internal static void Check(CodeAccessPermission cap, ref StackCrawlMark stackMark)
    {
      CodeAccessSecurityEngine.Check((object) cap, ref stackMark, false);
    }

    [SecurityCritical]
    internal static void Check(PermissionSet permSet, ref StackCrawlMark stackMark)
    {
      CodeAccessSecurityEngine.Check((object) permSet, ref stackMark, true);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern FrameSecurityDescriptor CheckNReturnSO(PermissionToken permToken, CodeAccessPermission demand, ref StackCrawlMark stackMark, int create);

    [SecurityCritical]
    internal static void Assert(CodeAccessPermission cap, ref StackCrawlMark stackMark)
    {
      FrameSecurityDescriptor securityDescriptor = CodeAccessSecurityEngine.CheckNReturnSO(CodeAccessSecurityEngine.AssertPermissionToken, (CodeAccessPermission) CodeAccessSecurityEngine.AssertPermission, ref stackMark, 1);
      if (securityDescriptor == null)
      {
        Environment.FailFast(Environment.GetResourceString("ExecutionEngine_MissingSecurityDescriptor"));
      }
      else
      {
        if (securityDescriptor.HasImperativeAsserts())
          throw new SecurityException(Environment.GetResourceString("Security_MustRevertOverride"));
        securityDescriptor.SetAssert((IPermission) cap);
      }
    }

    [SecurityCritical]
    internal static void Deny(CodeAccessPermission cap, ref StackCrawlMark stackMark)
    {
      if (!AppDomain.CurrentDomain.IsLegacyCasPolicyEnabled)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_CasDeny"));
      FrameSecurityDescriptor securityObjectForFrame = SecurityRuntime.GetSecurityObjectForFrame(ref stackMark, true);
      if (securityObjectForFrame == null)
      {
        Environment.FailFast(Environment.GetResourceString("ExecutionEngine_MissingSecurityDescriptor"));
      }
      else
      {
        if (securityObjectForFrame.HasImperativeDenials())
          throw new SecurityException(Environment.GetResourceString("Security_MustRevertOverride"));
        securityObjectForFrame.SetDeny((IPermission) cap);
      }
    }

    [SecurityCritical]
    internal static void PermitOnly(CodeAccessPermission cap, ref StackCrawlMark stackMark)
    {
      FrameSecurityDescriptor securityObjectForFrame = SecurityRuntime.GetSecurityObjectForFrame(ref stackMark, true);
      if (securityObjectForFrame == null)
      {
        Environment.FailFast(Environment.GetResourceString("ExecutionEngine_MissingSecurityDescriptor"));
      }
      else
      {
        if (securityObjectForFrame.HasImperativeRestrictions())
          throw new SecurityException(Environment.GetResourceString("Security_MustRevertOverride"));
        securityObjectForFrame.SetPermitOnly((IPermission) cap);
      }
    }

    private static void PreResolve(out bool isFullyTrusted, out bool isHomogeneous)
    {
      ApplicationTrust applicationTrust = AppDomain.CurrentDomain.SetupInformation.ApplicationTrust;
      if (applicationTrust != null)
      {
        isFullyTrusted = applicationTrust.DefaultGrantSet.PermissionSet.IsUnrestricted();
        isHomogeneous = true;
      }
      else if (CompatibilitySwitches.IsNetFx40LegacySecurityPolicy || AppDomain.CurrentDomain.IsLegacyCasPolicyEnabled)
      {
        isFullyTrusted = false;
        isHomogeneous = false;
      }
      else
      {
        isFullyTrusted = true;
        isHomogeneous = true;
      }
    }

    private static PermissionSet ResolveGrantSet(Evidence evidence, out int specialFlags, bool checkExecutionPermission)
    {
      PermissionSet grantSet = (PermissionSet) null;
      if (!CodeAccessSecurityEngine.TryResolveGrantSet(evidence, out grantSet))
        grantSet = new PermissionSet(PermissionState.Unrestricted);
      if (checkExecutionPermission)
      {
        SecurityPermission securityPermission = new SecurityPermission(SecurityPermissionFlag.Execution);
        if (!grantSet.Contains((IPermission) securityPermission))
          throw new PolicyException(Environment.GetResourceString("Policy_NoExecutionPermission"), -2146233320);
      }
      specialFlags = SecurityManager.GetSpecialFlags(grantSet, (PermissionSet) null);
      return grantSet;
    }

    [SecuritySafeCritical]
    internal static bool TryResolveGrantSet(Evidence evidence, out PermissionSet grantSet)
    {
      HostSecurityManager hostSecurityManager = AppDomain.CurrentDomain.HostSecurityManager;
      if (evidence.GetHostEvidence<GacInstalled>() != null)
      {
        grantSet = new PermissionSet(PermissionState.Unrestricted);
        return true;
      }
      if ((hostSecurityManager.Flags & HostSecurityManagerOptions.HostResolvePolicy) == HostSecurityManagerOptions.HostResolvePolicy)
      {
        PermissionSet target = hostSecurityManager.ResolvePolicy(evidence);
        if (target == null)
          throw new PolicyException(Environment.GetResourceString("Policy_NullHostGrantSet", (object) hostSecurityManager.GetType().FullName));
        if (AppDomain.CurrentDomain.IsHomogenous)
        {
          if (target.IsEmpty())
            throw new PolicyException(Environment.GetResourceString("Policy_NoExecutionPermission"));
          PermissionSet permissionSet = AppDomain.CurrentDomain.ApplicationTrust.DefaultGrantSet.PermissionSet;
          if ((target.IsUnrestricted() ? 1 : (!target.IsSubsetOf(permissionSet) ? 0 : (permissionSet.IsSubsetOf(target) ? 1 : 0))) == 0)
            throw new PolicyException(Environment.GetResourceString("Policy_GrantSetDoesNotMatchDomain", (object) hostSecurityManager.GetType().FullName));
        }
        grantSet = target;
        return true;
      }
      if (AppDomain.CurrentDomain.IsHomogenous)
      {
        grantSet = AppDomain.CurrentDomain.GetHomogenousGrantSet(evidence);
        return true;
      }
      grantSet = (PermissionSet) null;
      return false;
    }

    [SecurityCritical]
    private static PermissionListSet UpdateAppDomainPLS(PermissionListSet adPLS, PermissionSet grantedPerms, PermissionSet refusedPerms)
    {
      if (adPLS == null)
      {
        adPLS = new PermissionListSet();
        adPLS.UpdateDomainPLS(grantedPerms, refusedPerms);
        return adPLS;
      }
      PermissionListSet permissionListSet = new PermissionListSet();
      PermissionListSet adPLS1 = adPLS;
      permissionListSet.UpdateDomainPLS(adPLS1);
      PermissionSet grantSet = grantedPerms;
      PermissionSet deniedSet = refusedPerms;
      permissionListSet.UpdateDomainPLS(grantSet, deniedSet);
      return permissionListSet;
    }
  }
}
