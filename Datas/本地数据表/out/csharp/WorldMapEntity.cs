
//===================================================
//作    者：边涯  http://www.u3dol.com
//创建时间：2018-10-11 13:06:32
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;

/// <summary>
/// WorldMap实体
/// </summary>
public partial class WorldMapEntity : AbstractEntity
{
    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 场景名称
    /// </summary>
    public string SceneName { get; set; }

    /// <summary>
    /// 小地图图片
    /// </summary>
    public string SmallMapImg { get; set; }

    /// <summary>
    /// NPC列表
    /// </summary>
    public string NPCList { get; set; }

    /// <summary>
    /// 主角出生点坐标
    /// </summary>
    public string RoleBirthPos { get; set; }

    /// <summary>
    /// 摄像机旋转角度
    /// </summary>
    public string CameraRotation { get; set; }

    /// <summary>
    /// 传送点（坐标_y轴旋转_传送点编号_要传送的场景Id_目标场景出生传送点id）
    /// </summary>
    public string TransPos { get; set; }

    /// <summary>
    /// 是否主城
    /// </summary>
    public int IsCity { get; set; }

    /// <summary>
    /// 是否在地图上显示
    /// </summary>
    public int IsShowInMap { get; set; }

    /// <summary>
    /// 在地图上的坐标
    /// </summary>
    public string PosInMap { get; set; }

    /// <summary>
    /// 使用的图标
    /// </summary>
    public string IcoInMap { get; set; }

    /// <summary>
    /// 关联场景
    /// </summary>
    public string NearScene { get; set; }

    /// <summary>
    /// 背景音乐
    /// </summary>
    public string Audio_BG { get; set; }

}
