using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace android_tool
{
    public class ScreenCtrl 
    {
        const int WM_SYSCOMMAND = 0x0112;
        const int SC_CLOSE = 0xF060;
        private Cmd mCmd;
        private UsbClient mClient;
        private static SynchronizationContext mSyn;
        private bool bReadyExit = true;
        /*0 work, -1 undo*/
        private long bWorkThread = -1;
        private bool bOpen = false;
        private Thread mThread;
        private long mNowTime;
        private int iDiv;
        private PictureBox mScreen;
        public ScreenCtrl(Cmd cmd, PictureBox picture)
        {
            mCmd = cmd;
            mScreen = picture;
            mSyn = SynchronizationContext.Current;
            mCmd.excuteCmdRemount();
            mCmd.excuteCmdPush(Enums.Path.PATH_SRC + @"screen", Enums.AndroidPath.SYSTEM_BIN + "screen", Enums.LinuxPermission.RWX_RWX_RWX);

            mClient = new UsbClient(cmd);
            iDiv = 2;

        }

        public bool getState()
        {
            return bOpen;
        }

        public void disconnect()
        {
            if (bOpen == false)
                return;
            bOpen = false;

            Interlocked.Exchange(ref bWorkThread, -1);
            while (bReadyExit == false) ;
            mClient.cmdExit();
            mClient.disconnect();
            
        }

        public void connect()
        {
            // mCmd.excuteCmdExcute("screen");
            if (bOpen == true)
                return;
            bOpen = true;
            screenCapThread();
            Thread.Sleep(100);
            mClient.connect();
            mClient.setParam(iDiv);
            usbWorkThread();
            feedServerThread();
            
        }



        private void usbWorkThread()
        {
            Interlocked.Exchange(ref bWorkThread, 0);
            mThread = new Thread(readRaw);
            mThread.Start();

        }

        private void screenCapThread()
        {
            Thread thread = new Thread(screenCap, 0);
            thread.Start();
        }

        private void screenCap(Object o)
        {
            mCmd.excuteCmdExcute("screen");
        }

        private void feedServerThread()
        {
            new Thread(feedServer, 0).Start();
        }
        private void feedServer(Object o)
        {
            while (bWorkThread == 0)
            {
                mClient.feedServer();
                Thread.Sleep(5 * 1000);
            }
        }
        private void readRaw()
        {
            while (0 == Interlocked.Read(ref bWorkThread))
            {
                bReadyExit = false;
                mClient.cmdFrameBuffer();
                Console.WriteLine("send cmd");
                /*start*/
                bool b = mClient.isFrameStart();
                if (b == false)
                {
                    /*read to end*/
                    Console.WriteLine("is not start");
                    mClient.read(4096);
                    bReadyExit = true;
                    continue;
                }
                /*data*/
                Console.WriteLine("read data");
                byte[] bs = mClient.read(1280 * 720 * 4 / (iDiv * iDiv));
                /*end*/
                Console.WriteLine("read end before");
                b = mClient.isFrameEnd();
                Console.WriteLine("read end after");
                if (b == false)
                {
                    /*ensure read over*/
                    Console.WriteLine("is not end");
                    mClient.read(4096);
                    bReadyExit = true;
                    continue;
                }

                //Console.WriteLine(bs[0] + " " + bs[1] + " " + bs[2] + "　" + bs[3]);
                Console.WriteLine("trnasfer bitmap");
                Bitmap bmp = Rgb2Bitmap.transferBitmap(bs, 720 / iDiv, 1280 / iDiv);
                Bitmap dst = new Bitmap(bmp, new Size(mScreen.Width, mScreen.Height));
                Console.WriteLine("draw bitmap");
                mSyn.Post(drawBitmap, bmp);
                bReadyExit = true;
                //Console.WriteLine("read:" + bs[0] + "," + bs[1] + "," + bs[2]);
                bs = null;

            }
            bReadyExit = true;
        }

        public void drawBitmap(Object o)
        {
            Bitmap bmp = (Bitmap)o;
            mScreen.Image = bmp;

        }
       
    }
}
