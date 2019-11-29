using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YouYouFramework
{
    /// <summary>
    /// 系统相关数据
    /// </summary>
    public class SysDataManager : IDisposable
    {
        /// <summary>
        /// 当前服务器时间
        /// </summary>
        public long CurrServerTime;

        public SysDataManager()
        {
            CurrChannelConfig = new ChannelConfigEntity();
        }

        /// <summary>
        /// 当前的渠道设置
        /// </summary>
        public ChannelConfigEntity CurrChannelConfig
        {
            get;
            private set;
        }

        /// <summary>
        /// 清理数据
        /// </summary>
        public void Clear()
        {

        }

        public void Dispose()
        {

        }
    }
}
