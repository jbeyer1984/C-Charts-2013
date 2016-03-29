using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Drawing;
using System.Drawing.Drawing2D;
//using Newtonsoft.Json;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Converters;

namespace Charts
{
    public class DynamicDataBar : DynamicData
    {
        public float barWidth = 0.8f;

        public Color borderColor = Color.Black;

        public float borderThickness = 1.0f;

        public Color fillColor = Color.Black;

        public DashStyle borderPattern = DashStyle.Solid;

        public Color FillColor
        {
            get { return fillColor; }
            set { fillColor = value; }
        }

        public Color BorderColor
        {
            get { return borderColor; }
            set { borderColor = value; }
        }

        public float BorderThickness
        {
            get { return borderThickness; }
            set { borderThickness = value; }
        }

        public float BarWidth
        {
            get { return barWidth; }
            set { barWidth = value; }
        }

        public DashStyle BorderPattern
        {
            get { return borderPattern; }
            set { borderPattern = value; }
        }
    }
}
