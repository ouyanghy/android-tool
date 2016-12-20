using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace android_tool
{
    public class FileParseBuildProp
    {
        private FileParse mParse;
        private Cmd mCmd;
        public FileParseBuildProp(Cmd cmd)
        {
            mCmd = cmd;
            mParse = new FileParse();
        }

        public String getType()
        {
            mCmd.excuteCmdPull("/system/build.prop");
            FileInfo info = new FileInfo(Enums.Path.BUILD);
            if (info.Exists == false)
            {
                Console.WriteLine("getType failed 1 " + Enums.Path.BUILD);
                return null;
            }
            String s = mParse.getLineString(Enums.Path.BUILD, "ro.product.model");
            if (s == null)
            {
                Console.WriteLine("getType failed 2 " + Enums.Path.BUILD);
                return null;
            }
            else
            {

                if (s.Length > 17)
                    return s.Substring(17);
                else
                {
                    Console.WriteLine("getType failed 3 " + Enums.Path.BUILD);
                    return null;
                }
            }
        }

        public String getBuildId()
        {
            mCmd.excuteCmdPull("/system/build.prop");
            FileInfo info = new FileInfo(Enums.Path.BUILD);
            if (info.Exists == false)
            {
                Console.WriteLine("getBuildId failed 1 " + Enums.Path.BUILD);
                return null;
            }
            String s = mParse.getLineString(Enums.Path.BUILD, "ro.build.display.id");
            if (s == null)
            {
                Console.WriteLine("getBuildId failed 2 " + Enums.Path.BUILD);
                return null;
            }
            else
            {

                if (s.Length > 20)
                    return s.Substring(20);
                else
                {
                    Console.WriteLine("getBuildId failed 3 " + Enums.Path.BUILD);
                    return null;
                }
            }
        }

        public bool modifyType(String src, String m)
        {
            mCmd.excuteCmdPull(Enums.AndroidPath.SYSTEM + Enums.UpdateFileName.BUILD_PROP);
            bool ret = mParse.modifyString(Enums.Path.BUILD, @"ro\.product\.model=" + src, "ro.product.model=" + m);
            if (ret == false)
                return false;

          
            return mCmd.excuteCmdUpdate(Enums.Path.BUILD, Enums.AndroidPath.SYSTEM + Enums.UpdateFileName.BUILD_PROP, Enums.LinuxPermission.RW_R_R);
        }

        public bool modifyBuildId(String src, String m)
        {

            mCmd.excuteCmdPull(Enums.AndroidPath.SYSTEM + Enums.UpdateFileName.BUILD_PROP);
            bool ret = mParse.modifyString(Enums.Path.BUILD, @"ro\.build\.display\.id=" + src, "ro.build.display.id=" + m);
            if (ret == false)
                return false;

            return mCmd.excuteCmdUpdate(Enums.Path.BUILD, Enums.AndroidPath.SYSTEM + Enums.UpdateFileName.BUILD_PROP, Enums.LinuxPermission.RW_R_R);

        }
    }
}
