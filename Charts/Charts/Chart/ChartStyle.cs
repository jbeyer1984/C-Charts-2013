using Charts.Dynamic.Data;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Charts
{
    /// <summary>
    /// @todo make overview and comment
    /// </summary>
    public class ChartStyle : IDynamicData
    {
        private Panel panel;
        private Rectangle chartArea;
        private Rectangle plotArea;
        private Color chartBackColor;
        private Color chartBorderColor;
        private Color plotBackColor = Color.White;
        private Color plotBorderColor = Color.Black;

        //private StyleEnum styleType = StyleEnum.Bar;

        public DynamicDataChartStyle dd;
        public DynamicDataPlot ddp;

        private DashStyle gridPattern = DashStyle.Solid;
        private Color gridColor = Color.LightGray;
        private float gridLineThickness = 1.0f;
        private bool isXGrid = true;
        private bool isYGrid = true;
        private string xLabel = "X Axis";
        private string yLabel = "Y Axis";
        private string sTitle = "Title";
        private Font labelFont = new Font("Arial", 10, FontStyle.Regular);
        private Color labelFontColor = Color.Black;
        private Font titleFont = new Font("Arial", 12, FontStyle.Regular);
        private Color titleFontColor = Color.Black;
        private Font tickFont;
        private Color tickFontColor = Color.Black;

        public ChartStyle(Panel panel)
        {
            this.panel = panel;
            chartArea = panel.ClientRectangle;
            chartBackColor = panel.BackColor;
            chartBorderColor = panel.BackColor;
            PlotArea = chartArea;
            tickFont = panel.Font;

            initDynamicData();
        }

        public void initDynamicData()
        {
            dd = new DynamicDataChartStyle();

            dd.xLimMin = 0f;
            dd.xLimMax = 10f;
            dd.yLimMin = 0f;
            dd.yLimMax = 10f;

            dd.test = 4f;

            //ddp = new DynamicDataPlot();

            dd.xTickOffset = 0.5f;
            //dd.xTick = 1f;
            //dd.yTick = 0.5f;
        }

        public void AddChartPlot(Graphics g)
        {
            // Draw ChartArea and PlotArea:
            Pen aPen = new Pen(ChartBorderColor, 1f);
            SolidBrush aBrush = new SolidBrush(ChartBackColor);
            g.FillRectangle(aBrush, ChartArea);
            g.DrawRectangle(aPen, ChartArea);
            aPen = new Pen(PlotBorderColor, 1f);
            aBrush = new SolidBrush(PlotBackColor);
            g.FillRectangle(aBrush, PlotArea);
            g.DrawRectangle(aPen, PlotArea);
            SizeF tickFontSize = g.MeasureString("A", TickFont);

            float xStartPoint = 0;

            if (dd.styleType == DynamicDataChartStyle.StyleEnum.Bar) {
                xStartPoint = dd.xTickOffset + dd.xLimMin + dd.xTick / 2;
            }

            // Create vertical gridlines:
            float fX, fY;
            if (IsYGrid == true) {
                aPen = new Pen(GridColor, 1f);
                aPen.DashStyle = GridPattern;
                for (fX = xStartPoint; fX < dd.xLimMax; fX += dd.xTick) {
                    g.DrawLine(aPen, Point2D(new PointF(fX, dd.yLimMin)),
                    Point2D(new PointF(fX, dd.yLimMax)));
                }
            }

            // Create horizontal gridlines:
            if (IsXGrid == true) {
                aPen = new Pen(GridColor, 1f);
                aPen.DashStyle = GridPattern;
                for (fY = dd.yLimMin + dd.yTick; fY < dd.yLimMax; fY += dd.yTick) {
                    g.DrawLine(aPen, Point2D(new PointF(dd.xLimMin, fY)),
                    Point2D(new PointF(dd.xLimMax, fY)));
                }
            }

            // Create the x-axis tick marks:
            aBrush = new SolidBrush(TickFontColor);
            for (fX = xStartPoint; fX <= dd.xLimMax; fX += dd.xTick) {
                PointF yAxisPoint = Point2D(new PointF(fX, dd.yLimMin));
                g.DrawLine(
                    Pens.Black, yAxisPoint,
                    new PointF(yAxisPoint.X, yAxisPoint.Y - 4f)
                );
                StringFormat sFormat = new StringFormat();
                sFormat.Alignment = StringAlignment.Far;
                SizeF sizeXTick = g.MeasureString(fX.ToString(), TickFont);
                g.DrawString(
                    fX.ToString(), TickFont, aBrush,
                    new PointF(
                        yAxisPoint.X + sizeXTick.Width / 2,
                        yAxisPoint.Y + 4f
                    ),
                    sFormat
                );
            }

            // Create the y-axis tick marks:
            for (fY = dd.yLimMin; fY <= dd.yLimMax; fY += dd.yTick) {
                PointF xAxisPoint = Point2D(new PointF(dd.xLimMin, fY));
                g.DrawLine(Pens.Black, xAxisPoint,
                new PointF(xAxisPoint.X + 5f, xAxisPoint.Y));
                StringFormat sFormat = new StringFormat();
                sFormat.Alignment = StringAlignment.Far;
                g.DrawString(fY.ToString(), TickFont, aBrush,
                new PointF(xAxisPoint.X - 3f,
                xAxisPoint.Y - tickFontSize.Height / 2), sFormat);
            }
            aPen.Dispose();
            aBrush.Dispose();
            AddLabels(g);
        }

        private void AddLabels(Graphics g)
        {
            float xOffset = chartArea.Width / 100.0f;
            float yOffset = chartArea.Height / 100.0f;
            SizeF labelFontSize = g.MeasureString("A", LabelFont);
            SizeF titleFontSize = g.MeasureString("A", TitleFont);
            // Add horizontal axis label:
            SolidBrush aBrush = new SolidBrush(LabelFontColor);
            SizeF stringSize = g.MeasureString(XLabel, LabelFont);
            g.DrawString(XLabel, LabelFont, aBrush,
            new Point(PlotArea.Left + PlotArea.Width / 2 -
            (int)stringSize.Width / 2, ChartArea.Bottom -
            (int)yOffset - (int)labelFontSize.Height));
            // Add y-axis label:
            StringFormat sFormat = new StringFormat();
            sFormat.Alignment = StringAlignment.Center;
            stringSize = g.MeasureString(YLabel, LabelFont);
            // Save the state of the current Graphics object
            GraphicsState gState = g.Save();
            g.TranslateTransform(xOffset,
                yOffset + titleFontSize.Height
            + yOffset / 3 + PlotArea.Height / 2);
            g.RotateTransform(-90);
            g.DrawString(YLabel, LabelFont, aBrush, 0, 0, sFormat);
            // Restore it:
            g.Restore(gState);

            // Add title:
            aBrush = new SolidBrush(TitleFontColor);
            stringSize = g.MeasureString(Title, TitleFont);
            if (Title.ToUpper() != "NO TITLE") {
                g.DrawString(Title, TitleFont, aBrush,
                new Point(PlotArea.Left + PlotArea.Width / 2 -
                (int)stringSize.Width / 2,
                    ChartArea.Top + (int)yOffset));
            }
            aBrush.Dispose();
        }

        public PointF Point2D(PointF pt)
        {
            PointF aPoint = new PointF();
            if (pt.X < dd.xLimMin || pt.X > dd.xLimMax ||
            pt.Y < dd.yLimMin || pt.Y > dd.yLimMax) {
                pt.X = Single.NaN;
                pt.Y = Single.NaN;
            }
            aPoint.X = PlotArea.X + (pt.X - dd.xLimMin) *
            PlotArea.Width / (dd.xLimMax - dd.xLimMin);
            aPoint.Y = PlotArea.Bottom - (pt.Y - dd.yLimMin) *
            PlotArea.Height / (dd.yLimMax - dd.yLimMin);

            return aPoint;
        }

        public Font TickFont
        {
            get { return tickFont; }
            set { tickFont = value; }
        }

        public Color TickFontColor
        {
            get { return tickFontColor; }
            set { tickFontColor = value; }
        }

        public Color ChartBackColor
        {
            get { return chartBackColor; }
            set { chartBackColor = value; }
        }

        public Color ChartBorderColor
        {
            get { return chartBorderColor; }
            set { chartBorderColor = value; }
        }

        public Color PlotBackColor
        {
            get { return plotBackColor; }
            set { plotBackColor = value; }
        }

        public Color PlotBorderColor
        {
            get { return plotBorderColor; }
            set { plotBorderColor = value; }
        }

        public Rectangle ChartArea
        {
            get { return chartArea; }
            set { chartArea = value; }
        }

        public Rectangle PlotArea
        {
            get { return plotArea; }
            set { plotArea = value; }
        }

        public bool IsXGrid
        {
            get { return isXGrid; }
            set { isXGrid = value; }
        }

        public bool IsYGrid
        {
            get { return isYGrid; }
            set { isYGrid = value; }
        }

        public string Title
        {
            get { return sTitle; }
            set { sTitle = value; }
        }

        public string Label
        {
            get { return xLabel; }
            set { xLabel = value; }
        }

        public string XLabel
        {
            get { return xLabel; }
            set { xLabel = value; }
        }

        public string YLabel
        {
            get { return yLabel; }
            set { yLabel = value; }
        }

        public Font LabelFont
        {
            get { return labelFont; }
            set { labelFont = value; }
        }

        public Color LabelFontColor
        {
            get { return labelFontColor; }
            set { labelFontColor = value; }
        }

        public Font TitleFont
        {
            get { return titleFont; }
            set { titleFont = value; }
        }

        public Color TitleFontColor
        {
            get { return titleFontColor; }
            set { titleFontColor = value; }
        }

        public float xLimMax
        {
            get { return dd.xLimMax; }
            set { dd.xLimMax = value; }
        }

        //public float xLimMin
        //{
        //    get { return dd.xLimMin; }
        //    set { dd.xLimMin = value; }
        //}

        //public float yLimMax
        //{
        //    get { return dd.yLimMax; }
        //    set { dd.yLimMax = value; }
        //}

        //public float yLimMin
        //{
        //    get { return dd.yLimMin; }
        //    set { dd.yLimMin = value; }
        //}

        //public float XTick
        //{
        //    get { return xTick; }
        //    set { xTick = value; }
        //}

        //public float YTick
        //{
        //    get { return yTick; }
        //    set { yTick = value; }
        //}

        virtual public DashStyle GridPattern
        {
            get { return gridPattern; }
            set { gridPattern = value; }
        }

        public float GridThickness
        {
            get { return gridLineThickness; }
            set { gridLineThickness = value; }
        }

        virtual public Color GridColor
        {
            get { return gridColor; }
            set { gridColor = value; }
        }

        //public StyleEnum StyleType
        //{
        //    get { return styleType; }
        //    set { value = styleType; }
        //}

        //public enum StyleEnum
        //{
        //    Normal = 0,
        //    Bar = 1
        //}
    }
}