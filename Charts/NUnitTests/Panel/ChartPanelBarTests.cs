using NUnit.Framework;
using System.Data;

namespace Charts.Tests
{
    [TestFixture()]
    public class ChartPanelBarTests
    {
        private ChartPanelBarMock chartPanelBarMock;
        private ChartForm form;

        [SetUp()]
        public void setup()
        {
            chartPanelBarMock = new ChartPanelBarMock();
        }

        [TestCase]
        public void addData_DataSeries_IsDataSeriesAddedToColection()
        {
            DataCollection dataCollection = new DataCollection();
            chartPanelBarMock.addDataF(dataCollection, getData());
            chartPanelBarMock.DataCollection = dataCollection;
            Assert.IsInstanceOf<DataSeries>(chartPanelBarMock.DataCollection.DataSeriesList[0]);
            chartPanelBarMock.addDataF(dataCollection, getData());
            Assert.AreEqual(2, chartPanelBarMock.DataCollection.DataSeriesList.Count);
        }

        public virtual DataTable getData()
        {
            DataTable dataBar = new DataTable();
            dataBar.Columns.Add("x", typeof(float));
            dataBar.Columns.Add("y", typeof(float));
            float x = 0;
            float y = 0;
            for (int i = 0; i < 5; i++) {
                x = i + 1;
                y = 1.0f * x;
                dataBar.Rows.Add(x, y);
            }

            return dataBar;
        }

        [Test()]
        public void execute_collectionDrawOverride()
        {
            DataTable data = new DataTable();
            data.Columns.Add("x", typeof(float));
            data.Columns.Add("y", typeof(float));
            for (int i = 0; i < 20; i++) {
                data.Rows.Add(i, 4);
            }
        }

        [Test()]
        public void ChartPanelBarTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void fillChartTypeTest()
        {
            Assert.Fail();
        }
    }

    public class ChartPanelBarMock : ChartPanelBar
    {
        public ChartPanelBarMock()
        {
        }

        public void addDataF(DataCollection dataCollection, DataTable data)
        {
            this.addData(dataCollection, data);
        }
    }
}