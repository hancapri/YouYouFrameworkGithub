using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YouYouFramework
{
    /// <summary>
    /// Http管理器
    /// </summary>
    public class HttpManager : ManagerBase
    {
        /// <summary>
        /// 发送web数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="callBack"></param>
        /// <param name="isPost"></param>
        /// <param name="json"></param>
        public void SendData(string url, HttpSendDataCallBack callBack, bool isPost = false, Dictionary<string, object> dic = null)
        {
            //支持多个HttpRoutine，从对象池中取出HttpRoutine
            HttpRoutine http = GameEntry.Pool.DequeueClassObject<HttpRoutine>();
            http.SendData(url, callBack, isPost, dic);
        }
    }
}
