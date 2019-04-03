using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YouYouFramework;

public class UITaskForm : UIFormBase {

    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
        Debug.Log("OnInit");
    }
    protected override void OnOpen(object userData)
    {
        base.OnOpen(userData);
        Debug.Log("OnOpen");
    }
    protected override void OnClose()
    {
        base.OnClose();
        Debug.Log("OnClose");
    }
    protected override void OnBeforeDestroy()
    {
        base.OnBeforeDestroy();
        Debug.Log("OnBeforeDestroy");
    }
}
