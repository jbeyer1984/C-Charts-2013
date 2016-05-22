using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charts.Chart.StateFolder
{
    class StateChartPanelBar : StateAbstract
    {
        public StateChartPanelBar()
        {
            this.PropertyChanged += new PropertyChangedEventHandler(log);
        }
    }
}
