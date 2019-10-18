using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NugetPack2Folder
{
    public class PrjContentObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Boolean _addToOutput = true;
        private XElement _referenzObject = null;
        private String _basePath = null;
        private String _probingPath = null;

        public PrjContentObject(XElement referenzObject, String basePath, string probingPath)
        {
            _referenzObject = referenzObject;
            _basePath = basePath;
            _probingPath = probingPath;
        }

        private void DoPropertyChanged(String name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        [Bindable(true)]
        public Boolean AddToOutput
        {
            get => _addToOutput;
            set
            {
                _addToOutput = value;
                DoPropertyChanged("AddToOutput");
            }
        }

        [Bindable(true)]
        public String ProbingPath
        {
            get => _probingPath;
            set
            {
                _probingPath = value?.Trim();
                DoPropertyChanged("ProbingPath");
            }
        }

        [Bindable(false)]
        public String ReferenzPath
        {
            get
            {
                return Path.GetDirectoryName(_referenzObject.Element( Helper.GetXName(PTags.HintPath)).Value);
            }
        }

        [Bindable(true)]
        public String BasePath
        {
            get => _basePath;
            set
            {
                _basePath = value;
                DoPropertyChanged("BasePath");
            }
        }

        [Bindable(false)]
        public XElement ReferenzObject => _referenzObject;

        [Bindable(false)]
        public String NugetPathRef
        {
            get 
            {
                var x = new DirectoryInfo( Path.Combine(_basePath,ReferenzPath));
                return new Uri(x.FullName).ToString();
            }
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

        public IEnumerable<XElement> Containers
        {
            get
            {
                var hintPath = _referenzObject.Element( Helper.GetXName(PTags.HintPath)).Value;
                var fileName = Path.GetFileName(hintPath);
                var filePath = Path.GetDirectoryName(hintPath);

                var lstInclude = new List<Tuple<String, String>>();


                lstInclude.Add(new Tuple<string, string>(hintPath, filePath));
                var moduleDir = new DirectoryInfo(Path.Combine(_basePath, filePath));

                foreach (var fi in moduleDir.GetFiles("*", SearchOption.AllDirectories))
                {
                    if (String.Equals(fi.Name, fileName, StringComparison.InvariantCultureIgnoreCase))
                        continue;

                    var relPath = GetRelativePath(_basePath, fi.DirectoryName);

                    var linkBase = Path.Combine(relPath, fi.Name);

                    lstInclude.Add(new Tuple<string, string>(linkBase, relPath));
                }

                var resultGrp = lstInclude.OrderBy(s => s.Item2.Length).GroupBy(s => s.Item2);
                var offest = filePath.Length;

                foreach (var item1 in resultGrp)
                {
                    var sub = item1.Key.Substring(offest, item1.Key.Length - offest);

                    foreach (var item2 in item1)
                    {
                        var pa = Path.Combine(this.ProbingPath + sub, Path.GetFileName(item2.Item1));

                        yield return new XElement(MainFrm._xmlns + "Content",
                                        new XAttribute("Include", item2.Item1),
                                        new XElement(MainFrm._xmlns + "Link") { Value = pa },
                                        new XElement(MainFrm._xmlns + "CopyToOutputDirectory") { Value = "Always" });

                    }
                }

            }
        }

    }
}
