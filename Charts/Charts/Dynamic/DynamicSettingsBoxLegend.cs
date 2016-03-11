using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Charts.Dynamic;

namespace Charts
{
    class DynamicSettingsBoxLegend : DynamicSettingsBox
    {
        public DynamicSettingsBoxLegend(Panel panel, ChartForm form)
            : base(panel, form)
        {
        }

        public override String getDynamicSettingsJSONString()
        {
            return ((DynamicDataLegend) base.dynamicData).ToJSON();
        }

        public override void updateChangedDynamicData(object sender, EventArgs e)
        {
            try
            {
                //DynamicDataLegend dd = new DynamicDataLegend();
                //dd = dd.FromJSON(TBox.Text);
                base.dynamicData = ((DynamicDataLegend) base.dynamicData).FromJSON(TBox.Text);
                //Form1 anyForm = (Form1)Application.OpenForms["Form1"];
                Legend legend = FormToUpdate.Legend;
                legend.dd = (DynamicDataLegend) base.dynamicData;
                FormToUpdate.Refresh();

            }
            catch
            {
                Console.WriteLine("Fehler im Update in " + this.ToString());
            }

            //MessageBox.Show(dynamicData.spacing.ToString());
        }

    }
}
