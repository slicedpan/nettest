using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace nettest
{
    public class Connection
    {
        GameServer _server;
        Socket _socket;
        byte[] buffer = new byte[256];
        public void ReceiveData (IAsyncResult result)
        {
            String text = System.Text.Encoding.ASCII.GetString(buffer);
            text = text.TrimEnd('\0');
            text = "Message from " + _socket.RemoteEndPoint + ": " + text;
            _server.LastText = text;
            _socket.BeginReceive(buffer, 0, 256, SocketFlags.None, new AsyncCallback(ReceiveData), _socket);
        }
        public Connection(GameServer server, Socket socket)
        {
            _socket = socket;
            _server = server;
            _socket.BeginReceive(buffer, 0, 256, SocketFlags.None, new AsyncCallback(ReceiveData), _socket);
            _socket.BeginDisconnect(false, new AsyncCallback(Disconnect), null);
        }
        public void Disconnect(IAsyncResult result)
        {
            _server.End(this);
        }
    }
}
