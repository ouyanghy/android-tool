using System;
using System.IO;

namespace android_tool
{

    class UserInfo
    {
        private String PATH = Environment.CurrentDirectory + @"\";
        
        public UserInfo()
        {

        }

        public bool checkUserInfo(String usr, String pwd)
        {
            //Console.WriteLine("checkUserInfo");
            if (usr == null || pwd == null)
            {
                Console.WriteLine("NULL PTR");
                return false;
            }
           // Console.WriteLine("chec usr:" + usr.Equals(Enums.UserInfomation.USER));
           /// Console.WriteLine("chec pwd:" + pwd.Equals(Enums.UserInfomation.PASSWD));
           // Console.WriteLine("enum pwd len" + Enums.UserInfomation.PASSWD.Length);
           // Console.WriteLine("file pwd len" + pwd.Length);
           
            return usr.Equals(Enums.UserInfomation.USER) & pwd.Equals(Enums.UserInfomation.PASSWD);
        }

        public void saveUserInfo(String usr, String pwd)
        {
            FileInfo info = new FileInfo(PATH + Enums.UserInfomation.USERINFO_FILE_SAVE);
            if (info.Exists == false)
                info.Create();

            byte [] bs = System.Text.Encoding.Default.GetBytes(usr + "," +pwd + ",");

            FileStream writeStream = info.OpenWrite();
            writeStream.Write(bs, 0, bs.Length);
            writeStream.Close();
            Console.WriteLine("save user info path" + PATH + Enums.UserInfomation.USERINFO_FILE_SAVE);
        }

        public String loadUserInfo()
        {
            FileInfo info = new FileInfo(PATH + Enums.UserInfomation.USERINFO_FILE_SAVE);
            if (info.Exists == false)
                return null;

            byte[] bs = new byte[32]; 

            FileStream readStream = info.OpenRead();
            readStream.Read(bs, 0, 24);
            readStream.Close();
          
            return System.Text.Encoding.Default.GetString(bs);
        }

        public String getUser()
        {
            String info = loadUserInfo();
            if (info == null)
                return null;

            String []s = info.Split(',');
            if (s.Length > 1)
                return s[0];
            else
                return null;
        }

        public String getPasswd()
        {
            String info = loadUserInfo();
            if (info == null)
                return null;
            String[] s = info.Split(',');
            //Console.WriteLine("length:" + s.Length);
            // Console.WriteLine("s0:" + s[0]);
            // Console.WriteLine("s0:" + s[1]);
            if (s.Length > 1)
                return s[1];
            else
                return null;
        }

        public  bool initUserInfo()
        {
  
            String usr = getUser();
            String pwd = getPasswd();
           // Console.WriteLine("+-user:" + usr + ",pwd:" + pwd);
            bool b = checkUserInfo(usr, pwd);
            return b;

        }
    }
}
