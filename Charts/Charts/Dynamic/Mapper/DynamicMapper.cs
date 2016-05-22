using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Windows.Forms;
using System.Collections;

namespace Charts
{
    internal abstract class DynamicMapper
    {
        private Panel panelToUpdate;

        private ArrayList panelsToUpdate = new ArrayList();

        private DynamicData dynamicData;

        private MapperView view;

        protected void bindDecimal<T>(PropertyInfo property)
        {
            TextBox decimalBox = new TextBox();
            decimalBox.Name = property.Name;
            //decimalBox.DecimalPlaces = 2;
            T value = (T)property.GetValue(dynamicData, null);
            decimalBox.Text = (string)Convert.ChangeType(value, typeof(string));

            decimalBox.TextChanged += delegate (object sender, EventArgs e)
            { handleDecimalBox<T>(sender, e, property, decimalBox); };

            this.view.addControl(decimalBox);
        }

        protected void bindColor(PropertyInfo property)
        {
            Color color = (Color)property.GetValue(dynamicData, null);
            ColorBox lineStyleColorBox = new ColorBox();
            lineStyleColorBox.Name = property.Name;
            lineStyleColorBox.SelectedIndex = lineStyleColorBox.Items.IndexOf(color.ToKnownColor().ToString());

            lineStyleColorBox.SelectedIndexChanged += delegate (object sender, EventArgs e)
            { handleColorBox(sender, e, property, lineStyleColorBox); };

            this.view.addControl(lineStyleColorBox);
        }

        protected void bindLineStyle(PropertyInfo property)
        {
            LineStyle lineStyle = (LineStyle)property.GetValue(dynamicData, null);

            LineStylePatternBox lineStylePatternBox = new LineStylePatternBox();
            lineStylePatternBox.Name = "pattern";
            lineStylePatternBox.SelectedIndex = (int)lineStyle.Pattern;

            lineStylePatternBox.SelectedIndexChanged += delegate (object sender, EventArgs e)
            { handleLineStyleBox(sender, e, property, lineStylePatternBox); };

            this.view.addControl(lineStylePatternBox);

            ColorBox lineStyleColorBox = new ColorBox();
            lineStyleColorBox.Name = "color";
            lineStyleColorBox.SelectedIndex = lineStyleColorBox.Items.IndexOf(lineStyle.LineColor.ToKnownColor().ToString()); //@TODO is not set because of index searching not implemented

            lineStyleColorBox.SelectedIndexChanged += delegate (object sender, EventArgs e)
            { handleLineStyleBox(sender, e, property, lineStyleColorBox); };

            this.view.addControl(lineStyleColorBox);

            LineStyleThicknessBox lineStyleThicknessBox = new LineStyleThicknessBox();
            lineStyleThicknessBox.Name = "thickness";
            lineStyleThicknessBox.SelectedIndex = lineStyleThicknessBox.Items.IndexOf((int)lineStyle.Thickness);

            lineStyleThicknessBox.SelectedIndexChanged += delegate (object sender, EventArgs e)
            { handleLineStyleBox(sender, e, property, lineStyleThicknessBox); };

            this.view.addControl(lineStyleThicknessBox);
        }

        protected void handleDecimalBox<T>(Object sender, EventArgs e, PropertyInfo property, TextBox decimalBox)
        {
            try {
                T value = (T)Convert.ChangeType(decimalBox.Text, typeof(T));
                property.SetValue(dynamicData, value, null);
            } catch (Exception ex) {
            }

            this.updatePanels();
        }

        protected void handleColorBox(Object sender, EventArgs e, PropertyInfo property, ColorBox lineStyleBox)
        {
            Color color = Color.FromName(lineStyleBox.SelectedItem.ToString());
            property.SetValue(dynamicData, color, null);

            this.updatePanels();
        }

        protected void handleLineStyleBox(Object sender, EventArgs e, PropertyInfo property, ComboBox lineStyleBox)
        {
            LineStyle lineStyle = (LineStyle)property.GetValue(dynamicData, null);
            if (lineStyleBox is LineStylePatternBox) {
                lineStyle.Pattern = (DashStyle)Enum.Parse(typeof(DashStyle), (String)lineStyleBox.SelectedItem);
            } else if (lineStyleBox is ColorBox) {
                lineStyle.LineColor = Color.FromName(lineStyleBox.SelectedItem.ToString());
            } else if (lineStyleBox is LineStyleThicknessBox) {
                lineStyle.Thickness = (int)lineStyleBox.SelectedItem;
            }

            this.updatePanels();
        }

        private void updatePanels()
        {
            if (null == panelToUpdate) {
                return;
            }
            panelToUpdate.Invalidate();
            if (0 < panelsToUpdate.Count) {
                foreach (Panel panel in panelsToUpdate) {
                    panel.Invalidate();
                }
            }
        }

        public Panel PanelToUpdate
        {
            get { return panelToUpdate; }
            set { panelToUpdate = value; }
        }

        public ArrayList PanelsToUpdate
        {
            get { return panelsToUpdate; }
            set { panelsToUpdate = value; }
        }

        public DynamicData DynamicData
        {
            get { return dynamicData; }
            set { dynamicData = value; }
        }

        public MapperView View
        {
            get { return view; }
            set { view = value; }
        }
    }
}