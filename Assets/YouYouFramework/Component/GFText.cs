using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace YouYouFramework
{
    /// <summary>
    /// 自定义文本组件
    /// </summary>
    public class GFText : Text {
        [Header("本地化语言Key")]
        [SerializeField]
        private string m_Localization;
        protected override void Start()
        {
            base.Start();
            if (GameEntry.Localization != null)
            {
                text = GameEntry.Localization.GetString(m_Localization);
            }
        }
    }
}
