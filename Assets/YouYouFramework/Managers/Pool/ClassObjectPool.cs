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

        /// <summary>
        /// 类对象在池中的常驻数量
        /// </summary>
        public Dictionary<int, byte> ClassObjectCountDic
        {
            get;
            private set;
        }

        //对象池在编辑器面板中显示时，保存池中对象的计数
#if UNITY_EDITOR
        public Dictionary<Type, int> InspectorDic = new Dictionary<Type, int>();
#endif

        public ClassObjectPool()
        {
            m_ClassObjectPoolDic = new Dictionary<int, Queue<object>>();
            ClassObjectCountDic = new Dictionary<int, byte>();
        }

        /// <summary>
        /// 设置常驻数量
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="count"></param>
        public void SetResideCount<T>(byte count) where T : class
        {
            int key = typeof(T).GetHashCode();
            ClassObjectCountDic[key] = count;
        }

        #region 类对象池的出池和回池
        /// <summary>
        /// 类对象池：出池
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Dequeue<T>() where T : class, new()
        {
            lock (m_ClassObjectPoolDic)
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
                    object obj = queue.Dequeue();
                    //出池计数
#if UNITY_EDITOR
                    Type t = obj.GetType();
                    if (InspectorDic.ContainsKey(t))
                    {
                        InspectorDic[t]--;
                    }
                    else
                    {
                        InspectorDic[t] = 0;
                    }
#endif

                    return (T)obj;
                }
                else
                {
                    return new T();
                }
            }
        }

        /// <summary>
        /// 类对象池：回池
        /// </summary>
        /// <param name="obj"></param>
        public void Enqueue(object obj)
        {
            lock (m_ClassObjectPoolDic)
            {
                int key = obj.GetType().GetHashCode();
                Queue<object> queue = null;
                m_ClassObjectPoolDic.TryGetValue(key, out queue);

                //回池计数
#if UNITY_EDITOR
                Type t = obj.GetType();
                if (InspectorDic.ContainsKey(t))
                {
                    InspectorDic[t]++;
                }
                else
                {
                    InspectorDic[t] = 1;
                }
#endif

                if (queue != null)
                {
                    queue.Enqueue(obj);
                }
                else
                {
                    Debug.LogError("回池的类对象，不是从池中创建的，请检查，回池对象：" + obj.GetType().Name);
                }
            }
        }
        #endregion

        /// <summary>
        /// 从池中释放长久未使用的对象
        /// </summary>
        public void ClearPool()
        {
            lock (m_ClassObjectPoolDic)
            {
                int queueCount = 0;
                var enumerator = m_ClassObjectPoolDic.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    int key = enumerator.Current.Key;
                    Queue<object> queue = m_ClassObjectPoolDic[key];
#if UNITY_EDITOR
                    Type t = null;
#endif
                    queueCount = queue.Count;
                    byte resideCount = 0;
                    ClassObjectCountDic.TryGetValue(key, out resideCount);

                    while (queueCount > resideCount)
                    {
                        queueCount--;
                        object obj = queue.Dequeue();

#if UNITY_EDITOR
                        t = obj.GetType();
                        InspectorDic[t]--;
#endif
                    }
                    if (queueCount <= 0)
                    {
#if UNITY_EDITOR
                        if (t != null)
                        {
                            InspectorDic.Remove(t);
                        }
#endif
                    }
                }
                //整个项目有一处GC即可，间隔最好大于60s一次
                GC.Collect();
            }
        }

        public void Dispose()
        {
            m_ClassObjectPoolDic.Clear();
        }
    }
}
