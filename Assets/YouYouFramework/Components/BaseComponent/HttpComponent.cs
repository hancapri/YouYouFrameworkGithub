using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YouYouFramework
{
    /// <summary>
    /// Http组件
    /// </summary>
    public class HttpComponent : YouYouBaseComponent
    {
        [SerializeField]
        [Header("正式账号服务器Url")]
        private string m_WebAccountUrl;

        [SerializeField]
        [Header("测试账号服务器Url")]
        private string m_TestWebAccountUrl;

        [SerializeField]
        [Header("是否测试环境")]
        private bool m_IsTest;

        /// <summary>
        /// 当前真实的账号服务器Url
        /// </summary>
        public string RealWebUrl
        {
            get { return m_IsTest ? m_TestWebAccountUrl : m_WebAccountUrl; }
        }

        private HttpManager m_HttpManager;

        protected override void OnAwake()
        {
            base.OnAwake();
            m_HttpManager = new HttpManager();
        }

        /// <summary>
        /// 发送web数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="callBack"></param>
        /// <param name="isPost"></param>
        /// <param name="json"></param>
        public void SendData(string url, HttpSendDataCallBack callBack, bool isPost = false,bool isGetData = false, Dictionary<string, object> dic = null)
        {
            m_HttpManager.SendData(url, callBack, isPost, isGetData, dic);
        }

        public override void Shutdown()
        {

        }
    }
}
