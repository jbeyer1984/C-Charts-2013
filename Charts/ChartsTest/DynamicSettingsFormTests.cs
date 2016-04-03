using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Charts;
using System.Windows.Forms;

namespace ChartsTest
{
    [TestClass]
    public class DynamicSettingsFormTests
    {
        private TestContext tci;

        [TestMethod]
        public void addPanel()
        {
            ChartForm chartForm = new ChartForm();
            PanelMatrix panelMatrix = new PanelMatrix(chartForm);
            ChartPanel chartPanel = new ChartPanel(panelMatrix);
            DynamicSettingsForm form = new DynamicSettingsForm();
            
            // this value is set hard in class, not a good approach
            form.DivisorHorizontal = 3;
            // this value is set hard in class, not a good approach
            form.DivisorVertical = 2;
            Assert.AreEqual(form.GridWidth, ((form.ClientRectangle.Width - 40) / form.DivisorHorizontal));
            Assert.AreEqual(form.GridHeight, (form.ClientRectangle.Height / form.DivisorVertical));
            Assert.AreEqual(0, form.PosX);
            Assert.AreEqual(0, form.PosY);

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
}
