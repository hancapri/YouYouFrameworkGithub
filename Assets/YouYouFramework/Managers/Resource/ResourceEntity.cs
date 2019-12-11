using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YouYouFramework
{
    /// <summary>
    /// 资源实体（ab包和asset实体）
    /// </summary>
    public class ResourceEntity
    {
        /// <summary>
        /// 资源名称
        /// </summary>
        public string ResourceName;

        /// <summary>
        /// 资源分类
        /// </summary>
        public AssetCategory Category;

        /// <summary>
        /// 是否是ab包
        /// </summary>
        public bool IsAssetBundle;

        /// <summary>
        /// 关联目标
        /// </summary>
        public object Target;

        /// <summary>
        /// 上次使用时间
        /// </summary>
        private float LastUseTime;

        /// <summary>
        /// 引用计数
        /// </summary>
        public int ReferenceCount
        {
            get;
            private set;
        }

        /// <summary>
        /// 对象取池
        /// </summary>
        public void Spawn()
        {
            LastUseTime = Time.time;

            if (!IsAssetBundle)
            {
                ReferenceCount++;
            }
        }

        /// <summary>
        /// 对象回池
        /// </summary>
        public void UnSpawn()
        {
            LastUseTime = Time.time;
            ReferenceCount--;
            if (ReferenceCount < 0)
            {
                ReferenceCount = 0;
            }
        }

        /// <summary>
        /// 对象是否可以释放
        /// </summary>
        /// <returns></returns>
        public bool GetCanRelease()
        {
            if (ReferenceCount == 0 && Time.time - LastUseTime >GameEntry.Pool.ReleaseResourceInterval)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Release()
        {
            ResourceName = null;
            if (IsAssetBundle)
            {
                AssetBundle bundle = Target as AssetBundle;
                bundle.Unload(false);
                Debug.LogError("卸载了资源包");
            }
            Target = null;
            GameEntry.Pool.EnqueueClassObject(this);//把资源实体回池
        }
    }
}
