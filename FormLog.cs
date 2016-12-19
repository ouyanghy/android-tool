using System;
using System.Threading;
using System.Windows.Forms;

namespace android_tool
{
    public partial class FormLog : Form
    {
        private static SynchronizationContext mSyn;
        public FormLog()
        {
            InitializeComponent();
            mSyn = SynchronizationContext.Current;

        }

        public  void showLog(String s)
        {
            if (s == null)
                return;

            mSyn.Post(__showLog, s);
        }

        public void __showLog(Object obj)
        {
            String s = (String)(obj);
            tv.Text += s;
            tv.SelectionStart = tv.Text.Length;
            tv.ScrollToCaret();
           
        }

    }
}
