using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NugetPack2Folder
{
    internal enum PTags
    {
        Project,
        Import,
        PropertyGroup,
        Configuration,
        Platform,
        ProjectGuid,
        OutputType,
        RootNamespace,
        AssemblyName,
        TargetFrameworkVersion,
        FileAlignment,
        AutoGenerateBindingRedirects,
        Deterministic,
        PlatformTarget,
        DebugSymbols,
        DebugType,
        Optimize,
        OutputPath,
        DefineConstants,
        ErrorReport,
        WarningLevel,
        ItemGroup,
        Reference,
        HintPath,
        Compile,
        None,
        Content,
        Link,
        CopyToOutputDirectory,
        ImportGroup,
        Private,
    }

    internal class Helper
    {

        static private XNamespace _xmlns = "http://schemas.microsoft.com/developer/msbuild/2003";


        static public String[] SplitProbing( String probingString )
        {
            if (String.IsNullOrWhiteSpace(probingString))
                return new string[] { };

            var result = probingString.Split( new char[] { ',', ';' } ).Select( s => s.Trim() ).Where( s => String.IsNullOrWhiteSpace(s) == false );

            return result.ToArray();
        }

        static public XNamespace XMLNS => _xmlns;
        static public XName GetXName(PTags enTag)
        {
            return _xmlns + enTag.ToString();
        }

        static public XName GetXName( string name )
        {
            return _xmlns + name;
        }

    }
}
