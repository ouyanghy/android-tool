using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

using System.Threading.Tasks;
using System.Windows.Forms;

namespace android_tool
{
    public class MessageDialog
    {
        private String mCaption;
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        static extern bool EndDialog(IntPtr hDlg, out IntPtr nResult);


        public void ShowMessageBoxTimeout(string text, string caption, int timeout)
        {
            startTime(timeout);
            mCaption = caption;
            MessageBox.Show(text, caption);

        }
        private void startTime(int interval)
        {
            Timer timer = new Timer();
            timer.Interval = interval;
            timer.Tick += new EventHandler(timeDoing);
            timer.Enabled = true;

        }

        private void timeDoing(object sender, EventArgs e)
        {
            IntPtr dlg = FindWindow(null, mCaption);

            if (dlg != IntPtr.Zero)
            {
                Console.WriteLine("kill");
                IntPtr result;

                EndDialog(dlg, out result);
            }
            else
                Console.WriteLine("unkill");
        }
    }
}
