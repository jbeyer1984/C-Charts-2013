using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Charts.Dynamic;

namespace Charts
{
    class DynamicSettingsMapperLegend : DynamicSettingsMapper
    {
        public DynamicSettingsMapperLegend(Panel panel, ChartForm form)
            : base(panel, form)
        {
        }

        public override void mapDynamicData()
        {
            dynamicData = FormToUpdate.Legend.dd;
        }

        public override String getDynamicSettingsJSONString()
        {
            return ((DynamicDataLegend) base.dynamicData).ToJSON();
        }

        public override void updateChangedDynamicData(object sender, EventArgs e)
        {
            try
            {
                base.dynamicData = ((DynamicDataLegend) base.dynamicData).FromJSON(TBox.Text);
                Legend legend = FormToUpdate.Legend;
                legend.dd = (DynamicDataLegend) base.dynamicData;
                FormToUpdate.Refresh();

            }
            catch
            {
                Console.WriteLine("Fehler im Update in " + this.ToString());
            }
        }

    }
}
