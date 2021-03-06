﻿using Charts.Chart.CacheFolder.CacheInterfaces;
using Charts.Chart.Identifier;
using System.Drawing;

namespace Charts.Chart.PopupFolder
{
    /// <summary>
    /// show selected options and other fast options
    /// 
    /// disadvantages: border is not fully disappeared
    /// </summary>
    public class PopupWindow : System.Windows.Forms.ToolStripDropDown, IIdentifier, ICacheAble
    {
        private string identifier;
        private bool isAlreadyCreated;

        private System.Windows.Forms.Control _content;
        private System.Windows.Forms.ToolStripControlHost _host;

        public PopupWindow()
        {
            //Basic setup...
            this.AutoSize = false;
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;
        }

        public string Identifier
        {
            get { return identifier; }
            set { identifier = value; }
        }

        public bool IsAlreadyCreated
        {
            get { return isAlreadyCreated; }
            set { isAlreadyCreated = value; }
        }

        public void initPopup(System.Windows.Forms.Control content)
        {
            this._content = content;
            this._host = new System.Windows.Forms.ToolStripControlHost(content);

            //Positioning and Sizing
            this.MinimumSize = content.MinimumSize;
            this.MaximumSize = content.Size;
            this.Size = content.Size;
            content.Location = Point.Empty;

            //Add the host to the list
            this.Items.Add(this._host);
        }
    }
}