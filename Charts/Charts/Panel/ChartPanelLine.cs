using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Charts
{
    public class ChartPanelLine : ChartPanel
    {
        public ChartPanelLine(PanelMatrix panelMatrix) :
            base(panelMatrix)
        {
        }

        public override void addData(DataCollection dc, DataTable data)
        {
            dc.DataSeriesList.Clear();
            DataSeries ds = new DataSeries();

            //ds1.dd.lineStyle.LineColor = Color.Red;
            //ds1.dd.lineStyle.Thickness = 2f;
            //ds1.dd.lineStyle.Pattern = DashStyle.Dash;
            //int size = 0;
            int size = data.Rows.Count;

            for (int i = 0; i < size; i++) {
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

        protected override void fillChartType(Graphics g, DataTable data)
        {
            // add lines to data collection
            this.addData(DataCollection, data);
            this.setPlotArea();
            if (null == AlternativeChartPanel) {
                ChartStyle.AddChartPlot(g);
            }
            DataCollection.AddLines(g, ChartStyle);
        }
    }
}