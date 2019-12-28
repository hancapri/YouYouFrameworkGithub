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
        [Header("锁定的资源包")]
        /// <summary>
        /// 锁定的资源包（不释放）
        /// </summary>
        public string[] LockedAssetBundle;

        /// <summary>
        /// 锁定资源包长度
        /// </summary>
        private int m_LockedAssetBundleLength;

        /// <summary>
        /// 释放池中对象的时间间隔
        /// </summary>
        public int ClearInterval = 30;

        private float m_NextClearTime;

        /// <summary>
        /// 释放AssetBundle池的时间间隔
        /// </summary>
        [SerializeField]
        public int ReleaseResourceInterval = 60;

        private float m_ReleaseResourceNextRunTime = 0f;

        /// <summary>
        /// 释放Asset池的时间间隔
        /// </summary>
        [SerializeField]
        public int ReleaseAssetInterval = 120;

        private float m_ReleaseAssetNextRunTime = 0f;

        public PoolManager PoolManager
        {
            get;
            private set;
        }

        /// <summary>
        /// 游戏对象池分组
        /// </summary>
        [SerializeField]
        private GameObjectPoolEntity[] m_GameObjectPoolGroups;

        /// <summary>
        /// 显示分类资源池
        /// </summary>
        [SerializeField]
        public bool ShowAssetPool = false;

        protected override void OnAwake()
        {
            base.OnAwake();
            PoolManager = new PoolManager();
            m_VarObjectLock = new object();

            GameEntry.RegisterUpdateComponent(this);
            m_NextClearTime = Time.time;
            m_ReleaseResourceNextRunTime = Time.time;

            InitGameObjectPool();
        }

        protected override void OnStart()
        {
            base.OnStart();

            m_LockedAssetBundleLength = LockedAssetBundle.Length;

            InitReside();
        }

        /// <summary>
        /// 检查资源包是否锁定
        /// </summary>
        /// <param name="assetBundleName"></param>
        /// <returns></returns>
        public bool CheckAssetBundleIsLock(string assetBundleName)
        {
            for (int i = 0; i < m_LockedAssetBundleLength; i++)
            {
                if (LockedAssetBundle[i].Equals(assetBundleName, StringComparison.CurrentCultureIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 设置常用类的常驻数量
        /// </summary>
        private void InitReside()
        {
            SetClassObjectResideCount<HttpRoutine>(3);
            SetClassObjectResideCount<Dictionary<string,object>>(3);
            SetClassObjectResideCount<AssetBundleLoaderRoutine>(10);
            SetClassObjectResideCount<AssetLoaderRoutine>(10);
            SetClassObjectResideCount<ResourceEntity>(10);
            SetClassObjectResideCount<MainAssetLoaderRoutine>(30);
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
        public Transform GameObjectSpawn(byte poolId, Transform prefab, System.Action<Transform> onComplete)
        {
            return PoolManager.GameObjectPool.Spawn(poolId, prefab, onComplete);
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

        #region 变量对象池的出池和回池
        /// <summary>
        /// 变量对象池锁
        /// </summary>
        private object m_VarObjectLock;

        //对象池在编辑器面板中显示时，保存池中对象的计数
#if UNITY_EDITOR
        public Dictionary<Type, int> VarObjectInspectorDic = new Dictionary<Type, int>();
#endif
        /// <summary>
        /// 变量对象出池
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T DequeueVarObject<T>() where T : VariableBase, new()
        {
            lock (m_VarObjectLock)
            {
                T item = DequeueClassObject<T>();
#if UNITY_EDITOR
                Type t = item.GetType();
                if (VarObjectInspectorDic.ContainsKey(t))
                {
                    VarObjectInspectorDic[t]++;
                }
                else
                {
                    VarObjectInspectorDic[t] = 1;
                }
#endif
                return item;
            }
        }

        /// <summary>
        /// 变量对象回池
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        public void EnqueueVarObject<T>(T item) where T : VariableBase
        {
            lock (m_VarObjectLock)
            {
                EnqueueClassObject(item);
#if UNITY_EDITOR
                Type t = item.GetType();
                if (VarObjectInspectorDic.ContainsKey(t))
                {
                    VarObjectInspectorDic[t]--;
                    if (VarObjectInspectorDic[t] == 0)
                    {
                        VarObjectInspectorDic.Remove(t);
                    }
                }
#endif
            }
        }
        #endregion

        #region 实例对象资源管理和分类资源池释放
        /// <summary>
        /// 克隆出来的实例资源字典
        /// </summary>
        private Dictionary<int, ResourceEntity> m_InstanceResourceDic = new Dictionary<int, ResourceEntity>();

        /// <summary>
        /// 注册到实例字典
        /// </summary>
        /// <param name="instanceId"></param>
        /// <param name="resourceEntity"></param>
        public void RegisterInstanceResource(int instanceId, ResourceEntity resourceEntity)
        {
            m_InstanceResourceDic[instanceId] = resourceEntity;
        }

        /// <summary>
        /// 释放实例资源
        /// </summary>
        /// <param name="instanceId"></param>
        public void ReleaseInstanceResource(int instanceId)
        {
            ResourceEntity resourceEntity = null;
            if (m_InstanceResourceDic.TryGetValue(instanceId, out resourceEntity))
            {
                UnspawnResourceEntity(resourceEntity);
                m_InstanceResourceDic.Remove(instanceId);
            }
        }

        /// <summary>
        /// 资源实体回池
        /// </summary>
        /// <param name="resourceEntity"></param>
        private void UnspawnResourceEntity(ResourceEntity entity)
        {
            var curr = entity.DependsResourceList.First;
            while (curr != null)
            {
                UnspawnResourceEntity(curr.Value);
                curr = curr.Next;
            }

            GameEntry.Pool.PoolManager.AssetPool[entity.Category].UnSpawn(entity.ResourceName);
        }
        #endregion

        public void OnUpdate()
        {
            if (Time.time > m_NextClearTime + ClearInterval)
            {
                //类对象池该释放了
                m_NextClearTime = Time.time;
                PoolManager.ClearClassObjectPool();
                GameEntry.Log(LogCategory.Normal,"释放类对象池");
            }

            if (Time.time > m_ReleaseResourceNextRunTime + ReleaseResourceInterval)
            {
                //资源池该释放了
                m_ReleaseResourceNextRunTime = Time.time;
                PoolManager.ReleaseAssetBundlePool();
                GameEntry.Log(LogCategory.Normal, "释放AssetBundle池");
            }

            if (Time.time > m_ReleaseAssetNextRunTime + ReleaseAssetInterval)
            {
                //资源池该释放了
                m_ReleaseAssetNextRunTime = Time.time;
                PoolManager.ReleaseAssetPool();
                Resources.UnloadUnusedAssets();
                GameEntry.Log(LogCategory.Normal, "释放Asset池");
            }
        }

        public override void Shutdown()
        {
            GameEntry.RemoveUpdateComponent(this);
            PoolManager.Dispose();
        }
    }
}
