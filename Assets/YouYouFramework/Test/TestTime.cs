using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YouYouFramework;

public class TestTime : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.T))
        {
            //TimeAction action1 = GameEntry.Time.CreatTimeAction();
            //Debug.Log("创建第一个定时器");
            //action1.Init(1,0.5f,8,()=> 
            //{
            //    Debug.Log("开始执行");
            //},
            //(int loop)=> 
            //{
            //    Debug.Log("执行中，剩余次数：" + loop);
            //},
            //()=> 
            //{
            //    Debug.Log("结束执行");
            //}).Run();

            //TimeAction action2 = GameEntry.Time.CreatTimeAction();
            //Debug.Log("创建第二个定时器");
            //action2.Init(2, 1, 4, () =>
            //{
            //    Debug.Log("开始执行");
            //},
            //(int loop) =>
            //{
            //    Debug.Log("执行中，剩余次数：" + loop);
            //},
            //() =>
            //{
            //    Debug.Log("结束执行");
            //}).Run();

            TimeAction action1 = GameEntry.Time.CreatTimeAction();
            Debug.Log("创建第三个定时器");
            action1.Init(3, 0.5f, 1, () =>
            {
                Debug.Log("开始执行");
            },
            (int loop) =>
            {
                Debug.Log("执行中，剩余次数：" + loop);
            },
            () =>
            {
                Debug.Log("结束执行");
            }).Run();
        }
	}
}
