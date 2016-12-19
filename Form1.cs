using System;
using System.Windows.Forms;
using System.Threading;

namespace android_tool
{
    public partial class Form1 : Form, InterfaceOutput, InterfaceResult
    {
        private static String PATH = Environment.CurrentDirectory;
        private static String PATH_SRC = PATH + @"\src\";
        private static String PATH_NET = @"\\192.168.1.200\ouyanghy\out\";

        const int WM_SYSCOMMAND = 0x0112;
        const int SC_CLOSE = 0xF060;

        private static SynchronizationContext mSyn;
        private Cmd mCmd;

        private MessageDialog mMessage;
        private MProgressBar mProgressBarFlash;
        private MProgressBar mProgressBarUpdate;
        private MProgressBar mProgressBarApp;
        private CmdsThread mThreadCmd;
        private UsbDeviceState mDeviceState;
        private ProgressBar progressBarCur;
        private ProgressBar progressBarTotal;
        private FormLog mFormLog;
        private bool mIsRoot = false;

        public void initWindow()
        {
            //device
            buttonExRemount.setText(Enums.ButtonName.REMOUNT);
            buttonExRemount.setButtonClick(new Enums.ptr_func_flash(buttonExRemount_Click));

            buttonExReboot.setText(Enums.ButtonName.REBOOT);
            buttonExReboot.setButtonClick(new Enums.ptr_func_flash(buttonExReboot_Click));

            buttonExFastReboot.setText(Enums.ButtonName.FASTBOOT_REBOOT);
            buttonExFastReboot.setButtonClick(new Enums.ptr_func_flash(buttonExFastReboot_Click));

            buttonExBootloader.setText(Enums.ButtonName.BOOTLOADER);
            buttonExBootloader.setButtonClick(new Enums.ptr_func_flash(buttonExBootloader_Click));

            buttonExCmdline.setText(Enums.ButtonName.CMDLINE);
            buttonExCmdline.setButtonClick(new Enums.ptr_func_flash(buttonExCmdline_Click));

            //flash
            buttonExAboot.setText(Enums.ButtonName.ABOOT);
            buttonExAboot.setButtonClick(new Enums.ptr_func_flash(buttonExAboot_Click));

            buttonExBoot.setText(Enums.ButtonName.BOOT);
            buttonExBoot.setButtonClick(new Enums.ptr_func_flash(buttonExBoot_Click));

            buttonExSystem.setText(Enums.ButtonName.SYSTEM);
            buttonExSystem.setButtonClick(new Enums.ptr_func_flash(buttonExSystem_Click));

            buttonExModem.setText(Enums.ButtonName.MODEM);
            buttonExModem.setButtonClick(new Enums.ptr_func_flash(buttonExModem_Click));

            buttonExSplash.setText(Enums.ButtonName.SPLASH);
            buttonExSplash.setButtonClick(new Enums.ptr_func_flash(buttonExSplash_Click));

            buttonExUserdata.setText(Enums.ButtonName.USERDATA);
            buttonExUserdata.setButtonClick(new Enums.ptr_func_flash(buttonExUserdata_Click));

            buttonExRecovery.setText(Enums.ButtonName.RECOVERY);
            buttonExRecovery.setButtonClick(new Enums.ptr_func_flash(buttonExRecovery_Click));

            buttonExAll.setText(Enums.ButtonName.ALL);
            buttonExAll.setButtonClick(new Enums.ptr_func_flash(buttonExAll_Click));
            //key
            buttonExBack.setText(Enums.ButtonName.BACK);
            buttonExBack.setButtonClick(new Enums.ptr_func_flash(buttonExBack_Click));

            buttonExHome.setText(Enums.ButtonName.HOME);
            buttonExHome.setButtonClick(new Enums.ptr_func_flash(buttonExHome_Click));

            buttonExPower.setText(Enums.ButtonName.POWER);
            buttonExPower.setButtonClick(new Enums.ptr_func_flash(buttonExPower_Click));
            //update system
            buttonExFrameware.setText(Enums.ButtonName.FRAMEWARE);
            buttonExFrameware.setButtonClick(new Enums.ptr_func_flash(buttonExFrameware_Click));

            buttonExScan.setText(Enums.ButtonName.SCAN);
            buttonExScan.setButtonClick(new Enums.ptr_func_flash(buttonExScan_Click));

            buttonExLancher.setText(Enums.ButtonName.LANCHER);
            buttonExLancher.setButtonClick(new Enums.ptr_func_flash(buttonExLancher_Click));

            buttonExCamera.setText(Enums.ButtonName.CAMERA);
            buttonExCamera.setButtonClick(new Enums.ptr_func_flash(buttonExCamera_Click));
            //apk
            buttonExBusybox.setText(Enums.ThirdAppFileName.BUSYBOX);
            buttonExBusybox.setButtonClick(new Enums.ptr_func_flash(buttonExBusybox_Click));

            buttonExDisplay.setText(Enums.ThirdAppFileName.DISPLAY);
            buttonExDisplay.setButtonClick(new Enums.ptr_func_flash(buttonExDisplay_Click));

            buttonExPing.setText(Enums.ThirdAppFileName.PING);
            buttonExPing.setButtonClick(new Enums.ptr_func_flash(buttonExPing_Click));

            buttonExExplore.setText(Enums.ThirdAppFileName.EXPLORE);
            buttonExExplore.setButtonClick(new Enums.ptr_func_flash(buttonExExplore_Click));

            buttonExSdlgui4.setText(Enums.ThirdAppFileName.SDLGUI4);
            buttonExSdlgui4.setButtonClick(new Enums.ptr_func_flash(buttonExSdlGui4_Click));

            buttonExSdlgui5.setText(Enums.ThirdAppFileName.SDLGUI5);
            buttonExSdlgui5.setButtonClick(new Enums.ptr_func_flash(buttonExSdlgui5_Click));

            buttonExSerial.setText(Enums.ThirdAppFileName.SERIAL);
            buttonExSerial.setButtonClick(new Enums.ptr_func_flash(buttonExSerial_Click));

            tabControlEx1.setTagName();
         
        }

        public void initProgressBar()
        {
            progressBarCur = progressExApp.progressBarExCur;
            progressBarTotal = progressExApp.progressBarExTotal;
            mProgressBarApp = new MProgressBar(mSyn, progressBarCur, progressBarTotal);

            progressBarCur = progressExFlash.progressBarExCur;
            progressBarTotal = progressExFlash.progressBarExTotal;
            mProgressBarFlash = new MProgressBar(mSyn, progressBarCur, progressBarTotal);

            progressBarCur = progressExUpdate.progressBarExCur;
            progressBarTotal = progressExUpdate.progressBarExTotal;
            mProgressBarUpdate = new MProgressBar(mSyn, progressBarCur, progressBarTotal);
        }

        public void initUserInfo()
        {
            mIsRoot = new UserInfo().initUserInfo();
            if (mIsRoot == true)
            {
                checkBoxLog.Checked = true;
                textBoxUser.Enabled = false;
                textBoxPwd.Enabled = false;
                buttonLogin.Enabled = false;
            }
        }

        public Form1()
        {
            InitializeComponent();
            mFormLog = new FormLog();
           
            mCmd = new Cmd(this);
            mMessage = new MessageDialog();
            mSyn = SynchronizationContext.Current;

            initWindow();
            initProgressBar();
            initUserInfo();

            mThreadCmd = new CmdsThread(mCmd,mMessage,this);
            mDeviceState = new UsbDeviceState(mSyn, mCmd, tabControlEx1);
            mDeviceState.start();

            
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_SYSCOMMAND && (int)m.WParam == SC_CLOSE)
            {
                mDeviceState.pause();
                mDeviceState.stop();
            }

            base.WndProc(ref m);
        }

        public void showText(string s)
        {
            if (s != null)
            {
                Console.WriteLine("s:" + s);
                mFormLog.showLog(s);
               
            }

        }

        private void buttonExPower_Click(object sender, EventArgs e)
        {
            mCmd.excuteCmdPower();
        }

        private void buttonExBack_Click(object sender, EventArgs e)
        {
            mCmd.excuteCmdBack();
        }

        private void buttonExHome_Click(object sender, EventArgs e)
        {
            mCmd.excuteCmdHome();
        }

        private void buttonExCmdline_Click(object sender, EventArgs e)
        {
            mCmd.excuteCmdCmdline();
        }

        private void buttonExBusybox_Click(object sender, EventArgs e)
        {
            bool adbState = mCmd.excuteCmdGetAdbState();
            if (!adbState)
            {
                MessageBox.Show(Enums.Error.ADB, Enums.Title.ERROR);
                return;
            }
            bool state = mCmd.excuteCmdUpdate(PATH_SRC + Enums.ThirdAppFileName.BUSYBOX, Enums.AndroidPath.SYSTEM_BIN + Enums.ThirdAppFileName.BUSYBOX, Enums.LinuxPermission.RWX_RWX_RWX);
            if (state == false)
            {
                MessageBox.Show(Enums.Error.INSTALL, Enums.Title.ERROR);
            }

        }
        private void buttonExDisplay_Click(object sender, EventArgs e)
        {
            bool adbState = mCmd.excuteCmdGetAdbState();
            if (!adbState)
            {
                MessageBox.Show(Enums.Error.ADB, Enums.Title.ERROR);
                return;
            }

           
            mThreadCmd.startCmdInstallThread(PATH_SRC + Enums.ThirdAppFileName.DISPLAY, mProgressBarApp);

        }

        private void buttonExWireless_Click(object sender, EventArgs e)
        {
            bool adbState = mCmd.excuteCmdGetAdbState();
            if (!adbState)
            {
                MessageBox.Show(Enums.Error.ADB, Enums.Title.ERROR);
                return;
            }
         
            mThreadCmd.startCmdInstallThread(PATH_SRC + Enums.ThirdAppFileName.ADBWIRELESS, mProgressBarApp);

        }

        private void buttonExSerial_Click(object sender, EventArgs e)
        {
            bool adbState = mCmd.excuteCmdGetAdbState();
            if (!adbState)
            {
                MessageBox.Show(Enums.Error.ADB, Enums.Title.ERROR);
                return;
            }
            mThreadCmd.startCmdInstallThread(PATH_SRC + Enums.ThirdAppFileName.SERIAL, mProgressBarApp);

        }

        private void buttonExExplore_Click(object sender, EventArgs e)
        {
            bool adbState = mCmd.excuteCmdGetAdbState();
            if (!adbState)
            {
                MessageBox.Show(Enums.Error.ADB, Enums.Title.ERROR);
                return;
            }
            mThreadCmd.startCmdInstallThread(PATH_SRC + Enums.ThirdAppFileName.EXPLORE, mProgressBarApp);

        }

        private void buttonExPing_Click(object sender, EventArgs e)
        {
            bool adbState = mCmd.excuteCmdGetAdbState();
            if (!adbState)
            {
                MessageBox.Show(Enums.Error.ADB, Enums.Title.ERROR);
                return;
            }
            mThreadCmd.startCmdInstallThread(PATH_SRC + Enums.ThirdAppFileName.PING, mProgressBarApp);

        }

        private void buttonExSdlGui4_Click(object sender, EventArgs e)
        {
            bool adbState = mCmd.excuteCmdGetAdbState();
            if (!adbState)
            {
                MessageBox.Show(Enums.Error.ADB, Enums.Title.ERROR);
                return;
            }
            mThreadCmd.startCmdInstallThread(PATH_SRC + Enums.ThirdAppFileName.SDLGUI4, mProgressBarApp);

        }

        private void buttonExSdlgui5_Click(object sender, EventArgs e)
        {
            bool adbState = mCmd.excuteCmdGetAdbState();
            if (!adbState)
            {
                MessageBox.Show(Enums.Error.ADB, Enums.Title.ERROR);
                return;
            }
            mThreadCmd.startCmdInstallThread(PATH_SRC + Enums.ThirdAppFileName.SDLGUI5, mProgressBarApp);

        }

        private void buttonExFrameware_Click(object sender, EventArgs e)
        {
            bool adbState = mCmd.excuteCmdGetAdbState();
            if (!adbState)
            {
                MessageBox.Show(Enums.Error.ADB, Enums.Title.ERROR);
                return;
            }

            CmdOption option = new CmdOption(2);

            option.path[0] = PATH_NET + Enums.AndroidPath.SYSTEM_FRAMEWORK + Enums.UpdateFileName.FRAMEWORK2_JAR;
            option.dst[0] = Enums.AndroidPath.SYSTEM_FRAMEWORK + Enums.UpdateFileName.FRAMEWORK2_JAR;
            option.permission[0] = Enums.LinuxPermission.NO_CHANGE;
            option.path[1] = PATH_NET + @Enums.AndroidPath.SYSTEM_FRAMEWORK + Enums.UpdateFileName.FRAMEWORK2_JAR;
            option.dst[1] = Enums.AndroidPath.SYSTEM_FRAMEWORK + Enums.UpdateFileName.FRAMEWORK2_JAR;
            option.permission[1] = Enums.LinuxPermission.NO_CHANGE;

            mThreadCmd.startCmdUpdateThread(option, mProgressBarUpdate);
        }

        private void buttonExScan_Click(object sender, EventArgs e)
        {
            bool adbState = mCmd.excuteCmdGetAdbState();
            if (!adbState)
            {
                MessageBox.Show(Enums.Error.ADB, Enums.Title.ERROR);
                return;
            }

            CmdOption option = new CmdOption(2);
            option.path[0] = PATH_NET + Enums.AndroidPath.SYSTEM_APP + Enums.UpdateFileName.SCAN_APK;
            option.dst[0] = Enums.AndroidPath.SYSTEM_APP + Enums.UpdateFileName.SCAN_APK;
            option.permission[0] = Enums.LinuxPermission.NO_CHANGE;
            option.path[1] = PATH_NET + Enums.AndroidPath.SYSTEM_APP + Enums.UpdateFileName.SCAN_ODEX;
            option.dst[1] = Enums.AndroidPath.SYSTEM_APP + Enums.UpdateFileName.SCAN_ODEX;
            option.permission[1] = Enums.LinuxPermission.NO_CHANGE;

            mThreadCmd.startCmdUpdateThread(option, mProgressBarUpdate);
        }

        private void buttonExLancher_Click(object sender, EventArgs e)
        {
            bool adbState = mCmd.excuteCmdGetAdbState();
            if (!adbState)
            {
                MessageBox.Show(Enums.Error.ADB, Enums.Title.ERROR);
                return;
            }
            CmdOption option = new CmdOption(2);
            option.path[0] = PATH_NET + Enums.AndroidPath.SYSTEM_PRIV_APP + Enums.UpdateFileName.LANCHER2_APK;
            option.dst[0] = Enums.AndroidPath.SYSTEM_PRIV_APP + Enums.UpdateFileName.LANCHER2_APK;
            option.permission[0] = Enums.LinuxPermission.NO_CHANGE;
            option.path[1] = PATH_NET + Enums.AndroidPath.SYSTEM_APP + Enums.UpdateFileName.LANCHER2_ODEX;
            option.dst[1] = Enums.AndroidPath.SYSTEM_APP + Enums.UpdateFileName.LANCHER2_ODEX;
            option.permission[1] = Enums.LinuxPermission.NO_CHANGE;

            mThreadCmd.startCmdUpdateThread(option, mProgressBarUpdate);
        }

        private void buttonExCamera_Click(object sender, EventArgs e)
        {
            bool adbState = mCmd.excuteCmdGetAdbState();
            if (!adbState)
            {
                MessageBox.Show(Enums.Error.ADB, Enums.Title.ERROR);
                return;
            }
            CmdOption option = new CmdOption(1);
            option.path[0] = PATH_NET + Enums.AndroidPath.SYSTEM_LIB + Enums.UpdateFileName.CAMERA_SERVICE;
            option.dst[0] = Enums.AndroidPath.SYSTEM_LIB + Enums.UpdateFileName.CAMERA_SERVICE;
            option.permission[0] = Enums.LinuxPermission.NO_CHANGE;
            mThreadCmd.startCmdUpdateThread(option, mProgressBarUpdate);
        }

      

        public void setResult(int cmdType, bool b)
        {
            mDeviceState.resume();
            switch (cmdType)
            {
                case Enums.CmdType.ADB:
                    /*                    if (b)
                                            mMessage.ShowMessageBoxTimeout(Enums.Error.ADB, Enums.Title.ERROR, 2000);
                                        else
                                        mMessage.ShowMessageBoxTimeout(Enums.Error.ADB, Enums.Title.ERROR,2000);
                      */
                    break;
                case Enums.CmdType.FASTBOOT:
                    if (b)
                        mMessage.ShowMessageBoxTimeout(Enums.Succ.FLASH, Enums.Title.TIP, 3000);
                    else
                        mMessage.ShowMessageBoxTimeout(Enums.Error.FLASH, Enums.Title.ERROR, 3000);
                    break;
                case Enums.CmdType.APK:
                    if (b)
                        mMessage.ShowMessageBoxTimeout(Enums.Succ.INSTALL, Enums.Title.TIP, 3000);
                    else
                        mMessage.ShowMessageBoxTimeout(Enums.Error.INSTALL, Enums.Title.ERROR, 3000);

                    break;
                case Enums.CmdType.UPDATE:
                    if (b)
                        mMessage.ShowMessageBoxTimeout(Enums.Succ.UPDATE, Enums.Title.TIP, 3000);
                    else
                        mMessage.ShowMessageBoxTimeout(Enums.Error.UPDATE, Enums.Title.ERROR, 3000);

                    break;
            }

        }

        private void buttonExRemount_Click(object sender, EventArgs e)
        {
            mCmd.excuteCmdRemount();
        }

        private void buttonExReboot_Click(object sender, EventArgs e)
        {
            Console.WriteLine("buttonExReboot_Click");
            mCmd.excuteCmdReboot();
        }



        private void buttonExFastReboot_Click(object sender, EventArgs e)
        {
            if(mCmd.excuteCmdGetFastState())
                mCmd.excuteCmdFastbootReboot();
        }



        private void buttonExBootloader_Click(object sender, EventArgs e)
        {
            mCmd.excuteCmdRebootBootloader();
        }

        private void buttonExSystem_Click(object sender, EventArgs e)
        {

            bool state = mCmd.excuteCmdGetFastState();
            if (state == true)
            {
                mDeviceState.pause();
                mThreadCmd.startCmdFlashThread(Enums.PartitionIndex.SYSTEM, mProgressBarFlash);
            }
            else
            {
                MessageBox.Show(Enums.Error.FASTBOOT.ToString(), Enums.Title.ERROR);
            }


        }

        private void buttonExAboot_Click(object sender, EventArgs e)
        {
            //click fastboot reboot
            bool state = mCmd.excuteCmdGetFastState();
            if (state == true)
            {
                mDeviceState.pause();
                mThreadCmd.startCmdFlashThread(Enums.PartitionIndex.ABOOT, mProgressBarFlash);
            }
            else
            {
                MessageBox.Show(Enums.Error.FASTBOOT.ToString(), Enums.Title.ERROR);
            }

        }

        private void buttonExBoot_Click(object sender, EventArgs e)
        {

            bool state = mCmd.excuteCmdGetFastState();
            if (state == true)
            {
                mDeviceState.pause();
                mThreadCmd.startCmdFlashThread(Enums.PartitionIndex.BOOT, mProgressBarFlash);
            }
            else
            {
                MessageBox.Show(Enums.Error.FASTBOOT.ToString(), Enums.Title.ERROR);
            }

        }

        private void buttonExModem_Click(object sender, EventArgs e)
        {
            bool state = mCmd.excuteCmdGetFastState();
            if (state == true)
            {
                mDeviceState.pause();
                mThreadCmd.startCmdFlashThread(Enums.PartitionIndex.MODEM, mProgressBarFlash);
            }
            else
            {
                MessageBox.Show(Enums.Error.FASTBOOT.ToString(), Enums.Title.ERROR);
            }
        }

        private void buttonExSplash_Click(object sender, EventArgs e)
        {
            bool state = mCmd.excuteCmdGetFastState();
            if (state == true)
            {
                mDeviceState.pause();
                mThreadCmd.startCmdFlashThread(Enums.PartitionIndex.SPLASH, mProgressBarFlash);
            }
            else
            {
                MessageBox.Show(Enums.Error.FASTBOOT.ToString(), Enums.Title.ERROR);
            }
        }

        private void buttonExUserdata_Click(object sender, EventArgs e)
        {
            bool state = mCmd.excuteCmdGetFastState();
            if (state == true)
            {
                mDeviceState.pause();
                mThreadCmd.startCmdFlashThread(Enums.PartitionIndex.USERDATA, mProgressBarFlash);
            }
            else
            {
                MessageBox.Show(Enums.Error.FASTBOOT.ToString(), Enums.Title.ERROR);
            }
        }

        private void buttonExRecovery_Click(object sender, EventArgs e)
        {
            bool state = mCmd.excuteCmdGetFastState();
            if (state == true)
            {
                mDeviceState.pause();
                mThreadCmd.startCmdFlashThread(Enums.PartitionIndex.RECOVERY, mProgressBarFlash);
            }
            else
            {
                MessageBox.Show(Enums.Error.FASTBOOT.ToString(), Enums.Title.ERROR);
            }
        }

        private void buttonExAll_Click(object sender, EventArgs e)
        {
            bool state = mCmd.excuteCmdGetFastState();
            if (state == true)
            {
                mDeviceState.pause();
                mThreadCmd.startCmdFlashThread(Enums.PartitionIndex.INVAILD, mProgressBarFlash);
            }
            else
            {
                MessageBox.Show(Enums.Error.FASTBOOT.ToString(), Enums.Title.ERROR);
            }
        }



        private void buttonBrowseNet_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                textBoxNetRoute.Text = dialog.SelectedPath;
                PATH_NET = textBoxNetRoute.Text;

            }
        }

        private void buttonBrowseLocal_Click(object sender, EventArgs e)
        {

            FolderBrowserDialog dialog = new FolderBrowserDialog();        
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                textBoxLocalRoute.Text = dialog.SelectedPath;
                PATH_NET = textBoxLocalRoute.Text;

            }
        }

        private void buttonClearNet_Click(object sender, EventArgs e)
        {
            textBoxNetRoute.Text = "";
        }

        private void buttonClearLocal_Click(object sender, EventArgs e)
        {
            textBoxLocalRoute.Text = "";
        }

        private void checkBoxLog_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxLog.Checked)
                mFormLog.Show();
            else
                mFormLog.Hide();

        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            bool isRoot = new UserInfo().initUserInfo();
            Console.WriteLine("is root:" + isRoot);
            String usr = textBoxUser.Text;
            String pwd = textBoxPwd.Text;
            UserInfo info = new UserInfo();
            bool b = info.checkUserInfo(usr, pwd);
            if (b)
            {
                textBoxUser.Enabled = false;
                textBoxPwd.Enabled = false;
                buttonLogin.Enabled = false;
                info.saveUserInfo(usr,pwd);
            }

        }

        private void buttonQuit_Click(object sender, EventArgs e)
        {
            textBoxUser.Enabled = true;
            textBoxPwd.Enabled = true;
            buttonLogin.Enabled = true;
        }
    }
}
