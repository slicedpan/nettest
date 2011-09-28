using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using Microsoft.Xna.Framework;

namespace nettest
{
    public class Server
    {
        int portNumber;
        UdpClient udpServer;
        Socket socket;
        List<Socket> sockets = new List<Socket>();
        public void Accept(IAsyncResult result)
        {
            Socket s = (Socket)result.AsyncState;
            sockets.Add(s.EndAccept(result));
        }
        public Server(int port = 8024)
        {
            portNumber = port;
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(new IPEndPoint(IPAddress.Any, 8024));
            socket.Listen(10);
            socket.BeginAccept(new AsyncCallback(Accept), socket); 
        }
        String lastText = "";
        public String LastText
        {
            get
            {
                return lastText;
            }
        }
    }
}
