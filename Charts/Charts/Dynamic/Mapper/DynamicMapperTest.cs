using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Reflection;

namespace Charts
{
    class DynamicMapperTest
    {
        protected MapperView view;

        protected DynamicData dynamicData;

        private Panel panelToBind;
        private Panel panelToUpdate;
        //protected String key;

        private int labelHeight;
        private string jsonString;

        public DynamicMapperTest(Panel panelToUpdate, DynamicData dynamicData, MapperView view)
        {
            this.panelToUpdate = panelToUpdate;
            this.dynamicData = dynamicData;
            this.view = view;
        }

        //public DynamicMapper(Panel parentPanel, ChartPanel panelToUpdate)
        //{
        //    this.panelToUpdate = panelToUpdate;
        //    initPanel(parentPanel);
        //}

        //public DynamicMapper(Panel parentPanel, ChartPanel panelToUpdate, String key)
        //{
        //    this.panelToUpdate = panelToUpdate;
        //    this.key = key;
        //    initPanel(parentPanel);
        //}

        public void bind(Panel panelToBind)
        {
            this.view.RootPanel = panelToBind;
            this.view.build();

            PropertyInfo[] propertyInfos;
            propertyInfos = dynamicData.GetType().GetProperties();

            foreach (PropertyInfo property in propertyInfos)
            {
                Console.WriteLine("property is {0}", property.Name);
                if (property.PropertyType == typeof(LineStyle))
                {
                    this.bindLineStyle(property);

                    //property.SetValue(dynamicData, Convert.ChangeType(lineStyle, property.PropertyType), null);
                    //property.SetValue(dynamicData, lineStyle, null);
                    //lineStyle.Pattern = DashStyle.Solid;
                }
                //Console.WriteLine("property is {0}", property.PropertyType);
            }
        }

        protected void bindLineStyle(PropertyInfo property) {
            LineStyle lineStyle = (LineStyle)property.GetValue(dynamicData, null);

            LineStylePatternBox lineStylePatternBox = new LineStylePatternBox();
            lineStylePatternBox.SelectedIndex = (int) lineStyle.Pattern;

            lineStylePatternBox.SelectedIndexChanged += delegate(object sender, EventArgs e)
            { handleLineStylePatternBox(sender, e, property, lineStylePatternBox); };

            this.view.addControl(lineStylePatternBox);

            LineStyleColorBox lineStyleColorBox = new LineStyleColorBox();
            lineStyleColorBox.SelectedItem = lineStyle.LineColor; //@TODO is not set because of index searching not implemented

            lineStyleColorBox.SelectedIndexChanged += delegate(object sender, EventArgs e)
            { handleLineStyleColorBox(sender, e, property, lineStyleColorBox); };

            this.view.addControl(lineStyleColorBox);
        }

        protected void handleLineStylePatternBox(Object sender, EventArgs e, PropertyInfo property, LineStylePatternBox lineStyleBox)
        {
            LineStyle lineStyle = (LineStyle) property.GetValue(dynamicData, null);
            lineStyle.Pattern = (DashStyle)Enum.Parse(typeof(DashStyle), (String)lineStyleBox.SelectedItem);

            panelToUpdate.Invalidate();
        }

        protected void handleLineStyleColorBox(Object sender, EventArgs e, PropertyInfo property, LineStyleColorBox lineStyleBox)
        {
            LineStyle lineStyle = (LineStyle)property.GetValue(dynamicData, null);
            lineStyle.LineColor = Color.FromName(lineStyleBox.SelectedItem.ToString());

            panelToUpdate.Invalidate();
        }

        //private void initTextBox()
        //{
        //    tBox = new TextBox();
        //    tBox.Width = panelToBind.ClientRectangle.Width;
        //    tBox.Height = panelToBind.ClientRectangle.Height - labelHeight;
        //    tBox.Multiline = true;
        //    tBox.Top = labelHeight;

        //    tBox.TextChanged += new System.EventHandler(updateChangedDynamicData);
        //}









        //public virtual String getDynamicSettingsJSONString()
        //{
        //    return ((DynamicData) dynamicData).ToJSON();
        //}

        //public virtual void addDynamicSettingsBox(Graphics g)
        //{
        //    //jsonString = ((DynamicData) dynamicData).ToJSON();
        //    jsonString = getDynamicSettingsJSONString();
        //    tBox.Text = jsonString;

        //    if (0 > this.panelToBind.Controls.IndexOf(tBox))
        //    {
        //        this.panelToBind.Controls.Add(label);
        //        this.panelToBind.Controls.Add(tBox);
        //    }
        //}

        //public virtual void updateChangedDynamicData(object sender, EventArgs e) {}

        //public ChartPanel PanelToUpdate
        //{
        //    get { return panelToUpdate; }
        //    set { value = panelToUpdate; }
        //}

        //public TextBox TBox
        //{
        //    get { return tBox; }
        //    set { value = tBox; }
        //}

        public string JsonString
        {
            get { return jsonString; }
            set { jsonString = value; }
        }

        public int LabelHeight
        {
            get { return labelHeight; }
            set { labelHeight = value; }
        }

        //public Label Label
        //{
        //    get { return label; }
        //    set { label = value; }
        //}

        public Panel ParentPanel
        {
            get { return panelToBind; }
            set { panelToBind = value; }
        }
    }
}
