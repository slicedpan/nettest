using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using Microsoft.Xna.Framework;

namespace nettest
{
    public class GameServer
    {
        int portNumber;
        Socket socket;
        public bool isActive = false;
        List<Socket> sockets = new List<Socket>();
        List<Connection> connections = new List<Connection>();
        public void Accept(IAsyncResult result)
        {
            Socket s = (Socket)result.AsyncState;
            connections.Add(new Connection(this, s.EndAccept(result)));
            s.BeginAccept(new AsyncCallback(Accept), s);
        }
        public void End(Connection connectionToEnd)
        {
            connections.Remove(connectionToEnd);
        }
        public GameServer(int port = 8024)
        {
            portNumber = port;            
        }
        public void Listen()
        {
            isActive = true;
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(new IPEndPoint(IPAddress.Any, 8024));
            socket.Listen(10);
            socket.BeginAccept(new AsyncCallback(Accept), socket); 
        }

        public String LastText = "";

    }
}
