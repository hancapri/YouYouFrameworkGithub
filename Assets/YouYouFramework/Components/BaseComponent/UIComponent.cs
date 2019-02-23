using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YouYouFramework
{
    /// <summary>
    /// UI组件
    /// </summary>
    public class UIComponent : YouYouBaseComponent, IUpdateComponent
    {
        protected override void OnAwake()
        {
            base.OnAwake();
            GameEntry.RegisterUpdateComponent(this);
        }        

        public void OnUpdate()
        {
            //Debug.Log("UI组件的Update方法");
        }

        public override void Shutdown()
        {
            GameEntry.RemoveUpdateComponent(this);
        }
    }
}
