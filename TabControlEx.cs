using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace android_tool.pictures
{
    public class TabControlEx : TabControl
    {
        Bitmap[] mImage;
        private const int TABEL_IN_WIDTH = 72;
        private const int TABEL_IN_HEIGTH = 72;
        private const int TABEL_WIDTH = 142;
        private const int TABEL_HEIGHT = 113;

        private int TABEL_IN_X_START = (TABEL_WIDTH - TABEL_IN_WIDTH) /2;
        private int TABEL_IN_Y_START = (TABEL_HEIGHT - TABEL_IN_HEIGTH) / 2;
 
        public TabControlEx()
        {
            //base.Tabe
            base.SetStyle(ControlStyles.UserPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.ResizeRedraw |
                ControlStyles.SupportsTransparentBackColor,
                true);
            SizeMode = TabSizeMode.Fixed;
            ItemSize = new Size(TABEL_WIDTH, TABEL_HEIGHT);
            mImage = new Bitmap[10];
            mImage[0] = new Bitmap(this.GetType(), "connect.png");
            mImage[1] = new Bitmap(this.GetType(), "iso.png");
            mImage[2] = new Bitmap(this.GetType(), "upgrade.png");
            mImage[3] = new Bitmap(this.GetType(), "app.png");
            mImage[4] = new Bitmap(this.GetType(), "setting.png");
            this.MouseMove  += new MouseEventHandler(tabPage2_MouseMove);
            // mImageBack = new Bitmap(this.GetType(), "back.png");
            // Bitmap bmWatermark;
        

        
        }

        public void setTagName()
        {
            TabPages[0].Text = "       " + Enums.TabPageName.DEVICE_STATE;
            TabPages[1].Text = "           " + Enums.TabPageName.FLASH;
            TabPages[2].Text = "           " + Enums.TabPageName.UPDATE;
            TabPages[3].Text = "           " + Enums.TabPageName.APP;
            TabPages[4].Text = "           " + Enums.TabPageName.SETTING;
        }

        public void setTagDeviceName(String s)
        {
          
            TabPages[0].Text = "        " + s;
        }

        private void tabPage2_MouseMove(object sender, MouseEventArgs e)
        {
          //  Console.WriteLine("move:" + e.Location);
            int index = inRect(e.Location);
        //    Console.WriteLine("index:" + index + "move:" + e.Location);

            if (index != -1)
                SelectedIndex = index;
        }

        private int inRect(Point p)
        {
            for (int i = 0; i < this.TabCount; i++)
            {
                Rectangle rect = this.GetTabRect(i);
                if (p.X >= rect.Left && p.X <= rect.Right
                    && p.Y >= rect.Top && p.Y <= rect.Bottom)
                    return i;

            }
                return -1;
        }

        private void drawOneTab(Graphics graphics, int i)
        {
            float x, y;
            PointF p = new PointF();
            Rectangle rect;

            rect = this.GetTabRect(i);

            x = rect.X;//+ (rect.Width - textSize.Width) / 2;
            y = rect.Y + (rect.Height * 8) / 10 + 2;
            p.X = x;
            p.Y = y;
            //back
            Bitmap bmp = new Bitmap(this.GetType(), "default.png");
            graphics.DrawImage(bmp, this.GetTabRect(i));

            if (SelectedIndex == i)
            {
                bmp = new Bitmap(this.GetType(), "invert.png");
                bmp.MakeTransparent(Color.Aqua);
                graphics.DrawImage(bmp, this.GetTabRect(i));
            }

            Rectangle inRect = new Rectangle(rect.X + TABEL_IN_X_START, rect.Y + TABEL_IN_Y_START, TABEL_IN_WIDTH, TABEL_IN_HEIGTH);
            graphics.DrawImage(mImage[i], inRect);
            Font f = new Font("微软雅黑", 11, FontStyle.Regular);


            if (SelectedIndex == i)
            {
                graphics.DrawString(
                  TabPages[i].Text,
                    f,
                    SystemBrushes.ControlLightLight,    // 高光颜色  
                    p.X,
                    p.Y);
            }
            else
            {
                graphics.DrawString(
                  TabPages[i].Text,

                  f,
                  SystemBrushes.ControlDarkDark,    // 高光颜色  
                  p.X,
                  p.Y);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
          
            for (int i = 0; i < this.TabCount; i++)
            {

                drawOneTab(e.Graphics, i);       
            }
       
        }
    }
}
