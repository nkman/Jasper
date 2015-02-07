using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using WebSocket4Net;

namespace Jasper
{
    class StreamData
    {
        WebSocket websocket;
        public void openWebSocket()
        {
            websocket = new WebSocket("ws://jasperx.cloudapp.net:3000/");
            websocket.Opened += new EventHandler(websocket_Opened);
            websocket.Error += new EventHandler<SuperSocket.ClientEngine.ErrorEventArgs>(websocket_Error);
            websocket.Closed += new EventHandler(websocket_Closed);
            websocket.MessageReceived += new EventHandler<WebSocket4Net.MessageReceivedEventArgs>(websocket_MessageReceived);
            websocket.Open();
            
        }

        private void websocket_Opened(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Websocket opened");
            websocket.Send("Hello World!");

        }

        private void websocket_Error(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Error");
        }
        private void websocket_MessageReceived(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Message received");
        }

        private void websocket_Closed(object sender, EventArgs e)
        {

        }
    }
}
