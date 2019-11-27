using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 渠道设置
/// </summary>
public class ChannelConfigEntity {
    /// <summary>
    /// 渠道号
    /// </summary>
    public short ChannelId;

    /// <summary>
    /// 内部版本号
    /// </summary>
    public short InnerVersion;

    /// <summary>
    /// 服务器时间
    /// </summary>
    public long ServerTime;

    /// <summary>
    /// 资源版本号
    /// </summary>
    public string SourceVersion;

    /// <summary>
    /// 资源地址
    /// </summary>
    public string SourceUrl;

    /// <summary>
    /// 充值地址
    /// </summary>
    public string RechargeUrl;

    /// <summary>
    /// TDAppId
    /// </summary>
    public string TDAppId;

    /// <summary>
    /// 是否开启统计
    /// </summary>
    public bool IsOpenTD;

    /// <summary>
    /// 充值服务器编号
    /// </summary>
    public short PayServerNo;

    private string m_RealSourceUrl;
    /// <summary>
    /// 真实资源地址
    /// </summary>
    public string RealSourceUrl
    {
        get
        {
            if (string.IsNullOrEmpty(m_RealSourceUrl))
            {
                string buildTarget = string.Empty;
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
                buildTarget = "Windows";
#elif UNITY_ANDROID
                buildTarget = "Android";
#elif UNITY_IPHONE
                buildTarget = "iOS";
#endif
                m_RealSourceUrl = string.Format("{0}{1}/{2}/",SourceUrl,SourceVersion,buildTarget);
            }
            return m_RealSourceUrl;
        }
    }
}
