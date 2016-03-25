using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace Charts
{
    //[DataContract (Name="dynamicData")]
    public class DynamicDataChartStyle : DynamicData
    {
        public float xLimMin = 0f;
        public float xLimMax = 10f;
        public float yLimMin = 0f;
        public float yLimMax = 10f;

        public float xTickOffset;
        public float xTick = 1f;
        public float yTick = 0.5f;

        public float test = 0;

        public StyleEnum styleType = StyleEnum.Bar;

        [DataContract]
        public enum StyleEnum
        {
            [EnumMember(Value = "Normal")]
            Normal = 0,
            [EnumMember(Value = "Bar")]
            Bar = 1
        }
    }
}
