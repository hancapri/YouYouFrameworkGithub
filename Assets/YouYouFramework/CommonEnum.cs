using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YouYouFramework
{
    /// <summary>
    /// 日志分类
    /// </summary>
    public enum LogCategory
    {
        /// <summary>
        /// 普通日志
        /// </summary>
        Normal,

        /// <summary>
        /// 流程日志
        /// </summary>
        Procedure,

        /// <summary>
        /// 资源日志
        /// </summary>
        Resource,

        /// <summary>
        /// 协议日志
        /// </summary>
        Proto
    }

    /// <summary>
    /// 资源分类
    /// </summary>
    public enum AssetCategory
    {
        /// <summary>
        /// 声音
        /// </summary>
        Audio,
        /// <summary>
        /// 自定义shader
        /// </summary>
        CusShaders,
        /// <summary>
        /// 表格
        /// </summary>
        DataTable,
        /// <summary>
        /// 特效资源
        /// </summary>
        EffectSources,
        /// <summary>
        /// 角色特效预设
        /// </summary>
        RoleEffectPrefab,
        /// <summary>
        /// UI特效预设
        /// </summary>
        UIEffectPrefab,
        /// <summary>
        /// 角色预设
        /// </summary>
        RolePrefab,
        /// <summary>
        /// 角色资源
        /// </summary>
        RoleSources,
        /// <summary>
        /// 场景
        /// </summary>
        Scenes,
        /// <summary>
        /// 字体
        /// </summary>
        UIFont,
        /// <summary>
        /// UI预设
        /// </summary>
        UIPrefab,
        /// <summary>
        /// UI资源
        /// </summary>
        UIRes,
        /// <summary>
        /// Lua脚本
        /// </summary>
        xLuaLogic
    }
}
