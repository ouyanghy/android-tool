using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace android_tool
{
    class UsbClient
    {
        private SockClient mClient;
        private Cmd mCmd;


        public UsbClient(Cmd cmd)
        {
            mClient = new SockClient();
            mCmd = cmd;
            mCmd.excuteCmdForward(1352 + "", 2235 + "");
        }

        public void connect()
        {
            mClient.connect("127.0.0.1", 1352);
        }

        public void disconnect()
        {
            mClient.disconnect();
        }


        public byte[] read(int len)
        {
            byte[] bs = new byte[len];

            int ret = mClient.read(bs, len);
           // Console.WriteLine("read len:" + ret);
            return bs;
        }

        public int write(byte[] data, int len)
        {
            return mClient.write(data, len);
        }

        // public 
        public void cmdExit()
        {
            byte[] data = new byte[1024];
            data[0] = (byte)'e';
            data[1] = (byte)'x';
            data[2] = (byte)'i';
            data[3] = (byte)'t';
            data[4] = (byte)'4';
            write(data, 1024);
        }

        public void cmdFrameBuffer()
        {
            byte[] data = new byte[1024];
            data[0] = (byte)'f';
            data[1] = (byte)'r';
            data[2] = (byte)'a';
            data[3] = (byte)'m';
            data[4] = (byte)'e';
            data[5] = (byte)'5';
            write(data, 1024);

        }

        public bool isFrameStart()
        {
            byte [] bs  = read(6);
           // Console.WriteLine("framestart value:" + bs[0] + "," + bs[1] + "," + bs[2] + "," + bs[3] + "," + bs[4] + "," + bs[5] + "bs:" + System.Text.Encoding.Default.GetString(bs));
            if (bs[0] == (byte)'s' &&
                bs[1] == (byte)'t' &&
                bs[2] == (byte)'a' &&
                bs[3] == (byte)'r' &&
                bs[4] == (byte)'t' &&
                bs[5] == (byte)'5'
                )
                return true;

            return false;
        }

        public bool isFrameEnd()
        {
            byte[] bs = read(4);
            if (bs[0] == (byte)'e' &&
                bs[1] == (byte)'n' &&
                bs[2] == (byte)'d' &&
                bs[3] == (byte)'3'
                )
                return true;

            return false;
        }

        public void feedServer()
        {
            byte[] data = new byte[1024];
            data[0] = (byte)'h';
            data[1] = (byte)'e';
            data[2] = (byte)'a';
            data[3] = (byte)'r';
            data[4] = (byte)'t';
            data[5] = (byte)'5';
            write(data, 1024);
        }

        public int setParam(int div)
        {
            if (div > 9 || div <= 0)
                return -1;
            byte[] data = new byte[1024];
            data[0] = (byte)'s';
            data[1] = (byte)'e';
            data[2] = (byte)'t';
            data[3] = (byte)'p';
            data[4] = (byte)'a';
            data[5] = (byte)'r';
            data[6] = (byte)'a';
            data[7] = (byte)'7';
            data[8] = (byte)(div + '0');
            return write(data, 1024);
        }

        public int[] getInfo()
        {
            byte[] data = new byte[1024];
            data[0] = (byte)'i';
            data[1] = (byte)'n';
            data[2] = (byte)'f';
            data[3] = (byte)'o';
            data[4] = (byte)'4';
            data[5] = (byte)'5';

            write(data, 1024);
            byte[] info = read(16);
            int[] ia = new int[3];
            ia[0]= info[0] << 24 | info[1] << 16 | info[2] << 8 | info[3];
            ia[1] = info[4] << 24 | info[5] << 16 | info[6] << 8 | info[7];
            ia[2] = info[8] << 24 | info[9] << 16 | info[10] << 8 | info[11];
            return ia;
        }
    }
}


