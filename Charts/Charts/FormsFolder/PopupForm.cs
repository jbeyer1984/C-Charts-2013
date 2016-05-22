using Charts.Chart.CacheFolder.CacheInterfaces;
using Charts.Chart.Identifier;
using System;
using System.Windows.Forms;

namespace Charts
{
    public partial class PopupForm : Form, IIdentifier, ICacheAble
    {
        private string identifier;
        private bool isAlreadyCreated;

        public PopupForm()
        {
            InitializeComponent();
            this.Text = string.Empty;
            this.ControlBox = false;
            this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
            this.DoubleBuffered = true;
            // set this.FormBorderStyle to None here if needed
            // if set to none, make sure you have a way to close the form!
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_NCHITTEST)
                m.Result = (IntPtr)(HT_CAPTION);
        }

        private const int WM_NCHITTEST = 0x84;
        private const int HT_CLIENT = 0x1;
        private const int HT_CAPTION = 0x2;

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
    }
}