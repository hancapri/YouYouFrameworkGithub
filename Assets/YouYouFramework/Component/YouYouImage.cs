//===================================================
//作    者：边涯  http://www.u3dol.com
//创建时间：
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace YouYouFramework
{
    public class YouYouImage : Image
    {
        [Header("本地化语言Key")]
        [SerializeField]
        private string m_Localization;

        protected override void Start()
        {
            base.Start();
            if (GameEntry.Localization != null)
            {
                string path = GameUtil.GetUIResPath(GameEntry.Localization.GetString(m_Localization));
                Debug.Log(path);

                Texture2D texture = UnityEditor.AssetDatabase.LoadAssetAtPath<Texture2D>(path) as Texture2D;

                Sprite obj = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                sprite = obj;
                SetNativeSize();
            }
        }
    }
}