using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Charts
{
    public partial class DynamicSettingsForm : Form
    {
        private DynamicSettingsBox dynamicSettingsBox;

        public DynamicSettingsForm()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.BackColor = Color.White;

            this.addPanel();
        }

        private void DynamicSettings_Load(object sender, EventArgs e)
        {

        }

        private void addPanel()
        {
            Panel panel = new Panel();
            panel.Name = "LegendPanel";
            this.Controls.Add(panel);
            panel.Show();

            panel.Location = new Point(0, 0);
            panel.Size = new Size(200, 200);



        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            if (null == dynamicSettingsBox)
            {
                Panel panel = (Panel)this.Controls["LegendPanel"];
                ChartForm chartForm = (ChartForm)Application.OpenForms["ChartForm"];
                dynamicSettingsBox = new DynamicSettingsBoxLegend(panel, chartForm);
                dynamicSettingsBox.addDynamicSettingsBox(g);
            }

            g.Dispose();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        //private void drawingPanelPaint(object sender, PaintEventArgs e)
        //{
        //    drawingPanel.Left = offset;
        //    drawingPanel.Top = offset;
        //    //drawingPanel.Width = ClientRectangle.Width - 2 * offset;
        //    //drawingPanel.Height = ClientRectangle.Height - 2 * offset;
        //    drawingPanel.Width = 200;
        //    drawingPanel.Height = 200;
        //    //drawingPanel.Visible = true;

        //    Graphics g = e.Graphics;

        //    if (null == dynamicSettingsBox)
        //    {
        //        dynamicSettingsBox = new DynamicSettingsBox(drawingPanel, this);
        //        dynamicSettingsBox.addDynamicSettingsBox(g);
        //    }
        //    //ChartStyle chartStyle = new ChartStyle(this);
        //    //chartStyle.AddChartStyle(g);

        //    //AddData();
        //    //SetPlotArea(g);
        //    //cs.AddChartStyle(g);
        //    //dc.AddLines(g, cs);
        //    g.Dispose();
        //}
    }
}
