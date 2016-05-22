using Charts.Chart.CacheFolder;
using Charts.Chart.ConnectorFolder;
using Charts.Chart.Identifier;
using Charts.Factories;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Charts
{
    public partial class DynamicSettingsForm : Form, IIdentifier
    {
        public int numOfAddedInstances = 0;

        private int divisorHorizontal;
        private int divisorVertical;
        private int posX;
        private int posY;
        private int gridWidth;
        private int gridHeight;
        private int spaceWidth = 40;

        private ChartPanel panelToUpdate;

        private DynamicPanel currentDynamicPanel;
        private Boolean isPanelSwitchable;

        private bool dynamicSettingsMapperPainted = false;

        private String identifier;
        //private static int numOfInstances = 0;

        public DynamicSettingsForm()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.BackColor = Color.White;

            this.initPanelPaintAlignment(); //@todo extract maybe to public
            //this.countInstanceUp(); // @todo extract to StateRegistry
        }

        public void initDyanmicSettingsForm()
        {
            //ChartForm chartForm = chartForm;
            //panelToUpdate = chartForm.Controls["ChartPanel"] as ChartPanel;
            ChartForm chartForm = getConnector().with(this).getByType(typeof(ChartForm)) as ChartForm;
            panelToUpdate = chartForm.CurrentClickedPanel; //@todo another approach

            DynamicPanel fetchedDynamicPanel = getDynamicPanelInCache(panelToUpdate);

            if (!fetchedDynamicPanel.IsAlreadyCreated) {
                fetchedDynamicPanel.IsAlreadyCreated = true;
                currentDynamicPanel = fetchedDynamicPanel;
            } else {
                switchPanel(currentDynamicPanel, fetchedDynamicPanel);
            }
        }

        /// <summary>
        /// this panel is created new, or get fetched out of cache
        /// </summary>
        /// <param name="panelToUpdate"></param>
        /// <returns>DynamicPanel</returns>
        protected DynamicPanel getDynamicPanelInCache(ChartPanel panelToUpdate)
        {
            return getCache()
               .with(panelToUpdate)
               .canBeNew()
               .getByType(typeof(DynamicPanel)) as DynamicPanel
           ;
        }

        protected Connector getConnector()
        {
            return Inst.getInstance().getConnector();
        }

        protected Cache getCache()
        {
            return Inst.getInstance().getCache();
        }

        public void switchPanel(DynamicPanel currentDynamicPanel, DynamicPanel fetchedDynamicPanel)
        {
            isPanelSwitchable = false; // @todo extract dependency

            currentDynamicPanel.Hide();
            currentDynamicPanel = fetchedDynamicPanel;
            currentDynamicPanel.Show();

            isPanelSwitchable = true;
        }

        private void initPanelPaintAlignment()
        {
            // @todo this value are set hard in class, not a good approach
            divisorHorizontal = 3;
            divisorVertical = 1;
            posX = 0;
            posY = 0;
            // - 40 because place is wrong calculated, seems like whole Form with border
            gridWidth = (ClientRectangle.Width - spaceWidth) / divisorHorizontal;
            gridHeight = ClientRectangle.Height / divisorVertical;
        }

        private void DynamicSettings_Load(object sender, EventArgs e)
        {
        }

        public void addPanel(Panel panel)
        {
            this.currentDynamicPanel.Controls.Add(panel);
            this.currentDynamicPanel.Size = this.Size;
            panel.Show();

            this.calculateAlignmentForPanel();
            panel.Location = new Point(posX, posY);
            panel.Size = new Size(gridWidth, gridHeight);
            //Console.WriteLine("dynamic Form gridWidth: {0}", gridWidth);
        }

        public void allowRepaint()
        {
            this.initPanelPaintAlignment();
            this.dynamicSettingsMapperPainted = false;
        }

        public void clearPanels()
        {
            this.Controls.Clear();
        }

        public void calculateAlignmentForPanel()
        {
            // divisorHorizontal -1 because, last Panel in row is related to posX
            if ((posX) == (gridWidth * (divisorHorizontal - 1))) {
                posY += gridHeight;
            }

            // best bet is 0 % 3 is 0 and 3 % 3 is 0 ;)
            posX = gridWidth * (numOfAddedInstances % divisorHorizontal);

            numOfAddedInstances++;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            this.SuspendLayout();
            if (!dynamicSettingsMapperPainted) { // @todo remove isPanelSwitchable
                this.addInitializedPanels();
                this.Controls.Add(currentDynamicPanel); //@todo depency not visible
            }
            this.ResumeLayout();

            g.Dispose();
        }

        protected void addInitializedPanels()
        {
            if (isPanelSwitchable) { // @todo extract dependency with state
                //dynamicSettingsMapperPainted = true; // @todo bad duplication at end of function
                return;
            }

            if (!dynamicSettingsMapperPainted) {
                this.clearPanels();
                //ChartForm chartForm = Application.OpenForms["ChartForm"] as ChartForm;
                //panelToUpdate = chartForm.CurrentClickedPanel; //@todo another approach

                Panel panelChartStyle = new Panel();
                panelChartStyle.Text = "ChartStyle";
                Panel panelLegend = new Panel();
                panelLegend.Text = "Legend";

                this.addPanel(panelChartStyle);
                this.addPanel(panelLegend);

                MapperViewWithLabel mapperViewWithLabel = new MapperViewWithLabel();
                DynamicMapperBinds dynamicMapperBinds = new DynamicMapperBinds(
                    panelToUpdate as Panel,
                    panelToUpdate.ChartStyle.dd,
                    mapperViewWithLabel
                );
                dynamicMapperBinds.bind(panelChartStyle);

                mapperViewWithLabel = new MapperViewWithLabel();
                dynamicMapperBinds = new DynamicMapperBinds(
                    panelToUpdate as Panel,
                    panelToUpdate.Legend.dd,
                    mapperViewWithLabel
                );
                dynamicMapperBinds.bind(panelLegend);

                // init series
                int length = panelToUpdate.DynamicDataSeriesList.Count;
                foreach (string seriesName in panelToUpdate.DynamicDataSeriesList.Keys) {
                    Panel panelSeries = new Panel();
                    panelSeries.Text = seriesName;
                    this.addPanel(panelSeries);

                    mapperViewWithLabel = new MapperViewWithLabel();
                    dynamicMapperBinds = new DynamicMapperBinds(
                        ((Panel)panelToUpdate),
                        panelToUpdate.DynamicDataSeriesList[seriesName],
                        mapperViewWithLabel
                    );
                    dynamicMapperBinds.bind(panelSeries);
                }

                //@todo need convention for naming and including, it maps first graph with 3 graph
                //dynamicMapperBinds.PanelsToUpdate.Add(chartForm.PanelMatrix.Matrix[2]);

                dynamicSettingsMapperPainted = true;
            }
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

        public int SpaceWidth
        {
            get { return spaceWidth; }
            set { spaceWidth = value; }
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

        public ChartPanel PanelToUpdate
        {
            get { return panelToUpdate; }
            set { panelToUpdate = value; }
        }

        public DynamicPanel DynamicPanel
        {
            get { return currentDynamicPanel; }
            set { currentDynamicPanel = value; }
        }

        public Boolean IsPanelSwitchhable
        {
            get { return isPanelSwitchable; }
            set { isPanelSwitchable = value; }
        }

        public String Identifier
        {
            get
            { return identifier; }
            set { identifier = value; }
        }
    }
}