require "Download/XLuaLogic/Data/Create/SkillLevelEntity"

--数据访问
SkillLevelDBModel = { }

local this = SkillLevelDBModel;

local skilllevelTable = { }; --定义表格

function SkillLevelDBModel.New()
    return this;
end

function SkillLevelDBModel.Init()

    --这里从C#代码中获取一个数组

    local gameDataTable = CS.LuaHelper.Instance:GetData("SkillLevel.bytes");
    --print("行数"..gameDataTable.Row);
    --print("列数"..gameDataTable.Column);

    for i = 0, gameDataTable.Row - 1, 1 do
        skilllevelTable[#skilllevelTable+1] = SkillLevelEntity.New( tonumber(gameDataTable.Data[i][0]), tonumber(gameDataTable.Data[i][1]), tonumber(gameDataTable.Data[i][2]), tonumber(gameDataTable.Data[i][3]), tonumber(gameDataTable.Data[i][4]), tonumber(gameDataTable.Data[i][5]), tonumber(gameDataTable.Data[i][6]), tonumber(gameDataTable.Data[i][7]), tonumber(gameDataTable.Data[i][8]), tonumber(gameDataTable.Data[i][9]), tonumber(gameDataTable.Data[i][10]), tonumber(gameDataTable.Data[i][11]), tonumber(gameDataTable.Data[i][12]), tonumber(gameDataTable.Data[i][13]), tonumber(gameDataTable.Data[i][14]) );
    end

end

function SkillLevelDBModel.GetList()
    return skilllevelTable;
end

function SkillLevelDBModel.GetEntity(id)
    local ret = nil;
    for i = 1, #skilllevelTable, 1 do
        if (skilllevelTable[i].Id == id) then
            ret = skilllevelTable[i];
            break;
        end
    end
    return ret;
end