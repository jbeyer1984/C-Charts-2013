using Charts.Dynamic.Data;
using Charts.Dynamic.View;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Charts
{
    class MapperViewTest : MapperView
    {
        //public Panel rootPanel;

        private Label label;
        private int labelHeight;

        public MapperViewTest()
        {

        }

        public override void build()
        {
            this.initPanel();
        }

        protected virtual void initPanel()
        {

            //this.parentPanel = parentPanel;

            //this.mapDynamicData();

            this.initLabel();
            //initTextBox();
        }

        protected void initLabel()
        {
            labelHeight = 15;
            int labelWidth = RootPanel.ClientRectangle.Width;

            label = new Label();
            label.Text = RootPanel.Text;

            label.Location = new Point(0, NextOffset);
            label.Size = new Size(labelWidth, labelHeight);

            this.addControl(label);
        }

        //private void initTextBox()
        //{
        //    tBox = new TextBox();
        //    tBox.Width = rootPanel.ClientRectangle.Width;
        //    tBox.Height = rootPanel.ClientRectangle.Height - labelHeight;
        //    tBox.Multiline = true;
        //    tBox.Top = labelHeight;

        //    tBox.TextChanged += new System.EventHandler(updateChangedDynamicData);
        //}

        public Label Label
        {
            get { return label; }
            set { label = value; }
        }

        //public Panel RootPanel
        //{
        //    get { return rootPanel; }
        //    set { value = rootPanel; }
        //}
    }
}
