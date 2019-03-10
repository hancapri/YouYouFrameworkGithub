require "Download/XLuaLogic/Data/Create/SpriteEntity"

--数据访问
SpriteDBModel = { }

local this = SpriteDBModel;

local spriteTable = { }; --定义表格

function SpriteDBModel.New()
    return this;
end

function SpriteDBModel.Init()

    --这里从C#代码中获取一个数组

    local gameDataTable = CS.LuaHelper.Instance:GetData("Sprite.bytes");
    --print("行数"..gameDataTable.Row);
    --print("列数"..gameDataTable.Column);

    for i = 0, gameDataTable.Row - 1, 1 do
        spriteTable[#spriteTable+1] = SpriteEntity.New( tonumber(gameDataTable.Data[i][0]), tonumber(gameDataTable.Data[i][1]), gameDataTable.Data[i][2], tonumber(gameDataTable.Data[i][3]), tonumber(gameDataTable.Data[i][4]), gameDataTable.Data[i][5], gameDataTable.Data[i][6], gameDataTable.Data[i][7], tonumber(gameDataTable.Data[i][8]), tonumber(gameDataTable.Data[i][9]), tonumber(gameDataTable.Data[i][10]), tonumber(gameDataTable.Data[i][11]), tonumber(gameDataTable.Data[i][12]), tonumber(gameDataTable.Data[i][13]), tonumber(gameDataTable.Data[i][14]), tonumber(gameDataTable.Data[i][15]), tonumber(gameDataTable.Data[i][16]), tonumber(gameDataTable.Data[i][17]), tonumber(gameDataTable.Data[i][18]), tonumber(gameDataTable.Data[i][19]), gameDataTable.Data[i][20], gameDataTable.Data[i][21], tonumber(gameDataTable.Data[i][22]), tonumber(gameDataTable.Data[i][23]), tonumber(gameDataTable.Data[i][24]), tonumber(gameDataTable.Data[i][25]), tonumber(gameDataTable.Data[i][26]), tonumber(gameDataTable.Data[i][27]), tonumber(gameDataTable.Data[i][28]) );
    end

end

function SpriteDBModel.GetList()
    return spriteTable;
end

function SpriteDBModel.GetEntity(id)
    local ret = nil;
    for i = 1, #spriteTable, 1 do
        if (spriteTable[i].Id == id) then
            ret = spriteTable[i];
            break;
        end
    end
    return ret;
end