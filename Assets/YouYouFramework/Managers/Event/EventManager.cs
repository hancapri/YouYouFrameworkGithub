using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace YouYouFramework
{
    /// <summary>
    /// 事件管理器
    /// </summary>
    public class EventManager : ManagerBase, IDisposable
    {
        public SocketEvent SocketEvent
        {
            private set;
            get;
        }

        public CommonEvent CommonEvent
        {
            private set;
            get;
        }

        public EventManager()
        {
            SocketEvent = new SocketEvent();
            CommonEvent = new CommonEvent();
        }

        public void Dispose()
        {

        }
    }
}
