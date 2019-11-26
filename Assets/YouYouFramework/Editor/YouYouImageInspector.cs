//===================================================
//作    者：边涯  http://www.u3dol.com
//创建时间：
//备    注：
//===================================================
using UnityEditor;

namespace YouYouFramework
{
    [CustomEditor(typeof(YouYouImage))]
    public class YouYouImageInspector : UnityEditor.UI.ImageEditor
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