using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using Microsoft.Xna.Framework;

namespace nettest
{
    public class Client
    {
        UdpClient udpClient;
        int counter = 0;
        public Client(IPEndPoint endPoint)
        {            
            udpClient = new UdpClient(endPoint);
            udpClient.Connect(endPoint);
            Socket mySocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            mySocket.Bind(new IPEndPoint(IPAddress.Any, 8024));
            mySocket.Listen(10);
        }
        public void Update(GameTime gameTime)
        {
            counter += gameTime.ElapsedGameTime.Milliseconds;
            if (counter > 1000)
            {
                counter = 0;
                string name = String.Format("{0} ms have elapsed", gameTime.TotalGameTime.TotalMilliseconds);
                byte[] sdata = Encoding.ASCII.GetBytes(name);
                udpClient.Send(sdata, sdata.Length);
            }
        }
    }
}
