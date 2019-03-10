require "Download/XLuaLogic/Data/Create/RechargeShopEntity"

--数据访问
RechargeShopDBModel = { }

local this = RechargeShopDBModel;

local rechargeshopTable = { }; --定义表格

function RechargeShopDBModel.New()
    return this;
end

function RechargeShopDBModel.Init()

    --这里从C#代码中获取一个数组

    local gameDataTable = CS.LuaHelper.Instance:GetData("RechargeShop.bytes");
    --print("行数"..gameDataTable.Row);
    --print("列数"..gameDataTable.Column);

    for i = 0, gameDataTable.Row - 1, 1 do
        rechargeshopTable[#rechargeshopTable+1] = RechargeShopEntity.New( tonumber(gameDataTable.Data[i][0]), tonumber(gameDataTable.Data[i][1]), tonumber(gameDataTable.Data[i][2]), gameDataTable.Data[i][3], gameDataTable.Data[i][4], gameDataTable.Data[i][5], tonumber(gameDataTable.Data[i][6]), gameDataTable.Data[i][7] );
    end

end

function RechargeShopDBModel.GetList()
    return rechargeshopTable;
end

function RechargeShopDBModel.GetEntity(id)
    local ret = nil;
    for i = 1, #rechargeshopTable, 1 do
        if (rechargeshopTable[i].Id == id) then
            ret = rechargeshopTable[i];
            break;
        end
    end
    return ret;
end