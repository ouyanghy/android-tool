using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace android_tool
{
    public class CmdFunc
    {
        private CmdOption mOption;
        private CmdsThread mThread;
        private Cmd mCmd;
        private MProgressBar mBar;
        public CmdFunc(Cmd cmd, CmdsThread thread,CmdOption opt, MProgressBar bar)
        {
            mOption = opt;
            mThread = thread;
            mCmd = cmd;
            mBar = bar;
        }

        public void callClickFunc()
        {
            if (mOption.type == Enums.CmdType.APK)
                funcClickApk();
            else if (mOption.type == Enums.CmdType.UPDATE)
                funcClickFramware();
        }

        private void funcClickApk()
        {

            mThread.startCmdInstallThread(mOption.path[0], mBar);

        }

        private void funcClickFramware()
        {
            mThread.startCmdUpdateThread(mOption, mBar);
        }
    }
}
