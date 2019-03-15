using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YouYouFramework
{
    /// <summary>
    /// 系统相关数据
    /// </summary>
    public class SysData : IDisposable
    {
        /// <summary>
        /// 当前服务器时间
        /// </summary>
        public long CurrServerTime;

        public SysData()
        {

        }

        /// <summary>
        /// 清理数据
        /// </summary>
        public void Clear()
        {

        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
