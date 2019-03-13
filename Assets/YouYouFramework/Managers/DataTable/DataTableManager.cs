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
