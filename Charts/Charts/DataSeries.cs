﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Charts
{
    public class DataSeries
    {
        private ArrayList pointList;
        private LineStyle lineStyle;
        private string seriesName = "Default Name";

        public DataSeries()
        {
            lineStyle = new LineStyle();
            pointList = new ArrayList();
        }

        public LineStyle LineStyle
        {
            get { return lineStyle; }
            set { lineStyle = value; }
        }

        public string SeriesName
        {
            get { return seriesName; }
            set { seriesName = value; }
        }

        public ArrayList PointList
        {
            get { return pointList; }
            set { pointList = value; }
        }

        public void AddPoint(PointF pt)
        {
            pointList.Add(pt);
        }
    }
}