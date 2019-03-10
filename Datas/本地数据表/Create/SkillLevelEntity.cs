
//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2016-12-18 15:20:03
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;

/// <summary>
/// SkillLevel实体
/// </summary>
public partial class SkillLevelEntity : AbstractEntity
{
    /// <summary>
    /// 技能编号
    /// </summary>
    public int SkillId { get; set; }

    /// <summary>
    /// 技能等级
    /// </summary>
    public int Level { get; set; }

    /// <summary>
    /// 技能伤害率
    /// </summary>
    public int HurtValueRate { get; set; }

    /// <summary>
    /// 技能消耗的魔法值
    /// </summary>
    public int SpendMP { get; set; }

    /// <summary>
    /// 冰冻眩晕时间
    /// </summary>
    public float StateTime { get; set; }

    /// <summary>
    /// 附加异常状态的几率（%）
    /// </summary>
    public float AbnormalRatio { get; set; }

    /// <summary>
    /// 中毒烧伤伤害次数
    /// </summary>
    public int AStateTimes { get; set; }

    /// <summary>
    /// 中毒烧伤每秒掉血
    /// </summary>
    public int AStatexiaohao { get; set; }

    /// <summary>
    /// 此技能的CD间隔秒数
    /// </summary>
    public float SkillCDTime { get; set; }

    /// <summary>
    /// Buff使用几率(百分比)
    /// </summary>
    public float BuffChance { get; set; }

    /// <summary>
    /// Buff持续时间（秒）
    /// </summary>
    public float BuffDuration { get; set; }

    /// <summary>
    /// Buff效果值
    /// </summary>
    public int BuffValue { get; set; }

    /// <summary>
    /// 升级所需主角的等级
    /// </summary>
    public int NeedCharacterLevel { get; set; }

    /// <summary>
    /// 升级消耗金币
    /// </summary>
    public int SpendGold { get; set; }

}
