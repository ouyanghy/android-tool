using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace android_tool
{
    public partial class FormControl : Form
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
        private int iDiv ;
        public FormControl(Cmd cmd)
        {         
            mCmd = cmd;
            InitializeComponent();
            mSyn = SynchronizationContext.Current;
            mCmd.excuteCmdRemount();
            mCmd.excuteCmdPush(Enums.Path.PATH_SRC + @"screen", Enums.AndroidPath.SYSTEM_BIN + "screen", Enums.LinuxPermission.RWX_RWX_RWX);
          
             mClient = new UsbClient(cmd);
            iDiv = 2;
            
        }

        private void stop()
        {
            Interlocked.Exchange(ref bWorkThread, -1);
            while (bReadyExit == false) ;
            mClient.cmdExit();
            mClient.disconnect();
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            // mCmd.excuteCmdExcute("screen");
            buttonConnect.Visible = false;
            screenCapThread();
            Thread.Sleep(100);
            mClient.connect();
            mClient.setParam(iDiv);
            usbWorkThread();
            feedServerThread();
            bOpen = true;
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
            while(bWorkThread == 0)
            {
                mClient.feedServer();
                Thread.Sleep(10 * 1000);
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
                byte[] bs = mClient.read(1280 * 720 * 4/(iDiv * iDiv) );
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
                Bitmap bmp = Rgb2Bitmap.transferBitmap(bs, 720/iDiv, 1280/iDiv);
                Bitmap dst = new Bitmap(bmp, new Size(pictureBoxControl.Width, pictureBoxControl.Height));
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
            pictureBoxControl.Image = bmp;
           
        }
        protected override void WndProc(ref Message m)
        {
           // Console.WriteLine("Form cmd:" + m + " | "+ m.Msg + " par:" + m.WParam);
            if (m.Msg == 0x02 )
            {
                Console.WriteLine("Form close");
                if (bOpen)
                    stop();
            }

            base.WndProc(ref m);
        }
    }
}
