JobEntity = { Id = 0, Name = "", HeadPic = "", JobPic = "", PrefabName = "", Desc = "", Attack = 0, Defense = 0, Hit = 0, Dodge = 0, Cri = 0, Res = 0, UsedPhyAttackIds = "", UsedSkillIds = "" }

--这句是重定义元表的索引，就是说有了这句，这个才是一个类
JobEntity.__index = JobEntity;

function JobEntity.New(Id, Name, HeadPic, JobPic, PrefabName, Desc, Attack, Defense, Hit, Dodge, Cri, Res, UsedPhyAttackIds, UsedSkillIds)
    local self = { }; --初始化self
    setmetatable(self, JobEntity); --将self的元表设定为Class

    self.Id = Id;
    self.Name = Name;
    self.HeadPic = HeadPic;
    self.JobPic = JobPic;
    self.PrefabName = PrefabName;
    self.Desc = Desc;
    self.Attack = Attack;
    self.Defense = Defense;
    self.Hit = Hit;
    self.Dodge = Dodge;
    self.Cri = Cri;
    self.Res = Res;
    self.UsedPhyAttackIds = UsedPhyAttackIds;
    self.UsedSkillIds = UsedSkillIds;

    return self;
end