
//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2017-11-08 16:37:39
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;

/// <summary>
/// JobLevel实体
/// </summary>
public partial class JobLevelEntity : AbstractEntity
{
    /// <summary>
    /// 等级
    /// </summary>
    public int Level { get; set; }

    /// <summary>
    /// 从本级升到下一级所需经验
    /// </summary>
    public int NeedExp { get; set; }

    /// <summary>
    /// 体力
    /// </summary>
    public int Energy { get; set; }

    /// <summary>
    /// 基础血量
    /// </summary>
    public int HP { get; set; }

    /// <summary>
    /// 基础魔法值
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

}
