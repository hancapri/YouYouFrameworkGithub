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
using YouYouFramework;

/// <summary>
/// AssetBundle管理窗口
/// </summary>
public class AssetBundleWindow : EditorWindow
{
    private AssetBundleDAL dal;
    private List<AssetBundleEntity> m_List;
    private Dictionary<string, bool> m_Dic;

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
        string xmlPath = Application.dataPath + @"\YouYouFramework\Editor\AssetBundle\AssetBundleConfig.xml";
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

        if (GUILayout.Button("升级资源版本号(" + dal.GetVersion() + ")", GUILayout.Width(200)))
        {
            EditorApplication.delayCall = OnUpdateVersionCallBack;
        }

        EditorGUILayout.Space();
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal("box");

        if (GUILayout.Button("生成 AssetInfo.bytes", GUILayout.Width(200)))
        {
            EditorApplication.delayCall = OnCreateDependenciesFile;
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
        GUILayout.Label("是否加密", GUILayout.Width(220));
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
            GUILayout.Label(entity.IsEncrypt.ToString(), GUILayout.Width(200));
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
    /// 升级版本号
    /// </summary>
    private void OnUpdateVersionCallBack()
    {
        dal.UpdateVersion();
    }

    #region AssetBundleEncrypt 资源包加密
    /// <summary>
    /// 资源包加密
    /// </summary>
    /// <param name="path"></param>
    private void AssetBundleEncrypt(bool isDelete = false)
    {
        //循环设置文件夹包括子文件里边的项
        for (int i = 0; i < m_List.Count; i++)
        {
            AssetBundleEntity entity = m_List[i];//取到一个节点

            if (entity.IsEncrypt)
            {
                string[] folderArr = new string[entity.PathList.Count];
                for (int j = 0; j < entity.PathList.Count; j++)
                {
                    string path = Application.dataPath + "/../AssetBundles/" + dal.GetVersion() + "/" + arrBuildTarget[buildTargetIndex] + "/" + entity.PathList[j];

                    if (entity.IsFolder == false)
                    {
                        //不是遍历文件夹打包 说明这个路径就是一个包
                        path = path + ".assetbundle";

                        AssetBundleEncryptFile(path, isDelete);
                    }
                    else
                    {
                        AssetBundleEncryptFolder(path, isDelete);
                    }
                }
            }
        }

        if (isDelete)
        {
            Debug.Log("删除加密资源包完毕");
        }
        else
        {
            Debug.Log("资源包加密完毕");
        }
    }

    /// <summary>
    /// 加密文件夹下所有文件
    /// </summary>
    /// <param name="folderPath"></param>
    private void AssetBundleEncryptFolder(string folderPath, bool isDelete = false)
    {
        DirectoryInfo directory = new DirectoryInfo(folderPath);

        //拿到文件夹下所有文件
        FileInfo[] arrFiles = directory.GetFiles("*", SearchOption.AllDirectories);

        foreach (FileInfo file in arrFiles)
        {
            AssetBundleEncryptFile(file.FullName, isDelete);
        }
    }

    /// <summary>
    /// 加密文件
    /// </summary>
    /// <param name="filePath"></param>
    private void AssetBundleEncryptFile(string filePath, bool isDelete = false)
    {
        if (isDelete)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            if (File.Exists(filePath + ".manifest"))
            {
                File.Delete(filePath + ".manifest");
            }

            return;
        }

        FileInfo fileInfo = new FileInfo(filePath);

        if (!fileInfo.Extension.Equals(".assetbundle", StringComparison.CurrentCultureIgnoreCase)
            )
        {
            return;
        }

        byte[] buffer = null;

        using (FileStream fs = new FileStream(filePath, FileMode.Open))
        {
            buffer = new byte[fs.Length];
            fs.Read(buffer, 0, buffer.Length);
        }

        buffer = SecurityUtil.Xor(buffer);

        using (FileStream fs = new FileStream(filePath, FileMode.Create))
        {
            fs.Write(buffer, 0, buffer.Length);
            fs.Flush();
        }
    }
    #endregion

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

    #region 保存设置 清空设置
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
                //Debug.Log("newPath=" + newPath);

                //文件名
                string fileName = newPath.Replace("Assets/", "").Replace(file.Extension, "");

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
            #endregion
        }
        else
        {
            //说明是把文件夹 当成一个资源来打

            //Debug.Log("filePath=" + filePath);
            int index = filePath.IndexOf("Assets/", StringComparison.CurrentCultureIgnoreCase);

            //路径
            string newPath = filePath.Substring(index);
            //Debug.Log("newPath=" + newPath);

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
    #endregion

    #region OnAssetBundleCallBack 打包回调
    /// <summary>
    /// 打包回调
    /// </summary>
    private void OnAssetBundleCallBack()
    {
        AssetBundleEncrypt(true);
        string toPath = Application.dataPath + "/../AssetBundles/" + dal.GetVersion() + "/" + arrBuildTarget[buildTargetIndex];
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

        AssetBundleEncrypt();

        OnCreateDependenciesFile();

        OnCreateVersionFileCallBack();
    }
    #endregion

    #region OnClearAssetBundleCallBack 清空AssetBundle包回调
    /// <summary>
    /// 清空AssetBundle包回调
    /// </summary>
    private void OnClearAssetBundleCallBack()
    {
        string path = Application.dataPath + "/../AssetBundles/" + dal.GetVersion() + "/" + arrBuildTarget[buildTargetIndex];
        if (Directory.Exists(path))
        {
            Directory.Delete(path, true);
        }
        Debug.Log("清空完毕");
    }
    #endregion

    #region OnCreateVersionFileCallBack 生成版本文件
    /// <summary>
    /// 生成版本文件
    /// </summary>
    private void OnCreateVersionFileCallBack()
    {
        string path = Application.dataPath + "/../AssetBundles/" + dal.GetVersion() + "/" + arrBuildTarget[buildTargetIndex];
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

        sbContent.AppendLine(dal.GetVersion());
        for (int i = 0; i < arrFiles.Length; i++)
        {
            FileInfo file = arrFiles[i];

            if (file.Extension == ".manifest")
            {
                continue;
            }
            string fullName = file.FullName; //全名 包含路径扩展名

            //相对路径
            string name = fullName.Substring(fullName.IndexOf(arrBuildTarget[buildTargetIndex]) + arrBuildTarget[buildTargetIndex].Length + 1);

            string md5 = EncryptUtil.GetFileMD5(fullName); //文件的MD5
            if (md5 == null) continue;

            string size = file.Length.ToString(); //文件大小

            bool isFirstData = false; //是否初始数据
            bool isEncrypt = false;
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

                    name = name.Replace("\\", "/");
                    if (name.IndexOf(tempPath, StringComparison.CurrentCultureIgnoreCase) != -1)
                    {
                        isFirstData = m_List[j].IsFirstData;
                        isEncrypt = m_List[j].IsEncrypt;
                        isBreak = true;
                        break;
                    }
                }
                if (isBreak) break;
            }

            string strLine = string.Format("{0}|{1}|{2}|{3}|{4}", name, md5, size, isFirstData ? 1 : 0, isEncrypt ? 1 : 0);
            sbContent.AppendLine(strLine);
        }

        IOUtil.CreateTextFile(strVersionFilePath, sbContent.ToString());

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
                ms.WriteULong(ulong.Parse(arrInner[2]));
                ms.WriteByte(byte.Parse(arrInner[3]));
                ms.WriteByte(byte.Parse(arrInner[4]));
            }
        }

        string filePath = path + "/VersionFile.bytes"; //版本文件路径
        byte[] buffer = ms.ToArray();
        ms.Dispose();
        ms.Close();

        buffer = ZlibHelper.CompressBytes(buffer);
        using (FileStream fs = new FileStream(filePath, FileMode.Create))
        {
            fs.Write(buffer, 0, buffer.Length);
            fs.Close();
            fs.Dispose();
        }
        Debug.Log("创建版本文件成功");
    }

    #endregion

    #region OnCreateDependenciesFile 生成依赖关系文件
    /// <summary>
    /// 生成依赖关系文件
    /// </summary>
    private void OnCreateDependenciesFile()
    {
        //第一次循环 把所有的Asset存储到一个列表里

        //临时列表
        List<AssetEntity> tempLst = new List<AssetEntity>();

        //循环设置文件夹包括子文件里边的项
        for (int i = 0; i < m_List.Count; i++)
        {
            AssetBundleEntity entity = m_List[i];//取到一个节点

            string[] folderArr = new string[entity.PathList.Count];
            for (int j = 0; j < entity.PathList.Count; j++)
            {
                string path = Application.dataPath + "/" + entity.PathList[j];
                //Debug.LogError("path=" + path);
                CollectFileInfo(tempLst, path);
            }
        }

        //
        int len = tempLst.Count;

        //资源列表
        List<AssetEntity> assetList = new List<AssetEntity>();

        for (int i = 0; i < len; i++)
        {
            AssetEntity entity = tempLst[i];

            AssetEntity newEntity = new AssetEntity();
            newEntity.Category = entity.Category;
            newEntity.AssetName = entity.AssetFullName.Substring(entity.AssetFullName.LastIndexOf("/") + 1);
            newEntity.AssetName = newEntity.AssetName.Substring(0, newEntity.AssetName.LastIndexOf("."));
            newEntity.AssetFullName = entity.AssetFullName;
            newEntity.AssetBundleName = entity.AssetBundleName;

            assetList.Add(newEntity);

            //场景不需要检查依赖项
            if (entity.Category == AssetCategory.Scenes)
            {
                continue;
            }

            newEntity.DependsAssetList = new List<AssetDependsEntity>();

            string[] arr = AssetDatabase.GetDependencies(entity.AssetFullName);
            foreach (string str in arr)
            {
                if (!str.Equals(newEntity.AssetFullName, StringComparison.CurrentCultureIgnoreCase) && GetIsAsset(tempLst, str))
                {
                    AssetDependsEntity assetDepends = new AssetDependsEntity();
                    assetDepends.Category = GetAssetCategory(str);
                    assetDepends.AssetFullName = str;

                    //把依赖资源 加入到依赖资源列表
                    newEntity.DependsAssetList.Add(assetDepends);
                }
            }
        }

        //生成一个Json文件
        string targetPath = Application.dataPath + "/../AssetBundles/" + dal.GetVersion() + "/" + arrBuildTarget[buildTargetIndex];
        if (!Directory.Exists(targetPath))
        {
            Directory.CreateDirectory(targetPath);
        }

        string strJsonFilePath = targetPath + "/AssetInfo.json"; //版本文件路径
        IOUtil.CreateTextFile(strJsonFilePath, LitJson.JsonMapper.ToJson(assetList));
        Debug.Log("生成 AssetInfo.json 完毕");

        MMO_MemoryStream ms = new MMO_MemoryStream();
        //生成二进制文件
        len = assetList.Count;
        ms.WriteInt(len);

        for (int i = 0; i < len; i++)
        {
            AssetEntity entity = assetList[i];
            ms.WriteByte((byte)entity.Category);
            ms.WriteUTF8String(entity.AssetFullName);
            ms.WriteUTF8String(entity.AssetBundleName);

            if (entity.DependsAssetList != null)
            {
                //添加依赖资源
                int depLen = entity.DependsAssetList.Count;
                ms.WriteInt(depLen);
                for (int j = 0; j < depLen; j++)
                {
                    AssetDependsEntity assetDepends = entity.DependsAssetList[j];
                    ms.WriteByte((byte)assetDepends.Category);
                    ms.WriteUTF8String(assetDepends.AssetFullName);
                }
            }
            else
            {
                ms.WriteInt(0);
            }
        }

        string filePath = targetPath + "/AssetInfo.bytes"; //版本文件路径
        byte[] buffer = ms.ToArray();
        buffer = ZlibHelper.CompressBytes(buffer);
        FileStream fs = new FileStream(filePath, FileMode.Create);
        fs.Write(buffer, 0, buffer.Length);
        fs.Close();
        fs.Dispose();
        Debug.Log("生成 AssetInfo.bytes 完毕");
    }

    /// <summary>
    /// 判断某个资源是否存在于资源列表
    /// </summary>
    /// <param name="tempLst"></param>
    /// <param name="assetFullName"></param>
    /// <returns></returns>
    private bool GetIsAsset(List<AssetEntity> tempLst, string assetFullName)
    {
        int len = tempLst.Count;
        for (int i = 0; i < len; i++)
        {
            AssetEntity entity = tempLst[i];
            if (entity.AssetFullName.Equals(assetFullName, StringComparison.CurrentCultureIgnoreCase))
            {
                return true;
            }
        }
        return false;
    }
    #endregion

    #region CollectFileInfo 收集文件信息
    /// <summary>
    /// 收集文件信息
    /// </summary>
    /// <param name="tempLst"></param>
    /// <param name="folderPath"></param>
    private void CollectFileInfo(List<AssetEntity> tempLst, string folderPath)
    {
        if (folderPath.IndexOf(".unity") != -1)
        {
            int index = folderPath.IndexOf("Assets/", StringComparison.CurrentCultureIgnoreCase);
            //路径
            string newPath = folderPath.Substring(index);

            AssetImporter import = AssetImporter.GetAtPath(newPath);

            AssetEntity entity = new AssetEntity();
            entity.AssetFullName = newPath.Replace("\\", "/");
            entity.Category = AssetCategory.Scenes;
            entity.AssetBundleName = import.assetBundleName + ".assetbundle";
            tempLst.Add(entity);
        }
        else
        {
            DirectoryInfo directory = new DirectoryInfo(folderPath);

            //拿到文件夹下所有文件
            FileInfo[] arrFiles = directory.GetFiles("*", SearchOption.AllDirectories);

            for (int i = 0; i < arrFiles.Length; i++)
            {
                FileInfo file = arrFiles[i];
                if (file.Extension == ".meta")
                {
                    continue;
                }

                string filePath = file.FullName; //全名 包含路径扩展名

                //Debug.LogError("filePath=" + filePath);
                int index = filePath.IndexOf("Assets\\", StringComparison.CurrentCultureIgnoreCase);

                //路径
                string newPath = filePath.Substring(index);
                if (newPath.IndexOf(".idea") != -1) //过滤掉idea文件
                {
                    continue;
                }

                AssetEntity entity = new AssetEntity();
                entity.AssetFullName = newPath.Replace("\\", "/");
                entity.Category = GetAssetCategory(newPath.Replace(file.Name, "")); //去掉文件名，只保留路径
                entity.AssetBundleName = GetAssetBundleName(newPath) + ".assetbundle";
                tempLst.Add(entity);
            }
        }
    }
    #endregion

    #region GetAssetCategory 获取资源分类
    /// <summary>
    /// 获取资源分类
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    private AssetCategory GetAssetCategory(string filePath)
    {
        AssetCategory category = AssetCategory.None;

        if (filePath.IndexOf("Audio") != -1)
        {
            category = AssetCategory.Audio;
        }
        else if (filePath.IndexOf("CusShaders") != -1)
        {
            category = AssetCategory.CusShaders;
        }
        else if (filePath.IndexOf("DataTable") != -1)
        {
            category = AssetCategory.DataTable;
        }
        else if (filePath.IndexOf("EffectSources") != -1)
        {
            category = AssetCategory.EffectSources;
        }
        else if (filePath.IndexOf("RoleEffectPrefab") != -1)
        {
            category = AssetCategory.RoleEffectPrefab;
        }
        else if (filePath.IndexOf("UIEffectPrefab") != -1)
        {
            category = AssetCategory.UIEffectPrefab;
        }
        else if (filePath.IndexOf("RolePrefab") != -1)
        {
            category = AssetCategory.RolePrefab;
        }
        else if (filePath.IndexOf("RoleSources") != -1)
        {
            category = AssetCategory.RoleSources;
        }
        else if (filePath.IndexOf("Scenes") != -1)
        {
            category = AssetCategory.Scenes;
        }
        else if (filePath.IndexOf("UIFont") != -1)
        {
            category = AssetCategory.UIFont;
        }
        else if (filePath.IndexOf("UIPrefab") != -1)
        {
            category = AssetCategory.UIPrefab;
        }
        else if (filePath.IndexOf("UIRes") != -1)
        {
            category = AssetCategory.UIRes;
        }
        else if (filePath.IndexOf("xLuaLogic") != -1)
        {
            category = AssetCategory.xLuaLogic;
        }
        return category;
    }
    #endregion

    #region GetAssetBundleName 获取资源包的名称
    /// <summary>
    /// 获取资源包的名称
    /// </summary>
    /// <param name="newPath"></param>
    /// <returns></returns>
    private string GetAssetBundleName(string newPath)
    {
        AssetImporter import = AssetImporter.GetAtPath(newPath);
        if (import != null)
        {
            if (!string.IsNullOrEmpty(import.assetBundleName))
            {
                return import.assetBundleName;
            }
            else
            {
                //递归寻找上一级目录
                string path = newPath.Substring(0, newPath.Replace("\\", "/").LastIndexOf("/"));
                return GetAssetBundleName(path);
            }
        }
        return null;
    }
    #endregion
}