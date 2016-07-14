using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Sockets;

namespace TcpSocketServer
{
    class server
    {
        private const int BACKLOG = 5;    

        static void Main(string[] args)
        {
            int port = 5000;
            
            Socket server = null;

            try
            {
                
                server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                
                server.Bind(new IPEndPoint(IPAddress.Any, port));

                
                server.Listen(BACKLOG);
            }
            catch (SocketException se)
            {
                Console.WriteLine(se.ErrorCode + ":" + se.Message);
                Environment.Exit(se.ErrorCode);
            }

            byte[] Buf = new byte[1024];
            int rcvbyte=0;

            while(true)
            {
                Socket client = null; 
                try
                {
                    client = server.Accept(); 
                    Console.WriteLine("Handling client at " + client.RemoteEndPoint);

                    while ((rcvbyte = client.Receive(Buf, Buf.Length, SocketFlags.None)) > 0) 
                    {
                        client.Send(Buf, rcvbyte, SocketFlags.None);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    client.Close();
                }
            }
        }
    }
}



