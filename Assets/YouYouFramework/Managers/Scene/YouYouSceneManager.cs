using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YouYouFramework
{
    /// <summary>
    /// 场景管理器
    /// </summary>
    public class YouYouSceneManager : ManagerBase
    {
        /// <summary>
        /// 场景加载器链表
        /// </summary>
        private LinkedList<SceneLoaderRoutine> m_SceneLoaderList;

        /// <summary>
        /// 当前加载的场景编号
        /// </summary>
        private int m_CurrLoadSceneId;

        /// <summary>
        /// 当前场景数据实体
        /// </summary>
        //private Sys_SceneEntity m_CurrSceneEntity;


    }
}
