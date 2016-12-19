using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace android_tool
{
    public class CmdOption
    {
        public CmdOption(int length)
        {
            this.length = length;
            alloc();
        }

        private void alloc()
        {
            path = new String[length];
            dst = new String[length];
            permission = new String[length];
            size = new long[length];
            state = new int[length];
        }

        public void calcSize()
        {
            for (int i = 0; i < length; i++)
            {
                size[i] = calcFileSize(path[i]);
                sizeTotal += size[i];
            }
        }

        private long calcFileSize(String s)
        {
            if (s == null)
                return -1;
            FileInfo info = new FileInfo(s);
            if (info.Exists == false)
                return -1;
            Console.WriteLine("length:" + info.Length);
            return info.Length;

        }

        public string[] path;
        public string[] permission;
        public string[] dst;
        public int length;
        public long [] size;
        public long sizeTotal;
        public int[] state;
    }

    
}
