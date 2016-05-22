using Charts.Chart.Debug;
using Charts.Chart.StaticCallsFolder;
using Charts.Chart.Zone.ZoneInterfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Charts
{
    public class CollectionDrawerOverwrite : IOverwriteWithExecutor
    {
        private ChartPanel chartPanel;
        private List<ZoneExecutorDrawSeries> zoneExecutorSeriesList = new List<ZoneExecutorDrawSeries>();
        private bool isButtonsCreated;

        private List<GraphicsPath> paths = new List<GraphicsPath>();
        

        public CollectionDrawerOverwrite(ChartPanel chartPanel)
        {
            this.chartPanel = chartPanel;
        }

        //public CollectionDrawerOverwrite(ChartPanel chartPanel)
        //{
        //    this.chartPanel = chartPanel;
        //}

        //public ChartPanel ChartPanel
        //{
        //    get { return chartPanel; }
        //    set { chartPanel = value; }
        //}

        public void markDrawCollection()
        {
            //int counterSeries = 0;
            foreach (ZoneExecutorDrawSeries zoneExecutorDrawSeries in zoneExecutorSeriesList) {
                //counterSeries++;
                //DebugSettings.log(String.Format("$ overwriteDrawCollection $ length of seriesList : {0}", counterSeries));


                DataSeries dataSeries = zoneExecutorDrawSeries.DataSeries;

                //DebugSettings.log(String.Format("+++ state : {0}", chartPanel.State.State));
                if (StaticCall.isChartPanelSelectModeEqual(ChartPanel, EnumChartPanelSelectMode.isOneSelect)
                    && StaticCall.isChartPanelStateEqual(ChartPanel, EnumChartPanelState.isSelected)) { // singleMode
                    //DebugSettings.log(String.Format("$overwriteDrawCollection$ length of selecteZoneBarList : {0}", zoneExecutorDrawSeries.SelectedZoneBarList.Count)); 

                    if (1 < zoneExecutorDrawSeries.SelectedZoneBarList.Count) {
                        zoneExecutorDrawSeries.SelectedZoneBarList.Clear();
                    }
                }

                //int counter = 0;
                foreach (ZoneBarByIndex zoneBarByIndex in zoneExecutorDrawSeries.ZoneBarByIndexList) {
                    if (zoneBarByIndex.Selected) {
                        //counter++;
                        //DebugSettings.log(String.Format("$handleOnClick$ selected ZoneBars : {0}", counter));

                        zoneBarByIndex.Color = Color.Black;
                        zoneBarByIndex.Thickness = 2.0f;
                        this.paintSelection(zoneBarByIndex);
                    } else { // is sected @todo another approach with selections, should be not visible like here in red
                        zoneBarByIndex.Color = Color.Red;
                        zoneBarByIndex.Thickness = 2.0f;
                        this.paintSelection(zoneBarByIndex);
                    }
                }
                //break;
            }
        }

        protected void paintSelection(ZoneBarByIndex zoneBarByIndex)
        {
            Graphics g = chartPanel.CreateGraphics();
            Pen aPen;
            aPen = new Pen(
                zoneBarByIndex.Color,
                zoneBarByIndex.Thickness
            );
            //SolidBrush aBrush;
            //aBrush = new SolidBrush(Color.Red);
            //aBrush = new SolidBrush(zoneBarByIndex.Color);
            //aPen = new Pen(
            //    dataSeries.BarStyle.dd.BorderColor,
            //    dataSeries.BarStyle.dd.BorderThickness
            //);
            //aPen.DashStyle = dataSeries.BarStyle.dd.BorderPattern;
            //PointF[] pts = zoneBarByIndex.AreaToPaint;
            //GraphicsPath path = new GraphicsPath();
            //path.AddLines(pts);
            //paths.Add(path); // @todo remove later
            GraphicsPath path = zoneBarByIndex.Path;
            //path.Widen(aPen);
            //GraphicsPath path2 = new GraphicsPath(path);
            //path2.Widen(aPen);

            g.DrawPath(aPen, path);
            //Console.WriteLine("paint and overwrite plz");
            //g.FillPolygon(aBrush, pts);
            //g.DrawPolygon(aPen, pts);
            //aBrush.Dispose();
            aPen.Dispose();
        }

        public void overwriteSelectedBar(object sender, EventArgs e)
        {
            Graphics g = chartPanel.CreateGraphics();
            //Pen aPen = new Pen(
            //    Color.Yellow,
            //    2.0f
            //);
            SolidBrush aBrush;
            foreach (ZoneExecutorDrawSeries zoneExecutorDrawSeries in chartPanel.OverwriteDataComponents.CollectionDrawerOverwrite.ZoneExecutorSeriesList) {
                foreach (KeyValuePair<ZoneBarByIndex, bool> pair in zoneExecutorDrawSeries.SelectedZoneBarList) {
                    PointF[] rect = pair.Key.AreaToPaint;
                    //Color color = pair.Key.ColorBarFill;
                    Color color = Color.Red;
                    aBrush = new SolidBrush(color);
                    g.FillPolygon(aBrush, rect);
                    //g.DrawPolygon(aPen, rect);
                }
            }
        }

        public void add(IExecutor executor)
        {
            //ZoneExecutorSeriesList.Clear(); // @todo should be another approach
            if (0 == zoneExecutorSeriesList.Count) {
                zoneExecutorSeriesList.Add(executor as ZoneExecutorDrawSeries);
            }
        }

        public List<ZoneExecutorDrawSeries> ZoneExecutorSeriesList
        {
            get { return zoneExecutorSeriesList; }
            set { zoneExecutorSeriesList = value; }
        }

        public bool IsButtonsCreated
        {
            get { return isButtonsCreated; }
            set { isButtonsCreated = value; }
        }

        public ChartPanel ChartPanel
        {
            get { return chartPanel; }
            set { chartPanel = value; }
        }
    }
}