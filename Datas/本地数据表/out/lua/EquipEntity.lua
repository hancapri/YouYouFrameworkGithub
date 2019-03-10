EquipEntity = { Id = 0, Name = "", UsedLevel = 0, Quality = 0, Star = 0, Description = "", Type = 0, SellMoney = 0, BackAttrOneType = 0, BackAttrOneValue = 0, BackAttrTwoType = 0, BackAttrTwoValue = 0, Attack = 0, Defense = 0, Hit = 0, Dodge = 0, Cri = 0, Res = 0, HP = 0, MP = 0, maxHole = 0, embedProps = "", StrengthenItem = 0, StrengthenLvMax = 0, StrengthenValue = "", StrengthenItemNumber = "", StrengthenGold = "", StrengthenRatio = "" }

--这句是重定义元表的索引，就是说有了这句，这个才是一个类
EquipEntity.__index = EquipEntity;

function EquipEntity.New(Id, Name, UsedLevel, Quality, Star, Description, Type, SellMoney, BackAttrOneType, BackAttrOneValue, BackAttrTwoType, BackAttrTwoValue, Attack, Defense, Hit, Dodge, Cri, Res, HP, MP, maxHole, embedProps, StrengthenItem, StrengthenLvMax, StrengthenValue, StrengthenItemNumber, StrengthenGold, StrengthenRatio)
    local self = { }; --初始化self
    setmetatable(self, EquipEntity); --将self的元表设定为Class

    self.Id = Id;
    self.Name = Name;
    self.UsedLevel = UsedLevel;
    self.Quality = Quality;
    self.Star = Star;
    self.Description = Description;
    self.Type = Type;
    self.SellMoney = SellMoney;
    self.BackAttrOneType = BackAttrOneType;
    self.BackAttrOneValue = BackAttrOneValue;
    self.BackAttrTwoType = BackAttrTwoType;
    self.BackAttrTwoValue = BackAttrTwoValue;
    self.Attack = Attack;
    self.Defense = Defense;
    self.Hit = Hit;
    self.Dodge = Dodge;
    self.Cri = Cri;
    self.Res = Res;
    self.HP = HP;
    self.MP = MP;
    self.maxHole = maxHole;
    self.embedProps = embedProps;
    self.StrengthenItem = StrengthenItem;
    self.StrengthenLvMax = StrengthenLvMax;
    self.StrengthenValue = StrengthenValue;
    self.StrengthenItemNumber = StrengthenItemNumber;
    self.StrengthenGold = StrengthenGold;
    self.StrengthenRatio = StrengthenRatio;

    return self;
end