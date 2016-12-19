using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace android_tool
{
    public class CmdResult
    {
        public int ret = -1;
        public String output;
        public bool result;

        public bool getResult()
        {
            if (ret == 0)
                return true;

            return false;
        }
    }
}
