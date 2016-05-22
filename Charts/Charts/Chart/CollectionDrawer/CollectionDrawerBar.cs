using Charts.Chart.StaticCallsFolder;
using Charts.Chart.Zone.ZoneInterfaces;
using Charts.Factories;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Charts
{
    internal class CollectionDrawerBar : CollectionDrawer
    {
        private IExecutorByClick currentExecutorByClick;

        public CollectionDrawerBar(ChartPanel chartPanel)
            : base(chartPanel)
        {
        }

        public override void drawCollection(Graphics g)
        {
            //ChartPanel.SuspendLayout();
            this.addBars(g);
            //DebugSettings.log(String.Format("### state : {0}", ChartPanel.State.State));
            //if (null != ChartPanel.OverwriteDataComponents.CollectionDrawerOverwrite
            //    && ChartPanel.State.State.Equals(ChartPanelState.isMarkedSelected.ToString())) {
            //ChartPanel.OverwriteDataComponents.CollectionDrawerOverwrite.markDrawCollection();
            //}
            //ChartPanel.ResumeLayout();
        }

        private void addBars(Graphics g)
        {
            // case if y will be integrated
            //ArrayList temp = new ArrayList();
            //float[] tempy = new float[numberOfPoints];
            //ChartPanel.getState(ChartPanel.GetType()).State = ChartPanelState.invalidate.ToString();
            bool isInvalidate = StaticCall.isChartPanelStateEqual(ChartPanel, EnumChartPanelState.invalidate);
            bool isGlobalChange = StaticCall.isChartPanelGlobalModeEqual(ChartPanel, EnumChartPanelGlobalMode.change);
            if (null != ChartPanel.OverwriteDataComponents.CollectionDrawerOverwrite
                //&& !ChartPanel.OverwriteDataComponents.CollectionDrawerOverwrite.IsButtonsCreated) {
                && !isGlobalChange
                && isInvalidate) {
                //) {
                currentExecutorByClick = Inst.getInstance().getCache().with(this).canBeNew().getByType(typeof(ZoneExecutorDrawSeries)) as ZoneExecutorDrawSeries;
                ChartPanel.OverwriteDataComponents.CollectionDrawerOverwrite.add(currentExecutorByClick);
            } else if (null != ChartPanel.OverwriteDataComponents.CollectionDrawerOverwrite
                && isGlobalChange) {
                // @todo bad hack should be more dynamic
                //return;
                //ChartPanel.OverwriteDataComponents.CollectionDrawerOverwrite.ZoneExecutorSeriesList.Clear();

                Inst.getInstance().getCache().with(this).delete().getByType(typeof(ZoneExecutorDrawSeries));
                currentExecutorByClick = Inst.getInstance().getCache().with(this).canBeNew().getByType(typeof(ZoneExecutorDrawSeries)) as ZoneExecutorDrawSeries;

                //currentExecutorByClick = ChartPanel.OverwriteDataComponents.CollectionDrawerOverwrite.ZoneExecutorSeriesList[0];
                //currentExecutorByClick = Inst.getInstance().getCache().with(this).canBeNew().getByType(typeof(ZoneExecutorDrawSeries)) as ZoneExecutorDrawSeries;
            }

            ChartStyle chartStyle = ChartPanel.ChartStyle;

            int n = 0;

            int countDataSeries = ChartPanel.DataCollection.DataSeriesList.Count;
            foreach (DataSeries ds in ChartPanel.DataCollection.DataSeriesList) {
                Pen aPen = new Pen(ds.BarStyle.dd.BorderColor,
                ds.BarStyle.dd.BorderThickness);
                SolidBrush aBrush = new SolidBrush(ds.BarStyle.dd.FillColor);
                aPen.DashStyle = ds.BarStyle.dd.BorderPattern;
                PointF pt;
                float width;

                if (null != ChartPanel.OverwriteDataComponents.CollectionDrawerOverwrite) {
                    ((ZoneExecutorDrawSeries)currentExecutorByClick).DataSeries = ds;
                }

                if (chartStyle.dd.styleType == DynamicDataChartStyle.StyleEnum.Bar) {
                    if (countDataSeries == 1) {
                        width = chartStyle.dd.xTick * ds.BarStyle.dd.BarWidth;
                        for (int i = 0; i < ds.PointList.Count; i++) {
                            pt = (PointF)ds.PointList[i];
                            float x = chartStyle.dd.xTickOffset + pt.X - chartStyle.dd.xTick / 2;
                            //Console.WriteLine(" pos{0} : {1}", i, pt.X);
                            PointF start = chartStyle.Point2D(new PointF(x - width / 2, 0));
                            //PointF endX = chartStyle.Point2D(new PointF(x + width / 2, 0));
                            //PointF startY = chartStyle.Point2D(new PointF(x - width / 2, pt.Y));
                            PointF end = chartStyle.Point2D(new PointF(x + width / 2, pt.Y));

                            PointF[] pts = new PointF[4];
                            pts[0] = chartStyle.Point2D(new PointF(x - width / 2, 0));
                            pts[1] = chartStyle.Point2D(new PointF(x - width / 2, pt.Y));
                            pts[2] = chartStyle.Point2D(new PointF(x + width / 2, pt.Y));
                            pts[3] = chartStyle.Point2D(new PointF(x + width / 2, 0));
                            //pts[0].X = 180;
                            //pts[0].Y = 100;
                            //pts[1].X = 180;
                            //pts[1].Y = 150;
                            //pts[2].X = 230;
                            //pts[2].Y = 150;
                            //pts[3].X = 230;
                            //pts[3].Y = 100;
                            if (null != ChartPanel.OverwriteDataComponents.CollectionDrawerOverwrite
                                && isInvalidate) {
                                ZoneBarByIndex zoneBarByIndex = new ZoneBarByIndex(ChartPanel);
                                zoneBarByIndex.Index = i;
                                zoneBarByIndex.Color = Color.Yellow;
                                RectangleF areaToClick = new RectangleF(
                                    Math.Min(start.X, end.X),
                                    Math.Min(start.Y, end.Y),
                                    Math.Abs(start.X - end.X),
                                    Math.Abs(start.Y - end.Y)
                                );
                                zoneBarByIndex.AreaToClick = areaToClick;
                                zoneBarByIndex.AreaToPaint = pts;
                                zoneBarByIndex.ColorBarFill = ds.BarStyle.dd.FillColor;
                                GraphicsPath graphicsPath = new GraphicsPath();
                                graphicsPath.AddLines(pts);
                                zoneBarByIndex.Path = graphicsPath;
                                ((ZoneExecutorDrawSeries)currentExecutorByClick).ZoneBarByIndexList.Add(zoneBarByIndex);
                                //button.Tag = zoneBarByIndex;
                                //button.Click += new EventHandler(currentExecutorByClick.executeClick);
                                //button.Left = (int)pts[0].X;
                                //button.Top = 10;
                                //button.Width = 20;
                                //button.Height = 50;
                                //ChartPanel.Controls.Add(button);
                            }
                            g.FillPolygon(aBrush, pts);
                            g.DrawPolygon(aPen, pts);
                        }
                    } else if (countDataSeries > 1) {
                        width = 0.7f * chartStyle.dd.xTick;
                        for (int i = 0; i < ds.PointList.Count; i++) {
                            pt = (PointF)ds.PointList[i];
                            //Console.WriteLine(" pos{0} : {1}", i, pt.X);
                            float w1 = width / countDataSeries;
                            float w = ds.BarStyle.dd.BarWidth * w1;
                            float space = (w1 - w) / 2;
                            float x = pt.X - chartStyle.dd.xTick / 2;
                            PointF[] pts = new PointF[4];
                            pts[0] = chartStyle.Point2D(new PointF(x - width / 2 + space + n * w1, 0));
                            pts[1] = chartStyle.Point2D(new PointF(x - width / 2 + space + n * w1 + w, 0));
                            pts[2] = chartStyle.Point2D(new PointF(x - width / 2 + space + n * w1 + w, pt.Y));
                            pts[3] = chartStyle.Point2D(new PointF(x - width / 2 + space + n * w1, pt.Y));
                            g.FillPolygon(aBrush, pts);
                            g.DrawPolygon(aPen, pts);
                        }
                    }
                }
                n++;
                aPen.Dispose();
            }
            if (null != ChartPanel.OverwriteDataComponents.CollectionDrawerOverwrite) {
                ChartPanel.OverwriteDataComponents.CollectionDrawerOverwrite.IsButtonsCreated = true;
            }
        }

        public IExecutorByClick CurrentExecutorByClick
        {
            get { return currentExecutorByClick; }
            set { currentExecutorByClick = value; }
        }
    }
}