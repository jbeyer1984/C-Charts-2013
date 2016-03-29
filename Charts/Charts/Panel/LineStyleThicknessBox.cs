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
    public class LineStyleThicknessBox : ComboBox
    {
		private int[] values = new int[10];

        public LineStyleThicknessBox()
		{
			this.DrawMode = DrawMode.OwnerDrawFixed;
            this.DropDownStyle = ComboBoxStyle.DropDownList;
			FillLineTypes();
            SelectedItem = this.Items[0];
		}

		private void FillLineTypes()
		{
			this.Items.Clear();
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = i+1;
                this.Items.Add(values[i]);
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

                int i = e.Index;

                if (i <= values.Length - 1)
                {
                    SolidBrush aBrush = new SolidBrush(Color.Black);
                    StringFormat sFormat = new StringFormat();
                    e.Graphics.DrawString(values[i].ToString(), DefaultFont, aBrush, ItemRect.Left, ItemRect.Top, sFormat);
                    i++;
                }
            }
        }
	}
}
