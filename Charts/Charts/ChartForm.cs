using Charts.Chart.ConnectorFolder;
using Charts.Chart.Identifier;
using Charts.Factories;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Charts
{
    /// consists ChartPanel, the panel that consist chart display
    public partial class ChartForm : Form, IIdentifier
    {
        private PanelMatrix panelMatrix;

        //private ChartPanel onlyLine;
        private int offsetPanelX = 10;

        private int offsetPanelY = 30;
        private ChartPanel currentClickedPanel; //@todo should be mapped more general
        private DynamicSettingsForm dynamicSettingsForm;

        private String identifier;

        public ChartForm()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.BackColor = Color.White;
            this.Location = new Point(125, 124);
            this.Size = new Size(664, 437);
        }

        public void initChartForm()
        {
            panelMatrix = new PanelMatrix(this);
            this.collectInitializedChartPanelsInPanelMatrix(panelMatrix);
            panelMatrix.addPanelsToForm(this);
            //this.initDynamicSettingsForm();
        }

        private void collectInitializedChartPanelsInPanelMatrix(PanelMatrix panelMatrix)
        {
            this.panelMatrix = panelMatrix;

            ////onlyLine = new ChartPanelLine(panelMatrix);
            //onlyLine = new ChartPanelLine();
            //onlyLine.Name = "ChartPanel";

            DataTable dataLine = new DataTable();
            dataLine.Columns.Add("x", typeof(float));
            dataLine.Columns.Add("y", typeof(float));
            for (int i = 0; i < 20; i++) {
                dataLine.Rows.Add(i, 4);
            }

            //this.Controls.Add(dataGridView);

            ChartPanel onlyLine = this.getBuilder().getBuiltChartPanelByData(new ChartPanelLine(), dataLine, "LinePanel");

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

            ChartPanel onlyBar = this.getBuilder().getBuiltChartPanelByData(new ChartPanelBar(), dataBar, "BarPanel");
            getConnector().connect(onlyBar).by(this);

            ChartPanel lineMergedBar = new ChartPanelBar();
            lineMergedBar.Data = dataBar;
            lineMergedBar.AlternativeChartPanel = onlyLine;
            lineMergedBar.initPanel();

            panelMatrix.add(onlyLine);
            panelMatrix.add(onlyBar);
        }

        protected BuilderInstance getBuilder()
        {
            return Inst.getBuilder();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            panelMatrix.initSizeOfPanels();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.initDynamicSettingsForm();
            this.showDynamicSettignsForm();
        }

        protected void initDynamicSettingsForm()
        {
            if (0 == panelMatrix.Matrix.Count) {
                throw new Exception("there are no panels in panelMatrix");
            }
            this.currentClickedPanel = panelMatrix.Matrix[0];
            this.dynamicSettingsForm = new DynamicSettingsForm();
            Inst.getStaticCall().initIdentifierOneTime(this.dynamicSettingsForm);
            getConnector().connect(this.dynamicSettingsForm).by(this);
            //this.dynamicSettingsForm.initDyanmicSettingsForm(this);
            this.dynamicSettingsForm.initDyanmicSettingsForm();
            //form.Show();
        }

        public Connector getConnector()
        {
            return Inst.getInstance().getConnector();
        }

        protected void showDynamicSettignsForm()
        {
            this.dynamicSettingsForm.Show();
            this.dynamicSettingsForm.Location = new Point(788, 124);
            this.dynamicSettingsForm.Size = new Size(547, 333);
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

        public PanelMatrix PanelMatrix
        {
            get { return panelMatrix; }
            set { panelMatrix = value; }
        }

        public ChartPanel CurrentClickedPanel
        {
            get { return currentClickedPanel; }
            set { currentClickedPanel = value; }
        }

        public DynamicSettingsForm DynamicSettingsForm
        {
            get { return dynamicSettingsForm; }
            set { dynamicSettingsForm = value; }
        }

        public String Identifier
        {
            get { return identifier; }
            set { identifier = value; }
        }
    }
}