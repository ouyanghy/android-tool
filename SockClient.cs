using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace android_tool
{
   
    class SockClient
    {
        private Socket mSock;
        public SockClient()
        {
            mSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void connect(String addr, int port)
        {
            IPAddress ip = IPAddress.Parse(addr) ;
            mSock.Connect(ip, port);
        }

        public void disconnect()
        {
            mSock.Shutdown(SocketShutdown.Both);
            mSock.Close();
        }


        public int read(byte []b, int len)
        {
            int offset = 0;
            int ret = 0;
            int size = 4096;
            while(offset < len)
            {

                if (offset + 4096 > len)
                {
                    size = len - offset;
                }
                else
                    size = 4096;

                ret = mSock.Receive(b, offset, size, 0);
                offset += ret;
                
            }
            return offset;
        }

        public int write(byte [] data, int len)
        {
            return mSock.Send(data, len, 0);
        }
    }
}
