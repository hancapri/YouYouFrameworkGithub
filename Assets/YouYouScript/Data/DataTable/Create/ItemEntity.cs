
//===================================================
//作    者：边涯  http://www.u3dol.com
//创建时间：2019-11-07 20:46:04
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;
using YouYouFramework;

/// <summary>
/// Item实体
/// </summary>
public partial class ItemEntity : DataTableEntityBase
{
    /// <summary>
    /// 名称
    /// </summary>
    public string Name;

    /// <summary>
    /// 道具类别
    /// </summary>
    public int Type;

    /// <summary>
    /// 使用等级
    /// </summary>
    public int UsedLevel;

    /// <summary>
    /// 使用方法
    /// </summary>
    public string UsedMethod;

    /// <summary>
    /// 售价
    /// </summary>
    public int SellMoney;

    /// <summary>
    /// 品质
    /// </summary>
    public int Quality;

    /// <summary>
    /// 描述
    /// </summary>
    public string Description;

    /// <summary>
    /// 道具使用后获得的内容
    /// </summary>
    public string UsedItems;

    /// <summary>
    /// 最大堆叠数量
    /// </summary>
    public int maxAmount;

    /// <summary>
    /// 背包陈列顺序
    /// </summary>
    public int packSort;

}
