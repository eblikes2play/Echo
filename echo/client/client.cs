using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Net;
using System.Net.Sockets;

namespace TcpSocketClient
{
    
    class client
    {
        static void Main(string[] args)
        {
            String serverIP = "127.0.0.1";
            int port = 5000;

           
            Socket server = null;

            try
            {

                String m_data;

                
                server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPAddress ipServer = IPAddress.Parse(serverIP);
                
                IPEndPoint ipep = new IPEndPoint(ipServer, port);

                server.Connect(ipep); 
                Console.WriteLine("Connected to server...");

                Console.WriteLine("종료를 원하시면 exit을 입력하세요");

                while (true)
                {

                    byte[] message;
                    byte[] rcvbuff = new byte[1024];
                    int rcvbytenum = 0;

                    m_data = Console.ReadLine();

                    if (m_data.CompareTo("exit") == 0)
                    {
                        break;
                    }

                    message = Encoding.Unicode.GetBytes(m_data);

                    server.Send(message, message.Length, SocketFlags.None);

                    rcvbytenum = server.Receive(rcvbuff, SocketFlags.None);
                    
                    Console.WriteLine(Encoding.Unicode.GetString(rcvbuff, 0, rcvbytenum));
                }

               
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                server.Close();
                Console.WriteLine("Disconnected");  
            }
        }
    }
}



