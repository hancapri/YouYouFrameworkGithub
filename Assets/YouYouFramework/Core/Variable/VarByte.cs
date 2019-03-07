using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YouYouFramework
{
    /// <summary>
    /// byte变量
    /// </summary>
    public class VarByte : Variable<byte>
    {
        public static VarByte Alloc()
        {
            //要从对象池获取
            VarByte var = GameEntry.Pool.DequeueVarObject<VarByte>();
            var.Value = default(byte);
            var.Retain();
            return var;
        }

        /// <summary>
        /// 分配一个对象
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static VarByte Alloc(byte value)
        {
            //要从对象池获取
            VarByte var = Alloc();
            var.Value = value;
            return var;
        }

        public static implicit operator byte(VarByte value)
        {
            return value.Value;
        }
    }
}
