//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2016-03-14 22:50:55
//备    注：
//===================================================
using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using YouYouFramework;
using System.Text;
using System;

public class Menu
{
    #region Settings 宏设置
    [MenuItem("悠游工具/宏设置(Settings)")]
    public static void Settings()
    {
        SettingsWindow win = (SettingsWindow)EditorWindow.GetWindow(typeof(SettingsWindow));
        win.titleContent = new GUIContent("宏设置");
        win.Show();
    }
    #endregion

    #region CreateLuaView 生成LuaView和LuaCtrl脚本
    [MenuItem("悠游工具/生成LuaView脚本(CreateLuaViewAndLuaCtrl)")]
    public static void CreateLuaView()
    {
        if (Selection.transforms.Length == 0)
        {
            return;
        }

        Transform trans = Selection.transforms[0];

        LuaForm luaForm = trans.GetComponent<LuaForm>();
        if (luaForm == null)
        {
            Debug.LogError("该UI上没有LuaForm脚本");
            return;
        }

        string viewName = trans.gameObject.name;

        LuaCom[] luaComs = luaForm.LuaComs;

        int len = luaComs.Length;

        StringBuilder sbr = new StringBuilder();
        sbr.AppendFormat("");
        sbr.AppendFormat("{0}View = {{ }};\n", viewName);
        sbr.AppendFormat("local this = {0}View;\n", viewName);
        sbr.AppendFormat("\n");
        for (int i = 0; i < len; i++)
        {
            LuaCom com = luaComs[i];
            sbr.AppendFormat("local {0}Index = {1};\n", com.Name, i);
        }
        sbr.AppendFormat("\n");
        sbr.AppendFormat("function {0}View.OnInit(transform, userData)\n", viewName);
        sbr.AppendFormat("    this.InitView(transform);\n");
        sbr.AppendFormat("    {0}Ctrl.OnInit(userData);\n", viewName);
        sbr.AppendFormat("end\n");
        sbr.AppendFormat("\n");
        sbr.AppendFormat("function {0}View.InitView(transform)\n", viewName);
        sbr.AppendFormat("    this.LuaForm = transform:GetComponent(typeof(CS.YouYou.LuaForm));\n");
        for (int i = 0; i < len; i++)
        {
            LuaCom com = luaComs[i];
            sbr.AppendFormat("    this.{0} = this.LuaForm:GetLuaComs({0}Index);\n", com.Name);
        }
        sbr.AppendFormat("end\n");
        sbr.AppendFormat("\n");
        sbr.AppendFormat("function {0}View.OnOpen(userData)\n", viewName);
        sbr.AppendFormat("    {0}Ctrl.OnOpen(userData);\n", viewName);
        sbr.AppendFormat("end\n");
        sbr.AppendFormat("\n");
        sbr.AppendFormat("function {0}View.OnClose()\n", viewName);
        sbr.AppendFormat("    {0}Ctrl.OnClose();\n", viewName);
        sbr.AppendFormat("end\n");
        sbr.AppendFormat("\n");
        sbr.AppendFormat("function {0}View.OnBeforDestroy()\n", viewName);
        sbr.AppendFormat("    {0}Ctrl.OnBeforDestroy();\n", viewName);
        sbr.AppendFormat("    this.LuaForm = nil;\n");
        for (int i = 0; i < len; i++)
        {
            LuaCom com = luaComs[i];
            sbr.AppendFormat("    this.{0} = nil;\n", com.Name);
        }
        sbr.AppendFormat("end");

        string path = Application.dataPath + "/Download/xLuaLogic/Modules/Temp/" + viewName + "View.bytes";

        using (FileStream fs = new FileStream(path, FileMode.Create))
        {
            using (StreamWriter sw = new StreamWriter(fs))
            {
                sw.Write(sbr.ToString());
            }
        }

        //===================生成控制器
        sbr.Clear();
        sbr.AppendFormat("{0}Ctrl = {{ }};\n", viewName);
        sbr.AppendFormat("\n");
        sbr.AppendFormat("local this = {0}Ctrl;\n", viewName);
        sbr.AppendFormat("\n");
        sbr.AppendFormat("function {0}Ctrl.OnInit(userData)\n", viewName);
        sbr.AppendFormat("\n");
        sbr.AppendFormat("end\n");
        sbr.AppendFormat("\n");
        sbr.AppendFormat("function {0}Ctrl.OnOpen(userData)\n", viewName);
        sbr.AppendFormat("\n");
        sbr.AppendFormat("end\n");
        sbr.AppendFormat("\n");
        sbr.AppendFormat("function {0}Ctrl.OnClose()\n", viewName);
        sbr.AppendFormat("\n");
        sbr.AppendFormat("end\n");
        sbr.AppendFormat("\n");
        sbr.AppendFormat("function {0}Ctrl.OnBeforDestroy()\n", viewName);
        sbr.AppendFormat("\n");
        sbr.AppendFormat("end");
        path = Application.dataPath + "/Download/xLuaLogic/Modules/Temp/" + viewName + "Ctrl.bytes";

        using (FileStream fs = new FileStream(path, FileMode.Create))
        {
            using (StreamWriter sw = new StreamWriter(fs))
            {
                sw.Write(sbr.ToString());
            }
        }
    }
    #endregion

    #region AssetBundleMgr 资源管理
    [MenuItem("悠游工具/资源管理/资源包管理(AssetBundleMgr)")]
    public static void AssetBundleMgr()
    {
        AssetBundleWindow win = (AssetBundleWindow)EditorWindow.GetWindow(typeof(AssetBundleWindow));
        win.titleContent = new GUIContent("资源包管理");
        win.Show();
    }
    #endregion

    #region AssetBundleCopyToStreamingAsstes 初始资源拷贝到StreamingAsstes
    [MenuItem("悠游工具/资源管理/初始资源拷贝到StreamingAsstes")]
    public static void AssetBundleCopyToStreamingAsstes()
    {
        string toPath = Application.streamingAssetsPath + "/AssetBundles/";

        if (Directory.Exists(toPath))
        {
            Directory.Delete(toPath, true);
        }
        Directory.CreateDirectory(toPath);

        IOUtil.CopyDirectory(Application.persistentDataPath, toPath);

        //重新生成版本文件
        //1.先读取persistentDataPath里边的版本文件 这个版本文件里 存放了所有的资源包信息

        byte[] buffer = IOUtil.GetFileBuffer(Application.persistentDataPath + "/VersionFile.bytes");
        string version = "";
        Dictionary<string, AssetBundleInfoEntity> dic = ResourceManager.GetAssetBundleVersionList(buffer, ref version);
        Dictionary<string, AssetBundleInfoEntity> newDic = new Dictionary<string, AssetBundleInfoEntity>();

        DirectoryInfo directory = new DirectoryInfo(toPath);

        //拿到文件夹下所有文件
        FileInfo[] arrFiles = directory.GetFiles("*", SearchOption.AllDirectories);

        for (int i = 0; i < arrFiles.Length; i++)
        {
            FileInfo file = arrFiles[i];
            string fullName = file.FullName.Replace("\\", "/"); //全名 包含路径扩展名
            string name = fullName.Replace(toPath, "").Replace(".assetbundle", "").Replace(".unity3d", "");

            if (name.Equals("AssetInfo.json", StringComparison.CurrentCultureIgnoreCase)
                || name.Equals("Windows", StringComparison.CurrentCultureIgnoreCase)
                || name.Equals("Windows.manifest", StringComparison.CurrentCultureIgnoreCase)

                || name.Equals("Android", StringComparison.CurrentCultureIgnoreCase)
                || name.Equals("Android.manifest", StringComparison.CurrentCultureIgnoreCase)

                || name.Equals("iOS", StringComparison.CurrentCultureIgnoreCase)
                || name.Equals("iOS.manifest", StringComparison.CurrentCultureIgnoreCase)
                )
            {
                File.Delete(file.FullName);
                continue;
            }

            AssetBundleInfoEntity entity = null;
            dic.TryGetValue(name, out entity);


            if (entity != null)
            {
                newDic[name] = entity;
            }
        }

        StringBuilder sbContent = new StringBuilder();
        sbContent.AppendLine(version);

        foreach (var item in newDic)
        {
            AssetBundleInfoEntity entity = item.Value;
            string strLine = string.Format("{0}|{1}|{2}|{3}|{4}", entity.AssetBundleName, entity.MD5, entity.Size, entity.IsFirstData ? 1 : 0, entity.IsEncrypt ? 1 : 0);
            sbContent.AppendLine(strLine);
        }

        IOUtil.CreateTextFile(toPath + "VersionFile.txt", sbContent.ToString());

        //=======================
        MMO_MemoryStream ms = new MMO_MemoryStream();
        string str = sbContent.ToString().Trim();
        string[] arr = str.Split('\n');
        int len = arr.Length;
        ms.WriteInt(len);
        for (int i = 0; i < len; i++)
        {
            if (i == 0)
            {
                ms.WriteUTF8String(arr[i]);
            }
            else
            {
                string[] arrInner = arr[i].Split('|');
                ms.WriteUTF8String(arrInner[0]);
                ms.WriteUTF8String(arrInner[1]);
                ms.WriteInt(int.Parse(arrInner[2]));
                ms.WriteByte(byte.Parse(arrInner[3]));
                ms.WriteByte(byte.Parse(arrInner[4]));
            }
        }

        string filePath = toPath + "/VersionFile.bytes"; //版本文件路径
        buffer = ms.ToArray();
        buffer = ZlibHelper.CompressBytes(buffer);
        FileStream fs = new FileStream(filePath, FileMode.Create);
        fs.Write(buffer, 0, buffer.Length);
        fs.Close();

        AssetDatabase.Refresh();
        Debug.Log("初始资源拷贝到StreamingAsstes完毕");
    }
    #endregion

    #region AssetBundleOpenPersistentDataPath 打开persistentDataPath
    [MenuItem("悠游工具/资源管理/打开persistentDataPath")]
    public static void AssetBundleOpenPersistentDataPath()
    {
        string output = Application.persistentDataPath;
        if (!Directory.Exists(output))
        {
            Directory.CreateDirectory(output);
        }
        output = output.Replace("/", "\\");
        System.Diagnostics.Process.Start("explorer.exe", output);
    }
    #endregion
}