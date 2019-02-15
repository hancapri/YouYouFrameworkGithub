using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace YouYouFramework
{
    /// <summary>
    /// 类对象池
    /// </summary>
    public class ClassObjectPool : IDisposable
    {
        private Dictionary<int, Queue<object>> m_ClassObjectPoolDic;

        public ClassObjectPool()
        {
            m_ClassObjectPoolDic = new Dictionary<int, Queue<object>>();
        }

        #region 类对象池的出池和回池
        /// <summary>
        /// 类对象池：出池
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Dequeue<T>() where T : class, new()
        {
            int key = typeof(T).GetHashCode();

            Queue<object> queue = null;
            m_ClassObjectPoolDic.TryGetValue(key, out queue);

            if (queue == null)
            {
                queue = new Queue<object>();
                m_ClassObjectPoolDic[key] = queue;
            }
            if (queue.Count > 0)
            {
                Debug.Log("有，取出");
                return (T)queue.Dequeue();
            }
            else
            {
                Debug.Log("无，创建");
                return new T();
            }
        }

        /// <summary>
        /// 类对象池：回池
        /// </summary>
        /// <param name="obj"></param>
        public void Enqueue(object obj)
        {
            int key = obj.GetType().GetHashCode();

            Queue<object> queue = null;
            m_ClassObjectPoolDic.TryGetValue(key, out queue);

            if (queue != null)
            {
                Debug.Log("回池");
                queue.Enqueue(obj);
            }
            else
            {
                Debug.LogError("回池的类对象，不是从池中创建的，请检查，回池对象："+ obj.GetType().Name);
            } 
        }
        #endregion

        public void Dispose()
        {
            m_ClassObjectPoolDic.Clear();
        }
    }
}
