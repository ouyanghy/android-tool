using System;
using System.Drawing;
using System.Windows.Forms;

namespace android_tool.pictures
{
    public partial class ButtonEx : UserControl
    {
        private CmdFunc mFunc;
        private Enums.ptr_func_flash mSystemFunc;
        private ToolTip mTip;
        public ButtonEx()
        {
            InitializeComponent();
            mTip = new ToolTip();
            mTip.ShowAlways = true;        
        }

        public void setText(String text)
        {
            
            if (text.Length > 8)
            {
                String top = text.Substring(0, 8);
                labelButtonExTop.Text = top;
                String bottom = text.Substring(8);
                labelButtonExBottom.Text = bottom;
            }
            else
            {
                labelButtonExTop.Text = text;
            }
          
  
        }

        public void setTip(String tip)
        {
            mTip.SetToolTip(imageButtonEx, tip);
        }

        public void setPara(String text, Enums.ptr_func_flash func)
        {
            setText(text);
           // setButtonClick(func);
        }

        public void setPara(String text, String file, Enums.ptr_func_flash func)
        {
            setText(text);
            setBitmap(file);
         //   setButtonClick(func);
        }

        public void setBitmap(String file)
        {
            imageButtonEx.BackgroundImage = new Bitmap(GetType(), file);
        }


        public void setButtonClick(CmdFunc fuc)
        {
            mFunc = fuc;
          //  mFunc = func;
        }

        public void setButtonClick(Enums.ptr_func_flash f)
        {
            mSystemFunc = f;
            //mFunc = fuc;
            //  mFunc = func;
        }
        /*
        private void imageButtonEx_Click(object sender, EventArgs e)
        {
         
        }
        */
        private void imageButtonEx_Click_1(object sender, EventArgs e)
        {
            if (mFunc !=null)
                mFunc.callClickFunc();
            else
                mSystemFunc?.Invoke(sender, e);
            // Image bmp = imageButtonEx.BackgroundImage;
            //Graphics g = CreateGraphics();
            //g.DrawImage(bmp, new PointF(Location.X + 8, Location.Y + 8));
            //     this.labelButtonEx.Text = this.labelButtonEx.Text + "...";

            //mFunc?.Invoke(sender, e);
        }
    }
}
