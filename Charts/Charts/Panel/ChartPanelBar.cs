using Charts.Chart.StateFolder;
using Charts.Chart.Wrapper;
using Charts.Factories;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Charts
{
    public class ChartPanelBar : ChartPanel
    {
        public ChartPanelBar() :
            base()
        {
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            ResizeRedraw = true;
        }

        public override void initPanel()
        {
            base.initPanel();
            //button.MouseUp += new System.Windows.Forms.MouseEventHandler(this.overrideSelectedBar);
            //this.Click += new System.EventHandler(this.mouseClickBar);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mouseClickBar);
            State = getState(typeof(StateChartPanelBar));
            State.State = EnumChartPanelState.notInitialized.ToString();
            State.SelectMode = EnumChartPanelSelectMode.isOneSelect.ToString();
        }

        /// <summary>
        /// add data to data collection
        /// </summary>
        /// <param name="dc"></param>
        /// <param name="data"></param>
        protected override void addData(DataCollection dc, DataTable data)
        {
            DataSeries ds = new DataSeries();

            int size = data.Rows.Count;

            for (int i = 0; i < size; i++) {
                ds.AddPoint(new PointF(data.Rows[i].Field<float>(0), data.Rows[i].Field<float>(1)));
            }
            dc.add(ds);

            if (!DynamicDataSeriesList.ContainsKey(ds.SeriesName)) {
                ds.BarStyle.dd.BorderColor = Color.Transparent;
                ds.BarStyle.dd.FillColor = Color.Green;
                ds.BarStyle.dd.BarWidth = 0.6f;
                DynamicDataSeriesList.Add(ds.SeriesName, ds.BarStyle.dd);
            } else {
                ds.BarStyle.dd = (DynamicDataBar)DynamicDataSeriesList[ds.SeriesName];
            }
        }

        protected override void PlotPanelPaint(object sender, PaintEventArgs e)
        {
            getState(this.GetType()).State = EnumChartPanelState.invalidate.ToString();
            base.PlotPanelPaint(sender, e);

            getState(this.GetType()).State = EnumChartPanelState.isDrawn.ToString();
        }

        protected void mouseClickBar(object sender, MouseEventArgs e)
        {
            State.State = EnumChartPanelState.isSelected.ToString();
            State.GlobalMode = EnumChartPanelGlobalMode.select.ToString();

            MouseEventArgs me = e as MouseEventArgs;

            if (e.Button == MouseButtons.Left) {
                foreach (ZoneExecutorDrawSeries zoneExecutorDrawSeries in OverwriteDataComponents.CollectionDrawerOverwrite.ZoneExecutorSeriesList) {
                    zoneExecutorDrawSeries.executeClick(sender, me);
                }
            }

            SelectedOptionPanelWrapper selectedOptionPanelWrapper = new SelectedOptionPanelWrapper();
            selectedOptionPanelWrapper = Inst.getBuilder().getBuiltSelectedOptionPanelWrapperOneTime(selectedOptionPanelWrapper, this);
            DataTablePanelWrapper dataTablePanelWrapper = new DataTablePanelWrapper();
            dataTablePanelWrapper = Inst.getBuilder().getBuiltDataTablePanelWrapperOneTime(dataTablePanelWrapper, this);
            if (e.Button == MouseButtons.Right) {
                dataTablePanelWrapper.show();
                selectedOptionPanelWrapper.show();
            } else {
                dataTablePanelWrapper.hide();
                selectedOptionPanelWrapper.hide();
            }

            //this.overrideSelectedBar();
        }

        /// <summary>
        /// add bars to data collection
        /// </summary>
        /// <param name="g"></param>
        /// <param name="data"></param>
        public override void fillChartType(Graphics g, DataTable data)
        {
            base.fillChartType(g, data);

            CollectionDrawer barDrawer = new CollectionDrawerBar(this);
            //DataCollection.addBars(g, ChartStyle, 1, 4);
            barDrawer.drawCollection(g);
            Legend.AddLegend(g, DataCollection, ChartStyle);
        }
    }
}