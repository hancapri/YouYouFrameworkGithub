
//===================================================
//作    者：边涯  http://www.u3dol.com
//创建时间：2019-11-10 11:54:28
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;
using YouYouFramework;

/// <summary>
/// Job实体
/// </summary>
public partial class JobEntity : DataTableEntityBase
{
    /// <summary>
    /// 职业名称
    /// </summary>
    public string Name;

    /// <summary>
    /// 头像
    /// </summary>
    public string HeadPic;

    /// <summary>
    /// 职业半身像
    /// </summary>
    public string JobPic;

    /// <summary>
    /// 预设名称
    /// </summary>
    public string PrefabName;

    /// <summary>
    /// 职业描述
    /// </summary>
    public string Desc;

    /// <summary>
    /// 系数---攻击
    /// </summary>
    public int Attack;

    /// <summary>
    /// 系数--防御
    /// </summary>
    public int Defense;

    /// <summary>
    /// 系数--命中率
    /// </summary>
    public int Hit;

    /// <summary>
    /// 系数--闪避率
    /// </summary>
    public int Dodge;

    /// <summary>
    /// 系数--暴击率
    /// </summary>
    public int Cri;

    /// <summary>
    /// 系数--抗性
    /// </summary>
    public int Res;

    /// <summary>
    /// 使用的物理攻击Id
    /// </summary>
    public string UsedPhyAttackIds;

    /// <summary>
    /// 使用的技能Id
    /// </summary>
    public string UsedSkillIds;

}
