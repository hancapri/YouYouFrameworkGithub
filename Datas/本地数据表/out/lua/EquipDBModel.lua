require "Download/XLuaLogic/Data/Create/EquipEntity"

--数据访问
EquipDBModel = { }

local this = EquipDBModel;

local equipTable = { }; --定义表格

function EquipDBModel.New()
    return this;
end

function EquipDBModel.Init()

    --这里从C#代码中获取一个数组

    local gameDataTable = CS.LuaHelper.Instance:GetData("Equip.bytes");
    --print("行数"..gameDataTable.Row);
    --print("列数"..gameDataTable.Column);

    for i = 0, gameDataTable.Row - 1, 1 do
        equipTable[#equipTable+1] = EquipEntity.New( tonumber(gameDataTable.Data[i][0]), gameDataTable.Data[i][1], tonumber(gameDataTable.Data[i][2]), tonumber(gameDataTable.Data[i][3]), tonumber(gameDataTable.Data[i][4]), gameDataTable.Data[i][5], tonumber(gameDataTable.Data[i][6]), tonumber(gameDataTable.Data[i][7]), tonumber(gameDataTable.Data[i][8]), tonumber(gameDataTable.Data[i][9]), tonumber(gameDataTable.Data[i][10]), tonumber(gameDataTable.Data[i][11]), tonumber(gameDataTable.Data[i][12]), tonumber(gameDataTable.Data[i][13]), tonumber(gameDataTable.Data[i][14]), tonumber(gameDataTable.Data[i][15]), tonumber(gameDataTable.Data[i][16]), tonumber(gameDataTable.Data[i][17]), tonumber(gameDataTable.Data[i][18]), tonumber(gameDataTable.Data[i][19]), tonumber(gameDataTable.Data[i][20]), gameDataTable.Data[i][21], tonumber(gameDataTable.Data[i][22]), tonumber(gameDataTable.Data[i][23]), gameDataTable.Data[i][24], gameDataTable.Data[i][25], gameDataTable.Data[i][26], gameDataTable.Data[i][27] );
    end

end

function EquipDBModel.GetList()
    return equipTable;
end

function EquipDBModel.GetEntity(id)
    local ret = nil;
    for i = 1, #equipTable, 1 do
        if (equipTable[i].Id == id) then
            ret = equipTable[i];
            break;
        end
    end
    return ret;
end