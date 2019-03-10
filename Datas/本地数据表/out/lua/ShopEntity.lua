ShopEntity = { Id = 0, ShopCategoryId = 0, GoodsType = 0, GoodsId = 0, OldPrice = 0, Price = 0, SellStatus = 0 }

--这句是重定义元表的索引，就是说有了这句，这个才是一个类
ShopEntity.__index = ShopEntity;

function ShopEntity.New(Id, ShopCategoryId, GoodsType, GoodsId, OldPrice, Price, SellStatus)
    local self = { }; --初始化self
    setmetatable(self, ShopEntity); --将self的元表设定为Class

    self.Id = Id;
    self.ShopCategoryId = ShopCategoryId;
    self.GoodsType = GoodsType;
    self.GoodsId = GoodsId;
    self.OldPrice = OldPrice;
    self.Price = Price;
    self.SellStatus = SellStatus;

    return self;
end