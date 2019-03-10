SpriteEntity = { Id = 0, SpriteType = 0, Name = "", Level = 0, IsBoss = 0, PrefabName = "", TextureName = "", HeadPic = "", MoveSpeed = 0, HP = 0, MP = 0, Attack = 0, Defense = 0, Hit = 0, Dodge = 0, Cri = 0, Res = 0, Fighting = 0, ShowBloodBar = 0, BloodBarLayerCount = 0, UsedPhyAttack = "", UsedSkillList = "", CanArmor = 0, Armor_HP_Percentage = 0, Range_View = 0, Attack_Interval = 0, PhysicalAttackRate = 0, DelaySec_Attack = 0, RewardExp = 0 }

--这句是重定义元表的索引，就是说有了这句，这个才是一个类
SpriteEntity.__index = SpriteEntity;

function SpriteEntity.New(Id, SpriteType, Name, Level, IsBoss, PrefabName, TextureName, HeadPic, MoveSpeed, HP, MP, Attack, Defense, Hit, Dodge, Cri, Res, Fighting, ShowBloodBar, BloodBarLayerCount, UsedPhyAttack, UsedSkillList, CanArmor, Armor_HP_Percentage, Range_View, Attack_Interval, PhysicalAttackRate, DelaySec_Attack, RewardExp)
    local self = { }; --初始化self
    setmetatable(self, SpriteEntity); --将self的元表设定为Class

    self.Id = Id;
    self.SpriteType = SpriteType;
    self.Name = Name;
    self.Level = Level;
    self.IsBoss = IsBoss;
    self.PrefabName = PrefabName;
    self.TextureName = TextureName;
    self.HeadPic = HeadPic;
    self.MoveSpeed = MoveSpeed;
    self.HP = HP;
    self.MP = MP;
    self.Attack = Attack;
    self.Defense = Defense;
    self.Hit = Hit;
    self.Dodge = Dodge;
    self.Cri = Cri;
    self.Res = Res;
    self.Fighting = Fighting;
    self.ShowBloodBar = ShowBloodBar;
    self.BloodBarLayerCount = BloodBarLayerCount;
    self.UsedPhyAttack = UsedPhyAttack;
    self.UsedSkillList = UsedSkillList;
    self.CanArmor = CanArmor;
    self.Armor_HP_Percentage = Armor_HP_Percentage;
    self.Range_View = Range_View;
    self.Attack_Interval = Attack_Interval;
    self.PhysicalAttackRate = PhysicalAttackRate;
    self.DelaySec_Attack = DelaySec_Attack;
    self.RewardExp = RewardExp;

    return self;
end