using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YouYouFramework;

public class TestUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.U))
        {
            GameEntry.UI.OpenUIForm(UIFormId.UI_GameLevelMap);
        }
	}
}
