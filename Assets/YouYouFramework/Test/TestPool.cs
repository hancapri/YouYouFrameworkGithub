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

    class Animal
    {
        public int Age;
        public string Name;
        public void Init(int age, string name)
        {
            Age = age;
            Name = name;
        }
    }

    public Transform trans1;
    public Transform trans2;

    void Start () {
        GameEntry.Pool.SetClassObjectResideCount<Person>(2);
        GameEntry.Pool.SetClassObjectResideCount<Animal>(3);
    }
	
	// Update is called once per frame
	void Update () {
        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    Person p = GameEntry.Pool.DequeueClassObject<Person>();
        //    p.Init(1,"zhang");
        //    Debug.Log(p.Age);
        //    Debug.Log(p.Name);
        //    StartCoroutine(EnqueueClassObject(p, 5));

        //    Animal a = GameEntry.Pool.DequeueClassObject<Animal>();
        //    a.Init(1, "cat");
        //    Debug.Log(a.Age);
        //    Debug.Log(a.Name);
        //    StartCoroutine(EnqueueClassObject(a, 5));
        //}

        if (Input.GetKeyDown(KeyCode.P))
        {
            StartCoroutine(CreatObj());
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            GameEntry.Pool.InitGameObjectPool();
        }
    }

    IEnumerator EnqueueClassObject(object obj, float time)
    {
        yield return new WaitForSeconds(time);
        GameEntry.Pool.EnqueueClassObject(obj);
    }

    IEnumerator CreatObj()
    {
        for (int i = 0; i < 20; i++)
        {
            yield return new WaitForSeconds(0.5f);
            GameEntry.Pool.GameObjectSpawn(1, trans1, (Transform instance)=> {
                instance.transform.position += new Vector3(0,0,i*2);
                StartCoroutine(DespawnObj(1, instance));
            });

            GameEntry.Pool.GameObjectSpawn(2, trans2, (Transform instance) => {
                instance.transform.position += new Vector3(0, 2, i * 2);
                StartCoroutine(DespawnObj(2, instance));
            });
        }
    }

    IEnumerator DespawnObj(byte poolId, Transform instance)
    {
        yield return new WaitForSeconds(10);
        GameEntry.Pool.GameObjectDespawn(poolId, instance);
    }
}
