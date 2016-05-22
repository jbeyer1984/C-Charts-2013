using Charts.Chart.StateFolder;
using Charts.Chart.Wrapper;
using Charts.Factories;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Charts
{
    public class ChartPanelLine : ChartPanel
    {
        public ChartPanelLine() :
            base()
        {
        }

        public override void initPanel()
        {
            base.initPanel();
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mouseClickLine);
            State = getState(typeof(StateChartPanelBar));
            State.State = EnumChartPanelState.notInitialized.ToString();
            State.SelectMode = EnumChartPanelSelectMode.isOneSelect.ToString();
        }

        public override void fillChartType(Graphics g, DataTable data)
        {
            base.fillChartType(g, data);

            CollectionDrawer lineDrawer = new CollectionDrawerLine(this);
            lineDrawer.drawCollection(g);
        }

        protected override void addData(DataCollection dc, DataTable data)
        {
            dc.DataSeriesList.Clear();
            DataSeries ds = new DataSeries();

            int size = data.Rows.Count;

            for (int i = 0; i < size; i++) {
                ds.AddPoint(new PointF(data.Rows[i].Field<float>(0), data.Rows[i].Field<float>(1)));
            }
            dc.add(ds);
            if (!DynamicDataSeriesList.ContainsKey(ds.SeriesName)) {
                ds.dd.lineStyle.LineColor = Color.Red;
                ds.dd.lineStyle.Thickness = 2f;
                ds.dd.lineStyle.Pattern = DashStyle.Dash;
                DynamicDataSeriesList.Add(ds.SeriesName, ds.dd);
            } else {
                ds.dd = (DynamicDataSeries)DynamicDataSeriesList[ds.SeriesName];
            }
        }

        protected void mouseClickLine(object sender, MouseEventArgs e)
        {
            State.State = EnumChartPanelState.isSelected.ToString();
            State.GlobalMode = EnumChartPanelGlobalMode.select.ToString();

            MouseEventArgs me = e as MouseEventArgs;

            //if (e.Button == MouseButtons.Left) {
            //    foreach (ZoneExecutorDrawSeries zoneExecutorDrawSeries in OverwriteDataComponents.CollectionDrawerOverwrite.ZoneExecutorSeriesList) {
            //        zoneExecutorDrawSeries.executeClick(sender, me);
            //    }
            //}

            //SelectedOptionPanelWrapper selectedOptionPanelWrapper = new SelectedOptionPanelWrapper();
            //selectedOptionPanelWrapper = Inst.getBuilder().getInitializedSelectedOptionPanelWrapperOneTime(selectedOptionPanelWrapper, this);
            DataTablePanelWrapper dataTablePanelWrapper = new DataTablePanelWrapper();
            dataTablePanelWrapper = Inst.getBuilder().getBuiltDataTablePanelWrapperOneTime(dataTablePanelWrapper, this);
            if (e.Button == MouseButtons.Right) {
                dataTablePanelWrapper.show();
                //selectedOptionPanelWrapper.show();
            } else {
                dataTablePanelWrapper.hide();
                //selectedOptionPanelWrapper.hide();
            }

            //this.overrideSelectedBar();
        }
    }
}