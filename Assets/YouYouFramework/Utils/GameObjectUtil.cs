//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2015-12-01 21:45:22
//备    注：
//===================================================
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using DG.Tweening;

public static class GameObjectUtil
{
    /// <summary>
    /// 获取或创建组建
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static T GetOrCreatComponent<T>(this GameObject obj) where T : MonoBehaviour
    {
        T t = obj.GetComponent<T>();
        if (t == null)
        {
            t = obj.AddComponent<T>();
        }
        return t;
    }

    /// <summary>
    /// 设置物体的父物体
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="parent"></param>
    public static void SetParent(this GameObject obj, Transform parent)
    {
        obj.transform.parent = parent;
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localScale = Vector3.one;
        obj.transform.localEulerAngles = Vector3.zero;
    }

    /// <summary>
    /// 设置物体的层
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="layerName"></param>
    public static void SetLayer(this GameObject obj, string layerName)
    {
        Transform[] transArr = obj.transform.GetComponentsInChildren<Transform>();
        for (int i = 0; i < transArr.Length; i++)
        {
            transArr[i].gameObject.layer = LayerMask.NameToLayer(layerName);
        }
    }

    public static void SetNull(this MonoBehaviour[] arr)
    {
        if (arr != null)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = null;
            }
            arr = null;
        }
    }

    public static void SetNull(this Transform[] arr)
    {
        if (arr != null)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = null;
            }
            arr = null;
        }
    }

    public static void SetNull(this Sprite[] arr)
    {
        if (arr != null)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = null;
            }
            arr = null;
        }
    }

    public static void SetNull(this GameObject[] arr)
    {
        if (arr != null)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = null;
            }
            arr = null;
        }
    }

    //UI扩展=================================

    /// <summary>
    /// 设置Text值
    /// </summary>
    /// <param name="txtObj"></param>
    /// <param name="text"></param>
    public static void SetText(this Text txtObj, string text, bool isAnimation = false, float duration = 0.2f, ScrambleMode scrambleMode = ScrambleMode.None)
    {
        if (txtObj != null)
        {
            if (isAnimation)
            {
                txtObj.text = "";
                txtObj.DOText(text, duration, scrambleMode: scrambleMode);
            }
            else
            {
                txtObj.text = text;
            }
        }
    }

    /// <summary>
    /// 设置滑动条的值
    /// </summary>
    /// <param name="sliderObj"></param>
    /// <param name="value"></param>
    public static void SetSliderValue(this Slider sliderObj, float value)
    {
        if (sliderObj != null)
        {
            sliderObj.value = value;
        }
    }

    /// <summary>
    /// 设置图片名称
    /// </summary>
    /// <param name="imgObj"></param>
    /// <param name="imgName"></param>
    public static void SetImage(this Image imgObj, Sprite sprite)
    {
        if (imgObj != null)
        {
            imgObj.overrideSprite = sprite;
        }
    }
}