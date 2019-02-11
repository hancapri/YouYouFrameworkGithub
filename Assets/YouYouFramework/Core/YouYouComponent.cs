using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YouYouFramework
{
    /// <summary>
    /// 所有挂载组件的基类，包括基础组件，游戏中的逻辑组件等
    /// </summary>
    public class YouYouComponent : MonoBehaviour
    {
        private int m_InstanceID;
        public int InstanceID
        {
            get { return m_InstanceID; }
        }

        private void Awake()
        {
            m_InstanceID = GetInstanceID();
            OnAwake();
        }

        protected virtual void OnAwake() { }
    }
}
