RechargeShopEntity = { Id = 0, Type = 0, Name = "", SalesDesc = "", ProductDesc = "", Price = 0, Virtual = 0, Icon = "" }

--这句是重定义元表的索引，就是说有了这句，这个才是一个类
RechargeShopEntity.__index = RechargeShopEntity;

function RechargeShopEntity.New(Id, Type, Name, SalesDesc, ProductDesc, Price, Virtual, Icon)
    local self = { }; --初始化self
    setmetatable(self, RechargeShopEntity); --将self的元表设定为Class

    self.Id = Id;
    self.Type = Type;
    self.Name = Name;
    self.SalesDesc = SalesDesc;
    self.ProductDesc = ProductDesc;
    self.Price = Price;
    self.Virtual = Virtual;
    self.Icon = Icon;

    return self;
end