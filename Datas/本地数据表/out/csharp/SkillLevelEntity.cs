
//===================================================
//作    者：边涯  http://www.u3dol.com
//创建时间：2019-03-13 22:23:43
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;
using YouYouFramework;

/// <summary>
/// SkillLevel实体
/// </summary>
public partial class SkillLevelEntity : DataTableEntityBase
{
    /// <summary>
    /// 技能编号
    /// </summary>
    public int SkillId;

    /// <summary>
    /// 技能等级
    /// </summary>
    public int Level;

    /// <summary>
    /// 技能伤害率
    /// </summary>
    public int HurtValueRate;

    /// <summary>
    /// 技能消耗的魔法值
    /// </summary>
    public int SpendMP;

    /// <summary>
    /// 冰冻眩晕时间
    /// </summary>
    public float StateTime;

    /// <summary>
    /// 附加异常状态的几率（%）
    /// </summary>
    public float AbnormalRatio;

    /// <summary>
    /// 中毒烧伤伤害次数
    /// </summary>
    public int AStateTimes;

    /// <summary>
    /// 中毒烧伤每秒掉血
    /// </summary>
    public int AStatexiaohao;

    /// <summary>
    /// 此技能的CD间隔秒数
    /// </summary>
    public float SkillCDTime;

    /// <summary>
    /// Buff使用几率(百分比)
    /// </summary>
    public float BuffChance;

    /// <summary>
    /// Buff持续时间（秒）
    /// </summary>
    public float BuffDuration;

    /// <summary>
    /// Buff效果值
    /// </summary>
    public int BuffValue;

    /// <summary>
    /// 升级所需主角的等级
    /// </summary>
    public int NeedCharacterLevel;

    /// <summary>
    /// 升级消耗金币
    /// </summary>
    public int SpendGold;

}
