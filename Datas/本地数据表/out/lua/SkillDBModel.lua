require "Download/XLuaLogic/Data/Create/SkillEntity"

--数据访问
SkillDBModel = { }

local this = SkillDBModel;

local skillTable = { }; --定义表格

function SkillDBModel.New()
    return this;
end

function SkillDBModel.Init()

    --这里从C#代码中获取一个数组

    local gameDataTable = CS.LuaHelper.Instance:GetData("Skill.bytes");
    --print("行数"..gameDataTable.Row);
    --print("列数"..gameDataTable.Column);

    for i = 0, gameDataTable.Row - 1, 1 do
        skillTable[#skillTable+1] = SkillEntity.New( tonumber(gameDataTable.Data[i][0]), gameDataTable.Data[i][1], gameDataTable.Data[i][2], gameDataTable.Data[i][3], tonumber(gameDataTable.Data[i][4]), tonumber(gameDataTable.Data[i][5]), tonumber(gameDataTable.Data[i][6]), tonumber(gameDataTable.Data[i][7]), tonumber(gameDataTable.Data[i][8]), tonumber(gameDataTable.Data[i][9]), tonumber(gameDataTable.Data[i][10]), tonumber(gameDataTable.Data[i][11]), tonumber(gameDataTable.Data[i][12]), tonumber(gameDataTable.Data[i][13]), tonumber(gameDataTable.Data[i][14]), tonumber(gameDataTable.Data[i][15]) );
    end

end

function SkillDBModel.GetList()
    return skillTable;
end

function SkillDBModel.GetEntity(id)
    local ret = nil;
    for i = 1, #skillTable, 1 do
        if (skillTable[i].Id == id) then
            ret = skillTable[i];
            break;
        end
    end
    return ret;
end