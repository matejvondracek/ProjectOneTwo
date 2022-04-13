using System;
using System.Collections.Generic;
using System.Text;
using Lidgren.Network;

namespace ProjectOneTwo
{
    public class Server
    {
        readonly NetPeerConfiguration config;
        NetIncomingMessage incoming;
        NetConnection connection;
        readonly NetServer server;
        public bool running = false, playing = false;

        public Server()
        {
            config = new NetPeerConfiguration("ProjectOneTwo.exe")
            { Port = 12345 };
            server = new NetServer(config);           
        } 

        public void Start()
        {
            server.Start();
            running = true;
        }

        public void Shutdown()
        {
            server.Shutdown("tasked");
            running = false;
        }

        public void Update()
        {
            if (server.ConnectionsCount > 0) connection = server.Connections[0];
            IncomingMessage();
        }

        public bool StartGame()
        {
            if (playing) return true;

            if (incoming.ReadString() == "start_game")
            {
                playing = true;
                return true;
            }
            else
            {
                NetOutgoingMessage message;
                message = server.CreateMessage("ready_to_start");    
                SendMessage(message);
            }

            return false;
        }

        private void IncomingMessage()
        {
            incoming = server.ReadMessage();
        }

        private void SendMessage(NetOutgoingMessage outgoing)
        {
            server.SendMessage(outgoing, recipient: connection, NetDeliveryMethod.ReliableOrdered);
        }
    }
}
