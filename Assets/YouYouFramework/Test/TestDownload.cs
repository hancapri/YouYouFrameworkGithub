using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YouYouFramework;

public class TestDownload : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.D))
        {
            GameEntry.Download.BeginDownload("download/ssss.rar");
        }
	}
}
