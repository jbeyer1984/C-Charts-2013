using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Charts
{
    public class DataCollection
    {
        private ArrayList dataSeriesList;
        private int dataSeriesIndex = 0;
        public DataCollection()
        {
            dataSeriesList = new ArrayList();
        }
        public ArrayList DataSeriesList
        {
            get { return dataSeriesList; }
            set { dataSeriesList = value; }
        }
        public int DataSeriesIndex
        {
            get { return dataSeriesIndex; }
            set { dataSeriesIndex = value; }
        }
        public void add(DataSeries ds)
        {
            dataSeriesList.Add(ds);
            if (ds.SeriesName == "Default Name")
            {
                ds.SeriesName = "DataSeries" +
                dataSeriesList.Count.ToString();
            }
        }
        public void insert(int dataSeriesIndex, DataSeries ds)
        {
            dataSeriesList.Insert(dataSeriesIndex, ds);
            if (ds.SeriesName == "Default Name")
            {
                dataSeriesIndex = dataSeriesIndex + 1;
                ds.SeriesName = "DataSeries" +
                dataSeriesIndex.ToString();
            }
        }
        public void Remove(string dataSeriesName)
        {
            if (dataSeriesList != null)
            {
                for (int i = 0; i < dataSeriesList.Count; i++)
                {
                    DataSeries ds = (DataSeries)dataSeriesList[i];
                    if (ds.SeriesName == dataSeriesName)
                    {
                        dataSeriesList.RemoveAt(i);
                    }
                }
            }
        }
        public void RemoveAll()
        {
            dataSeriesList.Clear();
        }
        public void AddLines(Graphics g, ChartStyle cs)
        {
            // Plot lines: 
            foreach (DataSeries ds in DataSeriesList)
            {
                if (ds.dd.lineStyle.IsVisible == true)
                {
                    Pen aPen = new Pen(ds.dd.lineStyle.LineColor,
                     ds.dd.lineStyle.Thickness); aPen.DashStyle = ds.dd.lineStyle.Pattern;
                    for (int i = 1; i < ds.PointList.Count; i++)
                    {
                        g.DrawLine(aPen,
                          cs.Point2D((PointF)ds.PointList[i - 1]), cs.Point2D((PointF)ds.PointList[i]));
                    }
                    aPen.Dispose();
                }
            }
        }

        public void addBars(Graphics g, ChartStyle cs, int numberOfDataSeries, int numberOfPoints)
        {
            ArrayList temp = new ArrayList();
            float[] tempy = new float[numberOfPoints];
            int n = 0;
            foreach (DataSeries ds in DataSeriesList)
            {
                Pen aPen = new Pen(ds.BarStyle.BorderColor,
                ds.BarStyle.BorderThickness);
                SolidBrush aBrush = new SolidBrush(ds.BarStyle.FillColor);
                aPen.DashStyle = ds.BarStyle.BorderPattern;
                PointF[] pts = new PointF[4];
                PointF pt;
                float width;

                if (cs.dd.styleType == DynamicDataChartStyle.StyleEnum.Bar)
                {
                    if (numberOfDataSeries == 1)
                    {
                        width = cs.dd.xTick * ds.BarStyle.BarWidth;
                        for (int i = 0; i < ds.PointList.Count; i++)
                        {
                            pt = (PointF)ds.PointList[i];
                            float x = cs.dd.xTickOffset + pt.X - cs.dd.xTick / 2;
                            //Console.WriteLine(" pos{0} : {1}", i, pt.X);
                            pts[0] = cs.Point2D(new PointF(x - width / 2, 0));
                            pts[1] = cs.Point2D(new PointF(x + width / 2, 0));
                            pts[2] = cs.Point2D(new PointF(x + width / 2, pt.Y));
                            pts[3] = cs.Point2D(new PointF(x - width / 2, pt.Y));
                            g.FillPolygon(aBrush, pts);
                            g.DrawPolygon(aPen, pts);
                        }
                    }
                    else if (numberOfDataSeries > 1)
                    {
                        width = 0.7f * cs.dd.xTick;
                        for (int i = 0; i < ds.PointList.Count; i++)
                        {
                            pt = (PointF)ds.PointList[i];
                            Console.WriteLine(" pos{0} : {1}", i, pt.X);
                            float w1 = width / numberOfDataSeries;
                            float w = ds.BarStyle.BarWidth * w1;
                            float space = (w1 - w) / 2;
                            float x = pt.X - cs.dd.xTick / 2;
                            pts[0] = cs.Point2D(new PointF(x - width / 2 + space + n * w1, 0));
                            pts[1] = cs.Point2D(new PointF(x - width / 2 + space + n * w1 + w, 0));
                            pts[2] = cs.Point2D(new PointF(x - width / 2 + space + n * w1 + w, pt.Y));
                            pts[3] = cs.Point2D(new PointF(x - width / 2 + space + n * w1, pt.Y));
                            g.FillPolygon(aBrush, pts);
                            g.DrawPolygon(aPen, pts);
                        }
                    }
                }
                n++;
                aPen.Dispose();
            }
        }
    }
}
