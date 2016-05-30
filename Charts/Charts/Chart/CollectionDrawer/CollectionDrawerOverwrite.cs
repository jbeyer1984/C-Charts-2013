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
        private List<List<ZoneBarByIndex>> historyOfZones = new List<List<ZoneBarByIndex>>();
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

                //DataSeries dataSeries = zoneExecutorDrawSeries.DataSeries;

                //DebugSettings.log(String.Format("+++ state : {0}", chartPanel.State.State));
                if (StaticCall.isChartPanelSelectModeEqual(ChartPanel, EnumChartPanelSelectMode.isOneSelect)
                    && StaticCall.isChartPanelStateEqual(ChartPanel, EnumChartPanelState.isSelected)) { // singleMode
                    //DebugSettings.log(String.Format("$overwriteDrawCollection$ length of selecteZoneBarList : {0}", zoneExecutorDrawSeries.SelectedZoneBarList.Count));

                    if (1 < zoneExecutorDrawSeries.SelectedZoneBarList.Count) {
                        //zoneExecutorDrawSeries.SelectedZoneBarList.Clear();
                    }
                }

                //int counter = 0;
                //chartPanel.Invalidate();
                //chartPanel.Refresh();

                foreach (ZoneBarByIndex zoneBarByIndex in zoneExecutorDrawSeries.ZoneBarByIndexList) {
                    if (zoneBarByIndex.Selected) {
                        //counter++;
                        //DebugSettings.log(String.Format("$handleOnClick$ selected ZoneBars : {0}", counter));

                        zoneBarByIndex.Color = Color.Black;
                        zoneBarByIndex.Thickness = 2.0f;
                        this.paintSelection(zoneBarByIndex);
                    }
                }
                //break;
            }
        }

        public void clearSelected()
        {
            foreach (ZoneExecutorDrawSeries zoneExecutorDrawSeries in zoneExecutorSeriesList) {
                zoneExecutorDrawSeries.SelectedZoneBarList.Clear();
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
            GraphicsPath path = zoneBarByIndex.Path;

            g.DrawPath(aPen, path);
            aPen.Dispose();
        }

        public void overwriteSelectedBar(object sender, EventArgs e)
        {
            Graphics g = chartPanel.CreateGraphics();
            List<ZoneBarByIndex> list = new List<ZoneBarByIndex>();
            foreach (ZoneExecutorDrawSeries zoneExecutorDrawSeries in chartPanel.OverwriteDataComponents.CollectionDrawerOverwrite.ZoneExecutorSeriesList) {
                foreach (KeyValuePair<ZoneBarByIndex, bool> pair in zoneExecutorDrawSeries.SelectedZoneBarList) {
                    list.Add(pair.Key);
                }
            }

            historyOfZones.Add(list);
        }

        public void fillSelectedBars(Graphics g)
        {
            DebugSettings.log(string.Format("CollectionDrawerOverwrite.fillSelectedBars()"));

            //if (null == g) {
            //    g = chartPanel.CreateGraphics();
            //}
            
            SolidBrush aBrush = new SolidBrush(Color.Blue);

            foreach (List<ZoneBarByIndex> zoneList in historyOfZones) {
                foreach (ZoneBarByIndex zoneBarByIndex in zoneList) {
                    PointF[] rect = zoneBarByIndex.AreaToPaint;
                    Color color = zoneBarByIndex.TempColor;
                    //Color color = Color.Red;
                    aBrush.Color = color;
                    g.FillPolygon(aBrush, rect);
                }
            }

            aBrush.Dispose();

            //g.Dispose();
        }

        public void add(IExecutor executor)
        {
            //ZoneExecutorSeriesList.Clear(); // @todo should be another approach
            if (0 == zoneExecutorSeriesList.Count) {
                zoneExecutorSeriesList.Add(executor as ZoneExecutorDrawSeries);
            }
        }

        public void clear()
        {
            zoneExecutorSeriesList.Clear();
        }

        public List<ZoneExecutorDrawSeries> ZoneExecutorSeriesList
        {
            get { return zoneExecutorSeriesList; }
            set { zoneExecutorSeriesList = value; }
        }

        public List<List<ZoneBarByIndex>> HistoryOfZones
        {
            get { return historyOfZones; }
            set { historyOfZones = value; }
        }

        public bool IsBarsCreated
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