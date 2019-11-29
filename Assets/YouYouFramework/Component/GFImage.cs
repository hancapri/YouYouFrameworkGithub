using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace YouYouFramework
{
    /// <summary>
    /// 自定义图片组件
    /// </summary>
    public class GFImage : Image
    {
        [Header("本地化语言Key")]
        [SerializeField]
        private string m_Localization;

        protected override void Start()
        {
            base.Start();
            if (GameEntry.Localization != null)
            {
                string imagePath = GameUtil.GetUIPath(GameEntry.Localization.GetString(m_Localization));

#if UNITY_EDITOR
                //编辑器模式下
                Texture2D texture = UnityEditor.AssetDatabase.LoadAssetAtPath<Texture2D>(imagePath);
                Sprite obj = Sprite.Create(texture, new Rect(0,0,texture.width,texture.height),new Vector2(0.5f,0.5f));
                sprite = obj;
                SetNativeSize();
#endif
            }
        }
    }
}
