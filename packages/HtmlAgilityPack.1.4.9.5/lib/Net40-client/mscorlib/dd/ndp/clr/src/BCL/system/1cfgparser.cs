// Decompiled with JetBrains decompiler
// Type: System.ConfigTreeParser
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System
{
  internal class ConfigTreeParser : BaseConfigHandler
  {
    private ConfigNode rootNode;
    private ConfigNode currentNode;
    private string fileName;
    private int attributeEntry;
    private string key;
    private string[] treeRootPath;
    private bool parsing;
    private int depth;
    private int pathDepth;
    private int searchDepth;
    private bool bNoSearchPath;
    private string lastProcessed;
    private bool lastProcessedEndElement;

    internal ConfigNode Parse(string fileName, string configPath)
    {
      return this.Parse(fileName, configPath, false);
    }

    [SecuritySafeCritical]
    internal ConfigNode Parse(string fileName, string configPath, bool skipSecurityStuff)
    {
      if (fileName == null)
        throw new ArgumentNullException("fileName");
      this.fileName = fileName;
      if ((int) configPath[0] == 47)
      {
        this.treeRootPath = configPath.Substring(1).Split('/');
        this.pathDepth = this.treeRootPath.Length - 1;
        this.bNoSearchPath = false;
      }
      else
      {
        this.treeRootPath = new string[1];
        this.treeRootPath[0] = configPath;
        this.bNoSearchPath = true;
      }
      if (!skipSecurityStuff)
        new FileIOPermission(FileIOPermissionAccess.Read, Path.GetFullPathInternal(fileName)).Demand();
      new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Assert();
      try
      {
        this.RunParser(fileName);
      }
      catch (FileNotFoundException ex)
      {
        throw;
      }
      catch (DirectoryNotFoundException ex)
      {
        throw;
      }
      catch (UnauthorizedAccessException ex)
      {
        throw;
      }
      catch (FileLoadException ex)
      {
        throw;
      }
      catch (Exception ex)
      {
        throw new ApplicationException(this.GetInvalidSyntaxMessage(), ex);
      }
      return this.rootNode;
    }

    public override void NotifyEvent(ConfigEvents nEvent)
    {
    }

    public override void BeginChildren(int size, ConfigNodeSubType subType, ConfigNodeType nType, int terminal, [MarshalAs(UnmanagedType.LPWStr)] string text, int textLength, int prefixLength)
    {
      if (this.parsing || this.bNoSearchPath || (this.depth != this.searchDepth + 1 || string.Compare(text, this.treeRootPath[this.searchDepth], StringComparison.Ordinal) != 0))
        return;
      this.searchDepth = this.searchDepth + 1;
    }

    public override void EndChildren(int fEmpty, int size, ConfigNodeSubType subType, ConfigNodeType nType, int terminal, [MarshalAs(UnmanagedType.LPWStr)] string text, int textLength, int prefixLength)
    {
      this.lastProcessed = text;
      this.lastProcessedEndElement = true;
      if (this.parsing)
      {
        if (this.currentNode == this.rootNode)
          this.parsing = false;
        this.currentNode = this.currentNode.Parent;
      }
      else
      {
        if (nType != ConfigNodeType.Element)
          return;
        if (this.depth == this.searchDepth && string.Compare(text, this.treeRootPath[this.searchDepth - 1], StringComparison.Ordinal) == 0)
        {
          this.searchDepth = this.searchDepth - 1;
          this.depth = this.depth - 1;
        }
        else
          this.depth = this.depth - 1;
      }
    }

    public override void Error(int size, ConfigNodeSubType subType, ConfigNodeType nType, int terminal, [MarshalAs(UnmanagedType.LPWStr)] string text, int textLength, int prefixLength)
    {
    }

    public override void CreateNode(int size, ConfigNodeSubType subType, ConfigNodeType nType, int terminal, [MarshalAs(UnmanagedType.LPWStr)] string text, int textLength, int prefixLength)
    {
      if (nType == ConfigNodeType.Element)
      {
        this.lastProcessed = text;
        this.lastProcessedEndElement = false;
        if (this.parsing || this.bNoSearchPath && string.Compare(text, this.treeRootPath[0], StringComparison.OrdinalIgnoreCase) == 0 || this.depth == this.searchDepth && this.searchDepth == this.pathDepth && string.Compare(text, this.treeRootPath[this.pathDepth], StringComparison.OrdinalIgnoreCase) == 0)
        {
          this.parsing = true;
          ConfigNode parent = this.currentNode;
          this.currentNode = new ConfigNode(text, parent);
          if (this.rootNode == null)
            this.rootNode = this.currentNode;
          else
            parent.AddChild(this.currentNode);
        }
        else
          this.depth = this.depth + 1;
      }
      else
      {
        if (nType != ConfigNodeType.PCData || this.currentNode == null)
          return;
        this.currentNode.Value = text;
      }
    }

    public override void CreateAttribute(int size, ConfigNodeSubType subType, ConfigNodeType nType, int terminal, [MarshalAs(UnmanagedType.LPWStr)] string text, int textLength, int prefixLength)
    {
      if (!this.parsing)
        return;
      if (nType == ConfigNodeType.Attribute)
      {
        this.attributeEntry = this.currentNode.AddAttribute(text, "");
        this.key = text;
      }
      else
      {
        if (nType != ConfigNodeType.PCData)
          throw new ApplicationException(this.GetInvalidSyntaxMessage());
        this.currentNode.ReplaceAttribute(this.attributeEntry, this.key, text);
      }
    }

    private string GetInvalidSyntaxMessage()
    {
      string str = (string) null;
      if (this.lastProcessed != null)
        str = (this.lastProcessedEndElement ? "</" : "<") + this.lastProcessed + ">";
      return Environment.GetResourceString("XML_Syntax_InvalidSyntaxInFile", (object) this.fileName, (object) str);
    }
  }
}
