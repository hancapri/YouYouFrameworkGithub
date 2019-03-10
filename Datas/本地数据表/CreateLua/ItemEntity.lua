ItemEntity = { Id = 0, Name = "", Type = 0, UsedLevel = 0, UsedMethod = "", SellMoney = 0, Quality = 0, Description = "", UsedItems = "", maxAmount = 0, packSort = 0 }

--这句是重定义元表的索引，就是说有了这句，这个才是一个类
ItemEntity.__index = ItemEntity;

function ItemEntity.New(Id, Name, Type, UsedLevel, UsedMethod, SellMoney, Quality, Description, UsedItems, maxAmount, packSort)
    local self = { }; --初始化self
    setmetatable(self, ItemEntity); --将self的元表设定为Class

    self.Id = Id;
    self.Name = Name;
    self.Type = Type;
    self.UsedLevel = UsedLevel;
    self.UsedMethod = UsedMethod;
    self.SellMoney = SellMoney;
    self.Quality = Quality;
    self.Description = Description;
    self.UsedItems = UsedItems;
    self.maxAmount = maxAmount;
    self.packSort = packSort;

    return self;
end