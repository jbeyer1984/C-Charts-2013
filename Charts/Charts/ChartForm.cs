using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data;

/// consists ChartPanel, the panel that consist chart display
namespace Charts
{
    public partial class ChartForm : Form
    {
        private PanelMatrix panelMatrix;
        private ChartPanel chartPanel;
        private int offsetPanelX = 10;
        private int offsetPanelY = 30;

        public ChartForm()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.BackColor = Color.White;
            this.Location = new Point(125, 124);
            this.Size = new Size(664, 437);

            this.initChartPanel();
        }

        private void initChartPanel()
        {
            panelMatrix = new PanelMatrix(this);
            chartPanel = new ChartPanelLine(panelMatrix);
            chartPanel.Name = "ChartPanel";

            DataTable data = new DataTable();
            data.Columns.Add("x", typeof(float));
            data.Columns.Add("y", typeof(float));
            for (int i = 0; i < 20; i++) {
                //ds1.AddPoint(new PointF(i / 5.0f,
                //(float)Math.Sin(i / 5.0f)));
                data.Rows.Add(i, 4);
            }

            chartPanel.Data = data;

            DataTable dataBar = new DataTable();
            dataBar.Columns.Add("x", typeof(float));
            dataBar.Columns.Add("y", typeof(float));
            float x = 0;
            float y = 0;
            for (int i = 0; i < 5; i++) {
                x = i + 1;
                y = 1.0f * x;
                dataBar.Rows.Add(x, y);
            }

            ChartPanel onlyBar = new ChartPanelBar(panelMatrix);
            onlyBar.Data = dataBar;

            ChartPanel lineMergedBar = new ChartPanelBar(panelMatrix);
            lineMergedBar.Data = dataBar;
            lineMergedBar.AlternativeChartPanel = chartPanel;

            //panelMatrix.add(new ChartPanel(panelMatrix));
            panelMatrix.add(chartPanel);
            panelMatrix.add(onlyBar);
            panelMatrix.add(lineMergedBar);

            int size = panelMatrix.Matrix.Count;
            for (int i = 0; i < size; i++) {
                this.Controls.Add(panelMatrix.Matrix[i]);
            }

            this.initDynamicSettingsForm();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            panelMatrix.initSizeOfPanels();
        }

        protected void initDynamicSettingsForm()
        {
            Form form = new DynamicSettingsForm();
            form.Show();
            form.Location = new Point(788, 124);
            form.Size = new Size(547, 333);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.initDynamicSettingsForm();
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