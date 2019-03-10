//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2016-06-11 12:53:31
//备    注：
//===================================================
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using XLua;
using UnityEngine.UI;

[LuaCallCSharp]
public class GameUtil
{
    #region GetRandomPos 获取目标点周围的随机点
    /// <summary>
    /// 获取目标点周围的随机点
    /// </summary>
    /// <param name="targerPos"></param>
    /// <param name="distance"></param>
    /// <returns></returns>
    public static Vector3 GetRandomPos(Vector3 targerPos, float distance)
    {
        //1.定义一个向量
        Vector3 v = new Vector3(0, 0, 1); //z轴超前的

        //2.让向量旋转
        v = Quaternion.Euler(0, UnityEngine.Random.Range(0, 360f), 0) * v;

        //3.向量 * 距离(半径) = 坐标点
        Vector3 pos = v * distance * UnityEngine.Random.Range(0.8f, 1f);

        //4.计算出来的 围绕主角的 随机坐标点
        return targerPos + pos;
    }

    public static Vector3 GetRandomPos(Vector3 currPos, Vector3 targerPos, float distance)
    {
        //1.定义一个向量
        Vector3 v = (currPos - targerPos).normalized;

        //2.让向量旋转
        v = Quaternion.Euler(0, UnityEngine.Random.Range(-90f, 90f), 0) * v;

        //3.向量 * 距离(半径) = 坐标点
        Vector3 pos = v * distance * UnityEngine.Random.Range(0.8f, 1f);

        //4.计算出来的 围绕主角的 随机坐标点
        return targerPos + pos;
    }
    #endregion

    #region GetPathLen 计算路径的长度
    /// <summary>
    /// 计算路径的长度
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static float GetPathLen(List<Vector3> path)
    {
        float pathLen = 0f; //路径的总长度 计算出路径

        for (int i = 0; i < path.Count; i++)
        {
            if (i == path.Count - 1) continue;

            float dis = Vector3.Distance(path[i], path[i + 1]);
            pathLen += dis;
        }

        return pathLen;
    }
    #endregion

    #region GetFileName 获取文件名
    /// <summary>
    /// 获取文件名
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string GetFileName(string path)
    {
        string fileName = path;
        int lastIndex = path.LastIndexOf('/');
        if (lastIndex > -1)
        {
            fileName = fileName.Substring(lastIndex + 1);
        }

        lastIndex = fileName.LastIndexOf('.');
        if (lastIndex > -1)
        {
            fileName = fileName.Substring(0, lastIndex);
        }

        return fileName;
    }
    #endregion

    #region AutoLoadTexture 自动加载图片
    /// <summary>
    /// 自动加载图片
    /// </summary>
    /// <param name="go"></param>
    /// <param name="imgPath"></param>
    /// <param name="imgName"></param>
    public static void AutoLoadTexture(GameObject go, string imgPath, string imgName, bool isSetNativeSize)
    {
        if (go != null)
        {
            AutoLoadTexture component = go.GetOrCreatComponent<AutoLoadTexture>();
            if (component != null)
            {
                component.ImgPath = imgPath;
                component.ImgName = imgName;
                component.IsSetNativeSize = isSetNativeSize;
                component.SetImg();
            }
        }
    }
    #endregion

    #region AutoNumberAnimation 自动数字动画
    /// <summary>
    /// 自动数字动画
    /// </summary>
    /// <param name="go"></param>
    /// <param name="number"></param>
    public static void AutoNumberAnimation(GameObject go, int number)
    {
        if (go != null)
        {
            AutoNumberAnimation component = go.GetOrCreatComponent<AutoNumberAnimation>();
            component.DoNumber(number);
        }
    }
    #endregion

    /// <summary>
    /// 添加子物体
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="prefab"></param>
    /// <returns></returns>
    public static GameObject AddChild(Transform parent, GameObject prefab)
    {
        GameObject go = GameObject.Instantiate(prefab) as GameObject;

        if (go != null && parent != null)
        {
            Transform t = go.transform;
            t.SetParent(parent, false);
            go.layer = parent.gameObject.layer;
        }
        return go;
    }
}