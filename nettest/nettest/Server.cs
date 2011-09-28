using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace nettest
{
    public class Server
    {
        int portNumber;
        UdpClient udpServer;
        public Server(int port = 8024)
        {
            portNumber = port;
            udpServer = new UdpClient(8024);
        }
        public void Update()
        {
            
        }
    }
}
