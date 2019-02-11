using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YouYouFramework
{
    /// <summary>
    /// YouYou组件基类
    /// </summary>
    public abstract class YouYouBaseComponent : YouYouComponent
    {
        protected override void OnAwake()
        {
            base.OnAwake();
            //把自己加入基础组件列表
            GameEntry.RegisterComponent(this);
        }

        public abstract void Shutdown();
    }
}
