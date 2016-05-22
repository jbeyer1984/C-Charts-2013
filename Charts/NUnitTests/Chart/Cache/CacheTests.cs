using NUnit.Framework;
using Charts.Chart.CacheFolder.CacheInterfaces;
using Charts.Chart.CacheFolder;
using Charts.Chart.ConnectorFolder;

namespace Charts.Tests
{
    [TestFixture()]
    public class CacheTests
    {
        private Cache cache;

        [SetUp()]
        public void setUp()
        {
            cache = new Cache(new Connector());
        }

        [Test()]
        public void getByTypeTest_Connector_returnedValueExistsAsInstance()
        {
            DynamicSettingsForm dynamicSettingsForm = new DynamicSettingsForm();
            ChartForm chartForm = new ChartForm();

            object returnedChartForm = cache.cache(dynamicSettingsForm).canBeNew().getByType(typeof(ChartForm));
            Assert.IsInstanceOf(chartForm.GetType(), returnedChartForm);
            object returnedChartPanel = cache.cache(dynamicSettingsForm).canBeNull().getByType(typeof(ChartPanelBar));
            Assert.IsNull(returnedChartPanel);
            Assert.IsInstanceOf(chartForm.GetType(), returnedChartForm);
        }

        [Test()]
        public void deleteTest_Connector_ExpectObjectEmpty()
        {
            ChartPanelBar chartPanel = new ChartPanelBar();
            DynamicPanel dynamicPanel = new DynamicPanel();

            DynamicPanel dynamicPanelNew = cache.cache(chartPanel).canBeNew().getByType(dynamicPanel.GetType()) as DynamicPanel;
            Assert.IsNotNull(dynamicPanelNew);
            cache.cache(chartPanel).delete().getByType(dynamicPanelNew.GetType());
            dynamicPanelNew = this.cache.cache(chartPanel).canBeNull().getByType(dynamicPanel.GetType()) as DynamicPanel;
            Assert.IsNull(dynamicPanelNew);
        }
    }
}