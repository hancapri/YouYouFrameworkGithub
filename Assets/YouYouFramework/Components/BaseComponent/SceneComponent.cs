using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YouYouFramework
{
    /// <summary>
    /// 场景组件
    /// </summary>
    public class SceneComponent : YouYouBaseComponent,IUpdateComponent
    {
        /// <summary>
        /// 场景管理器
        /// </summary>
        private YouYouSceneManager m_YouYouSceneManager;

        protected override void OnAwake()
        {
            base.OnAwake();
            GameEntry.RegisterUpdateComponent(this);
            m_YouYouSceneManager = new YouYouSceneManager();
        }

        /// <summary>
        /// 加载场景
        /// </summary>
        public void LoadScene(int sceneId)
        {
            m_YouYouSceneManager.LoadScene(sceneId);
        }

        public override void Shutdown()
        {
            GameEntry.RemoveUpdateComponent(this);
        }

        public void OnUpdate()
        {
            m_YouYouSceneManager.OnUpdate();
        }
    }
}
