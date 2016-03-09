using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Charts
{
    public partial class Form1 : Form
    {
        private DataCollection dc;
        private ChartStyle cs;
        private Legend lg;
        //private Rectangle PlotArea;
        private int offset = 30;

        private float xMin = 4f;
        private float xMax = 6f;
        private float yMin = 3f;
        private float yMax = 6f;

        public Form1()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.BackColor = Color.White;

            dc = new DataCollection(); 
            cs = new ChartStyle(this);
            lg = new Legend();
            lg.IsLegendVisible = true;

            cs.XLimMin = 0f;
            cs.XLimMax = 6f;
            cs.YLimMin = -1.1f;
            cs.YLimMax = 1.1f;
            

            // Subscribing to a paint eventhandler to drawingPanel: 
            //drawingPanel.Paint += new PaintEventHandler(drawingPanelPaint);
            //drawingPanel.BorderStyle = BorderStyle.FixedSingle;
            //drawingPanel.Anchor = AnchorStyles.Bottom;
            //drawingPanel.Anchor = AnchorStyles.Left;
            //drawingPanel.Anchor = AnchorStyles.Right;
            //drawingPanel.Anchor = AnchorStyles.Top;

            //TestGetter testGetter = new TestGetter();
            //testGetter.Test = 3;
            //testGetter.print();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            cs.ChartArea = this.ClientRectangle;

            AddData();
            SetPlotArea(g);
            cs.AddChartStyle(g);
            dc.AddLines(g, cs);
            lg.AddLegend(g, dc, cs);

            g.Dispose();
        }

        private void drawingPanelPaint(object sender, PaintEventArgs e)
        {
            drawingPanel.Left = offset;
            drawingPanel.Top = offset;
            drawingPanel.Width = ClientRectangle.Width - 2 * offset;
            drawingPanel.Height = ClientRectangle.Height - 2 * offset;

            Graphics g = e.Graphics;
            ChartStyle chartStyle = new ChartStyle(this);
            chartStyle.AddChartStyle(g);

            AddData();
            SetPlotArea(g);
            cs.AddChartStyle(g);
            dc.AddLines(g, cs);
            g.Dispose();

            //Pen aPen = new Pen(Color.Green, 3);
            //g.DrawLine(aPen, Point2D(new PointF(2, 3)),
            //Point2D(new PointF(6, 7)));
            //aPen.Dispose();
            //g.Dispose();
        }

        public void AddData()
        {
            dc.DataSeriesList.Clear();
            // Add Sine data with 20 data points: 
            DataSeries ds1 = new DataSeries();
            ds1.LineStyle.LineColor = Color.Red;
            ds1.LineStyle.Thickness = 2f;
            ds1.LineStyle.Pattern = DashStyle.Dash;
            for (int i = 0; i < 20; i++)
            {
                ds1.AddPoint(new PointF(i / 5.0f,
                (float)Math.Sin(i / 5.0f)));
            }
            dc.Add(ds1);
            // Add Cosine data with 40 data points: 
            DataSeries ds2 = new DataSeries();
            ds2.LineStyle.LineColor = Color.Blue;
            ds2.LineStyle.Thickness = 1f;
            ds2.LineStyle.Pattern = DashStyle.Solid;
            for (int i = 0; i < 40; i++)
            {
                ds2.AddPoint(new PointF(i / 5.0f,
                (float)Math.Cos(i / 5.0f)));
            }
            dc.Add(ds2);
        }

        private void SetPlotArea(Graphics g)
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

        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    Graphics g = e.Graphics;
            // Following codes draw a line from (0, 0) to (1, 1) in unit of inch: 
            //g.PageUnit = GraphicsUnit.Inch;
            //Pen blackPen = new Pen(Color.Black, 1 / g.DpiX);
            //g.DrawLine(blackPen, 0, 0, 1, 1);

            //paintGreenLineFromMiddle(g);

            //paintRectArea(g);
        //}

        //private void paintRectArea(Graphics g)
        //{
        //    // Calculate the location and size of the drawing area 
        //    // within which we want to draw the graphics: 
        //    Rectangle rect = ClientRectangle;
        //    PlotArea = new Rectangle(rect.Location, rect.Size);
        //    PlotArea.Inflate(-offset, -offset);
        //    //Draw ClientRectangle and PlotArea using Pen: 
        //    g.DrawRectangle(Pens.Red, rect);
        //    g.DrawRectangle(Pens.Black, PlotArea);
        //    // Draw a line from point (3,2) to Point (6, 7) 
        //    // using a Pen with a width of 3 pixels: 
        //    Pen aPen = new Pen(Color.Green, 3);
        //    g.DrawLine(aPen, Point2D(new PointF(3, 2)), Point2D(new PointF(6, 7)));
        //    aPen.Dispose();
        //    g.Dispose();
        //}

        private PointF Point2D(PointF ptf)
        {
            PointF aPoint = new PointF();
            aPoint.X = (ptf.X - xMin) * drawingPanel.Width / (xMax - xMin);
            aPoint.Y = drawingPanel.Height - (ptf.Y - yMin) *
            drawingPanel.Height / (yMax - yMin);
            return aPoint;
        }

        //private PointF Point2D(PointF ptf)
        //{
        //    PointF aPoint = new PointF();
        //    aPoint.X = PlotArea.X + (ptf.X - xMin) *
        //    PlotArea.Width / (xMax - xMin);
        //    aPoint.Y = PlotArea.Bottom - (ptf.Y - yMin) *
        //    PlotArea.Height / (yMax - yMin);
        //    return aPoint;
        //} 
 
        private void paintGreenLineFromMiddle(Graphics g)
        {
            g.TranslateTransform(
                (ClientRectangle.Width / g.DpiX) / 2, 
                (ClientRectangle.Height / g.DpiY) / 2
            ); 
            Pen greenPen = new Pen(Color.Green, 1 / g.DpiX); 
            g.DrawLine(greenPen, 0, 0, 1, 1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("hallo");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void drawingPanel_Paint(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            // Subscribing to a paint eventhandler to drawingPanel: 
            drawingPanel.Paint += new PaintEventHandler(drawingPanelPaint);
            drawingPanel.BorderStyle = BorderStyle.FixedSingle;
            drawingPanel.Anchor = AnchorStyles.Bottom;
            drawingPanel.Anchor = AnchorStyles.Left;
            drawingPanel.Anchor = AnchorStyles.Right;
            drawingPanel.Anchor = AnchorStyles.Top;
        }
    }
}
