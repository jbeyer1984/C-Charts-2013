using Charts.Chart.CacheFolder.CacheInterfaces;
using Charts.Chart.Debug;
using Charts.Chart.Identifier;
using Charts.Chart.StaticCallsFolder;
using Charts.Chart.Zone.ZoneInterfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Charts
{
    public class ZoneExecutorDrawSeries : IExecutorByClick, IIdentifier, ICacheAble
    {
        private DataSeries dataSeries;
        private List<ZoneBarByIndex> zoneBarByIndexList = new List<ZoneBarByIndex>();
        private int lastPrintedIndex;
        private Dictionary<ZoneBarByIndex, bool> selectedZoneBarList = new Dictionary<ZoneBarByIndex, bool>();
        private string identifier;
        private bool isAlreadyCreated;

        public void executeClick(object sender, MouseEventArgs e)
        {
            PointF currentPoint = new PointF(e.X, e.Y);
            lastPrintedIndex = 0;
            //ZoneBarByIndex zoneBarByIndex = zoneBarByIndexList[i];

            ChartPanel chartPanel = sender as ChartPanel;

            Boolean found = false;
            foreach (ZoneBarByIndex zoneBarByIndex in zoneBarByIndexList) { // flow: go through all ZoneBars
                if (zoneBarByIndex.Path.GetBounds().Contains(currentPoint)) {

                    this.selectZoneByOneClick(zoneBarByIndex);

                    //DebugSettings.log(String.Format("-- in contain -- index: {0}, selected: {1}", zoneBarByIndex.Index, zoneBarByIndex.Selected));

                    found = true;
                } else {
                    zoneBarByIndex.Selected = false;
                    //DebugSettings.log(String.Format("index: {0}, selected: {1}", zoneBarByIndex.Index, zoneBarByIndex.Selected));
                }
                lastPrintedIndex++;
            }

            if (found) {
                StaticCall.changeChartPanelState(chartPanel, EnumChartPanelState.isMarkedSelected);
                chartPanel.OverwriteDataComponents.CollectionDrawerOverwrite.markDrawCollection();
            }

            DebugSettings.log("*** after executeClick in Zone Executor ***");
        }

        protected void selectZoneByOneClick(ZoneBarByIndex zoneBarByIndex)
        {
            if (selectedZoneBarList.ContainsKey(zoneBarByIndex)) {
                selectedZoneBarList.Remove(zoneBarByIndex);
                zoneBarByIndex.Selected = false;
            } else {
                selectedZoneBarList.Clear();
                zoneBarByIndex.Selected = true;
                selectedZoneBarList.Add(zoneBarByIndex, true);
            }
        }

        public DataSeries DataSeries
        {
            get { return dataSeries; }
            set { dataSeries = value; }
        }

        public List<ZoneBarByIndex> ZoneBarByIndexList
        {
            get { return zoneBarByIndexList; }
            set { zoneBarByIndexList = value; }
        }

        public Dictionary<ZoneBarByIndex, bool> SelectedZoneBarList
        {
            get { return selectedZoneBarList; }
            set { selectedZoneBarList = value; }
        }

        public int LastPrintedIndex
        {
            get { return lastPrintedIndex; }
            set { lastPrintedIndex = value; }
        }

        public string Identifier
        {
            get { return identifier; }
            set { identifier = value; }
        }

        public bool IsAlreadyCreated
        {
            get { return isAlreadyCreated; }
            set { isAlreadyCreated = value; }
        }
    }
}