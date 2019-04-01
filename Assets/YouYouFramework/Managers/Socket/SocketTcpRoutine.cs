using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

namespace YouYouFramework
{
    /// <summary>
    /// SocketTcp访问器
    /// </summary>
    public class SocketTcpRoutine
    {
        #region 发送消息所需变量
        //发送消息队列
        private Queue<byte[]> m_SendQueue = new Queue<byte[]>();

        //压缩数组的长度界限
        private const int m_CompressLen = 200;
        #endregion

        //是否连接成功
        private bool m_IsConnectedOk;

        #region 发送、接收消息所需变量
        //接收数据包的字节数组缓冲区
        private byte[] m_ReceiveBuffer = new byte[1024];

        //接收数据包的缓冲数据流
        private MMO_MemoryStream m_ReceiveMS = new MMO_MemoryStream();

        //接收消息的队列
        private Queue<byte[]> m_ReceiveQueue = new Queue<byte[]>();

        private int m_ReceiveCount = 0;

        //这一帧发送了多少
        private int m_SendCount = 0;

        //是否有未处理的字节数组
        private bool m_IsHasUnDealBytes = false;

        //存储从列表中取出但未处理的字节
        private byte[] m_UnDealBytes = null;
        #endregion

        /// <summary>
        /// 客户端socket
        /// </summary>
        private Socket m_Client;

        public Action OnConnectOK;

        internal void OnUpdate()
        {
            if (m_IsConnectedOk)
            {
                m_IsConnectedOk = false;
                if (OnConnectOK != null)
                {
                    OnConnectOK();
                }
                Debug.Log("连接成功");
            }

            #region 从队列中获取数据
            while (true)
            {
                if (m_ReceiveCount < GameEntry.Socket.MaxReceiveCount)
                {
                    lock (m_ReceiveQueue)
                    {
                        if (m_ReceiveQueue.Count > 0)
                        {
                            m_ReceiveCount++;
                            //得到队列中的数据包
                            byte[] buffer = m_ReceiveQueue.Dequeue();

                            //异或之后的数组
                            byte[] bufferNew = new byte[buffer.Length - 3];

                            bool isCompress = false;
                            ushort crc = 0;

                            using (MMO_MemoryStream ms = new MMO_MemoryStream(buffer))
                            {
                                isCompress = ms.ReadBool();
                                crc = ms.ReadUShort();
                                ms.Read(bufferNew, 0, bufferNew.Length);
                            }

                            //先crc
                            int newCrc = Crc16.CalculateCrc16(bufferNew);

                            if (newCrc == crc)
                            {
                                //异或 得到原始数据
                                bufferNew = SecurityUtil.Xor(bufferNew);

                                if (isCompress)
                                {
                                    bufferNew = ZlibHelper.DeCompressBytes(bufferNew);
                                }

                                ushort protoCode = 0;
                                byte[] protoContent = new byte[bufferNew.Length - 2];
                                using (MMO_MemoryStream ms = new MMO_MemoryStream(bufferNew))
                                {
                                    //协议编号
                                    protoCode = ms.ReadUShort();
                                    ms.Read(protoContent, 0, protoContent.Length);

                                    GameEntry.Event.SocketEvent.Dispatch(protoCode, protoContent);
                                }
                            }
                            else
                            {
                                break;
                            }

                            ////测试，待删除，上述注释代码为加密之后的解读过程，为真正的代码
                            //using (MMO_MemoryStream ms = new MMO_MemoryStream(buffer))
                            //{
                            //    //协议编号
                            //    ushort protoCode = ms.ReadUShort();
                            //    byte[] protoContent = new byte[buffer.Length - 2]; ;
                            //    ms.Read(protoContent, 0, protoContent.Length);
                            //    GameEntry.Event.SocketEvent.Dispatch(protoCode, protoContent);
                            //}
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                else
                {
                    m_ReceiveCount = 0;
                    break;
                }
            }

            CheckSendQueue();
            #endregion
        }

        #region Connect 连接到socket服务器
        /// <summary>
        /// 连接到socket服务器
        /// </summary>
        /// <param name="ip">ip</param>
        /// <param name="port">端口号</param>
        public void Connect(string ip, int port)
        {
            //如果socket已经存在 并且处于连接中状态 则直接返回
            if (m_Client != null && m_Client.Connected) return;

            m_Client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                m_Client.BeginConnect(new IPEndPoint(IPAddress.Parse(ip), port), ConnectCallBack, m_Client);

            }
            catch (Exception ex)
            {
                Debug.Log("连接失败=" + ex.Message);
            }
        }

        private void ConnectCallBack(IAsyncResult ar)
        {
            if (m_Client.Connected)
            {
                Debug.Log("socket连接成功");
                GameEntry.Socket.RegisterSocketTcpRoutine(this);

                ReceiveMsg();
                m_IsConnectedOk = true;
            }
            else
            {
                Debug.Log("socket连接失败");
            }
            m_Client.EndConnect(ar);
        }
        #endregion

        #region DisConnect 断开连接socket服务器
        /// <summary>
        /// 断开连接
        /// </summary>
        public void DisConnect()
        {
            if (m_Client != null && m_Client.Connected)
            {
                m_Client.Shutdown(SocketShutdown.Both);
                m_Client.Close();
                GameEntry.Socket.RemoveSocketTcpRoutine(this);
            }
        }
        #endregion

        #region CheckSendQueue 检查发送队列
        /// <summary>
        /// 检查队列的委托回调
        /// </summary>
        private void CheckSendQueue()
        {
            if (m_SendCount >= GameEntry.Socket.MaxSendCount)
            {
                //等待下一帧发送
                m_SendCount = 0;
                return;
            }
            lock (m_SendQueue)
            {
                if (m_SendQueue.Count > 0 || m_IsHasUnDealBytes)
                {
                    MMO_MemoryStream ms = GameEntry.Socket.CommonMemoryStream;
                    ms.SetLength(0);
                    //先处理未处理的包
                    if (m_IsHasUnDealBytes)
                    {
                        ms.Write(m_UnDealBytes, 0, m_UnDealBytes.Length);
                        m_IsHasUnDealBytes = false;
                    }

                    while (true)
                    {
                        if (m_SendQueue.Count == 0)
                        {
                            break;
                        }

                        byte[] buffer = m_SendQueue.Dequeue();
                        if ((buffer.Length + ms.Length) <= GameEntry.Socket.MaxSendByteCount)
                        {
                            ms.Write(buffer, 0, buffer.Length);
                        }
                        else
                        {
                            m_IsHasUnDealBytes = true;
                            m_UnDealBytes = buffer;
                            break;
                        }
                    }
                    m_SendCount++;
                    Send(ms.ToArray());
                }
            }
        }
        #endregion

        #region MakeData 封装数据包
        /// <summary>
        /// 封装数据包
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private byte[] MakeData(byte[] data)
        {
            byte[] retBuffer = null;

            //1.如果数据包的长度 大于了m_CompressLen 则进行压缩
            bool isCompress = data.Length > m_CompressLen ? true : false;
            if (isCompress)
            {
                data = ZlibHelper.CompressBytes(data);
            }

            //2.异或
            data = SecurityUtil.Xor(data);

            //3.Crc校验 压缩后的
            ushort crc = Crc16.CalculateCrc16(data);

            MMO_MemoryStream ms = GameEntry.Socket.CommonMemoryStream;
            ms.SetLength(0);
            ms.WriteUShort((ushort)(data.Length + 3));
            ms.WriteBool(isCompress);
            ms.WriteUShort(crc);
            ms.Write(data, 0, data.Length);

            retBuffer = ms.ToArray();
            return retBuffer;
        }
        #endregion

        #region SendMsg 发送消息 把消息加入队列
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="buffer"></param>
        public void SendMsg(byte[] buffer)
        {
            //得到封装后的数据包
            byte[] sendBuffer = MakeData(buffer);

            lock (m_SendQueue)
            {
                //把数据包加入队列
                m_SendQueue.Enqueue(sendBuffer);
            }
        }
        #endregion

        #region Send 真正发送数据包到服务器
        /// <summary>
        /// 真正发送数据包到服务器
        /// </summary>
        /// <param name="buffer"></param>
        private void Send(byte[] buffer)
        {
            m_Client.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, SendCallBack, m_Client);
        }
        #endregion

        #region SendCallBack 发送数据包的回调
        /// <summary>
        /// 发送数据包的回调
        /// </summary>
        /// <param name="ar"></param>
        private void SendCallBack(IAsyncResult ar)
        {
            m_Client.EndSend(ar);
        }
        #endregion

        //====================================================

        #region ReceiveMsg 接收数据
        /// <summary>
        /// 接收数据
        /// </summary>
        private void ReceiveMsg()
        {
            //异步接收数据
            m_Client.BeginReceive(m_ReceiveBuffer, 0, m_ReceiveBuffer.Length, SocketFlags.None, ReceiveCallBack, m_Client);
        }
        #endregion

        #region ReceiveCallBack 接收数据回调
        /// <summary>
        /// 接收数据回调
        /// </summary>
        /// <param name="ar"></param>
        private void ReceiveCallBack(IAsyncResult ar)
        {
            try
            {
                int len = m_Client.EndReceive(ar);

                if (len > 0)
                {
                    //已经接收到数据

                    //把接收到数据 写入缓冲数据流的尾部
                    m_ReceiveMS.Position = m_ReceiveMS.Length;

                    //把指定长度的字节 写入数据流
                    m_ReceiveMS.Write(m_ReceiveBuffer, 0, len);

                    //如果缓存数据流的长度>2 说明至少有个不完整的包过来了
                    //为什么这里是2 因为我们客户端封装数据包 用的ushort 长度就是2
                    if (m_ReceiveMS.Length > 2)
                    {
                        //进行循环 拆分数据包
                        while (true)
                        {
                            //把数据流指针位置放在0处
                            m_ReceiveMS.Position = 0;

                            //currMsgLen = 包体的长度
                            int currMsgLen = m_ReceiveMS.ReadUShort();

                            //currFullMsgLen 总包的长度=包头长度+包体长度
                            int currFullMsgLen = 2 + currMsgLen;

                            //如果数据流的长度>=整包的长度 说明至少收到了一个完整包
                            if (m_ReceiveMS.Length >= currFullMsgLen)
                            {
                                //至少收到一个完整包

                                //定义包体的byte[]数组
                                byte[] buffer = new byte[currMsgLen];

                                //把数据流指针放到2的位置 也就是包体的位置
                                m_ReceiveMS.Position = 2;

                                //把包体读到byte[]数组
                                m_ReceiveMS.Read(buffer, 0, currMsgLen);

                                lock (m_ReceiveQueue)
                                {
                                    m_ReceiveQueue.Enqueue(buffer);
                                }
                                //==============处理剩余字节数组===================

                                //剩余字节长度
                                int remainLen = (int)m_ReceiveMS.Length - currFullMsgLen;
                                if (remainLen > 0)
                                {
                                    //把指针放在第一个包的尾部
                                    m_ReceiveMS.Position = currFullMsgLen;

                                    //定义剩余字节数组
                                    byte[] remainBuffer = new byte[remainLen];

                                    //把数据流读到剩余字节数组
                                    m_ReceiveMS.Read(remainBuffer, 0, remainLen);

                                    //清空数据流
                                    m_ReceiveMS.Position = 0;
                                    m_ReceiveMS.SetLength(0);

                                    //把剩余字节数组重新写入数据流
                                    m_ReceiveMS.Write(remainBuffer, 0, remainBuffer.Length);

                                    remainBuffer = null;
                                }
                                else
                                {
                                    //没有剩余字节

                                    //清空数据流
                                    m_ReceiveMS.Position = 0;
                                    m_ReceiveMS.SetLength(0);

                                    break;
                                }
                            }
                            else
                            {
                                //还没有收到完整包
                                break;
                            }
                        }
                    }

                    //进行下一次接收数据包
                    ReceiveMsg();
                }
                else
                {
                    //客户端断开连接
                    Debug.Log(string.Format("服务器{0}断开连接", m_Client.RemoteEndPoint.ToString()));
                }
            }
            catch
            {
                //客户端断开连接
                Debug.Log(string.Format("服务器{0}断开连接", m_Client.RemoteEndPoint.ToString()));
            }
        }
        #endregion
    }
}
