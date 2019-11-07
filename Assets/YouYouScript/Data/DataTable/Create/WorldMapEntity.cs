
//===================================================
//作    者：边涯  http://www.u3dol.com
//创建时间：2019-11-07 20:46:06
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;
using YouYouFramework;

/// <summary>
/// WorldMap实体
/// </summary>
public partial class WorldMapEntity : DataTableEntityBase
{
    /// <summary>
    /// 名称
    /// </summary>
    public string Name;

    /// <summary>
    /// 场景名称
    /// </summary>
    public string SceneName;

    /// <summary>
    /// 小地图图片
    /// </summary>
    public string SmallMapImg;

    /// <summary>
    /// NPC列表
    /// </summary>
    public string NPCList;

    /// <summary>
    /// 主角出生点坐标
    /// </summary>
    public string RoleBirthPos;

    /// <summary>
    /// 摄像机旋转角度
    /// </summary>
    public string CameraRotation;

    /// <summary>
    /// 传送点（坐标_y轴旋转_传送点编号_要传送的场景Id_目标场景出生传送点id）
    /// </summary>
    public string TransPos;

    /// <summary>
    /// 是否主城
    /// </summary>
    public int IsCity;

    /// <summary>
    /// 是否在地图上显示
    /// </summary>
    public int IsShowInMap;

    /// <summary>
    /// 在地图上的坐标
    /// </summary>
    public string PosInMap;

    /// <summary>
    /// 使用的图标
    /// </summary>
    public string IcoInMap;

    /// <summary>
    /// 关联场景
    /// </summary>
    public string NearScene;

    /// <summary>
    /// 背景音乐
    /// </summary>
    public string Audio_BG;

}
