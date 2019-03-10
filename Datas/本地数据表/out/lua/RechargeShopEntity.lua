RechargeShopEntity = { Id = 0, Type = 0, Price = 0, Name = "", SalesDesc = "", ProductDesc = "", Virtual = 0, Icon = "" }

--这句是重定义元表的索引，就是说有了这句，这个才是一个类
RechargeShopEntity.__index = RechargeShopEntity;

function RechargeShopEntity.New(Id, Type, Price, Name, SalesDesc, ProductDesc, Virtual, Icon)
    local self = { }; --初始化self
    setmetatable(self, RechargeShopEntity); --将self的元表设定为Class

    self.Id = Id;
    self.Type = Type;
    self.Price = Price;
    self.Name = Name;
    self.SalesDesc = SalesDesc;
    self.ProductDesc = ProductDesc;
    self.Virtual = Virtual;
    self.Icon = Icon;

    return self;
end