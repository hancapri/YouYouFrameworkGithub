require "Download/XLuaLogic/Data/Create/JobLevelEntity"

--数据访问
JobLevelDBModel = { }

local this = JobLevelDBModel;

local joblevelTable = { }; --定义表格

function JobLevelDBModel.New()
    return this;
end

function JobLevelDBModel.Init()

    --这里从C#代码中获取一个数组

    local gameDataTable = CS.LuaHelper.Instance:GetData("JobLevel.bytes");
    --print("行数"..gameDataTable.Row);
    --print("列数"..gameDataTable.Column);

    for i = 0, gameDataTable.Row - 1, 1 do
        joblevelTable[#joblevelTable+1] = JobLevelEntity.New( tonumber(gameDataTable.Data[i][0]), tonumber(gameDataTable.Data[i][1]), tonumber(gameDataTable.Data[i][2]), tonumber(gameDataTable.Data[i][3]), tonumber(gameDataTable.Data[i][4]), tonumber(gameDataTable.Data[i][5]), tonumber(gameDataTable.Data[i][6]), tonumber(gameDataTable.Data[i][7]), tonumber(gameDataTable.Data[i][8]), tonumber(gameDataTable.Data[i][9]), tonumber(gameDataTable.Data[i][10]), tonumber(gameDataTable.Data[i][11]) );
    end

end

function JobLevelDBModel.GetList()
    return joblevelTable;
end

function JobLevelDBModel.GetEntity(id)
    local ret = nil;
    for i = 1, #joblevelTable, 1 do
        if (joblevelTable[i].Id == id) then
            ret = joblevelTable[i];
            break;
        end
    end
    return ret;
end