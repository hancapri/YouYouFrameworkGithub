//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2016-04-24 18:20:31
//备    注：
//===================================================
using UnityEngine;
using System.Collections;

[XLua.LuaCallCSharp]
public class ConstDefine
{
    /// <summary>
    /// 版本文件名称
    /// </summary>
    public const string VersionFileName = "VersionFile.bytes";

    /// <summary>
    /// 资源版本号
    /// </summary>
    public const string ResourceVersion = "ResourceVersion";

    public const string AssetInfoName = "AssetInfo.bytes";

#if UNITY_EDITOR
    public const string LogOn_AccountID = "LogOn_AccountID";
#else
    public const string LogOn_AccountID = "_LogOn_AccountID";
#endif

    public const string LogOn_AccountUserName = "LogOn_AccountUserName";
    public const string LogOn_AccountPwd = "LogOn_AccountPwd";

    public const string UILogOnView_btnLogOn = "UILogOnView_btnLogOn";
    public const string UILogOnView_btnToReg = "UILogOnView_btnToReg";

    public const string UIRegView_btnReg = "UIRegView_btnReg";
    public const string UIRegView_btnToLogOn = "UIRegView_btnToLogOn";

    public const string UIGmeServerEnterView_btnSelectGameServer = "UIGmeServerEnterView_btnSelectGameServer";
    public const string UIGmeServerEnterView_btnEnterGame = "UIGmeServerEnterView_btnEnterGame";

    //======================更新观察者相关 开始========================
    public const string RechargeOK = "RechargeOK"; //充值完毕
    public const string MoneyChange = "MoneyChange"; //元宝更新
    public const string GoldChange = "GoldChange"; //金币更新

    //======================更新观察者相关 结束========================


    //属性名称术语==============================================
    public const string JobId = "JobId";
    public const string NickName = "NickName";
    public const string Level = "Level";
    public const string Fighting = "Fighting";
    public const string Money = "Money";
    public const string Gold = "Gold";

    public const string CurrHP = "CurrHP";
    public const string MaxHP = "MaxHP";

    public const string CurrMP = "CurrMP";
    public const string MaxMP = "MaxMP";

    public const string CurrExp = "CurrExp";
    public const string MaxExp = "MaxExp";

    public const string Attack = "Attack";
    public const string Defense = "Defense";
    public const string Hit = "Hit";
    public const string Dodge = "Dodge";
    public const string Cri = "Cri";
    public const string Res = "Res";

    //剧情关卡
    public const string ChapterId = "ChapterId";
    public const string ChapterName = "ChapterName";
    public const string ChapterBG = "ChapterBG";

    public const string GameLevelList = "GameLevelList";
    public const string GameLevelId = "GameLevelId";
    public const string GameLevelName = "GameLevelName";
    public const string GameLevelPostion = "GameLevelPostion";
    public const string GameLevelisBoss = "GameLevelisBoss";
    public const string GameLevelIco = "GameLevelIco";
    public const string GameLevelDlgPic = "GameLevelDlgPic";

    public const string GameLevelExp = "GameLevelExp";
    public const string GameLevelGold = "GameLevelGold";
    public const string GameLevelDesc = "GameLevelDesc";
    public const string GameLevelConditionDesc = "GameLevelConditionDesc";
    public const string GameLevelCommendFighting = "GameLevelCommendFighting";

    public const string GameLevelReward = "GameLevelReward";

    public const string GameLevelPassTime = "GameLevelPassTime";
    public const string GameLevelStar = "GameLevelStar";

    public const string GoodsId = "GoodsId";
    public const string GoodsName = "GoodsName";
    public const string GoodsType = "GoodsType";



    //技能相关
    public const string SkillSlotsNo = "SkillSlotsNo";
    public const string SkillId = "SkillId";
    public const string SkillLevel = "SkillLevel";
    public const string SkillPic = "SkillPic";
    public const string SkillCDTime = "SkillCDTime";

    //世界地图
    public const string WorldMapList = "WorldMapList";
    public const string WorldMapId = "WorldMapId";
    public const string WorldMapName = "WorldMapName";
    public const string WorldMapPostion = "WorldMapPostion";
    public const string WorldMapIco = "WorldMapIco";
}