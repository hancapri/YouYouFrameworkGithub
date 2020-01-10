using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace YouYouFramework
{
    /// <summary>
    /// 数据表管理器
    /// </summary>
    public class DataTableManager : ManagerBase
    {
        public Sys_CodeDBModel Sys_CodeDBModel { get; private set; }
        public Sys_EffectDBModel Sys_EffectDBModel { get; private set; }
        public Sys_PrefabDBModel Sys_PrefabDBModel { get; private set; }
        public Sys_SoundDBModel Sys_SoundDBModel { get; private set; }
        public Sys_StorySoundDBModel Sys_StorySoundDBModel { get; private set; }
        public Sys_UIFormDBModel Sys_UIFormDBModel { get; private set; }
        public LocalizationDBModel LocalizationDBModel { get; private set; }
        public Sys_SceneDBModel Sys_SceneDBModel { get; private set; }
        public Sys_SceneDetailDBModel Sys_SceneDetailDBModel { get; private set; }
        /// <summary>
        /// 章节表
        /// </summary>
        public ChapterDBModel ChapterDBModel { get; private set;}

        /// <summary>
        /// 游戏关卡表
        /// </summary>
        public GameLevelDBModel GameLevelDBModel { get; private set; }

        /// <summary>
        /// 总共需要加载的表格数量
        /// </summary>
        public int TotalTableCount = 0;

        /// <summary>
        /// 当前已加载的表格数量
        /// </summary>
        public int CurrLoadTableCount = 0;

        public DataTableManager()
        {
            InitDBModel();
        }

        /// <summary>
        /// 初始化表格
        /// </summary>
        private void InitDBModel()
        {
            //每个表都new
            ChapterDBModel = new ChapterDBModel();
            GameLevelDBModel = new GameLevelDBModel();
            Sys_CodeDBModel = new Sys_CodeDBModel();
            Sys_EffectDBModel = new Sys_EffectDBModel();
            Sys_PrefabDBModel = new Sys_PrefabDBModel();
            Sys_SoundDBModel = new Sys_SoundDBModel();
            Sys_StorySoundDBModel = new Sys_StorySoundDBModel();
            Sys_UIFormDBModel = new Sys_UIFormDBModel();
            LocalizationDBModel = new LocalizationDBModel();
            Sys_SceneDBModel = new Sys_SceneDBModel();
            Sys_SceneDetailDBModel = new Sys_SceneDetailDBModel();
            //load完毕
        }

        /// <summary>
        /// 表格ab包
        /// </summary>
        public AssetBundle m_DataTableBundle;
        
        /// <summary>
        /// 异步加载表格
        /// </summary>
        public void LoadDataTableAsync()
        {
#if DISABLE_ASSETBUNDLE
            Task.Factory.StartNew(LoadDataTable);
#else
            GameEntry.Resource.ResourceLoaderManager.LoadAssetBundle(ConstDefine.DataTableAssetBundlePath, onComplete:(AssetBundle assetBundle)=>
            {
                m_DataTableBundle = assetBundle;
                LoadDataTable();
            });
#endif
        }

        public void LoadDataTable()
        {
            //可能在lua中读取load，只load自己需要的表
            //每个表都LoadData
            ChapterDBModel.LoadData();
            GameLevelDBModel.LoadData();
            Sys_CodeDBModel.LoadData();
            Sys_EffectDBModel.LoadData();
            Sys_PrefabDBModel.LoadData();
            Sys_SoundDBModel.LoadData();
            Sys_StorySoundDBModel.LoadData();
            Sys_UIFormDBModel.LoadData();
            LocalizationDBModel.LoadData();
            Sys_SceneDBModel.LoadData();
            Sys_SceneDetailDBModel.LoadData();
        }

        /// <summary>
        /// 获取表格的数组
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="onComplete"></param>
        public void GetDataTableBuffer(string tableName, BaseAction<byte[]> onComplete)
        {
#if DISABLE_ASSETBUNDLE
            byte[] buffer = GameEntry.Resource.GetFileBuffer(string.Format("{0}/download/DataTable/{1}.bytes", GameEntry.Resource.LocalFilePath, tableName));
            if (onComplete != null)
            {
                onComplete(buffer);
            }
#else
            GameEntry.Resource.ResourceLoaderManager.LoadAsset(GameEntry.Resource.GetLastPathName(tableName),m_DataTableBundle,onComplete:(UnityEngine.Object obj)=>
            {
                TextAsset asset = obj as TextAsset;
                if (onComplete != null)
                {
                    onComplete(asset.bytes);
                }
            });
#endif
        }

        public void Clear()
        {
            //每个表都clear
            ChapterDBModel.Clear();
        }
    }
}
