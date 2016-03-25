using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
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
