// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.LogSwitch
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;

namespace System.Diagnostics
{
  [Serializable]
  internal class LogSwitch
  {
    internal string strName;
    internal string strDescription;
    private LogSwitch ParentSwitch;
    internal volatile LoggingLevels iLevel;
    internal volatile LoggingLevels iOldLevel;

    public virtual string Name
    {
      get
      {
        return this.strName;
      }
    }

    public virtual string Description
    {
      get
      {
        return this.strDescription;
      }
    }

    public virtual LogSwitch Parent
    {
      get
      {
        return this.ParentSwitch;
      }
    }

    public virtual LoggingLevels MinimumLevel
    {
      get
      {
        return this.iLevel;
      }
      [SecuritySafeCritical] set
      {
        this.iLevel = value;
        this.iOldLevel = value;
        string strParentName = this.ParentSwitch != null ? this.ParentSwitch.Name : "";
        if (Debugger.IsAttached)
          Log.ModifyLogSwitch((int) this.iLevel, this.strName, strParentName);
        Log.InvokeLogSwitchLevelHandlers(this, this.iLevel);
      }
    }

    private LogSwitch()
    {
    }

    [SecuritySafeCritical]
    public LogSwitch(string name, string description, LogSwitch parent)
    {
      if (name != null && name.Length == 0)
        throw new ArgumentOutOfRangeException("Name", Environment.GetResourceString("Argument_StringZeroLength"));
      if (name == null || parent == null)
        throw new ArgumentNullException(name == null ? "name" : "parent");
      this.strName = name;
      this.strDescription = description;
      this.iLevel = LoggingLevels.ErrorLevel;
      this.iOldLevel = this.iLevel;
      this.ParentSwitch = parent;
      Log.m_Hashtable.Add((object) this.strName, (object) this);
      Log.AddLogSwitch(this);
    }

    [SecuritySafeCritical]
    internal LogSwitch(string name, string description)
    {
      this.strName = name;
      this.strDescription = description;
      this.iLevel = LoggingLevels.ErrorLevel;
      this.iOldLevel = this.iLevel;
      this.ParentSwitch = (LogSwitch) null;
      Log.m_Hashtable.Add((object) this.strName, (object) this);
      Log.AddLogSwitch(this);
    }

    public virtual bool CheckLevel(LoggingLevels level)
    {
      if (this.iLevel <= level)
        return true;
      if (this.ParentSwitch == null)
        return false;
      return this.ParentSwitch.CheckLevel(level);
    }

    public static LogSwitch GetSwitch(string name)
    {
      return (LogSwitch) Log.m_Hashtable[(object) name];
    }
  }
}
