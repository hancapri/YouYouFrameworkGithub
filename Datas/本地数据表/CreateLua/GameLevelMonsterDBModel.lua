require "Download/XLuaLogic/Data/Create/GameLevelMonsterEntity"

--数据访问
GameLevelMonsterDBModel = { }

local this = GameLevelMonsterDBModel;

local gamelevelmonsterTable = { }; --定义表格

function GameLevelMonsterDBModel.New()
    return this;
end

function GameLevelMonsterDBModel.Init()

    --这里从C#代码中获取一个数组

    local gameDataTable = CS.LuaHelper.Instance:GetData("GameLevelMonster.data");
    --表格的前三行是表头 所以获取数据时候 要从 3 开始
    --print("行数"..gameDataTable.Row);
    --print("列数"..gameDataTable.Column);

    for i = 3, gameDataTable.Row - 1, 1 do
        gamelevelmonsterTable[#gamelevelmonsterTable+1] = GameLevelMonsterEntity.New( tonumber(gameDataTable.Data[i][0]), tonumber(gameDataTable.Data[i][1]), tonumber(gameDataTable.Data[i][2]), tonumber(gameDataTable.Data[i][3]), tonumber(gameDataTable.Data[i][4]), tonumber(gameDataTable.Data[i][5]), tonumber(gameDataTable.Data[i][6]), tonumber(gameDataTable.Data[i][7]), gameDataTable.Data[i][8], gameDataTable.Data[i][9], gameDataTable.Data[i][10] );
    end

end

function GameLevelMonsterDBModel.GetList()
    return gamelevelmonsterTable;
end

function GameLevelMonsterDBModel.GetEntity(id)
    local ret = nil;
    for i = 1, #gamelevelmonsterTable, 1 do
        if (gamelevelmonsterTable[i].Id == id) then
            ret = gamelevelmonsterTable[i];
            break;
        end
    end
    return ret;
end