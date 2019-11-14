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

public class Menu
{
    [MenuItem("悠游工具/设置")]
    public static void Settings()
    {
        SettingsWindow win = (SettingsWindow)EditorWindow.GetWindow(typeof(SettingsWindow));
        win.titleContent = new GUIContent("全局设置");
        win.Show();
    }

    [MenuItem("悠游工具/资源管理/资源打包管理")]
    public static void AssetBundleCreate()
    {
        AssetBundleWindow win = EditorWindow.GetWindow<AssetBundleWindow>();
        win.titleContent = new GUIContent("资源打包");
        win.Show();
    }

    [MenuItem("悠游工具/生成LuaView脚本")]
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
        sbr.AppendFormat("    this.LuaForm = transform:GetComponent(typeof(CS.YouYouFramework.LuaForm));\n");
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
        sbr.AppendFormat("end");

        string path = Application.dataPath + "/Download/xLuaLogic/Modules/Temp/"+ viewName+"View.bytes";

        using (FileStream fs = new FileStream(path, FileMode.Create))
        {
            using (StreamWriter sw = new StreamWriter(fs))
            {
                sw.Write(sbr.ToString());
            }
        }
    }
}