
//===================================================
//作    者：边涯  http://www.u3dol.com
//创建时间：2018-10-11 13:06:32
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;

/// <summary>
/// Sprite实体
/// </summary>
public partial class SpriteEntity : AbstractEntity
{
    /// <summary>
    /// 精灵类型
    /// </summary>
    public int SpriteType { get; set; }

    /// <summary>
    /// 名字
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 精灵等级
    /// </summary>
    public int Level { get; set; }

    /// <summary>
    /// 是否是boss
    /// </summary>
    public int IsBoss { get; set; }

    /// <summary>
    /// 预设名称
    /// </summary>
    public string PrefabName { get; set; }

    /// <summary>
    /// 贴图文件
    /// </summary>
    public string TextureName { get; set; }

    /// <summary>
    /// 头像
    /// </summary>
    public string HeadPic { get; set; }

    /// <summary>
    /// 移动速度
    /// </summary>
    public float MoveSpeed { get; set; }

    /// <summary>
    /// 当前等级下最大hp
    /// </summary>
    public int HP { get; set; }

    /// <summary>
    /// 当前等级下最大mp
    /// </summary>
    public int MP { get; set; }

    /// <summary>
    /// 攻击
    /// </summary>
    public int Attack { get; set; }

    /// <summary>
    /// 防御
    /// </summary>
    public int Defense { get; set; }

    /// <summary>
    /// 命中
    /// </summary>
    public int Hit { get; set; }

    /// <summary>
    /// 闪避
    /// </summary>
    public int Dodge { get; set; }

    /// <summary>
    /// 暴击
    /// </summary>
    public int Cri { get; set; }

    /// <summary>
    /// 抗性
    /// </summary>
    public int Res { get; set; }

    /// <summary>
    /// 综合战斗力
    /// </summary>
    public int Fighting { get; set; }

    /// <summary>
    /// 是否在战斗界面的UI上显示血条
    /// </summary>
    public int ShowBloodBar { get; set; }

    /// <summary>
    /// 血条层数，默认为1
    /// </summary>
    public int BloodBarLayerCount { get; set; }

    /// <summary>
    /// 可使用的物理Id
    /// </summary>
    public string UsedPhyAttack { get; set; }

    /// <summary>
    /// 可使用的技能Id
    /// </summary>
    public string UsedSkillList { get; set; }

    /// <summary>
    /// 是否会霸体
    /// </summary>
    public int CanArmor { get; set; }

    /// <summary>
    /// 这个当血量小于等于百分之多少后进入霸体状态
    /// </summary>
    public int Armor_HP_Percentage { get; set; }

    /// <summary>
    /// 这个怪的视野范围
    /// </summary>
    public int Range_View { get; set; }

    /// <summary>
    /// 每隔多少秒攻击一次
    /// </summary>
    public float Attack_Interval { get; set; }

    /// <summary>
    /// 物理攻击的概率%
    /// </summary>
    public int PhysicalAttackRate { get; set; }

    /// <summary>
    /// 当主角进入其攻击范围后，延迟多少秒开始攻击
    /// </summary>
    public float DelaySec_Attack { get; set; }

    /// <summary>
    /// 当这个精灵死亡后，玩家获取的经验数
    /// </summary>
    public int RewardExp { get; set; }

}
