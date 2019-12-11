using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YouYouFramework
{
    /// <summary>
    /// 资源池
    /// </summary>
    public class ResourcePool
    {
#if UNITY_EDITOR
        /// <summary>
        /// 在监视面板显示的信息
        /// </summary>
        public Dictionary<string, int> InspectorDic = new Dictionary<string, int>();
#endif

        /// <summary>
        /// 资源池名称
        /// </summary>
        public string PoolName;

        /// <summary>
        /// 资源池链表
        /// </summary>
        private LinkedList<ResourceEntity> m_List;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="poolName"></param>
        public ResourcePool(string poolName)
        {
            PoolName = poolName;
            m_List = new LinkedList<ResourceEntity>();
        }

        /// <summary>
        /// 注册到资源池
        /// </summary>
        /// <param name="entity"></param>
        public void Register(ResourceEntity entity)
        {
            entity.Spawn();
#if UNITY_EDITOR
            InspectorDic[entity.ResourceName] = entity.ReferenceCount;
#endif
            m_List.AddLast(entity);
        }

        /// <summary>
        /// 资源取池
        /// </summary>
        /// <param name="resourceName"></param>
        /// <returns></returns>
        public ResourceEntity Spawn(string resourceName)
        {
            LinkedListNode<ResourceEntity> curr = m_List.First;
            while (curr != null)
            {
                ResourceEntity entity = curr.Value;
                if (entity.ResourceName.Equals(resourceName, StringComparison.CurrentCultureIgnoreCase))
                {
                    entity.Spawn();
#if UNITY_EDITOR
                    if (InspectorDic.ContainsKey(entity.ResourceName))
                    {
                        InspectorDic[entity.ResourceName] = entity.ReferenceCount;
                    }
#endif
                    return entity;
                }
                curr = curr.Next;
            }
            return null;
        }

        /// <summary>
        /// 资源回池
        /// </summary>
        /// <param name="resourceName"></param>
        public void UnSpawn(string resourceName)
        {
            LinkedListNode<ResourceEntity> curr = m_List.First;
            while (curr != null)
            {
                ResourceEntity entity = curr.Value;
                if (entity.ResourceName.Equals(resourceName,StringComparison.CurrentCultureIgnoreCase))
                {
                    entity.UnSpawn();
#if UNITY_EDITOR
                    if (InspectorDic.ContainsKey(entity.ResourceName))
                    {
                        InspectorDic[entity.ResourceName] = entity.ReferenceCount;
                    }
#endif
                }
                curr = curr.Next;
            }
        }

        /// <summary>
        /// 释放资源池中可释放资源
        /// </summary>
        public void Release()
        {
            LinkedListNode<ResourceEntity> curr = m_List.First;
            while (curr != null)
            {
                ResourceEntity entity = curr.Value;
                //LinkedListNode<ResourceEntity> next = curr.Next;
                if (entity.GetCanRelease())
                {
#if UNITY_EDITOR
                    if (InspectorDic.ContainsKey(entity.ResourceName))
                    {
                        InspectorDic.Remove(entity.ResourceName);
                    }
#endif
                    m_List.Remove(entity);
                    entity.Release();
                }
                curr = curr.Next;
            }
        }
    }
}
