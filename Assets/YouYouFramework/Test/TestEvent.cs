using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YouYouFramework;

public class TestEvent : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameEntry.Event.CommonEvent.AddEventListener(CommonEventId.RegComplete, ResComplete);
    }

    private void ResComplete(object param)
    {
        Debug.Log(param);
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.E))
        {
            GameEntry.Event.CommonEvent.Dispatch(CommonEventId.RegComplete,13);
        }
	}

    void OnDestroy()
    {
        GameEntry.Event.CommonEvent.RemoveEventListener(CommonEventId.RegComplete, ResComplete);
    }
}
