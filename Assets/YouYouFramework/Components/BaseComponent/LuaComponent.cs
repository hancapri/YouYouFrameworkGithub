//===================================================
//作    者：边涯  http://www.u3dol.com
//创建时间：
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YouYouFramework
{
    /// <summary>
    /// Lua组件
    /// </summary>
    public class LuaComponent : YouYouBaseComponent
    {
        private LuaManager m_LuaManager;

        /// <summary>
        /// 是否打印协议日志
        /// </summary>
        public bool DebugLogProto = false;

        protected override void OnAwake()
        {
            base.OnAwake();

            m_LuaManager = new LuaManager();

#if DEBUG_LOG_PROTO
            DebugLogProto = true;
#endif
        }

        protected override void OnStart()
        {
            base.OnStart();
            LoadDataTableMS = new MMO_MemoryStream();
            m_LuaManager.Init();
        }

        /// <summary>
        /// 加载数据表的MS
        /// </summary>
        public MMO_MemoryStream LoadDataTableMS
        {
            get;
            private set;
        }

        /// <summary>
        /// 加载数据表
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public MMO_MemoryStream LoadDataTable(string tableName)
        {
            byte[] buffer = GameEntry.Resource.GetFileBuffer(string.Format("{0}/download/DataTable/{1}.bytes", GameEntry.Resource.LocalFilePath, tableName));

            LoadDataTableMS.SetLength(0);
            LoadDataTableMS.Write(buffer, 0, buffer.Length);
            LoadDataTableMS.Position = 0;

            return LoadDataTableMS;
        }

        /// <summary>
        /// 在Lua中加载MemoryStream
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public MMO_MemoryStream LoadSocketReceiveMS(byte[] buffer)
        {
            MMO_MemoryStream ms = GameEntry.Socket.SocketReceiveMS;
            ms.SetLength(0);
            ms.Write(buffer, 0, buffer.Length);
            ms.Position = 0;
            return ms;
        }

        public override void Shutdown()
        {
            LoadDataTableMS.Dispose();
            LoadDataTableMS.Close();
        }
    }
}