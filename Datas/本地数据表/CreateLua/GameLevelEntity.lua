GameLevelEntity = { Id = 0, ChapterID = 0, Name = "", SceneName = "", SmallMapImg = "", isBoss = 0, Ico = "", PosInMap = "", DlgPic = "", CameraRotation = "", Audio_BG = "" }

--这句是重定义元表的索引，就是说有了这句，这个才是一个类
GameLevelEntity.__index = GameLevelEntity;

function GameLevelEntity.New(Id, ChapterID, Name, SceneName, SmallMapImg, isBoss, Ico, PosInMap, DlgPic, CameraRotation, Audio_BG)
    local self = { }; --初始化self
    setmetatable(self, GameLevelEntity); --将self的元表设定为Class

    self.Id = Id;
    self.ChapterID = ChapterID;
    self.Name = Name;
    self.SceneName = SceneName;
    self.SmallMapImg = SmallMapImg;
    self.isBoss = isBoss;
    self.Ico = Ico;
    self.PosInMap = PosInMap;
    self.DlgPic = DlgPic;
    self.CameraRotation = CameraRotation;
    self.Audio_BG = Audio_BG;

    return self;
end