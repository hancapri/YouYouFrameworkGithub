//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2016-03-14 22:54:42
//备    注：
//===================================================
using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

public class SettingsWindow : EditorWindow
{
    private List<MacorItem> m_List = new List<MacorItem>();
    private Dictionary<string, bool> m_Dic = new Dictionary<string, bool>();
    private string m_Macor = null;

    void OnEnable()
    {
        m_Macor = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android);

        m_List.Clear();
        m_List.Add(new MacorItem() { Name = "DEBUG_LOG_ERROR", DisplayName = "打印错误日志", IsDebug = true, IsRelease = false });
        m_List.Add(new MacorItem() { Name = "DEBUG_LOG_NORMAL", DisplayName = "打印普通日志", IsDebug = true, IsRelease = false });
        m_List.Add(new MacorItem() { Name = "DEBUG_LOG_PROCEDURE", DisplayName = "打印流程日志", IsDebug = true, IsRelease = false });
        m_List.Add(new MacorItem() { Name = "DEBUG_LOG_RESOURCE", DisplayName = "打印资源日志", IsDebug = true, IsRelease = false });
        m_List.Add(new MacorItem() { Name = "DEBUG_LOG_PROTO", DisplayName = "打印通讯日志", IsDebug = true, IsRelease = false });
        m_List.Add(new MacorItem() { Name = "DISABLE_ASSETBUNDLE", DisplayName = "禁用AssetBundle", IsDebug = false, IsRelease = false });
        m_List.Add(new MacorItem() { Name = "HOTFIX_ENABLE", DisplayName = "开启热补丁", IsDebug = false, IsRelease = true });
        m_List.Add(new MacorItem() { Name = "SDKCHANNEL_APPLE_STORE", DisplayName = "渠道_苹果商店", IsDebug = false, IsRelease = false });

        for (int i = 0; i < m_List.Count; i++)
        {
            if (!string.IsNullOrEmpty(m_Macor) && m_Macor.IndexOf(m_List[i].Name) != -1)
            {
                m_Dic[m_List[i].Name] = true;
            }
            else
            {
                m_Dic[m_List[i].Name] = false;
            }
        }
    }


    void OnGUI()
    {
        for (int i = 0; i < m_List.Count; i++)
        {
            EditorGUILayout.BeginHorizontal("box");
            m_Dic[m_List[i].Name] = GUILayout.Toggle(m_Dic[m_List[i].Name], m_List[i].DisplayName);
            EditorGUILayout.EndHorizontal();
        }

        //开启一行
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("保存", GUILayout.Width(100)))
        {
            SaveMacor();
        }

        if (GUILayout.Button("调试模式", GUILayout.Width(100)))
        {
            for (int i = 0; i < m_List.Count; i++)
            {
                m_Dic[m_List[i].Name] = m_List[i].IsDebug;
            }
            SaveMacor();
        }

        if (GUILayout.Button("发布模式", GUILayout.Width(100)))
        {
            for (int i = 0; i < m_List.Count; i++)
            {
                m_Dic[m_List[i].Name] = m_List[i].IsRelease;
            }
            SaveMacor();
        }
        EditorGUILayout.EndHorizontal();
    }

    private void SaveMacor()
    {
        m_Macor = string.Empty;
        foreach (var item in m_Dic)
        {
            if (item.Value)
            {
                m_Macor += string.Format("{0};", item.Key);
            }

            if (item.Key.Equals("DISABLE_ASSETBUNDLE", System.StringComparison.CurrentCultureIgnoreCase))
            {
                EditorBuildSettingsScene[] arrScene = EditorBuildSettings.scenes;
                for (int i = 0; i < arrScene.Length; i++)
                {
                    if (arrScene[i].path.IndexOf("download", System.StringComparison.CurrentCultureIgnoreCase) > -1)
                    {
                        arrScene[i].enabled = item.Value;
                    }
                }

                EditorBuildSettings.scenes = arrScene;
            }
        }
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, m_Macor);
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS, m_Macor);
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, m_Macor);
    }

    /// <summary>
    /// 宏项目
    /// </summary>
    public class MacorItem
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name;

        /// <summary>
        /// 显示的名称
        /// </summary>
        public string DisplayName;

        /// <summary>
        /// 是否调试项
        /// </summary>
        public bool IsDebug;

        /// <summary>
        /// 是否发布项
        /// </summary>
        public bool IsRelease;
    }
}