using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YouYouFramework;

public class TestPool : MonoBehaviour {

    class Person
    {
        public int Age;
        public string Name;
        public void Init(int age, string name)
        {
            Age = age;
            Name = name;
        }
    }
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Person p = GameEntry.Pool.ClassObjectPool.Dequeue<Person>();
            p.Init(1,"zhang");
            Debug.Log(p.Age);
            Debug.Log(p.Name);
            GameEntry.Pool.ClassObjectPool.Enqueue(p);
        }
	}
}
