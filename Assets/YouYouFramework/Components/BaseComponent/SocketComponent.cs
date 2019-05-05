using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YouYouFramework
{
    /// <summary>
    /// Socket组件
    /// </summary>
    public class SocketComponent : YouYouBaseComponent, IUpdateComponent
    {
        private SocketManager m_SocketManager;

        public MMO_MemoryStream SocketSendMS { get; private set; }
        public MMO_MemoryStream SocketReceiveMS { get; private set; }

        [Header("每帧最大发送包的数量")]
        /// <summary>
        /// 每帧最大发送包的数量
        /// </summary>
        public int MaxSendCount = 5;

        [Header("每个包的最大字节数")]
        /// <summary>
        /// 每个包的最大字节数
        /// </summary>
        public int MaxSendByteCount = 1024;

        [Header("每帧最大收包的数量")]
        /// <summary>
        /// 每帧最大接收包的数量
        /// </summary>
        public int MaxReceiveCount = 5;

        [Header("心跳间隔")]
        /// <summary>
        /// 心跳间隔
        /// </summary>
        public int HeartbeatInterval = 10;

        /// <summary>
        /// 上次心跳时间
        /// </summary>
        private float m_PrevHeartbeatTime = 0;

        /// <summary>
        /// PING值（毫秒）
        /// </summary>
        [HideInInspector]
        public int PingValue;

        /// <summary>
        /// 游戏服务器时间
        /// </summary>
        [HideInInspector]
        public long GameServerTime;

        /// <summary>
        /// 和服务器对表时刻
        /// </summary>
        [HideInInspector]
        public float CheckServerTime;

        /// <summary>
        /// 是否已经连接到主服务器
        /// </summary>
        private bool m_IsConnectToMainSocket = false;

        protected override void OnAwake()
        {
            base.OnAwake();
            GameEntry.RegisterUpdateComponent(this);
            m_SocketManager = new SocketManager();

            SocketSendMS = new MMO_MemoryStream();
            SocketReceiveMS = new MMO_MemoryStream();
        }

        protected override void OnStart()
        {
            base.OnStart();
            m_MainSocket = CreatSocketTcpRoutine();
            m_MainSocket.OnConnectOK = () =>
            {
                //已经建立了连接
                m_IsConnectToMainSocket = true;
            };
            SocketProtoListener.AddProtoListener();
        }

        /// <summary>
        /// 注册SocketTcp访问器
        /// </summary>
        /// <param name="routine"></param>
        internal void RegisterSocketTcpRoutine(SocketTcpRoutine routine)
        {
            m_SocketManager.RegisterSocketTcpRoutine(routine);
        }

        /// <summary>
        /// 移除SocketTcp访问器
        /// </summary>
        /// <param name="routine"></param>
        internal void RemoveSocketTcpRoutine(SocketTcpRoutine routine)
        {
            m_SocketManager.RemoveSocketTcpRoutine(routine);
        }

        /// <summary>
        /// 创建SocketTcp访问器（从池中获取，暂不回池，断开连接下次可能再连）
        /// </summary>
        /// <returns></returns>
        public SocketTcpRoutine CreatSocketTcpRoutine()
        {
            return GameEntry.Pool.DequeueClassObject<SocketTcpRoutine>();
        }

        public void OnUpdate()
        {
            m_SocketManager.OnUpdate();

            if (m_IsConnectToMainSocket)
            {
                if (Time.realtimeSinceStartup > m_PrevHeartbeatTime + HeartbeatInterval)
                {
                    //发送心跳
                    m_PrevHeartbeatTime = Time.realtimeSinceStartup;
                    System_HeartbeatProto proto = new System_HeartbeatProto();
                    proto.LocalTime = Time.realtimeSinceStartup * 1000;
                    CheckServerTime = Time.realtimeSinceStartup;
                    SendMsg(proto);
                }
            }
        }

        public override void Shutdown()
        {
            m_IsConnectToMainSocket = false;
            m_SocketManager.Dispose();
            GameEntry.Pool.EnqueueClassObject(m_MainSocket);
            SocketProtoListener.RemoveProtoListener();

            SocketSendMS.Dispose();
            SocketSendMS.Close();
            SocketReceiveMS.Close();
            SocketReceiveMS.Dispose();
        }

        //======================

        /// <summary>
        /// 主socket
        /// </summary>
        private SocketTcpRoutine m_MainSocket;

        /// <summary>
        /// 连接主服务器
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        public void ConnectToMainSocket(string ip, int port)
        {
            m_MainSocket.Connect(ip, port);
        }

        /// <summary>
        /// 向主服务器发送数据
        /// </summary>
        /// <param name="buffer"></param>
        public void SendMsg(IProto proto)
        {
            m_MainSocket.SendMsg(proto.ToArray());
        }
    }
}
