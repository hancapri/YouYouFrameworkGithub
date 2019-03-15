using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace YouYouFramework
{
    /// <summary>
    /// Http发送数据的回调委托
    /// </summary>
    /// <param name="args"></param>
    public delegate void HttpSendDataCallBack(HttpCallBackArgs args);

    /// <summary>
    /// Http访问器
    /// </summary>
    public class HttpRoutine
    {
        #region 属性
        /// <summary>
        /// Http请求回调
        /// </summary>
        private HttpSendDataCallBack m_CallBack;

        /// <summary>
        /// Http请求回调数据
        /// </summary>
        private HttpCallBackArgs m_CallBackArgs;

        /// <summary>
        /// 是否繁忙
        /// </summary>
        public bool IsBusy{ get;private set;}
        #endregion

        public HttpRoutine()
        {
            m_CallBackArgs = new HttpCallBackArgs();
        }

        #region SendData 发送web数据
        /// <summary>
        /// 发送web数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="callBack"></param>
        /// <param name="isPost"></param>
        /// <param name="json"></param>
        public void SendData(string url, HttpSendDataCallBack callBack, bool isPost = false, Dictionary<string, object> dic = null)
        {
            if (IsBusy) return;

            IsBusy = true;
            m_CallBack = callBack;

            if (!isPost)
            {
                GetUrl(url);
            }
            else
            {
                //web加密
                if (dic != null)
                {
                    //客户端标识符
                    dic["deviceIdentifier"] = DeviceUtil.DeviceIdentifier;

                    //设备型号
                    dic["deviceModel"] = DeviceUtil.DeviceModel;

                    long t = GameEntry.Data.SysData.CurrServerTime;
                    //签名
                    dic["sign"] = EncryptUtil.Md5(string.Format("{0}:{1}", t, DeviceUtil.DeviceIdentifier));

                    //时间戳
                    dic["t"] = t;
                }

                PostUrl(url, dic == null ? "" : JsonUtility.ToJson(dic));
            }
        }
        #endregion

        #region GetUrl Get请求
        /// <summary>
        /// Get请求
        /// </summary>
        /// <param name="url"></param>
        private void GetUrl(string url)
        {
            UnityWebRequest data = UnityWebRequest.Get(url);
            GameEntry.Http.StartCoroutine(Request(data));
        }
        #endregion

        #region PostUrl Post请求
        /// <summary>
        /// Post请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="json"></param>
        private void PostUrl(string url, string json)
        {
            //定义一个表单
            WWWForm form = new WWWForm();

            //给表单添加值
            form.AddField("", json);

            UnityWebRequest data = UnityWebRequest.Post(url, form);
            GameEntry.Http.StartCoroutine(Request(data));
        }
        #endregion

        #region Request 请求服务器
        /// <summary>
        /// 请求服务器
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private IEnumerator Request(UnityWebRequest data)
        {
            yield return data;

            IsBusy = false;
            if (data.isHttpError || data.isNetworkError)
            {
                if (m_CallBack != null)
                {
                    m_CallBackArgs.HasError = true;
                    m_CallBackArgs.Value = data.error;
                    m_CallBack(m_CallBackArgs);
                }
            }
            else
            {
                if (m_CallBack != null)
                {
                    m_CallBackArgs.HasError = false;
                    m_CallBackArgs.Value = data.downloadHandler.text;
                    m_CallBack(m_CallBackArgs);
                }
            }
            data.Dispose();
            data = null;
        }
        #endregion
    }
}
