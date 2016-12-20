using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace android_tool
{
    public class FileParse
    {
        private const int MAX_TIME = 1024;

        public bool existString(String s, String contain)
        {
            Regex regx = new Regex(contain);
            Match match = regx.Match(s);
            if (match.Length <= 0)
                return false;
            else
            {
                return true;
            }
        }

        public bool existStringFromFile(String file, String src)
        {
           // bool ret = false;
            FileInfo info = new FileInfo(file);
            if (info.Exists == false)
                return false;

            FileStream fileReader = info.OpenRead();
            StreamReader streamReader = new StreamReader(fileReader);
            if (streamReader == null)
                return false;

            int times = 0;
            String s = "";
            do
            {
                s = streamReader.ReadLine();
                times++;
                Regex regAdb = new Regex(src);
                Match match = regAdb.Match(s);
                if (match.Length <= 0)
                    continue;
                else
                {
                    break;
                }

            } while (s != null && times < MAX_TIME);

            streamReader.Close();
            fileReader.Close();

            return false;
        }

        public String getLineString(String file, String contain)
        {
            String ret = null;
            FileInfo info = new FileInfo(file);
            if (info.Exists == false)
                return null;

            FileStream fileReader = info.OpenRead();
            StreamReader streamReader = new StreamReader(fileReader);
            if (streamReader == null)
                return null;

            int times = 0;
            String s = "";
            do
            {
                times++;
                s = streamReader.ReadLine();

                Regex regx = new Regex(contain);
                Match match = regx.Match(s);
                if (match.Length <= 0)
                    continue;
                else
                {
                    ret = s;
                    break;

                }
            } while (s != null && times < MAX_TIME);

            streamReader.Close();
            fileReader.Close();

            return s;
        }

        public String readLine(FileStream fileStream)
        {
            StreamReader streamReader = new StreamReader(fileStream);
            return streamReader.ReadLine();
        }

        public bool modifyString(String file, String src, String dst)
        {
            bool ret = false;
            FileInfo info = new FileInfo(file);
            if (info.Exists == false)
                return false;

            FileStream fileStream = info.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);

            int times = 0;
            int index = 0;
            String s = "";
            do
            {

                s = readLine(fileStream);
                Console.WriteLine("readline:" + s);
                if (s == null)
                    return false;
                
                times++;
                Regex regx = new Regex(src);
                Match match = regx.Match(s);

                if (match.Length <= 0)
                {

                }
                else
                {
                    byte[] buf = System.Text.Encoding.Default.GetBytes(dst + "\n");
                    fileStream.Seek(index, SeekOrigin.Begin);
                    fileStream.Write(buf, 0, buf.Length);
                    ret = true;
                    break;
                }
                if (s != null)
                {
                    index = (int)fileStream.Position;
                }
            } while (s != null && times < MAX_TIME);


            fileStream.Close();


            return ret;
        }



    }


}
