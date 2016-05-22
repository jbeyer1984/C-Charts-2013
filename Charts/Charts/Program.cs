using Charts.Chart.Debug;
using Charts.Chart.StaticCallsFolder;
using Charts.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Charts
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //DebugSettings.deleteLogFile();
            DebugSettings.debug = true;
            DebugSettings.log("---------------------------------------");
            ChartForm chartForm = new ChartForm();
            Inst.getStaticCall().initIdentifierOneTime(chartForm); // @todo extract in builder
            chartForm.initChartForm();
            Application.Run(chartForm);
        }
    }
}
