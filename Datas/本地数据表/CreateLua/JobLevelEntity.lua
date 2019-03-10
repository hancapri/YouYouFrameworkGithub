JobLevelEntity = { Id = 0, Level = 0, NeedExp = 0, Energy = 0, HP = 0, MP = 0, Attack = 0, Defense = 0, Hit = 0, Dodge = 0, Cri = 0, Res = 0 }

--这句是重定义元表的索引，就是说有了这句，这个才是一个类
JobLevelEntity.__index = JobLevelEntity;

function JobLevelEntity.New(Id, Level, NeedExp, Energy, HP, MP, Attack, Defense, Hit, Dodge, Cri, Res)
    local self = { }; --初始化self
    setmetatable(self, JobLevelEntity); --将self的元表设定为Class

    self.Id = Id;
    self.Level = Level;
    self.NeedExp = NeedExp;
    self.Energy = Energy;
    self.HP = HP;
    self.MP = MP;
    self.Attack = Attack;
    self.Defense = Defense;
    self.Hit = Hit;
    self.Dodge = Dodge;
    self.Cri = Cri;
    self.Res = Res;

    return self;
end