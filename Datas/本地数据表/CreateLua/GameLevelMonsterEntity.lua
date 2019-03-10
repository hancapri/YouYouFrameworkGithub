GameLevelMonsterEntity = { Id = 0, GameLevelId = 0, Grade = 0, RegionId = 0, SpriteId = 0, SpriteCount = 0, Exp = 0, Gold = 0, DropEquip = "", DropItem = "", DropMaterial = "" }

--这句是重定义元表的索引，就是说有了这句，这个才是一个类
GameLevelMonsterEntity.__index = GameLevelMonsterEntity;

function GameLevelMonsterEntity.New(Id, GameLevelId, Grade, RegionId, SpriteId, SpriteCount, Exp, Gold, DropEquip, DropItem, DropMaterial)
    local self = { }; --初始化self
    setmetatable(self, GameLevelMonsterEntity); --将self的元表设定为Class

    self.Id = Id;
    self.GameLevelId = GameLevelId;
    self.Grade = Grade;
    self.RegionId = RegionId;
    self.SpriteId = SpriteId;
    self.SpriteCount = SpriteCount;
    self.Exp = Exp;
    self.Gold = Gold;
    self.DropEquip = DropEquip;
    self.DropItem = DropItem;
    self.DropMaterial = DropMaterial;

    return self;
end