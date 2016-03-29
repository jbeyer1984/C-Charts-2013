using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Charts
{
    //[DataContract (Name="dynamicData")]
    public class DynamicDataLegend : DynamicData
    {
        public float spacing;
        public float textHeight;
        public float htextHeight;
        public float lineLength;
        public float hlineLength;

        public float Spacing
        {
            get { return spacing; }
            set { spacing = value; }
        }

        public float TextHeight
        {
            get { return textHeight; }
            set { textHeight = value; }
        }

        public float HtextHeight
        {
            get { return htextHeight; }
            set { htextHeight = value; }
        }

        public float LineLength
        {
            get { return lineLength; }
            set { lineLength = value; }
        }

        public float HlineLength
        {
            get { return hlineLength; }
            set { hlineLength = value; }
        }
    }
}
