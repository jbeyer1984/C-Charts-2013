using Charts;
using NUnit.Framework;

//using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Forms;

namespace NUnitTests
{
    [TestFixture]
    public class DynamicSettingsFormTests
    {
        private TestContext tci;
        private ChartForm chartForm;
        private PanelMatrix panelMatrix;
        private DynamicSettingsForm form;

        [SetUp]
        public void setup()
        {
            //chartForm = new ChartForm();
            //panelMatrix = new PanelMatrix(chartForm);
            //ChartPanel chartPanel = new ChartPanel(panelMatrix);
            form = new DynamicSettingsForm();
        }

        [Test]
        public void width_and_height_of_Grid()
        {
            // this value is set hard in class, not a good approach
            form.DivisorHorizontal = 3; // number of panels in row
            // this value is set hard in class, not a good approach
            form.DivisorVertical = 2; // number of panels in column
            Assert.AreEqual(form.GridWidth, ((form.ClientRectangle.Width - form.SpaceWidth) / form.DivisorHorizontal));
            Assert.AreEqual(form.GridHeight, (form.ClientRectangle.Height / form.DivisorVertical));
            Assert.AreEqual(0, form.PosX);
            Assert.AreEqual(0, form.PosY);
        }

        [Test]
        public void positons_after_addPanel()
        {
            nTimesAddPanelInRow(form, form.DivisorHorizontal, 0);
            nTimesAddPanelInRow(form, form.DivisorHorizontal, 1);
        }

        private void nTimesAddPanelInRow(DynamicSettingsForm form, int n, int row)
        {
            Panel gridPanel = new Panel();
            gridPanel.Name = "GridPanel";

            // form is at posX = 0, 1. Panel in row
            // in row every yPos is same
            for (int i = 0; i < n; i++) {
                form.addPanel(gridPanel);
                Assert.AreEqual(i * form.GridWidth, form.PosX);
                Assert.AreEqual(row * form.GridHeight, form.PosY);
            }
        }

        public TestContext Tci
        {
            get
            {
                return tci;
            }
            set
            {
                tci = value;
            }
        }
    }

    //public class TestClass
    //{
    //    [Test]
    //    public void TestMethod()
    //    {
    //        // TODO: Add your test code here
    //        Assert.Pass("Your first passing test");
    //    }
    //}
}