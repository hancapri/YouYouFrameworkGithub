SkillLevelEntity = { Id = 0, SkillId = 0, Level = 0, HurtValueRate = 0, SpendMP = 0, StateTime = 0, AbnormalRatio = 0, AStateTimes = 0, AStatexiaohao = 0, SkillCDTime = 0, BuffChance = 0, BuffDuration = 0, BuffValue = 0, NeedCharacterLevel = 0, SpendGold = 0 }

--这句是重定义元表的索引，就是说有了这句，这个才是一个类
SkillLevelEntity.__index = SkillLevelEntity;

function SkillLevelEntity.New(Id, SkillId, Level, HurtValueRate, SpendMP, StateTime, AbnormalRatio, AStateTimes, AStatexiaohao, SkillCDTime, BuffChance, BuffDuration, BuffValue, NeedCharacterLevel, SpendGold)
    local self = { }; --初始化self
    setmetatable(self, SkillLevelEntity); --将self的元表设定为Class

    self.Id = Id;
    self.SkillId = SkillId;
    self.Level = Level;
    self.HurtValueRate = HurtValueRate;
    self.SpendMP = SpendMP;
    self.StateTime = StateTime;
    self.AbnormalRatio = AbnormalRatio;
    self.AStateTimes = AStateTimes;
    self.AStatexiaohao = AStatexiaohao;
    self.SkillCDTime = SkillCDTime;
    self.BuffChance = BuffChance;
    self.BuffDuration = BuffDuration;
    self.BuffValue = BuffValue;
    self.NeedCharacterLevel = NeedCharacterLevel;
    self.SpendGold = SpendGold;

    return self;
end