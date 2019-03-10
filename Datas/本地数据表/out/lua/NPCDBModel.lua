require "Download/XLuaLogic/Data/Create/NPCEntity"

--数据访问
NPCDBModel = { }

local this = NPCDBModel;

local npcTable = { }; --定义表格

function NPCDBModel.New()
    return this;
end

function NPCDBModel.Init()

    --这里从C#代码中获取一个数组

    local gameDataTable = CS.LuaHelper.Instance:GetData("NPC.bytes");
    --print("行数"..gameDataTable.Row);
    --print("列数"..gameDataTable.Column);

    for i = 0, gameDataTable.Row - 1, 1 do
        npcTable[#npcTable+1] = NPCEntity.New( tonumber(gameDataTable.Data[i][0]), gameDataTable.Data[i][1], gameDataTable.Data[i][2], gameDataTable.Data[i][3], gameDataTable.Data[i][4], gameDataTable.Data[i][5] );
    end

end

function NPCDBModel.GetList()
    return npcTable;
end

function NPCDBModel.GetEntity(id)
    local ret = nil;
    for i = 1, #npcTable, 1 do
        if (npcTable[i].Id == id) then
            ret = npcTable[i];
            break;
        end
    end
    return ret;
end