using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Lidgren.Network;

namespace ProjectOneTwo
{
    public class Server
    {
        readonly NetPeerConfiguration config;
        NetIncomingMessage incoming;
        string commands = "";
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

            if (commands.Contains("start_game"))
            {
                playing = true;
                return true;
            }
            else
            {
                NetOutgoingMessage message = server.CreateMessage("ready_to_start");    
                SendMessage(message);
            }

            return false;
        }

        private void IncomingMessage()
        {
            incoming = server.ReadMessage();
            if (incoming != null)
            {
                if (incoming.MessageType == NetIncomingMessageType.Data) commands = incoming.ReadString();
                else commands = "";
            }
            
        }

        public void SendMessage(NetOutgoingMessage outgoing)
        {
            if (connection != null) server.SendMessage(outgoing, recipient: connection, NetDeliveryMethod.ReliableOrdered);
        }

        public KeyboardState GetInput()
        {
            List<Keys> keys = new List<Keys>();
            if (commands.Contains("Up")) keys.Add(Keys.Up);
            if (commands.Contains("Left")) keys.Add(Keys.Left);
            if (commands.Contains("Down")) keys.Add(Keys.Down);
            if (commands.Contains("Right")) keys.Add(Keys.Right);

            return new KeyboardState(keys.ToArray());
        }

        public void SendState(string state)
        {
            NetOutgoingMessage message = server.CreateMessage(state);
            SendMessage(message);
        }

        public void GameOver(ScreenManager.Winner winner)
        {
            string text = "";
            
            switch (winner)
            {
                case ScreenManager.Winner.Player1:
                    text = "Winner:Player1;";
                    break;
                case ScreenManager.Winner.Player2:
                    text = "Winner:Player2;";
                    break;
                case ScreenManager.Winner.Draw:
                    text = "Winner:Draw;";
                    break;
                case ScreenManager.Winner.None:
                    text = "Winner:Error;";
                    break;
            }
            NetOutgoingMessage message = server.CreateMessage(text);
            SendMessage(message);
        }
    }
}
