using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace YouYouFramework
{
    public class GameEntry : MonoBehaviour
    {
        #region 组件属性
        /// <summary>
        /// 事件组件
        /// </summary>
        public static EventComponent Event
        {
            get;
            private set;
        }

        /// <summary>
        /// 时间组件
        /// </summary>
        public static TimeComponent Time
        {
            get;
            private set;
        }

        /// <summary>
        /// 状态机组件
        /// </summary>
        public static FsmComponent Fsm
        {
            get;
            private set;
        }

        /// <summary>
        /// 流程组件
        /// </summary>
        public static ProcedureComponent Procedure
        {
            get;
            private set;
        }

        /// <summary>
        /// 数据表组件
        /// </summary>
        public static DataTableComponent DataTable
        {
            get;
            private set;
        }

        /// <summary>
        /// Socket组件
        /// </summary>
        public static SocketComponent Socket
        {
            get;
            private set;
        }

        /// <summary>
        /// Http组件
        /// </summary>
        public static HttpComponent Http
        {
            get;
            private set;
        }

        /// <summary>
        /// 数据组件
        /// </summary>
        public static DataComponent Data
        {
            get;
            private set;
        }

        /// <summary>
        /// 本地化（多语言）组件
        /// </summary>
        public static LocalizationComponent Localization
        {
            get;
            private set;
        }

        /// <summary>
        /// 对象池组件
        /// </summary>
        public static PoolComponent Pool
        {
            get;
            private set;
        }

        /// <summary>
        /// 场景组件
        /// </summary>
        public static SceneComponent Scene
        {
            get;
            private set;
        }

        /// <summary>
        /// 设置组件
        /// </summary>
        public static SettingComponent Setting
        {
            get;
            private set;
        }

        /// <summary>
        /// 游戏对象组件
        /// </summary>
        public static GameObjComponent GameObj
        {
            get;
            private set;
        }

        /// <summary>
        /// 资源组件
        /// </summary>
        public static ResourceComponent Resource
        {
            get;
            private set;
        }

        /// <summary>
        /// 下载组件
        /// </summary>
        public static DownloadComponent Download
        {
            get;
            private set;
        }

        /// <summary>
        /// UI组件
        /// </summary>
        public static UIComponent UI
        {
            get;
            private set;
        }
        #endregion

        #region 基础组件管理

        /// <summary>
        /// 基础组件列表
        /// </summary>
        private static readonly LinkedList<YouYouBaseComponent> m_BaseComponentList = new LinkedList<YouYouBaseComponent>();

        #region RegisterComponent注册基础组件
        /// <summary>
        /// RegisterComponent基础组件注册到组件列表中
        /// </summary>
        /// <param name="component"></param>
        internal static void RegisterComponent(YouYouBaseComponent component)
        {
            //获取到组件类型
            Type type = component.GetType();

            LinkedListNode<YouYouBaseComponent> curr = m_BaseComponentList.First;
            while (curr != null)
            {
                if (curr.Value.GetType() == type) return;
                curr = curr.Next;
            }
            m_BaseComponentList.AddLast(component);
        }
        #endregion

        #region GetBaseComponent获取基础组件
        /// <summary>
        /// 泛型获取基础组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetBaseComponent<T>() where T : YouYouBaseComponent
        {
            return (T)GetBaseComponent(typeof(T));
        }

        /// <summary>
        /// 获取基础组件
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        internal static YouYouBaseComponent GetBaseComponent(Type type)
        {
            LinkedListNode<YouYouBaseComponent> curr = m_BaseComponentList.First;
            while (curr != null)
            {
                if (curr.Value.GetType() == type) return curr.Value;
                curr = curr.Next;
            }
            return null;
        }
        #endregion

        #endregion

        #region 全局的Update方法管理

        /// <summary>
        /// 全局Update方法列表
        /// </summary>
        private static readonly LinkedList<IUpdateComponent> m_UpdateComponentList = new LinkedList<IUpdateComponent>();

        #region RegisterUpdateComponent注册Update方法
        /// <summary>
        /// RegisterUpdateComponent注册Update方法
        /// </summary>
        /// <param name="component"></param>
        internal static void RegisterUpdateComponent(IUpdateComponent component)
        {
            m_UpdateComponentList.AddLast(component);
        }
        #endregion

        #region RemoveUpdateComponent移除Update方法
        /// <summary>
        /// RemoveUpdateComponent移除Update方法
        /// </summary>
        /// <param name="component"></param>
        internal static void RemoveUpdateComponent(IUpdateComponent component)
        {
            m_UpdateComponentList.Remove(component);
        }
        #endregion

        #endregion

        void Start()
        {
            InitBaseComponent();
        }   

        /// <summary>
        /// 基础组件属性的赋值
        /// </summary>
        private void InitBaseComponent()
        {
            Event = GetBaseComponent<EventComponent>();
            Time = GetBaseComponent<TimeComponent>();
            Fsm = GetBaseComponent<FsmComponent>();
            Procedure = GetBaseComponent<ProcedureComponent>();
            DataTable = GetBaseComponent<DataTableComponent>();
            Socket = GetBaseComponent<SocketComponent>();
            Http = GetBaseComponent<HttpComponent>();
            Data = GetBaseComponent<DataComponent>();
            Localization = GetBaseComponent<LocalizationComponent>();
            Pool = GetBaseComponent<PoolComponent>();
            Scene = GetBaseComponent<SceneComponent>();
            Setting = GetBaseComponent<SettingComponent>();
            GameObj = GetBaseComponent<GameObjComponent>();
            Resource = GetBaseComponent<ResourceComponent>();
            Download = GetBaseComponent<DownloadComponent>();
            UI = GetBaseComponent<UIComponent>();
        }

        void Update()
        {
            for (LinkedListNode<IUpdateComponent> curr = m_UpdateComponentList.First; curr != null; curr = curr.Next)
            {
                curr.Value.OnUpdate();
            }
        }

        private void OnDestroy()
        {
            for (LinkedListNode<YouYouBaseComponent> curr = m_BaseComponentList.First; curr != null; curr = curr.Next)
            {
                curr.Value.Shutdown();
            }
        }
    }
}
