
//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2016-12-18 15:20:11
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;

/// <summary>
/// Job实体
/// </summary>
public partial class JobEntity : AbstractEntity
{
    /// <summary>
    /// 职业名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 头像
    /// </summary>
    public string HeadPic { get; set; }

    /// <summary>
    /// 职业半身像
    /// </summary>
    public string JobPic { get; set; }

    /// <summary>
    /// 预设名称
    /// </summary>
    public string PrefabName { get; set; }

    /// <summary>
    /// 职业描述
    /// </summary>
    public string Desc { get; set; }

    /// <summary>
    /// 系数---攻击
    /// </summary>
    public int Attack { get; set; }

    /// <summary>
    /// 系数--防御
    /// </summary>
    public int Defense { get; set; }

    /// <summary>
    /// 系数--命中率
    /// </summary>
    public int Hit { get; set; }

    /// <summary>
    /// 系数--闪避率
    /// </summary>
    public int Dodge { get; set; }

    /// <summary>
    /// 系数--暴击率
    /// </summary>
    public int Cri { get; set; }

    /// <summary>
    /// 系数--抗性
    /// </summary>
    public int Res { get; set; }

    /// <summary>
    /// 使用的物理攻击Id
    /// </summary>
    public string UsedPhyAttackIds { get; set; }

    /// <summary>
    /// 使用的技能Id
    /// </summary>
    public string UsedSkillIds { get; set; }

}
