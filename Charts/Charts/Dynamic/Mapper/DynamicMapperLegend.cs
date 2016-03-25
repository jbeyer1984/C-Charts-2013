using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Charts.Dynamic;

namespace Charts
{
    class DynamicMapperLegend : DynamicMapper
    {
        public DynamicMapperLegend(Panel panel, ChartPanel panelToUpdate)
            : base(panel, panelToUpdate)
        {
        }

        public override void mapDynamicData()
        {
            dynamicData = PanelToUpdate.Legend.dd;
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
                Legend legend = PanelToUpdate.Legend;
                legend.dd = (DynamicDataLegend) base.dynamicData;
                PanelToUpdate.Refresh();

            }
            catch
            {
                Console.WriteLine("Fehler im Update in " + this.ToString());
            }
        }

    }
}
