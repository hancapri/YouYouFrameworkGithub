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

        /// <summary>
        /// 通用MemoryStream
        /// </summary>
        public MMO_MemoryStream CommonMemoryStream { get; private set; }

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
        /// 每帧最大发送包的数量
        /// </summary>
        public int MaxReceiveCount = 5;

        protected override void OnAwake()
        {
            base.OnAwake();
            GameEntry.RegisterUpdateComponent(this);
            m_SocketManager = new SocketManager();
            CommonMemoryStream = new MMO_MemoryStream();
        }

        protected override void OnStart()
        {
            base.OnStart();
            m_MainSocket = CreatSocketTcpRoutine();
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
        }

        public override void Shutdown()
        {
            m_SocketManager.Dispose();
            GameEntry.Pool.EnqueueClassObject(m_MainSocket);
            SocketProtoListener.RemoveProtoListener();
            CommonMemoryStream.Dispose();
            CommonMemoryStream.Close();
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
