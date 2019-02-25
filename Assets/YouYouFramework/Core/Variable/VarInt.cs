using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YouYouFramework
{
    /// <summary>
    /// int变量
    /// </summary>
    public class VarInt : Variable<int>
    {
        public static VarInt Alloc()
        {
            //要从对象池获取
            VarInt var = GameEntry.Pool.DequeueVarObject<VarInt>();
            var.Retain();
            return var;
        }

        /// <summary>
        /// 分配一个对象
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static VarInt Alloc(int value)
        {
            //要从对象池获取
            VarInt var = Alloc();
            var.Value = value;
            return var;
        }

        public static implicit operator int(VarInt value)
        {
            return value.Value;
        }
    }
}
