using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YouYouFramework;

public class TestScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.S))
        {
            GameEntry.Scene.LoadScene(1);
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            GameEntry.Scene.LoadScene(2);
        }
	}
}
