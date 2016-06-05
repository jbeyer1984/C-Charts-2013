using Charts.Chart.ConnectorFolder;
using Charts.Chart.Identifier;
using Charts.Chart.StaticCallsFolder;
using Charts.Factories;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Charts
{
    public partial class SelectedOptionPanel : System.Windows.Forms.Panel, IIdentifier
    {
        private string identifier;
        private DynamicDataSeries dynamicDataSeries;
        private ChartPanelBar chartPanelBar;
        private SolidBrush aBrush;

        public SelectedOptionPanel() :
            base()
        {
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            ResizeRedraw = true;
        }

        public void init()
        {
            this.Size = this.Size;
            this.Top = this.Location.Y + this.Size.Height - 100;
            this.BackColor = Color.Red;
            Button button = new Button();
            button.Text = "fill color";
            button.Size = new Size(200, 20);
            button.Top = 0;
            chartPanelBar = getConnector().with(this).getByType(typeof(ChartPanelBar)) as ChartPanelBar;
            button.Click += new System.EventHandler(
                //chartPanelBar.OverwriteDataComponents.CollectionDrawerOverwrite.overwriteSelectedBar
                paintSettingsToZone
            );
            this.Controls.Add(button);

            this.initDynamicDataSeries();

            MapperView mapperViewWithLabel = new MapperViewWithLabel();
            DynamicMapperBinds dynamicMapperBinds = new DynamicMapperBinds(
                (null),
                dynamicDataSeries,
                mapperViewWithLabel
            );
            //dynamicMapperBinds.bind(panelSeries);
            dynamicMapperBinds.bind(this);
        }

        protected void initDynamicDataSeries()
        {
            dynamicDataSeries = new DynamicDataSeries();
            dynamicDataSeries.lineStyle = new LineStyle();
            dynamicDataSeries.lineStyle.LineColor = Color.Red;
            dynamicDataSeries.lineStyle.Thickness = 2f;
            dynamicDataSeries.lineStyle.Pattern = DashStyle.Dash;
        }

        public void paintSettingsToZone(object sender, EventArgs e) // @todo function should be else where
        {
            if (0 == chartPanelBar.OverwriteDataComponents.CollectionDrawerOverwrite.ZoneExecutorSeriesList[0].SelectedZoneBarList.Count) {
                return;
            }

            Graphics g = chartPanelBar.CreateGraphics();

            Color color = dynamicDataSeries.LineStyle.LineColor;
            aBrush = new SolidBrush(color);
            PointF[] rect = null;
            foreach (KeyValuePair<ZoneBarByIndex, bool> pair in chartPanelBar.OverwriteDataComponents.CollectionDrawerOverwrite.ZoneExecutorSeriesList[0].SelectedZoneBarList) {
                chartPanelBar.OverwriteDataComponents.CollectionDrawerOverwrite.overwriteSelectedBar(sender, e);

                rect = pair.Key.AreaToPaint;
                pair.Key.TempColor = color;
                g.FillPolygon(aBrush, rect);
            }
            g.Dispose();

            StaticCall.changeChartPanelGlobalMode(chartPanelBar, EnumChartPanelGlobalMode.append);
        }

        protected Connector getConnector()
        {
            return Inst.getInstance().getConnector();
        }

        public string Identifier
        {
            get { return identifier; }
            set { identifier = value; }
        }
    }
}