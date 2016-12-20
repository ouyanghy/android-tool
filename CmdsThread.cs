using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace android_tool
{
    public class CmdsThread
    {
//        private String mDirPath = @"E:\镜像\\fastboot烧录\";

        private MProgressBar mProgress;
        private Cmd mCmd;
        private MessageDialog mMessage;
        private InterfaceResult mIntfResult;
        //Thread flash partition
        public CmdsThread(Cmd cmd, MessageDialog msg, InterfaceResult result)
        {
            mCmd = cmd;
            mMessage = msg;
            mIntfResult = result;
        }

        public void startCmdFlashThread(Object Option, MProgressBar bar)
        {
            mProgress = bar;
            Thread flash = new Thread(new ParameterizedThreadStart(flashThread));
            flash.Start(Option);
        }

        public void startCmdInstallThread(Object apk, MProgressBar bar)
        {
            mProgress = bar;
            //Console.WriteLine("bar:" + bar.p);
            Thread install = new Thread(new ParameterizedThreadStart(installApk));
            install.Start(apk);
        }

        public void startCmdUpdateThread(String[] path, String[] to, int length, String[] permission, MProgressBar bar)
        {
            mProgress = bar;
            CmdOption option = new CmdOption(length);

            option.path = path;
            option.permission = permission;
            option.dst = to;

            Thread update = new Thread(new ParameterizedThreadStart(updateSystem));
            update.Start(option);
        }

        public void startCmdUpdateThread(CmdOption option, MProgressBar bar)
        {
            mProgress = bar;
            Thread update = new Thread(new ParameterizedThreadStart(updateSystem));
            update.Start(option);
        }

        public void startCmdSearchThread(Object find)
        {
            mProgress = null;
            Thread search = new Thread(new ParameterizedThreadStart(searchThread));
            search.Start(find);
        }

        private void searchThread(Object obj)
        {
            String s = (String)obj;
            bool ret = true;

            ret = mCmd.excuteCmdSearch(s);
            mIntfResult.setResult(Enums.CmdType.SEARCH, ret);

        }

        public void startCmdPullThread(Object find)
        {
            mProgress = null;
            Thread pull = new Thread(new ParameterizedThreadStart(pullThread));
            pull.Start(find);
        }

        private void pullThread(Object obj)
        {
            String s = (String)obj;
            bool ret = true;

            ret = mCmd.excuteCmdPull(s);
            mIntfResult.setResult(Enums.CmdType.PULL, ret);

        }



        private void updateSystem(Object obj)
        {
            CmdOption option = (CmdOption)obj;
            bool ret = true;
            option.calcSize();
            if (mProgress != null)
                mProgress.showProgress(option);
            for (int i = 0; i < option.length; i++)
            {
                ret &= mCmd.excuteCmdUpdate(option.path[i], option.dst[i], option.permission[i]);
                option.state[i] = ret == true ? (int)Enums.WorkState.SUCC : (int)Enums.WorkState.ERROR;
            }

            mIntfResult.setResult(Enums.CmdType.UPDATE, ret);
            Thread.Sleep(1000);
            if (mProgress != null)
                mProgress.dismissProgress(0);

        }

        private void installApk(Object obj)
        {
            CmdOption option = new CmdOption(1);

            bool ret = false;
            option.state[0] = Enums.WorkState.RUNNING;
            option.path[0] = (String)obj;
            option.calcSize();

            mProgress.showProgress(option);
            ret = mCmd.excuteCmdInstallThirdApp(option.path[0]);
            option.state[0] = ret == true ? (int)Enums.WorkState.SUCC : (int)Enums.WorkState.ERROR;
            mIntfResult.setResult(Enums.CmdType.APK, ret);
            Thread.Sleep(1000);
            mProgress.dismissProgress(0);

        }
        private void flashThread(Object obj)
        {
            CmdOption option = (CmdOption)obj;

            int index = option.index;
            bool ret = false;

            switch (index)
            {
                case (Enums.PartitionIndex.ABOOT):
                    mProgress.showProgress(option);
                    ret = mCmd.excuteCmdFlashAboot(option.path[0]);
                    option.state[0] = ret == true ? (int)Enums.WorkState.SUCC : (int)Enums.WorkState.ERROR;
                    break;
                case Enums.PartitionIndex.BOOT:                   
                    mProgress.showProgress(option);
                    ret = mCmd.excuteCmdFlashBoot(option.path[0]);
                    option.state[0] = ret == true ? (int)Enums.WorkState.SUCC : (int)Enums.WorkState.ERROR;
                    break;
                case Enums.PartitionIndex.SYSTEM:                  
                    mProgress.showProgress(option);
                    ret = mCmd.excuteCmdFlashSystem(option.path[0]);
                    option.state[0] = ret == true ? (int)Enums.WorkState.SUCC : (int)Enums.WorkState.ERROR;
                    break;
                case Enums.PartitionIndex.MODEM:               
                    mProgress.showProgress(option);
                    ret = mCmd.excuteCmdFlashModem(option.path[0]);
                    option.state[0] = ret == true ? (int)Enums.WorkState.SUCC : (int)Enums.WorkState.ERROR;
                    break;
                case (Enums.PartitionIndex.SPLASH):                 
                    mProgress.showProgress(option);
                    ret = mCmd.excuteCmdFlashSplash(option.path[0]);
                    option.state[0] = ret == true ? (int)Enums.WorkState.SUCC : (int)Enums.WorkState.ERROR;
                    break;
                case (Enums.PartitionIndex.USERDATA):               
                    mProgress.showProgress(option);
                    ret = mCmd.excuteCmdFlashUserdata(option.path[0]);
                    option.state[0] = ret == true ? (int)Enums.WorkState.SUCC : (int)Enums.WorkState.ERROR;
                    break;
                case (Enums.PartitionIndex.RECOVERY):                
                    mProgress.showProgress(option);
                    ret = mCmd.excuteCmdFlashRecovery(option.path[0]);
                    option.state[0] = ret == true ? (int)Enums.WorkState.SUCC : (int)Enums.WorkState.ERROR;
                    break;
                case (Enums.PartitionIndex.ALL):
                    ret = true;                   
                    mProgress.showProgress(option);
                    ret &= mCmd.excuteCmdFlashAboot(option.path[0]);
                    option.state[0] = ret == true ? (int)Enums.WorkState.SUCC : (int)Enums.WorkState.ERROR;

                    ret &= mCmd.excuteCmdFlashBoot(option.path[1]);
                    option.state[1] = ret == true ? (int)Enums.WorkState.SUCC : (int)Enums.WorkState.ERROR;
              
                    ret &= mCmd.excuteCmdFlashSystem(option.path[2]);
                    option.state[2] = ret == true ? (int)Enums.WorkState.SUCC : (int)Enums.WorkState.ERROR;

                    ret &= mCmd.excuteCmdFlashModem(option.path[3]);
                    option.state[3] = ret == true ? (int)Enums.WorkState.SUCC : (int)Enums.WorkState.ERROR;

                    ret &= mCmd.excuteCmdFlashSplash(option.path[4]);
                    option.state[4] = ret == true ? (int)Enums.WorkState.SUCC : (int)Enums.WorkState.ERROR;

                    ret &= mCmd.excuteCmdFlashUserdata(option.path[5]);
                    option.state[5] = ret == true ? (int)Enums.WorkState.SUCC : (int)Enums.WorkState.ERROR;

                    ret &= mCmd.excuteCmdFlashRecovery(option.path[6]);
                    option.state[6] = ret == true ? (int)Enums.WorkState.SUCC : (int)Enums.WorkState.ERROR;
                    break;
            }

            mIntfResult.setResult(Enums.CmdType.FASTBOOT, ret);
            Thread.Sleep(1000);
            mProgress.dismissProgress(0);

        }



    }
}
