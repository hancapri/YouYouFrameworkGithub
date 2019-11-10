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
    public class YouYouText : Text
    {
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