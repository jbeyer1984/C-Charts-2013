using Charts.Chart.CacheFolder.CacheInterfaces;
using Charts.Chart.CacheFolder;
using Charts.Chart.ConnectorFolder;
using Charts.Chart.Identifier;
using Charts.Chart.StateFolder;
using Charts.Chart.StateFolder.StateInterfaces;
using Charts.Chart.StaticCallsFolder;
using Charts.Factories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Charts
{
    public enum EnumChartPanelState
    {
        isDrawn,
        invalidate,
        isSelected,
        isMarkedSelected,
        isOverwrittenInvalidate,
        selectedOption,
        notInitialized,
        isInitialized,
        reInitialize
    }

    public enum EnumChartPanelSelectMode
    {
        isMultipleSelect,
        isOneSelect,
    }

    public enum EnumChartPanelGlobalMode
    {
        select,
        change,
        append,
        repaint
    }

    public abstract partial class ChartPanel : System.Windows.Forms.Panel, ICacheAble, IIdentifier, IStateFull
    {
        private PanelMatrix panelMatrix;
        private DataTable data;
        private DataCollection dataCollection;
        private ChartStyle chartStyle;
        private ChartPanel alternativeChartPanel;
        private Legend legend;
        private int offsetPanelX = 10;
        private int offsetPanelY = 30;

        private Dictionary<String, DynamicData> dynamicDataSeriesList = new Dictionary<string, DynamicData>();

        DataGridView dataGridView;

        private OverwriteDataComponents overwriteDataComponents = new OverwriteDataComponents();

        private StateAbstract state;

        private String identifier;
        private static int numOfInstances = 0;
        private Boolean isFirstTimeCreated = false;

        public ChartPanel()
        {
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            ResizeRedraw = true;
            //DoubleBuffered = true;
        }

        public virtual void initDataGrid()
        {
            dataGridView = new DataGridView();
            dataGridView.DataSource = data;
            dataGridView.Location = this.Location;
            dataGridView.Left = 0;
            dataGridView.Top = 0;
            dataGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridChange);
        }

        protected void dataGridChange(object sender, DataGridViewCellEventArgs e)
        {
            this.Invalidate();
        }

        public virtual void initPanel()
        {
            this.Left = offsetPanelX;
            this.Top = offsetPanelY;
            //this.updateSize();
            this.Visible = true;

            this.Paint += new PaintEventHandler(PlotPanelPaint);
            this.Click += new System.EventHandler(this.mouseClick);
        }

        protected void mouseClick(object sender, EventArgs e)
        {
            ChartForm chartForm = Application.OpenForms["ChartForm"] as ChartForm; // @todo think about connector
            chartForm.CurrentClickedPanel = this;
            DynamicSettingsForm dynamicSettingsForm = chartForm.DynamicSettingsForm;
            if (null == dynamicSettingsForm) { //@todo needs better approach

                return;
            }
            dynamicSettingsForm.initDyanmicSettingsForm();
            dynamicSettingsForm.allowRepaint(); //@todo needs better approach
            dynamicSettingsForm.Invalidate();
        }

        public StateAbstract getState(Type type) // @todo other calls should be cleaned
        {
            if (null == state) {
                state = getCache().with(this).canBeNew().getByType(type) as StateChartPanelBar; // @todo wrong should not be Bar State
            }

            return state;
        }

        public Cache getCache()
        {
            return Inst.getInstance().getCache();
        }

        public Connector getConnector()
        {
            return Inst.getInstance().getConnector();
        }

        public void initChartArea()
        {
            if (null != chartStyle) {
                chartStyle.ChartArea = this.ClientRectangle;
            }
        }

        public void initDataToPlot()
        {
            dataCollection = new DataCollection();
            chartStyle = new ChartStyle(this);
            legend = new Legend();
            legend.IsLegendVisible = true;

            // @todo is fix should be more dynamic maybe
            chartStyle.dd.xLimMin = 0f;
            chartStyle.dd.xLimMax = 6f;
            //cs.dd.yLimMin = -1.1f;
            chartStyle.dd.yLimMin = 0f;
            //cs.dd.yLimMax = 1.1f;
            chartStyle.dd.yLimMax = 5f;
            chartStyle.dd.YTick = 0.5f;
        }

        protected virtual void PlotPanelPaint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;


            dataCollection.DataSeriesList.Clear();
            DataCollection.seriesCounter = 0;

            if (null != alternativeChartPanel) { // @todo another approach is needed
                alternativeChartPanel.fillChartType(g, alternativeChartPanel.Data);
            }
            this.fillChartType(g, data);

            g.Dispose();
        }

        public virtual void fillChartType(Graphics g, DataTable data)
        {
            // add chart to panel
            this.addData(dataCollection, data);
            this.setPlotArea();
            if (null == alternativeChartPanel) { // @todo another approach is needed
                chartStyle.AddChartPlot(g);
            }
        }

        protected abstract void addData(DataCollection dc, DataTable data);

        protected void setPlotArea()
        {
            // @todo offset ist fix, should be changed
            // Set PlotArea:
            int xOffset = 50; // cs.ChartArea.Width / 10;
            int yOffset = 40; // cs.ChartArea.Height / 10;
            // Define the plot area:
            int plotX = chartStyle.ChartArea.X + xOffset;
            int plotY = chartStyle.ChartArea.Y + yOffset;
            int plotWidth = chartStyle.ChartArea.Width - 2 * xOffset;
            int plotHeight = chartStyle.ChartArea.Height - 2 * yOffset;
            chartStyle.PlotArea = new Rectangle(plotX, plotY, plotWidth, plotHeight);
        }

        public PanelMatrix PanelMatrix
        {
            get { return panelMatrix; }
            set { panelMatrix = value; }
        }

        public DataTable Data
        {
            get { return data; }
            set { data = value; }
        }

        public Legend Legend
        {
            get { return legend; }
            set { legend = value; }
        }

        public ChartStyle ChartStyle
        {
            get { return chartStyle; }
            set { chartStyle = value; }
        }

        public ChartPanel AlternativeChartPanel
        {
            get { return alternativeChartPanel; }
            set { alternativeChartPanel = value; }
        }

        public DataCollection DataCollection
        {
            get { return dataCollection; }
            set { dataCollection = value; }
        }

        public int OffsetPanelX
        {
            get { return offsetPanelX; }
            set { offsetPanelX = value; }
        }

        public int OffsetPanelY
        {
            get { return offsetPanelY; }
            set { offsetPanelY = value; }
        }

        public Dictionary<String, DynamicData> DynamicDataSeriesList
        {
            get { return dynamicDataSeriesList; }
            set { dynamicDataSeriesList = value; }
        }

        public OverwriteDataComponents OverwriteDataComponents
        {
            get { return overwriteDataComponents; }
            set { overwriteDataComponents = value; }
        }

        public StateAbstract State
        {
            get { return state; }
            set { state = value; }
        }

        public String Identifier
        {
            get { return identifier; }
            set { identifier = value; }
        }

        public Boolean IsAlreadyCreated
        {
            get { return isFirstTimeCreated = false; }
            set { isFirstTimeCreated = value; }
        }

        public DataGridView DataGridView
        {
            get { return dataGridView; }
            set { dataGridView = value; }
        }
    }
}