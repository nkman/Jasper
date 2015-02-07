using SuperSocket.ClientEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocket4Net;

namespace Jasper
{
    class sock
    {
        WebSocket websocket = new WebSocket("ws://localhost:2012/");

        public void test()
        {
            websocket.Opened += new EventHandler(websocket_Opened);
            websocket.Error += new EventHandler<ErrorEventArgs>(websocket_Error);
            websocket.Closed += new EventHandler(websocket_Closed);
            //websocket.MessageReceived += new EventHandler(websocket_MessageReceived);
            websocket.Open();
        }

        private void websocket_Error(object sender, ErrorEventArgs e)
        {
            websocket.Close();
            //throw new NotImplementedException();
        }

        private void websocket_Closed(object sender, EventArgs e)
        {
            websocket.Close();
            //throw new NotImplementedException();
        }

        private void websocket_MessageReceived(object sender, EventArgs e)
        {
            //websocket.ReceiveBufferSize(128);
            throw new NotImplementedException();
        }

        public void websocket_Opened(object sender, EventArgs e)
        {
             websocket.Send("Hello World!");
        }
    }
}
