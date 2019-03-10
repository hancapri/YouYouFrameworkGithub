ShopCategoryEntity = { Id = 0, Name = "" }

--这句是重定义元表的索引，就是说有了这句，这个才是一个类
ShopCategoryEntity.__index = ShopCategoryEntity;

function ShopCategoryEntity.New(Id, Name)
    local self = { }; --初始化self
    setmetatable(self, ShopCategoryEntity); --将self的元表设定为Class

    self.Id = Id;
    self.Name = Name;

    return self;
end