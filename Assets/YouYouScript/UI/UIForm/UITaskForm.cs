using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YouYouFramework;

public class UITaskForm : UIFormBase,IUpdateComponent {
    private List<ServerTaskEntity> m_ServerTaskList;

    public int InstanceID
    {
        get { return GetInstanceID(); }
    }

    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
        Debug.Log("OnInit");
    }
    protected override void OnOpen(object userData)
    {
        base.OnOpen(userData);
        Debug.Log("OnOpen");
        GameEntry.RegisterUpdateComponent(this);
    }

    public void OnUpdate()
    {
        Debug.Log("某个UI的update");
    }

    protected override void OnClose()
    {
        base.OnClose();
        Debug.Log("OnClose");
        GameEntry.RemoveUpdateComponent(this);
    }
    protected override void OnBeforeDestroy()
    {
        base.OnBeforeDestroy();
        Debug.Log("OnBeforeDestroy");
    }

    private void LoadTaskList()
    {
        m_ServerTaskList = GameEntry.Data.UserDataManager.ServerTaskList;
    }
}
