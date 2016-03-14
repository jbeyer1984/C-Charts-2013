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
        public static int numOfInstance = 0;

        private int divisorHorizontal;
        private int divisorVertical;
        private int posX;
        private int posY;
        private int gridWidth;
        private int gridHeight;

        //private DynamicSettingsBox dynamicSetting
        private bool dynamicSettingsMapperPainted = false;

        public DynamicSettingsForm()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.BackColor = Color.White;

            this.initPanelPaintAlignment();
        }

        private void initPanelPaintAlignment()
        {
            divisorHorizontal = 3;
            divisorVertical = 2;
            posX = 0;
            posY = 0;
            gridWidth = ClientRectangle.Width / divisorHorizontal;
            gridHeight = ClientRectangle.Height / divisorVertical;
        }

        private void DynamicSettings_Load(object sender, EventArgs e)
        {

        }

        private void addPanel(Panel panel)
        {
            this.Controls.Add(panel);
            panel.Show();

            this.calculateVerticalAlignmentForPanel();
            panel.Location = new Point(posX, posY);
            panel.Size = new Size(gridWidth, gridHeight);

            DynamicSettingsForm.numOfInstance++;
        }

        private void calculateVerticalAlignmentForPanel()
        {
            posX = gridWidth * (DynamicSettingsForm.numOfInstance % divisorHorizontal);

            if (ClientRectangle.Width == gridWidth * (DynamicSettingsForm.numOfInstance + 1))
            {
                posY = gridHeight + 1;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            if (!dynamicSettingsMapperPainted)
            {
                Panel panelChartStyle = new Panel();
                panelChartStyle.Text = "ChartStyle";
                Panel panelLegend = new Panel();
                panelLegend.Text = "Legend";
                this.addPanel(panelChartStyle);
                this.addPanel(panelLegend);

                ChartForm chartForm = (ChartForm)Application.OpenForms["ChartForm"];
                DynamicSettingsMapper dynamicSettingsMapper = new DynamicSettingsMapperChartStyle(panelChartStyle, chartForm);
                DynamicSettingsMapper dynamicSettingsMapper2 = new DynamicSettingsMapperLegend(panelLegend, chartForm);
                dynamicSettingsMapper.addDynamicSettingsBox(g);
                dynamicSettingsMapper2.addDynamicSettingsBox(g);

                dynamicSettingsMapperPainted = true;
            }

            g.Dispose();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
