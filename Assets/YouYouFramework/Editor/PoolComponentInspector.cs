using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace YouYouFramework
{
    [CustomEditor(typeof(PoolComponent), true)]
    public class PoolComponentInspector : Editor
    {
        //关联PoolComponent组件上的ClearInterval（释放池中对象的时间间隔）字段的值
        private SerializedProperty m_ClearInterval;

        //关联PoolComponent组件上的游戏对象池集合
        private SerializedProperty m_GameObjectPoolGroups;

        //关联PoolComponent组件上的ReleaseResourceInterval（释放AssetBundle池的时间间隔）字段的值
        private SerializedProperty ReleaseResourceInterval;

        //关联PoolComponent组件上的LockedAssetBundle（锁定资源包）
        private SerializedProperty LockedAssetBundle;

        //关联PoolComponent组件上的ReleaseAssetInterval（释放Asset池的时间间隔）字段的值
        private SerializedProperty ReleaseAssetInterval;

        //关联PoolComponent组件上的是否显示资源池选项
        private SerializedProperty ShowAssetPool;

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
            GUILayout.Space(50);
            int releaseAssetBundleInterval = (int)EditorGUILayout.Slider("释放AssetBundle池的间隔", ReleaseResourceInterval.intValue, 10, 1800);
            if (releaseAssetBundleInterval != ReleaseResourceInterval.intValue)
            {
                component.ReleaseResourceInterval = releaseAssetBundleInterval;
            }
            else
            {
                ReleaseResourceInterval.intValue = releaseAssetBundleInterval;
            }

            //======================锁定资源包开始========================
            //GUILayout.Space(10);
            EditorGUILayout.PropertyField(LockedAssetBundle, true);
            //======================锁定资源包结束========================

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

            //绘制滑动条 释放资源池的间隔
            GUILayout.Space(50);
            int releaseAssetInterval = (int)EditorGUILayout.Slider("释放Asset池的间隔", ReleaseAssetInterval.intValue, 10, 1800);
            if (releaseAssetInterval != ReleaseAssetInterval.intValue)
            {
                component.ReleaseAssetInterval = releaseAssetInterval;
            }
            else
            {
                ReleaseAssetInterval.intValue = releaseAssetInterval;
            }

            //======================资源统计开始========================
            GUILayout.Space(10);
            bool showAssetPool = EditorGUILayout.Toggle("显示分类资源池", ShowAssetPool.boolValue);
            if (showAssetPool != ShowAssetPool.boolValue)
            {
                component.ShowAssetPool = showAssetPool;
            }
            else
            {
                ShowAssetPool.boolValue = showAssetPool;
            }
            if (showAssetPool)
            {
                GUILayout.Space(10);
                var enumerator = Enum.GetValues(typeof(AssetCategory)).GetEnumerator();
                while (enumerator.MoveNext())
                {
                    AssetCategory assetCategory = (AssetCategory)enumerator.Current;
                    if (assetCategory == AssetCategory.None)
                    {
                        continue;
                    }

                    GUILayout.Space(10);
                    GUILayout.BeginVertical("box");
                    GUILayout.BeginHorizontal("box");
                    GUILayout.Label("资源分类-"+assetCategory.ToString());
                    GUILayout.Label("计数", GUILayout.Width(50));
                    GUILayout.EndHorizontal();

                    if (component != null && component.PoolManager != null)
                    {
                        foreach (var item in component.PoolManager.AssetPool[assetCategory].InspectorDic)
                        {
                            GUILayout.BeginHorizontal("box");
                            GUILayout.Label(item.Key);
                            GUILayout.Label(item.Value.ToString(), GUILayout.Width(50));

                            GUILayout.EndHorizontal();
                        }
                    }
                    GUILayout.EndVertical();
                }
            }
            //======================资源统计结束========================

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
            ReleaseAssetInterval = serializedObject.FindProperty("ReleaseAssetInterval");
            LockedAssetBundle = serializedObject.FindProperty("LockedAssetBundle");
            ShowAssetPool = serializedObject.FindProperty("ShowAssetPool");

            serializedObject.ApplyModifiedProperties();
        }
    }
}
