using System;
using System.Collections.Generic;
using System.IO;
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

        static public String SearchNugetPack => Properties.Settings.Default.NugetKenner;

        static public XNamespace XMLNS => _xmlns;
        static public XName GetXName(PTags enTag)
        {
            return _xmlns + enTag.ToString();
        }

        static public XName GetXName( string name )
        {
            return _xmlns + name;
        }

        public static string GetRelativePath(string relBase, string path, bool throwOnDifferentRoot = true)
        {
            // Use case-insensitive comparing of path names.
            // NOTE: This may be different on other systems.
            StringComparison sc = StringComparison.InvariantCultureIgnoreCase;

            // Are both paths rooted?
            if (!Path.IsPathRooted(path))
                throw new ArgumentException("path argument is not a rooted path.");
            if (!Path.IsPathRooted(relBase))
                throw new ArgumentException("relBase argument is not a rooted path.");

            // Do both paths share the same root?
            string pathRoot = Path.GetPathRoot(path);
            string baseRoot = Path.GetPathRoot(relBase);
            if (!string.Equals(pathRoot, baseRoot, sc))
            {
                if (throwOnDifferentRoot)
                {
                    throw new InvalidOperationException("Both paths do not share the same root.");
                }
                else
                {
                    return path;
                }
            }

            // Cut off the path roots
            path = path.Substring(pathRoot.Length);
            relBase = relBase.Substring(baseRoot.Length);

            // Cut off the common path parts
            string[] pathParts = path.Split(Path.DirectorySeparatorChar);
            string[] baseParts = relBase.Split(Path.DirectorySeparatorChar);
            int commonCount;
            for (
                commonCount = 0;
                commonCount < pathParts.Length &&
                commonCount < baseParts.Length &&
                string.Equals(pathParts[commonCount], baseParts[commonCount], sc);
                commonCount++)
            {
            }

            // Add .. for the way up from relBase
            string newPath = "";
            for (int i = commonCount; i < baseParts.Length; i++)
            {
                newPath += ".." + Path.DirectorySeparatorChar;
            }

            // Append the remaining part of the path
            for (int i = commonCount; i < pathParts.Length; i++)
            {
                newPath = Path.Combine(newPath, pathParts[i]);
            }

            return newPath;
        }

    }
}
