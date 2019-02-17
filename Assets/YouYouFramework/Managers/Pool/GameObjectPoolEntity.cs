using PathologicalGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YouYouFramework
{
    /// <summary>
    /// 对象池实体
    /// </summary>
    [System.Serializable]
    public class GameObjectPoolEntity
    {
        /// <summary>
        /// 对象池编号
        /// </summary>
        public byte PoolId;

        /// <summary>
        /// 对象池名称
        /// </summary>
        public string PoolName;

        /// <summary>
        /// 是否开启缓存池自动清理模式
        /// </summary>
        public bool CullDespawned = true;

        /// <summary>
        /// 对象在缓存池中的常驻数量
        /// </summary>
        public int CullAbove = 5;

        /// <summary>
        /// 多长时间清理一次，单位秒
        /// </summary>
        public int CullDelay = 2;

        /// <summary>
        /// 每次清理几个
        /// </summary>
        public int CullMaxPerpass = 2;

        /// <summary>
        /// 对应的游戏物体对象池
        /// </summary>
        public SpawnPool Pool;
    }
}
