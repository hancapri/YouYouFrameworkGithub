MaterialEntity = { Id = 0, Name = "", Quality = 0, Description = "", Type = 0, FixedType = 0, FixedAddValue = 0, maxAmount = 0, packSort = 0, CompositionProps = "", CompositionMaterialID = 0, CompositionGold = "", SellMoney = 0 }

--这句是重定义元表的索引，就是说有了这句，这个才是一个类
MaterialEntity.__index = MaterialEntity;

function MaterialEntity.New(Id, Name, Quality, Description, Type, FixedType, FixedAddValue, maxAmount, packSort, CompositionProps, CompositionMaterialID, CompositionGold, SellMoney)
    local self = { }; --初始化self
    setmetatable(self, MaterialEntity); --将self的元表设定为Class

    self.Id = Id;
    self.Name = Name;
    self.Quality = Quality;
    self.Description = Description;
    self.Type = Type;
    self.FixedType = FixedType;
    self.FixedAddValue = FixedAddValue;
    self.maxAmount = maxAmount;
    self.packSort = packSort;
    self.CompositionProps = CompositionProps;
    self.CompositionMaterialID = CompositionMaterialID;
    self.CompositionGold = CompositionGold;
    self.SellMoney = SellMoney;

    return self;
end