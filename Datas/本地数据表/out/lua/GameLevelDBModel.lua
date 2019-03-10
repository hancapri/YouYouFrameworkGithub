require "Download/XLuaLogic/Data/Create/GameLevelEntity"

--数据访问
GameLevelDBModel = { }

local this = GameLevelDBModel;

local gamelevelTable = { }; --定义表格

function GameLevelDBModel.New()
    return this;
end

function GameLevelDBModel.Init()

    --这里从C#代码中获取一个数组

    local gameDataTable = CS.LuaHelper.Instance:GetData("GameLevel.bytes");
    --print("行数"..gameDataTable.Row);
    --print("列数"..gameDataTable.Column);

    for i = 0, gameDataTable.Row - 1, 1 do
        gamelevelTable[#gamelevelTable+1] = GameLevelEntity.New( tonumber(gameDataTable.Data[i][0]), tonumber(gameDataTable.Data[i][1]), gameDataTable.Data[i][2], gameDataTable.Data[i][3], gameDataTable.Data[i][4], tonumber(gameDataTable.Data[i][5]), gameDataTable.Data[i][6], gameDataTable.Data[i][7], gameDataTable.Data[i][8], gameDataTable.Data[i][9], gameDataTable.Data[i][10] );
    end

end

function GameLevelDBModel.GetList()
    return gamelevelTable;
end

function GameLevelDBModel.GetEntity(id)
    local ret = nil;
    for i = 1, #gamelevelTable, 1 do
        if (gamelevelTable[i].Id == id) then
            ret = gamelevelTable[i];
            break;
        end
    end
    return ret;
end