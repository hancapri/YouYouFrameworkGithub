using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YouYouFramework
{
    /// <summary>
    /// transform变量
    /// </summary>
    public class VarTransform : Variable<Transform>
    {
        public static VarTransform Alloc()
        {
            //要从对象池获取
            VarTransform var = GameEntry.Pool.DequeueVarObject<VarTransform>();
            var.Value = default(Transform);
            var.Retain();
            return var;
        }

        /// <summary>
        /// 分配一个对象
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static VarTransform Alloc(Transform value)
        {
            //要从对象池获取
            VarTransform var = Alloc();
            var.Value = value;
            return var;
        }

        public static implicit operator Transform(VarTransform value)
        {
            return value.Value;
        }
    }
}
