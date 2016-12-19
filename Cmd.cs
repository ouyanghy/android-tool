﻿using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;

namespace android_tool
{
    public class Cmd
    {

        private String TAG = "ClassCmd:";
        private InterfaceOutput mIntfShow;


        public Cmd(InterfaceOutput intf)
        {
            mIntfShow = intf;

        }

        public bool checkCmdExist()
        {
            return File.Exists(Enums.CmdType.CMD_ADB) & File.Exists(Enums.CmdType.CMD_FASTBOOT);
        }

        public async void writeErrorThread(Object obj)
        {
            StreamReader readStream = (StreamReader)obj;
            while (!readStream.EndOfStream)
            {
                String s = await  readStream.ReadLineAsync();
                s +="\r\n";
                if ((mIntfShow != null))
                    mIntfShow.showText(s);

            }
        }
        public CmdResult excuteCmd(String cmd, String para, bool show)
        {
            CmdResult result = new CmdResult();
            Process process = new Process();
            if (show)
                mIntfShow.showText( "$:" + cmd + " " + para + "\r\n");
            process.StartInfo.FileName = cmd;
            process.StartInfo.Arguments = para;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            //process.StartInfo.RedirectStandardInput = true;  // 重定向输入流 
            process.StartInfo.RedirectStandardOutput = true;  //重定向输出流 
            process.StartInfo.RedirectStandardError = true;  //重定向错误流 

            process.Start();
           

            Thread errorThreadReader = new Thread(new ParameterizedThreadStart(writeErrorThread));
            errorThreadReader.Start(process.StandardError);

            String output = "";
            StreamReader readStream = process.StandardOutput;
            while (!readStream.EndOfStream)
            {
                String s = readStream.ReadLine();
                s += "\r\n";
                if ((mIntfShow != null) && (show == true))
                    mIntfShow.showText(s);
                output += s;
            }
            //     

            process.WaitForExit();

            result.ret  = process.ExitCode;
            result.output = output;
            
          //  if (show)
           // Log( " return value:" + result.result);
            process.Close();
           
            return result;
        }
        //Logcat
        private void Log(String s)
        {
            Console.WriteLine(TAG + ":" + s);
        }
        private void Log(int s)
        {
            Console.WriteLine(TAG + ":" + s);
        }
        private void Log(bool s)
        {
            Console.WriteLine(TAG + ":" + s);
        }
        ///
        ///
        public void excuteCmdAdbStartServer()
        {
            excuteCmd(Enums.CmdType.CMD_ADB, "start-server", true);
        }

        public void excuteCmdReboot()
        {
            excuteCmd(Enums.CmdType.CMD_ADB, "reboot", true);
        }


        public void excuteCmdRebootBootloader()
        {
            excuteCmd(Enums.CmdType.CMD_ADB, "reboot bootloader", true);
        }

        public void excuteCmdRemount()
        {
            excuteCmd(Enums.CmdType.CMD_ADB, "remount", true);
        }

        public void excuteCmdFastbootReboot()
        {
            excuteCmd(Enums.CmdType.CMD_FASTBOOT, "reboot", true);
        }

        public bool excuteCmdFlashAboot(String s)
        {
            CmdResult result = new CmdResult() ;
            bool exist = File.Exists(s);
            if (exist)
                result = excuteCmd(Enums.CmdType.CMD_FASTBOOT, "flash aboot " + s, true);
            else
            {
                s += " is not exist";
                s += "\r\n";
                if ((mIntfShow != null))
                    mIntfShow.showText(s);
            }
            return exist & result.getResult() ;
        }

        public bool excuteCmdFlashBoot(String s)
        {
            CmdResult result = new CmdResult();
            bool exist = File.Exists(s);
            if (exist)
                result = excuteCmd(Enums.CmdType.CMD_FASTBOOT, "flash boot " + s, true);
            else
            {
                s += " is not exist";
                s += "\r\n";
                if ((mIntfShow != null))
                    mIntfShow.showText(s);
            }
            return exist & result.getResult();
        }

        public bool excuteCmdFlashSystem(String s)
        {
            bool exist = File.Exists(s);
            if (exist)
                excuteCmd(Enums.CmdType.CMD_FASTBOOT, "flash system " + s, true);
            else
            {
                s += " is not exist";
                s += "\r\n";
                if ((mIntfShow != null))
                    mIntfShow.showText(s);
            }
            return exist;
        }

        public bool excuteCmdFlashSplash(String s)
        {
            bool exist = File.Exists(s);
            if (exist)
                excuteCmd(Enums.CmdType.CMD_FASTBOOT, "flash splash " + s, true);
            else
            {
                s += " is not exist";
                s += "\r\n";
                if ((mIntfShow != null))
                    mIntfShow.showText(s);
            }
            return exist;
        }

        public bool excuteCmdFlashModem(String s)
        {
            bool exist = File.Exists(s);
            if (exist)
                excuteCmd(Enums.CmdType.CMD_FASTBOOT, "flash modem " + s, true);
            else
            {
                s += " is not exist";
                s += "\r\n";
                if ((mIntfShow != null))
                    mIntfShow.showText(s);
            }
            return exist;
        }

        public bool excuteCmdFlashUserdata(String s)
        {
            CmdResult result;
            bool exist = File.Exists(s);
            if (exist)
                result = excuteCmd(Enums.CmdType.CMD_FASTBOOT, "flash userdata " + s, true);
            else
            {
                s += " is not exist";
                s += "\r\n";
                if ((mIntfShow != null))
                    mIntfShow.showText(s);
            }
            return exist ;
        }

        public bool excuteCmdFlashRecovery(String s)
        {
            bool exist = File.Exists(s);
            if (exist)
                excuteCmd(Enums.CmdType.CMD_FASTBOOT, "flash recovery " + s, true);
            else
            {
                s += " is not exist";
                s += "\r\n";
                if ((mIntfShow != null))
                    mIntfShow.showText(s);
            }
            return exist;
        }

        public bool excuteCmdGetAdbState()
        {
            bool state = false;
            CmdResult result = excuteCmd(Enums.CmdType.CMD_ADB, "devices", false);
            String s = result.output;

            Regex regAdb = new Regex(@"\d{1,8}");
            Match match = regAdb.Match(s);
            if (match.Length > 0)
            {
                state = true;

            }
            else
            {
                state = false;
            }
            //  Log(Enums.CmdType.CMD_ADB + state);
            return state;
        }
        public bool excuteCmdGetFastState()
        {
            bool state = false;
            CmdResult result = excuteCmd(Enums.CmdType.CMD_FASTBOOT, "devices", false);
            String s = result.output;
            Regex regFastboot = new Regex(@"\d{1,8}");
            Match match = regFastboot.Match(s);
            if (match.Length > 0)
            {
                state = true;
            }
            else
            {
                state = false;
            }
            //   Log("fast:" + state);
            return state;

        }

        public void excuteCmdPower()
        {
            CmdResult result = excuteCmd(Enums.CmdType.CMD_ADB, "shell input keyevent 26", true);
        }

        public void excuteCmdBack()
        {
            CmdResult result = excuteCmd(Enums.CmdType.CMD_ADB, "shell input keyevent 4", true);
        }

        public void excuteCmdHome()
        {
            CmdResult result = excuteCmd(Enums.CmdType.CMD_ADB, "shell input keyevent 3", true);
        }
        public void excuteCmdCmdline()
        {
            CmdResult result = excuteCmd(Enums.CmdType.CMD_ADB, "shell cat /proc/cmdline", true);
        }
        public bool excuteCmdInstallThirdApp(String s)
        {
            bool exist = File.Exists(s);
            if (exist)
               excuteCmd(Enums.CmdType.CMD_ADB, "install -r " + s, true);
            else
            {
                s += " is not exist" ;
                s += "\r\n";
                if ((mIntfShow != null))
                    mIntfShow.showText(s);
            }

            return exist;
        }
        // to is include file name 
        public bool excuteCmdUpdate(String s, String to, String permission)
        {
            bool exist = File.Exists(s);
            if (exist)
            {
                excuteCmdRemount();
                excuteCmd(Enums.CmdType.CMD_ADB, "push " + s + " " + to, true);
                if (!permission.Equals(Enums.LinuxPermission.NO_CHANGE))
                    excuteCmd(Enums.CmdType.CMD_ADB, "shell chmod " + permission + " " + to, true);
            }
            else
            {
                s += " is not exist or path to is not exist " + to + "\r\n";             
                if ((mIntfShow != null))
                    mIntfShow.showText(s);
            }
               

           
            return exist;
        }

    }
    }