using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace leap_form
{
    class LocalNetwork
    {
        private static byte[] mBuffer = new byte[1024];
        private static Socket mServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private static List<Socket> mClientsSockets = new List<Socket>();

        public static void initServer()
        {
            Console.WriteLine("Setting up server...");
            IPEndPoint iplocal = new IPEndPoint(IPAddress.Any, 23);
            mServerSocket.Bind(iplocal);
            mServerSocket.Listen(1);
            mServerSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);
        }

        private static void AcceptCallback(IAsyncResult AR)
        {
            Console.WriteLine("Connected to client");
            Socket socket = mServerSocket.EndAccept(AR);
            mClientsSockets.Add(socket);
            socket.BeginReceive(mBuffer, 0, mBuffer.Length, SocketFlags.None, new AsyncCallback(Receive), socket);
            mServerSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);
        }

        public static void SendData(byte[] data)
        {
            mServerSocket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(SendCallback), mServerSocket);
        }

        private static void Receive(IAsyncResult AR)
        {
            Socket socket = (Socket)AR.AsyncState;
            int received = socket.EndReceive(AR);
            byte[] dataBuf = new byte[received];
            Array.Copy(mBuffer, dataBuf, received);
            string text = Encoding.ASCII.GetString(dataBuf);
            Console.WriteLine("Received: " + text);
            string resp = string.Empty;
            if (text.ToLower() != "1")
            {
                resp = "Invalid command";
            }
            else
            {
                resp = DateTime.Now.ToLongTimeString();
            }
            byte[] data = Encoding.ASCII.GetBytes(resp);
            socket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(SendCallback), socket);
            socket.BeginReceive(mBuffer, 0, mBuffer.Length, SocketFlags.None, new AsyncCallback(Receive), socket);
        }

        private static void SendCallback(IAsyncResult AR)
        {
            Socket socket = (Socket)AR.AsyncState;
            socket.EndSend(AR);
        }

    }
}
