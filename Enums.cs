using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace android_tool
{
    public class Enums
    {
        public delegate void ptr_func_flash(object sender, EventArgs e);
        public delegate void ptr_func_search(object sender, EventArgs e);
        public delegate void ptr_func_pull(object sender, EventArgs e);
        public delegate void ptr_func_showLog(String s);

        public class Error
        {
            public const string FASTBOOT = "未进入Fastboot模式";
            public const string ADB = "未进入Adb模式";
            public const string FLASH = "烧录分区错误";
            public const string ABOOT = "烧录Aboot分区错误";
            public const string BOOT = "烧录Boot分区错误";
            public const string SYSTEM = "烧录System分区错误";
            public const string MODEM = "烧录Modem分区错误";
            public const string SPLASH = "烧录Splash分区错误";
            public const string USERDATA = "烧录UserData分区错误";
            public const string RECOVERY = "烧录Recovery分区错误";
            public const string INSTALL = "安装第三方错误";
            public const string UPDATE = "更新系统组件错误";
            public const string CMDNOTEXIST = "未检测到命令";
            public const string INVAILD_INPUT = "输入错误";
            public const string CMDLINE = "未获取到命令行参数";
            public const string BUILD_ID = "版本号修改失败";
            public const string BUILD_TYPE = "型号修改失败";
        }

        public class WorkState
        {
            public const int RUNNING = 0;
            public const int SUCC = 1;
            public const int ERROR = 2;
            public const int NO_COMPELETE = 3;
           
        };

        public class Succ
        {
            public const string INSTALL = "安装第三方成功";
            public const string UPDATE = "更新系统组件成功";
            public const string FLASH = "烧录成功";
            public const string SEARCH = "查找完成";
            public const string PULL = "导出完成\r\n正在打开导出文件夹......";
            public const string BUILD_ID = "版本号修改成功";
            public const string BUILD_TYPE = "型号修改成功";
        }

        public enum ConnectState
        {
            STATE_NONE_CONNECT = 0x00,
            STATE_ADB_CONNECT = 0x02,
            STATE_FAST_CONNECT = 0x04,
            STATE_BOTH_CONNECT = 0x06,
            STATE_FLASH = 0x07,
        }

        public class PartitionIndex
        {
            public const  int ABOOT = 0;
            public const int BOOT = 1;
            public const int SYSTEM = 2;
            public const int MODEM = 3;
            public const int SPLASH = 4;
            public const int USERDATA = 5;
            public const int RECOVERY = 6;
            public const int ALL = 7;
            public const int INVAILD = 8;
        }

        public class DeviceState
        {
            public const string DEVICE_BOTH_CONNECT = "adb已连接|fastboot已连接";
            public const string DEVICE_ADB_CONNECT =  "adb已连接";
            public const string DEVICE_FAST_CONNECT = "fastboot连接";
            public const string DEVICE_NONE_CONNECT = "设备未连接";
            public const string DEVICE_ADB_START_SERVER = "正在启动adb服务";
            public const string DEVICE_FLASH = "正在烧录";
        }

        public class PartitionFileName
        {
            public const string ABOOT = "emmc_appsboot.mbn";
            public const string BOOT = "boot.img";
            public const string SYSTEM = "system.img";
            public const string MODEM = "NON-HLOS.bin";
            public const string SPLASH = "splash.img";
            public const string USERDATA = "userdata.img";
            public const string RECOVERY = "recovery.img";
        }

        public class ThirdAppFileName
        {
            public const string BUSYBOX = "busybox";
            public const string ADBWIRELESS = "adb-wireless.apk";
            public const string DISPLAY = "display.apk";
            public const string EXPLORE = "explore.apk";
            public const string SERIAL = "serial.apk";
            public const string PING = "ping.apk";
            public const string SDLGUI4 = "SDLgui4.0.apk";
            public const string SDLGUI5 = "SDLgui5.0.apk";
            public const string MOER = "更多...";
        }

        public class AndroidPath
        {
            public const string SYSTEM = "system/";
            public const string SYSTEM_LIB = "system/lib/";
            public const string SYSTEM_APP = "system/app/";
            public const string SYSTEM_PRIV_APP = "system/priv-app/";
            public const string SYSTEM_BIN = "system/bin/";
            public const string SYSTEM_FRAMEWORK = "system/framework/";
            public const string SYSTEM_ETC = "system/etc/";
            public const string SYSTEM_VENDOR_LIB = "/system/vendor/lib/";
        }

        public class UpdateFileName
        {
            public const string CHARGER_FLAG = "charger_flag";
            public const string BUILD_PROP = "build.prop";
            public const string FRAMEWORK_JAR = "framework.jar";
            public const string FRAMEWORK_ODEX = "framework.odex";
            public const string FRAMEWORK2_JAR = "framework2.jar";
            public const string FRAMEWORK2_ODEX = "framework2.odex";
            public const string EXT_JAR = "ext.jar";
            public const string EXT_ODEX = "ext.odex";
            public const string LANCHER2_APK = "Launcher2.apk";
            public const string LANCHER2_ODEX = "Launcher2.odex";
            public const string HLV_APK = "HlvFactory.apk";
            public const string HLV_ODEX = "HlvFactory.odex";
            public const string SCAN_APK = "Scan.apk";
            public const string SCAN_ODEX = "Scan.odex";
            public const string VENDOR_SE4710_LIB = "libmmcamera_skuab_shinetech_gc0339.so";
            public const string VENDOR_N5600_LIB = "libmmcamera_n5600.so";
            public const string CAMERA_SERVICE = "libcameraservice.so ";

            public const string PERMISSION = "platform.xml";
        }

        public class LinuxPermission
        {
            public const string RWX_RWX_RWX = "777";
            public const string RW_RW_RWX = "667";
            public const string RW_RW_RW = "666";
            public const string RW_R_R = "644";
            public const string R_R_R = "444";
            public const string NO_CHANGE = "0";
        }

        public class Title
        {
            public const string ERROR = "错误提示";
            public const string TIP = "提示";
            public const string CMDLINE = "命令参数行";
        }

        public class CmdType
        {
            public const int ADB = 0;
            public const int FASTBOOT = 1;
            public const int APK = 2;
            public const int UPDATE = 3;
            public const int SEARCH = 4;
            public const int PULL = 5;
            public const int UNKNOWN = 6;
            private static String PATH = Environment.CurrentDirectory;
            public  static String CMD_ADB = PATH + @"\tools\" + @"adb.exe";
            public static String CMD_FASTBOOT = PATH + @"\tools\" + @"fastboot.exe";
           
        }

        public class Path
        {

            public static String PATH = Environment.CurrentDirectory;
            public static String  PATH_SRC = PATH + @"\src\";
            public static String CACHE = PATH + @"\cache";
            public const string USERINFO_FILE_SAVE = "user.info";
            public const string USERINFO_FILE_PATH = "path.info";
            public static string COMPONET = PATH + @"\componet.rc";
            public  static string BUILD = CACHE + "\\" +"build.prop";
        }


        public class ButtonName
        {
            public const string REBOOT = "Reboot";
            public const string FASTBOOT_REBOOT = "Fastboot Reboot";
            public const string REMOUNT = "Remount";
            public const string BOOTLOADER = "Reboot  Bootloader";
            public const string CMDLINE = "Cmdline";

            public const string ABOOT = "Aboot";
            public const string BOOT = "Boot";
            public const string SYSTEM = "System";
            public const string MODEM = "Modem";
            public const string SPLASH = "Splash";
            public const string USERDATA = "UserData";
            public const string RECOVERY = "Recovery";
            public const string ALL = "All";

            public const string POWER = "Power";
            public const string HOME = "Home";
            public const string BACK = "Back";

            public const string FRAMEWARE = "Framework";
            public const string SCAN = "Scan";
            public const string LANCHER = "Lancher";
            public const string CAMERA = "Camera";
            public const string BUILD_ID = "手机版本";
            public const string BUILD_TYPE = "手机型号";

            public const string CONNECT = "连接";
            public const string DISCONNECT = "断开连接";
            public const string SCREEN_POWER = "电源键";
        }

        public class PNG
        {
            public const string CMD = "command.png";
            public const string KEY = "button.png";
            public const string FRAMEWORK = "package.png";
            public const string APK = "apk.png";
        }

        public class TabPageName
        {
            public const string DEVICE_STATE = "设备状态";
            public const string FLASH = "烧录";
            public const string UPDATE = "组件";
            public const string APP = "应用";
            public const string SETTING = "设置";
        }

        public class UserInfomation
        {
            public const string USER = "root";
            public const string PASSWD = "84799123";
            
        }

        public class Key
        {
            public const string NAME = "name";
            public const string TYPE = "type";
            public const string SRC = "src";
            public const string DST = "dst";
            public const string PERMISSION = "permission";
            public const string INDEX = "index";
        }

       

    }
}
