using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Charts
{
    /// <summary>
    /// Zone for marking specific regions, now it's used for bar
    /// </summary>
    public class ZoneBarByIndex
    {
        private ChartPanel chartPanel;

        private int index;
        private Color color;
        private Color colorBarFill;
        private Color tempColor;

        private RectangleF areaToClick;
        private PointF[] areaToPaint;
        private bool selected;
        private float thickness;

        private GraphicsPath path;

        public ZoneBarByIndex(ChartPanel chartPanel)
        {
            this.chartPanel = chartPanel;
        }

        /// <summary>
        /// copy constructor, exists because of interferencing different overwritten layers with zones
        /// </summary>
        /// <param name="zoneBarByIndex"></param>
        public ZoneBarByIndex(ZoneBarByIndex zoneBarByIndex)
        {
            this.color = zoneBarByIndex.Color;
            this.colorBarFill = zoneBarByIndex.ColorBarFill;
            this.areaToClick = zoneBarByIndex.AreaToClick;
            this.areaToPaint = zoneBarByIndex.AreaToPaint;
            this.selected = zoneBarByIndex.selected;
            this.thickness = zoneBarByIndex.thickness;
        }

        public void mapExecutor()
        {
            throw new NotImplementedException();
        }

        public ChartPanel ChartPanel
        {
            get { return chartPanel; }
            set { chartPanel = value; }
        }

        public int Index
        {
            get { return index; }
            set { index = value; }
        }

        public PointF[] AreaToPaint
        {
            get { return areaToPaint; }
            set { areaToPaint = value; }
        }

        public RectangleF AreaToClick
        {
            get { return areaToClick; }
            set { areaToClick = value; }
        }

        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

        public Color ColorBarFill
        {
            get { return colorBarFill; }
            set { colorBarFill = value; }
        }

        public Color TempColor
        {
            get { return tempColor; }
            set { tempColor = value; }
        }

        public bool Selected
        {
            get { return selected; }
            set { selected = value; }
        }

        public GraphicsPath Path
        {
            get { return path; }
            set { path = value; }
        }

        public float Thickness
        {
            get { return thickness; }
            set { thickness = value; }
        }
    }
}