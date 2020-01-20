using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YouYouFramework;
using UnityEngine.UI;
using System;

public class UICheckVersionForm : MonoBehaviour {
    [SerializeField]
    private Text txtTip;

    [SerializeField]
    private Scrollbar scrollbar;

    private BaseParams args;

    private  void Start()
    {
        GameEntry.Event.CommonEvent.AddEventListener(SysEventId.CheckVersionBeginDownload,OnCheckVersionBeginDownload);
        GameEntry.Event.CommonEvent.AddEventListener(SysEventId.CheckVersionDownloadUpdate, OnCheckVersionDownloadUpdate);
        GameEntry.Event.CommonEvent.AddEventListener(SysEventId.CheckVersionDownloadComplete, OnCheckVersionDownloadComplete);

        txtTip.gameObject.SetActive(false);
        scrollbar.gameObject.SetActive(false);
    }

    private void OnCheckVersionDownloadComplete(object param)
    {
        
    }

    private void OnCheckVersionDownloadUpdate(object param)
    {
        args = param as BaseParams;

        txtTip.text = string.Format("正在下载{0}/{1}",args.IntParam1,args.IntParam2);
        scrollbar.value = (float)args.IntParam1 / args.IntParam2;
    }

    private void OnCheckVersionBeginDownload(object param)
    {
        txtTip.gameObject.SetActive(true);
        scrollbar.gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        GameEntry.Event.CommonEvent.RemoveEventListener(SysEventId.CheckVersionBeginDownload, OnCheckVersionBeginDownload);
        GameEntry.Event.CommonEvent.RemoveEventListener(SysEventId.CheckVersionBeginDownload, OnCheckVersionDownloadUpdate);
        GameEntry.Event.CommonEvent.RemoveEventListener(SysEventId.CheckVersionBeginDownload, OnCheckVersionDownloadComplete);
    }
}
