using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Collections;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Charts.Dynamic;

namespace Charts
{
    class DynamicSettingsMapper : IDynamicSettingsMapper
    {
        protected ChartPanel panelToUpdate;
        private Panel parentPanel;
        private TextBox tBox;
        private Label label;

        private int labelHeight;
        private string jsonString;
        protected DynamicData dynamicData;

        public DynamicSettingsMapper(Panel panel)
        {
            initPanel(panel);
        }

        public DynamicSettingsMapper(Panel panel, ChartPanel panelToUpdate)
        {
            this.panelToUpdate = panelToUpdate;
            initPanel(panel);
        }

        private void initPanel(Panel panel)
        {
            labelHeight = 15;

            parentPanel = panel;

            this.mapDynamicData();

            initLabel();
            initTextBox();
        }

        public virtual void mapDynamicData()
        {
            this.dynamicData = panelToUpdate.Legend.dd;
        }

        private void initLabel()
        {
            int labelWidth = parentPanel.ClientRectangle.Width;

            label = new Label();
            label.Text = parentPanel.Text;

            label.Location = new Point(0, 0);
            label.Size = new Size(labelWidth, labelHeight);
        }

        private void initTextBox()
        {
            tBox = new TextBox();
            tBox.Width = parentPanel.ClientRectangle.Width;
            tBox.Height = parentPanel.ClientRectangle.Height - labelHeight;
            tBox.Multiline = true;
            tBox.Top = labelHeight;

            tBox.TextChanged += new System.EventHandler(updateChangedDynamicData);
        }

        public virtual String getDynamicSettingsJSONString()
        {
            return ((DynamicData) dynamicData).ToJSON();
        }

        public void addDynamicSettingsBox(Graphics g)
        {
            //jsonString = ((DynamicData) dynamicData).ToJSON();
            jsonString = getDynamicSettingsJSONString();
            tBox.Text = jsonString;

            int res = this.parentPanel.Controls.IndexOf(tBox);

            if (0 > this.parentPanel.Controls.IndexOf(tBox))
            {
                this.parentPanel.Controls.Add(label);
                this.parentPanel.Controls.Add(tBox);
            }
        }

        public virtual void updateChangedDynamicData(object sender, EventArgs e) {}

        public ChartPanel PanelToUpdate
        {
            get { return panelToUpdate; }
            set { value = panelToUpdate; }
        }

        public TextBox TBox
        {
            get { return tBox; }
            set { value = tBox; }
        }

        public string JsonString
        {
            get { return jsonString; }
            set { jsonString = value; }
        }
    }
}
