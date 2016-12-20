using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace android_tool
{
    public partial class PullEx : UserControl
    {
        private Enums.ptr_func_pull mFunc;
        public PullEx()
        {
            InitializeComponent();
        }

        public void setFunc(Enums.ptr_func_pull f)
        {
            mFunc = f;
        }

        private void buttonPull_Click(object sender, EventArgs e)
        {
            mFunc?.Invoke(sender, e);
        }
    }
}
