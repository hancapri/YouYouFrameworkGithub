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

        /// <summary>
        /// 章节表
        /// </summary>
        public ChapterDBModel ChapterDBModel { get; private set;}

        /// <summary>
        /// 游戏关卡表
        /// </summary>
        public GameLevelDBModel GameLevelDBModel { get; private set; }

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
            //load完毕
        }

        /// <summary>
        /// 异步加载表格
        /// </summary>
        public void LoadDataTableAsync()
        {
            Task.Factory.StartNew(LoadDataTable);
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

            //所有表加载完毕
            GameEntry.Event.CommonEvent.Dispatch(SysEventId.LoadDataTableComplete);
        }

        public void Clear()
        {
            //每个表都clear
            ChapterDBModel.Clear();
        }
    }
}
