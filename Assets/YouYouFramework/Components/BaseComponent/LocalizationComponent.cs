using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YouYouFramework
{
    /// <summary>
    /// 本地化（多语言）组件
    /// </summary>
    public class LocalizationComponent : YouYouBaseComponent
    {
        public enum LanguageEnum
        {
            Chinese = 0,
            English = 1
        }

        [SerializeField]
        private LanguageEnum m_CurrLanguage;

        /// <summary>
        /// 当前语言要和本地化表的语言字段保持一致
        /// </summary>
        public LanguageEnum CurrLanguage { get { return m_CurrLanguage; } }

        private LocalizationManager m_LocalizationManager;


        protected override void OnAwake()
        {
            base.OnAwake();
#if !UNITY_EDITOR
            Init();
#endif
        }

        protected override void OnStart()
        {
            base.OnStart();
            m_LocalizationManager = new LocalizationManager();
        }

        /// <summary>
        /// 获取多语言内容
        /// </summary>
        /// <param name="key"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public string GetString(string key, params object[] args)
        {
            return m_LocalizationManager.GetString(key,args);
        }

        /// <summary>
        /// 设置系统语言
        /// </summary>
        private void Init()
        {
            switch (Application.systemLanguage)
            {
                case SystemLanguage.Chinese:
                case SystemLanguage.ChineseSimplified:
                case SystemLanguage.ChineseTraditional:
                    m_CurrLanguage = LanguageEnum.Chinese;
                    break;
                case SystemLanguage.English:
                    m_CurrLanguage = LanguageEnum.English;
                    break;
                default:
                    m_CurrLanguage = LanguageEnum.Chinese;
                    break;
            }
        }

        public override void Shutdown()
        {

        }
    }
}
