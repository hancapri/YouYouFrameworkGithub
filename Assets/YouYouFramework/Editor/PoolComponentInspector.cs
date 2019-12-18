using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace YouYouFramework
{
    [CustomEditor(typeof(PoolComponent), true)]
    public class PoolComponentInspector : Editor
    {
        //关联PoolComponent组件上的ClearInterval（释放池中对象的时间间隔）字段的值
        private SerializedProperty m_ClearInterval;

        //关联PoolComponent组件上的游戏对象池集合
        private SerializedProperty m_GameObjectPoolGroups;

        //关联PoolComponent组件上的ReleaseResourceInterval（释放资源池的时间间隔）字段的值
        private SerializedProperty ReleaseResourceInterval;

        public override void OnInspectorGUI()
        {
            //base.OnInspectorGUI();
            serializedObject.Update();

            PoolComponent component = base.target as PoolComponent;

            //绘制滑动条
            int clearIntervalSlider = (int)EditorGUILayout.Slider("释放池中对象的间隔", m_ClearInterval.intValue, 10, 1800);
            if (clearIntervalSlider != m_ClearInterval.intValue)
            {
                component.ClearInterval = clearIntervalSlider;
            }

            //======================类对象池开始========================
            GUILayout.Space(10);
            GUILayout.BeginVertical("box");
            GUILayout.BeginHorizontal("box");
            GUILayout.Label("类名");
            GUILayout.Label("池中数量", GUILayout.Width(60));
            GUILayout.Label("常驻数量", GUILayout.Width(60));
            GUILayout.EndHorizontal();

            if (component != null && component.PoolManager != null)
            {
                foreach (var item in component.PoolManager.ClassObjectPool.InspectorDic)
                {
                    GUILayout.BeginHorizontal("box");
                    GUILayout.Label(item.Key.Name);
                    GUILayout.Label(item.Value.ToString(), GUILayout.Width(60));

                    int key = item.Key.GetHashCode();
                    byte resideCount = 0;
                    component.PoolManager.ClassObjectPool.ClassObjectCountDic.TryGetValue(key, out resideCount);
                    GUILayout.Label(resideCount.ToString(), GUILayout.Width(60));
                    GUILayout.EndHorizontal();
                }
            }
            GUILayout.EndVertical();
            //======================类对象池结束========================

            //======================变量计数开始========================
            GUILayout.Space(10);
            GUILayout.BeginVertical("box");
            GUILayout.BeginHorizontal("box");
            GUILayout.Label("变量");
            GUILayout.Label("数量", GUILayout.Width(60));
            GUILayout.EndHorizontal();

            if (component != null)
            {
                foreach (var item in component.VarObjectInspectorDic)
                {
                    GUILayout.BeginHorizontal("box");
                    GUILayout.Label(item.Key.Name);
                    GUILayout.Label(item.Value.ToString(), GUILayout.Width(60));

                    GUILayout.EndHorizontal();
                }
            }
            GUILayout.EndVertical();
            //======================变量计数结束========================

            GUILayout.Space(10);
            EditorGUILayout.PropertyField(m_GameObjectPoolGroups, true);

            //绘制滑动条 释放资源池的间隔
            int releaseAssetBundleInterval = (int)EditorGUILayout.Slider("释放资源池的间隔", ReleaseResourceInterval.intValue, 10, 1800);
            if (releaseAssetBundleInterval != ReleaseResourceInterval.intValue)
            {
                component.ReleaseResourceInterval = releaseAssetBundleInterval;
            }
            else
            {
                ReleaseResourceInterval.intValue = releaseAssetBundleInterval;
            }

            //======================资源包统计开始========================
            GUILayout.Space(10);
            GUILayout.BeginVertical("box");
            GUILayout.BeginHorizontal("box");
            GUILayout.Label("资源包");
            GUILayout.Label("数量", GUILayout.Width(60));
            GUILayout.EndHorizontal();

            if (component != null && component.PoolManager != null)
            {
                foreach (var item in component.PoolManager.AssetBundlePool.InspectorDic)
                {
                    GUILayout.BeginHorizontal("box");
                    GUILayout.Label(item.Key);
                    GUILayout.Label(item.Value.ToString(), GUILayout.Width(50));

                    GUILayout.EndHorizontal();
                }
            }
            GUILayout.EndVertical();
            //======================资源包统计结束========================

            serializedObject.ApplyModifiedProperties();
            //重绘
            Repaint();
        }

        private void OnEnable()
        {
            //建立字段关联
            m_ClearInterval = serializedObject.FindProperty("ClearInterval");
            m_GameObjectPoolGroups = serializedObject.FindProperty("m_GameObjectPoolGroups");
            ReleaseResourceInterval = serializedObject.FindProperty("ReleaseResourceInterval");

            serializedObject.ApplyModifiedProperties();
        }
    }
}
