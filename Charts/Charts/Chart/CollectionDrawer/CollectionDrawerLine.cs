using System.Drawing;

namespace Charts
{
    internal class CollectionDrawerLine : CollectionDrawer
    {
        public CollectionDrawerLine(ChartPanel chartPanel)
            : base(chartPanel)
        {
        }

        public override void drawCollection(Graphics g)
        {
            this.drawLines(g);
        }

        private void drawLines(Graphics g)
        {
            ChartStyle chartStyle = ChartPanel.ChartStyle;

            // Plot lines:
            foreach (DataSeries ds in ChartPanel.DataCollection.DataSeriesList) {
                if (ds.dd.lineStyle.IsVisible == true) {
                    Pen aPen = new Pen(ds.dd.lineStyle.LineColor,
                     ds.dd.lineStyle.Thickness); aPen.DashStyle = ds.dd.lineStyle.Pattern;
                    for (int i = 1; i < ds.PointList.Count; i++) {
                        g.DrawLine(aPen,
                          chartStyle.Point2D((PointF)ds.PointList[i - 1]), chartStyle.Point2D((PointF)ds.PointList[i]));
                    }
                    aPen.Dispose();
                }
            }
        }
    }
}