using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NugetPack2Folder
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main ()
        {

//             var xmlTrace = new XmlWriterTraceListener("C:\\test\\NugetPack2Folder.svclog");
//             xmlTrace.TraceOutputOptions = TraceOptions.ProcessId | TraceOptions.Callstack | TraceOptions.Timestamp;
//             Trace.Listeners.Add(xmlTrace);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainFrm());
        }
    }
}
