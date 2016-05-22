using NUnit.Framework;
using Charts.Chart.ConnectorFolder;
using Charts.Chart.Debug;
using System;
using System.Windows.Forms;
using Charts.Chart.StateFolder;

namespace Charts.Tests
{
    [TestFixture()]
    public class ConnectorTests
    {
        Connector connector;

        [SetUp()]
        public void setUp()
        {
            DebugSettings.debug = false;
            connector = new Connector();
        }

        [Test()]
        public void checkIIdentifierTest_Connector_ExpectException()
        {
            Panel panel = new Panel();

            Assert.Throws<Exception>(() => connector.connect(panel));
            Assert.That(
                () => connector.connect(panel),
                Throws.TypeOf<Exception>()
                    .With.Message.Contains("has not interface IIdentifier")
            );
        }

        [Test()]
        public void connectTest_Connector_WithTempExistsAsInstance()
        {
            DynamicSettingsForm dynamicSettingsForm = new DynamicSettingsForm();
            ChartPanel bar = new ChartPanelBar();

            connector.connect(dynamicSettingsForm);

            Assert.IsNotNull(connector.WithTemp);
            Assert.IsInstanceOf(dynamicSettingsForm.GetType(), connector.WithTemp);
        }

        [Test()]
        public void byTest_Connector_ExpectException()
        {
            ChartForm chartForm = new ChartForm();

            Assert.Throws<Exception>(() => connector.by(chartForm));
            Assert.That(
                () => connector.by(chartForm),
                Throws.TypeOf<Exception>()
                    .With.Message.Contain("WithTemp is NULL!")
            );
        }

        [Test()]
        public void byTest_Connector_ByTempExistsAsInstance()
        {
            DynamicSettingsForm dynamicSettingsForm = new DynamicSettingsForm();
            ChartForm chartForm = new ChartForm();

            connector.connect(dynamicSettingsForm).by(chartForm);

            Assert.IsNotNull(connector.ByTemp);
            Assert.IsInstanceOf(chartForm.GetType(), connector.ByTemp);
        }

        [Test()]
        public void withTest_Connector_WithTempExistsAsInstance()
        {
            DynamicSettingsForm dynamicSettingsForm = new DynamicSettingsForm();
            ChartForm chartForm = new ChartForm();

            connector.connect(dynamicSettingsForm).by(chartForm);
            connector.with(dynamicSettingsForm);

            Assert.IsNotNull(connector.WithTemp);
            Assert.IsInstanceOf(dynamicSettingsForm.GetType(), connector.WithTemp);
        }

        [Test()]
        public void getByTypeTest_Connector_ValueInDicitionaryIsSpecificInstance()
        {
            DynamicSettingsForm dynamicSettingsForm = new DynamicSettingsForm();
            ChartForm chartForm = new ChartForm();

            connector.connect(dynamicSettingsForm).by(chartForm);

            Assert.IsInstanceOf(chartForm.GetType(), connector.with(dynamicSettingsForm).getByType(typeof(ChartForm)));
        }

        [Test()]
        public void getByTypeTest_Connector_Exptect2DifferentEntriesInDictionary()
        {
            DynamicSettingsForm dynamicSettingsForm = new DynamicSettingsForm();
            DynamicSettingsForm dynamicSettingsForm2 = new DynamicSettingsForm();
            ChartForm chartForm = new ChartForm();

            connector.connect(dynamicSettingsForm).by(chartForm);
            connector.connect(dynamicSettingsForm2).by(chartForm);

            Assert.AreSame(chartForm, connector.with(dynamicSettingsForm).getByType(typeof(ChartForm)));
            Assert.AreSame(chartForm, connector.with(dynamicSettingsForm2).getByType(typeof(ChartForm)));
        }

        [Test()]
        public void forceCreationTest_Connector_With2DifferenInstances()
        {
            ChartPanelBar bar = new ChartPanelBar();
            ChartPanelBar bar2 = new ChartPanelBar();

            DynamicPanel panel1 = new DynamicPanel();
            DynamicPanel panel2 = new DynamicPanel();
            this.connector.connect(bar).forceCreation().getByType(typeof(DynamicPanel));
            this.connector.connect(bar2).forceCreation().getByType(typeof(DynamicPanel));
        }

        [Test()]
        public void removeTest_Connector_ExpectObjectEmpty()
        {
            ChartPanelBar chartPanel = new ChartPanelBar();
            DynamicPanel dynamicPanel = new DynamicPanel();

            this.connector.connect(chartPanel).by(dynamicPanel);
            DynamicPanel dynamicPanelSame = this.connector.with(chartPanel).getByType(dynamicPanel.GetType()) as DynamicPanel;
            Assert.IsNotNull(dynamicPanelSame);
            Assert.AreSame(dynamicPanel, dynamicPanelSame);
            this.connector.with(chartPanel).remove().getByType(dynamicPanelSame.GetType());
            dynamicPanelSame = this.connector.with(chartPanel).canBeNull().getByType(dynamicPanel.GetType()) as DynamicPanel;
            Assert.IsNull(dynamicPanelSame);
        }
    }
}