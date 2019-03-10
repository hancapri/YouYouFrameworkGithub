require "Download/XLuaLogic/Data/Create/ChapterEntity"

--数据访问
ChapterDBModel = { }

local this = ChapterDBModel;

local chapterTable = { }; --定义表格

function ChapterDBModel.New()
    return this;
end

function ChapterDBModel.Init()

    --这里从C#代码中获取一个数组

    local gameDataTable = CS.LuaHelper.Instance:GetData("Chapter.bytes");
    --print("行数"..gameDataTable.Row);
    --print("列数"..gameDataTable.Column);

    for i = 0, gameDataTable.Row - 1, 1 do
        chapterTable[#chapterTable+1] = ChapterEntity.New( tonumber(gameDataTable.Data[i][0]), gameDataTable.Data[i][1], tonumber(gameDataTable.Data[i][2]), gameDataTable.Data[i][3], tonumber(gameDataTable.Data[i][4]), tonumber(gameDataTable.Data[i][5]) );
    end

end

function ChapterDBModel.GetList()
    return chapterTable;
end

function ChapterDBModel.GetEntity(id)
    local ret = nil;
    for i = 1, #chapterTable, 1 do
        if (chapterTable[i].Id == id) then
            ret = chapterTable[i];
            break;
        end
    end
    return ret;
end