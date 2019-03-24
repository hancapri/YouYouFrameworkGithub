using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YouYouFramework;

public class TestSocket : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.S))
        {
            GameEntry.Socket.ConnectToMainSocket("192.168.0.106", 1010);
        }
	}
}
