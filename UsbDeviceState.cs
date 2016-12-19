using android_tool.pictures;
using System;
using System.Threading;

namespace android_tool
{
    class UsbDeviceState
    {
        private static SynchronizationContext mSyn;
        private Cmd mCmd;
        private bool bThreadDeviceStateWork = false;
        private bool bThreadDeviceStateContinue = false;
        private TabControlEx tag;
        private static String mDeviceState;
        Thread mThreadGetDeviceState;

        public UsbDeviceState(SynchronizationContext syn ,Cmd cmd, TabControlEx device)
        {
            mSyn = syn;
            mCmd = cmd;
            mDeviceState = Enums.DeviceState.DEVICE_ADB_START_SERVER;
            tag = device;
        }

        public void resume()
        {
            bThreadDeviceStateContinue = true;
        }
        public void pause()
        {
            bThreadDeviceStateContinue = false;
        }
        public void stop()
        {
            bThreadDeviceStateWork = false;
            bThreadDeviceStateContinue = false;
        }
        public void start()
        {
            if (bThreadDeviceStateWork)
                return;
            bThreadDeviceStateWork = true;
            bThreadDeviceStateContinue = true;
            mThreadGetDeviceState = new Thread(getDeviceState);
            mThreadGetDeviceState.Start();

        }
        //Thread get devices state
        public void getDeviceState()
        {
            mCmd.excuteCmdAdbStartServer();
            while (bThreadDeviceStateWork)
            {
                while (bThreadDeviceStateContinue)
                {
                    int state = (int)Enums.ConnectState.STATE_NONE_CONNECT;
                    bool adbState = mCmd.excuteCmdGetAdbState();
                    if (adbState)
                        state |= (int)Enums.ConnectState.STATE_ADB_CONNECT;
                    //exit quickly
                    if (bThreadDeviceStateWork == false)
                        break;
                    Thread.Sleep(2000);
                    bool fastbootState = mCmd.excuteCmdGetFastState();
                    if (fastbootState)
                        state |= (int)Enums.ConnectState.STATE_FAST_CONNECT;

                    mSyn.Post(displayDeviceState, state);
                    if (bThreadDeviceStateWork == false)
                        break;
                    Thread.Sleep(1000);
                }
                mSyn.Post(displayDeviceState, Enums.ConnectState.STATE_FLASH);
                Thread.Sleep(1000);
            }
        }



        //show device state
        public void displayDeviceState(Object get)
        {
            int state = (int)get;
            String s;
            //  Console.WriteLine("state:" + state);
            if (state == ((int)Enums.ConnectState.STATE_BOTH_CONNECT))
            {
                s = Enums.DeviceState.DEVICE_BOTH_CONNECT;
            }
            else if (state == ((int)Enums.ConnectState.STATE_ADB_CONNECT))
            {
                s = Enums.DeviceState.DEVICE_ADB_CONNECT;
            }
            else if (state == ((int)Enums.ConnectState.STATE_FAST_CONNECT))
            {
                s = Enums.DeviceState.DEVICE_FAST_CONNECT;
            }
            else if (state == ((int)Enums.ConnectState.STATE_FLASH))
            {
                s = Enums.DeviceState.DEVICE_FLASH;
            }
            else
            {
                s = Enums.DeviceState.DEVICE_NONE_CONNECT;
            }

            if (!s.Equals(mDeviceState))
            {
                mDeviceState = s;
                tag.setTagDeviceName(s);
            }
        }
    }
}
