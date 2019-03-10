
//===================================================
//作    者：边涯  http://www.u3dol.com
//创建时间：2018-10-11 13:06:32
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;

/// <summary>
/// Skill实体
/// </summary>
public partial class SkillEntity : AbstractEntity
{
    /// <summary>
    /// 技能名称
    /// </summary>
    public string SkillName { get; set; }

    /// <summary>
    /// 技能描述
    /// </summary>
    public string SkillDesc { get; set; }

    /// <summary>
    /// 技能释放按钮图片
    /// </summary>
    public string SkillPic { get; set; }

    /// <summary>
    /// 技能最大等级
    /// </summary>
    public int LevelLimit { get; set; }

    /// <summary>
    /// 是否物理攻击
    /// </summary>
    public int IsPhyAttack { get; set; }

    /// <summary>
    /// 伤害目标数量
    /// </summary>
    public int AttackTargetCount { get; set; }

    /// <summary>
    /// 此技能攻击攻击范围(米)
    /// </summary>
    public float AttackRange { get; set; }

    /// <summary>
    /// 群攻的伤害判定半径
    /// </summary>
    public float AreaAttackRadius { get; set; }

    /// <summary>
    /// 攻击动作发出多少秒后被攻击者才播放受伤效果
    /// </summary>
    public float ShowHurtEffectDelaySecond { get; set; }

    /// <summary>
    /// 主角被这个物理子攻击后，是否会导致屏幕四周泛红
    /// </summary>
    public int RedScreen { get; set; }

    /// <summary>
    /// 攻击后状态
    /// </summary>
    public int AttackState { get; set; }

    /// <summary>
    /// 附加异常状态
    /// </summary>
    public int AbnormalState { get; set; }

    /// <summary>
    /// BuffInfoID
    /// </summary>
    public int BuffInfoID { get; set; }

    /// <summary>
    /// Buff类型
    /// </summary>
    public int BuffTargetFilter { get; set; }

    /// <summary>
    /// Buff效果值是否按百分比计算
    /// </summary>
    public int BuffIsPercentage { get; set; }

}
