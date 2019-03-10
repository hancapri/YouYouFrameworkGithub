
//===================================================
//作    者：边涯  http://www.u3dol.com
//创建时间：2018-10-11 13:06:31
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;

/// <summary>
/// Chapter实体
/// </summary>
public partial class ChapterEntity : AbstractEntity
{
    /// <summary>
    /// 章名称
    /// </summary>
    public string ChapterName { get; set; }

    /// <summary>
    /// 拥有关卡个数
    /// </summary>
    public int GameLevelCount { get; set; }

    /// <summary>
    /// 背景图
    /// </summary>
    public string BG_Pic { get; set; }

    /// <summary>
    /// Uvx
    /// </summary>
    public float Uvx { get; set; }

    /// <summary>
    /// Uvy
    /// </summary>
    public float Uvy { get; set; }

}
