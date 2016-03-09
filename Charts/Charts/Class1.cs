using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charts
{
    class TestGetter
    {
        private int test;

        public int Test
        {
            get { return test; }
            set { test = value; }
        }

        public void print()
        {
            System.Windows.Forms.MessageBox.Show(this.test.ToString());
        }
    }
}
