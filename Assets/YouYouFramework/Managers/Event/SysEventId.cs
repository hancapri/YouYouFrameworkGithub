using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YouYouFramework
{
    /// <summary>
    /// 系统事件编号(系统事件编号采用4位，10 表示模块，01表示编号 )
    /// </summary>
    public class SysEventId
    {
        /// <summary>
        /// 所有表格加载完毕
        /// </summary>
        public const ushort LoadDataTableComplete = 1001;

        /// <summary>
        /// 单个表格加载完毕
        /// </summary>
        public const ushort LoadOneDataTableComplete = 1002;
    }
}
