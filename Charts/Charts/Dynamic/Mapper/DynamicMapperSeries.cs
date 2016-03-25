using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Charts.Dynamic;
using System.Runtime.Serialization;
using System.Reflection;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Charts
{
    class DynamicMapperSeries : DynamicMapper
    {
        private LineStylePatternBox lineStyleComboBox;

        public DynamicMapperSeries(Panel panel, ChartPanel panelToUpdate)
            : base(panel, panelToUpdate)
        {
        }

        public DynamicMapperSeries(Panel panel, ChartPanel panelToUpdate, String key)
            : base(panel, panelToUpdate, key)
        {
        }

        protected override void initPanel(Panel parentPanel)
        {
            LabelHeight = 15;

            this.ParentPanel = parentPanel;

            this.mapDynamicData();

            initLabel();
            //initDynamicDataProperties();
        }

        protected void initDynamicDataProperties()
        {
            if (0 > this.ParentPanel.Controls.IndexOf(Label))
            {
                this.ParentPanel.Controls.Add(Label);
                lineStyleComboBox = new LineStylePatternBox();
                lineStyleComboBox.Top = LabelHeight;
                lineStyleComboBox.SelectedIndexChanged += new System.EventHandler(updateChangedDynamicData);

                this.ParentPanel.Controls.Add(lineStyleComboBox);
                //this.ParentPanel.Controls.Add(tBox);
            }
        }

        public override void addDynamicSettingsBox(Graphics g)
        {
            this.initDynamicDataProperties();
        }

        public override void mapDynamicData()
        {
            this.dynamicData = (DynamicDataSeries) PanelToUpdate.dynamicDataList[this.key];
        }

        //public override String getDynamicSettingsJSONString()
        //{
        //    //return ((DynamicDataSeries) base.dynamicData).ToJSON();
        //    return "";
        //}

        public override void updateChangedDynamicData(object sender, EventArgs e)
        {
            try
            {
                ((DynamicDataSeries)base.dynamicData).lineStyle.Pattern = (DashStyle)Enum.Parse(typeof(DashStyle), (String) lineStyleComboBox.SelectedItem) ;
                //ChartStyle chartStyle = PanelToUpdate.ChartStyle;
                PanelToUpdate.dynamicDataList[this.key] = (DynamicDataSeries) base.dynamicData;
                PanelToUpdate.Invalidate();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Fehler in Updating in " + this.ToString());
            }
        }

    }
}
