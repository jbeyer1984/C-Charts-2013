using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Collections;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Charts
{
    public partial class ChartPanel : System.Windows.Forms.Panel
    {
        private ChartForm chartParent;
        private DataCollection dc;
        private DataCollection dc2;
        private ChartStyle cs;
        private Legend lg;
        private int offsetPanelX = 10;
        private int offsetPanelY = 30;

        public Dictionary<String, DynamicData> dynamicDataList = new Dictionary<string,DynamicData>();

        public ChartPanel(ChartForm chartParent)
        {
            this.chartParent = chartParent;
            //this.SetStyle(ControlStyles.ResizeRedraw, true);
            ResizeRedraw = true;
            initPanel();
            initDataToPlot();
        }

        private void initPanel()
        {
            this.Left = offsetPanelX;
            this.Top = offsetPanelY;
            this.updateSize();
            this.Visible = true;

            this.Paint += new PaintEventHandler(PlotPanelPaint);
        }

        public void updateSize()
        {
            this.Width = chartParent.Size.Width - offsetPanelX;
            this.Height = chartParent.Size.Height - 2 * offsetPanelY;
        }

        private void initDataToPlot()
        {
            dc = new DataCollection();
            dc2 = new DataCollection();
            cs = new ChartStyle(this);
            //cs.ChartArea = this.ClientRectangle;
            lg = new Legend();
            lg.IsLegendVisible = true;

            cs.dd.xLimMin = 0f;
            cs.dd.xLimMax = 6f;
            //cs.dd.yLimMin = -1.1f;
            cs.dd.yLimMin = 0f;
            //cs.dd.yLimMax = 1.1f;
            cs.dd.yLimMax = 5f;

        }

        private void PlotPanelPaint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            this.updateSize();

            //cs.ChartArea = this.ClientRectangle;

            // add lines to dc
            addData();
            this.setPlotArea(g);
            cs.AddChartPlot(g);
            dc.AddLines(g, cs);

            // add barst to dc2
            this.addDataTestBar(dc2);
            //this.setPlotArea(g);
            //cs.AddChartPlot(g);
            dc2.addBars(g, cs, 1, 4);
            lg.AddLegend(g, dc, cs);

            g.Dispose();
            //AddData(g);
            //cs.PlotPanelStyle(g);
            //dc.AddBars(g, cs, dc.DataSeriesList.Count, ds.PointList.Count);
        }

        private void setPlotArea(Graphics g)
        {
            // Set PlotArea: 
            int xOffset = cs.ChartArea.Width / 10;
            int yOffset = cs.ChartArea.Height / 10;
            // Define the plot area: 
            int plotX = cs.ChartArea.X + xOffset;
            int plotY = cs.ChartArea.Y + yOffset;
            int plotWidth = cs.ChartArea.Width - 2 * xOffset;
            int plotHeight = cs.ChartArea.Height - 2 * yOffset;
            cs.PlotArea = new Rectangle(plotX, plotY, plotWidth, plotHeight);
        }

        public void addData()
        {
            dc.DataSeriesList.Clear();
            // Add Sine data with 20 data points: 
            DataSeries ds1 = new DataSeries();
            if (!dynamicDataList.ContainsKey("ds1"))
            {
                ds1.dd.lineStyle.LineColor = Color.Red;
                ds1.dd.lineStyle.Thickness = 2f;
                ds1.dd.lineStyle.Pattern = DashStyle.Dash;
                dynamicDataList.Add("ds1", ds1.dd);
            } else {
                ds1.dd = (DynamicDataSeries) dynamicDataList["ds1"];
            }
            //ds1.dd.lineStyle.LineColor = Color.Red;
            //ds1.dd.lineStyle.Thickness = 2f;
            //ds1.dd.lineStyle.Pattern = DashStyle.Dash;
            
            for (int i = 0; i < 20; i++)
            {
                //ds1.AddPoint(new PointF(i / 5.0f,
                //(float)Math.Sin(i / 5.0f)));
                ds1.AddPoint(new PointF(i, 4));
            }
            dc.add(ds1);
            // Add Cosine data with 40 data points: 
            //DataSeries ds2 = new DataSeries();
            //ds2.LineStyle.LineColor = Color.Blue;
            //ds2.LineStyle.Thickness = 1f;
            //ds2.LineStyle.Pattern = DashStyle.Solid;
            //for (int i = 0; i < 40; i++)
            //{
            //    ds2.AddPoint(new PointF(i / 5.0f,
            //    (float)Math.Cos(i / 5.0f)));
            //}
            //dc.add(ds2);
        }

        public void addDataTestBar(DataCollection dc)
        {
            float x, y;
            // Add data series: 
            dc.DataSeriesList.Clear();
            DataSeries ds = new DataSeries();
            ds = new DataSeries();
            //ds.BarStyle.BorderColor = Color.Red;
            //ds.BarStyle.BorderColor = Color.Green;
            ds.BarStyle.BorderColor = Color.Transparent;
            ds.BarStyle.FillColor = Color.Green;
            ds.BarStyle.BarWidth = 0.6f;
            for (int i = 0; i < 5; i++)
            {
                x = i + 1;
                y = 1.0f * x;
                ds.AddPoint(new PointF(x, y));
            }
            dc.add(ds);
        }

        public Legend Legend
        {
            get { return lg; }
            set { value = lg; }
        }

        public ChartStyle ChartStyle
        {
            get { return cs; }
            set { value = cs; }
        }

        public Size size
        {
            get { return size; }
            set { value = size; }
        }
    }
}
