using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace YouYouFramework
{
    [CustomEditor(typeof(LocalizationComponent))]
    public class LocalizationComponentInspector : Editor
    {
        private SerializedProperty m_CurrLanguage;

        private void OnEnable()
        {
            m_CurrLanguage = serializedObject.FindProperty("m_CurrLanguage"); 
            //serializedObject.ApplyModifiedProperties(); 
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(m_CurrLanguage);
            serializedObject.ApplyModifiedProperties();
        }
    }
}