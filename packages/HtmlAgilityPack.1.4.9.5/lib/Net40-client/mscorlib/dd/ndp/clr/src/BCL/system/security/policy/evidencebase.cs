// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.EvidenceBase
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;

namespace System.Security.Policy
{
  /// <summary>提供一个基类，要用作证据的所有对象都必须派生自该类。</summary>
  [ComVisible(true)]
  [Serializable]
  [PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
  public abstract class EvidenceBase
  {
    /// <summary>初始化 <see cref="T:System.Security.Policy.EvidenceBase" /> 类的新实例。</summary>
    /// <exception cref="T:System.InvalidOperationException">要用作证据的对象不可序列化。</exception>
    protected EvidenceBase()
    {
      if (!this.GetType().IsSerializable)
        throw new InvalidOperationException(Environment.GetResourceString("Policy_EvidenceMustBeSerializable"));
    }

    /// <summary>创建作为当前实例的完整副本的新对象。</summary>
    /// <returns>此证据对象的重复副本。</returns>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Assert, SerializationFormatter = true)]
    [PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
    public virtual EvidenceBase Clone()
    {
      using (MemoryStream memoryStream1 = new MemoryStream())
      {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        MemoryStream memoryStream2 = memoryStream1;
        binaryFormatter.Serialize((Stream) memoryStream2, (object) this);
        memoryStream1.Position = 0L;
        MemoryStream memoryStream3 = memoryStream1;
        return binaryFormatter.Deserialize((Stream) memoryStream3) as EvidenceBase;
      }
    }
  }
}
