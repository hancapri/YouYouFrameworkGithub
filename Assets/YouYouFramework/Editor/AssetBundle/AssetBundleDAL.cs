//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2016-03-17 21:52:29
//备    注：
//===================================================
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;

public class AssetBundleDAL
{
    /// <summary>
    /// xml路径
    /// </summary>
    private string m_Path;

    /// <summary>
    /// 返回的数据集合
    /// </summary>
    private List<AssetBundleEntity> m_List = null;

    public AssetBundleDAL(string path)
    {
        m_Path = path;
        m_List = new List<AssetBundleEntity>();
    }

    private XDocument xDoc;

    /// <summary>
    /// 获取版本号
    /// </summary>
    /// <returns></returns>
    public string GetVersion()
    {
        XElement root = xDoc.Root;
        XElement assetBundleNode = root.Element("AssetBundle");
        XAttribute attribute = assetBundleNode.Attribute("ResourceVersion");
        return attribute.Value;
    }

    /// <summary>
    /// 升级版本号
    /// </summary>
    public void UpdateVersion()
    {
        XElement root = xDoc.Root;
        XElement assetBundleNode = root.Element("AssetBundle");
        XAttribute attribute = assetBundleNode.Attribute("ResourceVersion");
        string version = attribute.Value;
        string[] arr = version.Split('.');

        int shortVersion = int.Parse(arr[2]);
        version = string.Format("{0}.{1}.{2}", arr[0], arr[1], ++shortVersion);
        attribute.SetValue(version);
        xDoc.Save(m_Path);
    }

    /// <summary>
    /// 返回xml数据
    /// </summary>
    /// <returns></returns>
    public List<AssetBundleEntity> GetList()
    {
        m_List.Clear();

        //读取xml 把数据添加到m_List里边
        xDoc = XDocument.Load(m_Path);
        XElement root = xDoc.Root;

        XElement assetBundleNode = root.Element("AssetBundle");

        IEnumerable<XElement> lst = assetBundleNode.Elements("Item");

        int index = 0;
        foreach (XElement item in lst)
        {
            AssetBundleEntity entity = new AssetBundleEntity();
            entity.Key = "key" + ++index;
            entity.Name = item.Attribute("Name").Value;
            entity.Tag = item.Attribute("Tag").Value;
            entity.IsFolder = item.Attribute("IsFolder").Value.Equals("True", System.StringComparison.CurrentCultureIgnoreCase);
            entity.IsFirstData = item.Attribute("IsFirstData").Value.Equals("True", System.StringComparison.CurrentCultureIgnoreCase);
            entity.IsEncrypt = item.Attribute("IsEncrypt").Value.Equals("True", System.StringComparison.CurrentCultureIgnoreCase);

            IEnumerable<XElement> pathList = item.Elements("Path");
            foreach (XElement path in pathList)
            {
                entity.PathList.Add(path.Attribute("Value").Value);
            }

            m_List.Add(entity);
        }
        return m_List;
    }
}