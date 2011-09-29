using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using Microsoft.Xna.Framework;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Threading;

namespace nettest
{
    public class Client
    {
        Socket _socket;
        int counter = 0;
        bool isConnected = false;
        Timer timer;
        int attempts = 0;
        public void Connect (IAsyncResult result)
        {
            isConnected = true;
        }
        public Client(IPEndPoint endPoint)
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            TryConnect();            
        }
        void Retry(object state)
        {            
            if (_socket.Connected == false)
            {
                ++attempts;
                TryConnect();
            }
        }
        public void TryConnect()
        {
            _socket.BeginConnect(new IPEndPoint(IPAddress.Loopback, 8024), new AsyncCallback(Connect), _socket);
            if (timer == null)
            {
                timer = new Timer(new TimerCallback(Retry), timer, 1500, 0);
            }
            else
            {
                timer.Change(1500, 0);
            }
        }
        public void Update(GameTime gameTime)
        {
            counter += gameTime.ElapsedGameTime.Milliseconds;
            if (counter > 1000)
            {               
                if (_socket.Connected)
                {
                    DataChunk chunk = new DataChunk();
                    chunk.strData = String.Format("{0} ms have elapsed, connected after {1} attempts", gameTime.TotalGameTime.Milliseconds, attempts);
                    IFormatter formatter = new BinaryFormatter();
                    byte[] data = System.Text.Encoding.ASCII.GetBytes(chunk.strData);                      
                    _socket.Send(data);
                }
                counter = 0;
            }
        }
        public void Disconnect()
        {
            _socket.Disconnect(true);
        }
    }
}
