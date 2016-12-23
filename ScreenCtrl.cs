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
        private int iDiv;
        private PictureBox mScreen;
        private int mWidth, mHeight;
        private Point mPointDown,mPointUp;
        private float mXPercent,mYPercent;
        public ScreenCtrl(Cmd cmd, PictureBox picture)
        {
            mCmd = cmd;
            mScreen = picture;
            mSyn = SynchronizationContext.Current;
            mCmd.excuteCmdRemount();
            mCmd.excuteCmdPush(Enums.Path.PATH_SRC + @"screen", Enums.AndroidPath.SYSTEM_BIN + "screen", Enums.LinuxPermission.RWX_RWX_RWX);

            mClient = new UsbClient(cmd);
            iDiv = 2;
            mWidth = 720;
            mHeight = 1280;
            mXPercent = (float)mWidth / (float)picture.Width;
            mYPercent = (float)mHeight / (float)picture.Height;
            setMouseFunc();
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
           
            screenCapThread();
            Thread.Sleep(100);
            mClient.connect();
            mClient.setParam(iDiv);
            usbWorkThread();
            feedServerThread();
            bOpen = true;
        }

        private void setMouseFunc()
        {
            mScreen.MouseDown += new MouseEventHandler(mouseDown);
            mScreen.MouseUp += new MouseEventHandler(mouseUp);
          
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
            try
            {
                mCmd.excuteCmdExcute("screen");
            }
            catch (Exception ex)
            {

            }
        }

        private void feedServerThread()
        {
            new Thread(feedServer, 0).Start();
        }
        private void feedServer(Object o)
        {
            while (bWorkThread == 0)
            {
                try
                {
                    mClient.feedServer();
                }
                catch (Exception e)
                {
                    break;
                }
                Thread.Sleep(5 * 1000);
            }
        }
        private void readRaw()
        {
            while (0 == Interlocked.Read(ref bWorkThread))
            {
                try
                {
                    bReadyExit = false;
                    mClient.cmdFrameBuffer();
                    // Console.WriteLine("send cmd");
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
                    // Console.WriteLine("read data");
                    byte[] bs = mClient.read(mWidth * mHeight * 4 / (iDiv * iDiv));
                    /*end*/
                    //Console.WriteLine("read end before");
                    b = mClient.isFrameEnd();
                    //Console.WriteLine("read end after");
                    if (b == false)
                    {
                        /*ensure read over*/
                        Console.WriteLine("is not end");
                        mClient.read(4096);
                        bReadyExit = true;
                        continue;
                    }

                    //Console.WriteLine(bs[0] + " " + bs[1] + " " + bs[2] + "　" + bs[3]);
                    // Console.WriteLine("trnasfer bitmap");
                    Bitmap bmp = Rgb2Bitmap.transferBitmap(bs, mWidth / iDiv, mHeight / iDiv);
                    Bitmap dst = new Bitmap(bmp, new Size(mScreen.Width, mScreen.Height));
                    // Console.WriteLine("draw bitmap");
                    mSyn.Post(drawBitmap, bmp);
                    bReadyExit = true;
                    //Console.WriteLine("read:" + bs[0] + "," + bs[1] + "," + bs[2]);
                    bs = null;

                }
                catch (Exception ex)
                {
                    bReadyExit = true;
                }


            }
            bReadyExit = true;
        }

        public void drawBitmap(Object o)
        {
            Bitmap bmp = (Bitmap)o;
            mScreen.Image = bmp;

        }

        
        private int clickType()
        {
            int dx, dy;
            int ux, uy;
            dx = mPointDown.X;
            dy = mPointDown.Y;
            ux = mPointUp.X;
            uy = mPointUp.Y;

            if (Math.Abs(dx - ux) > 30)
                return Enums.ClickType.CLICK;
            else
                return Enums.ClickType.SWIPE;
        }

        private void mouseDown(object sender, System.EventArgs e)
        {
            mPointDown = getPoint();
        }

        private void mouseUp(object sender, System.EventArgs e)
        {
            int dx, dy, ux, uy;
            mPointUp = getPoint();
            dx = (int)(mPointDown.X * mXPercent);
            dy = (int)(mPointDown.Y * mYPercent);
            ux = (int)(mPointUp.X * mXPercent);
            uy = (int)(mPointUp.Y * mYPercent);
            mCmd.excuteCmdTouchSwipe(dx, dy, ux, uy);
            Console.WriteLine("dx:" + dx + " dy:" + dy + " ux:" + ux + " uy:" + uy);
            Console.WriteLine("down x:" + mPointDown.X + " down y:" + mPointDown.Y + " percent:" + mXPercent);
        }

        public Point getPoint()
        {
            Point p = mScreen.PointToClient(Control.MousePosition);
         //   Console.WriteLine("point:" + p);
            return p;
        }
       
        public void touch(int x, int y)
        {
            mCmd.excuteCmdTouchPoint(x, y);
        }

        public void swipe(int sx, int sy, int dx, int dy)
        {
            mCmd.excuteCmdTouchSwipe(sx, sy, dx, dy);
        }
    }
}
