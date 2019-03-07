using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YouYouFramework
{
    /// <summary>
    /// string变量
    /// </summary>
    public class VarString : Variable<string>
    {
        public static VarString Alloc()
        {
            //要从对象池获取
            VarString var = GameEntry.Pool.DequeueVarObject<VarString>();
            var.Value = default(string);
            var.Retain();
            return var;
        }

        /// <summary>
        /// 分配一个对象
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static VarString Alloc(string value)
        {
            //要从对象池获取
            VarString var = Alloc();
            var.Value = value;
            return var;
        }

        public static implicit operator string(VarString value)
        {
            return value.Value;
        }
    }
}
