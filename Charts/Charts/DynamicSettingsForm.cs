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
            gridWidth = (ClientRectangle.Width - 50) / divisorHorizontal;
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
        }

        private void calculateVerticalAlignmentForPanel()
        {
            if ((posX) == (gridWidth * (divisorHorizontal-1)))
            {
                posY = gridHeight + 1;
            }

            posX = gridWidth * (DynamicSettingsForm.numOfInstance % divisorHorizontal);

            DynamicSettingsForm.numOfInstance++;
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
                Panel panelSeries = new Panel();
                panelSeries.Text = "Series1";
                //Panel panelStyles = new Panel();
                //panelStyles.Text = "LineStyles";
                this.addPanel(panelChartStyle);
                this.addPanel(panelLegend);
                this.addPanel(panelSeries);
                //this.addPanel(panelStyles);

                Form chartForm = (Form) Application.OpenForms["ChartForm"];
                ChartPanel panelToUpdate = (ChartPanel) chartForm.Controls["ChartPanel"];

                DynamicMapper dynamicSettingsMapper = new DynamicMapperChartStyle(panelChartStyle, panelToUpdate);
                DynamicMapper dynamicSettingsMapper2 = new DynamicMapperLegend(panelLegend, panelToUpdate);
                DynamicMapper dynamicSettingsMapper3 = new DynamicMapperSeries(
                    panelSeries,
                    panelToUpdate,
                    "ds1"
                );
                dynamicSettingsMapper.addDynamicSettingsBox(g);
                dynamicSettingsMapper2.addDynamicSettingsBox(g);
                //dynamicSettingsMapper3.addDynamicSettingsBox(g);

                DynamicDataSeries dyns = new DynamicDataSeries();
                MapperViewTest mpt = new MapperViewTest();
                DynamicMapperTest dmt = new DynamicMapperTest(
                    ((Panel) panelToUpdate),
                    panelToUpdate.dynamicDataList["ds1"],
                    mpt
                );
                dmt.bind(panelSeries);
                

                dynamicSettingsMapperPainted = true;
            }

            g.Dispose();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
