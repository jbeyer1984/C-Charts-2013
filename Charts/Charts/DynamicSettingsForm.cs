using System;
using System.Drawing;
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
            // @todo this value are set hard in class, not a good approach
            divisorHorizontal = 3;
            divisorVertical = 2;
            posX = 0;
            posY = 0;
            // - 40 because place is wrong calculated, seems like whole Form with border
            gridWidth = (ClientRectangle.Width - 40) / divisorHorizontal;
            gridHeight = ClientRectangle.Height / divisorVertical;
        }

        private void DynamicSettings_Load(object sender, EventArgs e)
        {
        }

        public void addPanel(Panel panel)
        {
            this.Controls.Add(panel);
            panel.Show();

            this.calculateAlignmentForPanel();
            panel.Location = new Point(posX, posY);
            panel.Size = new Size(gridWidth, gridHeight);
            Console.WriteLine("dynamic Form gridWidth: {0}", gridWidth);
        }

        public void calculateAlignmentForPanel()
        {
            // divisorHorizontal -1 because, last Panel in row is related to posX
            if ((posX) == (gridWidth * (divisorHorizontal - 1))) {
                posY += gridHeight;
            }

            // best bet is 0 % 3 is 0 and 3 % 3 is 0 ;)
            posX = gridWidth * (DynamicSettingsForm.numOfInstance % divisorHorizontal);

            DynamicSettingsForm.numOfInstance++;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            if (!dynamicSettingsMapperPainted) {
                Form chartForm = (Form)Application.OpenForms["ChartForm"];
                ChartPanel panelToUpdate = (ChartPanel)chartForm.Controls["ChartPanel"];

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
                foreach (string seriesName in panelToUpdate.dynamicDataSeriesList.Keys) {
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

        public int GridWidth
        {
            get { return gridWidth; }
            set { gridWidth = value; }
        }

        public int GridHeight
        {
            get { return gridHeight; }
            set { gridHeight = value; }
        }

        public int DivisorHorizontal
        {
            get { return divisorHorizontal; }
            set { divisorHorizontal = value; }
        }

        public int DivisorVertical
        {
            get { return divisorVertical; }
            set { divisorVertical = value; }
        }

        public int PosX
        {
            get { return posX; }
            set { posX = value; }
        }

        public int PosY
        {
            get { return posY; }
            set { posY = value; }
        }
    }
}