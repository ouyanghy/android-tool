using System;
using System.Windows.Forms;
using System.Threading;
using System.Text.RegularExpressions;

namespace android_tool
{
    public partial class FormMain : Form, InterfaceOutput, InterfaceResult
    {

        private static String PATH_FRAMEWORK = "";
        private static String PATH_IMG   = "";

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
        private FormFindList mFormFind;
        private bool mIsRoot = false;
        private bool mShowSearch = false;
        private UserInfo mInfo;
        private FormControl mFormControl;
        private ScreenCtrl mScreen;
     
        public void initWindow()
        {
            //device
            buttonExRemount.setText(Enums.ButtonName.REMOUNT);
            buttonExRemount.setButtonClick(new Enums.ptr_func_flash(buttonExRemount_Click));
            buttonExRemount.setBitmap(Enums.PNG.CMD);

            buttonExReboot.setText(Enums.ButtonName.REBOOT);
            buttonExReboot.setButtonClick(new Enums.ptr_func_flash(buttonExReboot_Click));
            buttonExReboot.setBitmap(Enums.PNG.CMD);

            buttonExFastReboot.setText(Enums.ButtonName.FASTBOOT_REBOOT);
            buttonExFastReboot.setButtonClick(new Enums.ptr_func_flash(buttonExFastReboot_Click));
            buttonExFastReboot.setBitmap(Enums.PNG.CMD);

            buttonExBootloader.setText(Enums.ButtonName.BOOTLOADER);
            buttonExBootloader.setButtonClick(new Enums.ptr_func_flash(buttonExBootloader_Click));
            buttonExBootloader.setBitmap(Enums.PNG.CMD);

            buttonExCmdline.setText(Enums.ButtonName.CMDLINE);
            buttonExCmdline.setButtonClick(new Enums.ptr_func_flash(buttonExCmdline_Click));
            buttonExCmdline.setBitmap(Enums.PNG.CMD);

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
            buttonExBack.setBitmap(Enums.PNG.KEY);

            buttonExHome.setText(Enums.ButtonName.HOME);
            buttonExHome.setButtonClick(new Enums.ptr_func_flash(buttonExHome_Click));
            buttonExHome.setBitmap(Enums.PNG.KEY);

            buttonExPower.setText(Enums.ButtonName.POWER);
            buttonExPower.setButtonClick(new Enums.ptr_func_flash(buttonExPower_Click));
            buttonExPower.setBitmap(Enums.PNG.KEY);

            tabControlEx1.setTagName();

            searchEx1.setFunc(buttonSearch);
            pullEx1.setFunc(buttonPull);

        }
        
        private void setFlowPanelScroll(FlowLayoutPanel panel)
        {
            panel.AutoScroll = false;
            panel.FlowDirection = FlowDirection.TopDown;
            panel.WrapContents = false;
            panel.HorizontalScroll.Maximum = 0; // 把水平滚动范围设成0就看不到水平滚动条了
            flowLayoutPanel1.AutoScroll = true; // 注意启用滚动的顺序，应是完成设置的最后一条语句
        }
        
        public void initProgressBar()
        {
            progressBarCur = progressExApp.progressBarExCur;
            progressBarTotal = progressExApp.progressBarExTotal;
            mProgressBarApp = new MProgressBar(mSyn, progressExApp);

            progressBarCur = progressExFlash.progressBarExCur;
            progressBarTotal = progressExFlash.progressBarExTotal;
            mProgressBarFlash = new MProgressBar(mSyn, progressExFlash);

           
        }

        public void initUserInfo()
        {
            mInfo = new UserInfo();
            mIsRoot = mInfo.isRoot();
            if (mIsRoot == true)
            {
                //   checkBoxLog.Checked = true;
                textBoxUser.Enabled = false;
                textBoxPwd.Enabled = false;
                buttonLogin.Enabled = false;

                textBoxUser.Text = mInfo.getUser();
                textBoxPwd.Text = "*******";

            }
            String path = mInfo.getPathFramework();
            if (path != null)
                textBoxRouteFramework.Text = path;

            path = mInfo.getPathImg();
            if (path != null)
                textBoxImgRoute.Text = path;

            mInfo.startListenTextBoxChange(textBoxImgRoute, textBoxRouteFramework);
        }

        public FormMain()
        {
            InitializeComponent();
            mFormLog = new FormLog();

            mCmd = new Cmd(this);
            mMessage = new MessageDialog();
            mSyn = SynchronizationContext.Current;

            initWindow();
            initProgressBar();
            initUserInfo();

            mThreadCmd = new CmdsThread(mCmd, mMessage, this);
            mDeviceState = new UsbDeviceState(mSyn, mCmd, tabControlEx1);
            mDeviceState.start();

            setFlowPanelScroll(panelPage4);
            //setFlowPanelScroll(panel)
            new PanelComponet(panelApp, panelFrameware, mCmd, mThreadCmd, mProgressBarApp);
            mScreen = new ScreenCtrl(mCmd, pictureBoxScreen);
            
            initScreenContrl();

        }

        private void initScreenContrl()
        {
            buttonExScreenConnect.setText(Enums.ButtonName.CONNECT);
            buttonExScreenConnect.setButtonClick(new Enums.ptr_func_flash(buttonScreenConnectClick));

            buttonExScreenExit.setText(Enums.ButtonName.DISCONNECT);
            buttonExScreenExit.setButtonClick(new Enums.ptr_func_flash(buttonScreenDisconnectClick));

            buttonExScreenUp.setText(Enums.ButtonName.SCREEN_POWER);
            buttonExScreenUp.setButtonClick(new Enums.ptr_func_flash(buttonScreenPowerClick));


        }
        private void buttonScreenConnectClick(object sender, EventArgs e)
        {
            mScreen.connect();
        }

        private void buttonScreenPowerClick(object sender, EventArgs e)
        {
            buttonExPower_Click(null, null);
        }
        private void buttonScreenDisconnectClick(object sender, EventArgs e)
        {
            mScreen.disconnect();
        }


        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_SYSCOMMAND && (int)m.WParam == SC_CLOSE)
            {
                mDeviceState.pause();
                mDeviceState.stop();
                mInfo.stopListenTextBoxChange();
                if (mScreen.getState())
                    mScreen.disconnect();
                Console.WriteLine("close");
            }

            base.WndProc(ref m);
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
            String result = mCmd.excuteCmdCmdline();
            if (result == null)
                result = Enums.Error.CMDLINE;
            MessageBox.Show(result, Enums.Title.CMDLINE);
        }

        private void buttonExBusybox_Click(object sender, EventArgs e)
        {
         
            bool adbState = mCmd.excuteCmdGetAdbState();
            if (!adbState)
            {
                MessageBox.Show(Enums.Error.ADB, Enums.Title.ERROR);
                return;
            }

            CmdOption option = new CmdOption(1);

            option.path[0] = Enums.Path.PATH_SRC + Enums.ThirdAppFileName.BUSYBOX;
            option.dst[0] = Enums.AndroidPath.SYSTEM_BIN + Enums.ThirdAppFileName.BUSYBOX;
            option.permission[0] = Enums.LinuxPermission.RWX_RWX_RWX;
            mThreadCmd.startCmdUpdateThread(option, mProgressBarUpdate);

        }
    
        private void buttonExAppMore_Click(object sender, EventArgs e)
        {
            bool adbState = mCmd.excuteCmdGetAdbState();
            if (!adbState)
            {
                MessageBox.Show(Enums.Error.ADB, Enums.Title.ERROR);
                return;
            }

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = "c:\\";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                mThreadCmd.startCmdInstallThread(dialog.FileName, mProgressBarApp);
            }
        }

       
        private String getFramewarePath()
        {
            String s = textBoxRouteFramework.Text;
            if (s.Substring(s.Length - 1).Equals(@"\"))
                return s;
            return s + @"\";
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
            PATH_FRAMEWORK = getFramewarePath();
            option.path[0] = PATH_FRAMEWORK + Enums.AndroidPath.SYSTEM_FRAMEWORK + Enums.UpdateFileName.FRAMEWORK2_JAR;
            option.dst[0] = Enums.AndroidPath.SYSTEM_FRAMEWORK + Enums.UpdateFileName.FRAMEWORK2_JAR;
            option.permission[0] = Enums.LinuxPermission.NO_CHANGE;
            option.path[1] = PATH_FRAMEWORK + @Enums.AndroidPath.SYSTEM_FRAMEWORK + Enums.UpdateFileName.FRAMEWORK2_JAR;
            option.dst[1] = Enums.AndroidPath.SYSTEM_FRAMEWORK + Enums.UpdateFileName.FRAMEWORK2_JAR;
            option.permission[1] = Enums.LinuxPermission.NO_CHANGE;

            mThreadCmd.startCmdUpdateThread(option, mProgressBarUpdate);
        }

     
        private void buttonExBuild_Click(object sender, EventArgs e)
        {
            new FormBuildId(mCmd).Show();
        }


        public void setResult(int cmdType, bool b)
        {
            mDeviceState.resume();
            switch (cmdType)
            {
                case Enums.CmdType.PULL:
                    mMessage.ShowMessageBoxTimeout(Enums.Succ.PULL, Enums.Title.TIP, 5000);
                    System.Diagnostics.Process.Start("explorer.exe", Enums.Path.CACHE);
                    break;

                case Enums.CmdType.SEARCH:
                    mMessage.ShowMessageBoxTimeout(Enums.Succ.SEARCH, Enums.Title.TIP, 5000);
                    mShowSearch = false;
                    break;
                case Enums.CmdType.ADB:

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
            if (mCmd.excuteCmdGetFastState())
                mCmd.excuteCmdFastbootReboot();
        }



        private void buttonExBootloader_Click(object sender, EventArgs e)
        {
            mCmd.excuteCmdRebootBootloader();
        }

        private CmdOption getPartitonOption(int index)
        {
            PATH_IMG = textBoxImgRoute.Text;
            CmdOption option = new CmdOption(1);
            option.index = index;
            switch (index)
            {
                case (Enums.PartitionIndex.ABOOT):
                    option.state[0] = Enums.WorkState.RUNNING;
                    option.path[0] = PATH_IMG + @"\" + Enums.PartitionFileName.ABOOT;
                    option.calcSize();

                    break;
                case Enums.PartitionIndex.BOOT:
                    option.state[0] = Enums.WorkState.RUNNING;
                    option.path[0] = PATH_IMG + @"\" + Enums.PartitionFileName.BOOT;
                    option.calcSize();
                    break;
                case Enums.PartitionIndex.SYSTEM:
                    option.state[0] = Enums.WorkState.RUNNING;
                    option.path[0] = PATH_IMG + @"\" + Enums.PartitionFileName.SYSTEM;
                    option.calcSize();
                    break;
                case Enums.PartitionIndex.MODEM:
                    option.state[0] = Enums.WorkState.RUNNING;
                    option.path[0] = PATH_IMG + @"\" + Enums.PartitionFileName.MODEM;
                    option.calcSize();
                    break;
                case (Enums.PartitionIndex.SPLASH):
                    option.state[0] = Enums.WorkState.RUNNING;
                    option.path[0] = PATH_IMG + @"\" + Enums.PartitionFileName.SPLASH;
                    option.calcSize();
                    break;
                case (Enums.PartitionIndex.USERDATA):
                    option.state[0] = Enums.WorkState.RUNNING;
                    option.path[0] = PATH_IMG + @"\" + Enums.PartitionFileName.USERDATA;
                    option.calcSize();
                    break;
                case (Enums.PartitionIndex.RECOVERY):
                    option.state[0] = Enums.WorkState.RUNNING;
                    option.path[0] = PATH_IMG + @"\" + Enums.PartitionFileName.RECOVERY;
                    option.calcSize();
                    break;
                case (Enums.PartitionIndex.ALL):
                    option = new CmdOption(7);
                    option.index = Enums.PartitionIndex.ALL;
                    option.path[0] = PATH_IMG + @"\" + Enums.PartitionFileName.ABOOT;
                    option.path[1] = PATH_IMG + @"\" + Enums.PartitionFileName.BOOT;
                    option.path[2] = PATH_IMG + @"\" + Enums.PartitionFileName.SYSTEM;
                    option.path[3] = PATH_IMG + @"\" + Enums.PartitionFileName.MODEM;
                    option.path[4] = PATH_IMG + @"\" + Enums.PartitionFileName.SPLASH;
                    option.path[5] = PATH_IMG + @"\" + Enums.PartitionFileName.USERDATA;
                    option.path[6] = PATH_IMG + @"\" + Enums.PartitionFileName.RECOVERY;

                    option.state[0] = Enums.WorkState.RUNNING;
                    option.state[1] = Enums.WorkState.RUNNING;
                    option.state[2] = Enums.WorkState.RUNNING;
                    option.state[3] = Enums.WorkState.RUNNING;
                    option.state[4] = Enums.WorkState.RUNNING;
                    option.state[5] = Enums.WorkState.RUNNING;
                    option.state[6] = Enums.WorkState.RUNNING;
                    option.calcSize();
                    break;
            }
            return option;

        }
        private void buttonExSystem_Click(object sender, EventArgs e)
        {

            bool state = mCmd.excuteCmdGetFastState();
            if (state == true)
            {
                mDeviceState.pause();
                mThreadCmd.startCmdFlashThread(getPartitonOption(Enums.PartitionIndex.SYSTEM), mProgressBarFlash);
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
                mThreadCmd.startCmdFlashThread(getPartitonOption(Enums.PartitionIndex.ABOOT), mProgressBarFlash);
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
                mThreadCmd.startCmdFlashThread(getPartitonOption(Enums.PartitionIndex.BOOT), mProgressBarFlash);
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
                mThreadCmd.startCmdFlashThread(getPartitonOption(Enums.PartitionIndex.MODEM), mProgressBarFlash);
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
                mThreadCmd.startCmdFlashThread(getPartitonOption(Enums.PartitionIndex.SPLASH), mProgressBarFlash);
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
                mThreadCmd.startCmdFlashThread(getPartitonOption(Enums.PartitionIndex.USERDATA), mProgressBarFlash);
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
                mThreadCmd.startCmdFlashThread(getPartitonOption(Enums.PartitionIndex.RECOVERY), mProgressBarFlash);
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
                mThreadCmd.startCmdFlashThread(getPartitonOption(Enums.PartitionIndex.ALL), mProgressBarFlash);
            }
            else
            {
                MessageBox.Show(Enums.Error.FASTBOOT.ToString(), Enums.Title.ERROR);
            }
        }



        private void buttonBrowseDirFramework_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                textBoxRouteFramework.Text = dialog.SelectedPath;            
            }
        }

        private void buttonBrowseDirImg_Click(object sender, EventArgs e)
        {

            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                textBoxImgRoute.Text = dialog.SelectedPath;
            }
        }

        private void buttonClearDirFramework_Click(object sender, EventArgs e)
        {
            textBoxRouteFramework.Text = "";
        }

        private void buttonClearDirImg_Click(object sender, EventArgs e)
        {
            textBoxImgRoute.Text = "";
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
            bool isRoot = mInfo.isRoot();
            Console.WriteLine("is root:" + isRoot);
            String usr = textBoxUser.Text;
            String pwd = textBoxPwd.Text;

            bool b = mInfo.checkUserInfo(usr, pwd);
            if (b)
            {
                textBoxUser.Enabled = false;
                textBoxPwd.Enabled = false;
                buttonLogin.Enabled = false;
                mInfo.saveUserInfo(usr, pwd);
            }

        }

        private void buttonQuit_Click(object sender, EventArgs e)
        {
            textBoxUser.Enabled = true;
            textBoxPwd.Enabled = true;
            buttonLogin.Enabled = true;
        }

        public void showText(string s)
        {
            if (s != null && !s.Equals(""))
            {
              
                mFormLog.showLog(s);

                if (mShowSearch == true)
                {
                    if (mFormFind != null)
                    {
                        Regex regAdb = new Regex(@"\$\:");
                        Match match = regAdb.Match(s);
                        Console.WriteLine("s:" +s + " len " +  match.Length);

                        if (s.Equals("\r\n") || s.Equals("\n"))
                            return;
                        if (match.Length <= 0)
                            mFormFind.addItem(s);
                    }
                }

            }

        }


        private void buttonSearch(object sender, EventArgs e)
        {
            if (mShowSearch == true)
            {
                return;
            }
            if (mCmd.excuteCmdGetAdbState() == false)
            {
                mMessage.ShowMessageBoxTimeout(Enums.Error.ADB, Enums.Title.ERROR, 2000);
                return;
            }

            if (searchEx1.textBoxSerach.Text.Length < 1)
            {
                mMessage.ShowMessageBoxTimeout(Enums.Error.INVAILD_INPUT, Enums.Title.ERROR, 2000);
                return;
            }
            if (mFormFind == null)
            {
                mFormFind = new FormFindList();
            }
            else
            {
                mFormFind.Close();
                mFormFind = new FormFindList();
            }

            mFormFind.Show();         

            if (mCmd.excuteCmdExistFile(Enums.AndroidPath.SYSTEM_BIN + Enums.ThirdAppFileName.BUSYBOX) == false)                          
                buttonExBusybox_Click(sender, e);
            mShowSearch = true;
            mThreadCmd.startCmdSearchThread(searchEx1.textBoxSerach.Text);


        }

        private void buttonPull(object sender, EventArgs e)
        {
            if (mCmd.excuteCmdGetAdbState() == false)
            {
                mMessage.ShowMessageBoxTimeout(Enums.Error.ADB, Enums.Title.ERROR, 2000);
                return;
            }

            if (pullEx1.textBoxPull.Text.Length < 1)
            {
                mMessage.ShowMessageBoxTimeout(Enums.Error.INVAILD_INPUT, Enums.Title.ERROR, 2000);
                return;
            }
            mThreadCmd.startCmdPullThread(pullEx1.textBoxPull.Text);


        }

        private void flowLayoutPanel14_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
