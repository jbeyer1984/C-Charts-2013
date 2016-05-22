//using Newtonsoft.Json;
//using Newtonsoft.Json.Converters;

namespace Charts
{
    public class DynamicDataSeries : DynamicData
    {
        public LineStyle lineStyle;

        public LineStyle LineStyle
        {
            get { return lineStyle; }
            set { lineStyle = value; }
        }
    }
}