
//===================================================
//作    者：边涯  http://www.u3dol.com
//创建时间：2019-04-14 12:07:05
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;
using YouYou;

/// <summary>
/// Material实体
/// </summary>
public partial class MaterialEntity : DataTableEntityBase
{
    /// <summary>
    /// 名称
    /// </summary>
    public string Name;

    /// <summary>
    /// 品质
    /// </summary>
    public int Quality;

    /// <summary>
    /// 描述
    /// </summary>
    public string Description;

    /// <summary>
    /// 材料类别
    /// </summary>
    public int Type;

    /// <summary>
    /// 材料固定能力的种类
    /// </summary>
    public int FixedType;

    /// <summary>
    /// 材料固定能力增加的数值
    /// </summary>
    public int FixedAddValue;

    /// <summary>
    /// 最大堆叠数量
    /// </summary>
    public int maxAmount;

    /// <summary>
    /// 背包陈列顺序
    /// </summary>
    public int packSort;

    /// <summary>
    /// 合成后的材料ID_个数
    /// </summary>
    public string CompositionProps;

    /// <summary>
    /// 合成该物时所需的材料ID
    /// </summary>
    public int CompositionMaterialID;

    /// <summary>
    /// 合成后需要消耗的金币系数
    /// </summary>
    public string CompositionGold;

    /// <summary>
    /// 售价
    /// </summary>
    public int SellMoney;

}
