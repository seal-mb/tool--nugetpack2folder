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

    internal enum CfgTags
    {
        configuration,
        runtime,
        assemblyBinding, // "urn:schemas-microsoft-com:asm.v1"
        probing, // privatePath
    };

    internal class Helper
    {

        static private XNamespace _xmlns = "http://schemas.microsoft.com/developer/msbuild/2003";
        static private XNamespace _xmlns_bind = "urn:schemas-microsoft-com:asm.v1";


        static public String[] SplitProbing(String probingString)
        {
            if (String.IsNullOrWhiteSpace(probingString))
                return new string[] { };

            var result = probingString.Split(new char[] { ',', ';' }).Select(s => s.Trim()).Where(s => String.IsNullOrWhiteSpace(s) == false);

            return result.ToArray();
        }

        static public String SearchNugetPack => Properties.Settings.Default.NugetKenner;

        static public XNamespace XMLNS => _xmlns;
        static public XName GetXName(PTags enTag)
        {
            return _xmlns + enTag.ToString();
        }

        static public XName GetXName(string name)
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

        static public bool AddProbing(XDocument appCfg, String[] probingVal)
        {
            if (null != appCfg && probingVal.Any())
            {
                var configElement = appCfg.Element(CfgTags.configuration.ToString());
                if (null == configElement)
                    return false;

                var runtimeElement = configElement.Element(CfgTags.runtime.ToString());

                if (null == runtimeElement)
                {
                    runtimeElement = new XElement(CfgTags.runtime.ToString());
                    configElement.Add(runtimeElement);
                }

                var assemblyBindingElement = runtimeElement.Element(_xmlns_bind + CfgTags.assemblyBinding.ToString());

                if (null == assemblyBindingElement)
                {
                    assemblyBindingElement = new XElement(_xmlns_bind + CfgTags.assemblyBinding.ToString());
                    runtimeElement.Add(assemblyBindingElement);
                }

                var probingElement = assemblyBindingElement.Element(_xmlns_bind + CfgTags.probing.ToString());

                if (null == probingElement)
                {
                    probingElement = new XElement(_xmlns_bind + CfgTags.probing.ToString(), new XAttribute("privatePath", ""));
                    assemblyBindingElement.Add(probingElement);
                }

                var probingAttribute = probingElement.Attribute("privatePath");
                if(null == probingAttribute)
                {
                    probingAttribute = new XAttribute("privatePath", "");
                }

                var lstVals = new List<String>();

                var probingValues = probingAttribute.Value.Split(';');

                var res = probingVal.Where( s => !probingValues.Any( t => String.Equals(t,s,StringComparison.InvariantCultureIgnoreCase)) );

                

                lstVals.AddRange(probingValues);
                lstVals.AddRange(res);

                probingAttribute.Value = String.Join(";", lstVals.Where(s => !String.IsNullOrWhiteSpace(s)));

            }

            return true;
        }

    }
}
