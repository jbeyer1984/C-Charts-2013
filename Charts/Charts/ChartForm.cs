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
    public partial class ChartForm : Form
    {
        private DataCollection dc;
        private ChartStyle cs;
        private Legend lg;
        private DynamicSettingsMapper dynamicSettingsBox;
        //private Rectangle PlotArea;
        private int offset = 30;

        private float xMin = 4f;
        private float xMax = 6f;
        private float yMin = 3f;
        private float yMax = 6f;

        public ChartForm()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.BackColor = Color.White;
            this.Location = new Point(125, 124);
            this.Size = new Size(664, 437);

            dc = new DataCollection(); 
            cs = new ChartStyle(this);
            lg = new Legend();
            lg.IsLegendVisible = true;

            cs.dd.xLimMin = 0f;
            cs.dd.xLimMax = 6f;
            cs.dd.yLimMin = -1.1f;
            cs.dd.yLimMax = 1.1f;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            //e.Graphics.PixelOffsetMode = PixelOffsetMode.Half;
            cs.ChartArea = this.ClientRectangle;

            AddData();
            SetPlotArea(g);
            cs.AddChartStyle(g);
            dc.AddLines(g, cs);
            lg.AddLegend(g, dc, cs);

            g.Dispose();
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
            Form form = new DynamicSettingsForm();
            form.Show();
            form.Location = new Point(788, 124);
            form.Size = new Size(547, 333);
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
    }
}
