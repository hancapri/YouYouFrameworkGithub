using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

public class TestAsync : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Task.Factory.StartNew(DebugMore);
        //DebugMore();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DebugMore()
    {
        for (int i = 0; i < 10000; i++)
        {
            Debug.Log(i);
        }
    }
}
