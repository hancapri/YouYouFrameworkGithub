require "Download/XLuaLogic/Data/Create/ShopCategoryEntity"

--数据访问
ShopCategoryDBModel = { }

local this = ShopCategoryDBModel;

local shopcategoryTable = { }; --定义表格

function ShopCategoryDBModel.New()
    return this;
end

function ShopCategoryDBModel.Init()

    --这里从C#代码中获取一个数组

    local gameDataTable = CS.LuaHelper.Instance:GetData("ShopCategory.data");
    --表格的前三行是表头 所以获取数据时候 要从 3 开始
    --print("行数"..gameDataTable.Row);
    --print("列数"..gameDataTable.Column);

    for i = 3, gameDataTable.Row - 1, 1 do
        shopcategoryTable[#shopcategoryTable+1] = ShopCategoryEntity.New( tonumber(gameDataTable.Data[i][0]), gameDataTable.Data[i][1] );
    end

end

function ShopCategoryDBModel.GetList()
    return shopcategoryTable;
end

function ShopCategoryDBModel.GetEntity(id)
    local ret = nil;
    for i = 1, #shopcategoryTable, 1 do
        if (shopcategoryTable[i].Id == id) then
            ret = shopcategoryTable[i];
            break;
        end
    end
    return ret;
end