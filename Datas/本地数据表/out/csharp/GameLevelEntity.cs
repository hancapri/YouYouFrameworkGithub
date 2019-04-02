
//===================================================
//作    者：边涯  http://www.u3dol.com
//创建时间：2019-04-02 14:51:31
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;
using YouYouFramework;

/// <summary>
/// GameLevel实体
/// </summary>
public partial class GameLevelEntity : DataTableEntityBase
{
    /// <summary>
    /// 所属章编号
    /// </summary>
    public int ChapterID;

    /// <summary>
    /// 游戏关卡名称
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
    /// 是否boss关卡
    /// </summary>
    public int isBoss;

    /// <summary>
    /// 关卡图标
    /// </summary>
    public string Ico;

    /// <summary>
    /// 地图上的节坐标(x_y)
    /// </summary>
    public string PosInMap;

    /// <summary>
    /// 关卡图片
    /// </summary>
    public string DlgPic;

    /// <summary>
    /// 镜头的旋转角度
    /// </summary>
    public string CameraRotation;

    /// <summary>
    /// 背景音乐
    /// </summary>
    public string Audio_BG;

}
