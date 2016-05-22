using Charts.Dynamic.View;
using System.Drawing;
using System.Windows.Forms;

namespace Charts
{
    internal abstract class MapperView : IMapperView
    {
        private Panel rootPanel;

        private int nextOffset = 0;

        abstract public void build();

        public void addControl(Control control)
        {
            //Console.WriteLine("mapper view width of control: {0}", control.Width);
            int width = rootPanel.Width / 2;

            Label label = new Label();
            label.Text = control.Name;
            label.TextAlign = ContentAlignment.MiddleLeft;
            label.Top = nextOffset;
            label.Width = width;
            rootPanel.Controls.Add(label);

            control.Left = width;
            control.Top = nextOffset;
            control.Width = width;
            rootPanel.Controls.Add(control);
            this.addOffset(control);
        }

        private void addOffset(Control control)
        {
            nextOffset += control.Height;
        }

        public Panel RootPanel
        {
            get { return rootPanel; }
            set { rootPanel = value; }
        }

        public int NextOffset
        {
            get { return nextOffset; }
            set { nextOffset = value; }
        }
    }
}