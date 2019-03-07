using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YouYouFramework
{
    /// <summary>
    /// gameObject变量
    /// </summary>
    public class VarGameObject : Variable<GameObject>
    {
        public static VarGameObject Alloc()
        {
            //要从对象池获取
            VarGameObject var = GameEntry.Pool.DequeueVarObject<VarGameObject>();
            var.Value = default(GameObject);
            var.Retain();
            return var;
        }

        /// <summary>
        /// 分配一个对象
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static VarGameObject Alloc(GameObject value)
        {
            //要从对象池获取
            VarGameObject var = Alloc();
            var.Value = value;
            return var;
        }

        public static implicit operator GameObject(VarGameObject value)
        {
            return value.Value;
        }
    }
}
