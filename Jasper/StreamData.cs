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
        void openWebSocket()
        {
            websocket = new WebSocket("ws://localhost:2012/");
            websocket.Opened += new EventHandler(websocket_Opened);
            websocket.Error += new EventHandler<SuperSocket.ClientEngine.ErrorEventArgs>(websocket_Error);
            websocket.Closed += new EventHandler(websocket_Closed);
            websocket.MessageReceived += new EventHandler<WebSocket4Net.MessageReceivedEventArgs>(websocket_MessageReceived);
            websocket.Open();

        }

        private void websocket_Opened(object sender, EventArgs e)
        {
            websocket.Send("Hello World!");

        }

        private void websocket_Error(object sender, EventArgs e)
        {

        }
        private void websocket_MessageReceived(object sender, EventArgs e)
        {

        }

        private void websocket_Closed(object sender, EventArgs e)
        {

        }
    }
}
