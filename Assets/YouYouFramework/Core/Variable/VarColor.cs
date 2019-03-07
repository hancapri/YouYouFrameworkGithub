using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YouYouFramework
{
    /// <summary>
    /// color变量
    /// </summary>
    public class VarColor : Variable<Color>
    {
        public static VarColor Alloc()
        {
            //要从对象池获取
            VarColor var = GameEntry.Pool.DequeueVarObject<VarColor>();
            var.Value = default(Color);
            var.Retain();
            return var;
        }

        /// <summary>
        /// 分配一个对象
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static VarColor Alloc(Color value)
        {
            //要从对象池获取
            VarColor var = Alloc();
            var.Value = value;
            return var;
        }

        public static implicit operator Color(VarColor value)
        {
            return value.Value;
        }
    }
}
