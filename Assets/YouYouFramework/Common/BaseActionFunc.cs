//===================================================
//作    者：边涯  http://www.u3dol.com
//创建时间：
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

namespace YouYouFramework
{
    [CSharpCallLua]
    public delegate void BaseAction();

    [CSharpCallLua]
    public delegate void BaseAction<T1>(T1 t1);

    [CSharpCallLua]
    public delegate void BaseAction<T1, T2>(T1 t1, T2 t2);

    [CSharpCallLua]
    public delegate void BaseAction<T1, T2, T3>(T1 t1, T2 t2, T3 t3);

    [CSharpCallLua]
    public delegate void BaseAction<T1, T2, T3, T4>(T1 t1, T2 t2, T3 t3, T4 t4);

    [CSharpCallLua]
    public delegate void BaseAction<T1, T2, T3, T4, T5>(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5);

    [CSharpCallLua]
    public delegate void BaseAction<T1, T2, T3, T4, T5, T6>(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6);

    [CSharpCallLua]
    public delegate void BaseAction<T1, T2, T3, T4, T5, T6, T7>(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7);

    [CSharpCallLua]
    public delegate void BaseAction<T1, T2, T3, T4, T5, T6, T7, T8>(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8);

    [CSharpCallLua]
    public delegate void BaseAction<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9);

    [CSharpCallLua]
    public delegate void BaseAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10);

    [CSharpCallLua]
    public delegate void BaseAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11);

    [CSharpCallLua]
    public delegate void BaseAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12);

    [CSharpCallLua]
    public delegate void BaseAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13);

    [CSharpCallLua]
    public delegate void BaseAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13, T14 t14);

    [CSharpCallLua]
    public delegate void BaseAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13, T14 t14, T15 t15);

    [CSharpCallLua]
    public delegate void BaseAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13, T14 t14, T15 t15, T16 t16);


    [CSharpCallLua]
    public delegate R BaseFunc<out R>();

    [CSharpCallLua]
    public delegate R BaseFunc<T1, out R>(T1 t1);

    [CSharpCallLua]
    public delegate R BaseFunc<T1, T2, out R>(T1 t1, T2 t2);

    [CSharpCallLua]
    public delegate R BaseFunc<T1, T2, T3, out R>(T1 t1, T2 t2, T3 t3);

    [CSharpCallLua]
    public delegate R BaseFunc<T1, T2, T3, T4, out R>(T1 t1, T2 t2, T3 t3, T4 t4);

    [CSharpCallLua]
    public delegate R BaseFunc<T1, T2, T3, T4, T5, out R>(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5);

    [CSharpCallLua]
    public delegate R BaseFunc<T1, T2, T3, T4, T5, T6, out R>(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6);

    [CSharpCallLua]
    public delegate R BaseFunc<T1, T2, T3, T4, T5, T6, T7, out R>(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7);

    [CSharpCallLua]
    public delegate R BaseFunc<T1, T2, T3, T4, T5, T6, T7, T8, out R>(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8);

    [CSharpCallLua]
    public delegate R BaseFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, out R>(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9);

    [CSharpCallLua]
    public delegate R BaseFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, out R>(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10);

    [CSharpCallLua]
    public delegate R BaseFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, out R>(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11);

    [CSharpCallLua]
    public delegate R BaseFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, out R>(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12);

    [CSharpCallLua]
    public delegate R BaseFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, out R>(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13);

    [CSharpCallLua]
    public delegate R BaseFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, out R>(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13, T14 t14);

    [CSharpCallLua]
    public delegate R BaseFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, out R>(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13, T14 t14, T15 t15);

    [CSharpCallLua]
    public delegate R BaseFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, out R>(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13, T14 t14, T15 t15, T16 t16);
}