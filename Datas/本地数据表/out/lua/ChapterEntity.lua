ChapterEntity = { Id = 0, ChapterName = "", GameLevelCount = 0, BG_Pic = "", Uvx = 0, Uvy = 0 }

--这句是重定义元表的索引，就是说有了这句，这个才是一个类
ChapterEntity.__index = ChapterEntity;

function ChapterEntity.New(Id, ChapterName, GameLevelCount, BG_Pic, Uvx, Uvy)
    local self = { }; --初始化self
    setmetatable(self, ChapterEntity); --将self的元表设定为Class

    self.Id = Id;
    self.ChapterName = ChapterName;
    self.GameLevelCount = GameLevelCount;
    self.BG_Pic = BG_Pic;
    self.Uvx = Uvx;
    self.Uvy = Uvy;

    return self;
end