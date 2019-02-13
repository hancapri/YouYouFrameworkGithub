using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YouYouFramework
{
    /// <summary>
    /// 时间组件
    /// </summary>
    public class TimeComponent : YouYouBaseComponent, IUpdateComponent
    {
        /// <summary>
        /// 时间管理器
        /// </summary>
        public TimeManager TimeManager
        {
            get;
            private set;
        }

        protected override void OnAwake()
        {
            base.OnAwake();
            GameEntry.RegisterUpdateComponent(this);

            TimeManager = new TimeManager();
        }       

        public void OnUpdate()
        {
            TimeManager.OnUpdate();
        }

        public override void Shutdown()
        {
            GameEntry.RemoveUpdateComponent(this);
        }
    }
}
