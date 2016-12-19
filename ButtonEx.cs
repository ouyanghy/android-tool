using System;
using System.Drawing;
using System.Windows.Forms;

namespace android_tool.pictures
{
    public partial class ButtonEx : UserControl
    {
        private Enums.ptr_func_flash mFunc;
        public ButtonEx()
        {
            InitializeComponent();
        }

        public void setText(String text)
        {
            labelButtonEx.Text = text;
  
        }

        public void setPara(String text, Enums.ptr_func_flash func)
        {
            setText(text);
            setButtonClick(func);
        }

        public void setPara(String text, String file, Enums.ptr_func_flash func)
        {
            setText(text);
            setBitmap(file);
            setButtonClick(func);
        }

        public void setBitmap(String file)
        {
            imageButtonEx.BackgroundImage = new Bitmap(GetType(), file);
        }


        public void setButtonClick(Enums.ptr_func_flash func)
        {
            mFunc = func;
        }
        /*
        private void imageButtonEx_Click(object sender, EventArgs e)
        {
         
        }
        */
        private void imageButtonEx_Click_1(object sender, EventArgs e)
        {
            Image bmp = imageButtonEx.BackgroundImage;
            Graphics g = CreateGraphics();
            //g.DrawImage(bmp, new PointF(Location.X + 8, Location.Y + 8));
            //     this.labelButtonEx.Text = this.labelButtonEx.Text + "...";

            mFunc?.Invoke(sender, e);
        }
    }
}
