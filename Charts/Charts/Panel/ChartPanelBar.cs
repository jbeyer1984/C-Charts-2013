using System;
using System.Data;
using System.Drawing;

namespace Charts
{
    public class ChartPanelBar : ChartPanel
    {
        public ChartPanelBar(PanelMatrix panelMatrix) :
            base(panelMatrix)
        {
        }

        /// <summary>
        /// add data to data collection
        /// </summary>
        /// <param name="dc"></param>
        /// <param name="data"></param>
        public override void addData(DataCollection dc, DataTable data)
        {
            DataSeries ds = new DataSeries();
            ds = new DataSeries();
            //ds.BarStyle.BorderColor = Color.Red;
            //ds.BarStyle.BorderColor = Color.Green;

            int size = data.Rows.Count;

            for (int i = 0; i < size; i++) {
                ds.AddPoint(new PointF(data.Rows[i].Field<float>(0), data.Rows[i].Field<float>(1)));
            }
            dc.add(ds);

            if (!dynamicDataSeriesList.ContainsKey(ds.SeriesName)) {
                Console.WriteLine(ds.SeriesName);
                ds.BarStyle.dd.BorderColor = Color.Transparent;
                ds.BarStyle.dd.FillColor = Color.Green;
                ds.BarStyle.dd.BarWidth = 0.6f;
                dynamicDataSeriesList.Add(ds.SeriesName, ds.BarStyle.dd);
            } else {
                ds.BarStyle.dd = (DynamicDataBar)dynamicDataSeriesList[ds.SeriesName];
            }
        }

        /// <summary>
        /// add bars to data collection
        /// </summary>
        /// <param name="g"></param>
        /// <param name="data"></param>
        protected override void fillChartType(Graphics g, DataTable data)
        {
            this.addData(DataCollection, data);
            this.setPlotArea();
            if (null == AlternativeChartPanel) {
                ChartStyle.AddChartPlot(g);
            }
            //ChartStyle.AddChartPlot(g);
            DataCollection.addBars(g, ChartStyle, 1, 4);
            Legend.AddLegend(g, DataCollection, ChartStyle);
        }
    }
}