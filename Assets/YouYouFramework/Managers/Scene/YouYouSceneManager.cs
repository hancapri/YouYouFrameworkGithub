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
        private Sys_SceneEntity m_CurrSceneEntity;

        /// <summary>
        /// 当前场景明细
        /// </summary>
        private List<Sys_SceneDetailEntity> m_CurrSceneDetailList;

        /// <summary>
        /// 需要加载或卸载场景的数量
        /// </summary>
        private int m_NeedLoadOrUnLoadSceneDetailCount = 0;

        /// <summary>
        /// 当前加载或卸载场景的数量
        /// </summary>
        private int m_CurrLoadOrUnLoadSceneDetailCount = 0;

        /// <summary>
        /// 场景是否加载中
        /// </summary>
        private bool m_CurrSceneIsLoading;

        /// <summary>
        /// 当前进度（总进度）
        /// </summary>
        private float m_CurrProgress = 0;

        /// <summary>
        /// 目标进度（单个）
        /// </summary>
        private Dictionary<int, float> m_TargetProgressDic;

        private BaseParams m_CurrLoadingParam;
        public YouYouSceneManager()
        {
            m_SceneLoaderList = new LinkedList<SceneLoaderRoutine>();
            m_TargetProgressDic = new Dictionary<int, float>();
        }

        public void LoadScene(int sceneId)
        {
            if (m_CurrSceneIsLoading)
            {
                GameEntry.LogError("场景{0}正在加载中", m_CurrLoadSceneId);
                return;
            }

            if (m_CurrLoadSceneId == sceneId)
            {
                GameEntry.LogError("正在重复加载场景{0}", m_CurrLoadSceneId);
                return;
            }

            m_CurrLoadingParam = GameEntry.Pool.DequeueClassObject<BaseParams>();

            //加载Loading
            GameEntry.UI.OpenUIForm(UIFormId.Loading, onOpen: (UIFormBase formBase) =>
              {
                  m_CurrProgress = 0;
                  m_TargetProgressDic.Clear();

                  m_CurrSceneIsLoading = true;
                  m_CurrLoadSceneId = sceneId;

                  //先卸载当前场景
                  UnLoadCurrScene();
              });
        }

        /// <summary>
        /// 卸载当前场景
        /// </summary>
        private void UnLoadCurrScene()
        {
            if (m_CurrSceneEntity != null)
            {
                m_NeedLoadOrUnLoadSceneDetailCount = m_CurrSceneDetailList.Count;
                for (int i = 0; i < m_NeedLoadOrUnLoadSceneDetailCount; i++)
                {
                    SceneLoaderRoutine routine = GameEntry.Pool.DequeueClassObject<SceneLoaderRoutine>();
                    m_SceneLoaderList.AddLast(routine);
                    routine.UnLoadScene(m_CurrSceneDetailList[i].ScenePath, OnUnLoadSceneComplete);
                }
            }
            else
            {
                LoadNewScene();
            }
        }

        /// <summary>
        /// 加载新场景
        /// </summary>
        private void LoadNewScene()
        {
            m_CurrSceneEntity = GameEntry.DataTable.DataTableManager.Sys_SceneDBModel.Get(m_CurrLoadSceneId);
            m_CurrSceneDetailList = GameEntry.DataTable.DataTableManager.Sys_SceneDetailDBModel.GetListBySceneId(m_CurrSceneEntity.Id, 2);
            m_NeedLoadOrUnLoadSceneDetailCount = m_CurrSceneDetailList.Count;

            for (int i = 0; i < m_NeedLoadOrUnLoadSceneDetailCount; i++)
            {
                SceneLoaderRoutine routine = GameEntry.Pool.DequeueClassObject<SceneLoaderRoutine>();
                m_SceneLoaderList.AddLast(routine);

                Sys_SceneDetailEntity entity = m_CurrSceneDetailList[i];
                routine.LoadScene(entity.Id, entity.ScenePath, OnLoadSceneProgressUpdate, OnLoadSceneComplete);
            }
        }

        private void OnLoadSceneComplete(SceneLoaderRoutine routine)
        {
            m_SceneLoaderList.Remove(routine);
            GameEntry.Pool.EnqueueClassObject(routine);
        }

        private void OnLoadSceneProgressUpdate(int sceneDetailId, float progress)
        {
            //记录每个场景明细当前的进度
            m_TargetProgressDic[sceneDetailId] = progress;
            Debug.LogError("sceneDetailId="+ sceneDetailId);
            Debug.LogError("progress=" + progress);
        }

        private void OnUnLoadSceneComplete(SceneLoaderRoutine routine)
        {
            m_SceneLoaderList.Remove(routine);
            GameEntry.Pool.EnqueueClassObject(routine);

            m_CurrLoadOrUnLoadSceneDetailCount++;
            if (m_CurrLoadOrUnLoadSceneDetailCount == m_NeedLoadOrUnLoadSceneDetailCount)
            {
                Resources.UnloadUnusedAssets();
                m_NeedLoadOrUnLoadSceneDetailCount = 0;
                m_CurrLoadOrUnLoadSceneDetailCount = 0;
                LoadNewScene();
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        public void OnUpdate()
        {
            if (m_CurrSceneIsLoading)
            {
                var curr = m_SceneLoaderList.First;
                while (curr != null)
                {
                    curr.Value.OnUpdate();
                    curr = curr.Next;
                }

                float currTarget = GetCurrTotalProgress();
                float finalTarget = 0.9f * m_NeedLoadOrUnLoadSceneDetailCount;
                if (currTarget >= finalTarget)
                {
                    currTarget = m_NeedLoadOrUnLoadSceneDetailCount;
                }

                if (m_CurrProgress < m_NeedLoadOrUnLoadSceneDetailCount && m_CurrProgress <= currTarget)
                {
                    m_CurrProgress = m_CurrProgress + Time.deltaTime * m_NeedLoadOrUnLoadSceneDetailCount * 1;
                    m_CurrLoadingParam.IntParam1 = (int)LoadingType.ChangeScene;
                    m_CurrLoadingParam.FloatParam1 = (m_CurrProgress / m_NeedLoadOrUnLoadSceneDetailCount);
                    GameEntry.Event.CommonEvent.Dispatch(SysEventId.LoadingProgressChange, m_CurrLoadingParam);

                    Debug.LogError("m_CurrProgress=" + (m_CurrProgress / m_NeedLoadOrUnLoadSceneDetailCount));
                }
                else if (m_CurrProgress >= m_NeedLoadOrUnLoadSceneDetailCount)
                {
                    Debug.LogError("场景加载完毕");
                    m_NeedLoadOrUnLoadSceneDetailCount = 0;
                    m_CurrLoadOrUnLoadSceneDetailCount = 0;
                    m_CurrSceneIsLoading = false;
                    GameEntry.UI.CloseUIForm(UIFormId.Loading);
                    m_CurrLoadingParam.Reset();
                    GameEntry.Pool.EnqueueClassObject(m_CurrLoadingParam);
                }

            }
        }

        /// <summary>
        /// 获取当前的总进度
        /// </summary>
        /// <returns></returns>
        private float GetCurrTotalProgress()
        {
            float progress = 0;
            var lst = m_TargetProgressDic.GetEnumerator();
            while (lst.MoveNext())
            {
                progress += lst.Current.Value;
            }
            return progress;
        }
    }
}
