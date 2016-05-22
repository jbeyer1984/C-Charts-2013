using System.ComponentModel;

namespace Charts.Chart.StateFolder
{
    internal class StateChartPanelBar : StateAbstract
    {
        public StateChartPanelBar()
        {
            this.PropertyChanged += new PropertyChangedEventHandler(log);
        }
    }
}