SkillEntity = { Id = 0, SkillName = "", SkillDesc = "", SkillPic = "", LevelLimit = 0, IsPhyAttack = 0, AttackTargetCount = 0, AttackRange = 0, AreaAttackRadius = 0, ShowHurtEffectDelaySecond = 0, RedScreen = 0, AttackState = 0, AbnormalState = 0, BuffInfoID = 0, BuffTargetFilter = 0, BuffIsPercentage = 0 }

--这句是重定义元表的索引，就是说有了这句，这个才是一个类
SkillEntity.__index = SkillEntity;

function SkillEntity.New(Id, SkillName, SkillDesc, SkillPic, LevelLimit, IsPhyAttack, AttackTargetCount, AttackRange, AreaAttackRadius, ShowHurtEffectDelaySecond, RedScreen, AttackState, AbnormalState, BuffInfoID, BuffTargetFilter, BuffIsPercentage)
    local self = { }; --初始化self
    setmetatable(self, SkillEntity); --将self的元表设定为Class

    self.Id = Id;
    self.SkillName = SkillName;
    self.SkillDesc = SkillDesc;
    self.SkillPic = SkillPic;
    self.LevelLimit = LevelLimit;
    self.IsPhyAttack = IsPhyAttack;
    self.AttackTargetCount = AttackTargetCount;
    self.AttackRange = AttackRange;
    self.AreaAttackRadius = AreaAttackRadius;
    self.ShowHurtEffectDelaySecond = ShowHurtEffectDelaySecond;
    self.RedScreen = RedScreen;
    self.AttackState = AttackState;
    self.AbnormalState = AbnormalState;
    self.BuffInfoID = BuffInfoID;
    self.BuffTargetFilter = BuffTargetFilter;
    self.BuffIsPercentage = BuffIsPercentage;

    return self;
end