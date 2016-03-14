using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Charts
{
    //[DataContract (Name="dynamicData")]
    public class DynamicDataChartStyle : DynamicData
    {
        public float xLimMin = 0f;
        public float xLimMax = 10f;
        public float yLimMin = 0f;
        public float yLimMax = 10f;

        public float xTick = 1f;
        public float yTick = 0.5f;
    }
}
