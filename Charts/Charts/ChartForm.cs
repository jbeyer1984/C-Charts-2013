using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

/// consists ChartPanel, the panel that consist chart display
namespace Charts
{
    public partial class ChartForm : Form
    {
        ChartPanel chartPanel;
        private int offsetPanelX = 10;
        private int offsetPanelY = 30;

        public ChartForm()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.BackColor = Color.White;
            this.Location = new Point(125, 124);
            this.Size = new Size(664, 437);

            this.initChartPanel();
        }

        private void initChartPanel()
        {
            chartPanel = new ChartPanel(this);
            chartPanel.Name = "ChartPanel";

            this.Controls.Add(chartPanel);

            this.initDynamicSettingsForm();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            chartPanel.updateSize();
        }

        protected void initDynamicSettingsForm()
        {
            Form form = new DynamicSettingsForm();
            form.Show();
            form.Location = new Point(788, 124);
            form.Size = new Size(547, 333);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.initDynamicSettingsForm();
        }
    }
}
