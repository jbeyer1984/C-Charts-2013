using System.Drawing;
using System.Windows.Forms;

namespace Charts
{
    internal class MapperViewWithLabel : MapperView
    {
        private Label label;
        private int labelHeight;

        public MapperViewWithLabel()
        {
        }

        public override void build()
        {
            this.initPanel();
        }

        protected virtual void initPanel()
        {
            this.initLabel();
        }

        protected void initLabel()
        {
            labelHeight = 25;
            int labelWidth = RootPanel.ClientRectangle.Width;

            label = new Label();
            label.Text = RootPanel.Text;

            label.Location = new Point(0, NextOffset);
            label.Size = new Size(labelWidth, labelHeight);

            this.addControl(label);
        }

        public Label Label
        {
            get { return label; }
            set { label = value; }
        }
    }
}