using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YouYouFramework
{
    /// <summary>
    /// float变量
    /// </summary>
    public class VarFloat : Variable<float>
    {
        public static VarFloat Alloc()
        {
            //要从对象池获取
            VarFloat var = GameEntry.Pool.DequeueVarObject<VarFloat>();
            var.Value = default(float);
            var.Retain();
            return var;
        }

        /// <summary>
        /// 分配一个对象
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static VarFloat Alloc(float value)
        {
            //要从对象池获取
            VarFloat var = Alloc();
            var.Value = value;
            return var;
        }

        public static implicit operator float(VarFloat value)
        {
            return value.Value;
        }
    }
}
