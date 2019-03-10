NPCEntity = { Id = 0, Name = "", PrefabName = "", HeadPic = "", HalfBodyPic = "", Talk = "" }

--这句是重定义元表的索引，就是说有了这句，这个才是一个类
NPCEntity.__index = NPCEntity;

function NPCEntity.New(Id, Name, PrefabName, HeadPic, HalfBodyPic, Talk)
    local self = { }; --初始化self
    setmetatable(self, NPCEntity); --将self的元表设定为Class

    self.Id = Id;
    self.Name = Name;
    self.PrefabName = PrefabName;
    self.HeadPic = HeadPic;
    self.HalfBodyPic = HalfBodyPic;
    self.Talk = Talk;

    return self;
end