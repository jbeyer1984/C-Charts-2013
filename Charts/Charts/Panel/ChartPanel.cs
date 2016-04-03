using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Charts
{
    public partial class ChartPanel : System.Windows.Forms.Panel
    {
        private PanelMatrix panelMatrix;
        private DataTable data;
        private DataCollection dataCollection;
        private ChartStyle chartStyle;
        private ChartPanel alternativeChartPanel;
        private Legend legend;
        private int offsetPanelX = 10;
        private int offsetPanelY = 30;

        public Dictionary<String, DynamicData> dynamicDataSeriesList = new Dictionary<string, DynamicData>();

        public ChartPanel(PanelMatrix panelMatrix)
        {
            this.panelMatrix = panelMatrix;
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            ResizeRedraw = true;
            initPanel();
            initDataToPlot();
        }

        private void initPanel()
        {
            this.Left = offsetPanelX;
            this.Top = offsetPanelY;
            //this.updateSize();
            this.Visible = true;

            this.Paint += new PaintEventHandler(PlotPanelPaint);
        }

        public void initChartArea()
        {
            if (null != chartStyle) {
                chartStyle.ChartArea = this.ClientRectangle;
            }
        }

        private void initDataToPlot()
        {
            dataCollection = new DataCollection();
            chartStyle = new ChartStyle(this);
            //cs.ChartArea = this.ClientRectangle;
            legend = new Legend();
            legend.IsLegendVisible = true;

            chartStyle.dd.xLimMin = 0f;
            chartStyle.dd.xLimMax = 6f;
            //cs.dd.yLimMin = -1.1f;
            chartStyle.dd.yLimMin = 0f;
            //cs.dd.yLimMax = 1.1f;
            chartStyle.dd.yLimMax = 5f;
        }

        protected virtual void PlotPanelPaint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            Console.WriteLine("paint now");

            dataCollection.DataSeriesList.Clear();
            DataCollection.seriesCounter = 0;

            if (null != alternativeChartPanel) {
                alternativeChartPanel.fillChartType(g, alternativeChartPanel.Data);
            }
            this.fillChartType(g, data);

            g.Dispose();
        }

        protected virtual void fillChartType(Graphics g, DataTable data)
        {
            // add lines to dc
            this.addData(dataCollection, data);
            this.setPlotArea();
            dataCollection.AddLines(g, chartStyle);
        }

        protected void setPlotArea()
        {
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

        public virtual void addData(DataCollection dc, DataTable data)
        {
            dc.DataSeriesList.Clear();
            // Add Sine data with 20 data points:
            DataSeries ds = new DataSeries();

            //ds1.dd.lineStyle.LineColor = Color.Red;
            //ds1.dd.lineStyle.Thickness = 2f;
            //ds1.dd.lineStyle.Pattern = DashStyle.Dash;
            //int size = 0;
            int size = data.Rows.Count;

            for (int i = 0; i < size; i++) {
                //ds1.AddPoint(new PointF(i / 5.0f,
                //(float)Math.Sin(i / 5.0f)));
                ds.AddPoint(new PointF(data.Rows[i].Field<float>(0), data.Rows[i].Field<float>(1)));
            }
            dc.add(ds);
            if (!dynamicDataSeriesList.ContainsKey(ds.SeriesName)) {
                Console.WriteLine(ds.SeriesName);
                ds.dd.lineStyle.LineColor = Color.Red;
                ds.dd.lineStyle.Thickness = 2f;
                ds.dd.lineStyle.Pattern = DashStyle.Dash;
                dynamicDataSeriesList.Add(ds.SeriesName, ds.dd);
            } else {
                ds.dd = (DynamicDataSeries)dynamicDataSeriesList[ds.SeriesName];
            }
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
    }
}