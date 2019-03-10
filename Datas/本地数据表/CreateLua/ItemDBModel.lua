require "Download/XLuaLogic/Data/Create/ItemEntity"

--数据访问
ItemDBModel = { }

local this = ItemDBModel;

local itemTable = { }; --定义表格

function ItemDBModel.New()
    return this;
end

function ItemDBModel.Init()

    --这里从C#代码中获取一个数组

    local gameDataTable = CS.LuaHelper.Instance:GetData("Item.data");
    --表格的前三行是表头 所以获取数据时候 要从 3 开始
    --print("行数"..gameDataTable.Row);
    --print("列数"..gameDataTable.Column);

    for i = 3, gameDataTable.Row - 1, 1 do
        itemTable[#itemTable+1] = ItemEntity.New( tonumber(gameDataTable.Data[i][0]), gameDataTable.Data[i][1], tonumber(gameDataTable.Data[i][2]), tonumber(gameDataTable.Data[i][3]), gameDataTable.Data[i][4], tonumber(gameDataTable.Data[i][5]), tonumber(gameDataTable.Data[i][6]), gameDataTable.Data[i][7], gameDataTable.Data[i][8], tonumber(gameDataTable.Data[i][9]), tonumber(gameDataTable.Data[i][10]) );
    end

end

function ItemDBModel.GetList()
    return itemTable;
end

function ItemDBModel.GetEntity(id)
    local ret = nil;
    for i = 1, #itemTable, 1 do
        if (itemTable[i].Id == id) then
            ret = itemTable[i];
            break;
        end
    end
    return ret;
end