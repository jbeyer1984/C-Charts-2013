using Charts.Chart.CacheFolder.CacheInterfaces;
using Charts.Chart.Identifier;
using Charts.Factories;
using System.Drawing;
using System;

namespace Charts
{
    public abstract class CollectionDrawer : IIdentifier, ICacheAble
    {
        private ChartPanel chartPanel;
        private string identifier;
        private bool isAlreadyCreated;

        public CollectionDrawer()
        {
        }

        public CollectionDrawer(ChartPanel chartPanel)
        {
            this.chartPanel = chartPanel;
        }

        public abstract void init();

        public abstract void drawCollection(Graphics g);

        public ChartPanel ChartPanel
        {
            get { return chartPanel; }
            set { chartPanel = value; }
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