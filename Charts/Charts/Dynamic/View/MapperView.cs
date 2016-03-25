using Charts.Dynamic.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Charts
{
    abstract class MapperView : IMapperView
    {
        private Panel rootPanel;

        private int nextOffset = 0;

        abstract public void build();

        public void addControl(Control control)
        {
            control.Top = nextOffset;
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
