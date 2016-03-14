using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Charts.Dynamic;

namespace Charts
{
    class DynamicSettingsMapperChartStyle : DynamicSettingsMapper
    {
        public DynamicSettingsMapperChartStyle(Panel panel, ChartForm form)
            : base(panel, form)
        {
        }

        public override void mapDynamicData()
        {
            dynamicData = FormToUpdate.ChartStyle.dd;
        }

        public override String getDynamicSettingsJSONString()
        {
            return ((DynamicDataChartStyle) base.dynamicData).ToJSON();
        }

        public override void updateChangedDynamicData(object sender, EventArgs e)
        {
            try
            {
                base.dynamicData = ((DynamicDataChartStyle) base.dynamicData).FromJSON(TBox.Text);
                ChartStyle chartStyle = FormToUpdate.ChartStyle;
                chartStyle.dd = (DynamicDataChartStyle) base.dynamicData;
                FormToUpdate.Refresh();

            }
            catch (System.Runtime.Serialization.SerializationException ex)
            {
                Console.WriteLine("Fehler in Serialization in " + this.ToString());
            }
        }

    }
}
