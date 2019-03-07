using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YouYouFramework
{
    /// <summary>
    /// bool变量
    /// </summary>
    public class VarBool : Variable<bool>
    {
        public static VarBool Alloc()
        {
            //要从对象池获取
            VarBool var = GameEntry.Pool.DequeueVarObject<VarBool>();
            var.Value = default(bool);
            var.Retain();
            return var;
        }

        /// <summary>
        /// 分配一个对象
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static VarBool Alloc(bool value)
        {
            //要从对象池获取
            VarBool var = Alloc();
            var.Value = value;
            return var;
        }

        public static implicit operator bool(VarBool value)
        {
            return value.Value;
        }
    }
}
