
//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2017-10-27 11:26:20
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;

/// <summary>
/// GameLevel实体
/// </summary>
public partial class GameLevelEntity : AbstractEntity
{
    /// <summary>
    /// 所属章编号
    /// </summary>
    public int ChapterID { get; set; }

    /// <summary>
    /// 游戏关卡名称
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
    /// 是否boss关卡
    /// </summary>
    public int isBoss { get; set; }

    /// <summary>
    /// 关卡图标
    /// </summary>
    public string Ico { get; set; }

    /// <summary>
    /// 地图上的节坐标(x_y)
    /// </summary>
    public string PosInMap { get; set; }

    /// <summary>
    /// 关卡图片
    /// </summary>
    public string DlgPic { get; set; }

    /// <summary>
    /// 镜头的旋转角度
    /// </summary>
    public string CameraRotation { get; set; }

    /// <summary>
    /// 背景音乐
    /// </summary>
    public string Audio_BG { get; set; }

}
