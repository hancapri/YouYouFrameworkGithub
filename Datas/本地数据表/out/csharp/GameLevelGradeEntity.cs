
//===================================================
//作    者：边涯  http://www.u3dol.com
//创建时间：2018-10-11 13:06:31
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;

/// <summary>
/// GameLevelGrade实体
/// </summary>
public partial class GameLevelGradeEntity : AbstractEntity
{
    /// <summary>
    /// 游戏关卡编号
    /// </summary>
    public int GameLevelId { get; set; }

    /// <summary>
    /// 难度等级分类 0=普通 1=困难 2=地狱
    /// </summary>
    public int Grade { get; set; }

    /// <summary>
    /// 关卡描述
    /// </summary>
    public string Desc { get; set; }

    /// <summary>
    /// 关卡类型
    /// </summary>
    public int Type { get; set; }

    /// <summary>
    /// 关卡类型相关数据
    /// </summary>
    public string Parameter { get; set; }

    /// <summary>
    /// 过关条件描述文字
    /// </summary>
    public string ConditionDesc { get; set; }

    /// <summary>
    /// 奖励经验
    /// </summary>
    public int Exp { get; set; }

    /// <summary>
    /// 奖励金币
    /// </summary>
    public int Gold { get; set; }

    /// <summary>
    /// 推荐战力
    /// </summary>
    public int CommendFighting { get; set; }

    /// <summary>
    /// 时间限制
    /// </summary>
    public float TimeLimit { get; set; }

    /// <summary>
    /// 时间1
    /// </summary>
    public float Star1 { get; set; }

    /// <summary>
    /// 时间2
    /// </summary>
    public float Star2 { get; set; }

    /// <summary>
    /// 奖励装备 装备Id_概率_数量|…
    /// </summary>
    public string Equip { get; set; }

    /// <summary>
    /// 奖励道具 道具Id_概率_数量|…
    /// </summary>
    public string Item { get; set; }

    /// <summary>
    /// 奖励材料 材料Id_概率_数量|…
    /// </summary>
    public string Material { get; set; }

}
