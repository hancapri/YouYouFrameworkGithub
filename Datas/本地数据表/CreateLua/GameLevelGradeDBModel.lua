require "Download/XLuaLogic/Data/Create/GameLevelGradeEntity"

--数据访问
GameLevelGradeDBModel = { }

local this = GameLevelGradeDBModel;

local gamelevelgradeTable = { }; --定义表格

function GameLevelGradeDBModel.New()
    return this;
end

function GameLevelGradeDBModel.Init()

    --这里从C#代码中获取一个数组

    local gameDataTable = CS.LuaHelper.Instance:GetData("GameLevelGrade.data");
    --表格的前三行是表头 所以获取数据时候 要从 3 开始
    --print("行数"..gameDataTable.Row);
    --print("列数"..gameDataTable.Column);

    for i = 3, gameDataTable.Row - 1, 1 do
        gamelevelgradeTable[#gamelevelgradeTable+1] = GameLevelGradeEntity.New( tonumber(gameDataTable.Data[i][0]), tonumber(gameDataTable.Data[i][1]), tonumber(gameDataTable.Data[i][2]), gameDataTable.Data[i][3], tonumber(gameDataTable.Data[i][4]), gameDataTable.Data[i][5], gameDataTable.Data[i][6], tonumber(gameDataTable.Data[i][7]), tonumber(gameDataTable.Data[i][8]), tonumber(gameDataTable.Data[i][9]), tonumber(gameDataTable.Data[i][10]), tonumber(gameDataTable.Data[i][11]), tonumber(gameDataTable.Data[i][12]), gameDataTable.Data[i][13], gameDataTable.Data[i][14], gameDataTable.Data[i][15] );
    end

end

function GameLevelGradeDBModel.GetList()
    return gamelevelgradeTable;
end

function GameLevelGradeDBModel.GetEntity(id)
    local ret = nil;
    for i = 1, #gamelevelgradeTable, 1 do
        if (gamelevelgradeTable[i].Id == id) then
            ret = gamelevelgradeTable[i];
            break;
        end
    end
    return ret;
end