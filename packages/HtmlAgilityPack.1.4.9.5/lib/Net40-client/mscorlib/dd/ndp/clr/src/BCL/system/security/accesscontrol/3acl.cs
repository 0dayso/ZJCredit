﻿// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.CommonAcl
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security.Principal;

namespace System.Security.AccessControl
{
  /// <summary>表示访问控制列表 (ACL)，并且是 <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> 和 <see cref="T:System.Security.AccessControl.SystemAcl" /> 类的基类。</summary>
  public abstract class CommonAcl : GenericAcl
  {
    private static CommonAcl.PM[] AFtoPM = new CommonAcl.PM[16];
    private static CommonAcl.AF[] PMtoAF;
    private RawAcl _acl;
    private bool _isDirty;
    private readonly bool _isCanonical;
    private readonly bool _isContainer;
    private readonly bool _isDS;

    internal RawAcl RawAcl
    {
      get
      {
        return this._acl;
      }
    }

    /// <summary>获取 <see cref="T:System.Security.AccessControl.CommonAcl" /> 的修订级别。</summary>
    /// <returns>一个指定 <see cref="T:System.Security.AccessControl.CommonAcl" /> 的修订级别的字节值。</returns>
    public override sealed byte Revision
    {
      get
      {
        return this._acl.Revision;
      }
    }

    /// <summary>获取当前 <see cref="T:System.Security.AccessControl.CommonAcl" /> 对象中访问控制项 (ACE) 的数量。</summary>
    /// <returns>当前 <see cref="T:System.Security.AccessControl.CommonAcl" /> 对象中 ACE 的数量。</returns>
    public override sealed int Count
    {
      get
      {
        this.CanonicalizeIfNecessary();
        return this._acl.Count;
      }
    }

    /// <summary>获取当前 <see cref="T:System.Security.AccessControl.CommonAcl" /> 对象的二进制表示形式的长度（以字节为单位）。应该在使用 <see cref="M:System.Security.AccessControl.CommonAcl.GetBinaryForm(System.Byte[],System.Int32)" /> 方法将访问控制列表封送到二进制数组中之前使用此长度。</summary>
    /// <returns>获取当前 <see cref="T:System.Security.AccessControl.CommonAcl" /> 对象的二进制表示形式的长度（以字节为单位）。</returns>
    public override sealed int BinaryLength
    {
      get
      {
        this.CanonicalizeIfNecessary();
        return this._acl.BinaryLength;
      }
    }

    /// <summary>获取一个布尔值，该值指定当前 <see cref="T:System.Security.AccessControl.CommonAcl" /> 对象中的访问控制项 (ACE) 是否处于规范顺序。</summary>
    /// <returns>如果当前 <see cref="T:System.Security.AccessControl.CommonAcl" /> 对象中的 ACE 处于规范顺序，则为 true；否则为 false。</returns>
    public bool IsCanonical
    {
      get
      {
        return this._isCanonical;
      }
    }

    /// <summary>设置 <see cref="T:System.Security.AccessControl.CommonAcl" /> 对象是否为一个容器。</summary>
    /// <returns>如果当前 <see cref="T:System.Security.AccessControl.CommonAcl" /> 对象是一个容器，则为 true。</returns>
    public bool IsContainer
    {
      get
      {
        return this._isContainer;
      }
    }

    /// <summary>设置当前 <see cref="T:System.Security.AccessControl.CommonAcl" /> 对象是否为一个目录对象的访问控制列表 (ACL)。</summary>
    /// <returns>如果当前 <see cref="T:System.Security.AccessControl.CommonAcl" /> 对象是一个目录对象的 ACL，则为 true。</returns>
    public bool IsDS
    {
      get
      {
        return this._isDS;
      }
    }

    /// <summary>获取或设置指定索引处的 <see cref="T:System.Security.AccessControl.CommonAce" />。</summary>
    /// <returns>位于指定索引处的 <see cref="T:System.Security.AccessControl.CommonAce" />。</returns>
    /// <param name="index">要获取或设置的 <see cref="T:System.Security.AccessControl.CommonAce" /> 的从零开始的索引。</param>
    public override sealed GenericAce this[int index]
    {
      get
      {
        this.CanonicalizeIfNecessary();
        return this._acl[index].Copy();
      }
      set
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_SetMethod"));
      }
    }

    static CommonAcl()
    {
      for (int index = 0; index < CommonAcl.AFtoPM.Length; ++index)
        CommonAcl.AFtoPM[index] = CommonAcl.PM.GO;
      CommonAcl.AFtoPM[0] = CommonAcl.PM.F;
      CommonAcl.AFtoPM[4] = CommonAcl.PM.F | CommonAcl.PM.CO | CommonAcl.PM.GO;
      CommonAcl.AFtoPM[5] = CommonAcl.PM.F | CommonAcl.PM.CO;
      CommonAcl.AFtoPM[6] = CommonAcl.PM.CO | CommonAcl.PM.GO;
      CommonAcl.AFtoPM[7] = CommonAcl.PM.CO;
      CommonAcl.AFtoPM[8] = CommonAcl.PM.F | CommonAcl.PM.CF | CommonAcl.PM.GF;
      CommonAcl.AFtoPM[9] = CommonAcl.PM.F | CommonAcl.PM.CF;
      CommonAcl.AFtoPM[10] = CommonAcl.PM.CF | CommonAcl.PM.GF;
      CommonAcl.AFtoPM[11] = CommonAcl.PM.CF;
      CommonAcl.AFtoPM[12] = CommonAcl.PM.F | CommonAcl.PM.CF | CommonAcl.PM.CO | CommonAcl.PM.GF | CommonAcl.PM.GO;
      CommonAcl.AFtoPM[13] = CommonAcl.PM.F | CommonAcl.PM.CF | CommonAcl.PM.CO;
      CommonAcl.AFtoPM[14] = CommonAcl.PM.CF | CommonAcl.PM.CO | CommonAcl.PM.GF | CommonAcl.PM.GO;
      CommonAcl.AFtoPM[15] = CommonAcl.PM.CF | CommonAcl.PM.CO;
      CommonAcl.PMtoAF = new CommonAcl.AF[32];
      for (int index = 0; index < CommonAcl.PMtoAF.Length; ++index)
        CommonAcl.PMtoAF[index] = CommonAcl.AF.NP;
      CommonAcl.PMtoAF[16] = (CommonAcl.AF) 0;
      CommonAcl.PMtoAF[21] = CommonAcl.AF.OI;
      CommonAcl.PMtoAF[20] = CommonAcl.AF.OI | CommonAcl.AF.NP;
      CommonAcl.PMtoAF[5] = CommonAcl.AF.OI | CommonAcl.AF.IO;
      CommonAcl.PMtoAF[4] = CommonAcl.AF.OI | CommonAcl.AF.IO | CommonAcl.AF.NP;
      CommonAcl.PMtoAF[26] = CommonAcl.AF.CI;
      CommonAcl.PMtoAF[24] = CommonAcl.AF.CI | CommonAcl.AF.NP;
      CommonAcl.PMtoAF[10] = CommonAcl.AF.CI | CommonAcl.AF.IO;
      CommonAcl.PMtoAF[8] = CommonAcl.AF.CI | CommonAcl.AF.IO | CommonAcl.AF.NP;
      CommonAcl.PMtoAF[31] = CommonAcl.AF.CI | CommonAcl.AF.OI;
      CommonAcl.PMtoAF[28] = CommonAcl.AF.CI | CommonAcl.AF.OI | CommonAcl.AF.NP;
      CommonAcl.PMtoAF[15] = CommonAcl.AF.CI | CommonAcl.AF.OI | CommonAcl.AF.IO;
      CommonAcl.PMtoAF[12] = CommonAcl.AF.CI | CommonAcl.AF.OI | CommonAcl.AF.IO | CommonAcl.AF.NP;
    }

    internal CommonAcl(bool isContainer, bool isDS, byte revision, int capacity)
    {
      this._isContainer = isContainer;
      this._isDS = isDS;
      this._acl = new RawAcl(revision, capacity);
      this._isCanonical = true;
    }

    internal CommonAcl(bool isContainer, bool isDS, RawAcl rawAcl, bool trusted, bool isDacl)
    {
      if (rawAcl == null)
        throw new ArgumentNullException("rawAcl");
      this._isContainer = isContainer;
      this._isDS = isDS;
      if (trusted)
      {
        this._acl = rawAcl;
        this.RemoveMeaninglessAcesAndFlags(isDacl);
      }
      else
      {
        this._acl = new RawAcl(rawAcl.Revision, rawAcl.Count);
        for (int index = 0; index < rawAcl.Count; ++index)
        {
          GenericAce ace = rawAcl[index].Copy();
          if (this.InspectAce(ref ace, isDacl))
            this._acl.InsertAce(this._acl.Count, ace);
        }
      }
      if (this.CanonicalCheck(isDacl))
      {
        this.Canonicalize(true, isDacl);
        this._isCanonical = true;
      }
      else
        this._isCanonical = false;
    }

    private static CommonAcl.AF AFFromAceFlags(AceFlags aceFlags, bool isDS)
    {
      CommonAcl.AF af = (CommonAcl.AF) 0;
      if ((aceFlags & AceFlags.ContainerInherit) != AceFlags.None)
        af |= CommonAcl.AF.CI;
      if (!isDS && (aceFlags & AceFlags.ObjectInherit) != AceFlags.None)
        af |= CommonAcl.AF.OI;
      if ((aceFlags & AceFlags.InheritOnly) != AceFlags.None)
        af |= CommonAcl.AF.IO;
      if ((aceFlags & AceFlags.NoPropagateInherit) != AceFlags.None)
        af |= CommonAcl.AF.NP;
      return af;
    }

    private static AceFlags AceFlagsFromAF(CommonAcl.AF af, bool isDS)
    {
      AceFlags aceFlags = AceFlags.None;
      if ((af & CommonAcl.AF.CI) != (CommonAcl.AF) 0)
        aceFlags |= AceFlags.ContainerInherit;
      if (!isDS && (af & CommonAcl.AF.OI) != (CommonAcl.AF) 0)
        aceFlags |= AceFlags.ObjectInherit;
      if ((af & CommonAcl.AF.IO) != (CommonAcl.AF) 0)
        aceFlags |= AceFlags.InheritOnly;
      if ((af & CommonAcl.AF.NP) != (CommonAcl.AF) 0)
        aceFlags |= AceFlags.NoPropagateInherit;
      return aceFlags;
    }

    private static bool MergeInheritanceBits(AceFlags left, AceFlags right, bool isDS, out AceFlags result)
    {
      result = AceFlags.None;
      CommonAcl.AF af1 = CommonAcl.AFFromAceFlags(left, isDS);
      CommonAcl.AF af2 = CommonAcl.AFFromAceFlags(right, isDS);
      CommonAcl.PM pm1 = CommonAcl.AFtoPM[(int) af1];
      CommonAcl.PM pm2 = CommonAcl.AFtoPM[(int) af2];
      if (pm1 == CommonAcl.PM.GO || pm2 == CommonAcl.PM.GO)
        return false;
      CommonAcl.PM pm3 = pm1 | pm2;
      CommonAcl.AF af3 = CommonAcl.PMtoAF[(int) pm3];
      if (af3 == CommonAcl.AF.NP)
        return false;
      result = CommonAcl.AceFlagsFromAF(af3, isDS);
      return true;
    }

    private static bool RemoveInheritanceBits(AceFlags existing, AceFlags remove, bool isDS, out AceFlags result, out bool total)
    {
      result = AceFlags.None;
      total = false;
      CommonAcl.AF af1 = CommonAcl.AFFromAceFlags(existing, isDS);
      CommonAcl.AF af2 = CommonAcl.AFFromAceFlags(remove, isDS);
      CommonAcl.PM pm1 = CommonAcl.AFtoPM[(int) af1];
      CommonAcl.PM pm2 = CommonAcl.AFtoPM[(int) af2];
      if (pm1 == CommonAcl.PM.GO || pm2 == CommonAcl.PM.GO)
        return false;
      CommonAcl.PM pm3 = pm1 & ~pm2;
      if (pm3 == (CommonAcl.PM) 0)
      {
        total = true;
        return true;
      }
      CommonAcl.AF af3 = CommonAcl.PMtoAF[(int) pm3];
      if (af3 == CommonAcl.AF.NP)
        return false;
      result = CommonAcl.AceFlagsFromAF(af3, isDS);
      return true;
    }

    private void CanonicalizeIfNecessary()
    {
      if (!this._isDirty)
        return;
      this.Canonicalize(false, this is DiscretionaryAcl);
      this._isDirty = false;
    }

    private static int DaclAcePriority(GenericAce ace)
    {
      AceType aceType = ace.AceType;
      return (ace.AceFlags & AceFlags.Inherited) == AceFlags.None ? (aceType == AceType.AccessDenied || aceType == AceType.AccessDeniedCallback ? 0 : (aceType == AceType.AccessDeniedObject || aceType == AceType.AccessDeniedCallbackObject ? 1 : (aceType == AceType.AccessAllowed || aceType == AceType.AccessAllowedCallback ? 2 : (aceType == AceType.AccessAllowedObject || aceType == AceType.AccessAllowedCallbackObject ? 3 : (int) ushort.MaxValue + (int) ace._indexInAcl)))) : 131070 + (int) ace._indexInAcl;
    }

    private static int SaclAcePriority(GenericAce ace)
    {
      AceType aceType = ace.AceType;
      return (ace.AceFlags & AceFlags.Inherited) == AceFlags.None ? (aceType == AceType.SystemAudit || aceType == AceType.SystemAlarm || (aceType == AceType.SystemAuditCallback || aceType == AceType.SystemAlarmCallback) ? 0 : (aceType == AceType.SystemAuditObject || aceType == AceType.SystemAlarmObject || (aceType == AceType.SystemAuditCallbackObject || aceType == AceType.SystemAlarmCallbackObject) ? 1 : (int) ushort.MaxValue + (int) ace._indexInAcl)) : 131070 + (int) ace._indexInAcl;
    }

    private static CommonAcl.ComparisonResult CompareAces(GenericAce ace1, GenericAce ace2, bool isDacl)
    {
      int num1 = isDacl ? CommonAcl.DaclAcePriority(ace1) : CommonAcl.SaclAcePriority(ace1);
      int num2 = isDacl ? CommonAcl.DaclAcePriority(ace2) : CommonAcl.SaclAcePriority(ace2);
      if (num1 < num2)
        return CommonAcl.ComparisonResult.LessThan;
      if (num1 > num2)
        return CommonAcl.ComparisonResult.GreaterThan;
      KnownAce knownAce1 = ace1 as KnownAce;
      KnownAce knownAce2 = ace2 as KnownAce;
      if ((GenericAce) knownAce1 != (GenericAce) null && (GenericAce) knownAce2 != (GenericAce) null)
      {
        int num3 = knownAce1.SecurityIdentifier.CompareTo(knownAce2.SecurityIdentifier);
        if (num3 < 0)
          return CommonAcl.ComparisonResult.LessThan;
        if (num3 > 0)
          return CommonAcl.ComparisonResult.GreaterThan;
      }
      return CommonAcl.ComparisonResult.EqualTo;
    }

    private void QuickSort(int left, int right, bool isDacl)
    {
      if (left >= right)
        return;
      int num1 = left;
      int num2 = right;
      GenericAce ace2 = this._acl[left];
      while (left < right)
      {
        while (CommonAcl.CompareAces(this._acl[right], ace2, isDacl) != CommonAcl.ComparisonResult.LessThan && left < right)
          --right;
        if (left != right)
        {
          this._acl[left] = this._acl[right];
          ++left;
        }
        while (CommonAcl.ComparisonResult.GreaterThan != CommonAcl.CompareAces(this._acl[left], ace2, isDacl) && left < right)
          ++left;
        if (left != right)
        {
          this._acl[right] = this._acl[left];
          --right;
        }
      }
      this._acl[left] = ace2;
      int num3 = left;
      left = num1;
      right = num2;
      if (left < num3)
        this.QuickSort(left, num3 - 1, isDacl);
      if (right <= num3)
        return;
      this.QuickSort(num3 + 1, right, isDacl);
    }

    private bool InspectAce(ref GenericAce ace, bool isDacl)
    {
      KnownAce knownAce = ace as KnownAce;
      if ((GenericAce) knownAce != (GenericAce) null && knownAce.AccessMask == 0)
        return false;
      if (!this.IsContainer)
      {
        if ((ace.AceFlags & AceFlags.InheritOnly) != AceFlags.None)
          return false;
        if ((ace.AceFlags & AceFlags.InheritanceFlags) != AceFlags.None)
          ace.AceFlags &= ~AceFlags.InheritanceFlags;
      }
      else
      {
        if ((ace.AceFlags & AceFlags.InheritOnly) != AceFlags.None && (ace.AceFlags & AceFlags.ContainerInherit) == AceFlags.None && (ace.AceFlags & AceFlags.ObjectInherit) == AceFlags.None)
          return false;
        if ((ace.AceFlags & AceFlags.NoPropagateInherit) != AceFlags.None && (ace.AceFlags & AceFlags.ContainerInherit) == AceFlags.None && (ace.AceFlags & AceFlags.ObjectInherit) == AceFlags.None)
          ace.AceFlags &= ~AceFlags.NoPropagateInherit;
      }
      QualifiedAce qualifiedAce = knownAce as QualifiedAce;
      if (isDacl)
      {
        ace.AceFlags &= ~AceFlags.AuditFlags;
        if ((GenericAce) qualifiedAce != (GenericAce) null && qualifiedAce.AceQualifier != AceQualifier.AccessAllowed && qualifiedAce.AceQualifier != AceQualifier.AccessDenied)
          return false;
      }
      else if ((ace.AceFlags & AceFlags.AuditFlags) == AceFlags.None || (GenericAce) qualifiedAce != (GenericAce) null && qualifiedAce.AceQualifier != AceQualifier.SystemAudit)
        return false;
      return true;
    }

    private void RemoveMeaninglessAcesAndFlags(bool isDacl)
    {
      for (int index = this._acl.Count - 1; index >= 0; --index)
      {
        GenericAce ace = this._acl[index];
        if (!this.InspectAce(ref ace, isDacl))
          this._acl.RemoveAce(index);
      }
    }

    private void Canonicalize(bool compact, bool isDacl)
    {
      for (ushort index = 0; (int) index < this._acl.Count; ++index)
        this._acl[(int) index]._indexInAcl = index;
      this.QuickSort(0, this._acl.Count - 1, isDacl);
      if (!compact)
        return;
      for (int index = 0; index < this.Count - 1; ++index)
      {
        QualifiedAce ace = this._acl[index] as QualifiedAce;
        if (!((GenericAce) ace == (GenericAce) null))
        {
          QualifiedAce newAce = this._acl[index + 1] as QualifiedAce;
          if (!((GenericAce) newAce == (GenericAce) null) && this.MergeAces(ref ace, newAce))
            this._acl.RemoveAce(index + 1);
        }
      }
    }

    private void GetObjectTypesForSplit(ObjectAce originalAce, int accessMask, AceFlags aceFlags, out ObjectAceFlags objectFlags, out Guid objectType, out Guid inheritedObjectType)
    {
      objectFlags = ObjectAceFlags.None;
      objectType = Guid.Empty;
      inheritedObjectType = Guid.Empty;
      if ((accessMask & ObjectAce.AccessMaskWithObjectType) != 0)
      {
        objectType = originalAce.ObjectAceType;
        objectFlags |= originalAce.ObjectAceFlags & ObjectAceFlags.ObjectAceTypePresent;
      }
      if ((aceFlags & AceFlags.ContainerInherit) == AceFlags.None)
        return;
      inheritedObjectType = originalAce.InheritedObjectAceType;
      objectFlags |= originalAce.ObjectAceFlags & ObjectAceFlags.InheritedObjectAceTypePresent;
    }

    private bool ObjectTypesMatch(QualifiedAce ace, QualifiedAce newAce)
    {
      return (ace is ObjectAce ? ((ObjectAce) ace).ObjectAceType : Guid.Empty).Equals(newAce is ObjectAce ? ((ObjectAce) newAce).ObjectAceType : Guid.Empty);
    }

    private bool InheritedObjectTypesMatch(QualifiedAce ace, QualifiedAce newAce)
    {
      return (ace is ObjectAce ? ((ObjectAce) ace).InheritedObjectAceType : Guid.Empty).Equals(newAce is ObjectAce ? ((ObjectAce) newAce).InheritedObjectAceType : Guid.Empty);
    }

    private bool AccessMasksAreMergeable(QualifiedAce ace, QualifiedAce newAce)
    {
      if (this.ObjectTypesMatch(ace, newAce))
        return true;
      ObjectAceFlags objectAceFlags = ace is ObjectAce ? ((ObjectAce) ace).ObjectAceFlags : ObjectAceFlags.None;
      return (ace.AccessMask & newAce.AccessMask & ObjectAce.AccessMaskWithObjectType) == (newAce.AccessMask & ObjectAce.AccessMaskWithObjectType) && (objectAceFlags & ObjectAceFlags.ObjectAceTypePresent) == ObjectAceFlags.None;
    }

    private bool AceFlagsAreMergeable(QualifiedAce ace, QualifiedAce newAce)
    {
      return this.InheritedObjectTypesMatch(ace, newAce) || ((ace is ObjectAce ? (int) ((ObjectAce) ace).ObjectAceFlags : 0) & 2) == 0;
    }

    private bool GetAccessMaskForRemoval(QualifiedAce ace, ObjectAceFlags objectFlags, Guid objectType, ref int accessMask)
    {
      if ((ace.AccessMask & accessMask & ObjectAce.AccessMaskWithObjectType) != 0)
      {
        if (ace is ObjectAce)
        {
          ObjectAce objectAce = ace as ObjectAce;
          if ((objectFlags & ObjectAceFlags.ObjectAceTypePresent) != ObjectAceFlags.None && (objectAce.ObjectAceFlags & ObjectAceFlags.ObjectAceTypePresent) == ObjectAceFlags.None)
            return false;
          if (((objectFlags & ObjectAceFlags.ObjectAceTypePresent) == ObjectAceFlags.None ? 1 : (objectAce.ObjectTypesMatch(objectFlags, objectType) ? 1 : 0)) == 0)
            accessMask &= ~ObjectAce.AccessMaskWithObjectType;
        }
        else if ((objectFlags & ObjectAceFlags.ObjectAceTypePresent) != ObjectAceFlags.None)
          return false;
      }
      return true;
    }

    private bool GetInheritanceFlagsForRemoval(QualifiedAce ace, ObjectAceFlags objectFlags, Guid inheritedObjectType, ref AceFlags aceFlags)
    {
      if ((ace.AceFlags & AceFlags.ContainerInherit) != AceFlags.None && (aceFlags & AceFlags.ContainerInherit) != AceFlags.None)
      {
        if (ace is ObjectAce)
        {
          ObjectAce objectAce = ace as ObjectAce;
          if ((objectFlags & ObjectAceFlags.InheritedObjectAceTypePresent) != ObjectAceFlags.None && (objectAce.ObjectAceFlags & ObjectAceFlags.InheritedObjectAceTypePresent) == ObjectAceFlags.None)
            return false;
          if (((objectFlags & ObjectAceFlags.InheritedObjectAceTypePresent) == ObjectAceFlags.None ? 1 : (objectAce.InheritedObjectTypesMatch(objectFlags, inheritedObjectType) ? 1 : 0)) == 0)
            aceFlags &= ~AceFlags.InheritanceFlags;
        }
        else if ((objectFlags & ObjectAceFlags.InheritedObjectAceTypePresent) != ObjectAceFlags.None)
          return false;
      }
      return true;
    }

    private static bool AceOpaquesMatch(QualifiedAce ace, QualifiedAce newAce)
    {
      byte[] opaque1 = ace.GetOpaque();
      byte[] opaque2 = newAce.GetOpaque();
      if (opaque1 == null || opaque2 == null)
        return opaque1 == opaque2;
      if (opaque1.Length != opaque2.Length)
        return false;
      for (int index = 0; index < opaque1.Length; ++index)
      {
        if ((int) opaque1[index] != (int) opaque2[index])
          return false;
      }
      return true;
    }

    private static bool AcesAreMergeable(QualifiedAce ace, QualifiedAce newAce)
    {
      return ace.AceType == newAce.AceType && (ace.AceFlags & AceFlags.Inherited) == AceFlags.None && ((newAce.AceFlags & AceFlags.Inherited) == AceFlags.None && ace.AceQualifier == newAce.AceQualifier) && (!(ace.SecurityIdentifier != newAce.SecurityIdentifier) && CommonAcl.AceOpaquesMatch(ace, newAce));
    }

    private bool MergeAces(ref QualifiedAce ace, QualifiedAce newAce)
    {
      if (!CommonAcl.AcesAreMergeable(ace, newAce))
        return false;
      if (ace.AceFlags == newAce.AceFlags)
      {
        if (ace is ObjectAce || newAce is ObjectAce)
        {
          if (this.InheritedObjectTypesMatch(ace, newAce) && this.AccessMasksAreMergeable(ace, newAce))
          {
            ace.AccessMask |= newAce.AccessMask;
            return true;
          }
        }
        else
        {
          ace.AccessMask |= newAce.AccessMask;
          return true;
        }
      }
      if ((ace.AceFlags & AceFlags.InheritanceFlags) == (newAce.AceFlags & AceFlags.InheritanceFlags) && ace.AccessMask == newAce.AccessMask)
      {
        if (ace is ObjectAce || newAce is ObjectAce)
        {
          if (this.InheritedObjectTypesMatch(ace, newAce) && this.ObjectTypesMatch(ace, newAce))
          {
            ace.AceFlags |= newAce.AceFlags & AceFlags.AuditFlags;
            return true;
          }
        }
        else
        {
          ace.AceFlags |= newAce.AceFlags & AceFlags.AuditFlags;
          return true;
        }
      }
      if ((ace.AceFlags & AceFlags.AuditFlags) == (newAce.AceFlags & AceFlags.AuditFlags) && ace.AccessMask == newAce.AccessMask)
      {
        if (ace is ObjectAce || newAce is ObjectAce)
        {
          AceFlags result;
          if (this.ObjectTypesMatch(ace, newAce) && this.AceFlagsAreMergeable(ace, newAce) && CommonAcl.MergeInheritanceBits(ace.AceFlags, newAce.AceFlags, this.IsDS, out result))
          {
            ace.AceFlags = result | ace.AceFlags & AceFlags.AuditFlags;
            return true;
          }
        }
        else
        {
          AceFlags result;
          if (CommonAcl.MergeInheritanceBits(ace.AceFlags, newAce.AceFlags, this.IsDS, out result))
          {
            ace.AceFlags = result | ace.AceFlags & AceFlags.AuditFlags;
            return true;
          }
        }
      }
      return false;
    }

    private bool CanonicalCheck(bool isDacl)
    {
      if (isDacl)
      {
        int num1 = 0;
        for (int index = 0; index < this._acl.Count; ++index)
        {
          GenericAce genericAce = this._acl[index];
          int num2;
          if ((genericAce.AceFlags & AceFlags.Inherited) != AceFlags.None)
          {
            num2 = 2;
          }
          else
          {
            QualifiedAce qualifiedAce = genericAce as QualifiedAce;
            if ((GenericAce) qualifiedAce == (GenericAce) null)
              return false;
            if (qualifiedAce.AceQualifier == AceQualifier.AccessAllowed)
            {
              num2 = 1;
            }
            else
            {
              if (qualifiedAce.AceQualifier != AceQualifier.AccessDenied)
                return false;
              num2 = 0;
            }
          }
          if (num2 != 3)
          {
            if (num2 > num1)
              num1 = num2;
            else if (num2 < num1)
              return false;
          }
        }
      }
      else
      {
        int num1 = 0;
        for (int index = 0; index < this._acl.Count; ++index)
        {
          GenericAce genericAce = this._acl[index];
          if (!(genericAce == (GenericAce) null))
          {
            int num2;
            if ((genericAce.AceFlags & AceFlags.Inherited) != AceFlags.None)
            {
              num2 = 1;
            }
            else
            {
              QualifiedAce qualifiedAce = genericAce as QualifiedAce;
              if ((GenericAce) qualifiedAce == (GenericAce) null || qualifiedAce.AceQualifier != AceQualifier.SystemAudit && qualifiedAce.AceQualifier != AceQualifier.SystemAlarm)
                return false;
              num2 = 0;
            }
            if (num2 > num1)
              num1 = num2;
            else if (num2 < num1)
              return false;
          }
        }
      }
      return true;
    }

    private void ThrowIfNotCanonical()
    {
      if (!this._isCanonical)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ModificationOfNonCanonicalAcl"));
    }

    internal void CheckAccessType(AccessControlType accessType)
    {
      if (accessType != AccessControlType.Allow && accessType != AccessControlType.Deny)
        throw new ArgumentOutOfRangeException("accessType", Environment.GetResourceString("ArgumentOutOfRange_Enum"));
    }

    internal void CheckFlags(InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags)
    {
      if (this.IsContainer)
      {
        if (inheritanceFlags == InheritanceFlags.None && propagationFlags != PropagationFlags.None)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidAnyFlag"), "propagationFlags");
      }
      else
      {
        if (inheritanceFlags != InheritanceFlags.None)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidAnyFlag"), "inheritanceFlags");
        if (propagationFlags != PropagationFlags.None)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidAnyFlag"), "propagationFlags");
      }
    }

    internal void AddQualifiedAce(SecurityIdentifier sid, AceQualifier qualifier, int accessMask, AceFlags flags, ObjectAceFlags objectFlags, Guid objectType, Guid inheritedObjectType)
    {
      if (sid == (SecurityIdentifier) null)
        throw new ArgumentNullException("sid");
      this.ThrowIfNotCanonical();
      bool flag = false;
      if (qualifier == AceQualifier.SystemAudit && (flags & AceFlags.AuditFlags) == AceFlags.None)
        throw new ArgumentException(Environment.GetResourceString("Arg_EnumAtLeastOneFlag"), "flags");
      if (accessMask == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_ArgumentZero"), "accessMask");
      GenericAce ace1 = !this.IsDS || objectFlags == ObjectAceFlags.None ? (GenericAce) new CommonAce(flags, qualifier, accessMask, sid, false, (byte[]) null) : (GenericAce) new ObjectAce(flags, qualifier, accessMask, sid, objectFlags, objectType, inheritedObjectType, false, (byte[]) null);
      if (!this.InspectAce(ref ace1, this is DiscretionaryAcl))
        return;
      for (int index = 0; index < this.Count; ++index)
      {
        QualifiedAce ace2 = this._acl[index] as QualifiedAce;
        if (!((GenericAce) ace2 == (GenericAce) null) && this.MergeAces(ref ace2, ace1 as QualifiedAce))
        {
          flag = true;
          break;
        }
      }
      if (!flag)
      {
        this._acl.InsertAce(this._acl.Count, ace1);
        this._isDirty = true;
      }
      this.OnAclModificationTried();
    }

    internal void SetQualifiedAce(SecurityIdentifier sid, AceQualifier qualifier, int accessMask, AceFlags flags, ObjectAceFlags objectFlags, Guid objectType, Guid inheritedObjectType)
    {
      if (sid == (SecurityIdentifier) null)
        throw new ArgumentNullException("sid");
      if (qualifier == AceQualifier.SystemAudit && (flags & AceFlags.AuditFlags) == AceFlags.None)
        throw new ArgumentException(Environment.GetResourceString("Arg_EnumAtLeastOneFlag"), "flags");
      if (accessMask == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_ArgumentZero"), "accessMask");
      this.ThrowIfNotCanonical();
      GenericAce ace = !this.IsDS || objectFlags == ObjectAceFlags.None ? (GenericAce) new CommonAce(flags, qualifier, accessMask, sid, false, (byte[]) null) : (GenericAce) new ObjectAce(flags, qualifier, accessMask, sid, objectFlags, objectType, inheritedObjectType, false, (byte[]) null);
      if (!this.InspectAce(ref ace, this is DiscretionaryAcl))
        return;
      for (int index = 0; index < this.Count; ++index)
      {
        QualifiedAce qualifiedAce = this._acl[index] as QualifiedAce;
        if (!((GenericAce) qualifiedAce == (GenericAce) null) && (qualifiedAce.AceFlags & AceFlags.Inherited) == AceFlags.None && (qualifiedAce.AceQualifier == qualifier && !(qualifiedAce.SecurityIdentifier != sid)))
        {
          this._acl.RemoveAce(index);
          --index;
        }
      }
      this._acl.InsertAce(this._acl.Count, ace);
      this._isDirty = true;
      this.OnAclModificationTried();
    }

    internal bool RemoveQualifiedAces(SecurityIdentifier sid, AceQualifier qualifier, int accessMask, AceFlags flags, bool saclSemantics, ObjectAceFlags objectFlags, Guid objectType, Guid inheritedObjectType)
    {
      if (accessMask == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_ArgumentZero"), "accessMask");
      if (qualifier == AceQualifier.SystemAudit && (flags & AceFlags.AuditFlags) == AceFlags.None)
        throw new ArgumentException(Environment.GetResourceString("Arg_EnumAtLeastOneFlag"), "flags");
      if (sid == (SecurityIdentifier) null)
        throw new ArgumentNullException("sid");
      this.ThrowIfNotCanonical();
      bool flag1 = true;
      bool flag2 = true;
      int num1 = accessMask;
      AceFlags aceFlags1 = flags;
      byte[] binaryForm = new byte[this.BinaryLength];
      this.GetBinaryForm(binaryForm, 0);
      while (true)
      {
        try
        {
          for (int index = 0; index < this.Count; ++index)
          {
            QualifiedAce ace = this._acl[index] as QualifiedAce;
            if (!((GenericAce) ace == (GenericAce) null) && (ace.AceFlags & AceFlags.Inherited) == AceFlags.None && (ace.AceQualifier == qualifier && !(ace.SecurityIdentifier != sid)))
            {
              if (this.IsDS)
              {
                accessMask = num1;
                bool flag3 = !this.GetAccessMaskForRemoval(ace, objectFlags, objectType, ref accessMask);
                if ((ace.AccessMask & accessMask) != 0)
                {
                  flags = aceFlags1;
                  bool flag4 = !this.GetInheritanceFlagsForRemoval(ace, objectFlags, inheritedObjectType, ref flags);
                  if (((ace.AceFlags & AceFlags.ContainerInherit) != AceFlags.None || (flags & AceFlags.ContainerInherit) == AceFlags.None || (flags & AceFlags.InheritOnly) == AceFlags.None) && ((flags & AceFlags.ContainerInherit) != AceFlags.None || (ace.AceFlags & AceFlags.ContainerInherit) == AceFlags.None || (ace.AceFlags & AceFlags.InheritOnly) == AceFlags.None) && ((aceFlags1 & AceFlags.ContainerInherit) == AceFlags.None || (aceFlags1 & AceFlags.InheritOnly) == AceFlags.None || (flags & AceFlags.ContainerInherit) != AceFlags.None))
                  {
                    if (flag3 | flag4)
                    {
                      flag2 = false;
                      break;
                    }
                  }
                  else
                    continue;
                }
                else
                  continue;
              }
              else if ((ace.AccessMask & accessMask) == 0)
                continue;
              if (!saclSemantics || (ace.AceFlags & flags & AceFlags.AuditFlags) != AceFlags.None)
              {
                ObjectAceFlags objectFlags1 = ObjectAceFlags.None;
                Guid objectType1 = Guid.Empty;
                Guid inheritedObjectType1 = Guid.Empty;
                AceFlags aceFlags2 = AceFlags.None;
                int accessMask1 = 0;
                ObjectAceFlags objectFlags2 = ObjectAceFlags.None;
                Guid objectType2 = Guid.Empty;
                Guid inheritedObjectType2 = Guid.Empty;
                ObjectAceFlags objectFlags3 = ObjectAceFlags.None;
                Guid objectType3 = Guid.Empty;
                Guid inheritedObjectType3 = Guid.Empty;
                AceFlags result = AceFlags.None;
                bool total = false;
                AceFlags aceFlags3 = ace.AceFlags;
                int accessMask2 = ace.AccessMask & ~accessMask;
                if (ace is ObjectAce)
                  this.GetObjectTypesForSplit(ace as ObjectAce, accessMask2, aceFlags3, out objectFlags1, out objectType1, out inheritedObjectType1);
                if (saclSemantics)
                {
                  aceFlags2 = ace.AceFlags & ~(flags & AceFlags.AuditFlags);
                  accessMask1 = ace.AccessMask & accessMask;
                  if (ace is ObjectAce)
                    this.GetObjectTypesForSplit(ace as ObjectAce, accessMask1, aceFlags2, out objectFlags2, out objectType2, out inheritedObjectType2);
                }
                AceFlags existing = ace.AceFlags & AceFlags.InheritanceFlags | flags & ace.AceFlags & AceFlags.AuditFlags;
                int accessMask3 = ace.AccessMask & accessMask;
                if (!saclSemantics || (existing & AceFlags.AuditFlags) != AceFlags.None)
                {
                  if (!CommonAcl.RemoveInheritanceBits(existing, flags, this.IsDS, out result, out total))
                  {
                    flag2 = false;
                    break;
                  }
                  if (!total)
                  {
                    result |= existing & AceFlags.AuditFlags;
                    if (ace is ObjectAce)
                      this.GetObjectTypesForSplit(ace as ObjectAce, accessMask3, result, out objectFlags3, out objectType3, out inheritedObjectType3);
                  }
                }
                if (!flag1)
                {
                  if (accessMask2 != 0)
                  {
                    if (ace is ObjectAce && (((ObjectAce) ace).ObjectAceFlags & ObjectAceFlags.ObjectAceTypePresent) != ObjectAceFlags.None && (objectFlags1 & ObjectAceFlags.ObjectAceTypePresent) == ObjectAceFlags.None)
                    {
                      this._acl.RemoveAce(index);
                      ObjectAce objectAce = new ObjectAce(aceFlags3, qualifier, accessMask2, ace.SecurityIdentifier, objectFlags1, objectType1, inheritedObjectType1, false, (byte[]) null);
                      this._acl.InsertAce(index, (GenericAce) objectAce);
                    }
                    else
                    {
                      ace.AceFlags = aceFlags3;
                      ace.AccessMask = accessMask2;
                      if (ace is ObjectAce)
                      {
                        ObjectAce objectAce = ace as ObjectAce;
                        int num2 = (int) objectFlags1;
                        objectAce.ObjectAceFlags = (ObjectAceFlags) num2;
                        Guid guid1 = objectType1;
                        objectAce.ObjectAceType = guid1;
                        Guid guid2 = inheritedObjectType1;
                        objectAce.InheritedObjectAceType = guid2;
                      }
                    }
                  }
                  else
                  {
                    this._acl.RemoveAce(index);
                    --index;
                  }
                  if (saclSemantics && (aceFlags2 & AceFlags.AuditFlags) != AceFlags.None)
                  {
                    QualifiedAce qualifiedAce = !(ace is CommonAce) ? (QualifiedAce) new ObjectAce(aceFlags2, qualifier, accessMask1, ace.SecurityIdentifier, objectFlags2, objectType2, inheritedObjectType2, false, (byte[]) null) : (QualifiedAce) new CommonAce(aceFlags2, qualifier, accessMask1, ace.SecurityIdentifier, false, (byte[]) null);
                    ++index;
                    this._acl.InsertAce(index, (GenericAce) qualifiedAce);
                  }
                  if (!total)
                  {
                    QualifiedAce qualifiedAce = !(ace is CommonAce) ? (QualifiedAce) new ObjectAce(result, qualifier, accessMask3, ace.SecurityIdentifier, objectFlags3, objectType3, inheritedObjectType3, false, (byte[]) null) : (QualifiedAce) new CommonAce(result, qualifier, accessMask3, ace.SecurityIdentifier, false, (byte[]) null);
                    ++index;
                    this._acl.InsertAce(index, (GenericAce) qualifiedAce);
                  }
                }
              }
            }
          }
        }
        catch (OverflowException ex)
        {
          this._acl.SetBinaryForm(binaryForm, 0);
          return false;
        }
        if (flag1 & flag2)
          flag1 = false;
        else
          break;
      }
      this.OnAclModificationTried();
      return flag2;
    }

    internal void RemoveQualifiedAcesSpecific(SecurityIdentifier sid, AceQualifier qualifier, int accessMask, AceFlags flags, ObjectAceFlags objectFlags, Guid objectType, Guid inheritedObjectType)
    {
      if (accessMask == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_ArgumentZero"), "accessMask");
      if (qualifier == AceQualifier.SystemAudit && (flags & AceFlags.AuditFlags) == AceFlags.None)
        throw new ArgumentException(Environment.GetResourceString("Arg_EnumAtLeastOneFlag"), "flags");
      if (sid == (SecurityIdentifier) null)
        throw new ArgumentNullException("sid");
      this.ThrowIfNotCanonical();
      for (int index = 0; index < this.Count; ++index)
      {
        QualifiedAce qualifiedAce = this._acl[index] as QualifiedAce;
        if (!((GenericAce) qualifiedAce == (GenericAce) null) && (qualifiedAce.AceFlags & AceFlags.Inherited) == AceFlags.None && (qualifiedAce.AceQualifier == qualifier && !(qualifiedAce.SecurityIdentifier != sid)) && (qualifiedAce.AceFlags == flags && qualifiedAce.AccessMask == accessMask))
        {
          if (this.IsDS)
          {
            if (qualifiedAce is ObjectAce && objectFlags != ObjectAceFlags.None)
            {
              ObjectAce objectAce = qualifiedAce as ObjectAce;
              if (!objectAce.ObjectTypesMatch(objectFlags, objectType) || !objectAce.InheritedObjectTypesMatch(objectFlags, inheritedObjectType))
                continue;
            }
            else if (qualifiedAce is ObjectAce || objectFlags != ObjectAceFlags.None)
              continue;
          }
          this._acl.RemoveAce(index);
          --index;
        }
      }
      this.OnAclModificationTried();
    }

    internal virtual void OnAclModificationTried()
    {
    }

    /// <summary>将 <see cref="T:System.Security.AccessControl.CommonAcl" /> 对象的内容从指定的偏移量开始封送到指定的字节数组中。</summary>
    /// <param name="binaryForm">
    /// <see cref="T:System.Security.AccessControl.CommonAcl" /> 的内容将被封送到的字节数组。</param>
    /// <param name="offset">开始封送的偏移量。</param>
    public override sealed void GetBinaryForm(byte[] binaryForm, int offset)
    {
      this.CanonicalizeIfNecessary();
      this._acl.GetBinaryForm(binaryForm, offset);
    }

    /// <summary>从此 <see cref="T:System.Security.AccessControl.CommonAcl" /> 对象移除所有继承的访问控制项 (ACE)。</summary>
    public void RemoveInheritedAces()
    {
      this.ThrowIfNotCanonical();
      for (int index = this._acl.Count - 1; index >= 0; --index)
      {
        if ((this._acl[index].AceFlags & AceFlags.Inherited) != AceFlags.None)
          this._acl.RemoveAce(index);
      }
      this.OnAclModificationTried();
    }

    /// <summary>移除被此 <see cref="T:System.Security.AccessControl.CommonAcl" /> 对象包含并且与指定的 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 对象关联的所有访问控制项 (ACE)。</summary>
    /// <param name="sid">要检查的 <see cref="T:System.Security.Principal.SecurityIdentifier" /> 对象。</param>
    public void Purge(SecurityIdentifier sid)
    {
      if (sid == (SecurityIdentifier) null)
        throw new ArgumentNullException("sid");
      this.ThrowIfNotCanonical();
      for (int index = this.Count - 1; index >= 0; --index)
      {
        KnownAce knownAce = this._acl[index] as KnownAce;
        if (!((GenericAce) knownAce == (GenericAce) null) && (knownAce.AceFlags & AceFlags.Inherited) == AceFlags.None && knownAce.SecurityIdentifier == sid)
          this._acl.RemoveAce(index);
      }
      this.OnAclModificationTried();
    }

    [Flags]
    private enum AF
    {
      CI = 8,
      OI = 4,
      IO = 2,
      NP = 1,
      Invalid = NP,
    }

    [Flags]
    private enum PM
    {
      F = 16,
      CF = 8,
      CO = 4,
      GF = 2,
      GO = 1,
      Invalid = GO,
    }

    private enum ComparisonResult
    {
      LessThan,
      EqualTo,
      GreaterThan,
    }
  }
}
