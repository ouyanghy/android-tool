using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace android_tool
{
    public interface InterfaceOutput
    {
         void showText(String s);
    }

    public interface InterfaceResult
    {
        void setResult(int cmdType, bool b);
    }
}
