﻿// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.LegacyEvidenceList
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;

namespace System.Security.Policy
{
  [Serializable]
  internal sealed class LegacyEvidenceList : EvidenceBase, IEnumerable<EvidenceBase>, IEnumerable, ILegacyEvidenceAdapter
  {
    private List<EvidenceBase> m_legacyEvidenceList = new List<EvidenceBase>();

    public object EvidenceObject
    {
      get
      {
        if (this.m_legacyEvidenceList.Count <= 0)
          return (object) null;
        return (object) this.m_legacyEvidenceList[0];
      }
    }

    public Type EvidenceType
    {
      get
      {
        ILegacyEvidenceAdapter legacyEvidenceAdapter = this.m_legacyEvidenceList[0] as ILegacyEvidenceAdapter;
        if (legacyEvidenceAdapter != null)
          return legacyEvidenceAdapter.EvidenceType;
        return this.m_legacyEvidenceList[0].GetType();
      }
    }

    public void Add(EvidenceBase evidence)
    {
      this.m_legacyEvidenceList.Add(evidence);
    }

    public IEnumerator<EvidenceBase> GetEnumerator()
    {
      return (IEnumerator<EvidenceBase>) this.m_legacyEvidenceList.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.m_legacyEvidenceList.GetEnumerator();
    }

    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
    public override EvidenceBase Clone()
    {
      return base.Clone();
    }
  }
}
