using System.Collections;
using System.Collections.Generic;
using BotChan;
using LarkFramework;
using LarkFramework.Module;
using UnityEngine;

namespace Project
{
    public class TestSend : MonoBehaviour
    {

        public void Send()
        {
            Singleton<CoinModule>.Instance.SendMsg();
        }
    }

}
