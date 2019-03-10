TaskEntity = { Id = 0, Name = "", Status = 0, Content = "" }

--这句是重定义元表的索引，就是说有了这句，这个才是一个类
TaskEntity.__index = TaskEntity;

function TaskEntity.New(Id, Name, Status, Content)
    local self = { }; --初始化self
    setmetatable(self, TaskEntity); --将self的元表设定为Class

    self.Id = Id;
    self.Name = Name;
    self.Status = Status;
    self.Content = Content;

    return self;
end