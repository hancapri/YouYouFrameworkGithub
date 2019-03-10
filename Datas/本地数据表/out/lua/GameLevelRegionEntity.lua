GameLevelRegionEntity = { Id = 0, GameLevelId = 0, RegionId = 0, InitSprite = "" }

--这句是重定义元表的索引，就是说有了这句，这个才是一个类
GameLevelRegionEntity.__index = GameLevelRegionEntity;

function GameLevelRegionEntity.New(Id, GameLevelId, RegionId, InitSprite)
    local self = { }; --初始化self
    setmetatable(self, GameLevelRegionEntity); --将self的元表设定为Class

    self.Id = Id;
    self.GameLevelId = GameLevelId;
    self.RegionId = RegionId;
    self.InitSprite = InitSprite;

    return self;
end