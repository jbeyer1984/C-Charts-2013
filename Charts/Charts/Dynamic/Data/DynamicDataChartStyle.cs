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

        public float XLimMin
        {
            get { return xLimMin; }
            set { xLimMin = value;  }
        }

        public float XLimMax
        {
            get { return xLimMax; }
            set { xLimMax = value; }
        }

        public float YLimMin
        {
            get { return yLimMin; }
            set { yLimMin = value; }
        }

        public float YLimMax
        {
            get { return yLimMax; }
            set { yLimMax = value; }
        }


        public float XTickOffset
        {
            get { return xTickOffset; }
            set { xTickOffset = value; }
        }

        public float XTick
        {
            get { return xTick; }
            set { xTick = value; }
        }

        public float YTick
        {
            get { return yTick; }
            set { yTick = value; }
        }


    }
}
