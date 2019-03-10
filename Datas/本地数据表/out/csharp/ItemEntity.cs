
//===================================================
//作    者：边涯  http://www.u3dol.com
//创建时间：2018-10-11 13:06:31
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;

/// <summary>
/// Item实体
/// </summary>
public partial class ItemEntity : AbstractEntity
{
    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 道具类别
    /// </summary>
    public int Type { get; set; }

    /// <summary>
    /// 使用等级
    /// </summary>
    public int UsedLevel { get; set; }

    /// <summary>
    /// 使用方法
    /// </summary>
    public string UsedMethod { get; set; }

    /// <summary>
    /// 售价
    /// </summary>
    public int SellMoney { get; set; }

    /// <summary>
    /// 品质
    /// </summary>
    public int Quality { get; set; }

    /// <summary>
    /// 描述
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// 道具使用后获得的内容
    /// </summary>
    public string UsedItems { get; set; }

    /// <summary>
    /// 最大堆叠数量
    /// </summary>
    public int maxAmount { get; set; }

    /// <summary>
    /// 背包陈列顺序
    /// </summary>
    public int packSort { get; set; }

}
