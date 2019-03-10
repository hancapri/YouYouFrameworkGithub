LanguageEntity = { Id = 0, Module = "", Key = "", Desc = "", CN = "", EN = "" }

--这句是重定义元表的索引，就是说有了这句，这个才是一个类
LanguageEntity.__index = LanguageEntity;

function LanguageEntity.New(Id, Module, Key, Desc, CN, EN)
    local self = { }; --初始化self
    setmetatable(self, LanguageEntity); --将self的元表设定为Class

    self.Id = Id;
    self.Module = Module;
    self.Key = Key;
    self.Desc = Desc;
    self.CN = CN;
    self.EN = EN;

    return self;
end