using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace android_tool
{
    public partial class ProgressEx : UserControl
    {
        private bool bRunning = false;
        SynchronizationContext mSync;
        public ProgressEx()
        {
            mSync = SynchronizationContext.Current;
            InitializeComponent();
        }

        public void runCycle()
        {
            bRunning = true;
            Thread thread = new Thread(threadRunCycle);
            thread.Start();
        }

        private void threadRunCycle()
        {
            while (bRunning)
            {
                runOneCycle();
                Thread.Sleep(10);
            }

        }


        private Bitmap getBitmapFile()
        {
            return Properties.Resources.watting;
        }

        private void runOneCycle()
        {
            mSync.Post(draw, RotateFlipType.RotateNoneFlipNone);
            Thread.Sleep(120);
            mSync.Post(draw, RotateFlipType.Rotate270FlipNone);
            Thread.Sleep(120);
            mSync.Post(draw, RotateFlipType.Rotate180FlipNone);
            Thread.Sleep(120);
            mSync.Post(draw, RotateFlipType.Rotate90FlipNone);           
            Thread.Sleep(120);

        }

        private void draw(Object obj)
        {
            RotateFlipType angel = (RotateFlipType)obj;
            Bitmap bmp = getBitmapFile();
            bmp.RotateFlip(angel);
            pictureBoxEx.BackgroundImage = bmp;
            pictureBoxEx.Update();
        }

        public void stopCycle()
        {
            bRunning = false;
        }

    }
}
