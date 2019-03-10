
//===================================================
//作    者：边涯  http://www.u3dol.com
//创建时间：2018-10-11 13:06:31
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;

/// <summary>
/// Equip实体
/// </summary>
public partial class EquipEntity : AbstractEntity
{
    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 使用等级
    /// </summary>
    public int UsedLevel { get; set; }

    /// <summary>
    /// 品质
    /// </summary>
    public int Quality { get; set; }

    /// <summary>
    /// 星级
    /// </summary>
    public int Star { get; set; }

    /// <summary>
    /// 描述
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// 装备种类
    /// </summary>
    public int Type { get; set; }

    /// <summary>
    /// 售价
    /// </summary>
    public int SellMoney { get; set; }

    /// <summary>
    /// 基础属性1类型
    /// </summary>
    public int BackAttrOneType { get; set; }

    /// <summary>
    /// 基础属性1的值
    /// </summary>
    public int BackAttrOneValue { get; set; }

    /// <summary>
    /// 基础属性2类型
    /// </summary>
    public int BackAttrTwoType { get; set; }

    /// <summary>
    /// 基础属性2的值
    /// </summary>
    public int BackAttrTwoValue { get; set; }

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
    /// HP增加值
    /// </summary>
    public int HP { get; set; }

    /// <summary>
    /// MP增加值
    /// </summary>
    public int MP { get; set; }

    /// <summary>
    /// 装备的最大孔数
    /// </summary>
    public int maxHole { get; set; }

    /// <summary>
    /// 各个孔可以镶嵌的材料的子类型的总范围
    /// </summary>
    public string embedProps { get; set; }

    /// <summary>
    /// 强化时所用的材料的ID
    /// </summary>
    public int StrengthenItem { get; set; }

    /// <summary>
    /// 强化等级上限
    /// </summary>
    public int StrengthenLvMax { get; set; }

    /// <summary>
    /// 每级强化的能力值系数
    /// </summary>
    public string StrengthenValue { get; set; }

    /// <summary>
    /// 强化需要材料个数系数
    /// </summary>
    public string StrengthenItemNumber { get; set; }

    /// <summary>
    /// 强化需要金币系数
    /// </summary>
    public string StrengthenGold { get; set; }

    /// <summary>
    /// 强化成功率系数
    /// </summary>
    public string StrengthenRatio { get; set; }

}
