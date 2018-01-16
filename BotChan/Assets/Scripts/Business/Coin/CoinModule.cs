using System;
using System.Collections;
using System.Collections.Generic;
using BestHTTP;
using BestHTTP.WebSocket;
using LarkFramework;
using LarkFramework.Module;
using UnityEngine;

namespace BotChan
{
    public class CoinModule : Singleton<CoinModule>
    {
        WebSocket webSocket = new WebSocket(new Uri("wss://real.okex.com:10441/websocket"));

        public void Init()
        {
            Debuger.Log("CoinModule Init");

            webSocket.OnOpen += delegate { Debuger.Log("webSocket OnOpen");};
            webSocket.OnClosed += delegate { Debuger.Log("webSocket OnClosed"); };
            webSocket.OnErrorDesc += OnErrorDesc;
            webSocket.OnMessage += OnMessageReceived;
            webSocket.OnBinary += OnBinaryMsgReceived;

            webSocket.Open();
        }

        public void SendMsg()
        {
            webSocket.Send("{'event':'addChannel','channel':'ok_sub_spot_bch_btc_ticker'}");
            Debuger.Log("webSocket SendMsg");
        }

        void OnErrorDesc(WebSocket webSocket, string msg)
        {
            Debuger.Log("webSocket OnErrorDesc:" + msg);
        }

        void OnMessageReceived(WebSocket webSocket, string msg)
        {
            Debuger.Log("webSocket OnMessage:" + msg);
        }

        void OnBinaryMsgReceived(WebSocket webSocket, byte[] data)
        {
            Debuger.Log("webSocket OnBinary:" + data.Length);
        }
    }
}
