using Charts.Chart.ConnectorFolder;
using Charts.Chart.Identifier;
using Charts.Chart.StaticCallsFolder;
using Charts.Factories;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Charts
{
    public partial class DataTablePanel : System.Windows.Forms.Panel, IIdentifier
    {
        private ChartPanel chartPanel;

        private string identifier;

        public DataTablePanel() :
            base()
        {
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            ResizeRedraw = true;
        }

        public void connectChartPanelType(Type type)
        {
            chartPanel = getConnector().with(this).getByType(type) as ChartPanel;
        }

        public void init()
        {
            this.Size = new Size(400, 200);
            this.Left = 0;
            this.Top = 0;
            chartPanel.DataGridView.Top = 0;
            chartPanel.DataGridView.Left = 0;
            this.Controls.Add(chartPanel.DataGridView);
        }

        protected Connector getConnector()
        {
            return Inst.getInstance().getConnector();
        }

        public string Identifier
        {
            get { return identifier; }
            set { identifier = value; }
        }

    }
}
