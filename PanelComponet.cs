using android_tool.pictures;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace android_tool
{
    class PanelComponet
    {
        private CmdsThread mCmdThread;
        private bool bOk = false;
        private FileParse mParse;
        private Hashtable mTable;
        private FlowLayoutPanel mLayoutApp = null,mLayoutFrameware;
        private Cmd mCmd;
        private MProgressBar mBar;
        public PanelComponet(FlowLayoutPanel l1, FlowLayoutPanel l2, Cmd cmd, CmdsThread th, MProgressBar bar)
        {
            mLayoutApp = l1;
            mLayoutFrameware = l2;
            mCmdThread = th;
            mCmd = cmd;
            mBar = bar;
            mParse = new FileParse();
            mTable = new Hashtable();
            initLoadSource();
            loadUi();
        }

        public void addComponet(CmdOption option)
        {
            addComponet(option);
        }

        private void addControl(CmdOption option)
        {
            ButtonEx button = new ButtonEx();
            button.setText(option.name);
            CmdFunc func = new CmdFunc(mCmd,mCmdThread,option,mBar);
            button.setButtonClick(func);
            if (option.type == Enums.CmdType.APK)
            {
                button.setBitmap(Enums.PNG.APK);
                mLayoutApp.Controls.Add(button);
            }
            else if (option.type == Enums.CmdType.UPDATE)
            {
                mLayoutFrameware.Controls.Add(button);
                button.setBitmap(Enums.PNG.FRAMEWORK);
            }
           
        }
       
        /*
         * type:0/1/2
         * name:
         * src:
         * dst:
         * permission:
         * index:
         */
        private bool initLoadSource()
        {
            FileInfo info = new FileInfo(Enums.Path.COMPONET);
            if (info.Exists == false)
                return false;

            FileStream stream = info.OpenRead();
            StreamReader reader = new StreamReader(stream);
            String s;
            bool ret;

            s = reader.ReadLine();
            while (s != null)
            {

                Console.WriteLine("read:" + s);
                ret = mParse.existString(s, "type->");
                
                if (ret == false)
                {
                    Console.WriteLine("invalid line info,read next");
                    s = reader.ReadLine();
                    continue;
                }

                ret = mParse.existString(s, "#");
                if (ret == true)
                {
                    //just comment
                    s = reader.ReadLine();
                    continue;
                }
                CmdOption option = parseToOption(s);
                if (mTable.ContainsKey(option.name))
                    mTable.Remove(option.name);

                mTable.Add(option.name, option);
                

                s = reader.ReadLine();
            }
          
            reader.Close();
            stream.Close();
            return true;
        }

        private void loadUi()
        {
            String bmp=Enums.PNG.APK;
            
            foreach(DictionaryEntry entry in mTable)
            {
                CmdOption option = (CmdOption)entry.Value;             
                addControl(option);
            }
           
        }

        private CmdOption parseToOption(String s)
        {
            string[] ar = s.Split(',');
            string sp ="->";
            
            String type = getValue(ar[0], Enums.Key.TYPE, sp);
            String name = getValue(ar[1], Enums.Key.NAME, sp);
            String src = getValue(ar[2], Enums.Key.SRC, sp);
            String dst = getValue(ar[3], Enums.Key.DST, sp);
            String permission = getValue(ar[4], Enums.Key.PERMISSION, sp);
            String index = getValue(ar[5], Enums.Key.INDEX, sp);
            Console.WriteLine("name:" + name + "src:" + src);
            int id = int.Parse(index);

            bool exist = mTable.ContainsKey(name);
            CmdOption option;
            if (!exist)
            {
                option = new CmdOption(id);
            }
            else
                option = (CmdOption)mTable[name];

            option.type = int.Parse(type);
            option.name = name;
            option.path[id - 1] = src;
            option.dst[id - 1] = dst;
            option.permission[id -1] = permission;
            
            return option;
        }

        private String getValue(String src, String key, String sp)
        {
            if (src == null)
                return null;

            Regex regx = new Regex(key);
            Match match = regx.Match(src);
            if (match.Length <= 1) return null;

           // String[] ar = src.Split(sp.ToCharArray(),StringSplitOptions.RemoveEmptyEntries);
           String [] ar = Regex.Split(src, sp);
            if (ar.Length < 2) return null;


            return ar[1];
        }
        public void add(FlowLayoutPanel layout, String name, String path)
        {

        }


    }
}
