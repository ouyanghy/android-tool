using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace android_tool
{
    class Rgb2Bitmap
    {
        private byte[] mHeader = {0x42, 0x4d, 0x36, 0x30,
                                  0x2a, 0x00, 0x00, 0x00,
                                  //          a       b
                                  0x00, 0x00, 0x36, 0x00,//11
                                  //c     d     e    f   
                                  0x00, 0x00, 0x28, 0x00,

                                  //10 11      12   13
                                  0x00, 0x00, 0xd0, 0x02,
                                  //14  15    16    17
                                  0x00, 0x00, 0x00, 0x05,
                                  //18  19     1a   1b
                                  0x00, 0x00, 0x01, 0x00,
                                  //1c  1d
                                  0x18, 0x00, 0x00, 0x00,

                                  0x00, 0x00, 0x00, 0x30,

                                  0x2a, 0x00, 0x00, 0x00};
        public static Bitmap transferBitmap(byte[] data, int width, int height)
        {
            Bitmap bmp = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            BitmapData bd = bmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
           // bd.PixelFormat = PixelFormat.Format
            System.Runtime.InteropServices.Marshal.Copy(data, 0, bd.Scan0, data.Length);
            bmp.UnlockBits(bd);
            return bmp;
        }
    }

   
}
