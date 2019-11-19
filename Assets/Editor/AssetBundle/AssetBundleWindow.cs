//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2016-03-19 18:41:09
//备    注：
//===================================================
using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;

/// <summary>
/// AssetBundle管理窗口
/// </summary>
public class AssetBundleWindow : EditorWindow
{
    private AssetBundleDAL dal;
    private List<AssetBundleEntity> m_List;
    private Dictionary<string, bool> m_Dic;

    private string[] arrTag = { "All", "Audio", "Role", "Effect","Scene" ,"UI", "Lua", "None" };
    private int tagIndex = 0; //标记的索引
    private int selectTagIndex = -1; //选择的标记的索引

    private string[] arrBuildTarget = { "Windows", "Android", "iOS" };

    private int selectBuildTargetIndex = -1; //选择的打包平台索引
#if UNITY_STANDALONE_WIN
    private BuildTarget target = BuildTarget.StandaloneWindows;
    private int buildTargetIndex = 0; //打包的平台索引
#elif UNITY_ANDROID
    private BuildTarget target = BuildTarget.Android;
    private int buildTargetIndex = 1;
#elif UNITY_IPHONE
    private BuildTarget target = BuildTarget.iOS;
    private int buildTargetIndex = 2;
#endif


    private Vector2 pos;

    /// <summary>
    /// 构造函数
    /// </summary>
    public AssetBundleWindow()
    {

    }

    void OnEnable()
    {
        string xmlPath = Application.dataPath + @"\Editor\AssetBundle\AssetBundleConfig.xml";
        dal = new AssetBundleDAL(xmlPath);
        m_List = dal.GetList();

        m_Dic = new Dictionary<string, bool>();

        for (int i = 0; i < m_List.Count; i++)
        {
            m_Dic[m_List[i].Key] = true;
        }
    }


    /// <summary>
    /// 绘制窗口
    /// </summary>
    void OnGUI()
    {
        if (m_List == null) return;

        #region 按钮行
        GUILayout.BeginHorizontal("box");

        selectTagIndex = EditorGUILayout.Popup(tagIndex, arrTag, GUILayout.Width(100));
        if (selectTagIndex != tagIndex)
        {
            tagIndex = selectTagIndex;
            EditorApplication.delayCall = OnSelectTagCallBack;
        }


        selectBuildTargetIndex = EditorGUILayout.Popup(buildTargetIndex, arrBuildTarget, GUILayout.Width(100));
        if (selectBuildTargetIndex != buildTargetIndex)
        {
            buildTargetIndex = selectBuildTargetIndex;
            EditorApplication.delayCall = OnSelectTargetCallBack;
        }

        if (GUILayout.Button("保存设置", GUILayout.Width(200)))
        {
            EditorApplication.delayCall = OnSaveSettingCallBack;
        }

        if (GUILayout.Button("清空设置", GUILayout.Width(200)))
        {
            EditorApplication.delayCall = OnClearSettingCallBack;
        }

        if (GUILayout.Button("清空AssetBundle包", GUILayout.Width(200)))
        {
            EditorApplication.delayCall = OnClearAssetBundleCallBack;
        }

        if (GUILayout.Button("打AssetBundle包", GUILayout.Width(200)))
        {
            EditorApplication.delayCall = OnAssetBundleCallBack;
        }
        EditorGUILayout.Space();
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal("box");

        if (GUILayout.Button("拷贝数据表", GUILayout.Width(200)))
        {
            EditorApplication.delayCall = OnCopyDataTableCallBack;
        }

        if (GUILayout.Button("转换Lua脚本", GUILayout.Width(200)))
        {
            EditorApplication.delayCall = OnChangeLuaScriptCallBack;
        }

        if (GUILayout.Button("生成版本文件", GUILayout.Width(200)))
        {
            EditorApplication.delayCall = OnCreateVersionFileCallBack;
        }

        EditorGUILayout.Space();

        GUILayout.EndHorizontal();
        #endregion

        GUILayout.BeginHorizontal("box");
        GUILayout.Label("包名");
        GUILayout.Label("标记", GUILayout.Width(100));
        GUILayout.Label("文件夹", GUILayout.Width(200));
        GUILayout.Label("初始资源", GUILayout.Width(200));
        GUILayout.EndHorizontal();

        GUILayout.BeginVertical();

        pos = EditorGUILayout.BeginScrollView(pos);

        for (int i = 0; i < m_List.Count; i++)
        {
            AssetBundleEntity entity = m_List[i];

            GUILayout.BeginHorizontal("box");

            m_Dic[entity.Key] = GUILayout.Toggle(m_Dic[entity.Key], "", GUILayout.Width(20));
            GUILayout.Label(entity.Name);
            GUILayout.Label(entity.Tag, GUILayout.Width(100));
            GUILayout.Label(entity.IsFolder.ToString(), GUILayout.Width(200));
            GUILayout.Label(entity.IsFirstData.ToString(), GUILayout.Width(200));
            GUILayout.EndHorizontal();

            foreach (string path in entity.PathList)
            {
                GUILayout.BeginHorizontal("box");
                GUILayout.Space(40);
                GUILayout.Label(path);
                GUILayout.EndHorizontal();
            }
        }

        EditorGUILayout.EndScrollView();

        GUILayout.EndVertical();
    }

    /// <summary>
    /// 资源包加密
    /// </summary>
    /// <param name="path"></param>
    private void AssetBundleEncrypt(string path)
    {

        string[] files = Directory.GetFiles(path);
        foreach (string file in files)
        {
            FileInfo fileInfo = new FileInfo(file);

            if (!fileInfo.Extension.Equals(".assetbundle", StringComparison.CurrentCultureIgnoreCase) &&
                !fileInfo.Extension.Equals(".unity3d", StringComparison.CurrentCultureIgnoreCase)
                )
            {
                continue;
            }

            byte[] buffer = null;

            using (FileStream fs = new FileStream(file, FileMode.Open))
            {
                buffer = new byte[fs.Length];
                fs.Read(buffer, 0, buffer.Length);
            }

            buffer = SecurityUtil.Xor(buffer);

            using (FileStream fs = new FileStream(file, FileMode.Create))
            {
                fs.Write(buffer, 0, buffer.Length);
                fs.Flush();
            }
        }

        string[] dirs = Directory.GetDirectories(path);
        foreach (string dir in dirs)
        {
            AssetBundleEncrypt(dir);
        }
    }

    private void OnChangeLuaScriptCallBack()
    {
        string src = Application.dataPath + "/Download/xLuaLogic";
        string dest = Application.dataPath + "/../AssetBundles/" + arrBuildTarget[buildTargetIndex] + "/Download/xLuaLogic";

        CopyLuaDirectory(src, dest);

        AssetDatabase.Refresh();
        Debug.Log("转换Lua脚本完毕");
    }

    #region CopyLuaDirectory
    public void CopyLuaDirectory(string sourceDirName, string destDirName)
    {
        try
        {
            if (Directory.Exists(destDirName))
            {
                Directory.Delete(destDirName, true);
            }

            Directory.CreateDirectory(destDirName);
            File.SetAttributes(destDirName, File.GetAttributes(sourceDirName));

            if (destDirName[destDirName.Length - 1] != Path.DirectorySeparatorChar)
                destDirName = destDirName + Path.DirectorySeparatorChar;

            string[] files = Directory.GetFiles(sourceDirName);
            foreach (string file in files)
            {
                if (File.Exists(destDirName + Path.GetFileName(file)))
                    continue;
                FileInfo fileInfo = new FileInfo(file);
                if (!fileInfo.Extension.Equals(".lua", StringComparison.CurrentCultureIgnoreCase))
                    continue;

                string newPath = destDirName + Path.GetFileName(file);
                File.Copy(file, newPath, true);
                File.SetAttributes(newPath, FileAttributes.Normal);

                byte[] buffer = null;

                using (FileStream fs = new FileStream(newPath, FileMode.Open))
                {
                    buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, buffer.Length);
                }

                buffer = SecurityUtil.Xor(buffer);
                using (FileStream fs = new FileStream(newPath, FileMode.Create))
                {
                    fs.Write(buffer, 0, buffer.Length);
                    fs.Flush();
                }
            }

            string[] dirs = Directory.GetDirectories(sourceDirName);
            foreach (string dir in dirs)
            {
                CopyLuaDirectory(dir, destDirName + Path.GetFileName(dir));
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    /// <summary>
    /// 选定Tag回调
    /// </summary>
    private void OnSelectTagCallBack()
    {
        switch (tagIndex)
        {
            case 0: //全选
                foreach (AssetBundleEntity entity in m_List)
                {
                    m_Dic[entity.Key] = true;
                }
                break;
            case 1: //Audio
                foreach (AssetBundleEntity entity in m_List)
                {
                    m_Dic[entity.Key] = entity.Tag.Equals("Audio", StringComparison.CurrentCultureIgnoreCase);
                }
                break;
            case 2: //Role
                foreach (AssetBundleEntity entity in m_List)
                {
                    m_Dic[entity.Key] = entity.Tag.Equals("Role", StringComparison.CurrentCultureIgnoreCase);
                }
                break;
            case 3: //Effect
                foreach (AssetBundleEntity entity in m_List)
                {
                    m_Dic[entity.Key] = entity.Tag.Equals("Effect", StringComparison.CurrentCultureIgnoreCase);
                }
                break;
            case 4: //Scene
                foreach (AssetBundleEntity entity in m_List)
                {
                    m_Dic[entity.Key] = entity.Tag.Equals("Scene", StringComparison.CurrentCultureIgnoreCase);
                }
                break;
            case 5: //UI
                foreach (AssetBundleEntity entity in m_List)
                {
                    m_Dic[entity.Key] = entity.Tag.Equals("UI", StringComparison.CurrentCultureIgnoreCase);
                }
                break;
            case 6: //Lua
                foreach (AssetBundleEntity entity in m_List)
                {
                    m_Dic[entity.Key] = entity.Tag.Equals("Lua", StringComparison.CurrentCultureIgnoreCase);
                }
                break;
            case 7: //None
                foreach (AssetBundleEntity entity in m_List)
                {
                    m_Dic[entity.Key] = false;
                }
                break;
        }
        Debug.LogFormat("当前选择的Tag：{0}", arrTag[tagIndex]);
    }

    /// <summary>
    /// 选定Target回调
    /// </summary>
    private void OnSelectTargetCallBack()
    {
        switch (buildTargetIndex)
        {
            case 0: //Windows
                target = BuildTarget.StandaloneWindows;
                break;
            case 1: //Android
                target = BuildTarget.Android;
                break;
            case 2: //iOS
                target = BuildTarget.iOS;
                break;
        }
        Debug.LogFormat("当前选择的BuildTarget：{0}", arrBuildTarget[buildTargetIndex]);
    }

    /// <summary>
    /// 清空设置
    /// </summary>
    private void OnClearSettingCallBack()
    {
        string path = Application.dataPath + "/Download";

        SaveFolderSettings(new string[] { path }, true);
    }

    /// <summary>
    /// 保存设置
    /// </summary>
    private void OnSaveSettingCallBack()
    {
        //需要打包的对象
        List<AssetBundleEntity> lst = new List<AssetBundleEntity>();

        foreach (AssetBundleEntity entity in m_List)
        {
            if (m_Dic[entity.Key])
            {
                entity.IsChecked = true;
                lst.Add(entity);
            }
            else
            {
                entity.IsChecked = false;
                lst.Add(entity);
            }
        }

        //循环设置文件夹包括子文件里边的项
        for (int i = 0; i < lst.Count; i++)
        {
            AssetBundleEntity entity = lst[i];//取到一个节点
            if (entity.IsFolder)
            {
                //如果这个节点配置的是一个文件夹，那么需要遍历文件夹
                //需要把路变成绝对路径
                string[] folderArr = new string[entity.PathList.Count];
                for (int j = 0; j < entity.PathList.Count; j++)
                {
                    folderArr[j] = Application.dataPath + "/" + entity.PathList[j];
                }
                SaveFolderSettings(folderArr, !entity.IsChecked);
            }
            else
            {
                //如果不是文件夹 只需要设置里边的项
                string[] folderArr = new string[entity.PathList.Count];
                for (int j = 0; j < entity.PathList.Count; j++)
                {
                    folderArr[j] = Application.dataPath + "/" + entity.PathList[j];
                    SaveFileSetting(folderArr[j], !entity.IsChecked);
                }
            }
        }

        AssetDatabase.RemoveUnusedAssetBundleNames();
        AssetDatabase.Refresh();
        Debug.Log("保存设置完毕");
    }

    private void SaveFolderSettings(string[] folderArr, bool isSetNull)
    {
        foreach (string folderPath in folderArr)
        {
            //0.对文件夹进行设置

            if (isSetNull)
            {
                //Debug.Log("filePath=" + filePath);
                int index = folderPath.IndexOf("Assets/", StringComparison.CurrentCultureIgnoreCase);

                //路径
                string newPath = folderPath.Substring(index);
                Debug.Log("newfolderPath=" + newPath);

                //文件名
                string fileName = newPath.Replace("Assets/", "");

                //后缀
                string variant = "assetbundle";

                AssetImporter import = AssetImporter.GetAtPath(newPath);
                import.SetAssetBundleNameAndVariant(fileName, variant);

                if (isSetNull)
                {
                    import.SetAssetBundleNameAndVariant(null, null);
                }
                import.SaveAndReimport();
            }

            //1.先看这个文件夹下的文件
            string[] arrFile = Directory.GetFiles(folderPath); //文件夹下的文件

            //2.对文件进行设置
            foreach (string filePath in arrFile)
            {
                Debug.Log("filePath=" + filePath);
                //进行设置
                SaveFileSetting(filePath, isSetNull);
            }

            //3.看这个文件夹下的子文件夹
            string[] arrFolder = Directory.GetDirectories(folderPath);
            SaveFolderSettings(arrFolder, isSetNull);
        }
    }

    private void SaveFileSetting(string filePath, bool isSetNull)
    {
        if (filePath.IndexOf(".") != -1)
        {
            #region 判断如果包含.则 说明是文件
            FileInfo file = new FileInfo(filePath);
            if (!file.Extension.Equals(".meta", StringComparison.CurrentCultureIgnoreCase))
            {
                //Debug.Log("filePath=" + filePath);
                int index = filePath.IndexOf("Assets/", StringComparison.CurrentCultureIgnoreCase);

                //路径
                string newPath = filePath.Substring(index);
                Debug.Log("newPath=" + newPath);

                //文件名
                string fileName = newPath.Replace("Assets/", "").Replace(file.Extension, "");

                //后缀
                string variant = file.Extension.Equals(".unity", StringComparison.CurrentCultureIgnoreCase) ? "unity3d" : "assetbundle";

                AssetImporter import = AssetImporter.GetAtPath(newPath);
                import.SetAssetBundleNameAndVariant(fileName, variant);

                if (isSetNull)
                {
                    import.SetAssetBundleNameAndVariant(null, null);
                }
                import.SaveAndReimport();
            }
            #endregion
        }
        else
        {
            //说明是把文件夹 当成一个资源来打

            //Debug.Log("filePath=" + filePath);
            int index = filePath.IndexOf("Assets/", StringComparison.CurrentCultureIgnoreCase);

            //路径
            string newPath = filePath.Substring(index);
            Debug.Log("newPath=" + newPath);

            //文件名
            string fileName = newPath.Replace("Assets/", "");

            //后缀
            string variant = "assetbundle";

            AssetImporter import = AssetImporter.GetAtPath(newPath);
            import.SetAssetBundleNameAndVariant(fileName, variant);

            if (isSetNull)
            {
                import.SetAssetBundleNameAndVariant(null, null);
            }
            import.SaveAndReimport();
        }
    }

    /// <summary>
    /// 打包回调
    /// </summary>
    private void OnAssetBundleCallBack()
    {
        string toPath = Application.dataPath + "/../AssetBundles/" + arrBuildTarget[buildTargetIndex];
        if (!Directory.Exists(toPath))
        {
            Directory.CreateDirectory(toPath);
        }

        //BuildAssetBundleOptions.None --构建AssetBundle没有任何特殊的选项
        //BuildAssetBundleOptions.UncompressedAssetBundle --不进行数据压缩。如果使用该项，因为没有压缩\解压缩的过程， AssetBundle的发布和加载会很快，但是AssetBundle也会更大，下载变慢
        //BuildAssetBundleOptions.ChunkBasedCompression --Assetbundle的压缩格式为lz4。默认的是lzma格式，下载Assetbundle后立即解压。而lz4格式的Assetbundle会在加载资源的时候才进行解压，只是解压资源的时机不一样

        //打包方法就是一句话
        BuildPipeline.BuildAssetBundles(toPath, BuildAssetBundleOptions.ChunkBasedCompression, target);
        Debug.Log("打包完毕");

#if ASSETBUNDLE_ENCRYPT
        string path = Application.dataPath + "/../AssetBundles/" + arrBuildTarget[buildTargetIndex] + "/Download";
        AssetBundleEncrypt(path);
        Debug.Log("资源包加密完毕");
#endif
    }

    /// <summary>
    /// 清空AssetBundle包回调
    /// </summary>
    private void OnClearAssetBundleCallBack()
    {
        string path = Application.dataPath + "/../AssetBundles/" + arrBuildTarget[buildTargetIndex];
        if (Directory.Exists(path))
        {
            Directory.Delete(path, true);
        }
        Debug.Log("清空完毕");
    }

    /// <summary>
    /// 拷贝数据表
    /// </summary>
    private void OnCopyDataTableCallBack()
    {
        string fromPath = Application.dataPath + "/Download/DataTable";
        string toPath = Application.dataPath + "/../AssetBundles/" + arrBuildTarget[buildTargetIndex] + "/Download/DataTable";
        IOUtil.CopyDirectory(fromPath, toPath);
        Debug.Log("拷贝数据表完毕");
    }

    /// <summary>
    /// 生成版本文件
    /// </summary>
    private void OnCreateVersionFileCallBack()
    {
        string path = Application.dataPath + "/../AssetBundles/" + arrBuildTarget[buildTargetIndex];
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        string strVersionFilePath = path + "/VersionFile.txt"; //版本文件路径

        //如果版本文件存在 则删除
        IOUtil.DeleteFile(strVersionFilePath);

        StringBuilder sbContent = new StringBuilder();

        DirectoryInfo directory = new DirectoryInfo(path);

        //拿到文件夹下所有文件
        FileInfo[] arrFiles = directory.GetFiles("*", SearchOption.AllDirectories);

        for (int i = 0; i < arrFiles.Length; i++)
        {
            FileInfo file = arrFiles[i];
            string fullName = file.FullName; //全名 包含路径扩展名

            //相对路径
            string name = fullName.Substring(fullName.IndexOf(arrBuildTarget[buildTargetIndex]) + arrBuildTarget[buildTargetIndex].Length + 1);

            string md5 = EncryptUtil.GetFileMD5(fullName); //文件的MD5
            if (md5 == null) continue;

            string size = Math.Ceiling(file.Length / 1024f).ToString(); //文件大小

            bool isFirstData = true; //是否初始数据
            bool isBreak = false;

            for (int j = 0; j < m_List.Count; j++)
            {
                foreach (string xmlPath in m_List[j].PathList)
                {
                    string tempPath = xmlPath;
                    if (xmlPath.IndexOf(".") != -1)
                    {
                        tempPath = xmlPath.Substring(0, xmlPath.IndexOf("."));
                    }
                    if (name.IndexOf(tempPath, StringComparison.CurrentCultureIgnoreCase) != -1)
                    {
                        isFirstData = m_List[j].IsFirstData;
                        isBreak = true;
                        break;
                    }
                }
                if (isBreak) break;
            }

            //本地数据表 和 lua脚本 是初始数据
            if (name.IndexOf("DataTable") != -1 || name.IndexOf("xLuaLogic") != -1)
            {
                isFirstData = true;
            }

            string strLine = string.Format("{0} {1} {2} {3}", name, md5, size, isFirstData ? 1 : 0);
            sbContent.AppendLine(strLine);
        }

        IOUtil.CreateTextFile(strVersionFilePath, sbContent.ToString());
        Debug.Log("创建版本文件成功");
    }
}