////////////////////////////////////////////////////////////////////////////////
//
// Module: configuration.cs
//
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.IO;

////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

namespace Ev27 {

////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

public class Configuration
{
    ////////////////////////////////////////////////////////////////////////////
    // Member variables
    ////////////////////////////////////////////////////////////////////////////

    public string ConfigurationJson { get; set; }
    public string ConfigurationProjectFile { get; set; }

    public string BinWorkingDirectory { get; set; }
    public string ObjWorkingDirectory { get; set; }
    public string SourceWorkingDirectory { get; set; }

    ////////////////////////////////////////////////////////////////////////////
    // Private member variables
    ////////////////////////////////////////////////////////////////////////////

    private bool UseJsonProjectFileSystem { get; set; }
    private bool UseProjectFileSystem { get; set; }

    ////////////////////////////////////////////////////////////////////////////
    // Constructor
    ////////////////////////////////////////////////////////////////////////////

    public Configuration(string configurationJsonLocation=null)
    {
        SourceWorkingDirectory = Environment.CurrentDirectory;

        if (configurationJsonLocation != null)
        {
            ConfigurationJson = configurationJsonLocation;
        }

        Setup();
    }

    ////////////////////////////////////////////////////////////////////////////
    // Member methods
    ////////////////////////////////////////////////////////////////////////////

    ////////////////////////////////////////////////////////////////////////////
    // Static member variables
    ////////////////////////////////////////////////////////////////////////////

    private static Dictionary<string, bool> _validExtensions;
    private static Dictionary<string, bool> ValidExtensions 
    { 
        get
        {
            if (_validExtensions == null)
            {
                _validExtensions = new Dictionary<string, bool> {
                    { ".cs", true, },
                    { ".cpp", true },
                    { ".hpp", true },
                    { ".h", true }
                };
            }

            return _validExtensions;
        }

        private set;
    }
    
    ////////////////////////////////////////////////////////////////////////////
    // Helper methods
    ////////////////////////////////////////////////////////////////////////////

    private void CreateDefaultJsonProject()
    {
        List<string> files = new List<string>();
        foreach (var item in Directory.EnumerateFiles())
        {
            var itemSplit = item.Split('.');
            if (itemSplit.Length == 1) continue;

            var extension = itemSplit[1];
            if (ValidExtensions.ContainsKey(extension))
            {
                files.Add(item);
            }
        }

        if (files.Count == 0)
        {
            throw new Exception("No valid files.");
        }

        string configuration = "Debug";
        
    }

    private void ParseJsonProjectFile()
    {

    }

    private void ParseProjectFile()
    {
        
    }

    private void Setup()
    {
        UseJsonProjectFileSystem = false;

        if (ConfigurationJson != null || ConfigurationProjectFile)
        {
            if (ConfigurationJson) UseJsonProjectFileSystem = true;
            else
            {
                UseJsonProjectFileSystem = false;
            }
        }

        if (UseProjectFileSystem)
        {
            ParseProjectFile();
        }
        else if (!UseJsonProjectFileSystem)
        {
            CreateDefaultJsonProject();

            UseJsonProjectFileSystem = true;
        }

        if (UseJsonProjectFileSystem)
        {
            ParseJsonProjectFile();
        }
    }
}

////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

} // end of namespace(Ev27)

////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////