require "Download/XLuaLogic/Data/Create/GameLevelRegionEntity"

--数据访问
GameLevelRegionDBModel = { }

local this = GameLevelRegionDBModel;

local gamelevelregionTable = { }; --定义表格

function GameLevelRegionDBModel.New()
    return this;
end

function GameLevelRegionDBModel.Init()

    --这里从C#代码中获取一个数组

    local gameDataTable = CS.LuaHelper.Instance:GetData("GameLevelRegion.bytes");
    --print("行数"..gameDataTable.Row);
    --print("列数"..gameDataTable.Column);

    for i = 0, gameDataTable.Row - 1, 1 do
        gamelevelregionTable[#gamelevelregionTable+1] = GameLevelRegionEntity.New( tonumber(gameDataTable.Data[i][0]), tonumber(gameDataTable.Data[i][1]), tonumber(gameDataTable.Data[i][2]), gameDataTable.Data[i][3] );
    end

end

function GameLevelRegionDBModel.GetList()
    return gamelevelregionTable;
end

function GameLevelRegionDBModel.GetEntity(id)
    local ret = nil;
    for i = 1, #gamelevelregionTable, 1 do
        if (gamelevelregionTable[i].Id == id) then
            ret = gamelevelregionTable[i];
            break;
        end
    end
    return ret;
end