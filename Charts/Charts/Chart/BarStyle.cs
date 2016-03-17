﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Charts
{
    public class BarStyle
    {
        private Color fillColor = Color.Black;
        private Color borderColor = Color.Black;
        private float borderThickness = 1.0f;
        private float barWidth = 0.8f;
        private DashStyle borderPattern = DashStyle.Solid;

        public float BarWidth
        {
            get { return barWidth; }
            set { barWidth = value; }
        }

        virtual public DashStyle BorderPattern
        {
            get { return borderPattern; }
            set { borderPattern = value; }
        }

        public float BorderThickness
        {
            get { return borderThickness; }
            set { borderThickness = value; }
        }

        virtual public Color FillColor
        {
            get { return fillColor; }
            set { fillColor = value; }
        }

        virtual public Color BorderColor
        {
            get { return borderColor; }
            set { borderColor = value; }
        }
    }
}