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
            gridWidth = (ClientRectangle.Width - 40) / divisorHorizontal;
            gridHeight = ClientRectangle.Height / divisorVertical;
        }

        private void DynamicSettings_Load(object sender, EventArgs e)
        {

        }

        private void addPanel(Panel panel)
        {
            this.Controls.Add(panel);
            panel.Show();

            this.calculateAlignmentForPanel();
            panel.Location = new Point(posX, posY);
            panel.Size = new Size(gridWidth, gridHeight);
            Console.WriteLine("dynamic Form gridWidth: {0}", gridWidth);
        }

        private void calculateAlignmentForPanel()
        {
            if ((posX) == (gridWidth * (divisorHorizontal-1)))
            {
                posY += gridHeight;
            }

            posX = gridWidth * (DynamicSettingsForm.numOfInstance % divisorHorizontal);

            DynamicSettingsForm.numOfInstance++;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            if (!dynamicSettingsMapperPainted)
            {
                Form chartForm = (Form) Application.OpenForms["ChartForm"];
                ChartPanel panelToUpdate = (ChartPanel) chartForm.Controls["ChartPanel"];

                Panel panelChartStyle = new Panel();
                panelChartStyle.Text = "ChartStyle";
                Panel panelLegend = new Panel();
                panelLegend.Text = "Legend";

                this.addPanel(panelChartStyle);
                this.addPanel(panelLegend);

                MapperViewWithLabel mapperViewWitLabel = new MapperViewWithLabel();
                DynamicMapperBinds dynamicMapperBinds = new DynamicMapperBinds(
                    ((Panel)panelToUpdate),
                    panelToUpdate.ChartStyle.dd,
                    mapperViewWitLabel
                );
                dynamicMapperBinds.bind(panelChartStyle);

                mapperViewWitLabel = new MapperViewWithLabel();
                dynamicMapperBinds = new DynamicMapperBinds(
                    ((Panel)panelToUpdate),
                    panelToUpdate.Legend.dd,
                    mapperViewWitLabel
                );
                dynamicMapperBinds.bind(panelLegend);

                // init series
                int length = panelToUpdate.dynamicDataSeriesList.Count;
                foreach (string seriesName in panelToUpdate.dynamicDataSeriesList.Keys)
                {
                    Panel panelSeries = new Panel();
                    panelSeries.Text = seriesName;
                    this.addPanel(panelSeries);

                    mapperViewWitLabel = new MapperViewWithLabel();
                    dynamicMapperBinds = new DynamicMapperBinds(
                        ((Panel)panelToUpdate),
                        panelToUpdate.dynamicDataSeriesList[seriesName],
                        mapperViewWitLabel
                    );
                    dynamicMapperBinds.bind(panelSeries);
                }

                dynamicSettingsMapperPainted = true;
            }

            g.Dispose();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
