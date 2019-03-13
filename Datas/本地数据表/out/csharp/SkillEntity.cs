
//===================================================
//作    者：边涯  http://www.u3dol.com
//创建时间：2019-03-13 22:23:43
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;
using YouYouFramework;

/// <summary>
/// Skill实体
/// </summary>
public partial class SkillEntity : DataTableEntityBase
{
    /// <summary>
    /// 技能名称
    /// </summary>
    public string SkillName;

    /// <summary>
    /// 技能描述
    /// </summary>
    public string SkillDesc;

    /// <summary>
    /// 技能释放按钮图片
    /// </summary>
    public string SkillPic;

    /// <summary>
    /// 技能最大等级
    /// </summary>
    public int LevelLimit;

    /// <summary>
    /// 是否物理攻击
    /// </summary>
    public int IsPhyAttack;

    /// <summary>
    /// 伤害目标数量
    /// </summary>
    public int AttackTargetCount;

    /// <summary>
    /// 此技能攻击攻击范围(米)
    /// </summary>
    public float AttackRange;

    /// <summary>
    /// 群攻的伤害判定半径
    /// </summary>
    public float AreaAttackRadius;

    /// <summary>
    /// 攻击动作发出多少秒后被攻击者才播放受伤效果
    /// </summary>
    public float ShowHurtEffectDelaySecond;

    /// <summary>
    /// 主角被这个物理子攻击后，是否会导致屏幕四周泛红
    /// </summary>
    public int RedScreen;

    /// <summary>
    /// 攻击后状态
    /// </summary>
    public int AttackState;

    /// <summary>
    /// 附加异常状态
    /// </summary>
    public int AbnormalState;

    /// <summary>
    /// BuffInfoID
    /// </summary>
    public int BuffInfoID;

    /// <summary>
    /// Buff类型
    /// </summary>
    public int BuffTargetFilter;

    /// <summary>
    /// Buff效果值是否按百分比计算
    /// </summary>
    public int BuffIsPercentage;

}
