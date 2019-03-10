WorldMapEntity = { Id = 0, Name = "", SceneName = "", SmallMapImg = "", NPCList = "", RoleBirthPos = "", CameraRotation = "", TransPos = "", IsCity = 0, IsShowInMap = 0, PosInMap = "", IcoInMap = "", NearScene = "", Audio_BG = "" }

--这句是重定义元表的索引，就是说有了这句，这个才是一个类
WorldMapEntity.__index = WorldMapEntity;

function WorldMapEntity.New(Id, Name, SceneName, SmallMapImg, NPCList, RoleBirthPos, CameraRotation, TransPos, IsCity, IsShowInMap, PosInMap, IcoInMap, NearScene, Audio_BG)
    local self = { }; --初始化self
    setmetatable(self, WorldMapEntity); --将self的元表设定为Class

    self.Id = Id;
    self.Name = Name;
    self.SceneName = SceneName;
    self.SmallMapImg = SmallMapImg;
    self.NPCList = NPCList;
    self.RoleBirthPos = RoleBirthPos;
    self.CameraRotation = CameraRotation;
    self.TransPos = TransPos;
    self.IsCity = IsCity;
    self.IsShowInMap = IsShowInMap;
    self.PosInMap = PosInMap;
    self.IcoInMap = IcoInMap;
    self.NearScene = NearScene;
    self.Audio_BG = Audio_BG;

    return self;
end