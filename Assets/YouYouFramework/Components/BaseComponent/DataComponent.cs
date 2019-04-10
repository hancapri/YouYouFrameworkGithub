using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YouYouFramework
{
    /// <summary>
    /// 数据组件
    /// </summary>
    public class DataComponent : YouYouBaseComponent
    {
        /// <summary>
        /// 临时缓存数据
        /// </summary>
        public CacheDataManager CacheDataManager { get; private set; }

        /// <summary>
        /// 系统相关数据
        /// </summary>
        public SysDataManager SysDataManager { get; private set; }

        /// <summary>
        /// 用户数据
        /// </summary>
        public UserDataManager UserDataManager { get; private set; }

        /// <summary>
        /// PVE地图数据
        /// </summary>
        public PVEMapDataManager PVEMapDataManager { get; private set; }

        protected override void OnAwake()
        {
            base.OnAwake();
            CacheDataManager = new CacheDataManager();
            SysDataManager  = new SysDataManager();
            UserDataManager = new UserDataManager();
            PVEMapDataManager = new PVEMapDataManager();
        }

        public override void Shutdown()
        {
            CacheDataManager.Dispose();
            SysDataManager.Dispose();
            UserDataManager.Dispose();
            PVEMapDataManager.Dispose();
        }
    }
}
