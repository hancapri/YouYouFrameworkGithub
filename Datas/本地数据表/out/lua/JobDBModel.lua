require "Download/XLuaLogic/Data/Create/JobEntity"

--数据访问
JobDBModel = { }

local this = JobDBModel;

local jobTable = { }; --定义表格

function JobDBModel.New()
    return this;
end

function JobDBModel.Init()

    --这里从C#代码中获取一个数组

    local gameDataTable = CS.LuaHelper.Instance:GetData("Job.bytes");
    --print("行数"..gameDataTable.Row);
    --print("列数"..gameDataTable.Column);

    for i = 0, gameDataTable.Row - 1, 1 do
        jobTable[#jobTable+1] = JobEntity.New( tonumber(gameDataTable.Data[i][0]), gameDataTable.Data[i][1], gameDataTable.Data[i][2], gameDataTable.Data[i][3], gameDataTable.Data[i][4], gameDataTable.Data[i][5], tonumber(gameDataTable.Data[i][6]), tonumber(gameDataTable.Data[i][7]), tonumber(gameDataTable.Data[i][8]), tonumber(gameDataTable.Data[i][9]), tonumber(gameDataTable.Data[i][10]), tonumber(gameDataTable.Data[i][11]), gameDataTable.Data[i][12], gameDataTable.Data[i][13] );
    end

end

function JobDBModel.GetList()
    return jobTable;
end

function JobDBModel.GetEntity(id)
    local ret = nil;
    for i = 1, #jobTable, 1 do
        if (jobTable[i].Id == id) then
            ret = jobTable[i];
            break;
        end
    end
    return ret;
end