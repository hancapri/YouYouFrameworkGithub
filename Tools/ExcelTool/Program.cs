using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//如果要支持xlsx格式表格，请在本机电脑安装这个
//http://download.microsoft.com/download/7/0/3/703ffbcb-dc0c-4e19-b0da-1463960fdcdb/AccessDatabaseEngine.exe

namespace ExcelTool
{
    class Program
    {
        private static string SourceExcelPath; //源excel路径
        private static string OutBytesFilePath; //bytes文件路径
        private static string OutCSharpFilePath; //c#脚本路径
        private static string OutLuaFilePath; //lua脚本路径


        static void Main(string[] args)
        {
            LoadConfig();
            ReadFiles(SourceExcelPath);

            Console.WriteLine("全部生成完毕");
            Console.ReadLine();
        }

        private static void LoadConfig()
        {
            string configPath = Environment.CurrentDirectory + "/config.txt";

            if (File.Exists(configPath))
            {
                string str = "";
                using (FileStream fs = new FileStream(configPath, FileMode.Open))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        str = sr.ReadToEnd();
                    }
                }

                if (!string.IsNullOrEmpty(str))
                {
                    string[] arr = str.Split('\n');
                    if (arr.Length >= 4)
                    {
                        SourceExcelPath = arr[0].Trim();
                        OutBytesFilePath = arr[1].Trim();
                        OutCSharpFilePath = arr[2].Trim();
                        OutLuaFilePath = arr[3].Trim();
                    }
                }
            }
        }

        public static List<string> ReadFiles(string path)
        {
            string[] arr = Directory.GetFiles(path);

            List<string> lst = new List<string>();

            int len = arr.Length;
            for (int i = 0; i < len; i++)
            {
                string filePath = arr[i];
                FileInfo file = new FileInfo(filePath);
                if (file.Name.IndexOf("~$") > -1)
                {
                    continue;
                }
                if (file.Extension.Equals(".xls") || file.Extension.Equals(".xlsx"))
                {
                    ReadData(file.Extension.Equals(".xls"), file.FullName, file.Name.Substring(0, file.Name.LastIndexOf('.')));
                }
            }

            return lst;
        }


        private static void ReadData(bool isXls, string filePath, string fileName)
        {

            if (string.IsNullOrEmpty(filePath)) return;

            //把表格复制一下
            string newPath = filePath + ".temp";

            File.Copy(filePath, newPath, true);

            string tableName = "Sheet1";
            string strConn = "";
            if (isXls)
            {
                strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + newPath + ";" + "Extended Properties='Excel 8.0;HDR=NO;IMEX=1';";
            }
            else
            {
                strConn = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source =" + newPath + ";Extended Properties='Excel 12.0;HDR=NO;IMEX=1'";
            }

            DataTable dt = null;

            string strExcel = "";
            OleDbDataAdapter myCommand = null;
            DataSet ds = null;
            strExcel = string.Format("select * from [{0}$]", tableName);
            myCommand = new OleDbDataAdapter(strExcel, strConn);
            ds = new DataSet();
            myCommand.Fill(ds, "table1");
            dt = ds.Tables[0];
            myCommand.Dispose();

            File.Delete(newPath);

            if (fileName.Equals("Sys_Localization", StringComparison.CurrentCultureIgnoreCase))
            {
                //多语言表 单独处理
                CreateLocalization(fileName, dt);
            }
            else
            {
                CreateData(fileName, dt);
            }
        }

        #region 创建普通表
        //表头
        static string[,] tableHeadArr = null;

        private static void CreateData(string fileName, DataTable dt)
        {
            try
            {
                //数据格式 行数 列数 二维数组每项的值 这里不做判断 都用string存储
                tableHeadArr = null;

                byte[] buffer = null;

                using (MMO_MemoryStream ms = new MMO_MemoryStream())
                {
                    int rows = dt.Rows.Count;
                    int columns = dt.Columns.Count;

                    tableHeadArr = new string[columns, 3];

                    ms.WriteInt(rows - 3); //减去表头的三行
                    ms.WriteInt(columns);
                    for (int i = 0; i < rows; i++)
                    {
                        for (int j = 0; j < columns; j++)
                        {
                            if (i < 3)
                            {
                                tableHeadArr[j, i] = dt.Rows[i][j].ToString().Trim();
                            }
                            else
                            {
                                string type = tableHeadArr[j, 1];
                                string value = dt.Rows[i][j].ToString().Trim();

                                //Console.WriteLine("type=" + type + "||" + "value=" + value);

                                switch (type.ToLower())
                                {
                                    case "int":
                                        ms.WriteInt(string.IsNullOrEmpty(value) ? 0 : int.Parse(value));
                                        break;
                                    case "long":
                                        ms.WriteLong(string.IsNullOrEmpty(value) ? 0 : long.Parse(value));
                                        break;
                                    case "short":
                                        ms.WriteShort(string.IsNullOrEmpty(value) ? (short)0 : short.Parse(value));
                                        break;
                                    case "float":
                                        ms.WriteFloat(string.IsNullOrEmpty(value) ? 0 : float.Parse(value));
                                        break;
                                    case "byte":
                                        ms.WriteByte(string.IsNullOrEmpty(value) ? (byte)0 : byte.Parse(value));
                                        break;
                                    case "bool":
                                        ms.WriteBool(string.IsNullOrEmpty(value) ? false : bool.Parse(value));
                                        break;
                                    case "double":
                                        ms.WriteDouble(string.IsNullOrEmpty(value) ? 0 : double.Parse(value));
                                        break;
                                    default:
                                        ms.WriteUTF8String(value);
                                        break;
                                }
                            }
                        }
                    }
                    buffer = ms.ToArray();
                }

                //------------------
                //写入文件
                //------------------
                FileStream fs = new FileStream(string.Format("{0}{1}", OutBytesFilePath, fileName + ".bytes"), FileMode.Create);
                fs.Write(buffer, 0, buffer.Length);
                fs.Close();

                Console.WriteLine("表格=>" + fileName + " 生成bytes文件完毕");

                CreateEntity(fileName, tableHeadArr);

                Console.WriteLine("表格=>" + fileName + " 生成实体脚本完毕");

                CreateDBModel(fileName, tableHeadArr);

                Console.WriteLine("表格=>" + fileName + " 生成数据访问脚本完毕");

            }
            catch (Exception ex)
            {
                Console.WriteLine("表格=>" + fileName + " 处理失败:" + ex.Message);
            }
        }

        /// <summary>
        /// 创建实体
        /// </summary>
        private static void CreateEntity(string fileName, string[,] dataArr)
        {
            if (dataArr == null) return;

            StringBuilder sbr = new StringBuilder();
            sbr.Append("\r\n");
            sbr.Append("//===================================================\r\n");
            sbr.Append("//作    者：边涯  http://www.u3dol.com\r\n");
            sbr.AppendFormat("//创建时间：{0}\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            sbr.Append("//备    注：此代码为工具生成 请勿手工修改\r\n");
            sbr.Append("//===================================================\r\n");
            sbr.Append("using System.Collections;\r\n");
            sbr.Append("using YouYou;\r\n");
            sbr.Append("\r\n");
            sbr.Append("/// <summary>\r\n");
            sbr.AppendFormat("/// {0}实体\r\n", fileName);
            sbr.Append("/// </summary>\r\n");
            sbr.AppendFormat("public partial class {0}Entity : DataTableEntityBase\r\n", fileName);
            sbr.Append("{\r\n");

            for (int i = 0; i < dataArr.GetLength(0); i++)
            {
                if (i == 0) continue;
                sbr.Append("    /// <summary>\r\n");
                sbr.AppendFormat("    /// {0}\r\n", dataArr[i, 2]);
                sbr.Append("    /// </summary>\r\n");
                sbr.AppendFormat("    public {0} {1};\r\n", dataArr[i, 1], dataArr[i, 0]);
                sbr.Append("\r\n");
            }

            sbr.Append("}\r\n");


            using (FileStream fs = new FileStream(string.Format("{0}/{1}Entity.cs", OutCSharpFilePath, fileName), FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.Write(sbr.ToString());
                }
            }
        }

        /// <summary>
        /// 创建数据管理类
        /// </summary>
        private static void CreateDBModel(string fileName, string[,] dataArr)
        {
            if (dataArr == null) return;

            StringBuilder sbr = new StringBuilder();
            sbr.Append("\r\n");
            sbr.Append("//===================================================\r\n");
            sbr.Append("//作    者：边涯  http://www.u3dol.com\r\n");
            sbr.AppendFormat("//创建时间：{0}\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            sbr.Append("//备    注：此代码为工具生成 请勿手工修改\r\n");
            sbr.Append("//===================================================\r\n");
            sbr.Append("using System.Collections;\r\n");
            sbr.Append("using System.Collections.Generic;\r\n");
            sbr.Append("using System;\r\n");
            sbr.Append("using YouYou;\r\n");
            sbr.Append("\r\n");
            sbr.Append("/// <summary>\r\n");
            sbr.AppendFormat("/// {0}数据管理\r\n", fileName);
            sbr.Append("/// </summary>\r\n");
            sbr.AppendFormat("public partial class {0}DBModel : DataTableDBModelBase<{0}DBModel, {0}Entity>\r\n", fileName);
            sbr.Append("{\r\n");

            sbr.Append("    /// <summary>\r\n");
            sbr.Append("    /// 文件名称\r\n");
            sbr.Append("    /// </summary>\r\n");
            sbr.AppendFormat("    public override string DataTableName {{ get {{ return \"{0}\"; }} }}\r\n", fileName);
            sbr.Append("\r\n");


            sbr.Append("    /// <summary>\r\n");
            sbr.Append("    /// 加载列表\r\n");
            sbr.Append("    /// </summary>\r\n");
            sbr.Append("    protected override void LoadList(MMO_MemoryStream ms)\r\n");
            sbr.Append("    {\r\n");
            sbr.Append("        int rows = ms.ReadInt();\r\n");
            sbr.Append("        int columns = ms.ReadInt();\r\n");
            sbr.Append("\r\n");
            sbr.Append("        for (int i = 0; i < rows; i++)\r\n");
            sbr.Append("        {\r\n");
            sbr.AppendFormat("            {0}Entity entity = new {0}Entity();\r\n", fileName);

            for (int i = 0; i < dataArr.GetLength(0); i++)
            {
                if (dataArr[i, 1].Equals("byte", StringComparison.CurrentCultureIgnoreCase))
                {
                    sbr.AppendFormat("            entity.{0} = (byte)ms.Read{1}();\r\n", dataArr[i, 0], ChangeTypeName(dataArr[i, 1]));
                }
                else
                {
                    sbr.AppendFormat("            entity.{0} = ms.Read{1}();\r\n", dataArr[i, 0], ChangeTypeName(dataArr[i, 1]));
                }
            }

            sbr.Append("\r\n");
            sbr.Append("            m_List.Add(entity);\r\n");
            sbr.Append("            m_Dic[entity.Id] = entity;\r\n");
            sbr.Append("        }\r\n");
            sbr.Append("    }\r\n");

            sbr.Append("}");
            using (FileStream fs = new FileStream(string.Format("{0}/{1}DBModel.cs", OutCSharpFilePath, fileName), FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.Write(sbr.ToString());
                }
            }
        }

        private static string ChangeTypeName(string type)
        {
            string str = string.Empty;

            switch (type)
            {
                case "byte":
                    str = "Byte";
                    break;
                case "int":
                    str = "Int";
                    break;
                case "short":
                    str = "Short";
                    break;
                case "long":
                    str = "Long";
                    break;
                case "float":
                    str = "Float";
                    break;
                case "string":
                    str = "UTF8String";
                    break;
            }

            return str;
        }
        #endregion

        #region 创建多语言表
        private static void CreateLocalization(string fileName, DataTable dt)
        {
            try
            {
                int rows = dt.Rows.Count;
                int columns = dt.Columns.Count;

                int newcolumns = columns - 3; //减去前三列 后面表示有多少种语言

                int currKeyColumn = 2; //当前的Key列
                int currValueColumn = 3; //当前的值列

                tableHeadArr = new string[columns, 3];

                while (newcolumns > 0)
                {
                    newcolumns--;

                    #region 写入文件
                    byte[] buffer = null;

                    using (MMO_MemoryStream ms = new MMO_MemoryStream())
                    {
                        ms.WriteInt(rows - 3); //减去表头的三行
                        ms.WriteInt(2); //多语言表 只有2列 Key Value

                        for (int i = 0; i < rows; i++)
                        {
                            for (int j = 0; j < columns; j++)
                            {
                                if (i < 3)
                                {
                                    tableHeadArr[j, i] = dt.Rows[i][j].ToString().Trim();
                                }
                                else
                                {
                                    if (j == currKeyColumn)
                                    {
                                        //写入key
                                        string value = dt.Rows[i][j].ToString().Trim();
                                        ms.WriteUTF8String(value);
                                    }
                                    else if (j == currValueColumn)
                                    {
                                        //写入value
                                        string value = dt.Rows[i][j].ToString().Trim();
                                        ms.WriteUTF8String(value);
                                    }
                                }
                            }
                        }
                        buffer = ms.ToArray();
                    }

                    //------------------
                    //写入文件
                    //------------------
                    FileStream fs = new FileStream(string.Format("{0}/Localization/{1}", OutBytesFilePath, tableHeadArr[currValueColumn, 0] + ".bytes"), FileMode.Create);
                    fs.Write(buffer, 0, buffer.Length);
                    fs.Close();

                    currValueColumn++;
                    #endregion
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("表格=>" + fileName + " 处理失败:" + ex.Message);
            }
        }
        #endregion
    }
}