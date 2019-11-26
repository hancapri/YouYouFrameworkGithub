//===================================================
//作    者：边涯  http://www.u3dol.com
//创建时间：
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace YouYouFramework
{
    [CustomEditor(typeof(LocalizationComponent), true)]
    public class LocalizationComponentInspector : Editor
    {
        private SerializedProperty m_CurrLanguage = null;

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(m_CurrLanguage);
            serializedObject.ApplyModifiedProperties();
        }

        private void OnEnable()
        {
            //建立属性关系
            m_CurrLanguage = serializedObject.FindProperty("m_CurrLanguage");
            serializedObject.ApplyModifiedProperties();
        }
    }
}