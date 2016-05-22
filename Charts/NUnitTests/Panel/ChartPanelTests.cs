using NUnit.Framework;
using System;
using System.Data;

namespace Charts.Tests
{
    [TestFixture()]
    internal class ChartPanelTests
    {
        public ChartPanelInstance chartPanel;

        [SetUp()]
        public void setup()
        {
            chartPanel = new ChartPanelInstance();
        }

        [TestCase]
        public void initDataToPlot_ChartPanel_IsDataCollectionCreated()
        {
            Assert.IsNull(chartPanel.DataCollection);
            chartPanel.initDataToPlot();
            Assert.IsInstanceOf(typeof(DataCollection), chartPanel.DataCollection);
        }

        [TestCase]
        public void initDataToPlot_ChartPanel_IsChartStyleCreated()
        {
            Assert.IsNull(chartPanel.ChartStyle);
            chartPanel.initDataToPlot();
            Assert.IsInstanceOf(typeof(ChartStyle), chartPanel.ChartStyle);
        }

        [TestCase]
        public void setPlotArea_ChartPanel_IsChartStylePlotRectAreaCreated()
        {
            // @todo not good test harness
            chartPanel.ChartStyle = new ChartStyle(chartPanel);
            Assert.AreEqual(chartPanel.ChartStyle.PlotArea.X, chartPanel.ClientRectangle.X);
            chartPanel.setPlotAreaF();
            Assert.AreNotEqual(chartPanel.ChartStyle.PlotArea.X, chartPanel.ClientRectangle.X);
        }
    }

    public class ChartPanelInstance : ChartPanel
    {
        public ChartPanelInstance()
        {
        }

        protected override void addData(DataCollection dc, DataTable data)
        {
            throw new NotImplementedException();
        }

        public void setPlotAreaF()
        {
            this.setPlotArea();
        }
    }
}