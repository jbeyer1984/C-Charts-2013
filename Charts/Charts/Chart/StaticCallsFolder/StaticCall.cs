using Charts.Chart.Identifier;
using Charts.Factories;
using System;

namespace Charts.Chart.StaticCallsFolder
{
    /// <summary>
    /// provides static calls, but also use with singelton approach
    /// </summary>
    public class StaticCall
    {
        public void initIdentifierOneTime(object obj)
        {
            checkIIdentifier(obj);

            String identifier = ((IIdentifier)obj).Identifier;

            if (!("".Equals(identifier) || null == identifier)) {
                return;
            }

            ((IIdentifier)obj).Identifier = obj.GetType().Name +
                "."
                + Inst.getRegistry().getRegistryCountInstances().getInstanceCountOf(obj)
            ;
        }

        public void checkIIdentifier(object obj)
        {
            if (!(obj is IIdentifier)) { // @todo maybe extract to Checker Class
                throw new Exception(string.Format(
                    "object is not countable, it has no interface IIdentifier, {0}",
                    obj.GetType().Name
                ));
            }
        }

        public static void changeChartPanelState(ChartPanel chartPanel, EnumChartPanelState state)
        {
            chartPanel.getState(chartPanel.GetType()).State = state.ToString();
        }

        public static bool isChartPanelStateEqual(ChartPanel chartPanel, EnumChartPanelState state)
        {
            return chartPanel.State.State.Equals(state.ToString());
        }

        public static void changeChartPanelSelectMode(ChartPanel chartPanel, EnumChartPanelSelectMode state)
        {
            chartPanel.getState(chartPanel.GetType()).State = state.ToString();
        }

        public static bool isChartPanelSelectModeEqual(ChartPanel chartPanel, EnumChartPanelSelectMode state)
        {
            return chartPanel.State.State.Equals(state.ToString());
        }

        public static void changeChartPanelGlobalMode(ChartPanel chartPanel, EnumChartPanelGlobalMode state)
        {
            chartPanel.getState(chartPanel.GetType()).State = state.ToString();
        }

        public static bool isChartPanelGlobalModeEqual(ChartPanel chartPanel, EnumChartPanelGlobalMode state)
        {
            return chartPanel.State.State.Equals(state.ToString());
        }
    }
}