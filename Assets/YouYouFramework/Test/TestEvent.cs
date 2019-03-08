using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YouYouFramework;

public class TestEvent : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameEntry.Event.CommonEvent.AddEventListener(CommonEventId.RegComplete, ResComplete);
        GameEntry.Event.CommonEvent.AddEventListener(1002, ResCompleteInt);
    }

    private void ResComplete(VariableBase param)
    {
        Variable<int[]> a = param as Variable<int[]>;
        Debug.Log(a.Value[0]);
        Debug.Log(a.ReferenceCount);
    }

    private void ResCompleteInt(VariableBase param)
    {
        VarInt a = param as VarInt;
        Debug.Log(a.Value);
        Debug.Log(a.ReferenceCount);
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Variable<int[]> a = GameEntry.Pool.DequeueVarObject<Variable<int[]>>();
            a.Value = new int[2] { 112, 2 };
            a.Retain();

            GameEntry.Event.CommonEvent.Dispatch(CommonEventId.RegComplete, a);
            a.Release();
            Debug.Log(a.ReferenceCount);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            VarInt s = VarInt.Alloc(111);

            GameEntry.Event.CommonEvent.Dispatch(1002, s);
            s.Release();
            Debug.Log(s.ReferenceCount);
        }
    }

    void OnDestroy()
    {
        GameEntry.Event.CommonEvent.RemoveEventListener(CommonEventId.RegComplete, ResComplete);
    }
}
