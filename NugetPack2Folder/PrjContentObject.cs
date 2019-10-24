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
    public enum CopyOption
    {
        Always,
        PreserveNewest
    };

    public class PrjContentObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Boolean _addToOutput = true;
        private XElement _referenzObject = null;
        private String _basePath = null;
        private String _probingPath = null;
        private CopyOption _cpOption = CopyOption.PreserveNewest;

        public PrjContentObject ( XElement referenzObject, String basePath, string probingPath )
        {
            _referenzObject = referenzObject;
            _basePath = basePath;

            this.ProbingPath = probingPath;
        }

        private void DoPropertyChanged ( String name )
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
                var pvalnew = value?.Trim();
                var _old = _probingPath;

                if ( String.IsNullOrWhiteSpace(pvalnew) )
                    _probingPath = String.Empty;
                else
                {
                    if ( pvalnew.StartsWith(@".\") )
                        _probingPath = pvalnew.Substring(2);
                    else
                        _probingPath = pvalnew;
                }
                if ( _old != _probingPath )
                {
                    DoPropertyChanged("ProbingPath");
                }
            }
        }

        [Bindable(false)]
        public String ReferenzPath
        {
            get
            {
                return Path.GetDirectoryName(_referenzObject.Element(Helper.GetXName(PTags.HintPath)).Value);
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
                var x = new DirectoryInfo(Path.Combine(_basePath, ReferenzPath));
                return new Uri(x.FullName).ToString();
            }
        }

        [Bindable(true)]
        public CopyOption CpOption
        {
            get => _cpOption;
            set
            {
                _cpOption = value;
                DoPropertyChanged("CpOption");
            }
        }

        [Bindable(false)]
        public XElement OldElement { get; set; } = null;

        public IEnumerable<XElement> Containers
        {
            get
            {
                var hintPath = _referenzObject.Element(Helper.GetXName(PTags.HintPath)).Value;
                var fileName = Path.GetFileName(hintPath);
                var filePath = Path.GetDirectoryName(hintPath);

                var lstInclude = new List<Tuple<String, String>>();

                lstInclude.Add(new Tuple<string, string>(hintPath, filePath));
                var moduleDir = new DirectoryInfo(Path.Combine(_basePath, filePath));

                foreach ( var fi in moduleDir.GetFiles("*", SearchOption.AllDirectories) )
                {
                    if ( String.Equals(fi.Name, fileName, StringComparison.InvariantCultureIgnoreCase) )
                        continue;

                    var relPath = Helper.GetRelativePath(_basePath, fi.DirectoryName);

                    var linkBase = Path.Combine(relPath, fi.Name);

                    lstInclude.Add(new Tuple<string, string>(linkBase, relPath));
                }

                var resultGrp = lstInclude.OrderBy(s => s.Item2.Length).GroupBy(s => s.Item2);
                var offest = filePath.Length;

                foreach ( var item1 in resultGrp )
                {
                    var sub = item1.Key.Substring(offest, item1.Key.Length - offest);

                    foreach ( var item2 in item1 )
                    {
                        var pa = Path.Combine(this.ProbingPath + sub, Path.GetFileName(item2.Item1));

                        yield return new XElement(Helper.GetXName(PTags.Content),
                                        new XAttribute(Helper.Attr_Include, item2.Item1),
                                        new XElement(Helper.GetXName(PTags.Link)) { Value = pa },
                                        new XElement(Helper.GetXName(PTags.CopyToOutputDirectory)) { Value = this.CpOption.ToString() });

                    }
                }
            }
        }

    }
}
