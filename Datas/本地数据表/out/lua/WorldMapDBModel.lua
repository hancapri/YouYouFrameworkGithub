require "Download/XLuaLogic/Data/Create/WorldMapEntity"

--数据访问
WorldMapDBModel = { }

local this = WorldMapDBModel;

local worldmapTable = { }; --定义表格

function WorldMapDBModel.New()
    return this;
end

function WorldMapDBModel.Init()

    --这里从C#代码中获取一个数组

    local gameDataTable = CS.LuaHelper.Instance:GetData("WorldMap.bytes");
    --print("行数"..gameDataTable.Row);
    --print("列数"..gameDataTable.Column);

    for i = 0, gameDataTable.Row - 1, 1 do
        worldmapTable[#worldmapTable+1] = WorldMapEntity.New( tonumber(gameDataTable.Data[i][0]), gameDataTable.Data[i][1], gameDataTable.Data[i][2], gameDataTable.Data[i][3], gameDataTable.Data[i][4], gameDataTable.Data[i][5], gameDataTable.Data[i][6], gameDataTable.Data[i][7], tonumber(gameDataTable.Data[i][8]), tonumber(gameDataTable.Data[i][9]), gameDataTable.Data[i][10], gameDataTable.Data[i][11], gameDataTable.Data[i][12], gameDataTable.Data[i][13] );
    end

end

function WorldMapDBModel.GetList()
    return worldmapTable;
end

function WorldMapDBModel.GetEntity(id)
    local ret = nil;
    for i = 1, #worldmapTable, 1 do
        if (worldmapTable[i].Id == id) then
            ret = worldmapTable[i];
            break;
        end
    end
    return ret;
end