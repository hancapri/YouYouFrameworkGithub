using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YouYouFramework
{
    /// <summary>
    /// 通用事件
    /// </summary>
    public class CommonEvent
    {
        //[CSharpCallLua]
        public delegate void OnActionHandler(object param);
        public Dictionary<ushort, List<OnActionHandler>> dic = new Dictionary<ushort, List<OnActionHandler>>();

        #region AddEventListener 添加监听
        /// <summary>
        /// 添加监听
        /// </summary>
        /// <param name="key"></param>
        /// <param name="handler"></param>
        public void AddEventListener(ushort key, OnActionHandler handler)
        {
            if (dic.ContainsKey(key))
            {
                dic[key].Add(handler);
            }
            else
            {
                List<OnActionHandler> lstHandler = new List<OnActionHandler>();
                lstHandler.Add(handler);
                dic[key] = lstHandler;
            }
        }
        #endregion

        #region RemoveEventListener 移除监听
        /// <summary>
        /// 移除监听
        /// </summary>
        /// <param name="key"></param>
        /// <param name="handler"></param>
        public void RemoveEventListener(ushort key, OnActionHandler handler)
        {
            if (dic.ContainsKey(key))
            {
                List<OnActionHandler> lstHandler = dic[key];
                lstHandler.Remove(handler);
                if (lstHandler.Count == 0)
                {
                    dic.Remove(key);
                }
            }
        }
        #endregion

        #region Dispatch 派发
        /// <summary>
        /// 派发
        /// </summary>
        /// <param name="key"></param>
        /// <param name="p"></param>
        public void Dispatch(ushort key, object param)
        {
            if (dic.ContainsKey(key))
            {
                List<OnActionHandler> lstHandler = dic[key];
                if (lstHandler != null && lstHandler.Count > 0)
                {
                    for (int i = 0; i < lstHandler.Count; i++)
                    {
                        if (lstHandler[i] != null)
                        {
                            lstHandler[i](param);
                        }
                    }
                }
            }
        }

        public void Dispatch(ushort key)
        {
            Dispatch(key, null);
        }
        #endregion
    }
}
