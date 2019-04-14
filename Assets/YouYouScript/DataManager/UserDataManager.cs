using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace YouYouFramework
{
    /// <summary>
    /// 用户数据
    /// </summary>
    public class UserDataManager : IDisposable
    {
        public List<ServerTaskEntity> ServerTaskList { get; private set; }
        public UserDataManager()
        {
            ServerTaskList = new List<ServerTaskEntity>();
        }

        /// <summary>
        /// 清理数据
        /// </summary>
        public void Clear()
        {
            ServerTaskList.Clear();
        }

        public void Dispose()
        {
            ServerTaskList.Clear();
        }

        /// <summary>
        /// 接收服务器返回的任务列表
        /// </summary>
        /// <param name="proto"></param>
        public void ReceiveTask(Task_SearchTaskReturnProto proto)
        {
            int len = proto.CurrTaskItemList.Count;
            for (int i = 0; i < len; i++)
            {
                Task_SearchTaskReturnProto.TaskItem item = proto.CurrTaskItemList[i];
                ServerTaskList.Add(new ServerTaskEntity() {
                    Id = item.Id,
                    Status = item.Status
                });
            }
            
        }
    }
}
