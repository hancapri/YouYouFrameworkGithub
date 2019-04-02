using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace YouYouFramework
{
    /// <summary>
    /// 重绘自定义GFImage组件
    /// </summary>
    [CustomEditor(typeof(GFImage))]
    public class GFImageInspector : UnityEditor.UI.ImageEditor
    {
        private SerializedProperty m_Localization;

        protected override void OnEnable()
        {
            base.OnEnable();
            m_Localization = serializedObject.FindProperty("m_Localization");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();
            EditorGUILayout.PropertyField(m_Localization);
            serializedObject.ApplyModifiedProperties();
        }
    }
}
