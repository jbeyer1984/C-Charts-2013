using Charts.Chart.Identifier;
using System.Drawing;
using System;

namespace Charts
{
    internal abstract class CollectionDrawer : IIdentifier
    {
        private ChartPanel chartPanel;
        private string identifier;

        public CollectionDrawer(ChartPanel chartPanel)
        {
            this.chartPanel = chartPanel;
        }

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
    }
}