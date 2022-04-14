using System;
using System.Collections.Generic;
using System.Text;
using Lidgren.Network;

namespace ProjectOneTwo
{   
    public class Client
    {
        readonly NetPeerConfiguration config;
        readonly NetClient client;
        public bool connected = false, playing = false;
        NetIncomingMessage incoming;
        string commands = "";

        public Client()
        {
            config = new NetPeerConfiguration("ProjectOneTwo.exe")
            { Port = 12345 };
            client = new NetClient(config);
        }

        public bool Connect(string ipAdress)
        {
            client.Start();
            try
            {
                client.Connect(host: ipAdress, port: 12345);
                connected = true;
            }
            catch
            {
                client.Disconnect("error");
                client.Shutdown("error");
                connected = false;
                return false;
            }
            return true;
        }

        public void Disconnect()
        {
            connected = false;
            if (client.Status == NetPeerStatus.Running)
            {
                client.Disconnect("tasked");
                client.Shutdown("tasked");
            }
        }

        public bool StartGame()
        {
            if (playing) return true;
                    
            if (incoming != null)
            {
                string message = incoming.ReadString();
                if (message == "ready_to_start")
                {
                    NetOutgoingMessage response = client.CreateMessage("start_game");
                    SendMessage(response);
                    playing = true;
                    return true;
                }
            }
            return false;
        }

        public void Update()
        {
            IncomingMessage();
        }

        private void IncomingMessage()
        {
            incoming = client.ReadMessage();
            if (incoming != null)
            {
                if (incoming.MessageType == NetIncomingMessageType.Data) commands = incoming.ReadString();
                else commands = "";
            }
            
        }

        private void SendMessage(NetOutgoingMessage outgoing)
        {
            client.SendMessage(outgoing, NetDeliveryMethod.ReliableOrdered);
        }

        public string GetState()
        {
            return commands;
        }
    }
}
