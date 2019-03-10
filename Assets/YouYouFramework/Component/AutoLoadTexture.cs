using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using XLua;

/// <summary>
/// 自动加载图片
/// </summary>
public class AutoLoadTexture : MonoBehaviour
{
    /// <summary>
    /// 图片名称
    /// </summary>
    public string ImgName;

    /// <summary>
    /// 图片路径
    /// </summary>
    public string ImgPath;

    /// <summary>
    /// 是否设置图片原本大小
    /// </summary>
    public bool IsSetNativeSize;

    void Start()
    {
    }

    public void SetImg()
    {
        Image img = GetComponent<Image>();

        if (img != null && !string.IsNullOrEmpty(ImgPath))
        {
            //AssetBundleMgr.Instance.LoadOrDownload<Texture2D>(ImgPath, ImgName, (Texture2D obj) =>
            //{
            //    if (obj == null) return;

            //    var iconRect = new Rect(0, 0, obj.width, obj.height);
            //    var iconSprite = Sprite.Create(obj, iconRect, new Vector2(.5f, .5f));

            //    img.overrideSprite = iconSprite;
            //    if (IsSetNativeSize)
            //    {
            //        img.SetNativeSize();
            //    }
            //}, type: 1);
        }
    }
}