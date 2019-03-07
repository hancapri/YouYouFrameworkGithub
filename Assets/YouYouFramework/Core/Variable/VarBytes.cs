using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YouYouFramework
{
    /// <summary>
    /// bytes变量
    /// </summary>
    public class VarBytes : Variable<byte[]>
    {
        public static VarBytes Alloc()
        {
            //要从对象池获取
            VarBytes var = GameEntry.Pool.DequeueVarObject<VarBytes>();
            var.Value = default(byte[]);
            var.Retain();
            return var;
        }

        /// <summary>
        /// 分配一个对象
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static VarBytes Alloc(byte[] value)
        {
            //要从对象池获取
            VarBytes var = Alloc();
            var.Value = value;
            return var;
        }

        public static implicit operator byte[] (VarBytes value)
        {
            return value.Value;
        }
    }
}
