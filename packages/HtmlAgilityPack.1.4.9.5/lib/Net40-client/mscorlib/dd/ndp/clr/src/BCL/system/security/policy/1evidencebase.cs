// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.LegacyEvidenceWrapper
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security.Permissions;

namespace System.Security.Policy
{
  [Serializable]
  internal sealed class LegacyEvidenceWrapper : EvidenceBase, ILegacyEvidenceAdapter
  {
    private object m_legacyEvidence;

    public object EvidenceObject
    {
      get
      {
        return this.m_legacyEvidence;
      }
    }

    public Type EvidenceType
    {
      get
      {
        return this.m_legacyEvidence.GetType();
      }
    }

    internal LegacyEvidenceWrapper(object legacyEvidence)
    {
      this.m_legacyEvidence = legacyEvidence;
    }

    public override bool Equals(object obj)
    {
      return this.m_legacyEvidence.Equals(obj);
    }

    public override int GetHashCode()
    {
      return this.m_legacyEvidence.GetHashCode();
    }

    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
    public override EvidenceBase Clone()
    {
      return base.Clone();
    }
  }
}
