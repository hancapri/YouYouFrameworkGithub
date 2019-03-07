using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YouYouFramework
{
    /// <summary>
    /// long变量
    /// </summary>
    public class VarLong : Variable<long>
    {
        public static VarLong Alloc()
        {
            //要从对象池获取
            VarLong var = GameEntry.Pool.DequeueVarObject<VarLong>();
            var.Value = default(long);
            var.Retain();
            return var;
        }

        /// <summary>
        /// 分配一个对象
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static VarLong Alloc(long value)
        {
            //要从对象池获取
            VarLong var = Alloc();
            var.Value = value;
            return var;
        }

        public static implicit operator long(VarLong value)
        {
            return value.Value;
        }
    }
}
