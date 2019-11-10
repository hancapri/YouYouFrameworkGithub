
//===================================================
//作    者：边涯  http://www.u3dol.com
//创建时间：2019-11-10 11:54:29
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;
using YouYouFramework;

/// <summary>
/// Sprite实体
/// </summary>
public partial class SpriteEntity : DataTableEntityBase
{
    /// <summary>
    /// 精灵类型
    /// </summary>
    public int SpriteType;

    /// <summary>
    /// 名字
    /// </summary>
    public string Name;

    /// <summary>
    /// 精灵等级
    /// </summary>
    public int Level;

    /// <summary>
    /// 是否是boss
    /// </summary>
    public int IsBoss;

    /// <summary>
    /// 预设名称
    /// </summary>
    public string PrefabName;

    /// <summary>
    /// 贴图文件
    /// </summary>
    public string TextureName;

    /// <summary>
    /// 头像
    /// </summary>
    public string HeadPic;

    /// <summary>
    /// 移动速度
    /// </summary>
    public float MoveSpeed;

    /// <summary>
    /// 当前等级下最大hp
    /// </summary>
    public int HP;

    /// <summary>
    /// 当前等级下最大mp
    /// </summary>
    public int MP;

    /// <summary>
    /// 攻击
    /// </summary>
    public int Attack;

    /// <summary>
    /// 防御
    /// </summary>
    public int Defense;

    /// <summary>
    /// 命中
    /// </summary>
    public int Hit;

    /// <summary>
    /// 闪避
    /// </summary>
    public int Dodge;

    /// <summary>
    /// 暴击
    /// </summary>
    public int Cri;

    /// <summary>
    /// 抗性
    /// </summary>
    public int Res;

    /// <summary>
    /// 综合战斗力
    /// </summary>
    public int Fighting;

    /// <summary>
    /// 是否在战斗界面的UI上显示血条
    /// </summary>
    public int ShowBloodBar;

    /// <summary>
    /// 血条层数，默认为1
    /// </summary>
    public int BloodBarLayerCount;

    /// <summary>
    /// 可使用的物理Id
    /// </summary>
    public string UsedPhyAttack;

    /// <summary>
    /// 可使用的技能Id
    /// </summary>
    public string UsedSkillList;

    /// <summary>
    /// 是否会霸体
    /// </summary>
    public int CanArmor;

    /// <summary>
    /// 这个当血量小于等于百分之多少后进入霸体状态
    /// </summary>
    public int Armor_HP_Percentage;

    /// <summary>
    /// 这个怪的视野范围
    /// </summary>
    public int Range_View;

    /// <summary>
    /// 每隔多少秒攻击一次
    /// </summary>
    public float Attack_Interval;

    /// <summary>
    /// 物理攻击的概率%
    /// </summary>
    public int PhysicalAttackRate;

    /// <summary>
    /// 当主角进入其攻击范围后，延迟多少秒开始攻击
    /// </summary>
    public float DelaySec_Attack;

    /// <summary>
    /// 当这个精灵死亡后，玩家获取的经验数
    /// </summary>
    public int RewardExp;

}
