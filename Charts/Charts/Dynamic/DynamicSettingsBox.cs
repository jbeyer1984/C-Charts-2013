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
    class DynamicSettingsBox : IDynamicSettingsBox
    {
        Rectangle wrapper = new Rectangle(
            0, 100,
            0, 100
        );
        SolidBrush aBrush = new SolidBrush(Color.Red);
        private ChartForm formToUpdate;
        private Panel parentPanel;
        private TextBox tBox;
        //private Button submit;
        private string jsonString;
        protected DynamicData dynamicData; //@todo naming and scope

        public DynamicSettingsBox(Panel panel)
        {
            initPanel(panel);
        }

        public DynamicSettingsBox(Panel panel, ChartForm formToUpdate)
        {
            this.formToUpdate = formToUpdate;
            initPanel(panel);
        }

        private void initPanel(Panel panel)
        {
            parentPanel = panel;

            dynamicData = formToUpdate.Legend.dd; //@todo name and scope of dd

            tBox = new TextBox();
            tBox.Width = 100;
            tBox.Height = 100;
            tBox.Multiline = true;

            //submit = new Button();
            //submit.Width = 100;
            //submit.Height = 20;
            //submit.Top = 100;
            //submit.Text = "update";

            //submit.Click += new System.EventHandler(updateChangedDynamicData);
            tBox.TextChanged += new System.EventHandler(updateChangedDynamicData);
        }

        public virtual String getDynamicSettingsJSONString()
        {
            return ((DynamicData) dynamicData).ToJSON();
        }

        public void addDynamicSettingsBox(Graphics g)
        {
            //jsonString = ((DynamicDataLegend) dynamicData).ToJSON();
            jsonString = getDynamicSettingsJSONString();
            tBox.Text = jsonString;

            int res = this.parentPanel.Controls.IndexOf(tBox);

            if (0 > this.parentPanel.Controls.IndexOf(tBox))
            {
                this.parentPanel.Controls.Add(tBox);
                //this.parentPanel.Controls.Add(submit);
            }
            g.FillRectangle(aBrush, wrapper);
        }

        public virtual void updateChangedDynamicData(object sender, EventArgs e) {}

        public ChartForm FormToUpdate
        {
            get { return formToUpdate; }
            set { value = formToUpdate; }
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
