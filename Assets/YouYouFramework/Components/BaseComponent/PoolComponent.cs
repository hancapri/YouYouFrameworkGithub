using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YouYouFramework
{
    /// <summary>
    /// 对象池组件
    /// </summary>
    public class PoolComponent : YouYouBaseComponent, IUpdateComponent
    {
        /// <summary>
        /// 释放池中对象的时间间隔
        /// </summary>
        public int ClearInterval = 30;

        private float m_NextClearTime;

        public PoolManager PoolManager
        {
            get;
            private set;
        }

        /// <summary>
        /// 对象池分组
        /// </summary>
        [SerializeField]
        private GameObjectPoolEntity[] m_GameObjectPoolGroups;

        protected override void OnAwake()
        {
            base.OnAwake();
            PoolManager = new PoolManager();

            GameEntry.RegisterUpdateComponent(this);
            m_NextClearTime = Time.time;

            InitGameObjectPool();
        }

        /// <summary>
        /// 设置类对象的常驻数量
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="count"></param>
        public void SetClassObjectResideCount<T>(byte count) where T : class
        {
            PoolManager.ClassObjectPool.SetResideCount<T>(count);
        }

        #region 类对象的出池和回池
        /// <summary>
        /// 类对象池：出池
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T DequeueClassObject<T>() where T : class, new()
        {
            return PoolManager.ClassObjectPool.Dequeue<T>();
        }

        /// <summary>
        /// 类对象池：回池
        /// </summary>
        /// <param name="obj"></param>
        public void EnqueueClassObject(object obj)
        {
            PoolManager.ClassObjectPool.Enqueue(obj);
        }
        #endregion

        #region 游戏对象的出池回池
        /// <summary>
        /// 初始化游戏物体对象池
        /// </summary>
        public void InitGameObjectPool()
        {
            StartCoroutine(PoolManager.GameObjectPool.Init(m_GameObjectPoolGroups, transform));
        }

        /// <summary>
        /// 从对象池中获取对象
        /// </summary>
        /// <param name="poolId"></param>
        /// <param name="prefab"></param>
        /// <param name="onComplete"></param>
        public void GameObjectSpawn(byte poolId, Transform prefab, System.Action<Transform> onComplete)
        {
            PoolManager.GameObjectPool.Spawn(poolId, prefab, onComplete);
        }

        /// <summary>
        /// 对象回池
        /// </summary>
        /// <param name="poolId"></param>
        /// <param name="instance"></param>
        public void GameObjectDespawn(byte poolId, Transform instance)
        {
            PoolManager.GameObjectPool.Despawn(poolId, instance);
        }
        #endregion

        public void OnUpdate()
        {
            if (Time.time > m_NextClearTime + ClearInterval)
            {
                //该释放了
                m_NextClearTime = Time.time;
                PoolManager.ClearClassObjectPool();
            }
        }

        public override void Shutdown()
        {
            GameEntry.RemoveUpdateComponent(this);
            PoolManager.Dispose();
        }
    }
}
