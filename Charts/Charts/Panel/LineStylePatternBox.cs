using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
//using System.ComponentModel;

namespace Charts
{
    public class LineStylePatternBox : ComboBox
    {
		private string[] names = Enum.GetNames(typeof(DashStyle));

        public LineStylePatternBox()
		{
			this.DrawMode = DrawMode.OwnerDrawFixed;
			this.DropDownStyle = ComboBoxStyle.DropDownList;
			FillLineTypes();
            SelectedItem = this.Items[0];
		}

		private void FillLineTypes()
		{
			this.Items.Clear();
			for (int i = 0; i < names.Length; i++) {
				this.Items.Add(names[i]);
			}
		}

        protected override void OnSelectedItemChanged(EventArgs e)
        {
            base.OnSelectedItemChanged(e);


        }

        protected override void OnDrawItem(System.Windows.Forms.DrawItemEventArgs e)
        {
            base.OnDrawItem(e);
            if (e.Index >= 0)
            {
                Rectangle ItemRect = e.Bounds;
                Pen pen = new Pen(Color.Black, 2);

                int i = e.Index;

                if (i <= names.Length - 1)
                {
                    pen.DashStyle = (DashStyle)System.Enum.Parse(typeof(DashStyle), names[i]);
                    e.Graphics.DrawLine(pen, Convert.ToSingle(ItemRect.X), Convert.ToSingle(ItemRect.Y + ItemRect.Height / 2), Convert.ToSingle(ItemRect.X + ItemRect.Width), Convert.ToSingle(ItemRect.Y + ItemRect.Height / 2));
                    i++;
                }
            }
        }
	}
}
